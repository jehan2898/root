using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using gb.web.utils;
using DevExpress.Web;

public partial class AJAX_Pages_MissingSpecialty : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            
           
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                BindMissingSpecialty();
            }
            BindGrid();
        }
        catch (Exception ex)
        {

        }
    }

    public DataSet loadMissingSpeciality(string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindMissingSpecialty()
    {
        DataSet ds = new DataSet();
        ds = loadMissingSpeciality(this.txtCompanyID.Text);
        //ASPxListBox lstMissingSpecialty = (ASPxListBox)this.grdSpeciality.FindControl("lstMissingSpecialty");
        //lstMissingSpecialty.ValueField = "CODE";
        //lstMissingSpecialty.TextField = "DESCRIPTION";
        grdSpeciality.DataSource = ds;
        grdSpeciality.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All ---", "NA");
        //lstMissingSpecialty.Items.Insert(0, Item);

      //  lstMissingSpecialty.SelectAll();
    }
    private DataTable GetMissingSpecailty(ArrayList arrSpecialty)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        DataTable dt = new DataTable();
        dt.Columns.Add("Speicalty");
        dt.Columns.Add("Patient");
        dt.Columns.Add("sz_case_no");
        dt.Columns.Add("SZ_CASE_TYPE_NAME");
        dt.Columns.Add("DT_DATE_OF_ACCIDENT");
        dt.Columns.Add("dt_created_date");
        dt.Columns.Add("sz_insurance_name");
        dt.Columns.Add("sz_claim_number");
        dt.Columns.Add("sz_patient_cellno");
        dt.Columns.Add("SZ_PATIENT_PHONE");
        dt.Columns.Add("DaysOpen");
        conn.Open();
        try
        {
            for (int i = 0; i < arrSpecialty.Count; i++)
            {
                SpecialityPDFFill obj = (SpecialityPDFFill)arrSpecialty[i];
                SqlCommand comm = new SqlCommand("PROC_GET_MISSING_SPECIALITY_REPORT", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
                comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_NAME", obj.SZ_SPECIALITY_NAME);
                comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", obj.SZ_SPECIALITY_CODE);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0 )
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Speicalty"] = ds.Tables[0].Rows[j]["Speicalty"].ToString();
                        dr["Patient"] = ds.Tables[0].Rows[j]["Patient"].ToString();
                        dr["sz_case_no"] = ds.Tables[0].Rows[j]["sz_case_no"].ToString();
                        dr["SZ_CASE_TYPE_NAME"] = ds.Tables[0].Rows[j]["SZ_CASE_TYPE_NAME"].ToString();
                        dr["DT_DATE_OF_ACCIDENT"] = ds.Tables[0].Rows[j]["DT_DATE_OF_ACCIDENT"].ToString();
                        dr["dt_created_date"] = ds.Tables[0].Rows[j]["dt_created_date"].ToString();
                        dr["sz_insurance_name"] = ds.Tables[0].Rows[j]["sz_insurance_name"].ToString();
                        dr["sz_claim_number"] = ds.Tables[0].Rows[j]["sz_claim_number"].ToString();
                        dr["sz_patient_cellno"] = ds.Tables[0].Rows[j]["sz_patient_cellno"].ToString();
                       dr["SZ_PATIENT_PHONE"] = ds.Tables[0].Rows[j]["SZ_PATIENT_PHONE"].ToString();
                        dr["DaysOpen"] = ds.Tables[0].Rows[j]["DaysOpen"].ToString();
                        dt.Rows.Add(dr);

                    }
                }

            }
           
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        return dt;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrSpecialty = new ArrayList();
            DataTable dt = new DataTable();
            for (int i = 0; i < grdSpeciality.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdSpeciality.Columns[0];
                CheckBox chk = (CheckBox)grdSpeciality.FindRowCellTemplateControl(i, c, "chkall1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        SpecialityPDFFill obj = new SpecialityPDFFill();
                        obj.SZ_SPECIALITY_NAME = grdSpeciality.GetRowValues(i, "description").ToString();
                        obj.SZ_SPECIALITY_CODE = grdSpeciality.GetRowValues(i, "code").ToString();
                        arrSpecialty.Add(obj);
                    }
                }
            }

            dt = GetMissingSpecailty(arrSpecialty);
            grdMissingSpeciality.DataSource = dt;
            grdMissingSpeciality.DataBind();
            lblcount.Text = dt.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
    }
    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        BindGrid();
        string sFileName = null;
        string sFile = GetExportPhysicalPath(sUserName, gb.web.utils.DownloadFilesExportTypes.UNPROCESS_BILLS, out sFileName);
        System.IO.Stream stream1 = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdExport.WriteXlsx(stream1);
        stream1.Close();
        ArrayList list = new ArrayList();
        list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(sFileName));
        Session["Download_Files"] = list;
    }
    public static string GetExportPhysicalPath(string sUserName, DownloadFilesExportTypes type, out string sFileName)
    {
        string sRoot = getUploadDocumentPhysicalPath();
        string sFolder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
        sFileName = getFileName(sUserName);
        return sRoot + sFolder + sFileName;
    }
    private static string getUploadDocumentPhysicalPath()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
    }
    private static string getFileName(string sUserName)
    {
        return sUserName + "MissingSpeciality" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }
}