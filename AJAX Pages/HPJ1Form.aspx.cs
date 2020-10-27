using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using gbmodel = gb.mbs.da.model;
using gbservices = gb.mbs.da.services;




public partial class AJAX_Pages_HPJ1Form : System.Web.UI.Page
{
    string conn = ConfigurationManager.AppSettings["Connection_String"].ToString();    
    gbservices.patient.form.SrvHPJ1 oSrvHPJ1 = new gbservices.patient.form.SrvHPJ1();
    gbmodel.account.Account oAccount = new gbmodel.account.Account();
    gbmodel.user.User oUser = new gbmodel.user.User();
    gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
    gbmodel.carrier.Carrier oCarrier = new gbmodel.carrier.Carrier();
    gbmodel.bill.Bill oBill = new gbmodel.bill.Bill();
    gbmodel.specialty.Specialty oSpecialty = new gbmodel.specialty.Specialty();
    gbmodel.patient.form.HPJ1 oHPJ1 = new gbmodel.patient.form.HPJ1();

    protected void Page_Load(object sender, EventArgs e)
    {

        oUser.ID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
        oAccount.ID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();

        if (oUser.ID == null || oUser.ID == "")
        {
            usrMsg.PutMessage("Session Expired Please Re-Login");
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMsg.Show();

        }
        else
        {
            Load();
        }

        if (oAccount.ID == null || oAccount.ID == "")
        {
            usrMsg.PutMessage("Session Expired Please Re-Login");
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMsg.Show();

        }
        else
        {
            Load();
        }

        //Cancel();


    }
    private void Load()
    {
        try
        {
            if (Request.QueryString["caseid"] != null)
            {
                oPatient.CaseID = Convert.ToInt32(Request.QueryString["caseid"].ToString());
                oBill.Number = Request.QueryString["billnumber"].ToString();
                oSpecialty.Name = Request.QueryString["speciality"].ToString();
                oPatient.CaseNo = Convert.ToInt32(Request.QueryString["caseno"].ToString());
                oPatient.ID = oSrvHPJ1.PatientId(oPatient, oAccount);

            }

            if (!IsPostBack)
            {
                IntialLoad();
                Cancel();

            }
        }
        catch (Exception ex)
        {
            usrMsg.PutMessage(ex.ToString());
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMsg.Show();

        }
    }
    private void IntialLoad()
    {

        DataSet CarrierInfo = oSrvHPJ1.Select(oPatient, oAccount);
        DataSet hpj1Info = oSrvHPJ1.select_hpj1_data(oAccount, oPatient);
        // check existing data is available or not
        if (hpj1Info.Tables[0].Rows.Count > 0)
        {
            txtProviderName.Text = hpj1Info.Tables[0].Rows[0]["sz_provider"].ToString();
            txtProvAddress.Text = hpj1Info.Tables[0].Rows[0]["sz_provider_addrs"].ToString();
            txtProvCity.Text = hpj1Info.Tables[0].Rows[0]["sz_provider_city"].ToString();
            txtProvState.Text = hpj1Info.Tables[0].Rows[0]["sz_provider_state"].ToString();
            txtProvZip.Text = hpj1Info.Tables[0].Rows[0]["sz_provider_zip"].ToString();
            txtInsuredId.Text = hpj1Info.Tables[0].Rows[0]["sz_id_number"].ToString();
            txtInjuredOccured.Text = hpj1Info.Tables[0].Rows[0]["sz_injured_occured"].ToString();
            txtPhyCompDate.Text = hpj1Info.Tables[0].Rows[0]["dt_phy_comp_date"].ToString();
            txtAllOthCompDate.Text = hpj1Info.Tables[0].Rows[0]["dt_all_oth_comp_date"].ToString();
            txtWCBAuthNo.Text = hpj1Info.Tables[0].Rows[0]["sz_wcb_auth_number"].ToString();

            txtEmp.Text = hpj1Info.Tables[0].Rows[0]["sz_employer"].ToString();
            txtCarrName.Text = hpj1Info.Tables[0].Rows[0]["sz_insurnce"].ToString();
            txtCarrAddress.Text = hpj1Info.Tables[0].Rows[0]["sz_insurnce_addrs"].ToString();
            txtCarrCity.Text = hpj1Info.Tables[0].Rows[0]["sz_insurnce_city"].ToString();
            txtCarrState.Text = hpj1Info.Tables[0].Rows[0]["sz_insurance_state"].ToString();
            txtCarrZip.Text = hpj1Info.Tables[0].Rows[0]["sz_insurnce_zip"].ToString();
            txtAccidentDate.Text = hpj1Info.Tables[0].Rows[0]["dt_doa"].ToString();
            txtWCBCaseNo.Text = hpj1Info.Tables[0].Rows[0]["sz_wcb_case_number"].ToString();
            txtCarrCaseNo.Text = hpj1Info.Tables[0].Rows[0]["sz_insurnce_case_number"].ToString();

            txtEmptyProvider.Text = hpj1Info.Tables[0].Rows[0]["sz_provider_empty"].ToString();
            txtEmptyCarr.Text = hpj1Info.Tables[0].Rows[0]["sz_insurance_empty"].ToString();

            txtWSigntext.Text = hpj1Info.Tables[0].Rows[0]["WSignText"].ToString();
            txtWSigntext2.Text = hpj1Info.Tables[0].Rows[0]["WSignText2"].ToString();
            txtStateText.Text = hpj1Info.Tables[0].Rows[0]["StateText"].ToString();
            txtSSText.Text = hpj1Info.Tables[0].Rows[0]["SSText"].ToString();
            txtCountryOf.Text = hpj1Info.Tables[0].Rows[0]["CountryOf"].ToString();
            txtBeing.Text = hpj1Info.Tables[0].Rows[0]["BeingText"].ToString();
            txtHeisthe.Text = hpj1Info.Tables[0].Rows[0]["HeistheText"].ToString();
            txtDay.Text = hpj1Info.Tables[0].Rows[0]["DayText"].ToString();
            txtDayOf.Text = hpj1Info.Tables[0].Rows[0]["DayOffText"].ToString();


        }
        else
        {
            if (CarrierInfo.Tables[0].Rows.Count > 0)
            {

                txtEmp.Text = CarrierInfo.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                txtCarrName.Text = CarrierInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                txtCarrAddress.Text = CarrierInfo.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                txtCarrCity.Text = CarrierInfo.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                txtCarrState.Text = CarrierInfo.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                txtCarrZip.Text = CarrierInfo.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                txtAccidentDate.Text = CarrierInfo.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
                txtWCBCaseNo.Text = CarrierInfo.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                txtCarrCaseNo.Text = CarrierInfo.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
            }
        }

    }

