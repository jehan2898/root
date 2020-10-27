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
using System.Text;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Bill_Sys_ViewBillRecordDetails : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    String strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnUpdateStatus.Attributes.Add("onclick", "return UpdateStatus();");

            if (!Page.IsPostBack)
            {
                txtBillStatusdate.Text = System.DateTime.Today.ToShortDateString();
                if (Request.QueryString["flag"].ToString() == "View")
                {
                    Session["sz_StatusID"] = Request.QueryString["Status"].ToString();
                    Session["sz_DoctorID"] = Request.QueryString["speciality"].ToString();
                }
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                BindGrid();
            }
            foreach (DataGridItem dg in grdBillReportDetails.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkUpdateStatus");
                if (dg.Cells[20].Text == "TRF" || dg.Cells[20].Text == "DNL")
                {
                    chk.Enabled = false;
                }
                else
                {
                    chk.Enabled = true;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ViewBillRecordDetails.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
    protected void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _reportBO = new Bill_Sys_ReportBO();
        ArrayList objAL = new ArrayList();
        try
        {
            objAL.Add(Session["sz_StatusID"].ToString());
            objAL.Add(Session["sz_DoctorID"].ToString());
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objAL.Add(txtSearchOrder.Text);
            objAL.Add(Session["sz_Location_Id"].ToString());
            objAL.Add(Session["SZ_PROCEDURE_ID"].ToString());
            grdBillReportDetails.DataSource = getBillReportDetails(objAL);
            grdBillReportDetails.DataBind();
            grdBillReportDetails.Columns[17].Visible = false;
            grdBillReportDetails.Columns[18].Visible = false;
            grdBillReportDetails.Columns[19].Visible = false;
            Decimal toatal = 0;
            foreach (DataGridItem dg in grdBillReportDetails.Items)
            {
                if (dg.Cells[11].Text != "&nbsp;")
                {
                    toatal = toatal + Convert.ToDecimal(dg.Cells[11].Text);
                }
            }
            lblTotal.Text = toatal.ToString();
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
    //protected void grdBillReportDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    try
    //    {
    //        CheckBox chk = (CheckBox)grdBillReportDetails.FindControl("chkUpdateStatus");
    //        if (e.Item.Cells[20].Text = "TRF")
    //        {
    //            //chkUpdateStatu
                
    //            chk.Checked = false;
    //        }
    //        else
    //        {
    //            chk.Checked = true;
    //        }
    //    }
    //   catch (Exception ex)
    //    {
    //        usrMessage.PutMessage(_ex.ToString());
    //        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        usrMessage.Show();
    //    }
    //}
    
    protected void grdBillReportDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
            string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            if (e.CommandName.ToString() == "view")
            {
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[1].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[4].Text.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.Item.Cells[0].Text;
                Session["SZ_BILL_NUMBER"] = e.CommandArgument;
                Response.Redirect("AJAX Pages/Bill_Sys_BillTransaction.aspx?Type=Search", false);
            }
            else if (e.CommandName.ToString() == "Document Manager")
            {
                // Create Session for document Manager
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = e.Item.Cells[1].Text.ToString();
                _bill_Sys_CaseObject.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = e.Item.Cells[4].Text.ToString();
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = e.Item.Cells[0].Text;
                String szURL = "";
                String szCaseID = e.Item.Cells[0].Text;
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
            if (e.CommandName.ToString() == "Generate bill")
            {
                #region "Logic to view bills"
                Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
                DataSet objDS = new DataSet();
                objDS = objNF3Template.getBillList(e.Item.Cells[5].Text);
                if (objDS.Tables[0].Rows.Count > 1)
                {
                    grdViewBills.DataSource = objDS;
                    grdViewBills.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "showviewBills();", true);
                }
                else if (objDS.Tables[0].Rows.Count == 1)
                {
                    string szBillName = objDS.Tables[0].Rows[0]["PATH"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szBillName + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "alert('No bill generated ...!');", true);
                }

                #endregion
            }

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
    
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlBillStatus.Text != "NA")
            {
                _reportBO = new Bill_Sys_ReportBO();
                ArrayList objAL = new ArrayList();
                string szListOfBillIDs = "";
                Boolean flag = false;
                for (int i = 0; i < grdBillReportDetails.Items.Count; i++)
                {
                    CheckBox chkDelete1 = (CheckBox)grdBillReportDetails.Items[i].FindControl("chkUpdateStatus");
                    if (chkDelete1.Checked)
                    {
                        if (flag == false)
                        {
                            szListOfBillIDs = "'" + grdBillReportDetails.Items[i].Cells[5].Text + "'";
                            flag = true;
                        }
                        else
                        {
                            szListOfBillIDs = szListOfBillIDs + ",'" + grdBillReportDetails.Items[i].Cells[5].Text + "'";
                        }
                    }
                }
                if (szListOfBillIDs != "")
                {
                    objAL.Add(extddlBillStatus.Text);
                    objAL.Add(szListOfBillIDs);
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAL.Add(txtBillStatusdate.Text);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                    _reportBO.updateBillStatus(objAL);
                    usrMessage.PutMessage("Bill status updated successfully.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                   
                    BindGrid();
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
            for (int icount = 0; icount < grdBillReportDetails.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 3; i < grdBillReportDetails.Columns.Count-4; i++)
                    {
                        if ((i == 7) && (grdBillReportDetails.Columns[i].Visible == true))
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdBillReportDetails.Columns[19].HeaderText);
                            strHtml.Append("</td>");
                        }
                        else
                        if (grdBillReportDetails.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdBillReportDetails.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }
                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 3; j < grdBillReportDetails.Columns.Count-4; j++)
                {
                    if ((j == 7) && (grdBillReportDetails.Columns[j].Visible == true))
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdBillReportDetails.Items[icount].Cells[19].Text);
                        strHtml.Append("</td>");
                    }
                    else
                    if (grdBillReportDetails.Columns[j].Visible == true && grdBillReportDetails.Columns[j].HeaderText.Equals("Bill ID"))
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdBillReportDetails.Items[icount].Cells[17].Text);
                        strHtml.Append("</td>");
                    }
                    else
                        if (grdBillReportDetails.Columns[j].Visible == true && grdBillReportDetails.Columns[j].HeaderText.Equals("Case #"))
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdBillReportDetails.Items[icount].Cells[18].Text);
                            strHtml.Append("</td>");
                        }
                        else if (grdBillReportDetails.Columns[j].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdBillReportDetails.Items[icount].Cells[j].Text);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    public DataSet getBillReportDetails(ArrayList objAL)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        sqlCon = new SqlConnection(strsqlCon);
        ds = new DataSet();
        try
        {
            sqlCon.Open();

            sqlCmd = new SqlCommand("SP_GET_BILL_REPORT_DETAILS", sqlCon);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_SORTING_ORDER", objAL[3].ToString());
            if (objAL[4].ToString() != "" && objAL[3].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", objAL[4].ToString()); }
            if (objAL[5].ToString() != "" && objAL[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objAL[5].ToString()); }
            sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}
 