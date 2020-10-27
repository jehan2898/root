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

public partial class AJAX_Pages_Bill_Sys_Doctor_Leave : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdDoctorleaves;
        this.txtSearchBox.SourceGrid = grdDoctorleaves;
        this.grdDoctorleaves.Page = this.Page;
        this.grdDoctorleaves.PageNumberList = this.con;
        this.Title = "Doctor Leaves";
        btnSave.Attributes.Add("onclick", "return CheckVal();");
        if (!IsPostBack)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
             Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                DataSet dsDoctorName = _obj.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                ddlReferringDoctor.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                ddlReferringDoctor.DataTextField = "DESCRIPTION";
                ddlReferringDoctor.DataValueField = "CODE";
                ddlReferringDoctor.DataBind();
                ddlReferringDoctor.Items.Insert(0, objLI);
                ddlReferringDoctor.Visible = true;
                ddlDoctor.Visible = false;
            }
            else
            {
                DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                ddlDoctor.DataSource = dsDoctorName;
                ListItem objLI = new ListItem("---select---", "NA");
                ddlDoctor.DataTextField = "DESCRIPTION";
                ddlDoctor.DataValueField = "CODE";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, objLI);
                ddlReferringDoctor.Visible = false;
                ddlDoctor.Visible = true;
            }
                
        }
    }

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDoctor.SelectedValue!="NA")
        {
            txtDoctorID.Text = ddlDoctor.SelectedValue.ToString();
            grdDoctorleaves.XGridBindSearch();
        }
    }

    protected void ddlReferringDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReferringDoctor.SelectedValue != "NA")
        {
            txtDoctorID.Text = ddlReferringDoctor.SelectedValue.ToString();
            grdDoctorleaves.XGridBindSearch();
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdDoctorleaves.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
     protected void btnSearch_onclick(object sender, EventArgs e)
     {
         Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
         int iReturn = 0;
         if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
         {
             txtDoctorID.Text = ddlReferringDoctor.SelectedValue.ToString();
             iReturn = obj.saveLeave(txtCompanyID.Text, txtDoctorID.Text, ddlReferringDoctor.SelectedItem.Text, txtReason.Text, txtDate.Text);


         }
         else
         {
             txtDoctorID.Text = ddlDoctor.SelectedValue.ToString();
           iReturn=  obj.saveLeave(txtCompanyID.Text,txtDoctorID.Text,ddlDoctor.SelectedItem.Text,txtReason.Text,txtDate.Text);
         }

         if (iReturn > 0)
         {
             usrMessage.PutMessage("Leave Added Successfully ...!");
             usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
             usrMessage.Show();
             grdDoctorleaves.XGridBindSearch();
         }
         else
         {
             usrMessage.PutMessage("Not able to add Leave");
             usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
             usrMessage.Show();
         }

     }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iReturn = 0;
        Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
        string SzUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        for (int i = 0; i < grdDoctorleaves.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdDoctorleaves.Rows[i].FindControl("ChkDelete");
            if (chk.Checked)
            {
                string szleaveid = grdDoctorleaves.DataKeys[i]["I_LEAVE_ID"].ToString();
                string szdoctorid = grdDoctorleaves.DataKeys[i]["SZ_DOCTOR_ID"].ToString();
                iReturn = obj.DeleteEvent(txtCompanyID.Text, szleaveid, szdoctorid);
               
            }

        }

        //Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();
        //int iReturn = obj.DeleteEvent(txtCompanyID.Text, txtDoctorID.Text );
        if (iReturn > 0)
        {
            usrMessage.PutMessage("Visit deleted Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            grdDoctorleaves.XGridBindSearch();
        }
        else
        {
            usrMessage.PutMessage("Not able to delete visit");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
    }
   
}
