using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using System.Collections;


public partial class AdminProcess_AdProcess : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Reportfunction obj = new Reportfunction();
            //BindSubGrid();
            grdDesc.DataSource = BindSubGrid();
            grdDesc.DataBind();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        dao.ContentNotesDAO objcontent = new dao.ContentNotesDAO();
       
        Reportfunction obj1 = new Reportfunction();
        ArrayList list = new ArrayList();
        objcontent.Scompanyid = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        objcontent.SCreatedBy = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
        objcontent.STitle = Txttitle.Text;
        objcontent.SProcess = DDLprocess.SelectedItem.Value;

        for(int i=0; i<grdDesc.VisibleRowCount; i++)
        {
            dao.ContentSubNotesDAO objsubcontent = new dao.ContentSubNotesDAO();
            GridViewDataColumn c = (GridViewDataColumn)grdDesc.Columns[0]; // textbox column
            ASPxTextBox txtbox = (ASPxTextBox)grdDesc.FindRowCellTemplateControl(i, c, "txtsubtitle");
            GridViewDataColumn c1 = (GridViewDataColumn)grdDesc.Columns[1]; // txtMemo column
            ASPxMemo txtMemo = (ASPxMemo)grdDesc.FindRowCellTemplateControl(i, c1, "txtDesc");
           
            if (txtbox.Text.Trim() != "" && txtMemo.Text.Trim() != "")
            {
                objsubcontent.SSubTitle = txtbox.Text;
                objsubcontent.SSubDescription = txtMemo.Text;
                objsubcontent.SCompanyId1 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                objsubcontent.SCreatedBy = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                objcontent.AddContent(objsubcontent);
                
            }           
        }
        objcontent.SFileName = Fileupload.FileName;
        Result result = new Result();
        result = obj1.SaveNotesTransaction(objcontent);
        //obj1.addnotes(objcontent);      
    }
    public DataTable BindSubGrid()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataTable dt = new DataTable();
        try
        {
            
            dt.Columns.Add("SubTitle");
            dt.Columns.Add("Description");
            DataRow dr = dt.NewRow();
            dr["SubTitle"] = "";
            dr["Description"] = "";
            dt.Rows.Add(dr);
            DataRow dr1 = dt.NewRow();
            dr1["SubTitle"] = "";
            dr1["Description"] = "";
            dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow();
            dr2["SubTitle"] = "";
            dr2["Description"] = "";
            dt.Rows.Add(dr2);
            DataRow dr3 = dt.NewRow();
            dr3["SubTitle"] = "";
            dr3["Description"] = "";
            dt.Rows.Add(dr3);
            DataRow dr4 = dt.NewRow();
            dr4["SubTitle"] = "";
            dr4["Description"] = "";
            dt.Rows.Add(dr4);
  

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
        return dt;
    }


}