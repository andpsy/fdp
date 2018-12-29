// The authors disclaims copyright of this project. Use at your own risk.
// For bugs report, feature request, discussions, supports, please visit:
// http://mysqlbackupnet.codeplex.com/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MySql.Data.MySqlClient
{
    public class Table
    {
        private long _totalRows = -1;
        public string TableName { get; set; }
        public string CreateTableSql { get; set; }
        public long TotalRows { get { return _totalRows; } }
        public string[] ColumnNames
        {
            get
            {
                string[] sa = new string[ColumnDataType.Count];
                int count = -1;
                foreach (KeyValuePair<string, string> kv in ColumnDataType)
                {
                    count += 1;
                    sa[count] = kv.Key;
                }
                return sa;
            }
        }

        public Dictionary<string, string> ColumnDataType { get; set; }

        public Table(string tableName, ref MySqlCommand cmd)
        {
            _totalRows = 0;
            Methods md = new Methods();
            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            TableName = tableName;
            CreateTableSql = md.GetCreateTableSql(tableName, cmd);

            cmd.CommandText = "SHOW COLUMNS FROM `" + tableName + "`;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da = null;

            ColumnDataType = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                ColumnDataType.Add(dr["Field"].ToString(), dr["Type"].ToString().ToLower());
            }

            md = null;
        }

        public long GetTotalRows(ref MySqlCommand cmd)
        {
            if (_totalRows < 1)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM `" + TableName + "`;";
                _totalRows = (long)cmd.ExecuteScalar();
            }
            return _totalRows;
        }
    }
}