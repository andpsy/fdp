using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace FDP_Admin
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
                if (((System.Windows.Forms.ComboBox)control).SelectedIndex < 0)
                    return DBNull.Value;
                if (((System.Windows.Forms.ComboBox)control).DataSource != null && (((System.Windows.Forms.ComboBox)control).SelectedValue==null || Convert.ToInt32(((System.Windows.Forms.ComboBox)control).SelectedValue) == -1))
                    return DBNull.Value;
                return ((System.Windows.Forms.ComboBox)control).DataSource != null ? ((System.Windows.Forms.ComboBox)control).SelectedValue : ((System.Windows.Forms.ComboBox)control).SelectedItem;
            }
            return control.ToString();
        }

        public static void SetDateFormat(System.Windows.Forms.Form form)
        {
            foreach (System.Windows.Forms.Control ctrl in form.Controls)
            {
                if (ctrl.GetType().Name == "DateTimePicker")
                {
                    try
                    {
                        ((System.Windows.Forms.DateTimePicker)ctrl).CustomFormat = SettingsClass.DateFormat;
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
                        ((System.Windows.Forms.DateTimePicker)child_ctrl).CustomFormat = SettingsClass.DateFormat;
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

        public static ArrayList DateDifferenceInMonths(DateTime d1, DateTime d2)
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

    }
}
