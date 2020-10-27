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
using log4net;

public partial class AJAX_Pages_Bill_Sys_Employer_Procedure_Codes : PageBase
{
    private static ILog log = LogManager.GetLogger("AJAX_Pages_Bill_Sys_Employer_Procedure_Codes");
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_Visit_BO _visitBO;
    private ArrayList _objAL;
    private Bill_Sys_DeleteBO _deleteOpeation;
    private Bill_Sys_SystemObject objSystemObject;
    private bool Allow_modifier = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", "return Check();");
        btnUpdate.Attributes.Add("onclick", "return Check();");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
       
      
        btnDelete.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btnCopy.Attributes.Add("onclick", "return Validate();");
        btnUpdateModifier.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btnUpdatePre.Attributes.Add("onclick", "return Confirm_Delete_Code();");
     
        this.conEmployer.SourceGrid = grdEmployerProcedure;
        this.txtSearchBox.SourceGrid = grdEmployerProcedure;
        this.grdEmployerProcedure.Page = this.Page;
        this.grdEmployerProcedure.PageNumberList = this.conEmployer;

        if (!IsPostBack)
        {
            txtEmployerID.Text = " ";
            this.ajAutoEmp.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.ajAutoEmpTo.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlProCodeGroup.Flag_ID = txtCompanyID.Text;
            extddlLocation.Flag_ID = txtCompanyID.Text;
            grdEmployerProcedure.XGridBindSearch();
            BindAmountGrid("");
            //BindGrid();
            btnUpdate.Enabled = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txthdnCode.Text = txtProcedureCode.Text;
        if (txtEmployerCompany.Text == "")
        {
            hdninsurancecode.Value = "";
        }
        txtEmployerID.Text = hdninsurancecode.Value;
        txthdnDesc.Text = txtProcedureDesc.Text;
        txthdnModifier.Text = txtModifier.Text;
        txthdnAmt.Text = txtProcedureAmount.Text;
        txthdnSpe.Text = extddlProCodeGroup.Text;
        txthdnVisitType.Text = "0";
        txtLocation.Text = extddlLocation.Text;
        if (rdoVisitType.SelectedValue.ToString() != "")
        {
            txthdnVisitType.Text = rdoVisitType.SelectedValue.ToString();
        }
        txtPreList.Text = "";
        if (chkPrefList.Checked)
        {
            txtPreList.Text = "1";
        }

        grdEmployerProcedure.XGridBindSearch();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            ArrayList arrObj = new ArrayList();
            for (int i = 0; i < grdEmployerProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdEmployerProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    //_bill_Sys_ProcedureCode_BO.Copy_EmployerProcedureCode(hdninsurancecode.Value, hdnToinsurancecode.Value, grdEmployerProcedure.DataKeys[i]["SZ_PROCEDURE_ID"].ToString()); 
                    EmployerCopyTo objEmployerCopyTo = new EmployerCopyTo();
                    objEmployerCopyTo.EmployerFrom = grdEmployerProcedure.DataKeys[i]["SZ_EMPLOYER_ID"].ToString();
                    objEmployerCopyTo.EmployerTO = hdnToinsurancecode.Value;
                    objEmployerCopyTo.ProcedureCodeID = grdEmployerProcedure.DataKeys[i]["I_PROCEDURE_ID"].ToString();
                    objEmployerCopyTo.CompanyID = txtCompanyID.Text;
                    arrObj.Add(objEmployerCopyTo);
                }
            }
            if (arrObj.Count == 0)
            {
                usrMessage.PutMessage("No Recored is Selected");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
                return;
            }
        
