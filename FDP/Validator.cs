using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;

namespace FDP
{
    static class Validator
    {
        public static System.Boolean IsInteger(string Expression)
        {
            if (Expression.Trim() == "") return true;
            try
            {
                int x = Int32.Parse(Expression);
                return true;
            }
            catch { return false; }
        }

        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is string && (string)Expression == "") return true;
            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        public static System.Boolean IsCharactersOnly(string text)
        {
            Regex reg = new Regex(@"^[a-zA-Z''-'\s]{1,250}$");
            return reg.IsMatch(text);
        }

        public static System.Boolean IsPhoneNumber(string text)
        {
            Regex reg = new Regex(@"^[0-9- .]{6,20}$");
            return reg.IsMatch(text);
        }

        public static System.Boolean IsEmail(string text)
        {
            Regex reg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return reg.IsMatch(text);
        }


        public static ArrayList ValidateCNP(string cnp)
        {
            ArrayList errors = new ArrayList();
            try
            {
                long tmp = Convert.ToInt64(cnp);
            }
            catch
            {
                errors.Add(Language.GetErrorText("cnpNotNumeric", "CNP value is not numeric!"));
            }
            if (cnp.Length != 13)
            {
                errors.Add(Language.GetErrorText("cnpLengthError", "CNP length is invalid!"));
            }
            try
            {
                string sex = cnp.Substring(0, 1);
                string an = cnp.Substring(1, 2);
                string luna = cnp.Substring(3, 2);
                string zi = cnp.Substring(5, 2);
                string judet = cnp.Substring(7, 2);
                int suma_control = int.Parse(cnp[0].ToString()) * 2 + int.Parse(cnp[1].ToString()) * 7 + int.Parse(cnp[2].ToString()) * 9 + int.Parse(cnp[3].ToString()) * 1 +
                    int.Parse(cnp[4].ToString()) * 4 + int.Parse(cnp[5].ToString()) * 6 + int.Parse(cnp[6].ToString()) * 3 + int.Parse(cnp[7].ToString()) * 5 +
                    int.Parse(cnp[8].ToString()) * 8 + int.Parse(cnp[9].ToString()) * 2 + int.Parse(cnp[10].ToString()) * 7 + int.Parse(cnp[11].ToString()) * 9;
                int rest = suma_control % 11;
                if (cnp[0] != '1' && cnp[0] != '2' && cnp[0] != '5' && cnp[0] != '6')
                {
                    errors.Add(Language.GetErrorText("cnpSexError", "First letter of CNP is invalid!"));
                }
                if (int.Parse(zi) > 31 || int.Parse(zi) < 1)
                {
                    errors.Add(Language.GetErrorText("cnpDayError", "Birth day is invalid!"));
                }
                if (int.Parse(luna) > 12 || int.Parse(luna) < 1)
                {
                    errors.Add(Language.GetErrorText("cnpMonthError", "Birth month is invalid!"));
                }
                try
                {
                    DateTime d = new DateTime(int.Parse(an), int.Parse(luna), int.Parse(zi));
                }
                catch
                {
                    errors.Add(Language.GetErrorText("cnpDateError", "Birth date is invalid!"));
                }
                if (int.Parse(judet) > 52 || int.Parse(judet) < 1)
                {
                    errors.Add(Language.GetErrorText("cnpDistrictError", "District is invalid!"));
                }
                if (rest < 10 && rest != int.Parse(cnp[12].ToString()) || rest >= 10 && int.Parse(cnp[12].ToString()) != 1)
                {
                    errors.Add(Language.GetErrorText("cnpCheckError", "CNP control number is invalid!"));
                }
            }
            catch { }
            return errors;
        }

