using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Data;
using Kent.Boogaart.KBCsv;
using Kent.Boogaart.KBCsv.Extensions;

namespace BulkUpload
{
    public class BUpload
    {

        SPFolder fldErrorLogs;
        public BUpload()
        {

        }

        private DataTable ProcessData(DataSet dataSet, UploadTypes updateType)
        {
            DataTable dtBulkUpload = dataSet.Tables[0];
   
            #region Check Data Validity and Call Service

            dtBulkUpload.Columns.Add("Status");
            dtBulkUpload.Columns.Add("Message");

            DataTable dtDestination = dtBulkUpload.Copy();
            dtDestination.Rows.Clear();


            List<BulkUpload> registerDevices = new List<BulkUpload>();

            for (int cnt = 0; cnt < dtBulkUpload.Rows.Count; cnt++)
            {
                DataRow row = dtBulkUpload.Rows[cnt];
                registerDevices.Add(new BulkUpload(updateType, row));
            }


            foreach (BulkUpload reg in registerDevices)
            {
                if (reg.CurrentClass.Count == 0)
                {
                    reg.CurrentClass.InsertObject();

                }


                dtDestination.ImportRow(reg.CurrentClass.Row);


            }



            #endregion

            return dtDestination;
        }

        private void ProcessFile(SPListItem item)
        {
            DataTable dtErrors = new DataTable();
            string uploadedFileName = item.File.Name;
            string logFileName = "Error_Log_" + item.ID.ToString() + "_" + uploadedFileName;
            if (uploadedFileName.ToLower().EndsWith("csv"))
            {
                try
                {
                    using (CsvReader csvReader = new CsvReader(item.File.OpenBinaryStream()))
                    {
                        try
                        {
                            item["Status"] = "In Progress";
                            item.Update();

                            csvReader.ReadHeaderRecord();
                            DataSet dsBulkUpload = new DataSet();
                            int ret = DataExtensions.Fill(dsBulkUpload, csvReader, "BulkUpload");

                            if (ret > 0)
                            {
                                dtErrors = this.ProcessData(dsBulkUpload, (UploadTypes)Enum.Parse(typeof(UploadTypes), item["LoadType"].ToString(), true));
                            }
                       
                            string fileContent = string.Empty;
                            string columnContent = string.Empty;
                            for (int i = 0; i < dtErrors.Columns.Count; i++)
                            {
                                fileContent += (fileContent != string.Empty ? "," : string.Empty) + dtErrors.Columns[i].ColumnName;
                            }

                            for (int row = 0; row < dtErrors.Rows.Count; row++)
                            {
                                for (int column = 0; column < dtErrors.Columns.Count; column++)
                                {
                                    columnContent += (columnContent != string.Empty ? "," : string.Empty) + dtErrors.Rows[row][column].ToString();
                                }
                                fileContent += "\n" + columnContent;
                                columnContent = string.Empty;
                            }
                          SPFile file =   fldErrorLogs.Files.Add(logFileName, ASCIIEncoding.ASCII.GetBytes ( fileContent));

                          file.Update();


                          SPFieldUrlValue urlValue = new SPFieldUrlValue();
                          urlValue.Description = logFileName;
                          urlValue.Url = file.ServerRelativeUrl ;

                          item["Status"] = "Completed";
                          item["Url"] = urlValue.ToString();
                          item.Update();


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
            else
            {

            }

        }

        public void GetPendingFiles()
        {
            SPListItemCollection items = null;
            using (SPSite site = new SPSite("http://kamlesh-lt:2013"))
            {
                using (SPWeb web = site.OpenWeb())
                {

                    try
                    {
                        SPList list = web.Lists["BulkUpload"];
                        fldErrorLogs = web.Folders["ErrorLogs"];

                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Eq><FieldRef Name=\"Status\" /><Value Type=\"Choice\">Pending</Value></Eq></Where>";
                        query.ViewAttributes = "Scope='Recursive'";

                        items = list.GetItems(query);
                        for (int i = 0; i < items.Count; i++)
                        {
                            this.ProcessFile(items[i]);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }



        }
    }

}
