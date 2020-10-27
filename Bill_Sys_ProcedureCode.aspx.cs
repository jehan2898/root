/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_ProcedureCode.aspx.cs
/*Purpose              :       To Add and Edit Payment Scheme 
/*Author               :       Manoj c
/*Date of creation     :       11 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/

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
public partial class Bill_Sys_ProcedureCode : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_Visit_BO _visitBO;
    private ArrayList _objAL;
    private Bill_Sys_DeleteBO _deleteOpeation;
   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return formValidator('frmProcedureCode','txtProcedureCode,extddlProCodeGroup,ddlType,txtProcedureAmount');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmProcedureCode','txtProcedureCode,extddlProCodeGroup,ddlType,txtProcedureAmount');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            extddlProCodeGroup.Flag_ID = txtCompanyID.Text;
            //extddlVisitRoom.Flag_ID = txtCompanyID.Text;
            
            if (!IsPostBack)
            {
                BindAmountGrid("");
                BindGrid();         
                btnUpdate.Enabled = false;
            }

            _deleteOpeation = new Bill_Sys_DeleteBO();
            if (_deleteOpeation.checkForDelete(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                btnDelete.Visible = false;
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
            cv.MakeReadOnlyPage("Bill_Sys_ProcedureCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Event Handler"   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (rdoVisitType.SelectedValue.ToString() != "")
        {
            txtVisitType.Text = rdoVisitType.SelectedValue.ToString();
        }
        else
        {
            txtVisitType.Text = "0";
        }
        _saveOperation = new SaveOperation();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "procedurecode.xml";
            _saveOperation.SaveMethod();

            _visitBO = new Bill_Sys_Visit_BO();
             _objAL = new ArrayList();
            _objAL.Add(txtProcedureCode.Text);
            _objAL.Add(txtProcedureDesc.Text);
         //   _objAL.Add(txtProcedureAmount.Text);
            _objAL.Add(ddlType.SelectedItem.ToString());
            _objAL.Add(txtCompanyID.Text);
            _objAL.Add(extddlProCodeGroup.Text);
            _visitBO.saveVisit(_objAL);
            SaveAmount();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = " Procedure Code Saved successfully ! ";
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
        _editOperation = new EditOperation();
        txtVisitType.Text = rdoVisitType.SelectedValue.ToString();
        try
        {
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "procedurecode.xml";
            _editOperation.Primary_Value = Session["ProcedureCodeID"].ToString();
            _editOperation.UpdateMethod();

            if (Session["VisitID"] != null)
            {
                txtVisitID.Text = ddlType.SelectedItem.ToString(); // changed by shailesh 1-April-2010, accepted the selected value of ddltype
                 txtRoomId.Text = extddlProCodeGroup.Text;
                _editOperation = new EditOperation();
                _editOperation.Primary_Value = Session["VisitID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "NewVisit.xml";
                _editOperation.UpdateMethod();
            }
            else
            {
                _visitBO = new Bill_Sys_Visit_BO();
                _objAL = new ArrayList();
                _objAL.Add(txtProcedureCode.Text);
                _objAL.Add(txtProcedureDesc.Text);
          //      _objAL.Add(txtProcedureAmount.Text);
                _objAL.Add(ddlType.SelectedItem.ToString()); // changed by shailesh 1-April-2010, accepted the selected value of ddltype
                _objAL.Add("");
                _objAL.Add(txtCompanyID.Text);
                _objAL.Add(extddlProCodeGroup.Text);
                _visitBO.saveVisit(_objAL);
            }
            UpdateAmount();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = " Procedure Code Updated successfully ! ";

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
            extddlProCodeGroup.Text = "NA";
            txtProcedureCode.Text = "";
            txtProcedureDesc.Text = "";
            txtProcedureAmount.Text = "";
            Session["ProcedureCodeID"] = "";
            //extddlVisitRoom.Text="NA";
            //ddlType.Text="0";
            Session["VisitID"] = null;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Visible = false;
            rdoVisitType.ClearSelection();

            BindAmountGrid("");
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
    protected void grdProcedure_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            rdoVisitType.ClearSelection();
            Session["ProcedureCodeID"] = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[1].Text;
            BindAmountGrid(grdProcedure.Items[grdProcedure.SelectedIndex].Cells[1].Text);
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[2].Text != "&nbsp;") { txtProcedureCode.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[2].Text; }
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[4].Text != "&nbsp;"){ txtProcedureDesc.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[4].Text;}
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[6].Text != "&nbsp;") { extddlProCodeGroup.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[6].Text; }
       //     if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[5].Text != "&nbsp;") { ddlType.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[5].Text; }
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[9].Text != "&nbsp;") { Session["VisitID"] = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[9].Text; }
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[10].Text != "&nbsp;") { ddlType.SelectedValue = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[10].Text; }
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[14].Text != "&nbsp;") { txtProcedureAmount.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[14].Text; }
            if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text != "&nbsp;")
            {
                rdoVisitType.SelectedValue = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text;
            }
            else if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text == "&nbsp;" || grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text == "0")
            {
                rdoVisitType.ClearSelection();
            
            }
            
           // if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text != "&nbsp;") { extddlVisitRoom.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text; }
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            lblMsg.Visible = false;
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
    protected void grdProcedure_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdProcedure.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
            lblMsg.Visible = false;
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
    #region "Fetch Method"
    private void BindGrid()
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
            _listOperation.Xml_File = "procedurecode.xml";
            _listOperation.LoadList();

            if (txtVisitType.Text != "-1")
            {
                rdoVisitType.SelectedValue = txtVisitType.Text;
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

    private void BindAmountGrid(string procedureCodeId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            grdAmount.DataSource = _bill_Sys_ProcedureCode_BO.GetProcedure_Code_Amount_List(procedureCodeId, txtCompanyID.Text);
            grdAmount.DataBind();

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

    private void UpdateAmount()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList  arrayList;
         _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
         string strLatestid = _bill_Sys_ProcedureCode_BO.GetLatestProcedureCodeId(txtCompanyID.Text);
        try
        {
            
           
            foreach (DataGridItem item in grdAmount.Items)
            {
                arrayList = new ArrayList();
                if (item.Cells[0].Text != "&nbsp;" ) { arrayList.Add(item.Cells[0].Text); }
                if (item.Cells[0].Text != "&nbsp;" ) { arrayList.Add(item.Cells[4].Text); } else {  arrayList.Add(Session["ProcedureCodeID"].ToString()); }
                arrayList.Add(item.Cells[1].Text);
               
                arrayList.Add(((TextBox)item.Cells[3].FindControl("txtAmount")).Text);
                arrayList.Add(txtCompanyID.Text);

                if (item.Cells[0].Text != "&nbsp;" ) { _bill_Sys_ProcedureCode_BO.UpdateProcedure_Code_Amount(arrayList); } else { _bill_Sys_ProcedureCode_BO.SaveProcedure_Code_Amount(arrayList); }
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


    private void SaveAmount()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList arrayList;
       
        try
        {
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            string strLatestid = _bill_Sys_ProcedureCode_BO.GetLatestProcedureCodeId(txtCompanyID.Text);
            foreach (DataGridItem item in grdAmount.Items)
            {
                arrayList = new ArrayList();
                arrayList.Add(strLatestid);
                arrayList.Add(item.Cells[1].Text);
                arrayList.Add(((TextBox)item.Cells[3].FindControl("txtAmount")).Text);
                arrayList.Add(txtCompanyID.Text);
                _bill_Sys_ProcedureCode_BO.SaveProcedure_Code_Amount(arrayList);
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
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList objAL = new ArrayList();
            objAL.Add(txtProcedureCode.Text);
            objAL.Add(txtProcedureDesc.Text);
            objAL.Add(ddlType.SelectedValue);
            objAL.Add(extddlProCodeGroup.Text);
            objAL.Add(txtCompanyID.Text);
            objAL.Add("SEARCH");

            Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
            grdProcedure.DataSource = objProcBO.Search_ProcedureCodes(objAL);
            grdProcedure.DataBind();
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
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfProcedureCodes = "";
        try
        {
            for (int i = 0; i < grdProcedure.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    
                    if (!_deleteOpeation.deleteRecord("SP_MST_PROCEDURE_CODES", "@SZ_PROCEDURE_ID", grdProcedure.Items[i].Cells[1].Text))
                    {
                        if (szListOfProcedureCodes == "")
                        {
                            szListOfProcedureCodes = grdProcedure.Items[i].Cells[2].Text + " -- " + grdProcedure.Items[i].Cells[3].Text;
                        }
                        else
                        {
                            szListOfProcedureCodes = szListOfProcedureCodes + " , " + grdProcedure.Items[i].Cells[2].Text + " -- " + grdProcedure.Items[i].Cells[3].Text; 
                        }
                    }
                    _deleteOpeation.deleteRecord("SP_DELETE_BILL_PROC_TYPE", "@SZ_TYPE_CODE_ID", grdProcedure.Items[i].Cells[9].Text);
                }
            }
            if (szListOfProcedureCodes != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Procedure codes " + szListOfProcedureCodes + "  exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Procedure codes deleted successfully ...";
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

    protected void grdProcedure_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "DescriptionSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }

            if (e.CommandName.ToString() == "CodeSearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
                BindGrid();
            }

            if (e.CommandName.ToString() == "SpecialitySearch")
            {
                if (txtSearchOrder.Text == e.CommandArgument + " ASC")
                {
                    txtSearchOrder.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSearchOrder.Text = e.CommandArgument + " ASC";
                }
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
