using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_Bill_Sys_SendReminder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnSend_click(object sender,EventArgs e)
    {
      string Message=  SMSMessaging.SendSMS(Request.QueryString["szpatientPnumber"].ToString(), txtsms.Text);
        if (Message != "Error") {
            lblMsg.Text = "Message Sent.";
        }
        else {
            lblMsg.Text = "Error Sending Message.";

        }
        
        
    }
}