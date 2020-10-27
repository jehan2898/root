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

public partial class Bill_Sys_CheckOutNew : PageBase
{
    Bill_Sys_CheckoutBO _obj_CheckOutBO;
    string sz_UserID = "";
    string sz_CompanyID = "";
    ArrayList arrLst;
    string  GetProcGroup;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //ashutosh
           // string ds1 = Get_Signature_Code("45809");
            //

            //btnSearch_1.Attributes.Add("onclick", "return formValidator('form1',,extddlVisitType,ddlHours');");
            string sz_lstDocIds = "";
            ddlDateValues_1.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlOne_ddlDateValues_1','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlOne_txtFromDate_1','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlOne_txtToDate_1');");
            ddlDateValues_2.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlTwo_ddlDateValues_2','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlTwo_txtFromDate_2','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlTwo_txtToDate_2');");
            ddlDateValues_3.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlThree_ddlDateValues_3','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlThree_txtFromDate_3','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlThree_txtToDate_3');");
            ddlDateValues_4.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlFour_ddlDateValues_4','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlFour_txtFromDate_4','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlFour_txtToDate_4');");
            ddlDateValues_5.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlfive_ddlDateValues_5','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlfive_txtFromDate_5','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlfive_txtToDate_5');");
            ddlDateValues_6.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlsix_ddlDateValues_6','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlsix_txtFromDate_6','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlsix_txtToDate_6');");
            ddlDateValues_7.Attributes.Add("onChange", "javascript:SetDate('_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlseven_ddlDateValues_7','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlseven_txtFromDate_7','_ctl0_ContentPlaceHolder1_tabVistInformation_tabpnlseven_txtToDate_7');");
            DataSet dset = new DataSet();

