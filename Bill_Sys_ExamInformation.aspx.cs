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

public partial class Bill_Sys_ExamInformation : PageBase
{

    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtPatientID.Text = Session["PatientID"].ToString();
        txtBillNumber.Text = Session["TEMPLATE_BILL_NO"].ToString();
        txtDiagnosticTest.Attributes.Add("Onkeyup", "CheckLengthtest(this,190)");
        txtTreatment.Attributes.Add("Onkeyup", "CheckLengthtreatment(this,190)");
        txtPrognosis.Attributes.Add("Onkeyup", "CheckLengthprognosis(this,300)");
        txtExamInfo.Attributes.Add("Onkeyup", "CheckLengthexaminfo(this,210)");
        rdlistExamInfo.Attributes.Add("onclick", "validate_examinfordl();");
      //  imgbtnDateOfExam.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateOfExam,'imgbtnDateOfExam','MM/dd/yyyy'); return false;");

        if (Page.IsPostBack == false)
        {
            LoadData();
            passInformationToLoadData();
            if(txtDateOfExam.Text.ToString() == "1/1/1900")
            {
                txtDateOfExam.Text = "";
            }
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ExamInformation.aspx");
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
            if (_obj.PatientExistCheckForWC4("SP_MST_EXAM_INFORMATION_NEW", Session["TEMPLATE_BILL_NO"].ToString()) == false)
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "ExamInformation_New.xml";
                _saveOperation.SaveMethod();
            }
            else
            {
                updatedata();
            }

            Save_Patient_Compliments();
            Save_Patient_Injury();
            Save_Patient_Physical_Exam();
            Response.Redirect("Bill_Sys_DoctorOpinion.aspx",false);


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
           
            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "ExamInformationNew.xml";
            _editOperation.UpdateMethod();

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

