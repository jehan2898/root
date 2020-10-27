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

public partial class Bill_Sys_ReferalManagement : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_ReferalReminder _bill_Sys_ReferalReminder;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            String szScript = "";
            szScript = "<script language=JavaScript> " + "function autoComplete (field, select, property, forcematch) {" + "var found = false;" + "for (var i = 0; i < select.options.length; i++) {" + "if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {" + "		found=true; break;" + "}" + "}" + "if (found) { " + "select.selectedIndex = i;" + "}else {" + "select.selectedIndex = -1;" + "}" + "if (field.createTextRange) {" + "if (forcematch && !found) {" + "field.value=field.value.substring(0,field.value.length-1); " + "return;" + "}" + "var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';" + "if (cursorKeys.indexOf(event.keyCode+';') == -1) {" + "var r1 = field.createTextRange();" + "var oldValue = r1.text;" + "var newValue = found ? select.options[i][property] : oldValue;" + "if (newValue != field.value) {" + "field.value = newValue;" + "var rNew = field.createTextRange();" + "rNew.moveStart('character', oldValue.length) ;" + "rNew.select(); " + "}" + "}" + "}" + "} </script>";

           
            if(!IsClientScriptBlockRegistered("clientScript"))
            {
                RegisterClientScriptBlock("clientScript", szScript);
            }
           

            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;

            btnUpdate.Attributes.Add("onclick", "return formValidator('frmReferalManagement','extddlReferalList,extddlProceduralGroup,txtReminderDate,txtScheduleDate');");
            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            extddlProceduralGroup.Flag_ID = txtCompanyID.Text;
            extddlReferalList.Flag_ID = txtCompanyID.Text;
            if (!IsPostBack)
            {
                BindGrid();
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ReferalManagement.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    

 #region "Event Hanlder"

    protected void btnReset_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
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
    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          
            extddlReferalList.Text = "NA";
            txtScheduleDate.Text = "";
            extddlProceduralGroup.Text = "NA";
            lstProcedureCode.Items.Clear();
            txtReminderDate.Text = "";
            lblMsg.Visible = false;
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
            if (ValideDate())
            {

                //_saveOperation.WebPage = this.Page;
                //_saveOperation.Xml_File = "ReferalManagement.xml";
                //_saveOperation.SaveMethod();
                UpdateMethod();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Referal Infomation Saved Successfully ...!";
                ClearControl();
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
    
    private bool ValideDate()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool validate = true;
        try
        {
            if (Convert.ToDateTime(txtScheduleDate.Text) < System.DateTime.Now)
            {              
         
                lblMsg.Visible = true;
                lblMsg.Text = "Scheduled date should be greater...!";
                txtScheduleDate.Text = "";          
                validate = false;
            }
            else if (Convert.ToDateTime(txtReminderDate.Text) < System.DateTime.Now)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Reminder date should be greater ...!";
                txtReminderDate.Text = "";
                validate = false;
            }
            else if (Convert.ToDateTime(txtReminderDate.Text) >= Convert.ToDateTime(txtScheduleDate.Text))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Reminder date should be less than shedule date ...!";
                txtReminderDate.Text = "";
                validate = false;                
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
       
        return validate;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
  
    #endregion

 #region "Fetch Method"

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "ReferalManagement.xml";
            _listOperation.LoadList();
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

    private void UpdateMethod()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList _arrayList;
        _bill_Sys_ReferalReminder = new Bill_Sys_ReferalReminder();
        try
        {
            _arrayList = new ArrayList();

            for (int i = 0; i < lstProcedureCode.Items.Count ; i++)
            {
                if (lstProcedureCode.Items[i].Selected)
                {
                    _arrayList.Clear();
                    _arrayList.Add(extddlReferalList.Text);
                    _arrayList.Add(lstProcedureCode.Items[i].Value); 
                    _arrayList.Add(txtReminderDate.Text);
                    _arrayList.Add(txtScheduleDate.Text);
                    _arrayList.Add(txtUserID.Text);
                    _arrayList.Add(txtUserID.Text);       
                    _arrayList.Add(txtCompanyID.Text);
                    _bill_Sys_ReferalReminder.saveReminder(_arrayList);
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

    #endregion

    

    protected void extddlProceduralGroup_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_ReferalReminder = new Bill_Sys_ReferalReminder();
            lstProcedureCode.DataSource = _bill_Sys_ReferalReminder.GetProcedureCode(extddlProceduralGroup.Text,txtCompanyID.Text).Tables[0];
            lstProcedureCode.DataValueField = "CODE";
            lstProcedureCode.DataTextField = "DESCRIPTION";
            lstProcedureCode.DataBind();
            if (lstProcedureCode.Items.Count > 0)
            {
                lstProcedureCode.Items[0].Selected = true;
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