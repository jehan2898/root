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

public partial class Bill_Sys_AssociateProcedureCode : PageBase
{
    Bill_Sys_AssociatedCases _dill_Sys_ProcedureCode_BO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["diagnosisId"] != null)
                {
                    if (Request.QueryString["diagnosisId"].ToString() != "")
                    {
                        _dill_Sys_ProcedureCode_BO = new Bill_Sys_AssociatedCases();
                        DataSet ds = new DataSet();
                        ds = _dill_Sys_ProcedureCode_BO.GetAssociatedProcedure(Request.QueryString["diagnosisId"].ToString().Trim(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        grdProcedureCode.DataSource = ds.Tables[0];
                        grdProcedureCode.DataBind();
                        for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
                        {
                            foreach (DataGridItem dgiProcedureCode in grdProcedureCode.Items)
                            {
                                if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                                {
                                    CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                                    chkEmpty.Checked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    this.Dispose();
                }
            }
                
            
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AssociateProcedureCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayList;
            foreach (DataGridItem dgiProcedureCode in grdProcedureCode.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[1].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {
                    arrayList = new ArrayList();
                    arrayList.Add(Request.QueryString["diagnosisId"].ToString().Trim());
                    arrayList.Add(dgiProcedureCode.Cells[1].Text);
                    arrayList.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _dill_Sys_ProcedureCode_BO = new Bill_Sys_AssociatedCases();

                    _dill_Sys_ProcedureCode_BO.InsertAssociatedCases(arrayList);

                }
                lblMsg.Text = "Procedure code is successfully associated";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Request.QueryString["diagnosisId"] != null)
            {
                if (Request.QueryString["diagnosisId"].ToString() != "")
                {
                    _dill_Sys_ProcedureCode_BO = new Bill_Sys_AssociatedCases();
                    DataSet ds = new DataSet();
                    ds = _dill_Sys_ProcedureCode_BO.GetAssociatedProcedure(Request.QueryString["diagnosisId"].ToString().Trim(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    grdProcedureCode.DataSource = ds.Tables[0];
                    grdProcedureCode.DataBind();
                    for (int intLoop = 0; intLoop < ds.Tables[0].Rows.Count; intLoop++)
                    {
                        foreach (DataGridItem dgiProcedureCode in grdProcedureCode.Items)
                        {
                            if (dgiProcedureCode.ItemIndex == intLoop && ((Boolean)ds.Tables[0].Rows[intLoop]["CHECKED"]) == true)
                            {
                                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[0].FindControl("chkSelect");
                                chkEmpty.Checked = true;
                            }
                           
                        }
                    }
                }
                else
                {
                    this.Dispose();
                }
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
   
}
