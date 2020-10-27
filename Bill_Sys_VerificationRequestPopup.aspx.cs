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

public partial class Bill_Sys_VerificationRequestPopup : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extBillStatus.Flag_ID = txtCompanyID.Text;
            txtViewBillNumber.Text = Request.QueryString["BillNo"].ToString();
            //btnSave.Attributes.Add("onclick", "return formValidator('frmBillStatus','extBillStatus');");
            //btnUpdate.Attributes.Add("onclick", "return formValidator('frmBillStatus','extBillStatus');");

            btnSave.Attributes.Add("onclick", "return CheckedBillStatus();");
            //btnUpdate.Attributes.Add("onclick", "return formValidator('frmDoctor','extBillStatus');");
            
            if (!IsPostBack)
            {
                txtVerificationNotes.Text = "";
                btnUpdate.Enabled = false;
                Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
                DataSet dset = new DataSet();
                dset = _bill_Sys_NotesBO.GetBillDetailsVerificationPopUp(txtCompanyID.Text, txtViewBillNumber.Text);
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {
                    //txtVerificationNotes.Text = dset.Tables[0].Rows[i]["NOTES"].ToString();
                    txtVisitDate.Text = dset.Tables[0].Rows[i]["DT_VISIT_DATE"].ToString();
                    //txtVerificationDate.Text = dset.Tables[0].Rows[i]["DT_VERIFICATION_DATE"].ToString();
                    extBillStatus.Text = dset.Tables[0].Rows[i]["STATUS_ID"].ToString();
                }
                txtVerificationDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                BindGrid();
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
            cv.MakeReadOnlyPage("Bill_Sys_VerificationRequestPopup.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dset;
        try
        {
            Bill_Sys_NotesBO _bill_Sys_NotesBO = new Bill_Sys_NotesBO();
            dset = new DataSet();
            dset = _bill_Sys_NotesBO.GetBillDetailsFillGrid(txtCompanyID.Text, txtViewBillNumber.Text);
            grdVerificationReq.DataSource = dset.Tables[0];
            grdVerificationReq.DataBind();

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
            string sz_Bill_status_id = extBillStatus.Text;
            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
            string sz_status_code = _obj.GetStatusCode(txtCompanyID.Text,sz_Bill_status_id);
            //VerificationReceived
            if (sz_status_code == "vr")
            {
                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                ArrayList objAL = new ArrayList();

                objAL.Add(txtViewBillNumber.Text);
                objAL.Add(txtVerificationNotes.Text);
                objAL.Add(txtCompanyID.Text);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                obj.saveVerificationReceivedInformation(objAL);
                BindGrid();
            }

            //Verification Request
            if (sz_status_code == "vs")
            {
                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                ArrayList objAL = new ArrayList();

                objAL.Add(txtViewBillNumber.Text);
                objAL.Add(txtVerificationNotes.Text);
                objAL.Add(txtCompanyID.Text);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                obj.SaveVerificationRequest(objAL);

                BindGrid();
            }

            // Denial status
            if (sz_status_code == "den")
            {
                //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                ArrayList objAL = new ArrayList();
                string szListOfBillIDs = "";
                string szBillNumbers = "";
                string SZDENIALUSERID = "";
                Boolean flag = false;
                //for (int i = 0; i < grdBillSearch.Items.Count; i++)
                //{
                //    CheckBox chkDenial = ((CheckBox)grdBillSearch.Items[i].Cells[20].FindControl("chkDenial"));

                //    szBillNumbers = "'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //    //szListOfBillIDs = "'" + grdBillSearch.Items[i].Cells[2].Text + "'";
                //    if (chkDenial.Checked)
                //    {
                //        if (flag == false)
                //        {
                //            szListOfBillIDs = "'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //            flag = true;
                //        }
                //        else
                //        {
                //            szListOfBillIDs = szListOfBillIDs + ",'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //        }
                //    }

                //}
                objAL.Add("'" + txtViewBillNumber.Text + "'");
                objAL.Add(txtCompanyID.Text);
                objAL.Add(UserID);
                objAL.Add(txtVerificationNotes.Text);

                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                obj.updateBillStatusToDenial(objAL);
                BindGrid();
            }
            ClearControls();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_Sys_VerificationsentBills.aspx';window.self.close(); </script>");
            
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

    protected void grdVerificationReq_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Select")
            {
                extBillStatus.Text = e.Item.Cells[5].Text.ToString();
                if (e.Item.Cells[2].Text.ToString() == "&nbsp;")
                {
                    txtVerificationNotes.Text = "";
                }
                else
                {
                    txtVerificationNotes.Text = e.Item.Cells[2].Text.ToString();
                }
                extBillStatus.Enabled = false;
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string sz_Bill_status_id = extBillStatus.Text;
            Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
            string sz_status_code = _obj.GetStatusCode(txtCompanyID.Text, sz_Bill_status_id);
            //VerificationReceived
            if (sz_status_code == "vr")
            {
                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                ArrayList objAL = new ArrayList();

                objAL.Add(txtViewBillNumber.Text);
                objAL.Add(txtVerificationNotes.Text);
                objAL.Add(txtCompanyID.Text);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                obj.UpdateVerificationReceivedInformation(objAL);
                BindGrid();
            }

            //Verification Request
            if (sz_status_code == "vs")
            {
                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                ArrayList objAL = new ArrayList();

                objAL.Add(txtViewBillNumber.Text);
                objAL.Add(txtVerificationNotes.Text);
                objAL.Add(txtCompanyID.Text);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                obj.UpdateVerificationRequest(objAL);

                BindGrid();
            }

            // Denial status
            if (sz_status_code == "den")
            {
                //txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                string UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                ArrayList objAL = new ArrayList();
                string szListOfBillIDs = "";
                string szBillNumbers = "";
                string SZDENIALUSERID = "";
                Boolean flag = false;
                //for (int i = 0; i < grdBillSearch.Items.Count; i++)
                //{
                //    CheckBox chkDenial = ((CheckBox)grdBillSearch.Items[i].Cells[20].FindControl("chkDenial"));

                //    szBillNumbers = "'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //    //szListOfBillIDs = "'" + grdBillSearch.Items[i].Cells[2].Text + "'";
                //    if (chkDenial.Checked)
                //    {
                //        if (flag == false)
                //        {
                //            szListOfBillIDs = "'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //            flag = true;
                //        }
                //        else
                //        {
                //            szListOfBillIDs = szListOfBillIDs + ",'" + grdBillSearch.Items[i].Cells[1].Text + "'";
                //        }
                //    }

                //}
                objAL.Add("'" + txtViewBillNumber.Text + "'");
                objAL.Add(txtCompanyID.Text);
                objAL.Add(UserID);
                objAL.Add(txtVerificationNotes.Text);

                Bill_Sys_BillTransaction_BO obj = new Bill_Sys_BillTransaction_BO();
                obj.UpdateDenialNotes(objAL);
                BindGrid();

            }
            ClearControls();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_Sys_VerificationsentBills.aspx';window.self.close(); </script>");
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

    public void ClearControls()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtVerificationNotes.Text = "";
            extBillStatus.Selected_Text = "--- Select ---";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList objarr;
        Bill_Sys_BillTransaction_BO obj;
        
        try
        {
            for (int i = 0; i < grdVerificationReq.Items.Count; i++)
            {
                if (((CheckBox)grdVerificationReq.Items[i].Cells[6].FindControl("chkDelete")).Checked == true)
                {
                    Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();
                    string sz_status_code = _obj.GetStatusCode(txtCompanyID.Text,grdVerificationReq.Items[i].Cells[5].Text.ToString());
                    objarr = new ArrayList();
                    objarr.Add(txtCompanyID.Text);
                    objarr.Add(txtViewBillNumber.Text);
                    objarr.Add(sz_status_code);

                    obj = new Bill_Sys_BillTransaction_BO();
                    obj.DeleteVerificationNotes(objarr);
                }
            }
            BindGrid();
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControls();
            extBillStatus.Enabled = true;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
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
