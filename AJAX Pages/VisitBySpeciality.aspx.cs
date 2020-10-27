using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Visit_By_Speciality : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.extddlspeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlInsurance.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.ddlVisitDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        { 
        if (!IsPostBack)
        {
                this.btnSearch.Attributes.Add("onclick", "return OnSearch();");
                txtCompanyID.Text= ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
        }
        catch(Exception ex)
        {

        }
        BindGrid();
    }
    public void BindGrid()
    {
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            //ArrayList aList = new ArrayList();
            //aList.Add(extddlspeciality.Text);
            //aList.Add(txtCompanyID.Text);          
            //aList.Add(extddlInsurance.Text);
            //aList.Add(txtCaseNumber.Text);
            //aList.Add(txtFromVisitDate.Text);
            //aList.Add(txtToVisitDate.Text);            
            //for (int i = 0; i < aList.Count; i++)
            //{
                //SpecialityPDFFill obj = (SpecialityPDFFill)aList[i];
                SqlCommand comm = new SqlCommand("PROC_GET_SPECIALITY_WISE_VISIT_COUNT ", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
                comm.Parameters.AddWithValue("@SZ_SPECIALITY_ID", extddlspeciality.Text);
                comm.Parameters.AddWithValue("@SZ_INSUARNCE_ID",extddlInsurance.Text);
                comm.Parameters.AddWithValue("@SZ_CASE_NO",txtCaseNumber.Text);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
              grdCountWiseSpeciality.DataSource = ds;
              grdCountWiseSpeciality.DataBind();
                //Session["Dataset"] = ds;
           // }
        
    }
        catch(Exception ex)
        {

        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

}