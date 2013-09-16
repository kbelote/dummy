using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BulkUpload
{
    public interface IBaseClass:IList<Logs>
    {
        DataRow Row { get; }
        void Write(LogTypes logType, string message);
        void SetVariables();
        void InsertObject();
    }
}
