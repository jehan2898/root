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
using log4net;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using CustomControls.ContextMenuScope;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using Reminders;
using Visit_Application;

public partial class Agent_Bill_Sys_Agent_SearchCase : PageBase
{
    String SZ_Company_ID = "", SZ_User_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SZ_Company_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        SZ_User_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        extddlCaseStatus.Flag_ID = SZ_Company_ID;
        extddlCaseType.Flag_ID = SZ_Company_ID;
        extddlInsurance.Flag_ID = txtCompanyID.Text.ToString();
        extddlLocation.Flag_ID = SZ_Company_ID;
        try
        {
            ArrayList ArPat = new ArrayList();
            ArPat.Add(SZ_Company_ID);
            ArPat.Add(SZ_User_ID);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            ArPat.Add(null);
            FillPatientGrid FPG=new FillPatientGrid ();
            grdPatientList.DataSource = FPG.FillPatient(ArPat);
            grdPatientList.DataBind();
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
    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();

        if (extddlPatient.Text != "NA")
        {

            if (chkJmpCaseDetails.Checked == true)
            {
                string szCaseID = caseDetailsBO.GetCaseIdByPatientID(txtCompanyID.Text, extddlPatient.Text);
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    Response.Redirect("../Bill_Sys_ReCaseDetails.aspx?CaseID=" + szCaseID + "&cmp=" + txtCompanyID.Text + "", false);
                }
                else
                {
                    Response.Redirect("../Bill_Sys_CaseDetails.aspx?CaseID=" + szCaseID + "&cmp=" + txtCompanyID.Text + "", false);
                }
            }
            txtPatientName.Text = extddlPatient.Selected_Text;
        }
        else
        {
            txtPatientName.Text = "";
        }
    }

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extddlInsurance.Text != "NA")
        {
            txtInsuranceCompany.Text = extddlInsurance.Selected_Text; ;
        }
        else
        {
            txtInsuranceCompany.Text = "";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ArrayList ArPat = new ArrayList();
            ArPat.Add(SZ_Company_ID);
            ArPat.Add(SZ_User_ID);
            
            if (txtCaseNo.Text != "" && txtCaseNo.Text != null)
            {
                ArPat.Add(txtCaseNo.Text);
            }
            else
            {
                ArPat.Add(null);
            }

            if (extddlCaseType.Text != "NA")
            {
                ArPat.Add(extddlCaseType.Text.ToString());
            }
            else
            {
                ArPat.Add(null);
            }
            if (extddlCaseStatus.Text != "NA")
            {
                ArPat.Add(extddlCaseStatus.Text.ToString());
            }
            else
            {
                ArPat.Add(null);
            }

            if (txtPatientName.Text != "" && txtPatientName.Text != null)
            {
                ArPat.Add(txtPatientName.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtInsuranceCompany.Text != "" && txtInsuranceCompany.Text != null)
            {
                ArPat.Add(txtInsuranceCompany.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtClaimNumber.Text != "" && txtClaimNumber.Text != null)
            {
                ArPat.Add(txtClaimNumber.Text);
            }
            else
            {
                ArPat.Add(null);
            }

            if (txtSSNNo.Text != "" && txtSSNNo.Text != null)
            {
                ArPat.Add(txtSSNNo.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtDateofAccident.Text != "" && txtDateofAccident.Text != null)
            {
                ArPat.Add(txtDateofAccident.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtDateofBirth.Text != "" && txtDateofBirth.Text != null)
            {
                ArPat.Add(txtDateofBirth.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (extddlLocation.Text != "NA")
            {
                ArPat.Add(extddlLocation.Text.ToString());
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtpatientid.Text != "" && txtpatientid.Text != null)
            {
                ArPat.Add(txtpatientid.Text);
            }
            else
            {
                ArPat.Add(null);
            }
            if (txtChartNo.Text != "" && txtChartNo.Text != null)
            {
                ArPat.Add(txtChartNo.Text);
            }
            else
            {
                ArPat.Add(null);
            }

            FillPatientGrid FPG = new FillPatientGrid();
            grdPatientList.DataSource = FPG.FillPatient(ArPat);
            grdPatientList.DataBind();

            //fillcontrol(); //check serarch parameter
            //grdPatientList.XGridBindSearch();
            //clearcontrol();//clear all serach parameter for xml
            //SoftDelete();
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

    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        
    }
    #endregion
    protected void btnSoftDelete_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
        string sz_Case_id = "";

        try
        {
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

    }
}
