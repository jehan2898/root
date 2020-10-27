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

public partial class Bill_Sys_CM_MedicalHistory : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {
            LoadData();
            LoadPatientData();
        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_MedicalHistory.aspx");
        }
        #endregion

    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {

        Response.Redirect("Bill_Sys_CM_CurrentComplainNEW.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.SelectedValue.Equals(""))
        {
            txtRDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.Text = "-1";
        }
        else
        {
            txtRDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.Text = RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.SelectedValue;
        }

        _saveOperation = new SaveOperation();
        _saveOperation.WebPage = this.Page;
        _saveOperation.Xml_File = "CM_INITIAL_EVAL_MedicalHistory.xml";
        _saveOperation.SaveMethod();
        Response.Redirect("Bill_Sys_CM_PhysicalStatus.aspx", false);
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
            _editOperation.Xml_File = "CM_INITIAL_EVAL_MedicalHistory.xml";
            _editOperation.LoadData();
           


            if (txtRDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.Text.Equals("-1"))
            {
              
            }
            else
            {
                RDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.SelectedValue=txtRDO_MEDICAL_HISTORY_AFFECT_TREATMENT_NO.Text;
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
            _editOperation.Primary_Value = txtEventID.Text;
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
