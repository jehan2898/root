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
public partial class PatientInformationC4_2 : PageBase
{
    private EditOperation _editOperation;
    private Bill_Sys_BillingCompanyDetails_BO _billingCompanyDetailsBO;
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        btnSaveAndGoToNext.Attributes.Add("onclick", "return ConfirmUpdate();");
        try
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            //if (Request.QueryString["BillNumber"] != null)
            //{
            //    Session["TEMPLATE_BILL_NO"] = Request.QueryString["BillNumber"];
            //}
            //txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();// "sas0000001";           
            if (Request.QueryString["doctorID"] != null)
            {
                Session["DOCTOR_ID"] = Request.QueryString["doctorID"];
            }
            if (Request.QueryString["caseid"] != null)
            {
                txtCaseID.Text = Request.QueryString["caseid"].ToString();
                Session["caseid"] = txtCaseID.Text;
            }

            if (Request.QueryString["patientID"] != null)
            {
                txtPatientID.Text = Request.QueryString["patientID"].ToString();
                Session["patientID"] = txtPatientID.Text;
            }

            if (!IsPostBack)
            {
                _billingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                //txtPatientID.Text = _billingCompanyDetailsBO.GetInsuranceCompanyID(txtBillNumber.Text, "SP_MST_PATIENT_INFORMATION", "GETPATIENTID");
                //Session["C4_2_PATIENT_ID"] = txtPatientID.Text;
                if (Session["patientID"] != null)
                {
                    txtPatientID.Text = Session["patientID"].ToString();
                    Session["PatientID"] = txtPatientID.Text;
                    LoadData();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("PatientInformationC4_2.aspx");
        }
        #endregion
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }


    private void LoadData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtPatientID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientInformation_C4_2_MasterBilling.xml";
            _editOperation.LoadData();
            if (txtDateofExam.Text != "")
            {
                //txtDateofExam.Text = Convert.ToDateTime(txtDateofExam.Text).ToShortDateString();
                txtDateofExam.Text = txtDateofExam.Text;
            }
            if (txtDateOfInjury.Text != "")
            {
                txtDateOfInjury.Text = Convert.ToDateTime(txtDateOfInjury.Text).ToShortDateString();
            }
            if (txtstateid.Text != "")
            {
                extddlPatientState.Text = txtstateid.Text;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (btnhidden.Value == "YES")
        {
            btnhidden.Value = "";
            try
            {
                UpdateData();
                Response.Redirect("DoctorsInformationC4_2.aspx", false);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
        }
        else
        {
            Response.Redirect("DoctorsInformationC4_2.aspx", false);
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void UpdateData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _editOperation = new EditOperation();
        try
        {
            txtstateid.Text = extddlPatientState.Text;
            _editOperation.Primary_Value = txtPatientID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientInformation_C4_2_MasterBilling.xml";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}