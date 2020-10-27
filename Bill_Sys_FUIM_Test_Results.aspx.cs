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
using Componend;

public partial class Bill_Sys_FUIM_Test_Results : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    ArrayList al = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["IM_FW_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        TXT_EVENT_ID.Text = Session["IM_FW_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
            LoadDiagnosisCode();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_Test_Results.aspx");
        }
        #endregion
    }

    public void SaveDignosisValues()
    {
        if (chk301.Checked)
        {
            al.Add("301");
        }
        if (chk302.Checked)
        {
            al.Add("302");
        }
        if (chk303.Checked)
        {
            al.Add("303");
        }
        if (chk304.Checked)
        {
            al.Add("304");
        }
        if (chk305.Checked)
        {
            al.Add("305");
        }
        if (chk306.Checked)
        {
            al.Add("306");
        }
        if (chk307.Checked)
        {
            al.Add("307");
        }
        if (chk308.Checked)
        {
            al.Add("308");
        }
        if (chk309.Checked)
        {
            al.Add("309");
        }
        if (chk310.Checked)
        {
            al.Add("310");
        }
        if (chk311.Checked)
        {
            al.Add("311");
        }
        if (chk312.Checked)
        {
            al.Add("312");
        }
        if (chk313.Checked)
        {
            al.Add("313");
        }
        if (chk314.Checked)
        {
            al.Add("314");
        }
        if (chk315.Checked)
        {
            al.Add("315");
        }
        if (chk316.Checked)
        {
            al.Add("316");
        }
        if (chk317.Checked)
        {
            al.Add("317");
        }
        if (chk318.Checked)
        {
            al.Add("318");
        }
        if (chk319.Checked)
        {
            al.Add("319");
        }
        if (chk320.Checked)
        {
            al.Add("320");
        }
        if (chk321.Checked)
        {
            al.Add("321");
        }
        if (chk322.Checked)
        {
            al.Add("322");
        }
        if (chk323.Checked)
        {
            al.Add("323");
        }
        if (chk324.Checked)
        {
            al.Add("324");
        }
        if (chk325.Checked)
        {
            al.Add("325");
        }
        if (chk326.Checked)
        {
            al.Add("326");
        }
        if (chk327.Checked)
        {
            al.Add("327");
        }
        if (chk328.Checked)
        {
            al.Add("328");
        }
        if (chk329.Checked)
        {
            al.Add("329");
        }
        if (chk330.Checked)
        {
            al.Add("330");
        }
        if (chk331.Checked)
        {
            al.Add("331");
        }
        if (chk332.Checked)
        {
            al.Add("332");
        }
        if (chk333.Checked)
        {
            al.Add("333");
        }

    }
    public void LoadDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        try
        {
            DataSet ds = new DataSet();
            ds = _objCheckoutBO.GetCODiagnosis(TXT_EVENT_ID.Text);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["SZ_TEXT"].ToString() != "")
                {
                    String chkTempName = "chk" + ds.Tables[0].Rows[i]["SZ_TEXT"].ToString();
                    CheckBox chkTemp = (CheckBox)tblDiagCode.FindControl(chkTempName);
                    chkTemp.Checked = true;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Test_Results.xml";
            _editOperation.LoadData();
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = TXT_EVENT_ID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
            _editOperation.LoadData();



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        TXT_DOS.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void BTN_PREVIOUS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bill_Sys_FUIM_Examination_Section.aspx");
    }
    protected void BTN_SAVE_NEXT_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Test_Results.xml";  // Pass xml file to SaveOperation
                _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.
            }

            SaveDignosisValues();
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            obj.deleteCODigosisCode(TXT_EVENT_ID.Text.ToString());
            obj.SaveCODigosisCode(Convert.ToInt32(TXT_EVENT_ID.Text), al); 
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        Response.Redirect("~/Bill_Sys_FUIM_Plan.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
