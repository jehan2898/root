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
 
public partial class PatientInformation : System.Web.UI.UserControl
{
    Bill_Sys_PatientDeskList PatientList;
    protected void Page_Load(object sender, EventArgs e)
    {                                
    }

    //SP_GET_PATIENT_DESK_DETAILS
    public void GetPatienDeskList(string caseID, string companyID)
    {
        try
        {

            if (!((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                ScriptManager.RegisterStartupScript(this,GetType(), "ss", "spanhide();", true);
            }

            PatientList = new Bill_Sys_PatientDeskList();

            // Tushar:- To Bind Repeater
            rptPatientDeskList.DataSource = PatientList.GetPatienDeskList(caseID, companyID);
            rptPatientDeskList.DataBind();            
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1"))
            {
            }
            else
            {
                for (int i = 0; i < rptPatientDeskList.Items.Count; i++)
                {
                    HtmlTableCell tblcell = (HtmlTableCell)rptPatientDeskList.Controls[0].FindControl("tblheader");
                    HtmlTableCell tblcellvalue = (HtmlTableCell)rptPatientDeskList.Items[i].FindControl("tblvalue");
                    tblcell.Visible = false;
                    tblcellvalue.Visible = false;
                }
            }
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
            {

            }
            else
            {
                for (int i = 0; i < rptPatientDeskList.Items.Count; i++)
                {
                    HtmlTableCell tblcell = (HtmlTableCell)rptPatientDeskList.Controls[0].FindControl("tblRemoteCaseid");
                    HtmlTableCell tblcellvalue = (HtmlTableCell)rptPatientDeskList.Items[i].FindControl("tblRemoteValue");
                    tblcell.Visible = false;
                    tblcellvalue.Visible = false;
                }
            }
        }
        catch(Exception ex)
        {
            ex.ToString();
        }
      
    }
}
