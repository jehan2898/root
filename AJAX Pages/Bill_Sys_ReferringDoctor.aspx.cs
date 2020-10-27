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

public partial class AJAX_Pages_Bill_Sys_ReferringDoctor : PageBase
{

    
    private Bill_Sys_DeleteBO _deleteOpeation;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserId.Text=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        btnSave.Attributes.Add("onclick", "return validate();");
        btnUpdate.Attributes.Add("onclick", "return validate();");
      //  btnClear.Attributes.Add("onclick", "return clear();");
        btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        //btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
        Bill_Sys_BillingCompanyDetails_BO objOffID = new Bill_Sys_BillingCompanyDetails_BO();
        string sz_Off_Id = objOffID.GetRefDocID(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
        if (sz_Off_Id != "")
        {
            extddlOffice.Text = sz_Off_Id;
            extddlOffice.Enabled = false;
        }
            extddlOffice.Flag_ID = txtCompanyID.Text;
        
        extddlDoctorType.Flag_ID = txtCompanyID.Text;
        extddlProvider.Flag_ID = txtCompanyID.Text;
        

        this.con.SourceGrid = grdRffDoc;
        this.txtSearchBox.SourceGrid = grdRffDoc;
        this.grdRffDoc.Page = this.Page;
        this.grdRffDoc.PageNumberList = this.con;
        
        if (!IsPostBack)
        {
            hdCheck.Value = "0";
            if (Request.QueryString["ProviderId"] != null)
            {
                extddlProvider.Text = Request.QueryString["ProviderId"].ToString().Trim();
                extddlProvider.Enabled = false;
            }
           
            btnUpdate.Enabled = false;
            grdRffDoc.XGridBindSearch();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
        int i = 0;
       
        
            string szReturn = obj.CheckAssihnNo(txtCompanyID.Text, txtAssignNumber.Text);
            if (szReturn != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "Check();", true);
            }
            else
            {
                hdVlaue.Value = "1";
                test_click(sender, e);
               
            }
        
       
    }
    protected void test_click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
        int i = 0;

      //  if (hdVlaue.Value == "1")
        {
            hdVlaue.Value = "0";
            try
            {

                i = obj.InsertDoctorMaster(txtDoctorName.Text, txtAssignNumber.Text, txtLicenseNumber.Text, extddlOffice.Text.ToString(), extddlDoctorType.Text.ToString(), txtCompanyID.Text);
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

            if (i > 0)
            {
                grdRffDoc.XGridBindSearch();
                usrMessage.PutMessage("Doctor Saved Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Insertion Failed");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdRffDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            txtDoctorName.Text=grdRffDoc.Rows[index].Cells[2].Text.ToString();
            if (grdRffDoc.Rows[index].Cells[5].Text.ToString() != "&nbsp;")
            {
                txtLicenseNumber.Text = grdRffDoc.Rows[index].Cells[5].Text.ToString();
            }
            else
            {
                txtLicenseNumber.Text = "";
            }

            if (grdRffDoc.Rows[index].Cells[7].Text.ToString() != "&nbsp;")
            {
                txtAssignNumber.Text = grdRffDoc.Rows[index].Cells[7].Text.ToString();
            }
            else
            {
                txtAssignNumber.Text = "";
            }

            
            
           
            extddlDoctorType.Text = grdRffDoc.DataKeys[index][2].ToString();
            extddlOffice.Text = grdRffDoc.DataKeys[index][1].ToString();
            Session["DOCID"] = grdRffDoc.DataKeys[index][0].ToString();
            Session["ASSIGN"] = grdRffDoc.Rows[index].Cells[7].Text.ToString();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            



        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
        if (Session["ASSIGN"] != null)
        {
            if (Session["ASSIGN"].ToString() != txtAssignNumber.Text)
            {

                string szReturn = obj.CheckAssihnNo(txtCompanyID.Text, txtAssignNumber.Text);
                if (szReturn != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "CheckUpdate();", true);
                }
                else
                {
                    
                    CheckUpdate_click(sender, e);

                }
            }
            else
            {
                CheckUpdate_click(sender, e);

            }
        }
       
    }
    //protected void btnClear_Click(object sender, EventArgs e)
    //{

    //}
    protected void CheckUpdate_click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (Session["DOCID"] != null)
        {
            Bill_Sys_DoctorBO obj = new Bill_Sys_DoctorBO();
            int i = 0;
            try
            {
                i = obj.UpdateDoctorMaster(txtDoctorName.Text, txtAssignNumber.Text, txtLicenseNumber.Text, extddlOffice.Text.ToString(), extddlDoctorType.Text.ToString(), txtCompanyID.Text, Session["DOCID"].ToString());
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

            if (i > 0)
            {
                grdRffDoc.XGridBindSearch();
                usrMessage.PutMessage("Doctor Updated Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Updation Failed");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
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
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfDoctor = "";

            try
            {

                for (int i = 0; i < grdRffDoc.Rows.Count; i++)
                {
                    CheckBox chkDelete1 = (CheckBox)grdRffDoc.Rows[i].FindControl("ChkDelete");

                    if (chkDelete1.Checked)
                    {
                        if (!_deleteOpeation.deleteRecord("SP_MST_DOCTOR", "@SZ_DOCTOR_ID", grdRffDoc.DataKeys[i][0].ToString(), "REF_DELETE"))
                        {
                            if (szListOfDoctor == "")
                            {
                                szListOfDoctor = grdRffDoc.Rows[i].Cells[2].Text.ToString();
                            }
                            else
                            {
                                szListOfDoctor = szListOfDoctor + " , " + grdRffDoc.Rows[i].Cells[2].Text.ToString();
                            }
                        }
                    }

                }
            if (szListOfDoctor != "")
            {
                //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Doctor " + szListOfDoctor + "  is exists.'); ", true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Records for Doctor " + szListOfDoctor + "  is exists.');", true);
               
            }
                grdRffDoc.XGridBindSearch();
                usrMessage.PutMessage("Doctor deleted successfully ...");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdRffDoc.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }



}
