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

public partial class Bill_Sys_EditRProcPopupPage : PageBase
{
    string szRoomId = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        btnUPdate.Attributes.Add("onclick", "return confirm_update_bill_status();");
        btnUpdateNew.Attributes.Add("onclick", "return confirm_update_bill_status_LHR();");
        if(!IsPostBack)
        {
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

          szRoomId=  Session["GETROOMID"].ToString() ;
          if (szRoomId == "All")
          {
              Bill_Sys_ManageVisitsTreatmentsTests_BO obj = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
              DataSet ProCod = obj.GetAllProcCodeLHR(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
              grdProCode.DataSource = ProCod;
              grdProCode.DataBind();
              btnUpdateNew.Visible = true;
              extddlDoctor.Visible = true;
              lblDoctor.Visible = true;
              btnUPdate.Visible = false;
          }
          else
          {
              ArrayList arr = new ArrayList();
              arr.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
              arr.Add(szRoomId);

              Bill_Sys_ManageVisitsTreatmentsTests_BO obj = new Bill_Sys_ManageVisitsTreatmentsTests_BO();
              DataSet ProCod = obj.GetReferringProcCodeList(arr);
              grdProCode.DataSource = ProCod;
              grdProCode.DataBind();
              btnUPdate.Visible = true;
              lblDoctor.Visible = false;
              btnUpdateNew.Visible = false;
              extddlDoctor.Visible = false;
          }

        }
    }

    protected void btnUPdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int iReturn = 0;
            for (int i = 0; i < grdProCode.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdProCode.Items[i].FindControl("chkSelectProc");
                if (chk.Checked)
                {
                    Bill_Sys_CheckoutBO objupadteProcedureCodes = new Bill_Sys_CheckoutBO();
                   iReturn= objupadteProcedureCodes.UpdateProc(Session["EVENTPROCID"].ToString(), grdProCode.Items[i].Cells[0].Text);
                   
                    
                }
            }
            if (iReturn > 0)
            {
                usrMessage.PutMessage("Procedure Code  updated successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
               // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid1').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_PaidBills.aspx?Flag=report&Type=p&popup=done1';</script>");
            }
            else
            {
                usrMessage.PutMessage("Procedure Code not  updated");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    protected void btnUpdateNew_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int iReturn = 0;
            for (int i = 0; i < grdProCode.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdProCode.Items[i].FindControl("chkSelectProc");
                if (chk.Checked)
                {
                    Bill_Sys_CheckoutBO objupadteProcedureCodes = new Bill_Sys_CheckoutBO();
                    iReturn = objupadteProcedureCodes.UpdateProcLHR(Session["EVENTPROCID"].ToString(), grdProCode.Items[i].Cells[0].Text, extddlDoctor.Text);


                }
            }
            if (iReturn > 0)
            {
                usrMessage.PutMessage("Procedure Code  updated successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>parent.document.getElementById('divid1').style.visibility = 'hidden';window.parent.document.location.href='Bill_Sys_PaidBills.aspx?Flag=report&Type=p&popup=done1';</script>");
            }
            else
            {
                usrMessage.PutMessage("Procedure Code not  updated");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
