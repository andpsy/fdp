using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    public class ExportProgressArg : EventArgs
    {
        /// <summary>
        /// The name of table that is currently processing.
        /// </summary>
        public string CurrentTableName { get; set; }

        /// <summary>
        /// Total of rows in current processing table.
        /// </summary>
        public long TotalRowsInCurrentTable { get; set; }

        /// <summary>
        /// Total of rows in all tables.
        /// </summary>
        public long TotalRowsInAllTables { get; set; }

        /// <summary>
        /// Current processing row in current table.
        /// </summary>
        public long CurrentRowInCurrentTable { get; set; }

        /// <summary>
        /// Current processing row in all table.
        /// </summary>
        public long CurrentRowInAllTable { get; set; }

        /// <summary>
        /// Total tables that will be exported.
        /// </summary>
        public int TotalTables { get; set; }

        /// <summary>
        /// Non-zero base index of current processing table.
        /// </summary>
        public int CurrentTableIndex { get; set; }

        /// <summary>
        /// Percentage of completeness.
        /// </summary>
        public int PercentageCompleted { get; set; }

        /// <summary>
        /// Percentage of completeness of getting total rows in all tables.
        /// </summary>
        public int PercentageGetTotalRowsCompleted { get; set; }
    }
}
