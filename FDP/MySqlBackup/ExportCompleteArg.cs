using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    public class ExportCompleteArg
    {
        /// <summary>
        /// The Starting time of export process.
        /// </summary>
        public DateTime TimeStart;

        /// <summary>
        /// The Ending time of export process.
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
        /// Exception that occur during the process.
        /// </summary>
        public Exception Error = null;

        /// <summary>
        /// The completion type of current export processs.
        /// </summary>
        public CompleteType CompletedType = CompleteType.Completed;

        /// <summary>
        /// Total time used in current export process.
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

        public ExportCompleteArg()
        { }
    }
}
