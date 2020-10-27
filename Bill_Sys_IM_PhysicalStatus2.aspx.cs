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

public partial class Bill_Sys_IM_PhysicalStatus2 : PageBase
{
    private SaveOperation _saveobject;
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
            cv.MakeReadOnlyPage("Bill_Sys_IM_PhysicalStatus2.aspx");
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

        if (rdl_Knee_Effusion.SelectedValue.ToString().Equals(""))
        {
            txtrdl_Knee_Effusion.Text = "-1";
        }
        else
        {
            txtrdl_Knee_Effusion.Text = rdl_Knee_Effusion.SelectedValue;

        }

        
        if (rdlAnterior_Drawer_Sign_Left.SelectedValue.ToString().Equals(""))
        {
            txtrdlAnterior_Drawer_Sign_Left.Text = "-1";
        }
        else
        {
            txtrdlAnterior_Drawer_Sign_Left.Text = rdlAnterior_Drawer_Sign_Left.SelectedValue;

        }
        

        if (rdlAnterior_Drawer_Sign_Right.SelectedValue.ToString().Equals(""))
        {
            txtrdlAnterior_Drawer_Sign_Right.Text = "-1";
        }
        else
        {
            txtrdlAnterior_Drawer_Sign_Right.Text = rdlAnterior_Drawer_Sign_Right.SelectedValue;

        }

        
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
        
        if (rdlKnee_Crepitis.SelectedValue.ToString().Equals(""))
        {
            txtrdlKnee_Crepitis.Text = "-1";
        }
        else
        {
            txtrdlKnee_Crepitis.Text = rdlKnee_Crepitis.SelectedValue;

        }

        
        if (rdlknee_Joint_Line_Pain.SelectedValue.ToString().Equals(""))
        {
            txtrdlknee_Joint_Line_Pain.Text = "-1";
        }
        else
        {
            txtrdlknee_Joint_Line_Pain.Text = rdlknee_Joint_Line_Pain.SelectedValue;

        }
        

        if (rdlKnee_Point_Tenderness.SelectedValue.ToString().Equals(""))
        {
            txtrdlKnee_Point_Tenderness.Text = "-1";
        }
        else
        {
            txtrdlKnee_Point_Tenderness.Text = rdlKnee_Point_Tenderness.SelectedValue;

        }

        
        if (rdlknee_Swlling.SelectedValue.ToString().Equals(""))
        {
            txtrdlknee_Swlling.Text = "-1";
        }
        else
        {
            txtrdlknee_Swlling.Text = rdlknee_Swlling.SelectedValue;

        }

        
        if (rdlMcmurrays_Test_Left.SelectedValue.ToString().Equals(""))
        {
            txtrdlMcmurrays_Test_Left.Text = "-1";
        }
        else
        {
            txtrdlMcmurrays_Test_Left.Text = rdlMcmurrays_Test_Left.SelectedValue;

        }
        

        if (rdlMcmurrays_Test_Right.SelectedValue.ToString().Equals(""))
        {
            txtrdlMcmurrays_Test_Right.Text = "-1";
        }
        else
        {
            txtrdlMcmurrays_Test_Right.Text = rdlMcmurrays_Test_Right.SelectedValue;

        }

        
        if (rdlNumbness_In_Arm.SelectedValue.ToString().Equals(""))
        {
            txtrdlNumbness_In_Arm.Text = "-1";
        }
        else
        {
            txtrdlNumbness_In_Arm.Text = rdlNumbness_In_Arm.SelectedValue;

        }


        if (rdlNumbness_In_Forearm.SelectedValue.ToString().Equals(""))
        {
            txtrdlNumbness_In_Forearm.Text = "-1";
        }
        else
        {
            txtrdlNumbness_In_Forearm.Text = rdlNumbness_In_Forearm.SelectedValue;

        }

        
        if (rdlNumbness_In_Hand.SelectedValue.ToString().Equals(""))
        {
            txtrdlNumbness_In_Hand.Text = "-1";
        }
        else
        {
            txtrdlNumbness_In_Hand.Text = rdlNumbness_In_Hand.SelectedValue;

        }
        

        if (rdlPain_In_Forearm.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_In_Forearm.Text = "-1";
        }
        else
        {
            txtrdlPain_In_Forearm.Text = rdlPain_In_Forearm.SelectedValue;

        }


        if (rdlPain_In_Hand.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_In_Hand.Text = "-1";
        }
        else
        {
            txtrdlPain_In_Hand.Text = rdlPain_In_Hand.SelectedValue;

        }


        
        if (rdlPain_In_Upper_Arm.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_In_Upper_Arm.Text = "-1";
        }
        else
        {
            txtrdlPain_In_Upper_Arm.Text = rdlPain_In_Upper_Arm.SelectedValue;

        }

        
        if (rdlPain_In_Wrist.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_In_Wrist.Text = "-1";
        }
        else
        {
            txtrdlPain_In_Wrist.Text = rdlPain_In_Wrist.SelectedValue;

        }
        
