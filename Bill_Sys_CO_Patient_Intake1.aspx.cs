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

public partial class Bill_Sys_CO_Patient_Intake1 : PageBase
{
    //Bill_Sys_PatientBO _bill_Sys_PatientBO;
    DataSet ds;

    Bill_Sys_CaseObject _obhcaseno = new Bill_Sys_CaseObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PATIENT_INTEK_CASE_ID"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                       
        string Patient_ID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID.ToString();
        if (Request.QueryString["EID"] != null)
        {
            Session["PATIENT_INTEK_CASE_ID"] = Request.QueryString["EID"].ToString();
        }
        
       // TXT_CASE_ID.Text = Session["PATIENT_INTEK_CASE_ID"].ToString();
        TXT_CASE_ID.Text = Session["PATIENT_INTEK_CASE_ID"].ToString();
        if (!Page.IsPostBack)
        {
          // LoadPatientInformation();
            //LoadData();
            GetPatientDetails(Patient_ID);
            LoadData();
            TXT_CASE_ID1.Text = _obhcaseno.SZ_CASE_NO;
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
            _objEdit.Primary_Value = TXT_CASE_ID.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "Patient_Intake1.xml";
            _objEdit.LoadData();

            //if (txtrdblstPATIENT_MALE.Text != "")
            //{
            //    if (txtrdblstPATIENT_MALE.Text.Equals("Male"))                
            //    {
            //        rdblstPATIENT_MALE.SelectedValue = "0";
            //    }
            //    else {
            //        rdblstPATIENT_MALE.SelectedValue = "1";
            //   }
                 
            //}
       
             if (txtrdblstPATIENT_SINGLE.Text != "")
                {
                    rdblstPATIENT_SINGLE.SelectedValue = txtrdblstPATIENT_SINGLE.Text;
                }
             
                    if (txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text != "")
                    {
                        rdblstPATIENT_RELATIONSHIP_TO_INSURED.SelectedValue = txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text;
                    }
                   
                        if (txtrdblstAUTO_INJURY_ROLE.Text != "")
                        {
                            rdblstAUTO_INJURY_ROLE.SelectedValue = txtrdblstAUTO_INJURY_ROLE.Text;
                        }
                       
                            if (txtrdblstREPORT_INJURY_YES_NO.Text != "")
                            {
                                rdblstREPORT_INJURY_YES_NO.SelectedValue = txtrdblstREPORT_INJURY_YES_NO.Text;
                            }
                            
                                if (txtrdblstHOSPITALIZED_YES_NO.Text != "")
                                {
                                    rdblstHOSPITALIZED_YES_NO.SelectedValue = txtrdblstHOSPITALIZED_YES_NO.Text;
                                }
                                
                                    if (txtrdblstX_RAYS_YES_NO.Text != "")
                                    {
                                        rdblstX_RAYS_YES_NO.SelectedValue = txtrdblstX_RAYS_YES_NO.Text;
                                    }
                                  
                                        if (txtrdblstWERE_YOU_WORKING.Text != "")
                                        {
                                            rdblstWERE_YOU_WORKING.SelectedValue = txtrdblstWERE_YOU_WORKING.Text;
                                        }


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
    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            

            SaveOperation _objsave = new SaveOperation();
            _objsave.WebPage = this.Page;
            _objsave.Xml_File = "Patient_Intake1.xml";
            _objsave.SaveMethod();
           // LoadData();
           
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
        
        Response.Redirect("Bill_Sys_CO_Patient_Intake2.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    public void LoadPatientInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = Session["PATIENT_INTEK_CASE_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "PatientIntek_Information.xml";
            _editOperation.LoadData();
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
    public void GetPatientDetails(string Patient_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataSet _pateintList = GetPatientDetailList(Patient_ID, "LIST");
        try
        {
            if (_pateintList.Tables[0].Rows.Count > 0)
            {
                TXT_PATIENT_FIRST_NAME.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                TXT_PATIENT_FIRST_NAME1.Text = TXT_PATIENT_FIRST_NAME.Text;
                TXT_PATIENT_LAST_NAME.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                TXT_PATIENT_LAST_NAME1.Text = TXT_PATIENT_LAST_NAME.Text;
                TXT_PATIENT_MI.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                TXT_PATIENT_MI1.Text = TXT_PATIENT_MI.Text;
               // TXT_PATIENT_TITLE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                TXT_PATIENT_ADDRESS.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                TXT_PATIENT_ADDRESS1.Text = TXT_PATIENT_ADDRESS.Text;
                TXT_PATIENT_CITY.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                TXT_PATIENT_CITY1.Text = TXT_PATIENT_CITY.Text;
                TXT_PATIENT_STATE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                TXT_PATIENT_STATE1.Text = TXT_PATIENT_STATE.Text;
                TXT_PATIENT_ZIP.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                TXT_PATIENT_ZIP1.Text = TXT_PATIENT_ZIP.Text;
                TXT_PATIENT_HOME_PHONE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                TXT_PATIENT_HOME_PHONE1.Text = TXT_PATIENT_HOME_PHONE.Text;
                TXT_PATIENT_AGE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                TXT_PATIENT_AGE1.Text = TXT_PATIENT_AGE.Text;
                txtrdblstPATIENT_MALE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                if (txtrdblstPATIENT_MALE.Text.Equals("Male"))
                {
                    rdblstPATIENT_MALE.SelectedValue = "0";
                    txtrdblstPATIENT_MALE.Text = "0";
                }
                else
                {
                    rdblstPATIENT_MALE.SelectedValue = "1";
                    txtrdblstPATIENT_MALE.Text = "1";
                }
                TXT_PATIENT_SOC_SEC.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                TXT_PATIENT_SOC_SEC1.Text = TXT_PATIENT_SOC_SEC.Text;
                TXT_PATIENT_DATE_OF_BIRTH.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                TXT_PATIENT_DATE_OF_BIRTH1.Text = TXT_PATIENT_DATE_OF_BIRTH.Text;
                TXT_EMPLOYER.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                TXT_EMPLOYER1.Text = TXT_EMPLOYER.Text;
                TXT_ADDRESS.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                TXT_ADDRESS1.Text = TXT_ADDRESS.Text;
                TXT_CITY.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                TXT_CITY1.Text = TXT_CITY.Text;
                TXT_STATE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                TXT_STATE1.Text = TXT_STATE.Text;
                TXT_ZIP.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                TXT_ZIP1.Text = TXT_ZIP.Text;
                TXT_CASE_ID1.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                TXT_BUSINESS_PHONE1.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                _pateintList.Tables.Clear();
                _pateintList = GetPatientDetailList(Patient_ID, "ACCIDENT");
                TXT_DATE_OF_INJURY.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                TXT_DATE_OF_INJURY1.Text = TXT_DATE_OF_INJURY.Text;
                TXT_HOW_INJURY_HAPPEN.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                TXT_HOW_INJURY_HAPPEN1.Text = TXT_HOW_INJURY_HAPPEN.Text;
                txtrdblstAUTO_INJURY_ROLE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                if (txtrdblstAUTO_INJURY_ROLE.Text.Equals("1"))
                {
                    rdblstAUTO_INJURY_ROLE.SelectedValue = "0";
                    txtrdblstAUTO_INJURY_ROLE.Text = "0";
                }
                else if (txtrdblstAUTO_INJURY_ROLE.Text.Equals("2"))
                {
                    rdblstAUTO_INJURY_ROLE.SelectedValue = "1";
                    txtrdblstAUTO_INJURY_ROLE.Text = "1";
                }
                else if (txtrdblstAUTO_INJURY_ROLE.Text.Equals("3"))
                {
                    rdblstAUTO_INJURY_ROLE.SelectedValue = "2";
                    txtrdblstAUTO_INJURY_ROLE.Text = "2";
                }
                else
                {
                    rdblstAUTO_INJURY_ROLE.SelectedValue = "3";
                    txtrdblstAUTO_INJURY_ROLE.Text = "3";
                }
                _pateintList.Tables.Clear();
                _pateintList = GetPatientDetailList(Patient_ID, "INSURANCE");
                TXT_POLICY_NUMBER.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                TXT_POLICY_NUMBER1.Text = TXT_POLICY_NUMBER.Text;
                TXT_CLAIM_NUMBER.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                TXT_CLAIM_NUMBER1.Text = TXT_CLAIM_NUMBER.Text;
                TXT_ATTORNEY_NAME.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                TXT_ATTORNEY_NAME1.Text = TXT_ATTORNEY_NAME.Text;
                TXT_ATTORNEY_PHONE.Text = _pateintList.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                TXT_ATTORNEY_PHONE1.Text = TXT_ATTORNEY_PHONE.Text;

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

    public DataSet GetPatientDetailList(string patientid, string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection  sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
       
        try
        {
            sqlCon.Open();
         SqlCommand  sqlCmd = new SqlCommand("SP_GET_PATIENT_INTAKE_DETAILS_LIST", sqlCon);
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
    protected void rdblstPATIENT_MALE_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstPATIENT_MALE.Text = rdblstPATIENT_MALE.SelectedValue;
    }
    protected void rdblstPATIENT_SINGLE_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstPATIENT_SINGLE.Text = rdblstPATIENT_SINGLE.SelectedValue;
    }
    protected void rdblstPATIENT_RELATIONSHIP_TO_INSURED_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstPATIENT_RELATIONSHIP_TO_INSURED.Text = rdblstPATIENT_RELATIONSHIP_TO_INSURED.SelectedValue;
    }
    protected void rdblstAUTO_INJURY_ROLE_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstAUTO_INJURY_ROLE.Text = rdblstAUTO_INJURY_ROLE.SelectedValue;
    }
    protected void rdblstREPORT_INJURY_YES_NO_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstREPORT_INJURY_YES_NO.Text = rdblstREPORT_INJURY_YES_NO.SelectedValue;
    }
    protected void rdblstHOSPITALIZED_YES_NO_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstHOSPITALIZED_YES_NO.Text = rdblstHOSPITALIZED_YES_NO.SelectedValue;
    }
    protected void rdblstX_RAYS_YES_NO_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstX_RAYS_YES_NO.Text = rdblstX_RAYS_YES_NO.SelectedValue;
    }
    protected void rdblstWERE_YOU_WORKING_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtrdblstWERE_YOU_WORKING.Text = rdblstWERE_YOU_WORKING.SelectedValue;
    }
}
