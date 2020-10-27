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

public partial class Bill_Sys_CM_PhysicalStatus1 : PageBase
{
    private SaveOperation _saveOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {        
            LoadData();
            LoadPatientData();
        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_PhysicalStatus1.aspx");
        }
        #endregion
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_PhysicalStatus.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //check
        
        if (RDO_CERVICAL_SPINE_APPEAR.SelectedValue.Equals(""))
        {
            txtRDO_CERVICAL_SPINE_APPEAR.Text = "-1";
        }
        else
        {
            txtRDO_CERVICAL_SPINE_APPEAR.Text = RDO_CERVICAL_SPINE_APPEAR.SelectedValue;
        }
        //check
        if (RDO_CERVICAL_SPINE_MODERATE.SelectedValue.Equals(""))
        {
            txtRDO_CERVICAL_SPINE_MODERATE.Text = "-1";
        }
        else
        {
            txtRDO_CERVICAL_SPINE_MODERATE.Text = RDO_CERVICAL_SPINE_MODERATE.SelectedValue;
        }
        //check RDO_CERVICAL_SPINE_TENDERNESS
        if (RDO_CERVICAL_SPINE_TENDERNESS.SelectedValue.Equals(""))
        {
            txtRDO_CERVICAL_SPINE_TENDERNESS.Text = "-1";
        }
        else
        {
            txtRDO_CERVICAL_SPINE_TENDERNESS.Text = RDO_CERVICAL_SPINE_TENDERNESS.SelectedValue;
        }
        //check
        if (RDO_CERVICAL_SPINE_WITH.SelectedValue.Equals(""))
        {
            txtRDO_CERVICAL_SPINE_WITH.Text = "-1";
        }
        else
        {
            txtRDO_CERVICAL_SPINE_WITH.Text = RDO_CERVICAL_SPINE_WITH.SelectedValue;
        }
        
        if (RDO_HIP_TENDERNESS.SelectedValue.Equals(""))
        {
            txtRDO_HIP_TENDERNESS.Text = "-1";
        }
        else
        {
            txtRDO_HIP_TENDERNESS.Text = RDO_HIP_TENDERNESS.SelectedValue;
        }

        
        if (RDO_LEG_RAISING_LEFT.SelectedValue.Equals(""))
        {
            txtRDO_LEG_RAISING_LEFT.Text = "-1";
        }
        else
        {
            txtRDO_LEG_RAISING_LEFT.Text = RDO_LEG_RAISING_LEFT.SelectedValue;
        }


        if (RDO_LEG_RAISING_RIGHT.SelectedValue.Equals(""))
        {
            txtRDO_LEG_RAISING_RIGHT.Text = "-1";
        }
        else
        {
            txtRDO_LEG_RAISING_RIGHT.Text = RDO_LEG_RAISING_RIGHT.SelectedValue;
        }

        if (RDO_LOWER_LUMBER_PAIN.SelectedValue.Equals(""))
        {
            txtRDO_LOWER_LUMBER_PAIN.Text = "-1";
        }
        else
        {
            txtRDO_LOWER_LUMBER_PAIN.Text = RDO_LOWER_LUMBER_PAIN.SelectedValue;
        }
        
        if (RDO_MUSCLE_SPASM.SelectedValue.Equals(""))
        {
            txtRDO_MUSCLE_SPASM.Text = "-1";
        }
        else
        {
            txtRDO_MUSCLE_SPASM.Text = RDO_MUSCLE_SPASM.SelectedValue;
        }

        if (RDO_SACRO_ILLAC_PAIN.SelectedValue.Equals(""))
        {
            txtRDO_SACRO_ILLAC_PAIN.Text = "-1";
        }
        else
        {
            txtRDO_SACRO_ILLAC_PAIN.Text = RDO_SACRO_ILLAC_PAIN.SelectedValue;
        }


        if (RDO_UPPER_LUMBAR_PAIN.SelectedValue.Equals(""))
        {
            txtRDO_UPPER_LUMBAR_PAIN.Text = "-1";
        }
        else
        {
            txtRDO_UPPER_LUMBAR_PAIN.Text = RDO_UPPER_LUMBAR_PAIN.SelectedValue;

        }


        _saveOperation = new SaveOperation();
        _saveOperation.WebPage = this.Page;
        _saveOperation.Xml_File = "CM_INITIAL_EVAL_PhysicalStatus1.xml";
        _saveOperation.SaveMethod();
        Response.Redirect("Bill_Sys_CM_PhysicalStatus2.aspx", false);


    }


    public void LoadData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PhysicalStatus1.xml";
            _editOperation.LoadData();


            //check
            if (txtRDO_CERVICAL_SPINE_APPEAR.Text.Equals("-1"))
            {
                
            }
            else
            {
                RDO_CERVICAL_SPINE_APPEAR.SelectedValue= txtRDO_CERVICAL_SPINE_APPEAR.Text;
            }

            if (txtRDO_CERVICAL_SPINE_MODERATE.Text .Equals("-1"))
            {
               
            }
            else
            {
              RDO_CERVICAL_SPINE_MODERATE.SelectedValue=txtRDO_CERVICAL_SPINE_MODERATE.Text;
            }
            //Check
            if (txtRDO_CERVICAL_SPINE_TENDERNESS.Text.Equals("-1"))
            {
                 
            }
            else
            {
                  RDO_CERVICAL_SPINE_TENDERNESS.SelectedValue=txtRDO_CERVICAL_SPINE_TENDERNESS.Text;
            }

            if (txtRDO_CERVICAL_SPINE_WITH.Text.Equals("-1"))
            {
                
            }
            else
            {
                RDO_CERVICAL_SPINE_WITH.SelectedValue = txtRDO_CERVICAL_SPINE_WITH.Text;
            }

            if (txtRDO_HIP_TENDERNESS.Text.Equals("-1"))
            {
                
            }
            else
            {
                 RDO_HIP_TENDERNESS.SelectedValue=txtRDO_HIP_TENDERNESS.Text;
            }


            if ( txtRDO_LEG_RAISING_LEFT.Text.Equals("-1"))
            {
                
            }
            else
            {
                RDO_LEG_RAISING_LEFT.SelectedValue = txtRDO_LEG_RAISING_LEFT.Text;
            }


            if (txtRDO_LEG_RAISING_RIGHT.Text.Equals("-1"))
            {
              
            }
            else
            {
               RDO_LEG_RAISING_RIGHT.SelectedValue= txtRDO_LEG_RAISING_RIGHT.Text;
            }

            if ( txtRDO_LOWER_LUMBER_PAIN.Text .Equals("-1"))
            {
            
            }
            else
            {
             RDO_LOWER_LUMBER_PAIN.SelectedValue= txtRDO_LOWER_LUMBER_PAIN.Text;
            }

            if ( txtRDO_MUSCLE_SPASM.Text.Equals("-1"))
            {
      
            }
            else
            {
              RDO_MUSCLE_SPASM.SelectedValue= txtRDO_MUSCLE_SPASM.Text ;
            }

            if (txtRDO_SACRO_ILLAC_PAIN.Text.Equals("-1"))
            {
                
            }
            else
            {
               RDO_SACRO_ILLAC_PAIN.SelectedValue=txtRDO_SACRO_ILLAC_PAIN.Text;
            }


            if ( txtRDO_UPPER_LUMBAR_PAIN.Text.Equals("-1"))
            {
              
            }
            else
            {
                RDO_UPPER_LUMBAR_PAIN.SelectedValue = txtRDO_UPPER_LUMBAR_PAIN.Text;

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
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PatientInformetion.xml";
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
