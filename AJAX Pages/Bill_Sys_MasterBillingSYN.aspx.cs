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
using System.Data.SqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using mbs.LienBills;

public partial class AJAX_Pages_Bill_Sys_MasterBillingSYN : PageBase
{
    string sz_UserID = "";
    string sz_CompanyID = "";
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    CaseDetailsBO objCaseDetailsBO;
    PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    Bill_Sys_NF3_Template objNF3Template;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private ArrayList pdfbills;
    private ArrayList EventId;
    SqlConnection Sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
            txtCompanyID.Text = sz_CompanyID;
            txtFromDate.Text = DateTime.Parse("Jan 1, 2000").ToString();
            txtToDate.Text = DateTime.Parse("Jan 1, 2010").ToString();
            this.con.SourceGrid = grdMasterBilling;
            this.txtSearchBox.SourceGrid = grdMasterBilling;
            this.grdMasterBilling.Page = this.Page;
            this.grdMasterBilling.PageNumberList = this.con;
            if(!Page.IsPostBack)
            {
                txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                grdMasterBilling.XGridBind();
                //if (grdMasterBilling.RecordCount > 0)
                //{
                //    Btn_Patient.Visible = true;
                //    Btn_Selected.Visible = true;
                //}
                //else
                //{
                //    Btn_Selected.Visible = false;
                //    Btn_Patient.Visible = false;
                //}
            }
            //for (int f = 11; f <= 22; f++)
            //{
            //    grdMasterBilling.Columns[f].Visible = false;
            //}
            
            
        }
        catch(Exception ex)
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdMasterBilling.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
    protected void Btn_Patient_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            pdfbills = new ArrayList();
            ArrayList arrlst = new ArrayList();
            //for (int f = 11; f <= 22; f++)
            //{
            //    grdMasterBilling.Columns[f].Visible = true;
            //}

            foreach (GridViewRow item in grdMasterBilling.Rows)
            {
                CheckBox chk = (CheckBox)(item.Cells[10].FindControl("chkSelect"));
                if (chk.Checked == true)
                {
                    if (item.Cells[4].Text == "&nbsp;" || item.Cells[5].Text == "&nbsp;" || item.Cells[22].Text=="&nbsp;")
                    {
                        arrlst.Add(item.Cells[0].Text);
                    }
                }
            }

            string patientcnt = "";
            string repeat = "";
            if (arrlst.Count > 0)
            {
                for (int i = 0; i < arrlst.Count; i++)
                {
                    int cnt = 0;
                    repeat = arrlst[i].ToString();
                    patientcnt += arrlst[i].ToString() + ",";
                    for (int j = 0; j < arrlst.Count; j++)
                    {
                        if (repeat == arrlst[j].ToString())
                        {
                            cnt++;
                        }
                    }
                    if (cnt > 1)
                    {
                        i += cnt - 1;
                    }
                }
                popupmsg.InnerText = "You do not have a claim number or an insurance company or an insurance company address added to these Case NO: '" + patientcnt + "'.You cannot proceed furher";
               // Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage1();</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "MM1234", "<script type='text/javascript'>openExistsPage1();</script>", false);
                //for (int f = 11; f <= 22; f++)
                //{
                //    grdMasterBilling.Columns[f].Visible = false;
                //}
            }
            else
            {
              AddBillDiffCase();
              //for (int f = 11; f <= 22; f++)
              //{
              //    grdMasterBilling.Columns[f].Visible = false;
              //}
            }
            //  BindReffGrid();

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
    protected void AddBillDiffCase()
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
            ArrayList arrbill = new ArrayList();
            ArrayList arrlst;
            ArrayList objArr;
            ArrayList _arraylist;
            pdfbills = new ArrayList();
            string sz_compID = "";
            Bill_Sys_ReportBO objreport;
            EventId = new ArrayList();
            string patientID = "";
            int flag = 0;
            int _insertFlag = 0;
            string billno = "";
            string PatientTreatmentID = "";
            int cnt = 0; // 
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
            //

            DataRow dr;

            objreport = new Bill_Sys_ReportBO();
            // 
            foreach (GridViewRow ditem in grdMasterBilling.Rows)
            {

                CheckBox chkSelect = (CheckBox)(ditem.FindControl("chkSelect"));
                if (chkSelect.Checked == true)
                {
                    dr = dt.NewRow();
                    dr["SZ_CASE_ID"] = ditem.Cells[11].Text;
                    dr["SZ_PATIENT_ID"] = ditem.Cells[12].Text;
                    dr["CHART NO"] = ditem.Cells[13].Text;
                    dr["PATIENT NAME"] = ditem.Cells[1].Text;
                    dr["DATE OF SERVICE"] = ditem.Cells[7].Text;
                    dr["Patient name"] = ditem.Cells[1].Text;
                    dr["Date Of Service"] = ditem.Cells[7].Text;
                    dr["Procedure code"] = ditem.Cells[9].Text;
                    dr["Description"] = ditem.Cells[15].Text;
                    dr["Status"] = ditem.Cells[16].Text;
                    dr["Code ID"] = ditem.Cells[17].Text;
                    dr["EVENT ID"] = ditem.Cells[18].Text;
                    dr["Doctor ID"] = ditem.Cells[14].Text;
                    dr["CASE NO"] = ditem.Cells[0].Text;
                    dr["Company ID"] = ditem.Cells[19].Text;
                    dr["SZ_PATIENT_TREATMENT_ID"] = ditem.Cells[20].Text;
                    //
                    dr["SZ_PROCEDURE_GROUP_ID"] = ditem.Cells[21].Text;
                    //
                    dt.Rows.Add(dr);
                }
            }
            dt.DefaultView.Sort = "SZ_CASE_ID ASC";
            //Session["test"] = dt;
            objreport = new Bill_Sys_ReportBO();
            int c = 0;
            string invalidcodes="";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cnt = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    if (dt.Rows[i]["SZ_PATIENT_ID"].ToString() == drow["SZ_PATIENT_ID"].ToString() && dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString() == drow["SZ_PROCEDURE_GROUP_ID"].ToString())
                    {
                        cnt++;
                    }
                }
                dset0 = new DataSet();
                dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), dt.Rows[i]["SZ_CASE_ID"].ToString(), "GET_DOCTOR_DIAGNOSIS_CODE");
                if (dset0.Tables[0].Rows.Count == 0)
                {
                    if(invalidcodes=="")
                        invalidcodes = dt.Rows[i]["CASE NO"].ToString();
                    else
                        invalidcodes = invalidcodes + "," + dt.Rows[i]["CASE NO"].ToString();
                }
                i += cnt-1;
            }
            if (invalidcodes != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MM12323", "<script type='text/javascript'>alert('Cannot create bill for " +invalidcodes + " as no diagnosis code is assigned for the patient')</script>", false);
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //
                Session["Procedure_Code"] = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(); ;
                //
                cnt = 0;
                string Pid = dt.Rows[i]["SZ_PATIENT_ID"].ToString();
                String szSpecialityID = dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString();
                foreach (DataRow drow in dt.Rows)
                {
                    if (Pid == drow["SZ_PATIENT_ID"].ToString() && szSpecialityID == drow["SZ_PROCEDURE_GROUP_ID"].ToString())
                    {
                        cnt++;
                        EventId.Add(drow["Event ID"].ToString());
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
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(szSpecialityID, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dt.Rows[i]["Doctor ID"].ToString());
                        arrlst.Add("0");
                        arrlst.Add("");
                        arrlst.Add("");
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_trancation
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
                        arrlst.Add(dt.Rows[i]["Doctor ID"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);
                        if (dset1.Tables[0].Rows.Count == 0)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Procedure Code/Description for Code " + objArr[0].ToString() + " not found');</script>");
                            return;
                        }
                        _arraylist = new ArrayList();
                        _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(billno);
                        _arraylist.Add(dt.Rows[i]["DATE OF SERVICE"].ToString());
                        _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        _arraylist.Add("1");
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add("1");
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                        _arraylist.Add(dt.Rows[i]["Doctor ID"].ToString());
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

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                            pdfbills.Add(billno);
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                        else
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                            pdfbills.Add(billno);
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                    //To Update Bill Number in Calender Event Table
                    for (int a = 0; a < EventId.Count; a++)
                    {
                        string query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_BILL_NUMBER='" + billno + "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='" + EventId[a].ToString() + "', @SZ_CASE_ID='" + txtCaseID.Text + "',@SZ_COMPANY_ID='" + txtCompanyID.Text + "'";
                        SqlDataAdapter da = new SqlDataAdapter(query, Sqlcon);
                        DataSet dsEventId = new DataSet();
                        da.Fill(dsEventId);
                    }
                    EventId.Clear();
                    //End
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
                    dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(dt.Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString(), txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                    
                    if (dset0.Tables[0].Rows.Count > 0)
                    {
                        arrlst = new ArrayList();
                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                        objreport = new Bill_Sys_ReportBO();
                        arrlst.Add(txtCaseID.Text);
                        arrlst.Add(txtBillDate.Text);
                        arrlst.Add(txtCompanyID.Text);
                        arrlst.Add(dt.Rows[i]["Doctor ID"].ToString());
                        arrlst.Add("0");
                        arrlst.Add("");
                        arrlst.Add("");
                        arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        //TUSHAR:- To Save Procedure Group Id In Txn_Bill_trancation
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
                        arrlst.Add(dt.Rows[i]["Doctor ID"].ToString());
                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                        objArr = new ArrayList();
                        objArr.Add(dt.Rows[i]["Procedure code"].ToString());
                        objArr.Add(dt.Rows[i]["Description"].ToString());
                        objArr.Add(txtCompanyID.Text);
                        objreport = new Bill_Sys_ReportBO();
                        dset1 = new DataSet();
                        dset1 = objreport.GetProcCodeDetails(objArr);
                        if (dset1.Tables[0].Rows.Count == 0)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Procedure Code/Description for Code " + objArr[0].ToString() + " not found');</script>");
                            return;
                        }

                        for (int k = 0; k < cnt; k++)
                        {
                            //
                            //if ((k + i) < cnt)
                            //{
                            //    if (dt.Rows[k + i]["SZ_PROCEDURE_GROUP_ID"].ToString() == Session["Procedure_Code"].ToString())
                            //    {
                            //
                            objArr = new ArrayList();
                            objArr.Add(dt.Rows[k + i]["Procedure code"].ToString());
                            objArr.Add(dt.Rows[k + i]["Description"].ToString());
                            objArr.Add(txtCompanyID.Text);
                            objreport = new Bill_Sys_ReportBO();
                            dset1 = new DataSet();
                            dset1 = objreport.GetProcCodeDetails(objArr);
                            if (dset1.Tables[0].Rows.Count == 0)
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Procedure Code/Description for Code " + objArr[0].ToString() + " not found');</script>");
                                return;
                            }
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
                            _arraylist.Add(dt.Rows[k+i]["Doctor ID"].ToString());
                            _arraylist.Add(dt.Rows[k + i]["SZ_CASE_ID"].ToString());
                            _arraylist.Add(dt.Rows[k + i]["Code ID"].ToString());
                            _arraylist.Add("");
                            _arraylist.Add("");
                            //_arraylist.Add(PatientTreatmentID);
                            _arraylist.Add(dt.Rows[k + i]["SZ_PATIENT_TREATMENT_ID"].ToString());
                            _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
                            // c = c + 1;
                            //    }
                            //}
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

                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                            pdfbills.Add(billno);
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }
                        else
                        {
                            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                            //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                            pdfbills.Add(billno);
                            GenerateAddedBillPDF(billno, sz_speciality);
                            arrbill.Add(dt.Rows[i]["CASE NO"].ToString());
                        }

                    }
                    else
                    {

                        //objArr = new ArrayList();
                        arrdiag.Add(dt.Rows[i]["CASE NO"].ToString());
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('No diagnosis code assign for the patient');</script>");
                    }
                    // i = cnt - 1;
                    i += cnt - 1;


                    //To Update Bill Number in Calender Event Table
                    for (int a = 0; a < EventId.Count; a++)
                    {
                        string query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_BILL_NUMBER='" + billno + "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='" + EventId[a].ToString() + "',@SZ_CASE_ID='" + txtCaseID.Text + "',@SZ_COMPANY_ID='" + txtCompanyID.Text + "'";
                        SqlDataAdapter da = new SqlDataAdapter(query, Sqlcon);
                        DataSet dsEventId = new DataSet();
                        da.Fill(dsEventId);
                    }
                    EventId.Clear();
                    //End

                }

            }
            string Billno = "";
            if (arrbill.Count > 0)
            {

                for (int l = 0; l < arrbill.Count; l++)
                {
                    if (l == arrbill.Count - 1)
                    {
                        Billno += arrbill[l].ToString();
                    }
                    else
                    {
                        Billno += arrbill[l].ToString() + ", ";
                    }

                }
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert(' create bill for  case id');</script>"); 
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Bill Created Successfully For CaseNo " + Billno + " ');</script>");

            }
            string patientcnt = "";
            if (arrdiag.Count > 0)
            {
                for (int l = 0; l < arrdiag.Count; l++)
                {
                    patientcnt += arrdiag[l].ToString() + ", ";
                }
               // this.Page.RegisterStartupScript("mm", "<script type='text/javascript'>alert('Cannot create bill for " + patientcnt + " as no diagnosis code assign for the patient');</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "MM1231", "<script type='text/javascript'>alert('Cannot create bill for " + patientcnt + " as no diagnosis code is not assigned for the patient')</script>", false);
              //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Cannot create bill for '" + patientcnt + "' as no diagnosis code assign for the patient');</script>");
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Cannot create bill for '" + patientcnt + "' as no diagnosis code assign for the patient');</script>");
            }
            if (pdfbills.Count > 0)
            {
                billsperpatient(pdfbills);
                grdMasterBilling.XGridBindSearch();               
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
    protected void AddBillSameCase()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            pdfbills = new ArrayList();
            EventId = new ArrayList();
            string sz_compID = "";
            DataSet dset0;
            DataSet dset1;
            ArrayList arrbill = new ArrayList();
            ArrayList arrlst;
            ArrayList objArr;
            ArrayList _arraylist;
            Bill_Sys_ReportBO objreport;
            string patientID = "";
            int flag = 0;
            int _insertFlag = 0;
            string billno = "";
            string PatientTreatmentID = "";

            //Added Flag And Procedure Group :-Tushar
            int flag1 = 0;
            string Proc_Group_Id = "";
            //end
            foreach (GridViewRow item in grdMasterBilling.Rows)
            {
                CheckBox chk = (CheckBox)(item.Cells[10].FindControl("chkSelect"));
                if (chk.Checked == true)
                {
                    patientID = item.Cells[12].Text;
                }
            }

            foreach(GridViewRow j in grdMasterBilling.Rows)
            {
                CheckBox chkSelect = (CheckBox)(j.Cells[10].FindControl("chkSelect"));
                if (chkSelect.Checked == true)
                {
                    if (patientID == j.Cells[12].Text)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 2;
                        break;
                    }
                }
            }

            if (flag == 2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "yu12323", "<script type='text/javascript'>alert('Select visits for same patient')</script>", false);
            }
            else if (flag == 1)
            {
                ArrayList arrlst1 = new ArrayList();
                foreach (GridViewRow item in grdMasterBilling.Rows)
                {
                    CheckBox chk = (CheckBox)(item.Cells[10].FindControl("chkSelect"));
                    if (chk.Checked == true)
                    {
                        if (item.Cells[4].Text == "&nbsp;" || item.Cells[5].Text == "&nbsp;" ||item.Cells[22].Text=="&nbsp;")
                        {
                            arrlst1.Add(item.Cells[0].Text);
                        }
                    }
                }
                string patientcnt = "";
                string repeat = "";
                if (arrlst1.Count > 0)
                {
                    for (int i = 0; i < arrlst1.Count; i++)
                    {
                        int cnt = 0;
                        repeat = arrlst1[i].ToString();
                        patientcnt += arrlst1[i].ToString() + ",";
                        for (int j = 0; j < arrlst1.Count; j++)
                        {
                            if (repeat == arrlst1[j].ToString())
                            {
                                cnt++;
                            }
                        }
                        if (cnt > 1)
                        {
                            i += cnt - 1;
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "zu12323", "<script type='text/javascript'>alert('You do not have a claim number or an insurance company or an insurance company address added to these Case NO "+patientcnt+". You cannot proceed furher.')</script>", false);
                }
                else
                {
                    //to check weather procedure group is same or not:-Tushar
                    foreach (GridViewRow item in grdMasterBilling.Rows)
                    {

                        CheckBox chk = (CheckBox)(item.Cells[10].FindControl("chkSelect"));
                        if (chk.Checked == true)
                        {
                            Proc_Group_Id = item.Cells[21].Text;
                            Session["Procedure_Code"] = Proc_Group_Id;
                        }
                    }



                    foreach (GridViewRow j in grdMasterBilling.Rows)
                    {
                        CheckBox chkSelect = (CheckBox)(j.Cells[10].FindControl("chkSelect"));
                        if (chkSelect.Checked == true)
                        {
                            if (Proc_Group_Id == j.Cells[21].Text)
                            {
                                flag1 = 1;
                            }
                            else
                            {
                                flag1 = 2;
                                break;
                            }
                        }
                    }

                    if (flag1 == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "zu1323", "<script type='text/javascript'>alert('Select visits for same Speciality');</script>", false);
                    }
                    else
                    {
                        objreport = new Bill_Sys_ReportBO();
                        Bill_Sys_AssociateDiagnosisCodeBO _bill_Sys_AssociateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                        foreach (GridViewRow ditem in grdMasterBilling.Rows)
                        {

                            CheckBox chkSelect = (CheckBox)(ditem.Cells[10].FindControl("chkSelect"));
                            if (chkSelect.Checked == true)
                            {

                                _insertFlag = 1;
                                txtCaseID.Text = ditem.Cells[11].Text;
                                txtReadingDocID.Text = ditem.Cells[14].Text;
                                txtPatientID.Text = ditem.Cells[12].Text;
                                txtCaseNo.Text = (ditem.Cells[0].Text);
                                sz_compID = ditem.Cells[19].Text;
                                EventId.Add(ditem.Cells[18].Text);
                            }
                        }

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

                        if (_insertFlag == 1)
                        {
                            dset0 = new DataSet();
                            dset0 = _bill_Sys_AssociateDiagnosisCodeBO.GetDoctorDiagCodeforMasterBilling(Proc_Group_Id, txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE");
                            if (dset0.Tables[0].Rows.Count > 0)
                            {
                                arrlst = new ArrayList();
                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                objreport = new Bill_Sys_ReportBO();
                                arrlst.Add(txtCaseID.Text);
                                arrlst.Add(txtBillDate.Text);
                                arrlst.Add(txtCompanyID.Text);
                                arrlst.Add(txtReadingDocID.Text);
                                arrlst.Add("0");
                                arrlst.Add("");
                                arrlst.Add("");
                                arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                                //TUSHAR:- To Save Procedure Group Id In Txn_Bill_trancation
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
                                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                                _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                                foreach (GridViewRow ditem1 in grdMasterBilling.Rows)
                                {
                                    PatientTreatmentID = "";
                                    arrlst = new ArrayList();
                                    CheckBox chkSelect = (CheckBox)(ditem1.Cells[10].FindControl("chkSelect"));
                                    if (chkSelect.Checked == true)
                                    {

                                        objreport = new Bill_Sys_ReportBO();
                                        arrlst = new ArrayList();
                                        arrlst.Add(txtPatientID.Text);
                                        arrlst.Add(ditem1.Cells[17].Text);
                                        arrlst.Add(txtCompanyID.Text);
                                        arrlst.Add(ditem1.Cells[14].Text);
                                        PatientTreatmentID = objreport.GetTreatmentID(arrlst);

                                        objArr = new ArrayList();
                                        objArr.Add(ditem1.Cells[9].Text);
                                        objArr.Add(ditem1.Cells[15].Text);
                                        objArr.Add(txtCompanyID.Text);
                                        objreport = new Bill_Sys_ReportBO();
                                        dset1 = new DataSet();
                                        dset1 = objreport.GetProcCodeDetails(objArr);
                                        if (dset1.Tables[0].Rows.Count == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "zu1323", "<script type='text/javascript'>alert('Procedure Code/Description do not match');</script>", false);
                                            return;
                                        }
                                        _arraylist = new ArrayList();
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["PROC_ID"].ToString());
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(billno);
                                        _arraylist.Add(ditem1.Cells[7].Text);
                                        _arraylist.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                        _arraylist.Add("1");
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add("1");
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(dset1.Tables[0].Rows[0]["AMT"].ToString());
                                        _arraylist.Add(ditem1.Cells[14].Text);
                                        _arraylist.Add(ditem1.Cells[11].Text);
                                        _arraylist.Add(ditem1.Cells[17].Text);
                                        _arraylist.Add("");
                                        _arraylist.Add("");
                                        //_arraylist.Add(PatientTreatmentID);
                                        _arraylist.Add(ditem1.Cells[20].Text);
                                        _bill_Sys_BillTransaction.SaveTransactionData(_arraylist);
                                    }
                                }

                                _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                _bill_Sys_BillTransaction.DeleteTransactionDiagnosis(billno);
                                if (dset0.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dset0.Tables[0].Rows.Count; i++)
                                    {
                                        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                        _arraylist = new ArrayList();
                                        _arraylist.Add(dset0.Tables[0].Rows[i]["CODE"].ToString());
                                        _arraylist.Add(billno);
                                        _bill_Sys_BillTransaction.SaveTransactionDiagnosis(_arraylist);
                                    }
                                }

                                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                                {
                                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                                    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID));
                                    pdfbills.Add(billno);
                                    GenerateAddedBillPDF(billno, sz_speciality);
                                    arrbill.Add(txtCaseNo.Text);
                                }
                                else
                                {
                                    _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                                    string sz_speciality = _bill_Sys_BillTransaction.GetDocSpeciality(billno);
                                    //GenerateAddedBillPDF(billno, _bill_Sys_BillTransaction.GetDoctorSpeciality(billno, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                                    pdfbills.Add(billno);
                                    GenerateAddedBillPDF(billno, sz_speciality);
                                    arrbill.Add(txtCaseNo.Text);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "z23", "<script type='text/javascript'>alert('No diagnosis code assign for the patient');</script>", false);
                            }
                        }

                        //To Update Bill Number in Calender Event Table
                        for (int a = 0; a < EventId.Count; a++)
                        {
                            string query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_BILL_NUMBER='" + billno + "', @FLAG='UPDATE_BILL_NUMBER',@SZ_EVENT_ID ='" + EventId[a].ToString() + "',@SZ_CASE_ID='" + txtCaseID.Text + "',@SZ_COMPANY_ID='" + txtCompanyID.Text + "'";
                            SqlDataAdapter da = new SqlDataAdapter(query, Sqlcon);
                            DataSet dsEventId = new DataSet();
                            da.Fill(dsEventId);
                        }
                        EventId.Clear();
                        //End
                    }
                 
                    if (arrbill.Count > 0)
                    {
                        string caseno = arrbill[0].ToString();
                        ScriptManager.RegisterStartupScript(this,GetType(), "mmasd", "<script language='javascript'>alert('Bill Created Successfully For CaseNo " + caseno + " ');</script>",false);
                    }
                    if (pdfbills.Count > 0)
                    {
                        billsperpatient(pdfbills);
                        grdMasterBilling.XGridBindSearch();
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
   
    protected void billsperpatient(ArrayList pdfbills)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlDataAdapter da ;
        string pdfpath = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        objNF3Template = new Bill_Sys_NF3_Template();
        string query;
        try
        {
            query = "exec [SP_PDFBILLS_MASTERBILLING_SYN]  @SZ_BILL_NUMBER='" + pdfbills[0].ToString() + "', @FLAG='PDF'";
            da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string sz_case_id = ds.Tables[0].Rows[0]["CASEID"].ToString();
            Document doc = new Document(iTextSharp.text.PageSize.A4);

            string filename = "FUReport_" + pdfbills[0] + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
            string filepath = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "/" +
                              ds.Tables[0].Rows[0]["CASEID"].ToString() + "/No Fault File/Medicals/SYN/FUReport/";
            string destinationdir = ds.Tables[0].Rows[0]["Company_Address"].ToString().Substring(0, ds.Tables[0].Rows[0]["Company_Address"].ToString().IndexOf(';')) + "\\" +
                                     ds.Tables[0].Rows[0]["CASEID"].ToString() + "\\No Fault File\\Medicals\\SYN\\FUReport\\";
            pdfpath = ConfigurationManager.AppSettings["DocumentManagerURL"] + filepath + filename;
            MemoryStream m = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, m);
            doc.Open();

            PdfPTable HeadingTable = new PdfPTable(1);

            HeadingTable.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            HeadingTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HeadingTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            HeadingTable.DefaultCell.Border = Rectangle.NO_BORDER;
            HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_Name"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_Address"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            //HeadingTable.AddCell(new Phrase(ds.Tables[0].Rows[0]["Office_State"].ToString() + "," + ds.Tables[0].Rows[0]["Office_City"].ToString() + " " + ds.Tables[0].Rows[0]["Office_Zip"].ToString(), FontFactory.GetFont("Arial", 9, Font.BOLD)));
            HeadingTable.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin, writer.DirectContent);
            float Previous_Height = HeadingTable.TotalHeight;
            PdfPTable HeadingTable1 = new PdfPTable(1);
            HeadingTable1.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            HeadingTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            HeadingTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            HeadingTable1.DefaultCell.Border = Rectangle.NO_BORDER;
            HeadingTable1.AddCell(new Phrase("CLINICAL BIOELECTRONIC MEDICINE USING SYNAPTIC - 3400", FontFactory.GetFont("Arial", 12)));
            HeadingTable1.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + HeadingTable1.TotalHeight + 30;
            float[] width = new float[] { 1f, 3f, 1f, 3f };
            PdfPTable PatientDetails = new PdfPTable(width);
            PatientDetails.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            PatientDetails.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //1st row
            PatientDetails.AddCell(new Phrase("Patient :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["PATIENT_FIRST_NAME"].ToString() + " " + ds.Tables[0].Rows[0]["PATIENT_LAST_NAME"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("D.O.B :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["DATE OF BIRTH"].ToString(), FontFactory.GetFont("Arial", 9)));
            //2nd row
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("D.O.A :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["ACCIDENT_DATE"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("Insurance :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["INSURANCE NAME"].ToString(), FontFactory.GetFont("Arial", 9)));
            //3rd row
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("Claim# :", FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            PatientDetails.AddCell(new Phrase(ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString(), FontFactory.GetFont("Arial", 9)));
            PatientDetails.DefaultCell.Border = Rectangle.NO_BORDER;
            PatientDetails.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            PatientDetails.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            PatientDetails.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + PatientDetails.TotalHeight + 30;            
            width = new float[] { 1f, 2f, 2f, 2f, 2f, 2f, 2f,2f,2f };
            PdfPTable Event_Details = new PdfPTable(width);
            
            Event_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Event_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //add heading
            Event_Details.AddCell(new Phrase("S.No.", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Treatment Day", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Treatment Time", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Intensity", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Bias", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Area", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("L.O.P Before", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("L.O.P After", FontFactory.GetFont("Arial", 8, Font.BOLD)));
            Event_Details.AddCell(new Phrase("Sign", FontFactory.GetFont("Arial", 8, Font.BOLD)));

            query = "exec [SP_PDFBILLS_MASTERBILLING_SYN]  @SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_TREATMENT_INFORMATION'";
            da = new SqlDataAdapter(query, con);
            DataSet dsTreatment=new DataSet();
            da.Fill(dsTreatment);
            
            int i = 1;
            foreach (DataRow dr in dsTreatment.Tables[0].Rows)
            {
                Event_Details.AddCell(new Phrase((i++).ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[0].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[1].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[2].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[3].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[4].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[5].ToString(), FontFactory.GetFont("Arial", 8)));
                Event_Details.AddCell(new Phrase(dr[6].ToString(), FontFactory.GetFont("Arial", 8)));
                if (dr[7].ToString() != "" && dr[7].ToString() != "null")
                {
                    Event_Details.AddCell(iTextSharp.text.Image.GetInstance(dr[7].ToString()));
                }
                else
                {
                    Event_Details.AddCell(new Phrase("", FontFactory.GetFont("Arial", 8)));
                }
                if ((Event_Details.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50) && (ds.Tables[0].Rows.Count > i))
                {
                    Event_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
                    doc.NewPage();
                    Previous_Height = 0;
                    Event_Details = new PdfPTable(width);
                    Event_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
                    Event_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //add heading
                    Event_Details.AddCell(new Phrase("S.No.", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Treatment Day", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Treatment Time", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Intensity", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Bias", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Area", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("L.O.P Before", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("L.O.P After", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                    Event_Details.AddCell(new Phrase("Sign", FontFactory.GetFont("Arial", 8, Font.BOLD)));
                }
            }
            Event_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + Event_Details.TotalHeight + 30;

            query = "exec SP_PDFBILLS_MASTERBILLING_SYN   @SZ_BILL_NUMBER= '" + pdfbills[0].ToString() + "', @FLAG='GET_PROCEDURE_GROUP'";
            da = new SqlDataAdapter(query, con);
            DataSet ProcedureGroup = new DataSet();
            da.Fill(ProcedureGroup);



            query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_PROCEDURE_GROUP_ID='" + ProcedureGroup.Tables[0].Rows[0][0].ToString() + "',@SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_DOCTOR_DIAGNOSIS_CODE'";
            da = new SqlDataAdapter(query, con);
            DataSet dsDiagnosys = new DataSet();
            da.Fill(dsDiagnosys);
            string Data = "";
            foreach (DataRow dr in dsDiagnosys.Tables[0].Rows)
            {
                if ((dr[0].ToString() != "") || (dr[1].ToString() != ""))
                {
                    if (dr[1].ToString() != "")
                        Data = Data + dr[0].ToString() + "-" + dr[1].ToString() + ",";
                    else
                        Data = Data = dr[0].ToString() + ",";
                }
            }
            width = new float[] { 2f, 4f };
            PdfPTable Diagnosis_Details = new PdfPTable(width);
            Diagnosis_Details.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Diagnosis_Details.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Diagnosis_Details.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Diagnosis_Details.DefaultCell.Border = Rectangle.NO_BORDER;
            Diagnosis_Details.AddCell(new Phrase("TENTATIVE DIAGNOSIS :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Diagnosis_Details.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Diagnosis_Details.AddCell(new Phrase(Data, FontFactory.GetFont("Arial", 9)));
            if (Diagnosis_Details.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Diagnosis_Details.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height, writer.DirectContent);
            Previous_Height = Previous_Height + Diagnosis_Details.TotalHeight + 30;

            query = "exec SP_PDFBILLS_MASTERBILLING_SYN  @SZ_CASE_ID='" + sz_case_id.ToString() + "', @FLAG='GET_TREATMENT_PLAN'";
            da = new SqlDataAdapter(query, con);
            DataSet dsTreatmentPlan = new DataSet();
            da.Fill(dsTreatmentPlan);

            Data = "";
            foreach (DataRow dr in dsTreatmentPlan.Tables[0].Rows)
            {
                if (dr[0].ToString() != "")
                {
                    Data = Data + dr[0].ToString() + ",";

                }
            }
            PdfPTable Doctor_Notes = new PdfPTable(width);
            Doctor_Notes.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Doctor_Notes.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Doctor_Notes.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Doctor_Notes.DefaultCell.Border = Rectangle.NO_BORDER;
            Doctor_Notes.AddCell(new Phrase("TREATMENT PLAN :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Doctor_Notes.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Doctor_Notes.AddCell(new Phrase(Data, FontFactory.GetFont("Arial", 9)));
            if (Doctor_Notes.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Doctor_Notes.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);
            Previous_Height = Previous_Height + 30;
            Data = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[0].ToString() != "")
                {
                    Data = Data + dr[0].ToString() + ",";

                }
            }
            PdfPTable Comments = new PdfPTable(width);
            Comments.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            Comments.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            Comments.DefaultCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Comments.DefaultCell.Border = Rectangle.NO_BORDER;
            Comments.AddCell(new Phrase("COMMENTS :", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            Comments.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            Comments.AddCell(new Phrase("", FontFactory.GetFont("Arial", 9)));
            if (Comments.TotalHeight > doc.PageSize.Height - Previous_Height - doc.TopMargin - doc.BottomMargin - 50)
            {
                doc.NewPage();
                Previous_Height = 0;
            }
            Comments.WriteSelectedRows(0, -1, doc.LeftMargin, doc.PageSize.Height - doc.TopMargin - Previous_Height - 30, writer.DirectContent);

            doc.Close();


            query = "exec SP_INSERT_SYN_BILLING_REPORT_TO_DOCMANAGER @SZ_CASE_ID='" + sz_case_id.ToString() + "', @SZ_FILE_NAME='" + filename + "', " +
                       "@SZ_FILE_PATH='" + filepath + "', @SZ_LOGIN_ID ='" + (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME) + "'";
            da = new SqlDataAdapter(query, con);
            ds = new DataSet();
            da.Fill(ds);
            destinationdir = ds.Tables[1].Rows[0][0].ToString() + destinationdir;
            if (!Directory.Exists(destinationdir))
                Directory.CreateDirectory(destinationdir);
            System.IO.File.WriteAllBytes(destinationdir + filename, m.GetBuffer());
         
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
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
    protected void Btn_Selected_Click(object sender, EventArgs e)
    {
        //for (int f = 11; f <= 22; f++)
        //{
        //    grdMasterBilling.Columns[f].Visible = true;
        //}

        AddBillSameCase();
        //for (int f = 11; f <= 22; f++)
        //{
        //    grdMasterBilling.Columns[f].Visible = false;
        //}
    }

    public void BindReffGrid()
    {

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
            String szSpecility = p_szSpeciality;
            //String szSpecility = "MRI";
            //Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
            Session["TM_SZ_BILL_ID"] = p_szBillNumber;

            objNF3Template = new Bill_Sys_NF3_Template();

            CaseDetailsBO objCaseDetails = new CaseDetailsBO();
            String szCompanyID = "";
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
            {
                szCompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
            }
            else
            {
                szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }

            if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                String szDefaultPath = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDefaultPath = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }

                String szSourceDir = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szSourceDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }
                else
                {
                    szSourceDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                }

                String szDestinationDir = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID) + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }
                else
                {
                    szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + szSpecility + "/";
                    //szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/";

                }

                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strNextDiagFileName = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                String szFile3 = ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                String szFile4 = ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                String szPDFPage1 = "";
                String szXMLFileName;
                String szOriginalPDFFileName;
                String szLastXMLFileName;
                String szLastOriginalPDFFileName;
                String sz3and4Page = "";
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

                String szPDFFileName = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    // Note : Generate PDF with Billing Information table. **** II
                    szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                    //log.Debug("Bill Details PDF File : " + szPDFFileName);
                }
                else
                {
                    // Note : Generate PDF with Billing Information table. **** II
                    szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                    //log.Debug("Bill Details PDF File : " + szPDFFileName);
                }


                sz3and4Page = szFile3;//""objPDFReplacement.Merge3and4Page(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szFile3, szFile4);

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    szPDFPage1 = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                }


                //log.Debug("Page1 : " + szPDFPage1);


                // Merge **** I AND **** II
                String szPDF_1_3 = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
                }
                else
                {
                    // (                                                string p_szCompanyID,                                                           string p_szCompanyName,                                                             string p_szCaseID,                  string p_szBillID,              string p_szFile1, string p_szFile2)
                    szPDF_1_3 = objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDFPage1, szPDFFileName);
                }



                String szLastPDFFileName = "";
                String szPDFPage3 = "";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID), Session["TM_SZ_CASE_ID"].ToString());
                }
                else
                {
                    szPDFPage3 = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, szLastOriginalPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                }



                MergePDF.MergePDFFiles(objNF3Template.getPhysicalPath() + szDefaultPath + szPDF_1_3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3, objNF3Template.getPhysicalPath() + szDefaultPath + szPDFPage3.Replace(".pdf", "_MER.pdf"));

                szLastPDFFileName = szPDFPage3.Replace(".pdf", "_MER.pdf");// objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), szPDF_1_3, szPDFPage3);
                //szLastPDFFileName = objPDFReplacement.ReplacePDFvalues(szLastXMLFileName, ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDefaultPath + szLastPDFFileName, Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, Session["TM_SZ_CASE_ID"].ToString());
                String szGenereatedFileName = "";
                szGenereatedFileName = szDefaultPath + szLastPDFFileName;
                //log.Debug("GenereatedFileName : " + szGenereatedFileName);


                String szOpenFilePath = "";
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szGenereatedFileName;
                //szOpenFilePath = "C:\\LawAllies\\MBSUpload\\" + szGenereatedFileName;

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
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                }
                else
                {
                    if (System.IO.File.Exists(szFileNameWithFullPath) && System.IO.File.Exists(szFileNameWithFullPath.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        szFileNameForSaving = szOpenFilePath.Replace(".pdf", "_New.pdf").ToString();
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                    }
                    else
                    {
                        szFileNameForSaving = szOpenFilePath.ToString();
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
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
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                }
                else
                {
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                objAL.Add(Session["TM_SZ_CASE_ID"]);
                objAL.Add(szTemp[szTemp.Length - 1].ToString());
                objAL.Add(szDestinationDir);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                objAL.Add(szSpecility);
                //objAL.Add("");
                objAL.Add("NF");
                objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO);
                //objAL.Add(txtCaseNo.Text);
                objNF3Template.saveGeneratedBillPath(objAL);

                // Start : Save Notes for Bill.

                _DAO_NOTES_EO = new DAO_NOTES_EO();
                _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szBillName;

                _DAO_NOTES_BO = new DAO_NOTES_BO();
                _DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }

                _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                //BindLatestTransaction();

                // End 


            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                Bill_Sys_PVT_Template _objPvtBill;
                _objPvtBill = new Bill_Sys_PVT_Template();
                bool isReferingFacility = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string szCompanyId;
                string szCaseId = Session["TM_SZ_CASE_ID"].ToString();
               // string szSpecility ;
                string szCompanyName;
                string szBillId = Session["TM_SZ_BILL_ID"].ToString();
                string szUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                string szUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID)
                {
                    Bill_Sys_NF3_Template _objCompanyname = new Bill_Sys_NF3_Template();
                    szCompanyName = _objCompanyname.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    szCompanyId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    szCompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    szCompanyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                _objPvtBill.GeneratePVTBill(isReferingFacility, szCompanyId, szCaseId, szSpecility, szCompanyName, szBillId, szUserName, szUserId);
                
            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000001")
            {
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    string UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                    string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string CaseNO = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    // string speciality = Session["WC_Speciality"].ToString();
                    WC_Bill_Generation _objNFBill = new WC_Bill_Generation();
                 
                    //string szFinalPath = _objNFBill.GeneratePDFForWorkerComp(hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, rdbListPDFType.SelectedValue, szCompanyID, CmpName, UserId, UserName, CaseNO, hdnSpeciality.Value.ToString(), 1);
                    string szFinalPath = _objNFBill.GeneratePDFForReferalWorkerComp((string)Session["TM_SZ_BILL_ID"], ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, szCompanyID, CmpName, UserId, UserName, p_szSpeciality);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szFinalPath + "');", true);

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }
            }
            else if (objCaseDetails.GetCaseType(Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
            {
                mbs.LienBills.Lien obj = new Lien();
                string path = obj.GenratePdfForLien(txtCompanyID.Text, p_szBillNumber, _bill_Sys_BillTransaction.GetDoctorSpeciality(p_szBillNumber, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
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
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    

    protected void grdMasterBilling_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Editt")
        {
            bool _ischeck = false;
            string _caseID = "";
            int _isSameCaseID = 0;
            try
            {
                Bil_Sys_Associate_Diagnosis _dianosis_Association = new Bil_Sys_Associate_Diagnosis();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                _dianosis_Association.EventProcID = commandArgs[0].ToString();//SZ_PATIENT_TREATMENT_ID
                _dianosis_Association.DoctorID = commandArgs[1].ToString();//doctorid
                _dianosis_Association.CaseID = commandArgs[2].ToString(); //caseid
                _dianosis_Association.ProceuderGroupId = commandArgs[3].ToString(); //SZ_PROCEDURE_GROUP_ID
                Session["DIAGNOS_ASSOCIATION"] = _dianosis_Association;
               // Page.RegisterStartupScript("mm", "<script type='text/javascript'>showReceiveDocumentPopup();</script>");
               // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "<script type='text/javascript'>showReceiveDocumentPopup();</script>", true);
                ScriptManager.RegisterStartupScript(this,GetType(),"MM123","<script type='text/javascript'>showReceiveDocumentPopup();</script>", false);

                divid.Visible = true;                
            }
            catch { }
        }
        if (e.CommandName.ToString() == "DocManager")
        {
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(e.CommandArgument.ToString(), "");
            _bill_Sys_CaseObject.SZ_CASE_ID = e.CommandArgument.ToString();
            _bill_Sys_CaseObject.SZ_COMAPNY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID.ToString());
            _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
            Session["Case_ID"] = e.CommandArgument.ToString();
            Session["QStrCaseID"] = e.CommandArgument.ToString();
            Session["QStrCID"] = e.CommandArgument.ToString();
            Session["SN"] = "0";
            Session["Archived"] = "0";
            ScriptManager.RegisterStartupScript(this, GetType(), "MMss1231", "<script type='text/javascript'>window.open('../Document Manager/case/vb_CaseInformation.aspx', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');</script>", false);

        }
        
    }
}
