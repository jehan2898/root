/***********************************************************/
/*Project Name         :       Medical Billing System
/*Description          :       Revert Report
/*Author               :       Sandeep Y
/*Date of creation     :       15 Dec 2008
/*Modified By (Last)   :       Atul Jadhav 
/*Modified By (S-Last) :
/*Modified Date        :      30 April 2010 
/************************************************************/

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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class Bill_Sys_StatusProceudure : PageBase
{
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;

    protected void Page_Load(object sender, EventArgs e)
    {
            string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           // btnAssign.Attributes.Add("onclick", "return formValidator('frmAssociateDignosisCode','extddlDoctor');");
            btnRevert.Attributes.Add("onclick", "return  ChekOne();");
            btnDoctor.Attributes.Add("onclick", "return  Check();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {

                ///bind doctor name to dropdown
                DataSet ds = new DataSet();
                Bill_Sys_DoctorBO objDoc = new Bill_Sys_DoctorBO();
               ds= objDoc.GetReadingDoctorList(txtCompanyID.Text);
               ddlDoctor.DataSource = ds.Tables[0];
               ddlDoctor.DataTextField = "DESCRIPTION"; 
               ddlDoctor.DataValueField = "CODE";
               ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, "--Select--");







                if (Request.QueryString["CaseId"] != null)
                {
                    Session["CASE_OBJECT"] = null;
                    txtCaseID.Text = Request.QueryString["CaseId"].ToString();
                    GetPatientDeskList();
                }
                else if (Session["CASE_OBJECT"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    //////////////////////
                    //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills

                    Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                    _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;

                    Session["CASEINFO"] = _bill_Sys_Case;

                    Session["PassedCaseID"] = txtCaseID.Text;
                    String szURL = "";
                    String szCaseID = Session["PassedCaseID"].ToString();
                    Session["QStrCaseID"] = szCaseID;
                    Session["Case_ID"] = szCaseID;
                    Session["Archived"] = "0";
                    Session["QStrCID"] = szCaseID;
                    Session["SelectedID"] = szCaseID;
                    Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["SN"] = "0";
                    Session["LastAction"] = "vb_CaseInformation.aspx";


                    Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;

                    Session["TM_SZ_CASE_ID"] = txtCaseID.Text;
                    GetPatientDeskList();
                    //
                    ///////////////////
                }
                     
                else
                {
                    Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }


                GetProcedureList(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {

                }
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
            cv.MakeReadOnlyPage("Bill_Sys_StatusProceudure.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void GetProcedureList(string caseId,string companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            grdProcesureCode.DataSource = _bill_Sys_ProcedureCode_BO.GetStatusProcedureCodeList(caseId, companyID);
            grdProcesureCode.DataBind();
            Bill_Sys_ProcedureCode_BO obj=new Bill_Sys_ProcedureCode_BO();
            DataSet ds=obj.Get_Sys_Key("SS00014",txtCompanyID.Text);
          
            for (int i = 0; i < grdProcesureCode.Items.Count; i++)
			{
			 
			}
            if (ds.Tables[0].Rows[0][0].ToString() != "1")
            {
                grdProcesureCode.Columns[14].Visible = false;
                
            }
            else
            {
                grdProcesureCode.Columns[14].Visible = true;
                foreach (DataGridItem drg in grdProcesureCode.Items)
                {
                    LinkButton drp = (LinkButton)drg.FindControl("LKBView");
                    if (drg.Cells[11].Text == "Received Report")
                    {
                        drp.Visible = false;
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

    // To load Patient Details
    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            grdPatientDeskList.DataBind();
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                grdPatientDeskList.Columns[1].Visible = false;
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

   

    #region "Upload Report"

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        objNF3Template = new Bill_Sys_NF3_Template();
        try
        {
           if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                return;
            }
            string extension="";
            string result="";
            extension = Path.GetExtension(fuUploadReport.FileName);
            result = Path.GetFileNameWithoutExtension(fuUploadReport.FileName);
            string FileName = result + "_" + DateTime.Now.ToString("MMddyyyysstt")+ extension;
            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            foreach (DataGridItem drg in grdProcesureCode.Items)
            {
                CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
                if (drp.Checked == true)
                {

                    String szDestinationDir = "";

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

                    }
                    szDestinationDir = szDestinationDir + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/No Fault File/Medicals/" + drg.Cells[12].Text + "/";
                   
                    strLinkPath = szDestinationDir + FileName;
                    if (!Directory.Exists(szDefaultPath + szDestinationDir))
                    {
                        Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                    }
                    //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
                    //{
                        fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + FileName);
                        // Start : Save report under document manager.

                        ArrayList objAL = new ArrayList();
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID == (((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID))
                        {
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        else
                        {
                            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);                            
                        }
                        
                        objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
                        objAL.Add(FileName);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(drg.Cells[12].Text);
                        ImageId=objNF3Template.saveReportInDocumentManager(objAL);
                        // End :   Save report under document manager.
                    //}
                }
            }

            Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
            ArrayList arrOBJ;
            foreach (DataGridItem drg in grdProcesureCode.Items)
            {
                CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkSelect");
                if (drp.Checked == true)
                {
                    _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                    _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(drg.Cells[1].Text), strLinkPath, ImageId);
                }
            }
            GetProcedureList(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            lblMsg.Text = "Report Received Successfully";
            lblMsg.Visible = true;
            //Page.RegisterStartupScript("ss", "<script language = 'javascript'>alert('Report received successfully.');</script>");
           
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
    #endregion
    #region"Revert Report"
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void btnRevert_Click1(object sender, EventArgs e)
    {
        for (int j = 0; j < grdProcesureCode.Items.Count; j++)
        {
            CheckBox chkDelete1 = (CheckBox)grdProcesureCode.Items[j].FindControl("chkSelect");
            if (chkDelete1.Checked && grdProcesureCode.Items[j].Cells[11].Text.Trim().ToString().Equals("Received Report"))
            {
                Bill_Sys_ReportBO objUpdateReport = new Bill_Sys_ReportBO();
                objUpdateReport.RevertReport(Convert.ToInt32(grdProcesureCode.Items[j].Cells[1].Text.Trim().ToString()));
                #region Activity_Log
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "REP_REVERT";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Procedure Id  : " + grdProcesureCode.Items[j].Cells[1].Text.Trim().ToString() + " Report Revrted . ";
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)HttpContext.Current.Session["USER_OBJECT"]).SZ_USER_ID);
                this._DAO_NOTES_EO.SZ_CASE_ID = this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)HttpContext.Current.Session["CASE_OBJECT"]).SZ_CASE_ID);
                this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                #endregion
                lblMsg.Text = "Report Reverted Successfully";
                lblMsg.Visible = true;
            }

        }
        GetProcedureList(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    }
    #endregion
    protected void btnReceivedDocument_Click(object sender, EventArgs e)
    {

    }
    protected void btnDoctor_Click(object sender, EventArgs e)
    {
        for (int j = 0; j < grdProcesureCode.Items.Count; j++)
        {
            CheckBox chkDelete1 = (CheckBox)grdProcesureCode.Items[j].FindControl("chkSelect");
            if (chkDelete1.Checked)
            {
                Bill_Sys_ProcedureCode_BO  _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                _bill_Sys_ProcedureCode_BO.UpdateReadingDoctor(Convert.ToInt32(grdProcesureCode.Items[j].Cells[1].Text.Trim().ToString()), ddlDoctor.SelectedValue.ToString());

            }
        }
         GetProcedureList(txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

    }
    protected void grdProcesureCode_ItemCommand(object source, DataGridCommandEventArgs e)
    {

       if (e.CommandName.ToString() == "View")
        {
            string szCaseID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
           string szEventProcID=e.Item.Cells[1].Text;
           string szSpeciality = e.Item.Cells[12].Text;
            Page.RegisterStartupScript("mm", "<script language='javascript'>showReceiveDocumentPopup('"+szCaseID+"','"+szEventProcID+"','"+szSpeciality+"');</script>");
            
        }



    }


    
}
   