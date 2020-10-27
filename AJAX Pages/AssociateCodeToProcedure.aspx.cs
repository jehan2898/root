using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.Web;

public partial class AJAX_Pages_AssociateCodeToProcedure : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.btnAdd.Attributes.Add("onclick", "return confirm_payment_save();");
        this.Btn_Clear.Attributes.Add("onclick", "return confirm_payment_remove();");
        if (!IsPostBack)
        {
            string billNo = Request.QueryString["szBillNo"];
            string paymentId = Request.QueryString["szPaymentId"];
            txtBillNo.Text = billNo;
            txtPaymentId.Text = paymentId;
            txtCompanyName.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindDoctorList(billNo, paymentId, txtCompanyName.Text);
        }
    }

    private void BindDoctorList(string billNo, string paymentId, string companyId)
    {
            Payment_Procedure_Association_DO obj= new Payment_Procedure_Association_DO();
            obj.SZ_BILL_ID = billNo;
            obj.SZ_PAYMENT_ID = paymentId;
            obj.SZ_COMPANY_ID = companyId;
            PaymentProcedureAssociation objProcedureAssociation = new PaymentProcedureAssociation();
            DataSet dsDoctor = objProcedureAssociation.GetPaymentProcedureAssociation(obj);
            grdProcedure.DataSource = dsDoctor;
            grdProcedure.DataBind();
            DataSet dsCode = objProcedureAssociation.GetPaymentCode(obj);
              if(dsCode.Tables.Count>0)
              {
                  if(dsCode.Tables[0].Rows.Count>0)
                  {
                        for (int i = 0; i < this.grdProcedure.VisibleRowCount; i++) 
                        {
                              
                             for (int j = 0; j < dsCode.Tables[0].Rows.Count; j++) 
                             {
                                 if(grdProcedure.GetRowValues(i, new string[] { "SZ_PROC_CODE" }).ToString()==dsCode.Tables[0].Rows[j]["SZ_PROC_CODE_ID"].ToString())
                                 {
                                       GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdProcedure.Columns[2];
                                       TextBox txtBox = (TextBox)this.grdProcedure.FindRowCellTemplateControl(i, gridViewDataColumn, "txtProcedure");
                                       txtBox.Text = dsCode.Tables[0].Rows[j]["SZ_AMOUNT_PAID"].ToString();
                                       //box.Checked=true;

                                 }

                              }			 
                        }
                  }
              }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ArrayList list = new ArrayList();

            for (int i = 0; i < this.grdProcedure.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdProcedure.Columns[2];
                TextBox txtBox = (TextBox)this.grdProcedure.FindRowCellTemplateControl(i, gridViewDataColumn, "txtProcedure");
                if (txtBox.Text != "")
                {
                    Payment_Procedure_Association_DO t_dao = new Payment_Procedure_Association_DO();
                    //t_dao.SZ_TYPE_CODE_ID = grdProcedure.GetRowValues(i, "SZ_PROC_CODE_ID").ToString();
                    t_dao.SZ_PROC_CODE = grdProcedure.GetRowValues(i, new string[] { "SZ_PROC_CODE" }).ToString();
                    t_dao.SZ_PAYMENT_ID = this.txtPaymentId.Text;
                    t_dao.SZ_BILL_ID = txtBillNo.Text;
                    t_dao.SZ_COMPANY_ID = txtCompanyName.Text;
                    t_dao.SZ_AMOUNT_PAID = txtBox.Text;
                    list.Add(t_dao);
                }
                //CheckBox box = (CheckBox)this.grdProcedure.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
                //if (box.Checked)
                //{
                //    Payment_Procedure_Association_DO t_dao = new Payment_Procedure_Association_DO();
                //    //t_dao.SZ_TYPE_CODE_ID = grdProcedure.GetRowValues(i, "SZ_PROC_CODE_ID").ToString();
                //    t_dao.SZ_PROC_CODE = grdProcedure.GetRowValues(i, new string[] { "SZ_PROC_CODE" }).ToString();
                //    t_dao.SZ_PAYMENT_ID = this.txtPaymentId.Text;
                //    t_dao.SZ_BILL_ID = txtBillNo.Text;
                //    t_dao.SZ_COMPANY_ID = txtCompanyName.Text;
                //    list.Add(t_dao);
                //}
            }
            PaymentProcedureAssociation obj = new PaymentProcedureAssociation();
            obj.SaveCode(list);
            
            usrMessage.PutMessage("Codes added successfully");
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
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();

            Payment_Procedure_Association_DO t_dao = new Payment_Procedure_Association_DO();
            t_dao.SZ_PAYMENT_ID = txtPaymentId.Text;
            t_dao.SZ_COMPANY_ID = txtCompanyId.Text;
            list.Add(t_dao);

            PaymentProcedureAssociation obj = new PaymentProcedureAssociation();
            obj.DeleteCodes(list);

            for (int i = 0; i < this.grdProcedure.VisibleRowCount; i++)
            {
                GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdProcedure.Columns[2];
                TextBox txtBox = (TextBox)this.grdProcedure.FindRowCellTemplateControl(i, gridViewDataColumn, "txtProcedure");
                txtBox.Text = "0.0";
                //CheckBox box = (CheckBox)this.grdProcedure.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
                //box.Checked = false;
            }
            usrMessage.PutMessage("Codes removed Successfully");
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
}