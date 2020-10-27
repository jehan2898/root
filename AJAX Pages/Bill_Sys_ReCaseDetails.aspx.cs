/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_CaseDetails.aspx.cs
/*Purpose              :       associate case
/*Author               :       Manoj c
/*Date of creation     :       17 Dec 2008  
/*Modified By          :       prashant z
/*Modified Date        :       3 may 2010
/************************************************************/



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
using NOTES_OBJECT;
using NotesComponent;
using System.IO;
using System.Text.RegularExpressions;
using log4net;
using System.Data.SqlClient;
using PDFValueReplacement;
using iTextSharp.text.pdf;
using iTextSharp;
using iTextSharp.text.html;
using iTextSharp.text;
using Reminders;

public partial class Bill_Sys_ReCaseDetails : PageBase
{
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    SqlConnection sqlCon;
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Billing_Sys_ManageNotesBO _manageNotesBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private ArrayList _arrayList;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private NotesOperation _notesOperation;
    public string caseID = "";
    Patient_TVBO _patient_tvbo;
    private string associatecaseno = "";
    private string associtecasenoAllow = ""; // concanate allow case
    private string associatecasenoNotAllow = "";// concanate caseno for  different address
    private string associatecasenoUpdate = ""; //only update sourcepath but all destinati path case same need
    private string associatecasenoNull = ""; // concanate  all source and destination null
    Boolean updateFlag = false;
    Regex commonrange = new Regex("[^0-9)]");

    private static ILog log = LogManager.GetLogger("Bill_Sys_Casedetails");

