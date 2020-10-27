using AjaxControlToolkit;
using ASP;
using ExtendedDropDownList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MultipleVisits : Page, IRequiresSessionState
{

    public MultipleVisits()
    {
    }

    protected void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtEmployerName.Text = this.extddlEmployer.Selected_Text;
            Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
            ArrayList arrayLists = new ArrayList();
            arrayLists.Add(this.txtCompanyID.Text);
            arrayLists.Add(this.extddlEmployer.Text);
            if (this.txtCaseNo.Text == "")
            {
                arrayLists.Add("");
            }
            else
            {
                string str = "";
                string[] strArrays = this.txtCaseNo.Text.Trim().Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    str = (str != "" ? string.Concat(str, ",'", strArrays[i].ToString(), "'") : string.Concat("'", strArrays[i].ToString(), "'"));
                }
                arrayLists.Add(str);
            }
            arrayLists.Add(this.txtPatientName.Text);
            arrayLists.Add(this.rdoISActivePatient.SelectedValue.ToString());
            DataSet employeCase = billSysPatientBO.GetEmployeCase(arrayLists);
            this.grdCaseMaster.DataSource = employeCase;
            this.grdCaseMaster.DataBind();
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

    public void BindProcedureCodes()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EmployerBO employerBO = new EmployerBO();
            this.lstProcedureCode.Items.Clear();
            DataSet employeProcedure = employerBO.GetEmployeProcedure(this.extddlEmployer.Text, this.txtCompanyID.Text);
            this.lstProcedureCode.DataSource = employeProcedure;
            this.lstProcedureCode.DataTextField = "DESCRIPTION";
            this.lstProcedureCode.DataValueField = "CODE";
            this.lstProcedureCode.DataBind();
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

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.txtAppointdate.Text = "";
        foreach (ListItem item in this.lstProcedureCode.Items)
        {
            item.Selected = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.grdCaseMaster.Items.Count; i++)
            {
                if (((CheckBox)this.grdCaseMaster.Items[i].Cells[0].FindControl("chkSelect")).Checked)
                {
                    EmployerVisitDO employerVisitDO = new EmployerVisitDO()
                    {
                        CaseID = this.grdCaseMaster.Items[i].Cells[2].Text.ToString(),
                        CompanyID = this.txtCompanyID.Text,
                        UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString(),
                        EmploerID = this.extddlEmployer.Text,
                        VisitDate = this.txtAppointdate.Text,
                        EmployerVisitProcedure = new List<EmployerVisitProcedure>()
                    };
                    foreach (ListItem item in this.lstProcedureCode.Items)
                    {
                        if (!item.Selected)
                        {
                            continue;
                        }
                        string value = item.Value;
                        string[] strArrays = value.Split(new char[] { '~' });
                        EmployerVisitProcedure employerVisitProcedure = new EmployerVisitProcedure()
                        {
                            ProcedureCode = strArrays[0].ToString(),
                            ProcedureGroupId = strArrays[1].ToString()
                        };
                        employerVisitDO.EmployerVisitProcedure.Add(employerVisitProcedure);
                    }
                    arrayLists.Add(employerVisitDO);
                }
            }
            if ((new EmployerBO()).SaveVisit(arrayLists) == "")
            {
                this.usrMessage.PutMessage("can not save the visit");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("Visit save Sucessfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearchPArameter_Click(object sender, EventArgs e)
    {
        this.BindGrid();
        this.BindProcedureCodes();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.btnSearchPArameter.Attributes.Add("onclick", "return Validate();");
            this.btnSave.Attributes.Add("onclick", "return CheckDateCode();");
            this.btnclear.Attributes.Add("onclick", "return test(); ");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlEmployer.Flag_ID = this.txtCompanyID.Text;
            this.ajAutoName.ContextKey = this.txtCompanyID.Text;
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1")
            {
                this.RowLocation.Visible = false;
            }
            else
            {
                this.RowLocation.Visible = true;
                this.extddlLocation.Flag_ID = this.txtCompanyID.Text;
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            DataRow dataRow = dataTable.NewRow();
            dataRow["Name"] = "ABC";
            dataTable.Rows.Add(dataRow);
        }
    }
}