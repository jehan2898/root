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

public partial class Bill_Sys_IM_PhysicalStatus1 : PageBase
{
    SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EID"] != null)
            {
                Session["IMEventID"] = Request.QueryString["EID"].ToString();
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
            }
            else
            {
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
            }
        }
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        if (_objCheckoutBO.CheckIMData(Convert.ToInt32(txtEventID.Text)))
        {
            btnSave.Text = "Update&Next";
        }
        txtDOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_IM_PhysicalStatus1.aspx");
        }
        #endregion
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


            if (rdlCervicalSpineAppear.SelectedValue.ToString().Equals(""))
            {
                txtrdlCervicalSpineAppear.Text = "-1";
            }
            else
            {
                txtrdlCervicalSpineAppear.Text = rdlCervicalSpineAppear.SelectedValue;

            }



            if (rdlHipTenderness.SelectedValue.ToString().Equals(""))
            {
                txtrdlHipTenderness.Text = "-1";
            }
            else
            {
                txtrdlHipTenderness.Text = rdlHipTenderness.SelectedValue;

            }


            if (rdlLegRaisingLeft.SelectedValue.ToString().Equals(""))
            {
                txtrdlLegRaisingLeft.Text = "-1";
            }
            else
            {
                txtrdlLegRaisingLeft.Text = rdlLegRaisingLeft.SelectedValue;

            }



            if (rdlLegRaisingRight.SelectedValue.ToString().Equals(""))
            {
                txtrdlLegRaisingRight.Text = "-1";
            }
            else
            {
                txtrdlLegRaisingRight.Text = rdlLegRaisingRight.SelectedValue;

            }


            if (rdlLowerLumbarPain.SelectedValue.ToString().Equals(""))
            {
                txtrdlLowerLumbarPain.Text = "-1";
            }
            else
            {
                txtrdlLowerLumbarPain.Text = rdlLowerLumbarPain.SelectedValue;

            }





            if (rdlMuscleSpasm.SelectedValue.ToString().Equals(""))
            {
                txtrdlMuscleSpasm.Text = "-1";
            }
            else
            {
                txtrdlMuscleSpasm.Text = rdlMuscleSpasm.SelectedValue;

            }


            if (rdlSacroIllacPain.SelectedValue.ToString().Equals(""))
            {
                txtrdlSacroIllacPain.Text = "-1";
            }
            else
            {
                txtrdlSacroIllacPain.Text = rdlSacroIllacPain.SelectedValue;

            }


            if (rdlUpperLumbarPain.SelectedValue.ToString().Equals(""))
            {
                txtrdlUpperLumbarPain.Text = "-1";
            }
            else
            {
                txtrdlUpperLumbarPain.Text = rdlUpperLumbarPain.SelectedValue;

            }
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "IM_PhysicalStatus1.xml";
            _saveOperation.SaveMethod();
            Response.Redirect("Bill_Sys_IM_PhysicalStatus2.aspx", false);
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
            Response.Redirect("Bill_Sys_IM_PhysicalStatus.aspx", false);
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
            _editOperation.Xml_File = "IM_PhysicalStatus1.xml";
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


            if (txtrdlCervicalSpineAppear.Text .ToString().Equals("-1"))
            {
                
            }
            else
            {
                 rdlCervicalSpineAppear.SelectedValue=txtrdlCervicalSpineAppear.Text;

            }



            if (txtrdlHipTenderness.Text.ToString().Equals(""))
            {
             
            }
            else
            {
               rdlHipTenderness.SelectedValue=  txtrdlHipTenderness.Text;

            }


            if (txtrdlLegRaisingLeft.Text .ToString().Equals(""))
            {
               
            }
            else
            {
                rdlLegRaisingLeft.SelectedValue=txtrdlLegRaisingLeft.Text ;

            }



            if (txtrdlLegRaisingRight.Text.ToString().Equals(""))
            {
               
            }
            else
            {
                 rdlLegRaisingRight.SelectedValue=txtrdlLegRaisingRight.Text;

            }


            if ( txtrdlLowerLumbarPain.Text.ToString().Equals(""))
            {
                
            }
            else
            {
                rdlLowerLumbarPain.SelectedValue= txtrdlLowerLumbarPain.Text;

            }





            if (txtrdlMuscleSpasm.Text .ToString().Equals(""))
            {
                
            }
            else
            {
                rdlMuscleSpasm.SelectedValue = txtrdlMuscleSpasm.Text;

            }


            if (txtrdlSacroIllacPain.Text.ToString().Equals(""))
            {
                 
            }
            else
            {
                 rdlSacroIllacPain.SelectedValue=txtrdlSacroIllacPain.Text;

            }


            if (txtrdlUpperLumbarPain.Text.Equals(""))
            {
                 
            }
            else
            {
                 rdlUpperLumbarPain.SelectedValue=txtrdlUpperLumbarPain.Text;

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
}
