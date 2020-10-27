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
 

public partial class Bill_Sys_CO_Chiro_Past_Medical_History : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["EID"] != null)
        {
            Session["CO_CHIRO_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }

        TXT_EVENT_ID.Text = Session["CO_CHIRO_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
        }
        TXT_DOE.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CO_Chiro_Past_Medical_History.aspx");
        }
        #endregion
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CO_Chiro.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.Text = RDO_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.SelectedValue.ToString();
        TXT_HOME_OR_GYM.Text = RDO_HOME_OR_GYM.SelectedValue.ToString();
        TXT_HAND_DOMINATION.Text = RDO_HAND_DOMINATION.SelectedValue.ToString();
        TXT_CONSTANT_INTERMITTENT.Text = RDO_CONSTANT_INTERMITTENT.SelectedValue.ToString();
        TXT_CERVICAL_WITH_WITHOUT_RADIATION.Text = RDO_CERVICAL_WITH_WITHOUT_RADIATION.SelectedValue.ToString();
        TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1.Text = RDO_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT.SelectedValue.ToString();
        TXT_THORACIC_WITH_WITHOUT_RADIATION.Text = RDO_THORACIC_WITH_WITHOUT_RADIATION.SelectedValue.ToString();
        TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text = RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedValue.ToString();
        TXT_LUMBAR_WITH_WITHOUT_RADIATION.Text = RDO_LUMBAR_WITH_WITHOUT_RADIATION.SelectedValue.ToString();
        TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1.Text = RDO_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT.SelectedValue.ToString();
        TXT_SHOULDER_PAIN.Text = RDO_SHOULDER_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_ARM_PAIN.Text = RDO_RIGHT_LEFT_ARM_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_ELBOW_PAIN.Text = RDO_RIGHT_LEFT_ELBOW_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_FOREARM_PAIN.Text = RDO_RIGHT_LEFT_FOREARM_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_HAND_PAIN.Text = RDO_RIGHT_LEFT_HAND_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_DIGIT_PAIN.Text = RDO_RIGHT_LEFT_DIGIT_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text = RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedValue.ToString();
        TXT_WITH_WITHOUT_BREATHING.Text = RDO_WITH_WITHOUT_BREATHING.SelectedValue.ToString();
        TXT_RIGHT_LEFT_HIP_JOINT_PAIN.Text = RDO_RIGHT_LEFT_HIP_JOINT_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_GLUTEAL_PAIN.Text = RDO_RIGHT_LEFT_GLUTEAL_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_THIGH_PAIN.Text = RDO_RIGHT_LEFT_THIGH_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_KNEE_PAIN.Text = RDO_RIGHT_LEFT_KNEE_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_LEG_PAIN.Text = RDO_RIGHT_LEFT_LEG_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_ANKLE_PAIN.Text = RDO_RIGHT_LEFT_ANKLE_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_FOOT_PAIN.Text = RDO_RIGHT_LEFT_FOOT_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_TOE_PAIN.Text = RDO_RIGHT_LEFT_TOE_PAIN.SelectedValue.ToString();
        TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text = RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedValue.ToString();
        TXT_NUMBNESS_TINGLING_PARENTHESIA1.Text = RDO_NUMBNESS_TINGLING_PARENTHESIA.SelectedValue.ToString();
        TXT_WELL_NOURISHED_AND_MAINTAINED.Text = RDO_WELL_NOURISHED_AND_MAINTAINED.SelectedValue.ToString();
        TXT_VISUAL_LIMP_IN_RIGHT_LEFT.Text = RDO_VISUAL_LIMP_IN_RIGHT_LEFT.SelectedValue.ToString();
        TXT_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.Text = RDO_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.SelectedValue.ToString();
        TXT_UNABLE_TO_WALK_ON_TOES_HEALS.Text = RDO_UNABLE_TO_WALK_ON_TOES_HEALS.SelectedValue.ToString();


        if(RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGCURRENTLY.Checked)
        {
            TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING.Text = "0";
        }
        else if (RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGQUIT.Checked)
        {
            TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING.Text = "1";
        }


      _saveOperation = new SaveOperation();

        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_CO_Chiro_Past_Medical_History.xml";  // Pass xml file to SaveOperation
                _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.
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
        

        Response.Redirect("Bill_Sys_CO_Chiro_Spinal_Range_of_Motion.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            _editOperation.Primary_Value = Session["CO_CHIRO_EVENT_ID"].ToString(); 
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_CO_Chiro_Past_Medical_History.xml";
            _editOperation.LoadData();


            if (TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING.Text.Trim() != "-1")
            {
                if (TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING.Text.Trim() == "0")
                {
                    RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGCURRENTLY.Checked = true;
                }
                else if (TXT_CURRENTLY_SMOKING_OR_QUIT_SMOKING.Text.Trim() == "1")
                {
                    RDO_CURRENTLY_SMOKING_OR_QUIT_SMOKINGQUIT.Checked = true;
                }
            }

     if (TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.Text.Trim() != "-1")
        {
            if (TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.Text.Trim() == "0")
            {
                RDO_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.SelectedIndex = 0;
            }
            else if (TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.Text.Trim() == "1")
            {
                RDO_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.SelectedIndex = 1;
            }
            else if (TXT_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.Text.Trim() == "2")
            {
                RDO_CURRENTLY_WAS_WAS_NOT_PERFORMING_EXERCISE.SelectedIndex = 2;
            }
        }
        if (TXT_HOME_OR_GYM.Text.Trim() != "-1")
        {
            if (TXT_HOME_OR_GYM.Text.Trim() == "0")
            {
                RDO_HOME_OR_GYM.SelectedIndex = 0;
            }
            else if (TXT_HOME_OR_GYM.Text.Trim() == "1")
            {
                RDO_HOME_OR_GYM.SelectedIndex = 1;
            }
        }
        if (TXT_HAND_DOMINATION.Text.Trim() != "-1")
        {
            if (TXT_HAND_DOMINATION.Text.Trim() == "0")
            {
                RDO_HAND_DOMINATION.SelectedIndex = 0;
            }
            else if (TXT_HAND_DOMINATION.Text.Trim() == "1")
            {
                RDO_HAND_DOMINATION.SelectedIndex = 1;
            }
        }
        if (TXT_CONSTANT_INTERMITTENT.Text.Trim() != "-1")
        {
            if (TXT_CONSTANT_INTERMITTENT.Text.Trim() == "0")
            {
                RDO_CONSTANT_INTERMITTENT.SelectedIndex = 0;
            }
            else if (TXT_CONSTANT_INTERMITTENT.Text.Trim() == "1")
            {
                RDO_CONSTANT_INTERMITTENT.SelectedIndex = 1;
            }
        }
        if (TXT_CERVICAL_WITH_WITHOUT_RADIATION.Text.Trim() != "-1")
        {
            if (TXT_CERVICAL_WITH_WITHOUT_RADIATION.Text.Trim() == "0")
            {
                RDO_CERVICAL_WITH_WITHOUT_RADIATION.SelectedIndex = 0;
            }
            else if (TXT_CERVICAL_WITH_WITHOUT_RADIATION.Text.Trim() == "1")
            {
                RDO_CERVICAL_WITH_WITHOUT_RADIATION.SelectedIndex = 1;
            }
            else if (TXT_CERVICAL_WITH_WITHOUT_RADIATION.Text.Trim() == "2")
            {
                RDO_CERVICAL_WITH_WITHOUT_RADIATION.SelectedIndex = 2;
            }
        }
        if (TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() != "-1")
        {
            if (TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "0")
            {
                RDO_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 0;
            }
            else if (TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "1")
            {
                RDO_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 1;
            }
            else if (TXT_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "2")
            {
                RDO_CERVICAL_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 2;
            }
        }
        if (TXT_THORACIC_WITH_WITHOUT_RADIATION.Text.Trim() != "-1")
        {
            if (TXT_THORACIC_WITH_WITHOUT_RADIATION.Text.Trim() == "0")
            {
                RDO_THORACIC_WITH_WITHOUT_RADIATION.SelectedIndex = 0;
            }
            else if (TXT_THORACIC_WITH_WITHOUT_RADIATION.Text.Trim() == "1")
            {
                RDO_THORACIC_WITH_WITHOUT_RADIATION.SelectedIndex = 1;
            }
            else if (TXT_THORACIC_WITH_WITHOUT_RADIATION.Text.Trim() == "2")
            {
                RDO_THORACIC_WITH_WITHOUT_RADIATION.SelectedIndex = 2;
            }
        }
        if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() != "-1")
        {
            if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "0")
            {
                RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 0;
            }
            else if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "1")
            {
                RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 1;
            }
            else if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "2")
            {
                RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 2;
            }
        }
        if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() != "-1")
        {
            if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "0")
            {
                RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 0;
            }
            else if (TXT_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "1")
            {
                RDO_THORACIC_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 1;
            }
        }
        if (TXT_LUMBAR_WITH_WITHOUT_RADIATION.Text.Trim() != "-1")
        {
            if (TXT_LUMBAR_WITH_WITHOUT_RADIATION.Text.Trim() == "0")
            {
                RDO_LUMBAR_WITH_WITHOUT_RADIATION.SelectedIndex = 0;
            }
            else if (TXT_LUMBAR_WITH_WITHOUT_RADIATION.Text.Trim() == "1")
            {
                RDO_LUMBAR_WITH_WITHOUT_RADIATION.SelectedIndex = 1;
            }
            else if (TXT_LUMBAR_WITH_WITHOUT_RADIATION.Text.Trim() == "2")
            {
                RDO_LUMBAR_WITH_WITHOUT_RADIATION.SelectedIndex = 2;
            }
        }





        if (TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() != "-1")
        {
            if (TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "0")
            {
                RDO_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 0;
            }
            else if (TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "1")
            {
                RDO_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 1;
            }
            else if (TXT_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT1.Text.Trim() == "2")
            {
                RDO_LUMBAR_RADIATION_BILATERAL_LEFT_RIGHT.SelectedIndex = 2;
            }
        }
        if (TXT_SHOULDER_PAIN.Text.Trim() != "-1")
        {
            if (TXT_SHOULDER_PAIN.Text.Trim() == "0")
            {
                RDO_SHOULDER_PAIN.SelectedIndex = 0;
            }
            else if (TXT_SHOULDER_PAIN.Text.Trim() == "1")
            {
                RDO_SHOULDER_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_ARM_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_ARM_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_ARM_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_ARM_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_ARM_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_ELBOW_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_ELBOW_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_ELBOW_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_ELBOW_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_ELBOW_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_FOREARM_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_FOREARM_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_FOREARM_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_FOREARM_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_FOREARM_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_HAND_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_HAND_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_HAND_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_HAND_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_HAND_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_DIGIT_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_DIGIT_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_DIGIT_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_DIGIT_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_DIGIT_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedIndex = 1;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() == "2")
            {
                RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedIndex = 2;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() == "3")
            {
                RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedIndex = 3;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.Text.Trim() == "4")
            {
                RDO_RIGHT_LEFT_ACCURATE_DIGIT_PAIN.SelectedIndex = 4;
            }
        }
        if (TXT_WITH_WITHOUT_BREATHING.Text.Trim() != "-1")
        {
            if (TXT_WITH_WITHOUT_BREATHING.Text.Trim() == "0")
            {
                RDO_WITH_WITHOUT_BREATHING.SelectedIndex = 0;
            }
            else if (TXT_WITH_WITHOUT_BREATHING.Text.Trim() == "1")
            {
                RDO_WITH_WITHOUT_BREATHING.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_HIP_JOINT_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_HIP_JOINT_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_HIP_JOINT_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_HIP_JOINT_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_HIP_JOINT_PAIN.SelectedIndex = 1;
            }
        }




        if (TXT_RIGHT_LEFT_GLUTEAL_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_GLUTEAL_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_GLUTEAL_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_GLUTEAL_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_GLUTEAL_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_THIGH_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_THIGH_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_THIGH_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_THIGH_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_THIGH_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_KNEE_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_KNEE_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_KNEE_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_KNEE_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_KNEE_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_LEG_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_LEG_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_LEG_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_LEG_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_LEG_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_ANKLE_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_ANKLE_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_ANKLE_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_ANKLE_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_ANKLE_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_FOOT_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_FOOT_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_FOOT_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_FOOT_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_FOOT_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_TOE_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_TOE_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_TOE_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_TOE_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_TOE_PAIN.SelectedIndex = 1;
            }
        }
        if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() != "-1")
        {
            if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() == "0")
            {
                RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedIndex = 0;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() == "1")
            {
                RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedIndex = 1;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() == "2")
            {
                RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedIndex = 2;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() == "3")
            {
                RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedIndex = 3;
            }
            else if (TXT_RIGHT_LEFT_ACCURATE_TOE_PAIN.Text.Trim() == "4")
            {
                RDO_RIGHT_LEFT_ACCURATE_TOE_PAIN.SelectedIndex = 4;
            }
        }
        if (TXT_NUMBNESS_TINGLING_PARENTHESIA1.Text.Trim() != "-1")
        {
            if (TXT_NUMBNESS_TINGLING_PARENTHESIA1.Text.Trim() == "0")
            {
                RDO_NUMBNESS_TINGLING_PARENTHESIA.SelectedIndex = 0;
            }
            else if (TXT_NUMBNESS_TINGLING_PARENTHESIA1.Text.Trim() == "1")
            {
                RDO_NUMBNESS_TINGLING_PARENTHESIA.SelectedIndex = 1;
            }
            else if (TXT_NUMBNESS_TINGLING_PARENTHESIA1.Text.Trim() == "2")
            {
                RDO_NUMBNESS_TINGLING_PARENTHESIA.SelectedIndex = 2;
            }
        }
        if (TXT_WELL_NOURISHED_AND_MAINTAINED.Text.Trim() != "-1")
        {
            if (TXT_WELL_NOURISHED_AND_MAINTAINED.Text.Trim() == "0")
            {
                RDO_WELL_NOURISHED_AND_MAINTAINED.SelectedIndex = 0;
            }
            else if (TXT_WELL_NOURISHED_AND_MAINTAINED.Text.Trim() == "1")
            {
                RDO_WELL_NOURISHED_AND_MAINTAINED.SelectedIndex = 1;
            }
            else if (TXT_WELL_NOURISHED_AND_MAINTAINED.Text.Trim() == "2")
            {
                RDO_WELL_NOURISHED_AND_MAINTAINED.SelectedIndex = 2;
            }
        }


        if (TXT_VISUAL_LIMP_IN_RIGHT_LEFT.Text.Trim() != "-1")
        {
            if (TXT_VISUAL_LIMP_IN_RIGHT_LEFT.Text.Trim() == "0")
            {
                RDO_VISUAL_LIMP_IN_RIGHT_LEFT.SelectedIndex = 0;
            }
            else if (TXT_VISUAL_LIMP_IN_RIGHT_LEFT.Text.Trim() == "1")
            {
                RDO_VISUAL_LIMP_IN_RIGHT_LEFT.SelectedIndex = 1;
            }
        }
        if (TXT_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.Text.Trim() != "-1")
        {
            if (TXT_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.Text.Trim() == "0")
            {
                RDO_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.SelectedIndex = 0;
            }
            else if (TXT_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.Text.Trim() == "1")
            {
                RDO_GAIT_ABNORMALITY_PRESENTS_ANTALGIC_ATAXIC.SelectedIndex = 1;
            }
        }
        if (TXT_UNABLE_TO_WALK_ON_TOES_HEALS.Text.Trim() != "-1")
        {
            if (TXT_UNABLE_TO_WALK_ON_TOES_HEALS.Text.Trim() == "0")
            {
                RDO_UNABLE_TO_WALK_ON_TOES_HEALS.SelectedIndex = 0;
            }
            else if (TXT_UNABLE_TO_WALK_ON_TOES_HEALS.Text.Trim() == "1")
            {
                RDO_UNABLE_TO_WALK_ON_TOES_HEALS.SelectedIndex = 1;
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
            _editOperation.Primary_Value = Session["CO_CHIRO_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
            _editOperation.LoadData();
            if (TXT_SEX.Text.Trim() == "Male")
            {
                RDO_SEX.SelectedIndex = 0;
            }
            else
            {
                RDO_SEX.SelectedIndex = 1;
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
