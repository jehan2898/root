using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using Componend;


public partial class ActionPage : PageBase
{
    private ScanDao scanDao;
    private FileUpload _FileUpload;
    private ArrayList arrImgId;
    private string str21 = "";
    private string strsqlCon = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpPostedFile file = HttpContext.Current.Request.Files["RemoteFile"];
        string fileName = file.FileName;
        string str2 = ((ScanDao)Session["SCAN_OBJECT"]).sz_flag;

        this.scanDao = new ScanDao();
        ScanBo scanBo = new ScanBo();
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());


        if (str2.Equals("case"))
        {
            this.scanDao.sz_FileName = ((ScanDao)Session["SCAN_OBJECT"]).sz_FileName;
            this.scanDao.sz_flag = ((ScanDao)Session["SCAN_OBJECT"]).sz_flag;
            this.scanDao.sz_NodeID = ((ScanDao)Session["SCAN_OBJECT"]).sz_NodeID;
            this.scanDao.sz_DocId = ((ScanDao)Session["SCAN_OBJECT"]).sz_DocId;
            this.scanDao.sz_company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.scanDao.sz_DocType = ((ScanDao)Session["SCAN_OBJECT"]).sz_DocType;
            this.scanDao.sz_UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
            this.scanDao.sz_CompanyName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            this.scanDao.sz_case_id = ((ScanDao)Session["SCAN_OBJECT"]).sz_case_id;
            this.scanDao.sz_UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            this.scanDao.sz_Path = HttpContext.Current.Request.MapPath(".") + "/ImageScanned/" + fileName;
            file.SaveAs(HttpContext.Current.Request.MapPath(".") + "/ImageScanned/" + fileName);
            string str12 = "";
            string str13 = "";
            str13 = scanBo.GetNodePath(this.scanDao.sz_NodeID, this.scanDao.sz_case_id, this.scanDao.sz_company_id).Replace(@"\", "/");
            str12 = scanBo.getPhysicalPath() + str13;
            if (!Directory.Exists(str12))
            {
                Directory.CreateDirectory(str12);
            }
            File.Copy(this.scanDao.sz_Path, str12 + "/" + fileName, true);
            string text1 = this.scanDao.sz_CompanyName + "/" + this.scanDao.sz_case_id + "/" + this.scanDao.sz_DocType + "/";
            ArrayList list4 = new ArrayList();
            list4.Add(this.scanDao.sz_case_id);
            list4.Add(this.scanDao.sz_DocType);
            list4.Add(this.scanDao.sz_company_id);
            list4.Add(fileName);
            list4.Add(str13 + "/");
            list4.Add(this.scanDao.sz_UserName);
            list4.Add(this.scanDao.sz_NodeID);
            this.scanDao.sz_ImageId = scanBo.SaveDocumentData(list4);
            try
            {
                int.Parse(this.scanDao.sz_CaseDocId);
            }
            catch
            {
                this.scanDao.sz_CaseDocId = "0";
            }
            new SaveOperation();
            new EditOperation();
            if ((this.scanDao.sz_CaseDocId != "") && (this.scanDao.sz_CaseDocId != "0"))
            {
                string str14 = "";
                string str15 = "";
                scanBo.SaveReqDoc(this.scanDao.sz_CaseDocId, this.scanDao.sz_case_id, str14, this.scanDao.sz_DocId, "1", this.scanDao.sz_Notes, this.scanDao.sz_AssignTo, this.scanDao.sz_AssignOn, this.scanDao.sz_UserId, str15, "", this.scanDao.sz_company_id, this.scanDao.sz_ImageId, "UPDATE");
            }
            else
            {
                string str16 = "";
                string str17 = "";
                scanBo.SaveReqDoc("", this.scanDao.sz_case_id, str16, this.scanDao.sz_DocId, "1", this.scanDao.sz_Notes, this.scanDao.sz_AssignTo, this.scanDao.sz_AssignOn, this.scanDao.sz_UserId, str17, "", this.scanDao.sz_company_id, this.scanDao.sz_ImageId, "ADD");
            }
        }
    }
}
