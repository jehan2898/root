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

public partial class AJAX_Pages_Bill_Sys_Envelope_config : PageBase
{
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
            chkcompname.Attributes.Add("onclick", "chkcompanyname();");
            if (!IsPostBack)
            {
                Bill_Sys_Envelope_Config _Bill_Sys_Envelope_Config = new Bill_Sys_Envelope_Config();
                DataSet dscompainfo = new DataSet();
                dscompainfo = _Bill_Sys_Envelope_Config.Getenvelopeconfig(txtCompanyID.Text);
                if(dscompainfo.Tables.Count>0)
                {
                    if (dscompainfo.Tables[0].Rows.Count > 0)
                    {
                        if (dscompainfo.Tables[0].Rows[0]["BT_ADDRESS_STREET1"].ToString().ToLower() == "true")
                        {
                            chkaddress.Checked = true;

                        }
                        if (dscompainfo.Tables[0].Rows[0]["BT_ADDRESS_STREET2"].ToString().ToLower() == "true")
                        {
                            chkaddstreet.Checked = true;

                        }
                        if (dscompainfo.Tables[0].Rows[0]["BT_ADDRESS_CITY"].ToString().ToLower() == "true")
                        {
                            chkcity.Checked = true;

                        }
                        if (dscompainfo.Tables[0].Rows[0]["BT_ADDRESS_ZIP"].ToString().ToLower() == "true")
                        {
                            Chkzip.Checked = true;

                        }
                        if (dscompainfo.Tables[0].Rows[0]["BT_ADDRESS_STATE"].ToString().ToLower() == "true")
                        {
                            chkstate.Checked = true;

                        }
                        if (dscompainfo.Tables[0].Rows[0]["BT_COMPANY_NAME"].ToString().ToLower() == "true")
                        {
                            chkcompname.Checked = true;
                            hdnenvelopedispname.Value = "";

                        }
                        else
                        {
                            chkcompname.Checked = false;
                            txtcompanyname.Text = dscompainfo.Tables[0].Rows[0]["SZ_ENVELOPE_DISPLAY_NAME"].ToString().Trim();
                            hdnenvelopedispname.Value = txtcompanyname.Text;


                        }

                    }

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        string szcomname="";
        string szcomadd1="";
        string szcomnadd2="";
        string szcity="";
        string szstate="";
        string szzip="";
        Bill_Sys_Envelope_Config _Bill_Sys_Envelope_Config = new Bill_Sys_Envelope_Config();
        if(chkcompname.Checked)
        {
            szcomname="1";
            hdnenvelopedispname.Value = "";
        }
        else
        {
            szcomname="0";
            hdnenvelopedispname.Value = txtcompanyname.Text;

        }
         if(chkaddress.Checked)
        {
            szcomadd1="1";
        }
        else
        {
            szcomadd1="0";

        }
         if(chkaddstreet.Checked)
        {
            szcomnadd2="1";
        }
        else
        {
            szcomnadd2="0";

        }
         if(chkcity.Checked)
        {
            szcity="1";
        }
        else
        {
            szcity="0";

        }
         if(chkstate.Checked)
        {
            szstate="1";
        }
        else
        {
            szstate="0";

        }
         if(Chkzip.Checked)
        {
            szzip="1";
        }
        else
        {
            szzip="0";

        }
        int iReturn = 0;
     iReturn=   _Bill_Sys_Envelope_Config.Savecompanyinfo(txtCompanyID.Text, szcomadd1, szcomnadd2, szcity, szzip, szstate, szcomname,txtcompanyname.Text);
     if (iReturn > 0)
     {
         usrMessage.PutMessage("Save Successfully...");
         usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
         usrMessage.Show();
     }
     else
     {
         usrMessage.PutMessage("Fail To Save...");
         usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
         usrMessage.Show();

     }
    
    }
}
