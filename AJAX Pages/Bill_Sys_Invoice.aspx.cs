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
using ExtendedDropDownList;
 
public partial class Bill_Sys_Invoice : PageBase
{
    InvoiceDAO _InvoiceDAO;
    InvoiceGetSet _InvoiceGetSet;    
    ExtendedDropDownList.ExtendedDropDownList ext = new ExtendedDropDownList.ExtendedDropDownList();
    ExtendedDropDownList.ExtendedDropDownList ext1 = new ExtendedDropDownList.ExtendedDropDownList();
    DataSet dsFillGrid;
    DataTable dtFillGrid;
    ArrayList arrStartValues = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
                                 
            lblTodaysDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            extddlOffice.Flag_ID = txtCompanyId.Text;
            if (Page.IsPostBack == false)
            {
                Session["TotalAmount"] = "";
                if (Request.QueryString["InvoiceId"]!="" && Request.QueryString["InvoiceId"]!=null)
                {
                    _InvoiceDAO = new InvoiceDAO();
                    txtInvoiceId.Text = Request.QueryString["InvoiceId"];
                    dtFillGrid = new DataTable();
                    dtFillGrid = _InvoiceDAO.getInvoiceDetails(txtInvoiceId.Text, txtCompanyId.Text, "", "GET_INVOICE_DETAILS");
                    grdInvoice.DataSource = dtFillGrid;
                    grdInvoice.DataBind();
                    lblTAmount.Text = dtFillGrid.Rows[0][10].ToString();
                    lblFinalTotalAmount.Text = dtFillGrid.Rows[0][11].ToString();
                    extddlOffice.Text = dtFillGrid.Rows[0][12].ToString();
                    txtShipping.Text= dtFillGrid.Rows[0][6].ToString();
                    txtServiceDate.Text= dtFillGrid.Rows[0][7].ToString();

                    txtPersonName.Text = dtFillGrid.Rows[0][13].ToString();
                    txtPersonAddress.Text = dtFillGrid.Rows[0][14].ToString();
                    txtCity.Text = dtFillGrid.Rows[0][15].ToString();
                    extddlPatientState.Text = dtFillGrid.Rows[0][16].ToString();
                    txtZip.Text = dtFillGrid.Rows[0][17].ToString(); 
                    GridViewRow row = (GridViewRow)grdInvoice.HeaderRow;
                    ext = (ExtendedDropDownList.ExtendedDropDownList)row.FindControl("extddlItem");
                    ext.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;                   
                    
                    for (int i = 0; i < dtFillGrid.Rows.Count; i++)
                    {
                        ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                        ext1.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        ext1.Text = dtFillGrid.Rows[i][5].ToString();
                        ext1.Enabled=false;
                        arrStartValues.Add(dtFillGrid.Rows[i][9].ToString());
                    }
                    ViewState["ArrayList"] = (ArrayList)arrStartValues;
                    ViewState["Table"] = dtFillGrid;
                    
                    btnSave.Text = "Update";
                    dtFillGrid=_InvoiceDAO.getInvoiceDetails(txtInvoiceId.Text, txtCompanyId.Text, Request.QueryString["CaseId"].ToString(), "GET_PATIENT_INFO");                   
                    lblCaseNo.Text=dtFillGrid.Rows[0][1].ToString();
                    lblPatientName.Text = dtFillGrid.Rows[0][0].ToString();
                    CalculateTotalAmount();
                }                
                else
                {                    
                        FillGrid();
                        lblCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                        lblPatientName.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                }
                //BindgrdLitigationdesk();                                                  
            }
           
            #region "check version readonly or not"
            string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
            if (app_status.Equals("True"))
            {
                Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
                cv.MakeReadOnlyPage("Bill_Sys_Invoice.aspx");
            }
            #endregion
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

