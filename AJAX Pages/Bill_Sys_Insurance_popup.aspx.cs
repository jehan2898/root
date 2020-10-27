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

public partial class AJAX_Pages_Bill_Sys_Insurance_popup : PageBase
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
            ajAutoIns.ContextKey = Request.QueryString["caseid"].ToString();
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCoSignedby.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlInsuranceCompany.Flag_ID = Request.QueryString["caseid"].ToString();
                intialLoad(sender, e);
                Session["Refresh"] = "1";
                chkrefreshreport.Checked = true;
                ChkOC.Attributes.Add("onclick", "return SetDOADate();");
                extddlLocation.Flag_ID = txtCompanyID.Text;
                //ddlPayemntType.Attributes.Add("onchange", "return Validate();");


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

    protected void intialLoad(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szinsurnacecompanyname = Request.QueryString["insurancecompanyname"].ToString();
        if (szinsurnacecompanyname != null && szinsurnacecompanyname != "")
        {
            szinsurnacecompanyname = szinsurnacecompanyname.Replace("@sq@", "'");
        }
        string szinsurnaceid = Request.QueryString["insuranceid"].ToString();
        string szinsurnaceaddid = Request.QueryString["insuranceaddid"].ToString();
        string eventid = Request.QueryString["eventid"].ToString();
        string caseid = Request.QueryString["caseid"].ToString();
        string szclaimno = Request.QueryString["claimno"].ToString();
        string szpolicyno = Request.QueryString["policyno"].ToString();
        string szcasetype = Request.QueryString["casetype"].ToString();
        string szpolicyholder = Request.QueryString["policyholder"].ToString();
        string dateofaccident = Request.QueryString["dateofaccident"].ToString();
        string szpatientphone = Request.QueryString["patientPhone"].ToString();
        string szlocation = Request.QueryString["locationname"].ToString();
        string szEventProcID = Request.QueryString["EventProcID"].ToString();
        txtInsuranceid.Text = szinsurnaceid;
        txtInsuranceaddid.Text = szinsurnaceaddid;
        txteventid.Text = eventid;
        txtCaseID.Text = caseid;
        txtclaimno.Text = szclaimno;
        txtpoliccyno.Text = szpolicyno;
        txtInsuranceCompany.Text = szinsurnacecompanyname;
        extddlCaseType.Text = szcasetype;
        txtpolicyholder.Text = szpolicyholder;
        txtPatientPhoneNo.Text = szpatientphone.Trim();
        extddlLocation.Text = szlocation;
        txtEventPRocID.Text = szEventProcID;

        if (szcasetype == "CT000000000000000120")
        {
            extddlCoSignedby.Enabled = true;
        }

        DataSet ds = new DataSet();
        Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        ds = _bill_Sys_ProcedureCode_BO.UpdateCoSignedBy(Convert.ToInt32(szEventProcID), extddlCoSignedby.Text, "GET");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                extddlCoSignedby.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }

        if (dateofaccident.Trim() == "OC")
        {
            txtdateofaccident.Text = "";
            ChkOC.Checked = true;
            txtdateofaccident.Enabled = false;
            imgbtnATAccidentDate.Enabled = false;
        }
        else
        {
            if (dateofaccident.ToLower().Trim() != "missing")
            {
                txtdateofaccident.Text = dateofaccident;
            }
            else
            {
                txtdateofaccident.Text = "";
            }
        }
        //hdninsurancecode.Value = szinsurnaceid;
        if (txtInsuranceCompany.Text != "")
        {
            if (szinsurnaceid != "0")
            {
                try
                {
                    ClearInsurancecontrol();
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(szinsurnaceid);
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                    {
                        if (lstInsuranceCompanyAddress.Items[i].Value == szinsurnaceaddid)
                        {
                            lstInsuranceCompanyAddress.SelectedIndex = i;
                            lstInsuranceCompanyAddress_SelectedIndexChanged(sender, e);
                            break;
                        }

                    }
                    Page.MaintainScrollPositionOnPostBack = true;

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
                lstInsuranceCompanyAddress.Items.Clear();
                hdninsurancecode.Value = "";
            }
            btnremove.Enabled = true;
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsCity.Text = "";
            txtInsState.Text = "";
            txtInsZip.Text = "";
            txtInsPhone.Text = "";
            txtInsFax.Text = "";
            hdninsurancecode.Value = "";
            Page.MaintainScrollPositionOnPostBack = true;
            btnremove.Enabled = false;
        }


        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;
        string szCSS;
        string _url = "";
        if (Request.RawUrl.IndexOf("?") > 0)
        {
            _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        }
        else
        {
            _url = Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            szCSS = "css/main-ff.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder();
        b.AppendLine("");
        if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");

    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearInsurancecontrol();
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
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
    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceaddid.Text = lstInsuranceCompanyAddress.SelectedValue;
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsCity.Text = _arraylist[3].ToString();
            txtInsState.Text = _arraylist[4].ToString();
            txtInsZip.Text = _arraylist[5].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            Page.MaintainScrollPositionOnPostBack = true;

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
    private void ClearInsurancecontrol()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtInsuranceAddress.Text = "";
            txtInsCity.Text = "";
            txtInsState.Text = "";
            txtInsZip.Text = "";
            txtInsPhone.Text = "";
            txtInsFax.Text = "";

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
    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strInsuranceID = hdninsurancecode.Value;
        txtInsuranceid.Text = hdninsurancecode.Value;
        if (txtInsuranceCompany.Text != "")
        {
            if (strInsuranceID != "0")
            {
                try
                {
                    ClearInsurancecontrol();
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(txtInsuranceid.Text);
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;


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
                lstInsuranceCompanyAddress.Items.Clear();
                hdninsurancecode.Value = "";
            }
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceAddress.Text = "";
            txtInsCity.Text = "";
            txtInsState.Text = "";
            txtInsZip.Text = "";
            txtInsPhone.Text = "";
            txtInsFax.Text = "";
            hdninsurancecode.Value = "";
            txtInsuranceid.Text = "";
            Page.MaintainScrollPositionOnPostBack = true;
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
        try
        {
            btnremove.Enabled = true;
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            if (ChkOC.Checked)
            {
                _bill_Sys_PatientBO.UpdateInsuranceInfoeventforOC(txtCompanyID.Text, txtInsuranceid.Text, txtInsuranceaddid.Text, txteventid.Text, txtCaseID.Text, txtclaimno.Text, txtpoliccyno.Text, extddlCaseType.Text, txtpolicyholder.Text, txtdateofaccident.Text, txtPatientPhoneNo.Text, "1", extddlLocation.Text);
            }
            else
            {
                _bill_Sys_PatientBO.UpdateInsuranceInfoevent(txtCompanyID.Text, txtInsuranceid.Text, txtInsuranceaddid.Text, txteventid.Text, txtCaseID.Text, txtclaimno.Text, txtpoliccyno.Text, extddlCaseType.Text, txtpolicyholder.Text, txtdateofaccident.Text, txtPatientPhoneNo.Text, extddlLocation.Text);
            }

            if (extddlCoSignedby.Text != "NA")
            {
                DataSet dsMandetoryInfo = new DataSet();
                DataSet dsDoctorInfo = new DataSet();
                Bill_Sys_BillTransaction_BO _Bill_Sys_BillTransaction_BO = new Bill_Sys_BillTransaction_BO();
                dsMandetoryInfo = _Bill_Sys_BillTransaction_BO.GetReferringDoctorMandetoryInfo(txtCompanyID.Text, extddlCaseType.Text);
                dsDoctorInfo = _Bill_Sys_BillTransaction_BO.GetReadingDoctorInformation(txtCompanyID.Text, extddlCoSignedby.Text);

                if (dsDoctorInfo.Tables.Count > 0 && dsMandetoryInfo.Tables.Count > 0)
                {
                    if (dsDoctorInfo.Tables[0].Rows.Count > 0 && dsMandetoryInfo.Tables[0].Rows.Count > 0)
                    {
                        if (dsDoctorInfo != null && dsMandetoryInfo != null)
                        {
                            string szNPI, szLicenceNo, szWCBAuth, szWCBRating = "";
                            bool flag = false;
                            szNPI = dsMandetoryInfo.Tables[0].Rows[0][0].ToString();
                            szLicenceNo = dsMandetoryInfo.Tables[0].Rows[0][1].ToString();
                            szWCBAuth = dsMandetoryInfo.Tables[0].Rows[0][2].ToString();
                            szWCBRating = dsMandetoryInfo.Tables[0].Rows[0][3].ToString();

                            if (szNPI.ToLower() == "true")
                            {
                                if (dsDoctorInfo.Tables[0].Rows[0]["SZ_NPI"].ToString() == "" || dsDoctorInfo.Tables[0].Rows[0]["SZ_NPI"].ToString() == "&nbsp" || dsDoctorInfo.Tables[0].Rows[0]["SZ_NPI"].ToString() == null)
                                {
                                    flag = true;
                                }
                            }
                            if (szLicenceNo.ToLower() == "true")
                            {
                                if (dsDoctorInfo.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == "" || dsDoctorInfo.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == "&nbsp" || dsDoctorInfo.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == null)
                                {
                                    flag = true;
                                }
                            }
                            if (szWCBAuth.ToLower() == "true")
                            {
                                if (dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == "" || dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == "&nbsp" || dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == null)
                                {
                                    flag = true;
                                }
                            }
                            if (szWCBRating.ToLower() == "true")
                            {
                                if (dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == "" || dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == "&nbsp" || dsDoctorInfo.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == null)
                                {
                                    flag = true;
                                }
                            }

                            if (flag == true)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Co-Signed doctor does not have mandetory fields.');", true);
                            }
                            else
                            {
                                Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.UpdateCoSignedBy(Convert.ToInt32(txtEventPRocID.Text), extddlCoSignedby.Text, "UPDATE");
                                usrMessage1.PutMessage("Update Successfully");
                                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                                usrMessage1.Show();
                            }
                        }
                    }
                }//table count
            }
            else
            {
                Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                _bill_Sys_ProcedureCode_BO.UpdateCoSignedBy(Convert.ToInt32(txtEventPRocID.Text), "", "UPDATE");
                usrMessage1.PutMessage("Update Successfully");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();

            }
            //grdAllReports.XGridBindSearch();

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
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            _bill_Sys_PatientBO.RemoveInsuranceInfoevent(txteventid.Text, txtCompanyID.Text);
            usrMessage1.PutMessage("Remove Successfully");
            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage1.Show();
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceCompany.Text = "";
            txtclaimno.Text = "";
            txtpoliccyno.Text = "";
            txtInsCity.Text = "";
            txtInsState.Text = "";
            txtInsZip.Text = "";
            txtInsPhone.Text = "";
            txtInsFax.Text = "";
            hdninsurancecode.Value = "";
            Page.MaintainScrollPositionOnPostBack = true;

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
    protected void OnCheckedChanged_reportrefesh(object sender, EventArgs e)
    {
        if (chkrefreshreport.Checked)
        {
            Session["Refresh"] = "1";

        }
        else
        {
            Session["Refresh"] = "0";
        }

    }

    protected void extddlCaseType_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlCaseType.Selected_Text == "Workers Comp")
            {
                extddlCoSignedby.Enabled = true;
            }
            else
                extddlCoSignedby.Enabled = false;

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
