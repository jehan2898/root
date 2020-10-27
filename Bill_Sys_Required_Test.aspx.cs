/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_RequiredTest.aspx.cs
/*Purpose              :       To Add and Edit Required Test
/*Author               :       Bhilendra Y
/*Date of creation     :       03 Nov 2009
/*Modified By          :      
/*Modified Date        :       
/************************************************************/

#region  Using

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
using Componend;

#endregion


public partial class Bill_Sys_Required_Test : PageBase
{
    Bill_Sys_RequiredTest objRequiredTest;
    int intTestCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                lblCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID ;
                lblPatient.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                //lblPatient.ToolTip = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;

                BindRequiredTest();
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

    public void BindRequiredTest()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable dtRequiredTest = new DataTable();
            objRequiredTest = new Bill_Sys_RequiredTest();

            objRequiredTest.GetRequiredTestDetails(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID,  ref dtRequiredTest);           

            grdRequiredTest.DataSource = dtRequiredTest;
            grdRequiredTest.DataBind();


            DataTable dtExistRequiredTest = new DataTable();
            objRequiredTest.GetExist_RequiredTestDetails(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ref dtExistRequiredTest);

            foreach (GridViewRow rowItem in grdRequiredTest.Rows)
            {
                CheckBox chkGroup;
                chkGroup = (CheckBox)(rowItem.Cells[0].FindControl("chkGroup"));
                
                //intTestCount = dtRequiredTest.Rows.Count;

                if (dtExistRequiredTest.Rows.Count > 0)
                {
                    if (dtExistRequiredTest.Rows[intTestCount][2].ToString() == grdRequiredTest.DataKeys[rowItem.RowIndex]["SZ_PROCEDURE_GROUP_ID"].ToString())
                    {
                        chkGroup.Checked = true;
                        

                        if (dtExistRequiredTest.Rows[intTestCount][4].ToString() != "0")
                        {
                            chkGroup.Enabled = false;
                        }
                        if (intTestCount == dtExistRequiredTest.Rows.Count-1)
                        {
                            return;
                        }
                        intTestCount++;
                    }
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    

    protected void btnAddRequiredTest_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        objRequiredTest = new Bill_Sys_RequiredTest();
       string strPatientId;

       //Create a new DataSet Object
       DataSet oDataSet = new DataSet();

        try
        {
            //'Use the Tables.add method to add a datatable named "MST_HOLIDAYS"
            DataTable OTestTable = oDataSet.Tables.Add("MST_REQUIRED_TEST");

            OTestTable.Columns.Add("REQUIRED_TEST_ID", typeof(System.String));
            OTestTable.Columns.Add("CASE_ID", typeof(System.String));
            OTestTable.Columns.Add("PROCEDUREGROUP_ID", typeof(System.String));
            OTestTable.Columns.Add("TEST_DATE", typeof(System.DateTime));

            DataRow drowItem;

            strPatientId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            //************ Add Rows ************
            drowItem = OTestTable.NewRow();
           

            foreach (GridViewRow rowItem in grdRequiredTest.Rows)
            {
                CheckBox chkGroup ;
                chkGroup = (CheckBox)(rowItem.Cells[0].FindControl("chkGroup"));    
             
                if (chkGroup.Checked && chkGroup.Enabled)    
                {        
                    drowItem = OTestTable.NewRow();
                    drowItem["REQUIRED_TEST_ID"] = grdRequiredTest.DataKeys[rowItem.RowIndex]["REQUIRED_TEST_ID"].ToString();
                    drowItem["CASE_ID"] = strPatientId;
                    drowItem["PROCEDUREGROUP_ID"] = grdRequiredTest.DataKeys[rowItem.RowIndex]["SZ_PROCEDURE_GROUP_ID"].ToString();
                    drowItem["TEST_DATE"] = DateTime.Now.ToShortDateString();
                    OTestTable.Rows.Add(drowItem);   
                }
                
            }
            objRequiredTest.saveRequiredTestInformation(OTestTable);

           
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

    protected void btnClearTest_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (GridViewRow rowItem in grdRequiredTest.Rows)
            {
                CheckBox chkGroup;
                chkGroup = (CheckBox)(rowItem.Cells[0].FindControl("chkGroup"));
                chkGroup.Checked = false;
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

    protected void grdRequiredTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {                 
                
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
}
