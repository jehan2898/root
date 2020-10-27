using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Bill_Sys_ECG_Form1 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TXT_I_EVENT.Text = "11933";
        Session["ECG_EVENT_ID"] = TXT_I_EVENT.Text.ToString();

        TXT_DATE_OF_SERVICE.Text = DateTime.Now.ToString();
        if (!IsPostBack)
        {
            
            LoadData();
            LoadPatientData();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ECG_Form1.aspx");
        }
        #endregion

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            TXT_839_08_UPPER_MULTIPLE_SUBLUXATION.Text = RDO_839_08_UPPER_MULTIPLE_SUBLUXATION.SelectedIndex.ToString();
            TXT_723_1_UPPER_CERVICALGIA.Text = RDO_723_1_UPPER_CERVICALGIA.SelectedIndex.ToString();
            TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME.Text = RDO_723_2_UPPER_CERVICOCRANIAL_SYNDROME.SelectedIndex.ToString();
            TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.Text = RDO_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.SelectedIndex.ToString();
            TXT_723_4_UPPER_CERVICAL_REDICULITIS.Text = RDO_723_4_UPPER_CERVICAL_REDICULITIS.SelectedIndex.ToString();
            TXT_729_1_UPPER_MYALGIA_MYOSITIS.Text = RDO_729_1_UPPER_MYALGIA_MYOSITIS.SelectedIndex.ToString();
            TXT_728_85_UPPER_MUSCLE_SPASM.Text = RDO_728_85_UPPER_MUSCLE_SPASM.SelectedIndex.ToString();
            TXT_782_0_UPPER_NUMBNESS_TINGLING.Text = RDO_782_0_UPPER_NUMBNESS_TINGLING.SelectedIndex.ToString();
            TXT_839_2_LOWER_LUMBER_SUBLAXATION.Text = RDO_839_2_LOWER_LUMBER_SUBLAXATION.SelectedIndex.ToString();
            TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.Text = RDO_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.SelectedIndex.ToString();
            TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS.Text = RDO_724_2_LOWER_LUMBOSACRAL_RADICULITIS.SelectedIndex.ToString();
            TXT_724_8_LOWER_LUMBAGO.Text = RDO_724_8_LOWER_LUMBAGO.SelectedIndex.ToString();
            TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME.Text = RDO_723_4_LOWER_LUMBAR_FACET_SYNDROME.SelectedIndex.ToString();
            TXT_728_85_LOWER_MUSCLE_SPASM.Text = RDO_728_85_LOWER_MUSCLE_SPASM.SelectedIndex.ToString();
            TXT_782_0_LOWER_NUMBNESS_TINGLING.Text = RDO_782_0_LOWER_NUMBNESS_TINGLING.SelectedIndex.ToString();
            TXT_729_1_LOWER_MYALGIA_MYOSITIS.Text = RDO_729_1_LOWER_MYALGIA_MYOSITIS.SelectedIndex.ToString();

            if (Page.IsValid)
            {

                SaveOperation _objsave = new SaveOperation();
                _objsave.WebPage = this.Page;
                _objsave.Xml_File = "ECG_Form_First_Page.xml";
                _objsave.SaveMethod();
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
       

        Response.Redirect("Bill_Sys_ECG_Form2.aspx");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    public void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EditOperation _objEdit = new EditOperation();
            _objEdit.Primary_Value = TXT_I_EVENT.Text;
            _objEdit.WebPage = this.Page;
            _objEdit.Xml_File = "ECG_Form_First_Page.xml";
            _objEdit.LoadData();



            if(TXT_839_08_UPPER_MULTIPLE_SUBLUXATION.Text.Trim()!="-1")
            {
                if(TXT_839_08_UPPER_MULTIPLE_SUBLUXATION.Text.Trim()=="0")
                {
                    RDO_839_08_UPPER_MULTIPLE_SUBLUXATION.SelectedIndex=0;
                }
                else if(TXT_839_08_UPPER_MULTIPLE_SUBLUXATION.Text.Trim()=="1")
                {
                    RDO_839_08_UPPER_MULTIPLE_SUBLUXATION.SelectedIndex=1;
                }
                else if (TXT_839_08_UPPER_MULTIPLE_SUBLUXATION.Text.Trim()=="2")
                {
                    RDO_839_08_UPPER_MULTIPLE_SUBLUXATION.SelectedIndex = 2;
                }
            }

            if (TXT_723_1_UPPER_CERVICALGIA.Text.Trim() != "-1")
            {
                if (TXT_723_1_UPPER_CERVICALGIA.Text.Trim() == "0")
                {
                    RDO_723_1_UPPER_CERVICALGIA.SelectedIndex = 0;
                }
                else if (TXT_723_1_UPPER_CERVICALGIA.Text.Trim() == "1")
                {
                    RDO_723_1_UPPER_CERVICALGIA.SelectedIndex = 1;
                }
                else if (TXT_723_1_UPPER_CERVICALGIA.Text.Trim() == "2")
                {
                    RDO_723_1_UPPER_CERVICALGIA.SelectedIndex = 2;
                }
            }

            if (TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME.Text.Trim() != "-1")
            {
                if (TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME.Text.Trim() == "0")
                {
                    RDO_723_2_UPPER_CERVICOCRANIAL_SYNDROME.SelectedIndex = 0;
                }
                else if (TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME.Text.Trim() == "1")
                {
                    RDO_723_2_UPPER_CERVICOCRANIAL_SYNDROME.SelectedIndex = 1;
                }
                else if (TXT_723_2_UPPER_CERVICOCRANIAL_SYNDROME.Text.Trim() == "2")
                {
                    RDO_723_2_UPPER_CERVICOCRANIAL_SYNDROME.SelectedIndex = 2;
                }
            }

            if (TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.Text.Trim() != "-1")
            {
                if (TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.Text.Trim() == "0")
                {
                    RDO_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.SelectedIndex = 0;
                }
                else if (TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.Text.Trim() == "1")
                {
                    RDO_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.SelectedIndex = 1;
                }
                else if (TXT_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.Text.Trim() == "2")
                {
                    RDO_723_3_UPPER_CERVICOBRACHIAL_SYNDROME.SelectedIndex = 2;
                }
            }


            if (TXT_723_4_UPPER_CERVICAL_REDICULITIS.Text.Trim() != "-1")
            {
                if (TXT_723_4_UPPER_CERVICAL_REDICULITIS.Text.Trim() == "0")
                {
                    RDO_723_4_UPPER_CERVICAL_REDICULITIS.SelectedIndex = 0;
                }
                else if (TXT_723_4_UPPER_CERVICAL_REDICULITIS.Text.Trim() == "1")
                {
                    RDO_723_4_UPPER_CERVICAL_REDICULITIS.SelectedIndex = 1;
                }
                else if (TXT_723_4_UPPER_CERVICAL_REDICULITIS.Text.Trim() == "2")
                {
                    RDO_723_4_UPPER_CERVICAL_REDICULITIS.SelectedIndex = 2;
                }
            }


            if (TXT_729_1_UPPER_MYALGIA_MYOSITIS.Text.Trim() != "-1")
            {
                if (TXT_729_1_UPPER_MYALGIA_MYOSITIS.Text.Trim() == "0")
                {
                    RDO_729_1_UPPER_MYALGIA_MYOSITIS.SelectedIndex = 0;
                }
                else if (TXT_729_1_UPPER_MYALGIA_MYOSITIS.Text.Trim() == "1")
                {
                    RDO_729_1_UPPER_MYALGIA_MYOSITIS.SelectedIndex = 1;
                }
                else if (TXT_729_1_UPPER_MYALGIA_MYOSITIS.Text.Trim() == "2")
                {
                    RDO_729_1_UPPER_MYALGIA_MYOSITIS.SelectedIndex = 2;
                }
            }

            if (TXT_728_85_UPPER_MUSCLE_SPASM.Text.Trim() != "-1")
            {
                if (TXT_728_85_UPPER_MUSCLE_SPASM.Text.Trim() == "0")
                {
                    RDO_728_85_UPPER_MUSCLE_SPASM.SelectedIndex = 0;
                }
                else if (TXT_728_85_UPPER_MUSCLE_SPASM.Text.Trim() == "1")
                {
                    RDO_728_85_UPPER_MUSCLE_SPASM.SelectedIndex = 1;
                }
                else if (TXT_728_85_UPPER_MUSCLE_SPASM.Text.Trim() == "2")
                {
                    RDO_728_85_UPPER_MUSCLE_SPASM.SelectedIndex = 2;
                }
            }


            if (TXT_782_0_UPPER_NUMBNESS_TINGLING.Text.Trim() != "-1")
            {
                if (TXT_782_0_UPPER_NUMBNESS_TINGLING.Text.Trim() == "0")
                {
                    RDO_782_0_UPPER_NUMBNESS_TINGLING.SelectedIndex = 0;
                }
                else if (TXT_782_0_UPPER_NUMBNESS_TINGLING.Text.Trim() == "1")
                {
                    RDO_782_0_UPPER_NUMBNESS_TINGLING.SelectedIndex = 1;
                }
                else if (TXT_782_0_UPPER_NUMBNESS_TINGLING.Text.Trim() == "2")
                {
                    RDO_782_0_UPPER_NUMBNESS_TINGLING.SelectedIndex = 2;
                }
            }

            if (TXT_839_2_LOWER_LUMBER_SUBLAXATION.Text.Trim() != "-1")
            {
                if (TXT_839_2_LOWER_LUMBER_SUBLAXATION.Text.Trim() == "0")
                {
                    RDO_839_2_LOWER_LUMBER_SUBLAXATION.SelectedIndex = 0;
                }
                else if (TXT_839_2_LOWER_LUMBER_SUBLAXATION.Text.Trim() == "1")
                {
                   RDO_839_2_LOWER_LUMBER_SUBLAXATION.SelectedIndex = 1;
                }
                else if (TXT_839_2_LOWER_LUMBER_SUBLAXATION.Text.Trim() == "2")
                {
                   RDO_839_2_LOWER_LUMBER_SUBLAXATION.SelectedIndex = 2;
                }
            }



            if (TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.Text.Trim() != "-1")
            {
                if (TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.Text.Trim() == "0")
                {
                    RDO_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.SelectedIndex = 0;
                }
                else if (TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.Text.Trim() == "1")
                {
                    RDO_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.SelectedIndex = 1;
                }
                else if (TXT_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.Text.Trim() == "2")
                {
                    RDO_839_42_LOWER_SACROILLIAC_SUBLUXATIONS.SelectedIndex = 2;
                }
            }


            if (TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS.Text.Trim() != "-1")
            {
                if (TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS.Text.Trim() == "0")
                {
                    RDO_724_2_LOWER_LUMBOSACRAL_RADICULITIS.SelectedIndex = 0;
                }
                else if (TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS.Text.Trim() == "1")
                {
                    RDO_724_2_LOWER_LUMBOSACRAL_RADICULITIS.SelectedIndex = 1;
                }
                else if (TXT_724_2_LOWER_LUMBOSACRAL_RADICULITIS.Text.Trim() == "2")
                {
                    RDO_724_2_LOWER_LUMBOSACRAL_RADICULITIS.SelectedIndex = 2;
                }
            }



            if (TXT_724_8_LOWER_LUMBAGO.Text.Trim() != "-1")
            {
                if (TXT_724_8_LOWER_LUMBAGO.Text.Trim() == "0")
                {
                    RDO_724_8_LOWER_LUMBAGO.SelectedIndex = 0;
                }
                else if (TXT_724_8_LOWER_LUMBAGO.Text.Trim() == "1")
                {
                    RDO_724_8_LOWER_LUMBAGO.SelectedIndex = 1;
                }
                else if (TXT_724_8_LOWER_LUMBAGO.Text.Trim() == "2")
                {
                    RDO_724_8_LOWER_LUMBAGO.SelectedIndex = 2;
                }
            }


            if (TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME.Text.Trim() != "-1")
            {
                if (TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME.Text.Trim() == "0")
                {
                    RDO_723_4_LOWER_LUMBAR_FACET_SYNDROME.SelectedIndex = 0;
                }
                else if (TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME.Text.Trim() == "1")
                {
                    RDO_723_4_LOWER_LUMBAR_FACET_SYNDROME.SelectedIndex = 1;
                }
                else if (TXT_723_4_LOWER_LUMBAR_FACET_SYNDROME.Text.Trim() == "2")
                {
                    RDO_723_4_LOWER_LUMBAR_FACET_SYNDROME.SelectedIndex = 2;
                }
            }


            if (TXT_728_85_LOWER_MUSCLE_SPASM.Text.Trim() != "-1")
            {
                if (TXT_728_85_LOWER_MUSCLE_SPASM.Text.Trim() == "0")
                {
                    RDO_728_85_LOWER_MUSCLE_SPASM.SelectedIndex = 0;
                }
                else if (TXT_728_85_LOWER_MUSCLE_SPASM.Text.Trim() == "1")
                {
                    RDO_728_85_LOWER_MUSCLE_SPASM.SelectedIndex = 1;
                }
                else if (TXT_728_85_LOWER_MUSCLE_SPASM.Text.Trim() == "2")
                {
                    RDO_728_85_LOWER_MUSCLE_SPASM.SelectedIndex = 2;
                }
            }


            if (TXT_782_0_LOWER_NUMBNESS_TINGLING.Text.Trim() != "-1")
            {
                if (TXT_782_0_LOWER_NUMBNESS_TINGLING.Text.Trim() == "0")
                {
                    RDO_782_0_LOWER_NUMBNESS_TINGLING.SelectedIndex = 0;
                }
                else if (TXT_782_0_LOWER_NUMBNESS_TINGLING.Text.Trim() == "1")
                {
                    RDO_782_0_LOWER_NUMBNESS_TINGLING.SelectedIndex = 1;
                }
                else if (TXT_782_0_LOWER_NUMBNESS_TINGLING.Text.Trim() == "2")
                {
                    RDO_782_0_LOWER_NUMBNESS_TINGLING.SelectedIndex = 2;
                }
            }


            if (TXT_729_1_LOWER_MYALGIA_MYOSITIS.Text.Trim() != "-1")
            {
                if (TXT_729_1_LOWER_MYALGIA_MYOSITIS.Text.Trim() == "0")
                {
                    RDO_729_1_LOWER_MYALGIA_MYOSITIS.SelectedIndex = 0;
                }
                else if (TXT_729_1_LOWER_MYALGIA_MYOSITIS.Text.Trim() == "1")
                {
                    RDO_729_1_LOWER_MYALGIA_MYOSITIS.SelectedIndex = 1;
                }
                else if (TXT_729_1_LOWER_MYALGIA_MYOSITIS.Text.Trim() == "2")
                {
                    RDO_729_1_LOWER_MYALGIA_MYOSITIS.SelectedIndex = 2;
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


    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = Session["ECG_EVENT_ID"].ToString();
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "IM_FalloUP_PatientInformetion.xml";
            _editOperation.LoadData();



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

