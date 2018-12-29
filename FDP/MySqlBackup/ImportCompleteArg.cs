using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    public class ImportCompleteArg
    {
        /// <summary>
        /// The starting time of import process.
        /// </summary>
        public DateTime TimeStart;

        /// <summary>
        /// The ending time of import process.
        /// </summary>
        public DateTime TimeEnd;

        /// <summary>
        /// Enum of completion type
        /// </summary>
        public enum CompleteType
        {
            Completed,
            Cancelled,
            Error
        }

        /// <summary>
        /// The number of line in dump file when import process stopped.
        /// </summary>
        public long CurrentLineNo { get; set; }

        /// <summary>
        /// Indicates whether the import process has error(s).
        /// </summary>
        public bool HasErrors = false;

        /// <summary>
        /// The last error (exception) occur in import process.
        /// </summary>
        public Exception LastError = null;

        // <summary>
        /// The completion type of current import processs.
        /// </summary>
        public CompleteType CompletedType = CompleteType.Completed;

        /// <summary>
        /// The collection of errors. Exceptions will be collected if Ignore SQL Error has set to true in Import Info.
        /// </summary>
        public Dictionary<long, Exception> Errors = new Dictionary<long, Exception>();

        /// <summary>
        /// Total time used in current import process.
        /// </summary>
        public TimeSpan TimeUsed
        {
            get
            {
                TimeSpan ts = new TimeSpan();
                ts = TimeEnd - TimeStart;
                return ts;
            }
        }

        public ImportCompleteArg(CompleteType completeType)
        {
            CompletedType = completeType;
        }
    }
}
