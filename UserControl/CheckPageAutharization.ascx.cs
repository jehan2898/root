/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       UserControl_CheckPageAutharization.ascx.cs
/*Purpose              :       To Check Page Autharization according to User role 
/*Author               :       Sandeep D
/*Date of creation     :       16 Dec 2008  
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

public partial class UserControl_CheckPageAutharization : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["CASE_OBJECT"] != null)
            {
                lnkCaseID.Visible = false;
                lnkCaseID.Text = " " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "   " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                
              
               // test.NavigateUrl = "~/Bill_Sys_ReCaseDetails.aspx";
                

            }
            if (((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ROLE_NAME.ToLower() != "doctor")
            {
               // lnkCaseID.Visible = true;
                test.Visible = true;
                txtCaseSearch.Visible = true;
                btnGo.Visible = true;
            }
            else
            {
                lnkCaseID.Visible = false;
                test.Visible = false;
                txtCaseSearch.Visible = false;
                btnGo.Visible = false;
            }

            #region "To check page name for "AJAX Pages/Bill_Sys_AppointPatientEntry.aspx"."
            String _url = "";
            if (Request.RawUrl.IndexOf("?") > 0)
            {
                _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
            }
            else
            {
                _url = Request.RawUrl;
            }
            if (_url.Contains("AJAX Pages"))
            {
                Session["AJAXPage"] = "Yes";
            }
            else
            {
                Session["AJAXPage"] = "";
            }
            if (Session["CASE_OBJECT"] != null)
            {
                lnkCaseID.Visible = false;
                lnkCaseID.Text = " " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "   " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;

                test.Visible = true;
                test.Text = " " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "   " + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME;
                // test.NavigateUrl = "~/Bill_Sys_ReCaseDetails.aspx";
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    if (Session["AJAXPage"].ToString() == "Yes")
                    {
                        test.NavigateUrl = "~/AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx";
                    }
                    else
                    {
                        test.NavigateUrl = "~/AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx";
                    }
                }
                else
                {
                    if (Session["AJAXPage"].ToString() == "Yes")
                    {
                        test.NavigateUrl = "~/AJAX%20Pages/Bill_Sys_CaseDetails.aspx";
                    }
                    else
                    {
                        test.NavigateUrl = "~/AJAX%20Pages/Bill_Sys_CaseDetails.aspx";
                    }
                }

            }
            
            

            #endregion
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lnkCaseID_Click(object sender, EventArgs e)
    {
        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            if (Session["AJAXPage"].ToString() == "Yes")
            {
                Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
            }
            else
            {
                Response.Redirect("AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx", false);
            }
        }
        else
        {
            if (Session["AJAXPage"].ToString() == "Yes")
            {
                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
            }
            else
            {
                Response.Redirect("AJAX%20Pages/Bill_Sys_CaseDetails.aspx", false);
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
            string check = txtCaseSearch.Text.Trim();
            int isString = 0;

            foreach (Char chTest in check)
            {
                if (!Char.IsNumber(chTest))
                {
                    // there is a character in the entered text
                    isString = 0;   
                    //break;
                }
                else
                {
                    // when the user has entered the case number
                    // even if a single digit is entered by the user, it will be treated as case number                    
                    isString = 1;
                    break;
                }
            }

            foreach (Char chTest in check)
            {
                if (Char.IsNumber(chTest))
                {
                    isString = 2;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (isString == 0)  // this means user is searching with case number
            {
                {
                    Bill_Sys_BillingCompanyObject objCompany = new Bill_Sys_BillingCompanyObject();
                    objCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
                    Session["CASE_LIST_GO_BUTTON"] = null;
                    //DataSet dsPatientName = _caseDetailsBO.GetCaseListPatientName(txtCaseSearch.Text.Trim(), objCompany.SZ_COMPANY_ID);
                    DataSet dsPatientName = _caseDetailsBO.QuickSearch(txtCaseSearch.Text.Trim(), objCompany.SZ_COMPANY_ID,"");
                    //Session["CASE_LIST_GO_BUTTON"] = dsPatientName; //no need to add in session

                    // if the return dataset has exactly 1 match found, the application automatically takes the user
                    // to the workarea of that case.
                    if (dsPatientName.Tables[0].Rows.Count == 1)
                    {
                        Session["CASE_OBJECT"] = null;
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        //_bill_Sys_CaseObject.SZ_CASE_ID = dsPatientName.Tables[0].Rows[0]["SZ_CASE_ID"].ToString();
                        _bill_Sys_CaseObject.SZ_CASE_ID = dsPatientName.Tables[0].Rows[0]["CaseId"].ToString();
                        //_bill_Sys_CaseObject.SZ_PATIENT_ID = dsPatientName.Tables[0].Rows[0]["SZ_PATIENT_ID"].ToString();
                        _bill_Sys_CaseObject.SZ_PATIENT_ID = dsPatientName.Tables[0].Rows[0]["PatientId"].ToString();
                        //_bill_Sys_CaseObject.SZ_PATIENT_NAME = dsPatientName.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = dsPatientName.Tables[0].Rows[0]["PatientName"].ToString();
                        //_bill_Sys_CaseObject.SZ_COMAPNY_ID = dsPatientName.Tables[0].Rows[0]["SZ_COMPANY_ID"].ToString();
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = dsPatientName.Tables[0].Rows[0]["CompanyId"].ToString();

                        //_bill_Sys_CaseObject.SZ_CASE_NO = dsPatientName.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
                        _bill_Sys_CaseObject.SZ_CASE_NO = dsPatientName.Tables[0].Rows[0]["CaseNo"].ToString();

                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                        //Session["PassedCaseID"] = dsPatientName.Tables[0].Rows[0]["SZ_CASE_NO"].ToString();
                        Session["PassedCaseID"] = dsPatientName.Tables[0].Rows[0]["CaseNo"].ToString();

                        // check whether the login user is from a billing company or test facility.
                        // if billing co. then take him to Bill_Sys_CaseDetails else to Bill_Sys_ReCaseDetails
                        if (objCompany.BT_REFERRING_FACILITY == true)
                        {
                            if (Session["AJAXPage"].ToString() == "Yes")
                            {
                                Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx", false);
                            }
                        }
                        else
                        {
                            if (Session["AJAXPage"].ToString() == "Yes")
                            {
                                Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("AJAX%20Pages/Bill_Sys_CaseDetails.aspx", false);
                            }
                        }
                    }
                    else // if there are more than 1 rows returned by the search, reload the list page and bind the grid
                    {
                        Session["CASE_LIST_GO_BUTTON"] = txtCaseSearch.Text;
                        if (Session["AJAXPage"].ToString() == "Yes")
                        {
                            Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("AJAX Pages/Bill_Sys_SearchCase.aspx", false);
                        }
                    }
                }
            } // execute this block when the user searches by case number
            else if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && isString == 1)
            {
                // example: TM702
                int len = check.Length;
                int i = 0;
                foreach (Char chTest in check)
                {
                    if (!Char.IsNumber(chTest))
                    {
                        i++;
                    }
                   
                }

                string sz_CompanyPrefix = (check.Substring(0, i));
                string sz_CaseNo = check.Substring(2);

                DataSet ds = _caseDetailsBO.QuickSearch(txtCaseSearch.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, sz_CompanyPrefix);

                //if (_caseDetailsBO.CheckCaseExistsWithPrefix(sz_CompanyPrefix, sz_CaseNo, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
                if (ds.Tables[0].Rows.Count > 0) 
                {
                    Session["CASE_OBJECT"] = null;
                    Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                    //_bill_Sys_CaseObject.SZ_CASE_ID = _caseDetailsBO.GetCaseID(check, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _bill_Sys_CaseObject.SZ_CASE_ID = ds.Tables[0].Rows[0]["CaseId"].ToString();

                    //_bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(_bill_Sys_CaseObject.SZ_CASE_ID, "");
                    _bill_Sys_CaseObject.SZ_PATIENT_ID = ds.Tables[0].Rows[0]["patientId"].ToString();

                    //_bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                    _bill_Sys_CaseObject.SZ_PATIENT_NAME = ds.Tables[0].Rows[0]["PatientName"].ToString();

                    //_bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                    _bill_Sys_CaseObject.SZ_COMAPNY_ID = ds.Tables[0].Rows[0]["CompanyId"].ToString();

                    _bill_Sys_CaseObject.SZ_CASE_NO = check;
                    Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    Session["PassedCaseID"] = check;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        if (Session["AJAXPage"].ToString() == "Yes")
                        {
                            Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx", false);
                        }
                    }
                    else
                    {
                        if (Session["AJAXPage"].ToString() == "Yes")
                        {
                            Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("AJAX%20Pages/Bill_Sys_CaseDetails.aspx", false);
                        }
                    }
                }
            }
            else if (isString == 2)// the user is searching by case number --- CASE I
            {
                // we dont need to chcek if case exists in this separate procedure

                // create object of app_code. call new function, pass case number and company id
                // in the procedure, check if you get data.. if yes, set properties those are set below.. else do nothing.
                DataSet ds = new DataSet();
                ds = _caseDetailsBO.QuickSearch(txtCaseSearch.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID,"");

                //if (_caseDetailsBO.CheckCaseExists(txtCaseSearch.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID) == true)
                if(ds.Tables[0].Rows.Count > 0)
                {
                    Session["CASE_OBJECT"] = null;
                    Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();

                    //_bill_Sys_CaseObject.SZ_CASE_ID = _caseDetailsBO.GetCaseID(txtCaseSearch.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    _bill_Sys_CaseObject.SZ_CASE_ID = ds.Tables[0].Rows[0]["caseId"].ToString();
                    
                    //_bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(_bill_Sys_CaseObject.SZ_CASE_ID, "");
                    _bill_Sys_CaseObject.SZ_PATIENT_ID =ds.Tables[0].Rows[0]["PatientId"].ToString();

                    //_bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID);                    
                    _bill_Sys_CaseObject.SZ_PATIENT_NAME = ds.Tables[0].Rows[0]["PatientName"].ToString();

                    //_bill_Sys_CaseObject.SZ_COMAPNY_ID = _caseDetailsBO.GetPatientCompanyID(_bill_Sys_CaseObject.SZ_PATIENT_ID);
                    _bill_Sys_CaseObject.SZ_COMAPNY_ID = ds.Tables[0].Rows[0]["CompanyId"].ToString();

                    //_bill_Sys_CaseObject.SZ_CASE_NO = txtCaseSearch.Text;
                    string StrCaseNo = ds.Tables[0].Rows[0]["CASENO"].ToString();
                    _bill_Sys_CaseObject.SZ_CASE_NO = StrCaseNo.Replace(" ","");

                    Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                    Session["PassedCaseID"] = txtCaseSearch.Text;

                    if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                    {
                        if (Session["AJAXPage"].ToString() == "Yes")
                        {
                            Response.Redirect("Bill_Sys_ReCaseDetails.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("AJAX%20Pages/Bill_Sys_ReCaseDetails.aspx", false);
                        }
                    }
                    else
                    {
                        if (Session["AJAXPage"].ToString() == "Yes")
                        {
                            Response.Redirect("Bill_Sys_CaseDetails.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("AJAX%20Pages/Bill_Sys_CaseDetails.aspx", false);
                        }
                    }
                }
            }

        }
        catch
        {
            
        }
    }
}