        public static bool SimpleValidateCNP(string cnp)
        {
            try
            {
                long tmp = Convert.ToInt64(cnp);
            }
            catch
            {
                return false;
            }
            if (cnp.Length != 13)
            {
                return false;
            }
            try
            {
                string sex = cnp.Substring(0, 1);
                string an = cnp.Substring(1, 2);
                string luna = cnp.Substring(3, 2);
                string zi = cnp.Substring(5, 2);
                string judet = cnp.Substring(7, 2);
                int suma_control = int.Parse(cnp[0].ToString()) * 2 + int.Parse(cnp[1].ToString()) * 7 + int.Parse(cnp[2].ToString()) * 9 + int.Parse(cnp[3].ToString()) * 1 +
                    int.Parse(cnp[4].ToString()) * 4 + int.Parse(cnp[5].ToString()) * 6 + int.Parse(cnp[6].ToString()) * 3 + int.Parse(cnp[7].ToString()) * 5 +
                    int.Parse(cnp[8].ToString()) * 8 + int.Parse(cnp[9].ToString()) * 2 + int.Parse(cnp[10].ToString()) * 7 + int.Parse(cnp[11].ToString()) * 9;
                int rest = suma_control % 11;
                if (cnp[0] != '1' && cnp[0] != '2' && cnp[0] != '5' && cnp[0] != '6')
                {
                    return false;
                }
                if (int.Parse(zi) > 31 || int.Parse(zi) < 1)
                {
                    return false;
                }
                if (int.Parse(luna) > 12 || int.Parse(luna) < 1)
                {
                    return false;
                }
                try
                {
                    DateTime d = new DateTime(int.Parse(an), int.Parse(luna), int.Parse(zi));
                }
                catch
                {
                    return false;
                }
                if (int.Parse(judet) > 52 || int.Parse(judet) < 1)
                {
                    return false;
                }
                if (rest < 10 && rest != int.Parse(cnp[12].ToString()) || rest >= 10 && int.Parse(cnp[12].ToString()) != 1)
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }
        
        public static bool SimpleValidateCUI(string cif)
        {
            try
            {
                string pureCui = cif.ToLower().Trim().Replace("ro", "").Trim();
                try
                {
                    long tmp = Convert.ToInt64(pureCui);
                }
                catch
                {
                    return false;
                }
                if (pureCui.Length > 10)
                {
                    return false;
                }

                string key = "753217532";
                char[] y = key.ToCharArray();
                Array.Reverse(y);
                string revkey = new string(y);

                char[] x = pureCui.ToCharArray();
                Array.Reverse(x);
                string revcif = new string(x);
                int sum = 0;
                for (int i = 1; i < revcif.Length; i++)
                {
                    sum += (int.Parse(revcif[i].ToString()) * int.Parse(revkey[i - 1].ToString()));
                }
                int rest = (sum * 10 % 11 == 10) ? 0 : sum * 10 % 11;
                return rest == int.Parse(revcif[0].ToString()) ? true : false;
            }
            catch { return false; }
        }

        public static System.Boolean IsDouble(string text)
        {
            if (text.Trim() == "") return true;
            try
            {
                double x = Double.Parse(text);
                return true;
            }
            catch { return false; }
        }

        public static bool PropertyIsAssignedToActiveContract(int property_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_is_assigned_to_active_contract", new object[] { new MySqlParameter("_PROPERTY_ID", property_id) });
            return Convert.ToInt32(da.ExecuteScalarQuery()) > 0 ? true : false;
        }

        public static bool DataGridViewCellVallidator(System.Windows.Forms.DataGridViewCell dgvc) //, System.Windows.Forms.DataGridViewCellValidatingEventArgs e)
        {
            bool toReturn = true;
            if (dgvc.IsInEditMode && (dgvc.EditType != null && dgvc.EditType.Name != "DataGridViewComboBoxEditingControl"))
            {
                switch (dgvc.ValueType.ToString())
                {
                    case "System.Decimal":
                    case "System.Double":
                        if (!Validator.IsDouble(dgvc.EditedFormattedValue.ToString()))
                        {
                            //dgvc.ErrorText = Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!");
                            System.Windows.Forms.MessageBox.Show(Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"), "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            toReturn = false;
                        }
                        else
                        {
                            //dgvc.ErrorText = "";
                        }
                        break;
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt64":
                    case "System.UInt32":
                    case "System.UInt16":
                    case "System.Byte":
                        if (!Validator.IsInteger(dgvc.EditedFormattedValue.ToString()))
                        {
                            //dgvc.ErrorText = Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!");
                            System.Windows.Forms.MessageBox.Show(Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"), "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            toReturn = false;
                        }
                        else
                        {
                            //dgvc.ErrorText = "";
                        }
                        break;
                    //TO DO: TO ADD VALIDATION FOR THE OTHER TYPES
                }
            }
            return toReturn;
        }
    }
}

