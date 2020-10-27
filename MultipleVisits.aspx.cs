using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;

public partial class MultipleVisits : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSearchPArameter.Attributes.Add("onclick", "return Validate();");
            btnSave.Attributes.Add("onclick", "return CheckDateCode();");
            btnclear.Attributes.Add("onclick","return test(); ");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlEmployer.Flag_ID = txtCompanyID.Text ;
            ajAutoName.ContextKey = txtCompanyID.Text;
            if ((((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {
                RowLocation.Visible = true;
                extddlLocation.Flag_ID = txtCompanyID.Text;
            }
            else
            {
                RowLocation.Visible = false;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            dr["Name"] = "ABC";
            dt.Rows.Add(dr);


        }
    }
    protected void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtEmployerName.Text = extddlEmployer.Selected_Text;
            Bill_Sys_PatientBO objBillSysPatientBO = new Bill_Sys_PatientBO();
            ArrayList arrPatient = new ArrayList();
            arrPatient.Add(txtCompanyID.Text);
            arrPatient.Add(extddlEmployer.Text);
            if (txtCaseNo.Text != "")
            {
                string cases = "";
                string[] spiltcases = txtCaseNo.Text.Trim().Split(',');
                for (int i = 0; i < spiltcases.Length; i++)
                {
                    if (cases == "")
                    {
                        cases = "'" + spiltcases[i].ToString() + "'";
                    }
                    else
                    {
                        cases += ",'" + spiltcases[i].ToString() + "'";
                    }
                }
                arrPatient.Add(cases);
            }
            else
            {
                arrPatient.Add("");
            }
            arrPatient.Add(txtPatientName.Text);
            arrPatient.Add(rdoISActivePatient.SelectedValue.ToString());

            DataSet ds = objBillSysPatientBO.GetEmployeCase(arrPatient);
            grdCaseMaster.DataSource = ds;
            grdCaseMaster.DataBind();

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

    protected void btnSearchPArameter_Click(object sender, EventArgs e)
    {
        BindGrid();
        BindProcedureCodes();
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
            ArrayList arrCases = new ArrayList();
            for (int i = 0; i < grdCaseMaster.Items.Count;i++ )
            {
                CheckBox chk = (CheckBox)grdCaseMaster.Items[i].Cells[0].FindControl("chkSelect");
                if (chk.Checked)
                {
                    EmployerVisitDO objCases = new EmployerVisitDO();
                    objCases.CaseID = grdCaseMaster.Items[i].Cells[2].Text.ToString();
                    objCases.CompanyID = txtCompanyID.Text;
                    objCases.UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    objCases.EmploerID = extddlEmployer.Text;
                    objCases.VisitDate = txtAppointdate.Text;
                    objCases.EmployerVisitProcedure = new List<EmployerVisitProcedure>();
                    foreach (ListItem lstitem in this.lstProcedureCode.Items)
                    {
                        if (lstitem.Selected)
                        {
                            string lstVal = lstitem.Value;
                            string[] spiltVal = lstVal.Split('~');

                            EmployerVisitProcedure objEmployerVisitProcedure = new EmployerVisitProcedure();
                            objEmployerVisitProcedure.ProcedureCode = spiltVal[0].ToString();
                            objEmployerVisitProcedure.ProcedureGroupId = spiltVal[1].ToString();
                            objCases.EmployerVisitProcedure.Add(objEmployerVisitProcedure);

                        }
                    }
                    arrCases.Add(objCases);

                }

            }

            EmployerBO objEmployerBO = new EmployerBO();
           string returnSave= objEmployerBO.SaveVisit(arrCases);
           if (returnSave != "")
           {
               this.usrMessage.PutMessage("Visit save Sucessfully.");
               this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
               this.usrMessage.Show();
           }
           else
           {
               this.usrMessage.PutMessage("can not save the visit");
               this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
               this.usrMessage.Show();
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

    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtAppointdate.Text = "";
        foreach (ListItem lstitem in this.lstProcedureCode.Items)
        {
            lstitem.Selected = false;
        }
    }
    public void BindProcedureCodes()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EmployerBO objEmployerBO = new EmployerBO();
            lstProcedureCode.Items.Clear();
            DataSet ds = objEmployerBO.GetEmployeProcedure(extddlEmployer.Text, txtCompanyID.Text);
            lstProcedureCode.DataSource = ds;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}