            EmployerCopyTo objEmployerCopy = new EmployerCopyTo();
            objEmployerCopy = objEmployerCopy.Copy_EmployerProcedureCode(arrObj);
            if (objEmployerCopy.SucessMsg != "Error")
            {

                grdEmployerProcedure.XGridBindSearch();
                usrMessage.PutMessage("Procedure codes copied successfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                if (objEmployerCopy.DuplicateMsg != "")
                {
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "alert('" + objEmployerCopy.DuplicateMsg.ToString() + "');", true);
                }
            }
            else
            {
                usrMessage.PutMessage("unable to save procedure codes");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            int iVisitType = 0;
            if (rdoVisitType.SelectedValue.ToString() != "")
            {
                iVisitType = Convert.ToInt32(rdoVisitType.SelectedValue);
            }
            string szAddToPreferred = "0";

            if (chkPrefList.Checked)
            {
                szAddToPreferred = "1";
            }


            //_bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, "", "ADD", szAddToPreferred,txtRevCode.Text,txtValueCode.Text);
            _bill_Sys_ProcedureCode_BO.Save_Update_EmployerProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text,hdninsurancecode.Value, txtCompanyID.Text, "", "ADD", szAddToPreferred, txtRevCode.Text, txtValueCode.Text, txtProcedureLongDesc.Text, txtModifierDesc.Text, txtRVU.Text, extddlLocation.Text);

            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //using (Utils utility = new Utils())
                //{
                //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                //}
                //string str2 = "Error Request=" + id + ".Please share with Technical support.";
                //base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }

            ClearControls();
            grdEmployerProcedure.XGridBindSearch();

            usrMessage.PutMessage("Procedure codes save successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

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
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            int iVisitType = 0;
            if (rdoVisitType.SelectedValue.ToString() != "")
            {
                iVisitType = Convert.ToInt32(rdoVisitType.SelectedValue);
            }

            string szAddToPreferred = "0";

            if (chkPrefList.Checked)
            {
                szAddToPreferred = "1";
            }


                if (Session["ProcedureCodeID"] != null || Session["ProcedureCodeID"].ToString() != "&nbsp;")
                {
                    //_bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, Session["ProcedureCodeID"].ToString(), "UPDATE", szAddToPreferred, txtRevCode.Text, txtValueCode.Text);
                    _bill_Sys_ProcedureCode_BO.Save_Update_EmployerProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtEmployerID.Text, txtCompanyID.Text, Session["ProcedureCodeID"].ToString(), "UPDATE", szAddToPreferred, txtRevCode.Text, txtValueCode.Text, txtProcedureLongDesc.Text, txtModifierDesc.Text, txtRVU.Text, extddlLocation.Text);
                }

                try
                {
                    BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                    procs.Refresh();
                }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //using (Utils utility = new Utils())
                //{
                //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                //}
                //string str2 = "Error Request=" + id + ".Please share with Technical support.";
                //base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }

            ClearControls();
                grdEmployerProcedure.XGridBindSearch();

