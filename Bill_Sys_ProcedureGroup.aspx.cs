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
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;
public partial class Bill_Sys_ProcedureGroup : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    private Bill_Sys_ProcedureCode_BO _procedureBO;
    private Bill_Sys_Calender _Calendar;
    string _order = "";
    string _orderAmt = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnSave.Attributes.Add("onclick", "return formValidator('frmProcedureGroup','txtProcedureGroup');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmProcedureGroup','txtProcedureGroup');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Session["Flag"] != null && Session["Flag"].ToString() == "true")
            {
                //TreeMenuControl1.Visible = false;
                grdProcedureGroup.Visible = false;
                btnUpdate.Visible = false;
            }
            else
            {
                //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 
                Bill_Sys_DisplaySpeciality objspeciality = new Bill_Sys_DisplaySpeciality();
                DataSet dset = new DataSet();
                if (!IsPostBack)
                {
                    string szBitValue = Session["SendPatientToDoctor"].ToString();
                    if (szBitValue.ToLower() == "false")
                    {
                        chkDonotHaveNotes.Visible = false;
                        lblDonotHaveNotes.Visible = false;
                    }
                    else
                    {
                        chkDonotHaveNotes.Visible = true;
                        lblDonotHaveNotes.Visible = true;
                    }


                    BindGrid();
                    txtIELimit.Text = "0";
                    txtFULimit.Text = "0";
                    txtCLimit.Text = "0";
                    btnUpdate.Enabled = false;
                    BindProcedureGroupLstBox();
                    txtFULimit.Enabled = false;
                    txtIELimit.Enabled = false;
                    txtCLimit.Enabled = false;
                    _Calendar = new Bill_Sys_Calender();
                    DataTable CheckVisitTypes = new DataTable();
                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY.ToString().ToLower() == "false")
                    {
                        if (txtCompanyID.Text != "")
                            CheckVisitTypes = _Calendar.GET_Visit_Types(txtCompanyID.Text);
                        if (CheckVisitTypes.Rows.Count > 0)
                        {
                            foreach (DataRow dr in CheckVisitTypes.Rows)
                            {
                                if (dr["VISIT_TYPE"].ToString().Trim().ToLower() == "ie")
                                    txtIELimit.Enabled = true;
                                else if (dr["VISIT_TYPE"].ToString().Trim().ToLower() == "fu")
                                    txtFULimit.Enabled = true;
                                else if (dr["VISIT_TYPE"].ToString().Trim().ToLower() == "c")
                                    txtCLimit.Enabled = true;
                            }
                        }

                    }
                }
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
            cv.MakeReadOnlyPage("Bill_Sys_ProcedureGroup.aspx");
        }
        #endregion
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
        Bill_Sys_DisplaySpeciality objspeciality;
        ArrayList arrobj = new ArrayList();
        if (txtCLimit.Text == "")
        {
            txtCLimit.Text = "0";
        } if (txtFULimit.Text == "")
        {
            txtFULimit.Text = "0";
        } if (txtIELimit.Text == "")
        {
            txtIELimit.Text = "0";
        }
       
        string getLatestId = "";
        if (chkPatientDesk.Checked)
        {
            txtorder.Text = GetMaxOrder(txtCompanyID.Text);
        }
       
        _saveOperation = new SaveOperation();
        try
        {

            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "ProcedureGroup.xml";
            string isExistsSpeciallity = CheckSpeciallityExists(txtCompanyID.Text);
            if (isExistsSpeciallity == "")
            {
                _saveOperation.SaveMethod();
            }
            else
            {
                this.lblMsg.Text = "Speciallity name already exists.";
                return;
            }
            BindGrid();
            objspeciality = new Bill_Sys_DisplaySpeciality();
            getLatestId = objspeciality.GetLatestProcedureGroupID(txtCompanyID.Text);
            foreach (ListItem li in lstboxSpeciality.Items)
            {
                if (li.Selected == true)
                {
                    arrobj.Add(li.Value.ToString());
                }
            }

            if (arrobj.Count > 0)
            {

                for (int i = 0; i < arrobj.Count; i++)
                {
                    objspeciality = new Bill_Sys_DisplaySpeciality();
                    objspeciality.InsertAssociateSpeciality(getLatestId, arrobj[i].ToString(), txtCompanyID.Text);
                }

                for (int i = 0; i < arrobj.Count; i++)
                {
                    for (int j = i + 1; j < arrobj.Count; j++)
                    {
                        objspeciality = new Bill_Sys_DisplaySpeciality();
                        objspeciality.InsertAssociateSpeciality(arrobj[i].ToString(), arrobj[j].ToString(), txtCompanyID.Text);
                    }
                }
            }
            int num4 = 0;
            if (this.rdbRefferal.Checked)
            {
                num4 = 1;
            }
            else if (this.rdbReport.Checked)
            {
                num4 = 0;
            }
            if (objspeciality.InsertNodeType(this.txtCompanyID.Text, this.txtProcedureGroup.Text, num4) == 0)
            {
                ClearControl();
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Speciality Saved Successfully...!";
            }
            else
            {
                ClearControl();
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Speciality Saved failed...!";
            }
            //lblMsg.Visible = true;
            //lblMsg.Text = "Speciality Saved Successfully...!";
            foreach (ListItem li in lstboxSpeciality.Items)
            {
                if (li.Selected == true)
                {
                    li.Selected = false;
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

    #region "Fetch Method"
    private string CheckSpeciallityExists(string sz_companycode)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader dreader;
        string Speciallity = "";
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SELECT SZ_PROCEDURE_GROUP FROM MST_PROCEDURE_GROUP WHERE SZ_COMPANY_ID='" + sz_companycode + "' AND SZ_PROCEDURE_GROUP='" + txtProcedureGroup.Text + "'", sqlCon);
            sqlCmd.CommandType = CommandType.Text;
            dreader = sqlCmd.ExecuteReader();
            while (dreader.Read())
            {
                Speciallity = dreader["SZ_PROCEDURE_GROUP"].ToString();
            }
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return Speciallity;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
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
            _listOperation.Xml_File = "ProcedureGroup.xml";
            _listOperation.LoadList();
        }
        catch (Exception ex)
        {
            if (ex.Message != "Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount.")
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
            
            else
            {
                grdProcedureGroup.CurrentPageIndex = 0;
                BindGrid();
            }
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }
    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DisplaySpeciality objspeciality;
        ArrayList arrobj = new ArrayList();
        DataSet dset = new DataSet();
        _editOperation = new EditOperation();
        int flag = 0;
        try
        {
            if (Session["orderAmt"] != null)
            {
                _orderAmt = Session["orderAmt"].ToString();
            }
            if (chkPatientDesk.Checked && _orderAmt == "")
            {
                txtorder.Text = GetMaxOrder(txtCompanyID.Text);
                Session["orderAmt"] = txtorder.Text;
            }
            else if (chkPatientDesk.Checked && _orderAmt != "")
            {
                txtorder.Text = _orderAmt;
            }

            if (txtCLimit.Text == "")
            {
                txtCLimit.Text = "0";
            } if (txtFULimit.Text == "")
            {
                txtFULimit.Text = "0";
            } if (txtIELimit.Text == "")
            {
                txtIELimit.Text = "0";
            }
            _editOperation.Primary_Value = Session["ProcedureGroupID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "ProcedureGroup.xml";
            _editOperation.UpdateMethod();
            BindGrid();
            lblMsg.Visible = true;
            objspeciality = new Bill_Sys_DisplaySpeciality();
            //dset = objspeciality.GetAssociateSpecialityData(Session["ProcedureGroupID"].ToString(), txtCompanyID.Text);
            dset = (DataSet)Session["AssociateSpeciality"];
            if (dset.Tables[0].Rows.Count > 0)
            {
                foreach (ListItem li in lstboxSpeciality.Items)
                {
                    if (li.Selected == true)
                    {
                        arrobj.Add(li.Value.ToString());
                    }
                }
                ArrayList arrlst = new ArrayList();
                for (int j = 0; j < dset.Tables[0].Rows.Count; j++)
                {
                    string id = dset.Tables[0].Rows[j][0].ToString();
                    foreach (ListItem li in lstboxSpeciality.Items)
                    {
                        string liID = li.Value.ToString();
                        if (li.Selected == true)
                        {
                            if (id == liID)
                            {
                                flag = 1;
                                break;
                            }
                            else
                            {
                                flag = 2;
                                //arrlst.Add(dset.Tables[0].Rows[j][1].ToString());
                            }
                        }
                    }
                    if (flag == 2)
                    {
                        arrlst.Add(dset.Tables[0].Rows[j][1].ToString());
                    }
                }

                if (arrlst.Count > 0)
                {
                    for (int k = 0; k < arrlst.Count; k++)
                    {
                        objspeciality = new Bill_Sys_DisplaySpeciality();
                        objspeciality.DeleteUpdateAssociateSpeciality(arrlst[k].ToString(), Session["ProcedureGroupID"].ToString(), txtCompanyID.Text);
                    }
                }

                if (arrobj.Count > 0)
                {

                    for (int i = 0; i < arrobj.Count; i++)
                    {
                        objspeciality = new Bill_Sys_DisplaySpeciality();
                        objspeciality.InsertAssociateSpeciality(Session["ProcedureGroupID"].ToString(), arrobj[i].ToString(), txtCompanyID.Text);
                    }

                    for (int i = 0; i < arrobj.Count; i++)
                    {
                        for (int j = i + 1; j < arrobj.Count; j++)
                        {
                            objspeciality = new Bill_Sys_DisplaySpeciality();
                            objspeciality.InsertAssociateSpeciality(arrobj[i].ToString(), arrobj[j].ToString(), txtCompanyID.Text);
                        }
                    }
                }
            }
            else
            {
                foreach (ListItem li in lstboxSpeciality.Items)
                {
                    if (li.Selected == true)
                    {
                        arrobj.Add(li.Value.ToString());
                    }
                }

                if (arrobj.Count > 0)
                {

                    for (int i = 0; i < arrobj.Count; i++)
                    {
                        objspeciality = new Bill_Sys_DisplaySpeciality();
                        objspeciality.InsertAssociateSpeciality(Session["ProcedureGroupID"].ToString(), arrobj[i].ToString(), txtCompanyID.Text);
                    }

                    for (int i = 0; i < arrobj.Count; i++)
                    {
                        for (int j = i + 1; j < arrobj.Count; j++)
                        {
                            objspeciality = new Bill_Sys_DisplaySpeciality();
                            objspeciality.InsertAssociateSpeciality(arrobj[i].ToString(), arrobj[j].ToString(), txtCompanyID.Text);
                        }
                    }
                }
                lblMsg.Text = "Speciality Updated Successfully...!";
            }
            //if (chkAutoAddVisit.Checked)
            //{
            //    lbl.Visible = true;
            //    txtAutoVisit.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdAdjuster_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DisplaySpeciality objspeciality;
        DataSet dset = new DataSet();
        try
        {
            txtProcedureGroup.Enabled = false;
            Session["ProcedureGroupID"] = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[1].Text;
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[2].Text != "&nbsp;") { txtProcedureGroup.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[2].Text; }

            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[3].Text == "True")
            {
                chkPrintable.Checked = true;
            }
            else
            {
                chkPrintable.Checked = false;
            }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[4].Text == "True")
            {
                chkUnit.Checked = true;
            }
            else
            {
                chkUnit.Checked = false;
            }
            //txtFollowUpCount
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[5].Text != "&nbsp;") { txtDaysafterReeval.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[5].Text; }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[6].Text != "&nbsp;") { txtReevalCount.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[6].Text; }
           if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[21].Text != "&nbsp;") { txtvisitsafterReeval.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[21].Text; }
           if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[22].Text != "&nbsp;") { txtReevalCodes.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[22].Text; }
           if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[24].Text != "&nbsp;") { txtInitialCodes.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[24].Text; }
           if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[7].Text != "&nbsp;") {txtDaysafterInitial.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[7].Text; }
           if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[23].Text != "&nbsp;") { txtvisitsafterInitial.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[23].Text; }
           // if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[5].Text != "&nbsp;")
            //{ txtFollowUpDays.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[5].Text; }
            //else { txtFollowUpDays.Text = ""; }

            //if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[6].Text != "&nbsp;") { txtFollowUpCount.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[6].Text; } else { txtFollowUpCount.Text = ""; }

            //if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[7].Text != "&nbsp;") { txtIntialFollowoupDays.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[7].Text; } else { txtIntialFollowoupDays.Text = ""; }

            //if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[8].Text != "&nbsp;") { txtIntialFollowupCount.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[8].Text; } else { txtIntialFollowupCount.Text = ""; }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[10].Text != "&nbsp;") { chkPatientDesk.Checked = Convert.ToBoolean(grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[10].Text); } else { chkPatientDesk.Checked = false; }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[11].Text != "&nbsp;") { Session["orderAmt"] = Convert.ToString(grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[11].Text); } else { Session["orderAmt"] = ""; }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[12].Text == "True")
            {
                chkInitialEvaluation.Checked = true;
            }
            else
            {
                chkInitialEvaluation.Checked = false;
            }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[13].Text == "True")
            {
                chkAutoAssociate.Checked = true;
            }
            else
            {
                chkAutoAssociate.Checked = false;
            }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[14].Text == "True")
            {
                chkinclude_1500.Checked = true;
            }
            else
            {
                chkinclude_1500.Checked = false;
            }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[20].Text == "1")
            {
                chkDonotHaveNotes.Checked = true;
            }
            else
            {
                chkDonotHaveNotes.Checked = false;
            }
            BindProcedureGroupLstBox();

            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[15].Text != "&nbsp;" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[15].Text != "NULL" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[15].Text != "")
            {
                NumberFormatInfo n = CultureInfo.InvariantCulture.NumberFormat;
                
                txtIELimit.Text =grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[15].Text.ToString();
            }else
            {
                 txtIELimit.Text="0";
            }
            if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[16].Text != "&nbsp;" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[16].Text != "NULL" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[16].Text != "")
            {
                txtFULimit.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[16].Text;
            }
            else
            {
                txtFULimit.Text = "0";
            } if (grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[17].Text != "&nbsp;" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[17].Text != "NULL" && grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[17].Text != "")
            {
                txtCLimit.Text = grdProcedureGroup.Items[grdProcedureGroup.SelectedIndex].Cells[17].Text;
            }
            else
            {
                txtCLimit.Text = "0";
            }

            objspeciality = new Bill_Sys_DisplaySpeciality();
            dset = objspeciality.GetAssociateSpecialityData(Session["ProcedureGroupID"].ToString(), txtCompanyID.Text);
            Session["AssociateSpeciality"] = dset;
            if (dset.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {
                    string id = dset.Tables[0].Rows[i][0].ToString();
                    foreach (ListItem li in lstboxSpeciality.Items)
                    {
                        if (li.Value.ToString() == id)
                        {
                            li.Selected = true;
                        }
                    }
                }
            }
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdAdjuster_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdProcedureGroup.CurrentPageIndex = e.NewPageIndex;
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtProcedureGroup.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Visible = false;
            chkPrintable.Checked = false;
            chkUnit.Checked = false;
            //txtFollowUpDays.Text = "";
            //txtFollowUpCount.Text = "";
            //txtIntialFollowoupDays.Text = "";
            //txtIntialFollowupCount.Text = "";
            txtDaysafterReeval.Text = "";
            txtReevalCount.Text = "";
            txtvisitsafterReeval.Text = "";
            txtReevalCodes.Text = "";
            txtInitialCodes.Text = "";
            txtvisitsafterInitial.Text = "";
            txtDaysafterInitial.Text = "";
            chkPatientDesk.Checked = false;
            chkInitialEvaluation.Checked = false;
            chkAutoAssociate.Checked = false;
            txtIELimit.Text = "0";
            txtFULimit.Text = "0";
            txtCLimit.Text = "0";
            chkinclude_1500.Checked=false;
            chkDonotHaveNotes.Checked = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtProcedureGroup.Enabled = true;
            foreach (ListItem li in lstboxSpeciality.Items)
            {
                if (li.Selected == true)
                {
                    li.Selected = false;
                }
            }
            Session.Remove("AssociateSpeciality");
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
        String szListOfProcedureCode = "";
        Bill_Sys_DisplaySpeciality objspeciality;
        DataSet dset;
        ArrayList arrlst = new ArrayList();
        DataTable ID;
        try
        {
            for (int i = 0; i < grdProcedureGroup.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdProcedureGroup.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_PROCEDURE_GROUP", "@SZ_PROCEDURE_GROUP_ID", grdProcedureGroup.Items[i].Cells[1].Text))
                    {
                        if (szListOfProcedureCode == "")
                        {
                            szListOfProcedureCode = grdProcedureGroup.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfProcedureCode = szListOfProcedureCode + " , " + grdProcedureGroup.Items[i].Cells[2].Text;
                        }
                    }
                    //objspeciality = new Bill_Sys_DisplaySpeciality();
                    //dset = new DataSet();
                    //dset = objspeciality.GetAssociateSpecialityData(grdProcedureGroup.Items[i].Cells[1].Text, txtCompanyID.Text);
                    objspeciality = new Bill_Sys_DisplaySpeciality();
                    objspeciality.DeleteAssociateSpeciality(grdProcedureGroup.Items[i].Cells[1].Text, txtCompanyID.Text);
                    //for (int j = 0; j < dset.Tables[0].Rows.Count; j++)
                    //{
                    //    //ID = new DataTable();
                    //    objspeciality = new Bill_Sys_DisplaySpeciality();
                    //    objspeciality.DeleteAssociateSpeciality(dset.Tables[0].Rows[j][0].ToString(), txtCompanyID.Text);
                    //    //for (int k = 0; k > ID.Rows.Count; k++)
                    //    //{
                    //    //    arrlst.Add(ID.Rows[k][0].ToString());
                    //    //}
                    //}
                }
            }
            if (szListOfProcedureCode != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Speciality " + szListOfProcedureCode + "  exists.'); ", true);
            }
            else
            {
               
                lblMsg.Visible = true;
                lblMsg.Text = "Speciality deleted successfully ...";
            }
            //  grdProcedureGroup.CurrentPageIndex = 0;
            BindGrid();
            BindProcedureGroupLstBox();
            ClearControl();
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

    // 24 March 2010, #138 - Get max order for update in db - sachin

    private string GetMaxOrder(string _companyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _procedureBO = new Bill_Sys_ProcedureCode_BO();
        try
        {

            _order = _procedureBO.GetMaxOrder(txtCompanyID.Text);

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
        return _order;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        
    }

    public void BindProcedureGroupLstBox()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DisplaySpeciality objspeciality = new Bill_Sys_DisplaySpeciality();
        DataSet dset = new DataSet();
        try
        {
            dset = objspeciality.GetSpecialityList(txtCompanyID.Text);
            lstboxSpeciality.DataSource = dset.Tables[0];
            lstboxSpeciality.DataTextField = "DESCRIPTION";
            lstboxSpeciality.DataValueField = "CODE";
            lstboxSpeciality.DataBind();
            //ListItem objItem = new ListItem("---select---", "NA");
            //lstboxSpeciality.Items.Insert(0, objItem);
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
