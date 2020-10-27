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
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class AJAX_Pages_Attorney_Referral_Report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
       string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        DataSet ds;
        string flag = "GET_USER_LIST";
        Bill_Sys_PatientBO obj = new Bill_Sys_PatientBO();
        ds = obj.loadattorney(flag, companyid);
        lstattorny.ValueField="CODE";
        lstattorny.TextField = "DESCRIPTION";
        lstattorny.DataSource = ds;
        lstattorny.DataBind();
   }

   //not used yet
    private void viewrepoert()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            
            for (int i = 0; i < grdattorneyreport.VisibleRowCount; i++)
            {
                //if (((CheckBox)di.FindControl("chkSelect")).Checked)
                //{
                   // case_id = case_id + "'" + grdCaseMaster.DataKeys[di.RowIndex][0].ToString() + "',";
                  // string assignlawfirm=assignlawfirm+"'"+grdattorneyreport.GetRowValues(i,"SZ_ASSIGNED_LAWFIRM_ID");
            }
           
           
            
            string case_id= "5094,5142,5142,7536";
            string list_case_id = case_id.Substring(0, case_id.Length - 1);
             Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

             DataTable dt = _bill_Sys_PatientBO.Get_SpecAttorneyCountReport(list_case_id, company_id, "", "");
             Session["SpecialityCount"] = dt;

           // Page.RegisterClientScriptBlock("mm", "<script language='javascript'>window.open('../Bill_Sys_GetTreatment.aspx', 'TreatmentData', 'left=30,top=30,scrollbars=1');</script>");
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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet ds = new DataSet();

            string companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string strfrom = "";
            string strto = "";
            string attorney = "";

            foreach (Object item in lstattorny.SelectedValues)
            {


                if (attorney.Equals(""))
                {
                    attorney = item.ToString();

                }
                else if (attorney != null)
                {

                    attorney = attorney + "," + item.ToString();
                }
            }

            if (dtfromdate.Value != null && dttodate.Value != null)
            {
                DateTime from = Convert.ToDateTime(dtfromdate.Value);
                DateTime to = Convert.ToDateTime(dttodate.Value);
                strfrom = from.ToString("MM/dd/yyyy");
                strto = to.ToString("MM/dd/yyyy");

            }

            Bill_Sys_PatientBO obj = new Bill_Sys_PatientBO();
            ds = obj.loadgrid(companyid, attorney, strfrom, strto);
            grdattorneyreport.DataSource = ds;
            grdattorneyreport.DataBind();
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

 }