using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class CurrencyShow : UserControl
    {
        public DateTime CurrencyDate
        {
            get;
            set;
        }

        public CurrencyShow(DateTime currency_date)
        {
            CurrencyDate = currency_date;
            InitializeComponent();
            DataTable currencies = (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_date", new object[] { new MySqlParameter("_DATE", CurrencyDate) })).ExecuteSelectQuery().Tables[0];
            listBox1.Items.Clear();
            foreach (DataRow dr in currencies.Rows)
            {
                listBox1.Items.Add(String.Format("1 {0} = {1} RON", dr["currency"].ToString().ToUpper(), 
                    Math.Round(Convert.ToDouble(dr["rate"]) * Convert.ToDouble(dr["multiplier"] != DBNull.Value?dr["multiplier"]:1), 2) ));
            }
        }

        public CurrencyShow()
        {
            InitializeComponent();
        }

        public void GetCurrencies(DateTime currency_date)
        {
            listBox1.Items.Clear();
            DataTable currencies = (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_date", new object[] { new MySqlParameter("_DATE", currency_date) })).ExecuteSelectQuery().Tables[0];
            foreach (DataRow dr in currencies.Rows)
            {
                listBox1.Items.Add(String.Format("1 {0} = {1} RON", dr["currency"].ToString().ToUpper(),
                    Math.Round(Convert.ToDouble(dr["rate"]) / Convert.ToDouble(dr["multiplier"] != DBNull.Value ? dr["multiplier"] : 1), 4)));
            }
        }
    }
}
