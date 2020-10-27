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
using System.Text;
using System.IO;
using System.Collections;
using Componend;

public partial class Bill_Sys_CO_Chiro : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["CO_CHIRO_EVENT_ID"] = "12007"; "for testing used
        if (Request.QueryString["EID"] != null)
        {
            Session["CO_CHIRO_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }

        TXT_EVENT_ID.Text=Session["CO_CHIRO_EVENT_ID"].ToString();
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
            cv.MakeReadOnlyPage("Bill_Sys_CO_Chiro.aspx");
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

        TXT_INFORMATION_MODE1.Text = RDO_INFORMATION_MODE.SelectedValue.ToString();
        TXT_LEFT_RIGHT_PASSENGER.Text = RDO_LEFT_RIGHT_PASSENGER.SelectedValue.ToString();
        TXT_SEAT_BELT_WEARING.Text = RDO_SEAT_BELT_WEARING.SelectedValue.ToString();
        TXT_AIR_BAG_DEPLOYMENT.Text = RDO_AIR_BAG_DEPLOYMENT.SelectedValue.ToString();
        TXT_FRONT_REAR_IMPACT.Text = RDO_FRONT_REAR_IMPACT.SelectedValue.ToString();
        TXT_LEFT_RIGHT_SIDE_IMPACT.Text = RDO_LEFT_RIGHT_SIDE_IMPACT.SelectedValue.ToString();
        TXT_R_L_KNEE_PAIN.Text = RDO_R_L_KNEE_PAIN.SelectedValue.ToString();
        TXT_R_L_SHOULDER_PAIN.Text = RDO_R_L_SHOULDER_PAIN.SelectedValue.ToString();
        TXT_R_L_HIP_PAIN.Text = RDO_R_L_HIP_PAIN.SelectedValue.ToString();
        TXT_R_L_ARM_LEG.Text = RDO_R_L_ARM_LEG.SelectedValue.ToString();
        TXT_R_L_HAND_FOOT.Text = RDO_R_L_HAND_FOOT.SelectedValue.ToString();
        TXT_R_L_DIGITS.Text = RDO_R_L_DIGITS.SelectedValue.ToString();
        TXT_R_L_ACCURATE_DIGITS_PAIN.Text = RDO_R_L_ACCURATE_DIGITS_PAIN.SelectedValue.ToString();
        TXT_R_L_TOES.Text = RDO_R_L_TOES.SelectedValue.ToString();
        TXT_R_L_ACCURATE_TOES_PAIN.Text = RDO_R_L_ACCURATE_TOES_PAIN.SelectedValue.ToString();
        TXT_AMBULANCE_CALLED.Text = RDO_AMBULANCE_CALLED.SelectedValue.ToString();
        TXT_MULTIPLE_X_RAY_TAKEN.Text = RDO_MULTIPLE_X_RAY_TAKEN.SelectedValue.ToString();
        TXT_POSITIVE_NEGATIVE_FRACTURE.Text = RDO_POSITIVE_NEGATIVE_FRACTURE.SelectedValue.ToString();


        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
            _saveOperation.Xml_File = "Bill_Sys_CO_Chiro.xml";  // Pass xml file to SaveOperation
            _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.

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
        Response.Redirect("Bill_Sys_CO_Chiro_Past_Medical_History.aspx");
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
            _editOperation.Xml_File = "Bill_Sys_CO_Chiro.xml";
            _editOperation.LoadData();

            if (TXT_INFORMATION_MODE1.Text.Trim() != "-1")
            {
                if (TXT_INFORMATION_MODE1.Text.Trim() == "0")
                {
                    RDO_INFORMATION_MODE.SelectedIndex = 0;
                }
                else if (TXT_INFORMATION_MODE1.Text.Trim() == "1")
                {
                    RDO_INFORMATION_MODE.SelectedIndex = 1;
                }
            }



            if (TXT_LEFT_RIGHT_PASSENGER.Text.Trim() != "-1")
            {
                if (TXT_LEFT_RIGHT_PASSENGER.Text.Trim() == "0")
                {
                    RDO_LEFT_RIGHT_PASSENGER.SelectedIndex = 0;
                }
                else if (TXT_LEFT_RIGHT_PASSENGER.Text.Trim() == "1")
                {
                    RDO_LEFT_RIGHT_PASSENGER.SelectedIndex = 1;
                }
            }
            if (TXT_SEAT_BELT_WEARING.Text.Trim() != "-1")
            {
                if (TXT_SEAT_BELT_WEARING.Text.Trim() == "0")
                {
                    RDO_SEAT_BELT_WEARING.SelectedIndex = 0;
                }
                else if (TXT_SEAT_BELT_WEARING.Text.Trim() == "1")
                {
                    RDO_SEAT_BELT_WEARING.SelectedIndex = 1;
                }
            }
            if (TXT_AIR_BAG_DEPLOYMENT.Text.Trim() != "-1")
            {
                if (TXT_AIR_BAG_DEPLOYMENT.Text.Trim() == "0")
                {
                    RDO_AIR_BAG_DEPLOYMENT.SelectedIndex = 0;
                }
                else if (TXT_AIR_BAG_DEPLOYMENT.Text.Trim() == "1")
                {
                    RDO_AIR_BAG_DEPLOYMENT.SelectedIndex = 1;
                }
            }
            if (TXT_FRONT_REAR_IMPACT.Text.Trim() != "-1")
            {
                if (TXT_FRONT_REAR_IMPACT.Text.Trim() == "0")
                {
                    RDO_FRONT_REAR_IMPACT.SelectedIndex = 0;
                }
                else if (TXT_FRONT_REAR_IMPACT.Text.Trim() == "1")
                {
                    RDO_FRONT_REAR_IMPACT.SelectedIndex = 1;
                }
            }
            if (TXT_LEFT_RIGHT_SIDE_IMPACT.Text.Trim() != "-1")
            {
                if (TXT_LEFT_RIGHT_SIDE_IMPACT.Text.Trim() == "0")
                {
                    RDO_LEFT_RIGHT_SIDE_IMPACT.SelectedIndex = 0;
                }
                else if (TXT_LEFT_RIGHT_SIDE_IMPACT.Text.Trim() == "1")
                {
                    RDO_LEFT_RIGHT_SIDE_IMPACT.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_KNEE_PAIN.Text.Trim() != "-1")
            {
                if (TXT_R_L_KNEE_PAIN.Text.Trim() == "0")
                {
                    RDO_R_L_KNEE_PAIN.SelectedIndex = 0;
                }
                else if (TXT_R_L_KNEE_PAIN.Text.Trim() == "1")
                {
                    RDO_R_L_KNEE_PAIN.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_SHOULDER_PAIN.Text.Trim() != "-1")
            {
                if (TXT_R_L_SHOULDER_PAIN.Text.Trim() == "0")
                {
                    RDO_R_L_SHOULDER_PAIN.SelectedIndex = 0;
                }
                else if (TXT_R_L_SHOULDER_PAIN.Text.Trim() == "1")
                {
                    RDO_R_L_SHOULDER_PAIN.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_HIP_PAIN.Text.Trim() != "-1")
            {
                if (TXT_R_L_HIP_PAIN.Text.Trim() == "0")
                {
                    RDO_R_L_HIP_PAIN.SelectedIndex = 0;
                }
                else if (TXT_R_L_HIP_PAIN.Text.Trim() == "1")
                {
                    RDO_R_L_HIP_PAIN.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_ARM_LEG.Text.Trim() != "-1")
            {
                if (TXT_R_L_ARM_LEG.Text.Trim() == "0")
                {
                    RDO_R_L_ARM_LEG.SelectedIndex = 0;
                }
                else if (TXT_R_L_ARM_LEG.Text.Trim() == "1")
                {
                    RDO_R_L_ARM_LEG.SelectedIndex = 1;
                }
            }

            if (TXT_R_L_HAND_FOOT.Text.Trim() != "-1")
            {
                if (TXT_R_L_HAND_FOOT.Text.Trim() == "0")
                {
                    RDO_R_L_HAND_FOOT.SelectedIndex = 0;
                }
                else if (TXT_R_L_HAND_FOOT.Text.Trim() == "1")
                {
                    RDO_R_L_HAND_FOOT.SelectedIndex = 1;
                }
            }



            if (TXT_R_L_DIGITS.Text.Trim() != "-1")
            {
                if (TXT_R_L_DIGITS.Text.Trim() == "0")
                {
                    RDO_R_L_DIGITS.SelectedIndex = 0;
                }
                else if (TXT_R_L_DIGITS.Text.Trim() == "1")
                {
                    RDO_R_L_DIGITS.SelectedIndex = 1;
                }
            }

            if (TXT_R_L_ACCURATE_DIGITS_PAIN.Text.Trim() != "-1")
            {
                if (TXT_R_L_ACCURATE_DIGITS_PAIN.Text.Trim() == "0")
                {
                    RDO_R_L_ACCURATE_DIGITS_PAIN.SelectedIndex = 0;
                }
                else if (TXT_R_L_ACCURATE_DIGITS_PAIN.Text.Trim() == "1")
                {
                    RDO_R_L_ACCURATE_DIGITS_PAIN.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_TOES.Text.Trim() != "-1")
            {
                if (TXT_R_L_TOES.Text.Trim() == "0")
                {
                    RDO_R_L_TOES.SelectedIndex = 0;
                }
                else if (TXT_R_L_TOES.Text.Trim() == "1")
                {
                    RDO_R_L_TOES.SelectedIndex = 1;
                }
            }
            if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() != "-1")
            {
                if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() == "0")
                {
                    RDO_R_L_ACCURATE_TOES_PAIN.SelectedIndex = 0;
                }
                else if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() == "1")
                {
                    RDO_R_L_ACCURATE_TOES_PAIN.SelectedIndex = 1;
                }
                else if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() == "2")
                {
                    RDO_R_L_ACCURATE_TOES_PAIN.SelectedIndex = 2;
                }
                else if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() == "3")
                {
                    RDO_R_L_ACCURATE_TOES_PAIN.SelectedIndex = 3;
                }
                else if (TXT_R_L_ACCURATE_TOES_PAIN.Text.Trim() == "4")
                {
                    RDO_R_L_ACCURATE_TOES_PAIN.SelectedIndex = 4;
                }
            }
            if (TXT_AMBULANCE_CALLED.Text.Trim() != "-1")
            {
                if (TXT_AMBULANCE_CALLED.Text.Trim() == "0")
                {
                    RDO_AMBULANCE_CALLED.SelectedIndex = 0;
                }
                else if (TXT_AMBULANCE_CALLED.Text.Trim() == "1")
                {
                    RDO_AMBULANCE_CALLED.SelectedIndex = 1;
                }
            }
            if (TXT_MULTIPLE_X_RAY_TAKEN.Text.Trim() != "-1")
            {
                if (TXT_MULTIPLE_X_RAY_TAKEN.Text.Trim() == "0")
                {
                    RDO_MULTIPLE_X_RAY_TAKEN.SelectedIndex = 0;
                }
                else if (TXT_MULTIPLE_X_RAY_TAKEN.Text.Trim() == "1")
                {
                    RDO_MULTIPLE_X_RAY_TAKEN.SelectedIndex = 1;
                }
            }
            if (TXT_POSITIVE_NEGATIVE_FRACTURE.Text.Trim() != "-1")
            {
                if (TXT_POSITIVE_NEGATIVE_FRACTURE.Text.Trim() == "0")
                {
                    RDO_POSITIVE_NEGATIVE_FRACTURE.SelectedIndex = 0;
                }
                else if (TXT_POSITIVE_NEGATIVE_FRACTURE.Text.Trim() == "1")
                {
                    RDO_POSITIVE_NEGATIVE_FRACTURE.SelectedIndex = 1;
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
