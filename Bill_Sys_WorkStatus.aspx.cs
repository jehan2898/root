using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;

public partial class Bill_Sys_WorkStatus : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        

        //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtPatientID.Text = Session["PatientID"].ToString() ;
        txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString(); //"sas0000001";
        if (!IsPostBack )
        {
            chkPatientReturnToWorkWithoutLimitation.Attributes.Add("onclick", "checkvalidate_b();");
            chkPatientcannotReturn.Attributes.Add("onclick", "checkvalidate_a();");
            chkPatientReturnToWorkWithlimitation.Attributes.Add("onclick", "checkvalidate_c();");
            chklstPatientLimitationAllReason.Attributes.Add("onclick", "checkvalidate_b();");

            LoadData();
            if (chkPatientReturnToWorkWithlimitation.Checked)
            {
                LoadOtherInformation();
            }
            if (txtPatientMissedWorkDate.Text.ToString() == "1/1/1900")
            {
                txtPatientMissedWorkDate.Text = "";
            }
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_WorkStatus.aspx");
        }
        #endregion
    }

    protected void btnSaveAndGoToNext_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        WorkerTemplate _obj = new WorkerTemplate();
        try
        {
            if (rdlstPatientMissedWork.SelectedValue != "0")
                txtPatientMissedWorkDate.Text = "";


            if (_obj.PatientExistCheckForWC4("SP_MST_WORK_STATUS_NEW", Session["TEMPLATE_BILL_NO"].ToString()) == false)
            {
                
                loadReqData();

                if (rdlstPatientMissedWork.SelectedValue.ToString().Equals(""))
                {
                    txtPatientMissedWork.Text = "-1";
                }
                else
                {
                    txtPatientMissedWork.Text = rdlstPatientMissedWork.SelectedValue.ToString();
                }

                if (rdlstPatientCurrentlyWorking.SelectedValue.ToString().Equals(""))
                {
                    txtPatientCurrentlyWorking.Text = "-1";
                }
                else
                {
                    txtPatientCurrentlyWorking.Text = rdlstPatientCurrentlyWorking.SelectedValue.ToString();
                }


                if (rdlstDidPatientReturn.SelectedValue.ToString().Equals(""))
                {
                    txtDidPatientReturn.Text = "-1";
                }
                else
                {
                    txtDidPatientReturn.Text = rdlstDidPatientReturn.SelectedValue.ToString();
                }

                if (rdlstHowLongLimitaionApply.SelectedValue.ToString().Equals(""))
                {
                    txtHowLongLimitaionApply.Text = "-1";
                }
                else
                {
                    txtHowLongLimitaionApply.Text = rdlstHowLongLimitaionApply.SelectedValue.ToString();
                }


                if (rdlstDiscussPatientReturn.SelectedValue.ToString().Equals(""))
                {
                    txtDiscussPatientReturn.Text = "-1";
                }
                else
                {
                    txtDiscussPatientReturn.Text = rdlstDiscussPatientReturn.SelectedValue.ToString();
                }

                if (rdlstHealthCareProvider.SelectedValue.ToString().Equals(""))
                {
                    txtHealthCareProvider.Text = "-1";
                }
                else
                {
                    txtHealthCareProvider.Text = rdlstHealthCareProvider.SelectedValue.ToString();
                }
              
        

                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "WorkStatus_New.xml";
                _saveOperation.SaveMethod();

                

                if (chkPatientReturnToWorkWithlimitation.Checked)
                {
                    DeleteLimitations();
                    SaveOtherInformation();
                }
            }
            else
            {
                updatedata();
             //   if (chkPatientReturnToWorkWithlimitation.Checked)
                {
                    DeleteLimitations();
                    SaveOtherInformation();
                }
            }

            LoadData();

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void GetDoctorName()
    {
        Bill_Sys_DoctorBO objDoctor = new Bill_Sys_DoctorBO();
        string strDoctorName = objDoctor.GetDoctorForBill(txtCompanyID.Text, txtBillNumber.Text);
        txtDoctorName.Text = strDoctorName;
    }
    protected void rdlstPatientMissedWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (chklstPatientMissedWork.SelectedValue == "Yes")
        //{
        //    tblPatientmissedWork.Visible = true;
        //}
    }

    public void loadReqData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (chkPatientcannotReturn.Checked)
            {
                hdntxtPatientCanReturnWork.Text = "0";
                hdntxtPatientCanReturnWorkDescription.Text = txtPatientcannotReturn.Text;
                txtOtherLimitation.Text = "";
                txtQuantifyTheLimitaion.Text = "";
                foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
                {
                    _objLI.Selected = false;

                }

                //foreach (ListItem _objLI1 in chklstDiscussPatientReturn.Items)
                //{
                //    _objLI1.Selected = false;

                //}
            }

            if (chkPatientReturnToWorkWithoutLimitation.Checked)
            {
                hdntxtPatientCanReturnWork.Text = "1";
                hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithoutLimitaion.Text;
                txtOtherLimitation.Text = "";
                txtQuantifyTheLimitaion.Text = "";
                //foreach (ListItem _objLI in chklstHowLongLimitaionApply.Items)
                //{
                //    _objLI.Selected = false;
                //}

                //foreach (ListItem _objLI1 in chklstDiscussPatientReturn.Items)
                //{
                //    _objLI1.Selected = false;

                //}

            }

            if (chkPatientReturnToWorkWithlimitation.Checked)
            {
                hdntxtPatientCanReturnWork.Text = "2";
                hdntxtPatientCanReturnWorkDescription.Text = txtPatientWorkWithLimitaion.Text;
                
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

    public void DeleteLimitations()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        WorkerTemplate _objWT;
        try
        {
            ArrayList _objAL = new ArrayList();
            _objAL.Add("");
            _objAL.Add(txtPatientID.Text.ToString());
            _objAL.Add(txtCompanyID.Text.ToString());
            _objAL.Add(txtBillNumber.Text.ToString());
            _objAL.Add("DELETEALL");

            _objWT = new WorkerTemplate();
            _objWT.savePatientLimitationsWC4(_objAL);

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

    public void SaveOtherInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        WorkerTemplate _objWT;
        try
        {
           foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items )
           {
               if (_objLI.Selected == true)
               {
                   ArrayList _objAL = new ArrayList();
                   _objAL.Add(_objLI.Text);
                   _objAL.Add(txtPatientID.Text.ToString());
                   _objAL.Add(txtCompanyID.Text.ToString());
                    _objAL.Add(txtBillNumber.Text.ToString());
                    _objAL.Add("ADD");

                   _objWT = new WorkerTemplate();
                   _objWT.savePatientLimitationsWC4(_objAL);
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #region "Update Information"

    public void updatedata()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            loadReqData();


            txtPatientMissedWork.Text = rdlstPatientMissedWork.SelectedValue.ToString();
            txtPatientCurrentlyWorking.Text = rdlstPatientCurrentlyWorking.SelectedValue.ToString();
            txtDidPatientReturn.Text = rdlstDidPatientReturn.SelectedValue.ToString();
            txtHowLongLimitaionApply.Text = rdlstHowLongLimitaionApply.SelectedValue.ToString();
            txtDiscussPatientReturn.Text = rdlstDiscussPatientReturn.SelectedValue.ToString();
            txtHealthCareProvider.Text = rdlstHealthCareProvider.SelectedValue.ToString();

            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "WorkStatusNew.xml";
            _editOperation.UpdateMethod();
            //LoadData();

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

    #endregion


    #region "Load Data"
    protected void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            GetDoctorName();
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "WorkStatus_New.xml";
            _editOperation.LoadData();


               if(txtPatientMissedWork.Text.Trim()!="-1")
               {
                   if(txtPatientMissedWork.Text.Trim()=="0")
                   {
                       rdlstPatientMissedWork.SelectedIndex=0;
                   }
                   if(txtPatientMissedWork.Text.Trim()=="1")
                   {
                       rdlstPatientMissedWork.SelectedIndex=1;
                   }
               }
               if(txtPatientCurrentlyWorking.Text.Trim()!="-1")
               {
                   if(txtPatientCurrentlyWorking.Text.Trim()=="0")
                   {
                       rdlstPatientCurrentlyWorking.SelectedIndex=0;
                   }
                   if(txtPatientCurrentlyWorking.Text.Trim()=="1")
                   {
                       rdlstPatientCurrentlyWorking.SelectedIndex=1;
                   }
               }
            if(txtDidPatientReturn.Text.Trim()!="-1")
               {
                   if(txtDidPatientReturn.Text.Trim()=="0")
                   {
                       rdlstDidPatientReturn.SelectedIndex=0;
                   }
                   if(txtDidPatientReturn.Text.Trim()=="1")
                   {
                       rdlstDidPatientReturn.SelectedIndex=1;
                   }
               }

             if(txtHowLongLimitaionApply.Text.Trim()!="-1")
               {
                   if(txtHowLongLimitaionApply.Text.Trim()=="0")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=0;
                   }
                   if(txtHowLongLimitaionApply.Text.Trim()=="1")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=1;
                   }
                  if(txtHowLongLimitaionApply.Text.Trim()=="2")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=2;
                   }
                   if(txtHowLongLimitaionApply.Text.Trim()=="3")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=3;
                   }
                 if(txtHowLongLimitaionApply.Text.Trim()=="4")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=4;
                   }
                 if(txtHowLongLimitaionApply.Text.Trim()=="5")
                   {
                       rdlstHowLongLimitaionApply.SelectedIndex=5;
                   }
               }


             if(txtDiscussPatientReturn.Text.Trim()!="-1")
               {
                   if(txtDiscussPatientReturn.Text.Trim()=="0")
                   {
                       rdlstDiscussPatientReturn.SelectedIndex=0;
                   }
                   if(txtDiscussPatientReturn.Text.Trim()=="1")
                   {
                       rdlstDiscussPatientReturn.SelectedIndex=1;
                   }
                 if(txtDiscussPatientReturn.Text.Trim()=="2")
                   {
                       rdlstDiscussPatientReturn.SelectedIndex=2;
                   }
               }
            if(txtHealthCareProvider.Text.Trim()!="-1")
               {
                   if(txtHealthCareProvider.Text.Trim()=="0")
                   {
                       rdlstHealthCareProvider.SelectedIndex=0;
                   }
                   if(txtHealthCareProvider.Text.Trim()=="1")
                   {
                       rdlstHealthCareProvider.SelectedIndex=1;
                   }
               }
       
 
     




            if (hdntxtPatientCanReturnWork.Text == "0")
            {
                chkPatientcannotReturn.Checked = true;
                txtPatientcannotReturn.Text = hdntxtPatientCanReturnWorkDescription.Text;
            }

            if (hdntxtPatientCanReturnWork.Text == "1")
            {
                chkPatientReturnToWorkWithoutLimitation.Checked = true;
                txtPatientWorkWithoutLimitaion.Text = hdntxtPatientCanReturnWorkDescription.Text;
            }

            if (hdntxtPatientCanReturnWork.Text == "2")
            {
                chkPatientReturnToWorkWithlimitation.Checked = true;
                txtPatientWorkWithLimitaion.Text = hdntxtPatientCanReturnWorkDescription.Text;
                LoadOtherInformation();
            }
            if (txtPatientMissedWorkDate.Text != "")
            {
                txtPatientMissedWorkDate.Text = Convert.ToDateTime(txtPatientMissedWorkDate.Text).ToShortDateString();
            }
            if (txtPatientWorkWithoutLimitaion.Text != "")
            {
                txtPatientWorkWithoutLimitaion.Text = Convert.ToDateTime(txtPatientWorkWithoutLimitaion.Text).ToShortDateString();
            }
            if (txtPatientWorkWithLimitaion.Text != "")
            {
                txtPatientWorkWithLimitaion.Text = Convert.ToDateTime(txtPatientWorkWithLimitaion.Text).ToShortDateString();
            }
            
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "providerinformation.xml";
            _editOperation.LoadData();

            if (txtProviderDate.Text != "")
            {
                txtProviderDate.Text = Convert.ToDateTime(txtProviderDate.Text).ToShortDateString();
            }

            string strOfficeName = txtProviderName.Text;
            txtAuthProviderSpeciality.Text = txtProviderSpeciality.Text;

            if (txtProviderSpeciality.Text.Contains("HBOT") || txtProviderSpeciality.Text.Contains("ROM") || txtProviderSpeciality.Text.Contains("PCE") || txtProviderSpeciality.Text.Contains("OAT"))
            {
                txtProviderName.Text = strOfficeName;
                txtAuthProviderName.Text = strOfficeName;
            }
            else
            {
                txtProviderName.Text = txtDoctorName.Text;
                txtAuthProviderName.Text = strOfficeName;
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

    protected void LoadOtherInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet _objDataSet = new DataSet();
        try
        {
           
            WorkerTemplate _objWR = new WorkerTemplate();
            _objDataSet = _objWR.getWorkStatusLimitation_New(txtBillNumber.Text);

            foreach (DataRow obj1 in _objDataSet.Tables[0].Rows)
            {
                foreach (ListItem _objLI in chklstPatientLimitationAllReason.Items)
                {
                    if (_objLI.Text == obj1[1].ToString())
                    {
                        _objLI.Selected = true;
                    }
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion

}
