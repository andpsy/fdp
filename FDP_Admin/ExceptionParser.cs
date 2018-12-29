using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDP_Admin
{
    static class ExceptionParser
    {
        public static Dictionary<int, string> MySqlErrorCodes = new Dictionary<int, string>() {
            {1451, "foreignKeyConstraintFail"}
            // to do: to add MySqlErrors table
        };
        
        public static string ParseException(Exception exp)
        {
            if (exp is MySql.Data.MySqlClient.MySqlException)
            {
                try
                {
                    //return Language.GetErrorText( MySqlErrorCodes[((MySql.Data.MySqlClient.MySqlException)exp).Number], exp.Message);
                    return exp.Message;
                }
                catch { return exp.Message; }

            }
            return exp.Message;
        }
    }
}
