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
using System.IO;
using System.Data.SqlClient;

public partial class Bill_Sys_IM_DiagnosticImpresstion : PageBase
{
    ArrayList AL = new ArrayList();
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EID"] != null)
            {
               // Session["IMEventID"] = Request.QueryString["EID"].ToString();
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
                LoadDiagnosisCode();
            }
            else
            {
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
                LoadDiagnosisCode();
            }
        }
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        if (_objCheckoutBO.CheckIMData(Convert.ToInt32(txtEventID.Text)))
        {
            btnSave.Text = "Update&Next";
        }
        txtDOE.Text =DateTime.Now.ToString("MM/dd/yyy");

        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_IM_DiagnosticImpresstion.aspx");
        }
        #endregion



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
            ds= _objCheckoutBO.GetCODiagnosis(txtEventID.Text);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            


            if (rdlCause_Of_Injury.SelectedValue.ToString().Equals(""))
            {
                txtrdlCause_Of_Injury.Text = "-1";
            }
            else
            {
                txtrdlCause_Of_Injury.Text = rdlCause_Of_Injury.SelectedValue;

            }
            
            if (rdlComplaint_Consistent.SelectedValue.ToString().Equals(""))
            {
                txtrdlComplaint_Consistent.Text = "-1";
            }
            else
            {
                txtrdlComplaint_Consistent.Text = rdlComplaint_Consistent.SelectedValue;

            }

            
            if (rdlConsistent_Obj_Fidings.SelectedValue.ToString().Equals(""))
            {
                txtrdlConsistent_Obj_Fidings.Text = "-1";
            }
            else
            {
                txtrdlConsistent_Obj_Fidings.Text = rdlConsistent_Obj_Fidings.SelectedValue;

            }


            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;          
            _saveOperation.Xml_File = "IMDiagnosticImpresstion.xml";
            _saveOperation.SaveMethod();
            SaveChekBoxValue();
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            obj.deleteCODigosisCode(txtEventID.Text.ToString());
            obj.SaveCODigosisCode(Convert.ToInt32(txtEventID.Text), AL); 




            Response.Redirect("Bill_Sys_IM_PlanOfCare.aspx", false);
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

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_IM_NeuroLogicalExamination.aspx", false);
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
            _editOperation.Xml_File = "IMDiagnosticImpresstion.xml";
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
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_PatientInformetion.xml";
            _editOperation.LoadData();

            if (txtrdlCause_Of_Injury.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                rdlCause_Of_Injury.SelectedValue= txtrdlCause_Of_Injury.Text;

            }

            if (txtrdlComplaint_Consistent.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                  rdlComplaint_Consistent.SelectedValue=txtrdlComplaint_Consistent.Text;

            }


            if (txtrdlConsistent_Obj_Fidings.Text .ToString().Equals("-1"))
            {
               
            }
            else
            {
                rdlConsistent_Obj_Fidings.SelectedValue=txtrdlConsistent_Obj_Fidings.Text;

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


   

}
