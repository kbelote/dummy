using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BulkUpload
{
    public class BulkUpload
    {
        private UploadTypes UploadType;
        public IBaseClass CurrentClass;
        public DataRow Row;


        public BulkUpload(UploadTypes uploadType, DataRow row)
        {
            this.Row = row;
            this.UploadType = uploadType;
            this.SetClass();
           

            
        }


        private void SetClass()
        {


            switch (this.UploadType)
            {
                case UploadTypes.None:
                    this.CurrentClass = null;
                    break;
                case UploadTypes.RegisterDevice:
                    this.CurrentClass = new RegisterDevice(  this.Row);

                    break;
                case UploadTypes.MDM:
                    this.CurrentClass = new MDM();
                    break;
            }
        }
    }
}
