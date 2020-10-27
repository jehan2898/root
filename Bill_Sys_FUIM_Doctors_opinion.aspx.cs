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

public partial class Bill_Sys_FUIM_Doctors_opinion : PageBase
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_Doctors_opinion.aspx");
        }
        #endregion
        
    }
    protected void BTN_PREVIOUS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bill_Sys_FUIM_Plan.aspx");
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
       
        TXT_DOS.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
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

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Doctors_Opinion.xml";
            _editOperation.LoadData();

            if (TXT_CAUSE_OF_INJURY_YES.Text != "-1")
            {
                if (TXT_CAUSE_OF_INJURY_YES.Text == "0")
                {
                    RDO_CAUSE_OF_INJURY_YES.SelectedIndex = 0;
                }
                else if (TXT_CAUSE_OF_INJURY_YES.Text == "1")
                {
                    RDO_CAUSE_OF_INJURY_YES.SelectedIndex = 1;
                }

            }
            if (TXT_COMPLAINT_CONSISTENT_YES.Text != "-1")
            {
                if (TXT_COMPLAINT_CONSISTENT_YES.Text == "0")
                {
                    RDO_COMPLAINT_CONSISTENT_YES.SelectedIndex = 0;
                }
                else if (TXT_COMPLAINT_CONSISTENT_YES.Text == "1")
                {
                    RDO_COMPLAINT_CONSISTENT_YES.SelectedIndex = 1;
                }
           }
           if (TXT_CONSISTENT_OBJ_FIDINGS_YES.Text.Trim()!= "-1")
           {
               if (TXT_CONSISTENT_OBJ_FIDINGS_YES.Text.Trim() == "0")
               {
                   RDO_CONSISTENT_OBJ_FIDINGS_YES.SelectedIndex = 0;
               }
               else if (TXT_CONSISTENT_OBJ_FIDINGS_YES.Text.Trim() == "1")
               {
                   RDO_CONSISTENT_OBJ_FIDINGS_YES.SelectedIndex = 1;
               }
               else if (TXT_CONSISTENT_OBJ_FIDINGS_YES.Text.Trim() == "2")
               {
                   RDO_CONSISTENT_OBJ_FIDINGS_YES.SelectedIndex = 2;
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


    protected void BTN_SAVE_NEXT_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        TXT_CAUSE_OF_INJURY_YES.Text= RDO_CAUSE_OF_INJURY_YES.SelectedValue.ToString();
       TXT_COMPLAINT_CONSISTENT_YES.Text=RDO_COMPLAINT_CONSISTENT_YES.SelectedValue.ToString();
       TXT_CONSISTENT_OBJ_FIDINGS_YES.Text=RDO_CONSISTENT_OBJ_FIDINGS_YES.SelectedValue.ToString();


        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Doctors_Opinion.xml";  // Pass xml file to SaveOperation
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
        
        Response.Redirect("~/Bill_Sys_FUIM_Return_To_Work.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
}
