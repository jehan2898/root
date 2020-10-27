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

public partial class AJAX_Pages_Bill_Sys_ProcedureCode : PageBase
{
    private static ILog log = LogManager.GetLogger("AJAX_Pages_Bill_Sys_ProcedureCode");
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
        btnSave.Attributes.Add("onclick", "return validation();");
        btnUpdate.Attributes.Add("onclick", "return validation();");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        btnDelete.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btnUpdateModifier.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btnUpdatePre.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        extddlProCodeGroup.Flag_ID = txtCompanyID.Text;
        extddlLocation.Flag_ID = txtCompanyID.Text;
        this.con.SourceGrid = grdProcedure;
        this.txtSearchBox.SourceGrid = grdProcedure;
        this.grdProcedure.Page = this.Page;
        this.grdProcedure.PageNumberList = this.con;

        if (!IsPostBack)
        {
           grdProcedure.XGridBindSearch();
            BindAmountGrid("");
            //BindGrid();
            btnUpdate.Enabled = false;
            BindIns();
        }
    }

    protected void BindIns()
    {
        Insurance_Group objInsuranceGroup = new Insurance_Group();

   lstIns.DataSource= objInsuranceGroup.Get_Insurance_Group(txtCompanyID.Text);
        lstIns.DataTextField = "DESCRIPTION";
        lstIns.DataValueField = "CODE";
        lstIns.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txthdnCode.Text = txtProcedureCode.Text;
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

        grdProcedure.XGridBindSearch();
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

            DataTable dtInsid = new DataTable();
            dtInsid.Columns.Add("sz_insurance_id");
            for (int i = 0; i < lstIns.Items.Count; i++)
            {
                if(lstIns.Items[i].Selected)
                {
                    DataRow drInsid = dtInsid.NewRow();
                    drInsid["sz_insurance_id"] = lstIns.Items[i].Value;
                    dtInsid.Rows.Add(drInsid);
                }
            }


            //_bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, "", "ADD", szAddToPreferred,txtRevCode.Text,txtValueCode.Text);
            _bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, "", "ADD", szAddToPreferred, txtRevCode.Text, txtValueCode.Text, txtProcedureLongDesc.Text, txtModifierDesc.Text, txtRVU.Text,extddlLocation.Text,txt1500desc.Text,txtApplyDate.Text,txtContractAmount.Text,dtInsid);

            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception io)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(io);
            }


            ClearControls();
            grdProcedure.XGridBindSearch();

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

            if (Session["ProcedureCodeID"] != null || Session["ProcedureCodeID"].ToString() !="&nbsp;")
            {
                //_bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, Session["ProcedureCodeID"].ToString(), "UPDATE", szAddToPreferred, txtRevCode.Text, txtValueCode.Text);
                DataTable dtInsid = new DataTable();
                dtInsid.Columns.Add("sz_insurance_id");
                for (int i = 0; i < lstIns.Items.Count; i++)
                {
                    if (lstIns.Items[i].Selected)
                    {
                        DataRow drInsid = dtInsid.NewRow();
                        drInsid["sz_insurance_id"] = lstIns.Items[i].Value;
                        dtInsid.Rows.Add(drInsid);
                    }
                }
                _bill_Sys_ProcedureCode_BO.Save_Update_ProcedureCode(txtProcedureCode.Text, extddlProCodeGroup.Text, txtProcedureDesc.Text, txtProcedureAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, Session["ProcedureCodeID"].ToString(), "UPDATE", szAddToPreferred, txtRevCode.Text, txtValueCode.Text, txtProcedureLongDesc.Text, txtModifierDesc.Text, txtRVU.Text,extddlLocation.Text,txt1500desc.Text,txtApplyDate.Text,txtContractAmount.Text,dtInsid);
            }

            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }


            ClearControls();
            grdProcedure.XGridBindSearch();

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
            for (int i = 0; i < grdProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    string szProcCodeID = grdProcedure.DataKeys[i]["SZ_PROCEDURE_ID"].ToString();
                    arr.Add(szProcCodeID);
                }
            }
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            _bill_Sys_ProcedureCode_BO.Update_Modifier(arr, txtCompanyID.Text, txtModifier.Text);
            ClearControls();
            grdProcedure.XGridBindSearch();

            usrMessage.PutMessage("Modifier Updated successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
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
            for (int i = 0; i < grdProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {

                    if (!_deleteOpeation.deleteRecord("SP_MST_PROCEDURE_CODES", "@SZ_PROCEDURE_ID", grdProcedure.DataKeys[i]["SZ_PROCEDURE_ID"].ToString()))
                    {
                        if (szListOfProcedureCodes == "")
                        {
                            szListOfProcedureCodes = grdProcedure.DataKeys[i]["SZ_PROCEDURE_CODE"].ToString() + " -- " + grdProcedure.DataKeys[i]["SZ_CODE_DESCRIPTION"].ToString();
                        }
                        else
                        {
                            szListOfProcedureCodes = szListOfProcedureCodes + " , " + grdProcedure.DataKeys[i]["SZ_PROCEDURE_CODE"].ToString() + " -- " + grdProcedure.DataKeys[i]["SZ_CODE_DESCRIPTION"].ToString();
                        }
                    }
                    _deleteOpeation.deleteRecord("SP_DELETE_BILL_PROC_TYPE", "@SZ_TYPE_CODE_ID", grdProcedure.DataKeys[i]["SZ_TYPE_CODE_ID"].ToString());
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

            grdProcedure.XGridBindSearch();
            usrMessage.PutMessage("Procedure codes deleted successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            try
            {
                BlazeFast.server.SrvProcedures procs = new BlazeFast.server.SrvProcedures();
                procs.Refresh();
            }
            catch (Exception io)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(io);
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

    protected void grdProcedure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int index = 0;
        try
        {
            if (e.CommandName=="Select")
            {
                try
                {
                    int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
                
                    rdoVisitType.ClearSelection();
                
                    Session["ProcedureCodeID"] = grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_ID"].ToString();
                
                    BindAmountGrid(grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_ID"].ToString());

                    if (grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_CODE"].ToString() != "&nbsp;") { txtProcedureCode.Text = grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_CODE"].ToString(); }
                    if (grdProcedure.DataKeys[iIndex]["SZ_CODE_DESCRIPTION"].ToString() != "&nbsp;") { txtProcedureDesc.Text = grdProcedure.DataKeys[iIndex]["SZ_CODE_DESCRIPTION"].ToString(); }
                    if (grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_GROUP"].ToString() != "&nbsp;") { extddlProCodeGroup.Text = grdProcedure.DataKeys[iIndex]["SZ_PROCEDURE_GROUP_ID"].ToString(); }
                    //     if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[5].Text != "&nbsp;") { ddlType.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[5].Text; }
                    if (grdProcedure.DataKeys[iIndex]["SZ_TYPE_CODE_ID"].ToString() != "&nbsp;") { Session["VisitID"] = grdProcedure.DataKeys[iIndex]["SZ_TYPE_CODE_ID"].ToString(); }
                    if (grdProcedure.DataKeys[iIndex]["SZ_MODIFIER"].ToString() != "&nbsp;") { txtModifier.Text = grdProcedure.DataKeys[iIndex]["SZ_MODIFIER"].ToString(); }
                    if (grdProcedure.DataKeys[iIndex]["FLT_AMOUNT"].ToString() != "&nbsp;") { txtProcedureAmount.Text = grdProcedure.DataKeys[iIndex]["FLT_AMOUNT"].ToString(); }
                    if (grdProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() != "&nbsp;")
                    {
                        rdoVisitType.SelectedValue = grdProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString();
                    }
                    else if (grdProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() == "&nbsp;" || grdProcedure.DataKeys[iIndex]["I_VISIT_TYPE"].ToString() == "0")
                    {
                        rdoVisitType.ClearSelection();

                    }
                    if (grdProcedure.DataKeys[iIndex]["BT_ADD_TO_PREFERRED_LIST"].ToString() != "&nbsp;")
                    {
                        if (grdProcedure.DataKeys[iIndex]["BT_ADD_TO_PREFERRED_LIST"].ToString().ToLower() == "true")
                        {
                            chkPrefList.Checked = true;
                        }
                    }
                    if (grdProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString() != "NULL") 
                    {
                        txtRevCode.Text = grdProcedure.DataKeys[iIndex]["SZ_REV_CODE"].ToString();
                    }

                    if (grdProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString() != "NULL")
                    {
                        txtValueCode.Text = grdProcedure.DataKeys[iIndex]["SZ_VALUE_CODE"].ToString();
                    }
                    if (grdProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString() != "NULL")
                    {
                        txtProcedureLongDesc.Text = grdProcedure.DataKeys[iIndex]["SZ_CODE_LONG_DESC"].ToString();
                    }
                    if (grdProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString() != "NULL")
                    {
                        txtModifierDesc.Text = grdProcedure.DataKeys[iIndex]["SZ_MODIFIER_LONG_DESC"].ToString();
                    }
                    if (grdProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_RVU"].ToString() != "NULL")
                    {
                        txtRVU.Text = grdProcedure.DataKeys[iIndex]["SZ_RVU"].ToString();
                    }
                    if (grdProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "" && grdProcedure.DataKeys[iIndex]["sz_location_id"].ToString() != "NULL" && grdProcedure.DataKeys[iIndex]["sz_location_id"].ToString().ToUpper() != "NA" )
                    {
                        extddlLocation.Text = grdProcedure.DataKeys[iIndex]["sz_location_id"].ToString();
                    }
                    if(grdProcedure.DataKeys[iIndex]["SZ_1500_DESC"].ToString() != "&nbsp;" && grdProcedure.DataKeys[iIndex]["SZ_1500_DESC"].ToString() != "" && grdProcedure.DataKeys[iIndex]["SZ_1500_DESC"].ToString() != "NULL")
                    {
                        txt1500desc.Text = grdProcedure.DataKeys[iIndex]["SZ_1500_DESC"].ToString();
                    }

                    // if (grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text != "&nbsp;") { extddlVisitRoom.Text = grdProcedure.Items[grdProcedure.SelectedIndex].Cells[8].Text; }
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
                for (int i = 0; i < grdProcedure.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdProcedure.Rows[i].FindControl("lnkM");
                    LinkButton plus1 = (LinkButton)grdProcedure.Rows[i].FindControl("lnkP");
                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                    }
                }

                //grdProcedure.Columns[17].Visible = true; // column change 16 to 17 
                //grdProcedure.Columns[18].Visible = false;// column change 17 to 18 
                //grdProcedure.Columns[19].Visible = false;// column change 18 to 19 
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdProcedure.DataKeys[index]["SZ_PROCEDURE_ID"].ToString();
                GridView gv = (GridView)grdProcedure.Rows[index].FindControl("GridView2");
                LinkButton plus = (LinkButton)grdProcedure.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdProcedure.Rows[index].FindControl("lnkM");
                string caseid = grdProcedure.DataKeys[index][0].ToString();
                DataSet objds = new DataSet();
                //  objds = objDAO.GetLitigatedBills(grdLitigationDesk.DataKeys[index][0].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extdlitigate.Text, ddlTransferStatus.Text);
                //objds = objDAO.GetLitigatedBillsInfo(grdProcedure.DataKeys[index][2].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extdlitigate.Text);
                DataTable dt = new DataTable();
                dt.Columns.Add("SZ_CODE_LONG_DESC");
                dt.Columns.Add("SZ_MODIFIER_LONG_DESC");
                dt.Columns.Add("SZ_RVU");
                DataRow dr = dt.NewRow();
                dr["SZ_CODE_LONG_DESC"] = grdProcedure.DataKeys[index]["SZ_CODE_LONG_DESC"].ToString();
                dr["SZ_MODIFIER_LONG_DESC"] = grdProcedure.DataKeys[index]["SZ_MODIFIER_LONG_DESC"].ToString();
                dr["SZ_RVU"] = grdProcedure.DataKeys[index]["SZ_RVU"].ToString();
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
                //grdProcedure.Columns[17].Visible = false; // column change 16 to 17 By kapil 26 March 2012
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdProcedure.DataKeys[index]["SZ_PROCEDURE_ID"].ToString();
                LinkButton plus = (LinkButton)grdProcedure.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdProcedure.Rows[index].FindControl("lnkM");
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
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdProcedure.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
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
            txt1500desc.Text = "";
            rdoVisitType.ClearSelection();
            for (int i = 0; i < lstIns.Items.Count; i++)
            {
                lstIns.Items[i].Selected = false;
            }
            txtApplyDate.Text = "";
            txtContractAmount.Text = "";
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
            for (int i = 0; i < grdProcedure.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Rows[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    string szProcCodeID = grdProcedure.DataKeys[i]["SZ_PROCEDURE_ID"].ToString();
                    arr.Add(szProcCodeID);
                }
            }
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            if (chkPrefList.Checked)
            {
                _bill_Sys_ProcedureCode_BO.UpdatePreferredList(arr, txtCompanyID.Text, "1");
            }
            else
            {
                _bill_Sys_ProcedureCode_BO.UpdatePreferredList(arr, txtCompanyID.Text, "0");
            }
            ClearControls();
            grdProcedure.XGridBindSearch();

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
