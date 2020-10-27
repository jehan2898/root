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

public partial class AJAX_Pages_Bill_Sys_Psy_Information : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCaseID.Text = ((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID;
            if (!IsPostBack)
            {
                LoadData();
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

    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_save objGetInfo = new Bill_save();
       
        DataSet ds_psy_info = new DataSet();
        try
        {

            ds_psy_info = objGetInfo.Get_Psy_Info(txtCaseID.Text, txtCompanyID.Text);
            if (ds_psy_info.Tables.Count > 0)
            {
                if (ds_psy_info.Tables[0].Rows.Count > 0)
                {
                    if (ds_psy_info.Tables[0].Rows[0].ToString() != "" && ds_psy_info.Tables[0].Rows[0].ToString() != null)
                    {
                        string attending = ds_psy_info.Tables[0].Rows[0]["i_attending_psy"].ToString();
                        if (attending == "1")
                        {
                            rbattenting_psy.SelectedIndex = 0;
                        }

                        else if (attending == "2")
                        {
                            rbattenting_psy.SelectedIndex = 1;

                        }
                        string ser_provider = ds_psy_info.Tables[0].Rows[0]["bt_service_provider"].ToString();
                        if (ser_provider == "1")
                        {
                            rbserviceprovider.SelectedIndex = 0;
                        }

                        else if (ser_provider == "2")
                        {
                            rbserviceprovider.SelectedIndex = 1;

                        }
                        else if (ser_provider == "3")
                        {
                            rbserviceprovider.SelectedIndex = 2;

                        }
                        txtincident.Text = ds_psy_info.Tables[0].Rows[0]["sz_incident_description"].ToString();
                        txthistory.Text = ds_psy_info.Tables[0].Rows[0]["sz_preexisting_psy"].ToString();
                        string referall = ds_psy_info.Tables[0].Rows[0]["bt_referal_for"].ToString();
                        if (referall == "1")
                        {
                            rdbreferral.SelectedIndex = 0;
                            //txtcondition.Enabled = false;
                            //txtTreatment.Enabled = false;
                            //chkTreatment.Enabled = false;
                        }
                        else if (referall == "2")
                        {
                            rdbreferral.SelectedIndex = 1;
                            //txtEvaluation.Enabled = false;


                        }
                        else if (referall == "3")
                        {
                            rdbreferral.SelectedIndex = 2;

                        }

                        txtEvaluation.Text = ds_psy_info.Tables[0].Rows[0]["sz_evalution"].ToString();
                        txtcondition.Text = ds_psy_info.Tables[0].Rows[0]["sz_patient_condition"].ToString();
                        string chktreatmentplan = ds_psy_info.Tables[0].Rows[0]["bt_authentication_req"].ToString();
                        if (chktreatmentplan == "1")
                        {
                            chkTreatment.Checked = true;

                        }
                        else if (chktreatmentplan == "0")
                        {
                            chkTreatment.Checked = false;

                        }
                        txtTreatment.Text = ds_psy_info.Tables[0].Rows[0]["sz_authentication_req"].ToString();


                        txtVisitDate.Text = ds_psy_info.Tables[0].Rows[0]["dt_dateof_visited"].ToString();
                        txtFirstVisitDate.Text = ds_psy_info.Tables[0].Rows[0]["dt_first_dateof_visit"].ToString();
                        string patientseen = ds_psy_info.Tables[0].Rows[0]["bt_will_patient_see_again"].ToString();
                        if (patientseen == "1")
                        {
                            rdnpatientseen.SelectedIndex = 0;
                        }
                        else if (patientseen == "2")
                        {
                            rdnpatientseen.SelectedIndex = 1;

                        }
                        else if (patientseen == "3")
                        {
                            rdnpatientseen.SelectedIndex = 2;

                        }
                        txtPatientSeen.Text = ds_psy_info.Tables[0].Rows[0]["dt_yes_seen"].ToString();
                        string attendoctor = ds_psy_info.Tables[0].Rows[0]["i_no_seen"].ToString();
                        if (attendoctor == "1")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 0;
                        }
                        else if (attendoctor == "2")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 1;

                        }
                        else if (attendoctor == "3")
                        {
                            rbpatientAttendingDoctor.SelectedIndex = 2;

                        }
                        string patientworking = ds_psy_info.Tables[0].Rows[0]["i_is_patient_working"].ToString();
                        if (patientworking == "1")
                        {
                            rbPatientWorking.SelectedIndex = 0;
                        }
                        else if (patientworking == "2")
                        {
                            rbPatientWorking.SelectedIndex = 1;

                        }
                        else if (patientworking == "3")
                        {
                            rbPatientWorking.SelectedIndex = 2;

                        }
                        txtlimitedwork.Text = ds_psy_info.Tables[0].Rows[0]["sz_yes_patient_working"].ToString();
                        txtregularwork.Text = ds_psy_info.Tables[0].Rows[0]["sz_patient_regular_work"].ToString();
                        string sustained = ds_psy_info.Tables[0].Rows[0]["i_sustained"].ToString();
                        if (sustained == "1")
                        {
                            rbsustained.SelectedIndex = 0;
                        }
                        else if (sustained == "2")
                        {
                            rbsustained.SelectedIndex = 1;

                        }
                        else if (sustained == "3")
                        {
                            rbsustained.SelectedIndex = 2;

                        }
                        txtadditionalinfo.Text = ds_psy_info.Tables[0].Rows[0]["sz_additional_info"].ToString();
                        string vfbl = ds_psy_info.Tables[0].Rows[0]["i_vfbl_or_vawbl"].ToString();
                        if (vfbl == "1")
                        {
                            rbvfblorvawbl.SelectedIndex = 0;
                        }
                        else if (vfbl == "2")
                        {
                            rbvfblorvawbl.SelectedIndex = 1;

                        }

                        txtdateof_forth_history.Text = ds_psy_info.Tables[0].Rows[0]["dt_historyof_injury"].ToString();
                        txtPatientAccNo.Text = ds_psy_info.Tables[0].Rows[0]["sz_patient_acc_number"].ToString();



                    }
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    private void SaveData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //rdbreferral
        try
        {
            Bill_Sys_Psyinfo _objBill_Sys_Psyinfo = new Bill_Sys_Psyinfo();
            Bill_save _obj_Bill_save = new Bill_save();
            _objBill_Sys_Psyinfo.RBATTENDING = rbattenting_psy.SelectedValue;
            _objBill_Sys_Psyinfo.SERVICEPROVIDER = rbserviceprovider.SelectedValue;
            _objBill_Sys_Psyinfo.INCIDENT = txtincident.Text;
            _objBill_Sys_Psyinfo.HISTORY = txthistory.Text;
            _objBill_Sys_Psyinfo.BREFERAL = rdbreferral.SelectedValue;
            _objBill_Sys_Psyinfo.EVALUATION = txtEvaluation.Text;
            _objBill_Sys_Psyinfo.CONDITION = txtcondition.Text;
            _objBill_Sys_Psyinfo.TREATEMENTTEXT = txtTreatment.Text;
            _objBill_Sys_Psyinfo.VISITEDDATE = txtVisitDate.Text;
            _objBill_Sys_Psyinfo.FIRSTVISITEDDATE = txtFirstVisitDate.Text;
            _objBill_Sys_Psyinfo.PATIANTSEEN = rdnpatientseen.Text;
            _objBill_Sys_Psyinfo.NOPATIENTSEEN = txtPatientSeen.Text;
            _objBill_Sys_Psyinfo.PATIANTATTENDING_DOCTOR = rbpatientAttendingDoctor.SelectedValue;
            _objBill_Sys_Psyinfo.PATIANT_WORKING = rbPatientWorking.SelectedValue;
            _objBill_Sys_Psyinfo.LIMITEDWORK = txtlimitedwork.Text;
            _objBill_Sys_Psyinfo.REGULARWORK = txtregularwork.Text;
            _objBill_Sys_Psyinfo.SUSTAINED = rbsustained.SelectedValue;
            _objBill_Sys_Psyinfo.ADDITIONALINFO = txtadditionalinfo.Text;
            _objBill_Sys_Psyinfo.VLBF = rbvfblorvawbl.SelectedValue;
            _objBill_Sys_Psyinfo.DATEOFFORTH = txtdateof_forth_history.Text;
  
            _objBill_Sys_Psyinfo.CASEID = txtCaseID.Text;
            _objBill_Sys_Psyinfo.PATIANTACCNO = txtPatientAccNo.Text;
            _objBill_Sys_Psyinfo.COMPANYID = txtCompanyID.Text;
           
           
            if (chkTreatment.Checked)
            {

                _objBill_Sys_Psyinfo.CHK_TREATEMENT = "1";

            }
            else
            {
                _objBill_Sys_Psyinfo.CHK_TREATEMENT = "0";

            }
            
            _obj_Bill_save.Save_Psy_Info(_objBill_Sys_Psyinfo);
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveData();
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.PutMessage("Saved sucessfully");
            usrMessage.Show();
         
           
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
