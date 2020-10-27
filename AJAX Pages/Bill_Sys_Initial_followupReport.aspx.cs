using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;


public partial class AJAX_Pages_Bill_Sys_Initial_followupReport : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                extddlLocation.Visible = true;
                trLocation.Visible = true;
                extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
            }
        }
        BindGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_Get_Event_Data objEventData = new Bill_Sys_Get_Event_Data();
            DataSet ds = new DataSet();
            string szFrmDate = "";
            string Todate = "";
            if (dtfromdate.Value != null && dttodate.Value != null)
            {
                DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
                DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
                szFrmDate=dtFrmdate.ToString("MM/dd/yyyy");
                Todate = dtTodate.ToString("MM/dd/yyyy");
            }

            ds = objEventData.Getvisits(txtCompanyID.Text, extddlSpeciality.Text, extddlDoctor.Text, szFrmDate, Todate, extddlLocation.Text);
            //   ds = objEventData.Getvisits(txtCompanyID.Text, "NA", "NA", "", "");
            DataTable dt = objEventData.GetAllvisits(ds);
            
            grdVisits.DataSource = dt;
            grdVisits.DataBind();
            
            for (int i = 0; i < grdVisits.VisibleRowCount; i++)
            {
                string szVisit = grdVisits.GetRowValues(i, "SZ_VISIT").ToString();
                if (szVisit == "1")
                {
                    GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[7];
                    //GridViewRow r = (GridViewRow)grdVisits.GetDataRow(i);
                    
                    //c.CellStyle.BackgroundImage.ImageUrl = "Images/ok_small_1.gif";
                    //c.CellStyle.BackgroundImage.HorizontalPosition = "right";
                    //c.CellStyle.BackgroundImage.VerticalPosition = "bottom";

                }
                else
                {
                    //GridViewDataColumn c = (GridViewDataColumn)grdVisits.Columns[7];
                    //c.CellStyle.BackgroundImage.ImageUrl = "Images/error_small_1.gif";
                    //c.CellStyle.BackgroundImage.HorizontalPosition = "right";
                    //c.CellStyle.BackgroundImage.VerticalPosition = "bottom";
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

    protected void grdVisits_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        string temp = e.Column.FieldName;
    }
    protected void grdVisits_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        //string szVisit = grdVisits.GetRowValues(e.VisibleIndex - 1, "SZ_VISIT").ToString();
        //if (szVisit == "1")
        if (e.DataColumn.FieldName == "DT_EVENT_DATE")
        {
            string szVisit = grdVisits.GetRowValues(e.VisibleIndex, "SZ_VISIT").ToString();
            if (szVisit == "1")
            {
            

                e.Cell.Style.Add("background-image", ResolveUrl("Images/ok_small_1.gif"));
                e.Cell.Style.Add("background-repeat", "no-repeat");
            }
        }
    }

    
}