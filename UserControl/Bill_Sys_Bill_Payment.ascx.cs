using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Bill_Sys_Bill_Payment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bill_Sys_User_Payment objPayment = new Bill_Sys_User_Payment();
            DataSet ds = new DataSet();
            ds = objPayment.Get_PaymetInfo(txtBillNo.Text);
            if(ds.Tables.Count>0)
            {
                rptBillInfo.DataSource = ds.Tables[0];
                rptBillInfo.DataBind();
            }

            if (ds.Tables.Count > 1)
            {
                grdBillDetails.DataSource = ds.Tables[1];
                grdBillDetails.DataBind();
            }
        }
    }
    protected void grdBillDetails_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
      
    }
}