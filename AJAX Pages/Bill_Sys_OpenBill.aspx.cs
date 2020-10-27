using ASP;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class AJAX_Pages_Bill_Sys_OpenBill : Page, IRequiresSessionState
{

    public AJAX_Pages_Bill_Sys_OpenBill()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (base.Request.QueryString["bno"] != null)
        {
            string str = base.Request.QueryString["bno"].ToString();
            Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
            DataSet dataSet = new DataSet();
            dataSet = billSysNF3Template.getBillList(str);
            if (dataSet.Tables[0].Rows.Count <= 0)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "gogreen", "<script type='text/javascript'>alert('Bill is not generated. Please re-generate the bill.');</script>");
                return;
            }
            else
            {
                if (dataSet.Tables[0].Rows.Count <= 0)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "gogreen", "<script type='text/javascript'>alert('Bill is not generated. Please re-generate the bill.');</script>");
                    return;
                }
            }
            string str1 = dataSet.Tables[0].Rows[0][1].ToString();
            str1 = str1.Replace("\\", "/");
            string[] strArrays = str1.Split(new char[] { '/' });
            string str2 = "";
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                str2 = (i != 0 ? string.Concat(str2, "/", strArrays[i].Trim()) : strArrays[i].Trim());
            }
            string str3 = str2;
            Bill_Sys_NF3_Template billSysNF3Template1 = new Bill_Sys_NF3_Template();
            string str4 = dataSet.Tables[0].Rows[0][5].ToString();
            string physicalPath = dataSet.Tables[0].Rows[0][4].ToString();
            string str5 = str3;
            str5 = str5.Replace(str4, physicalPath);
            if (str3 == "" || str3 == null)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "sandeep", "<script type='text/javascript'>alert('Bill is not generated..');</script>");
                return;
            }
            if (!File.Exists(str5))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "sandeep", "<script type='text/javascript'>alert('File not found..');</script>");
                return;
            }
            this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "sandeep", string.Concat("<script type='text/javascript'>window.location.href='", str3, "'</script>"));
        }
    }
}