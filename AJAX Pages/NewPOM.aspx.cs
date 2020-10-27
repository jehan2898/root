using DevExpress.Web;
using gb.web.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewPOM : PageBase
{
    string CompanyID;
    string FromDate;
    string Todate;
    string Speciality;
    string Provider;
    string Bill_FromDate;
    string Bill_toDate;
    string BillDays;
    string POMDays;

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            CompanyID = txtCompanyID.Text;

            grdPOMReport.ExpandAll();
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlBillDate.Attributes.Add("onChange", "javascript:SetBillDate();");
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = txtCompanyID.Text;
                extddlOffice.Flag_ID = txtCompanyID.Text;
                txtFromBillDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToBillDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
                txt_Bill_from_Date.Attributes.Add("onblur", "javascript:return FromDateValidation_Bill(this);");
                txt_Bill_to_Date.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
            }
            BindGrid();
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
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindGrid()
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        Speciality = "";
        if (extddlSpeciality.Selected_Text != "--- Select ---")
        {
            Speciality = extddlSpeciality.Text;
        }
        string Provider = "";
        if (extddlOffice.Selected_Text != "--- Select ---")
        {
            Provider = extddlOffice.Text;
        }
        Bill_FromDate = txt_Bill_from_Date.Text;
        Bill_toDate = txt_Bill_to_Date.Text;
        BillDays = txt_search_POM.Text;
        POMDays = txt_search_Bill.Text;

        DataSet ds = new DataSet();
        ds = SearchForUnprocessBillgrid(CompanyID, FromDate, Todate, Speciality, Provider, Bill_FromDate, Bill_toDate, BillDays, POMDays);
        //ds = UnprocessBillgrid(CompanyID);
        grdPOMReport.DataSource = ds;
        grdPOMReport.DataBind();
    }
    public DataSet SearchForUnprocessBillgrid(string CompanyID, string FromDate, string Todate, string Speciality, string Provider, string Bill_FromDate, string Bill_toDate, string BillDays, string POMDays)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_unprocess_bills", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.Parameters.AddWithValue("@FROMDATE", FromDate);
            comm.Parameters.AddWithValue("@TODATE", Todate);
            comm.Parameters.AddWithValue("@SZ_SPECIALITY_ID", Speciality);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", Provider);
            comm.Parameters.AddWithValue("@BILL_FROMDATE", Bill_FromDate);
            comm.Parameters.AddWithValue("@BILL_TODATE", Bill_toDate);
            comm.Parameters.AddWithValue("@BILL_DAYS", BillDays);
            comm.Parameters.AddWithValue("@POM_DAYS", POMDays);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch(Exception ex)
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
    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        BindGrid();
        string sFileName = null;
        string sFile = GetExportPhysicalPath(sUserName, "", out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdExport.WriteXlsx(stream);
        stream.Close();
        ArrayList list = new ArrayList();
        list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(sFileName));
        Session["Download_Files"] = list;
    }
    public static string GetExportPhysicalPath(string sUserName, string type, out string sFileName)
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
        return sUserName + "UnProcessBills" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        Speciality = "";
        if (extddlSpeciality.Selected_Text != "--- Select ---")
        {
            Speciality = extddlSpeciality.Text;
        }
        string Provider = "";
        if (extddlOffice.Selected_Text != "--- Select ---")
        {
            Provider = extddlOffice.Text;
        }
        Bill_FromDate = txt_Bill_from_Date.Text;
        Bill_toDate = txt_Bill_to_Date.Text;
        BillDays = txt_search_POM.Text;
        POMDays = txt_search_Bill.Text;

        DataSet ds1 = new DataSet();
        ds1 = SearchForUnprocessBillgrid(CompanyID, FromDate, Todate, Speciality, Provider, Bill_FromDate, Bill_toDate, BillDays, POMDays);
        grdPOMReport.DataSource = ds1;
        grdPOMReport.DataBind();
    }

}