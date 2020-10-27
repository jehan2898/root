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

public partial class UserControl_Bill_Sys_InsCnt : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlInsuranceCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
           // txtPatientId.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                txtPatientId.Text = _caseDetailsBO.GetCasePatientID(txtCaseID.Text, "");

                Bill_Sys_PatientDeskList objGetVisitInfo = new Bill_Sys_PatientDeskList();
                DataSet dsVisitInfo = new DataSet();
                if (txtCaseID.Text.Trim() != "" && txtEventID.Text.Trim() != "")
                {
                    dsVisitInfo=objGetVisitInfo.GetVisitInsInfo(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtEventID.Text, "EVENT");
                   
                }
                else  if (txtCaseID.Text != "" &&   txtEventID.Text.Trim() == "")
                {
                  dsVisitInfo=  objGetVisitInfo.GetVisitInsInfo(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtEventID.Text, "CASE");
                }
                if (dsVisitInfo.Tables.Count > 0)
                {
                    if (dsVisitInfo.Tables[0].Rows.Count>0)
                    {
                        txtPolicyHolder.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                        txtPolicyNumber.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_POLICY_NO"].ToString();
                        txtClaimNumber.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                        txtdateofaccident.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_DOA"].ToString();
                        extddlCaseType.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                        hdninsurancecode.Value = dsVisitInfo.Tables[0].Rows[0]["SZ_INS_ID"].ToString();
                        txtInsuranceCompany.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();

                        Bill_Sys_PatientBO  _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                        //  lstInsuranceCompanyAddress.Items.Clear();
                        //notuse  lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text).Tables[0];
                        lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value).Tables[0];
                        lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                        lstInsuranceCompanyAddress.DataValueField = "CODE";
                        lstInsuranceCompanyAddress.DataBind();
                        lstInsuranceCompanyAddress.Text = dsVisitInfo.Tables[0].Rows[0]["SZ_INS_ADD_ID"].ToString();
                        lstInsuranceCompanyAddress_SelectedIndexChanged(sender, e);
                    }
                }
        }
    }

    protected void txtInsuranceCompany_TextChanged(object sender, EventArgs args)
    {
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        string strInsuranceID = hdninsurancecode.Value;
        int Index = 0;
        if (txtInsuranceCompany.Text != "")
        {
            if (strInsuranceID != "0" && strInsuranceID != "")
            {
                try
                {
                    ClearInsurancecontrol();

                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(strInsuranceID);
                    //
                    DataSet dslstInsuranceCompanyAddress = new DataSet();
                    dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
                    {
                        lstInsuranceCompanyAddress.Items.Clear();
                    }
                    else
                    {
                        lstInsuranceCompanyAddress.DataBind();
                        for (int i = 0; i < dslstInsuranceCompanyAddress.Tables[0].Rows.Count; i++)
                        {
                            if (dslstInsuranceCompanyAddress.Tables[0].Rows[i][2].ToString().Equals("1"))
                            {
                                Index = i;
                            }
                        }
                        lstInsuranceCompanyAddress.SelectedIndex = Index;
                        ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[Index][0].ToString());
                        txtInsuranceAddress.Text = _arraylist[2].ToString();
                        txtInsuranceCity.Text = _arraylist[3].ToString();
                        txtInsuranceState.Text = _arraylist[4].ToString();
                        //extddlInsuranceState.Text = _arraylist[4].ToString();
                        txtInsuranceZip.Text = _arraylist[5].ToString();
                        txtInsuranceStreet.Text = _arraylist[6].ToString();
                        if (_arraylist[8].ToString() != "")
                            //extddlInsuranceState.Text = _arraylist[8].ToString();
                            txtInsPhone.Text = _arraylist[10].ToString();
                        Page.MaintainScrollPositionOnPostBack = true;
                    }
                    lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    lstInsuranceCompanyAddress.DataValueField = "CODE";
                    lstInsuranceCompanyAddress.DataBind();
                    Page.MaintainScrollPositionOnPostBack = true;
                    if (lstInsuranceCompanyAddress.Items.Count == 1)
                    {
                        ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                        txtInsuranceAddress.Text = _arraylist[2].ToString();
                        txtInsuranceCity.Text = _arraylist[3].ToString();
                        txtInsuranceState.Text = _arraylist[4].ToString();
                        //extddlInsuranceState.Text = _arraylist[4].ToString();
                        txtInsuranceZip.Text = _arraylist[5].ToString();
                        txtInsuranceStreet.Text = _arraylist[6].ToString();
                        if (_arraylist[8].ToString() != "")
                            //extddlInsuranceState.Text = _arraylist[8].ToString();
                            Page.MaintainScrollPositionOnPostBack = true;
                    }

                    ArrayList _arraylistnew = new ArrayList();
                    lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(hdninsurancecode.Value);
                    if (_arraylistnew.Count == 0)
                    {
                    }
                    else
                    {
                        txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                        for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                        {
                            if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                            {
                                lstInsuranceCompanyAddress.SelectedIndex = i;
                                break;
                            }
                        }
                        txtInsuranceCity.Text = _arraylistnew[2].ToString();
                        txtInsuranceZip.Text = _arraylistnew[3].ToString();
                        txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                        txtInsuranceState.Text = _arraylistnew[6].ToString();
                        //extddlInsuranceState.Text = _arraylistnew[5].ToString();
                        Page.MaintainScrollPositionOnPostBack = true;
                    }
                }
                catch (Exception ex)
                {
                    //usrMessage.PutMessage(ex.ToString());
                    //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    //usrMessage.Show();
                }
            }
            else
            {
                lstInsuranceCompanyAddress.Items.Clear();
                txtInsuranceAddress.Text = "";
                txtInsuranceCity.Text = "";
                txtInsuranceState.Text = "";
                //extddlInsuranceState.Text = "";
                txtInsuranceZip.Text = "";
                txtInsuranceStreet.Text = "";
                txtInsPhone.Text = "";
                hdninsurancecode.Value = "";
            }
        }
        else
        {
            lstInsuranceCompanyAddress.Items.Clear();
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceState.Text = "";
            //extddlInsuranceState.Text = "";
            txtInsuranceZip.Text = "";
            txtInsuranceStreet.Text = "";
            txtInsPhone.Text = "";
            hdninsurancecode.Value = "";
        }
    }

    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearInsurancecontrol();
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            DataSet dslstInsuranceCompanyAddress = new DataSet();
            dslstInsuranceCompanyAddress = (DataSet)lstInsuranceCompanyAddress.DataSource;
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            if (dslstInsuranceCompanyAddress.Tables[0].Rows.Count == 0)
            {
                lstInsuranceCompanyAddress.Items.Clear();
            }
            else
            {
                lstInsuranceCompanyAddress.DataBind();
                lstInsuranceCompanyAddress.SelectedIndex = 0;
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                //extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    //extddlInsuranceState.Text = _arraylist[8].ToString();
                    txtInsPhone.Text = _arraylist[10].ToString();
                Page.MaintainScrollPositionOnPostBack = true;
            }
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
            if (lstInsuranceCompanyAddress.Items.Count == 1)
            {
                ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(dslstInsuranceCompanyAddress.Tables[0].Rows[0][0].ToString());
                txtInsuranceAddress.Text = _arraylist[2].ToString();
                txtInsuranceCity.Text = _arraylist[3].ToString();
                txtInsuranceState.Text = _arraylist[4].ToString();
                //extddlInsuranceState.Text = _arraylist[4].ToString();
                txtInsuranceZip.Text = _arraylist[5].ToString();
                txtInsuranceStreet.Text = _arraylist[6].ToString();
                if (_arraylist[8].ToString() != "")
                    //extddlInsuranceState.Text = _arraylist[8].ToString();
                    Page.MaintainScrollPositionOnPostBack = true;
            }
            ArrayList _arraylistnew = new ArrayList();
            _arraylistnew = _bill_Sys_PatientBO.GetDefaultDetailnew(extddlInsuranceCompany.Text);
            if (_arraylistnew.Count == 0)
            {

            }
            else
            {
                txtInsuranceAddress.Text = _arraylistnew[1].ToString();
                for (int i = 0; i < lstInsuranceCompanyAddress.Items.Count; i++)
                {
                    if (lstInsuranceCompanyAddress.Items[i].Value == _arraylistnew[0].ToString())
                    {
                        lstInsuranceCompanyAddress.SelectedIndex = i;
                        break;
                    }
                }
                txtInsuranceCity.Text = _arraylistnew[2].ToString();
                txtInsuranceZip.Text = _arraylistnew[3].ToString();
                txtInsuranceStreet.Text = _arraylistnew[4].ToString();
                txtInsuranceState.Text = _arraylistnew[6].ToString();
                //extddlInsuranceState.Text = _arraylistnew[5].ToString();
                Page.MaintainScrollPositionOnPostBack = true;
            }
        }
        catch (Exception ex)
        {
            //usrMessage.PutMessage(ex.ToString());
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            //usrMessage.Show();
        }
    }

    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList _arraylist = _bill_Sys_PatientBO.GetInsuranceAddressDetail(lstInsuranceCompanyAddress.SelectedValue);
            txtInsuranceAddress.Text = _arraylist[2].ToString();
            txtInsuranceCity.Text = _arraylist[3].ToString();
            txtInsuranceState.Text = _arraylist[4].ToString();
            txtInsuranceZip.Text = _arraylist[5].ToString();
            txtInsuranceStreet.Text = _arraylist[6].ToString();
            txtInsFax.Text = _arraylist[9].ToString();
            txtInsPhone.Text = _arraylist[10].ToString();
            //  txtInsContactPerson.Text = _arraylist[11].ToString();

            Page.MaintainScrollPositionOnPostBack = true;
        }
        catch (Exception ex)
        {
            //usrMessage.PutMessage(ex.ToString());
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            //usrMessage.Show();
        }
    }

    private void ClearInsurancecontrol()
    {
        try
        {
            //txtInsuranceAddressCD.Text = "";
            //txtInsuranceCityCD.Text = "";
            txtInsuranceState.Text = "";
            //txtInsuranceZipCD.Text = "";
            //txtInsuranceStreetCD.Text = "";
            ////extddlInsuranceStateNew.Text = "";
            //IDDefault.Checked = false; ;
            txtInsuranceAddress.Text = "";
            txtInsuranceCity.Text = "";
            txtInsuranceZip.Text = "";
        }
        catch (Exception ex)
        {
            //usrMessage.PutMessage(ex.ToString());
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            //usrMessage.Show();
        }
    }
}
