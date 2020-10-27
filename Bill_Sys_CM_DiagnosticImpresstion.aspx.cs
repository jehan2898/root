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


public partial class Bill_Sys_CM_DiagnosticImpresstion : PageBase
{
    ArrayList AL = new ArrayList();
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {
           
            LoadData();
            LoadPatientData();
            LoadDiagnosisCode();
        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_DiagnosticImpresstion.aspx");
        }
        #endregion
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_NeuroLogicalExamination.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       

        if (RDO_CAUSE_OF_INJURY.SelectedValue.Equals(""))
        {
            txtRDO_CAUSE_OF_INJURY.Text = "-1";
        }
        else
        {
            txtRDO_CAUSE_OF_INJURY.Text = RDO_CAUSE_OF_INJURY.SelectedValue;
        }


        if (RDO_COMPLAINT_CONSISTENT.SelectedValue.Equals(""))
        {
            txtRDO_COMPLAINT_CONSISTENT.Text = "-1";
        }
        else
        {
            txtRDO_COMPLAINT_CONSISTENT.Text = RDO_COMPLAINT_CONSISTENT.SelectedValue;
        }


        if (RDO_CONSISTENT_OBJ_FIDINGS.SelectedValue.Equals(""))
        {
            txtRDO_CONSISTENT_OBJ_FIDINGS.Text = "-1";
        }
        else
        {
            txtRDO_CONSISTENT_OBJ_FIDINGS.Text = RDO_CONSISTENT_OBJ_FIDINGS.SelectedValue;
        }

        _saveOperation = new SaveOperation();
        _saveOperation.WebPage = this.Page;
        _saveOperation.Xml_File = "CM_INITIAL_EVALDiagnosticImpresstion.xml";
        _saveOperation.SaveMethod();

        SaveChekBoxValue();
        Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
        obj.deleteCODigosisCode(txtEventID.Text.ToString());
        obj.SaveCODigosisCode(Convert.ToInt32(txtEventID.Text), AL);
        Response.Redirect("Bill_Sys_CM_PlanOfCare.aspx",false);

    }
    public void SaveChekBoxValue()
    {
        if (chk301.Checked)
        {
            AL.Add("301");
        }
        if (chk302.Checked)
        {
            AL.Add("302");
        }
        if (chk303.Checked)
        {
            AL.Add("303");
        }
        if (chk304.Checked)
        {
            AL.Add("304");
        }
        if (chk305.Checked)
        {
            AL.Add("305");
        }
        if (chk306.Checked)
        {
            AL.Add("306");
        }
        if (chk307.Checked)
        {
            AL.Add("307");
        }
        if (chk308.Checked)
        {
            AL.Add("308");
        }
        if (chk309.Checked)
        {
            AL.Add("309");
        }

        if (chk310.Checked)
        {
            AL.Add("310");
        }
        if (chk311.Checked)
        {
            AL.Add("311");
        }

        if (chk312.Checked)
        {
            AL.Add("312");
        }
        if (chk313.Checked)
        {
            AL.Add("313");
        }
        if (chk314.Checked)
        {
            AL.Add("314");
        }
        if (chk315.Checked)
        {
            AL.Add("315");
        }


        if (chk316.Checked)
        {
            AL.Add("316");
        }
        if (chk317.Checked)
        {
            AL.Add("317");
        }
        if (chk318.Checked)
        {
            AL.Add("318");
        }
        if (chk319.Checked)
        {
            AL.Add("319");
        }
        if (chk320.Checked)
        {
            AL.Add("320");
        }
        if (chk321.Checked)
        {
            AL.Add("321");
        }
        if (chk322.Checked)
        {
            AL.Add("322");
        }
        if (chk323.Checked)
        {
            AL.Add("323");
        }
        if (chk324.Checked)
        {
            AL.Add("324");
        }

        if (chk325.Checked)
        {
            AL.Add("325");
        }
        if (chk326.Checked)
        {
            AL.Add("326");
        }
        if (chk327.Checked)
        {
            AL.Add("327");
        }
        if (chk328.Checked)
        {
            AL.Add("328");
        }
        if (chk329.Checked)
        {
            AL.Add("329");
        }
        if (chk330.Checked)
        {
            AL.Add("330");
        }
        if (chk331.Checked)
        {
            AL.Add("331");
        }
        if (chk332.Checked)
        {
            AL.Add("332");
        }
        if (chk333.Checked)
        {
            AL.Add("333");
        }
    }
    public void LoadData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVALDiagnosticImpresstion.xml";
            _editOperation.LoadData();

            if (txtRDO_CAUSE_OF_INJURY.Text.Equals("-1"))
            {
          
            }
            else
            {
                RDO_CAUSE_OF_INJURY.SelectedValue = txtRDO_CAUSE_OF_INJURY.Text;
            }


            if (txtRDO_COMPLAINT_CONSISTENT.Text.Equals("-1"))
            {
                
            }
            else
            {
                 RDO_COMPLAINT_CONSISTENT.SelectedValue=txtRDO_COMPLAINT_CONSISTENT.Text;
            }


            if (txtRDO_CONSISTENT_OBJ_FIDINGS.Equals("-1"))
            {
               
            }
            else
            {
                RDO_CONSISTENT_OBJ_FIDINGS.SelectedValue= txtRDO_CONSISTENT_OBJ_FIDINGS.Text; 
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
            ds = _objCheckoutBO.GetCODiagnosis("11891");
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
            _editOperation.Primary_Value = "11891";
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PatientInformetion.xml";
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

    
}
