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
using System.Text;
using System.IO;
public partial class Get_Treatment : PageBase
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
            string company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            if (!IsPostBack)
            {
                string lawfirmid = "7536";
                
                string datefrom = "";
                string dateto = "";
                if (base.Request.QueryString["visitfromdate"] != null)
                {
                    datefrom = Request.QueryString["visitfromdate"].ToString();
                }
                if (base.Request.QueryString["visittodate"] != null)
                {
                    dateto = Request.QueryString["visittodate"].ToString();
                }
                if (base.Request.QueryString["assignedLFid"] != null)
                {
                    lawfirmid = Request.QueryString["assignedLFid"].ToString();
                }
                //string case_id = "'5094','5142','5142','7536'";
                //string list_case_id = case_id.Substring(0, case_id.Length - 1);
                Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                DataTable dt = _bill_Sys_PatientBO.Get_SpecAttorneyCountReport(lawfirmid, company_id, datefrom, dateto);
                //DataTable dt = new DataTable();
                //dt = ((DataTable)Session["SpecialityCount"]);
                if (dt.Rows.Count > 0)
                {
                    //grdTreatment.DataSource = ((DataTable)Session["SpecialityCount"]);
                    grdTreatment.DataSource = dt;
                    grdTreatment.DataBind();
                    //Session["EXPORTSpecialityCount"] = Session["SpecialityCount"];
                    Session["EXPORTSpecialityCount"] = dt;
                    //Session["SpecialityCount"] = null;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Get_Treatment.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            DataTable dt = ((DataTable)Session["EXPORTSpecialityCount"]);
            for (int icount = 0; icount < dt.Rows.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        strHtml.Append("<td>");
                        strHtml.Append(dt.Columns[i].Caption);
                        strHtml.Append("</td>");
                    }
                    strHtml.Append("</tr>");
                }
                strHtml.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    strHtml.Append("<td>");
                    strHtml.Append(dt.Rows[icount].ItemArray.GetValue(j).ToString());
                    strHtml.Append("</td>");

                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
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


    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }
}