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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Bill_Sys_CO_Patient_Intake2 : PageBase
{
   
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
    //    Session["PATIENT_INTEK_EVENT_ID"] = "11970";
         string Patient_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID.ToString();
        if (Request.QueryString["EID"] != null)
        {
            Session["PATIENT_INTEK_CASE_ID"] = Request.QueryString["EID"].ToString();
        }
        TXT_I_EVENT.Text = Session["PATIENT_INTEK_CASE_ID"].ToString();

        if (!Page.IsPostBack)
        {
       //     
            InsuranceInformation(Patient_ID);
            LoadData();
        }
  
    }

    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EditOperation _objEdit = new EditOperation();
            _objEdit.Primary_Value = TXT_I_EVENT.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "Patient_Intake2.xml";
            _objEdit.LoadData();

            if (txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text != "")
            {
                rdblstPATIENT_RELATIONSHIP_TO_INSURED.SelectedValue = txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text;
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
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CO_Patient_Intake1.aspx");
    }


    
    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text = rdblstPATIENT_RELATIONSHIP_TO_INSURED.SelectedValue;
            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "Patient_Intake2.xml";
            _objsave.SaveMethod();
         //   LoadData();
           
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
        
        Response.Redirect("Bill_Sys_CO_Patient_Intake3.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void InsuranceInformation(string Patiend_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet _pateintList = GetPatientDetailList(Patiend_ID, "INSURANCE");
            TXT_INSURANCE_COMPANY_NAME.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            TXT_INSURANCE_COMPANY_NAME1.Text = TXT_INSURANCE_COMPANY_NAME.Text;
            TXT_INSURANCE_POLICY_NUMBER.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            TXT_INSURANCE_POLICY_NUMBER1.Text = TXT_INSURANCE_POLICY_NUMBER.Text;
            TXT_INSURED_ADDRESS.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            TXT_INSURED_ADDRESS1.Text = TXT_INSURED_ADDRESS.Text;
            TXT_INSURANCE_PHONE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            TXT_INSURANCE_PHONE1.Text = TXT_INSURANCE_PHONE.Text;
            TXT_INSURED_NAME.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            TXT_INSURED_NAME1.Text = TXT_INSURED_NAME.Text;

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
    public DataSet GetPatientDetailList(string patientid, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());

        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_PATIENT_INTAKE_DETAILS_LIST", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;


            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_ID", patientid);
            sqlCmd.Parameters.AddWithValue("@FLAG", Flag);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void rdblstPATIENT_RELATIONSHIP_TO_INSURED_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text = rdblstPATIENT_RELATIONSHIP_TO_INSURED.SelectedValue;
    }
}
