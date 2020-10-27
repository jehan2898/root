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

public partial class AssociatePaymentReport : PageBase
{
    string CompanyID;
    string FromDate;
    string Todate;
    string ProcCode;
    string ProcDesc;
    string BillNo;
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
            BindGrid();
            grdAssoPaymentReport.ExpandAll();
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //extddlSpeciality.Flag_ID = txtCompanyID.Text;
                //extddlOffice.Flag_ID = txtCompanyID.Text;
                txtFromBillDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToBillDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
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
        ds = AssociatePaymentReportgrid(CompanyID, FromDate, Todate, ProcCode, ProcDesc, BillNo);
        grdAssoPaymentReport.DataSource = ds;
        grdAssoPaymentReport.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FromDate = txtFromBillDate.Text;
        Todate = txtToBillDate.Text;
        ProcCode = txtProcCode.Text;
        ProcDesc = txtProcCodeDesc.Text;        
        string str = txtBillNo.Text;
        string[] strArray = str.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if(strArray.Length>0){
            for (int i = 0; i < strArray.Length; i++)
			{
                if(i==0)
                {
                    BillNo = BillNo + "'" + strArray[i] + "'";
                }
                else
                {
                    BillNo = BillNo + ",'" + strArray[i] + "'";
                }
			 
			}
            
        }
        DataSet ds1 = new DataSet();
        ds1 = AssociatePaymentReportgrid(CompanyID, FromDate, Todate, ProcCode, ProcDesc, BillNo);
        grdAssoPaymentReport.DataSource = ds1;
        grdAssoPaymentReport.DataBind();
    }
    public DataSet AssociatePaymentReportgrid(string CompanyID, string FromDate, string Todate, string ProcCode, string ProcDesc, string BillNo)
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_Associate_Payment_Report", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyID);
            comm.Parameters.AddWithValue("@FROMDATE", FromDate);
            comm.Parameters.AddWithValue("@TODATE", Todate);
            comm.Parameters.AddWithValue("@sz_proc_code", ProcCode);
            comm.Parameters.AddWithValue("@sz_proc_code_desc", ProcDesc);
            comm.Parameters.AddWithValue("@sz_bill_number", BillNo);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch
        {

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
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
        return sUserName + "AssociatePaymentReport" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }

    

}