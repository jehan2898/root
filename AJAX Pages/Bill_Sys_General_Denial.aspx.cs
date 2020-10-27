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
using Componend;
using System.IO;
using System.Data.SqlClient;
using RequiredDocuments;
using System.Text;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_General_Denial : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_Case_Denial objDen = new Bill_Sys_Case_Denial();
        if (!IsPostBack)
        {
            extddenial.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnSaveDesc.Attributes.Add("onclick", "return Validate();");
            string caseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Bill_Sys_Case_Denial objdenial = new Bill_Sys_Case_Denial();
            hdnNodeId.Value = objDen.GetCaseGeneralDenialNode(caseId, companyId);
        }
        BindReasonGrid();//grdDenialReason
    }

    private void BindReasonGrid()
    {
        Bill_Sys_Case_Denial objDen = new Bill_Sys_Case_Denial();
        ArrayList objArl = new ArrayList();
        objArl.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString());
        objArl.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
        grdDenialReason.DataSource = objDen.GetCaseDenialDetail(objArl);
        grdDenialReason.DataBind();
    }

    protected void btnSaveDesc_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int j = 0;
            
            Bill_Sys_Case_Denial objOpDen = new Bill_Sys_Case_Denial();
            string reasons = hfdenialReason.Value.ToString();
            string[] reasonCodes = reasons.Split(',');
            try
            {
                ArrayList objDen = new ArrayList();
                for (int i = 0; i < reasonCodes.Length - 1; i++)
                {
                    Bill_Sys_Denial_Desc objDesc = new Bill_Sys_Denial_Desc();
                    objDesc.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                    objDesc.sz_case_id = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                    objDesc.sz_case_no = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
                    objDesc.sz_description = txtSaveDescription.Text.ToString();
                    objDesc.sz_verification_date = txtSaveDate.Text.ToString();
                    objDesc.sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    objDesc.sz_denial_reason_code = reasonCodes[i].ToString();
                    objDesc.sz_user_name = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                    objDesc.sz_flag = "DEN";

                    objDen.Add(objDesc);
                }

                j = objOpDen.saveCaseDenials(objDen);
                txtSaveDate.Text = "";
                extddenial.Text = "0";
                lbSelectedDenial.Text = "";
                hfdenialReason.Value = "";
                if (j > 0)
                {
                    usrMessage.PutMessage("Records Saved Successfully.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    
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
        //}

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnCancelDesc_Click(object sender, EventArgs e)
    {
    }

    protected void carTabPage_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
    {
    }

    protected void grdDenialReason_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int i = 0;
        try
        {
            if (e.CommandArgs.CommandName.ToString() == "Delete")
            {
                Bill_Sys_Case_Denial objOpDen = new Bill_Sys_Case_Denial();
                string szCaseID = e.CommandArgs.CommandArgument.ToString();
                string szDenialId = e.KeyValue.ToString();
                i = objOpDen.deleteCaseDenials(szCaseID, szDenialId);
                BindReasonGrid();
                if (i > 0)
                {
                    usrMessage.PutMessage("Records Deleted Successfully.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                }
            }

            if (e.CommandArgs.CommandName.ToString() == "Scan")
            {
                string str3 = "";
                string caseID = "";
                string companyId = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";
                string caseGeneralDenialNode = "";
                string str10 = "";
                string str11 = "";
                string str12 = "";
                string str13 = "";
                Bill_Sys_Case_Denial denial2 = new Bill_Sys_Case_Denial();
                str12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
                str13 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME.ToString();
                caseID = e.CommandArgs.CommandArgument.ToString();
                str10 = e.KeyValue.ToString();
                str7 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                str8 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString();
                str11 = "GeneralDenials";
                caseGeneralDenialNode = denial2.GetCaseGeneralDenialNode(caseID, companyId);
                str3 = ConfigurationManager.AppSettings["webscanurl"].ToString();
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "starScript", "window.open('" + (str3 + "&Flag=" + str11 + "&CaseId=" + caseID + "&CompanyId=" + companyId + "&CompanyName=" + str6 + "&UserId=" + str7 + "&UserName=" + str8 + "&NodeId=" + caseGeneralDenialNode + "&DenialId=" + str10 + "&PName=" + str13 + "&CaseNo=" + str12) + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
            }

            if (e.CommandArgs.CommandName.ToString() == "Upload")
            {
                string str14 = e.KeyValue.ToString();
                string str15 = e.CommandArgs.CommandArgument.ToString();
                this.hdfCaseID.Value = str15;
                this.hdfDenialID.Value = str14;
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAddDocumentDenialPopup", "ShowAddDocumentDenialPopup('" + str15 + "','" + str14 + "');", true);
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
}