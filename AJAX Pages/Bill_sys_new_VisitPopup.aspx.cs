using AjaxControlToolkit;
using Componend;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using GeneratePDFFile;
using log4net;
using mbs.LienBills;
using PDFValueReplacement;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using GYBValidation;

public partial class AJAX_Pages_Bill_sys_new_VisitPopup : PageBase
{
    CaseDetailsBO objCaseDetails;
    Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    ArrayList objAdd;
    Bill_Sys_PatientBO objPatientBO;
    Bill_Sys_DigosisCodeBO _digosisCodeBO;
    DataSet dset;
    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;
    Bill_Sys_LoginBO _bill_Sys_LoginBO;
    Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    CaseDetailsBO objCaseDetailsBO;
    Bill_Sys_InsertDefaultValues objDefaultValue;
    ListOperation _listOperation;
    string pdfpath;
    string bt_include;
    string str_1500;
    ArrayList billAppointmetDate;
    Bill_Sys_Upload_VisitReport GetSpecialty;
    MUVGenerateFunction _MUVGenerateFunction;
    Bill_Sys_Verification_Desc objVerification_Desc;
    static ILog log;
    string visitFlag = "";
    private Bill_Sys_SystemObject objSystemObject;

    static AJAX_Pages_Bill_sys_new_VisitPopup()
    {
        AJAX_Pages_Bill_sys_new_VisitPopup.log = LogManager.GetLogger("AJAX_Pages_Bill_sys_new_VisitPopup");
    }

    public AJAX_Pages_Bill_sys_new_VisitPopup()
    {
        this.billAppointmetDate = new ArrayList();
    }

