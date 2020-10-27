using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentTypeReport : PageBase
{
    string CompanyID;
    string FromDate;
    string Todate;
    string Speciality;
    string provider;
    string BillNo;
    string FromVisitDate;
    string ToVisitdate;
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
            BindGrid();
            grdPaymentReport.ExpandAll();
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlVisitDatevalue.Attributes.Add("onChange", "javascript:SetVisitDate();");
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //extddlSpeciality.Flag_ID = txtCompanyID.Text;
                //extddlOffice.Flag_ID = txtCompanyID.Text;
                extddlSpeciality.Flag_ID = txtCompanyID.Text;
                extddlOffice.Flag_ID = txtCompanyID.Text;
                txtFromBillDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToBillDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");

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
    private void BindGrid()
    {
        DataSet ds = new DataSet();
        PaymentReportBo obj = new PaymentReportBo();
        ds = obj.PaymentReportgrid(CompanyID, FromDate, Todate, FromVisitDate, ToVisitdate, Speciality, provider, BillNo);
        grdPaymentReport.DataSource = ds;
        grdPaymentReport.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        FromVisitDate = txtFromVisitDate.Text;
        ToVisitdate = txtToVisitDate.Text;
        Speciality = extddlSpeciality.Text;
        provider = extddlOffice.Text;
        BillNo = txtBillNo.Text;
        DataSet ds1 = new DataSet();
        PaymentReportBo obj = new PaymentReportBo();
        ds1 =obj.PaymentReportgrid(CompanyID, FromDate, Todate, FromVisitDate, ToVisitdate,Speciality,provider, BillNo);
        grdPaymentReport.DataSource = ds1;
        grdPaymentReport.DataBind();
    }
    
    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        //BindGrid();
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
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
        return sUserName + "PaymentReportByType" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }

    

}