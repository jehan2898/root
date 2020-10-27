using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class AJAX_Pages_Dignosis_Code_Visit_Popup : PageBase
{

    #region Local Variables
    string strContextKey;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    ArrayList ALSelectedDignosisCodes=new ArrayList();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        strContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) + "," + rbl_SZ_TYPE_ID.SelectedValue;
        this.ajDignosisCode.ContextKey = strContextKey;
        if (!IsPostBack)
        {
            strContextKey = (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) + "," + rbl_SZ_TYPE_ID.SelectedValue;
            this.ajDignosisCode.ContextKey = strContextKey;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlDiagnosisType.Flag_ID = this.txtCompanyID.Text;
            //this.cmbDiagnosisCodes.DataSource = SelectDiagnosisLookup();
            //this.cmbDiagnosisCodes.DataBind();
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < this.grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
                if (box.Checked)
                {
                    ListItem item = new ListItem(this.grdDiagonosisCode.Items[i].Cells[2].Text + '-' + this.grdDiagonosisCode.Items[i].Cells[4].Text, this.grdDiagonosisCode.Items[i].Cells[1].Text);
                    //if (!this.lstDgCodes.Items.Contains(item))
                    //{
                    //    this.lstDgCodes.Items.Add(item);
                    //    ALSelectedDignosisCodes.Add(item);
                    //}
                }
            }
            //this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["SelectedDGCode"] = ALSelectedDignosisCodes;
        ScriptManager.RegisterStartupScript(this, GetType(), "ReloadParent", "ReloadParent();", true);
    }

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text, rbl_SZ_TYPE_ID.SelectedValue);
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

    private void BindDiagnosisGrid(string typeid, string code, string description, string SZ_TYPE_ID)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description, SZ_TYPE_ID);
            this.grdDiagonosisCode.DataBind();
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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text == "NA")
            {
                this.BindDiagnosisGrid("");
            }
            else
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            //ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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

    private void BindDiagnosisGrid(string typeid)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
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

    protected void lnkAddDignosisCode_OnClick(object sender, EventArgs e)
    {
        //string[] dgarr = txtSearchDignosisCode.Text.Split(',');
        //ListItem item = new ListItem(dgarr[1],dgarr[0]);
        //if (!this.lstDgCodes.Items.Contains(item))
        //{
        //    this.lstDgCodes.Items.Add(item);
        //    ALSelectedDignosisCodes.Add(item);
        //    txtSearchDignosisCode.Text = "";
        //}
    }


}