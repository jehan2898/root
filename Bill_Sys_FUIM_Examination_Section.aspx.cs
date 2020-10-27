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

public partial class Bill_Sys_FUIM_Examination_Section : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["IM_FW_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        TXT_EVENT_ID.Text = Session["IM_FW_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
        }
        TXT_DOS.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_Examination_Section.aspx");
        }
        #endregion
    }
    protected void BTN_PREVIOUS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bill_Sys_FUIM_StartExamination.aspx");
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

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Examination_Section.xml";
            _editOperation.LoadData();


            if (TXT_PAIN_AND_NEEDLESS_FOREARM.Text != "-1")
            {
                if (TXT_PAIN_AND_NEEDLESS_FOREARM.Text == "0")
                {
                    RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedIndex = 0;
                }
                else if (TXT_PAIN_AND_NEEDLESS_FOREARM.Text == "1")
                {
                    RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedIndex = 1;
                }
                else if (TXT_PAIN_AND_NEEDLESS_FOREARM.Text == "2")
                {
                    RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_AND_NEEDLESS_ARM.Text != "-1")
            {
                if (TXT_PAIN_AND_NEEDLESS_ARM.Text == "0")
                {
                    RDO_PAIN_AND_NEEDLESS_ARM.SelectedIndex = 0;
                }
                else if (TXT_PAIN_AND_NEEDLESS_ARM.Text == "1")
                {
                    RDO_PAIN_AND_NEEDLESS_ARM.SelectedIndex = 1;
                }
                else if (TXT_PAIN_AND_NEEDLESS_ARM.Text == "2")
                {
                    RDO_PAIN_AND_NEEDLESS_ARM.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_IN_FOREARM.Text != "-1")
            {
                if (TXT_PAIN_IN_FOREARM.Text == "0")
                {
                    RDO_PAIN_IN_FOREARM.SelectedIndex = 0;
                }
                else if (TXT_PAIN_IN_FOREARM.Text == "1")
                {
                    RDO_PAIN_IN_FOREARM.SelectedIndex = 1;
                }
                else if (TXT_PAIN_IN_FOREARM.Text == "2")
                {
                    RDO_PAIN_IN_FOREARM.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_N_ELBOW.Text != "-1")
            {
                if (TXT_PAIN_N_ELBOW.Text == "0")
                {
                    RDO_PAIN_N_ELBOW.SelectedIndex = 0;
                }
                else if (TXT_PAIN_N_ELBOW.Text == "1")
                {
                    RDO_PAIN_N_ELBOW.SelectedIndex = 1;
                }
                else if (TXT_PAIN_N_ELBOW.Text == "2")
                {
                    RDO_PAIN_N_ELBOW.SelectedIndex = 2;
                }
            }
            if (TXT_NUMBNESS_IN_HAND.Text != "-1")
            {
                if (TXT_NUMBNESS_IN_HAND.Text == "0")
                {
                    RDO_NUMBNESS_IN_HAND.SelectedIndex = 0;
                }
                else if (TXT_NUMBNESS_IN_HAND.Text == "1")
                {
                    RDO_NUMBNESS_IN_HAND.SelectedIndex = 1;
                }
                else if (TXT_NUMBNESS_IN_HAND.Text == "2")
                {
                    RDO_NUMBNESS_IN_HAND.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_AND_NEEDLESS_HAND.Text != "-1")
            {
                if (TXT_PAIN_AND_NEEDLESS_HAND.Text == "0")
                {
                    RDO_PAIN_AND_NEEDLESS_HAND.SelectedIndex = 0;
                }
                else if (TXT_PAIN_AND_NEEDLESS_HAND.Text == "1")
                {
                    RDO_PAIN_AND_NEEDLESS_HAND.SelectedIndex = 1;
                }
                else if (TXT_PAIN_AND_NEEDLESS_HAND.Text == "2")
                {
                    RDO_PAIN_AND_NEEDLESS_HAND.SelectedIndex = 2;
                }
            }
            if (TXT_CERVICAL_SPINE_APPEAR.Text != "-1")
            {
                if (TXT_CERVICAL_SPINE_APPEAR.Text == "0")
                {
                    RDO_CERVICAL_SPINE_APPEAR.SelectedIndex = 0;
                }
                else if (TXT_CERVICAL_SPINE_APPEAR.Text == "1")
                {
                    RDO_CERVICAL_SPINE_APPEAR.SelectedIndex = 1;
                }
                else if (TXT_CERVICAL_SPINE_APPEAR.Text == "2")
                {
                    RDO_CERVICAL_SPINE_APPEAR.SelectedIndex = 2;
                }
            }
            if (TXT_FEET_SWOLLEN_FOOT.Text != "-1")
            {
                if (TXT_FEET_SWOLLEN_FOOT.Text == "0")
                {
                    RDO_FEET_SWOLLEN_FOOT.SelectedIndex = 0;
                }
                else if (TXT_FEET_SWOLLEN_FOOT.Text == "1")
                {
                    RDO_FEET_SWOLLEN_FOOT.SelectedIndex = 1;
                }
                else if (TXT_FEET_SWOLLEN_FOOT.Text == "2")
                {
                    RDO_FEET_SWOLLEN_FOOT.SelectedIndex = 2;
                }
            }
            if (TXT_FEET_FOOT_PAIN.Text != "-1")
            {
                if (TXT_FEET_FOOT_PAIN.Text == "0")
                {
                    RDO_FEET_FOOT_PAIN.SelectedIndex = 0;
                }
                else if (TXT_FEET_FOOT_PAIN.Text == "1")
                {
                    RDO_FEET_FOOT_PAIN.SelectedIndex = 1;
                }
                else if (TXT_FEET_FOOT_PAIN.Text == "2")
                {
                    RDO_FEET_FOOT_PAIN.SelectedIndex = 2;
                }
            }
            if (TXT_FEET_ANKLE_PAIN.Text != "-1")
            {
                if (TXT_FEET_ANKLE_PAIN.Text == "0")
                {
                    RDO_FEET_ANKLE_PAIN.SelectedIndex = 0;
                }
                else if (TXT_FEET_ANKLE_PAIN.Text == "1")
                {
                    RDO_FEET_ANKLE_PAIN.SelectedIndex = 1;
                }
                else if (TXT_FEET_ANKLE_PAIN.Text == "2")
                {
                    RDO_FEET_ANKLE_PAIN.SelectedIndex = 2;
                }
            }
            if (TXT_NUMBNESS_IN_FOREARM.Text != "-1")
            {
                if (TXT_NUMBNESS_IN_FOREARM.Text == "0")
                {
                    RDO_NUMBNESS_IN_FOREARM.SelectedIndex = 0;
                }
                else if (TXT_NUMBNESS_IN_FOREARM.Text == "1")
                {
                    RDO_NUMBNESS_IN_FOREARM.SelectedIndex = 1;
                }
                else if (TXT_NUMBNESS_IN_FOREARM.Text == "2")
                {
                    RDO_NUMBNESS_IN_FOREARM.SelectedIndex = 2;
                }
            }
            if (TXT_NUMBNESS_IN_ARM.Text != "-1")
            {
                if (TXT_NUMBNESS_IN_ARM.Text == "0")
                {
                    RDO_NUMBNESS_IN_ARM.SelectedIndex = 0;
                }
                else if (TXT_NUMBNESS_IN_ARM.Text == "1")
                {
                    RDO_NUMBNESS_IN_ARM.SelectedIndex = 1;
                }
                else if (TXT_NUMBNESS_IN_ARM.Text == "2")
                {
                    RDO_NUMBNESS_IN_ARM.SelectedIndex = 2;
                }
            }
            if (TXT_FEET_SWOLLEN_ANKLE.Text != "-1")
            {
                if (TXT_FEET_SWOLLEN_ANKLE.Text == "0")
                {
                    RDO_FEET_SWOLLEN_ANKLE.SelectedIndex = 0;
                }
                else if (TXT_FEET_SWOLLEN_ANKLE.Text == "1")
                {
                    RDO_FEET_SWOLLEN_ANKLE.SelectedIndex = 1;
                }
                else if (TXT_FEET_SWOLLEN_ANKLE.Text == "2")
                {
                    RDO_FEET_SWOLLEN_ANKLE.SelectedIndex = 2;
                }
            }
            if (TXT_FEET_NUMBNESS_OF_FOOT.Text != "-1")
            {
                if (TXT_FEET_NUMBNESS_OF_FOOT.Text == "0")
                {
                    RDO_FEET_NUMBNESS_OF_FOOT.SelectedIndex = 0;
                }
                else if (TXT_FEET_NUMBNESS_OF_FOOT.Text == "1")
                {
                    RDO_FEET_NUMBNESS_OF_FOOT.SelectedIndex = 1;
                }
                else if (TXT_FEET_NUMBNESS_OF_FOOT.Text == "2")
                {
                    RDO_FEET_NUMBNESS_OF_FOOT.SelectedIndex = 2;
                }
            }

            if (TXT_CERVICAL_SPINE_WITH.Text != "-1")
            {
                if (TXT_CERVICAL_SPINE_WITH.Text == "0")
                {
                    RDO_CERVICAL_SPINE_WITH.SelectedIndex = 0;
                }
                else if (TXT_CERVICAL_SPINE_WITH.Text == "1")
                {
                    RDO_CERVICAL_SPINE_WITH.SelectedIndex = 1;
                }
                else if (TXT_CERVICAL_SPINE_WITH.Text == "2")
                {
                    RDO_CERVICAL_SPINE_WITH.SelectedIndex = 2;
                }
            }
            if (TXT_CERVICAL_SPINE_MODERATE.Text != "-1")
            {
                if (TXT_CERVICAL_SPINE_MODERATE.Text == "0")
                {
                    RDO_CERVICAL_SPINE_MODERATE.SelectedIndex = 0;
                }
                else if (TXT_CERVICAL_SPINE_MODERATE.Text == "1")
                {
                    RDO_CERVICAL_SPINE_MODERATE.SelectedIndex = 1;
                }
                else if (TXT_CERVICAL_SPINE_MODERATE.Text == "2")
                {
                    RDO_CERVICAL_SPINE_MODERATE.SelectedIndex = 2;
                }
            }
            if (TXT_CERVICAL_SPINE_TENDERNESS.Text != "-1")
            {
                if (TXT_CERVICAL_SPINE_TENDERNESS.Text == "0")
                {
                    RDO_CERVICAL_SPINE_TENDERNESS.SelectedIndex = 0;
                }
                else if (TXT_CERVICAL_SPINE_TENDERNESS.Text == "1")
                {
                    RDO_CERVICAL_SPINE_TENDERNESS.SelectedIndex = 1;
                }
                else if (TXT_CERVICAL_SPINE_TENDERNESS.Text == "2")
                {
                    RDO_CERVICAL_SPINE_TENDERNESS.SelectedIndex = 2;
                }
            }
            if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN.Text != "-1")
            {
                if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN.Text == "0")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN.SelectedIndex = 0;
                }
                else if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN.Text == "1")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN.SelectedIndex = 1;
                }
                else if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN.Text == "2")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN.SelectedIndex = 2;
                }
            }
            if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.Text != "-1")
            {
                if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.Text == "0")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.SelectedIndex = 0;
                }
                else if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.Text == "1")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.SelectedIndex = 1;
                }
                else if (TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.Text == "2")
                {
                    RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.SelectedIndex = 2;
                }
            }
            if (TXT_KNEE_CREPITUS_POSITIVE.Text != "-1")
            {
                if (TXT_KNEE_CREPITUS_POSITIVE.Text == "0")
                {
                    RDO_KNEE_CREPITUS_POSITIVE.SelectedIndex = 0;
                }
                else if (TXT_KNEE_CREPITUS_POSITIVE.Text == "1")
                {
                    RDO_KNEE_CREPITUS_POSITIVE.SelectedIndex = 1;
                }
                else if (TXT_KNEE_CREPITUS_POSITIVE.Text == "2")
                {
                    RDO_KNEE_CREPITUS_POSITIVE.SelectedIndex = 2;
                }
            }
            if (TXT_KNEE_SWELLING_POSITIVE.Text != "-1")
            {
                if (TXT_KNEE_SWELLING_POSITIVE.Text == "0")
                {
                    RDO_KNEE_SWELLING_POSITIVE.SelectedIndex = 0;
                }
                else if (TXT_KNEE_SWELLING_POSITIVE.Text == "1")
                {
                    RDO_KNEE_SWELLING_POSITIVE.SelectedIndex = 1;
                }
                else if (TXT_KNEE_SWELLING_POSITIVE.Text == "2")
                {
                    RDO_KNEE_SWELLING_POSITIVE.SelectedIndex = 2;
                }
            }
            if (TXT_KNEE_POINT_TENDERNESS_NEGATIVE.Text != "-1")
            {
                if (TXT_KNEE_POINT_TENDERNESS_NEGATIVE.Text == "0")
                {
                    RDO_KNEE_POINT_TENDERNESS_NEGATIVE.SelectedIndex = 0;
                }
                else if (TXT_KNEE_POINT_TENDERNESS_NEGATIVE.Text == "1")
                {
                    RDO_KNEE_POINT_TENDERNESS_NEGATIVE.SelectedIndex = 1;
                }
                else if (TXT_KNEE_POINT_TENDERNESS_NEGATIVE.Text == "2")
                {
                    RDO_KNEE_POINT_TENDERNESS_NEGATIVE.SelectedIndex = 2;
                }
            }
            if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.Text != "-1")
            {
                if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.Text == "0")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.SelectedIndex = 0;
                }
                else if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.Text == "1")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.SelectedIndex = 1;
                }
                else if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.Text == "2")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.SelectedIndex = 2;
                }
            }
            if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.Text != "-1")
            {
                if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.Text == "0")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.SelectedIndex = 0;
                }
                else if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.Text == "1")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.SelectedIndex = 1;
                }
                else if (TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.Text == "2")
                {
                    RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_IN_UPPER_ARM.Text != "-1")
            {
                if (TXT_PAIN_IN_UPPER_ARM.Text == "0")
                {
                    RDO_PAIN_IN_UPPER_ARM.SelectedIndex = 0;
                }
                else if (TXT_PAIN_IN_UPPER_ARM.Text == "1")
                {
                    RDO_PAIN_IN_UPPER_ARM.SelectedIndex = 1;
                }
                else if (TXT_PAIN_IN_UPPER_ARM.Text == "2")
                {
                    RDO_PAIN_IN_UPPER_ARM.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_IN_WRIST.Text != "-1")
            {
                if (TXT_PAIN_IN_WRIST.Text == "0")
                {
                    RDO_PAIN_IN_WRIST.SelectedIndex = 0;
                }
                else if (TXT_PAIN_IN_WRIST.Text == "1")
                {
                    RDO_PAIN_IN_WRIST.SelectedIndex = 1;
                }
                else if (TXT_PAIN_IN_WRIST.Text == "2")
                {
                    RDO_PAIN_IN_WRIST.SelectedIndex = 2;
                }
            }
            if (TXT_PAIN_IN_HAND.Text != "-1")
            {
                if (TXT_PAIN_IN_HAND.Text == "0")
                {
                    RDO_PAIN_IN_HAND.SelectedIndex = 0;
                }
                else if (TXT_PAIN_IN_HAND.Text == "1")
                {
                    RDO_PAIN_IN_HAND.SelectedIndex = 1;
                }
                else if (TXT_PAIN_IN_HAND.Text == "2")
                {
                    RDO_PAIN_IN_HAND.SelectedIndex = 2;
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
            _editOperation.Primary_Value = TXT_EVENT_ID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
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
    protected void BTN_SAVE_NEXT_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        TXT_CERVICAL_SPINE_APPEAR.Text =RDO_CERVICAL_SPINE_APPEAR.SelectedValue.ToString();
      TXT_CERVICAL_SPINE_WITH.Text =RDO_CERVICAL_SPINE_WITH.SelectedValue.ToString();
      TXT_CERVICAL_SPINE_MODERATE.Text =RDO_CERVICAL_SPINE_MODERATE.SelectedValue.ToString();
      TXT_CERVICAL_SPINE_TENDERNESS.Text =RDO_CERVICAL_SPINE_TENDERNESS.SelectedValue.ToString();
      TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN.Text =  RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN.SelectedValue.ToString();
      TXT_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.Text=RDO_FORAMINAL_COMPRESSION_SPURLING_SIGN_POSITIVE.SelectedValue.ToString();
      TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.Text =RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_RIGHT.SelectedValue.ToString();
      TXT_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.Text =RDO_STRAIGHT_LEG_RAISING_TEST_SUPINE_LEFT.SelectedValue.ToString();
      TXT_KNEE_CREPITUS_POSITIVE.Text =  RDO_KNEE_CREPITUS_POSITIVE.SelectedValue.ToString();
      TXT_KNEE_SWELLING_POSITIVE.Text = RDO_KNEE_SWELLING_POSITIVE.SelectedValue.ToString();
      TXT_KNEE_POINT_TENDERNESS_NEGATIVE.Text =RDO_KNEE_POINT_TENDERNESS_NEGATIVE.SelectedValue.ToString();
      TXT_PAIN_IN_UPPER_ARM.Text = RDO_PAIN_IN_UPPER_ARM.SelectedValue.ToString();
      TXT_PAIN_N_ELBOW.Text = RDO_PAIN_N_ELBOW.SelectedValue.ToString();
      TXT_PAIN_IN_FOREARM.Text = RDO_PAIN_IN_FOREARM.SelectedValue.ToString();
      TXT_PAIN_AND_NEEDLESS_FOREARM.Text = RDO_PAIN_AND_NEEDLESS_FOREARM.SelectedValue.ToString();
      TXT_NUMBNESS_IN_ARM.Text = RDO_NUMBNESS_IN_ARM.SelectedValue.ToString();
      TXT_PAIN_IN_WRIST.Text = RDO_PAIN_IN_WRIST.SelectedValue.ToString();
      TXT_PAIN_IN_HAND.Text = RDO_PAIN_IN_HAND.SelectedValue.ToString();
      TXT_PAIN_AND_NEEDLESS_HAND.Text = RDO_PAIN_AND_NEEDLESS_HAND.SelectedValue.ToString();
      TXT_NUMBNESS_IN_HAND.Text = RDO_NUMBNESS_IN_HAND.SelectedValue.ToString();
      TXT_FEET_ANKLE_PAIN.Text = RDO_FEET_ANKLE_PAIN.SelectedValue.ToString();
      TXT_FEET_SWOLLEN_ANKLE.Text = RDO_FEET_SWOLLEN_ANKLE.SelectedValue.ToString();
      TXT_FEET_FOOT_PAIN.Text =RDO_FEET_FOOT_PAIN.SelectedValue.ToString();
      TXT_FEET_NUMBNESS_OF_FOOT.Text = RDO_FEET_NUMBNESS_OF_FOOT.SelectedValue.ToString();
      TXT_FEET_SWOLLEN_FOOT.Text = RDO_FEET_SWOLLEN_FOOT.SelectedValue.ToString();
      TXT_NUMBNESS_IN_FOREARM.Text = RDO_NUMBNESS_IN_FOREARM.SelectedValue.ToString();
      TXT_PAIN_AND_NEEDLESS_ARM.Text=RDO_PAIN_AND_NEEDLESS_ARM.SelectedValue.ToString();




        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Examination_Section.xml";  // Pass xml file to SaveOperation
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
        

        Response.Redirect("~/Bill_Sys_FUIM_Test_Results.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
}
