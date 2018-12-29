using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace FDP
{
    static class CommonFunctions
    {
        public static string SentenceCaseRegExp(string message)
        {
            int capturePosition;
            Capture capture;
            char[] messageArray = message.ToLower().ToCharArray();

            const string pattern = @"[^\s]\.[\s\\n\\r]+[a-z]";
            var ms = Regex.Matches(message.ToLower(), pattern);
            for (int i = 0; i < ms.Count; i++)
            {
                capture = ms[i].Captures[0];
                capturePosition = capture.Index + capture.Value.Length - 1;
                messageArray[capturePosition] = char.ToUpper(messageArray[capturePosition]);
            }
            ms = Regex.Matches(message.ToLower(), @"^[\s\\r\\n]*[a-z]");
            if (ms.Count > 0)
            {
                capture = ms[0].Captures[0];
                capturePosition = capture.Index + capture.Value.Length - 1;
                messageArray[capturePosition] = char.ToUpper(messageArray[capturePosition]);
            }
            return new string(messageArray);
        }


        public static string SentenceCase(string message)
        {
            char[] messageArray = message.ToLower().ToCharArray();
            messageArray[0] = char.ToUpper(messageArray[0]);
            return new string(messageArray);
        }

        public static string GetMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(System.Security.Cryptography.MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ToMySqlFormatDate(DateTime dt)
        {
            return dt.Year + "-" + dt.Month + "-" + dt.Day;
        }

        public static DateTime FromMySqlFormatDate(string dt)
        {
            try
            {
                string[] dtVals = dt.Split('-');
                return new DateTime(Convert.ToInt32(dtVals[0]), Convert.ToInt32(dtVals[1]), Convert.ToInt32(dtVals[2]));
            }
            catch
            {
                return new DateTime();
            }
        }

        public static DateTime SwitchBackFormatedDate(string dt)
        {
            DateTime toReturn = new DateTime();
            if (dt.IndexOf('.') > 0)
            {
                try
                {
                    string[] dly = dt.Split('.');
                    string newDate = dly[1] + "/" + dly[0] + "/" + dly[2];
                    return Convert.ToDateTime(newDate);
                }
                catch
                {
                    //return DateTime.Now.Date;
                    return new DateTime();
                }
            }
            else
            {
                try
                {
                    toReturn = Convert.ToDateTime(dt);
                    return toReturn;
                }
                catch
                {
                    //return DateTime.Now.Date;
                    return new DateTime();
                }
            }
            //return new DateTime();
        }

        public static string FromMySqlFormatDate(DateTime dt, string format)
        {
            //return dt.Day + separator + dt.Month + separator + dt.Year;
            return dt.ToString(format);
        }

        public static object SetNullable(object control)
        {
            if (control is System.Windows.Forms.ComboBox)
            {
                try
                {
                    if (((System.Windows.Forms.ComboBox)control).SelectedIndex < 0)
                        return DBNull.Value;
                    int index = 0;
                    if (((System.Windows.Forms.ComboBox)control).DataSource != null && (((System.Windows.Forms.ComboBox)control).SelectedValue == null || ( Int32.TryParse(((System.Windows.Forms.ComboBox)control).SelectedValue.ToString(), out index) && index == -1)))
                        return DBNull.Value;
                    return ((System.Windows.Forms.ComboBox)control).DataSource != null ? ((System.Windows.Forms.ComboBox)control).SelectedValue : ((System.Windows.Forms.ComboBox)control).SelectedItem;
                }
                catch { return DBNull.Value; }
            }
            //return control.ToString();
            return ((System.Windows.Forms.ComboBox)control).SelectedValue;
        }

        public static void SetDateFormat(System.Windows.Forms.Form form)
        {
            foreach (System.Windows.Forms.Control ctrl in form.Controls)
            {
                if (ctrl.GetType().Name == "DateTimePicker")
                {
                    try
                    {
                        //DateTime _o_val = ((System.Windows.Forms.DateTimePicker)ctrl).Value;
                        ((System.Windows.Forms.DateTimePicker)ctrl).Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                        ((System.Windows.Forms.DateTimePicker)ctrl).CustomFormat = SettingsClass.DateFormat;
                        //((System.Windows.Forms.DateTimePicker)ctrl).Value = _o_val;
                    }
                    catch (Exception exp) { exp.ToString(); }
                }
                try
                {
                    SetDateFormat(ctrl);
                }
                catch (Exception exp) { exp.ToString(); }
            }
        }

        public static void SetDateFormat(System.Windows.Forms.Control ctrl)
        {
            foreach (System.Windows.Forms.Control child_ctrl in ctrl.Controls)
            {
                if (child_ctrl.GetType().Name == "DateTimePicker")
                {
                    try
                    {
                        //DateTime _o_val = ((System.Windows.Forms.DateTimePicker)child_ctrl).Value;
                        ((System.Windows.Forms.DateTimePicker)child_ctrl).Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                        ((System.Windows.Forms.DateTimePicker)child_ctrl).CustomFormat = SettingsClass.DateFormat;
                        //(System.Windows.Forms.DateTimePicker)child_ctrl).Value = _o_val;
                    }
                    catch (Exception exp) { exp.ToString(); }
                }
                try
                {
                    SetDateFormat(child_ctrl);
                }
                catch (Exception exp) { exp.ToString(); }
            }
        }


        public static void SetFont(System.Windows.Forms.Form form)
        {
            try
            {
                form.Font = new System.Drawing.Font(SettingsClass.FontTheme, form.Font.Size);
            }
            catch { }

            foreach (System.Windows.Forms.Control ctrl in form.Controls)
            {
                try
                {
                    try
                    {
                        ctrl.Font = new System.Drawing.Font(SettingsClass.FontTheme, ctrl.Font.Size);
                    }
                    catch { }
                    SetFont(form.Name, ctrl);
                }
                catch { }
            }
        }

        public static void SetFont(string form_name, System.Windows.Forms.Control ctrl)
        {
            foreach (System.Windows.Forms.Control child_ctrl in ctrl.Controls)
            {
                try
                {
                    child_ctrl.Font = new System.Drawing.Font(SettingsClass.FontTheme, child_ctrl.Font.Size);
                }
                catch { }
                SetFont(form_name, child_ctrl);
            }
        }

        public static int DateDifferenceInNumberOfMonths(DateTime d1, DateTime d2)
        {
            int months = d1.Month - d2.Month + 1;
            int years = d1.Year - d2.Year;
            months += (years * 12);
            return months;
        }

        public static ArrayList DateDifferenceInMonths(DateTime d1, DateTime d2) // 13 MONTH
        {
            ArrayList months_list = new ArrayList();
            int months = d1.Month - d2.Month;
            int years = d1.Year - d2.Year;
            months += (years * 12);
            for (int i = 0; i <= months; i++)
            {
                //months_list.Add(new object[]{d1.AddMonths(i).Month, d1.AddMonths(i).Year});
                months_list.Add(String.Format("{0}/{1}", d2.AddMonths(i).Month, d2.AddMonths(i).Year));
            }
            return months_list;
        }

        public static ArrayList DateDifferenceInMonths2(DateTime d1, DateTime d2) // 12 MONTH ONLY
        {
            ArrayList months_list = new ArrayList();
            int months = d1.Month - d2.Month;
            int years = d1.Year - d2.Year;
            months += (years * 12);
            for (int i = 0; i < months; i++)
            {
                //months_list.Add(new object[]{d1.AddMonths(i).Month, d1.AddMonths(i).Year});
                months_list.Add(String.Format("{0}/{1}", d2.AddMonths(i).Month, d2.AddMonths(i).Year));
            }
            return months_list;
        }

        public static ArrayList DateDifferenceInYears(DateTime d1, DateTime d2) // plus one month
        {
            ArrayList years_list = new ArrayList();
            //int months = ((d1.Year - d2.Year) * 12) + d1.Month - d2.Month + 1;  // from 24.04.2016
            int months = ((d1.Year - d2.Year) * 12) + d1.Month - d2.Month + (d2.Day == 1 ? 0 : 1);

            if (months <= 12)
            {
                years_list.Add(new int[] { months, d2.Year });
            }
            else
            {
                if (months % 12 == 0)
                {
                    for (int i = d2.Year; i < d1.Year; i++)
                    {
                        //months = i == d1.Year - 1 ? 12 - d2.Month + 1 : 12;
                        years_list.Add(new int[] { 12, i });
                    }
                }
                else
                {
                    for (int i = d2.Year; i < d1.Year ; i++)
                    {
                        years_list.Add(new int[] { 12, i });
                    }
                    years_list.Add(new int[] { months % 12, d1.Year });

                }
            }
            return years_list;
        }

        public static ArrayList DateDifferenceInYears2(DateTime d1, DateTime d2)
        {
            ArrayList years_list = new ArrayList();
            int months = ((d1.Year - d2.Year) * 12) + d1.Month - d2.Month;

            if (months <= 12)
            {
                years_list.Add(new int[] { months, d2.Year });
            }
            else
            {
                if (months % 12 == 0)
                {
                    for (int i = d2.Year; i < d1.Year; i++)
                    {
                        //months = i == d1.Year - 1 ? 12 - d2.Month + 1 : 12;
                        years_list.Add(new int[] { 12, i });
                    }
                }
                else
                {
                    for (int i = d2.Year; i < d1.Year; i++)
                    {
                        years_list.Add(new int[] { 12, i });
                    }
                    years_list.Add(new int[] { months % 12, d1.Year });

                }
            }
            return years_list;
        }

        public static DateTime GetClosestDate(int year, int month, int day)
        {
            DateTime toReturn = new DateTime();
            bool good_date = false;
            while (!good_date)
            {
                try
                {
                    toReturn = new DateTime(year, month, day);
                    good_date = true;
                }
                catch
                {
                    day = (day == 1 ? 31 : day);
                    month = (day > 1 ? (month == 1 ? month = 12 : month) : (month - 1 == 1 ? 12 : month - 1));
                    year = ((month == 1 && day == 1) ? year - 1 : year);
                }
            }
            return toReturn;
        }

        public static object[] GenerateMySqlParameters(DataTable _dt, object[] _object, int _action)
        {
            ArrayList _alist = new ArrayList();
            for (int i = 0; i < _dt.Columns.Count; i++)
            {
                DataColumn dc = _dt.Columns[i];
                if (!(dc.ColumnName.ToLower() == "id" && _action == 0))
                    _alist.Add(new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i]));
                //else
                //    ;//_alist.Add(new MySqlParameter("_" + dc.ColumnName.ToUpper(), "-1"));

                /*
                if ((dc.ColumnName.ToLower() != "id" && _action == 0) || _action == 1)
                {
                    MySqlParameter mp = new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i].ToString());
                    _alist.Add(mp);
                }
                */
                /*
                    MySqlParameter mp = new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i].ToString());
                    if (dc.ColumnName.ToLower() == "id" && _action == 0)
                        mp.Direction = ParameterDirection.Output;
                    _alist.Add(mp);
                */
            }
            return _alist.ToArray();
        }

        public static DataRow CopyDataRow(DataRow source)
        {
            try
            {
                DataRow destination = source.Table.NewRow();
                foreach (DataColumn dc in source.Table.Columns)
                {
                    if (source[dc.ColumnName] == DBNull.Value)
                    {
                        switch (dc.DataType.ToString())
                        {
                            case "System.DateTime":
                                destination[dc.ColumnName] = CommonFunctions.ToMySqlFormatDate(DateTime.Now);
                                break;
                            case "System.Boolean":
                                destination[dc.ColumnName] = false;
                                break;
                            default:
                                destination[dc.ColumnName] = source[dc.ColumnName];
                                break;
                        }
                    }
                    else
                    {
                        destination[dc.ColumnName] = source[dc.ColumnName];
                    }
                }
                return destination;
            }
            catch { return null; }
        }

        public static string NumberToLetters(double number)
        {
            return NumberToLettersConverter.Convert(Convert.ToDecimal(number));

        }

        public static SortedDictionary<int, string> DataGridViewOrderedColumns(System.Windows.Forms.DataGridView dgv)
        {
            SortedDictionary<int, string> toReturn = new SortedDictionary<int, string>();
            foreach (System.Windows.Forms.DataGridViewColumn dgvc in dgv.Columns)
            {
                toReturn.Add(dgvc.DisplayIndex, dgvc.Name);
            }
            return toReturn;
        }

        public static void DisplayCompanyAlias()
        {

        }

        public static double ConvertCurrency(double value_to_convert, string currency1, string currency2, DateTime date)
        {
            double exchange_rate = 1;
            double value_to_return = value_to_convert;
            try
            {
                if (currency1.ToLower() == currency2.ToLower())
                {
                    exchange_rate = 1;
                }
                else
                {
                    if (currency2.ToLower() == "ron")
                    {
                        exchange_rate = CurrenciesClass.GetExchangeRate(date, currency1.ToLower());
                        value_to_return = Math.Round(value_to_convert * exchange_rate, 2);
                    }
                    else
                    {
                        if (currency1.ToLower() == "ron")
                        {
                            exchange_rate = CurrenciesClass.GetExchangeRate(date, currency2.ToLower());
                            value_to_return = Math.Round(value_to_convert / exchange_rate, 2);
                        }
                        else
                        {
                            exchange_rate = CurrenciesClass.GetExchangeRate(date, currency1.ToLower());
                            value_to_return = Math.Round(value_to_convert * exchange_rate, 2);
                            exchange_rate = CurrenciesClass.GetExchangeRate(date, currency2.ToLower());
                            value_to_return = Math.Round(value_to_return / exchange_rate, 2);
                        }
                    }
                }
                return value_to_return;
            }
            catch { return 1; }
        }

        public static string GetSingular(string plural)
        {
            if (plural.ToLower().EndsWith("ies")) return plural.Replace("ies", "y").Replace("IES", "Y");
            if (plural.ToLower().EndsWith("s")) return plural.Remove(plural.Length - 1);
            return plural;
        }

        public static string GenerateHTMLString(DataTable dt)
        {
            string tab = "\t";

            StringBuilder sb = new StringBuilder();
            sb.Append("<head><link href=\"style.css\" rel=\"stylesheet\" type=\"text/css\" /></head>");
            sb.AppendLine("<html>");
            sb.AppendLine(tab + "<body>");
            sb.AppendLine(tab + tab + "<table>");

            // headers.
            sb.Append(tab + tab + tab + "<tr>");

            foreach (DataColumn dc in dt.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", dc.ColumnName);
            }

            sb.AppendLine("</tr>");

            // data rows
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(tab + tab + tab + "<tr>");

                foreach (DataColumn dc in dt.Columns)
                {
                    string cellValue = dr[dc] != null ? dr[dc].ToString() : "";
                    sb.AppendFormat("<td>{0}</td>", cellValue);
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine(tab + tab + "</table>");
            sb.AppendLine(tab + "</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        public static bool CreateFile(string file_name, string content)
        {
            try
            {
                using (StreamWriter outfile = new StreamWriter(file_name, false))
                {
                    outfile.Write(content);
                }
                return true;
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                return false;
            }
        }

        public static List<ToolStripMenuItem> GetItems(MenuStrip menuStrip)
        {
            List<ToolStripMenuItem> myItems = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem i in menuStrip.Items)
            {
                GetMenuItems(i, myItems);
            }
            return myItems;
        }

        private static void GetMenuItems(ToolStripMenuItem item, List<ToolStripMenuItem> items)
        {
            items.Add(item);
            foreach (ToolStripItem i in item.DropDownItems)
            {
                if (i is ToolStripMenuItem)
                {
                    GetMenuItems((ToolStripMenuItem)i, items);
                }
            }
        }
    }
}
