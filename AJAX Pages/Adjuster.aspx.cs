using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Adjuster : Page, IRequiresSessionState
{

    public AJAX_Pages_Adjuster()
    {
    }

    protected void btnClearAdjuster_click(object sender, EventArgs e)
    {
        if (this.Link.Text != "update")
        {
            this.txtAdjusterPopupName.Text = "";
        }
        this.txtAdjusterPopupAddress.Text = "";
        this.txtAdjusterPopupCity.Text = "";
        this.txtAdjusterPopupState.Text = "";
        this.txtAdjusterPopupZip.Text = "";
        this.txtAdjusterPopupPhone.Text = "";
        this.txtAdjusterPopupExtension.Text = "";
        this.txtAdjusterPopupFax.Text = "";
        this.txtAdjusterPopupEmail.Text = "";
    }

    protected void btnSaveAdjuster_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Adjuster billSysAdjuster = new Bill_Sys_Adjuster();
            ArrayList arrayLists = new ArrayList();
            ArrayList arrayLists1 = new ArrayList();
            arrayLists.Add("");
            arrayLists.Add(this.txtAdjusterPopupName.Text);
            arrayLists.Add(this.txtAdjusterPopupAddress.Text);
            arrayLists.Add(this.txtAdjusterPopupCity.Text);
            arrayLists.Add(this.txtAdjusterPopupState.Text);
            arrayLists.Add(this.txtAdjusterPopupZip.Text);
            arrayLists.Add(this.txtAdjusterPopupPhone.Text);
            arrayLists.Add(this.txtAdjusterPopupExtension.Text);
            arrayLists.Add(this.txtAdjusterPopupFax.Text);
            arrayLists.Add(this.txtAdjusterPopupEmail.Text);
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.txtCaseID.Text);
            billSysAdjuster.saveAdjuster(arrayLists);
            string latestID = billSysAdjuster.getLatestID("SP_MST_ADJUSTER", this.txtCompanyID.Text);
            arrayLists1.Add(latestID);
            arrayLists1.Add(this.txtAdjusterPopupName.Text);
            arrayLists1.Add(this.txtAdjusterPopupAddress.Text);
            arrayLists1.Add(this.txtAdjusterPopupCity.Text);
            arrayLists1.Add(this.txtAdjusterPopupState.Text);
            arrayLists1.Add(this.txtAdjusterPopupZip.Text);
            arrayLists1.Add(this.txtAdjusterPopupPhone.Text);
            arrayLists1.Add(this.txtAdjusterPopupExtension.Text);
            arrayLists1.Add(this.txtAdjusterPopupFax.Text);
            arrayLists1.Add(this.txtAdjusterPopupEmail.Text);
            arrayLists1.Add(this.txtCompanyID.Text);
            arrayLists1.Add(this.txtCaseID.Text);
            if (this.ChkAddToCase.Checked)
            {
                billSysAdjuster.updateCaseMaster(arrayLists1);
            }
            this.txtAdjusterPopupName.Text = "";
            this.txtAdjusterPopupAddress.Text = "";
            this.txtAdjusterPopupCity.Text = "";
            this.txtAdjusterPopupState.Text = "";
            this.txtAdjusterPopupZip.Text = "";
            this.txtAdjusterPopupPhone.Text = "";
            this.txtAdjusterPopupExtension.Text = "";
            this.txtAdjusterPopupFax.Text = "";
            this.txtAdjusterPopupEmail.Text = "";
            this.hdacode.Value = "";
            this.hdadjusterCode.Value = "";
            this.hdfadjname.Value = "";
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.PutMessage("Save sucessfully");
            this.usrMessage.Show();
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

    protected void btnupdate_click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string value = this.hdadjusterCode.Value;
            Bill_Sys_Adjuster billSysAdjuster = new Bill_Sys_Adjuster();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.hdadjusterCode.Value);
            arrayLists.Add(this.txtAdjusterPopupName.Text);
            arrayLists.Add(this.txtAdjusterPopupAddress.Text);
            arrayLists.Add(this.txtAdjusterPopupCity.Text);
            arrayLists.Add(this.txtAdjusterPopupState.Text);
            arrayLists.Add(this.txtAdjusterPopupZip.Text);
            arrayLists.Add(this.txtAdjusterPopupPhone.Text);
            arrayLists.Add(this.txtAdjusterPopupExtension.Text);
            arrayLists.Add(this.txtAdjusterPopupFax.Text);
            arrayLists.Add(this.txtAdjusterPopupEmail.Text);
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.txtCaseID.Text);
            billSysAdjuster.updateAdjuster(arrayLists);
            if (this.ChkAddToCase.Checked)
            {
                billSysAdjuster.updateCaseMaster(arrayLists);
            }
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.PutMessage("Update sucessfully");
            this.Session["attoenyset"] = this.hdacode.Value;
            this.usrMessage.Show();
            this.txtAdjusterPopupName.Text = "";
            this.txtAdjusterPopupAddress.Text = "";
            this.txtAdjusterPopupCity.Text = "";
            this.txtAdjusterPopupState.Text = "";
            this.txtAdjusterPopupZip.Text = "";
            this.txtAdjusterPopupPhone.Text = "";
            this.txtAdjusterPopupExtension.Text = "";
            this.txtAdjusterPopupFax.Text = "";
            this.txtAdjusterPopupEmail.Text = "";
            this.hdacode.Value = "";
            this.hdadjusterCode.Value = "";
            this.hdfadjname.Value = "";
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

    public void getAdjusterdata(string sz_Companyid, string sz_adjuster)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = null;
        try
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            SqlCommand sqlCommand = new SqlCommand("SP_GET_ADJUSTER", sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlCommand.Parameters.Add("@SZ_COMPANY_ID", sz_Companyid);
            sqlCommand.Parameters.Add("@SZ_ADJUSTER_ID", sz_adjuster);
            sqlCommand.Parameters.Add("@FLAG", "LIST");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            sqlConnection.Close();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                this.txtAdjusterPopupAddress.Text = dataSet.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
                this.txtAdjusterPopupCity.Text = dataSet.Tables[0].Rows[0]["SZ_CITY"].ToString();
                this.txtAdjusterPopupState.Text = dataSet.Tables[0].Rows[0]["SZ_STATE"].ToString();
                this.txtAdjusterPopupZip.Text = dataSet.Tables[0].Rows[0]["SZ_ZIP"].ToString();
                this.txtAdjusterPopupPhone.Text = dataSet.Tables[0].Rows[0]["SZ_PHONE"].ToString();
                this.txtAdjusterPopupExtension.Text = dataSet.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
                this.txtAdjusterPopupFax.Text = dataSet.Tables[0].Rows[0]["SZ_FAX"].ToString();
                this.txtAdjusterPopupEmail.Text = dataSet.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
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

    protected void getAdjusterData()
    {
        Bill_Sys_Adjuster billSysAdjuster = new Bill_Sys_Adjuster();
        string str = "";
        str = (this.hdadjusterCode.Value != "" ? this.hdadjusterCode.Value : billSysAdjuster.getAdjusterID(this.txtCompanyID.Text, this.txtCaseID.Text));
        this.hdadjusterCode.Value = str;
        Patient_TVBO patientTVBO = new Patient_TVBO();
        DataSet dataSet = new DataSet();
        dataSet = patientTVBO.GetAdjusterInfoForUpdate(str, this.txtCompanyID.Text);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.txtAdjusterPopupName.Text = dataSet.Tables[0].Rows[0]["SZ_ADJUSTER_NAME"].ToString();
            this.txtAdjusterPopupPhone.Text = dataSet.Tables[0].Rows[0]["SZ_PHONE"].ToString();
            this.txtAdjusterPopupExtension.Text = dataSet.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
            this.txtAdjusterPopupFax.Text = dataSet.Tables[0].Rows[0]["SZ_FAX"].ToString();
            this.txtAdjusterPopupEmail.Text = dataSet.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            this.txtAdjusterPopupAddress.Text = dataSet.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
            this.txtAdjusterPopupCity.Text = dataSet.Tables[0].Rows[0]["SZ_CITY"].ToString();
            this.txtAdjusterPopupState.Text = dataSet.Tables[0].Rows[0]["SZ_STATE"].ToString();
            this.txtAdjusterPopupZip.Text = dataSet.Tables[0].Rows[0]["SZ_ZIP"].ToString();
            this.txtAdjusterPopupName.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnupdate.Attributes.Add("onclick", "return GetAdjusterValue();");
        if (this.Link.Text != "update")
        {
            this.btnupdate.Visible = false;
            this.btnSaveAdjuster.Visible = true;
        }
        else
        {
            this.btnSaveAdjuster.Visible = false;
            this.btnupdate.Visible = true;
        }
        if (!base.IsPostBack)
        {
            if (base.Request.QueryString["adjcompany"] != null)
            {
                this.txtCompanyID.Text = base.Request.QueryString["adjcompany"].ToString();
            }
            if (base.Request.QueryString["CaseID"] != null)
            {
                this.txtCaseID.Text = base.Request.QueryString["CaseID"].ToString();
            }
            if (base.Request.QueryString["objAdjusterID"] != null)
            {
                this.hdadjusterCode.Value = base.Request.QueryString["objAdjusterID"].ToString();
                base.Request.QueryString["objAdjusterID"].ToString();
            }
            if (base.Request.QueryString["link"] == null)
            {
                this.btnupdate.Visible = false;
                this.btnSaveAdjuster.Visible = true;
            }
            else
            {
                this.Link.Text = base.Request.QueryString["link"].ToString();
                this.btnSaveAdjuster.Visible = false;
                this.btnupdate.Visible = true;
            }
            string value = this.hdadjusterCode.Value;
            if (this.Link.Text == "update")
            {
                this.getAdjusterData();
            }
        }
    }

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        if (this.hdadjusterCode.Value != "" && this.hdadjusterCode.Value != "0")
        {
            this.hdfadjname.Value = this.txtAdjusterPopupName.Text;
            this.txtAdjusterPopupName.Text = this.hdfadjname.Value.Trim();
            this.hdacode.Value = this.hdadjusterCode.Value;
            this.hdadjusterCode.Value = "";
            this.getAdjusterdata(this.txtCompanyID.Text, this.hdacode.Value);
            return;
        }
        this.btnupdate.Visible = true;
        this.btnSaveAdjuster.Visible = true;
        this.txtAdjusterPopupAddress.Text = "";
        this.txtAdjusterPopupCity.Text = "";
        this.txtAdjusterPopupState.Text = "";
        this.txtAdjusterPopupZip.Text = "";
        this.txtAdjusterPopupPhone.Text = "";
        this.txtAdjusterPopupExtension.Text = "";
        this.txtAdjusterPopupFax.Text = "";
        this.txtAdjusterPopupEmail.Text = "";
        this.hdacode.Value = "";
        this.Session["attoenyset"] = null;
    }
}