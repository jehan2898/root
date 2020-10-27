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


public partial class Bill_Sys_History : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtPatientID.Text = Session["PatientID"].ToString();
        txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();
        //   imgbtnPreviouslyTreatedDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtPreviouslyTreated,'imgbtnPreviouslyTreatedDate','MM/dd/yyyy'); return false;");
        rdlstHospitalization.Attributes.Add("onclick", "validate_historyradiobtn();");
        rdlstInjury.Attributes.Add("onclick", "validate_rdlInjury();");
        rdlstPreviouslyTreated.Attributes.Add("onclick", "validate_PreviouslyTreated();");
        if (Page.IsPostBack == false)
        {
            LoadData();
            if (txtPreviouslyTreated.Text.ToString() == "1/1/1900")
            {
                txtPreviouslyTreated.Text = "";
            }
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_History.aspx");
        }

        txtInjuryHappen.Attributes.Add("Onkeyup", "CheckLength(this,260)");
        txtHospitalization.Attributes.Add("Onkeyup", "CheckLengthhop(this,120)");
        #endregion
        
    }
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(),"");
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        WorkerTemplate _obj = new WorkerTemplate();
        try
        {
            if (_obj.PatientExistCheckForWC4("SP_MST_HISTORY_NEW", Session["TEMPLATE_BILL_NO"].ToString()) == false)
            {

                //txthdnInjury.Text = chklstInjury.SelectedValue;
                //txthdnHospitalization.Text = chklstHospitalization.SelectedValue;
                //txthdnPreviouslyTreated.Text = chkPreviouslyTreated.SelectedValue;

                txtPrevTreat.Text = rdlstPreviouslyTreated.SelectedIndex.ToString();
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "History_New.xml";
                _saveOperation.SaveMethod();
            }
            else
	        {
                updatedata();
	        }
            Response.Redirect("Bill_Sys_ExamInformation.aspx", false);


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
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    public void updatedata()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            //txthdnInjury.Text = chklstInjury.SelectedValue;
            //txthdnHospitalization.Text = chklstHospitalization.SelectedValue;
            //txthdnPreviouslyTreated.Text = chkPreviouslyTreated.SelectedValue;
            txtPrevTreat.Text = rdlstPreviouslyTreated.SelectedIndex.ToString();
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "HistoryNew.xml";
            _editOperation.UpdateMethod();

           
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


    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
      
        try
        {
            _editOperation = new EditOperation();
           
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "History_New.xml";
            _editOperation.LoadData();


            if(txtPreviouslyTreated.Text != "")
                txtPreviouslyTreated.Text = Convert.ToDateTime(txtPreviouslyTreated.Text).ToShortDateString();

            //chklstInjury.SelectedValue = txthdnInjury.Text;

            //if (txthdnHospitalization.Text == "False")
            //    chklstHospitalization.SelectedValue = "0";
           
            //if (txthdnHospitalization.Text == "True")
            //    chklstHospitalization.SelectedValue = "1";

            //if (txthdnPreviouslyTreated.Text == "False")
            //    chkPreviouslyTreated.SelectedValue = "0";
            
            //if (txthdnPreviouslyTreated.Text == "True")
            //    chkPreviouslyTreated.SelectedValue = "1";
          
                if (txtPrevTreat.Text != "" && txtPrevTreat.Text.ToLower() != "null")
                {
                    rdlstPreviouslyTreated.SelectedValue = txtPrevTreat.Text;
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
