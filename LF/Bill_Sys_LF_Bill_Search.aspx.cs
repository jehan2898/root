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
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using CustomControls.ContextMenuScope;
using System.Collections;
using mbs.lawfirm;
using XMLData;
using System.Data.SqlClient;
using System.IO;

public partial class LF_Bill_Sys_LF_Bill_Search : PageBase
{
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ArrayList list2 = new ArrayList();
        string str = this.Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        string str3 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            CheckBox box = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkselectBllno");
            if (box.Checked)
            {
                DAO_PatientList list = new DAO_PatientList();
                list.CaseID=this.grdBillSearch.Rows[i].Cells[0].Text.ToString();
                list.Bill_NO = this.grdBillSearch.Rows[i].Cells[3].Text.ToString();//Change Cell 1 to 3
                list.CompanyId=this.grdBillSearch.DataKeys[i]["SZ_COMPANY_ID"].ToString();
                list.LAWFIRM_COMPANY_ID=this.txtCompanyId.Text.ToString();
                list.USER_ID=(str2);
                list.IP_ADDRESS=(str);
                list.PATIENT_NAME=this.grdBillSearch.DataKeys[i]["PATIENT_NAME"].ToString();
                list.PROCEDURE_GROUP_ID=this.grdBillSearch.DataKeys[i]["SPECIALITY_ID"].ToString();
                list.USER_NAME=str3;
                new Bill_Sys_CollectDocAndZip();
                list2.Add(list);
            }
        }
        string str4 = "";
        Bill_Sys_CollectDocAndZip zip = new Bill_Sys_CollectDocAndZip();
        if (this.chkCaseDoc.Checked)
        {
            str4 = zip.CollectAndZipBillDocs(list2, "1");
        }
        else
        {
            str4 = zip.CollectAndZipBillDocs(list2, "0");
        }
        if (str4 == "")
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('There are no files available on the server to download.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + str4 + "'); ", true);
        }
    }

    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        Bill_Sys_CollectDocAndZip zip = new Bill_Sys_CollectDocAndZip();
        ArrayList list = new ArrayList();
        DataSet set = new DataSet();
        XMLData.XMLData data = new XMLData.XMLData();
        XMLData.XMLData data2 = new XMLData.XMLData();
        string str = ConfigurationManager.AppSettings["LFBillSearch"].ToString();
        data2 = data.ReadXml(str);
        set = data.XGridBind(this.Page, data2, 1, this.grdBillSearch.RecordCount, "", this.txtSearchBox.Text);
        if (this.chkCaseDoc.Checked)
        {
            list = zip.CollectAndZipBillDoc(set, "1");
        }
        else
        {
            list = zip.CollectAndZipBillDoc(set, "0");
        }
        if (list.Count < 1)
        {
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('There are no files available on the server to download.');", true);
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                ScriptManager.RegisterStartupScript((Page)this, typeof(string), "popup" + i.ToString(), "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + list[i].ToString().Replace(ConfigurationSettings.AppSettings["BASEPATH_OF_DOWNLOAD"].ToString(), "").Trim() + "'); ", true);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.txtsearch.Text.Length > 0)
        {
            string[] strArray = this.txtsearch.Text.Split(new char[] { ',' });
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str == "")
                {
                    str = "'" + strArray[i].ToString() + "'";
                }
                else if (strArray[i].ToString() != "")
                {
                    str = str + ",'" + strArray[i].ToString() + "'";
                }
            }
            this.txtAllBillno.Text = str;
        }
        this.grdBillSearch.XGridBindSearch();
        this.txtAllBillno.Text = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        this.con.SourceGrid=this.grdBillSearch;
        this.txtSearchBox.SourceGrid=this.grdBillSearch;
        this.grdBillSearch.Page=this.Page;
        this.grdBillSearch.PageNumberList=this.con;
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        this.btnDownloadAll.Attributes.Add("onclick", "return ValidateALL();");
        this.btnDownload.Attributes.Add("onclick", "return checkSelected();");
        if (!base.IsPostBack)
        {
            this.txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        }
        else if (this.txtsearch.Text.Length > 0)
        {
            string[] strArray = this.txtsearch.Text.Split(new char[] { ',' });
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str == "")
                {
                    str = "'" + strArray[i].ToString() + "'";
                }
                else if (strArray[i].ToString() != "")
                {
                    str = str + ",'" + strArray[i].ToString() + "'";
                }
            }
            this.txtAllBillno.Text = str;
        }
    }
    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
       ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
