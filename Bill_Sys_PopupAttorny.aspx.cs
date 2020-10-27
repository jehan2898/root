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

public partial class Bill_Sys_PopupAttorny : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["edit"] != null)
                {
                    string attorneyid = Request.QueryString["edit"].ToString();
                    divrd.Visible = false;
                    divupdate.Visible = true;
                    Patient_TVBO _objAtrnyInfo = new Patient_TVBO();
                    DataSet _attornyDs = _objAtrnyInfo.GetAttornyInfo(attorneyid);
                    if (_attornyDs.Tables[0].Rows.Count > 0)
                    {
                        txtFirstname.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        txtLastname.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        txtCity.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        txtZip.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        txtPhone.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        txtFax.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        txtEmailID.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        txtState.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_STATE_ID"].ToString();
                        txtAddress.Text = _attornyDs.Tables[0].Rows[0]["SZ_ATTORNEY_ADDRESS"].ToString();

                    }
                    hdfid.Value = attorneyid;
                }
                else if (Session["AttornyID"].ToString() != null && Session["AttornyID"].ToString() != "")
                {
                    divrd.Visible = true;
                    divupdate.Visible = false;

                    string szAttornyID = Session["AttornyID"].ToString();
                    Patient_TVBO _objAtrnyInfo = new Patient_TVBO();
                    DataSet _attornyDs = _objAtrnyInfo.GetAttornyInfo(szAttornyID);

                    if (_attornyDs.Tables[0].Rows.Count > 0)
                    {
                        lblFirstName.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        lblLastName.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        lblCity.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        //    txtState.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        lblZip.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        lblPhoneNo.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        lblFax.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        lblEmailID.Text = _attornyDs.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        lblState.Text = _attornyDs.Tables[0].Rows[0]["SZ_STATE_NAME"].ToString();
                    }
                }
                else
                {
                    divrd.Visible = false;
                    divupdate.Visible = true;
                }

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
        

        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_PopupAttorny.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["connection_String"].ToString());
            SqlCommand command = new SqlCommand("sp_update_attorney", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@sz_attorney_id", hdfid.Value);
            command.Parameters.Add("@sz_address", txtAddress.Text);
            command.Parameters.Add("@sz_city", txtCity.Text);
            if (txtState.Text != "NA")
            {
                command.Parameters.Add("@sz_state_id", txtState.Text);
            }
            command.Parameters.Add("@sz_zip", txtZip.Text);
            command.Parameters.Add("@sz_email", txtEmailID.Text);
            command.Parameters.Add("@sz_phone", txtPhone.Text);
            command.Parameters.Add("@sz_fax", txtFax.Text);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command.ExecuteNonQuery();
            connection.Close();
            usrMessage.PutMessage("Update sucessfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
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