    private void Save()
    {
        try
        {
            gbmodel.patient.form.HPJ1 oHP = new gbmodel.patient.form.HPJ1();
            oHP.Patient = new gbmodel.patient.Patient();
            oHP.Patient.Account = new gbmodel.account.Account();
            oHP.User = new gbmodel.user.User();
            oHP.Carrier = new gbmodel.carrier.Carrier();
            oHP.Employer = new gbmodel.employer.Employer();
            oHP.Carrier.Address = new gbmodel.common.Address();
            oHP.Provider = new gbmodel.provider.Provider();


            oHP.Patient.Account.ID = oAccount.ID;
            oHP.Patient.CaseID = oPatient.CaseID;
            oHP.User.ID = oUser.ID;
            oHP.Patient.WCBNumber = txtWCBCaseNo.Text.Trim();
            oHP.WCBAuthNumber = txtWCBAuthNo.Text.Trim();
            oHP.Carrier.CaseNumber = txtCarrCaseNo.Text.Trim();
            oHP.InsuredIdNumber = txtInsuredId.Text.Trim();
            oHP.InjuredOccured = txtInjuredOccured.Text.Trim();
            oHP.Employer.Name = txtEmp.Text.Trim();
            oHP.Patient.DOA = txtAccidentDate.Text.Trim();
            oHP.Carrier.Id = "";
            oHP.Carrier.Name = txtCarrName.Text.Trim();
            oHP.Carrier.Address.AddressLines = txtCarrAddress.Text.Trim();
            oHP.Carrier.Address.City = txtCarrCity.Text.Trim();
            oHP.Carrier.Address.State = txtCarrState.Text.Trim();
            oHP.Carrier.Address.Zip = txtCarrZip.Text.Trim();
            oHP.Provider.Id = "";
            oHP.Provider.Name = txtProviderName.Text.Trim();
            oHP.ProviderAddress = txtProvAddress.Text.Trim();
            oHP.ProviderCity = txtProvCity.Text.Trim();
            oHP.ProviderState = txtProvState.Text.Trim();
            oHP.ProviderZip = txtProvZip.Text.Trim();
            oHP.PhyCompDate = txtPhyCompDate.Text.Trim();
            oHP.AllOthCompDate = txtAllOthCompDate.Text.Trim();
            oHP.CarrierEmpty = txtEmptyCarr.Text.Trim();
            oHP.ProviderEmpty = txtEmptyProvider.Text.Trim();

            oHP.WSignText = txtWSigntext.Text.Trim();
            oHP.WSignText2 = txtWSigntext2.Text.Trim();
            oHP.StateText = txtStateText.Text.Trim();
            oHP.SSText = txtSSText.Text.Trim();
            oHP.CountryOf = txtCountryOf.Text.Trim();
            oHP.BeingText = txtBeing.Text.Trim();
            oHP.HeistheText = txtHeisthe.Text.Trim();
            oHP.DayText = txtDay.Text.Trim();
            oHP.DayOffText = txtDayOf.Text.Trim();

            oSrvHPJ1.Create(oHP);
            usrMsg.PutMessage("Your changes were made successfully to the server");
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMsg.Show();
        }
        catch (Exception ex)
        {
            usrMsg.PutMessage(ex.ToString());
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMsg.Show();
        }
    }

