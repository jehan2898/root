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

public partial class Bill_Sys_IM_HistoryOfPresentIillness : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EID"] != null)
            {
                Session["IMEventID"] = Request.QueryString["EID"].ToString();
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
                LoadProcedureCodes();
     
            }
            else
            {
                txtEventID.Text = (string)Session["IMEventID"].ToString();
                LoadData();
                LoadPatientData();
                LoadProcedureCodes();
            }
        }

        txtDOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();

        DataSet dsObj = _objCheckoutBO.PatientName(txtEventID.Text);
        string sz_PatientName = dsObj.Tables[0].Rows[0][0].ToString();
        Session["ChkOutCaseID"] = dsObj.Tables[0].Rows[0][1].ToString();
        if (_objCheckoutBO.CheckIMData(Convert.ToInt32(txtEventID.Text)))
        {
            btnSave.Text = "Update&Next";
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_IM_HistoryOfPresentIillness.aspx");
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
            ds = _objCheckoutBO.PTNotesPatientTreatment(txtEventID.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
                {

                    if (ds.Tables[0].Rows.Count > i)
                    {
                        string j = ds.Tables[0].Rows[i]["INDEX"].ToString();
                        if (j == "101")
                        {
                            chk101.Checked = true;
                        }
                        if (j == "102")
                        {
                            chk102.Checked = true;
                        }
                        if (j == "103")
                        {
                            chk103.Checked = true;
                        }
                        if (j == "104")
                        {
                            chk104.Checked = true;
                        }
                        if (j == "105")
                        {
                            chk105.Checked = true;
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string sz_eventID = "";
           


            if (rdlInjuryIllness.SelectedValue.ToString().Equals(""))
            {
                txtrdlInjuryIllness.Text = "-1";
            }
            else
            {
                txtrdlInjuryIllness.Text = rdlInjuryIllness.SelectedValue;

            }
           
            if (rdlRDOPreviouslyTreated.SelectedValue.ToString().Equals(""))
            {
                txtrdlRDOPreviouslyTreated.Text = "-1";
            }
            else
            {
                txtrdlRDOPreviouslyTreated.Text = rdlRDOPreviouslyTreated.SelectedValue;

            }
           
            if (rdlTreatInjury.SelectedValue.ToString().Equals(""))
            {
                txtrdlTreatInjury.Text = "-1";
            }
            else
            {
                txtrdlTreatInjury.Text = rdlTreatInjury.SelectedValue;

            }
            Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
            DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(Session["IMEventID"].ToString());


            if (chk101.Checked)
            {
                SaveValues(101);
            }

            if (chk102.Checked)
            {
                SaveValues(102);
            }
            if (chk103.Checked)
            {
                SaveValues(103);
            }
            if (chk104.Checked)
            {
                SaveValues(104);
            }
            if (chk105.Checked)
            {
                SaveValues(105);
            }

            _saveOperation = new SaveOperation();
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "IM_HistoryOfPresentIillness.xml";
            _saveOperation.SaveMethod();
      
            Response.Redirect("Bill_Sys_IM_CurrentComplain.aspx", false);
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

    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation    _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_HistoryOfPresentIillness.xml";
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
       

        if (txtrdlInjuryIllness.Text.ToString().Equals("-1"))
        {


        }
        else
        {
            rdlInjuryIllness.SelectedValue = txtrdlInjuryIllness.Text;

        }

        if (txtrdlRDOPreviouslyTreated.Text.ToString().Equals("-1"))
        {

        }
        else
        {

            rdlRDOPreviouslyTreated.SelectedValue = txtrdlRDOPreviouslyTreated.Text;

        }

        if (txtrdlTreatInjury.Text.ToString().Equals("-1"))
        {

        }
        else
        {
            rdlTreatInjury.SelectedValue = txtrdlTreatInjury.Text;

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
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_PatientInformetion.xml";
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
            _objArray.Add(txtEventID.Text);
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
