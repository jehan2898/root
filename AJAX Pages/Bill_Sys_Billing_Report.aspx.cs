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

public partial class AJAX_Pages_Bill_Sys_Billing_Report : PageBase
{
    string pdfPath = "";
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    CaseDetailsBO objCaseDetailsBO;
    Bill_Sys_NF3_Template objNF3Template;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    //Bill_Sys_DigosisCodeBO _digosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();

    string strLinkPath = null;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    private ArrayList _arrayList;

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            //BindGrid();


        }
    }
    public void Check()
    {

    }
    public void BindGrid()
    {
        DataSet DSReport = new DataSet();
        Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
        DSReport = objReport.Get_Billing_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Text, extddlCaseType.Text, extddlDoctor.Text);
        grdAllReports.DataSource = DSReport.Tables[0];
        grdAllReports.DataBind();
        grdForReport.DataSource = DSReport.Tables[0];
        grdForReport.DataBind();
        Session["Billing"] = DSReport;
        lblCount.Text = DSReport.Tables[0].Rows.Count.ToString();

        foreach (DataGridItem dataGridItem in grdAllReports.Items)
        {
            LinkButton lnkbtn = (LinkButton)dataGridItem.FindControl("lnkAddBill");
            lnkbtn.Attributes.Add("onclick", "return CheckDetails('" + dataGridItem.Cells[19].Text + "','" + dataGridItem.Cells[20].Text + "','" + dataGridItem.Cells[21].Text + "');");
          

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel();
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
            //string strError = ex.Message.ToString();
            //strError = strError.Replace("\n", " ");
            //Response.Redirect("Error/PFS_Sys_ErrorPage.aspx?ErrMsg=" + strError);
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
    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "AddBill")
        {

           


                txtCaseID.Text = e.Item.Cells[0].Text.ToString();
                string billno = "";
                txtCaseNo.Text = e.Item.Cells[10].Text.ToString();
                if (!e.Item.Cells[23].Text.ToString().Equals("") && !e.Item.Cells[23].Text.ToString().Equals("NA") && !e.Item.Cells[23].Text.ToString().Equals(""))
                {
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        txtRefCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }


                    // Bill_Sys_ReportBO objreport = new Bill_Sys_ReportBO();
                    ArrayList arrlst = new ArrayList();
                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    //objreport = new Bill_Sys_ReportBO();
                    arrlst.Add(e.Item.Cells[0].Text.ToString());//0 case id
                    arrlst.Add(txtBillDate.Text);//1biildate
                    arrlst.Add(txtCompanyID.Text);
                    arrlst.Add(e.Item.Cells[23].Text.ToString());
                    arrlst.Add("0");
                    arrlst.Add("");
                    arrlst.Add(txtRefCompanyID.Text);
                    arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                    //objreport.InsertBillTransactionData(arrlst);
                    Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
                    DataSet ReportDs = new DataSet();
                    ReportDs = objReport.GetProcdureCode(txtCompanyID.Text, e.Item.Cells[15].Text.ToString());
                    ArrayList _objALBillProcedureCodeEO = new ArrayList();
                    ArrayList objALEventEO = new ArrayList();
                    for (int i = 0; i < ReportDs.Tables[0].Rows.Count; i++)
                    {



                        ArrayList objArr = new ArrayList();
                        objArr.Add(ReportDs.Tables[0].Rows[i][0].ToString());
                        objArr.Add(ReportDs.Tables[0].Rows[i][1].ToString());
                        objArr.Add(txtCompanyID.Text);
                        Bill_Sys_ReportBO objreport = new Bill_Sys_ReportBO();
                        DataSet dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                        objBillProcedureCodeEO.SZ_PROCEDURE_ID = dset1.Tables[0].Rows[0]["PROC_ID"].ToString();
                        objBillProcedureCodeEO.FL_AMOUNT = dset1.Tables[0].Rows[0]["AMT"].ToString();
                        objBillProcedureCodeEO.SZ_BILL_NUMBER = "";
                        objBillProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(e.Item.Cells[16].Text.ToString());
                        objBillProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        objBillProcedureCodeEO.I_UNIT = "1";
                        objBillProcedureCodeEO.FLT_PRICE = dset1.Tables[0].Rows[0]["AMT"].ToString();
                        objBillProcedureCodeEO.FLT_FACTOR = "1";
                        objBillProcedureCodeEO.DOCT_AMOUNT = dset1.Tables[0].Rows[0]["AMT"].ToString();
                        objBillProcedureCodeEO.PROC_AMOUNT = dset1.Tables[0].Rows[0]["AMT"].ToString();
                        objBillProcedureCodeEO.SZ_TYPE_CODE_ID = objreport.GetTypeCode(txtCompanyID.Text, dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                        objBillProcedureCodeEO.SZ_DOCTOR_ID = e.Item.Cells[23].Text.ToString();
                        objBillProcedureCodeEO.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                        objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        _objALBillProcedureCodeEO.Add(objBillProcedureCodeEO);



                    }
                    Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                    DataSet dsDiaadnosisCode = new DataSet();
                    dsDiaadnosisCode = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagnosisCode(e.Item.Cells[22].Text.ToString(), txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    ArrayList objALBillDiagnosisCodeEO = new ArrayList();
                    for (int i = 0; i < dsDiaadnosisCode.Tables[0].Rows.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = dsDiaadnosisCode.Tables[0].Rows[i]["CODE"].ToString();
                        objALBillDiagnosisCodeEO.Add(objBillDiagnosisCodeEO);
                    }

                    //e.Item.Cells[15].ToString();
                    Bill_Sys_ReportBO objRBO = new Bill_Sys_ReportBO();
                    BillTransactionDAO objBT_DAO = new BillTransactionDAO();
                    Result objResult = new Result();
                    objResult = objRBO.SaveBillTransaction(arrlst, e.Item.Cells[15].Text.ToString(), _objALBillProcedureCodeEO, objALBillDiagnosisCodeEO);
                    if (objResult.msg_code == "ERR")
                    {
                        //lblMsg.Text = objResult.msg;
                        //lblMsg.Visible = true;
                    }
                    else
                    {
                        billno = objResult.bill_no.ToString();

                        _DAO_NOTES_EO = new DAO_NOTES_EO();
                        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = billno;

                        _DAO_NOTES_BO = new DAO_NOTES_BO();
                        _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        _DAO_NOTES_EO.SZ_CASE_ID = e.Item.Cells[0].Text.ToString();
                        _DAO_NOTES_EO.SZ_COMPANY_ID = txtCompanyID.Text;
                        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                         objCaseDetailsBO = new CaseDetailsBO();
                      

                    }
                    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                    Session["WC_Speciality"] = sz_speciality;
                   
                    if (objCaseDetailsBO.GetCaseType(billno) == "WC000000000000000001")
                    {
                        string caseType = objCaseDetailsBO.GetCaseType(billno);

                        //'" + caseType + "','" + billno + "','" + sz_speciality + "'
                        // Page.ClientScript.RegisterClientScriptBlock(typeof(Button), "Msg", "alert('hi');document.getElementById('_ctl0_ContentPlaceHolder1_pnlPDFWorkerComp').style.height='180px'; alert('hi');document.getElementById('_ctl0_ContentPlaceHolder1_pnlPDFWorkerComp').style.visibility = 'visible'; document.getElementById('_ctl0_ContentPlaceHolder1_pnlPDFWorkerComp').style.position = 'absolute';  document.getElementById('_ctl0_ContentPlaceHolder1_pnlPDFWorkerComp').style.top = '100px'; document.getElementById('_ctl0_ContentPlaceHolder1_pnlPDFWorkerComp').style.left ='450px';", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "checkExistingBills('" + caseType + "','" + billno + "','" + sz_speciality + "')", true);

                    }
                    else
                    {

                        string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                        string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                        string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        string CaseId = e.Item.Cells[0].Text.ToString();
                        string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string CaseNO = txtCaseNo.Text;
                        GenerateAddedBillPDF(billno, sz_speciality, CaseId);
                        //WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
                        //string PathToOpen = _objNFBill.GenerateAddedBillPDF(billno, sz_speciality, CaseId, szCompanyID, CmpName, UserId, UserName, CaseNO);
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + PathToOpen + "');", true);
                        ////GenerateAddedBillPDF(billno, sz_speciality);
                        // GenerateAddedBillPDF(billno, sz_speciality);
                    }
                    BindGrid();
                }
            
        }


        int iFlagCheck = 0;

        if (e.CommandName.ToString() == "ShowPop")
        {
            txtCalimNumber.Text = "";
            lblMsg.Text = "";
            lblMsg.Visible = false;
            txtEvenId.Text = e.Item.Cells[15].Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text;
            if (!e.Item.Cells[20].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[20].Text.ToString().Equals("") && !e.Item.Cells[20].Text.ToString().Equals("NA"))
            {
                extddlInsuranceCompany.Text = e.Item.Cells[20].Text.ToString();
                Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                lstInsuranceCompanyAddress.DataSource = _bill_Sys_PatientBO.GetInsuranceCompanyAddress(e.Item.Cells[20].Text.ToString());
                lstInsuranceCompanyAddress.DataTextField = "DESCRIPTION";
                lstInsuranceCompanyAddress.DataValueField = "CODE";
                lstInsuranceCompanyAddress.DataBind();
                Page.MaintainScrollPositionOnPostBack = true;
                if (!e.Item.Cells[21].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[21].Text.ToString().Equals(""))
                {
                    lstInsuranceCompanyAddress.SelectedValue = e.Item.Cells[21].Text.ToString();
                }

            }
            if (!e.Item.Cells[19].Text.ToString().Equals("&nbsp;") && !e.Item.Cells[19].Text.ToString().Equals(""))
            {
                txtCalimNumber.Text = e.Item.Cells[19].Text.ToString();
            }
            txtSpeciality.Text = e.Item.Cells[18].Text.ToString();
            txtPGID.Text = e.Item.Cells[22].Text.ToString();

            txtCaseID.Text = e.Item.Cells[0].Text.ToString();
            txtDiagonosisCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerDiagnosisCode_tabpnlAssociate_btnSeacrh').click(); return false;}} else {alert('Other');return true;}; ");
            extddlDiagnosisType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
            _dianosis_Association = (Bil_Sys_Associate_Diagnosis)Session["DIAGNOS_ASSOCIATION"];

            //_dianosis_Association.EventProcID
            txtCaseID1.Text = txtCaseID.Text;
            GetAssignedDiagnosisCode(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE");
            GetDiagnosisCode(txtCaseID1.Text, txtCompanyID.Text, "", "GET_DIAGNOSIS_CODE");
            grdNormalDgCode.Visible = false;
            tabcontainerDiagnosisCode.ActiveTabIndex = 1;
            ModalPopupExtender1.Show();
        }

        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["Billing"];
        dv = ds.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "CASENO")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "PatientNameSearch")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }



        }

        else if (e.CommandName.ToString() == "EventDateSearch")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "DoctorNameSearch")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "CasetypeSearch")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "SpecialitySearch")
        {
            iFlagCheck = 1;
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        if (iFlagCheck == 1)
        {
            dv.Sort = txtSort.Text;
            grdAllReports.DataSource = dv;
            grdAllReports.DataBind();
            grdForReport.DataSource = dv;
            grdForReport.DataBind();
        }

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        SaveDiagnosisCode();
        BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
        lblMsg.Visible = true;
        lblMsg.Text = "Case Associate Diagnosis  Saved Successfully ...!";
        BindGrid();
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
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }

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

    protected void btnClose_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Hide();

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
                usrMessage1.PutMessage(ex.ToString());
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage1.Show();
            }
        }
    }
    #endregion
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
    protected void btnSeacrh1_Click(object sender, EventArgs e)
    {
        grdNormalDgCode.Visible = true;
        BindGrid(extddlDiagnosisType.Text, txtDiagonosisCode.Text, txtDescription.Text);
        ModalPopupExtender1.Show();
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
            int i = obj.Update_Insurace_Claim(txtCompanyID.Text, txtCaseID.Text, extddlInsuranceCompany.Text, lstInsuranceCompanyAddress.SelectedValue, txtCalimNumber.Text);
            if (i == 0)
            {
                lblMsg.Text = "The selected patient scheduled from Billing company. So you can not modify selected patient. ...!";
            }
            else
            {
                lblMsg.Text = "Insurace Information Updated Successfully ...!";
            }
            lblMsg.Visible = true;

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

                //Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);

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
    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        string CaseId = txtCaseID.Text;
        string szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string CaseNO = txtCaseNo.Text;
        string speciality = Session["WC_Speciality"].ToString();
        WC_Bill_Generation _objNFBill = new WC_Bill_Generation();

        string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), txtCaseID.Text, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, speciality, 1);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);
    }
    

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality , string szCaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String szSpecility = p_szSpeciality;


            Session["TM_SZ_CASE_ID"] = szCaseID;
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

                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;
               


                String szOpenFilePath = "";
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;


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
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenFilePath.ToString() + "');", true);

                    }
                }
                String[] szTemp;
                string szBillName = "";
                szTemp = szFileNameForSaving.Split('/');
                ArrayList objAL = new ArrayList();
                szFileNameForSaving = szFileNameForSaving.Remove(0, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString().Length);
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
                objAL.Add(txtCaseNo.Text);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = Session["TM_SZ_CASE_ID"].ToString(); ;
                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

               // BindLatestTransaction();


                // End 


            }
            else
            {
               // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
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
        
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


}
