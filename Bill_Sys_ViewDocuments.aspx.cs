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
using System.IO;

public partial class Bill_Sys_ViewDocuments : PageBase
{
    string caseid = "";
    string EventProcID = "";
    string szSpeciality = "";
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        btnView.Attributes.Add("onclick", "return confirm_update_bill_status();");
        if (!IsPostBack)
        {
            caseid = Request.QueryString[0].ToString();
            string view = Request.QueryString[1].ToString();
            EventProcID = Request.QueryString[2].ToString();
            szSpeciality = Request.QueryString[3].ToString();
            txtEventProcId.Text = EventProcID;
            txtSpecility.Text = szSpeciality;
            txtCaseId.Text = caseid;
            if (view == "YES")
            {
                btnView.Visible = true;
            }
            else
            {
                btnView.Visible = true;
            }
            //GetViewDoc(caseid, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            GetViewDoc(caseid, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, EventProcID);
            if (grdViewDocuments.Items.Count > 0)
            {
                DataSet ds = new DataSet();
                Bill_Sys_DoctorBO objDoc = new Bill_Sys_DoctorBO();
                ds = objDoc.GetReadingDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                ddlDoctor.DataSource = ds.Tables[0];
                ddlDoctor.DataTextField = "DESCRIPTION";
                ddlDoctor.DataValueField = "CODE";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, "--Select--");
                ddlDoctor.Visible = true;
                btnDoctor.Visible = true;
                btnView.Visible = true;
            }
            else
            {
                ddlDoctor.Visible = false;
                btnDoctor.Visible=false;
                btnView.Visible = false;


            }

            
        }
    }



    private void GetViewDoc(string caseId, string companyID, string proc_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            //grdViewDocuments.DataSource = _bill_Sys_ProcedureCode_BO.GetViewDocs(caseId, companyID, proc_id);
            grdViewDocuments.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(caseId, companyID, proc_id);
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        objNF3Template = new Bill_Sys_NF3_Template();
        String szDefaultPath = objNF3Template.getPhysicalPath();
        int ImageId = 0;
        string PathNotFound = "";
        try
        {
            foreach (DataGridItem drg in grdViewDocuments.Items)
            {
                CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkView");
                DropDownList drpReport = (DropDownList)drg.Cells[0].FindControl("ddlreport");
                if (drp.Checked == true)
                {

                    String szDestinationDir = "";

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    }
                    else
                    {
                        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    }
                    if (drpReport.SelectedValue == "0")
                    {
                        szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                    }
                    else if (drpReport.SelectedValue == "1")
                    {
                        szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/Referral/";
                    }

                    else if (drpReport.SelectedValue == "2")
                    {
                        szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/AOB/";
                    }


                    strLinkPath = szDefaultPath + drg.Cells[2].Text + drg.Cells[3].Text;
                    if (!Directory.Exists(szDefaultPath + szDestinationDir))
                    {
                        Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                    }
                    if (File.Exists(strLinkPath))
                    {

                        File.Copy(strLinkPath, szDefaultPath + szDestinationDir + drg.Cells[3].Text, true);
                        //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
                        //{
                        // fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
                        // Start : Save report under document manager.

                        ArrayList objAL = new ArrayList();
                        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        {
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }
                        else
                        {
                            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        }

                        objAL.Add(caseid);
                        objAL.Add(drg.Cells[3].Text);
                        objAL.Add(szDestinationDir);
                        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                        objAL.Add(txtSpecility.Text);

                        if (drpReport.SelectedValue == "0")
                        {
                            ImageId = objNF3Template.saveReportInDocumentManager(objAL);
                        }
                        else if (drpReport.SelectedValue == "1")
                        {
                            ImageId = objNF3Template.saveReportInDocumentManager_Referral(objAL);
                        }
                        else if (drpReport.SelectedValue == "2")
                        {
                            ImageId = objNF3Template.saveReportInDocumentManager_AOB(objAL);
                        }

                        // End :   Save report under document manager.
                        //}

                        Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
                        ArrayList arrOBJ;

                        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                        _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(txtEventProcId.Text), strLinkPath, ImageId);


                    }
                    else
                    {
                        if (PathNotFound == "")
                        {
                            PathNotFound =strLinkPath + ",\n";
                        }
                        else
                        {
                            PathNotFound = PathNotFound +  strLinkPath + ",\n";
                        }
                    }

                }

            }
            usrMessage.PutMessage("Document Received  successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            if (PathNotFound != "")
            {
                usrMessage.PutMessage("These file are not available \n" + PathNotFound);
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


        //GetProcedureList(caseid, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

    }

    protected void grdViewDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string PathNotFound = "";
        if (e.CommandName=="View")
        {
            string path = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string filepath = e.Item.Cells[2].Text;
            string filename = e.Item.Cells[3].Text;
            string fullpath = path + filepath + filename;
            if (File.Exists(fullpath))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
            }
            else
            {
                usrMessage.PutMessage("File is not available \n" + PathNotFound);
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
        }

    }


    protected void btnDoctor_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (ddlDoctor.SelectedValue != "--Select--")
        {
            try
            {
                   Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                _bill_Sys_ProcedureCode_BO.UpdateReadingDoctor( Convert.ToInt32(txtEventProcId.Text), ddlDoctor.SelectedValue.ToString());

                usrMessage.PutMessage(" Dodctor assign successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

            }catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
           
        }
        else
        {
            usrMessage.PutMessage("Please Select Dodctor");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdViewDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
