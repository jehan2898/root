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
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;


public partial class AJAX_Pages_Bill_Sys_Doctor_Notes : PageBase
{
    string Company_id = "",user_id="";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        //extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

         Company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
         user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
         if (!IsPostBack)
         { 
         
        Bill_Sys_Settings obj = new Bill_Sys_Settings();
        DataSet ds = new DataSet();
        ds = obj.GetSpeciality(Company_id);
        DDLDiagnosis.DataSource = ds;
        DDLDiagnosis.DataBind();
       
         }

    }
   
    protected void DDLDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        string selected_item = DDLDiagnosis.SelectedItem.Value.ToString();
        
        Bill_Sys_Settings obj = new Bill_Sys_Settings();
        DataSet ds = new DataSet();
        ds = obj.GetDoctorNotes(selected_item, Company_id);
        grdDoctorNotes.DataSource = ds;
        grdDoctorNotes.DataBind();

        for (int i = 0; i < grdDoctorNotes.VisibleRowCount; i++)
        {
            string speciality = selected_item;


            //string speciality =  obj.GetSpeciality(selected_item, Company_id);

            GridViewDataColumn c = (GridViewDataColumn)grdDoctorNotes.Columns[0];
            CheckBox checkBox = (CheckBox)grdDoctorNotes.FindRowCellTemplateControl(i, c, "chkall");
            if ((ds.Tables[0].Rows[i]["i_mst_mandatory_id"].ToString() != "") && (ds.Tables[0].Rows[i]["sz_specialty_id"].ToString() == speciality))
            {
                checkBox.Checked = true;

            }
        }
        if (grdDoctorNotes.VisibleRowCount == 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No record found";
        }
           
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string selected_item1 = DDLDiagnosis.SelectedItem.Value.ToString();
        //string code;
        //string speciality = DDLDiagnosis.SelectedItem.Text;
        string speciality = DDLDiagnosis.SelectedItem.Value.ToString();
        DataSet ds = new DataSet();
        Bill_Sys_Settings obj = new Bill_Sys_Settings();
        for (int i = 0; i < grdDoctorNotes.VisibleRowCount;i++)
        {
           

            GridViewDataColumn c = (GridViewDataColumn)grdDoctorNotes.Columns[0];
            CheckBox checkBox = (CheckBox)grdDoctorNotes.FindRowCellTemplateControl(i, c, "chkall");//Chkbox Id name
            if (checkBox != null)
            {
                if (checkBox.Checked)
                {

                   
                 
                    string id = Convert.ToString(grdDoctorNotes.GetRowValues(i, "i_txn_id"));
                    ds = obj.GetNotes(id, Company_id, user_id, selected_item1);




                }
                else
                {
                    DataSet dss = new DataSet();
                    string id = Convert.ToString(grdDoctorNotes.GetRowValues(i, "i_txn_id"));
                    dss = obj.DeleteNodes(id, Company_id, user_id);
                }
            }
           
        }
        if (grdDoctorNotes.VisibleRowCount > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Your changes made to server successfully.";
        }
        //if (grdDoctorNotes.VisibleRowCount == 0)
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "No record found";
        //}

    }
    //protected void btnclear_Click(object sender, EventArgs e)
    //{
    //    //lblmsg.Visible = false;
    //    //ClientScript.RegisterStartupScript(typeof(string), "auto_refreshparent", @" window.opener.location.reload(); ", true);
    //    //ClientScript.RegisterStartupScript(typeof(Page), "ThatsAllFolks", "window.close();", true);

    //    //Response.Redirect("~/AJAX Pages/Bill_Sys_AssignSysSettings.aspx");
    //    //Page.ClientScript.RegisterStartupScript(GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
      
    //}
    //protected void btnclose_Click(object sender, EventArgs e)
    //{
    //    //ScriptManager.RegisterStartupScript(this, GetType(), "kk11", "windowclose();", true);
    //    ClientScript.RegisterStartupScript(GetType(), "CloseScript","window.opener.location.reload(); window.close();", true);
       

      
    //}
}