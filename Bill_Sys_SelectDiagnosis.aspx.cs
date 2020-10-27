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

public partial class Bill_Sys_AssociateProcedureCode : PageBase
{
    Bill_Sys_DigosisCodeBO _bill_Sys_DigosisCodeBO;
    Bill_Sys_AssociateDiagnosisCodeBO _associateDiagnosisCodeBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
           // if (!IsPostBack)
           // {
               if (Session["SELECTED_SERVICES"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = ((DataTable)Session["SELECTED_SERVICES"]);
                    grdSelectedServices.DataSource = dt;
                    grdSelectedServices.DataBind();
                }
                _associateDiagnosisCodeBO = new Bill_Sys_AssociateDiagnosisCodeBO();
                try
                {
                    DataSet ds = new DataSet();
                    ds = _associateDiagnosisCodeBO.GetDiagnosisCode(Session["SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["DOCTOR_ID"].ToString(), "GET_DOCTOR_DIAGNOSIS_CODE");

                    grdDiagnosisCode.DataSource = ds.Tables[0]; //_associateDiagnosisCodeBO.GetDoctorDiagnosisCode(id, flag);
                    grdDiagnosisCode.DataBind();
                   
                   
                }
                catch (Exception ex)
                {
                    string strError = ex.Message.ToString();
                    strError = strError.Replace("\n", " ");
                    Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
                }
           // }
            lblMsg.Text = "";
            ErrorDiv.InnerText = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_AssociateProcedureCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void SelectOnlyOne(object sender, EventArgs e)
    {
        string m_ClientID = "";
        RadioButton rb = new RadioButton();

        rb = (RadioButton)(sender);
        m_ClientID = rb.ClientID;

        foreach (DataGridItem i in grdDiagnosisCode.Items)
        {
            rb = (RadioButton)(i.FindControl("rdbSelect"));
            rb.Checked = false;

            if (m_ClientID == rb.ClientID)
            {
                rb.Checked = true;
            }

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
            string diagnosisCode = "";
            string description = "";
            if (txtSearchText.Text != "")
            {
                if (chkDiagnosisCode.Checked == true)
                {
                    diagnosisCode = txtSearchText.Text;
                }
                else if (chkDiagnosisCodeDescription.Checked == true)
                {
                    description = txtSearchText.Text;
                }
            }
            //_bill_Sys_DigosisCodeBO = new Bill_Sys_DigosisCodeBO();
            DataSet ds;// = new DataSet();
            //ds = _bill_Sys_DigosisCodeBO.GeDignosisCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode,description);
            //grdDiagnosisCode.DataSource = ds.Tables[0];
            //grdDiagnosisCode.DataBind();
            _bill_Sys_DigosisCodeBO = new Bill_Sys_DigosisCodeBO();
             ds = new DataSet();
             ds = _bill_Sys_DigosisCodeBO.GeProcedureCode_List(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, diagnosisCode, description, Session["DOCTOR_ID"].ToString());
            grdProcedureCode.DataSource = ds.Tables[0];
            grdProcedureCode.DataBind();
        }
        catch(Exception ex)
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

    public void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
           
            RadioButton rb = new RadioButton();
           
             Boolean _prStatus = false;
            CheckBox ch = new CheckBox();
            foreach (DataGridItem i in grdProcedureCode.Items)
            {
                ch = (CheckBox)(i.FindControl("chkSelect"));
                if (ch.Checked == true)
                {
                   
                    _prStatus = true;
                    break;
                }
            }
           
            if (_prStatus == false)
            {
                ErrorDiv.InnerText = "Please select procedure code";
            }
            else
            {

                Createtable();
               
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

    private void Createtable()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            ArrayList arrDiagnosisObject = new ArrayList();
       
            DataTable dt = new DataTable();
            dt.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            dt.Columns.Add("DT_DATE_OF_SERVICE");
        
            dt.Columns.Add("SZ_PROCEDURE_ID");
            dt.Columns.Add("SZ_PROCEDURAL_CODE");
            dt.Columns.Add("SZ_CODE_DESCRIPTION");
            dt.Columns.Add("FLT_AMOUNT");
            dt.Columns.Add("FACTOR_AMOUNT");
            dt.Columns.Add("FACTOR");
            dt.Columns.Add("PROC_AMOUNT");
            dt.Columns.Add("DOCT_AMOUNT");
            dt.Columns.Add("I_UNIT");
            dt.Columns.Add("SZ_TYPE_CODE_ID");
            RadioButton rb;
            CheckBox ch;// = new RadioButton();
            DataRow dr;

            rb = new RadioButton();
        
            ch = new CheckBox();
            DateTime endDate;
            DateTime startDate = Convert.ToDateTime(Session["Date_Of_Service"].ToString());
            if (Session["TODate_Of_Service"] != null) { endDate = Convert.ToDateTime(Session["TODate_Of_Service"].ToString()); }
            else { endDate = Convert.ToDateTime(Session["Date_Of_Service"].ToString()); }
            TimeSpan ts = endDate.Subtract(startDate);
            DateTime currentDate = startDate;
            if (Session["SELECTED_SERVICES"] != null)
            {
                DataTable dataServices = new DataTable();
                dataServices = ((DataTable)Session["SELECTED_SERVICES"]);
                foreach (DataRow dataRowServices in dataServices.Rows)
                {
                    dr = dt.NewRow();
                    dr["SZ_BILL_TXN_DETAIL_ID"] = dataRowServices["SZ_BILL_TXN_DETAIL_ID"];
                    dr["DT_DATE_OF_SERVICE"] = dataRowServices["DT_DATE_OF_SERVICE"];
                    dr["SZ_PROCEDURE_ID"] = dataRowServices["SZ_PROCEDURE_ID"];
                    dr["SZ_PROCEDURAL_CODE"] = dataRowServices["SZ_PROCEDURAL_CODE"];
                    dr["SZ_CODE_DESCRIPTION"] = dataRowServices["SZ_CODE_DESCRIPTION"];
                    dr["FLT_AMOUNT"] = dataRowServices["FLT_AMOUNT"];
                    dr["FACTOR_AMOUNT"] = dataRowServices["FACTOR_AMOUNT"];
                    dr["FACTOR"] = dataRowServices["FACTOR"];
                    dr["PROC_AMOUNT"] = dataRowServices["PROC_AMOUNT"];
                    dr["DOCT_AMOUNT"] = dataRowServices["DOCT_AMOUNT"];
                    dr["I_UNIT"] = dataRowServices["I_UNIT"];

                    dt.Rows.Add(dr);
                }
            }
            if (Session["TEMP_SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable dataServices = new DataTable();
                dataServices = ((DataTable)Session["TEMP_SELECTED_DIA_PRO_CODE"]);
                foreach (DataRow dataRowServices in dataServices.Rows)
                {
                    dr = dt.NewRow();
                    dr["SZ_BILL_TXN_DETAIL_ID"] = dataRowServices["SZ_BILL_TXN_DETAIL_ID"];
                    dr["DT_DATE_OF_SERVICE"] = dataRowServices["DT_DATE_OF_SERVICE"];
                    dr["SZ_PROCEDURE_ID"] = dataRowServices["SZ_PROCEDURE_ID"];
                    dr["SZ_PROCEDURAL_CODE"] = dataRowServices["SZ_PROCEDURAL_CODE"];
                    dr["SZ_CODE_DESCRIPTION"] = dataRowServices["SZ_CODE_DESCRIPTION"];
                    dr["FLT_AMOUNT"] = dataRowServices["FLT_AMOUNT"];
                    dr["FACTOR_AMOUNT"] = dataRowServices["FACTOR_AMOUNT"];
                    dr["FACTOR"] = dataRowServices["FACTOR"];
                    dr["PROC_AMOUNT"] = dataRowServices["PROC_AMOUNT"];
                    dr["DOCT_AMOUNT"] = dataRowServices["DOCT_AMOUNT"];
                    dr["I_UNIT"] = dataRowServices["I_UNIT"];

                    dt.Rows.Add(dr);
                }
            }
            for (int i = 0; i <= ts.Days; i++)
            {

                foreach (DataGridItem j in grdProcedureCode.Items)
                {
                    ch = (CheckBox)(j.FindControl("chkSelect"));
                    if (ch.Checked == true)
                    {
                        dr = dt.NewRow();
                        dr["SZ_BILL_TXN_DETAIL_ID"] = "";
                        dr["DT_DATE_OF_SERVICE"] = currentDate.Date.ToShortDateString();// Session["Date_Of_Service"].ToString();
                     
                        dr["SZ_PROCEDURE_ID"] = j.Cells[1].Text.ToString();

                        dr["SZ_PROCEDURAL_CODE"] = j.Cells[2].Text.ToString();
                        dr["SZ_CODE_DESCRIPTION"] = j.Cells[3].Text.ToString();
                        if (Convert.ToDecimal(j.Cells[5].Text.ToString()) > 0) { dr["FLT_AMOUNT"] = (Convert.ToDecimal(j.Cells[5].Text.ToString()) * Convert.ToDecimal(j.Cells[6].Text.ToString())).ToString(); } else { dr["FLT_AMOUNT"] = (Convert.ToDecimal(j.Cells[4].Text.ToString()) * Convert.ToDecimal(j.Cells[6].Text.ToString())).ToString(); }
                        if (Convert.ToDecimal(j.Cells[5].Text.ToString()) > 0) { dr["FACTOR_AMOUNT"] = j.Cells[5].Text.ToString(); } else { dr["FACTOR_AMOUNT"] = j.Cells[4].Text.ToString(); }
                       
                        dr["FACTOR"] = j.Cells[6].Text.ToString();

                        dr["PROC_AMOUNT"] = j.Cells[4].Text.ToString();
                        if (j.Cells[5].Text.ToString() != "&nbsp;" && j.Cells[5].Text.ToString() != "") { dr["DOCT_AMOUNT"] = j.Cells[5].Text.ToString(); } else { dr["DOCT_AMOUNT"] = "0"; }
                        dr["I_UNIT"] = "";
                        dr["SZ_TYPE_CODE_ID"] = "TC000000000000000016";//NIMRAL
                        dt.Rows.Add(dr);
                     
                    }
                }
                currentDate = currentDate.AddDays(1);
            }
            Session["TEMP_SELECTED_DIA_PRO_CODE"] = dt;
            Session["SELECTED_DIA_PRO_CODE"] = dt;// arrDiagnosisObject;
            lblMsg.Text = "procedure code added successfully";
        }
        catch(Exception ex)
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["SELECTED_DIA_PRO_CODE"] = null;
            Session["TEMP_SELECTED_DIA_PRO_CODE"] = null;
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


    protected void Button2_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["TEMP_SELECTED_DIA_PRO_CODE"] = null;
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.opener.postbackFunc(); window.close();", true);
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
    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + Session["SZ_CASE_ID"].ToString() + "&DoctorID=" + Session["DOCTOR_ID"].ToString() + "','','titlebar=no,menubar=yes,resizable,alwaysRaised,scrollbars=yes,width=800,height=800,center ,top=0,left=0'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
        }
        catch(Exception ex)
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
