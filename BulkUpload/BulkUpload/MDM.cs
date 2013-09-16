using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BulkUpload
{
    public class MDM : List<Logs>, IBaseClass
    {
        private DataRow _row;

        public MDM()
        {

        }

        public MDM(DataRow dataRow)
        {

        }

        public void Write(LogTypes logType, string message)
        {
            this.Add(new Logs(logType, message));
        }


        public void SetVariables()
        {
            throw new NotImplementedException();
        }


        public void InsertObject()
        {
            throw new NotImplementedException();
        }

        public DataRow Row
        {
            get
            {
                return this._row;
            }
        }
    }
}
