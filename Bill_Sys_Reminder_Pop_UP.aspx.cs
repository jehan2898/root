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
using Reminders;
public partial class AJAX_Pages_Bill_Sys_Reminder_Pop_UP : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDismiss.Attributes.Add("onClick", "return ConfirmDisMis();");
        if (!Page.IsPostBack)
        {
            //Session["dtReminderView"] = "";
            //Session["intOrder"] = "1";                          
            LoadGrid();
        }
        btnDismiss.Attributes.Add("onclick", "return ValidateText();");
        btnDismissCase.Attributes.Add("onclick", "return ValidateCaseText();");
    }





    public void LoadGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = null; ;
        DataSet dsReminder = null;
        string strUserId = "";
        DateTime dtCurrent_Date;
        try
        {
            objReminder = new ReminderBO();
            dsReminder = new DataSet();
            strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            dtCurrent_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            dsReminder = objReminder.LoadReminderDetails(strUserId, dtCurrent_Date);
            if (dsReminder.Tables[0].Rows.Count > 0)
            {
                lblREsult.Visible = false;
                grdReminder.DataSource = null;
                grdReminder.DataBind();

                grdReminder.DataSource = dsReminder.Tables[0];
                grdReminder.DataBind();
                //Session["dtReminderView"] = dsReminder.Tables[0].DefaultView;
                lblDisReason.Visible = true;
                lblDoctorReminder.Visible = true;
                btnDismiss.Visible = true;
                txtDismissReason.Visible = true;
            }
            else
            {
                //Page.RegisterStartupScript("ss", "<script language='javascript'> CloseCheckoutReminderPopup();</script>");
                divReminder.Visible = false;
                //this.frmReminder.Dispose();
                //this.frmReminder.Style.Add("Visibility", "Hidden");

            }

            if (dsReminder.Tables[1].Rows.Count > 0)
            {
                lblCaseResult.Visible = false;
                grdreminder_Case.DataSource = null;
                grdreminder_Case.DataBind();

                grdreminder_Case.DataSource = dsReminder.Tables[1];
                grdreminder_Case.DataBind();
                lblDisReasonCase.Visible = true;
                lblCaseReminder.Visible = true;
                btnDismissCase.Visible = true;
                txtDismissCaseReason.Visible = true;
            }
            else
            {
                //Page.RegisterStartupScript("ss", "<script language='javascript'> CloseCheckoutReminderPopup();</script>");
                divCase_reminder.Visible = false;
                //this.frmReminder.Dispose();
                //this.frmReminder.Style.Add("Visibility", "Hidden");

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
        
        finally
        {
            objReminder = null;
            dsReminder = null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    protected void btnDismiss_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = null;
        DataSet dsReminder = null;
        DataSet dsLog = null;
        string strUserId, strDismissReason = "";
        int iRecurrence_Id, iReminderID;
        try
        {
            objReminder = new ReminderBO();
            dsReminder = new DataSet();
            dsLog = new DataSet();
            strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

            foreach (DataGridItem dgiDiary in grdReminder.Items)
            {
                CheckBox chkSelect = (CheckBox)dgiDiary.Cells[9].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    iRecurrence_Id = Convert.ToInt32(dgiDiary.Cells[0].Text.Trim().ToString());
                    iReminderID = Convert.ToInt32(dgiDiary.Cells[1].Text.Trim().ToString());
                    strDismissReason = txtDismissReason.Text;
                    dsReminder = objReminder.DismissReminder(iRecurrence_Id, iReminderID, strDismissReason);
                }
            }
            LoadGrid();
            ScriptManager.RegisterClientScriptBlock(btnDismiss, typeof(Button), "Msg", "alert('Reminder dismissed successfully..!!')", true);
            txtDismissReason.Text = "";
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
        
        finally
        {
            objReminder = null;
            dsReminder = null;
            dsLog = null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDismissCase_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = null;
        DataSet dsReminder = null;
        DataSet dsLog = null;
        string strUserId, strDismissReason = "";
        int iRecurrence_Id, iReminderID;
        try
        {
            objReminder = new ReminderBO();
            dsReminder = new DataSet();
            dsLog = new DataSet();
            strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

            foreach (DataGridItem dgiDiary_Case in grdreminder_Case.Items)
            {
                CheckBox chkSelect = (CheckBox)dgiDiary_Case.Cells[9].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    iRecurrence_Id = Convert.ToInt32(dgiDiary_Case.Cells[0].Text.Trim().ToString());
                    iReminderID = Convert.ToInt32(dgiDiary_Case.Cells[1].Text.Trim().ToString());
                    strDismissReason = txtDismissCaseReason.Text;
                    dsReminder = objReminder.DismissReminder(iRecurrence_Id, iReminderID, strDismissReason);
                }
            }
            LoadGrid();
            ScriptManager.RegisterClientScriptBlock(btnDismiss, typeof(Button), "Msg", "alert('Reminder dismissed successfully..!!')", true);
            txtDismissCaseReason.Text = "";
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
       
        {
            objReminder = null;
            dsReminder = null;
            dsLog = null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



}
