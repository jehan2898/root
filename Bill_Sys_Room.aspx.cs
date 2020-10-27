/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Room.aspx.cs
/*Purpose              :       To Add and Edit Room
/*Author               :       Manoj c
/*Date of creation     :       11 Dec 2008  
/*Modified By          :       Bhilendra Y
/*Modified Date        :       28 Oct 2009
/************************************************************/

#region  Using  

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
using System.Data.SqlClient;

#endregion

public partial class Bill_Sys_Room : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    public bool blnEdit = false;

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="room.xml";
            this._listOperation.LoadList();
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

    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    this.ddlHours.Items.Add(i.ToString());
                    this.ddlEndHours.Items.Add(i.ToString());
                }
                else
                {
                    this.ddlHours.Items.Add("0" + i.ToString());
                    this.ddlEndHours.Items.Add("0" + i.ToString());
                }
            }
            for (int j = 0; j < 60; j++)
            {
                if (j > 9)
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                    this.ddlEndMinutes.Items.Add(j.ToString());
                }
                else
                {
                    this.ddlMinutes.Items.Add("0" + j.ToString());
                    this.ddlEndMinutes.Items.Add("0" + j.ToString());
                }
            }
            this.ddlTime.Items.Add("AM");
            this.ddlTime.Items.Add("PM");
            this.ddlEndTime.Items.Add("AM");
            this.ddlEndTime.Items.Add("PM");
            for (int k = 0; k <= 12; k++)
            {
                if (k > 9)
                {
                    this.ddlTuesHours.Items.Add(k.ToString());
                    this.ddlTuesEndHours.Items.Add(k.ToString());
                }
                else
                {
                    this.ddlTuesHours.Items.Add("0" + k.ToString());
                    this.ddlTuesEndHours.Items.Add("0" + k.ToString());
                }
            }
            for (int m = 0; m < 60; m++)
            {
                if (m > 9)
                {
                    this.ddlTuesMinutes.Items.Add(m.ToString());
                    this.ddlTuesEndMinutes.Items.Add(m.ToString());
                }
                else
                {
                    this.ddlTuesMinutes.Items.Add("0" + m.ToString());
                    this.ddlTuesEndMinutes.Items.Add("0" + m.ToString());
                }
            }
            this.ddlTuesTime.Items.Add("AM");
            this.ddlTuesTime.Items.Add("PM");
            this.ddlTuesEndTime.Items.Add("AM");
            this.ddlTuesEndTime.Items.Add("PM");
            for (int n = 0; n <= 12; n++)
            {
                if (n > 9)
                {
                    this.ddlWednHours.Items.Add(n.ToString());
                    this.ddlWednEndHours.Items.Add(n.ToString());
                }
                else
                {
                    this.ddlWednHours.Items.Add("0" + n.ToString());
                    this.ddlWednEndHours.Items.Add("0" + n.ToString());
                }
            }
            for (int num6 = 0; num6 < 60; num6++)
            {
                if (num6 > 9)
                {
                    this.ddlWednMinutes.Items.Add(num6.ToString());
                    this.ddlWednEndMinutes.Items.Add(num6.ToString());
                }
                else
                {
                    this.ddlWednMinutes.Items.Add("0" + num6.ToString());
                    this.ddlWednEndMinutes.Items.Add("0" + num6.ToString());
                }
            }
            this.ddlWednTime.Items.Add("AM");
            this.ddlWednTime.Items.Add("PM");
            this.ddlWednEndTime.Items.Add("AM");
            this.ddlWednEndTime.Items.Add("PM");
            for (int num7 = 0; num7 <= 12; num7++)
            {
                if (num7 > 9)
                {
                    this.ddlThusHours.Items.Add(num7.ToString());
                    this.ddlThusEndHours.Items.Add(num7.ToString());
                }
                else
                {
                    this.ddlThusHours.Items.Add("0" + num7.ToString());
                    this.ddlThusEndHours.Items.Add("0" + num7.ToString());
                }
            }
            for (int num8 = 0; num8 < 60; num8++)
            {
                if (num8 > 9)
                {
                    this.ddlThusMinutes.Items.Add(num8.ToString());
                    this.ddlThusEndMinutes.Items.Add(num8.ToString());
                }
                else
                {
                    this.ddlThusMinutes.Items.Add("0" + num8.ToString());
                    this.ddlThusEndMinutes.Items.Add("0" + num8.ToString());
                }
            }
            this.ddlThusTime.Items.Add("AM");
            this.ddlThusTime.Items.Add("PM");
            this.ddlThusEndTime.Items.Add("AM");
            this.ddlThusEndTime.Items.Add("PM");
            for (int num9 = 0; num9 <= 12; num9++)
            {
                if (num9 > 9)
                {
                    this.ddlFridHours.Items.Add(num9.ToString());
                    this.ddlFridEndHours.Items.Add(num9.ToString());
                }
                else
                {
                    this.ddlFridHours.Items.Add("0" + num9.ToString());
                    this.ddlFridEndHours.Items.Add("0" + num9.ToString());
                }
            }
            for (int num10 = 0; num10 < 60; num10++)
            {
                if (num10 > 9)
                {
                    this.ddlFridMinutes.Items.Add(num10.ToString());
                    this.ddlFridEndMinutes.Items.Add(num10.ToString());
                }
                else
                {
                    this.ddlFridMinutes.Items.Add("0" + num10.ToString());
                    this.ddlFridEndMinutes.Items.Add("0" + num10.ToString());
                }
            }
            this.ddlFridTime.Items.Add("AM");
            this.ddlFridTime.Items.Add("PM");
            this.ddlFridEndTime.Items.Add("AM");
            this.ddlFridEndTime.Items.Add("PM");
            for (int num11 = 0; num11 <= 12; num11++)
            {
                if (num11 > 9)
                {
                    this.ddlSatHours.Items.Add(num11.ToString());
                    this.ddlSatEndHours.Items.Add(num11.ToString());
                }
                else
                {
                    this.ddlSatHours.Items.Add("0" + num11.ToString());
                    this.ddlSatEndHours.Items.Add("0" + num11.ToString());
                }
            }
            for (int num12 = 0; num12 < 60; num12++)
            {
                if (num12 > 9)
                {
                    this.ddlSatMinutes.Items.Add(num12.ToString());
                    this.ddlSatEndMinutes.Items.Add(num12.ToString());
                }
                else
                {
                    this.ddlSatMinutes.Items.Add("0" + num12.ToString());
                    this.ddlSatEndMinutes.Items.Add("0" + num12.ToString());
                }
            }
            this.ddlSatTime.Items.Add("AM");
            this.ddlSatTime.Items.Add("PM");
            this.ddlSatEndTime.Items.Add("AM");
            this.ddlSatEndTime.Items.Add("PM");
            for (int num13 = 0; num13 <= 12; num13++)
            {
                if (num13 > 9)
                {
                    this.ddlSunHours.Items.Add(num13.ToString());
                    this.ddlSunEndHours.Items.Add(num13.ToString());
                }
                else
                {
                    this.ddlSunHours.Items.Add("0" + num13.ToString());
                    this.ddlSunEndHours.Items.Add("0" + num13.ToString());
                }
            }
            for (int num14 = 0; num14 < 60; num14++)
            {
                if (num14 > 9)
                {
                    this.ddlSunMinutes.Items.Add(num14.ToString());
                    this.ddlSunEndMinutes.Items.Add(num14.ToString());
                }
                else
                {
                    this.ddlSunMinutes.Items.Add("0" + num14.ToString());
                    this.ddlSunEndMinutes.Items.Add("0" + num14.ToString());
                }
            }
            this.ddlSunTime.Items.Add("AM");
            this.ddlSunTime.Items.Add("PM");
            this.ddlSunEndTime.Items.Add("AM");
            this.ddlSunEndTime.Items.Add("PM");
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

    protected void btnAddHolidays_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.lstBoxHoliday.Items.Add(this.txtHoliday.Text.ToString());
            this.lblMsg.Text = "";
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearControl();
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._deleteOpeation = new Bill_Sys_DeleteBO();
        Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
        string text = "";
        try
        {
            for (int i = 0; i < this.grdRoomList.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdRoomList.Items[i].FindControl("chkDelete");
                if (box.Checked && !this._deleteOpeation.deleteRecord("SP_MST_ROOM", "@SZ_ROOM_ID", this.grdRoomList.Items[i].Cells[1].Text))
                {
                    days.DeleteRoomDetails(this.grdRoomList.Items[i].Cells[1].Text);
                    days.DeleteHolidayDetails(this.grdRoomList.Items[i].Cells[1].Text);
                    if (text == "")
                    {
                        text = this.grdRoomList.Items[i].Cells[2].Text;
                    }
                    else
                    {
                        text = text + " , " + this.grdRoomList.Items[i].Cells[2].Text;
                    }
                }
            }
            if (text != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Room " + text + "  exists.'); ", true);
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Room deleted successfully ...";
            }
            this.BindGrid();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        decimal num = 0M;
        decimal num2 = 0M;
        if (this.ddlTime.SelectedValue == "PM")
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0.00" + this.ddlMinutes.SelectedValue);
            num += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0.00" + this.ddlMinutes.SelectedValue);
        }
        if (this.ddlEndTime.SelectedValue == "PM")
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0.00" + this.ddlEndMinutes.SelectedValue);
            num2 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0.00" + this.ddlEndMinutes.SelectedValue);
        }
        if (num < num2)
        {
            //Change added on 28/05/2015 to not save the room name if it already exist
            int iFlag = 0;
            for (int i = 0; i < grdRoomList.Items.Count ; i++)
            {
                if (txtRoomName.Text.Trim() == grdRoomList.Items[i].Cells[2].Text.ToString().Trim())
                {
                    iFlag = 1;
                }
            }
            if (iFlag == 0)
            {
                this.txtStartTime.Text = num.ToString();
                this.txtEndTime.Text = num2.ToString();
                this._saveOperation = new SaveOperation();
                try
                {
                    this._saveOperation.WebPage = this.Page;
                    this._saveOperation.Xml_File = "room.xml";
                    this._saveOperation.SaveMethod();
                    this.saveRoomDays();
                    this.saveHolidayDetails();
                    this.BindGrid();
                    this.ClearControl();
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = "Room Saved Successfully ...!";
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
                
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Room With Same Name Already Exists !";
            }
        }
        else
        {
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Please select valid time!";
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
        decimal num = 0M;
        decimal num2 = 0M;
        if (this.ddlTime.SelectedValue == "PM")
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
            num += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
        }
        if (this.ddlEndTime.SelectedValue == "PM")
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
            num2 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
        }
        if (num < num2)
        {
            this.txtStartTime.Text = num.ToString();
            this.txtEndTime.Text = num2.ToString();
            this._editOperation = new EditOperation();
            try
            {
                this._editOperation.WebPage=this.Page;
                this._editOperation.Xml_File="room.xml";
                this._editOperation.Primary_Value=this.Session["RoomID"].ToString();
                this._editOperation.UpdateMethod();
                Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
                days.DeleteRoomDetails(this.Session["RoomID"].ToString());
                days.DeleteHolidayDetails(this.Session["RoomID"].ToString());
                this.blnEdit = true;
                this.saveRoomDays();
                this.saveHolidayDetails();
                this.BindGrid();
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Room Updated Successfully ...!";
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
            
        }
        else
        {
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Please select valid time!";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtRoomName.Text = "";
            txtRoomName.Enabled = true;
            this.extddlProCodeGroup.Text="NA";
            this.txtEndTime.Text = "";
            this.txtStartTime.Text = "";
            this.ddlEndHours.SelectedValue = "00";
            this.ddlEndMinutes.SelectedValue = "00";
            this.ddlHours.SelectedValue = "00";
            this.ddlMinutes.SelectedValue = "00";
            this.ddlTime.SelectedValue = "AM";
            this.ddlEndTime.SelectedValue = "AM";
            this.ddlTuesEndHours.SelectedValue = "00";
            this.ddlTuesEndMinutes.SelectedValue = "00";
            this.ddlTuesHours.SelectedValue = "00";
            this.ddlTuesMinutes.SelectedValue = "00";
            this.ddlTuesTime.SelectedValue = "AM";
            this.ddlTuesEndTime.SelectedValue = "AM";
            this.ddlWednEndHours.SelectedValue = "00";
            this.ddlWednEndMinutes.SelectedValue = "00";
            this.ddlWednHours.SelectedValue = "00";
            this.ddlWednMinutes.SelectedValue = "00";
            this.ddlWednTime.SelectedValue = "AM";
            this.ddlWednEndTime.SelectedValue = "AM";
            this.ddlThusEndHours.SelectedValue = "00";
            this.ddlThusEndMinutes.SelectedValue = "00";
            this.ddlThusHours.SelectedValue = "00";
            this.ddlThusMinutes.SelectedValue = "00";
            this.ddlThusTime.SelectedValue = "AM";
            this.ddlThusEndTime.SelectedValue = "AM";
            this.ddlFridEndHours.SelectedValue = "00";
            this.ddlFridEndMinutes.SelectedValue = "00";
            this.ddlFridHours.SelectedValue = "00";
            this.ddlFridMinutes.SelectedValue = "00";
            this.ddlFridTime.SelectedValue = "AM";
            this.ddlFridEndTime.SelectedValue = "AM";
            this.ddlSatEndHours.SelectedValue = "00";
            this.ddlSatEndMinutes.SelectedValue = "00";
            this.ddlSatHours.SelectedValue = "00";
            this.ddlSatMinutes.SelectedValue = "00";
            this.ddlSatTime.SelectedValue = "AM";
            this.ddlSatEndTime.SelectedValue = "AM";
            this.ddlSunEndHours.SelectedValue = "00";
            this.ddlSunEndMinutes.SelectedValue = "00";
            this.ddlSunHours.SelectedValue = "00";
            this.ddlSunMinutes.SelectedValue = "00";
            this.ddlSunTime.SelectedValue = "AM";
            this.ddlSunEndTime.SelectedValue = "AM";
            this.txtHoliday.Text = "";
            this.lstBoxHoliday.Items.Clear();
            this.btnUpdate.Enabled = false;
            this.btnSave.Enabled = true;
            this.lblMsg.Visible = false;
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

    public void GetRoomHolidaysDetails(string strRoomID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable table = new DataTable();
            Bill_Sys_RoomDays days = new Bill_Sys_RoomDays();
            days.GetRoomDaysDetails(strRoomID, ref table);
            if (table.Rows.Count > 0)
            {
                if (table.Rows[1][3].ToString() != "")
                {
                    string str = table.Rows[1][3].ToString();
                    int num = Convert.ToInt32(str.Substring(0, str.IndexOf(".")));
                    if (num > 12)
                    {
                        num -= 12;
                        this.ddlTuesHours.SelectedValue = num.ToString().PadLeft(2, '0');
                        this.ddlTuesTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlTuesHours.SelectedValue = num.ToString().PadLeft(2, '0');
                        this.ddlTuesTime.SelectedValue = "AM";
                    }
                    this.ddlTuesMinutes.SelectedValue = str.Substring(str.IndexOf(".") + 1, str.Length - (str.IndexOf(".") + 1));
                }
                if (table.Rows[1][4].ToString() != "")
                {
                    string str2 = table.Rows[1][4].ToString();
                    int num2 = Convert.ToInt32(str2.Substring(0, str2.IndexOf(".")));
                    if (num2 > 12)
                    {
                        num2 -= 12;
                        this.ddlTuesEndHours.SelectedValue = num2.ToString().PadLeft(2, '0');
                        this.ddlTuesEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlTuesEndHours.SelectedValue = num2.ToString().PadLeft(2, '0');
                        this.ddlTuesEndTime.SelectedValue = "AM";
                    }
                    this.ddlTuesEndMinutes.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, str2.Length - (str2.IndexOf(".") + 1));
                }
                if (table.Rows[2][3].ToString() != "")
                {
                    string str3 = table.Rows[2][3].ToString();
                    int num3 = Convert.ToInt32(str3.Substring(0, str3.IndexOf(".")));
                    if (num3 > 12)
                    {
                        num3 -= 12;
                        this.ddlWednHours.SelectedValue = num3.ToString().PadLeft(2, '0');
                        this.ddlWednTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlWednHours.SelectedValue = num3.ToString().PadLeft(2, '0');
                        this.ddlWednTime.SelectedValue = "AM";
                    }
                    this.ddlWednMinutes.SelectedValue = str3.Substring(str3.IndexOf(".") + 1, str3.Length - (str3.IndexOf(".") + 1));
                }
                if (table.Rows[2][4].ToString() != "")
                {
                    string str4 = table.Rows[2][4].ToString();
                    int num4 = Convert.ToInt32(str4.Substring(0, str4.IndexOf(".")));
                    if (num4 > 12)
                    {
                        num4 -= 12;
                        this.ddlWednEndHours.SelectedValue = num4.ToString().PadLeft(2, '0');
                        this.ddlWednEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlWednEndHours.SelectedValue = num4.ToString().PadLeft(2, '0');
                        this.ddlWednEndTime.SelectedValue = "AM";
                    }
                    this.ddlWednEndMinutes.SelectedValue = str4.Substring(str4.IndexOf(".") + 1, str4.Length - (str4.IndexOf(".") + 1));
                }
                if (table.Rows[3][3].ToString() != "")
                {
                    string str5 = table.Rows[3][3].ToString();
                    int num5 = Convert.ToInt32(str5.Substring(0, str5.IndexOf(".")));
                    if (num5 > 12)
                    {
                        num5 -= 12;
                        this.ddlThusHours.SelectedValue = num5.ToString().PadLeft(2, '0');
                        this.ddlThusTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlThusHours.SelectedValue = num5.ToString().PadLeft(2, '0');
                        this.ddlThusTime.SelectedValue = "AM";
                    }
                    this.ddlThusMinutes.SelectedValue = str5.Substring(str5.IndexOf(".") + 1, str5.Length - (str5.IndexOf(".") + 1));
                }
                if (table.Rows[3][4].ToString() != "")
                {
                    string str6 = table.Rows[3][4].ToString();
                    int num6 = Convert.ToInt32(str6.Substring(0, str6.IndexOf(".")));
                    if (num6 > 12)
                    {
                        num6 -= 12;
                        this.ddlThusEndHours.SelectedValue = num6.ToString().PadLeft(2, '0');
                        this.ddlThusEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlThusEndHours.SelectedValue = num6.ToString().PadLeft(2, '0');
                        this.ddlThusEndTime.SelectedValue = "AM";
                    }
                    this.ddlThusEndMinutes.SelectedValue = str6.Substring(str6.IndexOf(".") + 1, str6.Length - (str6.IndexOf(".") + 1));
                }
                if (table.Rows[4][3].ToString() != "")
                {
                    string str7 = table.Rows[4][3].ToString();
                    int num7 = Convert.ToInt32(str7.Substring(0, str7.IndexOf(".")));
                    if (num7 > 12)
                    {
                        num7 -= 12;
                        this.ddlFridHours.SelectedValue = num7.ToString().PadLeft(2, '0');
                        this.ddlFridTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlFridHours.SelectedValue = num7.ToString().PadLeft(2, '0');
                        this.ddlFridTime.SelectedValue = "AM";
                    }
                    this.ddlFridMinutes.SelectedValue = str7.Substring(str7.IndexOf(".") + 1, str7.Length - (str7.IndexOf(".") + 1));
                }
                if (table.Rows[4][4].ToString() != "")
                {
                    string str8 = table.Rows[4][4].ToString();
                    int num8 = Convert.ToInt32(str8.Substring(0, str8.IndexOf(".")));
                    if (num8 > 12)
                    {
                        num8 -= 12;
                        this.ddlFridEndHours.SelectedValue = num8.ToString().PadLeft(2, '0');
                        this.ddlFridEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlFridEndHours.SelectedValue = num8.ToString().PadLeft(2, '0');
                        this.ddlFridEndTime.SelectedValue = "AM";
                    }
                    this.ddlFridEndMinutes.SelectedValue = str8.Substring(str8.IndexOf(".") + 1, str8.Length - (str8.IndexOf(".") + 1));
                }
                if (table.Rows[5][3].ToString() != "")
                {
                    string str9 = table.Rows[5][3].ToString();
                    int num9 = Convert.ToInt32(str9.Substring(0, str9.IndexOf(".")));
                    if (num9 > 12)
                    {
                        num9 -= 12;
                        this.ddlSatHours.SelectedValue = num9.ToString().PadLeft(2, '0');
                        this.ddlSatTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlSatHours.SelectedValue = num9.ToString().PadLeft(2, '0');
                        this.ddlSatTime.SelectedValue = "AM";
                    }
                    this.ddlSatMinutes.SelectedValue = str9.Substring(str9.IndexOf(".") + 1, str9.Length - (str9.IndexOf(".") + 1));
                }
                if (table.Rows[5][4].ToString() != "")
                {
                    string str10 = table.Rows[5][4].ToString();
                    int num10 = Convert.ToInt32(str10.Substring(0, str10.IndexOf(".")));
                    if (num10 > 12)
                    {
                        num10 -= 12;
                        this.ddlSatEndHours.SelectedValue = num10.ToString().PadLeft(2, '0');
                        this.ddlSatEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlSatEndHours.SelectedValue = num10.ToString().PadLeft(2, '0');
                        this.ddlSatEndTime.SelectedValue = "AM";
                    }
                    this.ddlSatEndMinutes.SelectedValue = str10.Substring(str10.IndexOf(".") + 1, str10.Length - (str10.IndexOf(".") + 1));
                }
                if (table.Rows[6][3].ToString() != "")
                {
                    string str11 = table.Rows[6][3].ToString();
                    int num11 = Convert.ToInt32(str11.Substring(0, str11.IndexOf(".")));
                    if (num11 > 12)
                    {
                        num11 -= 12;
                        this.ddlSunHours.SelectedValue = num11.ToString().PadLeft(2, '0');
                        this.ddlSunTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlSunHours.SelectedValue = num11.ToString().PadLeft(2, '0');
                        this.ddlSunTime.SelectedValue = "AM";
                    }
                    this.ddlSunMinutes.SelectedValue = str11.Substring(str11.IndexOf(".") + 1, str11.Length - (str11.IndexOf(".") + 1));
                }
                if (table.Rows[6][4].ToString() != "")
                {
                    string str12 = table.Rows[6][4].ToString();
                    int num12 = Convert.ToInt32(str12.Substring(0, str12.IndexOf(".")));
                    if (num12 > 12)
                    {
                        num12 -= 12;
                        this.ddlSunEndHours.SelectedValue = num12.ToString().PadLeft(2, '0');
                        this.ddlSunEndTime.SelectedValue = "PM";
                    }
                    else
                    {
                        this.ddlSunEndHours.SelectedValue = num12.ToString().PadLeft(2, '0');
                        this.ddlSunEndTime.SelectedValue = "AM";
                    }
                    this.ddlSunEndMinutes.SelectedValue = str12.Substring(str12.IndexOf(".") + 1, str12.Length - (str12.IndexOf(".") + 1));
                }
                DataTable table2 = new DataTable();
                days.GetHolidayDetails(strRoomID, ref table2);
                this.lstBoxHoliday.DataSource = table2;
                this.lstBoxHoliday.DataTextField = "HOLIDAYS_DATE";
                this.lstBoxHoliday.DataValueField = "HOLIDAY_ID";
                this.lstBoxHoliday.DataBind();
            }
            else
            {
                this.ddlEndHours.SelectedValue = "00";
                this.ddlEndMinutes.SelectedValue = "00";
                this.ddlHours.SelectedValue = "00";
                this.ddlMinutes.SelectedValue = "00";
                this.ddlTime.SelectedValue = "AM";
                this.ddlEndTime.SelectedValue = "AM";
                this.ddlTuesEndHours.SelectedValue = "00";
                this.ddlTuesEndMinutes.SelectedValue = "00";
                this.ddlTuesHours.SelectedValue = "00";
                this.ddlTuesMinutes.SelectedValue = "00";
                this.ddlTuesTime.SelectedValue = "AM";
                this.ddlTuesEndTime.SelectedValue = "AM";
                this.ddlWednEndHours.SelectedValue = "00";
                this.ddlWednEndMinutes.SelectedValue = "00";
                this.ddlWednHours.SelectedValue = "00";
                this.ddlWednMinutes.SelectedValue = "00";
                this.ddlWednTime.SelectedValue = "AM";
                this.ddlWednEndTime.SelectedValue = "AM";
                this.ddlThusEndHours.SelectedValue = "00";
                this.ddlThusEndMinutes.SelectedValue = "00";
                this.ddlThusHours.SelectedValue = "00";
                this.ddlThusMinutes.SelectedValue = "00";
                this.ddlThusTime.SelectedValue = "AM";
                this.ddlThusEndTime.SelectedValue = "AM";
                this.ddlFridEndHours.SelectedValue = "00";
                this.ddlFridEndMinutes.SelectedValue = "00";
                this.ddlFridHours.SelectedValue = "00";
                this.ddlFridMinutes.SelectedValue = "00";
                this.ddlFridTime.SelectedValue = "AM";
                this.ddlFridEndTime.SelectedValue = "AM";
                this.ddlSatEndHours.SelectedValue = "00";
                this.ddlSatEndMinutes.SelectedValue = "00";
                this.ddlSatHours.SelectedValue = "00";
                this.ddlSatMinutes.SelectedValue = "00";
                this.ddlSatTime.SelectedValue = "AM";
                this.ddlSatEndTime.SelectedValue = "AM";
                this.ddlSunEndHours.SelectedValue = "00";
                this.ddlSunEndMinutes.SelectedValue = "00";
                this.ddlSunHours.SelectedValue = "00";
                this.ddlSunMinutes.SelectedValue = "00";
                this.ddlSunTime.SelectedValue = "AM";
                this.ddlSunEndTime.SelectedValue = "AM";
                this.txtHoliday.Text = "";
                this.lstBoxHoliday.Items.Clear();
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

    protected void grdRoomList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }

    protected void grdRoomList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdRoomList.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
            this.lblMsg.Visible = false;
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

    protected void grdRoomList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["RoomID"] = this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[1].Text;
            if (this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                this.txtRoomName.Text = this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[2].Text;
                txtRoomName.Enabled = false;
            }
            if (this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[4].Text != "&nbsp;")
            {
                this.extddlProCodeGroup.Text=this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[4].Text;
            }
            if (this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[8].Text != "&nbsp;")
            {
                string text = this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[8].Text;
                int num = Convert.ToInt32(text.Substring(0, text.IndexOf(".")));
                if (num > 12)
                {
                    num -= 12;
                    this.ddlHours.SelectedValue = num.ToString().PadLeft(2, '0');
                    this.ddlTime.SelectedValue = "PM";
                }
                else
                {
                    this.ddlHours.SelectedValue = num.ToString().PadLeft(2, '0');
                    this.ddlTime.SelectedValue = "AM";
                }
                this.ddlMinutes.SelectedValue = text.Substring(text.IndexOf(".") + 1, text.Length - (text.IndexOf(".") + 1));
            }
            if (this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[9].Text != "&nbsp;")
            {
                string str2 = this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[9].Text;
                int num2 = Convert.ToInt32(str2.Substring(0, str2.IndexOf(".")));
                if (num2 > 12)
                {
                    num2 -= 12;
                    this.ddlEndHours.SelectedValue = num2.ToString().PadLeft(2, '0');
                    this.ddlEndTime.SelectedValue = "PM";
                }
                else
                {
                    this.ddlEndHours.SelectedValue = num2.ToString().PadLeft(2, '0');
                    this.ddlEndTime.SelectedValue = "AM";
                }
                this.ddlEndMinutes.SelectedValue = str2.Substring(str2.IndexOf(".") + 1, str2.Length - (str2.IndexOf(".") + 1));
            }
            this.GetRoomHolidaysDetails(this.grdRoomList.Items[this.grdRoomList.SelectedIndex].Cells[1].Text);
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.lblMsg.Visible = false;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.btnSave.Attributes.Add("onclick", "return formValidator('frmRoom','txtRoomName');");
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmRoom','txtRoomName');");
            if (!base.IsPostBack)
            {
                this.Session["RoomID"] = "";
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlProCodeGroup.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.BindGrid();
                this.btnUpdate.Enabled = false;
                this.BindTimeControl();
            }
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            if (this._deleteOpeation.checkForDelete(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                this.btnDelete.Visible = false;
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

    public void saveHolidayDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set = new DataSet();
        try
        {
            string roomLatestID;
            DataTable table = set.Tables.Add("MST_HOLIDAYS");
            table.Columns.Add("HOLIDAYS_DATE", typeof(DateTime));
            table.Columns.Add("ROOM_ID", typeof(string));
            table.Columns.Add("DAYS", typeof(string));
            if (this.blnEdit)
            {
                roomLatestID = this.Session["RoomID"].ToString();
                this.blnEdit = false;
            }
            else
            {
                roomLatestID = new Bill_Sys_RoomDays().GetRoomLatestID();
            }
            DataRow row = table.NewRow();
            foreach (ListItem item in this.lstBoxHoliday.Items)
            {
                row = table.NewRow();
                row["HOLIDAYS_DATE"] = item.Text;
                row["ROOM_ID"] = roomLatestID;
                string str2 = Convert.ToDateTime(item.Text).DayOfWeek.ToString();
                row["DAYS"] = str2;
                table.Rows.Add(row);
            }
            new Bill_Sys_RoomDays().saveHolidayInformation(table);
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

    public void saveRoomDays()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet set = new DataSet();
        DataTable table = set.Tables.Add("MST_ROOM_DAYS");
        try
        {
            string roomLatestID;
            table.Columns.Add("RoomID", typeof(string));
            table.Columns.Add("Days", typeof(string));
            table.Columns.Add("StartTime", typeof(string));
            table.Columns.Add("EndTime", typeof(string));
            table.Columns.Add("EffectiveTo", typeof(DateTime));
            table.Columns.Add("EffectiveFrom", typeof(DateTime));
            if (this.blnEdit)
            {
                roomLatestID = this.Session["RoomID"].ToString();
            }
            else
            {
                roomLatestID = new Bill_Sys_RoomDays().GetRoomLatestID();
            }
            DataRow row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblMon.Text;
            decimal num = 0M;
            decimal num2 = 0M;
            if (this.ddlTime.SelectedValue == "PM")
            {
                num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
                num += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
            }
            if (this.ddlEndTime.SelectedValue == "PM")
            {
                num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
                num2 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
            }
            if (num < num2)
            {
                row["StartTime"] = num.ToString();
                row["EndTime"] = num2.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblTus.Text;
            decimal num3 = 0M;
            decimal num4 = 0M;
            if (this.ddlTuesTime.SelectedValue == "PM")
            {
                num3 = Convert.ToDecimal(this.ddlTuesHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesMinutes.SelectedValue);
                num3 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num3 = Convert.ToDecimal(this.ddlTuesHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesMinutes.SelectedValue);
            }
            if (this.ddlTuesEndTime.SelectedValue == "PM")
            {
                num4 = Convert.ToDecimal(this.ddlTuesEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesEndMinutes.SelectedValue);
                num4 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num4 = Convert.ToDecimal(this.ddlTuesEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesEndMinutes.SelectedValue);
            }
            if (num3 < num4)
            {
                row["StartTime"] = num3.ToString();
                row["EndTime"] = num4.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblWen.Text;
            decimal num5 = 0M;
            decimal num6 = 0M;
            if (this.ddlWednTime.SelectedValue == "PM")
            {
                num5 = Convert.ToDecimal(this.ddlWednHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednMinutes.SelectedValue);
                num5 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num5 = Convert.ToDecimal(this.ddlWednHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednMinutes.SelectedValue);
            }
            if (this.ddlWednEndTime.SelectedValue == "PM")
            {
                num6 = Convert.ToDecimal(this.ddlWednEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednEndMinutes.SelectedValue);
                num6 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num6 = Convert.ToDecimal(this.ddlWednEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednEndMinutes.SelectedValue);
            }
            if (num5 < num6)
            {
                row["StartTime"] = num5.ToString();
                row["EndTime"] = num6.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblThus.Text;
            decimal num7 = 0M;
            decimal num8 = 0M;
            if (this.ddlThusTime.SelectedValue == "PM")
            {
                num7 = Convert.ToDecimal(this.ddlThusHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusMinutes.SelectedValue);
                num7 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num7 = Convert.ToDecimal(this.ddlThusHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusMinutes.SelectedValue);
            }
            if (this.ddlThusEndTime.SelectedValue == "PM")
            {
                num8 = Convert.ToDecimal(this.ddlThusEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusEndMinutes.SelectedValue);
                num8 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num8 = Convert.ToDecimal(this.ddlThusEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusEndMinutes.SelectedValue);
            }
            if (num7 < num8)
            {
                row["StartTime"] = num7.ToString();
                row["EndTime"] = num8.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblFri.Text;
            decimal num9 = 0M;
            decimal num10 = 0M;
            if (this.ddlFridTime.SelectedValue == "PM")
            {
                num9 = Convert.ToDecimal(this.ddlFridHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridMinutes.SelectedValue);
                num9 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num9 = Convert.ToDecimal(this.ddlFridHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridMinutes.SelectedValue);
            }
            if (this.ddlFridEndTime.SelectedValue == "PM")
            {
                num10 = Convert.ToDecimal(this.ddlFridEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridEndMinutes.SelectedValue);
                num10 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num10 = Convert.ToDecimal(this.ddlFridEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridEndMinutes.SelectedValue);
            }
            if (num9 < num10)
            {
                row["StartTime"] = num9.ToString();
                row["EndTime"] = num10.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblSat.Text;
            decimal num11 = 0M;
            decimal num12 = 0M;
            if (this.ddlSatTime.SelectedValue == "PM")
            {
                num11 = Convert.ToDecimal(this.ddlSatHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatMinutes.SelectedValue);
                num11 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num11 = Convert.ToDecimal(this.ddlSatHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatMinutes.SelectedValue);
            }
            if (this.ddlSatEndTime.SelectedValue == "PM")
            {
                num12 = Convert.ToDecimal(this.ddlSatEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatEndMinutes.SelectedValue);
                num12 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num12 = Convert.ToDecimal(this.ddlSatEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatEndMinutes.SelectedValue);
            }
            if (num11 < num12)
            {
                row["StartTime"] = num11.ToString();
                row["EndTime"] = num12.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            row = table.NewRow();
            row["RoomID"] = roomLatestID;
            row["Days"] = this.lblSun.Text;
            decimal num13 = 0M;
            decimal num14 = 0M;
            if (this.ddlSunTime.SelectedValue == "PM")
            {
                num13 = Convert.ToDecimal(this.ddlSunHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunMinutes.SelectedValue);
                num13 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num13 = Convert.ToDecimal(this.ddlSunHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunMinutes.SelectedValue);
            }
            if (this.ddlSunEndTime.SelectedValue == "PM")
            {
                num14 = Convert.ToDecimal(this.ddlSunEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunEndMinutes.SelectedValue);
                num14 += Convert.ToDecimal((double)12.0);
            }
            else
            {
                num14 = Convert.ToDecimal(this.ddlSunEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunEndMinutes.SelectedValue);
            }
            if (num13 < num14)
            {
                row["StartTime"] = num13.ToString();
                row["EndTime"] = num14.ToString();
            }
            row["EffectiveTo"] = DateTime.Now;
            row["EffectiveFrom"] = DateTime.Now;
            table.Rows.Add(row);
            new Bill_Sys_RoomDays().saveRoomDaysInformation(table);
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

    public bool Validation()
    {
        decimal num = 0M;
        decimal num2 = 0M;
        if (this.ddlTime.SelectedValue == "PM")
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
            num += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num = Convert.ToDecimal(this.ddlHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlMinutes.SelectedValue);
        }
        if (this.ddlEndTime.SelectedValue == "PM")
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
            num2 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num2 = Convert.ToDecimal(this.ddlEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlEndMinutes.SelectedValue);
        }
        if (num < num2)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Monday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num3 = 0M;
        decimal num4 = 0M;
        if (this.ddlThusTime.SelectedValue == "PM")
        {
            num3 = Convert.ToDecimal(this.ddlTuesHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesMinutes.SelectedValue);
            num3 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num3 = Convert.ToDecimal(this.ddlTuesHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesMinutes.SelectedValue);
        }
        if (this.ddlTuesEndTime.SelectedValue == "PM")
        {
            num4 = Convert.ToDecimal(this.ddlTuesEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesEndMinutes.SelectedValue);
            num4 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num4 = Convert.ToDecimal(this.ddlTuesEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlTuesEndMinutes.SelectedValue);
        }
        if (num3 < num4)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Tuesday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num5 = 0M;
        decimal num6 = 0M;
        if (this.ddlWednTime.SelectedValue == "PM")
        {
            num5 = Convert.ToDecimal(this.ddlWednHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednMinutes.SelectedValue);
            num5 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num5 = Convert.ToDecimal(this.ddlWednHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednMinutes.SelectedValue);
        }
        if (this.ddlWednEndTime.SelectedValue == "PM")
        {
            num6 = Convert.ToDecimal(this.ddlWednEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednEndMinutes.SelectedValue);
            num6 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num6 = Convert.ToDecimal(this.ddlWednEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlWednEndMinutes.SelectedValue);
        }
        if (num5 < num6)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Wednesday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num7 = 0M;
        decimal num8 = 0M;
        if (this.ddlThusTime.SelectedValue == "PM")
        {
            num7 = Convert.ToDecimal(this.ddlThusHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusMinutes.SelectedValue);
            num7 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num7 = Convert.ToDecimal(this.ddlThusHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusMinutes.SelectedValue);
        }
        if (this.ddlThusEndTime.SelectedValue == "PM")
        {
            num8 = Convert.ToDecimal(this.ddlThusEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusEndMinutes.SelectedValue);
            num8 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num8 = Convert.ToDecimal(this.ddlThusEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlThusEndMinutes.SelectedValue);
        }
        if (num7 < num8)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Thusrday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num9 = 0M;
        decimal num10 = 0M;
        if (this.ddlFridTime.SelectedValue == "PM")
        {
            num9 = Convert.ToDecimal(this.ddlFridHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridMinutes.SelectedValue);
            num9 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num9 = Convert.ToDecimal(this.ddlFridHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridMinutes.SelectedValue);
        }
        if (this.ddlFridEndTime.SelectedValue == "PM")
        {
            num10 = Convert.ToDecimal(this.ddlFridEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridEndMinutes.SelectedValue);
            num10 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num10 = Convert.ToDecimal(this.ddlFridEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlFridEndMinutes.SelectedValue);
        }
        if (num9 < num10)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Friday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num11 = 0M;
        decimal num12 = 0M;
        if (this.ddlSatTime.SelectedValue == "PM")
        {
            num11 = Convert.ToDecimal(this.ddlSatHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatMinutes.SelectedValue);
            num11 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num11 = Convert.ToDecimal(this.ddlSatHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatMinutes.SelectedValue);
        }
        if (this.ddlSatEndTime.SelectedValue == "PM")
        {
            num12 = Convert.ToDecimal(this.ddlSatEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatEndMinutes.SelectedValue);
            num12 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num12 = Convert.ToDecimal(this.ddlSatEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSatEndMinutes.SelectedValue);
        }
        if (num11 < num12)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Saturday";
            this.lblMsg.Visible = true;
            return false;
        }
        decimal num13 = 0M;
        decimal num14 = 0M;
        if (this.ddlSunTime.SelectedValue == "PM")
        {
            num13 = Convert.ToDecimal(this.ddlSunHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunMinutes.SelectedValue);
            num13 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num13 = Convert.ToDecimal(this.ddlSunHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunMinutes.SelectedValue);
        }
        if (this.ddlSunEndTime.SelectedValue == "PM")
        {
            num14 = Convert.ToDecimal(this.ddlSunEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunEndMinutes.SelectedValue);
            num14 += Convert.ToDecimal((double)12.0);
        }
        else
        {
            num14 = Convert.ToDecimal(this.ddlSunEndHours.SelectedValue + ".00") + Convert.ToDecimal("0." + this.ddlSunEndMinutes.SelectedValue);
        }
        if (num13 < num14)
        {
            this.lblMsg.Text = "";
        }
        else
        {
            this.lblMsg.Text = "Please select valid time! Sunday";
            this.lblMsg.Visible = true;
            return false;
        }
        if (this.lblMsg.Text == "")
        {
            this.lblMsg.Visible = false;
            return true;
        }
        this.lblMsg.Visible = true;
        return false;
    }

    
}
