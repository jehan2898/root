/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillTransaction.aspx.cs
/*Purpose              :       To Add and Edit Bill Transaction
/*Author               :       Sandeep Y
/*Date of creation     :       19 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

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
using Componend;
using System.Data.SqlClient;

public partial class Bill_Sys_BillTransactionEntry : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ErrorDiv.InnerText = "";
            ErrorDiv.Style.Value = "color: red";
            if (Session["PassedCaseID"] != null)
            {
                txtCaseID.Text = Session["PassedCaseID"].ToString();
                Session["PassedCaseID"] = "";
            }
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;           
            //btnAddService.Attributes.Add("onclick", "return Amountvalidate();");
            //btnUpdateService.Attributes.Add("onclick", "return Amountvalidate();");
            btnAddService.Attributes.Add("onclick", "return formValidator('frmBillTrans','extddlDiagnosisCode,extddlProcedureCode,txtDateOfservice,txtDateOfServiceTo,txtUnit');");
            btnUpdateService.Attributes.Add("onclick", "return formValidator('frmBillTrans','extddlDiagnosisCode,extddlProcedureCode,txtDateOfservice,txtDateOfServiceTo,txtUnit');");
            imgbtnFromTo.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfServiceTo,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            imgbtnDateofService.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfservice,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            imgbtnOpenedDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtBillDate,'imgbtnOpenedDate','MM/dd/yyyy'); return false;");
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtBillDate,extddlDoctor');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmBillTrans','txtBillDate,extddlDoctor');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                Div1.Visible = true;

                BindAssociateGrid();

                BindLatestTransaction();
               // extddlDiagnosisCode.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
               // extddlProcedureCode.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (grdLatestBillTransaction.Items.Count >= 3)
                {
                    //BindGrid();
                }
                btnUpdate.Enabled = false;
                btnUpdateService.Enabled = false;
                if (Session["SZ_BILL_NUMBER"] != null)
                {
                    //Session["BillID"] = Session["SZ_BILL_NUMBER"];
                    _editOperation = new EditOperation();
                    _editOperation.Primary_Value = Session["SZ_BILL_NUMBER"].ToString();
                    _editOperation.WebPage = this.Page;
                    _editOperation.Xml_File = "BillTransaction.xml";
                    _editOperation.LoadData();
                    extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
                    txtBillNo.Text = Session["SZ_BILL_NUMBER"].ToString();
                    //grdBillTransaction.Visible = false;
                    btnUpdateService.Enabled = true;
                    txtBillDate.Text = String.Format("{0:MM/dd/yyyy}", txtBillDate.Text).ToString();
                    tblServices.Visible = true; 
                    //BindIC9Grid(Session["SZ_BILL_NUMBER"].ToString());
                    btnUpdate.Enabled = true;
                    btnSave.Enabled = false;
                    ClearControl();
                    //btnClear.Enabled = false;
                }
            }
            if (txtBillNo.Text.ToString()!="")
            {
                 BindTransactionData(txtBillNo.Text.ToString());
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
            cv.MakeReadOnlyPage("Bill_Sys_BillTransactionEntry.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        Session["SZ_BILL_ID"] = null;
        Session["PassedCaseID"] = null;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        ArrayList _arrayList;
        try
        {

            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "BillTransaction.xml";
            _saveOperation.SaveMethod();

            BindLatestTransaction();
            if (grdLatestBillTransaction.Items.Count > 3)
            {
                //BindGrid();
            }
            lblMsg.Visible = true;
            lblMsg.Text = " Bill Saved successfully ! ";
            if (Session["AssociateDiagnosis"] != null)
            {
                if (Convert.ToBoolean(Session["AssociateDiagnosis"].ToString()) == true)
                {
                    txtBillNo.Text = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
                    btnAddService.Enabled = true;
                    btnSave.Enabled = false;
                }
                else
                {
                   
                    tblServices.Visible = true;
                    txtBillNo.Text = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
                  
                    BindTransactionDetailsGrid(txtBillNo.Text);

                    grdTransactionDetails.Visible = true;
                }
            }
            else
            {
                tblServices.Visible = true;
                txtBillNo.Text = _bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(txtCompanyID.Text.ToString());
                Session["BillID"] = txtBillNo.Text;
                BindTransactionDetailsGrid(txtBillNo.Text);
                grdTransactionDetails.Visible = true;
                lnlInitialReport.Visible = true;
                //lnkCopyOldrogressReport.Visible = true;
                lnlProgessReport.Visible = true;
                lnkReportOfMMI.Visible = true;
            }
        }

        catch (SqlException objSqlExcp)
        {
            ErrorDiv.InnerText = " Bill Number already exists";
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            if (Session["SZ_BILL_NUMBER"] != null)
            {
                string str = txtBillNo.Text;
                _editOperation.Primary_Value = Session["SZ_BILL_NUMBER"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "BillTransaction.xml";
                _editOperation.UpdateMethod();
                Session["SZ_BILL_NUMBER"] = null;
                Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else
            {
                _editOperation.Primary_Value = Session["BillID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "BillTransaction.xml";
                _editOperation.UpdateMethod();
                //BindGrid();
              //  ErrorDiv.InnerText = " Bill Updated successfully ! ";
             //   ErrorDiv.Style.Value = "color: blue";

                lblMsg.Visible = true;
                lblMsg.Text = " Bill Updated successfully ! ";
                
            }
            BindLatestTransaction();
            //BindGrid();

        }
        catch (SqlException objSqlExcp)
        {
            ErrorDiv.InnerText = " Bill Number already exists";
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

    private void BindLatestTransaction()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "LatestBillTransaction.xml";
            _listOperation.LoadList();
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtBillNo.Text = "";
            txtBillDate.Text = "";
            extddlDoctor.Text = "NA";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;            
            tblServices.Visible = false;
            grdTransactionDetails.Visible = false;
            lblMsg.Visible = false;            
            ClearControl();
           
            
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
     
    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lnlInitialReport.Visible = true;
           // lnkCopyOldrogressReport.Visible = true;
            lnlProgessReport.Visible = true;
            lnkReportOfMMI.Visible = true;

            Session["BillID"] = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            txtBillNo.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            txtBillDate.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[3].Text;
            extddlDoctor.Text = grdLatestBillTransaction.Items[grdLatestBillTransaction.SelectedIndex].Cells[9].Text;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            btnUpdateService.Enabled = false;
            btnAddService.Enabled = true;
            tblServices.Visible = true;
            //btnFromDate.Visible = false;
            extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
            grdTransactionDetails.Visible = true;
            BindTransactionDetailsGrid(Session["BillID"].ToString());
           // BindIC9Grid(Session["BillID"].ToString());
            ClearControl();
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

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                Session["PassedBillID"] = e.CommandArgument;
                Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }


            if (e.CommandName.ToString() == "Generate bill")
            {
                Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;
                string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();

                string strPDFOrHtml = ConfigurationManager.AppSettings["PDFORHTML"].ToString();
                GeneratePDFFile.GeneratePDF objGeneratePDF = new GeneratePDFFile.GeneratePDF();
                string strGenFileName = "";

                if (strPDFOrHtml.Equals("PDF"))
                {
                    strGenFileName = objGeneratePDF.GeneratePDF1(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                }
                else
                {
                    strGenFileName = "saveddocs/" + objGeneratePDF.GenerateReplacedHtmlFile(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), strPath) + ".htm";
                }

                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + strGenFileName + "'); ", true);



               
                //string strPath = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                //GeneratePDFFile.GeneratePDF objGeneratePDF = new GeneratePDFFile.GeneratePDF();

                //String szPDFFileName = objGeneratePDF.GeneratePDF(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, Session["TM_SZ_CASE_ID"].ToString(), Session["TM_SZ_BILL_ID"].ToString(), "", strPath);
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFFileName + "'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
                //     Response.Redirect("TemplateManager/Bill_Sys_GeneratePDF.aspx", false);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {
                //if (e.Item.Cells[5].Text.ToString() != "" && e.Item.Cells[5].Text.ToString() != "&nbsp;")
                //{
                //    if (e.Item.Cells[5].Text.ToString() != "0.00")
                //    {
                if (e.Item.Cells[9].Text != "1")
                {
                    Session["PassedBillID"] = e.CommandArgument;
                    Session["Balance"] = e.Item.Cells[6].Text;
                    Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }

            }
            lblMsg.Visible = false;



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

    protected void lnkShowGrid_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
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

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            //if (e.Item.Cells[5].Text == "" || e.Item.Cells[5].Text == "&nbsp;" || e.Item.Cells[5].Text == "0.00")
            //{
            //    e.Item.Cells[7].Text = "";
            //}
            if (e.Item.Cells[8].Text == "1")
            {
                e.Item.Cells[7].Text = "";
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

    #region "Service Event Handler"

    protected void btnAddService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveTransactionData();
            BindTransactionData(txtBillNo.Text.ToString());
            ClearControl();
            BindLatestTransaction();
            btnUpdateService.Enabled = false;
            btnAddService.Enabled = true;
            if (Session["AssociateDiagnosis"] != null)
            {
                btnAddService.Enabled = false;
            }
           // btnFromDate.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = " Service Added successfully ! ";

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

    protected void btnUpdateService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            UpdateTransactionDetails();
            BindTransactionDetailsGrid(txtBillNo.Text);
            BindLatestTransaction();
           // BindIC9Grid(txtBillNo.Text.ToString());
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

    protected void btnClearService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
           
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

    protected void extddlIC9Code_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Menu = new Bill_Sys_Menu();
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            BindProcedureList(extddlDiagnosisCode.Text);
            //GET_PROCEDURE_CODE_LIST

           
            //extddlProcedureCode.Flag_ID =extddlDiagnosisCode.Text;
            //if (txtUnit.Text != "")
            //{
            //    txtTempAmt.Value = _bill_Sys_Menu.GetICcodeAmount(extddlDiagnosisCode.Text.ToString()).ToString();
            //    CalculateAmount(extddlDiagnosisCode.Text.ToString());
            //}
            //else
            //{
            //    txtAmount.Text = _bill_Sys_Menu.GetICcodeAmount(extddlDiagnosisCode.Text.ToString()).ToString();
            //    txtTempAmt.Value = _bill_Sys_Menu.GetICcodeAmount(extddlDiagnosisCode.Text.ToString()).ToString();
            //}
            btnSave.Attributes.Add("onclick", "return Amountvalidate();");
            lblMsg.Visible = false;
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

    #region "Service Fetch Method"

    private void BindTransactionData(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            //DataSet ds = new DataSet();
            //ds = _bill_Sys_BillTransaction.BindTransactionData(id);
            //int i = 0;
            //foreach (DataColumn c in ds.Tables[0].Columns)
            //{
            //    if (i == 8 || i == 9)
            //    {
            //        BoundColumn bc = new BoundColumn();
            //        bc.DataField = c.ColumnName;
            //        bc.HeaderText = c.ColumnName;
            //        //bc.DataFormatString = setFormating(c);
            //        grdTransactionDetails.Columns.Add(bc);
            //    }
            //    i++;
            //}
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(id);
           grdTransactionDetails.DataBind();           
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionDetailsGrid(string billnumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {

            //DataSet ds = new DataSet();
            //ds = _bill_Sys_BillTransaction.BindTransactionData(billnumber);
            //int i = 0;
            //foreach (DataColumn c in ds.Tables[0].Columns)
            //{
            //    if (i == 8 || i == 9)
            //    {
            //        BoundColumn bc = new BoundColumn();
            //        bc.DataField = c.ColumnName;
            //        bc.HeaderText = c.ColumnName;
            //        //bc.DataFormatString = setFormating(c);

            //        grdTransactionDetails.Columns.Add(bc);
            //    }
            //    i++;
            //}
            grdTransactionDetails.DataSource = _bill_Sys_BillTransaction.BindTransactionData(billnumber); //BindTransactionData
            grdTransactionDetails.DataBind();
            
    
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

    private void CalculateAmount(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Menu = new Bill_Sys_Menu();
        decimal amount;
        try
        {
            amount = _bill_Sys_Menu.GetICcodeAmount(id);
            //txtAmount.Text = Convert.ToDecimal(amount * Convert.ToDecimal(txtUnit.Text)).ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindIC9CodeControl(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        ArrayList _arrayList;
        _bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            tblServices.Visible = true;
            _arrayList = new ArrayList();
            _arrayList = _bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id);
            if (_arrayList.Count > 0)
            {
                txtTransDetailID.Text = _arrayList[0].ToString();
                extddlDiagnosisCode.Text = _arrayList[1].ToString();
                txtBillNo.Text = _arrayList[2].ToString();
                //txtUnit.Text = _arrayList[3].ToString();
                //txtAmount.Text = _arrayList[4].ToString();
                //txtDescription.Text = _arrayList[6].ToString();
                //txtTempAmt.Value = _bill_Sys_Menu.GetICcodeAmount(extddlDiagnosisCode.Text.ToString()).ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void UpdateTransactionDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ArrayList _arrayList;
        try
        {
            _arrayList = new ArrayList();
            for (int i = 0; i < lstProcedureCode.Items.Count;i++ )
            {
                if (lstProcedureCode.Items[i].Selected)
                {
                    if (txtTransDetailID.Text != "")
                    {
                        _arrayList.Add(txtTransDetailID.Text);
                        _arrayList.Add(extddlDiagnosisCode.Text);
                        _arrayList.Add(lstProcedureCode.Items[i].Value.Substring(lstProcedureCode.Items[i].Value.IndexOf("|") + 1, (lstProcedureCode.Items[i].Value.Length - lstProcedureCode.Items[i].Value.IndexOf("|")) - 1));
                        txtAmount.Text = _bill_Sys_BillTransaction.GetProcAmount(lstProcedureCode.Items[i].Value.Substring(lstProcedureCode.Items[i].Value.IndexOf("|") + 1, (lstProcedureCode.Items[i].Value.Length - lstProcedureCode.Items[i].Value.IndexOf("|")) - 1), txtCaseID.Text, txtCompanyID.Text).ToString();
                        _arrayList.Add(txtAmount.Text);
                        _arrayList.Add(txtBillNo.Text);
                        _arrayList.Add(txtDateOfservice.Text);
                        _arrayList.Add(txtUnit.Text);
                        _bill_Sys_BillTransaction.UpdateTransactionData(_arrayList);
                        ClearControl();
                        lblMsg.Visible = true;
                        lblMsg.Text = "Service Updated Successfully...! ";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

            lstProcedureCode.SelectionMode = ListSelectionMode.Multiple;
            extddlDiagnosisCode.Text = "NA";
            //extddlProcedureCode.Text = "NA";          
            //lstAmount.Items.Clear();
            txtUnit.Text = "";
            txtDateOfservice.Text = "";
            txtDateOfServiceTo.Text = "";
            lstProcedureCode.Items.Clear();
            btnUpdateService.Enabled = false;
            btnAddService.Enabled = true;
            //btnFromDate.Visible = true;
            lblMsg.Visible = false;
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

    private void SaveTransactionData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList _arrayList;
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            if (txtDateOfServiceTo.Visible == true)
            {
                System.TimeSpan diffResult = Convert.ToDateTime(txtDateOfServiceTo.Text).Subtract(Convert.ToDateTime(txtDateOfservice.Text));
                int datecount = diffResult.Days;
                DateTime dtdate = Convert.ToDateTime(txtDateOfservice.Text);
                for (int i = 0; i <= datecount; i++)
                {
                    for (int j = 0; j < lstProcedureCode.Items.Count; j++)
                    {
                        if (lstProcedureCode.Items[j].Selected)
                        {
                                _arrayList = new ArrayList();
                                _arrayList.Add(extddlDiagnosisCode.Text);
                                _arrayList.Add(lstProcedureCode.Items[j].Value.Substring(lstProcedureCode.Items[j].Value.IndexOf("|") + 1, (lstProcedureCode.Items[j].Value.Length - lstProcedureCode.Items[j].Value.IndexOf("|")) - 1));
                                txtAmount.Text = _bill_Sys_BillTransaction.GetProcAmount(lstProcedureCode.Items[j].Value.Substring(lstProcedureCode.Items[j].Value.IndexOf("|") + 1, (lstProcedureCode.Items[j].Value.Length - lstProcedureCode.Items[j].Value.IndexOf("|")) - 1), txtCaseID.Text, txtCompanyID.Text).ToString();
                                _arrayList.Add(txtAmount.Text);
                                _arrayList.Add(txtBillNo.Text);
                                _arrayList.Add(dtdate);
                                _arrayList.Add(txtCompanyID.Text);
                                _arrayList.Add(txtUnit.Text);
                                _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
                               
                            
                        }
                    }
                    dtdate = dtdate.AddDays(1);
                }
            }
            else if (txtDateOfServiceTo.Visible == false)
            {
                for (int cnt = 0; cnt < lstProcedureCode.Items.Count; cnt++)
                {
                    if (lstProcedureCode.Items[cnt].Selected)
                    {
                        _arrayList = new ArrayList();
                        _arrayList.Add(extddlDiagnosisCode.Text);
                        _arrayList.Add(lstProcedureCode.Items[cnt].Value.Substring(lstProcedureCode.Items[cnt].Value.IndexOf("|") + 1, (lstProcedureCode.Items[cnt].Value.Length - lstProcedureCode.Items[cnt].Value.IndexOf("|")) - 1));
                        txtAmount.Text = _bill_Sys_BillTransaction.GetProcAmount(lstProcedureCode.Items[cnt].Value.Substring(lstProcedureCode.Items[cnt].Value.IndexOf("|") + 1, (lstProcedureCode.Items[cnt].Value.Length - lstProcedureCode.Items[cnt].Value.IndexOf("|")) - 1), txtCaseID.Text, txtCompanyID.Text).ToString();
                        _arrayList.Add(txtAmount.Text);
                        _arrayList.Add(txtBillNo.Text);
                        _arrayList.Add(txtDateOfservice.Text);
                        _arrayList.Add(txtCompanyID.Text);
                        _arrayList.Add(txtUnit.Text);
                        _bill_Sys_BillTransaction.SaveTransactionData(_arrayList);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
       
    #endregion

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try 
        {
            extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
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
  
    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lstProcedureCode.SelectionMode = ListSelectionMode.Single;
            txtTransDetailID.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[1].Text;
            txtDateOfservice.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[3].Text;
            extddlDiagnosisCode.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[4].Text;
            
            //BindProceduralAmount();
           // extddlProcedureCode.Flag_ID = extddlDiagnosisCode.Text;
            BindProcedureList(extddlDiagnosisCode.Text);
            lstProcedureCode.SelectedValue = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[4].Text + "|" + grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[6].Text;
            

            
            //extddlProcedureCode.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[6].Text;
            txtAmount.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[8].Text;
            txtUnit.Text = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[9].Text;
            //lstAmount.SelectedValue = grdTransactionDetails.Items[grdTransactionDetails.SelectedIndex].Cells[7].Text;
            btnUpdateService.Enabled = true;
            btnAddService.Enabled = false;
            //btnFromDate.Visible = false;
            lblMsg.Visible = false;
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
    
    protected void btnOn_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtDateOfServiceTo.Visible = false;
            lblDateOfService.Visible = false;
            lblTo.Visible = false;
            imgbtnFromTo.Visible = false;
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

    protected void btnFromTo_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtDateOfServiceTo.Visible = true;
            lblDateOfService.Visible = true;
            lblTo.Visible = true;
            imgbtnFromTo.Visible = true;
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

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            
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
    
    private void BindProcedureList(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lstProcedureCode.DataSource = _bill_Sys_BillTransaction.GetProcedureCodeList(id);
            lstProcedureCode.DataTextField = "DESCRIPTION";
            lstProcedureCode.DataValueField = "CODE";
            lstProcedureCode.DataBind();
            if (lstProcedureCode.Items.Count > 0)
            {
                lstProcedureCode.Items[0].Selected = true;
            }
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnForceEntry_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            tblServices.Visible = false;
            BindTransactionDetailsGrid(txtBillNo.Text);
            grdTransactionDetails.Visible = true;
            extddlDoctor.Enabled = true;
            extddlDiagnosisCode.Enabled = true;
            lstProcedureCode.Enabled = true;
            btnAddService.Enabled = true;
            btnClear.Enabled = true;
            btnClearService.Enabled = true;
            Session["AssociateDiagnosis"] = false;
            grdAssociatedDiagnosisCode.Visible = false;
        }
        catch(Exception ex)
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

    private void BindAssociateGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            grdAssociatedDiagnosisCode.DataSource = _bill_Sys_ProcedureCode_BO.GetAssociatedDiagnosisCode_List(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID).Tables[0];
            grdAssociatedDiagnosisCode.DataBind();

            if (grdAssociatedDiagnosisCode.Items.Count > 1)
            {
                grdAssociatedDiagnosisCode.Visible = true;
                extddlDoctor.Enabled = false;
                btnForceEntry.Visible = true;
            }
            else
            {
                SetDataOfAssociate(grdAssociatedDiagnosisCode.Items[0].Cells[1].Text);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SetDataOfAssociate(string setID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet dataset = new DataSet();
            dataset = _bill_Sys_BillTransaction.GetAssociatedEntry(setID);

            //string _casetype = "";
            //_bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            //_casetype=_bill_Sys_BillTransaction.GetCaseType(Session["PassedCaseID"].ToString());

            
            for (int i=0;i<=dataset.Tables[0].Rows.Count-1;i++)
            {
                extddlDoctor.Text = dataset.Tables[0].Rows[i][1].ToString();
                extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
                

                    extddlDiagnosisCode.Text = dataset.Tables[0].Rows[i][2].ToString();
                    BindProcedureList(extddlDiagnosisCode.Text);
                    foreach (ListItem l in lstProcedureCode.Items)
                    {
                        if (l.Value == dataset.Tables[0].Rows[i][3].ToString()) { l.Selected = true; } else { l.Selected = false; }
                    }
               
            }

            btnForceEntry.Visible = true;
            tblServices.Visible = true;
            BindTransactionDetailsGrid(txtBillNo.Text);
            grdTransactionDetails.Visible = true;
            extddlDoctor.Enabled = false;
            extddlDiagnosisCode.Enabled = false;
            lstProcedureCode.Enabled = false;
            btnAddService.Enabled = false;
            btnClear.Enabled = false;
            btnClearService.Enabled = false;
            Session["AssociateDiagnosis"] = true;
        }
        catch(Exception ex)
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

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            BindTransactionDetailsGrid(txtBillNo.Text);
        }
        catch(Exception ex)
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

    protected void grdAssociatedDiagnosisCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SetDataOfAssociate(grdAssociatedDiagnosisCode.SelectedItem.Cells[1].Text);
            grdAssociatedDiagnosisCode.Visible = false;
        }
        catch(Exception ex)
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

    protected void ddlDiagnosisCodeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //if (ddlDiagnosisCodeType.SelectedValue == "0")
            //{
            //    extddlDiagnosisCode.Procedure_Name = "SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE";
            //    extddlDiagnosisCode.Flag_Key_Value = "GET_PT_DIAGNOSIS_CODE";
            //    extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
            //    extddlDiagnosisCode.Text = dataset.Tables[0].Rows[i][2].ToString();
            //        BindProcedureList(extddlDiagnosisCode.Text);
            //        foreach (ListItem l in lstProcedureCode.Items)
            //        {
            //            if (l.Value == dataset.Tables[0].Rows[i][3].ToString()) { l.Selected = true; } else { l.Selected = false; }
            //        }
                
            //}
            //else if (ddlDiagnosisCodeType.SelectedValue == "1")
            //{
            //    extddlDiagnosisCode.Procedure_Name = "SP_TXN_CASE_ASSOCIATED_DIAGNOSIS_CODE";
            //    extddlDiagnosisCode.Flag_Key_Value = "GET_EVALUATION_DIAGNOSIS_CODE";
            //    extddlDiagnosisCode.Flag_ID = extddlDoctor.Text;
               
            //        extddlDiagnosisCode.Text = dataset.Tables[0].Rows[i][2].ToString();
            //        BindProcedureList(extddlDiagnosisCode.Text);
            //        foreach (ListItem l in lstProcedureCode.Items)
            //        {
            //            if (l.Value == dataset.Tables[0].Rows[i][3].ToString()) { l.Selected = true; } else { l.Selected = false; }
            //        }
                
            //}
            //else
            //{

            //}
        }
        catch(Exception ex)
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

    protected void rdoFromTo_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtDateOfServiceTo.Visible = true;
            lblDateOfService.Visible = true;
            lblTo.Visible = true;
            imgbtnFromTo.Visible = true;
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

    protected void rdoOn_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtDateOfServiceTo.Visible = false;
            lblDateOfService.Visible = false;
            lblTo.Visible = false;
            imgbtnFromTo.Visible = false;
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

    protected void lnlProgessReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_2.aspx'); ", true);
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
    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        Session["TEMPLATE_BILL_NO"] = Session["BillID"].ToString();
        Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformation.aspx'); ", true);
    }
}

