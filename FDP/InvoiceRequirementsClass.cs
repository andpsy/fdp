using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace FDP
{
    static class InvoiceRequirementsClass
    {
        public static void InsertFromRentContract(DataRow contract)
        {
            //string contract_status = Convert.ToString(contract["status"]).ToLower();
            string contract_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString().ToLower();

            DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
            DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
            bool _generate_vat = (contract["rent_vat_included"] != DBNull.Value && contract["rent_vat_included"] != null && Convert.ToDouble(contract["rent_vat_included"]) > 0) ? true : false;

            /*
            DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
            bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(contract_expiration_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);
            */
            //ArrayList months = CommonFunctions.DateDifferenceInMonths(CommonFunctions.FromMySqlFormatDate(contract["finish_date"].ToString()), CommonFunctions.FromMySqlFormatDate(contract["start_date"].ToString()));
            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
            /*
            int prevoius_tenant_id = 0;
            bool new_tenant = false;
            if (contract["parent_contract_id"] != DBNull.Value && contract["parent_contract_id"] != null)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) });
                DataRow parent_contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
                prevoius_tenant_id = Convert.ToInt32(parent_contract["tenant_id"]);
                new_tenant = (prevoius_tenant_id>0 && prevoius_tenant_id!=Convert.ToInt32(contract["tenant_id"]));
            }
            */
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", contract["property_id"]) })).ExecuteSelectQuery().Tables[0];
            object contract_id = null;
            DataRow service = null;
            if (contract_services.Rows.Count > 0){
                contract_id = contract_services.Rows[0]["contract_id"];
                try
                {
                    service = contract_services.Select("service = 'Rent Management'")[0];
                }
                catch { service = null; }
            }
            if (service != null)
            {
                try
                {
                    //Delete any predicted ie generated from fdp contract as additional cost
                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status_by_property_service", new object[] { 
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]), 
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent Management") })).ExecuteScalarQuery())
                    })).ExecuteUpdateQuery();

                    (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_change_status_by_property_service", new object[] { 
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]), 
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent Management") })).ExecuteScalarQuery()) 
                    })).ExecuteUpdateQuery();
                }
                catch { }

                // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                double percent = Convert.ToDouble(service["value"]);
                bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null? false: service["percent"]);
                double value = procent ? Convert.ToDouble(contract["rent"]) * percent / 100 : percent;
                bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);
                int month_counter = 0;
                foreach (string month in months)
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent Management")})).ExecuteScalarQuery() ),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* ( Convert.ToBoolean(contract["prolongation"]) && !new_tenant ?0.5:1)),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* percent/100),
                        new MySqlParameter("_PRICE", value),
                        //TO CHECK IF THE VALUE IS SPLITTED ALSO FOR RENT MANAGEMENT FOR PROLONGATION OR SAME OWNER
                        new MySqlParameter("_MONTH", month),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(contract_start_date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    /*
                    da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", false),
                        new MySqlParameter("_CURRENCY", new_ir["currency"]),
                        new MySqlParameter("_AMOUNT", new_ir["price"]),
                        new MySqlParameter("_DATE", new_ir["date"]),
                        new MySqlParameter("_OWNER_ID", new_ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", new_ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", new_ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", new_ir["comments"]),
                        new MySqlParameter("_MONTH", new_ir["month"]),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"])
                    });
                    da.ExecuteInsertQuery();
                    */
                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                    //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]) - Convert.ToDouble(new_ir["price"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);
                    //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"], null, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "New"), new MySqlParameter("_LIST_TYPE", "predicted_ie_status") })).ExecuteScalarQuery());
                    double _vat = 0;
                    double _amount_total = Convert.ToDouble(contract["rent"]);
                    if (_generate_vat)
                    {
                        _vat = Convert.ToBoolean(new_ir["not_invoiceable"] == DBNull.Value ? false : new_ir["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(contract["rent"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                        _amount_total = Convert.ToDouble(contract["rent"]) + _vat;
                    }
                    if(months.Count==13 && month_counter < 12)  // generez Income for Rent for Owner doar pe 12 luni !!! - 26.04.2016
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["rent"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["rent"]),_vat,_amount_total);

                    // insert Income for tenant deposit !!! - from 15.05.2013 
                    if (months.IndexOf(month) == 0 && contract["deposit"] != DBNull.Value && Validator.IsDouble(contract["deposit"].ToString()))
                    {
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["deposit"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], String.Format("DEPOSIT - {0}", new_ir["comments"]), DBNull.Value, new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["deposit"]), 0, Convert.ToDouble(contract["deposit"]));
                    }
                    month_counter++;
                }
            }
            try
            {
                service = contract_services.Select("service = 'Rent'")[0];
            }
            catch { service = null; }

            //if (contract_services.Select("service = 'Rent'").Length > 0)
            if(service != null)
            {
                try
                {
                    //Delete any predicted ie generated from fdp contract as additional cost
                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status_by_property_service", new object[] { new MySqlParameter("_PROPERTY_ID", contract["property_id"]), new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent") })).ExecuteScalarQuery()) })).ExecuteUpdateQuery();
                    (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_change_status_by_property_service", new object[] { new MySqlParameter("_PROPERTY_ID", contract["property_id"]), new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent") })).ExecuteScalarQuery()) })).ExecuteUpdateQuery();
                }
                catch { }

                //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                foreach (int[] ym in CommonFunctions.DateDifferenceInYears2(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                {
                    double rent_rate;
                    try
                    {
                        //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                        rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))[0]["percent"]) / 100;
                    }
                    catch
                    {
                        rent_rate = 1;
                    }
                    service = contract_services.Select("service = 'Rent'")[0];
                    // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                    double percent = Convert.ToDouble(service["value"]);
                    bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null ? false : service["percent"]);
                    //double value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                    double value = procent?(Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) ? 0.5 : 1) :percent;
                    bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);

                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                        new MySqlParameter("_DATE", ym[1].ToString()==contract_start_date.Year.ToString()?CommonFunctions.ToMySqlFormatDate(contract_start_date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),contract_start_date.Month,contract_start_date.Day).Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    /*
                    da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                            new MySqlParameter("_TYPE", false),
                            new MySqlParameter("_CURRENCY", new_ir["currency"]),
                            new MySqlParameter("_AMOUNT", new_ir["price"]),
                            new MySqlParameter("_DATE", new_ir["date"]),
                            new MySqlParameter("_OWNER_ID", new_ir["owner_id"]),
                            new MySqlParameter("_PROPERTY_ID", new_ir["property_id"]),
                            new MySqlParameter("_CONTRACTSERVICE_ID", new_ir["contractservice_id"]),
                            new MySqlParameter("_SERVICE_DESCRIPTION", new_ir["comments"]),
                            new MySqlParameter("_MONTH", new_ir["month"]),
                            new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"])
                        });
                    da.ExecuteInsertQuery();
                    */
                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                }
            }
            
        }

        public static void UpdateFromRentContract(DataRow contract, bool edit_only)
        {
            //string contract_status = Convert.ToString(contract["status"]).ToLower();
            string contract_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString().ToLower();

            DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
            DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
            bool _generate_vat = (contract["rent_vat_included"] != DBNull.Value && contract["rent_vat_included"] != null && Convert.ToDouble(contract["rent_vat_included"]) > 0) ? true : false;

            /*
            DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
            bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(contract_expiration_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);
            */
            try
            {
                new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_rentcontract_id", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["id"]) }).ExecuteNonQuery();
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            int invoiced_status_id = Convert.ToInt32((new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery());

            //ArrayList months = CommonFunctions.DateDifferenceInMonths(CommonFunctions.FromMySqlFormatDate(contract["finish_date"].ToString()), CommonFunctions.FromMySqlFormatDate(contract["start_date"].ToString()));
            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", contract["property_id"]) })).ExecuteSelectQuery().Tables[0];
            object contract_id = null;
            DataRow service = null;
            if (contract_services != null && contract_services.Rows.Count > 0)
            {
                contract_id = contract_services.Rows[0]["contract_id"];
                service = contract_services.Select("service = 'Rent Management'")[0];
            }
            if (service != null)
            {
                // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                double percent = Convert.ToDouble(service["value"]);
                bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null ? false : service["percent"]);
                double value = procent ? Convert.ToDouble(contract["rent"]) * percent / 100 : percent;
                bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);
                int month_counter = 0;
                foreach (string month in months)
                {
                    /*
                    try
                    {
                        DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month", new object[] {
                            new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                            new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                            new MySqlParameter("_MONTH", month),
                            new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent Management")})).ExecuteScalarQuery())
                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                        (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete", new object[] {
                            new MySqlParameter("_ID", ir["id"]),
                        })).ExecuteUpdateQuery();
                        //value = (Convert.ToDouble(contract["rent"]) * percent / 100) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                        value = (Convert.ToDouble(contract["rent"]) * percent / 100);
                    }
                    catch
                    {
                        value = (Convert.ToDouble(contract["rent"]) * percent / 100);
                    }
                    */ 
                    value = (Convert.ToDouble(contract["rent"]) * percent / 100);

                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent Management")})).ExecuteScalarQuery() ),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* ( Convert.ToBoolean(contract["prolongation"]) && !new_tenant ?0.5:1)),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* percent/100),
                        new MySqlParameter("_PRICE", value),
                        //TO CHECK IF THE VALUE IS SPLITTED ALSO FOR RENT MANAGEMENT FOR PROLONGATION OR SAME OWNER
                        new MySqlParameter("_MONTH", month),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),                       
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(contract_start_date.Date)),                       
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];

                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                    //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]) - Convert.ToDouble(new_ir["price"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);

                    double _vat = 0;
                    double _amount_total = Convert.ToDouble(contract["rent"]);
                    if (_generate_vat)
                    {
                        _vat = Convert.ToBoolean(new_ir["not_invoiceable"] == DBNull.Value ? false : new_ir["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(contract["rent"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                        _amount_total = Convert.ToDouble(contract["rent"]) + _vat;
                    }
                    if (months.Count == 13 && month_counter < 12)  // generez Income for Rent for Owner doar pe 12 luni !!! - 26.04.2016
                    {
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["rent"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["rent"]), _vat, _amount_total);
                    }
                    // insert Income for tenant deposit !!! - from 15.05.2013 
                    if (months.IndexOf(month) == 0 && contract["deposit"] != DBNull.Value && Validator.IsDouble(contract["deposit"].ToString()))
                    {
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["deposit"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], String.Format("DEPOSIT - {0}", new_ir["comments"]), DBNull.Value, new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["deposit"]), 0, Convert.ToDouble(contract["deposit"]));
                    }
                    month_counter++;
                }
            }
            //if (contract_services != null && contract_services.Select("service = 'Rent'").Length > 0)
            if (contract_services != null && contract_services.Rows.Count > 0)
            {
                try
                {
                    contract_id = contract_services.Rows[0]["contract_id"];
                    service = contract_services.Select("service = 'Rent'")[0];
                }
                catch { service = null; }
            }
            if (service != null)
            {
                bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);
                //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                foreach (int[] ym in CommonFunctions.DateDifferenceInYears2(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                {
                    double rent_rate;
                    try
                    {
                        //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                        rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))[0]["percent"]) / 100;
                    }
                    catch
                    {
                        rent_rate = 1;
                    }

                    double percent = 0;
                    service = contract_services.Select("service = 'Rent'")[0];
                    /*
                    try
                    {
                        DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month", new object[] {
                            new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                            new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                            new MySqlParameter("_MONTH", null),
                            new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery())
                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete", new object[] {
                                    new MySqlParameter("_ID", ir["id"]),
                                })).ExecuteUpdateQuery();
                        percent = Convert.ToDouble(service["value"]);
                        //value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                        value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) ? 0.5 : 1);
                    }
                    catch
                    {
                        percent = Convert.ToDouble(service["value"]);
                        //value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                        value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) ? 0.5 : 1);
                    }
                    */ 

                    // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                    percent = Convert.ToDouble(service["value"]);
                    bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null ? false : service["percent"]);
                    double value = procent ? (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) ? 0.5 : 1) : percent;
                    
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                        new MySqlParameter("_DATE", ym[1].ToString()==contract_start_date.Year.ToString()?CommonFunctions.ToMySqlFormatDate(contract_start_date.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),contract_start_date.Month,contract_start_date.Day).Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];

                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, _generate_vat);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, _generate_vat);
                }
            }
        }

        public static void UpdateFromRentContract(DataRow contract) // for Addendums
        {
            #region -- old --
            /*
            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
            int prevoius_tenant_id = 0;
            bool new_tenant = false;
            DataRow parent_contract = null;
            if (contract["parent_contract_id"] != DBNull.Value && contract["parent_contract_id"] != null)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) });
                parent_contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
                prevoius_tenant_id = Convert.ToInt32(parent_contract["tenant_id"]);
                new_tenant = (prevoius_tenant_id > 0 && prevoius_tenant_id != Convert.ToInt32(contract["tenant_id"]));
            }

            if (Convert.ToDateTime(parent_contract["finish_date"]).Month >= Convert.ToDateTime(contract["start_date"]).Month)
            {
                if (System.Windows.Forms.MessageBox.Show(Language.GetMessageBoxText("confirmPreviousRequirementsDeletion", "The new Addendum is overlapping the initial contract. Do you wish to delete previous Invoice Requirements?"), "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ArrayList overlapping_months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(parent_contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
                    foreach (string month in overlapping_months)
                    {
                        //DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_month", new object[] { 
                        //    new MySqlParameter("_PROPERTY_ID", parent_contract["property_id"]), 
                        //    new MySqlParameter("_RENTCONTRACT_ID", parent_contract["id"]), 
                        //    new MySqlParameter("_MONTH", month), 
                        //    new MySqlParameter("_SERVICE_NAME", "Rent Management") });
                        //da.ExecuteUpdateQuery();

                        (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status_by_month", new object[] {
                            new MySqlParameter("_PROPERTY_ID", parent_contract["property_id"]), 
                            new MySqlParameter("_RENTCONTRACT_ID", parent_contract["id"]), 
                            new MySqlParameter("_MONTH", month), 
                            new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[]{new MySqlParameter("_NAME", "Rent management")})).ExecuteScalarQuery()),
                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                            new MySqlParameter("_DELETED", true)                            
                        })).ExecuteUpdateQuery();
                        //TO DO: CHANGE TO CHANGE_STATUS
                    }
                    //TO CHECK IF IS THE CASE TO DELETE ALSO REQUIREMENT FOR 'RENT' SERVICE.
                }
            }
            InsertFromRentContract(contract);
            */
            #endregion

            //string contract_status = Convert.ToString(contract["status"]).ToLower();
            string contract_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString().ToLower();

            DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
            DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
            bool _generate_vat = (contract["rent_vat_included"] != DBNull.Value && contract["rent_vat_included"] != null && Convert.ToDouble(contract["rent_vat_included"]) > 0) ? true : false;

            /*
            DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
            bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(contract_expiration_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);
            */
            int invoiced_status_id = Convert.ToInt32( (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery());
            double value;

            //ArrayList months = CommonFunctions.DateDifferenceInMonths(CommonFunctions.FromMySqlFormatDate(contract["finish_date"].ToString()), CommonFunctions.FromMySqlFormatDate(contract["start_date"].ToString()));
            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
            //int prevoius_tenant_id = 0;
            //bool new_tenant = false;
            /*
            if (contract["parent_contract_id"] != DBNull.Value && contract["parent_contract_id"] != null)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) });
                DataRow parent_contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
                prevoius_tenant_id = Convert.ToInt32(parent_contract["tenant_id"]);
                new_tenant = (prevoius_tenant_id > 0 && prevoius_tenant_id != Convert.ToInt32(contract["tenant_id"]));
            }
            */
            //DataRow parent_contract = null;
            //if (contract["parent_contract_id"] != DBNull.Value && contract["parent_contract_id"] != null)
            //{
            //    DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_number", new object[] { new MySqlParameter("_NUMBER", contract["number"]) });
            //    parent_contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
            //    prevoius_tenant_id = Convert.ToInt32(parent_contract["tenant_id"]);
            //    new_tenant = (prevoius_tenant_id > 0 && prevoius_tenant_id != Convert.ToInt32(contract["tenant_id"]));
            //}
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", contract["property_id"]) })).ExecuteSelectQuery().Tables[0];
            object contract_id = null;
            DataRow service = null;
            if (contract_services.Rows.Count > 0)
            {
                try
                {
                    contract_id = contract_services.Rows[0]["contract_id"];
                    service = contract_services.Select("service = 'Rent Management'")[0];
                }
                catch { service = null; }
            }
            if (service != null)
            {
                // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                double percent = Convert.ToDouble(service["value"]);
                bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null ? false : service["percent"]);
                value = procent ? Convert.ToDouble(contract["rent"]) * percent / 100 : percent;
                bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);
                int month_counter = 0;

                foreach (string month in months)
                {
                    try
                    {
                        DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month", new object[] {
                            new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                            new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                            new MySqlParameter("_MONTH", month),
                            new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent Management")})).ExecuteScalarQuery())
                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                        {
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                            new MySqlParameter("_ID", ir["id"]),
                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                            new MySqlParameter("_DELETED", true)                            
                        })).ExecuteUpdateQuery();
                            //value = (Convert.ToDouble(contract["rent"]) * percent / 100) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                            //value = (Convert.ToDouble(contract["rent"]) * percent / 100);
                        }
                        else // existing IR is invoiced
                        {
                            double old_value = Convert.ToDouble(ir["price"]);
                            //value = (Convert.ToDouble(contract["rent"]) * percent / 100) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1) - old_value;
                            //value = (Convert.ToDouble(contract["rent"]) * percent / 100) - old_value;
                            value -= old_value;
                        }
                    }
                    catch
                    {
                        //value = (Convert.ToDouble(contract["rent"]) * percent / 100);
                    }
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent Management")})).ExecuteScalarQuery() ),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* ( Convert.ToBoolean(contract["prolongation"]) && !new_tenant ?0.5:1)),
                        //new MySqlParameter("_PRICE", Convert.ToDouble(contract["rent"])* percent/100),
                        new MySqlParameter("_PRICE", value),
                        //TO CHECK IF THE VALUE IS SPLITTED ALSO FOR RENT MANAGEMENT FOR PROLONGATION OR SAME OWNER
                        new MySqlParameter("_MONTH", month),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),                       
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(contract_start_date.Date)),                       
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];

                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                    //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]) - Convert.ToDouble(new_ir["price"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);
                    double _vat = 0;
                    double _amount_total = Convert.ToDouble(contract["rent"]);
                    if (_generate_vat)
                    {
                        _vat = Convert.ToBoolean(new_ir["not_invoiceable"] == DBNull.Value ? false : new_ir["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(contract["rent"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                        _amount_total = Convert.ToDouble(contract["rent"]) + _vat;
                    }
                    if (months.Count == 13 && month_counter < 12)  // generez Income for Rent for Owner doar pe 12 luni !!! - 26.04.2016
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["rent"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["rent"]), _vat, _amount_total);

                    // insert Income for tenant deposit !!! - from 15.05.2013 
                    if (months.IndexOf(month) == 0 && contract["deposit"] != DBNull.Value && Validator.IsDouble(contract["deposit"].ToString()))
                    {
                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], Convert.ToDouble(contract["deposit"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], String.Format("DEPOSIT - {0}", new_ir["comments"]), DBNull.Value, new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(contract["deposit"]), 0, Convert.ToDouble(contract["deposit"]));
                    }
                    month_counter++;
                }
            }
            //if (contract_services.Select("service = 'Rent'").Length > 0)
            if (contract_services.Rows.Count > 0)
            {
                try
                {
                    contract_id = contract_services.Rows[0]["contract_id"];
                    service = contract_services.Select("service = 'Rent'")[0];
                }
                catch { service = null; }
            }
            if (service != null)
            {
                value = 0;
                try
                {
                    DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month", new object[] {
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_MONTH", null),
                        new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery())
                    })).ExecuteSelectQuery().Tables[0].Rows[0];
                    if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                    {
                        (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                            new MySqlParameter("_ID", ir["id"]),
                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                            new MySqlParameter("_DELETED", true)                            
                        })).ExecuteUpdateQuery();
                    }
                    else // existing IR is invoiced
                    {
                        double old_value = Convert.ToDouble(ir["price"]);
                        value -= old_value;
                    }
                }
                catch
                {
                }
                bool not_invoiceable = Convert.ToBoolean(service["not_invoiceable"] == DBNull.Value ? false : service["not_invoiceable"]);
                //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                foreach (int[] ym in CommonFunctions.DateDifferenceInYears2(Convert.ToDateTime(contract["finish_date"]), Convert.ToDateTime(contract["start_date"])))
                {
                    double rent_rate;
                    try
                    {
                        //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                        rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))[0]["percent"]) / 100;
                    }
                    catch
                    {
                        rent_rate = 1;
                    }

                    double percent = 0;
                    service = contract_services.Select("service = 'Rent'")[0];
                    // FROM 29.01.2013 - THERE CAN BE THE CASE OF FIXED AMOUNT AS RENT MANAGEMENT INSTEAD OF THE USUAL PERCENT !!!
                    percent = Convert.ToDouble(service["value"]);
                    bool procent = Convert.ToBoolean(service["percent"] == DBNull.Value || service["percent"] == null ? false : service["percent"]);
                    //value = (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) && !new_tenant ? 0.5 : 1);
                    value = procent ? (Convert.ToDouble(contract["rent"]) * percent / 100 * rent_rate) * (Convert.ToBoolean(contract["prolongation"]) ? 0.5 : 1) : percent;
                    /*
                    try
                    {
                        DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month", new object[] {
                            new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                            new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                            new MySqlParameter("_MONTH", null),
                            new MySqlParameter("_SERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery())
                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                        {
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", ir["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                        }
                        else // existing IR is invoiced
                        {
                            double old_value = Convert.ToDouble(ir["price"]);
                            value -= old_value;
                        }
                    }
                    catch
                    {
                    }
                    */
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", contract["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0]["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", contract["property_id"]),
                        new MySqlParameter("_CONTRACT_ID", contract_id),
                        new MySqlParameter("_RENTCONTRACT_ID", contract["id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Rent")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                        new MySqlParameter("_DATE", ym[1].ToString()==contract_start_date.Year.ToString()?CommonFunctions.ToMySqlFormatDate(contract_start_date.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),contract_start_date.Month,contract_start_date.Day).Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];

                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                }
            }
        }


        public static void InsertFromFDPContract(DataRow contract, DataRow property)
        {
            //string contract_status = Convert.ToString(contract["status"]).ToLower();
            string contract_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString().ToLower();

            DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
            DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
            DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
            bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
            bool use_expiration_date = Convert.ToBoolean(contract["use_expiration_date"] == DBNull.Value || contract["use_expiration_date"] == null ? false : contract["use_expiration_date"]);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_contract_id_property_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]), new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];

            double value = 0;
            if (contract_services != null && contract_services.Rows.Count > 0)
            {
                foreach (int[] months_year in months_years)
                {
                    DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                    DateTime end_date = start_date.AddMonths(months_year[0]);

                    foreach (DataRow dr in contract_services.Rows)
                    {
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        switch (period)
                        {
                            case "none":
                            default:
                                try
                                {
                                    if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                                    {
                                        if (!Convert.ToBoolean(dr["percent"] == DBNull.Value ? false : dr["percent"])
                                            && dr["SERVICE"].ToString().ToLower().IndexOf("rent") != 0 // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                        )
                                        {
                                            // TO DO: WE SHOULD GENERATE PREDICTED FUTURE IE BUT WE DON'T HAVE THE IR YET ! - AND THEN CHECK WHEN UPDATING / DELETING IRs !!!! - DONE
                                            double _vat = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                            double _amount_total = Convert.ToDouble(dr["value"]) + _vat;
                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), property["owner_id"], property["id"], dr["service_id"], "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), property["owner_id"], property["id"], dr["service_id"], "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                        }
                                    }
                                }
                                catch { }
                                break;
                            case "one time payment":
                                try
                                {
                                    if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                                    {
                                        value = Convert.ToDouble(dr["value"]);
                                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"]),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                            new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_PRICE", value),
                                            new MySqlParameter("_MONTH", DBNull.Value),
                                            //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                            new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                            new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                            new MySqlParameter("_COMMENTS", ""),
                                            //new MySqlParameter("_NOT_INVOICEABLE", false),
                                            new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                            new MySqlParameter("_CURRENCY", contract["currency"])
                                        });
                                        //da.ExecuteInsertQuery();
                                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                    }
                                }
                                catch { }
                                break;
                            case "50%-50%":
                                try
                                {
                                    if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                                    {
                                        value = Math.Round(Convert.ToDouble(dr["value"]) / 2, 2);
                                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"]),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                            new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_PRICE", value),
                                            new MySqlParameter("_MONTH", DBNull.Value),
                                            //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                            new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                            new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                            new MySqlParameter("_COMMENTS", "FIRST 50%"),
                                            //new MySqlParameter("_NOT_INVOICEABLE", false),
                                            new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                            new MySqlParameter("_CURRENCY", contract["currency"])
                                        });
                                        //da.ExecuteInsertQuery();
                                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                        // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, "LAST 50%", true);
                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, "LAST 50%", true);
                                    }
                                }
                                catch (Exception exp) { exp.ToString(); }
                                break;
                            case "per month":
                                value = Convert.ToDouble(dr["value"]);
                                //ArrayList months = CommonFunctions.DateDifferenceInMonths2(Convert.ToDateTime(contract["expiration_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
                                ArrayList months = CommonFunctions.DateDifferenceInMonths2(end_date, start_date);
                                foreach (string month in months)
                                {
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    //new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                    new MySqlParameter("_OWNER_ID", contract["owner_id"]),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"] ),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", month),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                }
                                break;
                            case "per year":
                                //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(contract["expiration_date"]), Convert.ToDateTime(contract["start_date"])))
                                {
                                    double rent_rate;
                                    try
                                    {
                                        //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                                        //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))) / 100;
                                        rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months_year[0].ToString()))[0]["percent"]) / 100;
                                    }
                                    catch
                                    {
                                        rent_rate = 1;
                                    }
                                    value = (Convert.ToDouble(dr["value"]) * rent_rate);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    //new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                    new MySqlParameter("_OWNER_ID", contract["owner_id"]),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"] ),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", DBNull.Value),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(Convert.ToInt32(ym[1]), DateTime.Now.Month, DateTime.Now.Day).Date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(months_year[1], start_date.Month, start_date.Day).Date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);

                                }
                                break;
                        }
                        #region -- old --
                        /*
                    if (dr["period"].ToString().ToLower() != "none")
                    {
                        //DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        if (dr["period"].ToString() == "one time" || dr["period"].ToString() == "per year")
                        {
                            try
                            {
                                value = (dr["period"].ToString() == "one time" ? Math.Round(Convert.ToDouble(dr["value"]) / 2, 2) : Convert.ToDouble(dr["value"]));
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", DBNull.Value),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", dr["period"].ToString() == "one time" ?"FIRST 50%":""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                                IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                                // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                                if (dr["period"].ToString() == "one time")
                                {
                                    IncomeExpensesClass.InsertPredictedIE(false, false, new_ir, "LAST 50%");
                                    IncomeExpensesClass.InsertPredictedIE(true, true, new_ir, "LAST 50%");
                                }
                            }
                            catch { }
                        }
                        else // dr["period"].ToString() == "per month" 
                        {
                            value = Convert.ToDouble(dr["value"]);
                            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["expiration_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
                            foreach (string month in months)
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"] ),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", month),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                                IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                            }
                        }
                    }
                    */
                        #endregion
                    }
                }
            }
        }

        public static void UpdateFromFDPContract(DataRow contract, DataRow property, DataRow old_contract)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_contract_id_property_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]), new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            DataTable contract_invoicerequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_contract_number", new object[] { new MySqlParameter("_CONTRACT_NUMBER", contract["number"]) })).ExecuteSelectQuery().Tables[0];
            object rentcontract_id = null;
            try
            {
                rentcontract_id = contract_invoicerequirements.Select("service = 'Rent'")[0]["rentcontract_id"];
            }
            catch { rentcontract_id = null; }
            
            if (contract_invoicerequirements != null && contract_invoicerequirements.Rows.Count > 0)
            {
                // check the deleted existing services's and delete the coresponding IRs
                //foreach (DataRow dr in contract_invoicerequirements.Rows)
                foreach (DataRow dr in contract_invoicerequirements.Select( ((property["ID"] == DBNull.Value || property["ID"] == null)?"IsNull(property_id, -1) = -1":String.Format("property_id = {0}", property["ID"])) ))
                {
                    if(dr["status_id"].ToString() != (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery().ToString())
                    {
                        if (contract_services == null || contract_services.Select(String.Format("service_id = {0}", dr["contractservice_id"].ToString())).Length < 1) // service was deleted from the contract with the adendum
                        {
                            //DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete", new object[]{new MySqlParameter("_ID", dr["id"])});
                            //da.ExecuteUpdateQuery();
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", dr["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                            dr.Delete();
                        }
                    }
                }
                contract_invoicerequirements.AcceptChanges();
            }

            if ((contract_services != null && contract_services.Rows.Count > 0) &&
                (contract_services.Select("modify = true").Length > 0 || Convert.ToDateTime(contract["expiration_date"]).Date != Convert.ToDateTime(old_contract["expiration_date"]).Date))
            {
                bool contract_date_modified = Convert.ToDateTime(contract["expiration_date"]).Date != Convert.ToDateTime(old_contract["expiration_date"]).Date;
                //foreach (DataRow dr in contract_services.Select("modify = true"))
                foreach (DataRow dr in contract_services.Rows)
                {
                    bool contract_service_modified = (dr["modify"]==DBNull.Value?false:Convert.ToBoolean(dr["modify"])) == true;
                    bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                    if (contract_date_modified && 
                        (contract_invoicerequirements == null || contract_invoicerequirements.Select(String.Format("contractservice_id = {0}", dr["service_id"].ToString())).Length < 1)) // service is new in the adendum
                    {
                        InsertInvoiceRequirement(
                            //Convert.ToString(dr["period"]),
                            (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower(),
                            Convert.ToDouble(dr["value"]),
                            Convert.ToBoolean(dr["percent"]),
                            property["id"],
                            contract["id"],
                            (dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent Management") })).ExecuteScalarQuery().ToString() ||
                                    dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent") })).ExecuteScalarQuery().ToString())?rentcontract_id:DBNull.Value,
                            dr["service_id"],
                            (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery(),
                            //property["owner_id"],
                            contract["owner_id"],
                            DateTime.Now.Date,
                            //false,
                            not_invoiceable,
                            contract["currency"].ToString(),
                            contract["start_date"],
                            contract["finish_date"],
                            contract["expiration_date"],
                            contract["automatically_renewed"],
                            contract["use_expiration_date"]
                            );
                    }
                    else // service or date is modified
                    {
                        if (contract_service_modified || (contract_date_modified &&
                            (((new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower() == "none") ||
                            ((new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower() == "per month") ||
                            ((new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower() == "per year")
                            )))
                        {
                            /*
                            DateTime d1 = new DateTime();
                            DateTime d2 = new DateTime();
                            if (contract_service_modified)
                            {
                                d1 = Convert.ToDateTime(contract["start_date"]);
                                d2 = Convert.ToDateTime(contract["expiration_date"]);
                            }
                            else{
                                d1 = Convert.ToDateTime(contract["start_date"]);
                                d2 = Convert.ToDateTime(contract["expiration_date"])<Convert.ToDateTime(old_contract["expiration_date"])?
                                }
                            */
                            UpdateInvoiceRequirement(
                                //Convert.ToString(dr["period"]),
                                (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower(),
                                Convert.ToDouble(dr["value"]),
                                Convert.ToBoolean(dr["percent"]),
                                property["id"],
                                contract["id"],
                                (dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent Management") })).ExecuteScalarQuery().ToString() ||
                                        dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent") })).ExecuteScalarQuery().ToString()) ? rentcontract_id : DBNull.Value,
                                dr["service_id"],
                                (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery(),
                                //property["owner_id"],
                                contract["owner_id"],
                                DateTime.Now.Date,
                                //false,
                                not_invoiceable,
                                contract["currency"].ToString(),
                                contract["start_date"],
                                contract["finish_date"],
                                contract["expiration_date"],
                                contract["automatically_renewed"],
                                contract["use_expiration_date"]
                                );
                        }
                    }
                }
            }

            #region -- old ---
            /*

            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_contract_id_property_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]), new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            DataTable contract_invoicerequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_by_contract_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;


            if (contract_services != null && contract_services.Rows.Count > 0)
            {
                foreach (DataRow dr in contract_services.Select("modify = 0"))
                {
                    if (dr["period"].ToString().ToLower() != "none")
                    {
                        if (dr["period"].ToString() == "one time" || dr["period"].ToString() == "per year")
                        {
                            try
                            {
                                if (contract_invoicerequirements.Select(String.Format("property_id={0} and contract_id = {1} and contractservice_id = {2}", property["id"], dr["contract_id"], dr["service_id"]))["status_id"])
                                {
                                    value = (dr["period"].ToString() == "one time" ? Math.Round(Convert.ToDouble(dr["value"]) / 2, 2) : Convert.ToDouble(dr["value"]));


                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_property_contract_service", new object[]{
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"])
                                });
                                    da.ExecuteUpdateQuery();

                                    da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", DBNull.Value),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", dr["period"].ToString() == "one time" ?"FIRST 50%":""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                                    IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                                    // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                                    if (dr["period"].ToString() == "one time")
                                    {
                                        IncomeExpensesClass.InsertPredictedIE(false, false, new_ir, "LAST 50%");
                                        IncomeExpensesClass.InsertPredictedIE(true, true, new_ir, "LAST 50%");
                                    }
                                }
                            }
                            catch { }
                        }
                        else // dr["period"].ToString() == "per month" 
                        {
                            value = Convert.ToDouble(dr["value"]);
                            ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(contract["finish_date"].ToString()), Convert.ToDateTime(contract["start_date"].ToString()));
                            foreach (string month in months)
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                    new MySqlParameter("_PROPERTY_ID", property["id"]),
                                    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                    new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"] ),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", month),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                    new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", contract["currency"])
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                                IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                            }
                        }

                    }
                }
            }
            */
            #endregion
        }


        public static void UpdateFromFDPContract(DataRow contract, DataRow property, DataRow old_contract, bool only_edit)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_contract_id_property_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]), new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            DataTable contract_invoicerequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_contract_number", new object[] { new MySqlParameter("_CONTRACT_NUMBER", contract["number"]) })).ExecuteSelectQuery().Tables[0];
            object rentcontract_id = null;
            try
            {
                rentcontract_id = contract_invoicerequirements.Select("service = 'Rent'")[0]["rentcontract_id"];
            }
            catch { rentcontract_id = null; }
            // TO DO: To delete also Company and Predicted IE for 'none' service !!!

            if (contract_invoicerequirements != null && contract_invoicerequirements.Rows.Count > 0)
            {
                // check the deleted existing services's and delete the coresponding IRs
                //foreach (DataRow dr in contract_invoicerequirements.Rows)
                foreach (DataRow dr in contract_invoicerequirements.Select(((property["ID"] == DBNull.Value || property["ID"] == null) ? "IsNull(property_id, -1) = -1" : String.Format("property_id = {0}", property["ID"]))))
                {
                    if (dr["status_id"].ToString() != (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery().ToString())
                    {
                        if (contract_services == null || contract_services.Select(String.Format("service_id = {0}", dr["contractservice_id"].ToString())).Length < 1) // service was deleted from the contract with the adendum
                        {
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete", new object[] { new MySqlParameter("_ID", dr["id"]) });
                            da.ExecuteUpdateQuery();
                            /*
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", dr["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                            dr.Delete(); // ??? see if it works like this
                            */
                        }
                    }
                }
                contract_invoicerequirements.AcceptChanges();
            }

            if (contract_services != null && contract_services.Rows.Count > 0)
            {
                foreach (DataRow dr in contract_services.Rows)
                {
                    bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                    InsertInvoiceRequirement(
                        //Convert.ToString(dr["period"]),
                        (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower(),
                        Convert.ToDouble(dr["value"]),
                        Convert.ToBoolean(dr["percent"]),
                        property["id"],
                        contract["id"],
                        (dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent Management") })).ExecuteScalarQuery().ToString() ||
                                dr["service_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rent") })).ExecuteScalarQuery().ToString()) ? rentcontract_id : DBNull.Value,
                        dr["service_id"],
                        (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery(),
                        //property["owner_id"],
                        contract["owner_id"],
                        DateTime.Now.Date,
                        //false,
                        not_invoiceable,
                        contract["currency"].ToString(),
                        contract["start_date"],
                        contract["finish_date"],
                        contract["expiration_date"],
                        contract["automatically_renewed"],
                        contract["use_expiration_date"]
                        );
                }
            }
        }


        public static void InsertInvoiceRequirement(string period, double initial_value, bool percent, object property_id, object contract_id, object rentcontract_id, object service_id, object status_id, object owner_id, DateTime date, bool not_invoiceable, string currency, object initial_start_date, object initial_finish_date, object expiration_date, object automatically_renewed, object use_expiration_date)
        {
            DateTime contract_start_date = Convert.ToDateTime(initial_start_date);
            DateTime contract_finish_date = Convert.ToDateTime(initial_finish_date);
            DateTime contract_expiration_date = Convert.ToDateTime(expiration_date);
            bool contract_automatically_renewed = automatically_renewed == null || automatically_renewed == DBNull.Value ? false : Convert.ToBoolean(automatically_renewed);
            bool use_expiration_date_converted = Convert.ToBoolean(use_expiration_date == null || use_expiration_date == DBNull.Value ? false : use_expiration_date);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date_converted ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);
            string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", service_id) })).ExecuteScalarQuery().ToString();

            foreach (int[] months_year in months_years)
            {
                DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                DateTime end_date = start_date.AddMonths(months_year[0]);

                switch (period)
                {
                    case "none":
                    default:
                        try
                        {
                            if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                            {
                                if (!percent
                                    && service.ToString().ToLower().IndexOf("rent") != 0 // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                )
                                {
                                    // TO DO: WE SHOULD GENERATE PREDICTED FUTURE IE BUT WE DON'T HAVE THE IR YET ! - AND THEN CHECK WHEN UPDATING / DELETING IRs !!!!
                                    double _vat = not_invoiceable ? 0 : Math.Round(initial_value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                    double _amount_total = initial_value + _vat;
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, currency, initial_value, start_date.AddMonths(2), owner_id, property_id, service_id, "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, initial_value, _vat, _amount_total);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, currency, initial_value, start_date.AddMonths(2), owner_id, property_id, service_id, "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, initial_value, _vat, _amount_total);
                                }
                            }
                        }
                        catch { }
                        break;
                    case "one time payment":
                        try
                        {
                            if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", initial_value),
                                    new MySqlParameter("_MONTH", null),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                            }
                        }
                        catch { }
                        break;
                    case "50%-50%":
                        try
                        {
                            if (months_years.IndexOf(months_year) == 0) // !!! If "one time payment" I generate ir and ie only once, although it is "automatically renewed" and for more than  year - BUG #1 / 25.01.2013 !!!
                            {
                                double value = Math.Round(initial_value / 2, 2);
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", null),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", "FIRST 50%"),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, "LAST 50%", true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, "LAST 50%", true);
                            }
                        }
                        catch { }
                        break;
                    case "per month":
                        //ArrayList months = CommonFunctions.DateDifferenceInMonths2(Convert.ToDateTime(expiration_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                        ArrayList months = CommonFunctions.DateDifferenceInMonths2(end_date, start_date);
                        foreach (string nmonth in months)
                        {
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", initial_value),
                                    new MySqlParameter("_MONTH", nmonth),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                        }
                        break;
                    case "per year":
                        //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(expiration_date), Convert.ToDateTime(start_date)))
                        {
                            double rent_rate;
                            try
                            {
                                ////rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                                //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))) / 100;
                                rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months_year[0].ToString()))[0]["percent"]) / 100;
                            }
                            catch
                            {
                                rent_rate = 1;
                            }
                            double value = (Convert.ToDouble(initial_value) * rent_rate);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                            new MySqlParameter("_OWNER_ID", owner_id),
                            new MySqlParameter("_PROPERTY_ID", property_id),
                            new MySqlParameter("_CONTRACT_ID", contract_id),
                            new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                            new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                            new MySqlParameter("_PRICE", value),
                            new MySqlParameter("_MONTH", DBNull.Value),
                            //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                            //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                            //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(Convert.ToInt32(ym[1]), DateTime.Now.Month, DateTime.Now.Day).Date)),
                            new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(months_year[1], start_date.Month, start_date.Day).Date)),
                            new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                            new MySqlParameter("_COMMENTS", ""),
                            //new MySqlParameter("_NOT_INVOICEABLE", false),
                            new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                            new MySqlParameter("_CURRENCY", currency)
                        });
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);

                        }
                        // TO DO: split in years like for months and apply table
                        break;
                }
            }

            #region -- old --
            /*
            if (period.ToLower() != "none")
            {
                if (period.ToLower() == "one time" || period.ToLower() == "per year")
                {
                    try
                    {
                            value = (period.ToLower() == "one time" ? Math.Round(value / 2, 2) : value);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", null),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", period.ToLower() == "one time" ?"FIRST 50%":""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                            IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                            // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                            if (period.ToLower() == "one time")
                            {
                                IncomeExpensesClass.InsertPredictedIE(false, false, new_ir, "LAST 50%");
                                IncomeExpensesClass.InsertPredictedIE(true, true, new_ir, "LAST 50%");
                            }
                    }
                    catch { }
                }
                else // dr["period"].ToString() == "per month" 
                {
                    ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(finish_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                    foreach (string nmonth in months)
                    {
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", nmonth),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                        //da.ExecuteInsertQuery();
                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                        IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                        IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                    }
                }
            }
            */
            #endregion
        }

        public static void UpdateInvoiceRequirement(string period, double initial_value, bool percent, object property_id, object contract_id, object rentcontract_id, object service_id, object status_id, object owner_id, DateTime date, bool not_invoiceable, string currency, object initial_start_date, object initial_finish_date, object expiration_date, object automatically_renewed, object use_expiration_date)
        {
            int invoiced_status_id = Convert.ToInt32( (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery());
            DateTime contract_start_date = Convert.ToDateTime(initial_start_date);
            DateTime contract_finish_date = Convert.ToDateTime(initial_finish_date);
            DateTime contract_expiration_date = Convert.ToDateTime(expiration_date);
            bool contract_automatically_renewed = automatically_renewed == null || automatically_renewed == DBNull.Value ? false : Convert.ToBoolean(automatically_renewed);
            bool use_expiration_date_converted = Convert.ToBoolean(use_expiration_date == null || use_expiration_date == DBNull.Value ? false : use_expiration_date);
            ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date_converted ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);

            foreach (int[] months_year in months_years)
            {
                DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                DateTime end_date = start_date.AddMonths(months_year[0]);

                switch (period)
                {
                    case "none":
                        double new_value = 0;
                        double rent = 0;
                        string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", service_id) })).ExecuteScalarQuery().ToString();

                        if (service != null && service.ToLower().IndexOf("rent") < 0)
                        {
                            {
                                // TO DO: WE SHOULD GENERATE PREDICTED FUTURE IE BUT WE DON'T HAVE THE IR YET ! - AND THEN CHECK WHEN UPDATING / DELETING IRs !!!!
                                double _vat = not_invoiceable ? 0 : Math.Round(initial_value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                double _amount_total = initial_value + _vat;
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, currency, initial_value, start_date.AddMonths(2), owner_id, property_id, service_id, "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, initial_value, _vat, _amount_total);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, currency, initial_value, start_date.AddMonths(2), owner_id, property_id, service_id, "100%", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, initial_value, _vat, _amount_total);
                            }
                        }

                        //ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(expiration_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                        ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(end_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                        if (service != null && service.ToLower() == "rent management")
                        {
                            foreach (string month in months)
                            {
                                try
                                {
                                    DataRow ir = null;
                                    try
                                    {
                                        ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                        new MySqlParameter("_PROPERTY_ID", property_id),
                                        new MySqlParameter("_CONTRACT_ID", contract_id),
                                        new MySqlParameter("_MONTH", month),
                                        new MySqlParameter("_SERVICE_ID", service_id)
                                    })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    }
                                    catch { ir = null; }
                                    bool insert = false;
                                    if (ir != null)
                                    {
                                        rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                        if (Math.Round(Convert.ToDouble(ir["price"]), 2) != Math.Round(rent * initial_value / 100, 2)) // modify only if the values are different
                                        {
                                            insert = true;
                                            if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_month2", new object[] {
                                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                                    new MySqlParameter("_MONTH", month),
                                                    new MySqlParameter("_SERVICE_ID", service_id)
                                                })).ExecuteUpdateQuery();
                                                */
                                                (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                                new MySqlParameter("_ID", ir["id"]),
                                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                                new MySqlParameter("_DELETED", true)                            
                                            })).ExecuteUpdateQuery();
                                                new_value = (rent * initial_value / 100);
                                            }
                                            else // existing IR is invoiced
                                            {
                                                double old_value = Convert.ToDouble(ir["price"]);
                                                new_value = (rent * initial_value / 100) - old_value;
                                            }
                                        }
                                    }
                                    else { insert = false; } // should never come here since the value is not changed through the FDP Contract
                                    if (insert)
                                    {
                                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        new MySqlParameter("_OWNER_ID", owner_id ),
                                        new MySqlParameter("_PROPERTY_ID", property_id),
                                        new MySqlParameter("_CONTRACT_ID", contract_id),
                                        new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", service_id ),
                                        new MySqlParameter("_PRICE", new_value),
                                        //TO CHECK IF THE VALUE IS SPLITTED ALSO FOR RENT MANAGEMENT FOR PROLONGATION OR SAME OWNER
                                        new MySqlParameter("_MONTH", month),
                                        //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                        new MySqlParameter("_STATUS_ID", status_id ),
                                        new MySqlParameter("_COMMENTS", ""),
                                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", currency)
                                    });
                                        //da.ExecuteInsertQuery();
                                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                        //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]) - Convert.ToDouble(new_ir["price"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);
                                        double _vat = not_invoiceable ? 0 : Math.Round(rent * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                        double _amount_total = initial_value + _vat;
                                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, new_ir["currency"], rent, new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"], DBNull.Value, DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, rent,_vat,_amount_total);
                                    }
                                }
                                catch { }
                            }
                        }
                        if (service != null && service.ToLower() == "rent")
                        {
                            double rent_rate;
                            try
                            {
                                rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))[0]["percent"]) / 100;
                            }
                            catch
                            {
                                rent_rate = 1;
                            }
                            try
                            {
                                DataRow ir = null;
                                try
                                {
                                    ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_MONTH", null),
                                    new MySqlParameter("_SERVICE_ID", service_id)
                                })).ExecuteSelectQuery().Tables[0].Rows[0];
                                }
                                catch { ir = null; }

                                bool insert = false;
                                if (ir != null)
                                {
                                    rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                    if (Math.Round(Convert.ToDouble(ir["price"]), 2) != Math.Round(rent * initial_value / 100 * rent_rate, 2)) // modify only if the values are different
                                    {
                                        insert = true;

                                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                        {
                                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                                new MySqlParameter("_ID", ir["id"]),
                                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                                new MySqlParameter("_DELETED", true)                            
                                            })).ExecuteUpdateQuery();
                                            //rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                            new_value = (rent * initial_value / 100 * rent_rate);
                                        }
                                        else // existing IR is invoiced
                                        {
                                            //rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", rentcontract_id) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                            double old_value = Convert.ToDouble(ir["price"]);
                                            new_value = (rent * initial_value / 100 * rent_rate) - old_value;
                                        }
                                    }
                                }
                                else { insert = false; } // should never come here since the value is not changed through the FDP Contract
                                if (insert)
                                {
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id ),
                                    new MySqlParameter("_PRICE", new_value),
                                    new MySqlParameter("_MONTH", DBNull.Value),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id),
                                    new MySqlParameter("_COMMENTS", ""),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                }
                            }
                            catch { }
                        }
                        break;
                    default:
                        // TO DO: CHECK FOR PREDICTED IE AND DELETE / UPDATE !!!
                        break;
                    case "one time payment":
                        try
                        {
                            DataRow ir = null;
                            try
                            {
                                ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_MONTH", null),
                                new MySqlParameter("_SERVICE_ID", service_id)
                            })).ExecuteSelectQuery().Tables[0].Rows[0];
                            }
                            catch { ir = null; }
                            bool insert = false;
                            if (ir != null)
                            {
                                if (Convert.ToDouble(ir["price"]) != initial_value) // modify only if the values are different
                                {
                                    insert = true;
                                    if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                    {
                                        (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                    new MySqlParameter("_ID", ir["id"]),
                                    //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                    new MySqlParameter("_DELETED", true)                            
                                })).ExecuteUpdateQuery();
                                    }
                                    else // existing IR is invoiced
                                    {
                                        double old_value = Convert.ToDouble(ir["price"]);
                                        double value = initial_value - old_value;
                                    }
                                }
                            }
                            else { insert = true; }
                            if (insert)
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", initial_value),
                                    new MySqlParameter("_MONTH", null),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                            }
                        }
                        catch (Exception exp) { exp.ToString(); }
                        break;
                    case "50%-50%":
                        try
                        {
                            bool insert = false;
                            double value = 0;
                            try
                            {
                                DataRow ir = null;
                                try
                                {
                                    ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_MONTH", null),
                                    new MySqlParameter("_SERVICE_ID", service_id)
                                })).ExecuteSelectQuery().Tables[0].Rows[0];
                                }
                                catch { ir = null; }
                                if (ir != null)
                                {
                                    if (Convert.ToDouble(ir["price"]) != initial_value) // modify only if the values are different
                                    {
                                        insert = true;
                                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                        {
                                            value = Math.Round(initial_value / 2, 2);
                                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                        new MySqlParameter("_ID", ir["id"]),
                                        //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                        new MySqlParameter("_DELETED", true)                            
                                    })).ExecuteUpdateQuery();
                                        }
                                        else // existing IR is invoiced
                                        {
                                            double old_value = Convert.ToDouble(ir["price"]);
                                            value = Math.Round(initial_value / 2, 2) - old_value;
                                        }
                                    }
                                }
                                else { insert = true; value = Math.Round(initial_value / 2, 2); }
                            }
                            catch
                            {
                                value = Math.Round(initial_value / 2, 2);
                                insert = true;
                            }
                            if (insert)
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", null),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", "FIRST 50%"),
                                    //new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, "LAST 50%", true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, "LAST 50%", true);
                            }
                        }
                        catch (Exception exp) { exp.ToString(); }
                        break;
                    case "per month":
                        //months = CommonFunctions.DateDifferenceInMonths2(Convert.ToDateTime(expiration_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                        months = CommonFunctions.DateDifferenceInMonths2(end_date, start_date);
                        foreach (string nmonth in months)
                        {
                            try
                            {
                                DataRow ir = null;
                                try
                                {
                                    ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_MONTH", nmonth),
                                    new MySqlParameter("_SERVICE_ID", service_id)
                                })).ExecuteSelectQuery().Tables[0].Rows[0];
                                }
                                catch
                                {
                                    ir = null; // service new in addendum or period extended
                                }

                                bool insert = false;
                                double value = 0;
                                if (ir != null)
                                {
                                    if (Convert.ToDouble(ir["price"]) != initial_value) // modify only if the values are different
                                    {
                                        insert = true;
                                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                        {
                                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                            new MySqlParameter("_ID", ir["id"]),
                                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                            new MySqlParameter("_DELETED", true)                            
                                        })).ExecuteUpdateQuery();
                                        }
                                        else // existing IR is invoiced
                                        {
                                            double old_value = Convert.ToDouble(ir["price"]);
                                            value = initial_value - old_value;
                                        }
                                    }
                                }
                                else { insert = true; }
                                if (insert)
                                {
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", nmonth),
                                    //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(start_date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                }
                                // TO DO: SEE WHAT TO DO WITH IRs THAT ARE NOT OVERLAPPING (BEFORE)
                            }
                            catch (Exception exp) { exp.ToString(); }
                        }
                        break;
                    case "per year":
                        //foreach (int[] ym in CommonFunctions.DateDifferenceInYears(Convert.ToDateTime(expiration_date), Convert.ToDateTime(start_date)))
                        {
                            double rent_rate;
                            try
                            {
                                //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                                //rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", ym[0].ToString()))) / 100;
                                rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months_year[0].ToString()))[0]["percent"]) / 100;
                            }
                            catch
                            {
                                rent_rate = 1;
                            }
                            double value = (Convert.ToDouble(initial_value) * rent_rate);
                            DataRow ir = null;
                            try
                            {
                                ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_MONTH", null),
                                new MySqlParameter("_SERVICE_ID", service_id)
                            })).ExecuteSelectQuery().Tables[0].Rows[0];
                            }
                            catch { ir = null; }
                            bool insert = false;
                            if (ir != null)
                            {
                                if (Convert.ToDouble(ir["price"]) != initial_value) // modify only if the values are different
                                {
                                    insert = true;
                                    if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                                    {
                                        (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                    new MySqlParameter("_ID", ir["id"]),
                                    //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                    new MySqlParameter("_DELETED", true)                            
                                })).ExecuteUpdateQuery();
                                    }
                                    else // existing IR is invoiced
                                    {
                                        double old_value = Convert.ToDouble(ir["price"]);
                                        value = initial_value - old_value;
                                    }
                                }
                            }
                            else { insert = true; }
                            if (insert)
                            {
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                new MySqlParameter("_OWNER_ID", owner_id),
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                new MySqlParameter("_PRICE", value),
                                new MySqlParameter("_MONTH", DBNull.Value),
                                //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                //new MySqlParameter("_DATE", ym[1].ToString()==DateTime.Now.Year.ToString()?CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date):CommonFunctions.ToMySqlFormatDate(new DateTime(Convert.ToInt32(ym[1]),1,1).Date)),
                                //new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(Convert.ToInt32(ym[1]), DateTime.Now.Month, DateTime.Now.Day).Date)),
                                new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(CommonFunctions.GetClosestDate(Convert.ToInt32(months_year[1]), start_date.Month, start_date.Day).Date)),
                                new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                new MySqlParameter("_COMMENTS", ""),
                                //new MySqlParameter("_NOT_INVOICEABLE", false),
                                new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                new MySqlParameter("_CURRENCY", currency)
                            });
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                            }

                        }
                        // TO DO: split in years like for months and apply table
                        break;
                }
            }
            #region -- old --
            /*
            if (period.ToLower() != "none")
            {
                if (period.ToLower() == "one time" || period.ToLower() == "per year")
                {
                    try
                    {
                        try
                        {
                            DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                            new MySqlParameter("_PROPERTY_ID", property_id),
                            new MySqlParameter("_CONTRACT_ID", contract_id),
                            new MySqlParameter("_MONTH", null),
                            new MySqlParameter("_SERVICE_ID", service_id)
                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                            if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                            {
                                value = (period.ToLower() == "one time" ? Math.Round(value / 2, 2) : value);
                                (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", ir["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                            }
                            else // existing IR is invoiced
                            {
                                double old_value = Convert.ToDouble(ir["price"]);
                                value = (period.ToLower() == "one time" ? Math.Round(value / 2, 2) : value) - old_value;
                            }
                        }
                        catch
                        {
                            value = (period.ToLower() == "one time" ? Math.Round(value / 2, 2) : value);
                        }
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id ),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", null),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", period.ToLower() == "one time" ?"FIRST 50%":""),
                                    new MySqlParameter("_NOT_INVOICEABLE", false),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                        //da.ExecuteInsertQuery();
                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                        IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                        IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                        // INSERT PREDICTED EXPENSE FOR THE LAST 50%
                        if (period.ToLower() == "one time")
                        {
                            IncomeExpensesClass.InsertPredictedIE(false, false, new_ir, "LAST 50%");
                            IncomeExpensesClass.InsertPredictedIE(true, true, new_ir, "LAST 50%");
                        }
                    }
                    catch (Exception exp) { exp.ToString(); }
                }
                else // dr["period"].ToString() == "per month" 
                {
                    ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(finish_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                    foreach (string nmonth in months)
                    {
                        try
                        {
                            DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_MONTH", nmonth),
                                new MySqlParameter("_SERVICE_ID", service_id)
                            })).ExecuteSelectQuery().Tables[0].Rows[0];

                            if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                            {
                                (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", ir["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                            }
                            else // existing IR is invoiced
                            {
                                double old_value = Convert.ToDouble(ir["price"]);
                                value = value - old_value;
                            }
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                    new MySqlParameter("_OWNER_ID", owner_id),
                                    new MySqlParameter("_PROPERTY_ID", property_id),
                                    new MySqlParameter("_CONTRACT_ID", contract_id),
                                    new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                    new MySqlParameter("_CONTRACTSERVICE_ID", service_id),
                                    new MySqlParameter("_PRICE", value),
                                    new MySqlParameter("_MONTH", nmonth),
                                    new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                    new MySqlParameter("_STATUS_ID", status_id ),
                                    new MySqlParameter("_COMMENTS", ""),
                                    new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                    new MySqlParameter("_CURRENCY", currency)
                                });
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                            IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);

                            // TO DO: SEE WHAT TO DO WITH IRs THAT ARE NOT OVERLAPPING (BEFORE)
                        }
                        catch { }
                    }
                }
            }
            else //for Rent and Rent Management
            {
                double new_value;
                double rent;
                string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", service_id) })).ExecuteScalarQuery().ToString();

                ArrayList months = CommonFunctions.DateDifferenceInMonths(Convert.ToDateTime(finish_date.ToString()), Convert.ToDateTime(start_date.ToString()));
                if (service != null && service.ToLower() == "rent management")
                {
                    foreach (string month in months)
                    {
                        try
                        {
                            DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_MONTH", month),
                                new MySqlParameter("_SERVICE_ID", service_id)
                            })).ExecuteSelectQuery().Tables[0].Rows[0];

                            if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                            {
                                (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", ir["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                            })).ExecuteUpdateQuery();
                                rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                new_value = (rent * value / 100);
                            }
                            else // existing IR is invoiced
                            {
                                rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                                double old_value = Convert.ToDouble(ir["price"]);
                                new_value = (rent * value / 100) - old_value;
                            }

                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                new MySqlParameter("_OWNER_ID", owner_id ),
                                new MySqlParameter("_PROPERTY_ID", property_id),
                                new MySqlParameter("_CONTRACT_ID", contract_id),
                                new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                                new MySqlParameter("_CONTRACTSERVICE_ID", service_id ),
                                new MySqlParameter("_PRICE", new_value),
                                //TO CHECK IF THE VALUE IS SPLITTED ALSO FOR RENT MANAGEMENT FOR PROLONGATION OR SAME OWNER
                                new MySqlParameter("_MONTH", month),
                                new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                new MySqlParameter("_STATUS_ID", status_id ),
                                new MySqlParameter("_COMMENTS", ""),
                                new MySqlParameter("_NOT_INVOICEABLE", false),
                                new MySqlParameter("_CURRENCY", currency)
                            });
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                            IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                            //IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], Convert.ToDouble(contract["rent"]) - Convert.ToDouble(new_ir["price"]), new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);
                            IncomeExpensesClass.InsertPredictedIE(false, true, new_ir["currency"], rent, new_ir["date"], new_ir["owner_id"], new_ir["property_id"], new_ir["contractservice_id"], new_ir["comments"], new_ir["month"], new_ir["id"]);
                        }
                        catch { }
                    }
                }
                if (service != null && service.ToLower() == "rent")
                {
                    double rent_rate;
                    try
                    {
                        rent_rate = Convert.ToDouble((new DataAccess(CommandType.StoredProcedure, "MONTHLY_RENT_RATESsp_select").ExecuteSelectQuery()).Tables[0].Select(String.Format("months = {0}", months.Count > 12 ? 12 : months.Count))) / 100;
                    }
                    catch
                    {
                        rent_rate = 1;
                    }
                    try
                    {
                        DataRow ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_month2", new object[] {
                            new MySqlParameter("_PROPERTY_ID", property_id),
                            new MySqlParameter("_CONTRACT_ID", contract_id),
                            new MySqlParameter("_MONTH", null),
                            new MySqlParameter("_SERVICE_ID", service_id)
                        })).ExecuteSelectQuery().Tables[0].Rows[0];

                        if (ir["status_id"].ToString() != invoiced_status_id.ToString()) // existing IR is not invoiced
                        {
                            (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status", new object[] {
                                new MySqlParameter("_ID", ir["id"]),
                                //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                                new MySqlParameter("_DELETED", true)                            
                        })).ExecuteUpdateQuery();
                            rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                            new_value = (rent * value / 100 * rent_rate);
                        }
                        else // existing IR is invoiced
                        {
                            rent = Convert.ToDouble(new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", rentcontract_id) }).ExecuteSelectQuery().Tables[0].Rows[0]["rent"]);
                            double old_value = Convert.ToDouble(ir["price"]);
                            new_value = (rent * value / 100 * rent_rate) - old_value;
                        }
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                            new MySqlParameter("_OWNER_ID", owner_id ),
                            new MySqlParameter("_PROPERTY_ID", property_id),
                            new MySqlParameter("_CONTRACT_ID", contract_id),
                            new MySqlParameter("_RENTCONTRACT_ID", rentcontract_id),
                            new MySqlParameter("_CONTRACTSERVICE_ID", service_id ),
                            new MySqlParameter("_PRICE", new_value),
                            new MySqlParameter("_MONTH", DBNull.Value),
                            new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                            new MySqlParameter("_STATUS_ID", status_id),
                            new MySqlParameter("_COMMENTS", ""),
                            new MySqlParameter("_NOT_INVOICEABLE", false),
                            new MySqlParameter("_CURRENCY", currency)
                        });
                        //da.ExecuteInsertQuery();
                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                        IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                        IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                    }
                    catch { }
                }
            }
            */
            #endregion
        }

        #region --- old ---
        /*
        public static void InsertFromProperty(DataRow property)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;
            if (property["apartment_optional_insurance_policy_number"] != DBNull.Value || property["mandatory_insurance_policy_number"] != DBNull.Value)
            {
                try
                {
                    if (contract_services.Select("service = 'Insurance'").Length > 0)
                    {
                        DataRow dr = contract_services.Select("service = 'Insurance'")[0];
                        DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                        da.ExecuteInsertQuery();
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }

            if (property["gas_contract"] != DBNull.Value && property["electricity_contract"] != DBNull.Value)
            {
                try
                {
                    if (contract_services.Select("service = 'Setup Utilities'").Length > 0)
                    {
                        DataRow dr = contract_services.Select("service = 'Utilities'")[0];
                        DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        value = Math.Round(
                            (dr["price_value"] != DBNull.Value && (dr["price_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_value_applicable"]))) ? Convert.ToDouble(dr["price_value"]) :
                                Convert.ToDouble(dr["price_one_value"]), 2);
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                        da.ExecuteInsertQuery();
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }

            if (property["sold_to"] != DBNull.Value && property["selling_price"] != DBNull.Value)
            {
                try
                {
                    if (contract_services.Select("service = 'Sell'").Length > 0)
                    {
                        DataRow dr = contract_services.Select("service = 'Sell'")[0];
                        DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        value = Math.Round(
                            (dr["price_value"] != DBNull.Value && (dr["price_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_value_applicable"]))) ? Convert.ToDouble(dr["price_value"]) :
                                (dr["price_one_value"] != DBNull.Value && (dr["price_one_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_one_value_applicable"]))) ? Convert.ToDouble(dr["price_one_value"]) :
                                    Convert.ToDouble(dr["price_percent"]) / 100 * Convert.ToDouble(property["selling_price"]), 2);
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                        new MySqlParameter("_PRICE", value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", ""),
                        new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                        da.ExecuteInsertQuery();
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }
        }
        */
        #endregion

        public static void InsertFromProperty(DataRow property, DataRow old_property)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;
            // Check furnish to see if we insert Invoice Requirements
            bool new_fc = (bool)(property["furnished_by_company"] == DBNull.Value ? false : property["furnished_by_company"]);
            bool old_fc = (bool)(old_property["furnished_by_company"] == DBNull.Value ? false : old_property["furnished_by_company"]);
            if (new_fc && new_fc != old_fc)
            {
                try
                {
                    if (contract_services.Select("service = 'Furnish'").Length > 0)
                    {
                        //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForFurnish", "Please confirm Invoice Requirement generation for Furnishing service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForFurnishOK", "Invoice Requirement will be generated for Furnishing service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (ans == DialogResult.OK)
                        {
                            DataRow dr = contract_services.Select("service = 'Furnish'")[0];
                            bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                            string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                            if (period == "50%-50%" || period == "none")
                            {
                                DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                /*
                                try
                                {
                                    DataRow future_ir = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_service_id", new object[]{
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%"))
                                    })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_ir_id", new object[]{
                                        new MySqlParameter("_ID", future_ir["ID"]),
                                        new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["ID"])
                                    })).ExecuteUpdateQuery();
                                }
                                catch { }
                                */
                                try
                                {
                                    //if (period == "none")
                                    //{
                                    /*
                                        DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                    DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                    if (pie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                                //new MySqlParameter("_STATUS_ID", DBNull.Value),
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    /*
                                        DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                        DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (cpie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    //}
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }


            // Check insurance to see if we insert Invoice Requirements
            string new_aoipn = property["apartment_optional_insurance_policy_number"].ToString();
            string old_aoipn = old_property["apartment_optional_insurance_policy_number"].ToString();
            string new_mipn = property["mandatory_insurance_policy_number"].ToString();
            string old_mipn = old_property["mandatory_insurance_policy_number"].ToString();
            bool new_idbc = (bool)(property["insurance_done_by_company"] == DBNull.Value ? false : property["insurance_done_by_company"]);
            bool old_idbc = (bool)(old_property["insurance_done_by_company"] == DBNull.Value ? false : old_property["insurance_done_by_company"]);
            if (new_idbc &&
                ((new_aoipn != null && new_aoipn.Trim() != "" && new_aoipn != old_aoipn) ||
                (new_mipn != null && new_mipn.Trim() != "" && new_mipn != old_mipn))
                )
            {
                try
                {
                    if (contract_services.Select("service = 'Insurance'").Length > 0)
                    {
                        DataRow dr = contract_services.Select("service = 'Insurance'")[0];
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        if (period == "50%-50%" || period == "none")
                        {
                            //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForInsurance", "Please confirm Invoice Requirement generation for Insurance service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForInsuranceOK", "Invoice Requirement will be generated for Insurance service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (ans == DialogResult.OK)
                            {
                                DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"] ),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        //new MySqlParameter("_NOT_INVOICEABLE", false),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                try
                                {
                                    //if (period == "none")
                                    //{
                                    /*
                                        DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                        DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (pie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    /*
                                        DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                        DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (cpie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    //}
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }

            // Check utilities contracts to see if we insert Invoice Requirements
            bool new_gcdbc = (bool)(property["gas_contract_done_by_company"] == DBNull.Value ? false : property["gas_contract_done_by_company"]);
            bool new_ecdbc = (bool)(property["electricity_contract_done_by_company"] == DBNull.Value ? false : property["electricity_contract_done_by_company"]);
            string new_gc = property["gas_contract"].ToString();
            string old_gc = old_property["gas_contract"].ToString();
            string new_ec = property["electricity_contract"].ToString();
            string old_ec = old_property["electricity_contract"].ToString();

            if ((new_gcdbc && (new_gc != null && new_gc.Trim() != "" && new_gc != old_gc)) ||
                (new_ecdbc && (new_ec != null && new_ec.Trim() != "" && new_ec != old_ec))
                )
            {
                try
                {
                    if (contract_services.Select("service = 'Setup Utilities'").Length > 0)
                    {
                        DataRow dr = contract_services.Select("service = 'Setup Utilities'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (period == "50%-50%" || period == "none")
                        {

                            DialogResult ans = new DialogResult();
                            if ((new_gc != old_gc && new_ec == old_ec) || (new_gc == old_gc && new_ec != old_ec))
                                ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForUtilities", "Please confirm Invoice Requirement generation for Utilities setup service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            else
                                ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForUtilitiesOK", "Invoice Requirement will be generated for Utilities setup service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (ans == DialogResult.OK)
                            {
                                DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                //value = (dr["price_value"] != DBNull.Value && (dr["price_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_value_applicable"]))) ? Convert.ToDouble(dr["price_value"]) : Convert.ToDouble(dr["price_one_payment"]);
                                value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"] ),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_CURRENCY", contract["currency"]),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable)});
                                //da.ExecuteInsertQuery();
                                DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                try
                                {
                                    //if (period == "none")
                                    //{
                                    /*
                                        DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                       DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (pie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    /*
                                        DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                     */
                                        DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (cpie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID",  cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                    //}
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }


            // Check selling information to see if we insert Invoice Requirements
            try
            {
                bool new_sbc = (bool)(property["sold_by_company"] == DBNull.Value ? false : property["sold_by_company"]);
                DateTime new_sd = new DateTime();
                DateTime old_sd = new DateTime();
                double new_sp, old_sp;
                try
                {
                    new_sd = (DateTime)property["selling_date"];
                    old_sd = (DateTime)old_property["selling_date"];
                }
                catch { }
                new_sp = property["selling_price"] == DBNull.Value ? 0 : (double)property["selling_price"];
                old_sp = old_property["selling_price"] == DBNull.Value ? 0 : (double)old_property["selling_price"];

                if (new_sbc && (new_sd != old_sd || (new_sp > 0 && new_sp != old_sp)))
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'Sell'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (contract_services.Select("service = 'Sell'").Length > 0)
                        {
                            if (period == "50%-50%" || period == "none")
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForSell", "Please confirm Invoice Requirement generation for Selling service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForSellOK", "Invoice Requirement will be generated for Selling service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    /*
                                    value = Math.Round(
                                        (dr["price_value"] != DBNull.Value && (dr["price_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_value_applicable"]))) ? Convert.ToDouble(dr["price_value"]) :
                                            (dr["price_one_payment"] != DBNull.Value && (dr["price_one_payment_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_one_payment_applicable"]))) ? Convert.ToDouble(dr["price_one_payment"]) :
                                                Convert.ToDouble(dr["price_percent"]) / 100 * Convert.ToDouble(property["selling_price"]), 2);
                                    */
                                    value = Convert.ToDouble(dr["value"]) / 100 * Convert.ToDouble(property["selling_price"]) / (period == "50%-50%" ? 2 : 1);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"] ),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO INSERT ANOTHER PREDICTED_INCOME_EXPENSE BECAUSE IT WAS NOT GENERATED WHEN CREATING THE CONTRACT
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, true);
                                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

            // Check registration information to see if we insert Invoice Requirements
            try
            {
                bool new_rp = (bool)(property["registered_property"] == DBNull.Value ? false : property["registered_property"]);
                DateTime new_rd = property["registration_date"] == DBNull.Value ? DateTime.Now : (DateTime)property["registration_date"];
                DateTime old_rd = old_property["registration_date"] == DBNull.Value ? DateTime.Now : (DateTime)old_property["registration_date"];
                if (new_rp && (new_rd != old_rd))
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'Registration of the property'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (contract_services.Select("service = 'Registration of the property'").Length > 0)
                        {
                            if (period == "50%-50%" || period == "none")
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForPropertyRegistration", "Please confirm Invoice Requirement generation for Property registration service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForPropertyRegistrationOK", "Invoice Requirement will be generated for Property registration service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    //value = (dr["price_value"] != DBNull.Value && (dr["price_value_applicable"] != DBNull.Value && Convert.ToBoolean(dr["price_value_applicable"]))) ? Convert.ToDouble(dr["price_value"]) : Convert.ToDouble(dr["price_one_payment"]);
                                    value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"] ),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                    // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                    try
                                    {
                                        //if (period == "none")
                                        //{
                                        /*
                                            DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                                new MySqlParameter("_TYPE", false),
                                                new MySqlParameter("_CURRENCY", contract["currency"]),
                                                new MySqlParameter("_AMOUNT", value),
                                                //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                                new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                                new MySqlParameter("_PROPERTY_ID", property["id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                        DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (pie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                        /*
                                        DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                        DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                        if (cpie != null)
                                        {
                                            /*
                                            (new DataAccess(CommandType.StoredProcedure, "COMPANYINCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                             */
                                            IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                        }
                                        //}
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        public static void InsertFromOwner(DataRow owner, DataRow old_owner)
        {
            DataTable owners_properties = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", owner["id"]) })).ExecuteSelectQuery().Tables[0];
            foreach (DataRow property in owners_properties.Rows)
            {
                DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
                double value = 0;
                // Check headquarters change to see if we insert Invoice Requirements
                bool new_hcbc = (bool)(owner["headquarters_changed_by_company"] == DBNull.Value ? false : owner["headquarters_changed_by_company"]);
                bool old_hcbc = (bool)(old_owner["headquarters_changed_by_company"] == DBNull.Value ? false : old_owner["headquarters_changed_by_company"]);
                string new_address = owner["address"].ToString();
                string old_address = old_owner["address"].ToString();
                if ((new_hcbc && new_hcbc != old_hcbc) && (new_address != null && new_address.Trim() != "" && new_address != old_address))
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'Change company headquarters'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (period == "50%-50%" || period == "none")
                        {
                            if (contract_services.Select("service = 'Change company headquarters'").Length > 0)
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForHeadquartersChange", "Please confirm Invoice Requirement generation for Changing Headquarters service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForHeadquartersChangeOK", "Invoice Requirement will be generated for Changing Headquarters service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                    value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                    // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                    try
                                    {
                                        //if (period == "none")
                                        //{
                                        /*
                                            DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (pie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        /*
                                            DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            new MySqlParameter("_OWNER_ID", property["owner_id"]),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (cpie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID",pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        //}
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }


                // Check closing to see if we insert Invoice Requirements
                bool new_cbc = (bool)(owner["closed_by_company"] == DBNull.Value ? false : owner["closed_by_company"]);
                bool old_cbc = (bool)(old_owner["closed_by_company"] == DBNull.Value ? false : old_owner["closed_by_company"]);
                string new_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["status_id"]) })).ExecuteScalarQuery().ToString();
                string old_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", old_owner["status_id"]) })).ExecuteScalarQuery().ToString();
                if ((new_cbc && new_cbc != old_cbc) && (new_status == "Finished" && new_status != old_status))
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'Company close-down'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (period == "50%-50%" || period == "none")
                        {
                            if (contract_services.Select("service = 'Company close-down'").Length > 0)
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForClosing", "Please confirm Invoice Requirement generation for Closing the owner's company service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForClosingOK", "Invoice Requirement will be generated for Closing the owner's company service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                    value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                    // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                    try
                                    {
                                        //if (period == "none")
                                        //{
                                        /*
                                            DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (pie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        /*
                                            DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (cpie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        //}
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }

                // Check trasferration to see if we insert Invoice Requirements
                bool new_tbc = (bool)(owner["transferred_by_company"] == DBNull.Value ? false : owner["transferred_by_company"]);
                bool old_tbc = (bool)(old_owner["transferred_by_company"] == DBNull.Value ? false : old_owner["transferred_by_company"]);
                string new_type = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["type_id"]) })).ExecuteScalarQuery().ToString();
                string old_type = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", old_owner["type_id"]) })).ExecuteScalarQuery().ToString();
                if ((new_tbc && new_tbc != old_tbc) && (new_type == "Individual" && new_type != old_type))
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'Property transfer to individual'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (period == "50%-50%" || period == "none")
                        {
                            if (contract_services.Select("service = 'Property transfer to individual'").Length > 0)
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForTransferring", "Please confirm Invoice Requirement generation for Transferring the owner's company to individual service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForTransferringOK", "Invoice Requirement will be generated for Transferring the owner's company to individual service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                    value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                    // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UODATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                    try
                                    {
                                        //if (period == "none")
                                        //{
                                        /*
                                            DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (pie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        /*
                                            DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (cpie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        //}
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }

                // Check POA to see if we insert Invoice Requirements
                bool new_pnba = (bool)(owner["poa_nif_and_bank_accounts_by_company"] == DBNull.Value ? false : owner["poa_nif_and_bank_accounts_by_company"]);
                bool old_pnba = (bool)(old_owner["poa_nif_and_bank_accounts_by_company"] == DBNull.Value ? false : old_owner["poa_nif_and_bank_accounts_by_company"]);
                if (new_pnba && new_pnba != old_pnba)
                {
                    try
                    {
                        DataRow dr = contract_services.Select("service = 'POA, NIF and Bank account openning'")[0];
                        string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                        bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                        if (period == "50%-50%" || period == "none")
                        {
                            if (contract_services.Select("service = 'POA, NIF and Bank account openning'").Length > 0)
                            {
                                //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForPOA", "Please confirm Invoice Requirement generation for POA, NIF and openning bank account(s) service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForPOAOK", "Invoice Requirement will be generated for POA, NIF and openning bank account(s) service"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (ans == DialogResult.OK)
                                {
                                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                                    value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                                    //da.ExecuteInsertQuery();
                                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                    // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                                    // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UPDATE THE IR_ID FOR PERIOD=="NONE"!!!!
                                    try
                                    {
                                        //if (period == "none")
                                        //{
                                        /*
                                            DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow pie = IncomeExpensesClass.FindIE(false, false, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (pie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(false, pie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        /*
                                            DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                         */
                                            DataRow cpie = IncomeExpensesClass.FindIE(true, true, contract["currency"], value, contract["owner_id"], property["id"], dr["service_id"], (period == "50%-50%" ? "LAST 50%" : "100%"));
                                            if (cpie != null)
                                            {
                                                /*
                                                (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                                 */
                                                IncomeExpensesClass.ChangeIRId(true, cpie["id"], new_ir["id"], CommonFunctions.ToMySqlFormatDate(DateTime.Now));
                                            }
                                        //}
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }
            }

        }

        public static void InsertPunctualService(DataRow property, string service, int part)
        {
            int service_id = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", service) }).ExecuteScalarQuery());
            InsertPunctualService(property, service_id, part);
        }

        public static void InsertPunctualService(DataRow property, int service_id, int part)
        {
            #region -- old --
            /*
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;
            try
            {
                if (contract_services.Select(String.Format("service_id = {0}", service_id.ToString())).Length > 0)
                {
                    DataRow dr = contract_services.Select(String.Format("service_id = {0}", service_id.ToString()))[0];
                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                    value = Convert.ToDouble(dr["value"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                        new MySqlParameter("_PRICE", part % 2==0?Math.Round(value/2,2):value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", part == 0?"FIRST 50%":part==2?"LAST 50%":""),
                        new MySqlParameter("_NOT_INVOICEABLE", false),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    if (part != 1)
                    {
                        da.ExecuteInsertQuery();
                        //WE DO NOT ADD PREDICTED I/E
                    }
                    else
                    {
                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                        
                        //da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                        //    new MySqlParameter("_TYPE", false),
                        //    new MySqlParameter("_CURRENCY", new_ir["currency"]),
                        //    new MySqlParameter("_AMOUNT", new_ir["price"]),
                        //    new MySqlParameter("_DATE", new_ir["date"]),
                        //    new MySqlParameter("_OWNER_ID", new_ir["owner_id"]),
                        //    new MySqlParameter("_PROPERTY_ID", new_ir["property_id"]),
                        //    new MySqlParameter("_CONTRACTSERVICE_ID", new_ir["contractservice_id"]),
                        //    new MySqlParameter("_SERVICE_DESCRIPTION", new_ir["comments"]),
                        //    new MySqlParameter("_MONTH", new_ir["month"]),
                        //    new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"])
                        //});
                        
                        IncomeExpensesClass.InsertPredictedIE(false, false, new_ir);
                        IncomeExpensesClass.InsertPredictedIE(true, true, new_ir);
                    }
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            */
            #endregion

            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;
            if (contract_services.Select(String.Format("service_id = {0}", service_id.ToString())).Length > 0)
            {
                try
                {
                    DataRow dr = contract_services.Select(String.Format("service_id = {0}", service_id.ToString()))[0];
                    string period = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["period"]) })).ExecuteScalarQuery().ToString().ToLower();
                    bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                    if (period == "50%-50%" || period == "none")
                    {
                        //DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForPOA", "Please confirm Invoice Requirement generation for POA, NIF and openning bank account(s) service"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("generateIRForService", "Invoice Requirement will be generated!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (ans == DialogResult.OK)
                        {
                            DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                            //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                            value = Math.Round(Convert.ToDouble(dr["value"]) / (period == "50%-50%" ? 2 : 1), 2);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                        //new MySqlParameter("_PRICE", Math.Round(value/2,2)),
                                        new MySqlParameter("_PRICE", value),
                                        new MySqlParameter("_MONTH", DBNull.Value),
                                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                                        new MySqlParameter("_COMMENTS", (period=="50%-50%"?"LAST 50%":"100%")),
                                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                                        new MySqlParameter("_CURRENCY", contract["currency"])});
                            //da.ExecuteInsertQuery();
                            DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                            // WE DO NOT INSERT ANOTHER PREDICTED_INCOME_EXPENSE
                            // TO DO: WE MUST FIND THE PREDICTED GENERATED IE AND UPDATE THE IR_ID FOR PERIOD=="NONE"!!!!
                            try
                            {
                                //if (period == "none")
                                //{
                                    DataRow pie = (new DataAccess(CommandType.StoredProcedure, "IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                    if (pie != null)
                                    {
                                        (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", pie["id"]),
                                                new MySqlParameter("_TYPE", pie["type"]),
                                                new MySqlParameter("_CURRENCY", pie["currency"]),
                                                new MySqlParameter("_AMOUNT", pie["amount"]),
                                                new MySqlParameter("_OWNER_ID", pie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", pie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", pie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", pie["service_description"]),
                                                new MySqlParameter("_MONTH", pie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", pie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", pie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                    }
                                    DataRow cpie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_find", new object[]{
                                            new MySqlParameter("_TYPE", false),
                                            new MySqlParameter("_CURRENCY", contract["currency"]),
                                            new MySqlParameter("_AMOUNT", value),
                                            //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                                            new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                                            new MySqlParameter("_PROPERTY_ID", property["id"]),
                                            new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                                            new MySqlParameter("_SERVICE_DESCRIPTION", (period=="50%-50%"?"LAST 50%":"100%"))})).ExecuteSelectQuery().Tables[0].Rows[0];
                                    if (pie != null)
                                    {
                                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", new object[]{
                                                new MySqlParameter("_ID", cpie["id"]),
                                                new MySqlParameter("_TYPE", cpie["type"]),
                                                new MySqlParameter("_CURRENCY", cpie["currency"]),
                                                new MySqlParameter("_AMOUNT", cpie["amount"]),
                                                new MySqlParameter("_OWNER_ID", cpie["owner_id"]),
                                                new MySqlParameter("_PROPERTY_ID", cpie["property_id"]),
                                                new MySqlParameter("_CONTRACTSERVICE_ID", cpie["contractservice_id"]),
                                                new MySqlParameter("_SERVICE_DESCRIPTION", cpie["service_description"]),
                                                new MySqlParameter("_MONTH", cpie["month"]),
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"]),
                                                new MySqlParameter("_INVOICE_ID", cpie["invoice_id"]),
                                                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", cpie["contract_service_additional_cost_id"])
                                            })).ExecuteUpdateQuery();
                                    }
                                //}
                            }
                            catch { }
                        }
                    }
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }
        }

        public static void UpdatePunctualService(DataRow property, string service, int part)
        {
            int service_id = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", service) }).ExecuteScalarQuery());
            UpdatePunctualService(property, service_id, part);
        }

        public static void UpdatePunctualService(DataRow property, int service_id, int part)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            double value = 0;
            try
            {
                if (contract_services.Select(String.Format("service_id = {0}", service_id.ToString())).Length > 0)
                {
                    DataRow dr = contract_services.Select(String.Format("service_id = {0}", service_id.ToString()))[0];
                    bool not_invoiceable = Convert.ToBoolean(dr["not_invoiceable"] == DBNull.Value ? false : dr["not_invoiceable"]);
                    DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", dr["contract_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    //DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_property_contract_service", new object[]{
                    //    new MySqlParameter("_PROPERTY_ID", property["id"]),
                    //    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                    //    new MySqlParameter("_CONTRACTSERVICE_ID", service_id)});
                    //da.ExecuteUpdateQuery();
                    (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status_by_month2", new object[] {
                            new MySqlParameter("_PROPERTY_ID", property["id"]), 
                            new MySqlParameter("_CONTRACT_ID", dr["contract_id"]), 
                            new MySqlParameter("_MONTH", null), 
                            new MySqlParameter("_SERVICE_ID", service_id),
                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                            new MySqlParameter("_DELETED", true)                            
                    })).ExecuteUpdateQuery();
                    // TO DO: CHANGE TO CHANGE_STATUS !!!

                    //value = Convert.ToDouble(dr["price_one_payment"] == DBNull.Value ? dr["price_value"] : dr["price_one_payment"]);
                    value = Convert.ToDouble(dr["value"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", new object[]{
                        //new MySqlParameter("_OWNER_ID", property["owner_id"] ),
                        new MySqlParameter("_OWNER_ID", contract["owner_id"] ),
                        new MySqlParameter("_PROPERTY_ID", property["id"]),
                        new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                        new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACTSERVICE_ID", dr["service_id"]),
                        new MySqlParameter("_PRICE", part % 2==0?Math.Round(value/2,2):value),
                        new MySqlParameter("_MONTH", DBNull.Value),
                        new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now.Date)),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_COMMENTS", part == 0?"FIRST 50%":part==2?"LAST 50%":""),
                        new MySqlParameter("_NOT_INVOICEABLE", not_invoiceable),
                        new MySqlParameter("_CURRENCY", contract["currency"])
                    });
                    if (part != 1)
                    {
                        da.ExecuteInsertQuery();
                        //WE DO NOT ADD PREDICTED I/E
                    }
                    else
                    {
                        DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];

                        //da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                        //    new MySqlParameter("_TYPE", false),
                        //    new MySqlParameter("_CURRENCY", new_ir["currency"]),
                        //    new MySqlParameter("_AMOUNT", new_ir["price"]),
                        //    new MySqlParameter("_DATE", new_ir["date"]),
                        //    new MySqlParameter("_OWNER_ID", new_ir["owner_id"]),
                        //    new MySqlParameter("_PROPERTY_ID", new_ir["property_id"]),
                        //    new MySqlParameter("_CONTRACTSERVICE_ID", new_ir["contractservice_id"]),
                        //    new MySqlParameter("_SERVICE_DESCRIPTION", new_ir["comments"]),
                        //    new MySqlParameter("_MONTH", new_ir["month"]),
                        //    new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"])
                        //});

                        IncomeExpensesClass.InsertIE(false, false,DBNull.Value, new_ir, true);
                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, true);
                    }
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        public static void DeletePunctualService(DataRow property, string service, int part)
        {
            int service_id = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", service) }).ExecuteScalarQuery());
            DeletePunctualService(property, service_id, part);
        }

        public static void DeletePunctualService(DataRow property, int service_id, int part)
        {
            DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", property["id"]) })).ExecuteSelectQuery().Tables[0];
            try
            {
                if (contract_services.Select(String.Format("service_id = {0}", service_id.ToString())).Length > 0)
                {
                    DataRow dr = contract_services.Select(String.Format("service_id = {0}", service_id.ToString()))[0];
                    //DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_property_contract_service", new object[]{
                    //    new MySqlParameter("_PROPERTY_ID", property["id"]),
                    //    new MySqlParameter("_CONTRACT_ID", dr["contract_id"]),
                    //    new MySqlParameter("_CONTRACTSERVICE_ID", service_id)});
                    //da.ExecuteUpdateQuery();
                    (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_change_status_by_month2", new object[] {
                            new MySqlParameter("_PROPERTY_ID", property["id"]), 
                            new MySqlParameter("_CONTRACT_ID", dr["contract_id"]), 
                            new MySqlParameter("_MONTH", null), 
                            new MySqlParameter("_SERVICE_ID", service_id),
                            //new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Inactive"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")} )).ExecuteScalarQuery()),
                            new MySqlParameter("_DELETED", true)                            
                    })).ExecuteUpdateQuery();
                    //TO DO: CHANGE TO CHANGE_STATUS
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        public static void ChangeStatus(int ir_id, int status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTsp_change_status", new object[]{
                new MySqlParameter("_ID", ir_id),
                new MySqlParameter("_STATUS_ID", status_id)
            });
            da.ExecuteUpdateQuery();
        }
    }
}