        if (rdlPain_N_Elbow.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_N_Elbow.Text = "-1";
        }
        else
        {
            txtrdlPain_N_Elbow.Text = rdlPain_N_Elbow.SelectedValue;

        }

        
        if (rdlPain_Needless_Arm.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_Needless_Arm.Text = "-1";
        }
        else
        {
            txtrdlPain_Needless_Arm.Text = rdlPain_Needless_Arm.SelectedValue;

        }


        if (rdlPain_Needless_Forearm.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_Needless_Forearm.Text = "-1";
        }
        else
        {
            txtrdlPain_Needless_Forearm.Text = rdlPain_Needless_Forearm.SelectedValue;

        }



        if (rdlPain_Needless_Hand.SelectedValue.ToString().Equals(""))
        {
            txtrdlPain_Needless_Hand.Text = "-1";
        }
        else
        {
            txtrdlPain_Needless_Hand.Text = rdlPain_Needless_Hand.SelectedValue;

        }

        
        if (rdlPosterior_Drawer_Sign_Left.SelectedValue.ToString().Equals(""))
        {
            txtrdlPosterior_Drawer_Sign_Left.Text = "-1";
        }
        else
        {
            txtrdlPosterior_Drawer_Sign_Left.Text = rdlPosterior_Drawer_Sign_Left.SelectedValue;

        }
        

        if (rdlPosterior_Drawer_Sign_Right.SelectedValue.ToString().Equals(""))
        {
            txtrdlPosterior_Drawer_Sign_Right.Text = "-1";
        }
        else
        {
            txtrdlPosterior_Drawer_Sign_Right.Text = rdlPosterior_Drawer_Sign_Right.SelectedValue;

        }
        
        if (rdlValgus_Test_Left.SelectedValue.ToString().Equals(""))
        {
            txtrdlValgus_Test_Left.Text = "-1";
        }
        else
        {
            txtrdlValgus_Test_Left.Text = rdlValgus_Test_Left.SelectedValue;

        }
        

        if (rdlValgus_Test_Right.SelectedValue.ToString().Equals(""))
        {
            txtrdlValgus_Test_Right.Text = "-1";
        }
        else
        {
            txtrdlValgus_Test_Right.Text = rdlValgus_Test_Right.SelectedValue;

        }

        
        if (rdlVarus_Test_Left.SelectedValue.ToString().Equals(""))
        {
            txtrdlVarus_Test_Left.Text = "-1";
        }
        else
        {
            txtrdlVarus_Test_Left.Text = rdlVarus_Test_Left.SelectedValue;

        }
        

        if (rdlVarus_Test_Right.SelectedValue.ToString().Equals(""))
        {
            txtrdlVarus_Test_Right.Text = "-1";
        }
        else
        {
            txtrdlVarus_Test_Right.Text = rdlVarus_Test_Right.SelectedValue;

        }




