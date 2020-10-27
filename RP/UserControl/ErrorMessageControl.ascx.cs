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

public partial class UserControl_ErrorMessageControl : System.Web.UI.UserControl
{
    private string cls_szMessage = null;
    private int cls_iErrorType = (int) DisplayType.Type_UserMessage;    //default to error message if not set

    public enum DisplayType : int
    {
        Type_ErrorMessage = 0,
        Type_SystemMessage = 1,
        Type_UserMessage =2
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cls_szMessage != null)
        {
            if (cls_szMessage.Trim().Length > 0)
            {
                this.lblUsrMessage.Text = cls_szMessage;
                this.divOuterMessage.Visible = true;
                if (cls_iErrorType == (int)DisplayType.Type_ErrorMessage)
                {
                    this.divErrorControl.Style.Value = "font-family:Arial;font-size:14px;padding-bottom:10px;text-align:left;";
                    this.divOuterMessage.Style.Value = "background-color:#FFFF99;border:1px;padding-bottom:10px";
                    this.imgMessage.Src = "~/images/error_small.gif";
                    this.lblUsrMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (cls_iErrorType == (int)DisplayType.Type_UserMessage)
                    {
                        this.divOuterMessage.Style.Value = "background-color:#DBE6FA;border:1px;padding-bottom:10px";
                        this.imgMessage.Src = "~/images/ok_small.gif";
                        this.lblUsrMessage.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        //this.lblUsrMessage.ForeColor = System.Drawing.Color.Black;
                        this.imgMessage.Src = "~/images/Warning.gif";
                        this.lblUsrMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                cls_szMessage = "";
            }
            else
            {
                this.divErrorControl.Style.Value = "padding-bottom:0px;";
                this.divOuterMessage.Style.Value = "padding-bottom:0px;";
                this.divOuterMessage.Visible = false;
                this.divDummyArea.Visible = false;
                this.imgMessage.Visible = false;
            }
        }
        else
        {
            this.divErrorControl.Style.Value = "padding-bottom:0px;";
            this.divOuterMessage.Style.Value = "padding-bottom:0px;";
            this.divOuterMessage.Visible = false;
            this.divDummyArea.Visible = false;
            this.imgMessage.Visible = false;
        }
    }

    public void PutMessage(string p_szMessage)
    {
        cls_szMessage = p_szMessage;
    }

    public void SetMessageType(DisplayType p_ErrorType)
    {
        this.cls_iErrorType = (int) p_ErrorType;
    }

    public void Show()
    {
        if (cls_szMessage != null)
        {
            if (cls_szMessage.Trim().Length > 0)
            {
                this.lblUsrMessage.Text = cls_szMessage;
                this.divOuterMessage.Visible = true;
                if (cls_iErrorType == (int)DisplayType.Type_ErrorMessage)
                {
                    this.divErrorControl.Style.Value = "font-family:Arial;font-size:14px;padding-bottom:10px;text-align:left;";
                    this.divOuterMessage.Style.Value = "background-color:#FFFF99;border:1px;padding-bottom:10px";
                    this.imgMessage.Visible = true;
                    this.imgMessage.Src = "~/images/error_small.gif";
                    this.lblUsrMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (cls_iErrorType == (int)DisplayType.Type_UserMessage)
                    {
                        this.divOuterMessage.Style.Value = "font-family:Arial;font-size:14px;background-color:#DBE6FA;border:1px;text-align:center;padding-bottom:10px";
                        this.imgMessage.Visible = true;
                        this.imgMessage.Src = "~/images/ok_small.gif";
                        this.lblUsrMessage.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        //this.lblUsrMessage.ForeColor = System.Drawing.Color.Black;
                        this.imgMessage.Visible = true;
                        this.imgMessage.Src = "~/images/Warning.gif";
                        this.lblUsrMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                cls_szMessage = "";
            }
            else
            {
                this.divErrorControl.Style.Value = "padding-bottom:0px;";
                this.divOuterMessage.Style.Value = "padding-bottom:0px;";
                this.divOuterMessage.Visible = false;
                this.divDummyArea.Visible = false;
                this.imgMessage.Visible = false;
            }
        }
        else
        {
            this.divErrorControl.Style.Value = "padding-bottom:0px;";
            this.divOuterMessage.Style.Value = "padding-bottom:0px;";
            this.divOuterMessage.Visible = false;
            this.divDummyArea.Visible = false;
            this.imgMessage.Visible = false;
        }
    }
}
