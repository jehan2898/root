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

public partial class Bill_Sys_CM_CurrentComplainNEW : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
       // txtEventID.Text=Session["CM_HI_EventID"].ToString();

        if (!IsPostBack)
        {

            //if (Request.QueryString["EID"] != null)
            //{

            // txtEventID.Text = (string)Session["IMEventID"].ToString();
            //txtEventID.Text = "11";
            LoadData();
            LoadPatientData();
            
           // LoadPatientData();
            // }
           //  else
           //  {
           // txtEventID.Text = (string)Session["IMEventID"].ToString();
           // LoadData();
           // LoadPatientData();
           // }
        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        //Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
        //if (_objCheckoutBO.CheckIMData(Convert.ToInt16(txtEventID.Text)))
        //{
        //    btnSave.Text = "Update&Next";
        //}
        //txtDOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_CurrentComplainNEW.aspx");
        }
        #endregion
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_HistoryOfPresentIillness.aspx", false);
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

           
            /////////
            if (rdlatientStatesDenied.SelectedValue.ToString().Equals(""))
            {
                txtrdlatientStatesDenied.Text = "-1";
            }
            else
            {
                txtrdlatientStatesDenied.Text = rdlatientStatesDenied.SelectedValue;

            }


            if (rdlDays.SelectedValue.ToString().Equals(""))
            {
                txtrdlDays.Text = "-1";
            }
            else
            {
                txtrdlDays.Text = rdlDays.SelectedValue;

            }


            if (rdlDazedDizzyNervous.SelectedValue.ToString().Equals(""))
            {
                txtrdlDazedDizzyNervous.Text = "-1";
            }
            else
            {
                txtrdlDazedDizzyNervous.Text = rdlDazedDizzyNervous.SelectedValue;

            }


            if (rdlInformationMode.SelectedValue.ToString().Equals(""))
            {
                txtrdlInformationMode.Text = "-1";
            }
            else
            {
                txtrdlInformationMode.Text = rdlInformationMode.SelectedValue;

            }


            if (rdlNumbnessTingling.SelectedValue.ToString().Equals(""))
            {
                txtrdlNumbnessTingling.Text = "-1";
            }
            else
            {
                txtrdlNumbnessTingling.Text = rdlNumbnessTingling.SelectedValue;

            }

            if (rdlPainDescription.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainDescription.Text = "-1";
            }
            else
            {
                txtrdlPainDescription.Text = rdlPainDescription.SelectedValue;

            }


            if (rdlPainRadiatingNeck.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainRadiatingNeck.Text = "-1";
            }
            else
            {
                txtrdlPainRadiatingNeck.Text = rdlPainRadiatingNeck.SelectedValue;

            }


            if (rdlPainRadingLowerBack.SelectedValue.ToString().Equals(""))
            {
                txtrdlPainRadingLowerBack.Text = "-1";
            }
            else
            {
                txtrdlPainRadingLowerBack.Text = rdlPainRadingLowerBack.SelectedValue;

            }



            if (rdlPatientAfterAccident.SelectedValue.ToString().Equals(""))
            {
                txtrdlPatientAfterAccident.Text = "-1";
            }
            else
            {
                txtrdlPatientAfterAccident.Text = rdlPatientAfterAccident.SelectedValue;

            }

            if (rdlPatientTreatedHimOrHer.SelectedValue.ToString().Equals(""))
            {
                txtrdlPatientTreatedHimOrHer.Text = "-1";
            }
            else
            {
                txtrdlPatientTreatedHimOrHer.Text = rdlPatientTreatedHimOrHer.SelectedValue;

            }


            if (RDO_NUMBNESS_TINGLING_LOWER_UPPER.SelectedValue.ToString().Equals(""))
            {
                txtRDO_NUMBNESS_TINGLING_LOWER_UPPER.Text = "-1";
            }
            else
            {
                txtRDO_NUMBNESS_TINGLING_LOWER_UPPER.Text = RDO_NUMBNESS_TINGLING_LOWER_UPPER.SelectedValue;

            }
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "Bill_Sys_CM_CurrentComplainNEW.xml";
            _saveOperation.SaveMethod();
            Response.Redirect("Bill_Sys_CM_MedicalHistory.aspx", false);
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
            _editOperation.Xml_File = "Bill_Sys_CM_CurrentComplainNEW.xml";
            _editOperation.LoadData();


            if (txtrdlatientStatesDenied.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlatientStatesDenied.SelectedValue = txtrdlatientStatesDenied.Text;

            }


            if (txtrdlDays.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlDays.SelectedValue = txtrdlDays.Text;

            }


            if (txtrdlDazedDizzyNervous.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlDazedDizzyNervous.SelectedValue = txtrdlDazedDizzyNervous.Text;

            }


            if (txtrdlInformationMode.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlInformationMode.SelectedValue = txtrdlInformationMode.Text;

            }


            if (txtrdlNumbnessTingling.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlNumbnessTingling.SelectedValue = txtrdlNumbnessTingling.Text;

            }

            if (txtrdlPainDescription.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPainDescription.SelectedValue = txtrdlPainDescription.Text;

            }


            if (txtrdlPainRadiatingNeck.Text.ToString().Equals("-1"))
            {

            }
            else
            {

                rdlPainRadiatingNeck.SelectedValue = txtrdlPainRadiatingNeck.Text;

            }


            if (txtrdlPainRadingLowerBack.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPainRadingLowerBack.SelectedValue = txtrdlPainRadingLowerBack.Text;

            }



            if (txtrdlPatientAfterAccident.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPatientAfterAccident.SelectedValue = txtrdlPatientAfterAccident.Text;

            }

            if (txtrdlPatientTreatedHimOrHer.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                rdlPatientTreatedHimOrHer.SelectedValue = txtrdlPatientTreatedHimOrHer.Text;

            }
            if (txtRDO_NUMBNESS_TINGLING_LOWER_UPPER.Text.ToString().Equals("-1"))
            {

            }
            else
            {
                RDO_NUMBNESS_TINGLING_LOWER_UPPER.SelectedValue = txtRDO_NUMBNESS_TINGLING_LOWER_UPPER.Text;

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
