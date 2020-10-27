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
public partial class Bill_Sys_CM_PhysicalStatus : PageBase
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
            cv.MakeReadOnlyPage("Bill_Sys_CM_PhysicalStatus.aspx");
        }
        #endregion
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_MedicalHistory.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //
        
        
////////////////////////////////////////////////////////////////////

      
        if (RDO_APPREHENSION_SIGN.SelectedValue.ToString().Equals(""))
        {
            txtRDO_APPREHENSION_SIGN.Text = "-1";
        }
        else
        {
            txtRDO_APPREHENSION_SIGN.Text = RDO_APPREHENSION_SIGN.SelectedValue;

        }
            if (RDO_PAIN_UPON_PALPATION.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_UPON_PALPATION.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_UPON_PALPATION.Text = RDO_PAIN_UPON_PALPATION.SelectedValue;

            }
            //RDO_TENDERNESS
            //txtRDO_TENDERNESS
            if (RDO_TENDERNESS.SelectedValue.ToString().Equals(""))
            {
                txtRDO_TENDERNESS.Text = "-1";
            }
            else
            {
                txtRDO_TENDERNESS.Text = RDO_TENDERNESS.SelectedValue;

            }
            //RDO_BACK_LUMBAR_LORDOSIS
            //txtxRDO_BACK_LUMBAR_LORDOSIS
            if (RDO_BACK_LUMBAR_LORDOSIS.SelectedValue.ToString().Equals(""))
            {
                txtxRDO_BACK_LUMBAR_LORDOSIS.Text = "-1";
            }
            else
            {
                txtxRDO_BACK_LUMBAR_LORDOSIS.Text = RDO_BACK_LUMBAR_LORDOSIS.SelectedValue;

            }
            //RDO_PAIN_IN_JOINT_LEFT
            //txtRDO_PAIN_IN_JOINT_LEFT
            if (RDO_PAIN_IN_JOINT_LEFT.SelectedValue.ToString().Equals(""))
            {

                txtRDO_PAIN_IN_JOINT_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_IN_JOINT_LEFT.Text = RDO_PAIN_IN_JOINT_LEFT.SelectedValue;

            }
            //RDO_PAIN_ACROSS_SHOULDERS_LEFT
            //txtRDO_PAIN_ACROSS_SHOULDERS_LEFT
            if (RDO_PAIN_ACROSS_SHOULDERS_LEFT.SelectedValue.ToString().Equals(""))
            {

                txtRDO_PAIN_ACROSS_SHOULDERS_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_ACROSS_SHOULDERS_LEFT.Text = RDO_PAIN_ACROSS_SHOULDERS_LEFT.SelectedValue;

            }
            //RDO_LIMITATION_ON_MOVEMENT_LEFT
            //txtCHK_LIMITATION_ON_MOVEMENT_LEFT
            if (RDO_LIMITATION_ON_MOVEMENT_LEFT.SelectedValue.ToString().Equals(""))
            {

                txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text = RDO_LIMITATION_ON_MOVEMENT_LEFT.SelectedValue;

            }

            //RDO_CREPITUS
            //txtRDO_CREPITUS
            if (RDO_CREPITUS.SelectedValue.ToString().Equals(""))
            {

                txtRDO_CREPITUS.Text = "-1";
            }
            else
            {
                txtRDO_CREPITUS.Text = RDO_CREPITUS.SelectedValue;

            }
            //RDO_DROP_ARM_TEST
            //txtRDO_DROP_ARM_TEST
            if (RDO_DROP_ARM_TEST.SelectedValue.ToString().Equals(""))
            {

                txtRDO_DROP_ARM_TEST.Text = "-1";
            }
            else
            {
                txtRDO_DROP_ARM_TEST.Text = RDO_DROP_ARM_TEST.SelectedValue;

            }
            //RDO_PAINFUL_IMPINGEMENT
            //txtRDO_PAINFUL_IMPINGEMENT
            if (RDO_PAINFUL_IMPINGEMENT.SelectedValue.ToString().Equals(""))
            {

                txtRDO_PAINFUL_IMPINGEMENT.Text = "-1";
            }
            else
            {
                txtRDO_PAINFUL_IMPINGEMENT.Text = RDO_PAINFUL_IMPINGEMENT.SelectedValue;

            }


          


            if (RDO_LIMITATION_ON_MOVEMENT_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text = RDO_LIMITATION_ON_MOVEMENT_LEFT.SelectedValue;

            }

            if (RDO_APPREHENSION_SIGN.SelectedValue.ToString().Equals(""))
            {
                txtRDO_APPREHENSION_SIGN.Text = "-1";   
            }
            else
            {
                txtRDO_APPREHENSION_SIGN.Text = RDO_APPREHENSION_SIGN.SelectedValue;

            }



            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "Bill_Sys_CM_PhysicalStatus.xml";
            _saveOperation.SaveMethod();

            Response.Redirect("Bill_Sys_CM_PhysicalStatus1.aspx", false);
            // Response.Redirect("Bill_Sys_IM_MedicalHistory.aspx", false);
        
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
            _editOperation.Xml_File = "Bill_Sys_CM_PhysicalStatus.xml";
            _editOperation.LoadData();

                  
           //RDO_PAIN_UPON_PALPATION
            //txtRDO_PAIN_UPON_PALPATION
            if (txtRDO_PAIN_UPON_PALPATION.Text.ToString().Equals("-1"))
            {
              
            }
            else
            {
                RDO_PAIN_UPON_PALPATION.SelectedValue= txtRDO_PAIN_UPON_PALPATION.Text;

            }
            //RDO_TENDERNESS
            //txtRDO_TENDERNESS
            if (txtRDO_TENDERNESS.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                RDO_TENDERNESS.SelectedValue = txtRDO_TENDERNESS.Text;

            }
            //RDO_BACK_LUMBAR_LORDOSIS
            //txtxRDO_BACK_LUMBAR_LORDOSIS
            if (txtxRDO_BACK_LUMBAR_LORDOSIS.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                RDO_BACK_LUMBAR_LORDOSIS.SelectedValue = txtxRDO_BACK_LUMBAR_LORDOSIS.Text;

            }
            //RDO_PAIN_IN_JOINT_LEFT
            //txtRDO_PAIN_IN_JOINT_LEFT
            if (txtRDO_PAIN_IN_JOINT_LEFT.Text.ToString().Equals("-1"))
            {

              
            }
            else
            {
              RDO_PAIN_IN_JOINT_LEFT.SelectedValue=   txtRDO_PAIN_IN_JOINT_LEFT.Text;

            }
            //RDO_PAIN_ACROSS_SHOULDERS_LEFT
            //txtRDO_PAIN_ACROSS_SHOULDERS_LEFT
            if (txtRDO_PAIN_ACROSS_SHOULDERS_LEFT.Text.ToString().Equals("-1"))
            {

                 
            }
            else
            {
                RDO_PAIN_ACROSS_SHOULDERS_LEFT.SelectedValue = txtRDO_PAIN_ACROSS_SHOULDERS_LEFT.Text;

            }
            //RDO_LIMITATION_ON_MOVEMENT_LEFT
            //txtCHK_LIMITATION_ON_MOVEMENT_LEFT
            if (txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text.ToString().Equals("-1"))
            {

                
            }
            else
            {
                RDO_LIMITATION_ON_MOVEMENT_LEFT.SelectedValue = txtRDO_LIMITATION_ON_MOVEMENT_LEFT.Text;

            }

            //RDO_CREPITUS
            //
            if (txtRDO_CREPITUS.Text.ToString().Equals("-1"))
            {

                
            }
            else
            {
                RDO_CREPITUS.SelectedValue = txtRDO_CREPITUS.Text;

            }
            //RDO_DROP_ARM_TEST
            //txtRDO_DROP_ARM_TEST
            if (txtRDO_DROP_ARM_TEST.ToString().Equals("-1"))
            {

                 //= "-1";
            }
            else
            {
                RDO_DROP_ARM_TEST.SelectedValue= txtRDO_DROP_ARM_TEST.Text;

            }
            //RDO_PAINFUL_IMPINGEMENT
            //txtRDO_PAINFUL_IMPINGEMENT
            if (txtRDO_PAINFUL_IMPINGEMENT.Text.ToString().Equals("-1"))
            {
               
             
            }
            else
            {
                RDO_PAINFUL_IMPINGEMENT.SelectedValue=txtRDO_PAINFUL_IMPINGEMENT.Text;

            }


            if (txtRDO_APPREHENSION_SIGN.Text.ToString().Equals("-1"))
            {


            }
            else
            {
                RDO_APPREHENSION_SIGN.SelectedValue = txtRDO_APPREHENSION_SIGN.Text;

            }





           // Response.Redirect("Bill_Sys_CM_PhysicalStatus1.aspx", false);

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
