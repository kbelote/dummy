using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace BulkUpload
{
    public enum LogTypes
    {
        None,
        Infomration,
        Error
    }

    public enum UploadTypes
    {
        None,
        MDM,
        RegisterDevice
    }

    public enum CollectionObjects
    {
        PlatForms,
        Groups,
        Templates,
        PhoneNumbers
    }

    public static  class CommonFunction
    {
        private static Dictionary<CollectionObjects, List<TextValuePair>> ObjectCollection { get; set; }
        private static List<CollectionObjects> EnumList;

       static  CommonFunction()
        {
            EnumList = new List<CollectionObjects>();
            EnumList.Add(CollectionObjects.PlatForms);
            EnumList.Add(CollectionObjects.Groups);
            EnumList.Add(CollectionObjects.PhoneNumbers);
            EnumList.Add(CollectionObjects.Templates);
            ObjectCollection = new Dictionary<CollectionObjects, List<TextValuePair>>();
            FillObject();
        }

       public static bool IsEmail(string emailAddress)
       {
           string emailValidationPattern = @"^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$";

           return new Regex(emailValidationPattern, RegexOptions.IgnoreCase).IsMatch(emailAddress);

       }

       public static bool IsValidString(string value, int minimumLength=1, int maxLength=50)
       {
           if (value.Trim().Length < minimumLength || value.Trim().Length > maxLength)
           {
               return false;
           }
           return true;
       }

        public static bool IsValueContain(CollectionObjects valueType, string Text, out string Value)
        {
            List<TextValuePair> textValueCollection = new List<TextValuePair>();
            bool returnValue = false;
            Value = string.Empty;
            if (ObjectCollection.TryGetValue(valueType, out  textValueCollection))
            {
                TextValuePair v1 = (from p in textValueCollection
                                    where p.Text.ToLower() == Text.ToLower()
                                    select p).FirstOrDefault<TextValuePair>();
                if (v1 != null)
                {
                    returnValue = true;
                    Value = v1.Value;
                }

            }

            return returnValue;
        }

        private static void FillObject()
        {

            List<TextValuePair> textValueCollection ;
            foreach (CollectionObjects cobject in EnumList)
            {
                textValueCollection = new List<TextValuePair>();
                switch (cobject)
                {
                    case CollectionObjects.PlatForms:
                        textValueCollection.Add(new TextValuePair("ios"));
                        textValueCollection.Add(new TextValuePair("android"));
                        ObjectCollection.Add(cobject, textValueCollection);
                        break;
                    case CollectionObjects.Groups:
                        textValueCollection.Add(new TextValuePair("Group 1", "1"));
                        textValueCollection.Add(new TextValuePair("Group 2", "2"));
                        ObjectCollection.Add(cobject, textValueCollection);
                        break;
                    case CollectionObjects.PhoneNumbers:
                        textValueCollection.Add(new TextValuePair("phone"));
                        textValueCollection.Add(new TextValuePair("pda"));
                        ObjectCollection.Add(cobject, textValueCollection);
                        break;
                    case CollectionObjects.Templates :
                        textValueCollection.Add(new TextValuePair("Template 1", "1"));
                        textValueCollection.Add(new TextValuePair("Template 2", "2"));
                        ObjectCollection.Add(cobject, textValueCollection);
                        break;
                }
            }
        }

    }

}