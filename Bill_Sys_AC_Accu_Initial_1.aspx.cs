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
public partial class Bill_Sys_AC_Accu_Initial : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editoperation;
    string sz_CompanyName = "";
    string sz_CompanyID="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["AC_INITIAL_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }

        txtEventID.Text = Session["AC_INITIAL_EVENT_ID"].ToString(); //Session["AC_INITIAL_EVENT_ID"].ToString();
       // TXT_CURRENT_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
       // sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
      //  sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

        if (!IsPostBack)
        {
           LoadPatientInformation();
            LoadData();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AC_Accu_Initial_1.aspx");
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
        //TXT_PATIENT_ACCIDENT_CATEGORY.Text = RDO_PATIENT_ACCIDENT_CATEGORY.SelectedValue.ToString();
        if (RDO_PATIENT_ACCIDENT_CATEGORY.SelectedValue.ToString().Equals("")) 
        {
            TXT_PATIENT_ACCIDENT_CATEGORY.Text= "-1";
        }
        else
        {
                TXT_PATIENT_ACCIDENT_CATEGORY.Text = RDO_PATIENT_ACCIDENT_CATEGORY.SelectedValue;
        }
        // TXT_PATIENT_HOSPITALIZED.Text = RDO_PATIENT_HOSPITALIZED.SelectedValue.ToString();

         if (RDO_PATIENT_HOSPITALIZED.SelectedValue.ToString().Equals(""))
         {
             TXT_PATIENT_HOSPITALIZED.Text = "-1";
         }
         else
         {
             TXT_PATIENT_HOSPITALIZED.Text = RDO_PATIENT_HOSPITALIZED.SelectedValue;
         }
       // TXT_HEADACHES_LEVEL.Text = RDO_HEADACHES_LEVEL.SelectedValue.ToString();
         if (RDO_HEADACHES_LEVEL.SelectedValue.ToString().Equals(""))
         {
             TXT_HEADACHES_LEVEL.Text = "-1";
         }
         else
         {
             TXT_HEADACHES_LEVEL.Text = RDO_HEADACHES_LEVEL.SelectedValue;
         }
       // TXT_DIZZINESS_LEVEL.Text = RDO_DIZZINESS_LEVEL.SelectedValue.ToString();
         if (RDO_DIZZINESS_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_DIZZINESS_LEVEL.Text = "-1";
        }
        else
        {
            TXT_DIZZINESS_LEVEL.Text = RDO_DIZZINESS_LEVEL.SelectedValue;
        }
       // TXT_SLEEPING_DISORDERS_LEVEL.Text = RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue.ToString();
        if (RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_SLEEPING_DISORDERS_LEVEL.Text = "-1";
        }
        else
        {
            TXT_SLEEPING_DISORDERS_LEVEL.Text = RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue;
        }
       // TXT_NECK_PAIN_LEVEL.Text = RDO_NECK_PAIN_LEVEL.SelectedValue.ToString();
        if (RDO_NECK_PAIN_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_NECK_PAIN_LEVEL.Text = "-1";
        }
        else
        {
            TXT_NECK_PAIN_LEVEL.Text = RDO_NECK_PAIN_LEVEL.SelectedValue;
        }
       // TXT_NECK_STIFFNESS_LEVEL.Text = RDO_NECK_STIFFNESS_LEVEL.SelectedValue.ToString();
        if (RDO_NECK_STIFFNESS_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_NECK_STIFFNESS_LEVEL.Text = "-1";
        }
        else
        {
            TXT_NECK_STIFFNESS_LEVEL.Text = RDO_NECK_STIFFNESS_LEVEL.SelectedValue;
        }
       // TXT_SHOULDER_PAIN_LEVEL.Text = RDO_SHOULDER_PAIN_LEVEL.SelectedValue.ToString();
        if (RDO_SHOULDER_PAIN_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_SHOULDER_PAIN_LEVEL.Text = "-1";
        }
        else
        {
            TXT_SHOULDER_PAIN_LEVEL.Text = RDO_SHOULDER_PAIN_LEVEL.SelectedValue;
        }

        //TXT_SHOULDER_STIFFNESS_LEVEL.Text = RDO_SHOULDER_STIFFNESS_LEVEL.SelectedValue.ToString();


        if (RDO_SHOULDER_STIFFNESS_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_SHOULDER_STIFFNESS_LEVEL.Text ="-1";
        }
        else
        {
            TXT_SHOULDER_STIFFNESS_LEVEL.Text = RDO_SHOULDER_STIFFNESS_LEVEL.SelectedValue;
        }
        //TXT_UPPER_BACK_PAIN_LEVEL.Text = RDO_UPPER_BACK_PAIN_LEVEL.SelectedValue.ToString();
        if (RDO_UPPER_BACK_PAIN_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_UPPER_BACK_PAIN_LEVEL.Text = "-1";
        }
        else
        {
            TXT_UPPER_BACK_PAIN_LEVEL.Text = RDO_UPPER_BACK_PAIN_LEVEL.SelectedValue;
        }
       // TXT_MIDDLE_BACK_PAIN_LEVEL.Text = RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue.ToString();


        if (RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_MIDDLE_BACK_PAIN_LEVEL.Text = "-1";
        }
        else
        {
            TXT_MIDDLE_BACK_PAIN_LEVEL.Text = RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue;
        }
        //TXT_LOWER_BACK_PAIN_LEVEL.Text = RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue.ToString();
        if (RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue.ToString().Equals(""))
        {
            TXT_LOWER_BACK_PAIN_LEVEL.Text = "-1";
        }
        else
        {
            TXT_LOWER_BACK_PAIN_LEVEL.Text = RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue;
        }

        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "AccuInitial_1.xml";
            _saveOperation.SaveMethod();
            Response.Redirect("Bill_Sys_AC_Accu_Initial_2.aspx", false);
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

    public void LoadPatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = Session["AC_INITIAL_EVENT_ID"].ToString(); //Session["AC_INITIAL_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "AccuInitial_Patient_Details.xml";          
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
        _editoperation = new EditOperation();
        try
        {
            _editoperation.Primary_Value = Session["AC_INITIAL_EVENT_ID"].ToString();
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "AccuInitial_1.xml";
            _editoperation.LoadData();



            if (TXT_PATIENT_ACCIDENT_CATEGORY.Text.Equals("-1"))
            {
              
            }
            else   
                {
                    RDO_PATIENT_ACCIDENT_CATEGORY.SelectedValue  = TXT_PATIENT_ACCIDENT_CATEGORY.Text;
                }



                if (TXT_PATIENT_HOSPITALIZED.Text.Equals("-1"))
                {
                }
                else
                {
                    RDO_PATIENT_HOSPITALIZED.SelectedValue = TXT_PATIENT_HOSPITALIZED.Text;
                }
                //else if (TXT_PATIENT_HOSPITALIZED.Text == "2")
                //{
                //    RDO_PATIENT_HOSPITALIZED.SelectedValue = 2;
                //}


                if (TXT_HEADACHES_LEVEL.Text.Equals("-1"))
                 {
                 }
                {
                    RDO_HEADACHES_LEVEL.SelectedValue = TXT_HEADACHES_LEVEL.Text ;
                }
                //else if (TXT_HEADACHES_LEVEL.Text == "2")
                //{
                //    RDO_HEADACHES_LEVEL.SelectedValue = "2";
                //}
                //else if (TXT_HEADACHES_LEVEL.Text == "3")
                //{
                //    RDO_HEADACHES_LEVEL.SelectedValue = "3";
                //}
           

            if (TXT_DIZZINESS_LEVEL.Text.Equals("-1"))
            {
                //if (TXT_DIZZINESS_LEVEL.Text == "1")
            }else
                {
                    RDO_DIZZINESS_LEVEL.SelectedValue = TXT_DIZZINESS_LEVEL.Text;
                }
                //else if (TXT_DIZZINESS_LEVEL.Text == "2")
                //{
                //    RDO_DIZZINESS_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_DIZZINESS_LEVEL.Text == "3")
                //{
                //    RDO_DIZZINESS_LEVEL.SelectedValue = 3;
                //}
         

            if (TXT_SLEEPING_DISORDERS_LEVEL.Text.Equals("-1"))
            {
               //if (TXT_DIZZINESS_LEVEL.Text == "1")
            }else
                {
                    RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue = TXT_SLEEPING_DISORDERS_LEVEL.Text;
                }
                //else if (TXT_SLEEPING_DISORDERS_LEVEL.Text == "2")
                //{
                //    RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_SLEEPING_DISORDERS_LEVEL.Text == "3")
                //{
                //    RDO_SLEEPING_DISORDERS_LEVEL.SelectedValue = 3;
                //}
         

            if (TXT_NECK_PAIN_LEVEL.Text.Equals("-1"))
            {
            }else
                {
                    RDO_NECK_PAIN_LEVEL.SelectedValue = TXT_NECK_PAIN_LEVEL.Text;
                }
              

           if (TXT_NECK_STIFFNESS_LEVEL.Text.Equals("-1"))
            {
           }else
                {
                    RDO_NECK_STIFFNESS_LEVEL.SelectedValue = TXT_NECK_STIFFNESS_LEVEL.Text;
                }
                //else if (TXT_NECK_STIFFNESS_LEVEL.Text == "2")
                //{
                //    RDO_NECK_STIFFNESS_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_NECK_STIFFNESS_LEVEL.Text == "3")
                //{
                //    RDO_NECK_STIFFNESS_LEVEL.SelectedValue = 3;
                //}

                if (TXT_SHOULDER_PAIN_LEVEL.Text.Equals("-1"))
            {
                //if (TXT_SHOULDER_PAIN_LEVEL.Text == "1")
                }else
                {
                    RDO_SHOULDER_PAIN_LEVEL.SelectedValue = TXT_SHOULDER_PAIN_LEVEL.Text ;
                }
                //else if (TXT_SHOULDER_PAIN_LEVEL.Text == "2")
                //{
                //    RDO_SHOULDER_PAIN_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_SHOULDER_PAIN_LEVEL.Text == "3")
                //{
                //    RDO_SHOULDER_PAIN_LEVEL.SelectedValue = 3;
                //}


                if (TXT_SHOULDER_STIFFNESS_LEVEL.Text.Equals("-1"))
            {
                //if (TXT_SHOULDER_STIFFNESS_LEVEL.Text == "1")
                }else
                {
                    RDO_SHOULDER_STIFFNESS_LEVEL.SelectedValue = TXT_SHOULDER_STIFFNESS_LEVEL.Text;
                }


                if (TXT_UPPER_BACK_PAIN_LEVEL.Text.Equals("-1"))
                {
                    // if (TXT_UPPER_BACK_PAIN_LEVEL.Text == "1")
                }
                {
                    RDO_UPPER_BACK_PAIN_LEVEL.SelectedValue = TXT_UPPER_BACK_PAIN_LEVEL.Text ;
                }
  

            if (TXT_MIDDLE_BACK_PAIN_LEVEL.Text .Equals("-1"))
            {
               //if (TXT_MIDDLE_BACK_PAIN_LEVEL.Text == "1")
            }else
                {
                    RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue = TXT_MIDDLE_BACK_PAIN_LEVEL.Text;
                }
                //else if (TXT_MIDDLE_BACK_PAIN_LEVEL.Text == "2")
                //{
                //    RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_MIDDLE_BACK_PAIN_LEVEL.Text == "3")
                //{
                //    RDO_MIDDLE_BACK_PAIN_LEVEL.SelectedValue = 3;
                //}
          

            if (TXT_LOWER_BACK_PAIN_LEVEL.Text.Equals("-1"))
            {
                //if (TXT_LOWER_BACK_PAIN_LEVEL.Text == "1")
            }else
                {
                    RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue = TXT_LOWER_BACK_PAIN_LEVEL.Text;
                }
                //else if (TXT_LOWER_BACK_PAIN_LEVEL.Text == "2")
                //{
                //    RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue = 2;
                //}
                //else if (TXT_LOWER_BACK_PAIN_LEVEL.Text == "3")
                //{
                //    RDO_LOWER_BACK_PAIN_LEVEL.SelectedValue = 3;
                //}
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
