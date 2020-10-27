using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AJAX_Pages_LHR_Manual_Import : PageBase
{
    LHR_Visit_Import objLHR = null;
    string CompanyID = string.Empty,UserID=string.Empty;
    LHR_Visit_ImportDAO objLHRDAO = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        UserID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID; 

        if (!IsPostBack)
        {
            loadState();
            loadBookFacility();
            loadModality();
        }
    }

    private void loadState()
    {
        DataSet ds = new DataSet();
        objLHR = new LHR_Visit_Import();
        ds = objLHR.FetchState();

        if (ds != null)
        {
            if (ds.Tables != null)
            {
                cmbState.DataSource = ds.Tables[0];
                cmbState.ValueField = "SZ_STATE_CODE";
                cmbState.TextField = "SZ_STATE_NAME";
                cmbState.DataBind();
            }
        }
    }

    private void loadBookFacility()
    {
        DataSet ds = new DataSet();
        objLHR = new LHR_Visit_Import();
        ds = objLHR.FetchLocation(CompanyID);

        if (ds != null)
        {
            if (ds.Tables != null)
            {
                cmdBookFacility.DataSource = ds.Tables[0];
                cmdBookFacility.ValueField = "CODE";
                cmdBookFacility.TextField = "DESCRIPTION";
                cmdBookFacility.DataBind();
            }
        }
    }

    private void loadModality()
    {
        DataSet ds = new DataSet();
        objLHR = new LHR_Visit_Import();
        ds = objLHR.FetchModality(CompanyID);

        if (ds != null)
        {
            if (ds.Tables != null)
            {
                cmbModality.DataSource = ds.Tables[0];
                cmbModality.ValueField = "SZ_MODALITY";
                cmbModality.TextField = "SZ_MODALITY";
                cmbModality.DataBind();
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        objLHRDAO = new LHR_Visit_ImportDAO();
        objLHR = new LHR_Visit_Import();
        DateTime dt = new DateTime();

        string timed = "";

        if (txtPatientFirstName.Text == string.Empty || txtPatientFirstName.Text == "" || txtPatientLastName.Text == string.Empty || txtPatientLastName.Text == "" ||
            txtPatientID.Text == string.Empty || txtPatientID.Text == "" || txtPatientAppoinmentID.Text == string.Empty || txtPatientAppoinmentID.Text == "" ||
            txtProcedureCode.Text == string.Empty || txtProcedureCode.Text == "" || txtProcedureDesc.Text == string.Empty || txtProcedureDesc.Text == "" ||
            timeVisitTime.Value ==null || cmbState.SelectedIndex ==-1 || cmbModality.SelectedIndex==-1 || cmbBoxCaseType.SelectedIndex==-1 || cmbGender.SelectedIndex ==-1)
        {
            return;
        }

        objLHRDAO.SZ_First_Name = txtPatientFirstName.Text;
        objLHRDAO.SZ_Last_Name= txtPatientLastName.Text;
        objLHRDAO.SZ_Patient_ID = txtPatientID.Text;
        objLHRDAO.SZ_Case_ID= txtPatientAppoinmentID.Text;
        if (dtDOB.Value != null)
        {
            dt =Convert.ToDateTime(dtDOB.Value);
            objLHRDAO.SZ_Date_Of_Birth = dt.ToString("MM/dd/yyyy");
        }
        objLHRDAO.SZ_Gender =Convert.ToString(cmbGender.SelectedItem.Value);
        objLHRDAO.SZ_Address = txtPatientAddr.Text;
        objLHRDAO.SZ_Address2 = txtPatientAddr2.Text;
        objLHRDAO.SZ_City = txtPatientCity.Text;
        objLHRDAO.SZ_Zip = txtPatientZipCode.Text;

        if (cmbBoxCaseType.SelectedIndex != -1)
        objLHRDAO.SZ_Case_Type =Convert.ToString(cmbBoxCaseType.SelectedItem);
        
        objLHRDAO.SZ_SSNO = txtPatientSSNO.Text;

        if (cmbState.SelectedIndex != -1)
        {
            objLHRDAO.SZ_State =Convert.ToString(cmbState.SelectedItem.Value);
        }
        if (dtDateOfAppointment.Value != null)
        {
            dt = Convert.ToDateTime(dtDateOfAppointment.Value);
            objLHRDAO.SZ_Date_Of_Appointment = dt.ToString("MM/dd/yyyy");
        }

        if (timeVisitTime.Value != null)
        {
            DateTime convertedDate = DateTime.Parse(timeVisitTime.Value.ToString());
            timed = convertedDate.ToString("HH:mm");

            try
            {
                string hr = convertedDate.ToString("HH");
                string min = convertedDate.ToString("mm");

                if (Convert.ToInt16(hr) < 12)
                {
                    objLHRDAO.SZ_Visit_Time =  hr+":"+min+ " AM";
                }
                else
                {
                    hr=Convert.ToString((Convert.ToInt32(hr) % 12));
                    objLHRDAO.SZ_Visit_Time = hr + ":" + min  +" PM";
                }
            }
            catch(Exception ex)
            {
                objLHRDAO.SZ_Visit_Time = timed + " AM";
            }
        }

        if (dtDateOfAcci.Value != null)
        {
            dt = Convert.ToDateTime(dtDateOfAcci.Value);
            objLHRDAO.SZ_Date_Of_Accident = dt.ToString("MM/dd/yyyy");
        }

        if(cmbModality.SelectedIndex != -1)
        {
            objLHRDAO.SZ_Modality =Convert.ToString( cmbModality.SelectedItem.Value);
            }

        if(cmdBookFacility.SelectedIndex != -1)
        {
            objLHRDAO.SZ_Book_Facility =Convert.ToString( cmdBookFacility.SelectedItem.Value);
        }

        objLHRDAO.SZ_Procedure_Code = txtProcedureCode.Text;
        objLHRDAO.SZ_Procedure_Desc = txtProcedureDesc.Text;
        objLHRDAO.Company_ID = CompanyID;
        objLHRDAO.User_ID = UserID;
        string status = objLHR.saveLHRData(objLHRDAO);
        
        if (status == "inserted")
        {
            usrMessage.PutMessage(GetLocalResourceObject("manualimport.visit.add.success").ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            
        }
        else
        {
            usrMessage.PutMessage(GetLocalResourceObject("manualimport.visit.add.error").ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }


        ClearControl();
    }

    private void ClearControl()
    {
        txtPatientFirstName.Text = string.Empty;
        txtPatientLastName.Text = string.Empty;
        txtPatientID.Text = string.Empty;
        txtPatientAppoinmentID.Text = string.Empty;
        dtDOB.Value = System.DateTime.Now.Date;
        cmbGender.SelectedIndex = -1;
        txtPatientAddr.Text = string.Empty;
        txtPatientAddr2.Text = string.Empty;
        txtPatientCity.Text = string.Empty;
        txtPatientZipCode.Text = string.Empty;
        cmbBoxCaseType.SelectedIndex = -1;
        txtPatientSSNO.Text = string.Empty;
        dtDateOfAppointment.Value = System.DateTime.Now.Date;
        timeVisitTime.Text = "";
        dtDateOfAcci.Value = System.DateTime.Now.Date;
        cmbModality.SelectedIndex = -1;
        cmdBookFacility.SelectedIndex = -1;
        txtProcedureCode.Text = string.Empty;
        txtProcedureDesc.Text = string.Empty;
    }
}