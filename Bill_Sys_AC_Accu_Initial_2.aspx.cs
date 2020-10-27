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
public partial class Bill_Sys_AC_Accu_Initial_2 : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["AC_INITIAL_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        txtEventID.Text = Session["AC_INITIAL_EVENT_ID"].ToString();//Session["AC_INITIAL_EVENT_ID"].ToString();

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
            cv.MakeReadOnlyPage("Bill_Sys_AC_Accu_Initial_2.aspx");
        }
        #endregion
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       // TXT_TONGUE_COLOR.Text = RDO_TONGUE_COLOR.SelectedValue.ToString();
        if (RDO_TONGUE_COLOR.SelectedValue.ToString().Equals(""))
        {
            TXT_TONGUE_COLOR.Text = "-1";
        }else
        {
            TXT_TONGUE_COLOR.Text = RDO_TONGUE_COLOR.SelectedValue;
        }

        //TXT_COATING_COLOR.Text = RDO_COATING_COLOR.SelectedValue.ToString();
        if (RDO_COATING_COLOR.SelectedValue.ToString().Equals(""))
        {
            TXT_COATING_COLOR.Text = "-1";
        }
        else
        {
            TXT_COATING_COLOR.Text = RDO_COATING_COLOR.SelectedValue;
        }
        //TXT_COATING_THICKNESS.Text = RDO_COATING_THICKNESS.SelectedValue.ToString();
        if (RDO_COATING_THICKNESS.SelectedValue.ToString().Equals(""))
        {
            TXT_COATING_THICKNESS.Text = "-1";
        }
        else
        {
            TXT_COATING_THICKNESS.Text = RDO_COATING_THICKNESS.SelectedValue;
        }
       // TXT_PULSE.Text = RDO_PULSE.SelectedValue.ToString();
        if (RDO_PULSE.SelectedValue.ToString().Equals(""))
        {
            TXT_PULSE.Text = "-1";
        }
        else
        {
            TXT_PULSE.Text = RDO_PULSE.SelectedValue;
        }
       // TXT_MUSCLE_TENSION.Text = RDO_MUSCLE_TENSION.SelectedValue.ToString();
        if (RDO_MUSCLE_TENSION.SelectedValue.ToString().Equals(""))
        {
            TXT_MUSCLE_TENSION.Text = "-1";
        }
        else
        {
            TXT_MUSCLE_TENSION.Text = RDO_MUSCLE_TENSION.SelectedValue;
        }
       // TXT_NECK_MOTION.Text = RDO_NECK_MOTION.SelectedValue.ToString();

        if (RDO_NECK_MOTION.SelectedValue.ToString().Equals(""))
        {
            TXT_NECK_MOTION.Text = "-1";
        }
        else
        {
            TXT_NECK_MOTION.Text = RDO_NECK_MOTION.SelectedValue;
        }
        //TXT_BACK_MOTION.Text = RDO_BACK_MOTION.SelectedValue.ToString();

        if (RDO_BACK_MOTION.SelectedValue.ToString().Equals(""))
        {
            TXT_BACK_MOTION.Text = "-1";
        }
        else
        {
            TXT_BACK_MOTION.Text = RDO_BACK_MOTION.SelectedValue;
        }
       //TXT_SHOULDER_LEFT_RIGHT_MOTION.Text = RDO_SHOULDER_LEFT_RIGHT_MOTION.SelectedValue.ToString();
        if (RDO_SHOULDER_LEFT_RIGHT_MOTION.SelectedValue.ToString().Equals(""))
        {
            TXT_SHOULDER_LEFT_RIGHT_MOTION.Text = "-1";
        }
        else
        {
            TXT_SHOULDER_LEFT_RIGHT_MOTION.Text = RDO_SHOULDER_LEFT_RIGHT_MOTION.SelectedValue;
        }


        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "AccuInitial_2.xml";
            _saveOperation.SaveMethod();
               Response.Redirect("Bill_Sys_AC_Accu_Initial_3.aspx", false);
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
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
            _editOperation.Primary_Value = txtEventID.Text; ;// Session["AC_INITIAL_EVENT_ID"].ToString();
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

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_AC_Accu_Initial_1.aspx", false);
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
        try
        {
            EditOperation _editoperation = new EditOperation();
            _editoperation.Primary_Value = txtEventID.Text;
            _editoperation.WebPage = this.Page;
            _editoperation.Xml_File = "AccuInitial_2.xml";
            _editoperation.LoadData();



            if (TXT_TONGUE_COLOR.Text.Equals("-1"))
            {
              //  if (TXT_TONGUE_COLOR.Text == "1")
            }else
                {
                    RDO_TONGUE_COLOR.SelectedValue = TXT_TONGUE_COLOR.Text;
                }
                //else if (TXT_TONGUE_COLOR.Text == "2")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 2;
                //}
                //else if (TXT_TONGUE_COLOR.Text == "3")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 3;
                //}
                //else if (TXT_TONGUE_COLOR.Text == "4")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 4;
                //}
                //else if (TXT_TONGUE_COLOR.Text == "5")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 5;
                //}
                //else if (TXT_TONGUE_COLOR.Text == "6")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 6;
                //}
                //else if (TXT_TONGUE_COLOR.Text == "7")
                //{
                //    RDO_TONGUE_COLOR.SelectedValue = 7;
                //}
           // }

                if (TXT_COATING_COLOR.Text.Equals("-1"))
            {
                
               
               
            }else
            {
                RDO_COATING_COLOR.SelectedValue = TXT_COATING_COLOR.Text;
            }

            if (TXT_COATING_THICKNESS.Text.Equals("-1"))
            {



            }
            else
            
                {
                    RDO_COATING_THICKNESS.SelectedValue = TXT_COATING_THICKNESS.Text;
                }
            
                    


            if (TXT_PULSE.Text.Equals("-1"))
            {



            }
            else
            {
                RDO_PULSE.SelectedValue = TXT_PULSE.Text;
            }
            if (TXT_MUSCLE_TENSION.Text.Equals("-1"))
            {
            }else 
                {
                    RDO_MUSCLE_TENSION.SelectedValue = TXT_MUSCLE_TENSION.Text;
                }
               

            if (TXT_NECK_MOTION.Text.Equals("-1"))
            {
            }else
              
                {
                    RDO_NECK_MOTION.SelectedValue = TXT_NECK_MOTION.Text;
                }
              

            if (TXT_BACK_MOTION.Text.Equals("-1"))
            {
            }else
                {
                    RDO_BACK_MOTION.SelectedValue = TXT_BACK_MOTION.Text;
                }
               

         

            if (TXT_SHOULDER_LEFT_RIGHT_MOTION.Text.Equals("-1"))
            {
            }
            else
                {
                    RDO_SHOULDER_LEFT_RIGHT_MOTION.SelectedValue = TXT_SHOULDER_LEFT_RIGHT_MOTION.Text;
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
