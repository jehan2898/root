using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using PTNotesPdf;
using PMNotesPdf;
using CHNotesPdf;
using ACNotesPdf;
using System.IO;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class ViewNotes : PageBase
{
    string eventId = "";
    string procedureGroupCode = "";
    string strsqlCon = ConfigurationSettings.AppSettings["Connection_String"].ToString();
    string companyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = "";
            try
            {
                //string caseId = "";
                string ProcedureGroupId = "";
                string url = ConfigurationManager.AppSettings["UrlOfpdf"].ToString();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                webapi.doctor.models.AppointmentRequest model = new webapi.doctor.models.AppointmentRequest();
                model.User = new webapi.doctor.da.gb.model.user.User();
                model.User.ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                model.User.UserName = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                model.User.Domain = "GogreenBills.com";
                model.User.Token = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME;
                model.User.Account = new webapi.doctor.da.gb.model.account.Account();
                model.User.Account.ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                Bill_Sys_Upload_VisitReport GetSpecialty = new Bill_Sys_Upload_VisitReport();
                ProcedureGroupId = GetSpecialty.GetDoctorSpecialty(Request.QueryString["Doc"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());

                model.User.Specialty = new webapi.doctor.da.gb.model.specialty.Specialty();
                model.User.Specialty.ID = ProcedureGroupId;
                model.User.Specialty.Code = GetSpecialty.GetDoctorSpecialtyCode(Request.QueryString["Doc"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
                model.User.Physician = new webapi.doctor.da.gb.model.physician.Physician();
                model.User.Physician.ID = Request.QueryString["Doc"].ToString();
                model.Appointment = new webapi.doctor.da.gb.model.appointment.Appointment();
                model.Appointment.ID = Convert.ToInt32(Request.QueryString["eid"].ToString());
                request.ContentType = "application/json";

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                using (var sw = new StreamWriter(request.GetRequestStream()))
                {
                    string json = serializer.Serialize(model);
                    sw.Write(json);
                    sw.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var sr = new StreamReader(response.GetResponseStream());
                // read the response stream as Text.
                var xml = sr.ReadToEnd();
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();  
                //Object obj =json_serializer.DeserializeObject(xml);
                ILResponse r = JsonConvert.DeserializeObject<ILResponse>(xml);
                string[] Path = r.Data[0].ToString().Split(',');
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewCHNotes", "<script type='text/javascript'>showpopup("+ Path [1].ToString().Replace("]","")+ "); </script>");
//                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "sandeep", string.Concat("<script type='text/javascript'>window.location.href='", Path[1].ToString(), "'</script>"));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
                lblMsg.Visible = true;
            }
        }

        //if (Request.QueryString["cid"] != null)
        //{
        //    caseId = Request.QueryString["cid"].ToString();
        //}
        //string Pid = "";
        //if (Request.QueryString["eid"] != null)
        //{
        //    eventId = Request.QueryString["eid"].ToString();
        //    if (!IsPostBack)
        //    {
        //        Session["UploadReport_VisitType"] = Request.QueryString["Type"].ToString();
        //        Session["UploadReport_DoctorId"] = Request.QueryString["Doc"].ToString();

        //        if (Request.QueryString["pgid"].ToString() == null || Request.QueryString["pgid"].ToString() == "")
        //        {
        //            Pid = "&nbsp";
        //        }
        //        Pid = Request.QueryString["pgid"].ToString();
        //        Bill_Sys_Upload_VisitReport GetSpecialty = new Bill_Sys_Upload_VisitReport();
        //        ProcedureGroupId = GetSpecialty.GetDoctorSpecialty(Session["UploadReport_DoctorId"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
        //        //if ((Pid.Substring(0, 1) == "&") || (Pid.Substring(0, 1) == "n"))
        //        //    ProcedureGroupId = GetSpecialty.GetDoctorSpecialty(Session["UploadReport_DoctorId"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
        //        //else
        //        //    ProcedureGroupId = Pid;

        //        Session["UploadReport_ProcedureGroupId"] = ProcedureGroupId;
        //        Session["UploadReport_EventId"] = Request.QueryString["eid"].ToString();
        //    }
        //    procedureGroupCode = ProcedureGroupId;

        //    string OutputFilePath = "";
        //    string strResult = "";
        //    if (Pid == "AC")
        //    {
        //        OutputFilePath = getApplicationSetting("PatientInfoSaveFilePath");
        //        string OpenFilepath = getApplicationSetting("PatientInfoOpenFilePath");
        //        string newPdfFilename = "";
        //        newPdfFilename = "AC_Notes_" + getFileName();

        //        if (!Directory.Exists(OutputFilePath))
        //        {
        //            Directory.CreateDirectory(OutputFilePath);
        //        }

        //        //OutputFilePath = OutputFilePath + newPdfFilename;

        //        OpenFilepath = OpenFilepath + newPdfFilename;
        //        OpenFilepath = OpenFilepath.Replace("\"", "/");

        //        GenerateHp1 objAcNotes = new GenerateHp1();
        //        objAcNotes.GenerateHp1PDF(OutputFilePath + newPdfFilename, eventId);
        //        strResult = SaveNotesDocManager(newPdfFilename, OutputFilePath, eventId);
        //        lblMsg.Text += strResult;
        //        if (strResult == "Success")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewACNotes", "<script type='text/javascript'>window.location.href='" + OpenFilepath + "'</script>");
        //        }
        //    }

        //    else if (Pid == "PM")
        //    {
        //        OutputFilePath = getApplicationSetting("PatientInfoSaveFilePath");
        //        string OpenFilepath = getApplicationSetting("PatientInfoOpenFilePath");
        //        string newPdfFilename = "";
        //        newPdfFilename = "PM_Notes_" + getFileName();

        //        if (!Directory.Exists(OutputFilePath))
        //        {
        //            Directory.CreateDirectory(OutputFilePath);
        //        }

        //        //OutputFilePath = OutputFilePath + newPdfFilename;

        //        OpenFilepath = OpenFilepath + newPdfFilename;
        //        OpenFilepath = OpenFilepath.Replace("\"", "/");

        //        PMNotes_PDF objPMNotes_PDF = new PMNotes_PDF();
        //        objPMNotes_PDF.GeneratePMReport(OutputFilePath + newPdfFilename, eventId);
        //        strResult = SaveNotesDocManager(newPdfFilename, OutputFilePath, eventId);
        //        lblMsg.Text += strResult;
        //        if (strResult == "Success")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewPMNotes", "<script type='text/javascript'>window.location.href='" + OpenFilepath + "'</script>");
        //        }
        //    }

        //    else if (Pid == "PT")
        //    {
        //        OutputFilePath = getApplicationSetting("PatientInfoSaveFilePath");
        //        string OpenFilepath = getApplicationSetting("PatientInfoOpenFilePath");
        //        string newPdfFilename = "";
        //        newPdfFilename = "PT_Notes_" + getFileName();

        //        if (!Directory.Exists(OutputFilePath))
        //        {
        //            Directory.CreateDirectory(OutputFilePath);
        //        }

        //        //OutputFilePath = OutputFilePath + newPdfFilename;

        //        OpenFilepath = OpenFilepath + newPdfFilename;
        //        OpenFilepath = OpenFilepath.Replace("\"", "/");

        //        PTNotes_PDF objPtNotes_Pdf = new PTNotes_PDF();
        //        objPtNotes_Pdf.GeneratePTReport(OutputFilePath + newPdfFilename, eventId);
        //        strResult = SaveNotesDocManager(newPdfFilename, OutputFilePath, eventId);
        //        lblMsg.Text += strResult;
        //        if (strResult == "Success")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewPTNotes", "<script type='text/javascript'>window.location.href='" + OpenFilepath + "'</script>");
        //        }
        //    }

        //    else if (Pid == "CH")
        //    {
        //        OutputFilePath = getApplicationSetting("PatientInfoSaveFilePath");
        //        string OpenFilepath = getApplicationSetting("PatientInfoOpenFilePath");
        //        string newPdfFilename = "";
        //        newPdfFilename = "CH_Notes_" + getFileName();

        //        if (!Directory.Exists(OutputFilePath))
        //        {
        //            Directory.CreateDirectory(OutputFilePath);
        //        }

        //        //OutputFilePath = OutputFilePath + newPdfFilename;

        //        OpenFilepath = OpenFilepath + newPdfFilename;
        //        OpenFilepath = OpenFilepath.Replace("\"", "/");

        //        CHNotes_PDF objCHNotes_Pdf = new CHNotes_PDF();
        //        objCHNotes_Pdf.GenerateCHReport(OutputFilePath + newPdfFilename, eventId);
        //        strResult = SaveNotesDocManager(newPdfFilename, OutputFilePath, eventId);
        //        lblMsg.Text += strResult;
        //        if (strResult == "Success")
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ViewCHNotes", "<script type='text/javascript'>window.location.href='" + OpenFilepath + "'</script>");
        //        }
        //    }
        //}

    }

    private string getFileName()
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
        return szFileName;
    }

    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    public string getApplicationSetting(String p_szKey)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        myConn.Open();
        String szParamValue = "";

        SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", myConn);
        SqlDataReader dr;
        dr = cmdQuery.ExecuteReader();

        while (dr.Read())
        {
            szParamValue = dr["parametervalue"].ToString();
        }
        return szParamValue;
    }

    protected string SaveNotesDocManager(string strFileName, string strBasePath, string iEventId)
    {
        string sourceFile = strBasePath + strFileName;
        string destFile = "";
        Bill_Sys_Upload_VisitReport objUpload = new Bill_Sys_Upload_VisitReport();
        ArrayList UploadObj = new ArrayList();
        UploadObj.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID.ToString());
        UploadObj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
        UploadObj.Add(Session["UploadReport_DoctorId"].ToString());
        UploadObj.Add(Session["UploadReport_VisitType"].ToString());
        UploadObj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString());
        UploadObj.Add(strFileName);
        UploadObj.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
        UploadObj.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
        UploadObj.Add(Session["UploadReport_EventId"].ToString());
        UploadObj.Add(Session["UploadReport_ProcedureGroupId"].ToString());

        string Result = objUpload.Upload_Report_For_Visit(UploadObj);
        //lblMsg.Text = UploadObj[0].ToString() + "," + UploadObj[1].ToString() + "," + UploadObj[2].ToString() + "," + UploadObj[3].ToString() + "," +
        //    UploadObj[4].ToString() + "," + UploadObj[5].ToString() + "," + UploadObj[6].ToString() + "," + UploadObj[7].ToString() + "," + UploadObj[8].ToString() + "," + UploadObj[9].ToString();
        if (Result != "Failed")
        {
            if (!(Directory.Exists(Result)))
                Directory.CreateDirectory(Result);
            destFile = Result;
            System.IO.File.Copy(sourceFile, destFile + strFileName, true);
            Result = "Success";
        }
        return Result;
    }
}