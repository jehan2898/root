using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreatingDoctor;
using System.Data;
using DevExpress.Web;
using System.Collections;

public partial class TreatingDoctor_Default2 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCaseType();
            BindSpeciality();
            BindDoctor();
        }
        BindGrid();   
    }

    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        BindGrid();
        string sFileName = null;
        string sFile = TreatingDoctor.utils.TreatingDoctorUtils.GetExportPhysicalPath(sUserName, TreatingDoctor.utils.ReferringProviderExportTypes.VISIT_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdVisitsExport.WriteXlsx(stream);
        stream.Close();

        ArrayList list = new ArrayList();
        list.Add(TreatingDoctor.utils.TreatingDoctorUtils.GetExportRelativeURL(sFileName));
        Session["Download_Files"] = list;
    }


    public void BindCaseType()
    {
        try
        {
            TreatingDoctor.TreatingDoctor objDocBO = new TreatingDoctor.TreatingDoctor();
            DataSet casetypelist = objDocBO.getCaseType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlCaseType.DataSource = casetypelist;
            ddlCaseType.TextField = "DESC";
            ddlCaseType.ValueField = "CODE";
            ddlCaseType.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlCaseType.Items.Insert(0, Item);
            ddlCaseType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindSpeciality()
    {
        try
        {
            TreatingDoctor.TreatingDoctor objDocBO = new TreatingDoctor.TreatingDoctor();
            DataSet casetypelist = objDocBO.getSpecialty(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlSpecility.DataSource = casetypelist;
            ddlSpecility.TextField = "DESC";
            ddlSpecility.ValueField = "CODE";
            ddlSpecility.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlSpecility.Items.Insert(0, Item);
            ddlSpecility.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindDoctor()
    {
        try
        {
            Bill_Sys_DoctorBO objDocBO = new Bill_Sys_DoctorBO();
            DataSet doctorList = objDocBO.GetDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            ddlDoctor.DataSource = doctorList;
            ddlDoctor.TextField = "DESCRIPTION";
            ddlDoctor.ValueField = "CODE";
            ddlDoctor.DataBind();
            DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
            ddlDoctor.Items.Insert(0, Item);
            ddlDoctor.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
        }
    }

    public void BindGrid()
    {
        DataSet dsVisits = new DataSet();
        TreatingDoctor.TreatingDoctor objDocBO = new TreatingDoctor.TreatingDoctor();
        TreatingDoctorVisitDAO objVisitDao = new TreatingDoctorVisitDAO();

        if (ddlDoctor.SelectedItem.Value.ToString() != "NA")
        {
            objVisitDao.DoctorID = ddlDoctor.SelectedItem.Value.ToString();
        }
        else
        {
            objVisitDao.DoctorID = "";
        }

        if (ddlCaseType.SelectedItem.Value.ToString() != "NA")
        {
            objVisitDao.CaseTypeID = ddlCaseType.SelectedItem.Value.ToString();
        }
        else
        {
            objVisitDao.CaseTypeID = "";
        }

        if (ddlSpecility.SelectedItem.Value.ToString() != "NA")
        {
            objVisitDao.SpecialtyID = ddlSpecility.SelectedItem.Value.ToString();
        }
        else
        {
            objVisitDao.SpecialtyID = "";
        }
        string szFrmDate = "";
        string Todate = "";
        if (dtfromdate.Value != null && dttodate.Value != null)
        {
            DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
            DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
            szFrmDate = dtFrmdate.ToString("MM/dd/yyyy");
            Todate = dtTodate.ToString("MM/dd/yyyy");
        }
        objVisitDao.VisitDateFrom = szFrmDate;
        objVisitDao.VisitDateTo = Todate;

        objVisitDao.CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        objVisitDao.ReferringOfficeID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_REFF_PROVIDER_ID;
        objVisitDao.UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

        dsVisits = objDocBO.getVisitList(objVisitDao);
        grdVisits.DataSource = dsVisits;
        grdVisits.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       //BindGrid();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        
        ddlDoctor.SelectedIndex = 0;
        ddlSpecility.SelectedIndex = 0;
        ddlCaseType.SelectedIndex = 0;
        ddlDateValues.SelectedIndex = 0;
        
        BindGrid();
    }
}