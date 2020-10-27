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
using System.IO;
using System.Data;
using System.Data.SqlClient;


public partial class Bill_Sys_Synoptic : PageBase
{
    private SaveOperation _saveOperation;
    Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
    string sz_CompanyName = "";
    string sz_UserID = "";
    string sz_CompanyID = "";
    string eventId = "";
    Bill_Sys_CheckoutBO _obj_CheckOutBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {            
            //TUSHAR:- To Show Complaints Of Particular Speciality
            Bill_Sys_CheckoutBO _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            DataSet DoctorName = new DataSet();
            string UserId = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
            DoctorName = _obj_CheckOutBO.GetDoctorUserID(UserId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            txtDoctorId.Text = DoctorName.Tables[0].Rows[0][0].ToString();
            if (txtDoctorId.Text != "" && txtDoctorId.Text != null)
            {
                extddlArea.Flag_ID = txtDoctorId.Text;
            }
            //End Of Code

            if (!IsPostBack)
            {
                Session["Patient_Sign"] = "";
                if (Request.QueryString["Flag"] != "SYN")
                {
                    Session["AC_SYNAPTIC_EVENT_ID"] = "";
                }
                else
                {
                    btnDiagnosysCodes.Enabled = true;
                }
                if ((Bill_Sys_CaseObject)Session["CASE_OBJECT"] != null)
                {
                    Session["company"] = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    if (Request.QueryString["CaseID"] != null)
                    {
                        if (Request.QueryString["CaseID"].ToString() != "")
                        {
                            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                            _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(Request.QueryString["CaseID"].ToString(), "");
                            _bill_Sys_CaseObject.SZ_CASE_ID = Request.QueryString["CaseID"].ToString();
                            if (Request.QueryString["cmp"] != null)
                            {
                                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, Request.QueryString["cmp"].ToString());
                                Session["company"] = Request.QueryString["cmp"].ToString();
                            }
                            else
                            {
                                _bill_Sys_CaseObject.SZ_CASE_NO = _caseDetailsBO.GetCaseNo(_bill_Sys_CaseObject.SZ_CASE_ID, Session["company"].ToString());
                            }
                            _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                            _bill_Sys_CaseObject.SZ_COMAPNY_ID = Session["company"].ToString();
                            Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                        }
                    }
                }
                TXT_PATIENT_FIRST_NAME.Text= ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
            }


            this.con.SourceGrid = grdProcedure;
            this.txtSearchBox.SourceGrid = grdProcedure;
            this.grdProcedure.Page = this.Page;
            this.grdProcedure.PageNumberList = this.con;
            //change by kunal
            this.con1.SourceGrid = gv_Complaints;
            //this.txtSearchBox.SourceGrid=gv_Complaints;
            this.gv_Complaints.Page = this.Page;
            this.gv_Complaints.PageNumberList = this.con1;
            //-----
            sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();

            sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();

            txtDoctorId.Text = GetDoctorUserID(sz_UserID, sz_CompanyID);
            TXT_CURRENT_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;

            if (!IsPostBack)
            {
                grdProcedure.XGridBind();
                gv_Complaints.XGridBindSearch();

                if (TXT_DOA.Text != "")
                {
                    TXT_DOA.Text = Convert.ToDateTime(TXT_DOA.Text).ToString("MM/dd/yyyy");
                }
                TXT_CURRENT_DATE.Text = Convert.ToDateTime(TXT_CURRENT_DATE.Text).ToString("MM/dd/yyyy");

                grdProcedure.Columns[2].Visible = false;
            }
            #region "check version readonly or not"
            string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
            if (app_status.Equals("True"))
            {
                Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
                cv.MakeReadOnlyPage("Bill_Sys_AC_Acupuncture_Followup.aspx");
            }
            #endregion

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

    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            AddVisit();
            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            ArrayList objGetEvent = new ArrayList();
            objGetEvent.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objGetEvent.Add(txtDoctorId.Text);
            objGetEvent.Add(txtCompanyID.Text);
            int eventid = _bill_Sys_Calender.GetEventID(objGetEvent);
            txtEventID.Text = Convert.ToString(eventid);
            Session["AC_SYNAPTIC_EVENT_ID"] = txtEventID.Text;
            grdProcedure.Columns[2].Visible = true;
            DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(txtEventID.Text);
            //_saveOperation = new SaveOperation();
            //_saveOperation.WebPage = this.Page;
            //_saveOperation.Xml_File = "SYNFollow.xml";
            //_saveOperation.SaveMethod();            

            //TUSHAR:- To Save Visit From SYN Doctor Screen
            ArrayList _VisitInfo = new ArrayList();
            _VisitInfo.Add(txtEventID.Text);
            _VisitInfo.Add(TXT_PATIENT_LAST_NAME.Text);
            _VisitInfo.Add(TXT_PATIENT_FIRST_NAME.Text);
            _VisitInfo.Add(TXT_DOA.Text);
            _VisitInfo.Add(TXT_CURRENT_DATE.Text);
            _VisitInfo.Add(DrpDoctorNotes.SelectedItem.Text.ToString() + " " + txtDoctorNotes.Text);
            _VisitInfo.Add(DrpTreatmentTime.SelectedItem.Text.ToString());
            _VisitInfo.Add(Slider2.Text);
            _VisitInfo.Add(Slider4.Text);
            _VisitInfo.Add(Listbias.SelectedValue.ToString());
            _VisitInfo.Add(Slider1.Text);
            _VisitInfo.Add(extddlArea.Selected_Text.ToString());
            _objCheckoutBO.Add_SYN_Visit(_VisitInfo);
            //End Of Code 
            

            for (int i = 0; i < grdProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Rows[i].FindControl("ChkProcId");
                if (chkDelete1.Checked)
                {
                    string ProcId = grdProcedure.Rows[i].Cells[2].Text;
                    SaveValues(ProcId);
                }
            }

            // check button for save or update
            if (css_btnSave.Text == "UPDATE TREATMENTS")
            {
                // FillPDFValue(txtEventID.Text, txtCompanyID.Text, sz_CompanyName);
            }
            else if (css_btnSave.Text == "SAVE TREATMENTS")
            {
                //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_AC_Accu_FollowUP_PatientSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
                //  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_AC_Accu_Followup_Signature.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            }

            bool _checkImage = true;
            _checkImage = _objCheckoutBO.checkImageForSynFollowup(txtEventID.Text);


            if (_checkImage == true)
            {
                css_btnSave.Text = "UPDATE TREATMENTS";
            }
            else
            {
                css_btnSave.Text = "SAVE TREATMENTS";
            }
            grdProcedure.Columns[2].Visible = false;
            btnDiagnosysCodes.Enabled = true;
            savecomplaints();
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
    public void AddVisit()
    {
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        ArrayList objAdd = new ArrayList();
        objAdd.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);//Case Id
        objAdd.Add(TXT_CURRENT_DATE.Text);//Appointment date
        objAdd.Add("8.30");//Appointment time
        objAdd.Add("");//Notes
        // objAdd.Add(extddlDoctor.Text); vivek 2/2/10
        objAdd.Add(txtDoctorId.Text);
        objAdd.Add("TY000000000000000003");//Type
        objAdd.Add(txtCompanyID.Text);
        objAdd.Add("AM");
        objAdd.Add("9.00");
        objAdd.Add("AM");
        objAdd.Add("2");

        // add visit for FU only
        Bill_Sys_PatientVisitBO objPV = new Bill_Sys_PatientVisitBO();
        String szDefaultVisitTypeID = objPV.GetVisitType(txtCompanyID.Text, "GET_FU_VALUE");
        objAdd.Add(szDefaultVisitTypeID);//Type

        _bill_Sys_Calender.SaveEvent(objAdd, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
    }

    protected void css_btnSign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SYN_Accu_Followup_SignaturePatient.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
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

    public void SaveValues(string index)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            ArrayList _objArray = new ArrayList();
            _objArray.Add(index);
            _objArray.Add(txtEventID.Text);
            _objArray.Add(txtCompanyID.Text);
            // obj.saveProcedureCodes(_objArray);
            save_Procedure_Codes(_objArray);
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
            _editOperation.Primary_Value = "8";
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "AC_Followup_PatientDetails.xml";
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

    public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //string sz_CaseID = Session["ChkOutCaseID"].ToString();
        DataSet dsObj = _objCheckoutBO.PatientName(txtEventID.Text);
        Session["AC_Followup_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        string sz_CaseID = Session["AC_Followup_CaseID"].ToString();
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["Accu_FollowUp_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["Accu_FollowUp_PDF_Path"].ToString();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/AC/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

            if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            }

            Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
            ArrayList objAL = new ArrayList();
            objAL.Add(sz_CaseID);
            objAL.Add(strGenFileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(EventID);
            objCheckoutBO.save_AC_REEVAL_DM(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect(szOpenFilePath, false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
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
       
        return strGenFileName;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void LoadData()
    {
        EditOperation _loadEdit = new EditOperation();
        _loadEdit.Primary_Value = Session["AC_SYNAPTIC_EVENT_ID"].ToString();
        _loadEdit.WebPage = this.Page;
        _loadEdit.Xml_File = "SYNFollow.xml";
        _loadEdit.LoadData();
    }
    public string GetDoctorUserID(string sz_UserID, string sz_CompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataReader dr;
        SqlCommand comm;

        string DoctId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();

        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_Doctor_UserID", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", sz_UserID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GETDOCTORID");
            //sqlda = new SqlDataAdapter(comm);
            //sqlda.Fill(ds);
            //return ds;
            dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DoctId = dr[0].ToString();
                }
            }
            return DoctId;
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
            return DoctId;
        }

        //finally { conn.Close(); }
        return DoctId;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet Get_Proc_Code(string i_Event_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string ProcId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_get_Proccode_id", conn);
            comm.Parameters.AddWithValue("@I_EVENT_ID", i_Event_id);
            comm.Parameters.AddWithValue("@flag", "getProcCode");
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", "");
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;
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
            return ds;
        }

        //finally { conn.Close(); }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        return ds;
    }

    public DataSet Get_Status_Code(string DoctorId, string eventId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string StatusId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_get_Proccode_id", conn);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", DoctorId);
            comm.Parameters.AddWithValue("@flag", "getStatusCode");
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;
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
        

        //finally { conn.Close(); }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void save_Procedure_Codes(ArrayList p_objArrayList)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataReader dr;
            SqlCommand comm;
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandText = "sp_insert_proc_code_id";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", Convert.ToInt32(p_objArrayList[1].ToString()));
            comm.Parameters.AddWithValue("@flag", "BT_UPDATE");
            //  comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[2].ToString());
            comm.ExecuteNonQuery();

            #endregion
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

    protected void css_btnFinalize_Click(object sender, EventArgs e)
    {
        if (Session["Patient_Sign"] == "Sign")
        {
            // function to update bt_finalize
            bool _checkImage = true;
            _checkImage = _objCheckoutBO.checkImageForSynFollowup(Session["AC_SYNAPTIC_EVENT_ID"].ToString());
            DataSet dsProcCode = Get_Proc_Code(Session["AC_SYNAPTIC_EVENT_ID"].ToString());
            if (dsProcCode != null)
            {
                if (dsProcCode.Tables[0].Rows.Count != 0 && _checkImage == true)
                {
                    ArrayList _objArray = new ArrayList();
                    _objArray.Add(Session["AC_SYNAPTIC_EVENT_ID"].ToString());
                    // _objArray.Add(txtCompanyID.Text);
                    Update_BT_Finalize(_objArray);
                }
            }
            Response.Redirect("Bill_Sys_CheckOut.aspx");
            //
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please Sign Before Finalize');", true);
        }

    }
    public void Update_BT_Finalize(ArrayList p_objArrayList)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataReader dr;
            SqlCommand comm;
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandText = "sp_insert_proc_code_id";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            //comm.Parameters.AddWithValue("@SZ_PROC_CODE", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", Convert.ToInt32(p_objArrayList[0].ToString()));
            comm.Parameters.AddWithValue("@flag", "BT_FINALIZE");
            //  comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objArrayList[2].ToString());
            comm.ExecuteNonQuery();

            #endregion
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


    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        savecomplaints();
    }
    public void savecomplaints()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        SqlTransaction TRAN;
        sqlCon.Open();
        TRAN = sqlCon.BeginTransaction();
        try
        {
            SqlCommand sqlCmdDelete = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", sqlCon);
            sqlCmdDelete.CommandType = CommandType.StoredProcedure;
            sqlCmdDelete.CommandTimeout = 0;
            sqlCmdDelete.Transaction = TRAN;
            sqlCmdDelete.Parameters.AddWithValue("@SZ_DOCTOR_ID", txtDoctorId.Text);
            sqlCmdDelete.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmdDelete.Parameters.AddWithValue("@FLAG", "DELETE");
            sqlCmdDelete.ExecuteNonQuery();

            for (int i = 0; i < gv_Complaints.Rows.Count; i++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)gv_Complaints.Rows[i].FindControl("ChkComplaintId");
                if (chk.Checked == true)
                {
                    SqlCommand sqlCmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.Transaction = TRAN;
                    sqlCmd.Parameters.AddWithValue("@SZ_EVENT_ID", Convert.ToInt32(Session["AC_SYNAPTIC_EVENT_ID"].ToString()));
                    sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", txtDoctorId.Text);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
                    sqlCmd.Parameters.AddWithValue("@SZ_COMPLAINT_ID", Convert.ToInt32(gv_Complaints.DataKeys[i][0].ToString()));
                    sqlCmd.Parameters.AddWithValue("@FLAG", "SAVE");
                    sqlCmd.ExecuteNonQuery();
                }
            }


            TRAN.Commit();

            usrMessage.PutMessage("Compaint Saved Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            //END CODE SAVE OR UPDATE TXN_COMPLAINT
        }
        catch (SqlException ex)
        {
            TRAN.Rollback();
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
                TRAN.Dispose();
                sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetComplaintId()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_COMPLAINT_DOCTOR_WISE", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", txtDoctorId.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmd.Parameters.AddWithValue("@FLAG", "GET");
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);
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

    protected void OnClick_btnDagnosys_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_AssociateDignosisCodeCaseTemplate.aspx?Flag=SYN");
    }

}
