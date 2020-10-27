using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Componend;
using System.Drawing;
using System.Text;

public partial class Bill_Sys_PatientVisitSummary : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    ListOperation _listOperation;

    Bill_Sys_PatientBO _bill_Sys_PatientBO;
    ArrayList objAdd;

    string case_id, list_case_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            btnViewReport.Attributes.Add("onClick", "return ShowConfirmation();");
            
            if (!IsPostBack)           
            {
              
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
         
                SearchList();
              
            }
            lblMsg.Text = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_TreatmentReport.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    private void SearchList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
         _reportBO =new Bill_Sys_ReportBO();
        try
        {
            DataSet objDT = new DataSet();
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                DataTable objDTL = new DataTable();
                objDT = _reportBO.GetPatientVisitSummary("SP_PATIENT_VISIT_SUMMARY_REPORT", txtCompanyID.Text);
                objDTL = DisplayLocationInGrid(objDT);
                grdCaseMaster.DataSource = objDTL;
                grdCaseMaster.DataBind();
                for (int i = 0; i < grdCaseMaster.Items.Count; i++)
                {
                    string str = grdCaseMaster.Items[i].Cells[2].Text.ToString();
                    str = str.ToString().Trim();
                    if (str.ToString().Trim() == "&nbsp;")
                    {
                        ((CheckBox)grdCaseMaster.Items[i].Cells[0].FindControl("chkSelect")).Visible = false;
                    }
                }
            }
            else
            {
                _listOperation.WebPage = this.Page;
                _listOperation.Xml_File = "SearchCase.xml";
                _listOperation.LoadList();
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

  

    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                       

            foreach(DataGridItem di in grdCaseMaster.Items)
            {
                if(((CheckBox)di.FindControl("chkSelect")).Checked)
                {
                    case_id = case_id + "'" + di.Cells[2].Text + "',";
                }
                
            }
            list_case_id = case_id.Substring(0, case_id.Length - 1);
            ///////////////////////
            DataTable dt=null;
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("GET_PATIENT_TREATMENT_DETAILS", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", list_case_id);
                sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", company_id);


                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                dt = new DataTable();
                sqlda.Fill(dt);

            }
            catch (SqlException ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            
            ///////////////////////
           
           Session["SpecialityCount"] = dt;

            Response.Write("<script language='javascript'>window.open('Bill_Sys_GetTreatment.aspx', 'TreatmentData', 'left=30,top=30');</script>");

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



    #region "Display Location wise patient in grid"

    public DataTable DisplayLocationInGrid(DataSet p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataTable objDT = new DataTable();
        try
        {



            objDT.Columns.Add("SZ_CASE_ID");
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PATIENT_ID");
            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_CASE_TYPE_NAME");
            objDT.Columns.Add("DT_DATE_OF_ACCIDENT");
            objDT.Columns.Add("DT_CREATED_DATE");
            objDT.Columns.Add("SZ_PATIENT_PHONE");
          

            DataRow objDR;
            string sz_Location_Name = "NA";




            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(sz_Location_Name))
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_ID"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DT_CREATED_DATE"] = objDS.Tables[0].Rows[i]["DT_CREATED_DATE"].ToString();
                    objDR["SZ_PATIENT_PHONE"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_PHONE"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();


                }
                else
                {


                    objDR = objDT.NewRow();

                    objDR["SZ_CASE_ID"] = "";
                    objDR["SZ_CASE_NO"] = "<b>Location Name</b>"; ;
                    objDR["SZ_PATIENT_ID"] = "";
                    objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString() + "<b>";
                    objDR["SZ_CASE_TYPE_NAME"] = "";
                    objDR["DT_DATE_OF_ACCIDENT"] = "";
                    objDR["DT_CREATED_DATE"] = "";
                    objDR["SZ_PATIENT_PHONE"] = "";

                    int count = grdCaseMaster.Items.Count;



                    objDT.Rows.Add(objDR);


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = objDS.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_ID"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = objDS.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DT_CREATED_DATE"] = objDS.Tables[0].Rows[i]["DT_CREATED_DATE"].ToString();
                    objDR["SZ_PATIENT_PHONE"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_PHONE"].ToString();
                    


                  
                    objDT.Rows.Add(objDR);

                    sz_Location_Name = objDS.Tables[0].Rows[i]["SZ_LOCATION_NAME"].ToString();





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
       
        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

}