    public void adjuster()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearAdjusterControl();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet set = this._bill_Sys_PatientBO.GetAdjusterDetail(this.extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "FORTEST");
            if (set.Tables[0].Rows.Count > 0)
            {
                this.txtAdjusterPhone.Text = set.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                this.txtAdjusterExtension.Text = set.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                this.txtfax.Text = set.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                this.txtEmail.Text = set.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex=2;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public bool allowAssociate()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string text = this.txtAssociateCases.Text;
        bool flag = false;
        int index = 0;
        string str2 = "";
        string str4 = "";
        if (!this.commonrange.IsMatch(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString()))
        {
            str4 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        }
        else
        {
            str4 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString().Remove(0, 2);
        }
        try
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new FormatException();
            }
            if (text.IndexOf(',') == -1)
            {
                text = text + ",";
            }
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                string text1 = this.lstInsuranceCompanyAddress.Text;
            }
            string[] szCase = text.Split(new char[] { ',' });
            if (!szCase[1].ToString().Equals(""))
            {
                this.updateFlag = false;
                if (!this.CheckUpdate(szCase, 0))
                {
                    return false;
                }
            }
            this.updateFlag = true;
            for (index = 0; index < szCase.Length; index++)
            {
                if (szCase[index] != "")
                {
                    Regex regex = new Regex("[^0-9]");
                    if (regex.IsMatch(szCase[index]))
                    {
                        str2 = szCase[index].Remove(0, 2);
                    }
                    else
                    {
                        str2 = szCase[index].ToString();
                    }
                    Patient_TVBO t_tvbo = new Patient_TVBO();
                    switch (t_tvbo.SavetoSaveToAllowed(str2, this.txtCompanyID.Text, str4, "InsAddress"))
                    {
                        case "Same":
                            this.associtecasenoAllow = this.associtecasenoAllow + str2.ToString() + ",";
                            flag = true;
                            break;

                        case "Update":
                            if (this.CheckUpdate(szCase, index))
                            {
                                t_tvbo.UpdatetoSaveToAllowed(str2.ToString(), this.txtCompanyID.Text, str4, "InsAddressUpdate");
                                this.associtecasenoAllow = this.associtecasenoAllow + str2.ToString() + ",";
                                this.LoadDataOnPage();
                                return true;
                            }
                            return false;

                        case "NotAllowed":
                            return false;

                        case "null":
                            this.associtecasenoAllow = this.associtecasenoAllow + str2.ToString() + ",";
                            this.associatecasenoNull = this.associatecasenoNull + szCase[index].ToString() + ",";
                            flag = false;
                            break;
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
            string elmahstr2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + elmahstr2);
        }
        return flag;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void BindSuppliesGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdAssignSupplies.DataSource = new CaseDetailsBO().GetCaseSupplies(this.txtCaseID.Text, this.txtCompanyID.Text);
            this.grdAssignSupplies.DataBind();
            for (int i = 0; i < this.grdAssignSupplies.Items.Count; i++)
            {
                if (this.grdAssignSupplies.Items[i].Cells[1].Text == "1")
                {
                    CheckBox box = (CheckBox)this.grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                    box.Checked = true;
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddAttorney_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            string str = "";
            //if (this.chkDefaultFirm.Checked)
            //{
            //    str = "1";
            //}
            //else
            //{
            //    str = "0";
            //}
            this._patient_tvbo.SaveAttorneyinfo("", this.txtAttorneyFirstName.Text, this.txtAttorneyLastName.Text, this.txtboxAttorneyCity.Text, this.extddlState.Selected_Text.ToString(), this.txtboxAttorneyZip.Text, this.txtAttorneyPhoneNo.Text, this.txtboxAttorneyFax.Text, this.txtAttorneyEmailID.Text, this.txtCompanyID.Text, this.extddlState.Text.ToString(), this.txtboxAttorneyAddress.Text, this.extddlAtttype.Text);
            this.extddlAttorneyAssign.Flag_ID=this.txtCompanyID.Text.ToString();
            this.usrMessage3.PutMessage("Attorney Saved Successfully");
            this.usrMessage3.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage3.Show();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        base.Response.Redirect(base.Request.Url.AbsoluteUri);
        this.tabcontainerPatientEntry.ActiveTabIndex=4;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnadjcall_Click(object sender, EventArgs e)
    {
        if (this.Session["attoenyset"] != null)
        {
            string str = this.Session["attoenyset"].ToString();
            this.extddlAdjuster.Text=str;
            this.Session["attoenyset"] = null;
        }
        else
        {
            this.extddlAdjuster.Text="NA";
        }
        this.adjuster();
    }

    protected void btnAssignSupplies_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CaseDetailsBO sbo = new CaseDetailsBO();
            sbo.DeleteCaseSupplies(this.txtCaseID.Text, this.txtCompanyID.Text);
            for (int i = 0; i < this.grdAssignSupplies.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdAssignSupplies.Items[i].FindControl("chkAssignSupplies");
                if (box.Checked)
                {
                    sbo.InsertCaseSupplies(this.grdAssignSupplies.Items[i].Cells[2].Text, this.txtCaseID.Text, this.txtCompanyID.Text);
                }
            }
            this.BindSuppliesGrid();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.allowAssociate())
        {
            this.CopyToCase();
            CaseDetailsBO sbo = new CaseDetailsBO();
            string caseID = "";
            try
            {
                if (this.txtAssociateCases.Text != "")
                {
                    string[] strArray = this.associtecasenoAllow.Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        sbo.SaveAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, strArray[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");
                        for (int j = i + 1; j < strArray.Length; j++)
                        {
                            caseID = sbo.GetCaseID(strArray[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            if (caseID.Equals(""))
                            {
                                caseID = sbo.GetCaseIDEmpty(strArray[i].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            }
                            sbo.SaveAssociateCases(caseID, strArray[j].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "ADD");
                            caseID = "";
                        }
                    }
                }
                this.GetAssociateCases();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }

        }
        else
        {
            this.usrMessage.PutMessage("Associate case not Allowed");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            this.usrMessage.Show();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnattcall_Click(object sender, EventArgs e)
    {
        this.setattorney();
        this.tabcontainerPatientEntry.ActiveTabIndex=1;
    }

    protected void btnAttorneyAssign_Click(object sender, EventArgs e)
    {
        this._patient_tvbo = new Patient_TVBO();
        string userIdForAttornyUser = "";
        string str2 = "";
        if (this.chkAttorneyAssign.Checked)
        {
            str2 = "1";
        }
        else
        {
            str2 = "0";
        }
        if (this.chkAttorneyAssign.Checked)
        {
            userIdForAttornyUser = this._patient_tvbo.GetUserIdForAttornyUser(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.extddlAttorneyAssign.Text);
            this._patient_tvbo.SaveAttorneyUser(userIdForAttornyUser, this.extddlAttorneyAssign.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtCaseID.Text, str2);
            if (userIdForAttornyUser == "")
            {
                this.Page.RegisterStartupScript("mm", "<script language='javascript'>alert('There is no user account added for the selected attorney. Contact your administrator to add or associate a user account to the selected attorney');</script>");
            }
        }
        else
        {
            this._patient_tvbo.SaveAttorneyUser(userIdForAttornyUser, this.extddlAttorneyAssign.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.txtCaseID.Text, str2);
        }
        this.grdAttorney.XGridBindSearch();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearControl();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDAssociate_Click(object sender, EventArgs e)
    {
        
        string text = this.txtAssociateCases.Text;
        string str2 = null;
        string str3 = "";
        bool flag = false;
        string str4 = "";
        string input = "";
        try
        {
            CaseDetailsBO sbo = new CaseDetailsBO();
            DataSet set = new DataSet();
            set = sbo.GetAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");
            if (string.IsNullOrEmpty(text))
            {
                throw new FormatException();
            }
            if (text.IndexOf(',') == -1)
            {
                text = text + ",";
            }
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                string text1 = this.lstInsuranceCompanyAddress.Text;
            }
            string[] strArray = text.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    if (strArray[i].ToString().Equals(set.Tables[0].Rows[j]["SZ_CASE_NO"].ToString()))
                    {
                        flag = true;
                        Regex regex = new Regex("[^0-9]");
                        if (regex.IsMatch(strArray[i]))
                        {
                            str4 = strArray[i].Remove(0, 2);
                        }
                        else
                        {
                            str4 = strArray[i].ToString();
                        }
                        input = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
                        if (regex.IsMatch(input))
                        {
                            input = input.Remove(0, 2);
                        }
                    }
                    if (flag)
                    {
                        new Patient_TVBO().UpdatetoSaveToAllowed(str4, this.txtCompanyID.Text, input, "Delete");
                    }
                }
                if (!flag)
                {
                    str3 = str3 + strArray[i].ToString() + ",";
                }
                else
                {
                    flag = false;
                }
            }
            this.GetAssociateCases();
        }
        catch (FormatException)
        {
            str2 = "Please enter DeAssociate Case No.";
            this.usrMessage.PutMessage(str2);
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
            this.usrMessage.Show();
        }
        finally
        {
            if (string.IsNullOrEmpty(str2))
            {
                if (str3.Equals("") || str3.Equals(","))
                {
                    str2 = "The  case no’s successfully DeAssociate.";
                    this.usrMessage.PutMessage(str2);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    str2 = str3 + " not DeAssociate";
                    this.usrMessage.PutMessage(str2);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                    this.usrMessage.Show();
                }
            }
        }
    }

    protected void btndeleteAtt_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        string str = "";
        string str2 = "";
        string str3 = "";
        try
        {
            for (int i = 0; i < this.grdAttorney.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdAttorney.Rows[i].FindControl("chkDeleteAtt");
                if (box.Checked)
                {
                    str = this.grdAttorney.DataKeys[i]["SZ_ATTORNEY_ID"].ToString();
                    str2 = this.grdAttorney.DataKeys[i]["SZ_COMPANY_ID"].ToString();
                    str3 = this.grdAttorney.DataKeys[i]["ID"].ToString();
                    this._patient_tvbo.DeleteCaseAttorny(str, str2, str3);
                }
            }
            this.grdAttorney.XGridBindSearch();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string erriorMessage = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + erriorMessage);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.LoadNoteGrid();
    }

    protected void btnPatientUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            if (this._associateDiagnosisCodeBO.GetCaseTypeName(this.extddlCaseType.Text) == "WC000000000000000001")
            {
                this.txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                this.txtAssociateDiagnosisCode.Text = "0";
            }
            bool flag = false;
            if (this.txtChartNo.Visible && !this.txtChartNo.Text.Equals(""))
            {
                flag = true;
                this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                Label label = (Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                if (!label.Text.Equals(this.txtChartNo.Text))
                {
                    string str2 = "CHART";
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        str2 = "REF";
                    }
                    if (this._bill_Sys_PatientBO.ExistChartNumber(this.txtCompanyID.Text, this.txtChartNo.Text, str2))
                    {
                        this.usrMessage.PutMessage(this.txtChartNo.Text + "  chart no is already exist ...!");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage);
                        this.usrMessage.Show();
                        return;
                    }
                    this.UpdatePatientInformation();
                }
                else
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                this.UpdatePatientInformation();
            }
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this._bill_Sys_PatientBO.UpdateTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString(), this.chkStatusProc.Checked ? 1 : 0, this.txtNF2Date.Text);
            this.Page.MaintainScrollPositionOnPostBack = false;
            this.CheckTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString());
            this.LoadDataOnPage();
            this.usrMessage.PutMessage("Patient Information Updated successfully !");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            this.Session["AttornyID"] = this.extddlAttorney.Text;
            this.SaveNotes();
            this.LoadNoteGrid();
            this.UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (!this.btassociate.Checked && !this.associatecaseno.Equals(""))
            {
                this.UpdateCopyToCase(this.associatecaseno);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._saveOperation = new SaveOperation();
        try
        {
            this._saveOperation.WebPage=this.Page;
            this._saveOperation.Xml_File="notes.xml";
            this._saveOperation.SaveMethod();
            this.ClearControl();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearchInsCompany_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.hdninsurancecode.Value = new CaseDetailsBO().SearchInsuranceCompany(this.txtSearchName.Text, this.txtSearchCode.Text, this.txtCompanyID.Text);
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex=2;
            this.txtSearchName.Text = "";
            this.txtSearchCode.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdateAdjuster_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            this.txtAdjusterid.Text = this.extddlAdjuster.Text;
            string text = this.txtAdjusterid.Text;
            this.MessageControl2.PutMessage("Adjuster Updated Successfully");
            this.MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.MessageControl2.Show();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        base.Response.Redirect(base.Request.Url.AbsoluteUri);
        this.tabcontainerPatientEntry.ActiveTabIndex=2;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdateAttorney_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._patient_tvbo = new Patient_TVBO();
        try
        {
            string str = "";
            //if (this.chkDefaultFirmAtt.Checked)
            //{
            //    str = "1";
            //}
            //else
            //{
            //    str = "0";
            //}
            this._patient_tvbo.UpdateAttornyInfo(this.txtAttorneyid.Text, this.txtAttFirstName.Text, this.txtAttLastName.Text, this.txtAttCity.Text, this.extddlstateAtt.Selected_Text, this.txtAttZip.Text, this.txtAttPhoneNo.Text, this.txtAttFax.Text, this.txtAttEmailID.Text, this.txtCompanyID.Text, this.extddlstateAtt.Text, this.txtAttAddress.Text, this.extddlAttorneyType.Text);
            this.extddlAttorneyAssign.Flag_ID=this.txtCompanyID.Text.ToString();
            this.grdAttorney.XGridBind();
            this.usrMessage1.PutMessage("Attorney Updated Successfully");
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage1.Show();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        base.Response.Redirect(base.Request.Url.AbsoluteUri);

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void CheckTemplateStatus(string p_szCaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataTable templateStatus = this._bill_Sys_PatientBO.GetTemplateStatus(p_szCaseID);
            if (Convert.ToBoolean(templateStatus.Rows[0].ItemArray.GetValue(0)))
            {
                this.chkStatusProc.Checked = true;
                this.txtNF2Date.Text = templateStatus.Rows[0].ItemArray.GetValue(1).ToString();
            }
            else
            {
                this.chkStatusProc.Checked = false;
                this.txtNF2Date.Text = "";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public bool CheckUpdate(string[] szCase, int j)
    {
        bool flag = true;
        string str = "";
        string str2 = "";
        Regex regex = new Regex("[[A-z]");
        if (regex.IsMatch(szCase[0]) && !szCase[1].Equals(""))
        {
            string str3 = szCase[1];
            szCase[1] = szCase[0];
            szCase[0] = str3;
        }
        for (int i = j + 1; i < szCase.Length; i++)
        {
            if (szCase[i] != "")
            {
                Regex regex2 = new Regex("[^0-9]");
                if (regex2.IsMatch(szCase[i]))
                {
                    str = szCase[i].Remove(0, 2);
                }
                else
                {
                    str = szCase[i].ToString();
                }
                if (regex2.IsMatch(szCase[j]))
                {
                    str2 = szCase[j].Remove(0, 2);
                }
                else
                {
                    str2 = szCase[j].ToString();
                }
                Patient_TVBO t_tvbo = new Patient_TVBO();
                switch (t_tvbo.SavetoSaveToAllowed(str, this.txtCompanyID.Text, str2, "InsAddress"))
                {
                    case "Same":
                        if (this.updateFlag)
                        {
                            flag = true;
                            this.associtecasenoAllow = this.associtecasenoAllow + str + ",";
                        }
                        break;

                    case "NotAllowed":
                        return false;
                }
            }
        }
        return flag;
    }

    private void ClearAdjusterControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtAdjusterExtension.Text = "";
            this.txtAdjusterPhone.Text = "";
            this.txtfax.Text = "";
            this.txtEmail.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            this.LoadNoteGrid();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ClearControlAttorney()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtboxAttorneyAddress.Text = "";
            this.txtboxAttorneyCity.Text = "";
            this.txtAttorneyEmailID.Text = "";
            this.txtboxAttorneyFax.Text = "";
            this.txtAttorneyFirstName.Text = "";
            this.txtAttorneyLastName.Text = "";
            this.txtAttorneyPhoneNo.Text = "";
            this.extddlState.Text="NA";
            this.extddlAtttype.Text="NA";
            this.txtboxAttorneyZip.Text = "";
            this.btnAddAttorney.Enabled = true;
            this.chkDefaultFirm.Checked = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
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
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
            this.txtInsuranceStreet.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ClearPatientAccidentControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtATAccidentDate.Text = "";
            this.txtATAddress.Text = "";
            this.txtATCity.Text = "";
            this.txtATReportNumber.Text = "";
            this.txtATAdditionalPatients.Text = "";
            this.extddlATAccidentState.Text="NA";
            this.txtATHospitalName.Text = "";
            this.txtATHospitalAddress.Text = "";
            this.txtATDescribeInjury.Text = "";
            this.txtATAdmissionDate.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void CopyToCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string associtecasenoAllow = this.associtecasenoAllow;
        string str2 = null;
        try
        {
            if (string.IsNullOrEmpty(associtecasenoAllow))
            {
                throw new FormatException();
            }
            if (associtecasenoAllow.IndexOf(',') == -1)
            {
                associtecasenoAllow = associtecasenoAllow + ",";
            }
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                string text = this.lstInsuranceCompanyAddress.Text;
            }
            string[] strArray = associtecasenoAllow.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                Patient_TVBO t_tvbo = new Patient_TVBO();
                string str3 = "";
                if (!this.commonrange.IsMatch(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString()))
                {
                    str3 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
                }
                else
                {
                    str3 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString().Remove(0, 2);
                }
                t_tvbo.UpdatetoSaveToAllowed(strArray[i].ToString(), this.txtCompanyID.Text, str3, "InsAddressUpdate");
            }
            this.LoadDataOnPage();
        }
        catch (Exception ex)
        {
            str2 = "Please enter Copy to Case No.";
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string elmahstr2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + elmahstr2);
        }
        finally
        {
            if (string.IsNullOrEmpty(str2))
            {
                if (this.associatecasenoNotAllow.Equals(""))
                {
                    str2 = "The Insurance Company and Address copied to case no’s successfully.";
                    this.usrMessage.PutMessage(str2);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    str2 = this.associatecasenoNotAllow + " not allow to asscociate";
                    this.usrMessage.PutMessage(str2);
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlDataReader reader;
        string str = "";
        string str2 = "";
        string str3 = "";
        if (this.ddlInsuranceType.SelectedItem != null)
        {
            string text = this.ddlInsuranceType.SelectedItem.Text;
            if (text == "Major Medical")
            {
                str = "MAJ";
            }
            else if (text == "Private")
            {
                str = "PRI";
            }
            else
            {
                str = "SEC";
            }
        }
        else
        {
            str = "SEC";
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        string cmdText = "select SZ_INSURANCE_ID, SZ_ADDRESS_ID FROM MST_SEC_INSURANCE_DETAIL WHERE SZ_CASE_ID='" + this.txtCaseID.Text + "' AND SZ_COMPANY_ID='" + this.txtCompanyID.Text + "' AND SZ_INSURANCE_TYPE='" + str + "'";
        SqlCommand selectCommand = new SqlCommand(cmdText, this.sqlCon);
        SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
        try
        {
            this.sqlCon.Open();
            selectCommand = new SqlCommand(cmdText, this.sqlCon);
            reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader["SZ_INSURANCE_ID"] != DBNull.Value)
                {
                    str2 = reader["SZ_INSURANCE_ID"].ToString();
                }
                if (reader["SZ_ADDRESS_ID"] != DBNull.Value)
                {
                    str3 = reader["SZ_ADDRESS_ID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        if (str2 != "")
        {
            string str7 = "select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='" + str2 + "'";
            try
            {
                this.sqlCon.Open();
                reader = new SqlCommand(str7, this.sqlCon).ExecuteReader();
                while (reader.Read())
                {
                    if (reader["SZ_INSURANCE_NAME"] != DBNull.Value)
                    {
                        this.txtSecInsName.Text = reader["SZ_INSURANCE_NAME"].ToString();
                    }
                }
            }
            catch (SqlException exception2)
            {
                exception2.Message.ToString();
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
        }
        else
        {
            this.txtSecInsName.Text = "";
        }
        if (str3 != "")
        {
            DataSet dataSet = null;
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                SqlCommand command2 = new SqlCommand("SP_MST_INSURANCE_ADDRESS", connection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.Add("@SZ_INS_ADDRESS_ID", str3);
                command2.Parameters.Add("@FLAG", "LIST");
                adapter = new SqlDataAdapter(command2);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    this.txtSecInsAddress.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                    this.txtInsCity.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                    this.txtInsState.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                    this.txtInsZip.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    this.txtSecInsPhone.Text = dataSet.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                    this.txtSecInsFax.Text = dataSet.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    this.txtInsConatactPerson.Text = dataSet.Tables[0].Rows[0]["sz_contact_person"].ToString();
                }
                return;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string erriorMessage = "Error Request ID=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + erriorMessage);
                return;
            }

        }
        this.txtSecInsAddress.Text = "";
        this.txtInsCity.Text = "";
        this.txtInsState.Text = "";
        this.txtInsZip.Text = "";
        this.txtSecInsPhone.Text = "";
        this.txtSecInsFax.Text = "";
        this.txtInsConatactPerson.Text = "";

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void EnableDisableControl(bool value)
    {
        this.txtPatientFName.ReadOnly = value;
        this.txtMI.ReadOnly = value;
        this.txtPatientLName.ReadOnly = value;
        this.txtDateOfBirth.ReadOnly = value;
        this.CalendarExtender2.Enabled=!value;
        this.txtSocialSecurityNumber.ReadOnly = value;
        this.ddlSex.Enabled = !value;
        this.txtPatientAddress.ReadOnly = value;
        this.txtPatientStreet.ReadOnly = value;
        this.txtPatientCity.ReadOnly = value;
        this.txtState.ReadOnly = value;
        this.extddlPatientState.Enabled = !value;
        this.txtPatientZip.ReadOnly = value;
        this.txtPatientPhone.ReadOnly = value;
        this.txtWorkPhone.ReadOnly = value;
        this.txtExtension.ReadOnly = value;
        this.chkWrongPhone.Enabled = !value;
        this.txtPatientEmail.ReadOnly = value;
        this.extddlAttorney.Enabled = !value;
        this.extddlCaseType.Enabled = !value;
        this.chkTransportation.Enabled = !value;
        this.txtDateofAccident.ReadOnly = value;
        this.CalendarExtender3.Enabled=!value;
        this.imgbtnDateofAccident.Enabled = !value;
        this.txtPlatenumber.ReadOnly = value;
        this.txtPolicyReport.ReadOnly = value;
        this.txtEmployerName.ReadOnly = value;
        this.txtEmployerAddress.ReadOnly = value;
        this.txtEmployerCity.ReadOnly = value;
        this.txtEmployerState.ReadOnly = value;
        this.extddlEmployerState.Enabled = !value;
        this.txtEmployerZip.ReadOnly = value;
        this.txtEmployerPhone.ReadOnly = value;
        this.txtInsuranceCompany.Enabled = !value;
        this.lstInsuranceCompanyAddress.Enabled = !value;
        this.txtInsuranceAddress.ReadOnly = value;
        this.txtInsuranceCity.ReadOnly = value;
        this.txtInsuranceState.ReadOnly = value;
        this.txtInsuranceZip.ReadOnly = value;
        this.txtClaimNumber.ReadOnly = value;
        this.txtWCBNumber.ReadOnly = value;
        this.extddlAdjuster.Enabled = !value;
        this.btnPatientUpdate.Visible = !value;
        this.btnPatientClear.Visible = !value;
        this.extddlCaseStatus.Enabled = !value;
        this.txtATAccidentDate.ReadOnly = value;
        this.calATAccidentDate.Enabled=!value;
        this.txtATReportNumber.ReadOnly = value;
        this.txtATPlateNumber.ReadOnly = value;
        this.txtATAddress.ReadOnly = value;
        this.txtATCity.ReadOnly = value;
        this.extddlATAccidentState.Enabled = !value;
        this.txtATHospitalName.ReadOnly = value;
        this.txtATHospitalAddress.ReadOnly = value;
        this.txtATAdmissionDate.ReadOnly = value;
        this.calATAdmissionDate.Enabled=!value;
        this.txtATAdditionalPatients.ReadOnly = value;
        this.txtATDescribeInjury.ReadOnly = value;
        this.txtDateofFirstTreatment.ReadOnly = value;
        this.CalendarExtender1.Enabled=!value;
        this.txtPolicyHolder.ReadOnly = value;
        this.txtPolicyNumber.ReadOnly = value;
        this.txtAssociateCases.ReadOnly = value;
        this.txtChartNo.ReadOnly = value;
    }

    protected void extddlAdjuster_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearAdjusterControl();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet set = this._bill_Sys_PatientBO.GetAdjusterDetail(this.extddlAdjuster.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "FORTEST");
            if (set.Tables[0].Rows.Count > 0)
            {
                this.hdadjusterCode.Value = this.extddlAdjuster.Text;
                this.txtAddress.Text = set.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
                this.txtCity.Text = set.Tables[0].Rows[0]["SZ_CITY"].ToString();
                this.txtZip.Text = set.Tables[0].Rows[0]["SZ_ZIP"].ToString();
                this.txtAdjusterState.Text = set.Tables[0].Rows[0]["SZ_STATE"].ToString();
                this.txtAdjusterPhone.Text = set.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                this.txtAdjusterExtension.Text = set.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                this.txtfax.Text = set.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                this.txtEmail.Text = set.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex=2;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlAttorney_selectedIndex(object sender, EventArgs e)
    {
        string str = this.extddlAttorney.Text;
        this.hdnattorneycode.Value = str;
        if (str != "NA")
        {
            DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(str);
            if (attornyInfo.Tables[0].Rows.Count > 0)
            {
                this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_city"].ToString();
                this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_zip"].ToString();
                this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_state"].ToString();
                this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_Address"].ToString();
            }
            else
            {
                this.txtattorneycity.Text = "";
                this.txtattorneyzip.Text = "";
                this.txtattorneState.Text = "";
                this.txtattorneyaddress.Text = "";
            }
        }
        this.tabcontainerPatientEntry.ActiveTabIndex=1;
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
            this.ClearInsurancecontrol();
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.extddlInsuranceCompany.Text);
            this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            this.lstInsuranceCompanyAddress.DataValueField = "CODE";
            this.lstInsuranceCompanyAddress.DataBind();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex=2;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected string GeneratePDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        GeneratePatientInfoPDF opdf = new GeneratePatientInfoPDF();
        string str = "";
        try
        {
            string str2 = File.ReadAllText(ConfigurationManager.AppSettings["PATIENT_INFO_HTML"]);
            str2 = opdf.getReplacedString(str2, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            SautinSoft.PdfMetamorphosis metamorphosis = new SautinSoft.PdfMetamorphosis();
            metamorphosis.Serial="10007706603";
            string str3 = this.getFileName("P") + ".htm";
            str = this.getFileName("P") + ".pdf";
            StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str3);
            writer.Write(str2);
            writer.Close();
            metamorphosis.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + str3, ConfigurationManager.AppSettings["EXCEL_SHEET"] + str);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        return str;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getApplicationSetting(string p_szKey)
    {
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        connection.Open();
        string str = "";
        SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", connection).ExecuteReader();
        while (reader.Read())
        {
            str = reader["parametervalue"].ToString();
        }
        return str;
    }

    public void GetAssociateCases()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CaseDetailsBO sbo = new CaseDetailsBO();
        this.divAssociateCaseID.Controls.Clear();
        try
        {
            DataSet set = new DataSet();
            set = sbo.GetAssociateCases(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "LIST");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                LinkButton child = new LinkButton();
                child.ID = "lnk" + set.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                child.Text = set.Tables[0].Rows[i]["NAME"].ToString() + " ";
                child.CssClass = "lbl";
                child.CommandArgument = set.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                child.Command += new CommandEventHandler(this.OnClick);
                this.divAssociateCaseID.Controls.Add(child);
                this.associatecaseno = this.associatecaseno + set.Tables[0].Rows[i]["SZ_CASE_NO"].ToString() + ",";
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string GetCompanyName(string szCompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug("Get Company Name;");
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string str2 = "";
        try
        {
            connection.Open();
            log.Debug("GET_COMPANY_NAME @SZ_COMPANY_ID='" + szCompanyId + "'");
            SqlCommand selectCommand = new SqlCommand("GET_COMPANY_NAME", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            str2 = dataSet.Tables[0].Rows[0][0].ToString();
            log.Debug("Company Name : " + dataSet.Tables[0].Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string erriorMessage = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + erriorMessage);

        }

        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        log.Debug("Return Company Name : " + str2);
        return str2;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return (p_szBillNumber + "_" + this.getRandomNumber() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
        if (Directory.Exists(p_szPath))
        {
            if (!Directory.Exists(p_szPath + "/Packet Document"))
            {
                Directory.CreateDirectory(p_szPath + "/Packet Document");
            }
        }
        else
        {
            Directory.CreateDirectory(p_szPath);
            Directory.CreateDirectory(p_szPath + "/Packet Document");
        }
        return (p_szPath + "/Packet Document/");
    }

    private void GetPatientDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SqlCommand command;
            SqlDataReader reader;
            this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            DataSet patientInfo = this._bill_Sys_PatientBO.GetPatientInfo(this.txtPatientID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (patientInfo.Tables[0].Rows.Count > 0)
            {
                this.DtlView.DataSource = patientInfo;
                this.DtlView.DataBind();
                if (patientInfo.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "&nbsp;")
                {
                    this.txtPatientFName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                }
                if (patientInfo.Tables[0].Rows[0]["sz_source_company_name"].ToString() != "")
                {
                    this.lblwalkin.Text = patientInfo.Tables[0].Rows[0]["sz_source_company_name"].ToString();
                }
                this.txtPatientLName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                this.txtPatientAge.Text = patientInfo.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();
                this.txtPatientAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                this.txtPatientCity.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
                this.txtPatientZip.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                this.txtPatientPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                this.txtPatientEmail.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_EMAIL"].ToString();
                this.txtWorkPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                this.txtExtension.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_PHONE_EXTENSION"].ToString();

                // -- add cell Phone #
                this.txtCellNo.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_CELLNO"].ToString();

                if ((patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() == "True") && (patientInfo.Tables[0].Rows[0]["BT_WRONG_PHONE"].ToString() != ""))
                {
                    CheckBox box = (CheckBox)this.DtlView.FindControl("chkViewWrongPhone");
                    this.chkWrongPhone.Checked = true;
                    box.Checked = true;
                }
                else
                {
                    this.chkWrongPhone.Checked = false;
                }
                this.txtMI.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();
                Label label = (Label)this.DtlView.Items[0].FindControl("lblViewMiddleName");
                label.Text = patientInfo.Tables[0].Rows[0]["MI"].ToString();
                this.txtWCBNo.Text = patientInfo.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                this.txtSocialSecurityNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                if ((patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "01/01/1900") && (patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString() != "&nbsp;"))
                {
                    this.txtDateOfBirth.Text = patientInfo.Tables[0].Rows[0]["DT_DOB"].ToString();
                }
                else
                {
                    this.txtDateOfBirth.Text = "";
                }
                Label label2 = (Label)this.DtlView.Items[0].FindControl("lblViewGender");
                this.ddlSex.SelectedValue = patientInfo.Tables[0].Rows[0]["SZ_GENDER"].ToString();
                label2.Text = this.ddlSex.SelectedItem.Text;
                if ((patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "01/01/1900") && (patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString() != "&nbsp;"))
                {
                    this.txtDateOfInjury.Text = patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString();
                }
                else
                {
                    this.txtDateOfInjury.Text = "";
                }
                this.txtJobTitle.Text = patientInfo.Tables[0].Rows[0]["SZ_JOB_TITLE"].ToString();
                this.txtWorkActivites.Text = patientInfo.Tables[0].Rows[0]["SZ_WORK_ACTIVITIES"].ToString();
                this.txtState.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATE"].ToString();
                this.txtCarrierCaseNo.Text = patientInfo.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
                this.txtEmployerName.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                this.txtEmployerPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_PHONE"].ToString();
                this.txtEmployerAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString();
                this.txtEmployerCity.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString();
                this.txtEmployerState.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString();
                this.txtEmployerZip.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                if ((patientInfo.Tables[0].Rows[0]["BT_TRANSPORTATION"] == "True") && (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"] != ""))
                {
                    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("chkTransportation");
                    box2.Checked = true;
                    box2.Checked = true;
                }
                else
                {
                    this.chkTransportation.Checked = false;
                    this.chkTransportation.Checked = false;
                }
                this.extddlPatientState.Text=patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATE_ID"].ToString();
                Label label3 = (Label)this.DtlView.Items[0].FindControl("lblViewPatientState");
                if ((this.extddlPatientState.Text != "NA") && (this.extddlPatientState.Text != ""))
                {
                    label3.Text = this.extddlPatientState.Selected_Text;
                }
                this.extddlEmployerState.Text=patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE_ID"].ToString();
                Label label4 = (Label)this.DtlView.Items[0].FindControl("lblViewEmployerState");
                if ((this.extddlEmployerState.Text != "NA") && (this.extddlEmployerState.Text != ""))
                {
                    label4.Text = this.extddlEmployerState.Selected_Text;
                }
                this.txtChartNo.Text = patientInfo.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();
                Label label5 = (Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                label5.Text = patientInfo.Tables[0].Rows[0]["I_RFO_CHART_NO"].ToString();
                if ((patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "01/01/1900") && (patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString() != "&nbsp;"))
                {
                    this.txtDateofFirstTreatment.Text = patientInfo.Tables[0].Rows[0]["DT_FIRST_TREATMENT"].ToString();
                }
                else
                {
                    this.txtDateofFirstTreatment.Text = "";
                }
            }
            this.ClearPatientAccidentControl();
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString() != "&nbsp;")
            {
                this.txtAccidentID.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_INFO_ID"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString() != "&nbsp;")
            {
                this.txtPlatenumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;")
            {
                this.txtAccidentAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;")
            {
                this.txtAccidentCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;")
            {
                this.txtAccidentState.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString() != "&nbsp;")
            {
                this.txtPolicyReport.Text = patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString() != "&nbsp;")
            {
                this.txtListOfPatient.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900"))
            {
                this.txtDateofAccident.Text = patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            else
            {
                this.txtDateofAccident.Text = "";
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString() != "&nbsp;")
            {
                string str = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_TYPE"].ToString();
                foreach (System.Web.UI.WebControls.ListItem item in this.rdolstPatientType.Items)
                {
                    if (item.Value.ToString() == str)
                    {
                        item.Selected = true;
                        break;
                    }
                }
                foreach (System.Web.UI.WebControls.ListItem item2 in this.rdolstPatientType.Items)
                {
                    if (item2.Selected)
                    {
                        Label label6 = (Label)this.DtlView.Items[0].FindControl("lblPatientType");
                        label6.Text = item2.Text.ToString();
                    }
                }
            }

            if (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_SPECIALTY"].ToString() != "&nbsp;")
            {
                string str = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_SPECIALTY"].ToString();
                this.txtAccidentSpecialty.Text = str;
                Label label_spec = (Label)this.DtlView.Items[0].FindControl("lblAccidentSpecialty");
                label_spec.Text = str;

            }
            Label label7 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
            label7.Text = patientInfo.Tables[0].Rows[0]["sz_location_Name"].ToString();
            if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString() != ""))
            {
                this.txtCaseID.Text = patientInfo.Tables[0].Rows[0]["SZ_CASE_ID"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != ""))
            {
                this.extddlCaseType.Text=patientInfo.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
                Label label8 = (Label)this.DtlView.Items[0].FindControl("lblViewCasetype");
                if ((this.extddlCaseType.Text != "NA") && (this.extddlCaseType.Text != ""))
                {
                    label8.Text = this.extddlCaseType.Selected_Text;
                }
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString() != ""))
            {
                this.extddlProvider.Text=patientInfo.Tables[0].Rows[0]["SZ_PROVIDER_ID"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != ""))
            {
                this.extddlInsuranceCompany.Text=patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.hdninsurancecode.Value = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
                this.txtInsuranceCompany.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                if (((this.extddlInsuranceCompany.Text != "NA") && (this.extddlInsuranceCompany.Text != "")) && ((this.txtInsuranceCompany.Text != "") && (this.txtInsuranceCompany.Text != "No suggestions found for your search")))
                {
                    this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                }
                this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value).Tables[0];
                this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                this.lstInsuranceCompanyAddress.DataBind();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString() != ""))
            {
                this.extddlCaseStatus.Text=patientInfo.Tables[0].Rows[0]["SZ_CASE_STATUS_ID"].ToString();
                Label label9 = (Label)this.DtlView.Items[0].FindControl("lblViewCaseStatus");
                if ((this.extddlCaseStatus.Text != "NA") && (this.extddlCaseStatus.Text != ""))
                {
                    label9.Text = this.extddlCaseStatus.Selected_Text;
                }
            }
            //Change added on 24/03/2015 to show the Patient relation on Load
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_RELATION"].ToString() != "&nbsp;")
            {
                string str = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_RELATION"].ToString();
                foreach (System.Web.UI.WebControls.ListItem item in this.rdlPatient_relation.Items)
                {
                    if (item.Value.ToString() == str)
                    {
                        item.Selected = true;
                        break;
                    }
                }
                foreach (System.Web.UI.WebControls.ListItem item2 in this.rdlPatient_relation.Items)
                {
                    if (item2.Selected)
                    {
                        Label labelPatientRelation = (Label)this.DtlView.Items[0].FindControl("lblPatientRelation");
                        labelPatientRelation.Text = item2.Text.ToString();
                    }
                }
            }
            if (patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATUS"].ToString() != "&nbsp;")
            {
                string str = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATUS"].ToString();
                foreach (System.Web.UI.WebControls.ListItem item in this.rdlPatient_Status.Items)
                {
                    if (item.Value.ToString() == str)
                    {
                        item.Selected = true;
                        break;
                    }
                }
                foreach (System.Web.UI.WebControls.ListItem item2 in this.rdlPatient_Status.Items)
                {
                    if (item2.Selected)
                    {
                        Label lblPatientStatus = (Label)this.DtlView.Items[0].FindControl("lblPatientStatus");
                        lblPatientStatus.Text = item2.Text.ToString();
                    }
                }
            }
            //if ((patientInfo.Tables[0].Rows[0]["BT_SINGLE"].ToString() == "True") )
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxSingle");
            //    this.ChkBoxSingle.Checked = true;
                
            //}
            //else
            //{
            //    this.ChkBoxSingle.Checked = false;
            //}
            //if ((patientInfo.Tables[0].Rows[0]["BT_MARRIED"].ToString() == "True"))
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxMarried");
            //    this.ChkBoxMarried.Checked = true;
            //}
            //else
            //{
            //    this.ChkBoxMarried.Checked = false;
            //}
            //if ((patientInfo.Tables[0].Rows[0]["BT_OTHER"].ToString() == "True"))
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxOther");
            //    this.ChkBoxOther.Checked = true;
            //}
            //else
            //{
            //    this.ChkBoxOther.Checked = false;
            //}
            //if ((patientInfo.Tables[0].Rows[0]["BT_EMPLOYEE"].ToString() == "True"))
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxEmployeed");
            //    this.ChkBoxEmployeed.Checked = true;
            //}
            //else
            //{
            //    this.ChkBoxEmployeed.Checked = false;
            //}
            //if ((patientInfo.Tables[0].Rows[0]["BT_FULLTIME"].ToString() == "True"))
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxFullTime");
            //    this.ChkBoxFullTime.Checked = true;
            //}
            //else
            //{
            //    this.ChkBoxFullTime.Checked = false;
            //}
            //if ((patientInfo.Tables[0].Rows[0]["BT_PARTTIME"].ToString() == "True"))
            //{
            //    CheckBox box2 = (CheckBox)this.DtlView.Items[0].FindControl("ChkBoxPartTime");
            //    this.ChkBoxPartTime.Checked = true;
            //}
            //else
            //{
            //    this.ChkBoxPartTime.Checked = false;
            //}
            //Change added on 24/03/2015 to show the Patient relation on Load
            if ((patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString() != ""))
            {
                this.extddlAttorney.Text=patientInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ID"].ToString();
                this.hdnattorneycode.Value = this.extddlAttorney.Text;
                DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(this.hdnattorneycode.Value.ToString());
                if (attornyInfo.Tables[0].Rows.Count > 0)
                {
                    this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ADDRESS"].ToString();
                    this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_CITY"].ToString();
                    this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["SZ_STATE_NAME"].ToString();
                    this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_ZIP"].ToString();
                    this.txtattorneyphone.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                    this.txtattorneyfax.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();
                }
                Label label10 = (Label)this.DtlView.Items[0].FindControl("lblViewAttorney");
                if ((this.extddlAttorney.Text != "NA") && (this.extddlAttorney.Text != ""))
                {
                    label10.Text = this.extddlAttorney.Selected_Text;
                }
                this.txtAttorneyCompany.Text = this.extddlAttorney.Selected_Text;
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString() != ""))
            {
                this.txtClaimNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                if (this.txtClaimNumber.Text.Equals("NA"))
                {
                    this.txtClaimNumber.Text = "";
                }
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString() != ""))
            {
                this.txtPolicyNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                if (this.txtPolicyNumber.Text.Equals("NA"))
                {
                    this.txtPolicyNumber.Text = "";
                }
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString() != ""))
            {
                this.extddlAdjuster.Text=patientInfo.Tables[0].Rows[0]["SZ_ADJUSTER_ID"].ToString();
                Label label11 = (Label)this.DtlView.Items[0].FindControl("lblViewAdjusterName");
                if ((this.extddlAdjuster.Text != "NA") && (this.extddlAdjuster.Text != ""))
                {
                    label11.Text = this.extddlAdjuster.Selected_Text;
                }
            }
            if ((patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString() != ""))
            {
                this.txtAssociateDiagnosisCode.Text = patientInfo.Tables[0].Rows[0]["BT_ASSOCIATE_DIAGNOSIS_CODE"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString() != ""))
            {
                this.txtPlatenumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NUMBER"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString() != ""))
            {
                this.txtAccidentAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString() != ""))
            {
                this.txtAccidentCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString() != ""))
            {
                this.txtAccidentState.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString() != ""))
            {
                this.txtPolicyReport.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_REPORT"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString() != ""))
            {
                this.txtListOfPatient.Text = patientInfo.Tables[0].Rows[0]["SZ_LIST_OF_PATIENTS"].ToString();
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString() != ""))
            {
                this.extddlLocation.Text=patientInfo.Tables[0].Rows[0]["SZ_LOCATION_ID"].ToString();
                Label label12 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                if ((this.extddlLocation.Text != "NA") && (this.extddlLocation.Text != ""))
                {
                    label12.Text = this.extddlLocation.Selected_Text;
                }
            }
            if (((patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString() != "")) && ((patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")))
            {
                try
                {
                    this.lstInsuranceCompanyAddress.SelectedValue = patientInfo.Tables[0].Rows[0]["SZ_INS_ADDRESS_ID"].ToString();
                    this.txtInsuranceAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                    this.txtInsuranceCity.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                    this.txtInsuranceState.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                    this.txtInsuranceZip.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    this.txtInsuranceStreet.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_STREET"].ToString();
                    this.txtInsFax.Text = patientInfo.Tables[0].Rows[0]["sz_fax_number"].ToString();
                    this.txtInsPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                    this.txtInsContactPerson.Text = patientInfo.Tables[0].Rows[0]["sz_contact_person"].ToString();
                }
                catch
                {
                }
            }
            string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
            this.sqlCon = new SqlConnection(connectionString);
            DataSet dataSet = new DataSet();
            try
            {
                string str3 = "SELECT *   FROM mst_case_type_wise_ui_access_control WHERE sz_case_type_id = '" + this.txtCaseTypeID.Text + "' and sz_company_id = '" + this.txtCompanyID.Text + "' and sz_page_name = 'Workarea' ";
                this.sqlCon.Open();
                command = new SqlCommand(str3, this.sqlCon);
                new SqlDataAdapter(command).Fill(dataSet);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    string str4 = dataSet.Tables[0].Rows[i]["sz_control_name"].ToString();
                    string str5 = dataSet.Tables[0].Rows[i]["sz_control_type"].ToString();
                    if (dataSet.Tables[0].Rows[i]["sz_access_type"].ToString() == "hidden")
                    {
                        if (str5 == "LinkButton")
                        {
                            if (this.lnkNF2Envelope.ID.ToString() == str4)
                            {
                                this.lnkNF2Envelope.Visible = false;
                            }
                            if (this.lnkGenerateNF2.ID.ToString() == str4)
                            {
                                this.lnkGenerateNF2.Visible = false;
                            }
                        }
                        if ((str5 == "CheckBox") && (this.chkStatusProc.ID.ToString() == str4))
                        {
                            this.chkStatusProc.Visible = false;
                        }
                        if ((str5 == "TextBox") && (this.txtNF2Date.ID.ToString() == str4))
                        {
                            this.txtNF2Date.Visible = false;
                        }
                        if ((str5 == "ImageButton") && (this.imgbtnNF2Date.ID.ToString() == str4))
                        {
                            this.imgbtnNF2Date.Visible = false;
                        }
                    }
                }
            }
            if (this.txtAssociateDiagnosisCode.Text == "1")
            {
                this.chkAssociateCode.Checked = true;
            }
            else
            {
                this.chkAssociateCode.Checked = false;
            }
            if ((patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString() != ""))
            {
                this.txtPolicyHolder.Text = patientInfo.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                if (this.txtPolicyHolder.Text.Equals("NA"))
                {
                    this.txtPolicyHolder.Text = "";
                }
            }
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string cmdText = "select SZ_INSURANCE_ID FROM  MST_SEC_INSURANCE_DETAIL  WHERE SZ_CASE_ID='" + this.txtCaseID.Text + "' AND SZ_COMPANY_ID='" + this.txtCompanyID.Text + "'";
            command = new SqlCommand(cmdText, this.sqlCon);
            SqlDataAdapter adapter2 = new SqlDataAdapter(command);
            try
            {
                this.sqlCon.Open();
                command = new SqlCommand(cmdText, this.sqlCon);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["SZ_INSURANCE_ID"] != DBNull.Value)
                    {
                        str7 = reader["SZ_INSURANCE_ID"].ToString();
                    }
                }
            }
            catch (SqlException exception2)
            {
                exception2.Message.ToString();
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (str7 != "")
            {
                string str11 = "select SZ_INSURANCE_NAME FROM MST_INSURANCE_COMPANY WHERE SZ_INSURANCE_ID='" + str7 + "'";
                try
                {
                    this.sqlCon.Open();
                    reader = new SqlCommand(str11, this.sqlCon).ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["SZ_INSURANCE_NAME"] != DBNull.Value)
                        {
                            this.txtSecInsName.Text = reader["SZ_INSURANCE_NAME"].ToString();
                        }
                    }
                }
                catch (SqlException exception3)
                {
                    exception3.Message.ToString();
                }
                finally
                {
                    if (this.sqlCon.State == ConnectionState.Open)
                    {
                        this.sqlCon.Close();
                    }
                }
            }
            else
            {
                this.txtSecInsName.Text = "";
            }
            string str12 = "select SZ_ADDRESS_ID, SZ_INSURANCE_TYPE from MST_SEC_INSURANCE_DETAIL where  SZ_CASE_ID='" + this.txtCaseID.Text + "' and SZ_COMPANY_ID='" + this.txtCompanyID.Text + "' and SZ_INSURANCE_ID='" + str7 + "'";
            try
            {
                this.sqlCon.Open();
                reader = new SqlCommand(str12, this.sqlCon).ExecuteReader();
                while (reader.Read())
                {
                    if (reader["SZ_ADDRESS_ID"] != DBNull.Value)
                    {
                        str8 = reader["SZ_ADDRESS_ID"].ToString();
                    }
                    if (reader["SZ_INSURANCE_TYPE"] != DBNull.Value)
                    {
                        str9 = reader["SZ_INSURANCE_TYPE"].ToString();
                    }
                }
            }
            catch (SqlException exception4)
            {
                exception4.Message.ToString();
            }
            finally
            {
                if (this.sqlCon.State == ConnectionState.Open)
                {
                    this.sqlCon.Close();
                }
            }
            if (str9 != "")
            {
                if (str9 == "SEC")
                {
                    this.ddlInsuranceType.SelectedIndex = 1;
                }
                else if (str9 == "MAJ")
                {
                    this.ddlInsuranceType.SelectedIndex = 2;
                }
                else if (str9 == "PRI")
                {
                    this.ddlInsuranceType.SelectedIndex = 3;
                }
                else
                {
                    this.ddlInsuranceType.SelectedIndex = 0;
                }
            }
            if (str8 != "")
            {
                DataSet set4 = null;
                try
                {
                    SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
                    SqlCommand selectCommand = new SqlCommand("SP_MST_INSURANCE_ADDRESS", connection);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.Add("@SZ_INS_ADDRESS_ID", str8);
                    selectCommand.Parameters.Add("@FLAG", "LIST");
                    adapter2 = new SqlDataAdapter(selectCommand);
                    set4 = new DataSet();
                    adapter2.Fill(set4);
                    connection.Close();
                    if (set4.Tables[0].Rows.Count > 0)
                    {
                        this.txtSecInsAddress.Text = set4.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                        this.txtInsCity.Text = set4.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString();
                        this.txtInsState.Text = set4.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString();
                        this.txtInsZip.Text = set4.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                        this.txtSecInsPhone.Text = set4.Tables[0].Rows[0]["SZ_INSURANCE_PHONE"].ToString();
                        this.txtSecInsFax.Text = set4.Tables[0].Rows[0]["sz_fax_number"].ToString();
                        this.txtInsConatactPerson.Text = set4.Tables[0].Rows[0]["sz_contact_person"].ToString();
                    }
                    goto Label_2AD1;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    using (Utils utility = new Utils())
                    {
                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                    }
                    string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
                    goto Label_2AD1;
                }

            }
            this.txtSecInsAddress.Text = "";
            this.txtInsCity.Text = "";
            this.txtInsState.Text = "";
            this.txtInsZip.Text = "";
            this.txtSecInsPhone.Text = "";
            this.txtSecInsFax.Text = "";
            this.txtInsConatactPerson.Text = "";
        Label_2AD1:
            this.txtAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ADDRESS"].ToString();
            this.txtCity.Text = patientInfo.Tables[0].Rows[0]["SZ_CITY"].ToString();
            this.txtAdjusterState.Text = patientInfo.Tables[0].Rows[0]["SZ_STATE"].ToString();
            this.txtZip.Text = patientInfo.Tables[0].Rows[0]["SZ_ZIP"].ToString();
            this.txtAdjusterPhone.Text = patientInfo.Tables[0].Rows[0]["SZ_PHONE"].ToString();
            this.txtAdjusterExtension.Text = patientInfo.Tables[0].Rows[0]["SZ_EXTENSION"].ToString();
            this.txtfax.Text = patientInfo.Tables[0].Rows[0]["SZ_FAX"].ToString();
            this.txtEmail.Text = patientInfo.Tables[0].Rows[0]["SZ_EMAIL"].ToString();
            this.ClearPatientAccidentControl();
            this.txtATPlateNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_PLATE_NO"].ToString();
            if (patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString() != "01/01/1900")
            {
                this.txtATAccidentDate.Text = patientInfo.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
            }
            else
            {
                this.txtATAccidentDate.Text = "";
            }
            this.txtATAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_ADDRESS"].ToString();
            this.txtATCity.Text = patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_CITY"].ToString();
            this.txtATReportNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_REPORT_NO"].ToString();
            this.txtATAdditionalPatients.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FROM_CAR"].ToString();
            this.extddlATAccidentState.Text=patientInfo.Tables[0].Rows[0]["SZ_ACCIDENT_STATE_ID"].ToString();
            Label label13 = (Label)this.DtlView.Items[0].FindControl("lblViewAccidentState");
            if ((this.extddlATAccidentState.Text != "NA") && (this.extddlATAccidentState.Text != ""))
            {
                label13.Text = this.extddlATAccidentState.Selected_Text;
            }
            this.txtATHospitalName.Text = patientInfo.Tables[0].Rows[0]["SZ_HOSPITAL_NAME"].ToString();
            this.txtATHospitalAddress.Text = patientInfo.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();
            this.txtATDescribeInjury.Text = patientInfo.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
            this.txtATAdmissionDate.Text = patientInfo.Tables[0].Rows[0]["DT_ADMISSION_DATE"].ToString();
            if ((patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != "&nbsp;") && (patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString() != ""))
            {
                Label label14 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                label14.Text = patientInfo.Tables[0].Rows[0]["sz_company_name"].ToString();
            }
            this.lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_PATIENTPOP_VIEW", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 0x2710).ToString();
    }

    public DataSet GetViewBillInfo(string caseID, string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        this.sqlCon = new SqlConnection(connectionString);
        try
        {
            this.sqlCon.Open();
            SqlCommand selectCommand = new SqlCommand("SP_CD_VIEW_BILLS", this.sqlCon);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPatientDeskList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "genpdf")
            {
                string str = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string str2 = "";
                str2 = str + this.GeneratePDF();
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str2.ToString() + "'); ", true);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
    }

    protected void hlnkPatientDesk_Click(object sender, EventArgs e)
    {
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        string companyID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.rpt_ViewBills.DataSource = this.GetViewBillInfo(caseID, companyID);
        this.rpt_ViewBills.DataBind();
        this.tblViewBills.Visible = true;
    }

    protected void lnkAddAttorney_Click(object sender, EventArgs e)
    {
        this.mp3.Show();
        this.ClearControlAttorney();
        this.tabcontainerPatientEntry.ActiveTabIndex=4;
    }

    protected void lnkAddbills_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_BillTransaction.aspx", false);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkDocumentManager_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            string str = "";
            string str2 = this.Session["PassedCaseID"].ToString();
            this.Session["QStrCaseID"] = str2;
            this.Session["Case_ID"] = str2;
            this.Session["Archived"] = "0";
            this.Session["QStrCID"] = str2;
            this.Session["SelectedID"] = str2;
            this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            this.Session["SN"] = "0";
            this.Session["LastAction"] = "vb_CaseInformation.aspx";
            str = "Document Manager/case/vb_CaseInformation.aspx";
            base.Response.Write("<script language='javascript'>window.open('" + str + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkGenerateNF2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PDFValueReplacement.PDFValueReplacement replacement = new PDFValueReplacement.PDFValueReplacement();
            string str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            string str2 = ConfigurationManager.AppSettings["NF2_PDF_FILE"];
            string str3 = ConfigurationManager.AppSettings["NF2_XML_FILE"];
            string str4 = replacement.ReplacePDFvalues(str3, str2, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
            string str5 =  ApplicationSettings.GetParameterValue("DocumentManagerURL") + str + str4;
            this.CheckTemplateStatus(this.Session["TM_SZ_CASE_ID"].ToString());
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str5 + "'); ", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkManageNotes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["Manage_Case_ID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_ManageNotes.aspx", false);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkNF2Envelope_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            PDFValueReplacement.PDFValueReplacement replacement = new PDFValueReplacement.PDFValueReplacement();
            string str = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
            string str2 = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            string str3 = ConfigurationManager.AppSettings["NF2_ENVELOPE_XML_FILE"];
            string str4 = replacement.ReplacePDFvalues(str3, str2, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
            string str5 = ApplicationSettings.GetParameterValue( "DocumentManagerURL") + str + str4;
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str5 + "'); ", true);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkNotes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_Notes.aspx", false);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkPaidBills_Click(object sender, EventArgs e)
    {
    }

    protected void lnkTemplateManager_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
            string str = "";
            str = "TemplateManager/templates.aspx";
            base.Response.Write("<script language='javascript'>window.open('" + str + "', 'AdditionalData');</script>");
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkUnpaidBills_Click(object sender, EventArgs e)
    {
    }

    protected void lnkUpdateAdu_Click(object sender, EventArgs e)
    {
        this.ModalPopupaduster.Show();
        this.txtAdjusterid.Text = this.extddlAdjuster.Text;
        string text = this.txtAdjusterid.Text;
        string str2 = this.txtCompanyID.Text;
        Patient_TVBO t_tvbo = new Patient_TVBO();
        DataSet adjusterInfoForUpdate = new DataSet();
        adjusterInfoForUpdate = t_tvbo.GetAdjusterInfoForUpdate(text, str2);
        if (adjusterInfoForUpdate.Tables[0].Rows.Count > 0)
        {
            this.txtAdjusterPopupName1.Text = adjusterInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            this.txtAdjusterPopupPhone1.Text = adjusterInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            this.txtAdjusterPopupExtension1.Text = adjusterInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            this.txtAdjusterPopupFax1.Text = adjusterInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            this.txtAdjusterPopupEmail1.Text = adjusterInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            this.txtAdjusterPopupName1.Enabled = false;
        }
        this.tabcontainerPatientEntry.ActiveTabIndex=2;
    }

    protected void lnkUpdateAtt_Click(object sender, EventArgs e)
    {
        this.mp2.Show();
        this.txtAttorneyid.Text = this.extddlAttorneyAssign.Text;
        string text = this.txtAttorneyid.Text;
        this._patient_tvbo = new Patient_TVBO();
        DataSet attornyInfoForUpdate = this._patient_tvbo.GetAttornyInfoForUpdate(text);
        if (attornyInfoForUpdate.Tables[0].Rows.Count > 0)
        {
            this.txtAttFirstName.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            this.txtAttLastName.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            this.txtAttCity.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            this.txtAttAddress.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            this.txtAttZip.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            this.txtAttPhoneNo.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            this.txtAttFax.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            this.txtAttEmailID.Text = attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            this.extddlstateAtt.Text=attornyInfoForUpdate.Tables[0].Rows[0]["SZ_ATTORNEY_STATE_ID"].ToString();
            this.extddlAttorneyType.Text=attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            if (attornyInfoForUpdate.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() == "True")
            {
                this.chkDefaultFirmAtt.Checked = true;
            }
            else
            {
                this.chkDefaultFirmAtt.Checked = false;
            }
            this.txtAttFirstName.Enabled = false;
            this.txtAttLastName.Enabled = false;
        }
        this.tabcontainerPatientEntry.ActiveTabIndex=4;
    }

    protected void lnkViewBills_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["PassedCaseID"] = this.txtCaseID.Text;
            base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadDataOnPage()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            new Bill_Sys_CaseObject().SZ_PATIENT_ID="";
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlCaseType.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            this.extddlProvider.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            this.extddlCaseStatus.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            this.extddlAttorney.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            this.extddlAdjuster.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            this.extddlInsuranceCompany.Flag_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            if (this.Session["PassedCaseID"] != null)
            {
                this.txtPatientID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID;
                this.GetPatientDetails();
                this.Session["Associate_Case_ID"] = this.Session["PassedCaseID"];
                this.txtCaseID.Text = this.Session["PassedCaseID"].ToString();
                EditOperation operation = new EditOperation();
                operation.Xml_File="CaseDetails.xml";
                operation.WebPage=this.Page;
                operation.Primary_Value=this.Session["PassedCaseID"].ToString();
                operation.LoadData();
                operation = new EditOperation();
                operation.Xml_File="CaseDetailsForLabel.xml";
                operation.WebPage=this.Page;
                operation.Primary_Value=this.Session["PassedCaseID"].ToString();
                operation.LoadData();
                this.Session["AttornyID"] = this.extddlAttorney.Text;
                this.BindSuppliesGrid();
            }
            else
            {
                base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void LoadNoteGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this.grdNotes.CurrentPageIndex = 0;
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="NoteSearch.xml";
            this._listOperation.LoadList();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
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
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            ArrayList insuranceAddressDetail = this._bill_Sys_PatientBO.GetInsuranceAddressDetail(this.lstInsuranceCompanyAddress.SelectedValue);
            this.txtInsuranceAddress.Text = insuranceAddressDetail[2].ToString();
            this.txtInsuranceCity.Text = insuranceAddressDetail[3].ToString();
            this.txtInsuranceState.Text = insuranceAddressDetail[4].ToString();
            this.txtInsuranceZip.Text = insuranceAddressDetail[5].ToString();
            this.txtInsuranceStreet.Text = insuranceAddressDetail[6].ToString();
            this.txtInsFax.Text = insuranceAddressDetail[9].ToString();
            this.txtInsPhone.Text = insuranceAddressDetail[10].ToString();
            this.txtInsContactPerson.Text = insuranceAddressDetail[11].ToString();
            this.Page.MaintainScrollPositionOnPostBack = true;
            this.tabcontainerPatientEntry.ActiveTabIndex=2;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void OnClick(object sender, CommandEventArgs e)
    {
        base.Response.Redirect("Bill_Sys_ReCaseDetails.aspx?CaseID=" + e.CommandArgument);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ajAutoIns.ContextKey=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.Ajautoattorney.ContextKey=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.Session["CASE_LIST_GO_BUTTON"] = null;
            bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
            this.con.SourceGrid=this.grdAttorney;
            this.txtSearchBox.SourceGrid=this.grdAttorney;
            this.grdAttorney.Page=this.Page;
            this.grdAttorney.PageNumberList=this.con;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlLocation.Flag_ID=this.txtCompanyID.Text.ToString();
            this.extddlAttorneyAssign.Flag_ID=this.txtCompanyID.Text.ToString();
            this.extddlAtttype.Flag_ID=this.txtCompanyID.Text.ToString();
            this.lnkGenerateNF2.Attributes.Add("onclick", "return ShowGenerateNF2Link();");
            this.lnkUpdateAtt.Attributes.Add("onclick", "return showAttorney();");
            this.btnAttorneyAssign.Attributes.Add("onclick", "return showAttorney();");
            this.btnAddAttorney.Attributes.Add("onclick", "return AttorneyAdd();");
            new PopupBO();
            this.txtCompanyIDForNotes.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnPatientUpdate.Attributes.Add("onclick", "return formValidator('frmPatient','tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientFName,tabcontainerPatientEntry:tabpnlPersonalInformation:txtPatientLName,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseType,tabcontainerPatientEntry:tabpnlPersonalInformation:extddlCaseStatus');");
            if (!base.IsPostBack)
            {
                if ((base.Request.QueryString["CaseID"] != null) && (base.Request.QueryString["CaseID"].ToString() != ""))
                {
                    CaseDetailsBO sbo = new CaseDetailsBO();
                    Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                    obj2.SZ_PATIENT_ID=sbo.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), "");
                    obj2.SZ_CASE_ID=base.Request.QueryString["CaseID"].ToString();
                    if (base.Request.QueryString["cmp"] != null)
                    {
                        obj2.SZ_COMAPNY_ID=base.Request.QueryString["cmp"].ToString();
                        obj2.SZ_CASE_NO=sbo.GetCaseNo(base.Request.QueryString["CaseID"].ToString(), base.Request.QueryString["cmp"].ToString());
                        this.Session["Company"] = base.Request.QueryString["cmp"].ToString();
                    }
                    else
                    {
                        obj2.SZ_COMAPNY_ID=this.Session["Company"].ToString();
                        obj2.SZ_CASE_NO=sbo.GetCaseNo(base.Request.QueryString["CaseID"].ToString(), this.Session["Company"].ToString());
                    }
                    obj2.SZ_PATIENT_NAME=sbo.GetPatientName(obj2.SZ_PATIENT_ID);
                    this.Session["CASE_OBJECT"] = obj2;
                }
                if (this.Session["CASE_OBJECT"] != null)
                {
                    this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this.Session["Company"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                this.LoadNoteGrid();
                if (base.Request.QueryString["CaseID"] != null)
                {
                    this.caseID = base.Request.QueryString["CaseID"].ToString();
                    this.txtCaseID.Text = this.caseID;
                    this.ShowPopupNotes(this.caseID);
                }
                this.grdAssociatedDiagnosisCode.Visible = false;
                this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                this.grdAssociatedDiagnosisCode.DataSource = this._bill_Sys_ProcedureCode_BO.GetAssociatedDiagnosisCode_List(this.caseID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).Tables[0];
                this.grdAssociatedDiagnosisCode.DataBind();
                Bill_Sys_Case @case = new Bill_Sys_Case();
                @case.SZ_CASE_ID=this.txtCaseID.Text;
                this.Session["CASEINFO"] = @case;
                this.Session["PassedCaseID"] = this.txtCaseID.Text;
                string str = this.Session["PassedCaseID"].ToString();
                this.Session["QStrCaseID"] = str;
                this.Session["Case_ID"] = str;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = str;
                this.Session["SelectedID"] = str;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                this.CheckTemplateStatus(this.txtCaseID.Text);
                this.LoadDataOnPage();
                this.UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string str3 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                this.EnableDisableControl(!str2.Equals(str3));
                this.grdAttorney.XGridBindSearch();
            }
            if (!base.IsPostBack)
            {
                ReminderBO rbo = null;
                DataSet set = null;
                string str4 = "";
                string text = "";
                rbo = new ReminderBO();
                set = new DataSet();
                str4 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                text = this.txtCaseID.Text;
                DateTime time = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (rbo.LoadReminderDetailsForCaseDeatils(str4, time, text).Tables[0].Rows.Count > 0)
                {
                    this.Page.RegisterStartupScript("ss", "<script language='javascript'> ShowReminder();</script>");
                }
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
            {
                Label label = (Label)this.DtlView.Items[0].FindControl("lblView");
                Label label2 = (Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                this.lblChart.Visible = true;
                this.txtChartNo.Visible = true;
                label2.Visible = true;
                label.Visible = true;
            }
            else
            {
                Label label3 = (Label)this.DtlView.Items[0].FindControl("lblView");
                Label label4 = (Label)this.DtlView.Items[0].FindControl("lblViewChartNo");
                this.lblChart.Visible = false;
                this.txtChartNo.Visible = false;
                label4.Visible = false;
                label3.Visible = false;
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_SHOW_PATIENT_WALK_IN_ON_WORKAREA == "1")
            {
                this.lbl.Visible = true;
                this.lblwalkin.Visible = true;
            }
            else
            {
                this.lbl.Visible = false;
                this.lblwalkin.Visible = false;
            }
            this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this.txtNoteCode.Text = Note_Code.New_Note_Added;
            this.GetAssociateCases();
            if (flag || (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                Label label5 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                Label label6 = (Label)this.DtlView.Items[0].FindControl("lblLocation");
                label5.Visible = false;
                label6.Visible = false;
                this.lblLocationddl.Visible = false;
                this.extddlLocation.Visible = false;
            }
            else
            {
                Label label7 = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
                Label label8 = (Label)this.DtlView.Items[0].FindControl("lblLocation");
                label7.Visible = true;
                label8.Visible = true;
                this.lblLocationddl.Visible = true;
                this.extddlLocation.Visible = true;
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_Copy_From.ToString() == "1")
            {
                Label label9 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrm");
                Label label10 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                label9.Visible = false;
                label10.Visible = false;
            }
            else
            {
                Label label11 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrm");
                Label label12 = (Label)this.DtlView.Items[0].FindControl("lblcopyfrom");
                label11.Visible = true;
                label12.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_ReCaseDetails.aspx");
        }
        if (this.btnPatientUpdate.Visible)
        {
            this.btnAssociate.Visible = true;
            this.btassociate.Visible = true;
            this.btnDAssociate.Visible = true;
            this.txtAssociateCases.Visible = true;
            this.lblassociate.Visible = true;
        }
        else
        {
            this.btnAssociate.Visible = false;
            this.btassociate.Visible = false;
            this.btnDAssociate.Visible = false;
            this.txtAssociateCases.Visible = false;
            this.lblassociate.Visible = false;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }

    protected void rptPatientDeskList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "genpdf")
            {
                string str = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
                string str2 = "";
                str2 = str + this.GeneratePDF();
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str2.ToString() + "'); ", true);
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SaveNotes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._notesOperation = new NotesOperation();
        try
        {
            this._notesOperation.WebPage=this.Page;
            this._notesOperation.Xml_File="InformationNotesXML.xml";
            this._notesOperation.Case_ID=this.txtCaseID.Text;
            this._notesOperation.User_ID=this.txtUserID.Text;
            this._notesOperation.Company_ID=this.txtCompanyID.Text;
            this._notesOperation.SaveNotesOperation();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void setattorney()
    {
        string str = this.extddlAttorney.Text;
        this.hdnattorneycode.Value = str;
        if (str != "NA")
        {
            DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(str);
            if (attornyInfo.Tables[0].Rows.Count > 0)
            {
                this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_city"].ToString();
                this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_zip"].ToString();
                this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_state"].ToString();
                this.txtattorneyphone.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                this.txtattorneyfax.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();
                this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_Address"].ToString();
            }
            else
            {
                this.txtattorneycity.Text = "";
                this.txtattorneyzip.Text = "";
                this.txtattorneState.Text = "";
                this.txtattorneyaddress.Text = "";
            }
        }
    }

    private void ShowPopupNotes(string szCaseid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._manageNotesBO = new Billing_Sys_ManageNotesBO();
        this._arrayList = new ArrayList();
        try
        {
            this._arrayList = this._manageNotesBO.GetPopupNotesDesc(szCaseid, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            for (int i = 0; i < this._arrayList.Count; i++)
            {
                base.Response.Write("<script language='javascript'>alert('" + this._arrayList[i].ToString() + "');</script>");
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void txtAttorneyCompany_TextChanged(object sender, EventArgs args)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = this.hdnattorney.Value;
        this.extddlAttorney.Text=str;
        this.hdnattorneycode.Value = this.extddlAttorney.Text;
        this.Session["AttornyID"] = this.extddlAttorney.Text;
        if (this.txtAttorneyCompany.Text != "")
        {
            if ((str != "0") && (str != ""))
            {
                try
                {
                    DataSet attornyInfo = new Patient_TVBO().GetAttornyInfo(str);
                    if (attornyInfo.Tables[0].Rows.Count > 0)
                    {
                        this.txtattorneycity.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_city"].ToString();
                        this.txtattorneyzip.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_zip"].ToString();
                        this.txtattorneState.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_state"].ToString();
                        this.txtattorneyphone.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_PHONE"].ToString();
                        this.txtattorneyfax.Text = attornyInfo.Tables[0].Rows[0]["SZ_ATTORNEY_FAX"].ToString();
                        this.txtattorneyaddress.Text = attornyInfo.Tables[0].Rows[0]["sz_attorney_Address"].ToString();
                    }
                    else
                    {
                        this.txtattorneycity.Text = "";
                        this.txtattorneyzip.Text = "";
                        this.txtattorneState.Text = "";
                        this.txtattorneyaddress.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    using (Utils utility = new Utils())
                    {
                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                    }
                    string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                }

            }
            else
            {
                this.txtattorneycity.Text = "";
                this.txtattorneyzip.Text = "";
                this.txtattorneState.Text = "";
                this.txtattorneyaddress.Text = "";
                this.txtattorneyphone.Text = "";
                this.txtattorneyfax.Text = "";
                this.hdnattorney.Value = "";
            }
        }
        else
        {
            this.txtattorneycity.Text = "";
            this.txtattorneyzip.Text = "";
            this.txtattorneState.Text = "";
            this.txtattorneyaddress.Text = "";
            this.txtattorneyphone.Text = "";
            this.txtattorneyfax.Text = "";
            this.hdnattorney.Value = "";
        }
        this.tabcontainerPatientEntry.ActiveTabIndex=1;

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
        string str = this.hdninsurancecode.Value;
        if (this.txtInsuranceCompany.Text != "")
        {
            if (str == "0")
            {
                this.lstInsuranceCompanyAddress.Items.Clear();
                this.hdninsurancecode.Value = "";
            }
            else
            {
                try
                {
                    this.ClearInsurancecontrol();
                    this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    this.lstInsuranceCompanyAddress.DataSource = this._bill_Sys_PatientBO.GetInsuranceCompanyAddress(this.hdninsurancecode.Value);
                    this.lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                    this.lstInsuranceCompanyAddress.DataValueField = "CODE";
                    this.lstInsuranceCompanyAddress.DataBind();
                    this.Page.MaintainScrollPositionOnPostBack = true;
                    this.tabcontainerPatientEntry.ActiveTabIndex=2;
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    using (Utils utility = new Utils())
                    {
                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                    }
                    string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                }

            }
        }
        else
        {
            this.lstInsuranceCompanyAddress.Items.Clear();
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
            this.txtInsuranceStreet.Text = "";
            this.txtInsPhone.Text = "";
            this.hdninsurancecode.Value = "";
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void UpdateCopyToCase(string associatecase)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = associatecase;
        string str2 = null;
        bool flag = false;
        try
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new FormatException();
            }
            if (str.IndexOf(',') == -1)
            {
                str = str + ",";
            }
            string text = "";
            if (!this.txtInsuranceAddress.Text.Equals(""))
            {
                text = this.lstInsuranceCompanyAddress.Text;
            }
            string[] strArray = str.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                Patient_TVBO t_tvbo = new Patient_TVBO();
                ArrayList list = new ArrayList();
                list.Add(strArray[i].ToString());
                list.Add(this.hdninsurancecode.Value);
                list.Add(text);
                list.Add(this.txtCompanyID.Text);
                list.Add(this.txtClaimNumber.Text);
                list.Add(this.txtPolicyNumber.Text);
                list.Add(this.extddlAdjuster.Text);
                list.Add(this.txtPolicyHolder.Text);
                list.Add("COPYTO");
                t_tvbo.UpdateInsurancetoCase(list);
            }
        }
        catch (Exception ex)
        {
            str2 = "";
            flag = true;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string elmahstr2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + elmahstr2);

        }
        finally
        {
            if (!flag)
            {
                str2 = "The Insurance Company and Address Updated to " + this.associatecaseno + "successfully.";
            }
            this.usrMessage.PutMessage(str2);
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void UpdateData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            this._editOperation.Primary_Value=this.txtCaseID.Text.ToString();
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="CaseMaster.xml";
            this._editOperation.UpdateMethod();
            this._editOperation.Primary_Value=this.txtCaseID.Text.ToString();
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="PatientAcccidentInfoEntry.xml";
            this._editOperation.UpdateMethod();
            this.LoadDataOnPage();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Case Details Updated Successfully ...!";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void UpdatePatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            list.Add(this.txtPatientFName.Text);
            list.Add(this.txtPatientLName.Text);
            list.Add(this.txtPatientAge.Text);
            list.Add(this.txtPatientAddress.Text);
            list.Add(this.txtPatientStreet.Text);
            list.Add(this.txtPatientCity.Text);
            list.Add(this.txtPatientZip.Text);
            list.Add(this.txtPatientPhone.Text);
            list.Add(this.txtPatientEmail.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.txtWorkPhone.Text);
            list.Add(this.txtExtension.Text);
            list.Add(this.txtMI.Text);
            list.Add(this.txtPolicyNumber.Text);
            list.Add(this.txtSocialSecurityNumber.Text);
            list.Add(this.txtDateOfBirth.Text);
            list.Add(this.ddlSex.SelectedValue);
            list.Add(this.txtDateOfInjury.Text);
            list.Add(this.txtJobTitle.Text);
            list.Add(this.txtWorkActivites.Text);
            list.Add(this.txtState.Text);
            list.Add(this.txtCarrierCaseNo.Text);
            list.Add(this.txtEmployerName.Text);
            list.Add(this.txtEmployerPhone.Text);
            list.Add(this.txtEmployerAddress.Text);
            list.Add(this.txtEmployerCity.Text);
            list.Add(this.txtEmployerState.Text);
            list.Add(this.txtEmployerZip.Text);
            Label label = (Label)this.DtlView.Items[0].FindControl("lblVLocation1");
            list.Add(label.Text);
            list.Add("UPDATE");
            list.Add(this.txtPatientID.Text);
            if (this.chkWrongPhone.Checked)
            {
                list.Add("True");
            }
            else
            {
                list.Add("False");
            }
            if (this.chkTransportation.Checked)
            {
                list.Add("True");
            }
            else
            {
                list.Add("False");
            }
            
            list.Add(this.extddlEmployerState.Text);
            list.Add(this.extddlPatientState.Text);
            list.Add(this.txtChartNo.Text);
            list.Add(this.txtDateofFirstTreatment.Text);
            //if (ChkBoxSingle.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            //if (ChkBoxMarried.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            //if (ChkBoxOther.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            //if (ChkBoxEmployeed.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            //if (ChkBoxFullTime.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            //if (ChkBoxPartTime.Checked)
            //{
            //    list.Add("True");
            //}
            //else
            //{
            //    list.Add("False");
            //}
            list.Add(rdlPatient_Status.SelectedValue.ToString());
            list.Add(rdlPatient_relation.SelectedValue.ToString());

            list.Add(txtCellNo.Text);//Add Cell Phone #

            Patient_TVBO t_tvbo = new Patient_TVBO();
            t_tvbo.savePatientInformation(list);
            list = new ArrayList();
            list.Add(this.txtAccidentID.Text);
            list.Add(this.txtPatientID.Text);
            list.Add(this.txtPlatenumber.Text);
            list.Add(this.txtDateofAccident.Text);
            list.Add(this.txtAccidentAddress.Text);
            list.Add(this.txtAccidentCity.Text);
            list.Add(this.txtAccidentState.Text);
            list.Add(this.txtPolicyReport.Text);
            list.Add(this.txtListOfPatient.Text);
            list.Add(this.txtCompanyID.Text);
            if (this.txtAccidentID.Text != "")
            {
                list.Add("UPDATE");
            }
            else
            {
                list.Add("Add");
            }
            list.Add(this.extddlATAccidentState.Text);
            list.Add("");
            t_tvbo.savePatientAccidentInformation(list);
            list = new ArrayList();
            list.Add(this.txtCaseID.Text);
            list.Add("");
            list.Add(this.extddlCaseType.Text);
            if ((!this.txtInsuranceCompany.Text.Equals("") && !this.txtInsuranceCompany.Text.Equals("No suggestions found for your search")) && (this.hdninsurancecode.Value != ""))
            {
                list.Add(this.hdninsurancecode.Value);
            }
            else
            {
                list.Add("");
            }
            list.Add(this.extddlCaseStatus.Text);
            list.Add(this.extddlAttorney.Text);
            list.Add(this.txtPatientID.Text);
            list.Add(this.txtCompanyID.Text);
            list.Add(this.txtClaimNumber.Text);
            list.Add(this.txtPolicyNumber.Text);
            list.Add(this.txtDateofAccident.Text);
            list.Add(this.extddlAdjuster.Text);
            list.Add(this.txtAssociateDiagnosisCode.Text);
            list.Add(this.lstInsuranceCompanyAddress.Text);
            list.Add("UPDATE");
            list.Add(this.txtPolicyHolder.Text);
            list.Add(this.extddlLocation.Text);
            list.Add(this.txtWCBNo.Text);
            t_tvbo = new Patient_TVBO();
            t_tvbo.saveCaseInformation(list);
            ArrayList list2 = new ArrayList();
            list2.Add(this.txtPatientID.Text);
            list2.Add(this.txtATPlateNumber.Text);
            list2.Add(this.txtATAccidentDate.Text);
            list2.Add(this.txtATAddress.Text);
            list2.Add(this.txtATCity.Text);
            list2.Add(this.txtATReportNumber.Text);
            list2.Add(this.txtATAdditionalPatients.Text);
            list2.Add(this.extddlATAccidentState.Text);
            list2.Add(this.txtATHospitalName.Text);
            list2.Add(this.txtATHospitalAddress.Text);
            list2.Add(this.txtATDescribeInjury.Text);
            list2.Add(this.txtATAdmissionDate.Text);
            list2.Add("");
            list2.Add(this.txtAccidentSpecialty.Text);
            t_tvbo.saveAccidentInformation(list2);
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "PATIENT_UPDATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Patient Id : " + txtPatientID.Text + " , Name : " + txtPatientFName.Text +" "+ txtPatientLName.Text; 
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //END



}




