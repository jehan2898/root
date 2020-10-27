using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class AJAX_Pages_Reffering_Provider_Visit : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDoctor();
            
        }
        if (hdnBtnclick.Value != "1")
        {
            BindGrid();
        }
    }


    public void BindDoctor()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_DoctorBO objDocBO = new Bill_Sys_DoctorBO();
            DataSet doctorList = objDocBO.GetDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);


            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlDoctor.DataSource = doctorList;
            ddlDoctor.TextField = "DESCRIPTION";
            ddlDoctor.ValueField = "CODE";
            ddlDoctor.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlDoctor.Items.Insert(0, Item);
            ddlDoctor.SelectedIndex = 0;

            Bill_Sys_ScanDco objScanDoc = new Bill_Sys_ScanDco();
            DataSet providerList = objScanDoc.GetProviderList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlRefferingProvider.DataSource = providerList;
            ddlRefferingProvider.TextField = "DESCRIPTION";
            ddlRefferingProvider.ValueField = "CODE";
            ddlRefferingProvider.DataBind();
            DevExpress.Web.ListEditItem providerItem = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlRefferingProvider.Items.Insert(0, providerItem);
            ddlRefferingProvider.SelectedIndex = 0;

            DevExpress.Web.ListEditItem ReffDoctor = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlRefferingDoctor.Items.Insert(0, ReffDoctor);
            ddlRefferingDoctor.SelectedIndex = 0;
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

    public void BindGrid()
    {
        DataSet ds = new DataSet();
        Bill_Sys_ScanDco objScanDoc = new Bill_Sys_ScanDco();
        string szFrmDate = "";
        string Todate = "";
        if (dtfromdate.Value != null && dttodate.Value != null)
        {
            DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
            DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
            szFrmDate = dtFrmdate.ToString("MM/dd/yyyy");
            Todate = dtTodate.ToString("MM/dd/yyyy");
        }
        ds = objScanDoc.GetVisit(txtCompanyID.Text, szFrmDate, Todate, txtCaseNo.Text, ddlDoctor.SelectedItem.Value.ToString(), ddlBiled.SelectedItem.Value.ToString());
        grdVisits.DataSource = ds;
        grdVisits.DataBind();
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string finalEventID = "";
            for (int i = 0; i < grdVisits.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[15];
                CheckBox chk = (CheckBox)grdVisits.FindRowCellTemplateControl(i, c, "chkall1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        string EventID = grdVisits.GetRowValues(i, "I_EVENT_ID").ToString();

                        finalEventID += "," + EventID;
                    }
                }
            }
            if (finalEventID != "")
            {
                finalEventID = finalEventID.Remove(0, 1);
            }
            string reffOffice=ddlRefferingProvider.SelectedItem.Value.ToString();
            string reffDoctorID = ddlRefferingDoctor.SelectedItem.Value.ToString();
            Bill_Sys_ScanDco objScanDoc = new Bill_Sys_ScanDco();
            string success = objScanDoc.updateVisitRefferingOffice(txtCompanyID.Text, finalEventID, reffOffice, reffDoctorID);
            if (success.ToLower().ToString() == "success")
            {
                usrMessage.PutMessage("Reffering provider attached successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage(success.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        hdnBtnclick.Value = "0";
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void ddlRefferingProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        string RefferingProviderID="";
        RefferingProviderID= ddlRefferingProvider.SelectedItem.Value.ToString();
        Bill_Sys_ScanDco objScanDoc = new Bill_Sys_ScanDco();
        DataSet dsRefferingDoctor = objScanDoc.GetRefferingDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,RefferingProviderID);
        ddlRefferingDoctor.DataSource = dsRefferingDoctor;
        ddlRefferingDoctor.TextField = "DESCRIPTION";
        ddlRefferingDoctor.ValueField = "CODE";
        ddlRefferingDoctor.DataBind();

        DevExpress.Web.ListEditItem ReffDoctor = new DevExpress.Web.ListEditItem("---Select---", "NA");
        ddlRefferingDoctor.Items.Insert(0, ReffDoctor);
        ddlRefferingDoctor.SelectedIndex = 0;
    }
}