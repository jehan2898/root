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
using DevExpress.Web;

public partial class Bill_Sys_GroupProcedureCode : PageBase
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
            this.con.SourceGrid = this.GrdProcedureGroup;
            this.txtSearchBox.SourceGrid = this.GrdProcedureGroup;
            this.GrdProcedureGroup.Page = this.Page;
            this.GrdProcedureGroup.PageNumberList = this.con;
            if (!IsPostBack)
            {
                //btnAssign.Attributes.Add("onclick", "return formValidator('frmGroupProcedureCode','extddlProCodeGroup,txtGroupName');");
                this.btnAssign.Attributes.Add("onclick", "return OnSave();");
                btnRemove.Attributes.Add("onclick", "return OnDelete();");
                this.btnUpdate.Attributes.Add("onclick", "return OnSave;");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlProCodeGroup.Flag_ID = txtCompanyID.Text;
                hdnProcedureCode.Value = "";
               
                //Session["Procedure_GroupID"] = "";
                //Session["Procedure_GroupID"] = extddlProCodeGroup.Text;
                btnAssign.Enabled = true;
                btnRemove.Enabled = true;
                btnUpdate.Enabled = false;
                //ProcedureDiv.Visible = false;
                Session["Selected"] = "";
            }
            if(Session["selected"].ToString() == "Y")
            {
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_GroupProcedureCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void extddlProCodeGroup_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            Session["selected"] = "Y";
            hdnProcedureCode.Value = "";
            string strhml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
            divGroupProcedureCode.InnerHtml = strhml;
            BindGrid();
            //txtSearchBox.SourceGrid = this.GrdProcedureGroup;
            //txtSearchBox.Text = "";
            BindGroup();
            lblMsg.Visible = false;
            txtGroupName.Text = "";
            txtGroupAmount.Text = "";
            lbl_search.Visible = true;
            txtSearchBox.Visible = true;
            lbl_record_count.Visible = true;
            lbl_Page_Count.Visible = true;
            con.Visible = true;
            ProcedureDiv.Visible = true;
            if (extddlProCodeGroup.Text == "NA")
            {
                Session["selected"] = "";
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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList objAL = new ArrayList();         
            objAL.Add(extddlProCodeGroup.Text);
            objAL.Add(txtCompanyID.Text);
            objAL.Add("LIST");

            Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
            //grdGroupProcedureCode.DataSource = objProcBO.Search_GroupProcedureCodes(objAL);
            //grdGroupProcedureCode.DataBind();
            Grid.DataSource = objProcBO.Search_GroupProcedureCodes(objAL);
            Grid.DataBind();
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
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["Procedure_GroupID"] = "";
            Session["Procedure_GroupID"] = extddlProCodeGroup.Text;
            ProcedureDiv.Visible = true;
            string[] arrspilt=hdnProcedureCode.Value.Split('~');
          
            bool _flag = false;
            for (int i = 0; i < arrspilt.Length-1; i++)
           
            {
                string code = arrspilt[i].ToString();
                code = code.Replace("--", "").Trim();
                code = code.Replace(",", "").Trim();

                ArrayList objAL = new ArrayList();
                   
                    objAL.Add("");
                    objAL.Add(code);
                    objAL.Add(txtGroupName.Text);
                    objAL.Add(txtCompanyID.Text);
                    objAL.Add("ADD");
                    objAL.Add(Session["Procedure_GroupID"].ToString());
                  
                    Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
                    objProcBO.SaveUpdateGroupProcedureCodes(objAL);
                    _flag = true;
                                  
            }

            if (_flag == true)
            {
                Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
                ArrayList objAL = new ArrayList();
               
                    objAL.Add(txtGroupAmount.Text);
                    objAL.Add(extddlProCodeGroup.Text);
                    objAL.Add(txtCompanyID.Text);
                    objAL.Add(txtGroupName.Text);
                    objProcBO.SaveGroupProcedureCodeAmount(objAL);
                
               
            }
           
            lblMsg.Visible = true;
            lblMsg.Text = "Procedure code group added successfully ...!";
            BindGrid();
            BindGroup();
            ClearControl();
            extddlProCodeGroup.Text = Session["Procedure_GroupID"].ToString();
            hdnProcedureCode.Value = "";
            divGroupProcedureCode.InnerHtml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
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
        
        hdnProcedureCode.Value = "";
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
           // string str_GroupName = Session["GroupName"].ToString();
            for (int i = 0; i < GrdProcedureGroup.Rows.Count; i++)
            {
                CheckBox chkRemove = (CheckBox)GrdProcedureGroup.Rows[i].FindControl("chkRemove");
                if (chkRemove.Checked)
                {
                        ArrayList objAL = new ArrayList();
                        objAL.Add(extddlProCodeGroup.Text);
                        objAL.Add(txtCompanyID.Text);
                        objAL.Add("");
                        objAL.Add(GrdProcedureGroup.DataKeys[i]["SZ_PROCEDURE_GROUP_NAME"].ToString());
                        objAL.Add("DELETE_GROUP");
                        BindProcedureGroup obj = new BindProcedureGroup();
                        obj.DELETE_PROCEDURE_CODES(objAL);

                        //Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
                        //objProcBO.RemoveGroupProcedureCodes(objAL);
                    

                }
            }
                        lblMsg.Visible = true;
            lblMsg.Text = "Procedure code group deleted successfully ...!";
            BindGrid();
            BindGroup();
            txtGroupName.Text = "";
            txtGroupAmount.Text = "";
            ProcedureDiv.Visible = true;
            divGroupProcedureCode.InnerHtml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
            
            //extddlProCodeGroup.Text = "";
            //divGroupProcedureCode.InnerHtml = "";
            hdnProcedureCode.Value = "";
            btnAssign.Enabled = true;
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
    protected void grdGroupProcedureCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdGroupProcedureCode.CurrentPageIndex = e.NewPageIndex;
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtGroupName.Text = "";
            txtGroupAmount.Text = "";
            extddlProCodeGroup.Text = "NA";
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
    private void BindGroup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txt_CompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txt_ProcedureGroupID.Text = extddlProCodeGroup.Text;
            Session["Procedure_GroupID"] = txt_ProcedureGroupID.Text;


            this.con.SourceGrid = this.GrdProcedureGroup;
            this.txtSearchBox.SourceGrid = this.GrdProcedureGroup;
            this.GrdProcedureGroup.Page = this.Page;
            this.GrdProcedureGroup.PageNumberList = this.con;
            this.GrdProcedureGroup.XGridBindSearch();
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
    protected void GrdProcedureGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new ArrayList();
        BindProcedureGroup obj = new BindProcedureGroup();
        //Session["Procedure_GroupID"] = "";
        //Session["Procedure_GroupID"] = extddlProCodeGroup.Text;
        ProcedureDiv.Visible = true;
        
        int num = 0;
        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
              
                lblMsg.Visible = false;
                foreach (DataGridItem grdItem in grdGroupProcedureCode.Items)
                {
                    CheckBox chkAssign = (CheckBox)grdItem.FindControl("chkAssign");
                    chkAssign.Checked = false;
                }

                for (int i = 0; i < this.GrdProcedureGroup.Rows.Count; i++)
                {
                    LinkButton button = (LinkButton)this.GrdProcedureGroup.Rows[i].FindControl("lnkM");
                    LinkButton button2 = (LinkButton)this.GrdProcedureGroup.Rows[i].FindControl("lnkP");
                    if (button.Visible)
                    {
                        button.Visible = false;
                        button2.Visible = true;
                    }
                }
                this.GrdProcedureGroup.Columns[3].Visible = true;
                
                num = Convert.ToInt32(e.CommandArgument);
                string str = "div";
                str = str + this.GrdProcedureGroup.DataKeys[num][0].ToString();
                GridView view = (GridView)this.GrdProcedureGroup.Rows[num].FindControl("GridView2");
                LinkButton button3 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkP");
                LinkButton button4 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkM");
                DataSet set = new DataSet();
                txt_ProcedureGroupID.Text = Session["Procedure_GroupID"].ToString();
                set = obj.Get_Procedure_Group_Details(this.GrdProcedureGroup.DataKeys[num][0].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, Session["Procedure_GroupID"].ToString());
                view.DataSource = set;
                view.DataBind();
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
                button3.Visible = false;
                button4.Visible = true;
                
                
            }
            if (e.CommandName.ToString() == "MNS")
            {
               
                lblMsg.Visible = false;
                foreach (DataGridItem grdItem in grdGroupProcedureCode.Items)
                {
                    CheckBox chkAssign = (CheckBox)grdItem.FindControl("chkAssign");
                    chkAssign.Checked = false;
                }

                this.GrdProcedureGroup.Columns[3].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str2 = "div";
                str2 = str2 + this.GrdProcedureGroup.DataKeys[num][0].ToString();
                LinkButton button5 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkP");
                LinkButton button6 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkM");
                ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "mm", "HideChildGrid('" + str2 + "') ;", true);
                button5.Visible = true;
                button6.Visible = false;
                
            }
            
            if (e.CommandName == "Select")
            {
                try
                { 
                    txtGroupName.Text = "";
                    txtGroupAmount.Text = "";
                    extddlProCodeGroup.Text = "";
                    hdnProcedureCode.Value = "";
                    txt_GroupId.Text = "";
                    lblMsg.Visible = false;
                    btnAssign.Enabled = false;
                    btnUpdate.Enabled = true;
                    
                    
                    foreach (DataGridItem grdItem in grdGroupProcedureCode.Items)
                    {
                        //CheckBox chkAssign = (CheckBox)grdItem.FindControl("chkAssign");
                        //chkAssign.Checked = false;
                       
                    }

                    int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    num = Convert.ToInt32(e.CommandArgument);
                    if (GrdProcedureGroup.DataKeys[iIndex]["SZ_PROCEDURE_GROUP_NAME"].ToString() != "&nbsp;")
                    {
                        txtGroupName.Text = GrdProcedureGroup.DataKeys[iIndex]["SZ_PROCEDURE_GROUP_NAME"].ToString();
                    }
                    if (GrdProcedureGroup.DataKeys[iIndex]["FLT_AMOUNT"].ToString() != "&nbsp;")
                    {
                        txtGroupAmount.Text = GrdProcedureGroup.DataKeys[iIndex]["FLT_AMOUNT"].ToString();
                    }
                    GridView view = (GridView)this.GrdProcedureGroup.Rows[num].FindControl("GridView2");
                    DataSet set = new DataSet();
                    txt_ProcedureGroupID.Text = Session["Procedure_GroupID"].ToString();
                    extddlProCodeGroup.Text = txt_ProcedureGroupID.Text;
                    	
                    set = obj.Get_Procedure_Group_Details(this.GrdProcedureGroup.DataKeys[num][0].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txt_ProcedureGroupID.Text);
                    view.DataSource = set;
                    view.DataBind();
                   // string[] arr4 = new string[set.Tables.Count];
                    string strhml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
                    strhml += "<table>";
                    if (set.Tables.Count > 0)
                    {
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                            {
                                
                                strhml += "<tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='Images/icon_delete.png' /></a></td>";
                                strhml += "<td>&nbsp;&nbsp;&nbsp;" + set.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "</td>";
                                strhml += "<td>&nbsp;&nbsp;&nbsp;" + set.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString() + "</td>";
                                strhml += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + set.Tables[0].Rows[i]["SZ_PROCEDURE_ID"].ToString() + "</td>";
                                //strhml += "<td><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='Images/checkbox.JPG' /></a></td>";
                                hdnProcedureCode.Value += set.Tables[0].Rows[i]["SZ_PROCEDURE_ID"].ToString() + "~--";
                                //arr4[i] = set.Tables[0].Rows[i]["SZ_PROCEDURE_ID"].ToString() + "~--";
                            }
                            strhml += "</table>";
                            divGroupProcedureCode.InnerHtml =  strhml;

                            
                            
                        }
                    }
                    for (int i = 0; i < this.GrdProcedureGroup.Rows.Count; i++)
                    {
                        LinkButton button = (LinkButton)this.GrdProcedureGroup.Rows[i].FindControl("lnkM");
                        LinkButton button2 = (LinkButton)this.GrdProcedureGroup.Rows[i].FindControl("lnkP");
                        if (button.Visible)
                        {
                            button.Visible = true;
                            button2.Visible = false;
                            this.GrdProcedureGroup.Columns[3].Visible = true;

                            num = Convert.ToInt32(e.CommandArgument);
                            string str = "div";
                            str = str + this.GrdProcedureGroup.DataKeys[num][0].ToString();
                            GridView view1 = (GridView)this.GrdProcedureGroup.Rows[num].FindControl("GridView2");
                            LinkButton button3 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkP");
                            LinkButton button4 = (LinkButton)this.GrdProcedureGroup.Rows[num].FindControl("lnkM");
                            DataSet set1 = new DataSet();
                            txt_ProcedureGroupID.Text = Session["Procedure_GroupID"].ToString();
                            set1 = obj.Get_Procedure_Group_Details(this.GrdProcedureGroup.DataKeys[num][0].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txt_ProcedureGroupID.Text);
                            view1.DataSource = set1;
                            view1.DataBind();
                            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
                            button3.Visible = false;
                            button4.Visible = true;
                            BindGrid();
                            BindGroup();
                        }
                    }
                    Session["GroupName"] = "";
                    Session["GroupName"]=txtGroupName.Text ;
                    btnRemove.Enabled = true;
                    
                }
                catch (Exception ex)
                {
                    
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
    protected void Grid_Load(object sender, EventArgs e)
    {
        Grid.Settings.ShowFilterRowMenu = true;
        
    }

    protected void btnUpdate_Click(object sender, EventArgs e )
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            ArrayList objAL1 = new ArrayList();
            objAL1.Add(extddlProCodeGroup.Text);
            objAL1.Add(txtCompanyID.Text);
            objAL1.Add("");
            objAL1.Add(txtGroupName.Text);
            objAL1.Add("DELETE_GROUP");
            BindProcedureGroup obj1 = new BindProcedureGroup();
            obj1.DELETE_PROCEDURE_CODES(objAL1);

            Session["Procedure_GroupID"] = "";
            Session["Procedure_GroupID"] = extddlProCodeGroup.Text;
            ProcedureDiv.Visible = true;
            string[] arrspilt = hdnProcedureCode.Value.Split('~');

            bool _flag = false;
            for (int i = 0; i < arrspilt.Length - 1; i++)
            {
                string code = arrspilt[i].ToString();
                code = code.Replace("--", "").Trim();
                code = code.Replace(",", "").Trim();

                ArrayList objAL = new ArrayList();
                objAL.Add("");
                objAL.Add(code);
                objAL.Add(txtGroupName.Text);
                objAL.Add(txtCompanyID.Text);
                
                objAL.Add("ADD");
                objAL.Add(Session["Procedure_GroupID"].ToString());

                Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
                objProcBO.SaveUpdateGroupProcedureCodes(objAL);
                _flag = true;



            }

            if (_flag == true)
            {
                Bill_Sys_ProcedureCode_BO objProcBO = new Bill_Sys_ProcedureCode_BO();
                ArrayList objAL = new ArrayList();
                objAL.Add(txtGroupAmount.Text);
                objAL.Add(extddlProCodeGroup.Text);
                objAL.Add(txtCompanyID.Text);
                objAL.Add(txtGroupName.Text); 
                objProcBO.SaveGroupProcedureCodeAmount(objAL);

            
            }

            lblMsg.Visible = true;
            lblMsg.Text = "Procedure code group update successfully ...!";
            BindGrid();
            BindGroup();
            ClearControl();
            hdnProcedureCode.Value = "";
            divGroupProcedureCode.InnerHtml = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
            extddlProCodeGroup.Text = Session["Procedure_GroupID"].ToString();
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
       
        divGroupProcedureCode.InnerHtml = "";
        hdnProcedureCode.Value = "";
        btnAssign.Enabled = true;
        btnUpdate.Enabled = false;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}