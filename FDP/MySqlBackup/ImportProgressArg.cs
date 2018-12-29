using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    public class ImportProgressArg : EventArgs
    {
        /// <summary>
        /// Number of processed bytes in current import process.
        /// </summary>
        public long CurrentByte { get; set; }

        /// <summary>
        /// Total bytes to be processed.
        /// </summary>
        public long TotalBytes { get; set; }

        /// <summary>
        /// The error information that encounter.
        /// </summary>
        public Exception Error { get; set; }
        public long CurrentLineNo { get; set; }

        /// <summary>
        /// Gets or Sets the SQL statement that creates the error (exception).
        /// </summary>
        public string ErrorSql { get; set; }

        /// <summary>
        /// Percentage of completeness.
        /// </summary>
        public int PercentageCompleted { get; set; }
    }
}
