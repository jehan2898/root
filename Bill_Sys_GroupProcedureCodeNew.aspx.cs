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
using DevExpress.Web;
using System.Text;


public partial class Bill_Sys_GroupProcedureCodeNew : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string BindGrid(string GroupCode,string CompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList objAL = new ArrayList();
            objAL.Add(GroupCode);
            objAL.Add(CompanyID);
            objAL.Add("LIST");

            Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
            return DataTableToJSONWithStringBuilder(objProcBO.Search_GroupProcedureCodes(objAL).Tables[0]);
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlProCodeGroup_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            Session["selected"] = "Y";
            hdnProcedureCode.Value = "";
            string strhml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
            //divGroupProcedureCode.InnerHtml = strhml;
           // BindGrid();
            //txtSearchBox.SourceGrid = this.GrdProcedureGroup;
            //txtSearchBox.Text = "";
            //BindGroup();
            lblMsg.Visible = false;
            txtGroupName.Text = "";
            txtGroupAmount.Text = "";
            //lbl_search.Visible = true;
            //txtSearchBox.Visible = true;
            //lbl_record_count.Visible = true;
            //lbl_Page_Count.Visible = true;
            //con.Visible = true;
            ProcedureDiv.Visible = true;
            if (extddlProCodeGroup.Text == "NA")
            {
                Session["selected"] = "";
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

    public static string DataTableToJSONWithStringBuilder(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    } 

    [System.Web.Services.WebMethod]

    public static bool IsLeapYear(int year)
    {

        return DateTime.IsLeapYear(year);

    }

    [System.Web.Services.WebMethod]

    public static string DataSource(string CodeGroup,string CompanyID,string Flag)
    {
        Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
        var dataSet = objProcBO.Search_GroupProcedureCodes(CodeGroup, CompanyID, Flag);
        string jsonDataSource = DataTableToJSONWithStringBuilder(dataSet.Tables[0]);
        return jsonDataSource;
    }
}