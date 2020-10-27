using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class AJAX_Pages_ViewJfKDocs : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDelete.Attributes.Add("onclick", "return confirm_delete();");
            txtvisitId.Text = Request.QueryString["visitId"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindData();
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {   ArrayList arrObj = new ArrayList ();
        for (int i = 0; i < grdDocs.Items.Count; i++)
        {
            CheckBox chk =  (CheckBox)grdDocs.Items[i].FindControl("ChkDelete");
            if(chk.Checked)
            {
                EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO ();
                objEmployerVisitDO.CompanyID=txtCompanyID.Text;
                objEmployerVisitDO.ImageId=grdDocs.Items[i].Cells[0].Text.ToString();
                objEmployerVisitDO.VisitId=grdDocs.Items[i].Cells[3].Text.ToString();
                arrObj.Add(objEmployerVisitDO);
            }
            
        }
        EmployerBO objEmployerBO = new EmployerBO();
        string sReturn=objEmployerBO.DeleteDocs(arrObj);
        if (sReturn == "deleted")
        {
            lblMsg.Text = "Document deleted successfully";
        }
        else
        {
            lblMsg.Text = sReturn;
        }
        lblMsg.Visible = true;
        BindData();
    }


    public void BindData()
    {
        EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
     
        objEmployerVisitDO.CompanyID = txtCompanyID.Text;
        objEmployerVisitDO.VisitId = txtvisitId.Text;
     
        EmployerBO objEmployerBO = new EmployerBO();
        DataSet ds = objEmployerBO.GetDocumnets(objEmployerVisitDO);
        grdDocs.DataSource = ds;
        grdDocs.DataBind();
    }
}