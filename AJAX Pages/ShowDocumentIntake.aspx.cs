using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_ShowDocumentIntake : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            hdnProvidertId.Value = Request.QueryString["i_id"].ToString();
            hdnUserId.Value = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            hdnCompanyId.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.ddlCaseType.Flag_ID = hdnCompanyId.Value;
            Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
            //Intakedao.sz_case_type_id = ddlCaseType.Text;
            //Intakedao.sz_company_id = hdnCompanyId.Value;
            //bindgrdIntakeDocCaseTyp();
        }
        if (Request.QueryString["i_id"] != null)
        {
            int IntakesheetProId =Convert.ToInt32(Request.QueryString["i_id"].ToString());

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


        Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
        try
        {

            ArrayList arrList = new ArrayList();
            for (int i = 0; i < grdShowIntakeDocument.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdShowIntakeDocument.Columns[0];
                CheckBox box = (CheckBox)this.grdShowIntakeDocument.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
                if(box.Checked)
                {
                      Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
                      Intakedao.i_id = 0;
                      string iDocumentID = this.grdShowIntakeDocument.GetRowValues(i, new string[] { "i_id" }).ToString();
                      Intakedao.i_provider_id = Convert.ToInt32(hdnProvidertId.Value);
                      Intakedao.sz_company_id = hdnCompanyId.Value;
                      Intakedao.sz_case_type_id = ddlCaseType.Text;
                      Intakedao.i_user_id = hdnUserId.Value;
                      Intakedao.i_documnet_id = Convert.ToInt32(iDocumentID);
                      arrList.Add(Intakedao);
                     
                }
                
            }
         string sREturn=   IntakeBo.save_intake_documnet(arrList);

      
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

    public void bindgrdShowIntakeDocument()
    {
        DataSet dsgrd = new DataSet();
        Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
        Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();

        Intakedao.sz_company_id = hdnCompanyId.Value;
        Intakedao.sz_case_type_id = ddlCaseType.Text;

        dsgrd = IntakeBo.select_intake_document(Intakedao);

        grdShowIntakeDocument.DataSource = dsgrd.Tables[0];
        grdShowIntakeDocument.DataBind();


    }
    protected void ddlCaseType_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
        Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
        try
        {

            Intakedao.i_id = 0;

            Intakedao.sz_case_id = hdnCaseId.Value;
            Intakedao.sz_company_id = hdnCompanyId.Value;
            Intakedao.sz_case_type_id = ddlCaseType.Text;
            Intakedao.i_user_id = hdnUserId.Value;
            grdShowIntakeDocument.DataSource = IntakeBo.Show_intake_document(Intakedao);
            grdShowIntakeDocument.DataBind();
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
}