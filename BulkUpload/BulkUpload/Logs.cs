using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkUpload
{
    public class Logs
    {
        LogTypes logType { get; set; }
        string Message { get; set; }
        protected internal Logs()
            : this(LogTypes.None, string.Empty)
        {
        }

        protected internal Logs(LogTypes logtype, string message)
        {
            this.logType = logtype;
            this.Message = message; 
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.logType.ToString(), Message);
        }
     

    }
}
