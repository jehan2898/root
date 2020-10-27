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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Bill_Sys_CO_Patient_Intake3 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["PATIENT_INTEK_CASE_ID"] = Request.QueryString["EID"].ToString();
        }

        TXT_I_EVENT.Text = Session["PATIENT_INTEK_CASE_ID"].ToString();
        if (!Page.IsPostBack)
        {
            LoadData();
        }

    }

    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EditOperation _objEdit = new EditOperation();
            _objEdit.Primary_Value = TXT_I_EVENT.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "Patient_Intake3.xml";
            _objEdit.LoadData();

            if (txtrdblstTREATED_PREVIOUSLY.Text != "")
            {
                rdblstTREATED_PREVIOUSLY.SelectedValue = txtrdblstTREATED_PREVIOUSLY.Text;
            }

            if (txtrdblstDO_YOU_WORK.Text != "")
            {
                rdblstDO_YOU_WORK.SelectedValue = txtrdblstDO_YOU_WORK.Text;
            }
            if (txtrdblstTAKING_MEDICATION.Text != "")
            {
                rdblstTAKING_MEDICATION.SelectedValue = txtrdblstTAKING_MEDICATION.Text;
            }

            if (txtrdblstALLERGIC_TO_DRUGS.Text != "")
            {
                rdblstALLERGIC_TO_DRUGS.SelectedValue = txtrdblstALLERGIC_TO_DRUGS.Text;
            }

            if (txtrdblstDO_YOU_SMOKE.Text != "")
            {
                rdblstDO_YOU_SMOKE.SelectedValue = txtrdblstDO_YOU_SMOKE.Text;
            }
            if (txtrdblstSUFFER_ALLERGIES.Text != "")
            {
                rdblstSUFFER_ALLERGIES.SelectedValue = txtrdblstSUFFER_ALLERGIES.Text;
            }
            if (txtrdblstHAD_SURGERY.Text != "")
            {
                rdblstHAD_SURGERY.SelectedValue = txtrdblstHAD_SURGERY.Text;
            }
            if (txtrdblstARE_YOU_PREGNANT.Text != "")
            {
                rdblstARE_YOU_PREGNANT.SelectedValue = txtrdblstARE_YOU_PREGNANT.Text;
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
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CO_Patient_Intake2.aspx");
    }
    
    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //txtrdblstTREATED_PREVIOUSLY.Text = rdblstTREATED_PREVIOUSLY.SelectedValue;
            //txtrdblstDO_YOU_WORK.Text = rdblstDO_YOU_WORK.SelectedValue;
            //txtrdblstTAKING_MEDICATION.Text = rdblstTAKING_MEDICATION.SelectedValue;
            //txtrdblstALLERGIC_TO_DRUGS.Text = rdblstALLERGIC_TO_DRUGS.SelectedValue;
            //txtrdblstDO_YOU_SMOKE.Text = rdblstDO_YOU_SMOKE.SelectedValue;
            //txtrdblstSUFFER_ALLERGIES.Text = rdblstSUFFER_ALLERGIES.SelectedValue;
            //txtrdblstHAD_SURGERY.Text = rdblstHAD_SURGERY.SelectedValue;
            //txtrdblstARE_YOU_PREGNANT.Text = rdblstARE_YOU_PREGNANT.SelectedValue;


            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "Patient_Intake3.xml";
            _objsave.SaveMethod();
          //  LoadData();
            
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
        
        Response.Redirect("Bill_Sys_CO_Patient_Intake4.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void rdblstTREATED_PREVIOUSLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstTREATED_PREVIOUSLY.Text = rdblstTREATED_PREVIOUSLY.SelectedValue;
    }
    protected void rdblstDO_YOU_WORK_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstDO_YOU_WORK.Text = rdblstDO_YOU_WORK.SelectedValue;
    }
    protected void rdblstTAKING_MEDICATION_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstTAKING_MEDICATION.Text = rdblstTAKING_MEDICATION.SelectedValue;
    }
    protected void rdblstALLERGIC_TO_DRUGS_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstALLERGIC_TO_DRUGS.Text = rdblstALLERGIC_TO_DRUGS.SelectedValue;
    }
    protected void rdblstDO_YOU_SMOKE_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstDO_YOU_SMOKE.Text = rdblstDO_YOU_SMOKE.SelectedValue;
    }
    protected void rdblstSUFFER_ALLERGIES_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstSUFFER_ALLERGIES.Text = rdblstSUFFER_ALLERGIES.SelectedValue;
    }
    protected void rdblstHAD_SURGERY_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstHAD_SURGERY.Text = rdblstHAD_SURGERY.SelectedValue;
    }
    protected void rdblstARE_YOU_PREGNANT_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstARE_YOU_PREGNANT.Text = rdblstARE_YOU_PREGNANT.SelectedValue;
    }
}
