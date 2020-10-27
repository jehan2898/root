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

public partial class AJAX_Pages_Bill_Sys_TransportationPopUP : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btndelete.Attributes.Add("onclick", "return confirm_deletetransport();");
        btnsave.Attributes.Add("onclick", "return val_CheckControls();");
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["caseid"] != null && Request.QueryString["cmpid"] != null)
                {
                    txtCaseID.Text = Request.QueryString["caseid"].ToString();
                    txtCompanyID.Text = Request.QueryString["cmpid"].ToString();
                }

                BindTimeControl();
                BillSearchDAO _BillSearchDAO = new BillSearchDAO();
                DataSet ds = new DataSet();
                ds = _BillSearchDAO.getTransportinfo(txtCaseID.Text, txtCompanyID.Text);
                grdTransport.DataSource = ds;
                grdTransport.DataBind();
                extddlTransport.Flag_ID = txtCompanyID.Text;
            }
            catch (Exception ex)
            {
                this.Session["SendPatientToDoctor"] = false;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        BillSearchDAO _BillSearchDAO = new BillSearchDAO();
        try
        {
            _BillSearchDAO.GetInsertTransport(extddlTransport.Selected_Text, extddlTransport.Text, txtCompanyID.Text, txtCaseID.Text, txtFromDate.Text, ddlHours.SelectedItem.ToString(), ddlMinutes.SelectedItem.ToString(), ddlTime.SelectedItem.ToString());
            MessageControl1.PutMessage("Save Successfully ...!");
            MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl1.Show();
            ClearControl_Popup();

            DataSet ds = new DataSet();
            ds = _BillSearchDAO.getTransportinfo(txtCaseID.Text, txtCompanyID.Text);
            grdTransport.DataSource = ds;
            grdTransport.DataBind();
        }
        catch (Exception ex)
        {
            this.Session["SendPatientToDoctor"] = false;
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
    private void BindTimeControl()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ddlHours.Items.Clear();
            ddlMinutes.Items.Clear();
            ddlTime.Items.Clear();
            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlHours.Items.Add(i.ToString());
                }
                else
                {
                    ddlHours.Items.Add("0" + i.ToString());
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlMinutes.Items.Add(i.ToString());
                }
                else
                {
                    ddlMinutes.Items.Add("0" + i.ToString());
                }
            }
            ddlTime.Items.Add("AM");
            ddlTime.Items.Add("PM");
        }
        catch (Exception ex)
        {
            this.Session["SendPatientToDoctor"] = false;
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
    protected void btdelete_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrtransport = new ArrayList();
            for (int i = 0; i < grdTransport.Items.Count; i++)
            {
                CheckBox chktransport = (CheckBox)grdTransport.Items[i].FindControl("chkDelete");
                if (chktransport.Checked == true)
                {
                    string strtransportid = grdTransport.DataKeys[i].ToString();
                    arrtransport.Add(strtransportid);

                }
            }
            BillSearchDAO _objBillSearchDAO = new BillSearchDAO();
            _objBillSearchDAO.Delete_Trans_Data(arrtransport, txtCompanyID.Text);


            MessageControl1.PutMessage("Delete Successfully ...!");
            MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl1.Show();

            DataSet ds = new DataSet();
            ds = _objBillSearchDAO.getTransportinfo(txtCaseID.Text, txtCompanyID.Text);
            grdTransport.DataSource = ds;
            grdTransport.DataBind();
            ClearControl_Popup();
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

    public void ClearControl_Popup()
    {
        extddlTransport.Text = "---Select---";
        txtFromDate.Text = "";
        ddlHours.SelectedValue = "00";
        ddlMinutes.SelectedValue = "00";
        ddlTime.SelectedValue = "AM";
    }
}
