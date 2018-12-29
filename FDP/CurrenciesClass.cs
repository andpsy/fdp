using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace FDP
{
    static class CurrenciesClass
    {

        public static void GetCurrencies()
        {
            // Download BNR Exchange file and get currency values
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_last_date");
                DateTime date = Convert.ToDateTime(da.ExecuteScalarQuery());
                //DateTime dt = CommonFunctions.FromMySqlFormatDate(date.ToString());
                if (date.Date < DateTime.Now.Date)
                {

                    string file = Path.Combine(SettingsClass.ExchangeRatesFilePath, String.Format("nbrfxrates_{0}.xml", DateTime.Now.Date.ToString("ddMMyyyy")));
                    if (!File.Exists(file))
                    {
                        WebClient webClient = new WebClient();
                        //webClient.DownloadFileAsync(new Uri(SettingsClass.ExchangeRatesFileUrl), file);
                        webClient.DownloadFile(new Uri(SettingsClass.ExchangeRatesFileUrl), file);
                    }
                    DataSet exchangeRates = new DataSet();
                    exchangeRates.ReadXml(file);

                    if (Convert.ToDateTime(exchangeRates.Tables["Cube"].Rows[0]["date"]).Date > date.Date)
                    {
                        foreach (DataRow dr in exchangeRates.Tables["Rate"].Rows)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_insert", new object[] { 
                                new MySqlParameter("_CURRENCY", dr["currency"]),
                                new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(DateTime.Now)),
                                new MySqlParameter("_RATE", dr["rate_text"]),
                                new MySqlParameter("_MULTIPLIER", dr["multiplier"])
                                });
                            da.ExecuteInsertQuery();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                throw exp;
            }

        }

        public static void GetMissingCurrencies(int year)
        {
            string file = Path.Combine(SettingsClass.ExchangeRatesFilePath, String.Format("nbrfxrates{0}.xml", year.ToString()));
            if (!File.Exists(file))
            {
                WebClient webClient = new WebClient();
                //webClient.DownloadFileAsync(new Uri(SettingsClass.ExchangeRatesFileUrl), file);
                webClient.DownloadFile(new Uri(String.Format(SettingsClass.YearlyExchangeRatesFileUrl, year.ToString())), file);
            }
            DataSet exchangeRates = new DataSet();
            exchangeRates.ReadXml(file);
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_dates");
            DataTable dates = da.ExecuteSelectQuery().Tables[0];
            foreach (DataRow dr in exchangeRates.Tables["Cube"].Rows)
            {
                try
                {

                    int cube_id = Convert.ToInt32(dr["Cube_Id"]);
                    DateTime date = Convert.ToDateTime(dr["date"]);
                    if (dates.Select(string.Format("[DATE] = #{0}#", date.ToString(DateTimeFormatInfo.InvariantInfo))).Length == 0)  // !!! to check here !!!
                    {
                        foreach (DataRow dre in exchangeRates.Tables["Rate"].Select(String.Format("Cube_Id = {0}", cube_id)))
                        {
                            try
                            {
                                da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_insert", new object[] { 
                                new MySqlParameter("_CURRENCY", dre["currency"]),
                                new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(date)),
                                new MySqlParameter("_RATE", dre["rate_text"]),
                                new MySqlParameter("_MULTIPLIER", dre["multiplier"])
                                });
                                da.ExecuteInsertQuery();
                            }
                            catch (Exception exp2) { throw exp2; }
                        }
                    }
                }
                catch (Exception exp) { throw exp; }
            }
        }

        public static double GetExchangeRate(DateTime date, string currency)
        {
            object exchange_rate = (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_date_and_currency", new object[]{
            new MySqlParameter("_DATE", date),
            new MySqlParameter("_CURRENCY", currency)})).ExecuteScalarQuery();
            return Math.Round(Convert.ToDouble(exchange_rate == null ? 1 : exchange_rate), 4);

        }
    }
}