    public void CalculateTotalAmount()
     {
        DataTable dt = new DataTable();
        double TotalAmount=0.00;
        dt = (DataTable)ViewState["Table"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TotalAmount = TotalAmount + Convert.ToDouble(dt.Rows[i][4].ToString());
        }
        lblTAmount.Text = TotalAmount.ToString();
        if (txtShipping.Text != "")
        {
            TotalAmount = 0.00;
            TotalAmount = Convert.ToDouble(lblTAmount.Text) + Convert.ToDouble(txtShipping.Text);
            lblFinalTotalAmount.Text = "$" + TotalAmount.ToString();
            if (lblTAmount.Text == "0")
            {
                lblTAmount.Text = '$' + TotalAmount.ToString();
            }
            else 
            {
                lblTAmount.Text = '$' + lblTAmount.Text;
            }
        }
        else 
        {
            lblFinalTotalAmount.Text = "$" + lblTAmount.Text;
            lblTAmount.Text = "$" + lblTAmount.Text;            
        }
        
        hdnTotAmount.Value = lblTAmount.Text;
    }

    public void FillGrid()
    {
         DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                dt.Columns.Add(new DataColumn("Price", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemId", typeof(string)));
                dt.Columns.Add(new DataColumn("Invoice_Detail_Id", typeof(string))); 
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["ItemName"] = string.Empty;
                dr["Quantity"] = string.Empty;
                dr["Price"] = 0.00;
                dr["TotalAmount"] = 0.00;
                dr["ItemId"] = string.Empty;
                dr["Invoice_Detail_Id"] = string.Empty; 
                dt.Rows.Add(dr);               
                ViewState["Table"] = dt;
                grdInvoice.DataSource = dt;
                grdInvoice.DataBind();
                GridViewRow row = (GridViewRow)grdInvoice.HeaderRow;
                ext = (ExtendedDropDownList.ExtendedDropDownList)row.FindControl("extddlItem");
                ext.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                for (int i = 0; i < grdInvoice.Rows.Count; i++)
                {
                    ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                    ext1.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    ext1.Text = dt.Rows[i][5].ToString();
                    ext1.Enabled = false;
                }
    }   
    
    public void BindgrdLitigationdesk()
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void OnextendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            double TotalAmount = 0.00;
            Label  lblPrice,lblTotalAmount;
            TextBox txtQuantity;
            GridViewRow row = (GridViewRow)grdInvoice.HeaderRow;
            lblPrice = (Label)row.FindControl("lblPrice");
            lblTotalAmount=(Label)row.FindControl("lblTotalAmount");
            txtQuantity=(TextBox)row.FindControl("txtQuantity");
            ext = (ExtendedDropDownList.ExtendedDropDownList)row.FindControl("extddlItem");
            _InvoiceDAO = new InvoiceDAO();
            DataSet ds = new DataSet();
            ds = _InvoiceDAO.getInvoiceItemPrice(ext.Text,txtCompanyId.Text);
            txtSelectedItemIndex.Text = ext.Text;
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else 
            {
                lblPrice.Text = "0.00";
            }
            if (txtQuantity.Text != "")
            {
                TotalAmount = Convert.ToDouble(lblPrice.Text) * Convert.ToDouble(txtQuantity.Text);
                lblTotalAmount.Text = TotalAmount.ToString();
                Session["TotalAmount"] = lblTotalAmount.Text;
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
           
    protected void onClick_lnkADD(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (ViewState["Table"] != null)
            {
                DataTable dtTable = (DataTable)ViewState["Table"];
                DataRow drCurrentRow = null;
                GridViewRow grdrow = (GridViewRow)grdInvoice.HeaderRow;
                ext = (ExtendedDropDownList.ExtendedDropDownList)grdrow.FindControl("extddlItem");
                TextBox txtQuantity1 = (TextBox)grdInvoice.HeaderRow.Cells[2].FindControl("txtQuantity");
                Label lblTotalAmount;
                lblTotalAmount = (Label)grdrow.FindControl("lblTotalAmount");
                double TotalAmount;
                string flag = "false";
                   for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i][5].ToString().Equals(ext.Text))
                        {
                            flag = "true";
                        }                       
                    }
                    if (flag == "false")
                    {

                        if (ext.Selected_Text.ToString().Equals("---Select---"))
                        {

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ValidateItem();", true);
                        }
                        else if (txtQuantity1.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ValidateQuantity();", true);
                        }
                        else
                        {
                            if (dtTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    TextBox txtQuantity = (TextBox)grdInvoice.HeaderRow.Cells[2].FindControl("txtQuantity");
                                    Label lblPrice = (Label)grdInvoice.HeaderRow.Cells[3].FindControl("lblPrice");
                                    Label lblTotal = (Label)grdInvoice.HeaderRow.Cells[4].FindControl("lblTotalAmount");
                                    drCurrentRow = dtTable.NewRow();
                                    drCurrentRow["RowNumber"] = i + 1;
                                    drCurrentRow["ItemName"] = ext.Selected_Text;
                                    drCurrentRow["Quantity"] = txtQuantity.Text;
                                    drCurrentRow["Price"] = lblPrice.Text;
                                    TotalAmount = Convert.ToDouble(txtQuantity.Text) * Convert.ToDouble(lblPrice.Text);
                                    drCurrentRow["TotalAmount"] = TotalAmount.ToString();                                   
                                    drCurrentRow["ItemId"] =Convert.ToInt32(ext.Text);
                                    drCurrentRow["Invoice_Detail_Id"] = 0; 
                                }
                                Session["TotalAmount"] = "";
                                //add new row to DataTable
                                dtTable.Rows.Add(drCurrentRow);
                                if (dtTable.Rows[0][1].ToString().Equals(""))
                                {
                                    dtTable.Rows.Remove(dtTable.Rows[0]);
                                }
                                //Store the current data to ViewState
                                ViewState["Table"] = dtTable;
                                //Rebind the Grid with the current data
                                grdInvoice.DataSource = dtTable;
                                grdInvoice.DataBind();
                                GridViewRow row = (GridViewRow)grdInvoice.HeaderRow;
                                ext = (ExtendedDropDownList.ExtendedDropDownList)row.FindControl("extddlItem");
                                ext.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                                    ext1.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                                    ext1.Text = dtTable.Rows[i][5].ToString();
                                    ext1.Enabled = false;
                                }
                                CalculateTotalAmount();
                            }
                        }
                    }
                    else 
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "DuplicateItemValidation();", true);
                    }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void grdInvoice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        int index = Convert.ToInt32(e.NewEditIndex);
            int flag = 0;
            DataTable dtTable = (DataTable)ViewState["Table"];
            LinkButton lnkEdit = (LinkButton)grdInvoice.Rows[index].Cells[0].FindControl("lnkEdit");              
            TextBox txtQuantityVal = (TextBox)grdInvoice.Rows[index].Cells[2].FindControl("txtQuantityValue");
            Label txtPrice = (Label)grdInvoice.Rows[index].Cells[3].FindControl("txtPriceValue");
            Label txtAmount = (Label)grdInvoice.Rows[index].Cells[4].FindControl("txtAmount");

            TextBox txtQuantity = (TextBox)grdInvoice.HeaderRow.Cells[2].FindControl("txtQuantity");
            Label lblPrice = (Label)grdInvoice.HeaderRow.Cells[2].FindControl("lblPrice");
            Label lblTotal = (Label)grdInvoice.HeaderRow.Cells[2].FindControl("lblTotalAmount");
            GridViewRow grdrow = (GridViewRow)grdInvoice.HeaderRow;
            ext = (ExtendedDropDownList.ExtendedDropDownList)grdrow.FindControl("extddlItem");
            ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[index].FindControl("ItemName");
           
            try
            {
                if (grdInvoice.Rows.Count <= 1 && ext1.Text == "NA")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "BlankRowValidation();", true);
                }
                else
                {
                    if (lnkEdit.Text == "Edit")
                    {

                        for (int i = 0; i < grdInvoice.Rows.Count; i++)
                        {
                            LinkButton lnkEdit1 = (LinkButton)grdInvoice.Rows[i].Cells[0].FindControl("lnkEdit");
                            if (lnkEdit1.Text == "Update")
                            {
                                flag = 1;
                            }
                        }
                        if (flag == 0)
                        {
                            ext1.Enabled = true;
                            txtQuantityVal.Enabled = true;
                            lnkEdit.Text = "Update";


                            Session["RowIndex"] = index;
                            Session["ItemIndex"] = ext1.Text;
                        }
                    }
                    else if (lnkEdit.Text == "Update")
                    {
                        ext1.Enabled = false;
                        txtQuantityVal.Enabled = false;
                        lnkEdit.Text = "Edit";
                        double Total = (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtQuantityVal.Text));
                        txtAmount.Text = Total.ToString();


                        //Bind Latest Data In ViewState
                        DataTable _datatable = new DataTable();
                        DataRow dr = null;
                        _datatable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                        _datatable.Columns.Add(new DataColumn("ItemName", typeof(string)));
                        _datatable.Columns.Add(new DataColumn("Quantity", typeof(string)));
                        _datatable.Columns.Add(new DataColumn("Price", typeof(float)));
                        _datatable.Columns.Add(new DataColumn("TotalAmount", typeof(float)));
                        _datatable.Columns.Add(new DataColumn("ItemId", typeof(string)));
                        _datatable.Columns.Add(new DataColumn("Invoice_Detail_Id", typeof(string)));
                        for (int i = 0; i < grdInvoice.Rows.Count; i++)
                        {
                            ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                            TextBox txtQuantityVal1 = (TextBox)grdInvoice.Rows[i].Cells[2].FindControl("txtQuantityValue");
                            Label txtPrice1 = (Label)grdInvoice.Rows[i].Cells[3].FindControl("txtPriceValue");
                            Label txtAmount1 = (Label)grdInvoice.Rows[i].Cells[4].FindControl("txtAmount");
                            dr = _datatable.NewRow();
                            dr["RowNumber"] = i;
                            dr["ItemName"] = ext1.Selected_Text;
                            dr["Quantity"] = txtQuantityVal1.Text;
                            dr["Price"] = txtPrice1.Text;
                            dr["TotalAmount"] = txtAmount1.Text;
                            dr["ItemId"] = ext1.Text;
                            dr["Invoice_Detail_Id"] = grdInvoice.DataKeys[i][1].ToString();
                            _datatable.Rows.Add(dr);
                        }
                        ViewState["Table"] = _datatable;
                        CalculateTotalAmount();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void grdInvoice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[index].FindControl("ItemName");

        if (grdInvoice.Rows.Count <= 1 && ext1.Text == "NA")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "BlankRowValidation();", true);
        }
        else
        {
            Session["RowIndex"] = e.RowIndex;
            Span2.InnerHtml = "Record will get Deleted Permenently.Do You Want To Continue?";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "DeleteConformation();", true);
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int flag=0;
            ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[0].FindControl("ItemName");

            for (int i = 0; i < grdInvoice.Rows.Count; i++)
            {
                LinkButton lnkEdit1 = (LinkButton)grdInvoice.Rows[i].Cells[0].FindControl("lnkEdit");
                if (lnkEdit1.Text == "Update")
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "UpdateValidation();", true);
            }
            else if (extddlOffice.Selected_Text == "--- Select ---")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ProviderValidate();", true);
            }
            else if (txtServiceDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "DateValidate();", true);
            }
            else if (txtShipping.Text== "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ShippingAmountValidate();", true);
            }
            else if (txtPersonName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "PersonName();", true);
            }
            else if (txtPersonAddress.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "EmptyAddress();", true);
            }
            else if (txtCity.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "EmptyCity();", true);
            }
            else if (extddlPatientState.Selected_Text == "--- Select ---")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "EmptyState();", true);
            }
            else if (txtZip.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "EmptyZip();", true);
            }
            
            else if (grdInvoice.Rows.Count <= 1 && ext1.Selected_Text == "---Select---")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "EmptyGridValidate();", true);
            }
            
            

            else
            {
                _InvoiceDAO = new InvoiceDAO();
                DataTable dt = new DataTable();
                ArrayList objArr = new ArrayList();
                dt = (DataTable)ViewState["Table"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _InvoiceGetSet = new InvoiceGetSet();
                    _InvoiceGetSet.ItemID = dt.Rows[i][5].ToString();
                    _InvoiceGetSet.Quantity = dt.Rows[i][2].ToString();
                    _InvoiceGetSet.UnitePrice = dt.Rows[i][3].ToString();
                    _InvoiceGetSet.Amount = dt.Rows[i][4].ToString();
                    _InvoiceGetSet.InvoiceItemId = dt.Rows[i]["Invoice_Detail_Id"].ToString();
                    objArr.Add(_InvoiceGetSet);
                }
                arrStartValues = (ArrayList)ViewState["ArrayList"];

                if (btnSave.Text == "Save")
                {
                    _InvoiceDAO.SaveInvoice(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, txtCompanyId.Text, txtServiceDate.Text, Convert.ToDouble(lblTAmount.Text.Substring(1)), lblTodaysDate.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Convert.ToDouble(txtShipping.Text), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "SAVE", txtInvoiceId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, lblCaseNo.Text, extddlOffice.Text, objArr, arrStartValues, txtPersonName.Text, txtPersonAddress.Text, txtZip.Text, extddlPatientState.Text, txtCity.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);

                    ClearFields();
                }
                else if (btnSave.Text == "Update")
                {
                    _InvoiceDAO.SaveInvoice(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, txtCompanyId.Text, txtServiceDate.Text, Convert.ToDouble(lblTAmount.Text.Substring(1)), lblTodaysDate.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Convert.ToDouble(txtShipping.Text), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, "UPDATE", txtInvoiceId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, lblCaseNo.Text, extddlOffice.Text, objArr, arrStartValues, txtPersonName.Text, txtPersonAddress.Text, txtZip.Text, extddlPatientState.Text, txtCity.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                }

                usrMessage.PutMessage("Invoice Items Saved Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
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
  
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnYes.Attributes.Add("onclick", "YesMassage");
        int index =Convert.ToInt32(Session["RowIndex"].ToString());
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Table"];
        try
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[index]);
                usrMessage.PutMessage("Record Deleted Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            ViewState["Table"] = dt;
            grdInvoice.DataSource = dt;
            grdInvoice.DataBind();
            GridViewRow row = (GridViewRow)grdInvoice.HeaderRow;
            ext = (ExtendedDropDownList.ExtendedDropDownList)row.FindControl("extddlItem");
            ext.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            for (int i = 0; i < grdInvoice.Rows.Count; i++)
            {
                ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                ext1.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                ext1.Text = dt.Rows[i][5].ToString();
                ext1.Enabled = false;
            }
            CalculateTotalAmount();
            if (dt.Rows.Count == 0)
            {
                FillGrid();
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

    protected void btnNo_Click(object sender, EventArgs e)
    {         
        btnNo.Attributes.Add("onclick", "NoMassage");        
    }

    protected void ItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {             
                DataTable dtTable = (DataTable)ViewState["Table"];
                int index = Convert.ToInt32(Session["RowIndex"].ToString());
                Label lblPrice;
                lblPrice = (Label)grdInvoice.Rows[index].FindControl("txtPriceValue");
                ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[index].FindControl("ItemName");
                TextBox txtQuantity1 = (TextBox)grdInvoice.Rows[index].FindControl("txtQuantityValue");
                Label txtAmount = (Label)grdInvoice.Rows[index].FindControl("txtAmount");
                _InvoiceDAO = new InvoiceDAO();
                DataSet ds = new DataSet();
                string flag = "false";
                if (ext1.Text != "NA")
                {
                    int p = Convert.ToInt32(ext1.Text);
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (i != index)
                        {
                            if (dtTable.Rows[i][5].ToString().Equals(ext1.Text))
                            {
                                flag = "true";
                            }
                        }
                    }
                    if (flag == "false")
                    {

                        if (ext1.Selected_Text.ToString().Equals("---Select---"))
                        {

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ValidateItem();", true);
                        }
                        else if (txtQuantity1.Text == "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ValidateQuantity();", true);
                        }
                        else
                        {
                            ds = _InvoiceDAO.getInvoiceItemPrice(ext1.Text, txtCompanyId.Text);
                            txtSelectedItemIndex.Text = ext1.Text;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblPrice.Text = ds.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                lblPrice.Text = "0.00";
                            }
                            double Total = Convert.ToDouble(lblPrice.Text) * Convert.ToDouble(txtQuantity1.Text);
                            txtAmount.Text = Total.ToString();

                            //Bind Latest Data In ViewState
                            DataTable _datatable = new DataTable();
                            DataRow dr = null;
                            _datatable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            _datatable.Columns.Add(new DataColumn("ItemName", typeof(string)));
                            _datatable.Columns.Add(new DataColumn("Quantity", typeof(string)));
                            _datatable.Columns.Add(new DataColumn("Price", typeof(float)));
                            _datatable.Columns.Add(new DataColumn("TotalAmount", typeof(float)));
                            _datatable.Columns.Add(new DataColumn("ItemId", typeof(string)));
                            _datatable.Columns.Add(new DataColumn("Invoice_Detail_Id", typeof(string)));
                            for (int i = 0; i < grdInvoice.Rows.Count; i++)
                            {
                                ext1 = (ExtendedDropDownList.ExtendedDropDownList)grdInvoice.Rows[i].FindControl("ItemName");
                                TextBox txtQuantityVal1 = (TextBox)grdInvoice.Rows[i].Cells[2].FindControl("txtQuantityValue");
                                Label txtPrice1 = (Label)grdInvoice.Rows[i].Cells[3].FindControl("txtPriceValue");
                                Label txtAmount1 = (Label)grdInvoice.Rows[i].Cells[4].FindControl("txtAmount");
                                dr = _datatable.NewRow();
                                dr["RowNumber"] = i;
                                dr["ItemName"] = ext1.Selected_Text;
                                dr["Quantity"] = txtQuantityVal1.Text;
                                dr["Price"] = txtPrice1.Text;
                                dr["TotalAmount"] = txtAmount1.Text;
                                dr["ItemId"] = ext1.Text;
                                dr["Invoice_Detail_Id"] = grdInvoice.DataKeys[i][1].ToString();
                                _datatable.Rows.Add(dr);
                            }
                            ViewState["Table"] = _datatable;
                            //End Of Code
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "DuplicateItemValidation();", true);
                        ext1.Text = Session["ItemIndex"].ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "NoItemSelectedValidation();", true);
                    ext1.Text = Session["ItemIndex"].ToString();
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

    public void ClearFields()
    {
        extddlOffice.Text = "0";
        txtServiceDate.Text = "";
        txtShipping.Text = "";
        lblFinalTotalAmount.Text = "";
        lblTAmount.Text = "";
        txtPersonAddress.Text = "";
        txtPersonName.Text = "";
        txtCity.Text = "";
        extddlPatientState.Text = "0";
        txtZip.Text = "";        
        grdInvoice.DataSource = null;
        grdInvoice.DataBind();
        FillGrid();
    }
}