            if (!IsPostBack)
            {
                Session["SPECIALITY_PDF_OBJECT"] = null;
                _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
                sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();

                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();

                txtCompanyID.Text = sz_CompanyID;
                txtuserid.Text = sz_UserID;
                if (sz_UserID != "" && sz_CompanyID != "")
                {
                    arrLst = new ArrayList();
                    dset = _obj_CheckOutBO.GetDoctorUserID(sz_UserID, sz_CompanyID);
                   
                    for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                    {
                        sz_lstDocIds += "'" + dset.Tables[0].Rows[i][0].ToString() + "'" + ",";
                    }
                    sz_lstDocIds = sz_lstDocIds.Substring(0, sz_lstDocIds.Length - 1);


                    DataSet ds = new DataSet();
                    ds = _obj_CheckOutBO.GetProcedureGroupID(sz_UserID, sz_CompanyID);

                    //get the values in the tab for the particular Speciality
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        int Cnt = j + 1;
                        //string specialityName = ds.Tables[0].Rows[j][1].ToString();
                        if (Cnt == 1)
                        {
                            arrLst.Clear();
                            lblHeadOne.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlOne.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID.Text = sz_procedure_group_id;
                            //txtDocIDLst.Text = sz_lstDocIds;
                            //txtCompanyID.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);


                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                           //try block
                            try
                            {
                                dsetGetPatientList.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dsetGetPatientList.Tables[0].Columns.Add("TREATMENT_STATUS");
                                for (int i = 0; i < dsetGetPatientList.Tables[0].Rows.Count; i++)
                                {
                                    GetProcGroup = GetProcGroup = GetProcGroupCode(dsetGetPatientList.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                                    if (GetProcGroup.ToString().Equals("SYN"))
                                    {
                                        dsetGetPatientList.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                        dsetGetPatientList.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                    }
                                    else if(GetProcGroup.ToString().Equals("AC"))
                                    {
                                        dsetGetPatientList.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                        dsetGetPatientList.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                    }
                                }
                            }
                            catch
                            {
                                dsetGetPatientList.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dsetGetPatientList.Tables[0].Columns.Add("TREATMENT_STATUS");
                            }
                            

                            DataView dv1;
                            dv1 = dsetGetPatientList.Tables[0].DefaultView;

                            Session["grdCheckout"] = dsetGetPatientList;

                            grdCheckout.DataSource = dsetGetPatientList;
                            grdCheckout.DataBind();
                        }
                        else if (Cnt == 2)
                        {
                            arrLst.Clear();
                            lblHeadtwo.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlTwo.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID2.Text = sz_procedure_group_id;
                            //txtDocIDLst2.Text = sz_lstDocIds;
                            //txtCompanyID2.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                        
                            grdCheckout2.DataSource = dsetGetPatientList;
                            grdCheckout2.DataBind();
                        }
                        else if (Cnt == 3)
                        {
                            arrLst.Clear();
                            lblHeadthree.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlThree.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID3.Text = sz_procedure_group_id;
                            //txtDocIDLst3.Text = sz_lstDocIds;
                            //txtCompanyID3.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                      
                            grdCheckout3.DataSource = dsetGetPatientList;
                            grdCheckout3.DataBind();
                        }
                        else if (Cnt == 4)
                        {
                            arrLst.Clear();
                            lblHeadfour.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlFour.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID4.Text = sz_procedure_group_id;
                            //txtDocIDLst4.Text = sz_lstDocIds;
                            //txtCompanyID4.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                            
                            grdCheckout4.DataSource = dsetGetPatientList;
                            grdCheckout4.DataBind();
                        }
                        else if (Cnt == 5)
                        {
                            arrLst.Clear();
                            lblHeadfive.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlfive.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID5.Text = sz_procedure_group_id;
                            //txtDocIDLst5.Text = sz_lstDocIds;
                            //txtCompanyID5.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                           
                            grdCheckout5.DataSource = dsetGetPatientList;
                            grdCheckout5.DataBind();
                        }
                        else if (Cnt == 6)
                        {
                            arrLst.Clear();
                            lblHeadSix.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlsix.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID6.Text = sz_procedure_group_id;
                            //txtDocIDLst6.Text = sz_lstDocIds;
                            //txtCompanyID6.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                            try
                            {
                                dsetGetPatientList.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dsetGetPatientList.Tables[0].Columns.Add("TREATMENT_STATUS");
                                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                                {
                                    GetProcGroup = GetProcGroup = GetProcGroupCode(dsetGetPatientList.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                                    if (GetProcGroup.ToString().Equals("SYN"))
                                    {
                                        dsetGetPatientList.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                        dsetGetPatientList.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                    }
                                    else if (GetProcGroup.ToString().Equals("AC"))
                                    {
                                        dsetGetPatientList.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                        dsetGetPatientList.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dsetGetPatientList.Tables[0].Rows[i][1].ToString());
                                    }
                                }
                            }
                            catch {
                                dsetGetPatientList.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dsetGetPatientList.Tables[0].Columns.Add("TREATMENT_STATUS");
                            }

                            grdCheckout6.DataSource = dsetGetPatientList;
                            grdCheckout6.DataBind();
                        }
                        else if (Cnt == 7)
                        {
                            arrLst.Clear();
                            lblHeadSeven.Text = ds.Tables[0].Rows[j][1].ToString();
                            tabpnlseven.Visible = true;

                            string sz_procedure_group_id = ds.Tables[0].Rows[j][0].ToString();
                            txtProcedureGroupID7.Text = sz_procedure_group_id;
                            //txtDocIDLst7.Text = sz_lstDocIds;
                            //txtCompanyID7.Text = sz_CompanyID;

                            string FromDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string ToDate = DateTime.Now.ToString("MM/dd/yyyy");
                            string sz_VisitStatus = "0";
                            //string sz_VisitType = "";

                            arrLst.Add(sz_UserID);
                            arrLst.Add(sz_procedure_group_id);
                            arrLst.Add(sz_CompanyID);
                            arrLst.Add(FromDate);
                            arrLst.Add(ToDate);
                            arrLst.Add(sz_VisitStatus);
                            //arrLst.Add(sz_VisitType);

                            DataSet dsetGetPatientList = new DataSet();
                            dsetGetPatientList = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
                            try
                            {
                                dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dset.Tables[0].Columns.Add("TREATMENT_STATUS");
                                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                                {
                                    GetProcGroup = GetProcGroup = GetProcGroupCode(dsetGetPatientList.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                                    if (GetProcGroup.ToString().Equals("SYN"))
                                    {
                                        dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                                        dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                                    }
                                    else if (GetProcGroup.ToString().Equals("AC"))
                                    {
                                        dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dset.Tables[0].Rows[i][1].ToString());
                                        dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dset.Tables[0].Rows[i][1].ToString());
                                    }
                                }
                            }
                            catch
                            {
                                dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
                                dset.Tables[0].Columns.Add("TREATMENT_STATUS");
                            }
                            grdCheckout7.DataSource = dsetGetPatientList;
                            grdCheckout7.DataBind();
                            
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //   dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
            //  dset.Tables[0].Columns.Add("TREATMENT_STATUS");
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
            cv.MakeReadOnlyPage("Bill_Sys_CheckOut.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearch_1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try 
        {
            GetPatientsList();
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

    protected void btnSearch_2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist2.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID2.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_2.Text);
            arrLst.Add(txtToDate_2.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(extddlVisitType2.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            grdCheckout2.DataSource = dset;
            grdCheckout2.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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

    protected void btn_Search_3_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist3.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID3.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_3.Text);
            arrLst.Add(txtToDate_3.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(ExtendedDropDownList1.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            grdCheckout3.DataSource = dset;
            grdCheckout3.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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

    protected void btnSearch_4_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist4.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID4.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_4.Text);
            arrLst.Add(txtToDate_4.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(ExtendedDropDownList2.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            grdCheckout4.DataSource = dset;
            grdCheckout4.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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

    protected void btnSearch_5_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist5.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID5.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_5.Text);
            arrLst.Add(txtToDate_5.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(ExtendedDropDownList3.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            grdCheckout5.DataSource = dset;
            grdCheckout5.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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

    protected void btnSearch_6_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist6.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID6.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_6.Text);
            arrLst.Add(txtToDate_6.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(ExtendedDropDownList4.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
            //tabVistInformation.ActiveTabIndex = 6;
            grdCheckout6.DataSource = dset;
            grdCheckout6.DataBind();
            
            
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

    protected void btnSearch_7_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";
        try
        {
            foreach (ListItem li in rdlist7.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID7.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_7.Text);
            arrLst.Add(txtToDate_7.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(ExtendedDropDownList5.Text);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            grdCheckout7.DataSource = dset;
            grdCheckout7.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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



    protected void rdlist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
        
        try
        {

            foreach (ListItem li in rdlist1.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_1.Text);
            arrLst.Add(txtToDate_1.Text);
            arrLst.Add(VisitStatus);

          // dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
            dset = GetPatientsListSpeciality(arrLst);
            try
            {
                dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
                dset.Tables[0].Columns.Add("TREATMENT_STATUS");
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {
                    GetProcGroup = GetProcGroup = GetProcGroupCode(dset.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                                    if (GetProcGroup.ToString().Equals("SYN"))
                                    {
                                        dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                                        dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                                    }
                                    else if (GetProcGroup.ToString().Equals("AC"))
                                    {
                                        dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dset.Tables[0].Rows[i][1].ToString());
                                        dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dset.Tables[0].Rows[i][1].ToString());
                                    }
                }
            }
            catch { }

            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;


            tabVistInformation.ActiveTabIndex = 0;
            grdCheckout.DataSource = dset;
            grdCheckout.DataBind();

            txtFromDate_1.Text = "";
            txtToDate_1.Text = "";
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

    protected void rdlist2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist2.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID2.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_2.Text);
            arrLst.Add(txtToDate_2.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;



            tabVistInformation.ActiveTabIndex = 1;
            grdCheckout2.DataSource = dset;
            grdCheckout2.DataBind();

            txtFromDate_2.Text = "";
            txtToDate_2.Text = "";
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

    protected void rdlist3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist3.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID3.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_3.Text);
            arrLst.Add(txtToDate_3.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);


            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;


            tabVistInformation.ActiveTabIndex = 2;
            grdCheckout3.DataSource = dset;
            grdCheckout3.DataBind();

            txtFromDate_3.Text = "";
            txtToDate_3.Text = "";
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

    protected void rdlist4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist4.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID4.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_4.Text);
            arrLst.Add(txtToDate_4.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);


            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;



            tabVistInformation.ActiveTabIndex = 3;
            grdCheckout4.DataSource = dset;
            grdCheckout4.DataBind();

            txtFromDate_4.Text = "";
            txtToDate_4.Text = "";
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

    protected void rdlist5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist5.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID5.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_5.Text);
            arrLst.Add(txtToDate_5.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;



            tabVistInformation.ActiveTabIndex = 4;
            grdCheckout5.DataSource = dset;
            grdCheckout5.DataBind();

            txtFromDate_5.Text = "";
            txtToDate_5.Text = "";
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

    protected void rdlist6_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist6.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }


            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID6.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_6.Text);
            arrLst.Add(txtToDate_6.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;


            tabVistInformation.ActiveTabIndex = 5;
            grdCheckout6.DataSource = dset;
            grdCheckout6.DataBind();

            //tabpnlsix.Focus();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((RadioButtonList)sender)) - 1;
            //txtFromDate_6.Text = "";
            //txtToDate_6.Text = "";
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

    protected void rdlist7_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        string VisitStatus = "";
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        try
        {

            foreach (ListItem li in rdlist7.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID7.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_7.Text);
            arrLst.Add(txtToDate_7.Text);
            arrLst.Add(VisitStatus);

            dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);

            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;


            tabVistInformation.ActiveTabIndex = 6;
            grdCheckout7.DataSource = dset;
            grdCheckout7.DataBind();

            txtFromDate_7.Text = "";
            txtToDate_7.Text = "";
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

    //protected void grdCheckout_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    string sz_case_id;
    //    _obj_CheckOutBO = new Bill_Sys_CheckoutBO();
    //    DataSet dset = new DataSet();
    //    try
    //    {
    //        if (e.CommandName == "ShowDiagnosisDetails")
    //        {
    //            sz_case_id = e.CommandArgument.ToString();
    //            string company_id = txtCompanyID.Text;
    //            dset = _obj_CheckOutBO.GetCaseDiagnosis(sz_case_id, company_id);

    //            pnlViewPatientDetails.Visible = true;
    //            grddiagnosis.DataSource = dset;
    //            grddiagnosis.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}

    #region "Open form according to speciality"

    protected void grdCheckout_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["grdCheckout"];
            dv = ds.Tables[0].DefaultView;
            int flag = 0;


            if (e.CommandName == "Open Form")
            {
                flag = 1;
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[15].Text.ToString();
                String szEventID = e.Item.Cells[14].Text.ToString();

                //ashutosh...
                Session["eventId"] = szEventID.ToString();
                //
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
            }



            else if (e.CommandName.ToString() == "Case")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Patient Name")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Insurance Company")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }

            else if (e.CommandName.ToString() == "Visit Date")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }

            if (flag == 0)
            {
                dv.Sort = txtSort.Text;
                grdCheckout.DataSource = dv;
                grdCheckout.DataBind();
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

    protected void grdCheckout2_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open Form")
            {
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[13].Text.ToString();
                String szEventID = e.Item.Cells[14].Text.ToString();
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
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

    protected void grdCheckout3_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open Form")
            {
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[13].Text.ToString();
                String szEventID = e.Item.Cells[12].Text.ToString();
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
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

    protected void grdCheckout4_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open Form")
            {
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[13].Text.ToString();
                String szEventID = e.Item.Cells[12].Text.ToString();
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
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

    protected void grdCheckout5_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open Form")
            {
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[13].Text.ToString();
                String szEventID = e.Item.Cells[12].Text.ToString();
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
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

    protected void grdCheckout6_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open Form")
            {
                String szProcedureGroupID = e.Item.Cells[10].Text.ToString();
                String szProcedureGroup = e.Item.Cells[13].Text.ToString();
                String szEventID = e.Item.Cells[12].Text.ToString();
                String szVisitType = e.Item.Cells[8].Text.ToString();
                String szCaseID = e.Item.Cells[0].Text.ToString();
                TransferControl(szEventID, szProcedureGroupID, szProcedureGroup, szVisitType, szCaseID);
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



    public void TransferControl(String p_szEventID, String p_szProcedureGroupID, String p_szProcedureGroup, String p_szVisitType, String p_szCaseID)
    {

        SpecialityPDFDAO _obj = new SpecialityPDFDAO();
        _obj.ProcedureGroup = p_szProcedureGroup;
        _obj.ProcedureGroupID = p_szProcedureGroupID;
        _obj.VisitType = p_szVisitType;
        _obj.CaseID = p_szCaseID;
        _obj.EventID = p_szEventID;
        Session["SPECIALITY_PDF_OBJECT"] = _obj;

        if (p_szProcedureGroup == "PT" || p_szProcedureGroup == "pt")
        {
            Response.Redirect("Bill_Sys_CO_PTNotes.aspx?EID=" + p_szEventID, false);
        }
        else if (p_szProcedureGroup == "IM" || p_szProcedureGroup == "im")
        {
            if (p_szVisitType == "IE")
            {
                Response.Redirect("Bill_Sys_IM_HistoryOfPresentIillness.aspx?EID=" + p_szEventID, false);
            }
            else if (p_szVisitType == "FU")
            {
                Response.Redirect("Bill_Sys_FUIM_StartExamination.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }

        }
        else if (p_szProcedureGroup == "AC" || p_szProcedureGroup == "ac")
        {
            if (p_szVisitType == "FU")
            {
                Response.Redirect("Bill_Sys_AC_Acupuncture_Followup.aspx?EID=" + p_szEventID, false);
            }
           else if (p_szVisitType == "IE")
            {
                Response.Redirect("Bill_Sys_AC_Accu_Initial_1.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }
        }
        else if (p_szProcedureGroup == "ROM" || p_szProcedureGroup == "rom")
        {
            Response.Redirect("Bill_Sys_Rom.aspx?EID=" + p_szEventID, false);
        }
        else if (p_szProcedureGroup == "CH" || p_szProcedureGroup == "ch")
        {
            if (p_szVisitType == "IE")
            {
                Response.Redirect("Bill_Sys_CO_Chiro_Ca.aspx?EID=" + p_szEventID, false);
            }
            else
            {
                Response.Redirect("Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
            }
        }
        else
        {
            Response.Redirect("Bill_Sys_CO_PTNotes_1.aspx?EID=" + p_szEventID, false);
        }
    }

    public string Get_Signature_Code(string eventId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string StatusId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_getSignature_Status", conn);
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

  
            //return "";
            return ds.Tables[0].Rows[0][0].ToString();
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
        return "";
        }

        //finally { conn.Close(); }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        
    }

    public string Get_Treatment_Code(string eventId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string StatusId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_get_Treatment_status", conn);

            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            //finally { conn.Close(); }
            return ds.Tables[0].Rows[0][0].ToString();
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
            return "Pending";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPatientsListSpeciality(ArrayList _objarrLst)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        ds = new DataSet();
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_CO_GET_PATIENT_CHECK_FINALIZE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_USER_ID", _objarrLst[0].ToString());
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", _objarrLst[1].ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _objarrLst[2].ToString());
            comm.Parameters.AddWithValue("@DT_FROM_DATE", _objarrLst[3].ToString());
            comm.Parameters.AddWithValue("@DT_TO_DATE", _objarrLst[4].ToString());
            comm.Parameters.AddWithValue("@I_STATUS", _objarrLst[5].ToString());
            //comm.Parameters.AddWithValue("@SZ_VISIT_TYPE_ID", _objarrLst[6].ToString());
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            return ds;
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
        return null;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

        arrLst.Add(txtuserid.Text);
        arrLst.Add(txtProcedureGroupID.Text);
        arrLst.Add(txtCompanyID.Text);
        arrLst.Add(txtFromDate_1.Text);
        arrLst.Add(txtToDate_1.Text);
        arrLst.Add("");
        dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
      
        // dset = GetPatientsListSpeciality(arrLst);
        try
        {
            dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
            dset.Tables[0].Columns.Add("TREATMENT_STATUS");
            for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
            {
                GetProcGroup = GetProcGroup = GetProcGroupCode(dset.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                if (GetProcGroup.ToString().Equals("SYN"))
                {
                    dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                    dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                }
                else if(GetProcGroup.ToString().Equals("AC"))
                {
                    dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dset.Tables[0].Rows[i][1].ToString());
                    dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dset.Tables[0].Rows[i][1].ToString());
                }
            }
            grdCheckout.DataSource = dset;
            grdCheckout.DataBind();
        }
        catch(Exception ex)
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



    protected void GetPatientsList()
     {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        arrLst = new ArrayList();
        DataSet dset = new DataSet();
        string VisitStatus = "";

        try
        {
            _obj_CheckOutBO = new Bill_Sys_CheckoutBO();

            foreach (ListItem li in rdlist1.Items)
            {
                if (li.Selected == true)
                {
                    VisitStatus = li.Value.ToString(); ;
                    break;
                }
            }

            arrLst.Add(txtuserid.Text);
            arrLst.Add(txtProcedureGroupID.Text);
            arrLst.Add(txtCompanyID.Text);
            arrLst.Add(txtFromDate_1.Text);
            arrLst.Add(txtToDate_1.Text);
            arrLst.Add(VisitStatus);
            //arrLst.Add(extddlVisitType.Text);

           // dset = _obj_CheckOutBO.GetPatientsListSpeciality(arrLst);
            dset = GetPatientsListSpeciality(arrLst);

            try
            {
                dset.Tables[0].Columns.Add("SIGNATURE_STATUS");
                dset.Tables[0].Columns.Add("TREATMENT_STATUS");
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {
                    GetProcGroup = GetProcGroup = GetProcGroupCode(dset.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP_ID"].ToString());
                if (GetProcGroup.ToString().Equals("SYN"))
                {
                    dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                    dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_CodeSYN(dset.Tables[0].Rows[i][1].ToString());
                }
                else if (GetProcGroup.ToString().Equals("AC"))
                {
                    dset.Tables[0].Rows[i]["SIGNATURE_STATUS"] = Get_Signature_Code(dset.Tables[0].Rows[i][1].ToString());
                    dset.Tables[0].Rows[i]["TREATMENT_STATUS"] = Get_Treatment_Code(dset.Tables[0].Rows[i][1].ToString());
                }
                }
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
           


            DataView dv1;
            dv1 = dset.Tables[0].DefaultView;

            Session["grdCheckout"] = dset;


            grdCheckout.DataSource = dset;
            grdCheckout.DataBind();
            //tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;

        }
        catch(Exception ex)
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



    protected string  GetProcGroupCode(string ProceGroupID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        ds = new DataSet();
        string ProcGroupCode;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("SP_GET_PROCEDURE_GROUP_CODE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", ProceGroupID);
            comm.Parameters.AddWithValue("@FLAG", "SPECIALITY");
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
            ProcGroupCode = ds.Tables[0].Rows[0][0].ToString();
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
            return null;
        }
        
        
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return ProcGroupCode;

    }


    public string Get_Signature_CodeSYN(string eventId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string StatusId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_getSignature_StatusSYN", conn);
            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);


            //return "";
            return ds.Tables[0].Rows[0][0].ToString();
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
            return "";
        }

        //finally { conn.Close(); }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    public string Get_Treatment_CodeSYN(string eventId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string StatusId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_get_Treatment_statusSYN", conn);

            comm.Parameters.AddWithValue("@I_EVENT_ID", eventId);
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            //finally { conn.Close(); }
            return ds.Tables[0].Rows[0][0].ToString();
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
            return "Pending";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
}
