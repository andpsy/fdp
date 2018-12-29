using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FDP
{
    static class IncomeExpensesClass
    {
        /*
        public static void InsertPredictedIE(bool company, bool type, object currency, object amount, object date, object owner_id, object property_id, object contractservice_id, object service_description, object month, object invoicerequirement_id, object contract_service_additional_cost_id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, company ? "COMPANY_PREDICTED_INCOME_EXPENSESsp_insert" : "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", type),
                        new MySqlParameter("_CURRENCY", currency),
                        new MySqlParameter("_AMOUNT", amount),
                        new MySqlParameter("_DATE", date),
                        new MySqlParameter("_OWNER_ID", owner_id),
                        new MySqlParameter("_PROPERTY_ID", property_id),
                        new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                        new MySqlParameter("_SERVICE_DESCRIPTION", service_description),
                        new MySqlParameter("_MONTH", month),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                        new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", contract_service_additional_cost_id),
                        new MySqlParameter("_STATUS_ID", status_id)
                    });
            da.ExecuteInsertQuery();
        }
        */
        public static DataRow InsertIE(object company, object type, object source, DataRow ir, bool _generate_vat)
        {
            double _vat = 0;
            double _amount_total = Convert.ToDouble(ir["price"]);
            if (_generate_vat)
            {
                _vat = Convert.ToBoolean(ir["not_invoiceable"] == DBNull.Value ? false : ir["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                _amount_total = Convert.ToDouble(ir["price"]) + _vat;
            }
            DataAccess da = new DataAccess(CommandType.StoredProcedure, Convert.ToBoolean(company) ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", type),
                        new MySqlParameter("_SOURCE", source),
                        new MySqlParameter("_CURRENCY", ir["currency"]),
                        new MySqlParameter("_AMOUNT", ir["price"]),
                        new MySqlParameter("_DATE", ir["date"]),
                        new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", ir["comments"]),
                        new MySqlParameter("_MONTH", ir["month"]),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", ir["id"]),
                        new MySqlParameter("_INVOICE_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", DBNull.Value),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", DBNull.Value),
                        new MySqlParameter("_AMOUNT_PAID", DBNull.Value),
                        new MySqlParameter("_AMOUNT_PAID_RON", DBNull.Value),
                        new MySqlParameter("_BALLANCE", ir["price"]),
                        new MySqlParameter("_VAT", _vat),
                        new MySqlParameter("_AMOUNT_TOTAL", _amount_total)
                    });
            DataRow new_ie = da.ExecuteSelectQuery().Tables[0].Rows[0];
            return new_ie;
        }

        public static void IEChangeStatus(object id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status2", new object[]{
                new MySqlParameter("_ID", id),
                new MySqlParameter("_STATUS_ID", status_id)});
            da.ExecuteUpdateQuery();
        }

        public static void CompanyIEChangeStatus(object id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status2", new object[]{
                new MySqlParameter("_ID", id),
                new MySqlParameter("_STATUS_ID", status_id)});
            da.ExecuteUpdateQuery();
        }

        public static void IEChangeStatusByIRId(object ir_id, object type, object invoice_id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status2_by_ir_id", new object[]{
                new MySqlParameter("_INVOICEREQUIREMENT_ID", ir_id),
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new MySqlParameter("_STATUS_ID", status_id)});
            da.ExecuteUpdateQuery();
        }

        public static void CompanyIEChangeStatusByIRId(object ir_id, object type, object invoice_id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status2_by_ir_id", new object[]{
                new MySqlParameter("_INVOICEREQUIREMENT_ID", ir_id),
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new MySqlParameter("_STATUS_ID", status_id)});
            da.ExecuteUpdateQuery();
        }

        public static DataRow InsertIE(bool company, bool type, object source, DataRow ir, string description, bool _generate_vat)
        {
            double _vat = 0;
            double _amount_total = Convert.ToDouble(ir["price"]);
            if (_generate_vat)
            {
                _vat = Convert.ToBoolean(ir["not_invoiceable"] == DBNull.Value ? false : ir["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                _amount_total = Convert.ToDouble(ir["price"]) + _vat;
            }
            DataAccess da = new DataAccess(CommandType.StoredProcedure, company ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", type),
                        new MySqlParameter("_SOURCE", source),
                        new MySqlParameter("_CURRENCY", ir["currency"]),
                        new MySqlParameter("_AMOUNT", ir["price"]),
                        new MySqlParameter("_DATE", ir["date"] == DBNull.Value || ir["date"] == null?DBNull.Value : (object)Convert.ToDateTime(ir["date"]).AddMonths(2) ),
                        new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", description),
                        new MySqlParameter("_MONTH", ir["month"]),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", ir["id"]),
                        new MySqlParameter("_INVOICE_ID", DBNull.Value),
                        new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", DBNull.Value),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status")})).ExecuteScalarQuery() ),
                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", DBNull.Value),
                        new MySqlParameter("_AMOUNT_PAID", DBNull.Value),
                        new MySqlParameter("_AMOUNT_PAID_RON", DBNull.Value),
                        new MySqlParameter("_BALLANCE", ir["price"]),
                        new MySqlParameter("_VAT", ir["vat"]),
                        new MySqlParameter("_AMOUNT_TOTAL", ir["amount_total"])
                    });
            DataRow new_ie = da.ExecuteSelectQuery().Tables[0].Rows[0];
            return new_ie;
        }
        /*
        public static void InsertIE(object company, object type, object source, object currency, object amount, object date, object owner_id, object property_id, object contractservice_id, object service_description, object month, object invoicerequirement_id, object invoice_id, object contract_service_additional_cost_id, object status_id)
        {
            //string sp = source ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert";
            //DataAccess da = new DataAccess(CommandType.StoredProcedure, sp, new object[]{
            DataAccess da = new DataAccess(CommandType.StoredProcedure, Convert.ToBoolean(company) ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert", new object[]{
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_SOURCE", source),
                new MySqlParameter("_CURRENCY", currency),
                new MySqlParameter("_AMOUNT", amount),
                new MySqlParameter("_DATE", date),
                new MySqlParameter("_OWNER_ID", owner_id),
                //new MySqlParameter("_PROPERTY_ID", property_id==-1?DBNull.Value:(object)property_id),
                new MySqlParameter("_PROPERTY_ID", property_id),
                //new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id==-1?DBNull.Value:(object)contractservice_id),
                new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                new MySqlParameter("_SERVICE_DESCRIPTION", service_description),
                new MySqlParameter("_MONTH", month),
                new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new  MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", contract_service_additional_cost_id),
                new MySqlParameter("_STATUS_ID", status_id) 
            });
            DataRow new_ie = da.ExecuteSelectQuery().Tables[0].Rows[0];
        }
        */
        public static DataRow InsertIE(object company, object type, object source, object currency, object amount, object date, object owner_id, object property_id, object contractservice_id, object service_description, object month, object invoicerequirement_id, object invoice_id, object contract_service_additional_cost_id, object status_id, object bank_account_details, object amount_paid, object ballance, object vat, object amount_total)
        {
            //string sp = source ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert";
            //DataAccess da = new DataAccess(CommandType.StoredProcedure, sp, new object[]{
            DataAccess da = new DataAccess(CommandType.StoredProcedure, Convert.ToBoolean(company) ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert", new object[]{
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_SOURCE", source),
                new MySqlParameter("_CURRENCY", currency),
                new MySqlParameter("_AMOUNT", amount),
                new MySqlParameter("_DATE", date),
                new MySqlParameter("_OWNER_ID", owner_id),
                //new MySqlParameter("_PROPERTY_ID", property_id==-1?DBNull.Value:(object)property_id),
                new MySqlParameter("_PROPERTY_ID", property_id),
                //new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id==-1?DBNull.Value:(object)contractservice_id),
                new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                new MySqlParameter("_SERVICE_DESCRIPTION", service_description),
                new MySqlParameter("_MONTH", month),
                new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", contract_service_additional_cost_id),
                new MySqlParameter("_STATUS_ID", status_id) ,
                new MySqlParameter("_BANK_ACCOUNT_DETAILS", bank_account_details),
                new MySqlParameter("_AMOUNT_PAID", amount_paid),
                new MySqlParameter("_AMOUNT_PAID_RON", DBNull.Value),
                new MySqlParameter("_BALLANCE", ballance),
                new MySqlParameter("_VAT", vat),
                new MySqlParameter("_AMOUNT_TOTAL", amount_total)
            });
            DataRow new_ie = da.ExecuteSelectQuery().Tables[0].Rows[0];
            return new_ie;
        }

        /*
        public static void InsertCompanyIE(object type, object source, object currency, object amount, object date, object owner_id, object property_id, object contractservice_id, object service_description, object month, object invoicerequirement_id, object invoice_id, object contract_service_additional_cost_id, object status_id)
        {
            //string sp = source ? "COMPANY_INCOME_EXPENSESsp_insert" : "INCOME_EXPENSESsp_insert";
            //DataAccess da = new DataAccess(CommandType.StoredProcedure, sp, new object[]{
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_insert", new object[]{
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_SOURCE", source),
                new MySqlParameter("_CURRENCY", currency),
                new MySqlParameter("_AMOUNT", amount),
                new MySqlParameter("_DATE", date),
                new MySqlParameter("_OWNER_ID", owner_id),
                //new MySqlParameter("_PROPERTY_ID", property_id==-1?DBNull.Value:(object)property_id),
                new MySqlParameter("_PROPERTY_ID", property_id),
                //new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id==-1?DBNull.Value:(object)contractservice_id),
                new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                new MySqlParameter("_SERVICE_DESCRIPTION", service_description),
                new MySqlParameter("_MONTH", month),
                new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new  MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", contract_service_additional_cost_id),
                new MySqlParameter("_STATUS_ID", status_id) 
            });
            DataRow new_ie = da.ExecuteSelectQuery().Tables[0].Rows[0];
        }
        */
        public static void InsertIE(DataRow invoice)
        {

        }

        public static DataRow FindIE(bool company, object type, object currency, object amount, object owner_id, object property_id, object contractservice_id, object service_description)
        {
            try
            {
                DataRow pie = (new DataAccess(CommandType.StoredProcedure, company ? "COMPANY_IEsp_find" : "IEsp_find", new object[]{
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_CURRENCY", currency),
                new MySqlParameter("_AMOUNT", amount),
                //new MySqlParameter("_OWNER_ID", owner_id ),
                new MySqlParameter("_OWNER_ID", owner_id ),
                new MySqlParameter("_PROPERTY_ID", property_id),
                new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                new MySqlParameter("_SERVICE_DESCRIPTION", service_description)})).ExecuteSelectQuery().Tables[0].Rows[0];
                return pie;
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); return null; }
        }

        public static void UpdateIE(bool company, object id, object type, object currency, object amount, object owner_id, object property_id, object contractservice_id, object service_description, object month, object invoicerequirement_id, object invoice_id, object contract_service_additional_cost_id, object status_id, object bank_account_details, object amount_paid, object ballance, object vat, object amount_total)
        {
            try
            {
                (new DataAccess(CommandType.StoredProcedure, company ? "COMAPNY_INCOME_EXPENSESsp_update" : "INCOME_EXPENSESsp_update", new object[]{
                new MySqlParameter("_ID", id),
                new MySqlParameter("_TYPE", type),
                new MySqlParameter("_CURRENCY", currency),
                new MySqlParameter("_AMOUNT", amount),
                new MySqlParameter("_OWNER_ID", owner_id),
                new MySqlParameter("_PROPERTY_ID", property_id),
                new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id),
                new MySqlParameter("_SERVICE_DESCRIPTION", service_description),
                new MySqlParameter("_MONTH", month),
                new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                new MySqlParameter("_INVOICE_ID", invoice_id),
                new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", contract_service_additional_cost_id),
                new MySqlParameter("_STATUS_ID", status_id),
                new MySqlParameter("_BANK_ACCOUNT_DETAILS", bank_account_details),
                new MySqlParameter("_AMOUNT_PAID", amount_paid),
                new MySqlParameter("_AMOUNT_PAID_RON", DBNull.Value),
                new MySqlParameter("_BALLANCE", ballance),
                new MySqlParameter("_VAT", vat),
                new MySqlParameter("_AMOUNT_TOTAL", amount_total)
            })).ExecuteUpdateQuery();
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        public static void ChangeIRId(bool company, object id, object invoicerequirement_id, object date)
        {
            try
            {
                (new DataAccess(CommandType.StoredProcedure, company ? "COMPANY_INCOME_EXPENSESsp_change_ir_id" : "INCOME_EXPENSESsp_change_ir_id", new object[]{
                    new MySqlParameter("_ID", id),
                    new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                    new MySqlParameter("_DATE", date)
                })).ExecuteUpdateQuery();
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }
    }
}