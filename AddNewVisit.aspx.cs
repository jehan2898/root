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
using System.Data.SqlClient;

public partial class AddNewVisit : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            trValid.Visible = false;
            trInvalid.Visible = false;
            lblMsg.Visible = false;
            btnSave.Attributes.Add("onclick", "return formValidator('frm','extddlDoctor,txtAddDate,extddlVisitType,txtCaseNo');");
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlDoctor.Flag_ID = txtCompanyID.Text;
                extddlVisitType.Flag_ID = txtCompanyID.Text;
                //RowProcedureCode.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request="+ id + ".Please share with Technical support.";
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
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int Count = 0;
            DateTime currentdate;
            currentdate = DateTime.Today;
            DateTime inserted_date;
            inserted_date = Convert.ToDateTime(txtAddDate.Text);

            if (DateTime.Compare(inserted_date, currentdate) > 0)
            {
                lblMsg.Text = "Selected Date should be less to Current Date";
                lblMsg.Visible = true;
                return;
            }

            if (lstProcedureCode.Items.Count > 0)
            {
                foreach (ListItem li in lstProcedureCode.Items)
                {
                    if (li.Selected == true)
                    {
                        Count = Count + 1;
                    }
                }

                if (Count == 0)
                {
                    lblMsg.Text = "Select atleast 1 procedure code";
                    lblMsg.Visible = true;
                    return;
                }
            }

            Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            CaseDetailsBO objcaseid = new CaseDetailsBO();
            string CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string DoctorID = extddlDoctor.Text;
            string ChkDate = txtAddDate.Text;
            string VisitType = extddlVisitType.Text;
            string CaseID;
            int flag = 0;


            string[] arrlstCaseNo = txtCaseNo.Text.Split(new char[] { ',' });
            ArrayList arrlstAddDetails = new ArrayList();

            Bill_Sys_AddVisit_DAO obj_bill_getset_value;
            for (int i = 0; i < arrlstCaseNo.Length; i++)
            {
                obj_bill_getset_value = new Bill_Sys_AddVisit_DAO();
                obj_bill_getset_value.CaseNo = arrlstCaseNo[i];
                obj_bill_getset_value.CompanyId = CompanyID;
                obj_bill_getset_value.DoctorID = DoctorID;
                obj_bill_getset_value.EventDate = ChkDate;
                obj_bill_getset_value.EventEndTimeType = "AM";
                obj_bill_getset_value.EventTimeType = "AM";
                obj_bill_getset_value.EventEndTime = "9.00";
                obj_bill_getset_value.EventNote = "";
                obj_bill_getset_value.IStatus = "2";
                obj_bill_getset_value.EventTime = "8.30";
                obj_bill_getset_value.TypeCodeId = "TY000000000000000003";
                obj_bill_getset_value.VisitTypeId = VisitType;

                arrlstAddDetails.Add(obj_bill_getset_value);
            }

            ArrayList arrlstProcedureCode = new ArrayList();

            foreach (ListItem li in lstProcedureCode.Items)
            {
                obj_bill_getset_value = new Bill_Sys_AddVisit_DAO();
                if (li.Selected == true)
                {
                    obj_bill_getset_value.ProcedureCode = li.Value.ToString();
                    arrlstProcedureCode.Add(obj_bill_getset_value);
                    flag = 1;
                }
            }

            if (flag == 1)
            {
                Bill_Sys_Bulk_Visits objVisits = _bill_Sys_Visit_BO.InsertBulkVisit(arrlstAddDetails, arrlstProcedureCode);
                if (objVisits.InValidList != null)
                {
                    if (objVisits.InValidList.Count != 0)
                    {
                        // show list of in-valid case numbers. "Visits could not be added to these cases"
                        lblInvalidCaseNo.Text = "";
                        for (int i = 0; i < objVisits.InValidList.Count; i++)
                        {
                            lblInvalidCaseNo.Text += objVisits.InValidList[i].ToString() + ",";
                        }
                        lblInvalidCaseNo.Text = lblInvalidCaseNo.Text.Substring(0, lblInvalidCaseNo.Text.Length - 1);

                        trInvalid.Visible = true;
                    }
                }

                if (objVisits.ValidList != null)
                {
                    if (objVisits.ValidList.Count != 0)
                    {
                        // show list of valid case numbers. "Visits that could be added to the cases"
                        lblValidCaseNo.Text = "";
                        for (int i = 0; i < objVisits.ValidList.Count; i++)
                        {
                            lblValidCaseNo.Text += objVisits.ValidList[i].ToString() + ",";
                        }
                        lblValidCaseNo.Text = lblValidCaseNo.Text.Substring(0, lblValidCaseNo.Text.Length - 1);
                        trValid.Visible = true;
                    }
                }
                pnlChkCaseno.Visible = true;
                ClearControl();
            }

            else if (flag == 0)
            {
                Bill_Sys_Bulk_Visits objVisits = _bill_Sys_Visit_BO.InsertBulkVisit(arrlstAddDetails, arrlstProcedureCode);
                if (objVisits.InValidList != null)
                {
                    if (objVisits.InValidList.Count != 0)
                    {
                        // show list of in-valid case numbers. "Visits could not be added to these cases"
                        for (int i = 0; i < objVisits.InValidList.Count; i++)
                        {
                            lblInvalidCaseNo.Text += objVisits.InValidList[i].ToString() + ",";
                        }
                        lblInvalidCaseNo.Text = lblInvalidCaseNo.Text.Substring(0, lblInvalidCaseNo.Text.Length - 1);

                        trInvalid.Visible = true;
                    }
                }

                if (objVisits.ValidList != null)
                {
                    if (objVisits.ValidList.Count != 0)
                    {
                        // show list of valid case numbers. "Visits that could be added to the cases"
                        for (int i = 0; i < objVisits.ValidList.Count; i++)
                        {
                            lblValidCaseNo.Text += objVisits.ValidList[i].ToString() + ",";
                        }
                        lblValidCaseNo.Text = lblValidCaseNo.Text.Substring(0, lblValidCaseNo.Text.Length - 1);
                        trValid.Visible = true;
                    }
                }
                pnlChkCaseno.Visible = true;
                ClearControl();

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


    private void ClearControl()
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCaseNo.Text = "";
            txtAddDate.Text = "";
            extddlDoctor.Text = "NA";
            extddlVisitType.Text = "NA";
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

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        if (_bill_Sys_Calender.CheckReferralExists(extddlDoctor.Text, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
        {
            //RowProcedureCode.Visible = true;
            GetProcedureCode(extddlDoctor.Text);
        }
        else
        {
            lstProcedureCode.Items.Clear();
            //RowProcedureCode.Visible = false;
        }
    }

    private void GetProcedureCode(string p_szDoctorID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            lstProcedureCode.Items.Clear();
            lstProcedureCode.DataSource = _bill_Sys_Calender.GetReferringDoctorProcedureCodeList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, p_szDoctorID);
            lstProcedureCode.DataTextField = "DESCRIPTION";
            lstProcedureCode.DataValueField = "CODE";
            lstProcedureCode.DataBind();
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