    private void BindControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if ((!this.extddlReferringFacility.Visible || (this.extddlReferringFacility.Text == "NA")) || (this.extddlReferringFacility.Text == ""))
            {
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','ddlDoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
                {
                    this.getDoctorDefaultList();
                }
                else
                {
                    this.dset = new DataSet();
                    this.objCaseDetails = new CaseDetailsBO();
                    string str = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                    string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                    string patientLocationID = this.objCaseDetails.GetPatientLocationID(str, str2);
                    this.dset = this.objCaseDetails.DoctorName(patientLocationID, str2);
                    this.ddlDoctor.DataSource = this.dset;
                    this.ddlDoctor.DataTextField = "DESCRIPTION";
                    this.ddlDoctor.DataValueField = "CODE";
                    this.ddlDoctor.DataBind();
                    ListItem item = new ListItem();
                    item.Value = "NA";
                    item.Text = "--- Select ----";
                    this.ddlDoctor.Items.Insert(0, item);
                }
                this.extddlVisitType.Text="NA";
                this.lblVisitType.Visible = true;
                this.extddlVisitType.Visible = true;
                this.lblTIme.Visible = false;
                this.ddlHours.Visible = false;
                this.ddlMinutes.Visible = false;
                this.ddlTime.Visible = false;
                this.lblProcedure.Visible = false;
                this.ddlTestNames.Visible = false;
                this.lblVisitStatus.Visible = false;
                this.ddlStatus.Visible = false;
                this.lblNotes.Visible = false;
                this.txtNotes.Visible = false;
                this.extddlRoom.Visible = false;
                this.lblRoom.Visible = false;
                this.extddlReferringDoctor.Visible = false;
                this.ddlDoctor.Visible = true;
                this.lblTIme.Visible = false;
                this.ddlHours.Visible = false;
                this.ddlMinutes.Visible = false;
                this.ddlTime.Visible = false;
                this.lblVisitType.Visible = true;
                this.extddlVisitType.Visible = true;
                this.lblTransport.Visible = false;
                this.chkTransportation.Visible = false;
            }
            else
            {
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlReferringFacility,extddlRoom,extddlReferringDoctor,txtAppointmentDate,ddlHours,ddlTestNames,flUpload');");
                this.extddlRoom.Visible = true;
                this.extddlRoom.Flag_ID=this.txtCompanyID.Text;
                this.BindReferringDoctor();
                this.lblNotes.Visible = true;
                this.txtNotes.Visible = true;
                this.lblRoom.Visible = true;
                this.BindReferringFacilityDoctor();
                this.extddlReferringDoctor.Visible = true;
                this.ddlDoctor.Visible = false;
                this.BindTimeControl();
                this.lblTIme.Visible = true;
                this.ddlHours.Visible = true;
                this.ddlMinutes.Visible = true;
                this.ddlTime.Visible = true;
                this.lblVisitType.Visible = false;
                this.extddlVisitType.Visible = false;
                this.lblProcedure.Visible = true;
                this.lblTransport.Visible = true;
                this.chkTransportation.Visible = true;
            }
            if ((this.extddlReferringFacility.Text != "NA") && (this.extddlReferringFacility.Text != ""))
            {
                this.lblTransport.Visible = true;
                this.chkTransportation.Visible = true;
                this.extddlTransport.Flag_ID=this.extddlReferringFacility.Text;
            }
            else
            {
                this.lblTransport.Visible = false;
                this.chkTransportation.Visible = false;
                this.extddlTransport.Visible = false;
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

    private void BindDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
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

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
            this.grdDiagonosisCode.DataBind();
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

    private void BindLatestTransaction()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="LatestBillTransaction.xml";
            this._listOperation.LoadList();
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

    protected void BindReferringDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO rbo = new Bill_Sys_DoctorBO();
            this.objCaseDetails = new CaseDetailsBO();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
            {
                DataSet referralDoctorList = rbo.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                this.ddlDoctor.DataSource = referralDoctorList;
                ListItem item = new ListItem("---select---", "NA");
                this.ddlDoctor.DataTextField = "DESCRIPTION";
                this.ddlDoctor.DataValueField = "CODE";
                this.ddlDoctor.DataBind();
                this.ddlDoctor.Items.Insert(0, item);
            }
            else
            {
                string str = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                string patientLocationID = this.objCaseDetails.GetPatientLocationID(str, str2);
                DataSet set2 = this.objCaseDetails.DoctorName(patientLocationID, str2);
                this.ddlDoctor.DataSource = set2;
                this.ddlDoctor.DataTextField = "DESCRIPTION";
                this.ddlDoctor.DataValueField = "CODE";
                this.ddlDoctor.DataBind();
                ListItem item2 = new ListItem();
                item2.Value = "NA";
                item2.Text = "--- Select ----";
                this.ddlDoctor.Items.Insert(0, item2);
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

    private void BindReferringFacilityControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.GET_SYS_KEY_BIT(this.txtCompanyID.Text) == "1")
            {
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlReferringFacility,extddlRoom,extddlReferringDoctor,txtAppointmentDate,ddlHours,ddlTestNames,flUpload,extddlMedicalOffice');");
            }
            else
            {
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','extddlReferringFacility,extddlRoom,extddlReferringDoctor,txtAppointmentDate,ddlHours,ddlTestNames,flUpload');");
            }
            this.extddlRoom.Visible = true;
            this.extddlRoom.Flag_ID=this.txtCompanyID.Text;
            this.BindReferringDoctor();
            this.lblNotes.Visible = true;
            this.txtNotes.Visible = true;
            this.lblRoom.Visible = true;
            this.BindReferringFacilityDoctor();
            this.extddlReferringDoctor.Visible = true;
            this.ddlDoctor.Visible = false;
            this.BindTimeControl();
            this.lblTIme.Visible = true;
            this.ddlHours.Visible = true;
            this.ddlMinutes.Visible = true;
            this.ddlTime.Visible = true;
            this.lblVisitType.Visible = false;
            this.extddlVisitType.Visible = false;
            this.lblProcedure.Visible = true;
            this.lblTransport.Visible = false;
            this.chkTransportation.Visible = false;
            this.extddlTransport.Visible = false;
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

    private void BindReferringFacilityDoctor()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO rbo = new Bill_Sys_DoctorBO();
            this.objCaseDetails = new CaseDetailsBO();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
            {
                DataSet referralDoctorList = rbo.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                this.extddlReferringDoctor.DataSource = referralDoctorList;
                ListItem item = new ListItem("---select---", "NA");
                this.extddlReferringDoctor.DataTextField = "DESCRIPTION";
                this.extddlReferringDoctor.DataValueField = "CODE";
                this.extddlReferringDoctor.DataBind();
                this.extddlReferringDoctor.Items.Insert(0, item);
            }
            else
            {
                string str = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                string patientLocationID = this.objCaseDetails.GetPatientLocationID(str, str2);
                DataSet set2 = this.objCaseDetails.DoctorName(patientLocationID, str2);
                this.extddlReferringDoctor.DataSource = set2;
                this.extddlReferringDoctor.DataTextField = "DESCRIPTION";
                this.extddlReferringDoctor.DataValueField = "CODE";
                this.extddlReferringDoctor.DataBind();
                ListItem item2 = new ListItem();
                item2.Value = "NA";
                item2.Text = "--- Select ----";
                this.extddlReferringDoctor.Items.Insert(0, item2);
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

    private void BindReferringProcedureCodes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                list.Add(this.txtCompanyID.Text);
            }
            else
            {
                list.Add(this.extddlReferringFacility.Text);
            }
            list.Add(this.extddlRoom.Text);
            Bill_Sys_ManageVisitsTreatmentsTests_BO s_bo = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            this.ddlTestNames.Items.Clear();
            this.ddlTestNames.DataSource = s_bo.GetReferringProcCodeList(list);
            this.ddlTestNames.DataTextField = "description";
            this.ddlTestNames.DataValueField = "code";
            this.ddlTestNames.DataBind();
            this.ddlTestNames.Items.Insert(0, "--- Select ---");
            this.ddlTestNames.Visible = true;
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

    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ddlHours.Items.Clear();
            this.ddlMinutes.Items.Clear();
            this.ddlTime.Items.Clear();
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    this.ddlHours.Items.Add(i.ToString());
                }
                else
                {
                    this.ddlHours.Items.Add("0" + i.ToString());
                }
            }
            for (int j = 0; j < 60; j++)
            {
                if (j > 9)
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                }
                else
                {
                    this.ddlMinutes.Items.Add("0" + j.ToString());
                }
            }
            this.ddlTime.Items.Add("AM");
            this.ddlTime.Items.Add("PM");
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

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        this.dummybtnAddGroup.Visible = true;
        this.hndPopUpvalue.Value = "GroupPopUpValue";
        this.showModalPopup();
    }

    protected void btnAddServices_Click(object sender, EventArgs e)
    {
        this.dummybtnAddServices.Visible = true;
        this.hndPopUpvalue.Value = "PopUpValue";
        this.showModalPopup();
    }

    protected void btnGenerateWCPDFAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
           // ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            string str6 = new WC_Bill_Generation().GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType1.SelectedValue, str4, str3, str, str2, str5, this.hdnSpeciality.Value.ToString(), 0);
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str6 + "');", true);
            this.pnlPDFWorkerCompAdd.Visible = false;
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

    protected void btnOK_Click2(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < this.grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (box.Checked)
                {
                    ListItem item = new ListItem(this.grdDiagonosisCode.Items[i].Cells[2].Text + '-' + this.grdDiagonosisCode.Items[i].Cells[4].Text, this.grdDiagonosisCode.Items[i].Cells[1].Text);
                    if (!this.lstDiagnosisCodes.Items.Contains(item))
                    {
                        this.lstDiagnosisCodes.Items.Add(item);
                    }
                }
            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ArrayList list = new ArrayList();
        if (this.Session["DELETED_PROC_CODES"] != null)
        {
            list = (ArrayList)this.Session["DELETED_PROC_CODES"];
        }
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("I_EventID");
            table.Columns.Add("SZ_VISIT_TYPE");
            table.Columns.Add("BT_IS_LIMITE");
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    DataRow row = table.NewRow();
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                    if ((((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString();
                    }
                    row["SZ_TYPE_CODE_ID"] = item.Cells[8].Text.ToString();
                    row["FLT_GROUP_AMOUNT"] = item.Cells[9].Text.ToString();
                    row["I_GROUP_AMOUNT_ID"] = item.Cells[10].Text.ToString();
                    row["I_EventID"] = item.Cells[12].Text.ToString();
                    row["SZ_VISIT_TYPE"] = item.Cells[13].Text.ToString();
                    row["BT_IS_LIMITE"] = item.Cells[14].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if ((this.grdTransactionDetails.Items[i].Cells[0].Text != "") && (this.grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;"))
                    {
                        CheckBox box = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box.Checked)
                        {
                            string text = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str2 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                if ((str2 == table.Rows[j][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(text), Convert.ToDateTime(table.Rows[j][1].ToString())) == 0))
                                {
                                    list.Add(table.Rows[j][0].ToString());
                                    table.Rows[j].Delete();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        CheckBox box2 = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box2.Checked)
                        {
                            string str3 = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str4 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int k = 0; k < table.Rows.Count; k++)
                            {
                                if ((str4 == table.Rows[k][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(str3), Convert.ToDateTime(table.Rows[k][1].ToString())) == 0))
                                {
                                    list.Add(table.Rows[k][0].ToString());
                                    table.Rows[k].Delete();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            double num4 = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao = new BillTransactionDAO();
                string str5 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string procID = ndao.GetProcID(this.txtCompanyID.Text, str5);
                string str7 = ndao.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                if ((str7 != "") && (str7 != "NULL"))
                {
                    for (int m = 0; m < this.grdTransactionDetails.Items.Count; m++)
                    {
                        if ((this.grdTransactionDetails.Items[m].Cells[5].Text != "") && (this.grdTransactionDetails.Items[m].Cells[5].Text != "&nbsp;"))
                        {
                            num4 += Convert.ToDouble(this.grdTransactionDetails.Items[m].Cells[5].Text);
                        }
                        if (m == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao2 = new BillTransactionDAO();
                            string str8 = this.grdTransactionDetails.Items[m].Cells[2].Text.ToString();
                            string str9 = this.grdTransactionDetails.Items[m].Cells[13].Text.ToString();
                            string str10 = ndao2.GetProcID(this.txtCompanyID.Text, str8);
                            string str11 = ndao2.GetLimit(this.txtCompanyID.Text, str9, str10);
                            if (str11 != "")
                            {
                                if (Convert.ToDouble(str11) < num4)
                                {
                                    this.grdTransactionDetails.Items[m].Cells[9].Text = str11;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[m].Cells[9].Text = num4.ToString();
                                }
                            }
                            num4 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[m].Cells[1].Text != this.grdTransactionDetails.Items[m + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str12 = this.grdTransactionDetails.Items[m].Cells[2].Text.ToString();
                            string str13 = this.grdTransactionDetails.Items[m].Cells[13].Text.ToString();
                            string str14 = ndao3.GetProcID(this.txtCompanyID.Text, str12);
                            string str15 = ndao3.GetLimit(this.txtCompanyID.Text, str13, str14);
                            if (str15 != "")
                            {
                                if (Convert.ToDouble(str15) < num4)
                                {
                                    this.grdTransactionDetails.Items[m].Cells[9].Text = str15;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[m].Cells[9].Text = num4.ToString();
                                }
                            }
                            num4 = 0.0;
                        }
                    }
                }
            }
            this.Session["DELETED_PROC_CODES"] = list;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //log.Debug("Function save start from here.");
        this.txteventid.Text = "";
        this.txtDoctid.Text = "";
        this.txtprocid.Text = "";
        this.txtvisittype.Text = "";
        bool flag = false;
        log.Debug("Inside Butoon Save Click");
        if ((this.extddlReferringFacility.Visible && (this.extddlReferringFacility.Text != "NA")) || ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            log.Debug("If Company is referring Company and ReferringFacility = " + this.extddlReferringFacility.Text);
            string str = this.GET_SYS_KEY_BIT(this.txtCompanyID.Text);
            log.Debug(" szOfficBit(from GET_SYS_KEY_BIT method by passing company ID) =" + str);
            if (str == "1")
            {
                log.Debug(" szOfficBit  =" + str);
                log.Debug("Doctor =" + this.ddlDoctor.SelectedValue);
                if ((this.extddlMedicalOffice.Text != "NA") && (this.extddlReferringDoctor.Text != "NA"))
                {
                    log.Debug("If MedicalOffice  is not NA  = " + this.extddlMedicalOffice.Text);
                    log.Debug("If ReferringDoctor is not NA = " + this.extddlReferringDoctor.Text);
                    string str3 = this.CHECK_DOC_OFF(this.extddlReferringDoctor.Text, this.extddlMedicalOffice.Text, this.txtCompanyID.Text);
                    if (str3 != "")
                    {
                        log.Debug("If szDocOff is not empty value= " + str3);
                        log.Debug("Go to the saveReferringFacility() method. ");
                        this.saveReferringFacility();
                    }
                    else
                    {
                        log.Debug("If szDocOff is an empty value");
                        this.lblMsg.Text = "";
                        this.lblMsg.Text = "Please select valid Doctor for Office";
                        this.lblMsg.Visible = true;
                    }
                }
                else
                {
                    log.Debug("If MedicalOffice and ReferringDoctor is NA ");
                    this.lblMsg.Text = "";
                    this.lblMsg.Text = "Please select Doctor and Office";
                    this.lblMsg.Visible = true;
                }
            }
            else
            {
                log.Debug(" szOfficBit is not 1,value =" + str);
                log.Debug("Go to the saveReferringFacility() method. ");
                this.saveReferringFacility();
            }
        }
        else
        {
            log.Debug("If Company is not referring Company and ReferringFacility = " + this.extddlReferringFacility.Text);
            log.Debug("Save normal funtionality ( Lawuser)");
            int num = 4;
            string str4 = "";
            string str5 = "";
            int num2 = 0;
            string str6 = "";
            int num3 = 0;
            string str7 = "";
            int num4 = 0;
            string str8 = "";
            int num5 = 0;
            this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            this.objAdd = new ArrayList();
            try
            {
                string[] strArray2 = this.txtAppointmentDate.Text.Split(new char[] { ',' });
                for (int i = 0; i < strArray2.Length; i++)
                {
                    object obj2 = strArray2[i];
                    if (obj2.ToString() != "")
                    {
                        if (((Convert.ToDateTime(obj2) > Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))) && !this.ddlTestNames.Visible) && (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).ALLOW_TO_ADD_VISIT_FOR_FUTURE_DATE.ToString() == "0"))
                        {
                            if (num2 == num)
                            {
                                str5 = str5 + obj2.ToString() + " , <br/>";
                                num2 = 0;
                            }
                            else
                            {
                                str5 = str5 + obj2.ToString() + " ,";
                                num2++;
                            }
                        }
                        else
                        {
                            bool flag2 = false;
                            bool flag3 = false;
                            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                            SqlCommand command = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                            command.CommandType = CommandType.StoredProcedure;
                            command.Connection = connection;
                            command.Connection.Open();
                            command.Parameters.AddWithValue("@SZ_CASE_ID", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                            command.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            command.Parameters.AddWithValue("@SZ_PATIENT_ID", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                            command.Parameters.AddWithValue("@SZ_DOCTOR_ID", this.ddlDoctor.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@VISIT_DATE", obj2);
                            SqlParameter parameter = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);
                            SqlParameter parameter2 = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                            parameter2.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter2);
                            command.ExecuteNonQuery();
                            command.Connection.Close();
                            this.txtDoctid.Text = this.ddlDoctor.SelectedValue.ToString();
                            this.txtvisittype.Text = this.extddlVisitType.Selected_Text.ToString();
                            flag2 = Convert.ToBoolean(parameter.Value);
                            flag3 = Convert.ToBoolean(parameter2.Value);
                            if (!flag3)
                            {
                                this.billAppointmetDate.Add(obj2);
                            }
                            if (!this.ddlTestNames.Visible)
                            {
                                if (!flag2 && (this.extddlVisitType.Selected_Text != "IE"))
                                {
                                    if (num3 == num)
                                    {
                                        str6 = str6 + obj2.ToString() + " , <br/>";
                                        num3 = 0;
                                    }
                                    else
                                    {
                                        str6 = str6 + obj2.ToString() + " , ";
                                        num3++;
                                    }
                                    goto Label_0BED;
                                }
                                if (flag2 && (this.extddlVisitType.Selected_Text == "IE"))
                                {
                                    if (num4 == num)
                                    {
                                        str7 = str7 + obj2.ToString() + " , <br/>";
                                        num4 = 0;
                                    }
                                    else
                                    {
                                        str7 = str7 + obj2.ToString() + " , ";
                                        num4++;
                                    }
                                    goto Label_0BED;
                                }
                                if (flag3)
                                {
                                    if (num5 == num)
                                    {
                                        str8 = str8 + obj2.ToString() + " , <br/>";
                                        num5 = 0;
                                    }
                                    else
                                    {
                                        str8 = str8 + obj2.ToString() + " , ";
                                        num5++;
                                    }
                                    goto Label_0BED;
                                }
                            }
                            Bill_Sys_Calender calender = new Bill_Sys_Calender();
                            this.objAdd = new ArrayList();
                            this.objAdd.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                            this.objAdd.Add(obj2);
                            if (this.ddlTestNames.Visible)
                            {
                                this.objAdd.Add(this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString());
                            }
                            else
                            {
                                this.objAdd.Add("8.30");
                            }
                            this.objAdd.Add("");
                            this.objAdd.Add(this.ddlDoctor.SelectedValue.ToString());
                            this.objAdd.Add("TY000000000000000003");
                            this.objAdd.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            if (this.ddlTestNames.Visible)
                            {
                                int num6 = Convert.ToInt32(this.ddlMinutes.SelectedValue) + Convert.ToInt32(30);
                                int num7 = Convert.ToInt32(this.ddlHours.SelectedValue);
                                string selectedValue = this.ddlTime.SelectedValue;
                                if (num6 >= 60)
                                {
                                    num6 -= 60;
                                    num7++;
                                    if (num7 > 12)
                                    {
                                        num7 -= 12;
                                        if (this.ddlHours.SelectedValue != "12")
                                        {
                                            if (selectedValue == "AM")
                                            {
                                                selectedValue = "PM";
                                            }
                                            else if (selectedValue == "PM")
                                            {
                                                selectedValue = "AM";
                                            }
                                        }
                                    }
                                    else if ((num7 == 12) && (this.ddlHours.SelectedValue != "12"))
                                    {
                                        if (selectedValue == "AM")
                                        {
                                            selectedValue = "PM";
                                        }
                                        else if (selectedValue == "PM")
                                        {
                                            selectedValue = "AM";
                                        }
                                    }
                                }
                                this.objAdd.Add(this.ddlTime.SelectedValue);
                                this.objAdd.Add(num7.ToString().PadLeft(2, '0') + "." + num6.ToString().PadLeft(2, '0'));
                                this.objAdd.Add(selectedValue);
                            }
                            else
                            {
                                this.objAdd.Add("AM");
                                this.objAdd.Add("9.00");
                                this.objAdd.Add("AM");
                            }
                            if (this.ddlTestNames.Visible)
                            {
                                this.objAdd.Add(this.ddlStatus.SelectedValue);
                            }
                            else
                            {
                                this.objAdd.Add("2");
                            }
                            this.objAdd.Add(this.extddlVisitType.Text);
                            calender.SaveEvent(this.objAdd, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                            ArrayList list = new ArrayList();
                            list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                            list.Add(this.ddlDoctor.SelectedValue.ToString());
                            list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            int eventID = calender.GetEventID(list);
                            hdnEventID.Value = eventID.ToString();
                            hdnVisitType.Value = extddlVisitType.Selected_Text;
                            hdnVisitTypeId.Value = extddlVisitType.Text;
                            this.GetSpecialty = new Bill_Sys_Upload_VisitReport();
                            if (this.txtprocid.Text == "")
                            {
                                this.txtprocid.Text = this.GetSpecialty.GetDoctorSpecialty(this.txtDoctid.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
                            }
                            hdnRoomId.Value = this.txtprocid.Text;
                            if (this.ddlTestNames.Visible)
                            {
                                foreach (ListItem item in this.ddlTestNames.Items)
                                {
                                    if (!item.Selected)
                                    {
                                        continue;
                                    }
                                    this.objAdd = new ArrayList();
                                    this.objAdd.Add(item.Value);
                                    this.objAdd.Add(eventID);
                                    if (this.ddlTestNames.Visible)
                                    {
                                        this.objAdd.Add(this.ddlStatus.SelectedValue);
                                    }
                                    else
                                    {
                                        this.objAdd.Add("2");
                                    }
                                    calender.Save_Event_RefferPrcedure(this.objAdd);
                                }
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="APPOINTMENT_ADDED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC="Date : " + obj2;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                            str4 = str4 + obj2 + " , ";
                        Label_0BED: ;
                        }
                    }
                }
                if (((str5 == "") && (str6 == "")) && ((str7 == "") && (str8 == "")))
                {
                    this.lblMsg.Text = "Appointment scheduled successfully.";
                    this.Session["CreateBill"] = null;
                    this.Session["CreateBill"] = this.billAppointmetDate;
                    flag = true;
                }
                else
                {
                    if (str4 != "")
                    {
                        this.Session["CreateBill"] = null;
                        this.Session["CreateBill"] = this.billAppointmetDate;
                        this.lblMsg.Text = this.lblMsg.Text + str4 + " -- Completed.<br/>";
                        flag = true;
                    }
                    if (str5 != "")
                    {
                        this.lblMsg.Text = this.lblMsg.Text + str5 + " -- Visit for future date cannot be added.<br/>";
                    }
                    if (str6 != "")
                    {
                        if (num3 > 2)
                        {
                            this.lblMsg.Text = this.lblMsg.Text + str6 + "<br/> -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                        }
                        else
                        {
                            this.lblMsg.Text = this.lblMsg.Text + str6 + " -- Schedule can not be saved patient is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                        }
                    }
                    if (str7 != "")
                    {
                        this.lblMsg.Text = this.lblMsg.Text + str7 + " -- Schedule can not be saved because patient already has Initial Evaluation.<br/>";
                    }
                    if (str8 != "")
                    {
                        this.lblMsg.Text = this.lblMsg.Text + str8 + " -- Schedule can not be saved because patient already has this visit.<br/>";
                    }
                }
                this.lblMsg.Focus();
                this.lblMsg.Visible = true;
                if (((flag && (this.extddlReferringFacility.Text == "NA")) && !((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY) || (this.extddlReferringFacility.Text == ""))
                {
                    if(flag)
                    {
                        this.lnkbtnRemoveDiag.Visible = true;
                        this.lnkAddDiagnosis.Visible = true;
                        this.btnAddGroup.Visible = true;
                        this.btnAddServices.Visible = true;
                        this.btnRemove.Visible = true;
                        this.Button2.Visible = true;
                        this.ReportUpload.Enabled = true;
                        A3.Visible = true;
                    }
                    
                    //if (this.extddlVisitType.Selected_Text != "C")
                    //{
                    //    this.UploadButton.Enabled = true;
                    //    this.lnkscan.Enabled = true;
                    //}
                    //else
                    //{
                    //    this.UploadButton.Enabled = false;
                    //    this.lnkscan.Enabled = false;
                    //}
                   
                }
                else
                {
                    this.lnkbtnRemoveDiag.Visible = false;
                    this.lnkAddDiagnosis.Visible = false;
                    this.btnAddGroup.Visible = false;
                    this.btnAddServices.Visible = false;
                    this.btnRemove.Visible = false;
                    this.Button2.Visible = false;
                    this.ReportUpload.Enabled = false;
                    //this.UploadButton.Enabled = false;
                    //this.lnkscan.Enabled = false;
                    A3.Visible = false;
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
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSaveWithTransaction_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            BillTransactionEO neo = new BillTransactionEO();
            neo.SZ_CASE_ID=this.txtCaseID.Text;
            neo.SZ_COMPANY_ID=this.txtCompanyID.Text;
            neo.DT_BILL_DATE=Convert.ToDateTime(this.txtBillDate.Text);
            neo.SZ_DOCTOR_ID=this.hndDoctorID.Value.ToString();
            neo.SZ_TYPE=this.ddlType.Text;
            neo.SZ_TESTTYPE="";
            neo.FLAG="ADD";
            neo.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            new Bill_Sys_Calender();
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                string text = item.Cells[12].Text;
                if (list.Count != 0)
                {
                    string str3 = "0";
                    for (int j = 0; j < list.Count; j++)
                    {
                        EventEO teo = new EventEO();
                        teo = (EventEO)list[j];
                        if ((text == teo.I_EVENT_ID) && (teo.I_EVENT_ID != "&nbsp;"))
                        {
                            str3 = "1";
                        }
                    }
                    if (str3 == "0")
                    {
                        EventEO teo2 = new EventEO();
                        teo2.I_EVENT_ID=text;
                        teo2.BT_STATUS="1";
                        teo2.I_STATUS="2";
                        teo2.SZ_BILL_NUMBER="";
                        teo2.DT_BILL_DATE=Convert.ToDateTime(this.txtBillDate.Text);
                        list.Add(teo2);
                    }
                }
                else
                {
                    EventEO teo3 = new EventEO();
                    teo3.I_EVENT_ID=text;
                    teo3.BT_STATUS="1";
                    teo3.I_STATUS="2";
                    teo3.SZ_BILL_NUMBER="";
                    teo3.DT_BILL_DATE=Convert.ToDateTime(this.txtBillDate.Text);
                    list.Add(teo3);
                }
                int num2 = 0;
                for (int i = 0; i < list2.Count; i++)
                {
                    EventRefferProcedureEO eeo = new EventRefferProcedureEO();
                    eeo = (EventRefferProcedureEO)list2[i];
                    if (eeo.I_EVENT_ID == text)
                    {
                        num2 = 1;
                        break;
                    }
                }
                if (num2 != 1)
                {
                    foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                    {
                        if ((item2.Cells[1].Text != "") && item.Cells[12].Text.Equals(item2.Cells[12].Text))
                        {
                            EventRefferProcedureEO eeo2 = new EventRefferProcedureEO();
                            eeo2.SZ_PROC_CODE=item2.Cells[8].Text;
                            eeo2.I_EVENT_ID=item2.Cells[12].Text;
                            eeo2.I_STATUS="2";
                            list2.Add(eeo2);
                        }
                    }
                    continue;
                }
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            ArrayList list3 = new ArrayList();
            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                if (((!(item3.Cells[1].Text.ToString() != "") || !(item3.Cells[1].Text.ToString() != "&nbsp;")) || (!(item3.Cells[3].Text.ToString() != "") || !(item3.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item3.Cells[4].Text.ToString() != "") || !(item3.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                BillProcedureCodeEO eeo3 = new BillProcedureCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo3.SZ_PROCEDURE_ID=item3.Cells[2].Text.ToString();
                this.hdnSpeciality.Value = item3.Cells[2].Text.ToString();
                if (item3.Cells[6].Text.ToString() != "&nbsp;")
                {
                    eeo3.FL_AMOUNT=item3.Cells[5].Text.ToString();
                }
                else
                {
                    eeo3.FL_AMOUNT="0";
                }
                eeo3.SZ_BILL_NUMBER="";
                eeo3.DT_DATE_OF_SERVICE=Convert.ToDateTime(item3.Cells[1].Text.ToString());
                eeo3.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                eeo3.I_UNIT=((TextBox)item3.Cells[6].FindControl("txtUnit")).Text.ToString();
                eeo3.FLT_PRICE=item3.Cells[5].Text.ToString();
                eeo3.SZ_DOCTOR_ID=this.hndDoctorID.Value.ToString();
                eeo3.SZ_CASE_ID=this.txtCaseID.Text;
                eeo3.SZ_TYPE_CODE_ID=item3.Cells[8].Text.ToString();
                eeo3.FLT_GROUP_AMOUNT=item3.Cells[9].Text.ToString();
                eeo3.I_GROUP_AMOUNT_ID=item3.Cells[10].Text.ToString();
                list3.Add(eeo3);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList list4 = new ArrayList();
            foreach (ListItem item4 in this.lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO eeo4 = new BillDiagnosisCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo4.SZ_DIAGNOSIS_CODE_ID=item4.Value.ToString();
                list4.Add(eeo4);
            }
            BillTransactionDAO ndao = new BillTransactionDAO();
            Result result = new Result();
            result = ndao.SaveBillTransactions(neo, list, list2, list3, list4);
            if (result.msg_code == "ERR")
            {
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
            else
            {
                this.txtBillID.Text = result.bill_no;
                str = this.txtBillID.Text;
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_CREATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                string patientID = this.objCaseDetailsBO.GetPatientID(str);
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000001")
                {
                    this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (this.grdLatestBillTransaction.Items.Count == 0)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    }
                    else if (this.grdLatestBillTransaction.Items.Count >= 1)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                    }
                }
                
                this.usrMessage.PutMessage(" Bill Saved successfully ! ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();

                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000002")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000003")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000001")
                {
                    this.hdnWCPDFBillNumber.Value = str.ToString();
                    this.pnlPDFWorkerCompAdd.Visible = true;
                    this.pnlPDFWorkerCompAdd.Width = Unit.Pixel(50);
                    this.pnlPDFWorkerCompAdd.Height = Unit.Pixel(100);
                }
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000004")
                {
                    Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
                    Lien lien = new Lien();
                    MUVGenerateFunction function = new MUVGenerateFunction();
                    string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    string str8 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.Session["TM_SZ_BILL_ID"] = str;
                    string str7 = function.get_bt_include(str8, doctorSpeciality, "", "Speciality");
                    string str6 = function.get_bt_include(str8, "", "WC000000000000000004", "CaseType");
                    if ((str7 == "True") && (str6 == "True"))
                    {
                        string str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                        string str10 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + doctorSpeciality + "/";
                        string str11 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string str12 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                        this.str_1500 = function.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        MergePDF.MergePDFFiles(template.getPhysicalPath() + str10 + lien.GenratePdfForLienWithMuv(str8, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str12, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str11), template.getPhysicalPath() + str9 + this.str_1500, template.getPhysicalPath() + str10 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        string text1 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str10 + this.str_1500.Replace(".pdf", "_MER.pdf");
                        ArrayList list5 = new ArrayList();
                        list5.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list5.Add(str10 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list5.Add(this.Session["TM_SZ_CASE_ID"]);
                        list5.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                        list5.Add(str10);
                        list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list5.Add(doctorSpeciality);
                        list5.Add("NF");
                        list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        template.saveGeneratedBillPath(list5);
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                }
                this.grdTransactionDetails.DataSource = null;
                this.grdTransactionDetails.DataBind();
                this.lstDiagnosisCodes.Items.Clear();
                this.btnAddGroup.Visible = false;
                this.btnAddServices.Visible = false;
                this.btnRemove.Visible = false;
            }
        }
        catch(Exception ex)
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataRow str;
        DateTime dateTime;
        try
        {
            if (this.lstDiagnosisCodes.Items.Count == 0)
            {
                this.lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO billSysAssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                this.lstDiagnosisCodes.DataSource = billSysAssociateDiagnosisCodeBO.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value, this.txtCompanyID.Text).Tables[0];
                this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                this.lstDiagnosisCodes.DataValueField = "CODE";
                this.lstDiagnosisCodes.DataBind();
                this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dataTable.Columns.Add("DT_DATE_OF_SERVICE");
            dataTable.Columns.Add("SZ_PROCEDURE_ID");
            dataTable.Columns.Add("SZ_PROCEDURAL_CODE");
            dataTable.Columns.Add("SZ_CODE_DESCRIPTION");
            dataTable.Columns.Add("FLT_AMOUNT");
            dataTable.Columns.Add("I_UNIT");
            dataTable.Columns.Add("SZ_TYPE_CODE_ID");
            dataTable.Columns.Add("FLT_GROUP_AMOUNT");
            dataTable.Columns.Add("I_GROUP_AMOUNT_ID");
            dataTable.Columns.Add("I_EventID");
            dataTable.Columns.Add("SZ_VISIT_TYPE");
            dataTable.Columns.Add("BT_IS_LIMITE");
            int num = 0;
            string str1 = "";
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                if (!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;") || !(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;") || !(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;"))
                {
                    continue;
                }
                str = dataTable.NewRow();
                if (!(item.Cells[0].Text.ToString() != "&nbsp;") || !(item.Cells[0].Text.ToString() != ""))
                {
                    str["SZ_BILL_TXN_DETAIL_ID"] = "";
                }
                else
                {
                    str["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                }
                dateTime = Convert.ToDateTime(item.Cells[1].Text.ToString());
                str["DT_DATE_OF_SERVICE"] = dateTime.ToShortDateString();
                DateTime dateTime1 = Convert.ToDateTime(item.Cells[1].Text.ToString());
                dateTime1.ToShortDateString();
                str["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                str["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                str["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                str["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                if (((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString() != "" && Convert.ToInt32(((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0)
                {
                    str["I_UNIT"] = ((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString();
                }
                str["SZ_TYPE_CODE_ID"] = item.Cells[8].Text.ToString();
                BillTransactionDAO billTransactionDAO = new BillTransactionDAO();
                string str2 = item.Cells[2].Text.ToString();
                string str3 = item.Cells[13].Text.ToString();
                string procID = billTransactionDAO.GetProcID(this.txtCompanyID.Text, str2);
                if (billTransactionDAO.GetLimit(this.txtCompanyID.Text, str3, procID) == "")
                {
                    str["FLT_GROUP_AMOUNT"] = item.Cells[9].Text.ToString();
                }
                else
                {
                    str["FLT_GROUP_AMOUNT"] = "";
                }
                str["I_GROUP_AMOUNT_ID"] = item.Cells[10].Text.ToString();
                str["I_EventID"] = item.Cells[12].Text.ToString();
                str["SZ_VISIT_TYPE"] = item.Cells[13].Text.ToString();
                str["BT_IS_LIMITE"] = item.Cells[14].Text.ToString();
                str1 = item.Cells[12].Text.ToString();
                dataTable.Rows.Add(str);
            }
            this.txtDateOfservice.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            foreach (DataGridItem dataGridItem in this.grdAllReports.Items)
            {
                if (!((CheckBox)dataGridItem.Cells[0].FindControl("chkselect")).Checked)
                {
                    continue;
                }
                foreach (DataGridItem item1 in this.grdProcedure.Items)
                {
                    CheckBox checkBox = (CheckBox)item1.FindControl("chkselect");
                    CheckBox checkBox1 = (CheckBox)dataGridItem.FindControl("chkLimit");
                    num = 1;
                    string str4 = dataGridItem.Cells[5].Text.ToString();
                    if (!checkBox.Checked)
                    {
                        continue;
                    }
                    str = dataTable.NewRow();
                    str["SZ_BILL_TXN_DETAIL_ID"] = "";
                    DateTime dateTime2 = Convert.ToDateTime(dataGridItem.Cells[2].Text.ToString());
                    str["DT_DATE_OF_SERVICE"] = dateTime2.ToShortDateString();
                    str["SZ_PROCEDURE_ID"] = item1.Cells[1].Text.ToString();
                    str["SZ_PROCEDURAL_CODE"] = item1.Cells[3].Text.ToString();
                    str["SZ_CODE_DESCRIPTION"] = item1.Cells[4].Text.ToString();
                    str["FLT_AMOUNT"] = item1.Cells[5].Text.ToString();
                    str["I_UNIT"] = "1";
                    str["SZ_TYPE_CODE_ID"] = item1.Cells[2].Text.ToString();
                    str["I_EventID"] = dataGridItem.Cells[4].Text.ToString();
                    str["SZ_VISIT_TYPE"] = str4;
                    if (num != 1)
                    {
                        str["BT_IS_LIMITE"] = "0";
                    }
                    else
                    {
                        str["BT_IS_LIMITE"] = "1";
                    }
                    dataTable.Rows.Add(str);
                }
            }
            if (this.grdAllReports.Items.Count != 0)
            {
                foreach (DataGridItem dataGridItem1 in this.grdProcedure.Items)
                {
                    if (!((CheckBox)dataGridItem1.FindControl("chkselect")).Checked)
                    {
                        continue;
                    }
                    str = dataTable.NewRow();
                    str["SZ_BILL_TXN_DETAIL_ID"] = "";
                    dateTime = Convert.ToDateTime(this.txtBillDate.Text);
                    str["DT_DATE_OF_SERVICE"] = dateTime.ToShortDateString();
                    str["SZ_PROCEDURE_ID"] = dataGridItem1.Cells[1].Text.ToString();
                    str["SZ_PROCEDURAL_CODE"] = dataGridItem1.Cells[3].Text.ToString();
                    str["SZ_CODE_DESCRIPTION"] = dataGridItem1.Cells[4].Text.ToString();
                    str["FLT_AMOUNT"] = dataGridItem1.Cells[5].Text.ToString();
                    str["I_UNIT"] = "1";
                    str["SZ_TYPE_CODE_ID"] = dataGridItem1.Cells[2].Text.ToString();
                    str["I_EventID"] = str1;
                    dataTable.Rows.Add(str);
                }
            }
            else if (this.Session["CreateBill"] != null)
            {
                this.billAppointmetDate = (ArrayList)this.Session["CreateBill"];
                if (this.billAppointmetDate.Count > 0)
                {
                    int num1 = 0;
                    foreach (string str5 in this.billAppointmetDate)
                    {
                        string str6 = str5;
                        foreach (DataGridItem item2 in this.grdProcedure.Items)
                        {
                            CheckBox checkBox2 = (CheckBox)item2.FindControl("chkselect");
                            num = 1;
                            string str7 = this.extddlVisitType.Selected_Text.ToString();
                            if (!checkBox2.Checked)
                            {
                                continue;
                            }
                            int num2 = 0;
                            while (num2 < dataTable.Rows.Count)
                            {
                                if (!(dataTable.Rows[num2][0].ToString() == item2.Cells[1].Text) || DateTime.Compare(Convert.ToDateTime(str6), Convert.ToDateTime(dataTable.Rows[num2][1].ToString())) != 0)
                                {
                                    num1 = 2;
                                    num2++;
                                }
                                else
                                {
                                    num1 = 1;
                                    break;
                                }
                            }
                            if (num1 != 2 && num1 != 0)
                            {
                                continue;
                            }
                            str = dataTable.NewRow();
                            str["SZ_BILL_TXN_DETAIL_ID"] = "";
                            DateTime dateTime3 = Convert.ToDateTime(str6);
                            str["DT_DATE_OF_SERVICE"] = dateTime3.ToShortDateString();
                            str["SZ_PROCEDURE_ID"] = item2.Cells[1].Text.ToString();
                            str["SZ_PROCEDURAL_CODE"] = item2.Cells[3].Text.ToString();
                            str["SZ_CODE_DESCRIPTION"] = item2.Cells[4].Text.ToString();
                            str["FLT_AMOUNT"] = item2.Cells[5].Text.ToString();
                            str["I_UNIT"] = "1";
                            str["SZ_TYPE_CODE_ID"] = item2.Cells[2].Text.ToString();
                            int eventID = this.GetEventID(this.txtCompanyID.Text, this.txtCaseID.Text, this.ddlDoctor.SelectedValue.ToString(), str6);
                            str["I_EventID"] = eventID;
                            str["SZ_VISIT_TYPE"] = str7;
                            if (num != 1)
                            {
                                str["BT_IS_LIMITE"] = "0";
                            }
                            else
                            {
                                str["BT_IS_LIMITE"] = "1";
                            }
                            dataTable.Rows.Add(str);
                        }
                    }
                }
            }
            DataView dataViews = new DataView();
            dataTable.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            double num3 = 0;
            this.grdTransactionDetails.DataSource = dataTable;
            this.grdTransactionDetails.DataBind();
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO billTransactionDAO1 = new BillTransactionDAO();
                string str8 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string procID1 = billTransactionDAO1.GetProcID(this.txtCompanyID.Text, str8);
                string sLIMITE = billTransactionDAO1.GET_IS_LIMITE(this.txtCompanyID.Text, procID1);
                if (sLIMITE != "" && sLIMITE != "NULL")
                {
                    for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                    {
                        if (this.grdTransactionDetails.Items[i].Cells[5].Text != "" && this.grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;")
                        {
                            num3 = num3 + Convert.ToDouble(this.grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == this.grdTransactionDetails.Items.Count - 1)
                        {
                            BillTransactionDAO billTransactionDAO2 = new BillTransactionDAO();
                            string str9 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str10 = this.grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                            string procID2 = billTransactionDAO2.GetProcID(this.txtCompanyID.Text, str9);
                            string limit = billTransactionDAO2.GetLimit(this.txtCompanyID.Text, str10, procID2);
                            if (limit != "")
                            {
                                if (Convert.ToDouble(limit) >= num3)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = num3.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = limit;
                                }
                            }
                            num3 = 0;
                        }
                        else if (this.grdTransactionDetails.Items[i].Cells[1].Text != this.grdTransactionDetails.Items[i + 1].Cells[1].Text)
                        {
                            BillTransactionDAO billTransactionDAO3 = new BillTransactionDAO();
                            string str11 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str12 = this.grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                            string procID3 = billTransactionDAO3.GetProcID(this.txtCompanyID.Text, str11);
                            string limit1 = billTransactionDAO3.GetLimit(this.txtCompanyID.Text, str12, procID3);
                            if (limit1 != "")
                            {
                                if (Convert.ToDouble(limit1) >= num3)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = num3.ToString();
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = limit1;
                                }
                            }
                            num3 = 0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (!this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            else
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                {
                    TextBox text = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                    text.Text = this.grdTransactionDetails.Items[j].Cells[7].Text;
                }
            }
        }
        catch(Exception ex)
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

    public string CHECK_DOC_OFF(string szDocID, string szOffID, string szCompanyId)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        string str2 = "";
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SP_CHECK_DOC_AND_OFFICE", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_DOCTOR_ID", szDocID);
            command.Parameters.AddWithValue("@SZ_OFFICE_ID", szOffID);
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                str2 = reader[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str2;
    }

    public void checkForReferringFacility()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
            if (!days.checkReferringFacility(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                this.extddlReferringFacility.Visible = false;
                this.lblReferringFacility.Visible = false;
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','ddlDoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
            }
            else
            {
                this.extddlReferringFacility.Visible = true;
                this.lblReferringFacility.Visible = true;
                this.btnSave.Attributes.Add("onclick", "return formValidator('form1','ddlDoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
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

    protected void chkTransportation_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.chkTransportation.Checked)
            {
                this.extddlTransport.Visible = true;
            }
            else
            {
                this.extddlTransport.Visible = false;
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtAppointmentDate.Text = "";
            this.extddlVisitType.Text="NA";
            this.ddlTestNames.Items.Clear();
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

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender calender = new Bill_Sys_Calender();
        try
        {
            if (calender.CheckReferralExists(this.ddlDoctor.SelectedValue.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                this.ddlTestNames.DataSource = calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.ddlDoctor.SelectedValue.ToString());
                this.ddlTestNames.DataTextField = "DESCRIPTION";
                this.ddlTestNames.DataValueField = "CODE";
                this.ddlTestNames.DataBind();
                this.BindTimeControl();
                Bill_Sys_PatientVisitBO tbo = new Bill_Sys_PatientVisitBO();
                this.extddlVisitType.Text=tbo.GetDefaultVisitType(this.txtCompanyID.Text);
                this.lblVisitType.Visible = false;
                this.extddlVisitType.Visible = false;
                this.lblTIme.Visible = true;
                this.ddlHours.Visible = true;
                this.ddlMinutes.Visible = true;
                this.ddlTime.Visible = true;
                this.lblProcedure.Visible = true;
                this.ddlTestNames.Visible = true;
                this.lblVisitStatus.Visible = true;
                this.ddlStatus.Visible = true;
            }
            else
            {
                this.extddlVisitType.Text="NA";
                this.lblVisitType.Visible = true;
                this.extddlVisitType.Visible = true;
                this.lblTIme.Visible = false;
                this.ddlHours.Visible = false;
                this.ddlMinutes.Visible = false;
                this.ddlTime.Visible = false;
                this.lblProcedure.Visible = false;
                this.ddlTestNames.Visible = false;
                this.lblVisitStatus.Visible = false;
                this.ddlStatus.Visible = false;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.ddlDoctor.SelectedValue.ToString(), this.txtCompanyID.Text).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    protected void extddlReferralDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender calender = new Bill_Sys_Calender();
        try
        {
            if (calender.CheckReferralExists(this.ddlDoctor.SelectedValue.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
            {
                this.ddlTestNames.DataSource = calender.GetReferringProcCodeList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.ddlDoctor.SelectedValue.ToString());
                this.ddlTestNames.DataTextField = "DESCRIPTION";
                this.ddlTestNames.DataValueField = "CODE";
                this.ddlTestNames.DataBind();
                this.BindTimeControl();
                Bill_Sys_PatientVisitBO tbo = new Bill_Sys_PatientVisitBO();
                this.extddlVisitType.Text=tbo.GetDefaultVisitType(this.txtCompanyID.Text);
                this.lblVisitType.Visible = false;
                this.extddlVisitType.Visible = false;
                this.lblTIme.Visible = true;
                this.ddlHours.Visible = true;
                this.ddlMinutes.Visible = true;
                this.ddlTime.Visible = true;
                this.lblProcedure.Visible = true;
                this.ddlTestNames.Visible = true;
                this.lblVisitStatus.Visible = true;
                this.ddlStatus.Visible = true;
            }
            else
            {
                this.extddlVisitType.Text="NA";
                this.lblVisitType.Visible = true;
                this.extddlVisitType.Visible = true;
                this.lblTIme.Visible = false;
                this.ddlHours.Visible = false;
                this.ddlMinutes.Visible = false;
                this.ddlTime.Visible = false;
                this.lblProcedure.Visible = false;
                this.ddlTestNames.Visible = false;
                this.lblVisitStatus.Visible = false;
                this.ddlStatus.Visible = false;
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

    protected void extddlReferringFacility_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindControls();
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

    protected void extddlRoom_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindReferringProcedureCodes();
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

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            MUVGenerateFunction function = new MUVGenerateFunction();
            string str = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            Bill_Sys_NF3_Template template = new Bill_Sys_NF3_Template();
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                string str6 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                string str7 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                string str13 = configuration.getConfigurationSettings(str2, "GET_DIAG_PAGE_POSITION");
                string str14 = configuration.getConfigurationSettings(str2, "DIAG_PAGE");
                string str9 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                string str10 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                string str11 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                string str12 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                PDFValueReplacement.PDFValueReplacement replacement = new PDFValueReplacement.PDFValueReplacement();
                string str15 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str6);
                string str8 = replacement.ReplacePDFvalues(str9, str10, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                string str16 = replacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str8, str15);
                string str18 = replacement.ReplacePDFvalues(str11, str12, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                string str20 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.bt_include = function.get_bt_include(str20, str, "", "Speciality");
                string str19 = function.get_bt_include(str20, "", "WC000000000000000002", "CaseType");
                if ((this.bt_include == "True") && (str19 == "True"))
                {
                    this.str_1500 = function.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                }
                MergePDF.MergePDFFiles(template.getPhysicalPath() + str3 + str16, template.getPhysicalPath() + str3 + str18, template.getPhysicalPath() + str3 + str18.Replace(".pdf", "_MER.pdf"));
                string str17 = str18.Replace(".pdf", "_MER.pdf");
                if ((this.bt_include == "True") && (str19 == "True"))
                {
                    MergePDF.MergePDFFiles(template.getPhysicalPath() + str3 + str17, template.getPhysicalPath() + str3 + this.str_1500, template.getPhysicalPath() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                    str17 = this.str_1500.Replace(".pdf", "_MER.pdf");
                }
                string str21 = "";
                str21 = str3 + str17;
                string str22 = "";
                str22 = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + str21;
                string path = template.getPhysicalPath() + "/" + str21;
                CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                string str24 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                class2.initialize(str24);
                if ((((class2 != null) && File.Exists(path)) && ((str14 != "CI_0000003") && (template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str13 == "CK_0000003") && ((str14 != "CI_0000004") || (template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                {
                    str15 = path.Replace(".pdf", "_NewMerge.pdf");
                }
                string str25 = "";
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str21 = path.Replace(".pdf", "_New.pdf").ToString();
                }
                if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    str25 = str22.Replace(".pdf", "_NewMerge.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str22.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                }
                else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                {
                    str25 = str22.Replace(".pdf", "_New.pdf").ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str22.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                }
                else
                {
                    str25 = str22.ToString();
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str22.ToString() + "'); ", true);
                }
                this.pdfpath = str25;
                string str26 = "";
                string[] strArray = str25.Split(new char[] { '/' });
                ArrayList list = new ArrayList();
                str25 = str25.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
                str26 = strArray[strArray.Length - 1].ToString();
                if (File.Exists(template.getPhysicalPath() + str4 + str26))
                {
                    if (!Directory.Exists(template.getPhysicalPath() + str5))
                    {
                        Directory.CreateDirectory(template.getPhysicalPath() + str5);
                    }
                    File.Copy(template.getPhysicalPath() + str4 + str26, template.getPhysicalPath() + str5 + str26);
                }
                list.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                list.Add(str5 + str26);
                list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list.Add(this.Session["TM_SZ_CASE_ID"]);
                list.Add(strArray[strArray.Length - 1].ToString());
                list.Add(str5);
                list.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list.Add(str);
                list.Add("NF");
                list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                template.saveGeneratedBillPath(list);
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="BILL_GENERATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC=str26;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                this.BindLatestTransaction();
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                string str27;
                string companyName;
                Bill_Sys_PVT_Template template2 = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str28 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str30 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str31 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str32 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str27 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str27 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template2.GeneratePVTBill(flag, str27, str28, str, companyName, str30, str31, str32) + "'); ", true);
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }
            new Bill_Sys_BillTransaction_BO();
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

    public string GET_SYS_KEY_BIT(string szCompanyId)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        string str2 = "";
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GET_OFFIC_SYS_VALUE", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                str2 = reader[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str2;
    }

    public void getDoctorDefaultList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet doctorList = new Bill_Sys_DoctorBO().GetDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.ddlDoctor.DataSource = doctorList;
            ListItem item = new ListItem("---select---", "NA");
            this.ddlDoctor.DataTextField = "DESCRIPTION";
            this.ddlDoctor.DataValueField = "CODE";
            this.ddlDoctor.DataBind();
            this.ddlDoctor.Items.Insert(0, item);
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

    private int GetEventID(string _sz_company_id, string _Caseid, string _doctorid, string _eventdate)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "select i_event_id from txn_calendar_event where sz_Company_id = '" + _sz_company_id + "' and sz_doctor_id='" + _doctorid + "' and sz_Case_id ='" + _Caseid + "' and DT_EVENT_DATE='" + _eventdate + "'";
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return (int)command.ExecuteScalar();
    }

    private void GetProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable table = new DataTable();
            for (int i = 1; i <= 3; i++)
            {
                table = this._bill_Sys_BillTransaction.GetDoctorProcedureCodeList(doctorId, "TY00000000000000000" + i.ToString(), this.txtCaseID.Text).Tables[0];
                if (table.Rows.Count > 0)
                {
                    try
                    {
                        DataTable table2 = new DataTable();
                        table2.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                        table2.Columns.Add("DT_DATE_OF_SERVICE");
                        table2.Columns.Add("SZ_PROCEDURE_ID");
                        table2.Columns.Add("SZ_PROCEDURAL_CODE");
                        table2.Columns.Add("SZ_CODE_DESCRIPTION");
                        table2.Columns.Add("FLT_AMOUNT");
                        table2.Columns.Add("I_UNIT");
                        table2.Columns.Add("SZ_TYPE_CODE_ID");
                        table2.Columns.Add("FLT_GROUP_AMOUNT");
                        table2.Columns.Add("I_GROUP_AMOUNT_ID");
                        string str = "";
                        string str2 = "";
                        foreach (DataRow row2 in table.Rows)
                        {
                            str = row2["CODE"].ToString().Substring(0, row2["CODE"].ToString().IndexOf("|"));
                            str2 = row2["CODE"].ToString().Substring(row2["CODE"].ToString().IndexOf("|") + 1, row2["CODE"].ToString().Length - (row2["CODE"].ToString().IndexOf("|") + 1));
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            DataTable table3 = new DataTable();
                            table3 = this._bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, str2).Tables[0];
                            foreach (DataRow row3 in table3.Rows)
                            {
                                DataRow row = table2.NewRow();
                                row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                string str3 = row2["DESCRIPTION"].ToString().Substring(row2["DESCRIPTION"].ToString().Substring(0, row2["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, row2["DESCRIPTION"].ToString().Length - (row2["DESCRIPTION"].ToString().Substring(0, row2["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                                row["DT_DATE_OF_SERVICE"] = str3.Substring(0, str3.IndexOf("--"));
                                row["SZ_PROCEDURE_ID"] = row3["SZ_PROCEDURE_ID"];
                                row["SZ_PROCEDURAL_CODE"] = row3["SZ_PROCEDURE_CODE"];
                                row["SZ_CODE_DESCRIPTION"] = row3["SZ_CODE_DESCRIPTION"];
                                row["I_UNIT"] = "";
                                row["SZ_TYPE_CODE_ID"] = str2;
                                table2.Rows.Add(row);
                            }
                        }
                        this.grdTransactionDetails.DataSource = table2;
                        this.grdTransactionDetails.DataBind();
                    }
                    catch
                    {
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.grdProcedure.DataSource = this._bill_Sys_BillTransaction.GetDoctorSpecialityProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdProcedure.DataBind();
            this.grdGroupProcCodeService.DataSource = this._bill_Sys_BillTransaction.GetDoctorSpecialityGroupProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdGroupProcCodeService.DataBind();
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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text == "NA")
            {
                this.BindDiagnosisGrid("");
            }
            else
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
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

    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

   

    protected void lnkAddGroupService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataRow row;
            if (this.lstDiagnosisCodes.Items.Count == 0)
            {
                this.lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
                this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value, this.txtCompanyID.Text).Tables[0];
                this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                this.lstDiagnosisCodes.DataValueField = "CODE";
                this.lstDiagnosisCodes.DataBind();
                this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            }
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("I_EVENTID");
            table.Columns.Add("SZ_VISIT_TYPE");
            table.Columns.Add("BT_IS_LIMITE");
            int num = 0;
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                row = table.NewRow();
                if ((((item.Cells[1].Text.ToString() != "") && (item.Cells[1].Text.ToString() != "&nbsp;")) && ((item.Cells[3].Text.ToString() != "") && (item.Cells[3].Text.ToString() != "&nbsp;"))) && ((item.Cells[4].Text.ToString() != "") && (item.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                    if ((((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString();
                    }
                    row["SZ_TYPE_CODE_ID"] = item.Cells[8].Text.ToString();
                    BillTransactionDAO ndao = new BillTransactionDAO();
                    string str = item.Cells[2].Text.ToString();
                    string str2 = item.Cells[13].Text.ToString();
                    string procID = ndao.GetProcID(this.txtCompanyID.Text, str);
                    if (ndao.GetLimit(this.txtCompanyID.Text, str2, procID) != "")
                    {
                        row["FLT_GROUP_AMOUNT"] = "";
                    }
                    else
                    {
                        row["FLT_GROUP_AMOUNT"] = item.Cells[9].Text.ToString();
                    }
                    row["I_GROUP_AMOUNT_ID"] = item.Cells[10].Text.ToString();
                    row["I_EventID"] = item.Cells[12].Text.ToString();
                    row["SZ_VISIT_TYPE"] = item.Cells[13].Text.ToString();
                    row["BT_IS_LIMITE"] = item.Cells[14].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.txtGroupDateofService.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") == "1")
            {
                foreach (DataGridItem item2 in this.grdAllReports.Items)
                {
                    if (((CheckBox)item2.Cells[0].FindControl("chkselect")).Checked)
                    {
                        string str6 = item2.Cells[5].Text.ToString();
                        foreach (DataGridItem item3 in this.grdGroupProcCodeService.Items)
                        {
                            CheckBox box = (CheckBox)item3.FindControl("chkselect");
                            CheckBox box1 = (CheckBox)item2.FindControl("chkLimit");
                            num = 1;
                            if (box.Checked)
                            {
                                DataSet set = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item3.Cells[1].Text.ToString(), this.txtCompanyID.Text, item3.Cells[2].Text.ToString());
                                int num2 = 1;
                                foreach (DataRow row2 in set.Tables[0].Rows)
                                {
                                    row = table.NewRow();
                                    row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item2.Cells[2].Text.ToString()).ToShortDateString();
                                    row["SZ_PROCEDURE_ID"] = row2.ItemArray.GetValue(0);
                                    row["SZ_PROCEDURAL_CODE"] = row2.ItemArray.GetValue(2);
                                    row["SZ_CODE_DESCRIPTION"] = row2.ItemArray.GetValue(3);
                                    row["FLT_AMOUNT"] = row2.ItemArray.GetValue(4);
                                    row["I_UNIT"] = "1";
                                    row["SZ_TYPE_CODE_ID"] = row2.ItemArray.GetValue(1);
                                    if ((num2 == set.Tables[0].Rows.Count) && (item3.Cells[4].Text.ToString() != ""))
                                    {
                                        row["FLT_GROUP_AMOUNT"] = item3.Cells[4].Text.ToString();
                                    }
                                    if (item3.Cells[3].Text.ToString() != "")
                                    {
                                        row["I_GROUP_AMOUNT_ID"] = item3.Cells[3].Text.ToString();
                                    }
                                    row["I_EventID"] = item2.Cells[4].Text.ToString();
                                    if (num == 1)
                                    {
                                        row["BT_IS_LIMITE"] = "1";
                                    }
                                    else
                                    {
                                        row["BT_IS_LIMITE"] = "0";
                                    }
                                    row["SZ_VISIT_TYPE"] = str6;
                                    table.Rows.Add(row);
                                    num2++;
                                }
                                continue;
                            }
                        }
                        continue;
                    }
                }
                if ((this.grdAllReports.Items.Count == 0) && (this.Session["CreateBill"] != null))
                {
                    this.billAppointmetDate = (ArrayList)this.Session["CreateBill"];
                    if (this.billAppointmetDate.Count > 0)
                    {
                        foreach (string str7 in this.billAppointmetDate)
                        {
                            string str8 = str7;
                            foreach (DataGridItem item4 in this.grdGroupProcCodeService.Items)
                            {
                                CheckBox box2 = (CheckBox)item4.FindControl("chkselect");
                                num = 1;
                                if (box2.Checked)
                                {
                                    DataSet set2 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item4.Cells[1].Text.ToString(), this.txtCompanyID.Text, item4.Cells[2].Text.ToString());
                                    int num3 = 1;
                                    foreach (DataRow row3 in set2.Tables[0].Rows)
                                    {
                                        row = table.NewRow();
                                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                        row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(str8).ToShortDateString();
                                        row["SZ_PROCEDURE_ID"] = row3.ItemArray.GetValue(0);
                                        row["SZ_PROCEDURAL_CODE"] = row3.ItemArray.GetValue(2);
                                        row["SZ_CODE_DESCRIPTION"] = row3.ItemArray.GetValue(3);
                                        row["FLT_AMOUNT"] = row3.ItemArray.GetValue(4);
                                        row["I_UNIT"] = "1";
                                        row["SZ_TYPE_CODE_ID"] = row3.ItemArray.GetValue(1);
                                        if ((num3 == set2.Tables[0].Rows.Count) && (item4.Cells[4].Text.ToString() != ""))
                                        {
                                            row["FLT_GROUP_AMOUNT"] = item4.Cells[4].Text.ToString();
                                        }
                                        if (item4.Cells[3].Text.ToString() != "")
                                        {
                                            row["I_GROUP_AMOUNT_ID"] = item4.Cells[3].Text.ToString();
                                        }
                                        int num4 = this.GetEventID(this.txtCompanyID.Text, this.txtCaseID.Text, this.ddlDoctor.SelectedValue.ToString(), str8);
                                        row["I_EventID"] = num4;
                                        row["SZ_VISIT_TYPE"] = this.extddlVisitType.Selected_Text.ToString();
                                        if (num == 1)
                                        {
                                            row["BT_IS_LIMITE"] = "1";
                                        }
                                        else
                                        {
                                            row["BT_IS_LIMITE"] = "0";
                                        }
                                        table.Rows.Add(row);
                                        num3++;
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem item5 in this.grdGroupProcCodeService.Items)
                {
                    CheckBox box3 = (CheckBox)item5.FindControl("chkselect");
                    if (box3.Checked)
                    {
                        DataSet set3 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item5.Cells[1].Text.ToString(), this.txtCompanyID.Text, item5.Cells[2].Text.ToString());
                        int num5 = 1;
                        foreach (DataRow row4 in set3.Tables[0].Rows)
                        {
                            row = table.NewRow();
                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                            row["DT_DATE_OF_SERVICE"] = this.txtBillDate.Text;
                            row["SZ_PROCEDURE_ID"] = row4.ItemArray.GetValue(0);
                            row["SZ_PROCEDURAL_CODE"] = row4.ItemArray.GetValue(2);
                            row["SZ_CODE_DESCRIPTION"] = row4.ItemArray.GetValue(3);
                            row["FLT_AMOUNT"] = row4.ItemArray.GetValue(4);
                            row["I_UNIT"] = "1";
                            row["SZ_TYPE_CODE_ID"] = row4.ItemArray.GetValue(1);
                            if ((num5 == set3.Tables[0].Rows.Count) && (item5.Cells[4].Text.ToString() != ""))
                            {
                                row["FLT_GROUP_AMOUNT"] = item5.Cells[4].Text.ToString();
                            }
                            if (item5.Cells[3].Text.ToString() != "")
                            {
                                row["I_GROUP_AMOUNT_ID"] = item5.Cells[3].Text.ToString();
                            }
                            table.Rows.Add(row);
                            num5++;
                        }
                        continue;
                    }
                }
            }
            DataView view = new DataView();
            table.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            double num6 = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao2 = new BillTransactionDAO();
                string str9 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[13].Text.ToString();
                string str10 = ndao2.GetProcID(this.txtCompanyID.Text, str9);
                string str11 = ndao2.GET_IS_LIMITE(this.txtCompanyID.Text, str10);
                if ((str11 != "") && (str11 != "NULL"))
                {
                    for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                    {
                        if ((this.grdTransactionDetails.Items[i].Cells[5].Text != "") && (this.grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;"))
                        {
                            num6 += Convert.ToDouble(this.grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str12 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str13 = this.grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                            string str14 = ndao3.GetProcID(this.txtCompanyID.Text, str12);
                            string str15 = ndao3.GetLimit(this.txtCompanyID.Text, str13, str14);
                            if (str15 != "")
                            {
                                if (Convert.ToDouble(str15) < num6)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = str15;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = num6.ToString();
                                }
                            }
                            num6 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[i].Cells[1].Text != this.grdTransactionDetails.Items[i + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao4 = new BillTransactionDAO();
                            string str16 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str17 = this.grdTransactionDetails.Items[i].Cells[13].Text.ToString();
                            string str18 = ndao4.GetProcID(this.txtCompanyID.Text, str16);
                            string str19 = ndao4.GetLimit(this.txtCompanyID.Text, str17, str18);
                            if (str19 != "")
                            {
                                if (Convert.ToDouble(str19) < num6)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = str19;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[9].Text = num6.ToString();
                                }
                            }
                            num6 = 0.0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                {
                    TextBox box4 = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                    box4.Text = this.grdTransactionDetails.Items[j].Cells[7].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
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

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < this.lstDiagnosisCodes.Items.Count; i++)
            {
                if (this.lstDiagnosisCodes.Items[i].Selected)
                {
                    this.lstDiagnosisCodes.Items.Remove(this.lstDiagnosisCodes.Items[i]);
                    i--;
                }
            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        string pGID = "";
        this.GetSpecialty = new Bill_Sys_Upload_VisitReport();
        if (this.txtprocid.Text == "")
        {
            this.txtprocid.Text = this.GetSpecialty.GetDoctorSpecialty(this.txtDoctid.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
        }
        ArrayList list = (ArrayList)this.Session["CreateBill"];
        string text = this.txtvisittype.Text;
        foreach (string str3 in list)
        {
            string eventID = Convert.ToString(this.GetEventID(this.txtCompanyID.Text, this.txtCaseID.Text, this.txtDoctid.Text, str3));
            string str5 = "";
            switch (text)
            {
                case "FU":
                    str5 = "Followup Report";
                    break;

                case "IE":
                    str5 = "Initial Report";
                    break;
            }
            if (str5 != "")
            {
                string szNODETYPESCAN = this.GetSpecialty.GetNodeType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(), str5, this.txtprocid.Text);
                if (szNODETYPESCAN != "")
                {
                    this.RedirectToScanApp(szNODETYPESCAN, eventID, pGID);
                }
                else
                {
                    this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Please set uplaod document nodetype')</script>");
                }
                continue;
            }
            this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Report Upload is not allowed')</script>");
        }
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
            this.Button2.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            this.btnSave.Attributes.Add("onclick", "return formValidator('form1','ddlDoctor,txtAppointmentDate,extddlVisitType,ddlHours');");
            if (!base.IsPostBack)
            {
                this.dset = new DataSet();
                this.Session["CreateBill"] = null;
                this.objCaseDetails = new CaseDetailsBO();
                string str = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                this.txtCaseID.Text = str;
                string str2 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                this.objCaseDetails.GetPatientLocationID(str, str2);
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlVisitType.Flag_ID=this.txtCompanyID.Text;
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.checkForReferringFacility();
                this.extddlDiagnosisType.Flag_ID=this.txtCompanyID.Text;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                {
                    hdnFacility.Value = "True";
                    this.lblOffice.Visible = true;
                    this.extddlMedicalOffice.Visible = true;
                    this.extddlRoom.Flag_Key_Value="REFERRING_ROOM_LIST";
                    this.extddlRoom.Flag_ID=this.txtCompanyID.Text;
                    this.extddlReferringFacility.Flag_ID=this.txtCompanyID.Text.ToString();
                    this.extddlMedicalOffice.Flag_ID=this.txtCompanyID.Text.ToString();
                    this.objPatientBO = new Bill_Sys_PatientBO();
                    string str3 = this.objPatientBO.Get_PatientOfficeID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, this.txtCompanyID.Text);
                    if (!(this.GET_SYS_KEY_BIT(this.txtCompanyID.Text) == "1") && (str3 != ""))
                    {
                        this.extddlMedicalOffice.Text=str3;
                        this.extddlMedicalOffice.Enabled = false;
                    }
                    this.BindReferringFacilityControls();
                }
                else
                {
                    hdnFacility.Value = "False";
                    this.lblOffice.Visible = false;
                    this.extddlMedicalOffice.Visible = false;
                    this.extddlRoom.Flag_ID=this.txtCompanyID.Text;
                    this.extddlReferringFacility.Flag_ID=this.txtCompanyID.Text.ToString();
                    this.BindControls();
                }
                hdnCaseID.Value= ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                hdnCaseNo.Value= ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                ArrayList claimInsurance = this._bill_Sys_BillTransaction.GetClaimInsurance(this.txtCaseID.Text);
                if (claimInsurance.Count > 0)
                {
                    if (((claimInsurance[0].ToString() != "") && (claimInsurance[0].ToString() != "NA")) && ((claimInsurance[2].ToString() != "") && (claimInsurance[2].ToString() != "NA")))
                    {
                        if ((claimInsurance[1].ToString() != "") && (claimInsurance[1].ToString() != ""))
                        {
                            this.txtClaimInsurance.Text = "3";
                        }
                        else
                        {
                            this.txtClaimInsurance.Text = "2";
                        }
                    }
                    else
                    {
                        this.txtClaimInsurance.Text = "1";
                    }
                }
                else
                {
                    this.txtClaimInsurance.Text = "0";
                }
                Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
                if (n_bo.checkNf2(this.txtCompanyID.Text, this.txtCaseID.Text))
                {
                    this.txtNf2.Text = "1";
                }
                else
                {
                    this.txtNf2.Text = "0";
                }
            }
            this.lblMsg.Text = "";
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_PopupNewVisit.aspx");
        }
        this.extddlMedicalOffice.Enabled = true;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void RedirectToScanApp(string szNODETYPESCAN, string EventID, string PGID)
    {
        string str = ConfigurationManager.AppSettings["webscanurl"].ToString();
        string str2 = new Bill_Sys_BillTransaction_BO().GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNODETYPESCAN);
        str = str + "&Flag=UploadDoc&CompanyId=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + (str + "&CaseNo=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + str2 + "&BillNo=" + EventID + "&UserId=" + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID + "&StatusID=" + PGID) + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    private void SaveEvents()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Calender calender = new Bill_Sys_Calender();
            ArrayList list = new ArrayList();
            list.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            list.Add(this.ddlDoctor.SelectedValue.ToString());
            list.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            int eventID = calender.GetEventID(list);
            if (this.ddlTestNames.Visible)
            {
                this.objAdd = new ArrayList();
                this.objAdd.Add(eventID);
                this.objAdd.Add(false);
                this.objAdd.Add(2);
                calender.UPDATE_Event_Status(this.objAdd);
                foreach (ListItem item in this.ddlTestNames.Items)
                {
                    if (item.Selected)
                    {
                        this.objAdd = new ArrayList();
                        this.objAdd.Add(item.Value);
                        this.objAdd.Add(eventID);
                        this.objAdd.Add(2);
                        calender.Save_Event_RefferPrcedure(this.objAdd);
                    }
                }
            }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="APPOINTMENT_ADDED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC="Date : " + this.txtAppointmentDate.Text;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this._DAO_NOTES_EO.SZ_COMPANY_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
            this.ClearControl();
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_SysPatientDesk.aspx';window.self.close(); </script>");
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

    protected void saveReferringFacility()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool flag = false;
        log.Debug("Now we are in saveReferringFacility() method.");
        this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        string str = "";
        string str2 = "";
        ArrayList list = new ArrayList();
        try
        {
            string[] strArray = this.txtAppointmentDate.Text.Split(new char[] { ',' });
            log.Debug("Split the multiple Appointment Date into single dates.  ");
            string[] strArray2 = strArray;
            for (int i = 0; i < strArray2.Length; i++)
            {
                object obj2 = strArray2[i];
                if (obj2.ToString() != "")
                {
                    log.Debug("Current date = " + this.hdnCurrentDate.Value);
                    log.Debug("Client date = " + Convert.ToDateTime(this.hdnCurrentDate.Value));
                    DateTime time2 = Convert.ToDateTime(Convert.ToDateTime(obj2).ToString("MM/dd/yyyy") + " " + this.ddlHours.SelectedValue + ":" + this.ddlMinutes.SelectedValue + " " + this.ddlTime.SelectedValue);
                    log.Debug("Entered date = " + time2);
                    DateTime time3 = time2.AddMinutes(30.0);
                    log.Debug("EnteredLast date = " + time3);
                    log.Debug("Check For Room Days and Time");
                    decimal num = 0.00M;
                    decimal num2 = 0.00M;
                    if ((this.ddlTime.SelectedValue == "PM") && (Convert.ToInt32(this.ddlHours.SelectedValue) < 12))
                    {
                        log.Debug("If ddltime(start time type=PM) and ddlHours<12");
                        num = (12.00M + Convert.ToDecimal(this.ddlHours.SelectedValue.ToString())) + Convert.ToDecimal("." + this.ddlMinutes.SelectedValue.ToString());
                        log.Debug("Start time = " + num);
                    }
                    else
                    {
                        log.Debug("If ddltime(start time type is not PM) or ddlHours > 12");
                        num = Convert.ToDecimal(this.ddlHours.SelectedValue.ToString()) + Convert.ToDecimal("." + this.ddlMinutes.SelectedValue.ToString());
                        log.Debug("Start time = " + num);
                    }
                    if ((time3.ToString("tt") == "PM") && (Convert.ToInt32(time3.ToString("hh")) < 12))
                    {
                        log.Debug("If EnteredLastDate(end time type=PM) and (Convert.ToInt32(dtEnteredLastDate.ToString('hh')) < 12)");
                        num2 = 12M + Convert.ToDecimal(time3.ToString("hh") + "." + time3.ToString("mm"));
                        log.Debug("End time = " + num2);
                    }
                    else
                    {
                        log.Debug("If EnteredLastDate(end time type is not PM) or(Convert.ToInt32(dtEnteredLastDate.ToString('hh')) > 12)");
                        num2 = Convert.ToDecimal(time3.ToString("hh") + "." + time3.ToString("mm"));
                        log.Debug("End time = " + num2);
                    }
                    Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
                    log.Debug("Create object of Bill_Sys_RoomDays page.  ");
                    ArrayList list2 = new ArrayList();
                    list2.Add(this.extddlRoom.Text);
                    hdnRoomId.Value = extddlRoom.Text;
                    list2.Add(Convert.ToDateTime(obj2).ToString("MM/dd/yyyy"));
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                    {
                        log.Debug("If the Company is referringFacility Company.");
                        list2.Add(this.txtCompanyID.Text);
                        log.Debug(" referringFacility Company = " + this.txtCompanyID.Text);
                    }
                    else
                    {
                        log.Debug("If the Company is not referringFacility Company.");
                        list2.Add(this.extddlReferringFacility.Text);
                        log.Debug(" Adding from extended ddl ,referringFacility Company = " + this.extddlReferringFacility.Text);
                    }
                    list2.Add(num);
                    log.Debug("StartTime = " + num);
                    list2.Add(num2);
                    log.Debug("EndTime = " + num2);
                    if (!days.checkRoomTiming(list2))
                    {
                        log.Debug("If the method checkRoomTiming(objChekList) return false,Procedure Name = SP_CHECK_ROOM_DAY_TIME");
                        list.Add(Convert.ToDateTime(obj2).ToString("MM/dd/yyyy"));
                        str = str + Convert.ToDateTime(obj2).ToString("MM/dd/yyyy") + " , ";
                        log.Debug("szError and (continue) = " + str);
                    }
                    else
                    {
                        string str4 = "";
                        if (flag)
                        {
                            Bill_Sys_ReferalEvent event2 = new Bill_Sys_ReferalEvent();
                            ArrayList list3 = new ArrayList();
                            list3.Add(this.ddlDoctor.Text);
                            list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                            list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            str4 = event2.AddDoctor(list3);
                            foreach (ListItem item in this.ddlTestNames.Items)
                            {
                                if (item.Selected)
                                {
                                    list3 = new ArrayList();
                                    list3.Add(str4);
                                    list3.Add(item.Value);
                                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list3.Add(item.Value);
                                    event2.AddDoctorAmount(list3);
                                    list3 = new ArrayList();
                                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                                    list3.Add(str4);
                                    list3.Add(Convert.ToDateTime(obj2).ToString("MM/dd/yyyy"));
                                    list3.Add(item.Value);
                                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list3.Add(this.ddlType.SelectedValue);
                                    event2.AddPatientProc(list3);
                                }
                            }
                        }
                        else
                        {
                            str4 = this.extddlReferringDoctor.SelectedItem.Value;
                        }
                        log.Debug("Start #region Save Event");
                        string str5 = Convert.ToDateTime(obj2).ToString("MM/dd/yyyy");
                        Bill_Sys_Calender calender = new Bill_Sys_Calender();
                        log.Debug("Create object of Bill_Sys_Calender as _bill_Sys_Calender");
                        ArrayList list4 = new ArrayList();
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                        log.Debug("Patient ID(from session) = " + ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID);
                        list4.Add(str5);
                        log.Debug("Event date = " + str5);
                        list4.Add(this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString());
                        log.Debug("Selected time hours.selected time minute = " + this.ddlHours.SelectedValue.ToString() + "." + this.ddlMinutes.SelectedValue.ToString());
                        list4.Add(this.txtNotes.Text);
                        log.Debug("txtNotes = " + this.txtNotes.Text);
                        list4.Add(str4);
                        log.Debug("szDoctorID = " + str4);
                        list4.Add(this.ddlTestNames.SelectedValue);
                        log.Debug("ddlTestNames = " + this.ddlTestNames.SelectedValue);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        log.Debug("COMPANY ID = " + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.ddlTime.SelectedValue);
                        log.Debug("ddlTime = " + this.ddlTime.SelectedValue);
                        list4.Add(time3.ToString("hh") + "." + time3.ToString("mm"));
                        log.Debug("Entered LastDate time hours.Entered LastDate time minute = " + time3.ToString("hh") + "." + time3.ToString("mm"));
                        list4.Add(time3.ToString("tt"));
                        log.Debug(" Entered LastDate = " + time3.ToString("tt"));
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            log.Debug("If the Company is referring Facility Company.");
                            list4.Add(this.txtCompanyID.Text);
                            log.Debug(" referringFacility Company = " + this.txtCompanyID.Text);
                        }
                        else
                        {
                            log.Debug("If the Company is not referring Facility Company.");
                            list4.Add(this.extddlReferringFacility.Text);
                            log.Debug(" Adding from extended ddl ,referringFacility Company = " + this.extddlReferringFacility.Text);
                        }
                        list4.Add("False");
                        if (this.chkTransportation.Checked)
                        {
                            list4.Add(1);
                        }
                        else
                        {
                            list4.Add(0);
                        }
                        if (this.chkTransportation.Checked)
                        {
                            list4.Add(Convert.ToInt32(this.extddlTransport.Text));
                        }
                        else
                        {
                            list4.Add(null);
                        }
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            list4.Add(this.extddlMedicalOffice.Text);
                        }
                        log.Debug("go to the method Save_Event()of bill_Sys_Calender .");
                        int num3 = calender.Save_Event(list4, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        hdnEventID.Value = num3.ToString();
                        log.Debug("return eventID = " + num3);
                        foreach (ListItem item2 in this.ddlTestNames.Items)
                        {
                            if (item2.Selected)
                            {
                                list4 = new ArrayList();
                                list4.Add(item2.Value);
                                list4.Add(num3);
                                list4.Add(0);
                                calender.Save_Event_RefferPrcedure(list4);
                            }
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE="APPOINTMENT_ADDED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC="Date : " + str5;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID=((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                }
            }
            if ((str == "") && (str2 == ""))
            {
                this.lblMsg.Text = "Appointment scheduled successfully.";
                this.ClearControl();
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='../Bill_SysPatientDesk.aspx';window.self.close(); </script>");
            }
            else
            {
                if (list != null)
                {
                    Bill_Sys_RoomDays days2 = new Bill_Sys_RoomDays();
                    this.lblMsg.Text = " Add appointment between ---> ";
                    for (int j = 0; j < list.Count; j++)
                    {
                        string str6 = "";
                        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
                        {
                            str6 = days2.getRoomStart_EndTime(this.extddlRoom.Text, list[j].ToString(), this.txtCompanyID.Text);
                        }
                        else
                        {
                            str6 = days2.getRoomStart_EndTime(this.extddlRoom.Text, list[j].ToString(), this.extddlReferringFacility.Text);
                        }
                        if (str6 == "")
                        {
                            this.lblMsg.Text = this.lblMsg.Text + "<br/> " + list[j].ToString() + " --- Holiday";
                        }
                        else
                        {
                            this.lblMsg.Text = this.lblMsg.Text + "<br/> " + list[j].ToString() + " --- " + str6;
                        }
                    }
                }
                if (str2 != "")
                {
                    this.lblMsg.Text = this.lblMsg.Text + "</br>" + str2 + " : Cannot add schedule on Previous Date and Time";
                }
                this.lblMsg.Focus();
                this.lblMsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void showModalPopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string doctorId = "";
            new ArrayList();
            new DataSet();
            new Bill_Sys_Visit_BO().GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            doctorId = this.ddlDoctor.SelectedValue.ToString();
            if (doctorId != "")
            {
                this.GetProcedureCode(doctorId);
                this.hndDoctorID.Value = doctorId;
            }
            else
            {
                this.GetProcedureCode(this.hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    TextBox box = (TextBox)this.grdTransactionDetails.Items[i].FindControl("txtUnit");
                    box.Text = this.grdTransactionDetails.Items[i].Cells[7].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            if (this.hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupAddservice.Show();
            }
            else if (this.hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupaddgroup.Show();
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

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.txtprocid.Text == "")
            {
                this.GetSpecialty = new Bill_Sys_Upload_VisitReport();
                this.txtprocid.Text = this.GetSpecialty.GetDoctorSpecialty(this.txtDoctid.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
            }
            if (((this.txtDoctid.Text != "") && (this.txtvisittype.Text != "")) && (this.txtprocid.Text != ""))
            {
                Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
                if (this.ReportUpload.HasFile)
                {
                    ArrayList list = (ArrayList)this.Session["CreateBill"];
                    foreach (string str in list)
                    {
                        this.txteventid.Text = Convert.ToString(this.GetEventID(this.txtCompanyID.Text, this.txtCaseID.Text, this.txtDoctid.Text, str));
                        ArrayList list2 = new ArrayList();
                        list2.Add(this.txtCaseID.Text);
                        list2.Add(this.txtCompanyID.Text);
                        list2.Add(this.txtDoctid.Text);
                        list2.Add(this.txtvisittype.Text);
                        list2.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString());
                        list2.Add(this.ReportUpload.FileName);
                        list2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                        list2.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                        list2.Add(this.txteventid.Text);
                        list2.Add(this.txtprocid.Text);
                        string path = report.Upload_Report_For_Visit(list2);
                        if (path != "Failed")
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            this.ReportUpload.SaveAs(path + this.ReportUpload.FileName);
                            this.Msglbl.Text = "Document Saved Successfully";
                            continue;
                        }
                        this.Msglbl.Text = "Unable to save the Document";
                    }
                }
                else
                {
                    this.Msglbl.Text = "No File Selected";
                }
            }
            else
            {
                this.Msglbl.Text = "Doctor or VisitType unknown ";
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string StrDGCodes = (hndSeletedDGCodes.Value.Replace("--,", "*")).Replace("--", "");
            string[] ArrCodes = StrDGCodes.Split('*');

            for (int i = 0; i < ArrCodes.Length; i++)
            {
                string[] arrInsert = ArrCodes[i].Split('~');
                ListItem item = new ListItem(arrInsert[1], arrInsert[2]);
                if (!this.lstDiagnosisCodes.Items.Contains(item))
                {
                    this.lstDiagnosisCodes.Items.Add(item);
                }

            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

