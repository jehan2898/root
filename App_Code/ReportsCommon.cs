using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

/// <summary>
/// Summary description for ReportsCommon
/// </summary>
public class ReportsCommon
{
    string strsqlcon;
    SqlConnection conn;
    SqlCommand comm;

    public ReportsCommon()
    {
        strsqlcon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
    }

    public int fetchFilterCode(string filterName)
    {
        try
        {
            SqlConnection conn;
            SqlCommand comm;
            conn = new SqlConnection(strsqlcon);
            SqlDataAdapter adpt = null;
            DataSet ds = new DataSet();
            int retFilterid;

            conn.Open();
            comm = new SqlCommand("fecth_reporting_filter_id");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_filter_name", filterName);
            retFilterid = Convert.ToInt16(comm.ExecuteScalar());
            conn.Close();
            return retFilterid;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return -1;
        }
    }

    public int fetchReportID(string reportName)
    {
        try
        {
            SqlConnection conn;
            SqlCommand comm;
            conn = new SqlConnection(strsqlcon);
            int retReportid;

            conn.Open();
            comm = new SqlCommand("fecth_reporting_report_id");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_report_name", reportName);
            retReportid = Convert.ToInt16(comm.ExecuteScalar());
            conn.Close();
            return retReportid;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return -1;
        }
    }

    public int fetchUserReportID(ArrayList arrParam)
    {
        try
        {
            SqlConnection conn;
            SqlCommand comm;
            conn = new SqlConnection(strsqlcon);
            int retReportid;

            conn.Open();
            comm = new SqlCommand("fecth_user_report_id");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@i_mst_report_id", arrParam[0]);
            comm.Parameters.AddWithValue("@sz_report_name", arrParam[1]);
            comm.Parameters.AddWithValue("@sz_user_id", arrParam[2]);
            comm.Parameters.AddWithValue("@sz_company_id", arrParam[3]);
            retReportid = Convert.ToInt16(comm.ExecuteScalar());
            conn.Close();
            return retReportid;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return -1;
        }
    }

