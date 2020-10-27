using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Data;

public partial class Bills_Sys_UploadDocPopup : PageBase
{
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
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                btnUploadFile.Attributes.Add("onclick", "return CheckUploadFile();");
                txtCaseID.Text = Request.QueryString["caseid"].ToString();
                txtPGID.Text = Request.QueryString["pgid"].ToString();
                txtPname.Text = Request.QueryString["pname"].ToString();
                txtDocID.Text = Request.QueryString["docid"].ToString();
                txtEID.Text = Request.QueryString["eid"].ToString();
                extDocType.Flag_ID = txtCompanyID.Text;
                extDocType.Flag_Key_Value = txtPGID.Text;
                btnDelete.Attributes.Add("onClick", "return confirm_delete_document();");
                BindGrid();
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

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_AssociateDocForMedical objDocUpload = new Bill_Sys_AssociateDocForMedical();
            ArrayList gridArr = new ArrayList();
            gridArr.Add(txtCaseID.Text.ToString());
            gridArr.Add(txtEID.Text.ToString());
            DataSet ds = new DataSet();
            ds = objDocUpload.getGridDetail(gridArr);
            grdViewDocuments.DataSource = ds;
            grdViewDocuments.DataBind();
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
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string SzFileName = "";
            string RepFolder = "";
            if (fuUploadReport.FileName != "")
            {
                SzFileName = FileUtilities.FormatUploadedFileName(fuUploadReport.FileName);
            }
            string[] pValue = extDocType.Text.ToString().Split(',');
            string nodeType = pValue[0].ToString();
            string ReportName = pValue[1].ToString();
            if (ReportName.ToLower().ToString() == "initial report")
            {
                RepFolder = "INTReport";
            }
            else if (ReportName.ToLower().ToString() == "followup report")
            {
                RepFolder = "FUReport";
            }
            else if (ReportName.ToLower().ToString() == "aob")
            {
                RepFolder = "AOB";
            }
            string szBasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
            string CompName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string path = szBasePath + "/" + CompName + "/" + txtCaseID.Text + "/" + "No Fault file/Medicals/ " + txtPname.Text + "/" + RepFolder + "/";
            string logicalPath = CompName + "/" + txtCaseID.Text + "/" + "No Fault file/Medicals/ " + txtPname.Text + "/" + RepFolder + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fuUploadReport.SaveAs(path + fuUploadReport.FileName);

            Bill_Sys_AssociateDocForMedical objDocUpload = new Bill_Sys_AssociateDocForMedical();
            ArrayList uploadArr = new ArrayList();
            uploadArr.Add(txtCompanyID.Text.ToString());
            uploadArr.Add(txtCaseID.Text.ToString());
            uploadArr.Add(fuUploadReport.FileName.ToString());
            uploadArr.Add(logicalPath);
            uploadArr.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
            uploadArr.Add(nodeType);
            uploadArr.Add(txtEID.Text.ToString());
            uploadArr.Add(txtPname.Text.ToString());
            string successMsg = objDocUpload.SaveUploadedDocInDocMng(uploadArr);
            if (successMsg.Contains("Success"))
            {
                BindGrid();
                usrMessage.PutMessage("Uploaded Succesfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

            }
            else
            {
                usrMessage.PutMessage(successMsg.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrDelete = new ArrayList();
            for (int i = 0; i < grdViewDocuments.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdViewDocuments.Items[i].FindControl("chkView");
                if (chk.Checked)
                {
                    string szImageID=grdViewDocuments.Items[i].Cells[4].Text.ToString();
                    Bill_Sys_AssociateDocForMedical objDocUpload = new Bill_Sys_AssociateDocForMedical();
                    objDocUpload.ImageID = szImageID;
                    objDocUpload.EventID = txtEID.Text;
                    arrDelete.Add(objDocUpload);
                }
                
            }
            Bill_Sys_AssociateDocForMedical objDelete = new Bill_Sys_AssociateDocForMedical();

            string successMsg = objDelete.DeleteDoc(arrDelete);
            if (successMsg.Contains("Success"))
            {
                BindGrid();
                usrMessage.PutMessage("Deleted Succesfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

            }
            else
            {
                usrMessage.PutMessage(successMsg.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
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