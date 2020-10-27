using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_Doctor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
        }
    }
    public void LoadDoctor(string CompanyID)
    {
        DataSet DsReadDoc = new DataSet();
        UserControlLib.Doctor objDoc = new UserControlLib.Doctor();
        DsReadDoc = objDoc.ActiveReadingDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (DsReadDoc.Tables.Count > 0)
        {
            if (DsReadDoc.Tables[0].Rows.Count > 0)
            {
                cmbActiveDoctor.ValueField = "SZ_DOCTOR_ID";
                cmbActiveDoctor.DataSource = DsReadDoc;
                cmbActiveDoctor.DataBind();
            }
        }
    }
    public void RefferDoctor(string CompanyID)
    {
        DataSet DsReferDoc = new DataSet();
        UserControlLib.Doctor objDoc = new UserControlLib.Doctor();
        DsReferDoc = objDoc.GetReferringDoctor(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (DsReferDoc.Tables.Count > 0)
        {
            if (DsReferDoc.Tables[0].Rows.Count > 0)
            {
                cmbReferrringDoctor.ValueField = "SZ_DOCTOR_ID";
                cmbReferrringDoctor.DataSource = DsReferDoc;
                cmbReferrringDoctor.DataBind();
            }
        }
    }

}
