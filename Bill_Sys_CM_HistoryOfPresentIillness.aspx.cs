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

public partial class Bill_Sys_CM_HistoryOfPresentIillness : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["CM_HI_EventID"] = "12007";
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {
            LoadData();
            LoadPatientData();
            LoadProcedureCodes();
        }

        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (RDO_INJURY_FROM.SelectedValue.Equals(""))
        {
            txtRDO_INJURY_FROM.Text = "-1";
        }
        else
        {
            txtRDO_INJURY_FROM.Text = RDO_INJURY_FROM.SelectedValue;
        }

           
        if (RDO_PREVIOUSLY_TREATED_No.SelectedValue.Equals(""))
        {
            txtRDO_PREVIOUSLY_TREATED_No.Text = "-1";
        }
        else
        {
            txtRDO_PREVIOUSLY_TREATED_No.Text = RDO_PREVIOUSLY_TREATED_No.SelectedValue;
        }
        

        if (RDO_TREAT_INJURY.SelectedValue.Equals(""))
        {
            txtRDO_TREAT_INJURY.Text = "-1";
        }
        else
        {
            txtRDO_TREAT_INJURY.Text = RDO_TREAT_INJURY.SelectedValue;
        }

         Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
         DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(Session["CM_HI_EventID"].ToString());


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
        _saveOperation.Xml_File = "CM_INITIAL_EVALHistoryOf PresentIllness.xml";

        _saveOperation.SaveMethod();
        //Response.Redirect("Bill_Sys_CM_MedicalHistory.aspx");
        Response.Redirect("Bill_Sys_CM_CurrentComplainNEW.aspx", false);

     
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

    public void LoadData()
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
            _editOperation.Xml_File = "CM_INITIAL_EVALHistoryOf PresentIllness.xml";
            _editOperation.LoadData();
           
            if ( txtRDO_INJURY_FROM.Text .Equals("-1"))
            {
             
            }
            else
            {
                RDO_INJURY_FROM.SelectedValue = txtRDO_INJURY_FROM.Text;
            }


            if (txtRDO_PREVIOUSLY_TREATED_No.Text.Equals("-1"))
            {
                 
            }
            else
            {
                RDO_PREVIOUSLY_TREATED_No.SelectedValue=  txtRDO_PREVIOUSLY_TREATED_No.Text;
            }


            if (txtRDO_TREAT_INJURY.Text.Equals(""))
            {
                 
            }
            else
            {
                RDO_TREAT_INJURY.SelectedValue= txtRDO_TREAT_INJURY.Text;
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
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PatientInformetion.xml";
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
