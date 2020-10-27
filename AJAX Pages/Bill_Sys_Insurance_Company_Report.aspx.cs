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

public partial class AJAX_Pages_Bill_Sys_Insurance_Company_Report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnLitigantion.Attributes.Add("onclick", "return Validate()");
       
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlInsurance.Flag_ID = txtCompanyID.Text;
        extddlOffice.Flag_ID = txtCompanyID.Text;
        extddlSpeciality.Flag_ID = txtCompanyID.Text;
        this.con.SourceGrid = grdInsuranceCompany;
        this.txtSearchBox.SourceGrid = grdInsuranceCompany;
        this.grdInsuranceCompany.Page = this.Page;
        this.grdInsuranceCompany.PageNumberList = this.con;
        if (!Page.IsPostBack)
        {
            //rdrecvd.Items[1].Selected = true;
            BillTransactionDAO status = new BillTransactionDAO();
            DataSet ds = new DataSet();
            ds = status.GetBillStaus(txtCompanyID.Text);
            lbStatus.DataSource = ds;
            lbStatus.DataTextField = "DESCRIPTION";
            lbStatus.DataValueField = "CODE";
            lbStatus.DataBind();
            Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

            string Status=_bill_Sys_BillingCompanyDetails_BO.GetBillStatusID(txtCompanyID.Text, "POM");
            for (int i = 0; i < lbStatus.Items.Count; i++)
            {
                if (lbStatus.Items[i].Value.ToString().Equals(Status))
                {
                    lbStatus.Items[i].Selected = true;
                }
            }
            txtReceived.Text = "'"+Status+"'";
            grdInsuranceCompany.XGridBindSearch();
            

        }
        btnClear1.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string BillStatus = "";
        for (int i = 0; i < lbStatus.Items.Count; i++)
        {
            if (lbStatus.Items[i].Selected)
            {
                if (BillStatus == "")
                {
                    BillStatus = BillStatus + "'" + lbStatus.Items[i].Value.ToString() +"'";

                }
                else
                {
                    BillStatus = BillStatus + ",'" + lbStatus.Items[i].Value.ToString() + "'";
                }
            }
        }
        txtReceived.Text = "";
        txtReceived.Text = BillStatus;
         grdInsuranceCompany.XGridBindSearch();
    }
    protected void btnLitigantion_Click(object sender, EventArgs e)
    {
        ArrayList _objarr = new ArrayList();
        for (int i = 0; i < grdInsuranceCompany.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)grdInsuranceCompany.Rows[i].FindControl("ChkLitigantion");
            if (chk.Checked)
            {
                Bill_sys_litigantion _objlitigantion = new Bill_sys_litigantion();
                string BillNo = grdInsuranceCompany.DataKeys[i][0].ToString();
                _objarr.Add(BillNo);
            }
        }
        Bill_Sys_NF3_Template objlitigate = new Bill_Sys_NF3_Template();
        objlitigate.UpdateLitigantion(_objarr, txtCompanyID.Text);
        usrMessage.PutMessage("Selected Bill sended litigation!");
        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        usrMessage.Show();
        grdInsuranceCompany.XGridBindSearch();
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdvDenial.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdInsuranceCompany.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

  
    
}
