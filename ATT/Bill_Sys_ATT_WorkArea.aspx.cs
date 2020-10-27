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

using DevExpress.Web;


public partial class ATT_Bill_Sys_ATT_WorkArea : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCaseID.Text = Request.QueryString["Caseid"].ToString();
            txtCompanyId.Text = Request.QueryString["cmp"].ToString();
            txtPatientId.Text = Request.QueryString["pid"].ToString(); 
            
            BindControl();
            BindPatientInfo();

        }

    }
    public void BindControl()
    {
        Bill_Sys_PatientDeskList obj = new Bill_Sys_PatientDeskList();
        DataSet Ds = obj.GetPatienDeskList(txtCaseID.Text, txtCompanyId.Text);
        DtlPatientDetails.DataSource = Ds;
        DtlPatientDetails.DataBind();
    }
    public void BindPatientInfo()
    {

        Bill_Sys_PatientBO  _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        DataSet DsPatientInfo = _bill_Sys_PatientBO.GetPatientInfo(txtPatientId.Text, txtCompanyId.Text);
        DtlView.DataSource = DsPatientInfo;
        DtlView.DataBind();
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_Att_SearchCase.aspx", false);
    }
}
