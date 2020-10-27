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

public partial class AJAX_Pages_Bill_Sys_PopupNewVisit : PageBase
{
    private CaseDetailsBO objCaseDetails;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;
    private Bill_Sys_PatientBO objPatientBO;
    DataSet dset;

    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnSave.Attributes.Add("onclick", "return formValidator('form1','ddldoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
            dset = new DataSet();
            objCaseDetails = new CaseDetailsBO();
            string sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_company_id = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).ToString();
            string sz_location_Id = objCaseDetails.GetPatientLocationID(sz_case_id, sz_company_id);


           // txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
           // extddlVisitType.Flag_ID = txtCompanyID.Text;


            //checkForReferringFacility();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
    protected void extddlReferringFacility_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void extddlRoom_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    { 
    }

       
}
