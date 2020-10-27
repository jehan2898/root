using log4net;
using SautinSoft;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;


public partial class AJAX_Pages_Make_Invoice_Payment : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!base.IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID; 
                lblinvoicenumber.Text = Request.QueryString["id"].ToString();
                hdninvoicenumber.Value = Request.QueryString["id"].ToString();
                if (Request.QueryString["bal"].ToString() != "" && Request.QueryString["bal"].ToString() != "&nbsp;")
                {
                lblbal.Text = Convert.ToDecimal(Request.QueryString["bal"].ToString()).ToString("F"); 
                }
                hdnbal.Value = Request.QueryString["bal"].ToString();
                lblemp.Text = Request.QueryString["empname"].ToString();
                hdnemployer.Value = Request.QueryString["empname"].ToString();
                if (Request.QueryString["amount"].ToString() != "" && Request.QueryString["amount"].ToString() != "&nbsp;")
                {
                    lblinvoiceamount.Text = Convert.ToDecimal(Request.QueryString["amount"].ToString()).ToString("F");
                }
                 hdninvoiceamount.Value = Request.QueryString["amount"].ToString();
                hdnempId.Value = Request.QueryString["empid"].ToString();
                btnSavesent.Attributes.Add("onclick", "return ooValidate();");
                btnUpdate.Attributes.Add("onclick", "return ooValidate();");
                btndelete.Attributes.Add("onclick", "return confirm_bill_delete();");
                btnUpdate.Enabled = false;
                btnSavesent.Enabled = true;
                BindPayment();

            }
        }
        catch
        {

        }
    }

    public void BindPayment()
    {
        Employer obj = new Employer();
        DataSet ds = new DataSet();
        ds = obj.GetInvoicePayment(hdninvoicenumber.Value, txtCompanyID.Text);
        grdMakeInvoicePayment.DataSource =ds;
        grdMakeInvoicePayment.DataBind();
    }
    public void btn_Save()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {


            Employer obj = new Employer();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(hdninvoicenumber.Value);
            arrayLists.Add(hdnempId.Value);
            arrayLists.Add(txtChequeNo.Text);
            arrayLists.Add(txtDate.Text);
            arrayLists.Add(txtAmount.Text);
            arrayLists.Add(txtCompanyID.Text);
            arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            arrayLists.Add("1");
            arrayLists.Add("Add");
            arrayLists.Add("");
            arrayLists.Add(txtNotes.Text);
            DataSet ds = obj.saveInvoicePayment(arrayLists);
            BindPayment();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "" && ds.Tables[0].Rows[0][0].ToString() != "&nbsp;")
                {
                    lblbal.Text = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()).ToString("F");
                }
                hdnbal.Value = ds.Tables[0].Rows[0][0].ToString();
                this.usrMessage.PutMessage("Payment saved successfully");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {

                this.usrMessage.PutMessage("Error to save payment");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
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
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


    }
    public void btn_update()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(),HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Employer obj = new Employer();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(hdninvoicenumber.Value);
            arrayLists.Add(hdnempId.Value);
            arrayLists.Add(txtChequeNo.Text);
            arrayLists.Add(txtDate.Text);
            arrayLists.Add(txtAmount.Text);
            arrayLists.Add(txtCompanyID.Text);
            arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            arrayLists.Add("1");
            arrayLists.Add("Update");
            arrayLists.Add(txtPaymentID.Text);
            arrayLists.Add(txtNotes.Text);
            DataSet ds = obj.saveInvoicePayment(arrayLists);
            BindPayment();

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0][0].ToString() !="" && ds.Tables[0].Rows[0][0].ToString() !="&nbsp;")
                {
                lblbal.Text = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()).ToString("F");
                }
                hdnbal.Value = ds.Tables[0].Rows[0][0].ToString();
                this.usrMessage.PutMessage("Payment updated successfully");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {

                this.usrMessage.PutMessage("Error to updated payment");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
                txtChequeNo.Text="";
                txtDate.Text="";
                txtAmount.Text="";
                txtPaymentID.Text="";
                txtNotes.Text = "";
                btnUpdate.Enabled = false;
                btnSavesent.Enabled = true;
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
    protected void btnSavesent_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btn_Save();
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Logging Start
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < grdMakeInvoicePayment.Items.Count; i++)
            {
                CheckBox box = (CheckBox)grdMakeInvoicePayment.Items[i].FindControl("chkRemove");
                if (box.Checked)
                {
                    ArrayList arrList = new ArrayList();
                    string id = grdMakeInvoicePayment.Items[i].Cells[7].Text.ToString();
                    string invid = grdMakeInvoicePayment.Items[i].Cells[2].Text.ToString();
                    arrList.Add(id);
                    arrList.Add(invid);
                    arrList.Add(txtCompanyID.Text);
                    Employer obj = new Employer();
                    DataSet ds = obj.DeleteInvoicePayment(arrList);
                    

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0][0].ToString() != "" && ds.Tables[0].Rows[0][0].ToString() != "&nbsp;")
                        {
                            lblbal.Text = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()).ToString("F");
                        }
                        hdnbal.Value = ds.Tables[0].Rows[0][0].ToString();
                        this.usrMessage.PutMessage("Payment deleted successfully");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        this.usrMessage.Show();
                    }
                    else
                    {

                        this.usrMessage.PutMessage("Error to save payment");
                        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        this.usrMessage.Show();
                    }

                }
            }
            BindPayment();

        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btn_update();
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
    protected void grdMakeInvoicePayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           txtPaymentID.Text = e.Item.Cells[7].Text;
           txtChequeNo.Text= e.Item.Cells[5].Text;
           txtDate.Text = e.Item.Cells[4].Text;
           txtAmount.Text = e.Item.Cells[3].Text.Replace("$","");
           if (e.Item.Cells[8].Text != "&nbsp;" && e.Item.Cells[8].Text.Trim() != "")
           {
               txtNotes.Text = e.Item.Cells[8].Text;
           }
           btnUpdate.Enabled = true;
           btnSavesent.Enabled = false;

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