    public void exportDataExcel(DataSet dsTemp, string reportName)
    {
        ArrayList paramArraylist = new ArrayList();
        string Excelpath = ConfigurationSettings.AppSettings["ExcelFile"] + "CarrierReports.xlsx";
        string dateTime = "CarrierReports " + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + ".xlsx";
        string Destpath = ConfigurationSettings.AppSettings["ExcelFile"] +dateTime ;
        DataSet DsExportedData = (DataSet)dsTemp;

        File.Copy(Excelpath,Destpath);
      
        Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        Microsoft.Office.Interop.Excel._Workbook workbook = XcelApp.Workbooks.Add(Type.Missing);
        Microsoft.Office.Interop.Excel._Workbook workbookDummy = XcelApp.Workbooks.Add(Type.Missing);
        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        Microsoft.Office.Interop.Excel.Range chartrange;

        #region Excel
        //workbook = XcelApp.Workbooks.Open(Destpath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true);  SVN

        try
        {
            if (DsExportedData != null)
            {
                if (DsExportedData.Tables[0] != null)
                {
                    #region TemplateData
                    worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["TemplateData"];
                    //worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
                    
                    DataTable dtTemplateData = (DataTable)DsExportedData.Tables[0];
                    for (int j = 0; j < dtTemplateData.Rows.Count; j++)
                    {
                        for (int k = 0; k < dtTemplateData.Columns.Count; k++)
                        {
                            worksheet.Cells[j + 2, k + 1] = dtTemplateData.Rows[j].ItemArray[k].ToString();
                        }
                    }

                    chartrange = worksheet.get_Range("A1", "D1");
                    chartrange.Font.Bold = true;
                    chartrange.Cells.Interior.Color = System.Drawing.Color.Gainsboro.ToArgb();
                    chartrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    chartrange.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

                    XcelApp.Columns.AutoFit();
                    workbook.Save();
                    #endregion
                }

                if (DsExportedData.Tables[1] != null)
                {
                    #region TemplateBillingData
                    worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["TemplateBillingData"];
                    //worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                    DataTable dtTemplateBillingData = (DataTable)DsExportedData.Tables[1];
                    for (int j = 0; j < dtTemplateBillingData.Rows.Count; j++)
                    {
                        for (int k = 0; k < dtTemplateBillingData.Columns.Count; k++)
                        {
                            worksheet.Cells[j + 2, k + 1] = dtTemplateBillingData.Rows[j].ItemArray[k].ToString();
                        }
                    }

                    chartrange = worksheet.get_Range("A1", "E1");
                    chartrange.Font.Bold = true;
                    chartrange.Cells.Interior.Color = System.Drawing.Color.Gainsboro.ToArgb();
                    chartrange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    chartrange.Borders.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);

                    XcelApp.Columns.AutoFit();
                    workbook.RefreshAll();
                    workbook.Save();
                    #endregion
                }
                else
                {
                }
            }
            else
            {
            }
           // workbook.Close();  SVN
        }
        catch (Exception ex)
        {
           // workbook.Close();   SVN
            ErrorHandler.WriteError(ex.Message);

        }
        #endregion
    }

    public void saveReportData(ArrayList arraylist)
    {
        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(strsqlcon);

        try
        {
            conn.Open();
            comm = new SqlCommand("sp_reporting_patient_list");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@sz_company_id", arraylist[0]);
            comm.Parameters.AddWithValue("@sz_user_id", arraylist[1]);
            comm.Parameters.AddWithValue("@i_report_id", arraylist[2]);
            comm.Parameters.AddWithValue("@i_filter_id", arraylist[3]);
            comm.Parameters.AddWithValue("@sz_selectedvalues", arraylist[4]);
            comm.Parameters.AddWithValue("@sz_whereclscol", arraylist[5]);
            comm.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
    }

    public DataSet load_report_data(string companyid, int reportid, string reportName)
    {
        conn = new SqlConnection(strsqlcon);
        DataSet ds = new DataSet();
        SqlDataAdapter adpt = null;
        try
        {
            conn.Open();
            comm = new SqlCommand("fetch_reporting_data", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@sz_comany_id", companyid);
            comm.Parameters.AddWithValue("@i_report_id", reportid);
            comm.Parameters.AddWithValue("@sz_report_name", reportName);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);
        }
        catch (Exception ex) { Elmah.ErrorSignal.FromCurrentContext().Raise(ex); }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public int saveReportNameData(ArrayList arraylist)
    {
        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(strsqlcon);
        int i = 0;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_save_user_report_name");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@sz_company_id", arraylist[0]);
            comm.Parameters.AddWithValue("@sz_user_id", arraylist[1]);
            comm.Parameters.AddWithValue("@i_report_id", arraylist[2]);
            comm.Parameters.AddWithValue("@sz_user_report_name", arraylist[3]);
            i=comm.ExecuteNonQuery();
            return i;
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return -1;
        }
        finally
        {
            conn.Close();
        }
    }

    public int saveReportMenuNameData(ArrayList arraylist)
    {
        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(strsqlcon);
        int i = 0;
        try
        {
            conn.Open();
            comm = new SqlCommand("sp_save_user_report_menu_name");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;

            comm.Parameters.AddWithValue("@sz_company_id", arraylist[0]);
            comm.Parameters.AddWithValue("@sz_user_id", arraylist[1]);
            comm.Parameters.AddWithValue("@i_report_id", arraylist[2]);
            comm.Parameters.AddWithValue("@sz_user_report_name", arraylist[3]);
            i = comm.ExecuteNonQuery();
            return i;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return -1;
        }
        finally
        {
            conn.Close();
        }
    }

    public static void loadSavedData(string source, string[] stringSeparators, ASPxListBox box)
    {
        try
        {
            if (box != null)
            {
                if (source == "Select All")
                {
                    ListEditItem item = box.Items.FindByValue("NA");
                    if (item != null)
                    {
                        foreach (ListEditItem itemLst in ((ASPxListBox)box).Items)
                        {
                            itemLst.Selected = true;
                        }
                    }
                }
                else if (source != "" && source != string.Empty)
                {
                    string[] result;
                    result = source.Split(stringSeparators, StringSplitOptions.None);

                    foreach (string s in result)
                    {
                        string istr = s.Remove(s.Length - 1, 1);
                        istr = istr.Remove(0, 1);
                        ListEditItem item = box.Items.FindByValue(istr);
                        if (item != null)
                        {
                            item.Selected = true;
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    public string setValueToSaveData(System.Web.UI.Control name, string sname)
    {
        try
        {
            foreach (ListEditItem item in ((ASPxListBox)name).SelectedItems)
            {
                // if (item.Value.ToString() == "NA")
                if (item.Index == 0)
                {
                    sname = "Select All";
                    break;
                }
                else
                {
                    if (sname.Equals("") && (item.Index != 0))
                    {
                        sname = "'" + item.Value.ToString() + "'";
                    }
                    else if (sname != null && (item.Index != 0))
                    {
                        sname = sname + ",'" + item.Value.ToString() + "'";
                    }
                }
            }
            return sname;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return sname = "";
        }
    }

    public string setValueToDisplayFilter(System.Web.UI.Control name, string sname)
    {
        try
        {
            foreach (ListEditItem item in ((ASPxListBox)name).SelectedItems)
            {
                if (item.Index == 0)
                {
                    foreach (ListEditItem itemAll in ((ASPxListBox)name).SelectedItems)
                    {
                        if (itemAll.Index == 0)
                            continue;
                        sname = sname + itemAll.Text.ToString() + ",";
                    }
                    sname = sname.Remove(sname.Length - 1, 1);
                    break;
                }
                else
                {
                    if (sname.Equals("") && (item.Index != 0))
                    {
                        sname = item.Text.ToString();
                    }
                    else if (sname != null && (item.Index != 0))
                    {
                        sname = sname + "," + item.Text.ToString();
                    }
                }
            }
            return sname;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return sname = "";
        }
    }

    public DataTable fetchMyReportMenu(string sz_companyid,string sz_userid)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection conn;
            SqlCommand comm;
            conn = new SqlConnection(strsqlcon);
            SqlDataAdapter adpt = null;
            DataSet ds = new DataSet();
            

            conn.Open();
            comm = new SqlCommand("sp_fetchReport_menu");
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_id", sz_companyid);
            comm.Parameters.AddWithValue("@sz_user_id", sz_userid);
            adpt = new SqlDataAdapter(comm);
            adpt.Fill(ds);
            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return dt;
        }
    }

}