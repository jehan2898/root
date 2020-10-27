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

public partial class Bill_Sys_BulkPayment : PageBase
{
    ArrayList _arraylist;
    DataSet _dataSet;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    //DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // imgbtnChequeDate.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtChequeDate,'imgbtnChequeDate','MM/dd/yyyy'); return false;");
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _arraylist = new ArrayList();
            _arraylist = (ArrayList)Session["BulkData"];
            if (!IsPostBack)
            {
                GetData();
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
            cv.MakeReadOnlyPage("Bill_Sys_BulkPayment.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void  GetData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _dataSet = new DataSet();
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        string strID="";
        try
        {
            for (int i = 0; i < _arraylist.Count; i++)
            {
              
                    if (strID == "") { strID = "'" + _arraylist[i].ToString() + "'"; }
                    else { strID = strID + ",'" + _arraylist[i].ToString() + "'"; }

            }
            _dataSet=_bill_Sys_BillingCompanyDetails_BO.GetPaymentList(strID);
           
            grvBulkPaymentTransaction.DataSource = _dataSet;
            grvBulkPaymentTransaction.DataBind();

            foreach (GridViewRow objItem in grvBulkPaymentTransaction.Rows)
            {
                if (((TextBox)objItem.Cells[3].Controls[1]).Text != "")
                {
                    objItem.Enabled = false;
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

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        for (int i = 0; i < _arraylist.Count; i++)
    //        {
    //            int getdiff = GetDifference(_arraylist[i].ToString());
    //            if (getdiff > 0 && getdiff != 1 )
    //            {
    //                ArrayList arrParam = new ArrayList();
    //                arrParam.Add(_arraylist[i].ToString());
    //                arrParam.Add(txtChequeNumber.Text.ToString());
    //                arrParam.Add(txtChequeDate.Text.ToString());
    //                if (getdiff < Convert.ToInt32(txtChequeAmount.Text.ToString()))
    //                {
    //                    arrParam.Add(getdiff);
    //                }
    //                else
    //                {
    //                    arrParam.Add(Convert.ToInt32(txtChequeAmount.Text));
    //                }
                    
    //                arrParam.Add(txtPaymentType.Text.ToString());
    //                arrParam.Add(txtCompanyID.Text.ToString());
    //                _bill_Sys_BillingCompanyDetails_BO.SaveData(arrParam);
    //                txtChequeAmount.Text =Convert.ToString(Convert.ToInt32(txtChequeAmount.Text.ToString()) - getdiff);
    //            }
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    private Int32 GetDifference(string billid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList arrayDiff;
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        Int32 idiff =0;
        try
        {
           arrayDiff = new ArrayList();
           arrayDiff =  _bill_Sys_BillingCompanyDetails_BO.GetDifference(billid);
           if (arrayDiff.Count > 0)
           {
               if (Convert.ToInt32(arrayDiff[1]) > Convert.ToInt32(arrayDiff[2]))
               {
                   idiff = Convert.ToInt32(arrayDiff[1]) - Convert.ToInt32(arrayDiff[2]);
               }
               else
               {
                   idiff = 0;
               }
           }
           else
           {
               idiff = 1;
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
        
        return idiff;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdateGridRecord_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
          
            foreach (GridViewRow objItem in grvBulkPaymentTransaction.Rows)
            {
                if (objItem.Enabled != false)
                {
                    _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                    if (((DropDownList)objItem.Cells[6].Controls[1]).SelectedValue != "3")
                    {
                        _bill_Sys_BillingCompanyDetails_BO.UpdatePaymentList(((Label)objItem.Cells[0].Controls[1]).Text, ((Label)objItem.Cells[1].Controls[1]).Text, ((TextBox)objItem.Cells[3].Controls[1]).Text, Convert.ToDateTime(((TextBox)objItem.Cells[4].Controls[1]).Text), Convert.ToDecimal(((TextBox)objItem.Cells[5].Controls[1]).Text), Convert.ToInt32(((DropDownList)objItem.Cells[6].Controls[1]).SelectedValue), ((TextBox)objItem.Cells[7].Controls[1]).Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                }
                
            }
            _arraylist = new ArrayList();
            _arraylist = (ArrayList)Session["BulkData"];
           
                GetData();
                lblMsg.Visible = true;
                lblMsg.Text = "Payment Saved Successfully ...!";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Payment successfully done!'); ", true);
            //Response.Redirect("Bill_Sys_BillSearch.aspx", false);


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



    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Response.Redirect("Bill_Sys_BillSearch.aspx",false);
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
