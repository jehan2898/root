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

public partial class AJAX_Pages_Bill_Sys_Reminders : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");

        if (!IsPostBack)
        {
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            Session["ReminderGrid"] = "";
            Session["CreatedReminder"] = "";
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            //Code change for Defect id:MH01 ---- Start
            Session["ReminderGrid"] = objReminder.getAssignReminderList(txtUserID.Text);
            grdReminderGrid.CurrentPageIndex = 0;
            grdReminderGrid.DataSource = (DataSet)Session["ReminderGrid"];
            grdReminderGrid.DataBind();
            //Code change for Defect id:MH01 ---- End
            foreach (DataGridItem grdItem in grdReminderGrid.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extRStatus = (ExtendedDropDownList.ExtendedDropDownList)grdItem.FindControl("extddlReminderStatus");
                extRStatus.Text = grdItem.Cells[6].Text;
            }
            //Code change for Defect id:MH01 ---- Start
            Session["CreatedReminder"] = objReminder.getCreatedReminderList(txtUserID.Text);
            grdCreatedReminder.CurrentPageIndex = 0;
            grdCreatedReminder.DataSource = (DataSet)Session["CreatedReminder"];
            grdCreatedReminder.DataBind();
            //Code change for Defect id:MH01 ---- End
            foreach (DataGridItem grdCItem in grdCreatedReminder.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extCStatus = (ExtendedDropDownList.ExtendedDropDownList)grdCItem.FindControl("extddlCReminderStatus");
                extCStatus.Text = grdCItem.Cells[6].Text;
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            foreach (DataGridItem grdItem in grdReminderGrid.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extRStatus = (ExtendedDropDownList.ExtendedDropDownList)grdItem.FindControl("extddlReminderStatus");
                objReminder.UpdateAssignReminder(grdItem.Cells[0].Text, extRStatus.Text);
            }
            BindGrid();
            ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Reminder updated successfully...!!')", true);
            //lblMsg.Visible = true;
            //lblMsg.Text = "Reminder updated successfully";
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

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            //Code change for Defect id:MH01 ---- Start
            Session["ReminderGrid"] = objReminder.getFilterReminderList(extddlSearchReminderStatus.Text, txtReminderDate.Text, "", txtUserID.Text);
            grdReminderGrid.CurrentPageIndex = 0;
            grdReminderGrid.DataSource = (DataSet)Session["ReminderGrid"];
            grdReminderGrid.DataBind();
            //Code change for Defect id:MH01 ---- End

            foreach (DataGridItem grdItem in grdReminderGrid.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extRStatus = (ExtendedDropDownList.ExtendedDropDownList)grdItem.FindControl("extddlReminderStatus");
                extRStatus.Text = grdItem.Cells[6].Text;
            }

            lblMsg.Visible = false;
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

    protected void btnCFilter_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            btnDelete.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            //Code change for Defect id:MH01 ---- Start
            Session["CreatedReminder"] = objReminder.getFilterReminderList(extddlCReminderStatus.Text, txtCReminderDate.Text, txtUserID.Text, "");
            grdCreatedReminder.CurrentPageIndex = 0;
            grdCreatedReminder.DataSource = (DataSet)Session["CreatedReminder"];
            grdCreatedReminder.DataBind();
            //Code change for Defect id:MH01 ---- End
            foreach (DataGridItem grdCItem in grdCreatedReminder.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extCStatus = (ExtendedDropDownList.ExtendedDropDownList)grdCItem.FindControl("extddlCReminderStatus");
                extCStatus.Text = grdCItem.Cells[6].Text;
            }
            lblMsg.Visible = false;
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            foreach (DataGridItem dgiReminder in grdCreatedReminder.Items)
            {
                CheckBox chkSelect = (CheckBox)dgiReminder.Cells[6].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    int intReminderId = Convert.ToInt32(dgiReminder.Cells[0].Text.ToString());
                    objReminder.RemoveAssignReminder(intReminderId);
                }
            }
            BindGrid();
            ScriptManager.RegisterClientScriptBlock(btnUpdate, typeof(Button), "Msg", "alert('Reminder removed successfully...!!')", true);
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

        finally
        {
            btnDelete.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            objReminder = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdReminderGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            grdReminderGrid.CurrentPageIndex = e.NewPageIndex;
            grdReminderGrid.DataSource = (DataSet)Session["ReminderGrid"];
            grdReminderGrid.DataBind();


            foreach (DataGridItem grdItem in grdReminderGrid.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extRStatus = (ExtendedDropDownList.ExtendedDropDownList)grdItem.FindControl("extddlReminderStatus");
                extRStatus.Text = grdItem.Cells[6].Text;
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

        finally
        {
            objReminder = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdCreatedReminder_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ReminderBO objReminder = new ReminderBO();
        try
        {
            grdCreatedReminder.CurrentPageIndex = e.NewPageIndex;
            grdCreatedReminder.DataSource = (DataSet)Session["CreatedReminder"];
            grdCreatedReminder.DataBind();

            foreach (DataGridItem grdCItem in grdCreatedReminder.Items)
            {
                ExtendedDropDownList.ExtendedDropDownList extCStatus = (ExtendedDropDownList.ExtendedDropDownList)grdCItem.FindControl("extddlCReminderStatus");
                extCStatus.Text = grdCItem.Cells[6].Text;
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

        finally
        {
            objReminder = null;
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