            _editOperation.Primary_Value = Session["TEMPLATE_BILL_NO"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "ExamInformation_New.xml";
            _editOperation.LoadData();

            if (txtDateOfExam.Text != "")
                txtDateOfExam.Text = Convert.ToDateTime(txtDateOfExam.Text).ToShortDateString();

            
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


    #region "Load Data"

    public void passInformationToLoadData()
    {
        
        DataSet _objDataSet = new DataSet();
        WorkerTemplate _objWR = new WorkerTemplate();
        _objDataSet = _objWR.getPatientComplaints_NEW(txtBillNumber.Text);
        LoadCheckBoxAndTextBox(_objDataSet);

        _objDataSet.Clear();
        _objDataSet = _objWR.getPatientInjuryType_New(txtBillNumber.Text);
        LoadCheckBoxAndTextBox(_objDataSet);

        _objDataSet.Clear();
        _objDataSet = _objWR.getPatientPhysicalExam_New(txtBillNumber.Text);
        LoadCheckBoxAndTextBox(_objDataSet);


    }


    public void LoadCheckBoxAndTextBox(DataSet p_objDataSet)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            foreach (DataRow obj1 in p_objDataSet.Tables[0].Rows)
            {
                for (int j = 0; j <= pnlExamInformation.Controls.Count - 1; j++)
                {
                    if (pnlExamInformation.Controls[j].ID != null)
                    {
                        if (pnlExamInformation.Controls[j].GetType() == typeof(CheckBox))
                        {
                            CheckBox objCheckBox;
                            objCheckBox = (CheckBox)pnlExamInformation.Controls[j];
                            if (objCheckBox.Text == obj1[0].ToString())
                            {
                                objCheckBox.Checked = true;
                                String szID = objCheckBox.ID;
                                String szTxtID = szID.Replace("chk", "txt");

                                TextBox objTextBox;
                                objTextBox = (TextBox)pnlExamInformation.FindControl(szTxtID);
                                if (objTextBox != null)
                                    objTextBox.Text = obj1[1].ToString();

                            }
                        }
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



    #region "Save Data"

    public void Save_Patient_Compliments()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            WorkerTemplate _objWT;
            _objWT = new WorkerTemplate();
            _objWT.deletePatientComplaints_New(txtBillNumber.Text.ToString());


            if (chkNumbnessTingling.Checked)
            {
                passInformationToSave(chkNumbnessTingling.Text,txtNumbnessTingling.Text);  
            }
            if(chkSwelling.Checked)
            {
                passInformationToSave(chkSwelling.Text,txtSwelling.Text);    
            }

            if(chkPain.Checked)
            {
                passInformationToSave(chkPain.Text,txtPain.Text);    
            }

            if(chkWeakness.Checked)
            {
                passInformationToSave(chkWeakness.Text, txtWeakness.Text);    
            }

            if(chkStiffness.Checked)
            {
                passInformationToSave(chkStiffness.Text, txtStiffness.Text);    
            }

            if(chkOther.Checked)
            {
                passInformationToSave(chkOther.Text, txtOther.Text);    
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

    public void passInformationToSave(string p_szText,string p_szDescription)
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
            _objAL.Add(p_szText);
            _objAL.Add(p_szDescription);
            _objAL.Add(txtPatientID.Text.ToString());
            _objAL.Add(txtCompanyID.Text.ToString());
            _objAL.Add("ADD");
            _objAL.Add(txtBillNumber.Text.ToString());

            _objWT = new WorkerTemplate();
            _objWT.savePatientComplaintsNEW(_objAL);
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


    public void Save_Patient_Injury()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            WorkerTemplate _objWT;
            _objWT = new WorkerTemplate();
            _objWT.deletePatientInjury_New(txtBillNumber.Text.ToString());


            if (chkAbrasion.Checked)
            {
                passPatientInjuryToSave(chkAbrasion.Text, txtAbrasion.Text);
            }

             if (chkInfectiousDisease.Checked)
            {
                passPatientInjuryToSave(chkInfectiousDisease.Text, txtInfectiousDisease.Text);
            }

             if (chkAmputation.Checked)
            {
                passPatientInjuryToSave(chkAmputation.Text, txtAmputation.Text);
            }

             if (chkInhalationExposure.Checked)
            {
                passPatientInjuryToSave(chkInhalationExposure.Text, txtInhalationExposure.Text);
            }

             if (chkAvulsion.Checked)
            {
                passPatientInjuryToSave(chkAvulsion.Text, txtAvulsion.Text);
            }

             if (chkLaceration.Checked)
            {
                passPatientInjuryToSave(chkLaceration.Text, txtLaceration.Text);
            }

             if (chkBite.Checked)
            {
                passPatientInjuryToSave(chkBite.Text, txtBite.Text);
            }

             if (chkNeedleStick.Checked)
            {
                passPatientInjuryToSave(chkNeedleStick.Text, txtNeedleStick.Text);
            }

             if (chkBurn.Checked)
            {
                passPatientInjuryToSave(chkBurn.Text, txtBurn.Text);
            }

             if (chkPoisoningToxicEffects.Checked)
            {
                passPatientInjuryToSave(chkPoisoningToxicEffects.Text, txtPoisoningToxicEffects.Text);
            }

             if (chkContusionHematoma.Checked)
            {
                passPatientInjuryToSave(chkContusionHematoma.Text, txtContusionHematoma.Text);
            }

             if (chkPsychological.Checked)
            {
                passPatientInjuryToSave(chkPsychological.Text, txtPsychological.Text);
            }

             if (chkCrushInjury.Checked)
            {
                passPatientInjuryToSave(chkCrushInjury.Text, txtCrushInjury.Text);
            }

             if (chkPunctureWound.Checked)
            {
                passPatientInjuryToSave(chkPunctureWound.Text, txtPunctureWound.Text);
            }

             if (chkDermatitis.Checked)
            {
                passPatientInjuryToSave(chkDermatitis.Text, txtDermatitis.Text);
            }

             if (chkRepetitiveStrainInjury.Checked)
            {
                passPatientInjuryToSave(chkRepetitiveStrainInjury.Text, txtRepetitiveStrainInjury.Text);
            }

             if (chkDislocation.Checked)
            {
                passPatientInjuryToSave(chkDislocation.Text, txtDislocation.Text);
            }

             if (chkSpinalCordInjury.Checked)
            {
                passPatientInjuryToSave(chkSpinalCordInjury.Text, txtSpinalCordInjury.Text);
            }

             if (chkFracture.Checked)
            {
                passPatientInjuryToSave(chkFracture.Text, txtFracture.Text);
            }

             if (chkSprainStrain.Checked)
            {
                passPatientInjuryToSave(chkSprainStrain.Text, txtSprainStrain.Text);
            }

            if (chkHearingLoss.Checked)
            {
                passPatientInjuryToSave(chkHearingLoss.Text, txtHearingLoss.Text);
            }

            if (chkTornLigamentTendonOrMuscle.Checked)
            {
                passPatientInjuryToSave(chkTornLigamentTendonOrMuscle.Text, txtTornLigamentTendonOrMuscle.Text);
            }

            if (chkHernia.Checked)
            {
                passPatientInjuryToSave(chkHernia.Text, txtHernia.Text);
            }

            if (chkVisionLoss.Checked)
            {
                passPatientInjuryToSave(chkVisionLoss.Text, txtVisionLoss.Text);
            }

            if (chkOtherNatureOfInjury.Checked)
            {
                passPatientInjuryToSave(chkOtherNatureOfInjury.Text, txtOtherNatureOfInjury.Text);
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

    public void passPatientInjuryToSave(string p_szText, string p_szDescription)
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
            _objAL.Add(p_szText);
            _objAL.Add(p_szDescription);
            _objAL.Add(txtPatientID.Text.ToString());
            _objAL.Add(txtCompanyID.Text.ToString());
            _objAL.Add("ADD");
            _objAL.Add(txtBillNumber.Text.ToString());

            _objWT = new WorkerTemplate();
            _objWT.savePatientInjury_New(_objAL);
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


    public void Save_Patient_Physical_Exam()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            WorkerTemplate _objWT;
            _objWT = new WorkerTemplate();
            _objWT.deletePatientPhysicalExam_New(txtBillNumber.Text.ToString());


            if (chkNoneAtPresent.Checked)
            {
                passPatientPhysicalExamToSave(chkNoneAtPresent.Text,"");
            }

            if (chkBruising.Checked)
            {
                passPatientPhysicalExamToSave(chkBruising.Text, txtBruising.Text);
            }

            if (chkBurns.Checked)
            {
                passPatientPhysicalExamToSave(chkBurns.Text, txtBurns.Text);
            }

            if (chkCrepitation.Checked)
            {
                passPatientPhysicalExamToSave(chkCrepitation.Text, txtCrepitation.Text);
            }

            if (chkDeformity.Checked)
            {
                passPatientPhysicalExamToSave(chkDeformity.Text, txtDeformity.Text);
            }

            if (chkEdema.Checked)
            {
                passPatientPhysicalExamToSave(chkEdema.Text, txtEdema.Text);
            }

            if (chkHematomaLumpSwelling.Checked)
            {
                passPatientPhysicalExamToSave(chkHematomaLumpSwelling.Text, txtHematomaLumpSwelling.Text);
            }

            if (chkJointEffusion.Checked)
            {
                passPatientPhysicalExamToSave(chkJointEffusion.Text, txtJointEffusion.Text);
            }

            if (chkLacerationSutures.Checked)
            {
                passPatientPhysicalExamToSave(chkLacerationSutures.Text, txtLacerationSutures.Text);
            }

            if (chkPainTenderness.Checked)
            {
                passPatientPhysicalExamToSave(chkPainTenderness.Text, txtPainTenderness.Text);
            }

            if (chkScar.Checked)
            {
                passPatientPhysicalExamToSave(chkScar.Text, txtScar.Text);
            }

            if (chkOtherPhysicalInfo.Checked)
            {
                passPatientPhysicalExamToSave(chkOtherPhysicalInfo.Text, txtOtherPhysicalInfo.Text);
            }

            if (chkNeuromuscularFindings.Checked)
            {
                passPatientPhysicalExamToSave(chkNeuromuscularFindings.Text,"");
            }

            if (chkAbnormalRestrictedROM.Checked)
            {
                passPatientPhysicalExamToSave(chkAbnormalRestrictedROM.Text, "");
            }

            if (chkActiveROM.Checked)
            {
                passPatientPhysicalExamToSave(chkActiveROM.Text, txtActiveROM.Text);
            }

            if (chkPassiveROM.Checked)
            {
                passPatientPhysicalExamToSave(chkPassiveROM.Text, txtPassiveROM.Text);
            }

            if (chkGait.Checked)
            {
                passPatientPhysicalExamToSave(chkGait.Text, txtGait.Text);
            }

            if (chkPalpableMuscleSpasm.Checked)
            {
                passPatientPhysicalExamToSave(chkPalpableMuscleSpasm.Text, txtPalpableMuscleSpasm.Text);
            }

            if (chkReflexes.Checked)
            {
                passPatientPhysicalExamToSave(chkReflexes.Text, txtReflexes.Text);
            }

            if (chkSensation.Checked)
            {
                passPatientPhysicalExamToSave(chkSensation.Text, txtSensation.Text);
            }

            if (chkStrengthWeakness.Checked)
            {
                passPatientPhysicalExamToSave(chkStrengthWeakness.Text, txtStrengthWeakness.Text);
            }

            if (chkWastingMuscleAtrophy.Checked)
            {
                passPatientPhysicalExamToSave(chkWastingMuscleAtrophy.Text, txtWastingMuscleAtrophy.Text);
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

    public void passPatientPhysicalExamToSave(string p_szText, string p_szDescription)
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
            _objAL.Add(p_szText);
            _objAL.Add(p_szDescription);
            _objAL.Add(txtPatientID.Text.ToString());
            _objAL.Add(txtCompanyID.Text.ToString());
            _objAL.Add("ADD");
            _objAL.Add(txtBillNumber.Text.ToString());

            _objWT = new WorkerTemplate();
            _objWT.savePatientPhysicalExam_New(_objAL);
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



    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
