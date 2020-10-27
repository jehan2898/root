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
using System.Data.Sql;
using System.Xml;
using System.Text;

public partial class AJAX_Pages_Bill_sys_unbilledTrasferTodoctor : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.con.SourceGrid = grdPatientList;
        this.txtSearchBox.SourceGrid = grdPatientList;
        this.grdPatientList.Page = this.Page;
        this.grdPatientList.PageNumberList = this.con;
      
        if (!IsPostBack)
        {
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extdupdateDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        bindgrid();
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    private void bindgrid()
    {
        txtScheduleid.Text = extddlDoctor.Text;
        grdPatientList.XGridBindSearch();

    }
    protected void btnassign_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ArrayList _objArr = new ArrayList();
            
            timeid.Text = "startTime " + System.DateTime.Now.Millisecond.ToString();
           for (int i = 0; i < grdPatientList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdPatientList.Rows[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                     Bill_sys_updateunBilledDoctor _objdoctor = new Bill_sys_updateunBilledDoctor();
                    _objdoctor.EventID = grdPatientList.DataKeys[i]["EVENTID"].ToString();
                    _objdoctor.CompanyId = txtCompanyID.Text;
                    _objdoctor.PatientID = grdPatientList.DataKeys[i]["SZ_PATIENT_ID"].ToString();
                    _objdoctor.DoctorID = extdupdateDoctor.Text.ToString();
                    _objdoctor.EventDate = txtUpdateDoctor.Text.Trim();
                    _objArr.Add(_objdoctor);
                    
                }
            }
          
            if (_objArr.Count > 0)
            {
                Bill_sys_updateunBilledDoctor _objmethod = new Bill_sys_updateunBilledDoctor();
                string flag = "";
                if (extdupdateDoctor.Text == "NA" && txtUpdateDoctor.Text != "")
                {
                    flag = "EVENT";
                }
                else if (txtUpdateDoctor.Text == "" && extdupdateDoctor.Text != "NA")
                {
                    flag = "DOCTOR";
                }
                else if (extdupdateDoctor.Text != "NA" && txtUpdateDoctor.Text != "")
                {
                    flag = "BOTH";
                }
                string str = "";
                if (flag != "")
                {
                    str = _objmethod.UpdateUnbilledDoctor(_objArr, flag);
                }
                Label1.Text = "endTime " + System.DateTime.Now.Millisecond.ToString() ;
                if (str == "Done")
                {
                    usrMessage.PutMessage("Your changes to the server were made successfully");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    bindgrid();
                }
                else
                {
                    usrMessage.PutMessage(str);
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                }
            }
        }
       
       
    }



    protected  void btnassign_Click1(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str1 = "";
        timeid.Text = "startTime "  +  System.DateTime.Now.Millisecond.ToString();
        SqlConnection sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        SqlTransaction tran;
        sqlcon.Open();
        tran = sqlcon.BeginTransaction();
        string select = "select I_EVENT_ID,DT_EVENT_DATE,SZ_DOCTOR_ID,SZ_COMPANY_ID,SZ_PATIENT_ID from txn_calendar_event where SZ_COMPANY_ID='" + txtCompanyID.Text + "'";
       
        try
        {
            SqlCommand selcmd = new SqlCommand(select,sqlcon);
            selcmd.Transaction = tran;
            SqlDataAdapter da = new SqlDataAdapter(selcmd);
            DataSet ds = new DataSet();
            da.Fill(ds,"change");
            DataTable dt = ds.Tables["change"];
            for (int i = 0; i < grdPatientList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdPatientList.Rows[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                   string grideventid = grdPatientList.DataKeys[i]["EVENTID"].ToString();
                   for (int j = 0; j < dt.Rows.Count; j++)
                   {
                       string eventid = dt.Rows[j]["I_EVENT_ID"].ToString();
                       if (eventid.Equals(grideventid))
                       {
                           if (extdupdateDoctor.Text != "NA")
                           {
                               dt.Rows[j]["SZ_DOCTOR_ID"] = extdupdateDoctor.Text;
                           }
                           if (txtUpdateDoctor.Text != "")
                           {
                               dt.Rows[j]["DT_EVENT_DATE"] = txtUpdateDoctor.Text;
                           }
                           break;
                       }
                   }
                }
            }
            SqlCommand sqlcmd = new SqlCommand("sp_update_unbilled_doctor", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Transaction = tran;
            sqlcmd.Parameters.Add("@sz_doctor_id", SqlDbType.NVarChar, 20, "SZ_DOCTOR_ID");
            sqlcmd.Parameters.Add("@dt_event_date ", SqlDbType.NVarChar, 10, "DT_EVENT_DATE");
            sqlcmd.Parameters.Add("@flag", "BOTH");
            SqlParameter prm = sqlcmd.Parameters.Add("@i_event_id", SqlDbType.NVarChar, 50, "I_EVENT_ID");
            prm.SourceVersion = DataRowVersion.Original;
            da.UpdateCommand = sqlcmd;
            da.Update(ds, "change");
            //sqlcmd.ExecuteNonQuery();
            tran.Commit();
            str1 = "Done";

        }
        catch (Exception ex)
        {
            tran.Rollback();
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        finally
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }
        Label1.Text = "endTime "  + System.DateTime.Now.Millisecond.ToString();
      //  return "Done";
      

        if (str1 == "Done")
        {
            usrMessage.PutMessage("Your changes to the server were made successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            bindgrid();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
}
