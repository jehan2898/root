using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
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
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using PDFValueReplacement;
using Vintasoft.Barcode;
using Vintasoft.Pdf;

public partial class Bill_Sys_Cm_All1 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TXT_I_EVENT.Text = "11919";
       Session["CM_Event_ID"]=  TXT_I_EVENT.Text;
        if (!IsPostBack)
        {
            
            LoadData();
            LoadPatientData();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Cm_All1.aspx");
        }
        #endregion

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Page.IsValid)
            {
                TXT_ARE_YOU_PREGNANT.Text = RDO_ARE_YOU_PREGNANT.SelectedValue.ToString();
                TXT_FULL_PART.Text = RDO_FULL_PART.SelectedValue.ToString();
                TXT_LAWSUIT_PENDING.Text = RDO_LAWSUIT_PENDING.SelectedValue.ToString();



                SaveOperation _objsave = new SaveOperation();
                _objsave.WebPage = this.Page;
                _objsave.Xml_File = "Cm_All_First_Page.xml";
                _objsave.SaveMethod();
               
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
        
        Response.Redirect("Bill_Sys_Cm_All2.aspx");
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
        try
        {

           
            EditOperation _objEdit = new EditOperation();
            _objEdit.Primary_Value = TXT_I_EVENT.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "Cm_All_First_Page.xml";
            _objEdit.LoadData();
            if (TXT_LAWSUIT_PENDING.Text.Trim() != "-1")
            {
                if (TXT_LAWSUIT_PENDING.Text.Trim() == "0")
                {
                    RDO_LAWSUIT_PENDING.SelectedIndex = 0;
                }
                else if (TXT_LAWSUIT_PENDING.Text.Trim() == "1")
                {
                    RDO_LAWSUIT_PENDING.SelectedIndex = 1;
                }
            }


            if (TXT_ARE_YOU_PREGNANT.Text.Trim() != "-1")
            {
                if (TXT_ARE_YOU_PREGNANT.Text.Trim() == "0")
                {
                    RDO_ARE_YOU_PREGNANT.SelectedIndex = 0;
                }
                else if (TXT_ARE_YOU_PREGNANT.Text.Trim() == "1")
                {
                    RDO_ARE_YOU_PREGNANT.SelectedIndex = 1;
                }
            }



            if (TXT_FULL_PART.Text.Trim() != "-1")
            {
                if (TXT_FULL_PART.Text.Trim() == "0")
                {
                    RDO_FULL_PART.SelectedIndex = 0;
                }
                else if (TXT_FULL_PART.Text.Trim() == "1")
                {
                    RDO_FULL_PART.SelectedIndex = 1;
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
            _editOperation.Primary_Value = Session["CM_Event_ID"].ToString();
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



   }