    public void Cancel()
    {
        txtWCBCaseNo.Text = "";
        txtWCBAuthNo.Text = "";
        txtAccidentDate.Text = "";
        txtCarrCaseNo.Text = "";
        txtInsuredId.Text = "";
        txtInjuredOccured.Text = "";
        this.txtProviderName.Text = "";
        txtEmptyProvider.Text = "";
        txtProvAddress.Text = "";
        txtProvCity.Text = "";
        txtProvState.Text = "";
        txtProvZip.Text = "";
        txtEmp.Text = "";
        txtCarrName.Text = "";
        txtEmptyCarr.Text = "";
        txtCarrAddress.Text = "";
        txtCarrCity.Text = "";
        txtCarrState.Text = "";
        txtCarrZip.Text = "";
        txtWSigntext.Text = "";
        txtPhyCompDate.Text = "";
        txtWSigntext2.Text = "";
        txtAllOthCompDate.Text = "";
        txtStateText.Text = "";
        txtSSText.Text = "";
        txtCountryOf.Text = "";
        txtBeing.Text = "";
        txtHeisthe.Text = "";
        txtDay.Text = "";
        txtDayOf.Text = "";
    }

    private void Print()
    {
        try
        {
            gbservices.patient.form.SrvHPJ1 o_srvHPJ1 = new gbservices.patient.form.SrvHPJ1();
            string sOpenPath = "";
            string sFileName = getFileName(oPatient);
            sOpenPath = o_srvHPJ1.Print(sFileName, oAccount, oPatient, oSpecialty);
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", "window.open('" + sOpenPath + "');", true);

        }
        catch (Exception ex)
        {
            usrMsg.PutMessage(ex.ToString());
            usrMsg.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMsg.Show();
        }
    }

    private string getFileName(gbmodel.patient.Patient p_oPatient)
    {
        String sFileName = "";
        sFileName = "HPJ_1" + "_" + oBill.Number + "_" + p_oPatient.CaseID + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf";
        return sFileName;
    }

    protected void btnTopSave_Click(object sender, EventArgs e)
    {
        Save();
    }
    protected void btnBottomSave_Click(object sender, EventArgs e)
    {
        Save();
    }
    protected void btnTopPrint_Click(object sender, EventArgs e)
    {
        Print();
    }
    protected void btnBottomPrint_Click(object sender, EventArgs e)
    {
        Print();
    }

    protected void btnBottomCancle_Click(object sender, EventArgs e)
    {
        Cancel();
    }

    protected void btnTopCancle_Click(object sender, EventArgs e)
    {
        Cancel();
    }
}