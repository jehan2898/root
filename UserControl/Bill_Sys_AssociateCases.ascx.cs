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

public partial class UserControl_Bill_Sys_AssociateCases : System.Web.UI.UserControl
{
    private Bill_Sys_AssociatedCases _associatedCases;
    private ArrayList _arrayList;
    private String szHTML;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["CaseID"] != null)
            {
                Session["Associate_Case_ID"] = Request.QueryString["CaseID"];
            }
            if (Session["Associate_Case_ID"] != null)
            {
                BindControl(Session["Associate_Case_ID"].ToString());
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    protected String getHTML()
    {

        return szHTML;
    }

    private void BindControl(string szCaseid)
    {
        _associatedCases = new Bill_Sys_AssociatedCases();
        _arrayList = new ArrayList();
        try
        {

            //divAssociatedCases.InnerHtml = "";
            Boolean status = false;
            _arrayList = _associatedCases.GetAssociatedCases(szCaseid, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            for (int i = 0; _arrayList.Count > i; i++)
            {

                //HyperLink lnk = new HyperLink();
                string strURL = "";
                status = true;
                if (i == 0)
                {
                    //lnk.ID = lnk + "_" + i;
                    //lnk.Text = _arrayList[i].ToString();
                    //lnk.CommandArgument = _arrayList[i].ToString();
                    
                    //lnk.CssClass = "sub-menu";
                    //lnk.PostBackUrl = "../Bill_Sys_CaseDetails.aspx?CaseID=" + _arrayList[i].ToString();
                    strURL = "Bill_Sys_CaseDetails.aspx?CaseID=" + _arrayList[i].ToString();
                    //divAssociatedCases.InnerHtml = divAssociatedCases.InnerHtml + "<li><a href='" + strURL + "' class='sub-menu'>" + _arrayList[i].ToString() + "</a></li>";
                    szHTML = szHTML + "<li><a href='" + strURL + "' class='sub-menu'>" + _arrayList[i].ToString() + "</a></li>";
                }
                else
                {
                    //lnk.ID = lnk + "_" + i;
                    //lnk.Text = _arrayList[i].ToString() + " | "; 
                    ////lnk.CommandArgument = _arrayList[i].ToString();
                    //lnk.CssClass = "sub-menu";
                    //lnk.NavigateUrl = "../Bill_Sys_CaseDetails.aspx?CaseID=" + _arrayList[i].ToString();
                    ////lnk.PostBackUrl = "../Bill_Sys_CaseDetails.aspx?CaseID=" + _arrayList[i].ToString();
                    ////divAssociatedCases.Controls.Add("<span>|<span>");
                    //divAssociatedCases.Controls.Add(lnk);
                    strURL = "Bill_Sys_CaseDetails.aspx?CaseID=" + _arrayList[i].ToString();
                    //divAssociatedCases.InnerHtml = divAssociatedCases.InnerHtml + " | " + "<li><a href='" + strURL + "'  class='sub-menu'>" + _arrayList[i].ToString() + "</a></li>";
                    szHTML = szHTML + "<li><a href='" + strURL + "' class='sub-menu'>" + _arrayList[i].ToString() + "</a></li>";
                }
               // string strCaseID = "Bill_Sys_CaseDetails.aspx " + "?CaseID=" + _arrayList[i].ToString();
                //if (i == 0) { divAssociatedCases.InnerHtml = divAssociatedCases.InnerHtml + ":  " + "<a href=" + strCaseID + ">"+_arrayList[i].ToString()+"</a>"; }
                //else { divAssociatedCases.InnerHtml = divAssociatedCases.InnerHtml + " | " + "<a href=" + strCaseID + ">"+_arrayList[i].ToString()+"</a>"; }

            }
            //if (status == true)
            //{ 
            //    divAssociatedCases.Visible=true; 
            //}
            //else { divAssociatedCases.Visible = false; }
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
}
