using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace FDP
{
    static class CsvImporter
    {
        public static DataSet Import(string fileName, bool headersOnFirstRow, char delimiter)
        {
            Int32 lineNo = 0;
            DataSet ds = new DataSet("CSV_IMPORTED");
            DataTable dt = new DataTable("CSV_IMPORTED");
            try
            {
                Int32 colNo = 0;
                var sr = new StreamReader(fileName);
                String line = sr.ReadLine();
                string[] values = line.Split(delimiter);
                colNo = values.Length;
                //var hst = new Hashtable();

                while (line != null)
                {
                    try
                    {
                        colNo = colNo > values.Length ? colNo : line.Split(delimiter).Length;
                        line = sr.ReadLine();
                        continue;
                    }
                    catch { }
                }

                try
                {
                    var dc = new DataColumn();
                    if (headersOnFirstRow)
                    {
                        int colindex = 0;
                        for (int col = 0; col < colNo; col++)
                        {
                            try
                            {
                                dc = new DataColumn();
                                if (values.Length <= col || Convert.ToString(values[col]).Trim() == "") colindex++;
                                dc.ColumnName = (values.Length > col && Convert.ToString(values[col]).Trim() != "") ? Convert.ToString(values[col]) : String.Format("F{0}", colindex);
                                dc.DataType = Type.GetType("System.String");
                                dt.Columns.Add(dc);
                            }
                            catch(Exception exp)
                            {
                                exp.ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int col = 0; col < colNo; col++)
                        {
                            try
                            {
                                dc = new DataColumn();
                                dc.ColumnName = "F" + Convert.ToString(col + 1);
                                dc.DataType = Type.GetType("System.String");
                                dt.Columns.Add(dc);
                            }
                            catch {
                                break;
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    exp.ToString();
                }
                sr.Close();
                dt.AcceptChanges();

                sr = new StreamReader(fileName);
                line = sr.ReadLine();
                while (line != null)
                {
                    try
                    {
                        values = line.Split(delimiter);
                        if (headersOnFirstRow && lineNo == 0)
                        {
                            lineNo++;
                            line = sr.ReadLine();
                            continue;
                        }

                        DataRow dr = dt.NewRow();
                        //for(int col=0; col<colNo; col++)
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            try
                            {
                                string colName = dt.Columns[col].ColumnName;
                                dr[colName] = Convert.ToString(values[col]);
                            }
                            catch { }
                        }
                        dt.Rows.Add(dr);
                        lineNo++;
                    }
                    catch (Exception exp)
                    {
                        exp.ToString();
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
                dt.AcceptChanges();
            }
            catch (Exception exp) { throw exp; }
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
