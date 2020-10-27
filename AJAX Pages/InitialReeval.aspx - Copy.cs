using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using DevExpress.Web;

public partial class AJAX_Pages_InitialReeval : PageBase
{

    public DataSet loadMissingSpeciality(string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
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
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindMissingSpecialty()
    {
        DataSet ds = new DataSet();
        ds = loadMissingSpeciality(this.txtCompanyID.Text);
        //ASPxListBox lstMissingSpecialty = (ASPxListBox)this.grdSpeciality.FindControl("lstMissingSpecialty");
        //lstMissingSpecialty.ValueField = "CODE";
        //lstMissingSpecialty.TextField = "DESCRIPTION";
        grdSpeciality.DataSource = ds;
        grdSpeciality.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All ---", "NA");
        //lstMissingSpecialty.Items.Insert(0, Item);

        //  lstMissingSpecialty.SelectAll();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (!IsPostBack)
        {


            try
            {
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");

                btnSearch.Attributes.Add("onclick", "return Vlidate();");
                this.ajAutoIns.ContextKey = this.txtCompanyID.Text;
                this.ajAutoOffice.ContextKey = this.txtCompanyID.Text;
                extddlCaseType.Flag_ID = txtCompanyID.Text;

                extddlCaseStatus.Flag_ID = txtCompanyID.Text;
                string caseSatusId = new CaseDetailsBO().GetCaseSatusId(this.txtCompanyID.Text);
                this.extddlCaseStatus.Text = caseSatusId;
                lblCnt.Text = "0";
                BindMissingSpecialty();
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
        else
        {
            //BindMissingSpecialty();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            GetData();
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
    public void GetData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet dsValues = new DataSet();
            for (int i = 0; i < grdSpeciality.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdSpeciality.Columns[0];
                CheckBox chk = (CheckBox)grdSpeciality.FindRowCellTemplateControl(i, c, "chkall1");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        //SpecialityPDFFill obj = new SpecialityPDFFill();
                        //obj.SZ_SPECIALITY_NAME = grdSpeciality.GetRowValues(i, "description").ToString();
                        //obj.SZ_SPECIALITY_CODE = grdSpeciality.GetRowValues(i, "code").ToString();
                        ArrayList aList = new ArrayList();
                        aList.Add(grdSpeciality.GetRowValues(i, "code").ToString());
                        aList.Add(txtCompanyID.Text);
                        if (txtINS1.Text == "")
                        {
                            hdnInsurace.Value = "";
                        }
                        aList.Add(hdnInsurace.Value);
                        if (txtOffice1.Text == "")
                        {
                            hdnOffice.Value = "";
                        }
                        aList.Add(hdnOffice.Value);
                        aList.Add(extddlCaseType.Text);
                        aList.Add(txtupdateFromDate.Text);
                        aList.Add(txtupdateToDate.Text);
                        aList.Add(rblInitialReeval.SelectedValue.ToString());
                        aList.Add(extddlCaseStatus.Text);
                        Bill_sys_report objBill_sys_report = new Bill_sys_report();
                        DataSet temp = new DataSet();
                        temp = objBill_sys_report.geIntialRevalCases(aList);
                        if (dsValues.Tables.Count <= 0)
                        {
                            if (temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0)
                            {
                                dsValues = temp;
                            }

                        }
                        else
                        {
                            if (temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0)
                            {
                                dsValues.Tables[0].Merge(temp.Tables[0], true);
                            }


                        }
                    }
                }
            }
            lblCnt.Text = dsValues.Tables[0].Rows.Count.ToString();
            grdVisits.DataSource = dsValues;
            grdVisits.DataBind();
            grdExPort.DataSource = dsValues;
            grdExPort.DataBind();
            Session["Dataset"] = dsValues;

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
    public static string getFileName(string p_szBillNumber)
    {


        String szBillNumber = "";


        szBillNumber = p_szBillNumber;


        String szFileName;


        DateTime currentDate;


        currentDate = DateTime.Now;


        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");


        return szFileName;


    }
    public static string getRandomNumber()
    {


        System.Random objRandom = new Random();


        return objRandom.Next(1, 10000).ToString();


    }
    protected void btnexport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
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
            for (int icount = 0; icount < grdExPort.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 1; i < grdExPort.Columns.Count; i++)
                    {
                        if (grdExPort.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdExPort.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 1; j < grdExPort.Columns.Count; j++)
                {
                    if (grdExPort.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExPort.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("Invoice_Report") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            //   Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename + "');", true);


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
    protected void grdVisits_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["Dataset"];
            dv = ds.Tables[0].DefaultView;
            if (e.CommandName.ToString() == "case")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Name")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Phone")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "DOA")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }

            else if (e.CommandName.ToString() == "Provider")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "LastDate")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            if (e.CommandName.ToString() == "CaseType")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Doctor")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Insurance")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }

            else if (e.CommandName.ToString() == "CPhone")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "WPhone")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }

            dv.Sort = txtSort.Text;
            grdVisits.DataSource = dv;
            grdExPort.DataSource = dv;

            grdVisits.DataBind();
            grdExPort.DataBind();

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




    protected void grdVisits_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        grdVisits.SelectedIndex = e.NewPageIndex;
        grdVisits.DataBind();
    }
}