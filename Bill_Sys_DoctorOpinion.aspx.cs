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

public partial class Bill_Sys_DoctorOpinion : PageBase
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
        txtRelevantDiagosticTest.Attributes.Add("Onkeyup", "CheckLengthRelevantDiagosticTest(this,190)");
        if (Page.IsPostBack == false)
        {
            LoadData();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_DoctorOpinion.aspx");
        }
        #endregion
    }
   
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        WorkerTemplate _obj = new WorkerTemplate();
        try
        {
            if (_obj.PatientExistCheckForWC4("SP_MST_DOCTORS_OPINION_NEW", Session["TEMPLATE_BILL_NO"].ToString()) == false)
            {
                //txthdnHistoryOfInjury.Text = chkHistoryOfInjury.SelectedValue;
                //txthdnMedicalCauses.Text = chkMedicalCauses.SelectedValue ;
                //txthdnObjectiveFindings.Text = chkObjectiveFindings.SelectedValue;

                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "DoctorOpinion_New.xml";
                _saveOperation.SaveMethod();
            }
            else
            {
                updatedata();
            }
            Response.Redirect("Bill_Sys_PlanOfCare.aspx", false);


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
            //txthdnHistoryOfInjury.Text = chkHistoryOfInjury.SelectedValue;
            //txthdnMedicalCauses.Text = chkMedicalCauses.SelectedValue;
            //txthdnObjectiveFindings.Text = chkObjectiveFindings.SelectedValue;


            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "DoctorOpinionNew.xml";
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
        _editOperation = new EditOperation();
        try
        {
            
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "DoctorOpinion_New.xml";
            _editOperation.LoadData();

            //chkObjectiveFindings.SelectedValue = txthdnObjectiveFindings.Text;

            //if (txthdnHistoryOfInjury.Text == "False")
            //    chkHistoryOfInjury.SelectedValue = "0";

            //if (txthdnHistoryOfInjury.Text == "True")
            //    chkHistoryOfInjury.SelectedValue = "1";

            //if (txthdnMedicalCauses.Text == "False")
            //    chkMedicalCauses.SelectedValue = "0";

            //if (txthdnMedicalCauses.Text == "True")
            //    chkMedicalCauses.SelectedValue = "1";
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
}