        try
        {

            _saveobject = new SaveOperation();
            _saveobject.WebPage = this.Page;
            _saveobject.Xml_File = "IM_PhysicalStatus2.xml";
            _saveobject.SaveMethod();
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

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
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
            _editOperation.Xml_File = "IM_PhysicalStatus2.xml";
            _editOperation.LoadData();





            if (txtrdl_Knee_Effusion.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
               rdl_Knee_Effusion.SelectedValue=   txtrdl_Knee_Effusion.Text;

            }


            if (txtrdlAnterior_Drawer_Sign_Left.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                 rdlAnterior_Drawer_Sign_Left.SelectedValue=txtrdlAnterior_Drawer_Sign_Left.Text;

            }


            if ( txtrdlAnterior_Drawer_Sign_Right.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
               rdlAnterior_Drawer_Sign_Right.SelectedValue= txtrdlAnterior_Drawer_Sign_Right.Text ;

            }


            if ( txtrdlFeet_Ankle_Pain.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                 rdlFeet_Ankle_Pain.SelectedValue=txtrdlFeet_Ankle_Pain.Text;

            }

            if (txtrdlFeet_Foot_Pain.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
                 rdlFeet_Foot_Pain.SelectedValue= txtrdlFeet_Foot_Pain.Text;

            }

            if (txtrdlFeet_Numbness_Of_Foot.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                rdlFeet_Numbness_Of_Foot.SelectedValue = txtrdlFeet_Numbness_Of_Foot.Text;

            }


            if ( txtrdlFeet_Swollen_Ankle.Text.ToString().Equals("-1"))
            {
             
            }
            else
            {
                rdlFeet_Swollen_Ankle.SelectedValue = txtrdlFeet_Swollen_Ankle.Text;

            }


            if (txtrdlFeet_Swollen_Foot.Text.ToString().Equals("-1"))
            {
          
            }
            else
            {
                 rdlFeet_Swollen_Foot.SelectedValue=txtrdlFeet_Swollen_Foot.Text;

            }

            if (txtrdlKnee_Crepitis.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                rdlKnee_Crepitis.SelectedValue= txtrdlKnee_Crepitis.Text;

            }


            if (txtrdlknee_Joint_Line_Pain.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                 rdlknee_Joint_Line_Pain.SelectedValue= txtrdlknee_Joint_Line_Pain.Text;

            }


            if (txtrdlKnee_Point_Tenderness.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
              rdlKnee_Point_Tenderness.SelectedValue=  txtrdlKnee_Point_Tenderness.Text ;

            }


            if (txtrdlknee_Swlling.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
               rdlknee_Swlling.SelectedValue =txtrdlknee_Swlling.Text;

            }


            if (txtrdlMcmurrays_Test_Left.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
                rdlMcmurrays_Test_Left.SelectedValue=  txtrdlMcmurrays_Test_Left.Text;

            }


            if ( txtrdlMcmurrays_Test_Right.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
               rdlMcmurrays_Test_Right.SelectedValue= txtrdlMcmurrays_Test_Right.Text;

            }


            if (txtrdlNumbness_In_Arm.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
               rdlNumbness_In_Arm.SelectedValue=txtrdlNumbness_In_Arm.Text ;

            }


            if (txtrdlNumbness_In_Forearm.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                rdlNumbness_In_Forearm.SelectedValue= txtrdlNumbness_In_Forearm.Text;

            }


            if ( txtrdlNumbness_In_Hand.Text.ToString().Equals("-1"))
            {
              
            }
            else
            {
                 rdlNumbness_In_Hand.SelectedValue=txtrdlNumbness_In_Hand.Text;

            }


            if (txtrdlPain_In_Forearm.Text.ToString().Equals("-1"))
            {
              
            }
            else
            {
               rdlPain_In_Forearm.SelectedValue= txtrdlPain_In_Forearm.Text ;

            }


            if ( txtrdlPain_In_Hand.Text .ToString().Equals("-1"))
            {
            
            }
            else
            {
                rdlPain_In_Hand.SelectedValue= txtrdlPain_In_Hand.Text;

            }



            if (txtrdlPain_In_Upper_Arm.Text.ToString().Equals("-1"))
            {
              
            }
            else
            {
                 rdlPain_In_Upper_Arm.SelectedValue=txtrdlPain_In_Upper_Arm.Text;

            }


            if ( txtrdlPain_In_Wrist.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
              rdlPain_In_Wrist.SelectedValue=txtrdlPain_In_Wrist.Text;

            }

            if (txtrdlPain_N_Elbow.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                rdlPain_N_Elbow.SelectedValue= txtrdlPain_N_Elbow.Text;

            }


            if ( txtrdlPain_Needless_Arm.Text.ToString().Equals("-1"))
            {
               
            }
            else
            {
                 rdlPain_Needless_Arm.SelectedValue=txtrdlPain_Needless_Arm.Text;

            }


            if (txtrdlPain_Needless_Forearm.Text.ToString().Equals("-1"))
            {
              
            }
            else
            {
                 rdlPain_Needless_Forearm.SelectedValue=txtrdlPain_Needless_Forearm.Text;

            }



            if ( txtrdlPain_Needless_Hand.Text .ToString().Equals("-1"))
            {
               
            }
            else
            {
                rdlPain_Needless_Hand.SelectedValue = txtrdlPain_Needless_Hand.Text;

            }


            if (txtrdlPosterior_Drawer_Sign_Left.Text.ToString().Equals("-1"))
            {
                 
            }
            else
            {
                rdlPosterior_Drawer_Sign_Left.SelectedValue=txtrdlPosterior_Drawer_Sign_Left.Text ;

            }


            if (txtrdlPosterior_Drawer_Sign_Right.Text.ToString().Equals("-1"))
            {
             
            }
            else
            {
                rdlPosterior_Drawer_Sign_Right.SelectedValue = txtrdlPosterior_Drawer_Sign_Right.Text;

            }

            if (  txtrdlValgus_Test_Left.Text .ToString().Equals("-1"))
            {
            
            }
            else
            {
                rdlValgus_Test_Left.SelectedValue=txtrdlValgus_Test_Left.Text;

            }


            if ( txtrdlValgus_Test_Right.Text.ToString().Equals("-1"))
            {
             
            }
            else
            {
               rdlValgus_Test_Right.SelectedValue=txtrdlValgus_Test_Right.Text;

            }


            if (txtrdlVarus_Test_Left.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
               rdlVarus_Test_Left.SelectedValue=  txtrdlVarus_Test_Left.Text;

            }


            if (txtrdlVarus_Test_Right.Text.ToString().Equals("-1"))
            {
                
            }
            else
            {
                rdlVarus_Test_Right.SelectedValue = txtrdlVarus_Test_Right.Text;

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
