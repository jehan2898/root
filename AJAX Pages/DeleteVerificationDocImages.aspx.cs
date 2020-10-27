using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DevExpress.Web;

public partial class AJAX_Pages_DeleteVerificationDocImages : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            if (!IsPostBack)
            {
                this.btnDelete.Attributes.Add("onclick", "return CheckDelete();");

                hdncaseid.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                BindGrid();
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
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet dsDelImgDoc = new DataSet();
            dsDelImgDoc = this.GetDelVerificationDocInfo(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, hdncaseid.Value.ToString(), "");
            grdDelDocImg.DataSource = dsDelImgDoc.Tables[0];
            grdDelDocImg.DataBind();
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

    public DataSet GetDelVerificationDocInfo(string sz_company_id, string sz_case_id, string sz_bill_no)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        try
        {

            connection.Open();
            SqlCommand selectCommand = new SqlCommand("sp_get_DelDocVerificationImgeInfo", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_company_id);
            selectCommand.Parameters.AddWithValue("@SZ_CASE_ID", sz_case_id);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_no);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string companyid = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            //this.btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            for (int i = 0; i < this.grdDelDocImg.VisibleRowCount; i++)
            {

                GridViewDataColumn col = (GridViewDataColumn)grdDelDocImg.Columns[5];
                CheckBox box = (CheckBox)this.grdDelDocImg.FindRowCellTemplateControl(i, col, "chkDel");

                if (box.Checked == true)
                {


                    //To Delete to documents images from database //

                    string billno = grdDelDocImg.GetRowValues(i, "SZ_BILL_NUMBER").ToString();
                    string Filename = grdDelDocImg.GetRowValues(i, "Filename").ToString();
                    Int32 Imageid = Convert.ToInt32(grdDelDocImg.GetRowValues(i, "ImageID").ToString());
                    string msg = "Verification File " + Filename + " deleted by user " + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "on " + DateTime.Now.ToString("MM/dd/yyyy") + " For Bill Number " + billno;
                    this.DeleteImageDocument(companyid, billno, Imageid, hdncaseid.Value, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, msg);


                    // To Move documents //

                    string filename = "";
                    string Physicalfilepath = "";
                    string destpath = "";
                    string deletefile = "";

                    filename = grdDelDocImg.GetRowValues(i, "Filename").ToString();
                    Physicalfilepath = grdDelDocImg.GetRowValues(i, "Physical_File_Path").ToString();

                    //pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath ;
                    if (Physicalfilepath == "")
                    {
                        string pth = ConfigurationManager.AppSettings["BASEPATH"] + Physicalfilepath;
                        if (Directory.Exists(pth))
                        { }
                        else
                            Directory.CreateDirectory(pth);
                    }
                    else
                    {
                        destpath = ConfigurationManager.AppSettings["BASEPATH"] + Physicalfilepath;

                        deletefile = destpath + ".deleted";

                        // if delete file is exist
                        if (File.Exists(deletefile))
                        {
                            File.Move(destpath, destpath + DateTime.Now.ToString("MMddyyyyss") + ".deleted");
                        }

                        // if file is exist 
                        if (File.Exists(destpath))
                        {
                            File.Move(destpath, destpath + ".deleted");
                        }
                        else
                        {
                            //this.usrMessage.PutMessage("File Not Found..!");
                            //this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                            //this.usrMessage.Show();
                        }

                    }
                    #region Activity_Log
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();

                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "VER_IMAGE_DELETED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "for  Bill Id : " + billno + " Image Id  : " + Imageid;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)HttpContext.Current.Session["USER_OBJECT"]).SZ_USER_ID);
                    this._DAO_NOTES_EO.SZ_CASE_ID = new Bill_Sys_BillingCompanyDetails_BO().GetCaseID(billno, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    #endregion

                    this.usrMessage.PutMessage("Delete Records Sucessfully");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
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

    public void DeleteImageDocument(string CompanyId, string BillNo, Int32 Imgeid)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        SqlCommand comm1;

        connection.Open();

        try
        {
            if (connection != null)
            {

                comm1 = new SqlCommand("sp_delete_VerificationDocumentImges ", connection);
                comm1.CommandType = CommandType.StoredProcedure;

                comm1.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
                comm1.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNo);
                comm1.Parameters.AddWithValue("@I_IMAGE_ID", Imgeid);

                comm1.ExecuteNonQuery();

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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void DeleteImageDocument(string CompanyId, string BillNo, Int32 Imgeid, string CaseId, string UserId, string msg)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        SqlCommand comm1;
        SqlCommand comm;
        connection.Open();

        try
        {
            if (connection != null)
            {

                comm1 = new SqlCommand("sp_delete_VerificationDocumentImges ", connection);
                comm1.CommandType = CommandType.StoredProcedure;

                comm1.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
                comm1.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNo);
                comm1.Parameters.AddWithValue("@I_IMAGE_ID", Imgeid);

                comm1.ExecuteNonQuery();

                comm = new SqlCommand("SP_TXN_NOTES", connection);
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
                comm.Parameters.AddWithValue("@SZ_CASE_ID", CaseId);
                comm.Parameters.AddWithValue("@SZ_USER_ID", UserId);
                comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
                comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", msg);
                comm.Parameters.AddWithValue("@IS_DENIED", "");
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
                comm.Parameters.AddWithValue("@FLAG", "ADD");
                comm.ExecuteNonQuery();

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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}