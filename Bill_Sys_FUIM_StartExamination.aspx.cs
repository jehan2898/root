using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;
using System.Text;
using System.IO;
using System.Collections;

public partial class Bill_Sys_FUIM_StartExamination : PageBase 
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            Session["IM_FW_EVENT_ID"] = Request.QueryString["EID"].ToString();
        }
        TXT_EVENT_ID.Text = Session["IM_FW_EVENT_ID"].ToString();
        if (!IsPostBack)
        {
            LoadPatientData();
            LoadData();
            LoadProcedureCodes();

        }
        TXT_DOE.Text = DateTime.Today.Date.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_FUIM_StartExamination.aspx");
        }
        #endregion
    }



    public void LoadProcedureCodes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
            DataSet ds = new DataSet();
            ds = _objCheckoutBO.PTNotesPatientTreatment(TXT_EVENT_ID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
                {

                    if (ds.Tables[0].Rows.Count > i)
                    {
                        string j = ds.Tables[0].Rows[i]["INDEX"].ToString();
                        if (j == "501")
                        {
                            CHK_TREATMENT_99211.Checked = true;
                        }
                        if (j == "502")
                        {
                            CHK_TREATMENT_99212.Checked = true;
                        }
                        if (j == "503")
                        {
                            CHK_TREATMENT_99213.Checked = true;
                        }
                        if (j == "504")
                        {
                            CHK_TREATMENT_99214.Checked = true;
                        }
                        if (j == "505")
                        {
                            CHK_TREATMENT_99215.Checked = true;
                        }
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



    protected void BTN_SAVE_NEXT_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        // Create object of SaveOperation. With the help of this object you save information into table.
        try
        {
            if (Page.IsValid)  // Check for Validation.
            {
                _saveOperation.WebPage = this.Page;  // Pass Current web form to SaveOperation.
                _saveOperation.Xml_File = "Bill_Sys_IM_Start_Examination.xml";  // Pass xml file to SaveOperation
                _saveOperation.SaveMethod(); // Call  save method of SaveOperation. Will save all information from web form.

                Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
                DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(Session["IM_FW_EVENT_ID"].ToString());

                if (CHK_TREATMENT_99211.Checked)
                {
                    SaveValues(501);
                }

                if (CHK_TREATMENT_99212.Checked)
                {
                    SaveValues(502);
                }
                if (CHK_TREATMENT_99213.Checked)
                {
                    SaveValues(503);
                }
                if (CHK_TREATMENT_99214.Checked)
                {
                    SaveValues(504);
                }
                if (CHK_TREATMENT_99215.Checked)
                {
                    SaveValues(505);
                }
            }
            //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_IM_Followup_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
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
       

        Response.Redirect("Bill_Sys_FUIM_Examination_Section.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "Bill_Sys_IM_Start_Examination.xml";
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

    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = Session["IM_FW_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
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

    #region "Save ProcedureCodes"

    public void SaveValues(int index)
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
            _objArray.Add(TXT_EVENT_ID.Text);
            _objArray.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            obj.saveProcedureCodes(_objArray);
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

}
