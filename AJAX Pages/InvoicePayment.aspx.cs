using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_InvoicePayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnempId.Value = Request.QueryString["empid"].ToString();
            hdnemployer.Value = Request.QueryString["empname"].ToString();
            hdninvoicenumber.Value = Request.QueryString["invoiceid"].ToString();
            hdninvoiceamount.Value =  Convert.ToDouble(Request.QueryString["amt"].ToString()).ToString("C");
            hdnbal.Value = Convert.ToDouble(Request.QueryString["bal"].ToString()).ToString("C");
            lblemp.Text = hdnemployer.Value;
            this.btnSavesent.Attributes.Add("onclick", "return ooValidate();");
            this.btnUpdate.Attributes.Add("onclick", "return ooValidate();");
            this.btndelete.Attributes.Add("onclick", "return confirm_bill_delete();");

            lblInvoiceNumber.Text = hdninvoicenumber.Value;
            lblAmount.Text = hdninvoiceamount.Value;
            lblBalance.Text = hdnbal.Value;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindPayment();
            btnSavesent.Enabled = true;
            btnUpdate.Enabled = false;
            txtPaymentID.Text = "";
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdMakeInvoicePayment.Items.Count; i++)
        {
            string icount = grdMakeInvoicePayment.Items[i].Cells[13].Text.ToString();
            HtmlAnchor a = (HtmlAnchor)grdMakeInvoicePayment.Items[i].FindControl("lnkView");
            if (icount != "0")
            {
                a.Visible = true;
            }
            else
            {
                a.Visible = false;
            }

        }
    }
    protected void btnSavesent_Click(object sender, EventArgs e)
    {
        try
        {
                Employer objEmployer = new Employer();
            ArrayList arrData = new ArrayList();
            arrData.Add(txtCompanyID.Text);
            arrData.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
            arrData.Add(txtCheckAmount.Text);
            if (chkwirteoff.Checked)
            {
                arrData.Add("W");
            }
            else
            {
                arrData.Add("S");
            }
            arrData.Add(hdninvoicenumber.Value);
            arrData.Add(txtDate.Text);
            arrData.Add(hdnempId.Value);
            arrData.Add(txtCheckNo.Text);
            arrData.Add(txtNotes.Text);
           int iReturn= objEmployer.SavePayment(arrData);
            if (iReturn != 0)
            {

                this.usrMessage.PutMessage("Payment saved successfully ...");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {

                this.usrMessage.PutMessage("Error to save payment ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            BindPayment();
            clear();
            btnSavesent.Enabled = true;
            btnUpdate.Enabled = false;

        }
        catch (Exception)
        {


        }
    }

    public void BindPayment()
    {
        try
        {
            Employer objEmployer = new Employer();
            DataSet ds = objEmployer.GetPayment(hdninvoicenumber.Value, txtCompanyID.Text);
            grdMakeInvoicePayment.DataSource = ds;
            grdMakeInvoicePayment.DataBind();
            if (ds.Tables.Count > 1)
            {
                lblBalance.Text = Convert.ToDouble(ds.Tables[1].Rows[0][0].ToString()).ToString("C");
            }
            else
            {
                lblBalance.Text = "0.0";
            }
            btnSavesent.Enabled = true;
            btnUpdate.Enabled = false;
        }
        catch (Exception EX)
        {


        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList arrList = new ArrayList();
            for (int i = 0; i < grdMakeInvoicePayment.Items.Count; i++)
            {
             CheckBox chk= (CheckBox)  grdMakeInvoicePayment.Items[i].FindControl("chkRemove");
                if(chk.Checked)
                {
                    string Id = grdMakeInvoicePayment.Items[i].Cells[9].Text;
                    arrList.Add(Id);


                }

            }
            Employer objEmployer = new Employer();
          int iReturn=  objEmployer.DeletePayment(arrList, txtCompanyID.Text, hdninvoicenumber.Value);
            if (iReturn !=0)
            {

                this.usrMessage.PutMessage("Payment deleted successfully ...");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {

                this.usrMessage.PutMessage("Error to delete payment ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            BindPayment();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void grdMakeInvoicePayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                txtPaymentID.Text = e.Item.Cells[9].Text;
                txtCheckAmount.Text = e.Item.Cells[3].Text.Replace("$","").Trim();
                txtCheckNo.Text = e.Item.Cells[5].Text;
                txtDate.Text = e.Item.Cells[4].Text;
                txtNotes.Text = e.Item.Cells[10].Text;
                lblemp.Text = e.Item.Cells[1].Text;
                lblInvoiceNumber.Text = e.Item.Cells[2].Text;
                btnSavesent.Enabled = false;
                btnUpdate.Enabled = true;
            }   
        }catch(Exception ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Employer objEmployer = new Employer();
        ArrayList arrData = new ArrayList();
        arrData.Add(txtCompanyID.Text);
        arrData.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
        arrData.Add(txtCheckAmount.Text);
        if (chkwirteoff.Checked)
        {
            arrData.Add("W");
        }
        else
        {
            arrData.Add("S");
        }
        arrData.Add(hdninvoicenumber.Value);
        arrData.Add(txtDate.Text);
        arrData.Add(hdnempId.Value);
        arrData.Add(txtCheckNo.Text);
        arrData.Add(txtNotes.Text);
        arrData.Add(txtPaymentID.Text);
      int iReturn=  objEmployer.Update(arrData);
        if (iReturn != 0)
        {

            this.usrMessage.PutMessage("Payment updated successfully ...");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
        }
        else
        {

            this.usrMessage.PutMessage("Error to updated payment ");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage.Show();
        }
        BindPayment();
        clear();
        txtPaymentID.Text = "";

    }
    public void clear()
    {

        txtPaymentID.Text = "";
        txtCheckAmount.Text = "";
        txtCheckNo.Text = "";
        txtDate.Text = "";
        txtNotes.Text ="";
    }
}