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
using System.IO;
using System.Text;
public partial class Bill_Sys_ThirtyDaysUnbilledVisits : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    private ArrayList objAL;
    protected void Page_Load(object sender, EventArgs e)
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (!IsPostBack)
            {
                 
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _reportBO = new Bill_Sys_ReportBO();
                extddlSpeciality.Text = _reportBO.getTop1Speciality(txtCompanyID.Text);
                extddlDiagnosisType.Flag_ID = txtCompanyID.Text;
               // extddlSpeciality.Selected_Text = "---Select---";
                if (Session["SELECTED_PROC_CODE"] != null)
                {
                    extddlSpeciality.Text = Session["SELECTED_PROC_CODE"].ToString();
                    Session["SELECTED_PROC_CODE"] = null;
                   /// extddlSpeciality.Selected_Text = "---Select---";
                }
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                   
                }
               // BindGrid();
                extddlSpeciality.Text="---Select---";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ThirtyDaysUnbilledVisits.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        objAL = new ArrayList();
        try
        {
            objAL.Add(txtCompanyID.Text);
            objAL.Add(extddlDoctor.Text);
            objAL.Add(txtFromDate.Text);
            objAL.Add(txtToDate.Text);
            objAL.Add("");
            objAL.Add("");
            objAL.Add(extddlSpeciality.Text);
        
            //send location id as parameter to function
            objAL.Add(extddlLocation.Text);
            objAL.Add(extddlCaseType.Text);
            DataSet ds = new DataSet();
            ds = _bill_Sys_Visit_BO.Last30DaysUnBilledVisitReport(objAL);
            

            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("SZ_CASE_ID");
            dtTemp.Columns.Add("PATIENT_NAME");
            dtTemp.Columns.Add("DT_EVENT_DATE");
            dtTemp.Columns.Add("DOCTOR_NAME");
            dtTemp.Columns.Add("Speciality");
            dtTemp.Columns.Add("SZ_VISIT");
            dtTemp.Columns.Add("DIAG COUNT");
            dtTemp.Columns.Add("CASE ID");
            dtTemp.Columns.Add("SZ_DOCTOR_ID");
            dtTemp.Columns.Add("SZ_PATIENT_ID");
            dtTemp.Columns.Add("Speciality_ID");
            dtTemp.Columns.Add("I_EVENT_ID");
            dtTemp.Columns.Add("NO_OF_DAYS");
            dtTemp.Columns.Add("SZ_CASE_TYPE_NAME");

            DataRow drTemp;
            String szTempCaseID = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    drTemp = dtTemp.NewRow();
                    drTemp["SZ_CASE_ID"] = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    drTemp["PATIENT_NAME"] = ds.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                    drTemp["DT_EVENT_DATE"] = ds.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                    drTemp["DOCTOR_NAME"] = ds.Tables[0].Rows[i]["DOCTOR_NAME"].ToString();
                    drTemp["Speciality"] = ds.Tables[0].Rows[i]["Speciality"].ToString();
                    drTemp["SZ_VISIT"] = ds.Tables[0].Rows[i]["SZ_VISIT"].ToString();
                    drTemp["DIAG COUNT"] = ds.Tables[0].Rows[i]["DIAG COUNT"].ToString();
                    drTemp["CASE ID"] = ds.Tables[0].Rows[i]["CASE ID"].ToString();
                    drTemp["SZ_DOCTOR_ID"] = ds.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                    drTemp["SZ_PATIENT_ID"] = ds.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                    drTemp["Speciality_ID"] = ds.Tables[0].Rows[i]["Speciality_ID"].ToString();
                    drTemp["I_EVENT_ID"] = ds.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                    drTemp["NO_OF_DAYS"] = ds.Tables[0].Rows[i]["NO_OF_DAYS"].ToString();
                    drTemp["SZ_CASE_TYPE_NAME"] = ds.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    dtTemp.Rows.Add(drTemp);
                }
                else
                {
                    if (ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString() != ds.Tables[0].Rows[i - 1]["SZ_CASE_ID"].ToString())
                    {
                        drTemp = dtTemp.NewRow();
                        drTemp["SZ_CASE_ID"] = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                        drTemp["PATIENT_NAME"] = ds.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                        drTemp["DT_EVENT_DATE"] = ds.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                        drTemp["DOCTOR_NAME"] = ds.Tables[0].Rows[i]["DOCTOR_NAME"].ToString();
                        drTemp["Speciality"] = ds.Tables[0].Rows[i]["Speciality"].ToString();
                        drTemp["SZ_VISIT"] = ds.Tables[0].Rows[i]["SZ_VISIT"].ToString();
                        drTemp["DIAG COUNT"] = ds.Tables[0].Rows[i]["DIAG COUNT"].ToString();
                        drTemp["CASE ID"] = ds.Tables[0].Rows[i]["CASE ID"].ToString();
                        drTemp["SZ_DOCTOR_ID"] = ds.Tables[0].Rows[i]["SZ_DOCTOR_ID"].ToString();
                        drTemp["SZ_PATIENT_ID"] = ds.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                        drTemp["Speciality_ID"] = ds.Tables[0].Rows[i]["Speciality_ID"].ToString();
                        drTemp["I_EVENT_ID"] = ds.Tables[0].Rows[i]["I_EVENT_ID"].ToString();
                        drTemp["NO_OF_DAYS"] = ds.Tables[0].Rows[i]["NO_OF_DAYS"].ToString();
                        drTemp["SZ_CASE_TYPE_NAME"] = ds.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                        dtTemp.Rows.Add(drTemp);
                    }
                }
            }
            grdAllReports.DataSource = dtTemp;//ds.Tables[0];
            grdAllReports.DataBind();
            //lblCount.Text = dtTemp.Rows.Count.ToString();
            //grdCount.DataSource = ds.Tables[1];
            //grdCount.DataBind();
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
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }
    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdAllReports.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdAllReports.Columns.Count; i++)
                    {
                        if (grdAllReports.Columns[i].Visible == true)
                        {
                            if (grdAllReports.Columns[i].HeaderText != "Group Service")
                            {
                                strHtml.Append("<td>");
                                strHtml.Append(grdAllReports.Columns[i].HeaderText);
                                strHtml.Append("</td>");
                            }
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdAllReports.Columns.Count; j++)
                {
                    if (grdAllReports.Columns[j].Visible == true)
                    {
                        if (grdAllReports.Columns[j].HeaderText != "Group Service")
                        {
                            strHtml.Append("<td>");
                            if (grdAllReports.Columns[j].HeaderText == "Case #")
                            {
                                strHtml.Append(grdAllReports.Items[icount].Cells[13].Text);//13
                            }
                            else if (grdAllReports.Columns[j].HeaderText == "Diagnosis Code")
                            {
                                strHtml.Append(grdAllReports.Items[icount].Cells[16].Text);//16
                            }
                            else
                            {
                                strHtml.Append(grdAllReports.Items[icount].Cells[j].Text);
                            }
                            strHtml.Append("</td>");
                        }
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue( "EXCEL_SHEET") + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename, false);


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
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindGrid();
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
    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            #region "Document Manager"
            if (e.CommandName == "Document Manager")
            {
                // Create Session for document Manager
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[2].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[13].Text.ToString();//13
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.CommandArgument.ToString();
                String szURL = "";
                String szCaseID = e.CommandArgument.ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
                //    Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
                Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData');</script>");
            }
            #endregion

            #region "Display Diagnosis Code"
            if (e.CommandName == "Display Diag Code")
            {
                objAL = new ArrayList();
                objAL.Add(e.Item.Cells[0].Text.ToString());
                objAL.Add(e.Item.Cells[1].Text.ToString());
                objAL.Add("");
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                BindGrid_DisplayDiagonosisCode(objAL);
                Session["DIAGINFO"] = objAL;
            }
            #endregion

            #region "Add Diagnosis Code"
            if (e.CommandName == "Add Diagnosis Code")
            {
                objAL = new ArrayList();
                objAL.Add(e.Item.Cells[0].Text.ToString());
                objAL.Add(e.Item.Cells[1].Text.ToString());
                objAL.Add(e.Item.Cells[2].Text.ToString());
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                Session["DIAGINFO"] = objAL;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            }
            #endregion

            #region "Open Case Details"
            if (e.CommandName == "Open Case")
            {
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[2].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[13].Text.ToString();//13
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.CommandArgument.ToString();
                String szURL = "";
                String szCaseID = e.CommandArgument.ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }
            #endregion


            #region "Add Group Service"

            if (e.CommandName == "Group Service")
            {
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[2].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[13].Text.ToString();//13
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.CommandArgument.ToString();
                String szURL = "";
                String szCaseID = e.CommandArgument.ToString();
                Session["QStrCaseID"] = szCaseID;
                Session["Case_ID"] = szCaseID;
                Session["Archived"] = "0";
                Session["QStrCID"] = szCaseID;
                Session["SelectedID"] = szCaseID;
                Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                Session["SN"] = "0";
                Session["LastAction"] = "vb_CaseInformation.aspx";
                szURL = "Document Manager/case/vb_CaseInformation.aspx";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "setDiv('" + e.Item.Cells[1].Text.ToString() + "',EID='" + e.Item.Cells[15].Text.ToString()  + "');", true);//15
            }

            #endregion
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

    #region "Code For Diagnosis Code pop up."

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objAL = new ArrayList();
            objAL = (ArrayList)Session["DIAGINFO"];
            _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
            for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (chk.Checked)
                {
                    ArrayList _arrayList = new ArrayList();
                    _arrayList.Add("");
                    _arrayList.Add(objAL[0].ToString());
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add("");
                    }
                    _arrayList.Add(grdDiagonosisCode.Items[i].Cells[1].Text.ToString());
                    _arrayList.Add(objAL[3].ToString());
                    _arrayList.Add(objAL[2].ToString());
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
                }
            }
            BindGrid();

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

    private void BindDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DigosisCodeBO _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid);
            grdDiagonosisCode.DataBind();

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

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DigosisCodeBO _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdDiagonosisCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdDiagonosisCode.DataBind();

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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (extddlDiagnosisType.Text == "NA")
            {
                BindDiagnosisGrid("");
            }
            else
            {
                BindDiagnosisGrid(extddlDiagnosisType.Text);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindDiagnosisGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "<script>javascript:showDiagnosisCodePopup();</script>", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDiagnosisCodePopup();", true);
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

    #endregion

    #region "Display Diagnosis Code"

    protected void BindGrid_DisplayDiagonosisCode(ArrayList p_objAL)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            grdDisplayDiagonosisCode.DataSource = _reportBO.getAssociatedDiagnosisCode(p_objAL);
            grdDisplayDiagonosisCode.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDisplayDiagnosisCodePopup();", true);
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

    protected void grdDisplayDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDisplayDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            objAL = new ArrayList();
            objAL = (ArrayList)Session["DIAGINFO"];
            BindGrid_DisplayDiagonosisCode(objAL);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showDisplayDiagnosisCodePopup();", true);
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

    #endregion

    protected void btnlClosePopUP_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "closePopup();", true);
        BindGrid();
    }
}
