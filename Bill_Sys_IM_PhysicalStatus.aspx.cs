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

public partial class Bill_Sys_IM_PhysicalStatus : PageBase
{
    private SaveOperation _saveOperation;
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
            cv.MakeReadOnlyPage("Bill_Sys_IM_PhysicalStatus.aspx");
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
        _saveOperation = new SaveOperation();
        try

              
        {
           

            if (rdlApprehensionSign.SelectedValue.ToString().Equals(""))
            {
                txtrdlApprehensionSign.Text = "-1";
            }
            else
            {
                txtrdlApprehensionSign.Text = rdlApprehensionSign.SelectedValue;

            }
            

        if (rdlBackLumbarLordosis.SelectedValue.ToString().Equals(""))
            {
                txtrdlBackLumbarLordosis.Text = "-1";
            }
            else
            {
                txtrdlBackLumbarLordosis.Text = rdlBackLumbarLordosis.SelectedValue;

            }
            
            

            if (rdlCrepitus.SelectedValue.ToString().Equals(""))
            {
                txtrdlCrepitus.Text = "-1";
            }
            else
            {
                txtrdlCrepitus.Text = rdlCrepitus.SelectedValue;

            }

            
            if (rdlDropArmTest.SelectedValue.ToString().Equals(""))
            {
                txtrdlDropArmTest.Text = "-1";
            }
            else
            {
                txtrdlDropArmTest.Text = rdlDropArmTest.SelectedValue;

            }




            if (rdlLimitationOnMovementLeft.SelectedValue.ToString().Equals(""))
            {
                txtrdlLimitationOnMovementLeft.Text = "-1";
            }
            else
            {
                txtrdlLimitationOnMovementLeft.Text = rdlLimitationOnMovementLeft.SelectedValue;

            }

            
            if (rdlPAinAcrossShoulders.SelectedValue.ToString().Equals(""))
            {
                txtrdlPAinAcrossShoulders.Text = "-1";
            }
            else
            {
                txtrdlPAinAcrossShoulders.Text = rdlPAinAcrossShoulders.SelectedValue;

            }

            

            if (rdlPainfulImpingement.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainfulImpingement.Text = "-1";
            }
            else
            {
                txtrdlPainfulImpingement.Text = rdlPainfulImpingement.SelectedValue;

            }


            if (rdlPainInJointLeft.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainInJointLeft.Text = "-1";
            }
            else
            {
                txtrdlPainInJointLeft.Text = rdlPainInJointLeft.SelectedValue;

            }
            

            if (rdlPainUponPalpation.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainUponPalpation.Text = "-1";
            }
            else
            {
                txtrdlPainUponPalpation.Text = rdlPainUponPalpation.SelectedValue;

            }
            
            if (rdlTenderness.SelectedValue.ToString().Equals(""))
            {
                txtrdlTenderness.Text = "-1";
            }
            else
            {
                txtrdlTenderness.Text = rdlTenderness.SelectedValue;

            }
            
            





            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "IM_PhysicalStatus.xml";
            _saveOperation.SaveMethod();
            Response.Redirect("Bill_Sys_IM_PhysicalStatus1.aspx", false);
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
            Response.Redirect("Bill_Sys_IM_MedicalHistory.aspx", false);
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
            _editOperation.Xml_File = "IM_PhysicalStatus.xml";
            _editOperation.LoadData();




            if (txtrdlApprehensionSign.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlApprehensionSign.SelectedValue = txtrdlApprehensionSign.Text;

            }


            if (txtrdlBackLumbarLordosis.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlBackLumbarLordosis.SelectedValue = txtrdlBackLumbarLordosis.Text;

            }



            if (txtrdlCrepitus.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlCrepitus.SelectedValue = txtrdlCrepitus.Text;

            }


            if (txtrdlDropArmTest.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlDropArmTest.SelectedValue = txtrdlDropArmTest.Text;

            }




            if (txtrdlLimitationOnMovementLeft.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlLimitationOnMovementLeft.SelectedValue = txtrdlLimitationOnMovementLeft.Text;

            }


            if (txtrdlPAinAcrossShoulders.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPAinAcrossShoulders.SelectedValue = txtrdlPAinAcrossShoulders.Text;

            }



            if (txtrdlPainfulImpingement.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPainfulImpingement.SelectedValue = txtrdlPainfulImpingement.Text;

            }


            if (txtrdlPainInJointLeft.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPainInJointLeft.SelectedValue = txtrdlPainInJointLeft.Text;

            }


            if (txtrdlPainUponPalpation.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPainUponPalpation.SelectedValue = txtrdlPainUponPalpation.Text;

            }

            if (txtrdlTenderness.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlTenderness.SelectedValue = txtrdlTenderness.Text;

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
            _editOperation.Xml_File = "IM_PatientInformetion.xml";
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
