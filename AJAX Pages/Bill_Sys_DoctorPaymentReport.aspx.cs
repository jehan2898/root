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

public partial class Bill_Sys_DoctorPaymentReport : PageBase
{
    string CompanyID;
    string FromDate;
    string Todate;
    string PaymentToDate;
    string paymentFromDate;
    string VisitToDate;
    string VisitFromDate;
    string CaseNo;
    string FinalDoctorID;
    string Insurance;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try 
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            CompanyID = txtCompanyID.Text;
            btnSearch.Attributes.Add("onClick", "return mapSelectedClick()");
            btnSearch.Attributes.Add("onClick", "return mapInsSelectedClick()");
            ddlBillDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlPaymentDatevalue.Attributes.Add("onChange", "javascript:SetPaymentDate();");
            ddlVisitDatevalue.Attributes.Add("onChange", "javascript:SetVisitDate();");
            BindGrid();
            if (!IsPostBack)
            {
                BindDoctorName();
                BindInsurance();
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtFromBillDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToBillDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");

                txtFromPaymentDate.Attributes.Add("onblur", "javascript:return FromPaymentDateValidation(this);");
                txtToPaymentDate.Attributes.Add("onblur", "javascript:return ToPaymentDateValidation(this);");

                txtFromVisitDate.Attributes.Add("onblur", "javascript:return FromVisitDateValidation(this);");
                txtToVisitDate.Attributes.Add("onblur", "javascript:return ToVisitDateValidation(this);");
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        paymentFromDate = txtFromPaymentDate.Text;
        PaymentToDate = txtToPaymentDate.Text;
        VisitFromDate = txtFromVisitDate.Text;
        VisitToDate = txtToVisitDate.Text;
        CaseNo = txtCaseNo.Text;
        FinalDoctorID = "";
        for (int i = 0; i < grdDoctorName.VisibleRowCount; i++)
        {
            GridViewDataColumn c = (GridViewDataColumn)grdDoctorName.Columns[0];
            CheckBox chk = (CheckBox)grdDoctorName.FindRowCellTemplateControl(i, c, "chkall1");
            if (chk != null)
            {
                if (chk.Checked)
                {
                    string DoctorID = grdDoctorName.GetRowValues(i, "CODE").ToString();

                    FinalDoctorID += ",'" + DoctorID+"'";
                }
            }
        }
        if (FinalDoctorID != "")
        {
            FinalDoctorID = FinalDoctorID.Remove(0, 1);
        }
        Insurance = "";
        for (int j = 0; j < grdInsurance.VisibleRowCount; j++)
        {
            GridViewDataColumn ck = (GridViewDataColumn)grdInsurance.Columns[0];
            CheckBox chk1 = (CheckBox)grdInsurance.FindRowCellTemplateControl(j, ck, "chkall2");
            if (chk1 != null)
            {
                if (chk1.Checked)
                {
                    string InsuranceID = grdInsurance.GetRowValues(j, "CODE").ToString();

                    Insurance += ",'" + InsuranceID + "'";
                }
            }
        }
        if (Insurance != "")
        {
            Insurance = Insurance.Remove(0, 1);
        }
        DataSet ds1 = new DataSet();
        ds1 = SearchForDoctorPaymentgrid(CompanyID, FromDate, Todate, paymentFromDate, PaymentToDate, VisitFromDate, VisitToDate, FinalDoctorID, Insurance, CaseNo);
        grdPaymentReport.DataSource = ds1;
        grdPaymentReport.DataBind();
    }

    private void BindDoctorName()
    {
        DataSet ds = new DataSet();
        ds = getDoctorName(txtCompanyID.Text);
        grdDoctorName.DataSource = ds;
        grdDoctorName.DataBind();
    }
    private void BindInsurance()
    {
        DataSet ds1 = new DataSet();
        ds1 = getInsurance(txtCompanyID.Text);
        grdInsurance.DataSource = ds1;
        grdInsurance.DataBind();
    }

    public DataSet getInsurance(string CompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds3 = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_INSURANCE_COMPANY", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@ID", CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "INSURANCE_LIST");
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds3);
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
        return ds3;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet getDoctorName(string CompanyID)
    {
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
            SqlCommand comm = new SqlCommand("SP_MST_DOCTOR", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@id", CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
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
    private void BindGrid()
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        paymentFromDate = txtFromPaymentDate.Text;
        PaymentToDate = txtToPaymentDate.Text;
        VisitFromDate = txtFromVisitDate.Text;
        VisitToDate = txtToVisitDate.Text;
        CaseNo = txtCaseNo.Text;
        FinalDoctorID = "";
        for (int i = 0; i < grdDoctorName.VisibleRowCount; i++)
        {
            GridViewDataColumn c = (GridViewDataColumn)grdDoctorName.Columns[0];
            CheckBox chk = (CheckBox)grdDoctorName.FindRowCellTemplateControl(i, c, "chkall1");
            if (chk != null)
            {
                if (chk.Checked)
                {
                    string DoctorID = grdDoctorName.GetRowValues(i, "CODE").ToString();

                    FinalDoctorID += ",'" + DoctorID + "'";
                }
            }
        }
        if (FinalDoctorID != "")
        {
            FinalDoctorID = FinalDoctorID.Remove(0, 1);
        }
        Insurance = "";
        for (int j = 0; j < grdInsurance.VisibleRowCount; j++)
        {
            GridViewDataColumn ck = (GridViewDataColumn)grdInsurance.Columns[0];
            CheckBox chk1 = (CheckBox)grdInsurance.FindRowCellTemplateControl(j, ck, "chkall2");
            if (chk1 != null)
            {
                if (chk1.Checked)
                {
                    string InsuranceID = grdInsurance.GetRowValues(j, "CODE").ToString();

                    Insurance += ",'" + InsuranceID + "'";
                }
            }
        }
        if (Insurance != "")
        {
            Insurance = Insurance.Remove(0, 1);
        }
        DataSet ds = new DataSet();
        ds = SearchForDoctorPaymentgrid(CompanyID, FromDate, Todate, paymentFromDate, PaymentToDate, VisitFromDate, VisitToDate, FinalDoctorID,Insurance,CaseNo);
        grdPaymentReport.DataSource = ds;
        grdPaymentReport.DataBind();

        // grdPaymentReport.Columns[0].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
        //// grdPaymentReport.Columns[0].Format.FormatString = "c2";
    }

    public DataSet SearchForDoctorPaymentgrid(string CompanyID, string FromDate, string Todate, string paymentFromDate, string PaymentToDate, string VisitFromDate, string VisitToDate, string FinalDoctorID, string Insurance, string CaseNo)
    {
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
            SqlCommand comm = new SqlCommand("SP_GET_DOCTOR_PAYMENT", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.Parameters.AddWithValue("@DT_FORM_BILL_DATE", FromDate);
            comm.Parameters.AddWithValue("@DT_TO_BILL_DATE", Todate);
            comm.Parameters.AddWithValue("@DT_FORM_PAYMENT_DATE", paymentFromDate);
            comm.Parameters.AddWithValue("@DT_TO_PAYMENT_DATE", PaymentToDate);
            comm.Parameters.AddWithValue("@DT_FORM_VISIT_DATE", VisitFromDate);
            comm.Parameters.AddWithValue("@DT_TO_VISIT_DATE", VisitToDate);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", FinalDoctorID);
            comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", Insurance);
            comm.Parameters.AddWithValue("@SZ_CASE_NO", CaseNo);
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
        return sUserName + "DoctorPaymentReport" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }

}