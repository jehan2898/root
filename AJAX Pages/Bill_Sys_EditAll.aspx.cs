using AjaxControlToolkit;
using ExtendedDropDownList;
using log4net;
using ProxyLibrary;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_EditAll : PageBase
{
    private ArrayList _arrayList;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private string _CompanyID = "";
    private Bill_Sys_DeleteBO _deleteBO = new Bill_Sys_DeleteBO();
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private DataSet _ds;
    private DataTable _patientGrid_dt = new DataTable();
    //protected Button btnAdd;
    //protected Button btnAssign;
    //protected Button btnDeassociateDiagCode;
    //protected Button btndelete;
    //protected Button btnopen;
    //protected Button btnSeacrh;
    //protected Button btnUPdate;
    //protected Button btnUpdateSpec;
    //protected Button btnView;
    private string caseid = "";
    //protected CheckBox chkClose;
    //protected DataGrid dgAudit;
    private string EventProcID = "";
    //protected ExtendedDropDownList.ExtendedDropDownList extddlCoSignedby;
    //protected ExtendedDropDownList.ExtendedDropDownList extddlDiagnosisType;
    //protected ExtendedDropDownList.ExtendedDropDownList extddlReadingDoctor;
    //protected ExtendedDropDownList.ExtendedDropDownList extddlSpecialityDia;
    //protected ExtendedDropDownList.ExtendedDropDownList extddlSpecialityUp;
    //protected ExtendedDropDownList.ExtendedDropDownList extddlSpecialty;
    //protected HtmlForm form1;
    //protected DataGrid grdAssignedDiagnosisCode;
    //protected DataGrid grdDelete;
    //protected DataGrid grdNormalDgCode;
    //protected DataGrid grdProCode;
    //protected DataGrid grdSelectedDiagnosisCode;
    //protected DataGrid grdViewDocuments;
    //protected HtmlHead Head1;
    //protected Label lblDiagnosisCount;
    //protected LinkButton lnkNext;
    //protected LinkButton lnkPrevious;
    //protected LinkButton lnkunassociatedoc;
    private static ILog log = LogManager.GetLogger("EditAll");
    //protected UserControl_ErrorMessageControl MessageControl1;
    private Bill_Sys_NF3_Template objNF3Template;
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;
    private Bill_Sys_UserObject objSessionUser;
    private Bill_Sys_SystemObject objSystemObject;
    //protected UpdatePanel ReportUpdate;
    //protected ScriptManager ScriptManager1;
    private string strCaseType = "";
    private string strLinkPath;
    private string szdescrption = "";
    private string szProceduregroup_id = "";
    private string szRoomId = "";
    private string szSpeciality = "";
    //protected TabContainer tabcontainerDiagnosisCode;
    //protected TabContainer tabcontainerprocedurecodedocs;
    //protected HtmlTableRow table_row_specialty_drpdwn;
    //protected TabPanel tabpnlAssociate;
    //protected TabPanel tabpnlDeassociate;
    //protected TabPanel tabpnldelte;
    //protected TabPanel tabpnlDignosiscode;
    //protected TabPanel tabpnlprocedurecodedocs;
    //protected TabPanel tabpnlSpeciality;
    //protected HtmlTable tblDiagnosisCodeFirst;
    //protected HtmlTableCell Td1;
    //protected HtmlTableCell Td2;
    //protected HtmlTableCell Td3;
    //protected HtmlTableCell Td4;
    //protected HtmlTableCell Td5;
    //protected HtmlTableCell Td6;
    //protected HtmlTableCell Td8;
    //protected TextBox TextBox1;
    //protected TextBox TextBox2;
    //protected TextBox TextBox3;
    //protected TabPanel tpAudit;
    //protected HtmlTableRow Tr1;
    //protected HtmlTableRow Tr2;
    //protected HtmlTableRow trDoctorType;
    //protected TextBox txtCaseID;
    //protected TextBox txtcode;
    //protected TextBox txtCompanyID;
    //protected TextBox txtdesc;
    //protected TextBox txtDescription;
    //protected TextBox txtDiagnosisSetID;
    //protected TextBox txtDiagonosisCode;
    //protected TextBox txtEventProcId;
    //protected TextBox txtProcDesc;
    //protected TextBox txtProcGroupId;
    //protected TextBox txtprocid;
    //protected TextBox txtSpecility;
    //protected TextBox txtUserId;
    //protected TextBox txtUserName;
    //protected TextBox txtUserRole;
    //protected UpdatePanel UpdatePanel1;
    //protected UpdatePanel UpdatePanel10;
    //protected UpdatePanel UpdatePanel2;
    //protected UpdatePanel UpdatePanel3;
    //protected UpdatePanel UpdatePanel4;
    //protected UpdatePanel UpdatePanel6;
    //protected UpdateProgress UpdateProgress1;
    //protected UpdateProgress UpdateProgress12;
    //protected UserControl_ErrorMessageControl usrMessage;
    //protected UserControl_ErrorMessageControl usrMessage1;

    private void BindGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdNormalDgCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
            this.grdNormalDgCode.DataBind();
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

    private void bindProcGrid()
    {
        new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        DataSet procedureCode = new LHRProcedureCode().GetProcedureCode(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        procedureCode.Tables[0].DefaultView.RowFilter = "sz_procedure_group_id='" + this.extddlSpecialty.Text+ "'";
        this.grdProCode.DataSource = procedureCode.Tables[0].DefaultView.ToTable();
        this.grdProCode.DataBind();
        if (!this.txtdesc.Text.ToLower().Contains("unknown"))
        {
            for (int i = 0; i < this.grdProCode.Items.Count; i++)
            {
                if (this.grdProCode.Items[i].Cells[1].Text.ToString().ToUpper() == (this.txtcode.Text + "-" + this.txtdesc.Text).ToUpper())
                {
                    CheckBox box = (CheckBox)this.grdProCode.Items[i].FindControl("chkSelectProc");
                    box.Checked = true;
                    return;
                }
            }
        }
    }

    private void bindProcGridOnUpdateSpecialty()
    {
        this.table_row_specialty_drpdwn.Visible = false;
        this.extddlSpecialityDia.Text=this.extddlSpecialityUp.Text;
        new Bill_Sys_ManageVisitsTreatmentsTests_BO();
        DataSet procedureCode = new LHRProcedureCode().GetProcedureCode(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        procedureCode.Tables[0].DefaultView.RowFilter = "sz_procedure_group_id='" + this.extddlSpecialityUp.Text + "'";
        this.grdProCode.DataSource = procedureCode.Tables[0].DefaultView.ToTable();
        this.grdProCode.DataBind();
        if (!this.txtdesc.Text.ToLower().Contains("unknown"))
        {
            for (int i = 0; i < this.grdProCode.Items.Count; i++)
            {
                if (this.grdProCode.Items[i].Cells[1].Text.ToString().ToUpper() == (this.txtcode.Text + "-" + this.txtdesc.Text).ToUpper())
                {
                    CheckBox box = (CheckBox)this.grdProCode.Items[i].FindControl("chkSelectProc");
                    box.Checked = true;
                    return;
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            bool flag1 = this.txtCompanyID.Text == "CO000000000000000081";
            if (this.table_row_specialty_drpdwn.Visible)
            {
                if (this.extddlSpecialty.Text == "NA")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds10sl435", "alert('Please select a Specialty');", true);
                    return;
                }
                new Bill_Sys_CheckoutBO().UpdateProc(txt_event_ProcID.Text.ToString(), this.extddlSpecialty.Text);
                ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).ProceuderGroupId=this.extddlSpecialty.Text;
                this.txtSpecility.Text = this.extddlSpecialty.Selected_Text;
                this.SaveProcedureDiagnosisCode();
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds10sl435a", "alert('Diagnosis Code associated successfully');", true);
            }
            else
            {
                this.SaveProcedureDiagnosisCode();
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds10sl435q", "alert('Diagnosis Code associated successfully');", true);
            }
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }

    protected void btnDeassociateDiagCode_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            foreach (DataGridItem item in this.grdAssignedDiagnosisCode.Items)
            {
                CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelect");
                if (box.Checked)
                {
                    this._arrayList = new ArrayList();
                    this._arrayList.Add(item.Cells[1].Text.ToString());
                    this._arrayList.Add(this.txtCaseID.Text);
                    this._arrayList.Add(this.txtCompanyID.Text);
                    if (!this._associateDiagnosisCodeBO.DeleteAssociateProcedureDiagonisCode(this._arrayList))
                    {
                        str = str + "  " + item.Cells[2].Text.ToString() + ",";
                    }
                }
            }
            bool flag1 = str == "";
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
            this.grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
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

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug("Start Delete button event.");
        string str = "";
        DataSet imageIDLhr = new DataSet();
        for (int i = 0; i < this.grdDelete.Items.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdDelete.Items[i].FindControl("chkView");
            if (box.Checked)
            {
                try
                {
                    string text = this.grdDelete.Items[i].Cells[3].Text;
                    string str3 = this.txtEventProcId.Text;
                    imageIDLhr = this._deleteBO.GetImageIDLhr(str3, text);
                    string str4 = ConfigurationManager.AppSettings["MiscCommanFolder"].ToString() + @"\" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + @"\" + this.txtCaseID.Text + @"\" + ConfigurationManager.AppSettings["MiscFolder"].ToString() + @"\";
                    string str5 = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                    string str6 = this.grdDelete.Items[i].Cells[2].Text;
                    string sourceDirName = (str5 + str6 + text).Replace("/", @"\");
                    log.Debug("filename : " + text);
                    log.Debug("szEventProcId :" + str3);
                    log.Debug("szMISCpath :" + str4);
                    new DataSet();
                    if (((text != "") && (str3 != "")) && (str4 != ""))
                    {
                        if (imageIDLhr.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; imageIDLhr.Tables[0].Rows.Count > j; j++)
                            {
                                str = imageIDLhr.Tables[0].Rows[j][0].ToString();
                                log.Debug("szImageID :" + str);
                                if (this._deleteBO.DeleteDocuments(str3, text, str, str4).Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                {
                                    Directory.Move(sourceDirName, sourceDirName + ".deleted");
                                    log.Debug("dsResult : SUCCESS");
                                    this.MessageControl2.PutMessage("File successfully Deleted.");
                                    this.MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                                    this.MessageControl2.Show();
                                }
                            }
                        }
                        else if (this._deleteBO.DeleteDocuments(str3, text, str, str4).Tables[0].Rows[0][0].ToString() == "SUCCESS")
                        {
                            Directory.Move(sourceDirName, sourceDirName + ".deleted");
                            log.Debug("dsResult : SUCCESS");
                            this.MessageControl2.PutMessage("File successfully Deleted.");
                            this.MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            this.MessageControl2.Show();
                        }
                    }
                    else
                    {
                        log.Debug("Failed to delete file!");
                    }
                    this.GetViewDoc(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, str3);
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
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnopen_Click(object sender, EventArgs e)
    {
        string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
        string str2 = ConfigurationManager.AppSettings["FILE_URL"].ToString();
        for (int i = 0; i < this.grdViewDocuments.Items.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdViewDocuments.Items[i].FindControl("chkView");
            if (box.Checked)
            {
                string text = this.grdViewDocuments.Items[i].Cells[2].Text;
                string str4 = this.grdViewDocuments.Items[i].Cells[3].Text;
                string str5 = str + text + str4;
                if (File.Exists(str2 + text + str4))
                {
                    ScriptManager.RegisterStartupScript((Page)this, typeof(string), "popup" + i.ToString(), " window.open('" + str5 + "');", true);
                }
            }
        }
    }

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        try
        {
            this.grdNormalDgCode.Visible = true;
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
        }
        catch
        {
        }
    }

    protected void btnUPdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool flag = false;
        new ArrayList();
        ArrayList list1 = (ArrayList)this.Session["EVENT_ID"];
        DataSet procIdusingEventProcId = new DataSet();
        new DataSet();
        string str = "";
        string text = "";
        string str3 = "";
        string str4 = "";
        string str5 = base.Request.QueryString["lhrcode"].ToString();
        string str6 = this.extddlSpecialityUp.Text;
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            if (this.extddlCoSignedby.Text != "NA")
            {
                DataSet referringDoctorMandetoryInfo = new DataSet();
                DataSet readingDoctorInformation = new DataSet();
                new DataSet();
                string str7 = "";
                Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
                str7 = n_bo.GetCaseTypeID(this.txtCompanyID.Text, this.txtCaseID.Text).Tables[0].Rows[0][0].ToString();
                referringDoctorMandetoryInfo = n_bo.GetReferringDoctorMandetoryInfo(this.txtCompanyID.Text, str7);
                readingDoctorInformation = n_bo.GetReadingDoctorInformation(this.txtCompanyID.Text, this.extddlCoSignedby.Text);
                if ((readingDoctorInformation != null) && (referringDoctorMandetoryInfo != null))
                {
                    string str8 = "";
                    bool flag2 = false;
                    string str9 = referringDoctorMandetoryInfo.Tables[0].Rows[0][0].ToString();
                    string str10 = referringDoctorMandetoryInfo.Tables[0].Rows[0][1].ToString();
                    string str11 = referringDoctorMandetoryInfo.Tables[0].Rows[0][2].ToString();
                    str8 = referringDoctorMandetoryInfo.Tables[0].Rows[0][3].ToString();
                    if ((str9.ToLower() == "true") && (((readingDoctorInformation.Tables[0].Rows[0]["SZ_NPI"].ToString() == "") || (readingDoctorInformation.Tables[0].Rows[0]["SZ_NPI"].ToString() == "&nbsp")) || (readingDoctorInformation.Tables[0].Rows[0]["SZ_NPI"].ToString() == null)))
                    {
                        flag2 = true;
                    }
                    if ((str10.ToLower() == "true") && (((readingDoctorInformation.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == "") || (readingDoctorInformation.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == "&nbsp")) || (readingDoctorInformation.Tables[0].Rows[0]["SZ_DOCTOR_LICENSE_NUMBER"].ToString() == null)))
                    {
                        flag2 = true;
                    }
                    if ((str11.ToLower() == "true") && (((readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == "") || (readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == "&nbsp")) || (readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString() == null)))
                    {
                        flag2 = true;
                    }
                    if ((str8.ToLower() == "true") && (((readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == "") || (readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == "&nbsp")) || (readingDoctorInformation.Tables[0].Rows[0]["SZ_WCB_RATING_CODE"].ToString() == null)))
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('Co-Signed doctor does not have mandetory fields.');", true);
                        return;
                    }
                }
            }
            if (this.table_row_specialty_drpdwn.Visible)
            {
                if (this.extddlSpecialty.Text != "NA")
                {
                    new Bill_Sys_CheckoutBO().UpdateProc(txt_event_ProcID.Text.ToString(), this.extddlSpecialty.Text);
                    ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).ProceuderGroupId=this.extddlSpecialty.Text;
                    this.txtSpecility.Text = this.extddlSpecialty.Selected_Text;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds10sl", "alert('Please select a Specialty');", true);
                    return;
                }
            }
            bool flag3 = true;
            for (int i = 0; i < this.grdViewDocuments.Items.Count; i++)
            {
                if (((CheckBox)this.grdViewDocuments.Items[i].Cells[0].FindControl("chkView")).Checked)
                {
                    flag = true;
                    if (((DropDownList)this.grdViewDocuments.Items[i].Cells[4].FindControl("ddlreport")).SelectedValue == "8")
                    {
                        flag3 = false;
                    }
                }
            }
            if (!flag3)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds42d", "alert('Please Select Document Category.');", true);
            }
            else
            {
                Bill_Sys_CheckoutBO tbo;
                int num2 = 0;
                bool flag4 = false;
                bool flag5 = false;
                int num3 = 0;
                for (int j = 0; j < this.grdProCode.Items.Count; j++)
                {
                    CheckBox box = (CheckBox)this.grdProCode.Items[j].FindControl("chkSelectProc");
                    if (box.Checked)
                    {
                        num3++;
                        if (this.txtProcDesc.Text == this.grdProCode.Items[j].Cells[1].Text.ToString())
                        {
                            flag5 = true;
                        }
                    }
                }
                if (num3 > 1)
                {
                    flag4 = true;
                }
                for (int k = 0; k < this.grdProCode.Items.Count; k++)
                {
                    CheckBox box2 = (CheckBox)this.grdProCode.Items[k].FindControl("chkSelectProc");
                    if (box2.Checked)
                    {
                        if (!flag4)
                        {
                            Bill_Sys_CheckoutBO tbo2 = new Bill_Sys_CheckoutBO();
                            procIdusingEventProcId = tbo2.GetProcIdusingEventProcId(txt_event_ProcID.Text.ToString());
                            if ((procIdusingEventProcId.Tables[0] != null) && (procIdusingEventProcId.Tables[0].Rows.Count > 0))
                            {
                                for (int m = 0; m < procIdusingEventProcId.Tables[0].Rows.Count; m++)
                                {
                                    if (str == "")
                                    {
                                        str = procIdusingEventProcId.Tables[0].Rows[m]["SZ_PROC_CODE"].ToString();
                                    }
                                    str3 = procIdusingEventProcId.Tables[0].Rows[m]["I_EVENT_ID"].ToString();
                                }
                            }
                            
                            num2 = tbo2.UpdateProc(txt_event_ProcID.Text.ToString(), this.grdProCode.Items[k].Cells[0].Text);
                            text = this.grdProCode.Items[k].Cells[0].Text;
                            string str12 = this.grdProCode.Items[k].Cells[1].Text;
                            string str13 = "Procedure code updated to '" + str12 + "' by " + this.txtUserName.Text + "";
                            tbo = new Bill_Sys_CheckoutBO();
                            if (this.table_row_specialty_drpdwn.Visible)
                            {
                                tbo.AddProcedureCodeNotesForLhr(str3, this.extddlSpecialty.Text, str, text, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str13);
                            }
                            else
                            {
                                tbo.AddProcedureCodeNotesForLhr(str3, this.txtProcGroupId.Text, str, text, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str13);
                            }
                            if (num2 > 0)
                            {
                            }
                        }
                        else
                        {
                            if (!flag5)
                            {
                                Bill_Sys_CheckoutBO tbo3 = new Bill_Sys_CheckoutBO();
                                procIdusingEventProcId = tbo3.GetProcIdusingEventProcId(txt_event_ProcID.Text.ToString());
                                if ((procIdusingEventProcId.Tables[0] != null) && (procIdusingEventProcId.Tables[0].Rows.Count > 0))
                                {
                                    for (int n = 0; n < procIdusingEventProcId.Tables[0].Rows.Count; n++)
                                    {
                                        if (str == "")
                                        {
                                            str = procIdusingEventProcId.Tables[0].Rows[n]["SZ_PROC_CODE"].ToString();
                                        }
                                        str3 = procIdusingEventProcId.Tables[0].Rows[n]["I_EVENT_ID"].ToString();
                                    }
                                }
                                num2 = tbo3.UpdateProc(txt_event_ProcID.Text.ToString(), this.grdProCode.Items[k].Cells[0].Text);
                                text = this.grdProCode.Items[k].Cells[0].Text;
                                this.txtProcDesc.Text = this.grdProCode.Items[k].Cells[1].Text.ToString();
                                str4 = this.grdProCode.Items[k].Cells[1].Text;
                                break;
                            }
                            if (this.txtProcDesc.Text == this.grdProCode.Items[k].Cells[1].Text.ToString())
                            {
                                num2 = new Bill_Sys_CheckoutBO().UpdateProc(txt_event_ProcID.Text.ToString(), this.grdProCode.Items[k].Cells[0].Text);
                                str4 = this.grdProCode.Items[k].Cells[1].Text;
                                if (num2 <= 0)
                                {
                                }
                                break;
                            }
                        }
                    }
                }
                if (this.extddlReadingDoctor.Selected_Text != "---Select---")
                {
                    new Bill_Sys_ProcedureCode_BO().UpdateReadingDoctor(Convert.ToInt32(this.txtEventProcId.Text), this.extddlReadingDoctor.Text);
                }
                if (this.extddlCoSignedby.Text != "NA")
                {
                    new Bill_Sys_ProcedureCode_BO().UpdateCoSignedBy(Convert.ToInt32(this.txtEventProcId.Text), this.extddlCoSignedby.Text, "UPDATE");
                }
                else
                {
                    new Bill_Sys_ProcedureCode_BO().UpdateCoSignedBy(Convert.ToInt32(this.txtEventProcId.Text), "", "UPDATE");
                }
                if (flag)
                {
                    this.objNF3Template = new Bill_Sys_NF3_Template();
                    string str14 = this.objNF3Template.getPhysicalPath();
                    int num8 = 0;
                    string str15 = "";
                    foreach (DataGridItem item in this.grdViewDocuments.Items)
                    {
                        CheckBox box3 = (CheckBox)item.Cells[0].FindControl("chkView");
                        DropDownList list = (DropDownList)item.Cells[0].FindControl("ddlreport");
                        if (box3.Checked)
                        {
                            string companyName = "";
                            if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
                            {
                                companyName = this.objNF3Template.GetCompanyName(this.objSessionBillingCompany.SZ_COMPANY_ID);
                            }
                            else
                            {
                                companyName = this.objNF3Template.GetCompanyName(this.objSessionBillingCompany.SZ_COMPANY_ID);
                            }
                            if (list.SelectedValue == "0")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/Medicals/" + this.txtSpecility.Text + "/";
                            }
                            else if (list.SelectedValue == "1")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/Medicals/" + this.txtSpecility.Text + "/Referral/";
                            }
                            else if (list.SelectedValue == "2")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/AOB/";
                            }
                            else if (list.SelectedValue == "3")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/Comp Authorization/";
                            }
                            else if (list.SelectedValue == "4")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/HIPPA Consent/";
                            }
                            else if (list.SelectedValue == "5")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/Lien Form/";
                            }
                            else if (list.SelectedValue == "6")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/MISC/";
                            }
                            else if (list.SelectedValue == "7")
                            {
                                companyName = companyName + "/" + this.txtCaseID.Text + "/No Fault File/MISC/";
                            }
                            this.strLinkPath = item.Cells[2].Text + item.Cells[3].Text;
                            if (!Directory.Exists(str14 + companyName))
                            {
                                Directory.CreateDirectory(str14 + companyName);
                            }
                            Bill_Sys_FileType_Settings settings = new Bill_Sys_FileType_Settings();
                            DataSet set4 = new DataSet();
                            set4 = settings.GET_IMAGE_ID(this.txtEventProcId.Text, item.Cells[3].Text, this.txtCompanyID.Text);
                            if (set4.Tables[0].Rows.Count > 0)
                            {
                                for (int num9 = 0; num9 < set4.Tables[0].Rows.Count; num9++)
                                {
                                    ArrayList list2 = new ArrayList();
                                    list2.Add(set4.Tables[0].Rows[num9]["I_ID"].ToString());
                                    list2.Add(this.txtEventProcId.Text);
                                    settings.LhrDeleteDocuments(list2);
                                }
                                for (int num10 = 0; num10 < set4.Tables[0].Rows.Count; num10++)
                                {
                                    DataSet set5 = new DataSet();
                                    set5 = settings.GET_IMAGE_ID_LHR(set4.Tables[0].Rows[num10]["I_IMAGE_ID"].ToString());
                                    string sourceFileName = (ConfigurationManager.AppSettings["BASEPATH"].ToString() + set4.Tables[0].Rows[0]["SZ_FILE_PATH"].ToString() + set4.Tables[0].Rows[0]["SZ_FILE_NAME"].ToString()).Replace("/", @"\");
                                    if (set5.Tables[0].Rows.Count <= 0)
                                    {
                                        ArrayList list3 = new ArrayList();
                                        list3.Add(set4.Tables[0].Rows[num10]["I_IMAGE_ID"].ToString());
                                        list3.Add(item.Cells[3].Text);
                                        settings.Deletelhrdocuments(list3);
                                        File.Move(sourceFileName, sourceFileName + ".deleted");
                                    }
                                }
                            }
                            if (File.Exists(str14 + this.strLinkPath))
                            {
                                File.Copy(str14 + this.strLinkPath, str14 + companyName + item.Cells[3].Text, true);
                                ArrayList objAL = new ArrayList();
                                if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
                                {
                                    objAL.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                }
                                else
                                {
                                    objAL.Add(this.objSessionBillingCompany.SZ_COMPANY_ID);
                                }
                                objAL.Add(this.txtCaseID.Text);
                                objAL.Add(item.Cells[3].Text);
                                objAL.Add(companyName);
                                objAL.Add(this.objSessionUser.SZ_USER_NAME);
                                objAL.Add(this.txtSpecility.Text);
                                if (list.SelectedValue == "0")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager(objAL);
                                }
                                else if (list.SelectedValue == "1")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager_Referral(objAL);
                                }
                                else if (list.SelectedValue == "2")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager_AOB(objAL);
                                }
                                else if (list.SelectedValue == "3")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager_NFCA(objAL);
                                }
                                else if (list.SelectedValue == "4")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager_NFHC(objAL);
                                }
                                else if (list.SelectedValue == "5")
                                {
                                    num8 = this.objNF3Template.saveReportInDocumentManager_NFLF(objAL);
                                }
                                else if (list.SelectedValue == "6")
                                {
                                    num8 = this.saveReportInDocumentManager_NFMIS(objAL);
                                }
                                else if ((list.SelectedValue == "7") && File.Exists(str14 + this.strLinkPath))
                                {
                                    string str18 = "";
                                    str18 = item.Cells[2].Text.Replace("/", @"\");
                                    num8 = Convert.ToInt32(this.objNF3Template.GetImageID(item.Cells[3].Text, item.Cells[2].Text, str18));
                                }
                                if ((num8.ToString().Trim() != "0") && (num8.ToString().Trim() != ""))
                                {
                                    this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                    this._bill_Sys_ProcedureCode_BO.assignLHRDocument(objAL, num8, list.SelectedValue, Convert.ToInt32(this.txtEventProcId.Text));
                                }
                                if (list.SelectedValue == "0")
                                {
                                    this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                    this._bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(this.txtEventProcId.Text), this.strLinkPath, num8);
                                }
                                continue;
                            }
                            if (str15 == "")
                            {
                                str15 = str14 + this.strLinkPath + ",\n";
                            }
                            else
                            {
                                str15 = str15 + str14 + this.strLinkPath + ",\n";
                            }
                        }
                    }
                    if (str15 != "")
                    {
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds10", "alert('These file are not available');", true);
                        return;
                    }
                }
                string str19 = "";
                if (flag4)
                {
                    string str20 = "";
                    for (int num11 = 0; num11 < this.grdProCode.Items.Count; num11++)
                    {
                        CheckBox box4 = (CheckBox)this.grdProCode.Items[num11].FindControl("chkSelectProc");
                        if (box4.Checked && (this.txtProcDesc.Text != this.grdProCode.Items[num11].Cells[1].Text.ToString()))
                        {
                            procIdusingEventProcId = new Bill_Sys_CheckoutBO().GetProcIdusingEventProcId(txt_event_ProcID.Text.ToString());
                            if ((procIdusingEventProcId.Tables[0] != null) && (procIdusingEventProcId.Tables[0].Rows.Count > 0))
                            {
                                for (int num12 = 0; num12 < procIdusingEventProcId.Tables[0].Rows.Count; num12++)
                                {
                                    if (str == "")
                                    {
                                        str = procIdusingEventProcId.Tables[0].Rows[num12]["SZ_PROC_CODE"].ToString();
                                    }
                                    str3 = procIdusingEventProcId.Tables[0].Rows[num12]["I_EVENT_ID"].ToString();
                                }
                            }
                            if (this.SaveMulitpleProcCode(this.grdProCode.Items[num11].Cells[0].Text, this.txtEventProcId.Text).ToString() != "1")
                            {
                                str19 = str19 + this.grdProCode.Items[num11].Cells[1].Text + "; ";
                            }
                            text = this.grdProCode.Items[num11].Cells[0].Text;
                            string str21 = this.grdProCode.Items[num11].Cells[1].Text;
                            if (str20 == "")
                            {
                                str20 = this.grdProCode.Items[num11].Cells[1].Text;
                            }
                            else
                            {
                                str20 = str20 + "," + this.grdProCode.Items[num11].Cells[1].Text;
                            }
                            tbo = new Bill_Sys_CheckoutBO();
                            string str22 = "";
                            if (str4 != "")
                            {
                                str22 = "'" + str21 + "' is split to '" + str4 + "' by " + this.txtUserName.Text + "";
                            }
                            else
                            {
                                str22 = "Procedure code updated to '" + str21 + "' by " + this.txtUserName.Text + "";
                            }
                            if (this.table_row_specialty_drpdwn.Visible)
                            {
                                tbo.AddProcedureCodeNotesForLhr(str3, this.extddlSpecialty.Text, str, text, this.txtEventProcId.Text, this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str22);
                            }
                            else
                            {
                                tbo.AddProcedureCodeNotesForLhr(str3, this.txtProcGroupId.Text, str, text, this.txtEventProcId.Text, this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str22);
                            }
                        }
                    }
                    string str23 = "'" + str4 + "' is split to '" + str20 + " by " + this.txtUserName.Text + "";
                    tbo = new Bill_Sys_CheckoutBO();
                    if (this.table_row_specialty_drpdwn.Visible)
                    {
                        tbo.AddProcedureCodeNotesForLhr(str3, this.extddlSpecialty.Text, str, text, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str23);
                    }
                    else
                    {
                        tbo.AddProcedureCodeNotesForLhr(str3, this.txtProcGroupId.Text, str, text, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str5, str6, this.txtUserId.Text, 0, str23);
                    }
                }
                if (str19 == "")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds28", "alert('Saved  successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ds2822", "alert('" + this.txtProcDesc.Text + " saved successfully, Already Added Procedure Codes - " + str19 + "');", true);
                }
                if (this.chkClose.Checked)
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "ReturnToParentPage();", true);
                    this.Session["CloseWindow"] = "1";
                }
                else
                {
                    this.Session["CloseWindow"] = "0";
                }
                this.ShowAuditGrid();
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

    protected void btnUpdateSpec_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet procIdusingEventProcId = new DataSet();
            new DataSet();
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = base.Request.QueryString["lhrcode"].ToString();
            string str5 = this.extddlSpecialityUp.Text;
            this.txtProcGroupId.Text = this.extddlSpecialityUp.Text;
            procIdusingEventProcId = new Bill_Sys_CheckoutBO().GetProcIdusingEventProcId(txt_event_ProcID.Text.ToString());
            if ((procIdusingEventProcId.Tables[0] != null) && (procIdusingEventProcId.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < procIdusingEventProcId.Tables[0].Rows.Count; i++)
                {
                    if (str == "")
                    {
                        str = procIdusingEventProcId.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                    }
                    str3 = procIdusingEventProcId.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                }
            }
            new Bill_Sys_ProcedureCode_BO().UpdateSpecialty(this.txtCompanyID.Text, this.extddlSpecialityUp.Text, this.txtEventProcId.Text, this.txtCaseID.Text);
            string str6 = "";
            Bill_Sys_CheckoutBO tbo = new Bill_Sys_CheckoutBO();
            if (this.table_row_specialty_drpdwn.Visible)
            {
                str6 = "Specialty changed from 'OT' to '" + this.extddlSpecialityUp.Selected_Text + "' by " + this.txtUserName.Text + "";
                tbo.AddProcedureCodeNotesForLhr(str3, "PG000000000000000670", str, str2, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str4, str5, this.txtUserId.Text, 1, str6);
            }
            else
            {
                str6 = "Specialty changed from '" + this.txtSpecility.Text + "' to '" + this.extddlSpecialityUp.Selected_Text + "' by " + this.txtUserName.Text + "";
                tbo.AddProcedureCodeNotesForLhr(str3, this.txtProcGroupId.Text, str, str2, txt_event_ProcID.Text.ToString(), this.txtCompanyID.Text, this.txtCaseID.Text, str4, str5, this.txtUserId.Text, 1, str6);
            }
            this.bindProcGridOnUpdateSpecialty();
            if (this.chkClose.Checked)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "ReturnToParentPage();", true);
                this.Session["CloseWindow"] = "1";
            }
            else
            {
                this.Session["CloseWindow"] = "0";
            }
            this.ShowAuditGrid();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "alert('Specialty updated successfully!')", true);
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

    protected void extddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.extddlSpecialityUp.Text=this.extddlSpecialty.Text;
        this.extddlSpecialityDia.Text=this.extddlSpecialty.Text;
        this.bindProcGrid();
        Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
        DataSet caseProcCode = new DataSet();
        caseProcCode = e_bo.GetCaseProcCode(txt_event_ID.Text.ToString());
        try
        {
            if (caseProcCode.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < caseProcCode.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < this.grdProCode.Items.Count; j++)
                    {
                        if (this.grdProCode.Items[j].Cells[0].Text.ToString().Trim() == caseProcCode.Tables[0].Rows[i]["CODE"])
                        {
                            CheckBox box = (CheckBox)this.grdProCode.Items[j].FindControl("chkSelectProc");
                            box.Enabled = false;
                        }
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

    private void GetAssignedDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            this.grdNormalDgCode.DataSource = set.Tables[0];
            this.grdNormalDgCode.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
                    {
                        DataRow row = table.NewRow();
                        row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                        row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                        row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                        row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        table.Rows.Add(row);
                    }
                }
            }
            this.grdAssignedDiagnosisCode.DataSource = table;
            this.grdAssignedDiagnosisCode.DataBind();
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

    private void GetAssignedProcedureDiagnosisCode(string caseID, string companyId, string szEventProcID, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventProcID, flag);
            this.grdNormalDgCode.DataSource = set.Tables[0];
            this.grdNormalDgCode.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
                    {
                        DataRow row = table.NewRow();
                        row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                        row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                        row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                        row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        table.Rows.Add(row);
                    }
                }
            }
            this.grdAssignedDiagnosisCode.DataSource = table;
            this.grdAssignedDiagnosisCode.DataBind();
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

    private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            this.grdNormalDgCode.DataSource = set.Tables[0];
            this.grdNormalDgCode.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
                    {
                        DataRow row = table.NewRow();
                        row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                        row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                        row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                        row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        table.Rows.Add(row);
                    }
                }
            }
            this.grdSelectedDiagnosisCode.DataSource = table;
            this.grdSelectedDiagnosisCode.DataBind();
            this.lblDiagnosisCount.Text = table.Rows.Count.ToString();
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

    private void GetProcedureDiagnosisCode(string caseID, string companyId, string szEventID, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet set = new DataSet();
            set = this._associateDiagnosisCodeBO.GetProcedureDiagnosisCode(caseID, companyId, szEventID, flag);
            DataTable table = new DataTable();
            table.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table.Columns.Add("SZ_DIAGNOSIS_CODE");
            table.Columns.Add("SZ_DESCRIPTION");
            table.Columns.Add("SZ_COMPANY_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataTable table2 = new DataTable();
            table2.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            table2.Columns.Add("SZ_DIAGNOSIS_CODE");
            table2.Columns.Add("SZ_DESCRIPTION");
            table2.Columns.Add("SZ_COMPANY_ID");
            table2.Columns.Add("SZ_PROCEDURE_GROUP");
            table2.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                foreach (DataGridItem item in this.grdNormalDgCode.Items)
                {
                    if ((item.ItemIndex == i) && ((bool)set.Tables[0].Rows[i]["CHECKED"]))
                    {
                        DataRow row = table.NewRow();
                        row["SZ_DIAGNOSIS_CODE_ID"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        row["SZ_DIAGNOSIS_CODE"] = set.Tables[0].Rows[i]["SZ_DIAGNOSIS_CODE"].ToString();
                        row["SZ_DESCRIPTION"] = set.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        row["SZ_COMPANY_ID"] = set.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
                        row["SZ_PROCEDURE_GROUP"] = set.Tables[0].Rows[i]["Speciality"].ToString();
                        row["SZ_PROCEDURE_GROUP_ID"] = set.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        table.Rows.Add(row);
                    }
                }
            }
            this.grdSelectedDiagnosisCode.DataSource = table;
            this.grdSelectedDiagnosisCode.DataBind();
            this.lblDiagnosisCount.Text = table.Rows.Count.ToString();
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

    private void GetViewDoc(string caseId, string companyID, string proc_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
            DataSet set = new DataSet();
            set = e_bo.GetLHRDocs(caseId, companyID, proc_id);
            this.grdViewDocuments.DataSource = set;
            this.grdViewDocuments.DataBind();
            this.grdDelete.DataSource = set;
            this.grdDelete.DataBind();
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

    protected void grdAssignedDiagnosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
            this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
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

    protected void grdDelete_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string str = " ";
        if (e.CommandName == "View")
        {
            string str2 = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string str3 = ConfigurationManager.AppSettings["FILE_URL"].ToString();
            string text = e.Item.Cells[2].Text;
            string str5 = e.Item.Cells[3].Text;
            string str6 = str2 + text + str5;
            if (File.Exists(str3 + text + str5))
            {
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + str6 + "');", true);
            }
            else
            {
                this.MessageControl2.PutMessage("File is not available \n" + str);
                this.MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.MessageControl2.Show();
            }
        }
    }

    protected void grdNormalDgCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_ONE_DIAGNOSIS_CODE");
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

    protected void grdNormalDgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdViewDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string str = " ";
        if (e.CommandName == "View")
        {
            string str2 = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string str3 = ConfigurationManager.AppSettings["FILE_URL"].ToString();
            string text = e.Item.Cells[2].Text;
            string str5 = e.Item.Cells[3].Text;
            string str6 = str2 + text + str5;
            if (File.Exists(str3 + text + str5))
            {
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + str6 + "');", true);
            }
            else
            {
                this.usrMessage.PutMessage("File is not available \n" + str);
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
        }
    }

    protected void grdViewDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void intialLoad()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szProcedureGroupId = base.Request.QueryString["pdid"].ToString();
        string str3 = base.Request.QueryString["EProcid"].ToString();
        string szCaseId = base.Request.QueryString["CaseID"].ToString();
        string szPatientId = base.Request.QueryString["patientid"].ToString();
        string szEventId = base.Request.QueryString["eventid"].ToString();
        string szProcedureGroup = base.Request.QueryString["spc"].ToString();
        string szPatientName = base.Request.QueryString["patientname"].ToString();
        string szDateOfService = base.Request.QueryString["dateofservice"].ToString();
        string szCaseNo = base.Request.QueryString["caseno"].ToString();
        string szDoctorId = base.Request.QueryString["doctorid"].ToString();
        string szProcCode = base.Request.QueryString["code"].ToString();
        string str13 = base.Request.QueryString["ReadingDoctorId"].ToString();
        if (base.Request.QueryString["CaseType"].ToString() == "Workers Comp")
        {
            this.extddlCoSignedby.Enabled = true;
        }
        else
        {
            this.extddlCoSignedby.Enabled = false;
        }
        DataSet set = new DataSet();
        set = new Bill_Sys_ProcedureCode_BO().UpdateCoSignedBy(Convert.ToInt32(str3), this.extddlCoSignedby.Text, "GET");
        if ((set != null) && (set.Tables[0].Rows.Count > 0))
        {
            this.extddlCoSignedby.Text=set.Tables[0].Rows[0][0].ToString();
        }
        this.LoadSession(szProcedureGroupId, str3, szCaseId, szPatientId, szEventId, szProcedureGroup, szPatientName, szDateOfService, szCaseNo, szDoctorId, szProcCode);
        this.objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        this.objSessionUser = (Bill_Sys_UserObject)this.Session["USER_OBJECT"];
        this.txtCompanyID.Text = this.objSessionBillingCompany.SZ_COMPANY_ID;
        this.txtprocid.Text = "";
        this.extddlSpecialty.Flag_ID=this.txtCompanyID.Text;
        this.extddlDiagnosisType.Flag_ID=this.objSessionBillingCompany.SZ_COMPANY_ID;
        if (base.Request.QueryString["SetId"] != null)
        {
            this.txtDiagnosisSetID.Text = base.Request.QueryString["SetId"].ToString();
            this._ds = this._associateDiagnosisCodeBO.GetCaseAssociateDiagnosisDetails(this.txtDiagnosisSetID.Text);
        }
        else
        {
            this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
        }
        if ((this.Session["DIAGNOS_ASSOCIATION_PAID"] != null) && (((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"]).Count > 0))
        {
            this.txtCaseID.Text = ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CaseID;
        }
        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, str3, "GET_DIAGNOSIS_CODE");
        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, str3, "GET_ONE_DIAGNOSIS_CODE");
        this.grdNormalDgCode.Visible = false;
        this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
        string str14 = base.Request.QueryString[1].ToString();
        this.szdescrption = base.Server.HtmlDecode(base.Request.QueryString["desc"].ToString());
        base.Request.QueryString["lhrcode"].ToString();
        this.txtEventProcId.Text = str3;
        this.txtSpecility.Text = szProcedureGroup;
        this.txtCaseID.Text = szCaseId;
        this.txtProcGroupId.Text = szProcedureGroupId;
        this.txtdesc.Text = this.szdescrption;
        this.txtcode.Text = szProcCode;
        if (str14 == "YES")
        {
            this.btnView.Visible = false;
        }
        else
        {
            this.btnView.Visible = false;
        }
        this.GetViewDoc(szCaseId, this.objSessionBillingCompany.SZ_COMPANY_ID, str3);
        this.szRoomId = this.Session["GETROOMID"].ToString();
        DataSet procedureCode = null;
        if (this.szRoomId == "All")
        {
            this.table_row_specialty_drpdwn.Visible = true;
            new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            procedureCode = new LHRProcedureCode().GetProcedureCode(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        }
        else
        {
            this.table_row_specialty_drpdwn.Visible = false;
            ArrayList list = new ArrayList();
            list.Add(this.objSessionBillingCompany.SZ_COMPANY_ID);
            list.Add(this.szRoomId);
            new Bill_Sys_ManageVisitsTreatmentsTests_BO();
            DataSet set3 = new DataSet();
            set3 = new LHRProcedureCode().GetProcedureCode(list);
            this.grdProCode.DataSource = set3;
            this.grdProCode.DataBind();
        }
        if (!this.table_row_specialty_drpdwn.Visible)
        {
            if ((!this.szdescrption.ToLower().Contains("unknown") && (this.szdescrption != "")) && (this.szdescrption != "&nbsp;"))
            {
                for (int i = 0; i < this.grdProCode.Items.Count; i++)
                {
                    if (this.grdProCode.Items[i].Cells[1].Text.ToString().ToUpper() == (szProcCode + "-" + this.szdescrption).ToUpper())
                    {
                        CheckBox box = (CheckBox)this.grdProCode.Items[i].FindControl("chkSelectProc");
                        box.Checked = true;
                        this.txtProcDesc.Text = this.grdProCode.Items[i].Cells[1].Text.ToString();
                        break;
                    }
                }
            }
        }
        else if ((this.szdescrption.ToLower().Contains("unknown") || (this.szdescrption == "")) || (this.szdescrption == "&nbsp;"))
        {
            this.bindProcGrid();
        }
        else
        {
            if ((procedureCode.Tables.Count > 0) && (procedureCode.Tables[0].Rows.Count > 0))
            {
                for (int j = 0; j < procedureCode.Tables[0].Rows.Count; j++)
                {
                    if (procedureCode.Tables[0].Rows[j]["DESCRIPTION"].ToString().ToUpper() == (szProcCode + "-" + this.szdescrption).ToUpper())
                    {
                        this.extddlSpecialty.Text=procedureCode.Tables[0].Rows[j]["sz_Procedure_group_id"].ToString();
                        break;
                    }
                }
            }
            this.bindProcGrid();
        }
        if (this.grdProCode.Items.Count > 0)
        {
            this.btnView.Visible = false;
        }
        else
        {
            this.btnView.Visible = false;
        }
        txt_event_ID.Text = szEventId;
        Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
        DataSet caseProcCode = new DataSet();
        caseProcCode = e_bo.GetCaseProcCode(szEventId);
        e_bo.GetDocIdUsingEventProcId(str3);
        this.extddlReadingDoctor.Text=str13;
        try
        {
            if (caseProcCode.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < caseProcCode.Tables[0].Rows.Count; k++)
                {
                    for (int m = 0; m < this.grdProCode.Items.Count; m++)
                    {
                        if (this.grdProCode.Items[m].Cells[0].Text.ToString().Trim() == caseProcCode.Tables[0].Rows[k]["CODE"].ToString().Trim())
                        {
                            CheckBox box2 = (CheckBox)this.grdProCode.Items[m].FindControl("chkSelectProc");
                            box2.Enabled = false;
                        }
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
        this.extddlSpecialityUp.Text=szProcedureGroupId;
        this.extddlSpecialityDia.Text=szProcedureGroupId;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void intialLoad(string CaseID, string Type, string EProcid, string pdid, string eventid, string spc, string desc, string code, string szCaseType, string patientname, string dateofservice, string lhrcode, string caseno)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.ShowAuditGrid();
        this.objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        this.objSessionUser = (Bill_Sys_UserObject)this.Session["USER_OBJECT"];
        this.txtprocid.Text = "";
        this.extddlSpecialty.Flag_ID=this.txtCompanyID.Text;
        this.txtDiagnosisSetID.Text = this._associateDiagnosisCodeBO.GetDiagnosisSetID();
        this.extddlDiagnosisType.Flag_ID=this.objSessionBillingCompany.SZ_COMPANY_ID;
        if ((this.Session["DIAGNOS_ASSOCIATION_PAID"] != null) && (((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"]).Count > 0))
        {
            this.txtCaseID.Text = ((Bil_Sys_Associate_Diagnosis)((ArrayList)this.Session["DIAGNOS_ASSOCIATION_PAID"])[0]).CaseID;
        }
        this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, EProcid, "GET_DIAGNOSIS_CODE");
        this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
        this.grdNormalDgCode.Visible = false;
        this.tabcontainerDiagnosisCode.ActiveTabIndex=1;
        this.caseid = CaseID;
        string str = Type;
        this.EventProcID = EProcid;
        this.szSpeciality = spc;
        this.txtEventProcId.Text = this.EventProcID;
        this.txtSpecility.Text = this.szSpeciality;
        this.txtCaseID.Text = this.caseid;
        this.szProceduregroup_id = pdid;
        this.szdescrption = desc;
        string str2 = code;
        this.txtdesc.Text = this.szdescrption;
        this.txtcode.Text = str2;
        if (str == "YES")
        {
            this.btnView.Visible = false;
        }
        else
        {
            this.btnView.Visible = false;
        }
        this.GetViewDoc(this.caseid, this.objSessionBillingCompany.SZ_COMPANY_ID, this.EventProcID);
        this.szRoomId = this.Session["GETROOMID"].ToString();
        DataSet allProcCodeLHR = null;
        if (this.szRoomId == "All")
        {
            this.table_row_specialty_drpdwn.Visible = true;
            allProcCodeLHR = new Bill_Sys_ManageVisitsTreatmentsTests_BO().GetAllProcCodeLHR(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        }
        else
        {
            this.table_row_specialty_drpdwn.Visible = false;
            ArrayList list = new ArrayList();
            list.Add(this.objSessionBillingCompany.SZ_COMPANY_ID);
            list.Add(this.szRoomId);
            DataSet referringProcCodeList = new Bill_Sys_ManageVisitsTreatmentsTests_BO().GetReferringProcCodeList(list);
            this.grdProCode.DataSource = referringProcCodeList;
            this.grdProCode.DataBind();
        }
        if (!this.table_row_specialty_drpdwn.Visible)
        {
            if ((!this.szdescrption.ToLower().Contains("unknown") && (this.szdescrption != "")) && (this.szdescrption != "&nbsp;"))
            {
                for (int i = 0; i < this.grdProCode.Items.Count; i++)
                {
                    if (this.grdProCode.Items[i].Cells[1].Text.ToString().ToUpper() == (str2 + "-" + this.szdescrption).ToUpper())
                    {
                        CheckBox box = (CheckBox)this.grdProCode.Items[i].FindControl("chkSelectProc");
                        box.Checked = true;
                        this.txtProcDesc.Text = this.grdProCode.Items[i].Cells[1].Text.ToString();
                        break;
                    }
                }
            }
        }
        else if ((this.szdescrption.ToLower().Contains("unknown") || (this.szdescrption == "")) || (this.szdescrption == "&nbsp;"))
        {
            this.bindProcGrid();
        }
        else
        {
            if ((allProcCodeLHR.Tables.Count > 0) && (allProcCodeLHR.Tables[0].Rows.Count > 0))
            {
                for (int j = 0; j < allProcCodeLHR.Tables[0].Rows.Count; j++)
                {
                    if (allProcCodeLHR.Tables[0].Rows[j]["DESCRIPTION"].ToString().ToUpper() == (str2 + "-" + this.szdescrption).ToUpper())
                    {
                        this.extddlSpecialty.Text=allProcCodeLHR.Tables[0].Rows[j]["sz_Procedure_group_id"].ToString();
                        break;
                    }
                }
            }
            this.bindProcGrid();
        }
        if (this.grdProCode.Items.Count > 0)
        {
            this.btnView.Visible = false;
        }
        else
        {
            this.btnView.Visible = false;
        }
        new DataSet();
        new Bill_Sys_DoctorBO();
        string str3 = eventid;
        txt_event_ID.Text = str3;
       
        Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
        DataSet caseProcCode = new DataSet();
        caseProcCode = e_bo.GetCaseProcCode(str3);
        string docIdUsingEventProcId = e_bo.GetDocIdUsingEventProcId(this.EventProcID);
        try
        {
            this.extddlReadingDoctor.Text=docIdUsingEventProcId;
        }
        catch
        {
        }
        try
        {
            if (caseProcCode.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < caseProcCode.Tables[0].Rows.Count; k++)
                {
                    for (int m = 0; m < this.grdProCode.Items.Count; m++)
                    {
                        if (this.grdProCode.Items[m].Cells[0].Text.ToString().Trim() == caseProcCode.Tables[0].Rows[k]["CODE"].ToString().Trim())
                        {
                            CheckBox box2 = (CheckBox)this.grdProCode.Items[m].FindControl("chkSelectProc");
                            box2.Enabled = false;
                        }
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
            string errorMessage ="Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

        }
        this.grdDelete.DataSource = new Bill_Sys_ProcedureCode_BO().GetLHRDocs(this.caseid, this.objSessionBillingCompany.SZ_COMPANY_ID, this.EventProcID);
        this.grdDelete.DataBind();
        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "updateParent", "updateParent('" + patientname + "', '" + dateofservice + "', '" + lhrcode + "', '" + szCaseType + "', '" + caseno + "');", true);

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        if (this.Session["Pending_Report_Datatable"] == null)
        {
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "noDataset", "alert('Could not load the record.');", true);
        }
        else
        {
            DataTable dt = (DataTable)this.Session["Pending_Report_Datatable"];
            int procIndex = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["i_Event_proc_id"].ToString() == this.txtEventProcId.Text)
                {
                    procIndex = i + 1;
                    break;
                }
            }
            if (procIndex != -1)
            {
                this.loadDetails(procIndex, dt);
                if ((procIndex + 1) >= dt.Rows.Count)
                {
                    this.lnkPrevious.Enabled = true;
                    this.lnkNext.Enabled = false;
                }
                else
                {
                    this.lnkNext.Enabled = true;
                    this.lnkPrevious.Enabled = true;
                }
            }
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        if (this.Session["Pending_Report_Datatable"] == null)
        {
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "noDataset", "alert('Could not load the record.');", true);
        }
        else
        {
            DataTable dt = (DataTable)this.Session["Pending_Report_Datatable"];
            int procIndex = -1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["i_Event_proc_id"].ToString() == this.txtEventProcId.Text)
                {
                    procIndex = i - 1;
                    break;
                }
            }
            if (procIndex != -1)
            {
                this.loadDetails(procIndex, dt);
                if ((procIndex - 1) < 0)
                {
                    this.lnkPrevious.Enabled = false;
                    this.lnkNext.Enabled = true;
                }
                else
                {
                    this.lnkPrevious.Enabled = true;
                    this.lnkNext.Enabled = true;
                }
            }
        }
    }

    protected void lnkUnassociate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable table2 = new DataTable();
        dt = (DataTable)this.Session["Pending_Report_Datatable"];
        table2 = (DataTable)this.Session["Associate"];
        this.Session["ieventid"] = 0;
        DataTable table3 = (DataTable)this.Session["Pending_Report_Datatable"];
        int num = -1;
        for (int i = 0; i < table3.Rows.Count; i++)
        {
            if (table3.Rows[i]["i_Event_proc_id"].ToString() == this.txtEventProcId.Text)
            {
                num = i;
                break;
            }
        }
        if (num != -1)
        {
            for (int j = num + 1; j < dt.Rows.Count; j++)
            {
                string str = "1";
                for (int k = 0; k < table2.Rows.Count; k++)
                {
                    if (dt.Rows[j].ItemArray[10].ToString() == table2.Rows[k].ItemArray[0].ToString())
                    {
                        str = "0";
                        break;
                    }
                }
                if (str == "1")
                {
                    this.txtEventProcId.Text = dt.Rows[j].ItemArray[10].ToString();
                    this.loadDetails(j, dt);
                    return;
                }
            }
        }
    }

    protected void loadDetails(int procIndex, DataTable dt)
    {
        Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
        new DataSet();
        if ((dt.Rows[procIndex]["sz_procedure_group"].ToString().ToUpper() != "OT") && (dt.Rows[procIndex]["sz_procedure_group"].ToString().ToUpper() != ""))
        {
            string str = n_bo.GetRoomId(dt.Rows[procIndex]["SZ_PROCEDURE_GROUP_ID"].ToString(), this.txtCompanyID.Text).Tables[0].Rows[0][0].ToString();
            string str2 = dt.Rows[procIndex]["I_EVENT_PROC_ID"].ToString();
            this.Session["GETROOMID"] = str;
            txt_event_ProcID.Text = str2;
        }
        else
        {
            string str3 = dt.Rows[procIndex]["I_EVENT_PROC_ID"].ToString();
            this.Session["GETROOMID"] = "All";
            txt_event_ProcID.Text = str3;
        }
        string eProcid = dt.Rows[procIndex]["I_EVENT_PROC_ID"].ToString();
        string caseID = dt.Rows[procIndex]["SZ_CASE_ID"].ToString();
        string pdid = dt.Rows[procIndex]["SZ_PROCEDURE_GROUP_ID"].ToString();
        dt.Rows[procIndex]["SZ_PATIENT_ID"].ToString();
        string eventid = dt.Rows[procIndex]["I_EVENT_ID"].ToString();
        string spc = dt.Rows[procIndex]["sz_procedure_group"].ToString();
        string patientname = dt.Rows[procIndex]["PATIENT_NAME"].ToString();
        string dateofservice = dt.Rows[procIndex]["DT_DATE_OF_SERVICE"].ToString();
        string lhrcode = dt.Rows[procIndex]["SZ_LHR_CODE"].ToString();
        string caseno = dt.Rows[procIndex]["CASE_NO"].ToString();
        string szCaseType = dt.Rows[procIndex]["SZ_CASE_TYPE_NAME"].ToString();
        ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "tes", "var parentWindow = window.parent;parentWindow.ChangeHeaderText('" + patientname + "','" + dateofservice + "','" + lhrcode + "','" + szCaseType + "', '" + caseno + "');", true);
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        list2.Add(dt.Rows[procIndex]["I_EVENT_ID"].ToString());
        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
        diagnosis.EventProcID=dt.Rows[procIndex]["I_EVENT_PROC_ID"].ToString();
        diagnosis.DoctorID=dt.Rows[procIndex]["SZ_DOCTOR_ID"].ToString();
        diagnosis.CaseID=dt.Rows[procIndex]["SZ_CASE_ID"].ToString();
        diagnosis.ProceuderGroupId=dt.Rows[procIndex]["SZ_PROCEDURE_GROUP_ID"].ToString();
        diagnosis.ProceuderGroupName=dt.Rows[procIndex]["sz_procedure_group"].ToString();
        diagnosis.PatientId=dt.Rows[procIndex]["SZ_PATIENT_ID"].ToString();
        diagnosis.DateOfService=dt.Rows[procIndex]["DT_DATE_OF_SERVICE"].ToString();
        diagnosis.ProcedureCode=dt.Rows[procIndex]["SZ_PROC_CODE"].ToString();
        diagnosis.CompanyId=this.txtCompanyID.Text;
        list.Add(diagnosis);
        this.Session["DIAGNOS_ASSOCIATION_PAID"] = list;
        new DataSet();
        n_bo.GetRoomId(pdid, this.txtCompanyID.Text);
        string code = dt.Rows[procIndex]["SZ_CODE"].ToString();
        string desc = dt.Rows[procIndex]["SZ_CODE_DESCRIPTION"].ToString();
        Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
        if (e_bo.Get_Sys_Key("SS00014", this.txtCompanyID.Text).Tables[0].Rows[0][0].ToString() == "1")
        {
            this.Session["EVENT_ID"] = list2;
        }
        this.intialLoad(caseID, "YES", eProcid, pdid, eventid, spc, desc, code, szCaseType, patientname, dateofservice, lhrcode, caseno);
    }

    public void LoadSession(string szProcedureGroupId, string szEventProcId, string szCaseId, string szPatientId, string szEventId, string szProcedureGroup, string szPatientName, string szDateOfService, string szCaseNo, string szDoctorId, string szProcCode)
    {
        new DataSet();
        Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
        if ((szProcedureGroup.ToUpper() != "OT") && (szProcedureGroup.ToUpper() != ""))
        {
            string str = n_bo.GetRoomId(szProcedureGroupId, this.txtCompanyID.Text).Tables[0].Rows[0][0].ToString();
            this.Session["GETROOMID"] = str;
            txt_event_ProcID.Text = szEventProcId;
        }
        else
        {
            this.Session["GETROOMID"] = "All";
            txt_event_ProcID.Text = szEventProcId;
        }
        ArrayList list = new ArrayList();
        ArrayList list2 = new ArrayList();
        list2.Add(szEventId);
        Bil_Sys_Associate_Diagnosis diagnosis = new Bil_Sys_Associate_Diagnosis();
        diagnosis.EventProcID=szEventProcId;
        diagnosis.DoctorID=szDoctorId;
        diagnosis.CaseID=szCaseId;
        diagnosis.ProceuderGroupId=szProcedureGroupId;
        diagnosis.ProceuderGroupName=szProcedureGroup;
        diagnosis.PatientId=szPatientId;
        diagnosis.DateOfService=szDateOfService;
        diagnosis.ProcedureCode=szProcCode;
        diagnosis.CompanyId=this.txtCompanyID.Text;
        list.Add(diagnosis);
        this.Session["DIAGNOS_ASSOCIATION_PAID"] = list;
        Bill_Sys_ProcedureCode_BO e_bo = new Bill_Sys_ProcedureCode_BO();
        if (e_bo.Get_Sys_Key("SS00014", this.txtCompanyID.Text).Tables[0].Rows[0][0].ToString() == "1")
        {
            this.Session["EVENT_ID"] = list2;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        string str;
        HttpBrowserCapabilities browser = base.Request.Browser;
        string type = browser.Type;
        string text2 = browser.Browser;
        string rawUrl = "";
        if (base.Request.RawUrl.IndexOf("?") > 0)
        {
            rawUrl = base.Request.RawUrl.Substring(0, base.Request.RawUrl.IndexOf("?"));
        }
        else
        {
            rawUrl = base.Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            str = "css/main-ff.css";
        }
        else if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
        {
            str = "css/main-ch.css";
        }
        else
        {
            str = "css/main-ie.css";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("");
        if (rawUrl.Contains("AJAX Pages"))
        {
            builder.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />");
        }
        else
        {
            builder.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        }
        builder.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        builder.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        builder.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        builder.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        builder.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        builder.AppendLine("<link href='" + str + "' type='text/css' rel='Stylesheet' />");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
        this.objSystemObject = (Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"];
        this.objSessionUser = (Bill_Sys_UserObject)this.Session["USER_OBJECT"];
        this.txtCompanyID.Text = this.objSessionBillingCompany.SZ_COMPANY_ID;
        this.txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
        this.btnUPdate.Attributes.Add("onclick", "return confirm_update_bill_status_procode();");
        this.btndelete.Attributes.Add("onClick", "return confirm_delete_document();");
        this.extddlSpecialityUp.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlSpecialityDia.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlReadingDoctor.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlCoSignedby.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.txtUserId.Text = this.objSessionUser.SZ_USER_ID;
        this.txtUserName.Text = this.objSessionUser.SZ_USER_NAME;
        this.txtUserRole.Text = this.objSessionUser.SZ_USER_ROLE_NAME;
        if (!base.IsPostBack)
        {
            if (this.Session["CloseWindow"] != null)
            {
                if (this.Session["CloseWindow"].ToString() == "1")
                {
                    this.chkClose.Checked = true;
                }
                else if (this.Session["CloseWindow"].ToString() == "0")
                {
                    this.chkClose.Checked = false;
                }
            }
            this.intialLoad();
            if (this.txtUserRole.Text.ToLower() == "admin")
            {
                this.tpAudit.Visible = true;
                this.tabcontainerprocedurecodedocs.ActiveTabIndex=0;
            }
            this.tabcontainerprocedurecodedocs.ActiveTabIndex=0;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Bill_Sys_Upload_VisitReport report = new Bill_Sys_Upload_VisitReport();
        for (int i = 0; i < this.grdViewDocuments.Items.Count; i++)
        {
            string filedDocType = report.GetFiledDocType(this.grdViewDocuments.Items[i].Cells[3].Text, this.txtEventProcId.Text);
            if (filedDocType != "")
            {
                DropDownList list = (DropDownList)this.grdViewDocuments.Items[i].FindControl("ddlreport");
                CheckBox box = (CheckBox)this.grdViewDocuments.Items[i].FindControl("chkView");
                list.Text = filedDocType;
                box.Checked = true;
            }
        }
    }

    private void SaveDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        new DataSet();
        try
        {
            ArrayList list;
            this._associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(this.txtCaseID.Text, this.txtCompanyID.Text, "");
            foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
            {
                list = new ArrayList();
                list.Add(this.txtDiagnosisSetID.Text);
                list.Add(this.txtCaseID.Text);
                if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
                {
                    list.Add("");
                }
                else
                {
                    list.Add("");
                }
                list.Add(item.Cells[0].Text.ToString());
                list.Add(this.txtCompanyID.Text);
                list.Add(item.Cells[5].Text.ToString());
                this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
            }
            foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
            {
                CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
                if (box.Checked)
                {
                    list = new ArrayList();
                    list.Add(this.txtDiagnosisSetID.Text);
                    list.Add(this.txtCaseID.Text);
                    if (this.objSessionBillingCompany.BT_REFERRING_FACILITY)
                    {
                        list.Add("");
                    }
                    else
                    {
                        list.Add("");
                    }
                    list.Add(item2.Cells[1].Text.ToString());
                    list.Add(this.txtCompanyID.Text);
                    list.Add(this.extddlSpecialityDia.Text);
                    this._associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(list);
                }
            }
            this.GetAssignedDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            this.GetDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
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

    protected int SaveMulitpleProcCode(string typeCodeId, string eventProcId)
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        int num = 0;
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_CREATE_PROCEDURE_LHR", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@I_EVENT_PROC_ID", eventProcId);
            selectCommand.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", typeCodeId);
            DataSet dataSet = new DataSet();
            new SqlDataAdapter(selectCommand).Fill(dataSet);
            if (((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0)) && (dataSet.Tables[0].Rows[0][0].ToString() == "1"))
            {
                num = 1;
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
        return num;
    }

    public void SaveProcedureDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        new DataSet();
        try
        {
            ArrayList list;
            this._associateDiagnosisCodeBO.DeleteProcedureAssociateDignosisCode(this.txtCaseID.Text, this.txtCompanyID.Text, this.txtEventProcId.Text);
            foreach (DataGridItem item in this.grdSelectedDiagnosisCode.Items)
            {
                list = new ArrayList();
                list.Add(this.txtEventProcId.Text);
                list.Add(this.txtCaseID.Text);
                list.Add(txt_event_ID.Text.ToString());
                list.Add(item.Cells[0].Text.ToString());
                list.Add(item.Cells[5].Text.ToString());
                list.Add(this.txtCompanyID.Text);
                list.Add(this.txtDiagnosisSetID.Text);
                this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
            }
            foreach (DataGridItem item2 in this.grdNormalDgCode.Items)
            {
                CheckBox box = (CheckBox)item2.Cells[1].FindControl("chkSelect");
                if (box.Checked)
                {
                    list = new ArrayList();
                    list.Add(this.txtEventProcId.Text);
                    list.Add(this.txtCaseID.Text);
                    list.Add(txt_event_ID.Text.ToString());
                    list.Add(item2.Cells[1].Text.ToString());
                    list.Add(this.extddlSpecialityDia.Text);
                    list.Add(this.txtCompanyID.Text);
                    list.Add(this.txtDiagnosisSetID.Text);
                    this._associateDiagnosisCodeBO.SaveProcedureAssociateDignosisCode(list);
                }
            }
            this.GetAssignedProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
            this.GetProcedureDiagnosisCode(this.txtCaseID.Text, this.objSessionBillingCompany.SZ_COMPANY_ID, this.txtEventProcId.Text, "GET_DIAGNOSIS_CODE");
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

    public int saveReportInDocumentManager_NFMIS(ArrayList objAL)
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        int num = 0;
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFMIS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            command.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
            command.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
            command.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
            command.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
            if (objAL[5].ToString().Equals("X-RAY"))
            {
                command.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
            }
            else
            {
                command.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
            }
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();
            num = (int)parameter.Value;
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
        return num;
    }

    public void ShowAuditGrid()
    {
        if (this.txtUserRole.Text.ToLower() == "admin")
        {
            this.tpAudit.Visible = true;
            Bill_Sys_CheckoutBO tbo = new Bill_Sys_CheckoutBO();
            DataSet auditDetails = new DataSet();
            auditDetails = tbo.GetAuditDetails(this.txtCompanyID.Text);
            this.dgAudit.DataSource = auditDetails;
            this.dgAudit.DataBind();
        }
    }

    protected void tabcontainerprocedurecodedocs_ActiveTabChanged(object sender, EventArgs e)
    {
        if (this.tabcontainerprocedurecodedocs.ActiveTabIndex == 4)
        {
            this.ShowAuditGrid();
        }
    }
}
