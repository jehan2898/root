using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Linq;
namespace mbs.lawfirm
{
    public class SrvLawfirm
    {
        public DataSet SelectGBBHeader(string p_sAssignedLawfirm)
        {
            string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            connection = new SqlConnection(connectionString);
            DataSet dataSet = new DataSet();
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_lf_ggb_header", connection);
                selectCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@sz_assigned_lawfirm_id", p_sAssignedLawfirm);
                new SqlDataAdapter(selectCommand).Fill(dataSet);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dataSet;
        }

        public string MarkBillsDownloaded(DataTable p_Table,string sz_user_id)
        {
            string strCnn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            SqlConnection conn = new SqlConnection(strCnn);
            string returnvalue = "";

            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("sp_update_lf_billdownloaded", conn);
                sqlCommand.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParam_Office = sqlCommand.Parameters.AddWithValue("@tvp_bill", p_Table);
                sqlCommand.Parameters.AddWithValue("@sz_user_id", sz_user_id);
                tvpParam_Office.SqlDbType = SqlDbType.Structured;
                tvpParam_Office.TypeName = "typ_bill";
                sqlCommand.ExecuteNonQuery();
                SqlDataReader dr = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                    conn = null;
                }
            }
            return returnvalue;
        }

        public static string DatatabletoExcel(DataTable dt, string file)
        {
            try
            {
                dt.WriteXml(file);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return file;
        }

        public static string GenerateXL(DataTable dt, string file)
        {
            int maxStringLength = dt.AsEnumerable()
                              .SelectMany(row => row.ItemArray.OfType<string>())
                              .Max(str => str.Length);

            string BlankSpace = new string(' ', maxStringLength+5);
            //todo: add required parameters to this function
            //DataColumn column = null;
            OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", file, ";Extended Properties='Excel 12.0;HDR=Yes;'"));
            try
            {
                try
                {
                    oleDbConnection.Open();
                    StringBuilder stringBuilder = new StringBuilder();
                    int num = 0;
                    string str = "";
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (num != 0)
                        {
                            stringBuilder.Append(string.Concat(", [", column.ColumnName.Replace(" ", "_").Replace("#", "No").Replace("\'", "-"), "]"));
                            str = string.Concat(str, ", ", column.ColumnName.Replace(" ", "_").Replace("#", "No").Replace("\'", "-"), " nvarchar(50)");
                        }
                        else
                        {
                            stringBuilder.Append(string.Concat("[", column.ColumnName.Replace(" ", "_").Replace("#", "No").Replace("\'", "-"), "]"));
                            str = string.Concat(column.ColumnName.Replace(" ", "_").Replace("#", "No").Replace("\'", "-"), " nvarchar(50)");
                        }
                        num++;
                    }
                    OleDbCommand oleDbCommand = new OleDbCommand();

                    oleDbCommand.Connection = oleDbConnection;
                    oleDbCommand.CommandText = "drop table [Sheet1$]";
                    
                    oleDbCommand.ExecuteNonQuery();
                    oleDbCommand.CommandText = string.Concat("create table [Sheet1$](", str, ") ");
                    oleDbCommand.ExecuteNonQuery();
                    foreach (DataRow row in dt.Rows)
                    {
                        num = 0;
                        StringBuilder stringBuilder1 = new StringBuilder();
                        foreach (DataColumn dataColumn in dt.Columns)
                        {
                            if (num == 0)
                            {
                                if (!(row[dataColumn.ColumnName] is long))
                                {
                                    stringBuilder1.Append(string.Concat("'", row[dataColumn.ColumnName].ToString().Replace("\'", "-").Trim()+ BlankSpace, "'"));
                                }
                                else
                                {
                                    stringBuilder1.Append(string.Concat("'", row[dataColumn.ColumnName].ToString().Replace("\'", "-").Trim()+ BlankSpace, "'"));
                                }
                            }
                            else if (!(row[dataColumn.ColumnName] is long))
                            {
                                stringBuilder1.Append(string.Concat(",'", row[dataColumn.ColumnName].ToString().Replace("\'", "-").Trim() + BlankSpace, "'"));
                            }
                            else
                            {
                                stringBuilder1.Append(string.Concat(",'", row[dataColumn.ColumnName].ToString().Replace("\'", "-").Trim() + BlankSpace, "'"));
                            }
                            num++;
                        }
                        OleDbCommand oleDbCommand1 = new OleDbCommand();

                        oleDbCommand1.CommandType = CommandType.Text;
                        oleDbCommand1.CommandTimeout = 0;

                        //#region Workaround
                        //oleDbCommand1.CommandText = "";
                        //oleDbCommand1.Connection = oleDbConnection;
                        //oleDbCommand1.ExecuteNonQuery();
                        //#endregion

                        object[] objArray = new object[] { "INSERT INTO [Sheet1$] (", stringBuilder, ")values(", stringBuilder1, ")" };
                        oleDbCommand1.CommandText = string.Concat(objArray);
                        oleDbCommand1.Connection = oleDbConnection;
                        oleDbCommand1.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
            finally
            {
                oleDbConnection.Close();
            }
            return file;
        }
    }
}
