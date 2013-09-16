using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BulkUpload
{
    public class RegisterDevice : List<Logs>, IBaseClass
    {
        #region "Variables"
        static int RecordNumber;
        private string _deviceName;
        private string _emailAddress;
        private string _szTemplateID;
        private string _Template;
        private string _group;
        private string _groupID;
        private string _firstName;
        private string _lastName;
        private string _platformType;
        private bool _isAddContainer;
        public DataRow _row;
        #endregion


        #region "Properties"

        public string DeviceName
        {
            get
            {
                return this._deviceName;
            }
            set
            {
                this._deviceName = value;
                if (!CommonFunction.IsValidString(value))
                {
                    this.Write(LogTypes.Error, "Invalid User Name");
                }
            }
        }

        public string TemplateID
        {
            get
            {
                return
                this._szTemplateID;
            }
        }

        public string Template
        {
            get
            {
                return this._Template;
            }
            set
            {
                this._Template = value;
                if (!CommonFunction.IsValueContain(CollectionObjects.Templates, value, out  this._szTemplateID))
                {
                    this.Write(LogTypes.Error, string.Format("Template name not found @ {0}", RecordNumber));
                }
            }
        }

        public string GroupName
        {
            get
            {
                return this._group;
            }
            set
            {
                this._group = value;
                if (!CommonFunction.IsValueContain(CollectionObjects.Groups, value, out  this._groupID))
                {
                    this.Write(LogTypes.Error, string.Format("Group name not found @ {0}", RecordNumber));
                }

            }
        }

        public string GroupID
        {
            get
            {
                return this._groupID;
            }

        }

        public string EmailAddress
        {
            get
            {
                return this._emailAddress;
            }
            set
            {
                this._emailAddress = value;
                if (!CommonFunction.IsValidString(value))
                {
                    this.Write(LogTypes.Error, "Invalid User Name");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
                if (!CommonFunction.IsValidString(value))
                {
                    this.Write(LogTypes.Error, string.Format("Error in record {0} for first name ", RecordNumber));
                }
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
                if (!CommonFunction.IsValidString(value))
                {
                    this.Write(LogTypes.Error, string.Format("Error in record {0} for last name ", RecordNumber));
                }
            }
        }

        public string Platform
        {
            get
            {
                return this._platformType;
            }
            set
            {
                this._platformType = value;
                if (!CommonFunction.IsValueContain(CollectionObjects.PlatForms, value, out this._platformType))
                {
                    this.Write(LogTypes.Error, string.Format("Error in record {0} for Platform Type", RecordNumber));
                }
            }
        }

        public bool AddContainer
        {
            get
            {
                return this._isAddContainer;
            }
            set
            {
                this._isAddContainer = value;
            }
        }


        public RegisterDevice()
        {
            RecordNumber++;
            this._emailAddress = this._group = this._groupID = this._szTemplateID = this._Template = this._deviceName = string.Empty;
        }

        public RegisterDevice(DataRow dataRow)
            : this()
        {
            this._row = dataRow;
            this.SetVariables();
            if (this.Count > 0)
            {
                this._row["Status"] = "Error";
                this._row["Message"] = this.ToString();
            }

        }

        public void Write(LogTypes logType, string message)
        {
            this.Add(new Logs(logType, message));
        }

        public override string ToString()
        {
            string messageString = string.Empty;

            for (int i = 0; i < this.Count; i++)
            {
                messageString += this[i].ToString();
            }

            return messageString;
        }

        #endregion



        public void SetVariables()
        {
            try
            {
                this.EmailAddress = Convert.ToString(this._row["EmailAddress"]);
                this.FirstName = Convert.ToString(this._row["FirstName"]);
                this.LastName = Convert.ToString(this._row["LastName"]);
                this.GroupName = Convert.ToString(this._row["GroupName"]);
                this.Platform = Convert.ToString(this._row["Platform"]);
                this.DeviceName = Convert.ToString(this._row["DeviceName"]);
                this.Template = Convert.ToString(this._row["TemplateName"]);
                this.AddContainer = (this.TemplateID != string.Empty) ? true : false;
            }
            catch (Exception ex)
            {
                this._row["Status"] = "Failed";
                this._row["Message"] = ex.Message.ToString();
            }
        }

        public void InsertObject()
        {
            try
            {
                if (true)
                {
                    this._row["Status"] = "Succeed";
                    this._row["Message"] = string.Format(" DeviceUUID :- {0}", "DeviceUUID");
                }
                else
                {
                    this._row["Status"] = "Failed";
                    this._row["Message"] = "Please check logs for details. Error in Service.";
                }
            }
            catch (Exception ex)
            {
                this._row["Status"] = "Failed";
                this._row["Message"] = ex.Message.ToString();
                this.Add(new Logs(LogTypes.Error, ex.Message));
            }
        }

        public DataRow Row
        {
            get
            {
                return _row;
            }

        }
    }
}
