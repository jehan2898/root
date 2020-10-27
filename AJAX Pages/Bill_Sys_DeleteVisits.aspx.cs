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

public partial class AJAX_Pages_Bill_Sys_DeleteVisits : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.con.SourceGrid = grdDeleteVisit;
        this.txtSearchBox.SourceGrid = grdDeleteVisit;
        this.grdDeleteVisit.Page = this.Page;
        this.grdDeleteVisit.PageNumberList = this.con;
        this.Title = "Delete Visits";
        this.con1.SourceGrid = grdDoctorChange;
        this.txtSearchBox1.SourceGrid = grdDoctorChange;
        this.grdDoctorChange.Page = this.Page;
        this.grdDoctorChange.PageNumberList = this.con1;


        btnDelete.Attributes.Add("onclick", "return confirm_delete();");
        btnUpdate.Attributes.Add("onclick", "return confirm_update();");

        try
        {
            if (!IsPostBack)
            {
                ddlDateValues.Attributes.Add("onChange", "javascript:SetDate1();");
                DropDownList1.Attributes.Add("onChange", "javascript:SetDate();");
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    DataSet dsDoctorName = _obj.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                    ddlreferringdocs.DataSource = dsDoctorName;
                    ListItem objLI = new ListItem("---select---", "NA");
                    ddlreferringdocs.DataTextField = "DESCRIPTION";
                    ddlreferringdocs.DataValueField = "CODE";
                    ddlreferringdocs.DataBind();
                    ddlreferringdocs.Items.Insert(0, objLI);
                    ddlreferringdocs.Visible = true;
                    ddldoctors.Visible = false;
                }
                else
                {
                    DataSet dsDoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    ddldoctors.DataSource = dsDoctorName;
                    ListItem objLI = new ListItem("---select---", "NA");
                    ddldoctors.DataTextField = "DESCRIPTION";
                    ddldoctors.DataValueField = "CODE";
                    ddldoctors.DataBind();
                    ddldoctors.Items.Insert(0, objLI);
                    ddlreferringdocs.Visible = false;
                    ddldoctors.Visible = true;
                }

                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    DataSet dsDoctorName = obj.GetReferralDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "GETDOCTORLIST");
                    ddlReferringDoctor.DataSource = dsDoctorName;
                    ListItem objLI = new ListItem("---select---", "NA");
                    ddlReferringDoctor.DataTextField = "DESCRIPTION";
                    ddlReferringDoctor.DataValueField = "CODE";
                    ddlReferringDoctor.DataBind();
                    ddlReferringDoctor.Items.Insert(0, objLI);
                    ddlReferringDoctor.Visible = true;
                    ddlDoctor.Visible = false;
                    //  ddldoctors.SelectedValue
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

                grdDeleteVisit.XGridBindSearch();
                grdDoctorChange.XGridBindSearch();
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


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ArrayList arrEventId = new ArrayList();
        string SzUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        for (int i = 0; i < grdDeleteVisit.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdDeleteVisit.Rows[i].FindControl("ChkDelete1");
            if (chk.Checked)
            {
                string EventId = grdDeleteVisit.DataKeys[i]["I_EVENT_ID"].ToString();
                arrEventId.Add(EventId);
            }

        }

        Bill_Sys_Visit_BO obj = new Bill_Sys_Visit_BO();
        int iReturn = obj.DeleteEvent(arrEventId, txtCompanyID.Text, SzUserName);
        if (iReturn > 0)
        {
            usrMessage.PutMessage("Visit deleted Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            grdDeleteVisit.XGridBindSearch();
           
            grdDoctorChange.XGridBindSearch();
        }
        else
        {
            usrMessage.PutMessage("Not able to delete visit");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
    }


    protected void lnkExportTOExcel1_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdDoctorChange.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdDeleteVisit.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {  //txtProcedureGroupId
        txtProcedureGroupId.Text = extddlSpeciality.Text;
        txtDate1.Text = txtFromDate.Text;
        txtDate2.Text = txtToDate.Text;
        txtDocId.Text = extddlDoctor.Text;

        grdDeleteVisit.XGridBindSearch();
        txtProcedureGroupId.Text = "";
        txtDate1.Text = "";
        txtDate2.Text = "";
        txtDocId.Text = "";

    }

    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        txthdnFromDate.Text = txtFromDate1.Text;
        txthdnToDate.Text = txtToDate1.Text;
        txthdnDoctor.Text = ddldoctors.SelectedValue.ToString();
        grdDoctorChange.XGridBindSearch();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlDoctor.SelectedValue != "NA")
        {
            ArrayList arrEventId = new ArrayList();
            string SzUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            for (int i = 0; i < grdDoctorChange.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdDoctorChange.Rows[i].FindControl("ChkDelete");
                if (chk.Checked)
                {
                    string EventId = grdDoctorChange.DataKeys[i]["I_EVENT_ID"].ToString();
                    arrEventId.Add(EventId);
                }

            }

            Bill_Sys_Visit_BO obj = new Bill_Sys_Visit_BO();
            int iReturn = obj.UpdateEvent(txtCompanyID.Text, ddlDoctor.SelectedValue.ToString(), arrEventId);

            if (iReturn > 0)
            {
                usrMessage1.PutMessage("Update Successfully ...!");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();
                grdDoctorChange.XGridBindSearch();
                grdDeleteVisit.XGridBindSearch();
              
            }
            else
            {
                usrMessage1.PutMessage("Not able to Update visit");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage1.Show();
            }
        }
        else
        {
            usrMessage1.PutMessage("Please Select Doctor");
            usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage1.Show();
        }
    }




}

