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
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using log4net;


public partial class Bill_Sys_ViewBillRecordSpeciality : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    private static ILog log = LogManager.GetLogger("Bill_Sys_ViewBillRecordSpeciality");
  
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnUpdateStatus.Attributes.Add("onclick", "return formValidator('frmDoctor','txtBillStatusdate');");
            btnPrintEnvelop.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnPrintEnvelop')");
            btnPrintPOM.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnPrintPOM')");
            btnUpdateStatus.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnUpdateStatus')");

            if (!Page.IsPostBack)
            {
                txtBillStatusdate.Text = System.DateTime.Today.ToShortDateString();
                if (Request.QueryString["flag"].ToString() == "View")
                {
                    Session["sz_StatusID"] = Request.QueryString["Status"].ToString();
                    Session["sz_SpecialityID"] = Request.QueryString["speciality"].ToString();
                }
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                log.Debug("BindGrid method is being called");
                BindGrid();
                log.Debug("BindGrid method is called");
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
            cv.MakeReadOnlyPage("Bill_Sys_ViewBillRecordSpeciality.aspx");
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
            if (Session["sz_StatusID"] != null)
            {
                objAL.Add(Session["sz_StatusID"].ToString());//0
            }
            else
            {
                Session["sz_StatusID"] = "";
            }
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);//1
            objAL.Add(txtSearchOrder.Text);//2
            if (Session["sz_SpecialityID"] != null)
            {
                objAL.Add(Session["sz_SpecialityID"].ToString());//3
            }
            else
            {
                Session["sz_SpecialityID"] = "";
            }
            if (Session["sz_LOCATION_Id"] != null)
            {
                objAL.Add(Session["sz_LOCATION_Id"].ToString());//4
            }
            else
            {
                Session["sz_LOCATION_Id"] = "";
            }
            if (Session["User_ID"] != null)
            {
                objAL.Add(Session["User_ID"].ToString());//5
            }
            else
            {
                Session["User_ID"] = "";
            }
            if (Session["sz_FromDate"] != null)
            {
                objAL.Add(Session["sz_FromDate"].ToString());//6
            }
            else
            {
                Session["sz_FromDate"] = "";
            }
            if (Session["sz_Todate"] != null)
            {
                objAL.Add(Session["sz_Todate"].ToString());//7
            }
            else
            {
                Session["sz_Todate"] = "";
            }
            //Nirmalkumar
            if (Session["sz_visitFromDate"] != null)
            {
                objAL.Add(Session["sz_visitFromDate"].ToString());//6
            }
            else
            {
                Session["sz_visitFromDate"] = "";
            }
            if (Session["sz_visitTodate"] != null)
            {
                objAL.Add(Session["sz_visitTodate"].ToString());//7
            }
            else
            {
                Session["sz_visitTodate"] = "";
            }
        
            //grdBillReportDetails.DataSource = _reportBO.getBillReportSpecialityDetails(objAL);
            //grdBillReportDetails.DataBind();
            log.Debug("getBillReportSpecialitySearch method is being called");
            DataSet ds = new DataSet();
            ds= _reportBO.getBillReportSpecialitySearch(objAL);
            log.Debug("getBillReportSpecialitySearch method is called");
            //add status code column
            SqlConnection sqlCon;
            String strsqlCon;
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            sqlCon = new SqlConnection(strsqlCon);
            SqlCommand sqlCmd;
            SqlDataAdapter sqlda;
            DataSet ds1 = new DataSet();
            if (ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dtr in ds.Tables[0].Rows)
                {
                    sqlCmd = new SqlCommand("sp_get_status_code_for_bill", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Parameters.AddWithValue("@sz_bill_number", dtr[0].ToString());
                    sqlda = new SqlDataAdapter(sqlCmd);
                    sqlda.Fill(ds1);
                   
                }
            }
            //end add
            grdBillReportDetails.DataSource=ds.Tables[0];
            grdBillReportDetails.DataBind();
            
       
            Boolean flag = true;
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grdBillReportDetails.Items[i].FindControl("chkUpdateStatus");

                        if ((ds1.Tables[0].Rows[i][0].ToString() == "TRF") || (ds1.Tables[0].Rows[i][0].ToString() == "DNL"))
                        {
                            chk.Enabled = false;
                            flag = false;
                        }
                        else
                        {
                            chk.Enabled = true;
                        }
                    }
                }
            }
            if(flag==false)
            ((CheckBox)(grdBillReportDetails.Controls[0].Controls[0].FindControl("chkSelectAll"))).Enabled = false;
            
            Session["sort"] = ds;
            Decimal toatal = 0;
            foreach (DataGridItem dg in grdBillReportDetails.Items)
            {
                if(dg.Cells[11].Text != "&nbsp" && dg.Cells[11].Text != "")
                    toatal = toatal + Convert.ToDecimal(dg.Cells[11].Text);
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


            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["sort"];
            dv = ds.Tables[0].DefaultView;
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
                //BindGrid();
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
                //BindGrid();
            }
            dv.Sort = txtSearchOrder.Text;
            grdBillReportDetails.DataSource = dv;
            grdBillReportDetails.DataBind();

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
                    lblMessage.Text = "Bill status updated successfully.";
                    lblMessage.Visible = true;
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
            String szXMLPhysicalpath = ConfigurationManager.AppSettings["NF3_ENVELOPE_XML_FILE"];
            String szXMLPhysicalpathWC = ConfigurationManager.AppSettings["NF3_ENVELOPE_XML_WC_FILE"];
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            String szSourceFile1 = "";
            String szSourceFile1_FullPath = "";
            String szSourceFile2 = "";
            String szSourceFile2_FullPath = "";
            String szOpenFilePath = "";

            String szSourceFileWC1 = "";
            String szSourceFileWC1_FullPath = "";
            String szSourceFileWC2 = "";
            String szSourceFileWC2_FullPath = "";
            String szOpenFilePathWC = "";
            String szDefaultPathWC = "";
            String szDefaultPath = "";
            for (int i = 0; i < grdBillReportDetails.Items.Count; i++)
            {
               
                String szCaseID = grdBillReportDetails.Items[i].Cells[0].Text;
                String szBillID = grdBillReportDetails.Items[i].Cells[5].Text;
                CheckBox chkDelete1 = (CheckBox)grdBillReportDetails.Items[i].FindControl("chkUpdateStatus");
                if (chkDelete1.Checked)
                {
                    string _caseType = grdBillReportDetails.Items[i].Cells[23].Text;
                    String szCaseIDWC = grdBillReportDetails.Items[i].Cells[0].Text;
                    if (_caseType == "WC000000000000000001")
                    {
                        //szDefaultPathWC = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string szGeneratedPDFName = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpathWC, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);
                        if (szSourceFileWC1 == "")
                        {
                            szDefaultPathWC = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";

                            szSourceFileWC1 = szGeneratedPDFName;
                            szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC  + szSourceFileWC1;
                            szOpenFilePathWC = szDefaultPathWC + szSourceFile1;
                            string szGeneratedPDFName1 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);

                            szSourceFileWC2 =  szGeneratedPDFName1;
                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC + "_" + szSourceFileWC1);
                            szSourceFileWC1 = "_" + szSourceFileWC1;
                            szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1;
                            szOpenFilePathWC = szDefaultPathWC + szSourceFileWC1;
                        }
                        else
                        {
                            //string szGeneratedPDFName1 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                           String szDefaultPathWCElse = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseIDWC + "/Packet Document/";

                            szSourceFileWC2 =  szGeneratedPDFName;
                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWCElse + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC +  szSourceFileWC1);

                            string szGeneratedPDFName2 = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseIDWC);
                            szSourceFileWC2 = szGeneratedPDFName2;

                            szSourceFileWC2_FullPath = szBasePhysicalPath + szDefaultPathWCElse + szSourceFileWC2;
                            MergePDF.MergePDFFiles(szSourceFileWC1_FullPath, szSourceFileWC2_FullPath, szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1);
                           // szSourceFile1 = "_" + szSourceFile1;
                           szSourceFileWC1_FullPath = szBasePhysicalPath + szDefaultPathWC + szSourceFileWC1;
                           szOpenFilePathWC = szDefaultPathWC + szSourceFileWC1;
                        }
                    }
                    else
                    {

                         //szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
                        PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string szGeneratedPDFNameNF = objPDFReplacement.ReplacePDFvalues(szXMLPhysicalpath, szPDFPhysicalpath, szBillID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                        if (szSourceFile1 == "")
                        {
                            szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";

                            szSourceFile1 = szGeneratedPDFNameNF;
                            szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                            szSourceFile1 =  szSourceFile1;
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }
                        else
                        {
                         String szDefaultPathNF = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";

                            szSourceFile2 = szGeneratedPDFNameNF;
                            szSourceFile2_FullPath = szBasePhysicalPath + szDefaultPathNF + szSourceFile2;
                            MergePDF.MergePDFFiles(szSourceFile1_FullPath, szSourceFile2_FullPath, szBasePhysicalPath + szDefaultPath + "_" + szSourceFile1);
                            szSourceFile1 = "_" + szSourceFile1;
                            szSourceFile1_FullPath = szBasePhysicalPath + szDefaultPath + szSourceFile1;
                            szOpenFilePath = szDefaultPath + szSourceFile1;
                        }
                    
                    }
                   
                }

            }

            if (szOpenFilePathWC != "" && szOpenFilePath != "")
            {
               
                MergePDF.MergePDFFiles(szBasePhysicalPath + szOpenFilePathWC, szBasePhysicalPath + szOpenFilePath, szBasePhysicalPath + szOpenFilePathWC);
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() +  szOpenFilePathWC;

            }
            else if (szOpenFilePath != "")
            {
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePath;
            }
            else if (szOpenFilePathWC != "" )
            {
                szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePathWC;
            }
            
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

   

    #region "POM Generation"

    protected void btnPrintPOM_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            creatPDF();
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

    protected string GenerateHTML()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        StringBuilder sbFinalHTML = new StringBuilder();
        sbFinalHTML.AppendLine("");
        string genhtml = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
        try
        {
            int i = 0;
            int printi = 0;
            int iPageCount = 1;
            int iRecordOnPage = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
            string billid = "";
            DataSet ds = CreateGroupData();
            for (int iCount=0 ; iCount < ds.Tables[0].Rows.Count;iCount++)
            {
                if (i == (iPageCount * iRecordOnPage))
                {
                    genhtml += "</table>";
                   // sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtml, iRecordOnPage, ds.Tables[0].Rows[iCount][5].ToString()));
                    sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtml, ds.Tables[0].Rows[iCount][5].ToString()));

                    sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                    genhtml = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                    iPageCount++;
                    printi = 0;
                }
                //                                            Index                                                    Claim Number                                           Insurance Company Name             Insurance Company Address                                                                                                                                                        Speciality / Minimum date of service                                              // Total Bill Amount / Last date of service of bill                                                                                           // Bill Number                                                                             // Patient Name
                genhtml += "<tr><td style='font-size:9px'>" + (printi + 1).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[iCount][20].ToString() + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[iCount][18].ToString() + "</b><br/>" + ds.Tables[0].Rows[iCount][19].ToString() + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[iCount][9].ToString() + "<br />" + ds.Tables[0].Rows[iCount][21].ToString() + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[iCount][11].ToString() + "<br />" + ds.Tables[0].Rows[iCount][22].ToString() + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[iCount][5].ToString() + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[iCount][8].ToString() + ' ' + ds.Tables[0].Rows[iCount][4].ToString() + "</td></tr>";

                i = i + 1;
                printi = printi + 1;
                Session["VL_COUNT"] = i;
                billid = ds.Tables[0].Rows[iCount][5].ToString();
            }
            genhtml += "</table>";
           // sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtml, (i - (iRecordOnPage * (iPageCount - 1))), billid));
            sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtml, billid));
            
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
        return sbFinalHTML.ToString();

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //protected string ReplaceHeaderAndFooter(String szInput,int szCount,string sz_bill_no)
   protected string ReplaceHeaderAndFooter(String szInput,string sz_bill_no)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string szFileData = "";
        try
        {
            szFileData = File.ReadAllText(ConfigurationManager.AppSettings["POM_HTML"]);
            //GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
            szFileData = getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, sz_bill_no);
            szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", szInput);
           szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", "");
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

    public string getNF2MailDetails(string p_htmlstring, string p_szCompanyID,string sz_bill_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        try
        {
            //log.Debug("Bill_Sys_BilPOM. Method - getNF2MailDetails : ");
            string szReplaceString = "";
            szReplaceString = p_htmlstring;
            SqlCommand objSqlComm1 = new SqlCommand("SP_NF3_MAILS_DETAILS", objSqlConn);
            SqlDataReader objDR;
            objSqlComm1.CommandType = CommandType.StoredProcedure;
            objSqlComm1.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
            objSqlComm1.Parameters.AddWithValue("@SZ_BILL_ID", sz_bill_id);
            objDR = objSqlComm1.ExecuteReader();
            szReplaceString = objPDF.ReplaceNF2MailDetails(szReplaceString, objDR);
            return szReplaceString;
        }
        catch (Exception ex)
        {
            //log.Debug("Bill_Sys_BilPOM. Method - getNF2MailDetails : " + ex.Message.ToString());
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            return null;
        }
        finally
        {
            if (objSqlConn.State == ConnectionState.Open)
            {
                objSqlConn.Close();
            }
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
        GeneratePatientInfoPDF objPDF = new GeneratePatientInfoPDF();
        string pdffilename = "";
        try
        {
          //  string szFileData = File.ReadAllText(ConfigurationManager.AppSettings["NF2_SENT_MAIL_HTML"]);
          //  szFileData = objPDF.getNF2MailDetails(szFileData, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            string strHtml = GenerateHTML();
            //szFileData = szFileData.Replace("VL_SZ_TABLE_DATA", strHtml);
            //szFileData = szFileData.Replace("VL_SZ_CASE_COUNT", Session["VL_COUNT"].ToString());
            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();
            objHTMToPDF.Serial = "10007706603";
            string htmfilename = getFileName("P") + ".htm";
            pdffilename = getFileName("P") + ".pdf";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename, ConfigurationManager.AppSettings["EXCEL_SHEET"] + pdffilename);
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
       
        return pdffilename;
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
        string pdffilename="";
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
        string billidNF="";
        Bill_Sys_NF3_Template _objTemp = new Bill_Sys_NF3_Template();
        StringBuilder sbFinalHTML = new StringBuilder();


        sbFinalHTML.AppendLine("");
        int iRecordOnpageNF = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        int iRecordOnpageWC = Convert.ToInt32(ConfigurationManager.AppSettings["POM_RECORD_PER_PAGE"]);
        try
        {
           
            String szBasePhysicalPath = _objTemp.getPhysicalPath();
            string szDefaultPath = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"]; 
            int iFlag = 0;
            DataSet ds = CreateGroupData();
            int iRecordsPerPageNF = 0;
            int iRecordsPerPageWC = 0;
             string strProvider=null;
            string strProviderNF= null;
            string strCasetype=null;
            bool iWCCount = false;
            
             genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
            genhtmlWC= "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";//</td><td><b>Patient Name</b></td><td><b>Insurance Company</b></td><td><b>Insurance Address</b></td><td><b>Claim No</b></td><td><b>Date Of Accident</b></td></tr>";
           
            // create html and pdf for groupwise provider . print per page in pdf for provider 
            
             int _iCountWC = 0;
             int _iCountNF = 0;  



             

             for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
             {

                 strCasetype = ds.Tables[0].Rows[j]["CaseType"].ToString();

                 if (strCasetype == "WC000000000000000001")
                 {

                     sbFinalHTML.AppendLine("");
                     if (iFlag == 0)
                     {
                         strProvider = ds.Tables[0].Rows[j]["Provider"].ToString();
                         iFlag = 1;
                         billidWC = ds.Tables[0].Rows[j]["Bill ID"].ToString();
                         iRecordsPerPageWC = 0;
                     }
                     //IF PRESENT PROVIDER OR FIRST PROVIDER MATCH WITH PRIVISIOUS PROVIDER OR FRIST PROVIDER
                     if (strProvider.Equals(ds.Tables[0].Rows[j]["Provider"].ToString()))
                     {
                         ++printi;
                         ++printiWC;
                         if (iRecordsPerPageWC == iRecordOnpageWC )
                         {
                             sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billIDWC));
                             sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                             iWCCount = true;
//                             genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                             iRecordsPerPageWC = 1;
                             _iCountWC = 1;
                         }
                         if (iWCCount == true)
                         {
                             genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                             iWCCount = false;
                         }
                        
                         // append nf html for wc bill
                         genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                         _iCountNF++;


                         //append wc html for wc bill
                         //                                            Index                                                    Claim Number                                                                       wc Name                                wc  Address                                                                                                                                                                                              Speciality / Minimum date of service                                                                                          // Total Bill Amount / Last date of service of bill                                                                                           // Bill Number                                                                             // Patient Name
                         genhtmlWC += "<tr><td style='font-size:9px'>" + (printiWC).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>" + ds.Tables[0].Rows[j]["WC_ADDRESS"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                         _iCountWC++;
                      
                         iRecordsPerPageWC++;
                         billidWC = ds.Tables[0].Rows[j]["Bill ID"].ToString();
                         billidNF  = ds.Tables[0].Rows[j]["Bill ID"].ToString();


                     }
                     else
                     {
                         printi = 1;
                         printiWC = 1;
                         // SUPPOSE NEW PROVIDER OR CHANGE PROVIDER WHICH NO MATCH WITH PRIVOUS PROIVDER
                         if (_iCountWC > 0)
                         {
                             genhtmlWC += "</table>";
                             sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC ));
                             sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                             iWCCount = true;
                             //genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                             printiWC = 1;
                         }
                         if (iWCCount == true)
                         {
                             genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                             iWCCount = false;
                         }

                         genhtmlNF += "</table>";
                         sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF));
                         sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");

                         // NEW PROVIDER AND BILLID SAVE IN VARIABLES
                         strProvider = ds.Tables[0].Rows[j]["Provider"].ToString();
                         billidWC = ds.Tables[0].Rows[j]["Bill ID"].ToString();
                         billidNF = ds.Tables[0].Rows[j]["Bill ID"].ToString();

                         genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";

                         //append nf html for wc bill
                         genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                         _iCountNF++;

                         //append wc html for wc bill
                         genhtmlWC += "<tr><td style='font-size:9px'>" + (printiWC).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b> Worker's Compensation Board </b><br/>" + ds.Tables[0].Rows[j]["WC_ADDRESS"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                         _iCountWC++;
                        
                         iPageCount++;
                        
                         iRecordsPerPageWC = 1;
                         iRecordsPerPageNF = 1;
                         _iCountNF = 1;
                     }



                 }
                 else if (strCasetype != "WC000000000000000001")                     
                 {
                     // ONLY NF PROVIDER
                     iRecordsPerPageNF = _iCountNF;
                     sbFinalHTML.AppendLine("");
                     if (iFlag == 0)
                     {
                         strProvider = ds.Tables[0].Rows[j]["Provider"].ToString();
                         iFlag = 1;
                         billidNF = ds.Tables[0].Rows[j]["Bill ID"].ToString();
                         iRecordsPerPageNF = 0;
                     }
                             if (strProvider.Equals(ds.Tables[0].Rows[j]["Provider"].ToString()))
                             {
                                 ++printi;

                                 if (iRecordsPerPageNF == iRecordOnpageNF)
                                 {
                                     sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, ds.Tables[0].Rows[j]["Bill ID"].ToString()));
                                     sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                                     genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                     iRecordsPerPageNF = 0;
                                     _iCountNF = 0;
                                 }

                               ////changes from sy
                               //  if (_iCountWC > 0 )
                               //  {
                               //      genhtmlNF += "</table>";
                               //      sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF));
                               //      sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                               //      genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                               //  }
                               //  //======================================================================



                                 genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                                 _iCountNF++;
                                 iRecordsPerPageNF++;
                             }
                             else
                             {
                                 printi = 1;
                                 //save provider name in variable
                                 strProvider = ds.Tables[0].Rows[j]["Provider"].ToString();
                               
                                 genhtmlNF += "</table>";

                                 if (_iCountWC > 0)
                                 {
                                     genhtmlWC += "</table>";
                                     sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlWC, billidWC ));
                                     sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                                     iWCCount = true;
                                    // genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                     printiWC = 0;
                                 }
                                 //if (iWCCount == true)
                                 //{
                                 //    genhtmlWC = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";
                                 //    iWCCount = false;
                                 //}
                                
                                 sbFinalHTML.Append(ReplaceHeaderAndFooter(genhtmlNF, billidNF));
                                 sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");
                                 genhtmlNF = "<table border='1' width='100%'>  <tr>    <td style='font-size:9px' width='3%'>Line</td>    <td style='font-size:9px' width='5%'>Article Number</td>    <td style='font-size:9px' width='25%'>Name of Addressee, Street, and Post Office Address</td>    <td style='font-size:9px' width='5%'>Postage</td>    <td style='font-size:9px' width='5%'>Fee</td>    <td style='font-size:9px' width='5%'>Handling Charge</td>    <td style='font-size:9px' width='5%'>Act. Value (if regis.)</td>    <td style='font-size:9px' width='5%'>Insured Value</td>    <td style='font-size:9px' width='5%'>Due Sender if COD</td>    <td style='font-size:9px' width='5%'>R.R. fee</td>    <td style='font-size:9px' width='5%'>S.D. fee</td>    <td style='font-size:9px' width='5%'>S.H. fee</td>    <td style='font-size:9px' width='5%'>Rest Del fee <br/>     Remarks</td>  </tr>";

                                 genhtmlNF += "<tr><td style='font-size:9px'>" + (printi).ToString() + "</td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Claim Number"] + "</td><td style='font-size:9px'><b>" + ds.Tables[0].Rows[j]["Insurance Company"] + "</b><br/>" + ds.Tables[0].Rows[j]["Insurance Address"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Spciality"] + "<br />" + ds.Tables[0].Rows[j]["Min Date Of Service"] + "</td><td style='font-size:9px' align='right'> $" + ds.Tables[0].Rows[j]["Bill Amount"] + "<br />" + ds.Tables[0].Rows[j]["Max Date Of Service"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Bill ID"] + "</td><td style='font-size:9px'></td><td style='font-size:9px'>" + ds.Tables[0].Rows[j]["Patient Name"] + ' ' + ds.Tables[0].Rows[j]["Case #"] + "</td></tr>";
                                 _iCountNF++;
                                 iPageCount++;
                                 billidNF = ds.Tables[0].Rows[j]["Bill ID"].ToString();
                                 iRecordsPerPageNF = 1;
                                 _iCountNF = 1;
                             
                             }
                    
                 }

             }

            
          
          
             string genHtml = "";
             if (iWCCount == false && _iCountWC>0)
             {
                
                 genhtmlWC += "</table>";
                 sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genhtmlWC, billidWC));
                 sbFinalHTML.AppendLine("<span style='page-break-after:always'></span>");   
             } 
            
            
             if (_iCountNF > 0)
             {
                 genhtmlNF += "</table>";
                 genHtml = genhtmlNF;
                 sbFinalHTML.AppendLine(ReplaceHeaderAndFooter(genHtml, billidNF));
               
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
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename);
            sw.Write(strHtml);
            sw.Close();
            Int32 iTemp;
            iTemp = objHTMToPDF.HtmlToPdfConvertFile(ConfigurationManager.AppSettings["EXCEL_SHEET"] + htmfilename, ConfigurationManager.AppSettings["EXCEL_SHEET"] + pdffilename);


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
            dt.Columns.Add("Bill ID");
            dt.Columns.Add("Case #");
            dt.Columns.Add("Patient Name");
            dt.Columns.Add("Spciality");
            dt.Columns.Add("Provider");
            dt.Columns.Add("Bill Amount");
            dt.Columns.Add("Visit Date");
            dt.Columns.Add("Bill Date");
            dt.Columns.Add("Bill Staus Date");
            dt.Columns.Add("Status");
            dt.Columns.Add("Username");
            dt.Columns.Add("Claim Number");
            dt.Columns.Add("Insurance Company");
            dt.Columns.Add("Insurance Address");
            dt.Columns.Add("Min Date Of Service");
            dt.Columns.Add("Max Date Of Service");
            dt.Columns.Add("CaseType");
            dt.Columns.Add("WC_ADDRESS");
            foreach (DataGridItem dg in grdBillReportDetails.Items)
            {
                if (((CheckBox)dg.Cells[0].FindControl("chkUpdateStatus")).Checked == true)
                {

                    DataRow dr = dt.NewRow();
                    dr["Bill ID"] = dg.Cells[5].Text;
                    dr["Case #"] = dg.Cells[4].Text;
                    dr["Patient Name"] = dg.Cells[8].Text;
                    dr["Spciality"] = dg.Cells[9].Text;
                    dr["Provider"] = dg.Cells[10].Text;
                    dr["Bill Amount"] = dg.Cells[11].Text;
                    dr["Visit Date"] = dg.Cells[12].Text;
                    dr["Bill Date"] = dg.Cells[13].Text;
                    dr["Bill Staus Date"] = dg.Cells[14].Text;
                    dr["Status"] = dg.Cells[15].Text;
                    dr["Username"] = dg.Cells[16].Text;
                    dr["Claim Number"] = dg.Cells[20].Text;
                    dr["Insurance Company"] = dg.Cells[18].Text;
                    dr["Insurance Address"] = dg.Cells[19].Text;
                    dr["Min Date Of Service"] = dg.Cells[21].Text;
                    dr["Max Date Of Service"] = dg.Cells[22].Text;
                    dr["CaseType"] = dg.Cells[23].Text;
                    dr["WC_ADDRESS"] = dg.Cells[24].Text;
                    dt.Rows.Add(dr);
                   
                }
            }
            if (dt.Rows.Count > 0)
            {
                //convert DataTable to DataView   
                DataView dv = dt.DefaultView;
                //apply the sort on CustomerSurname column   
                dv.Sort = "Provider";
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

    #endregion

    //private void createEnvelopForWC()
    //{
    //    try
    //    {
    //        for (int i = 0; i < grdBillReportDetails.Items.Count; i++)
    //        {
    //            string _caseType = grdBillReportDetails.Items[i].Cells[23].Text;


    //        }
    //    }
    //    catch (Exception ex)
    //    {
            
    //        throw;
    //    }
    //}
    protected void grdBillReportDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnCreatePacket_Click(object sender, EventArgs e)
    {

    }
}
 