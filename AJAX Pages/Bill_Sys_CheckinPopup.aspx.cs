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
using log4net;
using System.Data.SqlClient;
using System.Drawing;


public partial class Bill_Sys_CheckinPopup : PageBase
{

    Bill_Sys_CheckinBO objCheckinBO;
    private static ILog log = LogManager.GetLogger("Bill_Sys_CheckinPopup");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CHECK_IN_CASE_ID"] = Request.QueryString["CaseID"].ToString();
            Session["CHECK_IN_PATIENT_ID"] = Request.QueryString["PatientID"].ToString();
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindDoctorListGird();
            txtVisitDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            setStatus();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CheckinPopup.aspx");
        }
        #endregion
    }

    private void setStatus()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet objDS = new DataSet();
            objCheckinBO = new Bill_Sys_CheckinBO();
            objDS = objCheckinBO.getCheckinStatus(Session["CHECK_IN_CASE_ID"].ToString(),((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if(objDS.Tables[0].Rows.Count >0)
            {

                string[] completetxt = objDS.Tables[0].Rows[0]["Completed"].ToString().Split(',');
                for (int i = 0; i < completetxt.Length; i++)
                {
                    if (i % 2 == 0 && i!=0)
                    {
                        lblCompleted.Text = lblCompleted.Text + "<br />" + completetxt[i].ToString();
                    }
                    else
                    {
                        lblCompleted.Text += completetxt[i].ToString();
                    }
                }

                string[] incompletetxt = objDS.Tables[0].Rows[0]["Uncompleted"].ToString().Split(',');
                for (int j = 0; j < incompletetxt.Length; j++)
                {
                    if (j % 2 == 0 && j != 0)
                    {
                        lblNotCompleted.Text = lblNotCompleted.Text + "<br />" + incompletetxt[j].ToString();
                    }
                    else
                    {
                        lblNotCompleted.Text += incompletetxt[j].ToString();
                    }
                }
                //for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
                //{
                //    if (i % 2 == 0)
                //    {
                //        lblCompleted.Text = lblCompleted.Text + "<br>";
                //        lblNotCompleted.Text = objDS.Tables[0].Rows[i]["Uncompleted"].ToString();
                //    }
                //    else
                //    {
                //        lblCompleted.Text = objDS.Tables[0].Rows[i]["Completed"].ToString();
                //    }
                //}
                //if (i / 2 = 0)
                //{
                //    lblCompleted.Text = lblCompleted.Text + "<br>";
                //    lblNotCompleted.Text = objDS.Tables[0].Rows[0]["Uncompleted"].ToString();
                //}
                //else
                //{
                //    lblCompleted.Text = objDS.Tables[0].Rows[0]["Completed"].ToString();
                //}
                //lblCompleted.Text = objDS.Tables[0].Rows[0]["Completed"].ToString();
                
                //if (lblCompleted.Text != "" || lblNotCompleted.Text != "")
                //{
                //    lbl1.Visible = true; lbl2.Visible = true;
                //}
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


    #region "Check in Screen"

    private void BindVisiTypeList(ref RadioButtonList listVisitType)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
        try
        {
            listVisitType.Items.Clear();
            listVisitType.DataSource = _bill_Sys_Calender.GET_Visit_Types(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            listVisitType.DataTextField = "VISIT_TYPE";
            listVisitType.DataValueField = "VISIT_TYPE_ID";
            listVisitType.DataBind();
            listVisitType.Items[2].Selected = true;
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


    protected void grdDoctorList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() != "Header" && e.Item.ItemType.ToString() != "Footer")
        {
            RadioButtonList listVisitType = (RadioButtonList)e.Item.Cells[3].FindControl("listVisitType");
            BindVisiTypeList(ref listVisitType);
        }
    }





    protected void btnCheckinSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Boolean blInitialVisitNotExist = false;
        Boolean blInitialVisitExist = false;
        Boolean blVisitExist = false;


        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
        lblCheckinMsg.Text = "";
        #region "Save"


        Bill_Sys_Visit_BO _bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        ArrayList objAdd = new ArrayList();
        try
        {
            #region "Check for Valid visit"

            for (int i = 0; i < grdDoctorList.Items.Count; i++)
            {
                if (((CheckBox)grdDoctorList.Items[i].Cells[0].FindControl("chkVisit")).Checked == true)
                {

                    String szSelectedVisit = ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).SelectedItem.Text;
                    String szSelectedVisitID = ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).SelectedItem.Value;
                    #region "Check for Visit"

                    Boolean iEvisitExists = false;
                    Boolean visitExists = false;
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                    SqlCommand comd = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                    comd.CommandType = CommandType.StoredProcedure;
                    comd.Connection = con;
                    comd.Connection.Open();
                    comd.Parameters.AddWithValue("@SZ_CASE_ID", Session["CHECK_IN_CASE_ID"].ToString());
                    comd.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    comd.Parameters.AddWithValue("@SZ_PATIENT_ID", Session["CHECK_IN_PATIENT_ID"].ToString());
                    comd.Parameters.AddWithValue("@SZ_DOCTOR_ID", grdDoctorList.Items[i].Cells[1].Text);
                    comd.Parameters.AddWithValue("@VISIT_DATE", txtVisitDate.Text);
                    string visitType = ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).SelectedItem.Text; 

                    comd.Parameters.AddWithValue("@VISIT_TYPE", visitType);
                    //int intCountVisits = Convert.ToInt32(comd.ExecuteScalar());

                    SqlParameter objIEExists = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                    objIEExists.Direction = ParameterDirection.Output;
                    comd.Parameters.Add(objIEExists);
                    SqlParameter objVisitStatus = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                    objVisitStatus.Direction = ParameterDirection.Output;
                    comd.Parameters.Add(objVisitStatus);
                    comd.ExecuteNonQuery();
                    comd.Connection.Close();

                    iEvisitExists = Convert.ToBoolean(objIEExists.Value);
                    visitExists = Convert.ToBoolean(objVisitStatus.Value);
                    if (iEvisitExists == false && szSelectedVisit != "IE")
                    {
                        ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).BackColor = Color.Yellow;
                        blInitialVisitNotExist = true;
                        continue;
                    }
                    if (iEvisitExists == true && szSelectedVisit == "IE")
                    {
                        ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).BackColor = Color.Pink;
                        blInitialVisitExist = true;
                        continue;
                    }
                    if (visitExists == true)
                    {
                        ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).BackColor = Color.Red;
                        blVisitExist = true;
                        continue;
                    }
                    #endregion
                }
            }


            if (blInitialVisitNotExist == true)
            {
                lblCheckinMsg.Text = "<br>Schedules cannot be saved because patient shown in yellow color are visiting first time hence there visit type should be Initial Evaluation.";
                lblCheckinMsg.Focus();
                lblCheckinMsg.Visible = true;
            }
            if (blInitialVisitExist == true)
            {
                lblCheckinMsg.Text = lblCheckinMsg.Text + "<br>Schedules cannot be saved because patient shown in pink color already has Initial Evaluation.";
                lblCheckinMsg.Focus();
                lblCheckinMsg.Visible = true;
            }
            if (blVisitExist == true)
            {
                lblCheckinMsg.Text = lblCheckinMsg.Text + "<br>Schedules cannot be saved because patient shown in red color already has this visit";
                lblCheckinMsg.Focus();
                lblCheckinMsg.Visible = true;
            }



            #endregion

            #region "Save All Visit"
            if (blInitialVisitNotExist == false && blInitialVisitExist == false && blVisitExist == false)
            {
                for (int i = 0; i < grdDoctorList.Items.Count; i++)
                {
                    if (((CheckBox)grdDoctorList.Items[i].Cells[0].FindControl("chkVisit")).Checked == true)
                    {
                        String szSelectedVisit = ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).SelectedItem.Text;
                        String szSelectedVisitID = ((RadioButtonList)grdDoctorList.Items[i].Cells[3].FindControl("listVisitType")).SelectedItem.Value;
                        Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
                        ArrayList objAL = new ArrayList();
                        objAL.Add(Session["CHECK_IN_CASE_ID"].ToString()); //Case Id
                        objAL.Add(txtVisitDate.Text); //Appointment date
                        objAL.Add("8.30");//Appointment time
                        objAL.Add("");//Notes
                        objAL.Add(grdDoctorList.Items[i].Cells[1].Text); // Doctor ID
                        objAL.Add("TY000000000000000003");//Type
                        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID); // Company ID
                        objAL.Add("AM");
                        objAL.Add("9.00");
                        objAL.Add("AM");
                        objAL.Add("0");
                        objAL.Add(szSelectedVisitID);
                        _bill_Sys_Calender.SaveEvent(objAL,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    }
                }
                if (blInitialVisitNotExist == false && blInitialVisitExist == false && blVisitExist == false)
                {
                    lblCheckinMsg.Text = "Visit Added Successfully.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ss", "<script language='javascript'> window.parent.document.location.href='Bill_Sys_SearchCase.aspx';window.self.close(); </script>");
                }
            }
            #endregion
            
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
        #endregion

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSearchSpeciality_Click(object sender, EventArgs e)
    {
        if (extddlSpeciality.Text == "NA")
        {
            BindDoctorListGird();
        }
        else
        {
            Bill_Sys_CheckinBO _objCheckinBO = new Bill_Sys_CheckinBO();
            grdDoctorList.DataSource = _objCheckinBO.getDoctorList_Acc_Speciality(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlSpeciality.Text);
            grdDoctorList.DataBind();
        }
    }

    public void BindDoctorListGird()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objCheckinBO = new Bill_Sys_CheckinBO();
            grdDoctorList.DataSource = objCheckinBO.getDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdDoctorList.DataBind();
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


            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
    }

    #endregion
    
}
