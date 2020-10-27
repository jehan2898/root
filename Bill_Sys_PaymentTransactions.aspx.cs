/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_PaymentTransactions.aspx.cs
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
using System.IO;
using RequiredDocuments;

public partial class Bill_Sys_PaymentTransactions : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    Bill_Sys_BillTransaction_BO _obj;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //txtChequeNumber.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
      
           btnSave.Attributes.Add("onclick", "return ooValidate();");
            
           btnUpdate.Attributes.Add("onclick", "return formValidator('frmPaymentTrans','txtChequeNumber,txtChequeDate,txtChequeAmount,txtPaymentType');");            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnLitigation.Attributes.Add("onclick", "return AddComment();");
            btnWriteoff.Attributes.Add("onclick", "return AddComment();");
            //billsearch ajax page
            if (Request.QueryString["csid"] != null)
            {
                Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                string szCaseId = Request.QueryString["csid"];
               
                string szBollNo = Request.QueryString["bno"];
              

                string szBalance = Request.QueryString["bal"];
              
                _bill_Sys_Case.SZ_CASE_ID = szCaseId;
                CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(_bill_Sys_Case.SZ_CASE_ID, "");
                _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, _bill_Sys_CaseObject.SZ_COMAPNY_ID); 
                Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                Session["CASEINFO"] = _bill_Sys_Case;
                Session["PassedCaseID"] = szCaseId;
                Session["SZ_BILL_NUMBER"] = szBollNo;

                Session["PassedBillID"] = szBollNo;
                Session["Balance"] = szBalance;
            }

            txtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            txtBillNo.Text =Session["PassedBillID"].ToString();
           

            if (!IsPostBack)
            {
                lblCaseNo.Text = txtBillNo.Text;
                txtBalance.Text=Session["Balance"].ToString();
                lblPosteddate.Text = DateTime.Today.ToShortDateString();
                BindGrid();
                getVisitDate();
                btnUpdate.Enabled = false;
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
            cv.MakeReadOnlyPage("Bill_Sys_PaymentTransactions.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void getVisitDate()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillTransaction_BO _objBT = new Bill_Sys_BillTransaction_BO();
            lblVisitDate.Text = _objBT.GetBillVisitDate(txtBillNo.Text, txtCompanyID.Text);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

        try
        {
            decimal _balance = Convert.ToDecimal(_bill_Sys_BillingCompanyDetails_BO.GetBalance(txtBillNo.Text));
            if (rdbList.SelectedValue.ToString() == "1")//Litigation
            {  _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO ();
                txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "LT");
            }
            if (rdbList.SelectedValue.ToString()== "2")//Write-Off
            {
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "WOF");
            }
            if (rdbList.SelectedValue.ToString() == "0")//save
            {
                if (_balance > Convert.ToDecimal(txtChequeAmount.Text))
                {
                    //_bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                    //txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "PBP");
                  

                 }
                else
                 {
                     _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                     txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "FBP");
           
                }
            }
            
           
            //if (_balance >= Convert.ToDecimal(txtChequeAmount.Text))
            //{ 
                txtBalance.Visible = false;
                txtPostBalance.Text = Convert.ToString((_balance - Convert.ToDecimal(txtChequeAmount.Text)));
                txtPostBalance.Visible = true;
                txthdcheckamount.Text = "";
               // tdLitti_Write.Visible = true;
               // tdAddUpdate.Visible = false;
                txtPaymentType.Text = rdbList.SelectedValue.ToString();// j00mla1209
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "PaymentTransaction.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                
                //ErrorDivServer.InnerText = "Payment saved successfully.";
                

                if (txtPaymentType.Text == "1")
                {
                   // Response.Redirect("Bill_Sys_LitigationDesk.aspx", false);
                }

                if (txtPaymentType.Text == "2")
                {
                    //Response.Redirect("Bill_Sys_WriteOffDesk.aspx", false);
                }
                ClearControl();
                usrMessage.PutMessage("Payment saved successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            //}
            //else if (_balance < Convert.ToDecimal(txtChequeAmount.Text))
            //{
            //    txtBalance.Text = _balance.ToString();
            //    ErrorDivServer.InnerText = "Please enter amount less than or equal to balance";
           // }
           // else
           // {
            //    _saveOperation.WebPage = this.Page;
            //    _saveOperation.Xml_File = "PaymentTransaction.xml";
            //    _saveOperation.SaveMethod();
            //    //BindGrid();
            //   // if (Convert.ToInt32(Request.Form["hiddenconfirmBox"].ToString()) == 1)
            //   // {
            //      //  _bill_Sys_BillingCompanyDetails_BO.UpdateBillTransaction(txtBillNo.Text, Convert.ToInt32(Request.Form["hiddenconfirmBox"].ToString()), txtComment.Text.ToString());
            //    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            //   // }
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        lblMsg.Visible = false;
        try
        {
            decimal _balance = Convert.ToDecimal(_bill_Sys_BillingCompanyDetails_BO.GetBalance(txtBillNo.Text));
            decimal _newBalance = _balance + Convert.ToDecimal(lblPrevAmount.Text);
           // if (_newBalance >= Convert.ToDecimal(txtChequeAmount.Text))



          //  {


            if (rdbList.SelectedValue.ToString() == "1")//Litigation
            {
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "LT");
   
            }
            if (rdbList.SelectedValue.ToString() == "2")//Write-Off
            {
                _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "WOF");
            } 
            if (rdbList.SelectedValue.ToString() == "0")//save
            {
                if (_newBalance > Convert.ToDecimal(txtChequeAmount.Text))
                {
                   

                }
                else
                {
                    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                    txtBillStatusId.Text = _bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "FBP");

                }   
            }
            
            
            
            
            _editOperation.Primary_Value = Session["PaymentID"].ToString();
                txtPaymentType.Text = rdbList.SelectedValue.ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "PaymentTransaction.xml";
                _editOperation.UpdateMethod();
                BindGrid();

                if (txtPaymentType.Text == "1")
                {
                //    Response.Redirect("Bill_Sys_LitigationDesk.aspx", false);
                }

                if (txtPaymentType.Text == "2")
                {
                //    Response.Redirect("Bill_Sys_WriteOffDesk.aspx", false);
                }
                Bill_Sys_BillTransaction_BO _objBT = new Bill_Sys_BillTransaction_BO();
                txtPostBalance.Text = _bill_Sys_BillingCompanyDetails_BO.GetBalance(txtBillNo.Text).ToString();
                txtPostBalance.Visible = true;
                txtBalance.Visible = false;
                ClearControl();
                //ErrorDivServer.InnerText = "Payment updated successfully.";
                usrMessage.PutMessage("Payment updated successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
          //  }
           // else
           // {
          //      ErrorDivServer.InnerText = " Please enter amount less than or equal to balance";
          //  }
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

    private void BindGrid()
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
            _listOperation.Xml_File = "PaymentTransaction.xml";
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

    protected void grdPaymentTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["PaymentID"] = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[1].Text;
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[2].Text != "&nbsp;") { txtChequeNumber.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[2].Text; }
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[3].Text != "&nbsp;") { lblPosteddate.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[3].Text; }
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[4].Text != "&nbsp;") { txtChequeDate.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[4].Text; }
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[5].Text != "&nbsp;") { txtChequeAmount.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[5].Text; txthdcheckamount.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[5].Text; }
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[6].Text != "&nbsp;") { txtComment.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[6].Text; } else { txtComment.Text = ""; }
            if (grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[7].Text != "&nbsp;") { txtPaymentType.Text = grdPaymentTransaction.Items[grdPaymentTransaction.SelectedIndex].Cells[7].Text; } else { txtPaymentType.Text = "0"; }
            if (txtPaymentType.Text == "1")
            {
                rdbList.SelectedValue = "1";
            }
            else if (txtPaymentType.Text == "2")
            {
                rdbList.SelectedValue = "2";
            }
            else
            {
                rdbList.SelectedValue = "0";
            }
            lblPrevAmount.Text = txtChequeAmount.Text;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfPayment = "";
        try
        {
            for (int i = 0; i < grdPaymentTransaction.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdPaymentTransaction.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_TXN_PAYMENT_TRANSACTIONS", "@SZ_PAYMENT_ID", grdPaymentTransaction.Items[i].Cells[1].Text))
                    {
                        if (szListOfPayment == "")
                        {
                            szListOfPayment = grdPaymentTransaction.Items[i].Cells[2].Text + " " + grdPaymentTransaction.Items[i].Cells[4].Text;
                        }
                        else
                        {
                            szListOfPayment = "," + grdPaymentTransaction.Items[i].Cells[2].Text + " " + grdPaymentTransaction.Items[i].Cells[4].Text;
                        }
                    }
                }
            }
            if (szListOfPayment != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for payment " + szListOfPayment + "  exists.'); ", true);
            }
            else
            {
                usrMessage.PutMessage("Payment deleted successfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                //lblMsg.Visible = true;
                //lblMsg.Text = "Payment deleted successfully ...";
            }
            BindGrid();
            _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            decimal _balance = Convert.ToDecimal(_bill_Sys_BillingCompanyDetails_BO.GetBalance(txtBillNo.Text));
            txtBalance.Text = _balance.ToString();
            txtPostBalance.Text = _balance.ToString();
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

    protected void grdPaymentTransaction_DeleteCommand(object source, DataGridCommandEventArgs e)
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

    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtChequeNumber.Text = "";
            txtChequeDate.Text = "";
            txtChequeAmount.Text = "";
            txtComment.Text = "";
            lblPosteddate.Text = DateTime.Today.ToShortDateString();
            btnSave.Enabled = true;
            rdbList.SelectedValue = "0";
            btnUpdate.Enabled = false;
            //ErrorDivServer.InnerText = "";
            
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

    protected void grdPaymentTransaction_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdPaymentTransaction.CurrentPageIndex = e.NewPageIndex;
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

    protected void grdPaymentTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        //try
        //{

        //    if (e.CommandName.ToString() == "Add IC9 Code")
        //    {
        //        Session["PassedBillID"] = e.CommandArgument;
        //        Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    string strError = ex.Message.ToString();
        //    strError = strError.Replace("\n", " ");
        //    Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        //}
    }

    protected void btnLitigation_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {
           // Response.Write("<script language='javascript'>alert('Do u want to add comments ..!')</script>");
            txtPaymentType.Text = "1";

            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "PaymentTransaction.xml";
            _saveOperation.SaveMethod();

            Response.Redirect("Bill_Sys_LitigationDesk.aspx", false);
            //txtComment.Visible = true;
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

    protected void btnWriteoff_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtPaymentType.Text = "2";
            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "PaymentTransaction.xml";
            _saveOperation.SaveMethod();

            Response.Redirect("Bill_Sys_WriteOffDesk.aspx", false);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_BillSearch.aspx?fromCase=true", false);
            //txtPaymentType.Text = "3";
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

    protected void txtChequeAmount_TextChanged1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
             _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
             decimal _balance = _bill_Sys_BillingCompanyDetails_BO.GetBalance(txtBillNo.Text);
            if (_balance > Convert.ToDecimal(txtChequeAmount.Text))
            {
                tdLitti_Write.Visible = true;
                tdAddUpdate.Visible = false;
            }
            else if (_balance < Convert.ToDecimal(txtChequeAmount.Text)) 
            {
                txtBalance.Text = _balance.ToString();
                //ErrorDivServer.InnerText = "Please enter amount less than or equal to balance";
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

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int iindex = grdPaymentTransaction.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;
        Session["scanpayment_No"] = it.Cells[1].Text;
        RedirectToScanApp(iindex);
    }

    public void RedirectToScanApp(int iindex)
    {
        iindex = (int)grdPaymentTransaction.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        _obj = new Bill_Sys_BillTransaction_BO();
        //String NodeId = _obj.GetNodeID(txtCompanyID.Text, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString(), "NFPAY");
        String NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPAY");
        szUrl = szUrl + "&Flag=ReqPay" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CaseId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&CaseNo=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + NodeId;
        szUrl = szUrl + "&PatientId=" + Session["scanpayment_No"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&BillNo=" + txtBillNo.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }
    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;        
        Session["payment_No"] = it.Cells[1].Text;
       Page.RegisterStartupScript("ss", "<script language='javascript'>showUploadFilePopup();</script>");
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        string strLinkPath = "";
        try
        {
            if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                return;
            }
            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            String szDestinationDir = "";

            szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            _obj = new Bill_Sys_BillTransaction_BO();
            Bill_Sys_RequiredDocumentBO bo = new Bill_Sys_RequiredDocumentBO();
            String NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPAY");
            string szNodePath = bo.GetNodePath(NodeId, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            szNodePath = szNodePath.Replace("\\", "/");
            strLinkPath =  szNodePath + "/" + fuUploadReport.FileName;
            if (!Directory.Exists(szDefaultPath + szNodePath + "/"))
            {
                Directory.CreateDirectory(szDefaultPath +  szNodePath + "/" );
            }
            //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
            //{
            fuUploadReport.SaveAs(szDefaultPath +  szNodePath + "/" + fuUploadReport.FileName);
            // Start : Save report under document manager.
         
            //String NodeId = _obj.GetNodeID(txtCompanyID.Text, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString(), "NFPAY");
            ArrayList objAL = new ArrayList();

            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            objAL.Add(fuUploadReport.FileName);
            objAL.Add(szNodePath + "/");
            objAL.Add(NodeId);
            objAL.Add("");
            objNF3Template.UpdateDocMgr(objAL);
            // End :   Save report under document manager.
            //}
            // Code To get Image Id Of Saved Record  
            ArrayList objALImage = new ArrayList();
            objALImage.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objALImage.Add("");
            objALImage.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objALImage.Add(fuUploadReport.FileName);
            objALImage.Add(szNodePath + "/");
            objALImage.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
            objALImage.Add(NodeId);
            string ImgId = objNF3Template.SaveDocumentData(objALImage);
            // End of Code

            //Function To Save Entry In Txn_Bil_Payment_Images Table
            ArrayList objarrtxnbill = new ArrayList();
            objarrtxnbill.Add(txtBillNo.Text);
            objarrtxnbill.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            objarrtxnbill.Add(ImgId);
            objarrtxnbill.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
            objarrtxnbill.Add(Session["payment_No"].ToString());
            _bill_Sys_BillingCompanyDetails_BO.InsertBillPaymentImages(objarrtxnbill);
            //End Of  Function 
            BindGrid();
            usrMessage.PutMessage("File Upload Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            //lblMsg.Text = "File Upload Successfully";
            //lblMsg.Visible = true;
        }
        //Page.RegisterStartupScript("ss", "<script language = 'javascript'>alert('Report received successfully.');</script>");


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

