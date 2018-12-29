using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FDP
{
    static class LogWriter
    {
        public static void Log(string logMessage, string errors_log_file)
        {
            TextWriter w = File.AppendText(errors_log_file);
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
            w.Close();
        }

        public static void Log(Exception exp, string errors_log_file)
        {
            TextWriter w = File.AppendText(errors_log_file);
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("Message : {0}", exp.Message);
            w.WriteLine("Source : {0}", exp.Source);
            w.WriteLine("InnerException : {0}", exp.InnerException);
            w.WriteLine("TargetSite : {0}", exp.TargetSite);
            w.WriteLine("StackTrace : {0}", exp.StackTrace);
            w.WriteLine("-------------------------------");
            // Update the underlying file.
            w.Flush();
            w.Close();
        }


        public static void DumpLog(StreamReader r)
        {
            // While not at the end of the file, read and write lines.
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            r.Close();
        }
    }
}
