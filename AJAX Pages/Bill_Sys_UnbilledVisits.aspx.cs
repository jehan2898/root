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

public partial class Bill_Sys_UnbilledVisits : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAL;
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
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
                _reportBO = new Bill_Sys_ReportBO();



                //extddlSpeciality.Text = _reportBO.getTop1Speciality(txtCompanyID.Text);
                //if (Session["SELECTED_PROC_CODE"] != null)
                //{
                //    extddlSpeciality.Text = Session["SELECTED_PROC_CODE"].ToString();
                //    Session["SELECTED_PROC_CODE"] = null;
                //}
                //if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                //{
                //    extddlLocation.Visible = true;
                //    lblLocationName.Visible = true;
                //    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                //}
                //BindGrid();
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
            cv.MakeReadOnlyPage("Bill_Sys_UnbilledVisits.aspx");
        }
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindGrid()
    {//Logging Start
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
            if (txtSearchOrder.Text != "")
            {
                objAL.Add(txtSearchOrder.Text);
            }
            else
            {
                objAL.Add(txtSearchOrder.Text);
            }
            objAL.Add(extddlLocation.Text);
            DataSet ds = new DataSet();
            ds = _bill_Sys_Visit_BO.UnBilledVisitReport(objAL);
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
            grdAllReports.DataSource = ds.Tables[0];
            grdAllReports.DataBind();
            lblCount.Text = ds.Tables[0].Rows.Count.ToString();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
    {//Logging Start
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
                            strHtml.Append("<td>");
                            strHtml.Append(grdAllReports.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdAllReports.Columns.Count; j++)
                {
                    if (grdAllReports.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdAllReports.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + filename);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private void ExportToExcelFixColumnName()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            strHtml.Append("<tr>");
            strHtml.Append("<td>Case #</td>");
            strHtml.Append("<td>Patient Name</td>");
            strHtml.Append("<td>Date Of Visit</td>");
            strHtml.Append("<td>Visit Type</td>");
            strHtml.Append("<td>Doctor Name</td>");
            strHtml.Append("<td>Speciality</td>");
            strHtml.Append("<td>No Of Days</td>");
            strHtml.Append("<td>Diagnosis Code</td>");
            strHtml.Append("</tr>");

            for (int icount = 0; icount < grdAllReports.Items.Count; icount++)
            {
                //Case #
                strHtml.Append("<tr><td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[9].Text);
                strHtml.Append("</td>");

                // Patient Name
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[8].Text);
                strHtml.Append("</td>");

                // Date Of Visit
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[15].Text);
                strHtml.Append("</td>");

                //Visit Type
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[5].Text);
                strHtml.Append("</td>");

                // Doctor Name
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[16].Text);
                strHtml.Append("</td>");

                // Speciality
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[17].Text);
                strHtml.Append("</td>");

                // No Of Days
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[11].Text);
                strHtml.Append("</td>");


                // Diagnosis Code
                strHtml.Append("<td>");
                strHtml.Append(grdAllReports.Items[icount].Cells[18].Text);
                strHtml.Append("</td></tr>");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          //  ExportToExcel();
            ExportToExcelFixColumnName();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "CaseSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "PatientNameSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "EventDateSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " DESC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "DoctorNameSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }
            if (e.CommandName.ToString() == "SpecialitySearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }

            if (e.CommandName.ToString() == "CaseID")
            {
                Session["CASE_OBJECT"] = "";
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[10].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[9].Text.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.Item.Cells[0].Text.ToString();
                String szURL = "";
                String szCaseID = e.Item.Cells[0].Text.ToString();
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

            #region "Add Group Service"

            if (e.CommandName == "Group Service")
            {
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[10].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[9].Text.ToString();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "setDiv('" + e.Item.Cells[1].Text.ToString() + "',EID='" + e.Item.Cells[14].Text.ToString() + "');", true);
                BindGrid();
            }

            #endregion

            #region "Document Manager"
            if (e.CommandName == "Document Manager")
            {
                // Create Session for document Manager
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[10].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[9].Text.ToString();
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
                objAL.Add(e.Item.Cells[0].Text.ToString()); // Case ID
                objAL.Add(e.Item.Cells[1].Text.ToString()); // Doctor ID
                objAL.Add("");
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                BindGrid_DisplayDiagonosisCode(objAL);
                Session["DIAGINFO"] = objAL;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Display Diagnosis Code"

    protected void BindGrid_DisplayDiagonosisCode(ArrayList p_objAL)
    {//Logging Start
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdDisplayDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {//Logging Start
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
