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
using XMLDMLComponent;
using mbs.dao;
using System.Data.SqlClient;

public partial class Bill_Sys_PrintPOM : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_Case _bill_Sys_Case;
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_BillingCompanyObject objCompany;
    private Bill_Sys_SystemObject objSystem;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
            objSystem = (Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"];

            ajAutoName.ContextKey = objCompany.SZ_COMPANY_ID;
            btnSendMail.Attributes.Add("onclick", "return confirm_check();");

            Button1.Attributes.Add("onclick", "return confirm_check();");

            if (!IsPostBack)
            {
                txtCompanyID.Text = objCompany.SZ_COMPANY_ID;
            }

            if (objCompany.BT_REFERRING_FACILITY == true)
            {
                extddlOffice.Flag_Key_Value = "BILLING_OFFICE_LIST";
            }
            extddlOffice.Flag_ID = txtCompanyID.Text;
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

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            DataSet objDatasetResult = new DataSet();
            txtPatientName.Text = txtPatientName.Text.Replace("'", "");
            txtPatientName.Text = txtPatientName.Text.Replace("\"", "");
            if (objSystem.SZ_LOCATION == "1")
            {
                //DataTable objDTLocationWise = new DataTable();
                //objDatasetResult = _bill_Sys_BillingCompanyDetails_BO.getPrintPOM(txtCompanyID.Text, "NotNull");
                //objDTLocationWise = DisplayLocationInGrid(objDatasetResult);
                //grdUnsentNF2.DataSource = objDTLocationWise;
                //grdUnsentNF2.DataBind();

                string szCaseList = "";
                if (txtCaseNo.Text.Length != 0)
                {
                    szCaseList = getCaseList();
                }
                String szConnection = System.Configuration.ConfigurationManager.AppSettings["Connection_String"];
                XMLDMLComponent.SQLToDAO objDao = new XMLDMLComponent.SQLToDAO(szConnection);
                mbs.dao.PrintPOM oPrintPOM = new mbs.dao.PrintPOM();

                oPrintPOM.sz_company_id = txtCompanyID.Text;
                oPrintPOM.sz_case_no = szCaseList;
                oPrintPOM.sz_patient_name = txtPatientName.Text;
                oPrintPOM.sz_location_id = "1";

                grdUnsentNF2.DataSource = objDao.LoadDataSet("SP_PRINT_POM", "mbs.dao.PrintPOM", oPrintPOM, "mbs.dao");
                grdUnsentNF2.DataBind();

                for (int i = 0; i < grdUnsentNF2.Items.Count; i++)
                {
                    string str = grdUnsentNF2.Items[i].Cells[10].Text.ToString();
                    str = str.ToString().Trim();
                    if (str.ToString().Trim() == "&nbsp;")
                    {
                        ((Label)grdUnsentNF2.Items[i].Cells[0].FindControl("lbl_Location_Id")).Visible = true;
                        ((LinkButton)grdUnsentNF2.Items[i].Cells[0].FindControl("lnkSelectCase2")).Visible = false;
                        ((CheckBox)grdUnsentNF2.Items[i].Cells[0].FindControl("ChkSent")).Visible = false;
                    }
                }
            }
            else
            {
                string szCaseList = "";
                if (txtCaseNo.Text.Length != 0)
                {
                    szCaseList = getCaseList();
                }

                //_reportBO = new Bill_Sys_ReportBO();
                //grdPayment.DataSource = _reportBO.Get_Referral_Schedule_Report(objAL);
                //grdPayment.DataBind();

                String szConnection = System.Configuration.ConfigurationManager.AppSettings["Connection_String"];
                XMLDMLComponent.SQLToDAO objDao = new XMLDMLComponent.SQLToDAO(szConnection);

                mbs.dao.PrintPOM oPrintPOM = new mbs.dao.PrintPOM();
                oPrintPOM.sz_company_id = txtCompanyID.Text;
                oPrintPOM.sz_case_no = szCaseList;
                oPrintPOM.sz_patient_name = txtPatientName.Text;
                oPrintPOM.sz_location_id = "";

                grdUnsentNF2.DataSource = objDao.LoadDataSet("SP_PRINT_POM", "mbs.dao.PrintPOM", oPrintPOM, "mbs.dao");
                grdUnsentNF2.DataBind();

                //objDatasetResult= _bill_Sys_BillingCompanyDetails_BO.getPrintPOM(txtCompanyID.Text);
                //grdUnsentNF2.DataSource = objDatasetResult;
                //grdUnsentNF2.DataBind();
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

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            creatPDF();
            //string pdffile = GeneratePDF();
            //if (pdffile != "")
            //{
            //    string szDefaultPath = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"];
            //    string szPDFName = "";
            //    szPDFName = szDefaultPath + pdffile;
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFName.ToString() + "'); ", true);
            //}
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
            for (int icount = 0; icount < grdUnsentNF2.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdUnsentNF2.Columns.Count; i++)
                    {
                        if (grdUnsentNF2.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdUnsentNF2.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdUnsentNF2.Columns.Count; j++)
                {
                    if (grdUnsentNF2.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");


                        if (j == 1)
                        {
                            strHtml.Append(grdUnsentNF2.Items[icount].Cells[11].Text);
                            if (((Label)grdUnsentNF2.Items[icount].Cells[j].FindControl("lbl_Location_Id")).Visible == true)
                            {
                                strHtml.Append("<b>Location</b>");
                            }
                        }
                        else
                        {
                            strHtml.Append(grdUnsentNF2.Items[icount].Cells[j].Text);
                        }


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

    protected void grdUnsentNF_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {

                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
                _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = e.Item.Cells[2].Text;
                _bill_Sys_CaseObject.SZ_CASE_NO = ((LinkButton)e.Item.FindControl("lnkSelectCase2")).Text;
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;

                _bill_Sys_Case = new Bill_Sys_Case();
                _bill_Sys_Case.SZ_CASE_ID = e.CommandArgument.ToString();

                Session["CASEINFO"] = _bill_Sys_Case;
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
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

    protected void btnPrintEnvelop_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        try
        {
            String szPDFPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_PDF_FILE"];
            // String szXMLPhysicalpath = ConfigurationManager.AppSettings["NF2_ENVELOPE_XML_FILE"];
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["PROVIDERNAME"];
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            String szSourceFile1 = "";
            String szSourceFile1_FullPath = "";
            String szSourceFile2 = "";
            String szSourceFile2_FullPath = "";
            String szOpenFilePath = "";

            for (int i = 0; i < grdUnsentNF2.Items.Count; i++)
            {
                String szCaseID = grdUnsentNF2.Items[i].Cells[10].Text;
                CheckBox chkDelete1 = (CheckBox)grdUnsentNF2.Items[i].FindControl("ChkSent");
                if (chkDelete1.Checked)
                {
                    string InsCompanyName = ((TextBox)grdUnsentNF2.Items[i].FindControl("txtInsDetails")).Text;
                    string Insdescription = ((TextBox)grdUnsentNF2.Items[i].FindControl("txtInsAddress")).Text;
                    string InsDetials = ((TextBox)grdUnsentNF2.Items[i].FindControl("txtInsState")).Text;
                    String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
                    // PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    ReplacePdfValues objReplacePdf = new ReplacePdfValues();
                    //string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szCaseID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);

                    // string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, extddlOffice.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);

                    // Use Function           string szGeneratedPDFName = objReplacePdf.PrintEnvelope(szXMLPhysicalpath, szPDFPhysicalpath, extddlOffice.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                    string szGeneratedPDFName = objReplacePdf.PrintEnvelope1(szXMLPhysicalpath, szPDFPhysicalpath, extddlOffice.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID, InsCompanyName, Insdescription, InsDetials);

                    if (szSourceFile1 == "")
                    {
                        szSourceFile1 = szGeneratedPDFName;
                        szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                        szOpenFilePath = szDefaultPath + szSourceFile1;
                    }
                    else
                    {
                        szSourceFile2 = szGeneratedPDFName;
                        szSourceFile2_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile2;
                        MergePDF.MergePDFFiles(szSourceFile1_FullPath, szSourceFile2_FullPath, szBasePhysicalPath + szDefaultPath + "_" + szSourceFile1);
                        szSourceFile1 = "_" + szSourceFile1;
                        szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                        szOpenFilePath = szDefaultPath + szSourceFile1;
                    }

                }
            }
            szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szOpenFilePath;
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
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

    

    #region "Display Location wise patient in grid"

    public DataTable DisplayLocationInGrid(DataSet p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataTable objDT = new DataTable();
        try
        {



            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_INSURANCE_NAME");
            objDT.Columns.Add("DT_DATE_OF_ACCIDENT");
            objDT.Columns.Add("DAYS");
            objDT.Columns.Add("CLAIM_NO");
            objDT.Columns.Add("SZ_INSURANCE_ADDRESS");
            objDT.Columns.Add("SZ_CASE_ID");


            DataRow objDR;
            string sz_Location_Name = "NA";




            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(sz_Location_Name))
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DAYS"] = objDS.Tables[0].Rows[i]["DAYS"].ToString();
                    objDR["CLAIM_NO"] = objDS.Tables[0].Rows[i]["CLAIM_NO"].ToString();
                    objDR["SZ_INSURANCE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_ADDRESS"].ToString();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();


                }
                else
                {


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_NO"] = "<b>Location</b>";
                    objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString() + "<b>";
                    objDR["SZ_INSURANCE_NAME"] = "";
                    objDR["DT_DATE_OF_ACCIDENT"] = "";
                    objDR["DAYS"] = "";
                    objDR["CLAIM_NO"] = "";
                    objDR["SZ_INSURANCE_ADDRESS"] = "";
                    objDR["SZ_CASE_ID"] = "";
                    int count = grdUnsentNF2.Items.Count;



                    objDT.Rows.Add(objDR);


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DAYS"] = objDS.Tables[0].Rows[i]["DAYS"].ToString();
                    objDR["CLAIM_NO"] = objDS.Tables[0].Rows[i]["CLAIM_NO"].ToString();
                    objDR["SZ_INSURANCE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_ADDRESS"].ToString();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();





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

        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    private string getCaseList()
    {
        string sList = "";
        Boolean Flag = false;
        txtCaseNo.Text = txtCaseNo.Text.Replace("'", "");
        txtCaseNo.Text = txtCaseNo.Text.Replace("\"", "");

        string[] sArr = txtCaseNo.Text.Split(',');

        if (sArr.Length > 0)
        {
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i].Length > 2)
                {
                    char[] ch = sArr[i].ToCharArray(0, 2);
                    for (int j = 0; j < ch.Length; j++)
                    {

                        if ((ch[j] >= 'A' && ch[j] <= 'Z') || (ch[j] >= 'a' && ch[j] <= 'z'))
                        {
                            Flag = true;
                            break;
                        }
                    }
                    if (Flag == true)
                    {
                        sArr[i] = sArr[i].Substring(2);
                        Flag = false;
                    }
                    sList = sList + "'" + sArr[i] + "',";
                }
                else
                {
                    sList = sList + "'" + sArr[i] + "',";
                }
            }
            sList = sList.Remove(sList.Length - 1);
            sList = "(" + sList + ")";
        }

        return sList;
    }


    protected string ReplaceHeaderAndFooter(String szInput, string sz_bill_no, int i_pom_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szFileData = "";
        try
        {
            GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
            szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
            szFileData = File.ReadAllText(ConfigurationManager.AppSettings["POM_HTML"]);
            szFileData = objPDF.getPrintPOM(szFileData, extddlOffice.Text);
            //szFileData = getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, sz_bill_no);
            szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", szInput);
            szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());
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

        return szFileData;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet CreateGroupData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Case #");  //case
            dt.Columns.Add("Patient Name"); //name
            dt.Columns.Add("Provider");
            dt.Columns.Add("Claim Number"); //artical
            dt.Columns.Add("Insurance Company"); //ins
            dt.Columns.Add("Insurance Address"); //ins
            dt.Columns.Add("InsDescription");
            dt.Columns.Add("CaseType");
            dt.Columns.Add("Case Id");
            foreach (DataGridItem dg in grdUnsentNF2.Items)
            {
                if (((CheckBox)dg.Cells[0].FindControl("ChkSent")).Checked == true)
                {
                    DataRow dr = dt.NewRow();

                    LinkButton lnkbtn = ((LinkButton)dg.Cells[1].FindControl("lnkSelectCase2"));
                    dr["Case #"] = lnkbtn.Text;
                    dr["Patient Name"] = dg.Cells[2].Text;

                    dr["Provider"] = extddlOffice.Selected_Text.ToString();

                    dr["Claim Number"] = dg.Cells[8].Text;

                    TextBox txtins = ((TextBox)dg.Cells[4].FindControl("txtInsDetails"));
                    if (txtins.Text == "")
                    {
                        dr["Insurance Company"] = dg.Cells[3].Text;
                        dr["Insurance Address"] = dg.Cells[9].Text;
                    }
                    else
                    {
                        dr["Insurance Company"] = txtins.Text;
                        TextBox txtinsadd = ((TextBox)dg.Cells[4].FindControl("txtInsAddress"));
                        TextBox txtinstate = ((TextBox)dg.Cells[4].FindControl("txtInsState"));
                        dr["Insurance Address"] = txtinsadd.Text + ' ' + txtinstate.Text;
                    }

                    TextBox txtinsdesc = ((TextBox)dg.Cells[5].FindControl("txtInsDescritpion"));
                    dr["InsDescription"] = txtinsdesc.Text;
                    dr["CaseType"] = "XXX";
                    dt.Rows.Add(dr);

                }
            }
            if (dt.Rows.Count > 0)
            {
                //convert DataTable to DataView   
                DataView dv = dt.DefaultView;
                //apply the sort on CustomerSurname column   
                // dv.Sort = "Provider";
                //save our newly ordered results back into our datatable   
                dt = dv.ToTable();
            }



            ds.Tables.Add(dt);

            return ds;
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    private void creatPDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String szSourceFile1 = "";
        String szSourceFile1_FullPath = "";
        String szSourceFile2 = "";
        String szSourceFile2_FullPath = "";
        String szOpenFilePath = "";
        string pdffilename = "";
        string genhtmlNF = "";
        string genhtmlWC = "";
        string billIDWC = "";
        string billIDNF = "";
        string providerNameWC = "";
        string providerNameNF = "";
        int i = 0;
        int printi = 0;
        int printiWC = 0;
        int iPageCount = 1;
        string billidWC = "";
        string billidNF = "";
        int i_pom_id = 0;
        string NodeIdPath = "";
        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        StringBuilder sbFinalHTML = new StringBuilder();

        sbFinalHTML.AppendLine("");
        int iRecordOnpageNF = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordOnpageWC = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            string szDefaultPath = ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET");
            int iFlag = 0;
            DataSet ds = CreateGroupData();
            int iRecordsPerPageNF = 0;
            int iRecordsPerPageWC = 0;
            string strProvider = null;
            string strProviderNF = null;
            string strCasetype = null;
            bool iWCCount = false;

            genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
            genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";

            // create html and pdf for groupwise provider . print per page in pdf for provider 

            int _iCountWC = 0;
            int _iCountNF = 0;

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                strCasetype = ds.Tables[0].Rows[j]["CaseType"].ToString();

                if (strCasetype != "WC000000000000000001")
                {
                    // ONLY NF PROVIDER
                    iRecordsPerPageNF = _iCountNF;
                    sbFinalHTML.AppendLine("");
                    if (iFlag == 0)
                    {
                        strProvider = ds.Tables[0].Rows[j]["Provider"].ToString();
                        iFlag = 1;
                        billidNF = "";
                        iRecordsPerPageNF = 0;
                    }
                    if (strProvider.Equals(ds.Tables[0].Rows[j]["Provider"].ToString()))
                    {
                        ++printi;
                        //int i = 0;
                        if (iRecordsPerPageNF == iRecordOnpageNF)
                        {
                            sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, "", i_pom_id));
                            sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                            genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                            iRecordsPerPageNF = 0;
                            _iCountNF = 0;
                        }

                        if (ds.Tables[0].Rows[j]["InsDescription"] != "")
                        {
                            //genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px' colspan=6></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                            genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</b></td><td style='font-size:9px;' colspan=6> " + ds.Tables[0].Rows[j]["InsDescription"] + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Case #"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td><div style=top:100px;left:25px;> </div></tr>";
                            _iCountNF++;
                            iRecordsPerPageNF++;
                        }
                        else
                        {
                            genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Case #"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                            _iCountNF++;
                            iRecordsPerPageNF++;
                        }
                        //i = i + 1;
                        Session["VL_COUNT"] = ds.Tables[0].Rows.Count;
                    }
                }
            }

            string genHtml = "";
            if (iWCCount == false && _iCountWC > 0)
            {
                genhtmlWC += "</table>";
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtmlWC, billidWC, i_pom_id));
                sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
            }

            if (_iCountNF > 0)
            {
                genhtmlNF += "</table>";
                genHtml = genhtmlNF;
                sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genHtml, billidNF, i_pom_id));

            }
            if (_iCountWC > 0)
            {

                // sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");                     
                //genHtml = genhtmlWC;
                //sbFinalHTML.Append(ReplaceHeaderAndFooter(genHtml, billidWC ));       
            }

            string strHtml = sbFinalHTML.ToString();
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("P") + ".htm";
            pdffilename = getFileName("P") + ".pdf";
            string file_Path = ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename;
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + htmfilename, ApplicationSettings.GetParameterValue("EXCEL_SHEET") + pdffilename);

            string szPDFName = "";
            szPDFName = szDefaultPath + pdffilename;

            if (szSourceFile1 == "")
            {
                szSourceFile1 = pdffilename;
                szSourceFile1_FullPath = szDefaultPath + szSourceFile1;
                szOpenFilePath = szDefaultPath + szSourceFile1;
            }
            else
            {
                szSourceFile2 = pdffilename;
                szSourceFile2_FullPath = szDefaultPath + szSourceFile2;


            }
            szOpenFilePath = szDefaultPath + szSourceFile1;
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);

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

}
