using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PayamDowlat
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {

           
            if (FUPayamDowlat.HasFile)

            {
                string FileName = Path.GetFileName(FUPayamDowlat.PostedFile.FileName);

                string Extension = Path.GetExtension(FUPayamDowlat.PostedFile.FileName);

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);

                var f = new MemoryStream(FUPayamDowlat.FileBytes);

                using (var stream = f)

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    var result = reader.AsDataSet(new ExcelDataSetConfiguration() //use library of excsell reader nuget and use help of document to add 
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            EmptyColumnNamePrefix = "Column",
                            UseHeaderRow = true,
                            FilterColumn = (rowReader, columnIndex) =>
                            {
                                return true;
                            }
                        }
                    });

                    DGVpayamDowlat.DataSource = result;
                    DGVpayamDowlat.DataBind();
                    string connectionString = @"Data Source=Geek;Initial Catalog=IstgPayamDowlat;Integrated Security=True";


                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            // Configure the command and parameter.  
                            SqlCommand insertCommand = new SqlCommand("sp_Tvp", conn);
                            insertCommand.CommandType = CommandType.StoredProcedure;
                            var param = new SqlParameter()
                            {
                                SqlDbType = SqlDbType.Structured,
                                Value = result.Tables[0],
                                ParameterName = "@TvpAddressBook"
                            };
                            SqlParameter tvp = insertCommand.Parameters.Add(param);
                            // Execute the command.  
                            insertCommand.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Error : " + ex.Message.ToString());
                        }
                    }
                }

            }
        }
    }
}