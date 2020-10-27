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
using Componend;

public partial class Bill_Sys_ConvertProcedureCode : PageBase
{
    private ListOperation _listOperation;
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    private Bill_Sys_Visit_BO _visitBO;
    private Bill_Sys_Visit_BO _procBO;
    private ArrayList _objAL;

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
                btnConToVisit.Attributes.Add("onclick", "return formValidator('frmProcedureCode','extddlSpeciality');");
                btnConToTreatment.Attributes.Add("onclick", "return formValidator('frmProcedureCode','extddlSpeciality');");
                btnConToTest.Attributes.Add("onclick", "return formValidator('frmProcedureCode','extddlSpeciality');");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //extddlVisitRoom.Flag_ID = txtCompanyID.Text;
                extddlSpeciality.Flag_ID = txtCompanyID.Text;
                GetProcedureCode();
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ConvertProcedureCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
      

    private void GetProcedureCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
         try
         {
             _listOperation.WebPage = this.Page;
             _listOperation.Xml_File = "procedurecodelist.xml";
             _listOperation.LoadList();
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

    private void SaveProcCode(string _type)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet _billDs = new DataSet();
       
        try
        {
         
            foreach (DataGridItem dgiProcedureCode in grdProcedure.Items)
            {
                CheckBox chkEmpty = (CheckBox)dgiProcedureCode.Cells[1].FindControl("chkSelect");
                if (chkEmpty.Checked == true)
                {

                    _visitBO = new Bill_Sys_Visit_BO();
                    _objAL = new ArrayList();
                    _objAL.Add(dgiProcedureCode.Cells[2].Text.ToString());
                    _objAL.Add(dgiProcedureCode.Cells[3].Text.ToString());
                    _objAL.Add(_type);
                    _objAL.Add(txtCompanyID.Text);
                    //_objAL.Add(extddlVisitRoom.Text);
                    _objAL.Add(extddlSpeciality.Text);
                    _visitBO.saveVisit(_objAL);

                    _objAL.Clear();

                    _procBO = new Bill_Sys_Visit_BO();
                    _objAL.Add(dgiProcedureCode.Cells[1].Text.ToString());
                    _objAL.Add(dgiProcedureCode.Cells[2].Text.ToString());
                    _objAL.Add(dgiProcedureCode.Cells[3].Text.ToString());
                    _objAL.Add(dgiProcedureCode.Cells[4].Text.ToString());
                    _objAL.Add(dgiProcedureCode.Cells[5].Text.ToString());
                    _objAL.Add(extddlSpeciality.Text);
                    _procBO.saveProc(_objAL);
                    

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (chkProcedureCode.Checked == true)
            {
                txtProcedureCode.Text = txtSearchText.Text;
            }
            if (chkProcedureCodeDescription.Checked == true)
            {
                txtProcedureDesc.Text = txtSearchText.Text;
            }
            GetProcedureCode();
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


    protected void btnConToVisit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            SaveProcCode("Visits");
            lblMsg.Visible = true;
            lblMsg.Text = " Procedure Code Saved successfully ! ";
            GetProcedureCode();
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
    protected void btnConToTreatment_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveProcCode("Treatments");
            lblMsg.Visible = true;
            lblMsg.Text = " Procedure Code Saved successfully ! ";
            GetProcedureCode();
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
    protected void btnConToTest_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SaveProcCode("Test");
            lblMsg.Visible = true;
            lblMsg.Text = " Procedure Code Saved successfully ! ";
            GetProcedureCode();
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
