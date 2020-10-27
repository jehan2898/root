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

public partial class AJAX_Pages_Bill_Sys_DenialReasons : PageBase
{
    private Bill_Sys_DenialBO DenialBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.con.SourceGrid = grdDenialReasons;
                this.txtsearch.SourceGrid = grdDenialReasons;
                this.grdDenialReasons.Page = this.Page;
                this.grdDenialReasons.PageNumberList = this.con;
                btnDelete.Attributes.Add("onclick", "return checkSelected();");
                if(!Page.IsPostBack)
                {
                    txtDenialReason.Text="";
                    txtDenialID.Text="";
                    grdDenialReasons.XGridBind();
                }
                //else
                //{
                //    grdDenialReasons.XGridBindSearch();
                //}
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdDenialReasons.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }
    protected void grdDenialReasons_RowCommand(object sender,GridViewCommandEventArgs e)
    {
        if(e.CommandName=="Select")
        {
             int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
             txtDenialReason.Text= grdDenialReasons.DataKeys[iIndex]["DenialReason"].ToString();
             txtDenialID.Text=grdDenialReasons.DataKeys[iIndex]["DenialID"].ToString();
        }
    }

    protected void btnAdd_Click(object sender,EventArgs e)
    {
        DenialBO=new Bill_Sys_DenialBO();
        int result=0;
        if (txtDenialReason.Text.Trim()!="")
            result=DenialBO.AddDenialMaster(txtDenialReason.Text.Trim(),txtCompanyID.Text);
        if(result>0)
        {
             ScriptManager.RegisterClientScriptBlock(this,GetType(),"addclick1","alert('Denial Reason Added Successfully');",true);
             txtDenialID.Text="";
             txtDenialReason.Text="";
        }
        else
        {
             ScriptManager.RegisterClientScriptBlock(this,GetType(),"addclick2","alert('No Records Added. Please check that Denial Reason doesnot already exists');",true);
        }

        grdDenialReasons.XGridBindSearch();
    }
    protected void btnUpdate_Click(object sender,EventArgs e)
    {
        DenialBO=new Bill_Sys_DenialBO();
        int result=0;
        if((txtDenialID.Text.Trim()!="") && (txtDenialReason.Text.Trim()!=""))
            result=DenialBO.UpdateDenialMaster(txtDenialID.Text,txtDenialReason.Text.Trim(),txtCompanyID.Text);
        if(result>0)
        {
             ScriptManager.RegisterClientScriptBlock(this,GetType(),"updateclick1","alert('Denial Reason Updated Successfully');",true);
             txtDenialID.Text="";
             txtDenialReason.Text="";
        }
        else
        {
             ScriptManager.RegisterClientScriptBlock(this,GetType(),"updateclick1","alert('No Records Updated');",true);
        }
        grdDenialReasons.XGridBindSearch();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string szMessageDelete="";
        string szMessageNotDelete = "";
        int iReturnValue = 0;
        Bill_Sys_DenialBO objDelete;
        for (int i = 0; i < grdDenialReasons.Rows.Count; i++)
        {
            CheckBox chkDelete1 = (CheckBox)grdDenialReasons.Rows[i].FindControl("ChkDenail");
            if (chkDelete1.Checked)
            {
                objDelete = new Bill_Sys_DenialBO();
                iReturnValue = objDelete.DeleteenialMaster(grdDenialReasons.DataKeys[i]["DenialID"].ToString(), txtCompanyID.Text);

                if (iReturnValue ==1)
                {
                    if (szMessageDelete == "")
                    {
                        szMessageDelete = grdDenialReasons.DataKeys[i]["DenialReason"].ToString();
                    }
                    else
                    {
                        szMessageDelete = szMessageDelete + "," + grdDenialReasons.DataKeys[i]["DenialReason"].ToString();
                    }
                }
                else
                {
                    if (szMessageNotDelete == "")
                    {
                        szMessageNotDelete = grdDenialReasons.DataKeys[i]["DenialReason"].ToString();
                    }
                    else
                    {
                        szMessageNotDelete = szMessageNotDelete + "," + grdDenialReasons.DataKeys[i]["DenialReason"].ToString();
                    }
                }
            }
        }

        if (szMessageDelete != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('" + szMessageDelete + " -Denial Reasons Are Delete Successfully .');", true);
        } 
        
        if (szMessageNotDelete != "")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('" + szMessageNotDelete + " Denial Reasons Are In Use');", true);
        }




         grdDenialReasons.XGridBindSearch();
    }
}
