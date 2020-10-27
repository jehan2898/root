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
using System.Globalization;
using mbs.BillConfigurationBO;

public partial class AJAX_Pages_Bills_Sys_WC_Configure : PageBase
{
    string[] datevalues;
    WC_Configuration _obj = new WC_Configuration();
    DataSet dsmandatorycheck = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdConfiguration;
        this.txtSearchBox.SourceGrid = grdConfiguration;
        this.grdConfiguration.Page = this.Page;
        this.grdConfiguration.PageNumberList = this.con;

        txthdnCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string strmandatoryfield = "ACDT";
        string strconfigurationType = "MANDATORY_CHECK";
        chkPalceService.Attributes.Add("onclick", "return FunchkPalceService();");

        if (!Page.IsPostBack)
        {
            dsmandatorycheck = _obj.GetBillConfigurationForMandatoryCheck(txthdnCompanyID.Text, strmandatoryfield);
            if (dsmandatorycheck.Tables[0].Rows.Count > 0)
            {
                if (dsmandatorycheck.Tables[0].Rows[0][0].ToString() == strconfigurationType)
                {
                    chkdtac.Checked = true;

                }
                else
                {
                    chkdtac.Checked = false;

                }

            }
            grdConfiguration.XGridBind();
            string[] FC = _obj.formatcode;
            ArrayList al0 = new ArrayList();
            al0.Add("--Select--");
            foreach (string s in FC)
            {
                if ((s != "") && (s != null))
                {
                    al0.Add(s);
                }
            }

            ddlFormatCode.DataSource = al0;
            ddlFormatCode.DataBind();
            LoadData();
        }
    }

    protected void ddlFormatValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormatCode.SelectedValue.ToString() == "Date")
        {
            CultureInfo CI = new CultureInfo("en-us");
            CI.DateTimeFormat.ShortDatePattern = ddlFormatValue.SelectedValue.ToString();
            System.Threading.Thread.CurrentThread.CurrentCulture = CI;
            lblExample.Text = DateTime.Parse("2011" + "/01/" + "28").ToShortDateString();
        }
        else if (ddlFormatCode.SelectedValue.ToString() == "Phone")
        {
            if (ddlFormatValue.SelectedValue.ToString() == "(xxx)xxx-xxxx")
            {
                lblExample.Text = "(123)456-8910";
            }
            else if (ddlFormatValue.SelectedValue.ToString() == "xxx-xxx-xxxx")
            {
                lblExample.Text = "123-456-8910";
            }

        }
    }

    protected void ddlFormatCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ArrayList al1 = new ArrayList();
        al1 = _obj.GetValues(ddlFormatCode.SelectedValue);
        al1.Insert(0, "--Select--");
        ddlFormatValue.DataSource = al1;
        ddlFormatValue.DataBind();
        lblExample.Text = "";
    }

    public void ClearControls()
    {
        ddlFormatCode.SelectedValue = "--Select--";
        ddlFormatValue.SelectedValue = "--Select--";
        ddlType.SelectedValue = "--Select--";
        lblMsg.Text = "";
        lblExample.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ddlFormatCode.SelectedValue == "--Select--")
        {
            lblMsg.Text = "Please select Format Code.";
            return;
        }
        else
            if (ddlFormatValue.SelectedValue == "--Select--")
            {
                //lblMsg.Text = "Please select Format Value.";

                usrMessage.PutMessage("Please select Format Value.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
                return;
            }
            else
                if (ddlType.SelectedValue == "--Select--")
                {
                    // lblMsg.Text = "Please select Type.";
                    usrMessage.PutMessage("Please select Case Type.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                    return;
                }
        string sz_companyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        string FormatCode, FormatValue, Type;
        FormatCode = ddlFormatCode.SelectedValue;
        FormatValue = ddlFormatValue.SelectedValue;
        Type = ddlType.SelectedValue;
        string sz_configurationType = "FORMATTING_CHECK";

        if (_obj.SaveConfiguration(FormatCode, FormatValue, Type, sz_companyID, sz_configurationType))
        {

            usrMessage.PutMessage("key saved successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            grdConfiguration.XGridBind();
        }
        else
        {
            usrMessage.PutMessage("Configuration not saved.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();


        }
    }

    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdConfiguration.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
    #endregion

    //soft delete functionality


    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdConfiguration.XGridBindSearch();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        WC_Configuration objDelete = new WC_Configuration();
        String szListOfSettings = "";
        for (int i = 0; i < grdConfiguration.Rows.Count; i++)
        {
            CheckBox chkDelete1 = (CheckBox)grdConfiguration.Rows[i].FindControl("ChkDelete");
            if (chkDelete1.Checked)
            {
                objDelete.DeleteConfiguration(grdConfiguration.DataKeys[i]["I_CONFIGURATION_ID"].ToString(), txthdnCompanyID.Text);

                usrMessage1.PutMessage("Changes were saved successfully");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();
            }
            else
            {
                usrMessage1.PutMessage("please select value");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage1.Show();
            }

        }

        grdConfiguration.XGridBind();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        mbs.BillConfigurationBO.BillConfigurationDO objBillBillConfigurationDO = new mbs.BillConfigurationBO.BillConfigurationDO();
        mbs.BillConfigurationBO.BillConfigurationBO objBillBillConfigurationBO = new mbs.BillConfigurationBO.BillConfigurationBO();
        int strckhval;
        objBillBillConfigurationDO.sz_configuration_type = "MANDATORY_CHECK";
        objBillBillConfigurationDO.sz_company_id = txthdnCompanyID.Text;
        objBillBillConfigurationDO.sz_mandatory_field = "ACDT";

        if (chkdtac.Checked)
        {
            strckhval = 1;
        }
        else
        {
            strckhval = 0;
        }
        objBillBillConfigurationDO.bt_mandatory_value = strckhval;

        objBillBillConfigurationBO.loadParameters(objBillBillConfigurationDO);

        usrMessage2.PutMessage("Changes were saved successfully");
        usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        usrMessage2.Show();

    }
    protected void btnUpdateC4AMR_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string chkPalceServicevalue = "";
        string chkBalanceDuevalue = "";
        string rdbNPI = rdlstNPIConfig.SelectedValue.ToString();
        string rdbPlaceService = rdlstZipConfig.SelectedValue.ToString();
        txthdnCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (chkPalceService.Checked)
        {
            chkPalceServicevalue = "1";
        }
        else
        {
            chkPalceServicevalue = "0";
        }
         if (chkBalanceDue.Checked)
        {
            chkBalanceDuevalue = "1";
        }
        else
        {
            chkBalanceDuevalue = "0";
        }
        try
        {
            _obj.SaveC4AMRConfiguration(rdbNPI, rdbPlaceService, chkPalceServicevalue, txtPlaceService.Text, txthdnCompanyID.Text, chkBalanceDuevalue);

            usrMessage3.PutMessage("Changes were saved successfully");
            usrMessage3.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage3.Show();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void LoadData()
    {
        DataSet dsConfig = new DataSet();
       dsConfig=_obj.GetC4AMRConfiguration(txthdnCompanyID.Text);

       if (dsConfig.Tables.Count > 0)
       {
           if (dsConfig.Tables[0].Rows.Count > 0)
           {
               if(dsConfig.Tables[0].Rows[0]["SZ_NPI"].ToString()=="1")
               {
                   rdlstNPIConfig.Text="1";
               }else
               {
                   rdlstNPIConfig.Text = "0";
               }
               if (dsConfig.Tables[0].Rows[0]["SZ_SERVICE_LOCATION_ZIP_CODE"].ToString() == "1")
               {
                   rdlstZipConfig.Text = "1";
               }
               else
               {
                   rdlstZipConfig.Text = "0";
               }

               if (dsConfig.Tables[0].Rows[0]["SZ_CHK_PLACE_SERVICE"].ToString() == "1")
               {
                   chkPalceService.Checked = true;
                   txtPlaceService.Enabled = false;
               }
               else
               {
                   chkPalceService.Checked = false;
                   txtPlaceService.Enabled = true;
                   txtPlaceService.Text = dsConfig.Tables[0].Rows[0]["SZ_PLACE_OF_SERVICE"].ToString();

               }
               if (dsConfig.Tables[0].Rows[0]["SZ_SHOW_BALANCE_DUE"].ToString() == "1")
               {
                   chkBalanceDue.Checked = true;
                  
               }
               else
               {
                   chkBalanceDue.Checked = false;
                   

               }

           }
       }
    }

}