                usrMessage.PutMessage("Procedure codes Updated successfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();


            
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {   
        ClearControls();
    }

    protected void btnUpdateModifier_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < grdEmployerProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdEmployerProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    string szProcCodeID = grdEmployerProcedure.DataKeys[i]["I_PROCEDURE_ID"].ToString();
                    arr.Add(szProcCodeID);
                }
            }
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            _bill_Sys_ProcedureCode_BO.Update_Employer_Modifier(arr, txtCompanyID.Text, txtModifier.Text);
            ClearControls();
            grdEmployerProcedure.XGridBindSearch();

            usrMessage.PutMessage("Modifier Updated successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfProcedureCodes = "";
        try
        {
            for (int i = 0; i < grdEmployerProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdEmployerProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {

                    if (!_deleteOpeation.deleteEmployerRecord( grdEmployerProcedure.DataKeys[i]["I_PROCEDURE_ID"].ToString(),txtCompanyID.Text))
                    {
                        if (szListOfProcedureCodes == "")
                        {
                            szListOfProcedureCodes = grdEmployerProcedure.DataKeys[i]["SZ_PROCEDURE_CODE"].ToString() + " -- " + grdEmployerProcedure.DataKeys[i]["SZ_CODE_DESCRIPTION"].ToString();
                        }
                        else
                        {
                            szListOfProcedureCodes = szListOfProcedureCodes + " , " + grdEmployerProcedure.DataKeys[i]["SZ_PROCEDURE_CODE"].ToString() + " -- " + grdEmployerProcedure.DataKeys[i]["SZ_CODE_DESCRIPTION"].ToString();
                        }
                    }
                    
                }
            }
            if (szListOfProcedureCodes != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Procedure codes " + szListOfProcedureCodes + "  exists.'); ", true);
            }
            else
            {
                usrMessage.PutMessage("Procedure codes deleted successfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }

            grdEmployerProcedure.XGridBindSearch();
            usrMessage.PutMessage("Procedure codes deleted successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

           
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

    protected void grdEmployerProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int index = 0;
        try
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    int iIndex = Convert.ToInt32(e.CommandArgument.ToString());

                    rdoVisitType.ClearSelection();

                    Session["ProcedureCodeID"] = grdEmployerProcedure.DataKeys[iIndex]["I_PROCEDURE_ID"].ToString();

                  //  BindAmountGrid(grdEmployerProcedure.DataKeys[iIndex]["I_PROCEDURE_ID"].ToString());

                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_PROCEDURE_CODE"].ToString() != "&nbsp;") { txtProcedureCode.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_PROCEDURE_CODE"].ToString(); }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_DESCRIPTION"].ToString() != "&nbsp;") { txtProcedureDesc.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_DESCRIPTION"].ToString(); }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_PROCEDURE_GROUP"].ToString() != "&nbsp;") { extddlProCodeGroup.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_PROCEDURE_GROUP_ID"].ToString(); }
                    //     if (grdEmployerProcedure.Items[grdEmployerProcedure.SelectedIndex].Cells[5].Text != "&nbsp;") { ddlType.Text = grdEmployerProcedure.Items[grdEmployerProcedure.SelectedIndex].Cells[5].Text; }
                   // if (grdEmployerProcedure.DataKeys[iIndex]["SZ_TYPE_CODE_ID"].ToString() != "&nbsp;") { Session["VisitID"] = grdEmployerProcedure.DataKeys[iIndex]["SZ_TYPE_CODE_ID"].ToString(); }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER"].ToString() != "&nbsp;") { txtModifier.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER"].ToString(); }
                    if (grdEmployerProcedure.DataKeys[iIndex]["FLT_AMOUNT"].ToString() != "&nbsp;") { txtProcedureAmount.Text = grdEmployerProcedure.DataKeys[iIndex]["FLT_AMOUNT"].ToString(); }

                    //if (grdEmployerProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() != "&nbsp;")
                    //{
                    //    rdoVisitType.SelectedValue = grdEmployerProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString();
                    //}
                    //else if (grdEmployerProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() == "&nbsp;" || grdEmployerProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() == "0")
                    //{
                    //    rdoVisitType.ClearSelection();

                    //}
                    if (grdEmployerProcedure.DataKeys[iIndex]["BT_ADD_TO_PREFERRED_LIST"].ToString() != "&nbsp;")
                    {
                        if (grdEmployerProcedure.DataKeys[iIndex]["BT_ADD_TO_PREFERRED_LIST"].ToString().ToLower() == "true")
                        {
                            chkPrefList.Checked = true;
                        }
                    }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "NULL")
                    {
                        txtRevCode.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString();
                    }

                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "NULL")
                    {
                        txtValueCode.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString();
                    }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "NULL")
                    {
                        txtProcedureLongDesc.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString();
                    }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "NULL")
                    {
                        txtModifierDesc.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString();
                    }
                    if (grdEmployerProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "NULL")
                    {
                        txtRVU.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_RVU"].ToString();
                    }
                    if (grdEmployerProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "NULL" && grdEmployerProcedure.DataKeys[iIndex]["sz_location_id"].ToString().ToUpper() != "NA")
                    {
                        extddlLocation.Text = grdEmployerProcedure.DataKeys[iIndex]["sz_location_id"].ToString();
                    }

                     if (grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_ID"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_ID"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_ID"].ToString() != "NULL" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_ID"].ToString().ToUpper() != "NA")
                    {
                        hdninsurancecode.Value = grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_ID"].ToString();
                        txtEmployerID.Text = hdninsurancecode.Value;
                    }
                     if (grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_NAME"].ToString() != "&nbsp;" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_NAME"].ToString() != "" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_NAME"].ToString() != "NULL" && grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_NAME"].ToString().ToUpper() != "NA")
                    {
                        txtEmployerCompany.Text = grdEmployerProcedure.DataKeys[iIndex]["SZ_EMPLOYER_NAME"].ToString();
                    }
                    
                    // if (grdEmployerProcedure.Items[grdEmployerProcedure.SelectedIndex].Cells[8].Text != "&nbsp;") { extddlVisitRoom.Text = grdEmployerProcedure.Items[grdEmployerProcedure.SelectedIndex].Cells[8].Text; }
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
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
            }
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdEmployerProcedure.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdEmployerProcedure.Rows[i].FindControl("lnkM");
                    LinkButton plus1 = (LinkButton)grdEmployerProcedure.Rows[i].FindControl("lnkP");
                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                    }
                }

                //grdEmployerProcedure.Columns[17].Visible = true; // column change 16 to 17 
                //grdEmployerProcedure.Columns[18].Visible = false;// column change 17 to 18 
                //grdEmployerProcedure.Columns[19].Visible = false;// column change 18 to 19 
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdEmployerProcedure.DataKeys[index]["SZ_PROCEDURE_ID"].ToString();
                GridView gv = (GridView)grdEmployerProcedure.Rows[index].FindControl("GridView2");
                LinkButton plus = (LinkButton)grdEmployerProcedure.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdEmployerProcedure.Rows[index].FindControl("lnkM");
                string caseid = grdEmployerProcedure.DataKeys[index][0].ToString();
                DataSet objds = new DataSet();
                //  objds = objDAO.GetLitigatedBills(grdLitigationDesk.DataKeys[index][0].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extdlitigate.Text, ddlTransferStatus.Text);
                //objds = objDAO.GetLitigatedBillsInfo(grdEmployerProcedure.DataKeys[index][2].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extdlitigate.Text);
                DataTable dt = new DataTable();
                dt.Columns.Add("SZ_CODE_LONG_DESC");
                dt.Columns.Add("SZ_MODIFIER_LONG_DESC");
                dt.Columns.Add("SZ_RVU");
                DataRow dr = dt.NewRow();
                dr["SZ_CODE_LONG_DESC"] = grdEmployerProcedure.DataKeys[index]["SZ_CODE_LONG_DESC"].ToString();
                dr["SZ_MODIFIER_LONG_DESC"] = grdEmployerProcedure.DataKeys[index]["SZ_MODIFIER_LONG_DESC"].ToString();
                dr["SZ_RVU"] = grdEmployerProcedure.DataKeys[index]["SZ_RVU"].ToString();
                dt.Rows.Add(dr);

                gv.DataSource = dt;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;
            }
            //int index = 0;     
            if (e.CommandName.ToString() == "MNS")
            {
                //grdEmployerProcedure.Columns[17].Visible = false; // column change 16 to 17 By kapil 26 March 2012
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdEmployerProcedure.DataKeys[index]["SZ_PROCEDURE_ID"].ToString();
                LinkButton plus = (LinkButton)grdEmployerProcedure.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdEmployerProcedure.Rows[index].FindControl("lnkM");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdEmployerProcedure.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            extddlProCodeGroup.Text = "NA";
            txtProcedureCode.Text = "";
            txtRevCode.Text = "";
            txtValueCode.Text = "";
            txtProcedureDesc.Text = "";
            txtProcedureAmount.Text = "";
            txtModifier.Text = "";
            txtProcedureLongDesc.Text = "";
            txtModifierDesc.Text = "";
            txtRVU.Text = "";
            Session["ProcedureCodeID"] = "";
            Session["VisitID"] = null;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            chkPrefList.Checked = false;
            extddlLocation.Text = "NA";
            rdoVisitType.ClearSelection();
            txtEmployerCompany.Text = "";
            txtEmployerCompanyTo.Text = "";
            hdninsurancecode.Value = "";
            txtEmployerID.Text = "";
            hdnToinsurancecode.Value = "";
            hdninsurancecode.Value = "";
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUpdatePre_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < grdEmployerProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdEmployerProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    string szProcCodeID = grdEmployerProcedure.DataKeys[i]["I_PROCEDURE_ID"].ToString();
                    arr.Add(szProcCodeID);
                }
            }
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            if (chkPrefList.Checked)
            {
                _bill_Sys_ProcedureCode_BO.Update_Employer_PreferredList(arr, txtCompanyID.Text, "1");
            }
            else
            {
                _bill_Sys_ProcedureCode_BO.Update_Employer_PreferredList(arr, txtCompanyID.Text, "0");
            }
            ClearControls();
            grdEmployerProcedure.XGridBindSearch();

            usrMessage.PutMessage("Modifier Updated successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

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