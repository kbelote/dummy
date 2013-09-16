using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.SharePoint;
using Kent.Boogaart.KBCsv;
using Kent.Boogaart.KBCsv.Extensions;

namespace BulkUpload
{
    public class TextValuePair
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public TextValuePair()
            : this(string.Empty)
        {
        }
        public TextValuePair(string text)
            : this(text, text)
        {
        }

        public TextValuePair(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {

            #region "Old Code"
            /*
            List<BulkUpload> registerDevices = new List<BulkUpload>();
            try
            {
                DataTable dt = GetDataTable();

                dt.Columns.Add("Status");
                dt.Columns.Add("Message");
                for (int cnt = 0; cnt < dt.Rows.Count; cnt++)
                {
                    DataRow row = dt.Rows[cnt];
                    registerDevices.Add(new BulkUpload(UploadTypes.RegisterDevice, row));                   
                }

                DataTable dtDestination = dt.Copy();
                dtDestination.Rows.Clear();
               
                foreach (BulkUpload reg in registerDevices)
                {
                    if (reg.CurrentClass.Count == 0)
                    {
                        reg.CurrentClass.InsertObject();
                        
                    }


                    dtDestination.ImportRow(reg.CurrentClass.Row);

                       
                }

                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // new BulkUpload(UploadTypes.MDM).CurrentClass.Write();
             */

            /*  using (SPSite site = new SPSite("http://demo:2013"))
              {
                  using (SPWeb web = site.OpenWeb())
                  {

                      try
                      {

                          #region Bulk register started

                          SPList list = web.Lists["BulkUpload"];
                          SPFolder fldErrorLogs = web.Folders["ErrorLogs"];

                          SPQuery query = new SPQuery();
                          query.Query = "<Where><FieldRef Name=\"Status\" /><Value Type=\"Choice\">Pending</Value></Where>";
                          query.ViewAttributes = "Scope='Recursive'";

                          SPListItemCollection items = list.GetItems(query);
                          for (int i = 0; i < items.Count; i++)
                          {
                              SPListItem item = items[i];
                              SPFile spfile = item.File;
                              string uploadedFileName = spfile.Name;

                              string logFileName = "Error_Log_" + item.ID.ToString() + "_" + uploadedFileName;

                              if (uploadedFileName.ToLower().EndsWith("csv"))
                              {
                                  try
                                  {
                                      using (CsvReader csvReader = new CsvReader(spfile.OpenBinaryStream()))
                                      {
                                          try
                                          {
                                              item["Status"] = "In Progress";
                                              item.Update();

                                              csvReader.ReadHeaderRecord();
                                              DataSet dsBulkUpload = new DataSet();
                                              int ret = DataExtensions.Fill(dsBulkUpload, csvReader, "BulkUpload");

                                              #region Check Data Validity and Call Service

                                              #endregion

                                          }
                                          catch (Exception ex)
                                          {
                                          }
                                      }
                                  }
                                  catch (Exception ex)
                                  {

                                  }
                              }
                          }

                          #endregion

                      }
                      catch (Exception ex)
                      {
                      }
                  }
              }*/

            #endregion

            new BUpload().GetPendingFiles();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
