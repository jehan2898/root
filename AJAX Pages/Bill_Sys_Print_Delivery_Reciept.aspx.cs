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
using DevExpress.Web;
using System.IO;
public partial class AJAX_Pages_Bill_Sys_Print_Delivery_Reciept : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            UserPatientInfoControl.GetPatienDeskList(((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            DataTable dtprint = new DataTable();
            dtprint.Columns.Add("SZ_PROCEDURE_ID");
            dtprint.Columns.Add("SZ_PROCEDURE_CODE");
            dtprint.Columns.Add("SZ_CODE_DESCRIPTION");
            dtprint.Columns.Add("FLT_AMOUNT");
            dtprint.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            dtprint.Columns.Add("SZ_PROCEDURE_GROUP");
            Session["printtable"] = dtprint;
        }
    }

    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dt = new DataSet();

        if (extddlSpeciality.Text != "NA")
        {
            Delivery_Reciept delobj = new Delivery_Reciept();
            dt = delobj.Get_DeliveryReciept(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, extddlSpeciality.Text);
            grdreciept.DataSource = dt;
            grdreciept.DataBind();


        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        DataTable dtPrint = (DataTable)Session["printtable"];
        for (int i = 0; i < grdreciept.VisibleRowCount; i++)
        {
            // ASPxGridView _ASPxGridView = (ASPxGridView)grdreciept.FindControl("grdexcludingbill");
            GridViewDataColumn c = (GridViewDataColumn)grdreciept.Columns[0];
            CheckBox checkBox = (CheckBox)grdreciept.FindRowCellTemplateControl(i, c, "chkall");
            if (checkBox != null)
            {
                if (checkBox.Checked)
                {
                    DataRow[] drPrintRows = dtPrint.Select(" SZ_PROCEDURE_ID = '" + Convert.ToString(grdreciept.GetRowValues(i, "SZ_PROCEDURE_ID")) + "'");
                   if (drPrintRows.Length < 1)
                   {
                       DataRow dr = dtPrint.NewRow();
                       dr["SZ_PROCEDURE_ID"] = Convert.ToString(grdreciept.GetRowValues(i, "SZ_PROCEDURE_ID"));
                       dr["SZ_PROCEDURE_CODE"] = Convert.ToString(grdreciept.GetRowValues(i, "SZ_PROCEDURE_CODE"));
                       dr["SZ_CODE_DESCRIPTION"] = Convert.ToString(grdreciept.GetRowValues(i, "SZ_CODE_DESCRIPTION"));
                       dr["FLT_AMOUNT"] = Convert.ToString(grdreciept.GetRowValues(i, "FLT_AMOUNT"));
                       dr["SZ_PROCEDURE_GROUP_ID"] = Convert.ToString(grdreciept.GetRowValues(i, "SZ_PROCEDURE_GROUP_ID"));
                       dr["SZ_PROCEDURE_GROUP"] = Convert.ToString(grdreciept.GetRowValues(i, "SZ_PROCEDURE_GROUP"));

                       dtPrint.Rows.Add(dr);
                   }
                
                }
            }

           

        }
        Session["printtable"] = dtPrint;
        grdprintselected.DataSource = dtPrint;
        grdprintselected.DataBind();


    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable dtprint = new DataTable();
            dtprint.Columns.Add("SZ_PROCEDURE_ID");
            dtprint.Columns.Add("SZ_PROCEDURE_CODE");
            dtprint.Columns.Add("SZ_CODE_DESCRIPTION");
            dtprint.Columns.Add("FLT_AMOUNT");
            dtprint.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            dtprint.Columns.Add("SZ_PROCEDURE_GROUP");

            for (int i = 0; i < grdprintselected.VisibleRowCount; i++)
            {
                // ASPxGridView _ASPxGridView = (ASPxGridView)grdreciept.FindControl("grdexcludingbill");
                GridViewDataColumn c = (GridViewDataColumn)grdprintselected.Columns[0];
                CheckBox checkBox = (CheckBox)grdprintselected.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (!checkBox.Checked)
                    {

                        DataRow dr = dtprint.NewRow();
                        dr["SZ_PROCEDURE_ID"] = Convert.ToString(grdprintselected.GetRowValues(i, "SZ_PROCEDURE_ID"));
                        dr["SZ_PROCEDURE_CODE"] = Convert.ToString(grdprintselected.GetRowValues(i, "SZ_PROCEDURE_CODE"));
                        dr["SZ_CODE_DESCRIPTION"] = Convert.ToString(grdprintselected.GetRowValues(i, "SZ_CODE_DESCRIPTION"));
                        dr["FLT_AMOUNT"] = Convert.ToString(grdprintselected.GetRowValues(i, "FLT_AMOUNT"));
                        dr["SZ_PROCEDURE_GROUP_ID"] = Convert.ToString(grdprintselected.GetRowValues(i, "SZ_PROCEDURE_GROUP_ID"));
                        dr["SZ_PROCEDURE_GROUP"] = Convert.ToString(grdprintselected.GetRowValues(i, "SZ_PROCEDURE_GROUP"));

                        dtprint.Rows.Add(dr);
                        //_Bill_Sys_Billing_Provider.RemoveexcludingBill(txtCompanyID.Text, Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_READING_DOCTOR_ID")), Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_INSURANCE_ID")), Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_CASE_TYPE_ID")));
                        //_Bill_Sys_Billing_Provider.RemoveexcludingBill(szDoctorname, txtCompanyID.Text);
                        //ClearControl();
                        //usrMessage.PutMessage("Delete Successfully ...!");
                        //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        //usrMessage.Show();
                    }
                }
            }


            Session["printtable"] = dtprint;
            grdprintselected.DataSource = dtprint;
            grdprintselected.DataBind();
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string patientname = "";

            ArrayList arr = new ArrayList();
            DataSet dt = new DataSet();
           // DataSet dtorderlist = new DataSet();
            
            DrugDAO obj = new DrugDAO();
            DrugDAO drgobj = new DrugDAO();
            //drugrs drgobj = new drugrs();
         
            DrugReport objdrugreport = new DrugReport();
           SrvDrugrs srvobj = new SrvDrugrs();

           dt = (DataSet)srvobj.GetDrugReports(((Bill_Sys_CaseObject)(Session["CASE_OBJECT"])).SZ_CASE_ID,((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
           if (dt.Tables[0].Rows.Count > 0)
           {
               //dtorderlist = (DataSet)srvobj.GetDeliveryOrderList();
               //if (dtorderlist.Tables[0].Rows.Count > 0)
               //{
                   drgobj.sz_company_name = dt.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString();
                   drgobj.sz_company_name_address = dt.Tables[0].Rows[0]["SZ_ADDRESS_STREET"].ToString();
                   drgobj.sz_city = dt.Tables[0].Rows[0]["SZ_ADDRESS_CITY"].ToString();
                   drgobj.sz_state = dt.Tables[0].Rows[0]["SZ_ADDRESS_STATE"].ToString();
                   drgobj.sz_zip = dt.Tables[0].Rows[0]["SZ_ADDRESS_ZIP"].ToString();
                   drgobj.sz_tel = "718 299 4400";
                   drgobj.sz_fax = "718 299 4700";

                   drgobj.sz_patient_name = dt.Tables[0].Rows[0]["PATIENT FIRSTNAME"].ToString() + " " + dt.Tables[0].Rows[0]["PATIENT LASTNAME"].ToString();
                   patientname = dt.Tables[0].Rows[0]["PATIENT FIRSTNAME"].ToString() + "_" + dt.Tables[0].Rows[0]["PATIENT LASTNAME"].ToString();
                   drgobj.sz_patient_address = dt.Tables[0].Rows[0]["ADDRESS OF PATIENT"].ToString();




                   //for (int i = 0; i < dtorderlist.Tables[0].Rows.Count; i++)
                   //{
                   //    drugorderlist drgorderlist = new drugorderlist();
                   //    drgorderlist.ordernumber = dtorderlist.Tables[0].Rows[i]["SZ_PROCEDURE_CODE"].ToString();
                   //    drgorderlist.ordereddrug = dtorderlist.Tables[0].Rows[i]["SZ_CODE_DESCRIPTION"].ToString();
                   //    arr.Add(drgorderlist);

                   //}


                   string dateofacc = String.Format("{0:mm/dd/yyyy}", dt.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString());
                   drgobj.date_of_accident = dateofacc;
                   drgobj.sz_Ins_Co = dt.Tables[0].Rows[0]["INSURANCE CARRIER"].ToString();


                   //  string sFilePath = "D:\\LawAllies\\Drug.pdf";


                   MUVGenerateFunction objSettings = new MUVGenerateFunction();



                   string path = objSettings.getApplicationSetting("PatientInfoSaveFilePath");
                   string OpenFilepath = objSettings.getApplicationSetting("PatientInfoOpenFilePath");
                   if (!Directory.Exists(path))
                   {
                       Directory.CreateDirectory(path);
                   }




                   string newPdfFilename = patientname + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".pdf";
                   //string newPdfFilename ="DrugReport.pdf";
                   string pdfPath = path + newPdfFilename;

                   DataTable dtFinalPrint = (DataTable)Session["printtable"];
                   objdrugreport.GenerateDrugReport(pdfPath, drgobj,txtFromDate.Text, dtFinalPrint);
                   //objdrugreport.GenerateDrugReport(pdfPath, drgobj, arr, txtFromDate.Text,dtFinalPrint);
                   // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('ok done');</script>");
                   string OpenPdfFilepath = OpenFilepath + newPdfFilename;
                   //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sandeep", "<script type='text/javascript'>window.location.href='" + OpenPdfFilepath + "'</script>");
                   ScriptManager.RegisterClientScriptBlock(this, GetType(), "kk", "window.open('" + OpenPdfFilepath + "')", true);




                   //objdrugreport.GenerateDrugReport(sFilePath, drgobj, arr);
                   ////  MessageBox.Show("PDF Generated");

                   //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('ok done');</script>");
               //}
           }


    }
}