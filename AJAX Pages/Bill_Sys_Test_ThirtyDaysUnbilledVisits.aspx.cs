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
using PDFValueReplacement;
using mbs.LienBills;
using iTextSharp.text.pdf;


public partial class Bill_Sys_Test_ThirtyDaysUnbilledVisits : PageBase
{
    string pdfPath = "";
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    Bill_Sys_NF3_Template objNF3Template;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    private ArrayList _arrayList;

    string bt_include;
    String str_1500;
    MUVGenerateFunction _MUVGenerateFunction;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Session["TestUnBilled"] == null)
            {

                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();

                ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int i = 0;
            Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
            DataSet dsReport = new DataSet();
            if (!txtFromDate.Text.Trim().Equals("") && !txtToDate.Text.Trim().Equals("")) i = DateTime.Compare(Convert.ToDateTime(txtToDate.Text), Convert.ToDateTime(txtFromDate.Text));
            if (i.Equals(1) || i.Equals(0))
            {

                if (extddlSpeciality.Text.Equals("NA"))
                {
                    if (extddlCaseType.Text.Equals("NA"))
                    {
                        dsReport = objReport.Get30DaysUnbilled_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, "---Select---", "--- Select ---");
                    }
                    else
                    {
                        dsReport = objReport.Get30DaysUnbilled_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, "---Select---", extddlCaseType.Selected_Text.ToString());
                    }
                }
                else
                {
                    if (extddlCaseType.Text.Equals("NA"))
                    {
                        dsReport = objReport.Get30DaysUnbilled_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Selected_Text.ToString(), "--- Select ---");
                    }
                    else
                    {
                        dsReport = objReport.Get30DaysUnbilled_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Selected_Text.ToString(), extddlCaseType.Selected_Text.ToString());
                    }
                }

                DataView dv1;
                dv1 = dsReport.Tables[0].DefaultView;
                dv1.Sort = "SCHEDULE_DATE,PATIENT_NAME DESC";

                grd30DaysReport.DataSource = dv1;
                grdForReport.DataSource = dv1;
                Session["ThirtyDaysUnbilled"] = dsReport;
                grd30DaysReport.DataBind();
                grdForReport.DataBind();
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
                {
                    grd30DaysReport.Columns[0].Visible = false;
                    grdForReport.Columns[0].Visible = false;
                }
                lblCount.Text = grd30DaysReport.Items.Count.ToString();
                for (int j = 0; j < grd30DaysReport.Items.Count; j++)
                {
                    if (grd30DaysReport.Items[j].Cells[29].Text.Trim().Equals("False"))
                    {
                        grd30DaysReport.Items[j].BackColor = System.Drawing.Color.Red;
                    }
                    else if (grd30DaysReport.Items[j].Cells[32].Text.Trim().Equals("0"))
                    {
                        grd30DaysReport.Items[j].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Select valid date range');", true);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
            for (int icount = 0; icount < grdForReport.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdForReport.Columns.Count; i++)
                    {
                        if (grdForReport.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdForReport.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdForReport.Columns.Count; j++)
                {
                    if (grdForReport.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdForReport.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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
    protected void grd30DaysReport_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        int iFlagCheck = 0;
        if (e.CommandName.ToString() == "ShowPop")
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;
            txtEventProc.Text = e.Item.Cells[23].Text.ToString();
            txtEvenId.Text = e.Item.Cells[19].Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text;


            iFlagCheck = 1;
            if (e.Item.Cells[29].Text.Trim().Equals("False"))
            {
                btnUploadFile.Visible = true;
                fuUploadReport.Visible = true;
                lblUP.Visible = true;
                btnUPRD.Visible = false;

            }
            else
            {
                btnUploadFile.Visible = false;
                fuUploadReport.Visible = false;
                lblUP.Visible = false;
                btnUPRD.Visible = true;
            }
            btnUploadFile.Attributes.Add("onClick", "return showuploadfile();");
            btnUPRD.Attributes.Add("onClick", "return   UpadteDoctor();");
            if (!e.Item.Cells[13].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[13].Text.ToString().Equals(""))
            {
                extddlInsuranceCompany.Text = e.Item.Cells[13].Text.ToString();
                Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
                lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                lstInsuranceCompanyAddress.DataValueField = "CODE";
                lstInsuranceCompanyAddress.DataBind();
                Page.MaintainScrollPositionOnPostBack = true;
                if (!e.Item.Cells[14].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[14].Text.ToString().Equals(""))
                {
                    lstInsuranceCompanyAddress.SelectedValue = e.Item.Cells[14].Text.ToString();
                }
            }
            if (!e.Item.Cells[12].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[12].Text.ToString().Equals(""))
            {
                txtCalimNumber.Text = e.Item.Cells[12].Text.ToString();
            }
            if (!e.Item.Cells[12].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[12].Text.ToString().Equals(""))
            {
                txtCalimNumber.Text = e.Item.Cells[12].Text.ToString();
            }
            txtSpeciality.Text = e.Item.Cells[33].Text.ToString();


            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            txtPGID.Text = e.Item.Cells[21].Text.ToString();
            txtCaseID.Text = e.Item.Cells[11].Text.ToString();
            txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
            extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlReadingDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

            //_dianosis_Association.EventProcID
            txtCaseID1.Text = txtCaseID.Text;
            // To Show Rading Doctor At Page Load:- Tushar
            // extddlReadingDoctor.Text = txtReadingDocID.Text;
            extddlReadingDoctor.Text = "NA";
            //End Of Code

            if (!e.Item.Cells[30].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[30].Text.ToString().Equals(""))
            {
                extddlReadingDoctor.Text = e.Item.Cells[30].Text.ToString();
            }
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetDiagnosisCode(txtCaseID1.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
            grdNormalDgCode.Visible = false;
            tabcontainerDiagnosisCode.ActiveTabIndex = 1;
            ModalPopupExtender1.Show();
        }


        if (e.CommandName.ToString() == "Bill")
        {
            try
            {


                if (e.Item.Cells[29].Text.Trim().Equals("False"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Visit does not have report, You can not create bill!');", true);
                    return;
                }
                ArrayList arrlst = new ArrayList();



                if (e.Item.Cells[12].Text == "&nbsp;" || e.Item.Cells[13].Text == "&nbsp;" || e.Item.Cells[14].Text == "&nbsp;")
                {
                    arrlst.Add(e.Item.Cells[1].Text);
                }


                string patientcnt = "";
                string repeat = "";
                if (arrlst.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('You do not have a claim number or an insurance company or an insurance company address added to these Case NO,You cannot proceed furher');", true);
                }
                else
                {
                    if (e.Item.Cells[30].Text == "&nbsp;")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Reading Doctotor does not exist');", true);
                    }
                    else
                    {
                        AddBillDiffCase(e);
                        BindGrid();
                    }
                }   
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                strError = strError.Replace("\n", " ");
                Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            }
        }
        else
        {

            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["ThirtyDaysUnbilled"];
            dv = ds.Tables[0].DefaultView;

            if (e.CommandName.ToString() == "ChartNo")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "CaseNO")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "PatientName")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Scheduledate")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Days")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            if (iFlagCheck == 0)
            {
                dv.Sort = txtSort.Text;
                grd30DaysReport.DataSource = dv;
                grdForReport.DataSource = dv;

                grd30DaysReport.DataBind();
                grdForReport.DataBind();

                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
                {
                    grd30DaysReport.Columns[0].Visible = false;
                    grdForReport.Columns[0].Visible = false;
                }

                for (int i = 0; i < grd30DaysReport.Items.Count; i++)
                {
                    if (grd30DaysReport.Items[i].Cells[29].Text.Trim().Equals("False"))
                    {
                        grd30DaysReport.Items[i].BackColor = System.Drawing.Color.Red;
                    }
                    else if (grd30DaysReport.Items[i].Cells[32].Text.Trim().Equals("0"))
                    {
                        grd30DaysReport.Items[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }
    }



    //#region "Logic to Generate PDF"

    //private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    ////private void GenerateAddedBillPDF(string p_szBillNumber)
    //{
    //    try
    //    {
    //        String szSpecility = p_szSpeciality;
    //        //String szSpecility = "MRI";
    //        //Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
    //        Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
    //        Session["TM_SZ_BILL_ID"] = p_szBillNumber;

    //        objNF3Template = new Bill_Sys_NF3_Template();

    //        CaseDetailsBO objCaseDetails = new CaseDetailsBO();
    //        String szCompanyID = "";
    //        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //        {
    //            szCompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
    //        }
    //        else
    //        {
    //            szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //        }

    //        if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
    //        {
    //            String szDefaultPath = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                szDefaultPath = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
    //            }
    //            else
    //            {
    //                szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
    //            }

    //            String szSourceDir = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                szSourceDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
    //            }
    //            else
    //            {
    //                szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
    //            }

    //            String szDestinationDir = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
    //                //szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

    //            }
    //            else
    //            {
    //                szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
    //                //szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

    //            }

    //            string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

    //            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
    //            String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
    //            String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
    //            String szPDFPage1 = "";
    //            String szXMLFileName;
    //            String szOriginalPDFFileName;
    //            String szLastXMLFileName;
    //            String szLastOriginalPDFFileName;
    //            String sz3and4Page = "";
    //            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
    //            String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
    //            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

    //            szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
    //            szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
    //            szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
    //            szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


    //            Boolean fAddDiag = true;



    //            GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
    //            objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

    //            String szPDFFileName = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                // Note : Generate PDF with Billing Information table. **** II
    //                szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
    //                //log.Debug("Bill Details PDF File : " + szPDFFileName);
    //            }
    //            else
    //            {
    //                // Note : Generate PDF with Billing Information table. **** II
    //                szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
    //                //log.Debug("Bill Details PDF File : " + szPDFFileName);
    //            }


    //            sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
    //            }
    //            else
    //            {

    //                szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
    //            }


    //            //log.Debug("Page1 : " + szPDFPage1);


    //            // Merge **** I AND **** II
    //            String szPDF_1_3 = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
    //                szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
    //            }
    //            else
    //            {
    //                // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
    //                szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
    //            }



    //            String szLastPDFFileName = "";
    //            String szPDFPage3 = "";
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
    //                szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
    //            }
    //            else
    //            {
    //                szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
    //            }



    //            MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

    //            szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
    //            //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
    //            String szGenereatedFileName = "";
    //            szGenereatedFileName = szDefaultPath + szLastPDFFileName;
    //            //log.Debug("GenereatedFileName : " + szGenereatedFileName);

    //            pdfPath = szGenereatedFileName;
    //            String szOpenFilePath = "";
    //            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;
    //            //szOpenFilePath = "C:\\LawAllies\\MBSUpload\\" + szGenereatedFileName;

    //            string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


    //            CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //            string newPdfFilename = "";
    //            string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //            objMyForm.initialize(KeyForCutePDF);

    //            if (objMyForm == null)
    //            {
    //                // Response.Write("objMyForm not initialized");
    //            }
    //            else
    //            {
    //                if (System.IO.File.Exists(szFileNameWithFullPath))
    //                {
    //                    //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

    //                    if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
    //                    {
    //                        if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
    //                        {
    //                        }
    //                        else
    //                        {
    //                            //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
    //                            szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
    //                        }
    //                    }

    //                }

    //            }

    //            // End Logic

    //            string szFileNameForSaving = "";

    //            // Save Entry in Table
    //            if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
    //            {
    //                szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
    //            }

    //            // End

    //            if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
    //            {
    //                szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
    //                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
    //            }
    //            else
    //            {
    //                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
    //                {
    //                    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
    //                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
    //                }
    //                else
    //                {
    //                    szFileNameForSaving = szOpenFilePath.ToString();
    //                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
    //                }
    //            }
    //            String[] szTemp;
    //            string szBillName = "";
    //            szTemp = szFileNameForSaving.Split('/');
    //            ArrayList objAL = new ArrayList();
    //            szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
    //            szBillName = szTemp[szTemp.Length - 1].ToString();

    //            if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
    //            {
    //                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
    //                {
    //                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
    //                }
    //                File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
    //            }

    //            objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
    //            objAL.Add(szDestinationDir + szBillName);
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
    //            }
    //            else
    //            {
    //                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //            }
    //            objAL.Add(Session["TM_SZ_CASE_ID"]);
    //            objAL.Add(szTemp[szTemp.Length - 1].ToString());
    //            objAL.Add(szDestinationDir);
    //            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
    //            objAL.Add(szSpecility);
    //            //objAL.Add("");
    //            objAL.Add("NF");
    //            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
    //            //objAL.Add(txtCaseNo.Text);
    //            objNF3Template.saveGeneratedBillPath(objAL);

    //            // Start : Save Notes for Bill.

    //            _DAO_NOTES_EO = new DAO_NOTES_EO();
    //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
    //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

    //            _DAO_NOTES_BO = new DAO_NOTES_BO();
    //            _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //            _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
    //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
    //            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //            {
    //                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
    //            }
    //            else
    //            {
    //                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //            }

    //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

    //            //BindLatestTransaction();

    //            // End 


    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
    //        }

    //        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
    //        String szLogicalPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
    //        pdfPath = szLogicalPath + pdfPath;
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + pdfPath + "');", true);

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    //#endregion
    //public void AddBillDiffCase(DataGridCommandEventArgs e1)
    //{
    //    try
    //    {

    //        DataSet dset0;
    //        DataSet dset1;

    //        ArrayList arrdiag = new ArrayList();
    //        ArrayList arrlst;
    //        ArrayList objArr;
    //        ArrayList _arraylist;

    //        string sz_compID = "";
    //        Bill_Sys_ReportBO objreport;
    //        DataTable dtble = new DataTable();
    //        string patientID = "";
    //        int flag = 0;
    //        int _insertFlag = 0;
    //        string billno = "";
    //        string PatientTreatmentID = "";
    //        int cnt = 0;
    //        Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

    //        DataTable dt = new DataTable();
    //        dt.Columns.Add("SZ_CASE_ID");
    //        dt.Columns.Add("SZ_PATIENT_ID");
    //        dt.Columns.Add("CHART NO");
    //        dt.Columns.Add("PATIENT NAME");
    //        dt.Columns.Add("DATE OF SERVICE");
    //        dt.Columns.Add("Patient name");
    //        dt.Columns.Add("Date Of Service");
    //        dt.Columns.Add("Procedure code");
    //        dt.Columns.Add("Description");
    //        dt.Columns.Add("Status");
    //        dt.Columns.Add("Code ID");
    //        dt.Columns.Add("EVENT ID");
    //        dt.Columns.Add("Doctor ID");
    //        dt.Columns.Add("CASE NO");
    //        dt.Columns.Add("Company ID");
    //        dt.Columns.Add("SZ_PATIENT_TREATMENT_ID");
    //        //
    //        dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
    //        //

    //        DataRow dr;

    //        objreport = new Bill_Sys_ReportBO();
    //        dtble = objreport.GetDoctorID(txtCompanyID.Text);

    //        dr = dt.NewRow();
    //        dr["SZ_CASE_ID"] = e1.Item.Cells[10].Text;
    //        dr["SZ_PATIENT_ID"] = e1.Item.Cells[9].Text;
    //        dr["CHART NO"] = e1.Item.Cells[23].Text;
    //        dr["PATIENT NAME"] = e1.Item.Cells[24].Text;
    //        dr["DATE OF SERVICE"] = e1.Item.Cells[26].Text;
    //        dr["Patient name"] = e1.Item.Cells[25].Text;
    //        dr["Date Of Service"] = e1.Item.Cells[26].Text;
    //        dr["Procedure code"] = e1.Item.Cells[14].Text;
    //        dr["Description"] = e1.Item.Cells[15].Text;
    //        dr["Status"] = e1.Item.Cells[16].Text;
    //        dr["Code ID"] = e1.Item.Cells[17].Text;
    //        dr["EVENT ID"] = e1.Item.Cells[18].Text;
    //        dr["Doctor ID"] = e1.Item.Cells[19].Text;
    //        dr["CASE NO"] = e1.Item.Cells[24].Text;
    //        dr["Company ID"] = e1.Item.Cells[21].Text;
    //        dr["SZ_PATIENT_TREATMENT_ID"] = e1.Item.Cells[22].Text;
    //        //
    //        dr["SZ_PROCEDURE_GROUP_ID"] = e1.Item.Cells[20].Text;
    //        //
    //        dt.Rows.Add(dr);


    //        dt.DefaultView.Sort = "SZ_CASE_ID ASC";
    //        //Session["test"] = dt;
    //        objreport = new Bill_Sys_ReportBO();
    //        dtble = objreport.GetDoctorID(txtCompanyID.Text);
    //        int c = 0;
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            //
    //            Session["Procedure_Code"] = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(); ;
    //            //
    //            cnt = 0;
    //            string Pid = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
    //            foreach (DataRow drow in dt.Rows)
    //            {
    //                if (Pid == drow["SZ_PATIENT_ID"].ToString())
    //                {
    //                    cnt++;
    //                }
    //            }
    //            if (cnt == 1)
    //            {
    //                txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
    //                txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
    //                txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
    //                txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
    //                sz_compID = dt.Rows[i]["Company ID"].ToString();

    //                Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
    //                obj1.SZ_CASE_ID = txtCaseID.Text;
    //                obj1.SZ_COMAPNY_ID = sz_compID;
    //                obj1.SZ_CASE_NO = txtCaseNo.Text;
    //                obj1.SZ_PATIENT_ID = txtPatientID.Text;
    //                Session["CASE_OBJECT"] = obj1;

    //                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //                {
    //                    txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
    //                }
    //                else
    //                {
    //                    txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //                }


    //                dset0 = new DataSet();
    //                dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode("", txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
    //                if (dset0.Tables[0].Rows.Count > 0)
    //                {
    //                    arrlst = new ArrayList();
    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    objreport = new Bill_Sys_ReportBO();
    //                    arrlst.Add(txtCaseID.Text);
    //                    arrlst.Add(txtBillDate.Text);
    //                    arrlst.Add(txtCompanyID.Text);
    //                    arrlst.Add(dtble.Rows[0]["CODE"].ToString());
    //                    arrlst.Add("0");
    //                    arrlst.Add(txtReadingDocID.Text);
    //                    arrlst.Add(txtRefCompanyID.Text);
    //                    objreport.InsertBillTransactionData(arrlst);

    //                    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
    //                    billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

    //                    _DAO_NOTES_EO = new DAO_NOTES_EO();
    //                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
    //                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

    //                    _DAO_NOTES_BO = new DAO_NOTES_BO();
    //                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //                    _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
    //                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

    //                    objreport = new Bill_Sys_ReportBO();
    //                    arrlst = new ArrayList();
    //                    arrlst.Add(txtPatientID.Text);
    //                    arrlst.Add(dt.Rows[i]["Code ID"].ToString());
    //                    arrlst.Add(txtCompanyID.Text);
    //                    arrlst.Add(dtble.Rows[0]["CODE"].ToString());
    //                    PatientTreatmentID = objreport.GetTreatmentID(arrlst);

    //                    objArr = new ArrayList();
    //                    objArr.Add(dt.Rows[i]["Procedure code"].ToString());
    //                    objArr.Add(dt.Rows[i]["Description"].ToString());
    //                    objArr.Add(txtCompanyID.Text);
    //                    objreport = new Bill_Sys_ReportBO();
    //                    dset1 = new DataSet();
    //                    dset1 = objreport.GetProcCodeDetails(objArr);

    //                    _arraylist = new ArrayList();
    //                    _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
    //                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                    _arraylist.Add(billno);
    //                    _arraylist.Add(dt.Rows[i]["DATE OF SERVICE"].ToString());
    //                    _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                    _arraylist.Add("1");
    //                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                    _arraylist.Add("1");
    //                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                    _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
    //                    _arraylist.Add(dt.Rows[i]["SZ_CASE_ID"].ToString());
    //                    _arraylist.Add(dt.Rows[i]["Code ID"].ToString());
    //                    _arraylist.Add("");
    //                    _arraylist.Add("");
    //                    _arraylist.Add(dt.Rows[i]["SZ_PATIENT_TREATMENT_ID"].ToString());
    //                    _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);

    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
    //                    if (dset0.Tables[0].Rows.Count > 0)
    //                    {
    //                        for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
    //                        {
    //                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                            _arraylist = new ArrayList();
    //                            _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
    //                            _arraylist.Add(billno);
    //                            _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
    //                        }
    //                    }

    //                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //                    {
    //                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
    //                        //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
    //                        GenerateAddedBillPDF(billno, sz_speciality);
    //                    }
    //                    else
    //                    {
    //                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
    //                        //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
    //                        GenerateAddedBillPDF(billno, sz_speciality);
    //                    }
    //                }
    //                else
    //                {

    //                    //objArr = new ArrayList();
    //                    arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
    //                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
    //                }
    //            }
    //            else if (cnt > 1)
    //            {

    //                txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
    //                txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
    //                txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
    //                txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
    //                sz_compID = dt.Rows[i]["Company ID"].ToString();

    //                Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
    //                obj1.SZ_CASE_ID = txtCaseID.Text;
    //                obj1.SZ_COMAPNY_ID = sz_compID;
    //                obj1.SZ_CASE_NO = txtCaseNo.Text;
    //                obj1.SZ_PATIENT_ID = txtPatientID.Text;
    //                Session["CASE_OBJECT"] = obj1;

    //                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //                {
    //                    txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
    //                }
    //                else
    //                {
    //                    txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //                }


    //                dset0 = new DataSet();
    //                dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode("", txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
    //                if (dset0.Tables[0].Rows.Count > 0)
    //                {
    //                    arrlst = new ArrayList();
    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    objreport = new Bill_Sys_ReportBO();
    //                    arrlst.Add(txtCaseID.Text);
    //                    arrlst.Add(txtBillDate.Text);
    //                    arrlst.Add(txtCompanyID.Text);
    //                    arrlst.Add(dtble.Rows[0]["CODE"].ToString());
    //                    arrlst.Add("0");
    //                    arrlst.Add(txtReadingDocID.Text);
    //                    arrlst.Add(txtRefCompanyID.Text);
    //                    objreport.InsertBillTransactionData(arrlst);

    //                    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
    //                    billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

    //                    _DAO_NOTES_EO = new DAO_NOTES_EO();
    //                    _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
    //                    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

    //                    _DAO_NOTES_BO = new DAO_NOTES_BO();
    //                    _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //                    _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
    //                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //                    _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

    //                    objreport = new Bill_Sys_ReportBO();
    //                    arrlst = new ArrayList();
    //                    arrlst.Add(txtPatientID.Text);
    //                    arrlst.Add(dt.Rows[i]["Code ID"].ToString());
    //                    arrlst.Add(txtCompanyID.Text);
    //                    arrlst.Add(dtble.Rows[0]["CODE"].ToString());
    //                    PatientTreatmentID = objreport.GetTreatmentID(arrlst);

    //                    objArr = new ArrayList();
    //                    objArr.Add(dt.Rows[i]["Procedure code"].ToString());
    //                    objArr.Add(dt.Rows[i]["Description"].ToString());
    //                    objArr.Add(txtCompanyID.Text);
    //                    objreport = new Bill_Sys_ReportBO();
    //                    dset1 = new DataSet();
    //                    dset1 = objreport.GetProcCodeDetails(objArr);

    //                    for (int k = 0; k < cnt; k++)
    //                    {
    //                        //
    //                        if ((k + i) < cnt)
    //                        {
    //                            if (dt.Rows[k + i]["SZ_PROCEDURE_GROUP_ID"].ToString() == Session["Procedure_Code"].ToString())
    //                            {
    //                                //
    //                                objArr = new ArrayList();
    //                                objArr.Add(dt.Rows[k + i]["Procedure code"].ToString());
    //                                objArr.Add(dt.Rows[k + i]["Description"].ToString());
    //                                objArr.Add(txtCompanyID.Text);
    //                                objreport = new Bill_Sys_ReportBO();
    //                                dset1 = new DataSet();
    //                                dset1 = objreport.GetProcCodeDetails(objArr);

    //                                _arraylist = new ArrayList();
    //                                _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
    //                                _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                                _arraylist.Add(billno);
    //                                _arraylist.Add(dt.Rows[k + i]["DATE OF SERVICE"].ToString());
    //                                _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //                                _arraylist.Add("1");
    //                                _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                                _arraylist.Add("1");
    //                                _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                                _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
    //                                _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
    //                                _arraylist.Add(dt.Rows[k + i]["SZ_CASE_ID"].ToString());
    //                                _arraylist.Add(dt.Rows[k + i]["Code ID"].ToString());
    //                                _arraylist.Add("");
    //                                _arraylist.Add("");
    //                                //_arraylist.Add(PatientTreatmentID);
    //                                _arraylist.Add(dt.Rows[k + i]["SZ_PATIENT_TREATMENT_ID"].ToString());
    //                                _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
    //                                c = c + 1;
    //                            }
    //                        }
    //                    }

    //                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                    _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
    //                    if (dset0.Tables[0].Rows.Count > 0)
    //                    {
    //                        for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
    //                        {
    //                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                            _arraylist = new ArrayList();
    //                            _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
    //                            _arraylist.Add(billno);
    //                            _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
    //                        }
    //                    }

    //                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
    //                    {
    //                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
    //                        //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
    //                        GenerateAddedBillPDF(billno, sz_speciality);
    //                    }
    //                    else
    //                    {
    //                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    //                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
    //                        //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
    //                        GenerateAddedBillPDF(billno, sz_speciality);
    //                    }
    //                    i = c - 1;
    //                }
    //                else
    //                {

    //                    //objArr = new ArrayList();
    //                    arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
    //                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
    //                }
    //                //i += cnt - 1;

    //            }

    //        }
    //        string patientcnt = "";
    //        if (arrdiag.Count > 0)
    //        {
    //            for (int l = 0; l < arrdiag.Count; l++)
    //            {
    //                patientcnt += arrdiag[l].ToString() + ", ";
    //            }



    //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Cannot create bill for '" + patientcnt + "' as no diagnosis code assign for the patient');", true);
    //        }
    //        ///BindReffGrid();
    //        /// setLabels();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    //private string MergePDF.MergePDFFiles(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    //{
    //    try
    //    {
    //        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //        int iResult = 0;
    //        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //        objMyForm.initialize(KeyForCutePDF);

    //        if (objMyForm == null)
    //        {

    //        }
    //        else
    //        {
    //            if (System.IO.File.Exists(p_szSource1) && System.IO.File.Exists(p_szSource2))
    //            {
    //                iResult = objMyForm.mergePDF(p_szSource1, p_szSource2, p_szDestinationFileName);
    //                //     iResult = objMyForm.mergePDF("D:/1.pdf", "D:/2.pdf", "D:/3.pdf");
    //            }
    //        }
    //        if (iResult == 0)
    //            return "FAIL";
    //        else
    //            return "SUCCESS";
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    public void AddBillDiffCase(DataGridCommandEventArgs e1)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            DataSet dset0;
            DataSet dset1;

            ArrayList arrdiag = new ArrayList();
            ArrayList arrlst;
            ArrayList objArr;
            ArrayList _arraylist;

            string sz_compID = "";
            Bill_Sys_ReportBO objreport;
            DataTable dtble = new DataTable();
            string patientID = "";
            int flag = 0;
            int _insertFlag = 0;
            string billno = "";
            string PatientTreatmentID = "";
            int cnt = 0;
            Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_CASE_ID");
            dt.Columns.Add("SZ_PATIENT_ID");
            dt.Columns.Add("CHART NO");
            dt.Columns.Add("PATIENT NAME");
            dt.Columns.Add("DATE OF SERVICE");
            dt.Columns.Add("Patient name");
            dt.Columns.Add("Date Of Service");
            dt.Columns.Add("Procedure code");
            dt.Columns.Add("Description");
            dt.Columns.Add("Status");
            dt.Columns.Add("Code ID");
            dt.Columns.Add("EVENT ID");
            dt.Columns.Add("Doctor ID");
            dt.Columns.Add("CASE NO");
            dt.Columns.Add("Company ID");
            dt.Columns.Add("SZ_PATIENT_TREATMENT_ID");
            //
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            dt.Columns.Add("SZ_STUDY_NUMBER");
            //

            DataRow dr;

            objreport = new Bill_Sys_ReportBO();
            dtble = objreport.GetDoctorID(txtCompanyID.Text);

            dr = dt.NewRow();
            dr["SZ_CASE_ID"] = e1.Item.Cells[11].Text;
            dr["SZ_PATIENT_ID"] = e1.Item.Cells[10].Text;
            dr["CHART NO"] = e1.Item.Cells[24].Text;
            dr["PATIENT NAME"] = e1.Item.Cells[25].Text;
            dr["DATE OF SERVICE"] = e1.Item.Cells[27].Text;
            dr["Patient name"] = e1.Item.Cells[26].Text;
            dr["Date Of Service"] = e1.Item.Cells[27].Text;
            dr["Procedure code"] = e1.Item.Cells[15].Text;
            dr["Description"] = e1.Item.Cells[16].Text;
            dr["Status"] = e1.Item.Cells[17].Text;
            dr["Code ID"] = e1.Item.Cells[18].Text;
            dr["EVENT ID"] = e1.Item.Cells[19].Text;
            dr["Doctor ID"] = e1.Item.Cells[30].Text;
            dr["CASE NO"] = e1.Item.Cells[25].Text;
            dr["Company ID"] = e1.Item.Cells[22].Text;
            dr["SZ_PATIENT_TREATMENT_ID"] = e1.Item.Cells[23].Text;
            //
            dr["SZ_PROCEDURE_GROUP_ID"] = e1.Item.Cells[21].Text;
            dr["SZ_STUDY_NUMBER"] = e1.Item.Cells[34].Text;
            dt.Rows.Add(dr);


            dt.DefaultView.Sort = "SZ_CASE_ID ASC";
            //Session["test"] = dt;
            objreport = new Bill_Sys_ReportBO();
            dtble = objreport.GetDoctorID(txtCompanyID.Text);
            int c = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //
                Session["Procedure_Code"] = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(); ;
                //
                cnt = 0;
                string Pid = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                foreach (DataRow drow in dt.Rows)
                {
                    if (Pid == drow["SZ_PATIENT_ID"].ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt == 1)
                {
                    txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
                    txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
                    txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                    txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
                    sz_compID = dt.Rows[i]["Company ID"].ToString();

                    Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
                    obj1.SZ_CASE_ID = txtCaseID.Text;
                    obj1.SZ_COMAPNY_ID = sz_compID;
                    obj1.SZ_CASE_NO = txtCaseNo.Text;
                    obj1.SZ_PATIENT_ID = txtPatientID.Text;
                    Session["CASE_OBJECT"] = obj1;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }


                    dset0 = new DataSet();
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(e1.Item.Cells[31].Text, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        arrlst.Add("0");
                        arrlst.Add(txtReadingDocID.Text);
                        arrlst.Add(txtRefCompanyID.Text);
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_Transaction
                        arrlst.Add(Session["Procedure_Code"].ToString());
                        //End
                        objreport.InsertBillTransactionData(arrlst);                        

                        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        objreport = new Bill_Sys_ReportBO();
                        arrlst = new ArrayList();
                        arrlst.Add(txtPatientID.Text);
                        arrlst.Add(dt.Rows[i]["Code ID"].ToString());
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);

                        _arraylist = new ArrayList();
                        _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(billno);
                        _arraylist.Add(dt.Rows[i]["DATE OF SERVICE"].ToString());
                        _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_EMG_BILL == "True")
                        {
                            if (dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == "" || dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == null || dt.Rows[i]["SZ_STUDY_NUMBER"].ToString() == "&nbsp;")
                                 {
                                     _arraylist.Add("1");
                                  }
                            else{
                                    _arraylist.Add(dt.Rows[i]["SZ_STUDY_NUMBER"].ToString());
                                 }
                            
                        }
                        else
                        {
                            _arraylist.Add("1");
                        }
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add("1");
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                        _arraylist.Add(dt.Rows[i]["SZ_CASE_ID"].ToString());
                        _arraylist.Add(dt.Rows[i]["Code ID"].ToString());
                        _arraylist.Add("");
                        _arraylist.Add("");
                        _arraylist.Add(dt.Rows[i]["SZ_PATIENT_TREATMENT_ID"].ToString());
                        _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);

                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                        if (dset0.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
                            {
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _arraylist = new ArrayList();
                                _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
                                _arraylist.Add(billno);
                                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                            }
                        }
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        Session["WC_Speciality"] = sz_speciality;

                        string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                        string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;

                        if (e1.Item.Cells[28].Text.Trim().Equals("WC000000000000000001"))
                        {
                            string caseType = e1.Item.Cells[28].Text;
                            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                            {
                                GeneratePDFForWorkerComp(billno, CaseId, "");
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "checkExistingBills('" + caseType + "','" + billno + "','" + sz_speciality + "')", true);
                            }
                        }
                        else if (e1.Item.Cells[28].Text.Trim().Equals("WC000000000000000003"))
                        {
                            Bill_Sys_PVT_Template _objPvtBill;
                            _objPvtBill = new Bill_Sys_PVT_Template();
                            bool isReferingFacility = true;
                            string szCaseId = CaseId;
                            string szBillId = billno;
                            string szUserId = UserId;
                            Session["TM_SZ_CASE_ID"] = szCaseId;
                            string PathToOpen = _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyID, szCaseId, sz_speciality, CmpName, szBillId, UserName, szUserId);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + PathToOpen + "');", true);

                        }
                        else if (e1.Item.Cells[28].Text.Trim().Equals("WC000000000000000004"))
                        {
                            Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
                            Session["TM_SZ_BILL_ID"] = billno;
                            _MUVGenerateFunction = new MUVGenerateFunction();
                            objNF3Template = new Bill_Sys_NF3_Template();
                            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + sz_speciality + "/";
                            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                            mbs.LienBills.Lien obj = new Lien();
                            string path;
                    //Tushar
                    string bt_CaseType;
                    string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    bt_include = _MUVGenerateFunction.get_bt_include(strComp, sz_speciality, "", "Speciality");
                    bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000004", "CaseType");
                    if (bt_include == "True" && bt_CaseType == "True")
                    {
                        str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                        String FileName;
                        FileName = obj.GenratePdfForLienWithMuv(strComp, billno, sz_speciality, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szUserName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, szUserId);
                        MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDestinationDir + FileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        path = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf");

                        ArrayList objAL = new ArrayList();
                        objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                        objAL.Add(szDestinationDir + str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        objAL.Add(Session["TM_SZ_CASE_ID"]);
                        objAL.Add(str_1500.Replace(".pdf", "_MER.pdf"));
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(sz_speciality);
                        objAL.Add("NF");
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                        objNF3Template.saveGeneratedBillPath(objAL);

                        // Start : Save Notes for Bill.

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = str_1500;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                    }
                    else
                    {
                      path = obj.GenratePdfForLien(szCompanyID, billno, sz_speciality, CaseId, UserName, CaseNO, UserId);
                    }
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + path + "');", true);
                        }
                        else
                        {
                            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
                            string PathToOpen = GenerateAddedBillPDF(billno, sz_speciality, CaseId, szCompanyID, CmpName, UserId, UserName, CaseNO);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + PathToOpen + "');", true);

                            //GenerateAddedBillPDF(billno, sz_speciality);
                        }

                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        //{
                        //    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        //    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                        //    GenerateAddedBillPDF(billno, sz_speciality);
                        //}
                        //else
                        //{
                        //    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        //    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                        //    GenerateAddedBillPDF(billno, sz_speciality);
                        //}
                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                }
                else if (cnt > 1)
                {

                    txtCaseID.Text = dt.Rows[i]["SZ_CASE_ID"].ToString();
                    txtReadingDocID.Text = dt.Rows[i]["Doctor ID"].ToString();
                    txtPatientID.Text = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                    txtCaseNo.Text = dt.Rows[i]["CASE NO"].ToString();
                    sz_compID = dt.Rows[i]["Company ID"].ToString();

                    Bill_Sys_CaseObject obj1 = new Bill_Sys_CaseObject();
                    obj1.SZ_CASE_ID = txtCaseID.Text;
                    obj1.SZ_COMAPNY_ID = sz_compID;
                    obj1.SZ_CASE_NO = txtCaseNo.Text;
                    obj1.SZ_PATIENT_ID = txtPatientID.Text;
                    Session["CASE_OBJECT"] = obj1;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }


                    dset0 = new DataSet();
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(e1.Item.Cells[31].Text, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        arrlst.Add("0");
                        arrlst.Add(txtReadingDocID.Text);
                        arrlst.Add(txtRefCompanyID.Text);
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_Transaction
                        arrlst.Add(Session["Procedure_Code"].ToString());
                        //End
                        objreport.InsertBillTransactionData(arrlst);

                        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                        billno = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                        objreport = new Bill_Sys_ReportBO();
                        arrlst = new ArrayList();
                        arrlst.Add(txtPatientID.Text);
                        arrlst.Add(dt.Rows[i]["Code ID"].ToString());
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dtble.Rows[0]["CODE"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);

                        for (int k = 0; k < cnt; k++)
                        {
                            //
                            if ((k + i) < cnt)
                            {
                                if (dt.Rows[k + i]["SZ_PROCEDURE_GROUP_ID"].ToString() == Session["Procedure_Code"].ToString())
                                {
                                    //
                                    objArr = new ArrayList();
                                    objArr.Add(dt.Rows[k + i]["Procedure code"].ToString());
                                    objArr.Add(dt.Rows[k + i]["Description"].ToString());
                                    objArr.Add(txtCompanyID.Text);
                                    objreport = new Bill_Sys_ReportBO();
                                    dset1 = new DataSet();
                                    dset1 = objreport.GetProcCodeDetails(objArr);

                                    _arraylist = new ArrayList();
                                    _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                    _arraylist.Add(billno);
                                    _arraylist.Add(dt.Rows[k + i]["DATE OF SERVICE"].ToString());
                                    _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    _arraylist.Add("1");
                                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                    _arraylist.Add("1");
                                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                    _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                    _arraylist.Add(dtble.Rows[0]["CODE"].ToString());
                                    _arraylist.Add(dt.Rows[k + i]["SZ_CASE_ID"].ToString());
                                    _arraylist.Add(dt.Rows[k + i]["Code ID"].ToString());
                                    _arraylist.Add("");
                                    _arraylist.Add("");
                                    //_arraylist.Add(PatientTreatmentID);
                                    _arraylist.Add(dt.Rows[k + i]["SZ_PATIENT_TREATMENT_ID"].ToString());
                                    _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
                                    c = c + 1;
                                }
                            }
                        }

                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                        if (dset0.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dset0.Tables[0].Rows.Count; j++)
                            {
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _arraylist = new ArrayList();
                                _arraylist.Add(dset0.Tables[0].Rows[j]["CODE"].ToString());
                                _arraylist.Add(billno);
                                _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                            }
                        }
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        if (e1.Item.Cells[28].Text.Trim().Equals("WC000000000000000001"))
                        {
                            string caseType = e1.Item.Cells[28].Text;
                            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                            {
                                GeneratePDFForWorkerComp(billno, txtCaseID.Text, "");
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "checkExistingBills('" + caseType + "','" + billno + "','" + sz_speciality + "')", true);
                            }

                        }
                        else
                        {
                            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                            string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                            string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
                            string PathToOpen = GenerateAddedBillPDF(billno, sz_speciality, CaseId, szCompanyID, CmpName, UserId, UserName, CaseNO);
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + PathToOpen + "');", true);
                            //GenerateAddedBillPDF(billno, sz_speciality);
                        }
                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        //{
                        //    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        //    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                        //    GenerateAddedBillPDF(billno, sz_speciality);
                        //}
                        //else
                        //{
                        //    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        //    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                        //    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                        //    GenerateAddedBillPDF(billno, sz_speciality);
                        //}
                        //i = c - 1;
                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                    //i += cnt - 1;

                }

            }
            string patientcnt = "";
            if (arrdiag.Count > 0)
            {
                for (int l = 0; l < arrdiag.Count; l++)
                {
                    patientcnt += arrdiag[l].ToString() + ", ";
                }


                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Cannot create bill  as no diagnosis code assign for the patient');", true);

            }

            /// setLabels();
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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



    #region "Worker Compenstion PDF"
    public void GeneratePDFForWorkerComp(string szBillNumber, string szCaseID, string p_szPDFNo)
    {
        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
            // string speciality = Session["WC_Speciality"].ToString();
            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
            hdnWCPDFBillNumber.Value = szBillNumber;
            hdnSpeciality.Value = Session["WC_Speciality"].ToString();
            //string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, hdnSpeciality.Value.ToString(), 1);
            string szFinalPath = _objNFBill.GeneratePDFForReferalWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szCompanyID, CmpName, UserId, UserName, hdnSpeciality.Value.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
        }
        else
        {

            String szURLDocumentManager = "";
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            szURLDocumentManager = ApplicationSettings.GetParameterValue("DocumentManagerURL");
            String szDefaultPhysicalPath = _bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
            string strGenFileName = "";
            try
            {
                if (p_szPDFNo == "1")
                {
                    PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                    // strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    //strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName, 4);
                    ArrayList objAL = new ArrayList();
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                    objAL.Add(szBillNumber);
                    objAL.Add(szCaseID);
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                    string openPath = szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + openPath + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + openPath + "');", true);
                }

                if (p_szPDFNo == "2")
                {
                    PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                    // strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    //strGenFileName = lfnMergeDiagCodePage(szDefaultPhysicalPath, strGenFileName, 4);
                    ArrayList objAL = new ArrayList();
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                    objAL.Add(szBillNumber);
                    objAL.Add(szCaseID);
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName + "'); ", true);
                    string openPath = szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + openPath + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + openPath + "');", true);
                }

                if (p_szPDFNo == "3")
                {
                    PDFValueReplacement.PDFValueReplacement _pDFValueReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string xmlFilePath = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();//"http://localhost/BILLINGSYSTEM/c4.2.pdf";
                    //strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    strGenFileName = _pDFValueReplacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEST_TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), xmlFilePath, szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    //strGenFileName = lfnMergeDiagCodePageForC43(szDefaultPhysicalPath, strGenFileName, 2);
                    ArrayList objAL = new ArrayList();
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName);
                    objAL.Add(szBillNumber);
                    objAL.Add(szCaseID);
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _bill_Sys_NF3_Template.saveGeneratedNF3File(objAL);
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName + "'); ", true);
                    string openPath = szURLDocumentManager + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName;
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + openPath + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + openPath + "');", true);

                }
                // Copy generated file from Packet Document to WC File.
                objNF3Template = new Bill_Sys_NF3_Template();

                String szBasePhysicalPath = objNF3Template.getPhysicalPath();
                String szNewPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/No Fault File/Bills/" + hdnSpeciality.Value.Trim() + "/";
                if (File.Exists(szBasePhysicalPath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName))
                {
                    if (!Directory.Exists(szBasePhysicalPath + szNewPath))
                    {
                        Directory.CreateDirectory(szBasePhysicalPath + szNewPath);
                    }
                    File.Copy(szBasePhysicalPath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + strGenFileName, szBasePhysicalPath + szNewPath + strGenFileName);
                }

                ArrayList objAL1 = new ArrayList();
                objAL1.Add(szBillNumber); // SZ_BILL_NUMBER
                objAL1.Add(szNewPath + strGenFileName); // SZ_BILL_PATH
                objAL1.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                objAL1.Add(szCaseID);
                objAL1.Add(strGenFileName); // SZ_BILL_NAME
                objAL1.Add(szNewPath); // SZ_BILL_FILE_PATH
                objAL1.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                objAL1.Add(hdnSpeciality.Value.Trim());
                objAL1.Add("WC");
                objAL1.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                objNF3Template.saveGeneratedBillPath(objAL1);

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = strGenFileName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                strError = strError.Replace("\n", " ");
                Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
            }
        }
    }

    private string lfnMergeDiagCodePage(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            String szNextDiagPDFFileName = "";
            GeneratePDFFile.GenerateC42PDF objGeneratePDF = new GeneratePDFFile.GenerateC42PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + szNextDiagPDFFileName, p_szDefaultPath + szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf"));
                return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private string lfnMergeDiagCodePageForC43(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        try
        {
            _bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
            string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");

            String szNextDiagPDFFileName = "";
            GeneratePDFFile.GenerateC43PDF objGeneratePDF = new GeneratePDFFile.GenerateC43PDF();

            if (szGenerateNextDiagPage == "CI_0000005" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }

            if (szGenerateNextDiagPage == "CI_0000004" && _bill_Sys_NF3_Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords)
            {
                szNextDiagPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strNextDiagFileName);
            }
            if (szNextDiagPDFFileName == "")
            {
                return p_szGeneratedFileName;
            }
            else
            {
                MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + szNextDiagPDFFileName, p_szDefaultPath + szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf"));
                return szNextDiagPDFFileName.Replace(".pdf", "_Merge.pdf");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

   

    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        try
        {// 
            //GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue);

            string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
            string speciality = Session["WC_Speciality"].ToString();
            WC_Bill_Generation _objNFBill = new WC_Bill_Generation();

            string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, speciality, 1);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    #endregion

    #region "Generate PDF Logic"

    //private string MergePDF.MergePDFFiles(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    //{
    //    try
    //    {
    //        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //        int iResult = 0;
    //        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //        objMyForm.initialize(KeyForCutePDF);

    //        if (objMyForm == null)
    //        {

    //        }
    //        else
    //        {
    //            if (System.IO.File.Exists(p_szSource1) && System.IO.File.Exists(p_szSource2))
    //            {
    //                iResult = objMyForm.mergePDF(p_szSource1, p_szSource2, p_szDestinationFileName);
    //                log.Debug("Bill_Sys_BillTransaction. Method - MergePDF.MergePDFFiles : Merge Result " + iResult);
    //                //     iResult = objMyForm.mergePDF("D:/1.pdf", "D:/2.pdf", "D:/3.pdf");
    //            }
    //        }
    //        if (iResult == 0)
    //            return "FAIL";
    //        else
    //            return "SUCCESS";
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Debug("Bill_Sys_BillTransaction. Method - MergePDF.MergePDFFiles : " + ex.Message.ToString());
    //        throw ex;
    //    }
    //}

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        try
        {
            String szSpecility = p_szSpeciality;


            Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            Session["TM_SZ_BILL_ID"] = p_szBillNumber;

            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                String szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                String szPDFPage1;
                String szXMLFileName;
                String szOriginalPDFFileName;
                String szLastXMLFileName;
                String szLastOriginalPDFFileName;
                String sz3and4Page;
                Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                Boolean fAddDiag = true;

                GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                // Note : Generate PDF with Billing Information table. **** II
                String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);


                sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());




                // Merge **** I AND **** II
                String szPDF_1_3;
                // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);

                String szLastPDFFileName;
                String szPDFPage3;
                szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());

                //Tushar
                string bt_CaseType;
                string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000002", "CaseType");

                if (bt_include == "True" && bt_CaseType == "True")
                {
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                }

                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);

                //Tushar
                if (bt_include == "True" && bt_CaseType == "True")
                {
                    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500.Replace(".pdf", "_MER.pdf"));
                    szLastPDFFileName = str_1500.Replace(".pdf", "_MER.pdf");
                }
                //

                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;



                String szOpenFilePath = "";
                szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;


                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                string newPdfFilename = "";
                string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                objMyForm.initialize(KeyForCutePDF);

                if (objMyForm == null)
                {
                    // Response.Write("objMyForm not initialized");
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath))
                    {
                        //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                        if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                        {
                            if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(Session["TM_SZ_BILL_ID"].ToString()) == 5)
                            {
                            }
                            else
                            {
                                //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                            }
                        }

                    }

                }

                // End Logic

                string szFileNameForSaving = "";

                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                }

                // End

                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "');", true);
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                        // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "');", true);
                    }
                    else
                    {
                        szFileNameForSaving = szOpenFilePath.ToString();
                        //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.ToString() + "');", true);
                    }
                }
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                szBillName = szTemp[szTemp.Length - 1].ToString();

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                }

                objAL.Add(Session["TM_SZ_BILL_ID"].ToString());
                objAL.Add(szDestinationDir + szBillName);
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                objAL.Add(Session["TM_SZ_CASE_ID"]);
                objAL.Add(szTemp[szTemp.Length - 1].ToString());
                objAL.Add(szDestinationDir);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                objAL.Add(szSpecility);
                objAL.Add("NF");
                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);




                // End 


            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }

            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();


        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

    protected void btnUpdateInsurance_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_ReportBO obj = new Bill_Sys_ReportBO();
            obj.Update_Insurace_Claim(txtCompanyID.Text, txtCaseID.Text, extddlInsuranceCompany.Text, lstInsuranceCompanyAddress.SelectedValue, txtCalimNumber.Text);
            lblMsg.Visible = true;
            lblMsg.Text = "Insurace Information Updated Successfully ...!";
            BindGrid();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void btnSeacrh1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdNormalDgCode.Visible = true;
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    private void BindGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            grdNormalDgCode.DataSource = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, typeid, code, description);
            grdNormalDgCode.DataBind();

        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveDiagnosisCode();
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            lblMsg.Visible = true;
            lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
            BindGrid();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void btnUPRD_Click(object sender, EventArgs e)
    {
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        _bill_Sys_ProcedureCode_BO.UpdateReadingDoctor(Convert.ToInt32(txtEventProc.Text), extddlReadingDoctor.Text);
        lblMsg.Text = "Reading Doctor Updated Successfully";
        lblMsg.Visible = true;
        BindGrid();
        ModalPopupExtender1.Show();

    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            ModalPopupExtender1.Show();
            objNF3Template = new Bill_Sys_NF3_Template();
            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            String szDestinationDir = "";
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            }
            else
            {
                szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

            } szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + txtSpeciality.Text + "/Reports/";

            strLinkPath = szDestinationDir + fuUploadReport.FileName;
            if (!Directory.Exists(szDefaultPath + szDestinationDir))
            {
                Directory.CreateDirectory(szDefaultPath + szDestinationDir);
            }
            //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
            //{
            fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
            // Start : Save report under document manager.

            ArrayList objAL = new ArrayList();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            }
            else
            {
                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
            }

            objAL.Add(txtCaseID.Text);
            objAL.Add(fuUploadReport.FileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(txtSpeciality.Text);
            ImageId = objNF3Template.saveReportInDocumentManager(objAL);

            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
            ArrayList arrOBJ;

            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(txtEventProc.Text), strLinkPath, ImageId);
            Bill_Sys_ReportBO _bill_Sys_RepotBO = new Bill_Sys_ReportBO();
            _bill_Sys_ProcedureCode_BO.UpdateReadingDoctor(Convert.ToInt32(txtEventProc.Text), extddlReadingDoctor.Text);

            lblMsg.Text = "Report Received Successfully";
            BindGrid();
            lblMsg.Visible = true;
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void btnDeassociateDiagCode_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Boolean blStatus = false;
            string szDiagIDs = "";
            foreach (DataGridItem dgiItem in grdAssignedDiagnosisCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiItem.Cells[0].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {
                    _arrayList = new ArrayList();
                    _arrayList.Add(dgiItem.Cells[1].Text.ToString());
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add("");
                    }

                    _arrayList.Add(txtCaseID.Text);
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(dgiItem.Cells[5].Text.ToString());
                    if (_associateDiagnosisCodeBO.DeleteAssociateDiagonisCode(_arrayList))
                    {
                    }
                    else
                    {
                        szDiagIDs += "  " + dgiItem.Cells[2].Text.ToString() + ",";
                    }
                }
            }
            if (szDiagIDs == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Diagnosis code deassociated successfully ...!";
            }
            else
            {
                lblMsg.Visible = true;

                lblMsg.Text = szDiagIDs + " diagnosis code used in bills. You can not de-associate it.";
            }
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            grdAssignedDiagnosisCode.CurrentPageIndex = 0;
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            BindGrid();
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void extddlInsuranceCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(extddlInsuranceCompany.Text);
            lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
            lstInsuranceCompanyAddress.DataValueField = "CODE";
            lstInsuranceCompanyAddress.DataBind();
            Page.MaintainScrollPositionOnPostBack = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void lstInsuranceCompanyAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
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
            grdNormalDgCode.CurrentPageIndex = e.NewPageIndex;
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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
    protected void grdAssignedDiagnosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAssignedDiagnosisCode.CurrentPageIndex = e.NewPageIndex;
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            tabcontainerDiagnosisCode.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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

    #region "New code from Associate Diagnosis code page"
    private void GetDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);

            grdNormalDgCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
            grdNormalDgCode.DataBind();
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            dt.Columns.Add("SZ_PROCEDURE_GROUP");
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow drNew;
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dr["SZ_PROCEDURE_GROUP"] = ds.Tables[0].Rows[intLoop]["Speciality"].ToString();
                        dr["SZ_PROCEDURE_GROUP_ID"] = ds.Tables[0].Rows[intLoop]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        dt.Rows.Add(dr);

                    }

                }
            }

            grdSelectedDiagnosisCode.DataSource = dt;
            grdSelectedDiagnosisCode.DataBind();
            lblDiagnosisCount.Text = dt.Rows.Count.ToString();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    #endregion

    private void GetAssignedDiagnosisCode(string caseID, string companyId, string doctorId, string flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            DataSet ds = new DataSet();
            // please check the below function.. it returns all the rows from the database when a doctor is selected
            ds = _associateDiagnosisCodeBO.GetDiagnosisCode(caseID, companyId, doctorId, flag);
            //    ds = _digosisCodeBO.GetDiagnosisCodeReferalList(txtCompanyID.Text, extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
            grdNormalDgCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
            grdNormalDgCode.DataBind();
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dt.Columns.Add("SZ_DIAGNOSIS_CODE");
            dt.Columns.Add("SZ_DESCRIPTION");
            dt.Columns.Add("SZ_COMPANY_ID");
            dt.Columns.Add("SZ_PROCEDURE_GROUP");
            dt.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow dr;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE_ID");
            dtNew.Columns.Add("SZ_DIAGNOSIS_CODE");
            dtNew.Columns.Add("SZ_DESCRIPTION");
            dtNew.Columns.Add("SZ_COMPANY_ID");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP");
            dtNew.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            DataRow drNew;
            for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
            {
                foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
                {
                    if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_DIAGNOSIS_CODE_ID"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE_ID"].ToString();
                        dr["SZ_DIAGNOSIS_CODE"] = ds.Tables[0].Rows[intLoop]["SZ_DIAGNOSIS_CODE"].ToString();
                        dr["SZ_DESCRIPTION"] = ds.Tables[0].Rows[intLoop]["SZ_DESCRIPTION"].ToString();
                        dr["SZ_COMPANY_ID"] = ds.Tables[0].Rows[intLoop]["SZ_COMPANY_ID"].ToString();
                        dr["SZ_PROCEDURE_GROUP"] = ds.Tables[0].Rows[intLoop]["Speciality"].ToString();
                        dr["SZ_PROCEDURE_GROUP_ID"] = ds.Tables[0].Rows[intLoop]["SZ_PROCEDURE_GROUP_ID"].ToString();
                        dt.Rows.Add(dr);

                    }

                }
            }

            grdAssignedDiagnosisCode.DataSource = dt;
            grdAssignedDiagnosisCode.DataBind();


            //BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);



        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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

    private void SaveDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
        DataSet _billDs = new DataSet();
        ArrayList _arrayList;
        try
        {

            _associateDiagnosisCodeBO.DeleteCaseAssociateDignosisCodeWithProcCode(txtCaseID.Text, txtCompanyID.Text, "");//, lstPTDProcCode.Items[j].Value.Substring(0, lstPTDProcCode.Items[j].Value.IndexOf("|")), lstPTDProcCode.Items[j].Value.Substring((lstPTDProcCode.Items[j].Value.IndexOf("|") + 1), ((lstPTDProcCode.Items[j].Value.Length - lstPTDProcCode.Items[j].Value.IndexOf("|")) - 1)));
            txtDiagnosisSetID.Text = _associateDiagnosisCodeBO.GetDiagnosisSetID();
            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];
            foreach (DataGridItem dgiProcedureCode in grdSelectedDiagnosisCode.Items)
            {


                _arrayList = new ArrayList();
                _arrayList.Add(txtDiagnosisSetID.Text);
                _arrayList.Add(txtCaseID.Text);
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    _arrayList.Add("");
                }
                else
                {
                    _arrayList.Add("");
                }
                _arrayList.Add(dgiProcedureCode.Cells[0].Text.ToString());
                _arrayList.Add(txtCompanyID.Text);
                //_arrayList.Add(_dianosis_Association.ProceuderGroupId);
                _arrayList.Add(dgiProcedureCode.Cells[5].Text.ToString());
                _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);



            }


            foreach (DataGridItem dgiProcedureCode in grdNormalDgCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[1].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {

                    _arrayList = new ArrayList();
                    _arrayList.Add(txtDiagnosisSetID.Text);
                    _arrayList.Add(txtCaseID.Text);
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        _arrayList.Add("");
                    }
                    else
                    {
                        _arrayList.Add("");
                    }
                    _arrayList.Add(dgiProcedureCode.Cells[1].Text.ToString());
                    _arrayList.Add(txtCompanyID.Text);
                    _arrayList.Add(txtPGID.Text);
                    _associateDiagnosisCodeBO.SaveCaseAssociateDignosisCode(_arrayList);
                }

            }
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            //new code from associate diagnosis page
            GetDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
        }
        catch (Exception ex)
        {
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

            if (ex.Message.ToString().Equals("Object reference not set to an instance of an object."))
            {
                string errmsg = "1";
                Response.Redirect("../Bill_Sys_Login.aspx?ErrMsg=" + errmsg);
            }
            else
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Hide();

    }


    public string GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality, string CaseId, string CmpId, string CmpName, string UserId, string UserName, string CaseNO)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string returnPath = "";
        try
        {
            _MUVGenerateFunction = new MUVGenerateFunction();

            String szSpecility = p_szSpeciality;





            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = CmpId;
            if (objCaseDetails.GetCaseType(p_szBillNumber) == "WC000000000000000002")
            {
                String szDefaultPath = CmpName + "/" + CaseId + "/Packet Document/";
                String szSourceDir = CmpName + "/" + CaseId + "/Packet Document/";
                String szDestinationDir = CmpName + "/" + CaseId + "/No Fault File/Bills/" + szSpecility + "/";
                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                String szPDFPage1;
                String szXMLFileName;
                String szOriginalPDFFileName;
                String szLastXMLFileName;
                String szLastOriginalPDFFileName;
                String sz3and4Page;
                Bill_Sys_Configuration objConfiguration = new Bill_Sys_Configuration();
                String szDiagPDFFilePosition = objConfiguration.getConfigurationSettings(szCompanyID, "GET_DIAG_PAGE_POSITION");
                String szGenerateNextDiagPage = objConfiguration.getConfigurationSettings(szCompanyID, "DIAG_PAGE");

                szXMLFileName = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                szOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                szLastXMLFileName = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                szLastOriginalPDFFileName = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();


                Boolean fAddDiag = true;

                


                GeneratePDFFile.GenerateNF3PDF objGeneratePDF = new GeneratePDFFile.GenerateNF3PDF();
                objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

                // Note : Generate PDF with Billing Information table. **** II
                String szPDFFileName = objGeneratePDF.GeneratePDF(CmpId, CmpName, UserId, UserName, CaseId, p_szBillNumber, "", strPath);


                sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(CmpId, CmpName, CaseId.ToString(), p_szBillNumber, szFile3, szFile4);

                szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, p_szBillNumber, CmpName, CaseId);




                // Merge **** I AND **** II
                String szPDF_1_3;
                // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                szPDF_1_3 = objPDFReplacement.MergePDFFiles(CmpId, CmpName, CaseId, p_szBillNumber, szPDFPage1, szPDFFileName);

                String szLastPDFFileName;
                String szPDFPage3;
                szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, p_szBillNumber, CmpName, CaseId);

                Session["TM_SZ_BILL_ID"] = p_szBillNumber;
                Session["TM_SZ_CASE_ID"] = CaseId;
                //Tushar
                string bt_CaseType;
                string strComp = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                bt_include = _MUVGenerateFunction.get_bt_include(strComp, szSpecility, "", "Speciality");
                bt_CaseType = _MUVGenerateFunction.get_bt_include(strComp, "", "WC000000000000000002", "CaseType");

                if (bt_include == "True" && bt_CaseType == "True")
                {
                    str_1500 = _MUVGenerateFunction.FillPdf(Session["TM_SZ_BILL_ID"].ToString());
                }


                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(CmpId, CmpName, CaseId.ToString(), p_szBillNumber, szPDF_1_3, szPDFPage3);


                //Tushar
                if (bt_include == "True" && bt_CaseType == "True") 
                {
                    MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szLastPDFFileName, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500, objNF3Template.getPhysicalPath() + szDefaultPath + str_1500.Replace(".pdf", "_MER.pdf"));
                    szLastPDFFileName = str_1500.Replace(".pdf", "_MER.pdf");
                }
                //


                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName,  ApplicationSettings.GetParameterValue("DocumentManagerURL") + szDefaultPath + szLastPDFFileName, p_szBillNumber, CmpName, CaseId.ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;



                String szOpenFilePath = "";
                szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szGenereatedFileName;


                string szFileNameWithFullPath = objNF3Template.getPhysicalPath() + "/" + szGenereatedFileName;


                CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
                string newPdfFilename = "";
                string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                objMyForm.initialize(KeyForCutePDF);

                if (objMyForm == null)
                {
                    // Response.Write("objMyForm not initialized");
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath))
                    {
                        //    objMyForm.mergePDF(szFileNameWithFullPath, szFileNameWithFullPath.Replace(".pdf", "_Temp.pdf").ToString(), szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString());

                        if (szGenerateNextDiagPage != Bill_Sys_Constant.constGenerateNextDiagPage && objNF3Template.getDiagnosisCodeCount(p_szBillNumber) >= 5 && szDiagPDFFilePosition == Bill_Sys_Constant.constAT_THE_END)
                        {
                            if (szGenerateNextDiagPage == "CI_0000004" && objNF3Template.getDiagnosisCodeCount(p_szBillNumber) == 5)
                            {
                            }
                            else
                            {
                                //MergePDF.MergePDFFiles(szFileNameWithFullPath, objNF3Template.getPhysicalPath() + szDefaultPath + szNextDiagPDFFileName, szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString());
                                szPDFFileName = szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf");
                            }
                        }

                    }

                }

                // End Logic

                string szFileNameForSaving = "";

                // Save Entry in Table
                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                {
                    szGenereatedFileName = szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString();
                }

                // End

                if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_NewMerge.pdf").ToString()))
                {
                    szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString();
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                    returnPath = szOpenFilePath.Replace(".pdf", "_NewMerge.pdf");
                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "');", true);
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                        // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "');", true);
                        returnPath = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                    }
                    else
                    {
                        szFileNameForSaving = szOpenFilePath.ToString();
                        //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.ToString() + "');", true);
                        returnPath = szOpenFilePath.ToString();
                    }
                }
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                szBillName = szTemp[szTemp.Length - 1].ToString();

                if (File.Exists(objNF3Template.getPhysicalPath() + szSourceDir + szBillName))
                {
                    if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                    {
                        Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                    }
                    File.Copy(objNF3Template.getPhysicalPath() + szSourceDir + szBillName, objNF3Template.getPhysicalPath() + szDestinationDir + szBillName);
                }

                objAL.Add(p_szBillNumber);
                objAL.Add(szDestinationDir + szBillName);
                objAL.Add(CmpId);
                objAL.Add(CaseId);
                objAL.Add(szTemp[szTemp.Length - 1].ToString());
                objAL.Add(szDestinationDir);
                objAL.Add(UserName);
                objAL.Add(szSpecility);
                objAL.Add("NF");
                objAL.Add(CaseNO);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = UserId;
                _DAO_NOTES_EO.SZ_CASE_ID = CaseId;
                _DAO_NOTES_EO.SZ_COMPANY_ID = CmpId;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);




                // End 


            }
            else
            {
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
            }

            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();


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
        return returnPath;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
