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

public partial class Bill_Sys_CM_PhysicalStatus2 : PageBase
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
            cv.MakeReadOnlyPage("Bill_Sys_CM_PhysicalStatus2.aspx");
        }
        #endregion
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_PhysicalStatus1.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    

       if (rdlFeet_Ankle_Pain.SelectedValue.ToString().Equals(""))
            {
                txtrdlFeet_Ankle_Pain.Text = "-1";
            }
            else
            {
                txtrdlFeet_Ankle_Pain.Text = rdlFeet_Ankle_Pain.SelectedValue;

            }

            if (rdlFeet_Foot_Pain.SelectedValue.ToString().Equals(""))
            {
                txtrdlFeet_Foot_Pain.Text = "-1";
            }
            else
            {
                txtrdlFeet_Foot_Pain.Text = rdlFeet_Foot_Pain.SelectedValue;

            }


            if (rdlFeet_Numbness_Of_Foot.SelectedValue.ToString().Equals(""))
            {
                txtrdlFeet_Numbness_Of_Foot.Text = "-1";
            }
            else
            {
                txtrdlFeet_Numbness_Of_Foot.Text = rdlFeet_Numbness_Of_Foot.SelectedValue;

            }


            if (rdlFeet_Swollen_Ankle.SelectedValue.ToString().Equals(""))
            {
                txtrdlFeet_Swollen_Ankle.Text = "-1";
            }
            else
            {
                txtrdlFeet_Swollen_Ankle.Text = rdlFeet_Swollen_Ankle.SelectedValue;

            }


            if (rdlFeet_Swollen_Foot.SelectedValue.ToString().Equals(""))
            {
                txtrdlFeet_Swollen_Foot.Text = "-1";
            }
            else
            {
                txtrdlFeet_Swollen_Foot.Text = rdlFeet_Swollen_Foot.SelectedValue;

            }


            if (RDO_ANTERIOR_DRAWER_SIGN_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_ANTERIOR_DRAWER_SIGN_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_ANTERIOR_DRAWER_SIGN_LEFT.Text = RDO_ANTERIOR_DRAWER_SIGN_LEFT.SelectedValue;

            }


            if (RDO_ANTERIOR_DRAWER_SIGN_RIGHT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_ANTERIOR_DRAWER_SIGN_RIGHT.Text = "-1";
            }
            else
            {
                txtRDO_ANTERIOR_DRAWER_SIGN_RIGHT.Text = RDO_ANTERIOR_DRAWER_SIGN_RIGHT.SelectedValue;

            }

            if (RDO_KNEE_CREPITUS.SelectedValue.ToString().Equals(""))
            {
                txtRDO_KNEE_CREPITUS.Text = "-1";
            }
            else
            {
                txtRDO_KNEE_CREPITUS.Text = RDO_KNEE_CREPITUS.SelectedValue;

            }

            if (RDO_KNEE_EFFUSION.SelectedValue.ToString().Equals(""))
            {
                txtRDO_KNEE_EFFUSION.Text = "-1";
            }
            else
            {
                txtRDO_KNEE_EFFUSION.Text = RDO_KNEE_EFFUSION.SelectedValue;

            }

            if (RDO_KNEE_JOINT_LINE_PAIN.SelectedValue.ToString().Equals(""))
            {
                txtRDO_KNEE_JOINT_LINE_PAIN.Text = "-1";
            }
            else
            {
                txtRDO_KNEE_JOINT_LINE_PAIN.Text = RDO_KNEE_JOINT_LINE_PAIN.SelectedValue;

            }

            if (RDO_KNEE_POINT_TENDERNESS.SelectedValue.ToString().Equals(""))
            {
                txtRDO_KNEE_POINT_TENDERNESS.Text = "-1";
            }
            else
            {
                txtRDO_KNEE_POINT_TENDERNESS.Text = RDO_KNEE_POINT_TENDERNESS.SelectedValue;

            }

            if (RDO_KNEE_SWELLING.SelectedValue.ToString().Equals(""))
            {
                txtRDO_KNEE_SWELLING.Text = "-1";
            }
            else
            {
                txtRDO_KNEE_SWELLING.Text = RDO_KNEE_SWELLING.SelectedValue;

            }



            if (RDO_MCMURRAYS_TEST_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_MCMURRAYS_TEST_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_MCMURRAYS_TEST_LEFT.Text = RDO_MCMURRAYS_TEST_LEFT.SelectedValue;

            }


            if (RDO_MCMURRAYS_TEST_RIGHT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_MCMURRAYS_TEST_RIGHT.Text = "-1";
            }
            else
            {
                txtRDO_MCMURRAYS_TEST_RIGHT.Text = RDO_MCMURRAYS_TEST_RIGHT.SelectedValue;

            }

            if (RDO_NUMBNESS_IN_ARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_NUMBNESS_IN_ARM.Text = "-1";
            }
            else
            {
                txtRDO_NUMBNESS_IN_ARM.Text = RDO_NUMBNESS_IN_ARM.SelectedValue;

            }


            if (RDO_NUMBNESS_IN_FOREARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_NUMBNESS_IN_FOREARM.Text = "-1";
            }
            else
            {
                txtRDO_NUMBNESS_IN_FOREARM.Text = RDO_NUMBNESS_IN_FOREARM.SelectedValue;

            }


            if (RDO_NUMBNESS_IN_HAND.SelectedValue.ToString().Equals(""))
            {
                txtRDO_NUMBNESS_IN_HAND.Text = "-1";
            }
            else
            {
                txtRDO_NUMBNESS_IN_HAND.Text = RDO_NUMBNESS_IN_HAND.SelectedValue;

            }


            if (RDO_PAIN_AND_NEEDLESS_ARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_AND_NEEDLESS_ARM.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_AND_NEEDLESS_ARM.Text = RDO_PAIN_AND_NEEDLESS_ARM.SelectedValue;

            }


            if (RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_AND_NEEDLESS_FOREARM.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_AND_NEEDLESS_FOREARM.Text = RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedValue;

            }


            if (RDO_PAIN_AND_NEEDLESS_HAND.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_AND_NEEDLESS_HAND.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_AND_NEEDLESS_HAND.Text = RDO_PAIN_AND_NEEDLESS_HAND.SelectedValue;

            }


            if (RDO_PAIN_IN_FOREARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_IN_FOREARM.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_IN_FOREARM.Text = RDO_PAIN_IN_FOREARM.SelectedValue;

            }


            if (RDO_PAIN_IN_HAND.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_IN_HAND.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_IN_HAND.Text = RDO_PAIN_IN_HAND.SelectedValue;

            }


            if (RDO_PAIN_IN_UPPER_ARM.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_IN_UPPER_ARM.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_IN_UPPER_ARM.Text = RDO_PAIN_IN_UPPER_ARM.SelectedValue;

            }


            if (RDO_PAIN_IN_WRIST.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_IN_WRIST.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_IN_WRIST.Text = RDO_PAIN_IN_WRIST.SelectedValue;

            }

            if (RDO_PAIN_N_ELBOW.SelectedValue.ToString().Equals(""))
            {
                txtRDO_PAIN_N_ELBOW.Text = "-1";
            }
            else
            {
                txtRDO_PAIN_N_ELBOW.Text = RDO_PAIN_N_ELBOW.SelectedValue;

            }


            if (RDO_POSTERIOR_DRAWER_SIGN_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_POSTERIOR_DRAWER_SIGN_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_POSTERIOR_DRAWER_SIGN_LEFT.Text = RDO_POSTERIOR_DRAWER_SIGN_LEFT.SelectedValue;

            }


            if (RDO_POSTERIOR_DRAWER_SIGN_RIGHT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_POSTERIOR_DRAWER_SIGN_RIGHT.Text = "-1";
            }
            else
            {
                txtRDO_POSTERIOR_DRAWER_SIGN_RIGHT.Text = RDO_POSTERIOR_DRAWER_SIGN_RIGHT.SelectedValue;

            }


            if (RDO_VALGUS_TEST_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_VALGUS_TEST_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_VALGUS_TEST_LEFT.Text = RDO_VALGUS_TEST_LEFT.SelectedValue;

            }


            if (RDO_VALGUS_TEST_RIGHT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_VALGUS_TEST_RIGHT.Text = "-1";
            }
            else
            {
                txtRDO_VALGUS_TEST_RIGHT.Text = RDO_VALGUS_TEST_RIGHT.SelectedValue;

            }


            if (RDO_VARUS_TEST_LEFT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_VARUS_TEST_LEFT.Text = "-1";
            }
            else
            {
                txtRDO_VARUS_TEST_LEFT.Text = RDO_VARUS_TEST_LEFT.SelectedValue;

            }

            if (RDO_VARUS_TEST_RIGHT.SelectedValue.ToString().Equals(""))
            {
                txtRDO_VARUS_TEST_RIGHT.Text = "-1";
            }
            else
            {
                txtRDO_VARUS_TEST_RIGHT.Text = RDO_VARUS_TEST_RIGHT.SelectedValue;

            }

         _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "CM_INITIAL_EVAL_PhysicalStatus2.xml";
            _saveOperation.SaveMethod();

           Response.Redirect("Bill_Sys_CM_NeuroLogicalExamination.aspx", false);
        
    
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


    public void LoadData()
    {



        EditOperation _editOperation = new EditOperation();
    
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PhysicalStatus2.xml";
            _editOperation.LoadData();

     if ( txtrdlFeet_Ankle_Pain.Text.ToString().Equals("-1"))
        {
          
        }
        else
        {
             rdlFeet_Ankle_Pain.SelectedValue=txtrdlFeet_Ankle_Pain.Text;

        }
        
        if ( txtrdlFeet_Foot_Pain.Text .ToString().Equals("-1"))
        {
     
        }
        else
        {
           rdlFeet_Foot_Pain.SelectedValue=txtrdlFeet_Foot_Pain.Text;

        }


        if ( txtrdlFeet_Numbness_Of_Foot.Text.ToString().Equals("-1"))
        {
          
        }
        else
        {
           rdlFeet_Numbness_Of_Foot.SelectedValue=txtrdlFeet_Numbness_Of_Foot.Text;

        }


        if ( txtrdlFeet_Swollen_Ankle.Text.ToString().Equals("-1"))
        {
          
        }
        else
        {
            rdlFeet_Swollen_Ankle.SelectedValue=  txtrdlFeet_Swollen_Ankle.Text;

        }

        
        if ( txtrdlFeet_Swollen_Foot.Text .ToString().Equals("-1"))
        {
          
        }
        else
        {
            rdlFeet_Swollen_Foot.SelectedValue = txtrdlFeet_Swollen_Foot.Text;

        }
        

        if (txtRDO_ANTERIOR_DRAWER_SIGN_LEFT.Text.ToString().Equals("-1"))
        {
            
        }
        else
        {
           RDO_ANTERIOR_DRAWER_SIGN_LEFT.SelectedValue= txtrdlFeet_Swollen_Foot.Text;

        }


        if ( txtRDO_ANTERIOR_DRAWER_SIGN_RIGHT.Text .ToString().Equals("-1"))
        {
           
        }
        else
        {
           RDO_ANTERIOR_DRAWER_SIGN_RIGHT.SelectedValue= txtRDO_ANTERIOR_DRAWER_SIGN_RIGHT.Text;

        }

        if (txtRDO_KNEE_CREPITUS.Text.ToString().Equals("-1"))
        {
           
        }
        else
        {
          RDO_KNEE_CREPITUS.SelectedValue=txtRDO_KNEE_CREPITUS.Text;

        }

        if (txtRDO_KNEE_EFFUSION.Text .ToString().Equals("-1"))
        {
    
        }
        else
        {
            RDO_KNEE_EFFUSION.SelectedValue= txtRDO_KNEE_EFFUSION.Text;

        }

        if ( txtRDO_KNEE_JOINT_LINE_PAIN.Text.ToString().Equals("-1"))
        {
         
        }
        else
        {
            RDO_KNEE_JOINT_LINE_PAIN.SelectedValue=txtRDO_KNEE_JOINT_LINE_PAIN.Text;

        }

        if (txtRDO_KNEE_POINT_TENDERNESS.Text .ToString().Equals("-1"))
        {
            
        }
        else
        {
            RDO_KNEE_POINT_TENDERNESS.SelectedValue = txtRDO_KNEE_POINT_TENDERNESS.Text;

        }

        if (txtRDO_KNEE_SWELLING.Text.ToString().Equals("-1"))
        {
        
        }
        else
        {
           RDO_KNEE_SWELLING.SelectedValue=   txtRDO_KNEE_SWELLING.Text;

        }

        

        if (txtRDO_MCMURRAYS_TEST_LEFT.Text.ToString().Equals("-1"))
        {
           
        }
        else
        {
             RDO_MCMURRAYS_TEST_LEFT.SelectedValue=txtRDO_MCMURRAYS_TEST_LEFT.Text;

        }


        if ( txtRDO_MCMURRAYS_TEST_RIGHT.Text.ToString().Equals("-1"))
        {
           
        }
        else
        {
           RDO_MCMURRAYS_TEST_RIGHT.SelectedValue=txtRDO_MCMURRAYS_TEST_RIGHT.Text;

        }
       
        if ( txtRDO_NUMBNESS_IN_ARM.Text.ToString().Equals("-1"))
        {
        }
        else
        {
             RDO_NUMBNESS_IN_ARM.SelectedValue= txtRDO_NUMBNESS_IN_ARM.Text;

        }


        if (txtRDO_NUMBNESS_IN_FOREARM.Text .ToString().Equals("-1"))
        {
          
        }
        else
        {
           RDO_NUMBNESS_IN_FOREARM.SelectedValue=  txtRDO_NUMBNESS_IN_FOREARM.Text;

        }


        if (txtRDO_NUMBNESS_IN_HAND.Text.ToString().Equals("-1"))
        {
          
        }
        else
        {
            RDO_NUMBNESS_IN_HAND.SelectedValue= txtRDO_NUMBNESS_IN_HAND.Text;

        }


        if (  txtRDO_PAIN_AND_NEEDLESS_ARM.Text.ToString().Equals("-1"))
        {
         
        }
        else
        {
            RDO_PAIN_AND_NEEDLESS_ARM.SelectedValue= txtRDO_PAIN_AND_NEEDLESS_ARM.Text ;

        }


        if (txtRDO_PAIN_AND_NEEDLESS_FOREARM.Text .ToString().Equals("-1"))
        {
          
        }
        else
        {
            RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedValue=txtRDO_PAIN_AND_NEEDLESS_FOREARM.Text ;

        }

      
        if (txtRDO_PAIN_AND_NEEDLESS_HAND.Text.Equals("-1"))
        {
             
        }
        else
        {
           RDO_PAIN_AND_NEEDLESS_HAND.SelectedValue= txtRDO_PAIN_AND_NEEDLESS_HAND.Text;

        }
        

        if ( txtRDO_PAIN_IN_FOREARM.Text.ToString().Equals("-1"))
        {
            
        }
        else
        {
            RDO_PAIN_IN_FOREARM.SelectedValue= txtRDO_PAIN_IN_FOREARM.Text;

        }

        
        if (txtRDO_PAIN_IN_HAND.Text.Equals("-1"))
        {
           
        }
        else
        {
            RDO_PAIN_IN_HAND.SelectedValue= txtRDO_PAIN_IN_HAND.Text;

        }


        if (txtRDO_PAIN_IN_UPPER_ARM.Text.ToString().Equals("-1"))
        {
            
        }
        else
        {
             RDO_PAIN_IN_UPPER_ARM.SelectedValue= txtRDO_PAIN_IN_UPPER_ARM.Text;

        }


        if (txtRDO_PAIN_IN_WRIST.Text.ToString().Equals("-1"))
        {
            
        }
        else
        {
            RDO_PAIN_IN_WRIST.SelectedValue= txtRDO_PAIN_IN_WRIST.Text ;

        }
        
        if (txtRDO_PAIN_N_ELBOW.Text.ToString().Equals("-1"))
        {
         
        }
        else
        {
             RDO_PAIN_N_ELBOW.SelectedValue=txtRDO_PAIN_N_ELBOW.Text;

        }


        if (txtRDO_POSTERIOR_DRAWER_SIGN_LEFT.Text.ToString().Equals("-1"))
        {
         
        }
        else
        {
         RDO_POSTERIOR_DRAWER_SIGN_LEFT.SelectedValue=txtRDO_POSTERIOR_DRAWER_SIGN_LEFT.Text ;

        }


        if ( txtRDO_POSTERIOR_DRAWER_SIGN_RIGHT.Text.ToString().Equals("-1"))
        {
           
        }
        else
        {
          RDO_POSTERIOR_DRAWER_SIGN_RIGHT.SelectedValue=   txtRDO_POSTERIOR_DRAWER_SIGN_RIGHT.Text;

        }

        
        if (  txtRDO_VALGUS_TEST_LEFT.Text.ToString().Equals("-1"))
        {
          
        }
        else
        {
            RDO_VALGUS_TEST_LEFT.SelectedValue= txtRDO_VALGUS_TEST_LEFT.Text;

        }


        if (txtRDO_VALGUS_TEST_RIGHT.Text .ToString().Equals("-1"))
        {
          
        }
        else
        {
             RDO_VALGUS_TEST_RIGHT.SelectedValue=txtRDO_VALGUS_TEST_RIGHT.Text ;

        }

        
        if (txtRDO_VARUS_TEST_LEFT.Text .ToString().Equals("-1"))
        {
          
        }
        else
        {
           RDO_VARUS_TEST_LEFT.SelectedValue=  txtRDO_VARUS_TEST_LEFT.Text;

        }
        
        if (txtRDO_VARUS_TEST_RIGHT.Equals("-1"))
        {
           
        }
        else
        {
          RDO_VARUS_TEST_RIGHT.SelectedValue= txtRDO_VARUS_TEST_RIGHT.Text;

        }


  
         

        
 }
    












}


    
        
        
     