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

public partial class AJAX_Pages_Bill_Sys_BillConfig : PageBase
{
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;

    //private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //_saveOperation = new SaveOperation();
        try
        {
             int iReturn = 0;
            Bill_Sys_Configuration objConfiguration=new Bill_Sys_Configuration();
            ArrayList objarrlist = new ArrayList();
            Bill_Sys_Configobject objconfig = new Bill_Sys_Configobject();
            Bill_Sys_Configobject objconfignew = new Bill_Sys_Configobject();

            objconfig.sz_companyid = objSessionBillingCompany.SZ_COMPANY_ID;
            objconfignew.sz_companyid = objSessionBillingCompany.SZ_COMPANY_ID;
            objconfig.sz_point = " Show Case No of pdf";
            objconfignew.sz_point = " Show Bill No of pdf";
            if (chkcaseno.Checked)
            {
                objconfig.bt_visible = "1";
            }
            else
            {
                objconfig.bt_visible = "0";
            }

            if (chkbillno.Checked)
            {
                objconfignew.bt_visible ="1";

            }
            else
            {
                objconfignew.bt_visible ="0";
            }

            objarrlist.Add(objconfig);
            objarrlist.Add(objconfignew);

            iReturn = objConfiguration.SaveConfig(objarrlist);



             if (iReturn > 0)
         {
             usrMessage1.PutMessage("Setting  Added Successfully ...!");
             usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
             usrMessage1.Show();
            
         }
         else
         {
             usrMessage1.PutMessage("Not Added Successfully");
             usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
             usrMessage1.Show();
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
