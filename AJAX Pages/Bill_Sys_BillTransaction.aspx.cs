using AjaxControlToolkit;
using Componend;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using GeneratePDFFile;
using log4net;
using mbs.LienBills;
using Microsoft.SqlServer.Management.Common;
using PDFValueReplacement;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_Sys_BillTransaction : Page
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    private Bill_Sys_LoginBO _bill_Sys_LoginBO;
    private Bill_Sys_Menu _bill_Sys_Menu;
    private Bill_Sys_NF3_Template _bill_Sys_NF3_Template;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private Bill_Sys_DigosisCodeBO _digosisCodeBO;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private MUVGenerateFunction _MUVGenerateFunction;
    private SaveOperation _saveOperation;
    private static string bt_Change_Amount;
    private static string bt_diagnosis_code;
    private string bt_include;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_DOCTOR = 12;
    private const int I_COL_GRID_COMPLETED_VISITS_ADDED_BY_USER = 15;
    private const int I_COL_GRID_COMPLETED_VISITS_FINALIZED = 13;
    private static ILog log = LogManager.GetLogger("Bill_Sys_BillTransaction");
    private CaseDetailsBO objCaseDetailsBO;
    private Bill_Sys_InsertDefaultValues objDefaultValue;
    private Bill_Sys_DigosisCodeBO objDiagCodeBO;
    private Bill_Sys_NF3_Template objNF3Template;
    private PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    private Bill_Sys_SystemObject objSystemObject;
    private Bill_Sys_Verification_Desc objVerification_Desc;
    private string pdfpath;
    private SqlConnection Sqlcon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
    private string str_1500;
    private string strHdnDiagnosisCode;

    private void BindAssociateGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
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

    private void BindDiagnosisGrid(string typeid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid);
            this.grdDiagonosisCode.DataBind();
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

    private void BindDiagnosisGrid(string typeid, string code, string description)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._digosisCodeBO = new Bill_Sys_DigosisCodeBO();
        try
        {
            this.grdDiagonosisCode.DataSource = this._digosisCodeBO.GetDiagnosisCodeReferalList(this.txtCompanyID.Text, typeid, code, description);
            this.grdDiagonosisCode.DataBind();
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

    public void BindDoctorsGrid(string szBillNumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            DataSet set = new DataSet();
            set = new Bill_Sys_Visit_BO().GetBillDoctorList(this.txtCompanyID.Text, szBillNumber, "GetBillDoctor");
            this.grdCompleteVisit.DataSource = set.Tables[0];
            this.grdCompleteVisit.DataBind();
            this.checkLimit();
            int num = 0;
            Random random = new Random();
            for (int i = 0; i < this.grdCompleteVisit.Items.Count; i++)
            {
                string text = this.grdCompleteVisit.Items[i].Cells[2].Text;
                list.Add(text);
                for (int k = 0; k < this.grdCompleteVisit.Items.Count; k++)
                {
                    if (text == this.grdCompleteVisit.Items[k].Cells[2].Text)
                    {
                        num++;
                    }
                }
                i = num - 1;
            }
            for (int j = 0; j < list.Count; j++)
            {
                byte blue = (byte)random.Next(0, 0xff);
                byte green = (byte)random.Next(0, 0xff);
                byte red = (byte)random.Next(0, 0xff);
                string str2 = list[j].ToString();
                foreach (DataGridItem item in this.grdCompleteVisit.Items)
                {
                    if (str2 == item.Cells[2].Text)
                    {
                        if (((blue > 150) && (green > 150)) && (red > 150))
                        {
                            blue = (byte)(blue - 100);
                            green = (byte)(green - 100);
                        }
                        item.Cells[1].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[2].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[3].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[4].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[5].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[6].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[7].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[8].ForeColor = Color.FromArgb(red, green, blue);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Visit_BO t_bo = new Bill_Sys_Visit_BO();
        ArrayList list = new ArrayList();
        try
        {
            list.Add(this.txtCompanyID.Text);
            list.Add(this.hndDoctorID.Value.ToString());
            list.Add("");
            list.Add("");
            list.Add("1");
            list.Add(this.txtCaseID.Text);
            list.Add("");
            this.grdAllReports.DataSource = t_bo.VisitReport(list);
            this.grdAllReports.DataBind();
            this.grdAllReports.Visible = true;
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

    private void BindIC9CodeControl(string id)
    {
        string id1 = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id1, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            ArrayList list = new ArrayList();
            list = this._bill_Sys_BillingCompanyDetails_BO.GetIC9CodeData(id);
            if (list.Count > 0)
            {
                this.txtTransDetailID.Text = list[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id1 + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id1, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindLatestTransaction()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage = this.Page;
            this._listOperation.Xml_File = "LatestBillTransaction.xml";
            this._listOperation.LoadList();
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

    private void BindTransactionData(string id)
    {
        string Elmahid = string.Format("elmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            this.grdTransactionDetails.DataSource = this._bill_Sys_BillTransaction.BindTransactionData(id);
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            double num = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        TextBox box = (TextBox)this.grdTransactionDetails.Items[i].Cells[6].FindControl("txtAmt");
                        string str = box.Text.ToString();
                        if ((str != "") && (str != "&nbsp;"))
                        {
                            num += Convert.ToDouble(str);
                        }
                    }
                    else if ((this.grdTransactionDetails.Items[i].Cells[5].Text != "") && (this.grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;"))
                    {
                        num += Convert.ToDouble(this.grdTransactionDetails.Items[i].Cells[5].Text);
                    }
                    if (i == (this.grdTransactionDetails.Items.Count - 1))
                    {
                        BillTransactionDAO ndao = new BillTransactionDAO();
                        string str2 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                        string str3 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                        string procID = ndao.GetProcID(this.txtCompanyID.Text, str2);
                        string str5 = ndao.GetLimit(this.txtCompanyID.Text, str3, procID);
                        if (str5 != "")
                        {
                            if (Convert.ToDouble(str5) < num)
                            {
                                this.grdTransactionDetails.Items[i].Cells[10].Text = str5;
                            }
                            else
                            {
                                this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                            }
                        }
                        num = 0.0;
                    }
                    else if (this.grdTransactionDetails.Items[i].Cells[1].Text != this.grdTransactionDetails.Items[i + 1].Cells[1].Text)
                    {
                        BillTransactionDAO ndao2 = new BillTransactionDAO();
                        string str6 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                        string str7 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                        string str8 = ndao2.GetProcID(this.txtCompanyID.Text, str6);
                        string str9 = ndao2.GetLimit(this.txtCompanyID.Text, str7, str8);
                        if (str9 != "")
                        {
                            if (Convert.ToDouble(str9) < num)
                            {
                                this.grdTransactionDetails.Items[i].Cells[10].Text = str9;
                            }
                            else
                            {
                                this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                            }
                        }
                        num = 0.0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void BindTransactionDetailsGrid(string billnumber)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            this.grdTransactionDetails.DataSource = this._bill_Sys_BillTransaction.BindTransactionData(billnumber);
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    TextBox box = (TextBox)this.grdTransactionDetails.Items[i].FindControl("txtUnit");
                    box.Text = this.grdTransactionDetails.Items[i].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao = new BillTransactionDAO();
                string str = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string procID = ndao.GetProcID(this.txtCompanyID.Text, str);
                string str3 = ndao.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                double num2 = 0.0;
                if ((str3 != "") && (str3 != "NULL"))
                {
                    for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                    {
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            TextBox box2 = (TextBox)this.grdTransactionDetails.Items[j].Cells[6].FindControl("txtAmt");
                            string str4 = box2.Text.ToString();
                            if ((str4 != "") && (str4 != "&nbsp;"))
                            {
                                num2 += Convert.ToDouble(str4);
                            }
                        }
                        else if ((this.grdTransactionDetails.Items[j].Cells[5].Text != "") && (this.grdTransactionDetails.Items[j].Cells[5].Text != "&nbsp;"))
                        {
                            num2 += Convert.ToDouble(this.grdTransactionDetails.Items[j].Cells[5].Text);
                        }
                        if (j == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao2 = new BillTransactionDAO();
                            string str5 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str6 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string str7 = ndao2.GetProcID(this.txtCompanyID.Text, str5);
                            string str8 = ndao2.GetLimit(this.txtCompanyID.Text, str6, str7);
                            if (str8 != "")
                            {
                                if (Convert.ToDouble(str8) < num2)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = str8;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = num2.ToString();
                                }
                            }
                            num2 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[j].Cells[1].Text != this.grdTransactionDetails.Items[j + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str9 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str10 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string str11 = ndao3.GetProcID(this.txtCompanyID.Text, str9);
                            string str12 = ndao3.GetLimit(this.txtCompanyID.Text, str10, str11);
                            if (str12 != "")
                            {
                                if (Convert.ToDouble(str12) < num2)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = str12;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = num2.ToString();
                                }
                            }
                            num2 = 0.0;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void BindVisitCompleteGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList list = new ArrayList();
            DataSet set = new DataSet();
            set = new Bill_Sys_Visit_BO().GetCompletedVisitList(this.txtCaseID.Text, this.txtCompanyID.Text, "VISITCOMPLETEDETAILS");
            this.grdCompleteVisit.DataSource = set.Tables[0];
            this.grdCompleteVisit.DataBind();
            int num = 0;
            Random random = new Random();
            for (int i = 0; i < this.grdCompleteVisit.Items.Count; i++)
            {
                string text = this.grdCompleteVisit.Items[i].Cells[2].Text;
                list.Add(text);
                for (int k = 0; k < this.grdCompleteVisit.Items.Count; k++)
                {
                    if (text == this.grdCompleteVisit.Items[k].Cells[2].Text)
                    {
                        num++;
                    }
                }
                i = num - 1;
            }
            for (int j = 0; j < list.Count; j++)
            {
                byte blue = (byte)random.Next(0, 0xff);
                byte green = (byte)random.Next(0, 0xff);
                byte red = (byte)random.Next(0, 0xff);
                string str2 = list[j].ToString();
                foreach (DataGridItem item in this.grdCompleteVisit.Items)
                {
                    if (str2 == item.Cells[2].Text)
                    {
                        if (((blue > 150) && (green > 150)) && (red > 150))
                        {
                            blue = (byte)(blue - 100);
                            green = (byte)(green - 100);
                        }
                        item.Cells[1].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[2].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[3].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[4].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[5].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[6].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[7].ForeColor = Color.FromArgb(red, green, blue);
                        item.Cells[8].ForeColor = Color.FromArgb(red, green, blue);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        this.dummybtnAddGroup.Visible = true;
        this.hndPopUpvalue.Value = "GroupPopUpValue";
        this.showModalPopup();
    }

    protected void btnAddServices_Click(object sender, EventArgs e)
    {
        this.dummybtnAddServices.Visible = true;
        this.hndPopUpvalue.Value = "PopUpValue";
        this.showModalPopup();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtBillDate.Text = "";
            this.extddlDoctor.SelectedValue = "NA";
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.grdTransactionDetails.Visible = false;
            this.ClearControl();
            this.lblDateOfService.Style.Add("visibility", "hidden");
            this.txtDateOfservice.Style.Add("visibility", "hidden");
            this.Image1.Style.Add("visibility", "hidden");
            this.lblGroupServiceDate.Style.Add("visibility", "hidden");
            this.txtGroupDateofService.Style.Add("visibility", "hidden");
            this.imgbtnDateofService.Style.Add("visibility", "hidden");
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

    protected void btnclearDiaProc_Click(object sender, EventArgs e)
    {
        this.lstDiagnosisCodes.Items.Clear();
        this.grdTransactionDetails.DataSource = null;
        this.grdTransactionDetails.DataBind();
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
        {
            this.grdTransactionDetails.Columns[5].Visible = false;
            this.grdTransactionDetails.Columns[6].Visible = true;
        }
        else
        {
            this.grdTransactionDetails.Columns[5].Visible = true;
            this.grdTransactionDetails.Columns[6].Visible = false;
        }
    }

    protected void btnClearService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.lblDateOfService.Style.Add("visibility", "hidden");
            this.txtDateOfservice.Style.Add("visibility", "hidden");
            this.Image1.Style.Add("visibility", "hidden");
            this.lblGroupServiceDate.Style.Add("visibility", "hidden");
            this.txtGroupDateofService.Style.Add("visibility", "hidden");
            this.imgbtnDateofService.Style.Add("visibility", "hidden");
            this.ClearControl();
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

    protected void btnGenerateWCPDF_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            WC_Bill_Generation generation = new WC_Bill_Generation();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType.SelectedValue, str4, str3, str, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0) + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
            ScriptManager.RegisterClientScriptBlock(pnlPDFWorkerComp, pnlPDFWorkerComp.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
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

    void GenerateWCPDFADDNEW()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        BillTransactionDAO ndao = new BillTransactionDAO();
        Result result = new Result();
        try
        {

            string str = "";
            BillTransactionEO neo = new BillTransactionEO();
            neo.SZ_CASE_ID = this.txtCaseID.Text;
            neo.SZ_COMPANY_ID = this.txtCompanyID.Text;
            neo.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
            neo.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
            neo.SZ_TYPE = this.ddlType.Text;
            neo.SZ_TESTTYPE = "";
            neo.FLAG = "ADD";
            neo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            if (this.grdCompleteVisit.Visible)
            {
                new Bill_Sys_Calender();
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    string text = item.Cells[14].Text;
                    if (list.Count != 0)
                    {
                        string str3 = "0";
                        for (int j = 0; j < list.Count; j++)
                        {
                            EventEO teo = new EventEO();
                            teo = (EventEO)list[j];
                            if ((text == teo.I_EVENT_ID) && (teo.I_EVENT_ID != "&nbsp;"))
                            {
                                str3 = "1";
                            }
                        }
                        if (str3 == "0")
                        {
                            EventEO teo2 = new EventEO();
                            teo2.I_EVENT_ID = text;
                            teo2.BT_STATUS = "1";
                            teo2.I_STATUS = "2";
                            teo2.SZ_BILL_NUMBER = "";
                            teo2.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                            list.Add(teo2);
                        }
                    }
                    else
                    {
                        EventEO teo3 = new EventEO();
                        teo3.I_EVENT_ID = text;
                        teo3.BT_STATUS = "1";
                        teo3.I_STATUS = "2";
                        teo3.SZ_BILL_NUMBER = "";
                        teo3.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                        list.Add(teo3);
                    }
                    int num2 = 0;
                    for (int i = 0; i < list2.Count; i++)
                    {
                        EventRefferProcedureEO eeo = new EventRefferProcedureEO();
                        eeo = (EventRefferProcedureEO)list2[i];
                        if (eeo.I_EVENT_ID == text)
                        {
                            num2 = 1;
                            break;
                        }
                    }
                    if (num2 != 1)
                    {
                        foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                        {
                            if ((item2.Cells[1].Text != "") && item.Cells[14].Text.Equals(item2.Cells[14].Text))
                            {
                                EventRefferProcedureEO eeo2 = new EventRefferProcedureEO();
                                eeo2.SZ_PROC_CODE = item2.Cells[9].Text;
                                eeo2.I_EVENT_ID = item2.Cells[14].Text;
                                eeo2.SZ_MODIFIER_ID = item2.Cells[17].Text;
                                eeo2.I_STATUS = "2";
                                list2.Add(eeo2);
                            }
                        }
                        continue;
                    }
                }
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            ArrayList list3 = new ArrayList();
            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                if (((!(item3.Cells[1].Text.ToString() != "") || !(item3.Cells[1].Text.ToString() != "&nbsp;")) || (!(item3.Cells[3].Text.ToString() != "") || !(item3.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item3.Cells[4].Text.ToString() != "") || !(item3.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                BillProcedureCodeEO eeo3 = new BillProcedureCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo3.SZ_PROCEDURE_ID = item3.Cells[2].Text.ToString();
                this.hdnSpeciality.Value = item3.Cells[3].Text.ToString();
                if (item3.Cells[7].Text.ToString() != "&nbsp;")
                {
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        eeo3.FL_AMOUNT = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        eeo3.FL_AMOUNT = item3.Cells[5].Text.ToString();
                    }
                }
                else
                {
                    eeo3.FL_AMOUNT = "0";
                }
                eeo3.SZ_BILL_NUMBER = "";
                eeo3.DT_DATE_OF_SERVICE = Convert.ToDateTime(item3.Cells[1].Text.ToString());
                eeo3.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                eeo3.I_UNIT = ((TextBox)item3.Cells[7].FindControl("txtUnit")).Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    eeo3.FLT_PRICE = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    eeo3.FLT_PRICE = item3.Cells[5].Text.ToString();
                }
                eeo3.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
                eeo3.SZ_CASE_ID = this.txtCaseID.Text;
                eeo3.SZ_TYPE_CODE_ID = item3.Cells[9].Text.ToString();
                //123
                if (item3.Cells[10].Text.ToString() != "&nbsp;")
                {
                    eeo3.FLT_GROUP_AMOUNT = item3.Cells[10].Text.ToString();
                }
                else
                {
                    eeo3.FLT_GROUP_AMOUNT = "";
                }
                if (((item3.Cells[11].Text.ToString() != "&nbsp;") && (item3.Cells[11].Text.ToString() != "&nbsp;")) && (item3.Cells[11].Text.ToString() != "&nbsp;"))
                {
                    eeo3.I_GROUP_AMOUNT_ID = item3.Cells[11].Text.ToString();
                }
                else
                {
                    eeo3.I_GROUP_AMOUNT_ID = "";
                }
                eeo3.SZ_MODIFIER_ID = item3.Cells[17].Text.ToString();
                list3.Add(eeo3);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList list4 = new ArrayList();
            foreach (ListItem item4 in this.lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO eeo4 = new BillDiagnosisCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo4.SZ_DIAGNOSIS_CODE_ID = item4.Value.ToString();
                list4.Add(eeo4);
            }


            ServerConnection currentConnection = ndao.BeginBillTranaction();
            result = ndao.SaveBillTransactions(neo, list, list2, list3, list4);
            if (result.msg_code == "ERR")
            {
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
            else
            {
                this.txtBillID.Text = result.bill_no;
                str = this.txtBillID.Text;
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                string patientID = this.objCaseDetailsBO.GetPatientID(str, currentConnection);
                string caseType = this.objCaseDetailsBO.GetCaseType(str, currentConnection);


                this.usrMessage.PutMessage(" Bill Saved successfully ! ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                message = " Bill Saved successfully ! ";

                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();

                if (caseType == "WC000000000000000001")
                {
                    string strn = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                    WC_Bill_Generation generation = new WC_Bill_Generation();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForWorkerComp(this.txtBillID.Text, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType1.SelectedValue, str4, str3, str, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(this.txtBillID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection), 0, currentConnection) + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
                    ScriptManager.RegisterClientScriptBlock(pnlPDFWorkerCompAdd, pnlPDFWorkerCompAdd.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    this.pnlPDFWorkerCompAdd.Visible = false;
                }


            }
            // ndao.CommitBillTranaction();
        }
        catch (Exception ex)
        {
            ndao.DeleteBillRecord(result.bill_no, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DELETE");
            // ndao.RollBackBillTranaction();
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
    protected void btnGenerateWCPDFAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            string str2 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
            string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            string str5 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            WC_Bill_Generation generation = new WC_Bill_Generation();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + generation.GeneratePDFForWorkerComp(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, this.rdbListPDFType1.SelectedValue, str4, str3, str, str2, str5, this._bill_Sys_BillTransaction.GetDoctorSpeciality(this.hdnWCPDFBillNumber.Value.ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), 0) + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
            ScriptManager.RegisterClientScriptBlock(pnlPDFWorkerCompAdd, pnlPDFWorkerCompAdd.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
            this.pnlPDFWorkerCompAdd.Visible = false;
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
    //protected void btnGenerateWCPDFAdd_Click(object sender, EventArgs e)
    //{
    //    string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
    //    using (Utils utility = new Utils())
    //    {
    //        utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
    //    }
    //    try
    //    {
    //        GenerateWCPDFADDNEW();
    //    }
    //    catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        using (Utils utility = new Utils())
    //        {
    //            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
    //        }
    //        string str2 = "Error Request=" + id + ".Please share with Technical support.";
    //        base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
    //    }
    //    //Method End
    //    using (Utils utility = new Utils())
    //    {
    //        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
    //    }
    //}
    protected void btnLoadProcedures_Click(object sender, EventArgs e)
    {
    }

    protected void btnNf2_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            new Bill_Sys_PatientBO().UpdateTemplateStatus(this.txtCaseID.Text, this.chkNf2.Checked ? 1 : 0, "");
            if (this.chkNf2.Checked)
            {
                this.txtNf2.Text = "1";
            }
            else
            {
                this.txtNf2.Text = "0";
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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //new Bill_Sys_AssociateDiagnosisCodeBO();
        //try
        //{
        //    for (int i = 0; i < this.grdDiagonosisCode.Items.Count; i++)
        //    {
        //        CheckBox box = (CheckBox) this.grdDiagonosisCode.Items[i].FindControl("chkAssociateDiagnosisCode");
        //        if (box.Checked)
        //        {
        //            ListItem item = new ListItem(this.grdDiagonosisCode.Items[i].Cells[2].Text + '-' + this.grdDiagonosisCode.Items[i].Cells[4].Text, this.grdDiagonosisCode.Items[i].Cells[1].Text);
        //            if (!this.lstDiagnosisCodes.Items.Contains(item))
        //            {
        //                this.lstDiagnosisCodes.Items.Add(item);
        //            }
        //        }
        //    }
        //    this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            string StrDGCodes = (hndSeletedDGCodes.Value.Replace("--,", "*")).Replace("--", "");
            string[] ArrCodes = StrDGCodes.Split('*');

            for (int i = 0; i < ArrCodes.Length; i++)
            {
                string[] arrInsert = ArrCodes[i].Split('~');
                ListItem item = new ListItem(arrInsert[1], arrInsert[2]);
                if (!this.lstDiagnosisCodes.Items.Contains(item))
                {
                    this.lstDiagnosisCodes.Items.Add(item);
                }

            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    public void btnProcSearch_Click(object sender, EventArgs e)
    {
        DataSet set = new DataSet();
        set = (DataSet)this.Session["doctorProcCodes"];
        string filterExpression = "";
        if (this.txtProcCode.Text != "")
        {
            filterExpression = "SZ_PROCEDURE_CODE LIKE '%" + this.txtProcCode.Text + "%'";
        }
        if ((this.txtProcDesc.Text != "") && (filterExpression != ""))
        {
            filterExpression = filterExpression + " or SZ_CODE_DESCRIPTION LIKE '%" + this.txtProcDesc.Text + "%'";
        }
        else if (this.txtProcDesc.Text != "")
        {
            filterExpression = "SZ_CODE_DESCRIPTION LIKE '%" + this.txtProcDesc.Text + "%'";
        }
        string text1 = "SZ_PROCEDURE_CODE LIKE '%" + this.txtProcCode.Text + "%'";
        string text2 = "SZ_CODE_DESCRIPTION LIKE '%" + this.txtProcDesc.Text + "%'";
        string sort = "SZ_CODE_DESCRIPTION ASC";
        DataRow[] rowArray = set.Tables[0].Select(filterExpression, sort);
        DataTable table = new DataTable();
        table.Columns.Add("SZ_PROCEDURE_ID");
        table.Columns.Add("SZ_TYPE_CODE_ID");
        table.Columns.Add("SZ_PROCEDURE_CODE");
        table.Columns.Add("SZ_CODE_DESCRIPTION");
        table.Columns.Add("FLT_AMOUNT");
        foreach (DataRow row in rowArray)
        {
            DataRow row2 = table.NewRow();
            row2["SZ_PROCEDURE_ID"] = row.ItemArray.GetValue(0).ToString();
            row2["SZ_TYPE_CODE_ID"] = row.ItemArray.GetValue(1).ToString();
            row2["SZ_PROCEDURE_CODE"] = row.ItemArray.GetValue(2).ToString();
            row2["SZ_CODE_DESCRIPTION"] = row.ItemArray.GetValue(3).ToString();
            row2["FLT_AMOUNT"] = row.ItemArray.GetValue(4).ToString();
            table.Rows.Add(row2);
        }
        this.grdProcedure.DataSource = table;
        this.grdProcedure.DataBind();
        this.modalpopupAddservice.Show();
    }

    protected void btnQuickBill_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (this.hdnQuick.Value.Equals("true"))
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                table.Columns.Add("DT_DATE_OF_SERVICE");
                table.Columns.Add("SZ_PROCEDURE_ID");
                table.Columns.Add("SZ_PROCEDURAL_CODE");
                table.Columns.Add("SZ_CODE_DESCRIPTION");
                table.Columns.Add("FLT_AMOUNT");
                table.Columns.Add("I_UNIT");
                table.Columns.Add("SZ_TYPE_CODE_ID");
                table.Columns.Add("FLT_GROUP_AMOUNT");
                table.Columns.Add("I_GROUP_AMOUNT_ID");
                table.Columns.Add("I_EventID");
                table.Columns.Add("SZ_VISIT_TYPE");
                table.Columns.Add("BT_IS_LIMITE");
                string str = "";
                ArrayList list = new ArrayList();
                DataSet visitTypeList = new DataSet();
                Bill_Sys_Visit_BO t_bo = new Bill_Sys_Visit_BO();
                visitTypeList = t_bo.GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
                int num = 0;
                foreach (DataGridItem item in this.grdCompleteVisit.Items)
                {
                    CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelectItem");
                    if (box.Checked)
                    {
                        string text = item.Cells[3].Text;
                        for (int j = 0; j < visitTypeList.Tables[0].Rows.Count; j++)
                        {
                            if (text == visitTypeList.Tables[0].Rows[j][1].ToString())
                            {
                                ArrayList list2 = new ArrayList();
                                list2.Add(this.txtCompanyID.Text);
                                list2.Add(item.Cells[6].Text);
                                list2.Add(text);
                                list2.Add(item.Cells[7].Text);
                                DataTable procedureCodeList = new DataTable();
                                procedureCodeList = t_bo.GetProcedureCodeList(list2);
                                if (procedureCodeList.Rows.Count > 0)
                                {
                                    list.Add(procedureCodeList);
                                }
                                else
                                {
                                    num = 1;
                                }
                            }
                        }
                        str = item.Cells[8].Text;
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    DataTable table3 = (DataTable)list[i];
                    foreach (DataRow row in table3.Rows)
                    {
                        DataRow row2 = table.NewRow();
                        row2["SZ_BILL_TXN_DETAIL_ID"] = "";
                        row2["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(row.ItemArray.GetValue(5)).ToString();
                        row2["SZ_PROCEDURE_ID"] = row.ItemArray.GetValue(0).ToString();
                        row2["SZ_PROCEDURAL_CODE"] = row.ItemArray.GetValue(2).ToString();
                        row2["SZ_CODE_DESCRIPTION"] = row.ItemArray.GetValue(3).ToString();
                        row2["FLT_AMOUNT"] = row.ItemArray.GetValue(4).ToString();
                        row2["I_UNIT"] = "1";
                        row2["SZ_TYPE_CODE_ID"] = row.ItemArray.GetValue(1).ToString();
                        row2["I_EventID"] = row.ItemArray.GetValue(6).ToString();
                        row2["BT_IS_LIMITE"] = "";
                        row2["SZ_VISIT_TYPE"] = row.ItemArray.GetValue(7).ToString();
                        table.Rows.Add(row2);
                    }
                }
                new DataView();
                table.DefaultView.Sort = "DT_DATE_OF_SERVICE";
                double num4 = 0.0;
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    this.grdTransactionDetails.Columns[5].Visible = false;
                    this.grdTransactionDetails.Columns[6].Visible = true;
                }
                else
                {
                    this.grdTransactionDetails.Columns[5].Visible = true;
                    this.grdTransactionDetails.Columns[6].Visible = false;
                }
                ArrayList list3 = new ArrayList();
                if (this.grdTransactionDetails.Items.Count > 0)
                {
                    BillTransactionDAO ndao = new BillTransactionDAO();
                    string str3 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                    this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                    string procID = ndao.GetProcID(this.txtCompanyID.Text, str3);
                    string str5 = ndao.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                    list3.Add(this.grdTransactionDetails.Items[0].Cells[14].Text.ToString());
                    if ((str5 != "") && (str5 != "NULL"))
                    {
                        for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                        {
                            this.grdTransactionDetails.Items[k].Cells[13].Text.ToString();
                            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                            {
                                TextBox box2 = (TextBox)this.grdTransactionDetails.Items[k].Cells[6].FindControl("txtAmt");
                                string str6 = box2.Text.ToString();
                                if ((str6 != "") && (str6 != "&nbsp;"))
                                {
                                    num4 += Convert.ToDouble(str6);
                                }
                            }
                            else if ((this.grdTransactionDetails.Items[k].Cells[5].Text != "") && (this.grdTransactionDetails.Items[k].Cells[5].Text != "&nbsp;"))
                            {
                                num4 += Convert.ToDouble(this.grdTransactionDetails.Items[k].Cells[5].Text);
                            }
                            if (k == (this.grdTransactionDetails.Items.Count - 1))
                            {
                                BillTransactionDAO ndao2 = new BillTransactionDAO();
                                string str7 = this.grdTransactionDetails.Items[k].Cells[2].Text.ToString();
                                string str8 = this.grdTransactionDetails.Items[k].Cells[15].Text.ToString();
                                string str9 = ndao2.GetProcID(this.txtCompanyID.Text, str7);
                                string str10 = ndao2.GetLimit(this.txtCompanyID.Text, str8, str9);
                                if (str10 != "")
                                {
                                    if (Convert.ToDouble(str10) < num4)
                                    {
                                        this.grdTransactionDetails.Items[k].Cells[10].Text = str10;
                                    }
                                    else
                                    {
                                        this.grdTransactionDetails.Items[k].Cells[10].Text = num4.ToString();
                                    }
                                }
                                num4 = 0.0;
                            }
                            else if (this.grdTransactionDetails.Items[k].Cells[1].Text != this.grdTransactionDetails.Items[k + 1].Cells[1].Text)
                            {
                                BillTransactionDAO ndao3 = new BillTransactionDAO();
                                string str11 = this.grdTransactionDetails.Items[k].Cells[2].Text.ToString();
                                string str12 = this.grdTransactionDetails.Items[k].Cells[15].Text.ToString();
                                string str13 = ndao3.GetProcID(this.txtCompanyID.Text, str11);
                                string str14 = ndao3.GetLimit(this.txtCompanyID.Text, str12, str13);
                                if (str14 != "")
                                {
                                    if (Convert.ToDouble(str14) < num4)
                                    {
                                        this.grdTransactionDetails.Items[k].Cells[10].Text = str14;
                                    }
                                    else
                                    {
                                        this.grdTransactionDetails.Items[k].Cells[10].Text = num4.ToString();
                                    }
                                }
                                num4 = 0.0;
                            }
                        }
                    }
                }
                DataSet set2 = new DataSet();
                set2 = new Bill_Sys_AssociateDiagnosisCodeBO().GetCaseDiagnosisCode(this.txtCaseID.Text, str, this.txtCompanyID.Text);
                if (set2.Tables[0].Rows.Count == 0)
                {
                    num = 2;
                }
                else
                {
                    this.lstDiagnosisCodes.DataSource = set2.Tables[0];
                    this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                    this.lstDiagnosisCodes.DataValueField = "CODE";
                    this.lstDiagnosisCodes.DataBind();
                }
                switch (num)
                {
                    case 1:
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "alert('No Procedure Code for visit. Cannot generate bill');", true);
                        this.grdTransactionDetails.DataSource = null;
                        this.grdTransactionDetails.DataBind();
                        if (!(((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1"))
                        {
                            break;
                        }
                        this.grdTransactionDetails.Columns[5].Visible = false;
                        this.grdTransactionDetails.Columns[6].Visible = true;
                        return;

                    case 2:
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "ss", "alert('No diagnosis codes associated with speciality. Cannot generate bill');", true);
                        this.grdTransactionDetails.DataSource = null;
                        this.grdTransactionDetails.DataBind();
                        if (!(((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1"))
                        {
                            goto Label_0BC9;
                        }
                        this.grdTransactionDetails.Columns[5].Visible = false;
                        this.grdTransactionDetails.Columns[6].Visible = true;
                        return;

                    default:
                        goto Label_0BF9;
                }
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
                return;
                Label_0BC9:
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
                return;
                Label_0BF9:
                this.saveQuickBills(str);
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + this.pdfpath.ToString() + "'); ", true);
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

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        ArrayList list = new ArrayList();
        if (this.Session["DELETED_PROC_CODES"] != null)
        {
            list = (ArrayList)this.Session["DELETED_PROC_CODES"];
        }
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("SZ_MODIFIER");
            table.Columns.Add("I_EventID");
            table.Columns.Add("SZ_VISIT_TYPE");
            table.Columns.Add("BT_IS_LIMITE");
            table.Columns.Add("SZ_MODIFIER_CODE");
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    DataRow row = table.NewRow();
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        row["FLT_AMOUNT"] = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                    }
                    if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                    }
                    row["SZ_TYPE_CODE_ID"] = item.Cells[9].Text.ToString();
                    row["FLT_GROUP_AMOUNT"] = item.Cells[10].Text.ToString();
                    row["I_GROUP_AMOUNT_ID"] = item.Cells[11].Text.ToString();
                    row["I_EventID"] = item.Cells[14].Text.ToString();
                    row["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                    row["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                    row["SZ_MODIFIER"] = item.Cells[12].Text.ToString();
                    row["SZ_MODIFIER_CODE"] = item.Cells[17].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                {
                    if ((this.grdTransactionDetails.Items[i].Cells[0].Text != "") && (this.grdTransactionDetails.Items[i].Cells[0].Text != "&nbsp;"))
                    {
                        CheckBox box = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box.Checked)
                        {
                            string text = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str2 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                if ((str2 == table.Rows[j][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(text), Convert.ToDateTime(table.Rows[j][1].ToString())) == 0))
                                {
                                    list.Add(table.Rows[j][0].ToString());
                                    table.Rows[j].Delete();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        CheckBox box2 = (CheckBox)this.grdTransactionDetails.Items[i].FindControl("chkSelect");
                        if (box2.Checked)
                        {
                            string str3 = this.grdTransactionDetails.Items[i].Cells[1].Text;
                            string str4 = this.grdTransactionDetails.Items[i].Cells[3].Text;
                            for (int k = 0; k < table.Rows.Count; k++)
                            {
                                if ((str4 == table.Rows[k][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(str3), Convert.ToDateTime(table.Rows[k][1].ToString())) == 0))
                                {
                                    list.Add(table.Rows[k][0].ToString());
                                    table.Rows[k].Delete();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            double num4 = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao = new BillTransactionDAO();
                string str5 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string procID = ndao.GetProcID(this.txtCompanyID.Text, str5);
                string str7 = ndao.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                if ((str7 != "") && (str7 != "NULL"))
                {
                    for (int m = 0; m < this.grdTransactionDetails.Items.Count; m++)
                    {
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            TextBox box3 = (TextBox)this.grdTransactionDetails.Items[m].Cells[6].FindControl("txtAmt");
                            string str8 = box3.Text.ToString();
                            if ((str8 != "") && (str8 != "&nbsp;"))
                            {
                                num4 += Convert.ToDouble(this.grdTransactionDetails.Items[m].Cells[5].Text);
                            }
                        }
                        else if ((this.grdTransactionDetails.Items[m].Cells[5].Text != "") && (this.grdTransactionDetails.Items[m].Cells[5].Text != "&nbsp;"))
                        {
                            num4 += Convert.ToDouble(this.grdTransactionDetails.Items[m].Cells[5].Text);
                        }
                        if (m == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao2 = new BillTransactionDAO();
                            string str9 = this.grdTransactionDetails.Items[m].Cells[2].Text.ToString();
                            string str10 = this.grdTransactionDetails.Items[m].Cells[15].Text.ToString();
                            string str11 = ndao2.GetProcID(this.txtCompanyID.Text, str9);
                            string str12 = ndao2.GetLimit(this.txtCompanyID.Text, str10, str11);
                            if (str12 != "")
                            {
                                if (Convert.ToDouble(str12) < num4)
                                {
                                    this.grdTransactionDetails.Items[m].Cells[10].Text = str12;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[m].Cells[10].Text = num4.ToString();
                                }
                            }
                            num4 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[m].Cells[1].Text != this.grdTransactionDetails.Items[m + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str13 = this.grdTransactionDetails.Items[m].Cells[2].Text.ToString();
                            string str14 = this.grdTransactionDetails.Items[m].Cells[15].Text.ToString();
                            string str15 = ndao3.GetProcID(this.txtCompanyID.Text, str13);
                            string str16 = ndao3.GetLimit(this.txtCompanyID.Text, str14, str15);
                            if (str16 != "")
                            {
                                if (Convert.ToDouble(str16) < num4)
                                {
                                    this.grdTransactionDetails.Items[m].Cells[10].Text = str16;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[m].Cells[10].Text = num4.ToString();
                                }
                            }
                            num4 = 0.0;
                        }
                    }
                }
            }
            this.Session["DELETED_PROC_CODES"] = list;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //ArrayList list;
            //this._saveOperation = new SaveOperation();
            //this._saveOperation.WebPage=this.Page;
            //this._saveOperation.Xml_File="  .xml";
            //this._saveOperation.SaveMethod();

            ArrayList arBills = new ArrayList();
            arBills.Add(this.txtCaseID.Text);
            arBills.Add(this.txtBillDate.Text);
            arBills.Add(this.txtCompanyID.Text);
            arBills.Add(extddlDoctor.Text);
            arBills.Add(ddlType.SelectedValue);



            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            string latestBillID = this._bill_Sys_BillingCompanyDetails_BO.GetLatestBillID(this.txtCompanyID.Text.ToString());
            this.txtBillID.Text = latestBillID;
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();

            if (this.grdAllReports.Visible)
            {
                Bill_Sys_Calender calender = new Bill_Sys_Calender();
                foreach (DataGridItem item in this.grdAllReports.Items)
                {
                    if (((CheckBox)item.Cells[0].FindControl("chkselect")).Checked)
                    {

                        //list2.Add(Convert.ToInt32(item.Cells[4].Text));
                        //list2.Add("1");
                        //list2.Add("2");
                        //list2.Add(this.txtBillID.Text);
                        //list2.Add(this.txtBillDate.Text);
                        calender.UPDATE_Event_Status(list2);

                        UpdateEventStatus objUpdateEventStatus = new UpdateEventStatus();
                        objUpdateEventStatus.I_EVENT_ID = Convert.ToInt32(item.Cells[4].Text);
                        objUpdateEventStatus.BT_STATUS = 1;
                        objUpdateEventStatus.I_STATUS = 2;
                        objUpdateEventStatus.SZ_BILL_NUMBER = "";
                        objUpdateEventStatus.DT_BILL_DATE = Convert.ToDateTime(txtBillDate.Text);
                        list2.Add(objUpdateEventStatus);

                        foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                        {
                            if (item.Cells[2].Text == item2.Cells[1].Text)
                            {
                                //list2 = new ArrayList();
                                //list2.Add(item2.Cells[12].Text);
                                //list2.Add(Convert.ToInt32(item.Cells[4].Text));
                                //list2.Add("2");
                                //  calender.Save_Event_RefferPrcedure(list2);
                                UpdateEventStatus objEventStatus = new UpdateEventStatus();
                                objEventStatus.sz_procode_id = item2.Cells[12].Text;
                                objEventStatus.I_EVENT_ID = Convert.ToInt32(item.Cells[4].Text);
                                objEventStatus.I_STATUS = 2;
                                list3.Add(objEventStatus);
                            }
                        }
                        continue;
                    }
                }
            }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = latestBillID;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;

            //this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);

            //this.objCaseDetailsBO = new CaseDetailsBO();
            //string patientID = this.objCaseDetailsBO.GetPatientID(latestBillID);
            //if (this.objCaseDetailsBO.GetCaseType(latestBillID) == "WC000000000000000001")
            //{
            //    this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
            //    if (this.grdLatestBillTransaction.Items.Count == 0)
            //    {
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
            //    }
            //    else if (this.grdLatestBillTransaction.Items.Count >= 1)
            //    {
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), latestBillID, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), latestBillID, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), latestBillID, patientID);
            //        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), latestBillID, patientID);
            //    }
            //}
            ArrayList list4 = new ArrayList();

            DataTable dtProcCode = new System.Data.DataTable();
            dtProcCode.Columns.Add("sz_type_code_id");
            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                DataRow drRow = dtProcCode.NewRow();
                drRow["sz_type_code_id"] = item3.Cells[11].Text.ToString();
                dtProcCode.Rows.Add(drRow);
            }
            // int iProcCount = 0;
            //CyclicCode objCyclicCode = new CyclicCode();
            //if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CYCLIC_PROCEDURE_CODE == "1")
            //{
            //    objCyclicCode = GetCyclicCode(txtCaseID.Text, txtCompanyID.Text, szInuranceID, szSpecilaty, dtProcCode);
            //    iProcCount = objCyclicCode.i_Count + 1;
            //}

            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                if (((!(item3.Cells[1].Text.ToString() != "") || !(item3.Cells[1].Text.ToString() != "&nbsp;")) || (!(item3.Cells[3].Text.ToString() != "") || !(item3.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item3.Cells[4].Text.ToString() != "") || !(item3.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                BillProcedureCodeEO billProcedureCodeEO = new BillProcedureCodeEO();

                //this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                //list = new ArrayList();
                billProcedureCodeEO.SZ_PROCEDURE_ID = item3.Cells[2].Text.ToString();
                // list.Add(item3.Cells[2].Text.ToString());

                if (item3.Cells[7].Text.ToString() != "&nbsp;")
                {
                    // list.Add(item3.Cells[7].Text.ToString());
                    billProcedureCodeEO.FL_AMOUNT = item3.Cells[7].Text.ToString();
                }
                else
                {
                    // list.Add(0);
                    billProcedureCodeEO.FL_AMOUNT = "0";
                }
                // list.Add(latestBillID);
                billProcedureCodeEO.SZ_BILL_NUMBER = "";

                //list.Add(Convert.ToDateTime(item3.Cells[1].Text.ToString()));
                billProcedureCodeEO.DT_DATE_OF_SERVICE = Convert.ToDateTime(item3.Cells[1].Text.ToString());

                //list.Add(((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                billProcedureCodeEO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                //list.Add(((TextBox) item3.Cells[7].FindControl("txtUnit")).Text.ToString());
                billProcedureCodeEO.I_UNIT = ((TextBox)item3.Cells[7].FindControl("txtUnit")).Text.ToString();

                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    // list.Add(((TextBox) item3.Cells[6].FindControl("txtAmt")).Text.ToString());
                    billProcedureCodeEO.FLT_PRICE = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                    billProcedureCodeEO.DOCT_AMOUNT = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                    // list.Add(((TextBox) item3.Cells[6].FindControl("txtAmt")).Text.ToString());
                    billProcedureCodeEO.FLT_FACTOR = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    //  list.Add(item3.Cells[5].Text.ToString());
                    billProcedureCodeEO.FLT_PRICE = item3.Cells[5].Text.ToString();
                    billProcedureCodeEO.DOCT_AMOUNT = item3.Cells[5].Text.ToString();
                    //  list.Add(item3.Cells[5].Text.ToString());
                    billProcedureCodeEO.FLT_FACTOR = item3.Cells[5].Text.ToString();
                }
                //   list.Add(item3.Cells[10].Text.ToString());
                if ((item3.Cells[9].Text.ToString() != "&nbsp;") && (item3.Cells[9].Text.ToString() != ""))
                {
                    // list.Add(item3.Cells[9].Text.ToString());
                    billProcedureCodeEO.PROC_AMOUNT = item3.Cells[9].Text.ToString();
                }
                else
                {
                    //list.Add(0);
                    billProcedureCodeEO.PROC_AMOUNT = "0";
                }
                //  list.Add(this.hndDoctorID.Value.ToString());
                billProcedureCodeEO.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
                // list.Add(this.txtCaseID.Text);
                billProcedureCodeEO.SZ_CASE_ID = this.txtCaseID.Text;

                // list.Add(item3.Cells[11].Text.ToString());
                billProcedureCodeEO.SZ_TYPE_CODE_ID = item3.Cells[11].Text.ToString();

                //list.Add(item3.Cells[12].Text.ToString());
                billProcedureCodeEO.FLT_GROUP_AMOUNT = item3.Cells[12].Text.ToString();

                //list.Add(item3.Cells[13].Text.ToString());
                billProcedureCodeEO.I_GROUP_AMOUNT_ID = item3.Cells[13].Text.ToString();

                //list.Add(item3.Cells[14].Text.ToString());
                billProcedureCodeEO.SZ_PATIENT_TREATMENT_ID = item3.Cells[14].Text.ToString();
                list4.Add(billProcedureCodeEO);
                //this._bill_Sys_BillTransaction.SaveTransactionData(list);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            //  this._bill_Sys_BillTransaction.DeleteTransactionDiagnosis(latestBillID);
            ArrayList list5 = new ArrayList();
            foreach (ListItem item4 in this.lstDiagnosisCodes.Items)
            {
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                BillDiagnosisCodeEO billDiagnosisCodeEO = new BillDiagnosisCodeEO();

                //list.Add(item4.Value.ToString());
                billDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID = item4.Value.ToString();
                //    list.Add(latestBillID);
                //  this._bill_Sys_BillTransaction.SaveTransactionDiagnosis(list);
                list5.Add(billDiagnosisCodeEO);
            }
            Result objResult = new Result();
            Referral_Bill_Transaction objReferral_Bill_Transaction = new Referral_Bill_Transaction();
            objResult = objReferral_Bill_Transaction.saveBillTransaction(arBills, list2, list3, _DAO_NOTES_EO, list4, list5);
            this.objCaseDetailsBO = new CaseDetailsBO();
            string patientID = this.objCaseDetailsBO.GetPatientID(objResult.bill_no);
            if (this.objCaseDetailsBO.GetCaseType(objResult.bill_no) == "WC000000000000000001")
            {
                this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                if (this.grdLatestBillTransaction.Items.Count == 0)
                {
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                }
                else if (this.grdLatestBillTransaction.Items.Count >= 1)
                {
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), objResult.bill_no, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), objResult.bill_no, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), objResult.bill_no, patientID);
                    this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), objResult.bill_no, patientID);
                }
            }

            this.BindLatestTransaction();
            this.ClearControl();
            this.usrMessage.PutMessage(" Bill Saved successfully ! ");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            message = " Bill Saved successfully ! ";
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.GenerateAddedBillPDF(objResult.bill_no, this._bill_Sys_BillTransaction.GetDoctorSpeciality(objResult.bill_no, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
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
    string message;
    protected void btnSaveWithTransaction_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        BillTransactionDAO ndao = new BillTransactionDAO();
        Result result = new Result();
        try
        {

            string str = "";
            BillTransactionEO neo = new BillTransactionEO();
            neo.SZ_CASE_ID = this.txtCaseID.Text;
            neo.SZ_COMPANY_ID = this.txtCompanyID.Text;
            neo.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
            neo.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
            neo.SZ_TYPE = this.ddlType.Text;
            neo.SZ_TESTTYPE = "";
            neo.FLAG = "ADD";
            neo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            if (this.grdCompleteVisit.Visible)
            {
                new Bill_Sys_Calender();
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    string text = item.Cells[14].Text;
                    if (list.Count != 0)
                    {
                        string str3 = "0";
                        for (int j = 0; j < list.Count; j++)
                        {
                            EventEO teo = new EventEO();
                            teo = (EventEO)list[j];
                            if ((text == teo.I_EVENT_ID) && (teo.I_EVENT_ID != "&nbsp;"))
                            {
                                str3 = "1";
                            }
                        }
                        if (str3 == "0")
                        {
                            EventEO teo2 = new EventEO();
                            teo2.I_EVENT_ID = text;
                            teo2.BT_STATUS = "1";
                            teo2.I_STATUS = "2";
                            teo2.SZ_BILL_NUMBER = "";
                            teo2.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                            list.Add(teo2);
                        }
                    }
                    else
                    {
                        EventEO teo3 = new EventEO();
                        teo3.I_EVENT_ID = text;
                        teo3.BT_STATUS = "1";
                        teo3.I_STATUS = "2";
                        teo3.SZ_BILL_NUMBER = "";
                        teo3.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                        list.Add(teo3);
                    }
                    int num2 = 0;
                    for (int i = 0; i < list2.Count; i++)
                    {
                        EventRefferProcedureEO eeo = new EventRefferProcedureEO();
                        eeo = (EventRefferProcedureEO)list2[i];
                        if (eeo.I_EVENT_ID == text)
                        {
                            num2 = 1;
                            break;
                        }
                    }
                    if (num2 != 1)
                    {
                        foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                        {
                            if ((item2.Cells[1].Text != "") && item.Cells[14].Text.Equals(item2.Cells[14].Text))
                            {
                                EventRefferProcedureEO eeo2 = new EventRefferProcedureEO();
                                eeo2.SZ_PROC_CODE = item2.Cells[9].Text;
                                eeo2.I_EVENT_ID = item2.Cells[14].Text;
                                eeo2.SZ_MODIFIER_ID = item2.Cells[17].Text;
                                eeo2.I_STATUS = "2";
                                list2.Add(eeo2);
                            }
                        }
                        continue;
                    }
                }
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            ArrayList list3 = new ArrayList();
            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                if (((!(item3.Cells[1].Text.ToString() != "") || !(item3.Cells[1].Text.ToString() != "&nbsp;")) || (!(item3.Cells[3].Text.ToString() != "") || !(item3.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item3.Cells[4].Text.ToString() != "") || !(item3.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                BillProcedureCodeEO eeo3 = new BillProcedureCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo3.SZ_PROCEDURE_ID = item3.Cells[2].Text.ToString();
                this.hdnSpeciality.Value = item3.Cells[3].Text.ToString();
                if (item3.Cells[7].Text.ToString() != "&nbsp;")
                {
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        eeo3.FL_AMOUNT = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        eeo3.FL_AMOUNT = item3.Cells[5].Text.ToString();
                    }
                }
                else
                {
                    eeo3.FL_AMOUNT = "0";
                }
                eeo3.SZ_BILL_NUMBER = "";
                eeo3.DT_DATE_OF_SERVICE = Convert.ToDateTime(item3.Cells[1].Text.ToString());
                eeo3.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                eeo3.I_UNIT = ((TextBox)item3.Cells[7].FindControl("txtUnit")).Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    eeo3.FLT_PRICE = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    eeo3.FLT_PRICE = item3.Cells[5].Text.ToString();
                }
                eeo3.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
                eeo3.SZ_CASE_ID = this.txtCaseID.Text;
                eeo3.SZ_TYPE_CODE_ID = item3.Cells[9].Text.ToString();
                //123
                if (item3.Cells[10].Text.ToString() != "&nbsp;")
                {
                    eeo3.FLT_GROUP_AMOUNT = item3.Cells[10].Text.ToString();
                }
                else
                {
                    eeo3.FLT_GROUP_AMOUNT = "";
                }
                if (((item3.Cells[11].Text.ToString() != "&nbsp;") && (item3.Cells[11].Text.ToString() != "&nbsp;")) && (item3.Cells[11].Text.ToString() != "&nbsp;"))
                {
                    eeo3.I_GROUP_AMOUNT_ID = item3.Cells[11].Text.ToString();
                }
                else
                {
                    eeo3.I_GROUP_AMOUNT_ID = "";
                }
                eeo3.SZ_MODIFIER_ID = item3.Cells[17].Text.ToString();
                list3.Add(eeo3);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList list4 = new ArrayList();
            foreach (ListItem item4 in this.lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO eeo4 = new BillDiagnosisCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo4.SZ_DIAGNOSIS_CODE_ID = item4.Value.ToString();
                list4.Add(eeo4);
            }


            ServerConnection currentConnection = ndao.BeginBillTranaction();
            result = ndao.SaveBillTransactions(neo, list, list2, list3, list4);
            if (result.msg_code == "ERR")
            {
                if (result.bill_no != string.Empty)
                    ndao.DeleteBillRecord(result.bill_no, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DELETE");
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
            else
            {
                this.txtBillID.Text = result.bill_no;
                str = this.txtBillID.Text;
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                string patientID = this.objCaseDetailsBO.GetPatientID(str, currentConnection);
                string caseType = this.objCaseDetailsBO.GetCaseType(str, currentConnection);
                if (caseType == "WC000000000000000001")
                {
                    this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (this.grdLatestBillTransaction.Items.Count == 0)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    }
                    else if (this.grdLatestBillTransaction.Items.Count >= 1)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                    }
                }
                this.BindLatestTransaction();
                this.BindVisitCompleteGrid();
                this.ClearControl();
                this.usrMessage.PutMessage(" Bill Saved successfully ! ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                message = " Bill Saved successfully ! ";

                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                if (caseType == "WC000000000000000002")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection), currentConnection);
                }
                if (caseType == "WC000000000000000003")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection), currentConnection);
                }
                else if (caseType == "WC000000000000000001")
                {
                    this.hdnWCPDFBillNumber.Value = str.ToString();
                    this.pnlPDFWorkerCompAdd.Visible = true;
                    this.pnlPDFWorkerCompAdd.Width = Unit.Pixel(100);
                    this.pnlPDFWorkerCompAdd.Height = Unit.Pixel(100);
                }
                if (caseType == "WC000000000000000004")
                {
                    string str5;
                    this.objNF3Template = new Bill_Sys_NF3_Template();
                    Lien lien = new Lien();
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection);
                    string str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.Session["TM_SZ_BILL_ID"] = str;
                    this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                    this.objVerification_Desc.sz_bill_no = str;
                    this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.objVerification_Desc.sz_flag = "BILL";
                    ArrayList list5 = new ArrayList();
                    ArrayList list6 = new ArrayList();
                    string str8 = "";
                    string str9 = "";
                    list5.Add(this.objVerification_Desc);
                    list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5, currentConnection);
                    if (list6.Contains("NFVER"))
                    {
                        str8 = "OLD";
                        str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + doctorSpeciality + "/";
                    }
                    else
                    {
                        str8 = "NEW";
                        str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
                    }
                    string str10 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set = new DataSet();
                    string str11 = "";
                    set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.Session["TM_SZ_BILL_ID"].ToString(), currentConnection);
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                        {
                            str11 = set.Tables[0].Rows[k]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str11 == "1")
                    {
                        string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                        Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                        this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection);
                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str9))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str9);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500);
                        }
                        str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500;
                        ArrayList list7 = new ArrayList();
                        if (str8 == "OLD")
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str9 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str9);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list7, currentConnection);
                        }
                        else
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str9 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str9);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list7.Add(list6[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list7, currentConnection);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        string str12 = this._MUVGenerateFunction.get_bt_include(str7, doctorSpeciality, "", "Speciality");
                        string str13 = this._MUVGenerateFunction.get_bt_include(str7, "", "WC000000000000000004", "CaseType");
                        if ((str12 == "True") && (str13 == "True"))
                        {
                            string str14 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                            string str15 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            string str16 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString(), currentConnection);
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str9 + lien.GenratePdfForLienWithMuv(str7, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str16, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str15, currentConnection), this.objNF3Template.getPhysicalPath() + str14 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500.Replace(".pdf", "_MER.pdf");
                            ArrayList list8 = new ArrayList();
                            if (str8 == "OLD")
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str9);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillPath(list8, currentConnection);
                            }
                            else
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str9);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list8.Add(list6[0].ToString());
                                this.objNF3Template.saveGeneratedBillPath_New(list8, currentConnection);
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                        else
                        {
                            str5 = lien.GenratePdfForLien(this.txtCompanyID.Text, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, currentConnection);
                        }
                    }

                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.open('" + str5 + "');", true);
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                }
                else if (caseType == "WC000000000000000007")
                {
                    string str5;
                    this.objNF3Template = new Bill_Sys_NF3_Template();
                    Employer lien = new Employer();
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection);
                    string str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.Session["TM_SZ_BILL_ID"] = str;
                    this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                    this.objVerification_Desc.sz_bill_no = str;
                    this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.objVerification_Desc.sz_flag = "BILL";
                    ArrayList list5 = new ArrayList();
                    ArrayList list6 = new ArrayList();
                    string str8 = "";
                    string str9 = "";
                    list5.Add(this.objVerification_Desc);
                    list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5, currentConnection);
                    if (list6.Contains("NFVER"))
                    {
                        str8 = "OLD";
                        str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + doctorSpeciality + "/";
                    }
                    else
                    {
                        str8 = "NEW";
                        str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
                    }
                    string str10 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set = new DataSet();
                    string str11 = "";
                    set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.Session["TM_SZ_BILL_ID"].ToString(), currentConnection);
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                        {
                            str11 = set.Tables[0].Rows[k]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str11 == "1")
                    {
                        string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                        Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                        this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection);

                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str9))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str9);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str10 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500);
                        }
                        str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500;
                        ArrayList list7 = new ArrayList();
                        if (str8 == "OLD")
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str9 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str9);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list7, currentConnection);
                        }
                        else
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str9 + this.str_1500);
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500);
                            list7.Add(str9);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("LN");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list7.Add(list6[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list7, currentConnection);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        string str12 = this._MUVGenerateFunction.get_bt_include(str7, doctorSpeciality, "", "Speciality");
                        string str13 = this._MUVGenerateFunction.get_bt_include(str7, "", "WC000000000000000007", "CaseType");
                        if ((str12 == "True") && (str13 == "True"))
                        {
                            string str14 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                            string str15 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            string str16 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString(), currentConnection);
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str9 + lien.GenratePdfForEmployerWithMuv(str7, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str16, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str15), this.objNF3Template.getPhysicalPath() + str14 + this.str_1500, this.objNF3Template.getPhysicalPath() + str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            str5 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str9 + this.str_1500.Replace(".pdf", "_MER.pdf");
                            ArrayList list8 = new ArrayList();
                            if (str8 == "OLD")
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str9);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillPath(list8, currentConnection);
                            }
                            else
                            {
                                list8.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list8.Add(str9 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list8.Add(this.Session["TM_SZ_CASE_ID"]);
                                list8.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list8.Add(str9);
                                list8.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list8.Add(doctorSpeciality);
                                list8.Add("LN");
                                list8.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list8.Add(list6[0].ToString());
                                this.objNF3Template.saveGeneratedBillPath_New(list8, currentConnection);
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                        else
                        {
                            str5 = lien.GenratePdfForEmployer(this.txtCompanyID.Text, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, currentConnection), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, currentConnection);
                        }
                    }

                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str5 + "');", true);
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                }
            }
            // ndao.CommitBillTranaction();
        }
        catch (Exception ex)
        {
            ndao.DeleteBillRecord(result.bill_no, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DELETE");
            // ndao.RollBackBillTranaction();
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

    protected void btnSeacrh_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.BindDiagnosisGrid(this.extddlDiagnosisType.Text, this.txtDiagonosisCode.Text, this.txtDescription.Text);
            //ScriptManager.RegisterStartupScript((Page) this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            if (this.Session["SZ_BILL_NUMBER"] != null)
            {
                this._editOperation.Primary_Value = this.Session["SZ_BILL_NUMBER"].ToString();
                this._editOperation.WebPage = this.Page;
                this._editOperation.Xml_File = "BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.Session["SZ_BILL_NUMBER"] = null;
                base.Response.Redirect("Bill_Sys_BillSearch.aspx", false);
            }
            else
            {
                this._editOperation.Primary_Value = this.Session["BillID"].ToString();
                this._editOperation.WebPage = this.Page;
                this._editOperation.Xml_File = "BillTransaction.xml";
                this._editOperation.UpdateMethod();
                this.usrMessage.PutMessage("Bill Updated successfully !");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                message = " Bill Updated successfully ! ";
            }
            this.BindLatestTransaction();
        }
        catch (SqlException)
        {
            this.usrMessage.PutMessage(" Bill Number already exists");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
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

    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        try
        {
            this.Session["BillID"] = this.Session["SZ_BILL_NUMBER"].ToString();
            if (this.Session["BillID"] != null)
            {
                BillTransactionEO neo = new BillTransactionEO();
                neo.SZ_BILL_NUMBER = this.Session["BillID"].ToString();
                neo.SZ_BILL_ID = this.Session["BillID"].ToString();
                neo.SZ_CASE_ID = this.txtCaseID.Text;
                neo.SZ_COMPANY_ID = this.txtCompanyID.Text;
                neo.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                neo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                neo.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
                neo.SZ_TYPE = this.ddlType.Text;
                neo.SZ_TESTTYPE = "";
                neo.FLAG = "UPDATE";
                new ArrayList();
                ArrayList list = new ArrayList();
                this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
                string str = this.Session["BillID"].ToString();
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    if (((!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;")) || (!(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;")))
                    {
                        continue;
                    }
                    BillProcedureCodeEO eeo = new BillProcedureCodeEO();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    if ((item.Cells[0].Text.ToString() != "") && (item.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        eeo.SZ_BILL_TXN_DETAIL_ID = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        eeo.SZ_BILL_TXN_DETAIL_ID = "";
                    }
                    eeo.SZ_PROCEDURE_ID = item.Cells[2].Text.ToString();
                    eeo.SZ_MODIFIER_ID = item.Cells[17].Text.ToString();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        if ((((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString() != "&nbsp;") && (((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString() != ""))
                        {
                            eeo.FL_AMOUNT = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                        }
                        else
                        {
                            eeo.FL_AMOUNT = "0";
                        }
                    }
                    else if (item.Cells[5].Text.ToString() != "&nbsp;")
                    {
                        eeo.FL_AMOUNT = item.Cells[5].Text.ToString();
                    }
                    else
                    {
                        eeo.FL_AMOUNT = "0";
                    }
                    eeo.SZ_BILL_NUMBER = str;
                    if ((item.Cells[1].Text.ToString() != "") && (item.Cells[1].Text.ToString() != "&nbsp;"))
                    {
                        eeo.DT_DATE_OF_SERVICE = Convert.ToDateTime(item.Cells[1].Text.ToString());
                    }
                    eeo.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    eeo.I_UNIT = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        eeo.FLT_PRICE = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        eeo.FLT_PRICE = item.Cells[5].Text.ToString();
                    }
                    this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                    if (item.Cells[10].Text.ToString() != "&nbsp;")
                    {
                        eeo.FLT_GROUP_AMOUNT = item.Cells[10].Text.ToString();
                    }
                    else
                    {
                        eeo.FLT_GROUP_AMOUNT = "";
                    }
                    if (((item.Cells[11].Text.ToString() != "&nbsp;") && (item.Cells[11].Text.ToString() != "&nbsp;")) && (item.Cells[11].Text.ToString() != "&nbsp;"))
                    {
                        eeo.I_GROUP_AMOUNT_ID = item.Cells[11].Text.ToString();
                    }
                    else
                    {
                        eeo.I_GROUP_AMOUNT_ID = "";
                    }
                    if ((item.Cells[0].Text.ToString() != "") && (item.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        eeo.SZ_TYPE_CODE_ID = item.Cells[9].Text.ToString();
                        eeo.FLAG = "UPDATE";
                        list.Add(eeo);
                    }
                    else
                    {
                        eeo.SZ_DOCTOR_ID = this.hndDoctorID.Value.ToString();
                        eeo.SZ_CASE_ID = this.txtCaseID.Text;
                        eeo.SZ_TYPE_CODE_ID = item.Cells[9].Text.ToString();
                        eeo.FLAG = "ADD";
                        list.Add(eeo);
                    }
                }
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                {
                    if ((item2.Cells[0].Text.ToString() != "") && (item2.Cells[0].Text.ToString() != "&nbsp;"))
                    {
                        if (!list3.Contains(item2.Cells[14].Text))
                        {
                            list3.Add(item2.Cells[14].Text);
                        }
                    }
                    else
                    {
                        if (!list3.Contains(item2.Cells[14].Text))
                        {
                            list3.Add(item2.Cells[14].Text);
                        }
                        EventRefferProcedureEO eeo2 = new EventRefferProcedureEO();
                        eeo2.SZ_EVENT_DATE = item2.Cells[1].Text;
                        eeo2.SZ_PROC_CODE = item2.Cells[9].Text;
                        eeo2.I_EVENT_ID = item2.Cells[14].Text;
                        eeo2.SZ_MODIFIER_ID = item2.Cells[17].Text;
                        eeo2.I_STATUS = "2";
                        list2.Add(eeo2);
                    }
                }
                ArrayList list4 = new ArrayList();
                foreach (ListItem item3 in this.lstDiagnosisCodes.Items)
                {
                    BillDiagnosisCodeEO eeo3 = new BillDiagnosisCodeEO();
                    this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                    ArrayList list5 = new ArrayList();
                    list5.Add(item3.Value.ToString());
                    eeo3.SZ_DIAGNOSIS_CODE_ID = item3.Value.ToString();
                    list4.Add(eeo3);
                    list5.Add(str);
                }
                ArrayList list6 = new ArrayList();
                list6 = (ArrayList)this.Session["DELETED_PROC_CODES"];
                BillTransactionDAO ndao = new BillTransactionDAO();
                Result result = new Result();
                result = ndao.UpdateBillTransactions(neo, list, list4, list6, list2, list3);
                this.Session["DELETED_PROC_CODES"] = null;
                if (result.msg_code == "SCC")
                {
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_UPDATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    double num = 0.0;
                    if (this.grdTransactionDetails.Items.Count > 0)
                    {
                        BillTransactionDAO ndao2 = new BillTransactionDAO();
                        string str2 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                        this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                        string procID = ndao2.GetProcID(this.txtCompanyID.Text, str2);
                        string str4 = ndao2.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                        if ((str4 != "") && (str4 != "NULL"))
                        {
                            for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                            {
                                this.grdTransactionDetails.Items[i].Cells[10].Text = "";
                                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                                {
                                    string text = "";
                                    TextBox box = (TextBox)this.grdTransactionDetails.Items[i].Cells[6].FindControl("txtAmt");
                                    text = box.Text;
                                    if ((text != "") && (text != "&nbsp;"))
                                    {
                                        num += Convert.ToDouble(text);
                                    }
                                }
                                else if ((this.grdTransactionDetails.Items[i].Cells[5].Text != "") && (this.grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;"))
                                {
                                    num += Convert.ToDouble(this.grdTransactionDetails.Items[i].Cells[5].Text);
                                }
                                if (i == (this.grdTransactionDetails.Items.Count - 1))
                                {
                                    BillTransactionDAO ndao3 = new BillTransactionDAO();
                                    string str6 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string str7 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                                    string str8 = ndao3.GetProcID(this.txtCompanyID.Text, str6);
                                    string str9 = ndao3.GetLimit(this.txtCompanyID.Text, str7, str8);
                                    if (str9 != "")
                                    {
                                        if (Convert.ToDouble(str9) < num)
                                        {
                                            this.grdTransactionDetails.Items[i].Cells[10].Text = str9;
                                        }
                                        else
                                        {
                                            this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                                        }
                                    }
                                    num = 0.0;
                                }
                                else if (this.grdTransactionDetails.Items[i].Cells[1].Text != this.grdTransactionDetails.Items[i + 1].Cells[1].Text)
                                {
                                    BillTransactionDAO ndao4 = new BillTransactionDAO();
                                    string str10 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                                    string str11 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                                    string str12 = ndao4.GetProcID(this.txtCompanyID.Text, str10);
                                    string str13 = ndao4.GetLimit(this.txtCompanyID.Text, str11, str12);
                                    if (str13 != "")
                                    {
                                        if (Convert.ToDouble(str13) < num)
                                        {
                                            this.grdTransactionDetails.Items[i].Cells[10].Text = str13;
                                        }
                                        else
                                        {
                                            this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                                        }
                                    }
                                    num = 0.0;
                                }
                            }
                        }
                    }
                    this.usrMessage.PutMessage(" Bill Updated successfully ! ");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
                else
                {
                    this.usrMessage.PutMessage(result.msg);
                    this.usrMessage.SetMessageType(0);
                    this.usrMessage.Show();
                }
            }
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            this.BindDoctorsGrid(this.Session["BillID"].ToString());
            this.BindLatestTransaction();
        }
        catch (SqlException)
        {
            this.usrMessage.PutMessage(" Bill Number already exists");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataRow row;
            if (this.lstDiagnosisCodes.Items.Count == 0)
            {
                this.lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
                this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value, this.txtCompanyID.Text).Tables[0];
                this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                this.lstDiagnosisCodes.DataValueField = "CODE";
                this.lstDiagnosisCodes.DataBind();
                this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            }
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("SZ_MODIFIER");
            table.Columns.Add("I_EventID");
            table.Columns.Add("SZ_VISIT_TYPE");
            table.Columns.Add("BT_IS_LIMITE");
            table.Columns.Add("SZ_MODIFIER_CODE");
            int num = 0;
            string str = "";
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                if (((!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;")) || (!(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                row = table.NewRow();
                if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                {
                    row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                }
                else
                {
                    row["SZ_BILL_TXN_DETAIL_ID"] = "";
                }
                row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                str = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    row["FLT_AMOUNT"] = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                }
                if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                {
                    row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                }
                row["SZ_TYPE_CODE_ID"] = item.Cells[9].Text.ToString();
                BillTransactionDAO ndao = new BillTransactionDAO();
                string str2 = item.Cells[2].Text.ToString();
                string str3 = item.Cells[15].Text.ToString();
                string procID = ndao.GetProcID(this.txtCompanyID.Text, str2);
                if (ndao.GetLimit(this.txtCompanyID.Text, str3, procID) != "")
                {
                    row["FLT_GROUP_AMOUNT"] = "";
                }
                else
                {
                    row["FLT_GROUP_AMOUNT"] = item.Cells[10].Text.ToString();
                }
                row["I_GROUP_AMOUNT_ID"] = item.Cells[11].Text.ToString();
                row["I_EventID"] = item.Cells[14].Text.ToString();
                row["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                row["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                item.Cells[13].Text.ToString();
                row["SZ_MODIFIER"] = item.Cells[12].Text.ToString();
                row["SZ_MODIFIER_CODE"] = item.Cells[17].Text.ToString();
                table.Rows.Add(row);
            }
            this.txtDateOfservice.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            foreach (DataGridItem item2 in this.grdAllReports.Items)
            {
                if (((CheckBox)item2.Cells[0].FindControl("chkselect")).Checked)
                {
                    foreach (DataGridItem item3 in this.grdProcedure.Items)
                    {
                        CheckBox box = (CheckBox)item3.FindControl("chkselect");
                        CheckBox box1 = (CheckBox)item2.FindControl("chkLimit");
                        num = 1;
                        string str5 = item2.Cells[5].Text.ToString();
                        if (box.Checked)
                        {
                            row = table.NewRow();
                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                            row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item2.Cells[2].Text.ToString()).ToShortDateString();
                            row["SZ_PROCEDURE_ID"] = item3.Cells[1].Text.ToString();
                            row["SZ_PROCEDURAL_CODE"] = item3.Cells[3].Text.ToString();
                            row["SZ_CODE_DESCRIPTION"] = item3.Cells[4].Text.ToString();
                            row["FLT_AMOUNT"] = item3.Cells[5].Text.ToString();
                            row["I_UNIT"] = "1";
                            row["SZ_TYPE_CODE_ID"] = item3.Cells[2].Text.ToString();
                            row["I_EventID"] = item2.Cells[4].Text.ToString();
                            row["SZ_VISIT_TYPE"] = str5;
                            if (num == 1)
                            {
                                row["BT_IS_LIMITE"] = "1";
                            }
                            else
                            {
                                row["BT_IS_LIMITE"] = "0";
                            }
                            table.Rows.Add(row);
                        }
                    }
                    continue;
                }
            }
            if (this.grdAllReports.Items.Count == 0)
            {
                if (this.grdCompleteVisit.Items.Count > 0)
                {
                    int num2 = 0;

                    foreach (DataGridItem item4 in this.grdCompleteVisit.Items)
                    {
                        CheckBox box2 = (CheckBox)item4.FindControl("chkSelectItem");
                        if (box2.Checked)
                        {
                            string text = item4.Cells[1].Text;
                            foreach (DataGridItem item5 in this.grdProcedure.Items)
                            {
                                CheckBox box3 = (CheckBox)item5.FindControl("chkselect");
                                CheckBox box8 = (CheckBox)item4.FindControl("chkLimit");
                                num = 1;
                                string str7 = item4.Cells[3].Text.ToString();
                                if (box3.Checked)
                                {
                                    for (int i = 0; i < table.Rows.Count; i++)
                                    {
                                        if ((table.Rows[i][0].ToString() == item5.Cells[1].Text) && (DateTime.Compare(Convert.ToDateTime(text), Convert.ToDateTime(table.Rows[i][1].ToString())) == 0))
                                        {
                                            num2 = 1;
                                            break;
                                        }
                                        else
                                        {
                                            num2 = 2;
                                        }
                                    }

                                    if (num2 == 2 || num2 == 0)
                                    {
                                        row = table.NewRow();
                                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                        row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(text).ToShortDateString();
                                        row["SZ_PROCEDURE_ID"] = item5.Cells[1].Text.ToString();
                                        row["SZ_PROCEDURAL_CODE"] = item5.Cells[3].Text.ToString();
                                        row["SZ_CODE_DESCRIPTION"] = item5.Cells[4].Text.ToString();
                                        row["FLT_AMOUNT"] = item5.Cells[5].Text.ToString();
                                        row["I_UNIT"] = "1";
                                        row["SZ_TYPE_CODE_ID"] = item5.Cells[2].Text.ToString();
                                        TextBox txtMod = (TextBox)item5.FindControl("txtModifier");

                                        if (txtMod.Text == "" || txtMod.Text == "&nbsp")
                                        {
                                            row["SZ_MODIFIER"] = "";
                                        }
                                        else
                                        {
                                            row["SZ_MODIFIER"] = txtMod.Text;
                                        }
                                        row["I_EventID"] = item4.Cells[7].Text;
                                        row["SZ_VISIT_TYPE"] = str7;
                                        if (num == 1)
                                        {
                                            row["BT_IS_LIMITE"] = "1";
                                        }
                                        else
                                        {
                                            row["BT_IS_LIMITE"] = "0";
                                        }
                                        if (txtMod.Text == "" || txtMod.Text == "&nbsp")
                                        {
                                            row["SZ_MODIFIER_CODE"] = "";
                                        }
                                        else
                                        {
                                            row["SZ_MODIFIER_CODE"] = txtMod.Text;
                                        }
                                        table.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (this.grdCompleteVisit.Visible)
                {
                    int num4 = 0;
                    foreach (DataGridItem item6 in this.grdTransactionDetails.Items)
                    {
                        string str8 = item6.Cells[1].Text;
                        if (str8 != "")
                        {
                            foreach (DataGridItem item7 in this.grdProcedure.Items)
                            {
                                CheckBox box4 = (CheckBox)item7.FindControl("chkselect");
                                if (box4.Checked)
                                {
                                    for (int j = 0; j < table.Rows.Count; j++)
                                    {
                                        if ((table.Rows[j][0].ToString() == item7.Cells[1].Text) && (DateTime.Compare(Convert.ToDateTime(str8), Convert.ToDateTime(table.Rows[j][1].ToString())) == 0))
                                        {
                                            num4 = 1;
                                            break;
                                        }
                                        else
                                        {
                                            num4 = 2;
                                        }
                                    }
                                    if (num4 == 2)
                                    {
                                        row = table.NewRow();
                                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                        row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(str8).ToShortDateString();
                                        row["SZ_PROCEDURE_ID"] = item7.Cells[1].Text.ToString();
                                        row["SZ_PROCEDURAL_CODE"] = item7.Cells[3].Text.ToString();
                                        row["SZ_CODE_DESCRIPTION"] = item7.Cells[4].Text.ToString();
                                        row["FLT_AMOUNT"] = item7.Cells[5].Text.ToString();
                                        row["I_UNIT"] = "1";
                                        row["SZ_TYPE_CODE_ID"] = item7.Cells[2].Text.ToString();
                                        row["I_EventID"] = item6.Cells[12].Text;
                                        table.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataGridItem item8 in this.grdProcedure.Items)
                    {
                        CheckBox box5 = (CheckBox)item8.FindControl("chkselect");
                        if (box5.Checked)
                        {
                            row = table.NewRow();
                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                            row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(str).ToShortDateString();
                            row["SZ_PROCEDURE_ID"] = item8.Cells[1].Text.ToString();
                            row["SZ_PROCEDURAL_CODE"] = item8.Cells[3].Text.ToString();
                            row["SZ_CODE_DESCRIPTION"] = item8.Cells[4].Text.ToString();
                            row["FLT_AMOUNT"] = item8.Cells[5].Text.ToString();
                            row["I_UNIT"] = "1";
                            row["SZ_TYPE_CODE_ID"] = item8.Cells[2].Text.ToString();
                            row["I_EventID"] = "";
                            table.Rows.Add(row);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem item9 in this.grdProcedure.Items)
                {
                    CheckBox box6 = (CheckBox)item9.FindControl("chkselect");
                    if (box6.Checked)
                    {
                        row = table.NewRow();
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                        row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(this.txtBillDate.Text).ToShortDateString();
                        row["SZ_PROCEDURE_ID"] = item9.Cells[1].Text.ToString();
                        row["SZ_PROCEDURAL_CODE"] = item9.Cells[3].Text.ToString();
                        row["SZ_CODE_DESCRIPTION"] = item9.Cells[4].Text.ToString();
                        row["FLT_AMOUNT"] = item9.Cells[5].Text.ToString();
                        row["I_UNIT"] = "1";
                        row["SZ_TYPE_CODE_ID"] = item9.Cells[2].Text.ToString();
                        row["I_EventID"] = "";
                        table.Rows.Add(row);
                    }
                }
            }
            new DataView();
            table.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            double num6 = 0.0;
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao2 = new BillTransactionDAO();
                string str9 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string str10 = ndao2.GetProcID(this.txtCompanyID.Text, str9);
                string str11 = ndao2.GET_IS_LIMITE(this.txtCompanyID.Text, str10);
                if ((str11 != "") && (str11 != "NULL"))
                {
                    for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                    {
                        if ((this.grdTransactionDetails.Items[k].Cells[5].Text != "") && (this.grdTransactionDetails.Items[k].Cells[5].Text != "&nbsp;"))
                        {
                            num6 += Convert.ToDouble(this.grdTransactionDetails.Items[k].Cells[5].Text);
                        }
                        if (k == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str12 = this.grdTransactionDetails.Items[k].Cells[2].Text.ToString();
                            string str13 = this.grdTransactionDetails.Items[k].Cells[15].Text.ToString();
                            string str14 = ndao3.GetProcID(this.txtCompanyID.Text, str12);
                            string str15 = ndao3.GetLimit(this.txtCompanyID.Text, str13, str14);
                            if (str15 != "")
                            {
                                if (Convert.ToDouble(str15) < num6)
                                {
                                    this.grdTransactionDetails.Items[k].Cells[10].Text = str15;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[k].Cells[10].Text = num6.ToString();
                                }
                            }
                            num6 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[k].Cells[1].Text != this.grdTransactionDetails.Items[k + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao4 = new BillTransactionDAO();
                            string str16 = this.grdTransactionDetails.Items[k].Cells[2].Text.ToString();
                            string str17 = this.grdTransactionDetails.Items[k].Cells[15].Text.ToString();
                            string str18 = ndao4.GetProcID(this.txtCompanyID.Text, str16);
                            string str19 = ndao4.GetLimit(this.txtCompanyID.Text, str17, str18);
                            if (str19 != "")
                            {
                                if (Convert.ToDouble(str19) < num6)
                                {
                                    this.grdTransactionDetails.Items[k].Cells[10].Text = str19;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[k].Cells[10].Text = num6.ToString();
                                }
                            }
                            num6 = 0.0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int m = 0; m < this.grdTransactionDetails.Items.Count; m++)
                {
                    TextBox box7 = (TextBox)this.grdTransactionDetails.Items[m].FindControl("txtUnit");
                    box7.Text = this.grdTransactionDetails.Items[m].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
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

    private void CalculateAmount(string id)
    {
        string Elmahid = string.Format("elmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            this._bill_Sys_Menu.GetICcodeAmount(id);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void checkLimit()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            for (int i = 0; i < this.grdCompleteVisit.Items.Count; i++)
            {
                string text = this.grdCompleteVisit.Items[i].Cells[6].Text;
                string str2 = this.grdCompleteVisit.Items[i].Cells[3].Text;
                BillTransactionDAO ndao = new BillTransactionDAO();
                if (ndao.GetLimit(this.txtCompanyID.Text, str2, text) == "")
                {
                    CheckBox box = (CheckBox)this.grdCompleteVisit.Items[i].Cells[10].FindControl("chkLimit");
                    box.Checked = false;
                    box.Enabled = false;
                }
                else
                {
                    CheckBox box2 = (CheckBox)this.grdCompleteVisit.Items[i].Cells[10].FindControl("chkLimit");
                    box2.Checked = true;
                    box2.Enabled = true;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void checksession()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.SessionCheck.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
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


            this.SessionCheck.Value = "";
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void chkSelectItem_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnAddServices.Enabled = false;
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            string doctorId = "";
            ArrayList list = new ArrayList();
            DataSet visitTypeList = new DataSet();
            Bill_Sys_Visit_BO t_bo = new Bill_Sys_Visit_BO();
            visitTypeList = t_bo.GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            foreach (DataGridItem item in this.grdCompleteVisit.Items)
            {
                CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelectItem");
                if (box.Checked)
                {
                    string text = item.Cells[3].Text;
                    for (int j = 0; j < visitTypeList.Tables[0].Rows.Count; j++)
                    {
                        if (text == visitTypeList.Tables[0].Rows[j][1].ToString())
                        {
                            ArrayList list2 = new ArrayList();
                            list2.Add(this.txtCompanyID.Text);
                            list2.Add(item.Cells[6].Text);
                            list2.Add(text);
                            list2.Add(item.Cells[7].Text);
                            DataTable procedureCodeList = new DataTable();
                            procedureCodeList = t_bo.GetProcedureCodeList(list2);
                            list.Add(procedureCodeList);
                        }
                    }
                    doctorId = item.Cells[8].Text;
                }
            }
            this.GetProcedureCode(doctorId);
            this.hndDoctorID.Value = doctorId;
            for (int i = 0; i < list.Count; i++)
            {
                DataTable table3 = (DataTable)list[i];
                foreach (DataRow row in table3.Rows)
                {
                    DataRow row2 = table.NewRow();
                    row2["SZ_BILL_TXN_DETAIL_ID"] = "";
                    row2["DT_DATE_OF_SERVICE"] = row.ItemArray.GetValue(5).ToString();
                    row2["SZ_PROCEDURE_ID"] = row.ItemArray.GetValue(0).ToString();
                    row2["SZ_PROCEDURAL_CODE"] = row.ItemArray.GetValue(2).ToString();
                    row2["SZ_CODE_DESCRIPTION"] = row.ItemArray.GetValue(3).ToString();
                    row2["FLT_AMOUNT"] = row.ItemArray.GetValue(4).ToString();
                    row2["I_UNIT"] = "1";
                    row2["SZ_TYPE_CODE_ID"] = row.ItemArray.GetValue(1).ToString();
                    table.Rows.Add(row2);
                }
            }
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                {
                    TextBox box2 = (TextBox)this.grdTransactionDetails.Items[k].FindControl("txtUnit");
                    box2.Text = this.grdTransactionDetails.Items[k].Cells[7].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.extddlDoctor.SelectedValue = "NA";
            this.txtBillDate.Text = "";
            this.grdTransactionDetails.DataSource = null;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            this.Session["SELECTED_DIA_PRO_CODE"] = null;
            this.Session["SZ_BILL_NUMBER"] = null;
            this.lstDiagnosisCodes.DataSource = null;
            this.lstDiagnosisCodes.DataBind();
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.lstDiagnosisCodes.Items.Clear();
            this.lstDiagnosisCodes.DataSource = null;
            this.lstDiagnosisCodes.DataBind();
            this.grdAllReports.DataSource = null;
            this.grdAllReports.DataBind();
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

    private void Createtable()
    {
        new ArrayList();
        DataTable table = new DataTable();
        table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
        table.Columns.Add("DT_DATE_OF_SERVICE");
        table.Columns.Add("SZ_PROCEDURE_ID");
        table.Columns.Add("SZ_PROCEDURAL_CODE");
        table.Columns.Add("SZ_CODE_DESCRIPTION");
        table.Columns.Add("FLT_AMOUNT");
        table.Columns.Add("I_UNIT");
        foreach (DataGridItem item in this.grdTransactionDetails.Items)
        {
            DataRow row = table.NewRow();
            if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
            {
                row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
            }
            else
            {
                row["SZ_BILL_TXN_DETAIL_ID"] = "";
            }
            row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
            row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
            row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
            row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                row["FLT_AMOUNT"] = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
            }
            else
            {
                row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
            }
            if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
            {
                row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
            }
            table.Rows.Add(row);
        }
        this.Session["SELECTED_SERVICES"] = table;
    }

    protected void extddlDoctor_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlDoctor.SelectedValue != "NA")
        {
            this.GetProcedureCode(this.extddlDoctor.SelectedValue);
            this.Session["TEMP_DOCTOR_ID"] = this.extddlDoctor.SelectedValue;
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") == "1")
            {
                this.BindGrid();
                this.lblDateOfService.Style.Add("visibility", "hidden");
                this.txtDateOfservice.Style.Add("visibility", "hidden");
                this.Image1.Style.Add("visibility", "hidden");
                this.lblGroupServiceDate.Style.Add("visibility", "hidden");
                this.txtGroupDateofService.Style.Add("visibility", "hidden");
                this.imgbtnDateofService.Style.Add("visibility", "hidden");
            }
            else
            {
                DataTable table = new DataTable();
                table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                table.Columns.Add("DT_DATE_OF_SERVICE");
                table.Columns.Add("SZ_PROCEDURE_ID");
                table.Columns.Add("SZ_PROCEDURAL_CODE");
                table.Columns.Add("SZ_CODE_DESCRIPTION");
                table.Columns.Add("FLT_AMOUNT");
                table.Columns.Add("FACTOR_AMOUNT");
                table.Columns.Add("FACTOR");
                table.Columns.Add("PROC_AMOUNT");
                table.Columns.Add("DOCT_AMOUNT");
                table.Columns.Add("I_UNIT");
                table.Columns.Add("SZ_TYPE_CODE_ID");
                table.Columns.Add("FLT_GROUP_AMOUNT");
                table.Columns.Add("I_GROUP_AMOUNT_ID");
                foreach (DataGridItem item in this.grdTransactionDetails.Items)
                {
                    if (((!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;")) || (!(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;")))
                    {
                        continue;
                    }
                    DataRow row = table.NewRow();
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString()) > 0M)
                    {
                        row["FACTOR_AMOUNT"] = ((Label)item.Cells[5].FindControl("lblPrice")).Text.ToString();
                    }
                    if (Convert.ToDecimal(((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString()) > 0M)
                    {
                        row["FACTOR"] = ((Label)item.Cells[5].FindControl("lblFactor")).Text.ToString();
                    }
                    row["FLT_AMOUNT"] = item.Cells[6].Text.ToString();
                    if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString();
                    }
                    row["PROC_AMOUNT"] = item.Cells[10].Text.ToString();
                    row["DOCT_AMOUNT"] = item.Cells[11].Text.ToString();
                    row["SZ_TYPE_CODE_ID"] = item.Cells[13].Text.ToString();
                    row["FLT_GROUP_AMOUNT"] = item.Cells[14].Text.ToString();
                    row["I_GROUP_AMOUNT_ID"] = item.Cells[15].Text.ToString();
                    table.Rows.Add(row);
                }
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    this.grdTransactionDetails.Columns[5].Visible = false;
                    this.grdTransactionDetails.Columns[6].Visible = true;
                }
                else
                {
                    this.grdTransactionDetails.Columns[5].Visible = true;
                    this.grdTransactionDetails.Columns[6].Visible = false;
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
        }
        else
        {
            this.grdAllReports.DataSource = null;
            this.grdAllReports.DataBind();
        }
    }

    protected void extddlProcedureCode_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
    }

    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string str = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = p_szBillNumber;
            this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str2 = "";
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
            if (list2.Contains("NFVER"))
            {
                str2 = "OLD";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
            }
            else
            {
                str2 = "NEW";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
            }
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
            {
                string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                string str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                this.objCaseDetailsBO = new CaseDetailsBO();
                DataSet set = new DataSet();
                string str8 = "";
                set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, this.Session["TM_SZ_BILL_ID"].ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        str8 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (str8 == "1")
                {

                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    ArrayList list3 = new ArrayList();

                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500);
                    }
                    str3 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str4 + this.str_1500;
                    if (str2 == "OLD")
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list3);
                    }
                    else
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list3.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list3);
                    }
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                    {
                        list3.Clear();
                        string companyId = string.Empty;
                        companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        CoverContract_pdf cpdf = new CoverContract_pdf();
                        string fileName = "Contract_" + this.str_1500;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                        bool isGenerated = false;
                        cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str4 + fileName, out isGenerated);
                        if (isGenerated)
                        {
                            list3.Clear();
                            if (str2 == "OLD")
                            {
                                list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list3.Add(str4 + fileName);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                list3.Add(fileName);
                                list3.Add(str4);
                                list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list3.Add(str);
                                list3.Add("NF");
                                list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillContractPath(list3);
                            }
                            else
                            {
                                list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list3.Add(str4 + fileName);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                list3.Add(fileName);
                                list3.Add(str4);
                                list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list3.Add(str);
                                list3.Add("NF");
                                list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list3.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillContractPath_New(list3);
                            }
                        }
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    this.BindLatestTransaction();

                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str3.ToString() + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                }
                else
                {
                    string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                    string str10 = configuration.getConfigurationSettings(str5, "GET_DIAG_PAGE_POSITION");
                    string str11 = configuration.getConfigurationSettings(str5, "DIAG_PAGE");
                    string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str16 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str9);
                    log.Debug("Bill Details PDF File : " + str16);
                    string str17 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    log.Debug("Page1 : " + str17);
                    string str18 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str17, str16);
                    string str19 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                    string str20 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str20, str, "", "Speciality");
                    string str21 = this._MUVGenerateFunction.get_bt_include(str20, "", "WC000000000000000002", "CaseType");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    }
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str18, this.objNF3Template.getPhysicalPath() + str6 + str19, this.objNF3Template.getPhysicalPath() + str6 + str19.Replace(".pdf", "_MER.pdf"));
                    string str22 = str19.Replace(".pdf", "_MER.pdf");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str22, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str22 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str23 = "";
                    str23 = str6 + str22;
                    log.Debug("GenereatedFileName : " + str23);
                    string str24 = "";
                    str24 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str23;
                    string path = this.objNF3Template.getPhysicalPath() + "/" + str23;
                    CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                    string str26 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    class2.initialize(str26);
                    if ((((class2 != null) && File.Exists(path)) && ((str11 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str10 == "CK_0000003") && ((str11 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                    {
                        str16 = path.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str27 = "";
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str23 = path.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else
                    {
                        str27 = str24.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    this.pdfpath = str27;
                    string str28 = "";
                    string[] strArray = str27.Split(new char[] { '/' });
                    ArrayList list4 = new ArrayList();
                    str27 = str27.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                    str28 = strArray[strArray.Length - 1].ToString();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + str28))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + str28, this.objNF3Template.getPhysicalPath() + str4 + str28);
                    }
                    if (str2 == "OLD")
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str28);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list4);
                    }
                    else
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str28);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list4.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list4);
                    }
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                    {
                        list4.Clear();
                        string companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        CoverContract_pdf cpdf = new CoverContract_pdf();
                        string fileName = "Contract_" + str28;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";

                        bool isGenerated = false;
                        cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str4 + fileName, out isGenerated);
                        list4.Clear();
                        if (isGenerated)
                        {
                            if (str2 == "OLD")
                            {
                                list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list4.Add(str4 + fileName);
                                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                list4.Add(fileName);
                                list4.Add(str4);
                                list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list4.Add(str);
                                list4.Add("NF");
                                list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillContractPath(list4);
                            }
                            else
                            {
                                list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list4.Add(str4 + fileName);
                                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                list4.Add(fileName);
                                list4.Add(str4);
                                list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list4.Add(str);
                                list4.Add("NF");
                                list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list4.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillContractPath_New(list4);
                            }
                        }
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str28;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    this.BindLatestTransaction();
                }
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
            {
                string str29;
                string companyName;
                Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str31 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str32 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str33 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str34 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str29 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }


                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str29, str31, str, companyName, str32, str33, str34) + "'); ", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);


            }
            new Bill_Sys_BillTransaction_BO();
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


    private void GenerateAddedBillPDF(string p_szBillNumber, string p_szSpeciality, ServerConnection conn)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            string str = p_szSpeciality;
            this.Session["TM_SZ_CASE_ID"] = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this.Session["TM_SZ_BILL_ID"] = p_szBillNumber;
            this.objNF3Template = new Bill_Sys_NF3_Template();
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = p_szBillNumber;
            this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str2 = "";
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list, conn);
            if (list2.Contains("NFVER"))
            {
                str2 = "OLD";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
            }
            else
            {
                str2 = "NEW";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
            }
            CaseDetailsBO sbo = new CaseDetailsBO();
            string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString(), conn) == "WC000000000000000002")
            {
                string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                string str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                this.objCaseDetailsBO = new CaseDetailsBO();
                DataSet set = new DataSet();
                string str8 = "";
                set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, this.Session["TM_SZ_BILL_ID"].ToString(), conn);
                if (set.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        str8 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                    }
                }
                if (str8 == "1")
                {
                    string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                    Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                    this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, conn);
                    ArrayList list3 = new ArrayList();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + this.str_1500, this.objNF3Template.getPhysicalPath() + str4 + this.str_1500);
                    }
                    str3 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str4 + this.str_1500;
                    if (str2 == "OLD")
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list3, conn);
                    }
                    else
                    {
                        list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list3.Add(str4 + this.str_1500);
                        list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list3.Add(this.Session["TM_SZ_CASE_ID"]);
                        list3.Add(this.str_1500);
                        list3.Add(str4);
                        list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list3.Add(str);
                        list3.Add("NF");
                        list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list3.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list3, conn);
                    }

                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                    {
                        list3.Clear();
                        string companyId = string.Empty;
                        companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        CoverContract_pdf cpdf = new CoverContract_pdf();
                        string fileName = "Contract_" + this.str_1500;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                        bool isGenerated = false;
                        cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str4 + fileName, out isGenerated);
                        if (isGenerated)
                        {
                            list3.Clear();
                            if (str2 == "OLD")
                            {
                                list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list3.Add(str4 + fileName);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                list3.Add(fileName);
                                list3.Add(str4);
                                list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list3.Add(str);
                                list3.Add("NF");
                                list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillContractPath(list3);
                            }
                            else
                            {
                                list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list3.Add(str4 + fileName);
                                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                list3.Add(fileName);
                                list3.Add(str4);
                                list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list3.Add(str);
                                list3.Add("NF");
                                list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list3.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillContractPath_New(list3);
                            }
                        }
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);



                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str3.ToString() + "'); ", true);
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                }
                else
                {
                    string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                    ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                    ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                    Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                    string str10 = configuration.getConfigurationSettings(str5, "GET_DIAG_PAGE_POSITION");
                    string str11 = configuration.getConfigurationSettings(str5, "DIAG_PAGE");
                    string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                    string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                    string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                    string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                    GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                    this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                    string str16 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str9, conn);
                    log.Debug("Bill Details PDF File : " + str16);
                    string str17 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), conn);
                    log.Debug("Page1 : " + str17);
                    string str18 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str17, str16);
                    string str19 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), conn);
                    string str20 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str20, str, "", "Speciality");
                    string str21 = this._MUVGenerateFunction.get_bt_include(str20, "", "WC000000000000000002", "CaseType");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString(), conn);
                    }
                    log.Debug(str18 + "merge : " + str19);
                    MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str18, this.objNF3Template.getPhysicalPath() + str6 + str19, this.objNF3Template.getPhysicalPath() + str6 + str19.Replace(".pdf", "_MER.pdf"));
                    string str22 = str19.Replace(".pdf", "_MER.pdf");
                    if ((this.bt_include == "True") && (str21 == "True"))
                    {
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str6 + str22, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str6 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str22 = this.str_1500.Replace(".pdf", "_MER.pdf");
                    }
                    string str23 = "";
                    str23 = str6 + str22;
                    log.Debug("GenereatedFileName : " + str23);
                    string str24 = "";
                    str24 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str23;
                    string path = this.objNF3Template.getPhysicalPath() + "/" + str23;
                    CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                    string str26 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                    class2.initialize(str26);
                    if ((((class2 != null) && File.Exists(path)) && ((str11 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString(), conn) >= 5))) && ((str10 == "CK_0000003") && ((str11 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString(), conn) != 5))))
                    {
                        str16 = path.Replace(".pdf", "_NewMerge.pdf");
                    }
                    string str27 = "";
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str23 = path.Replace(".pdf", "_New.pdf").ToString();
                    }
                    if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_NewMerge.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_NewMerge.pdf").ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                    {
                        str27 = str24.Replace(".pdf", "_New.pdf").ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.Replace(".pdf", "_New.pdf").ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    else
                    {
                        str27 = str24.ToString();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str24.ToString() + "'); ", true);
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
                    }
                    this.pdfpath = str27;
                    string str28 = "";
                    string[] strArray = str27.Split(new char[] { '/' });
                    ArrayList list4 = new ArrayList();
                    str27 = str27.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                    str28 = strArray[strArray.Length - 1].ToString();
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str7 + str28))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str4))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str4);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str7 + str28, this.objNF3Template.getPhysicalPath() + str4 + str28);
                    }
                    if (str2 == "OLD")
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str28);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list4, conn);
                    }
                    else
                    {
                        list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list4.Add(str4 + str28);
                        list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list4.Add(this.Session["TM_SZ_CASE_ID"]);
                        list4.Add(strArray[strArray.Length - 1].ToString());
                        list4.Add(str4);
                        list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list4.Add(str);
                        list4.Add("NF");
                        list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list4.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list4, conn);
                    }
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                    {
                        list4.Clear();
                        string companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        CoverContract_pdf cpdf = new CoverContract_pdf();
                        string fileName = "Contract_" + str28;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                        bool isGenerated = false;
                        cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str4 + fileName, out isGenerated);
                        if (isGenerated)
                        {
                            list4.Clear();
                            if (str2 == "OLD")
                            {
                                list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list4.Add(str4 + fileName);
                                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                list4.Add(fileName);
                                list4.Add(str4);
                                list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list4.Add(str);
                                list4.Add("NF");
                                list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillContractPath(list4);
                            }
                            else
                            {
                                list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list4.Add(str4 + fileName);
                                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                list4.Add(fileName);
                                list4.Add(str4);
                                list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list4.Add(str);
                                list4.Add("NF");
                                list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list4.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillContractPath_New(list4);
                            }
                        }
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str28;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    //this.BindLatestTransaction();
                }
            }
            else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString(), conn) == "WC000000000000000003")
            {
                string str29;
                string companyName;
                Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
                bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                string str31 = this.Session["TM_SZ_CASE_ID"].ToString();
                string str32 = this.Session["TM_SZ_BILL_ID"].ToString();
                string str33 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                string str34 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                {
                    companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    str29 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                }
                else
                {
                    companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                    str29 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }


                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str29, str31, str, companyName, str32, str33, str34, conn) + "'); ", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);


            }
            new Bill_Sys_BillTransaction_BO();
        }
        catch (Exception ex)
        {
            throw ex;
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //using (Utils utility = new Utils())
            //{
            //    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            //}
            //string str2 = "Error Request=" + id + ".Please share with Technical support.";
            //base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void GeneratePDFForWorkerComp(string szBillNumber, string szCaseID, string p_szPDFNo)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        this._bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
        str = ApplicationSettings.GetParameterValue("DocumentManagerURL");
        string text1 = this._bill_Sys_NF3_Template.getPhysicalPath() + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/";
        string str2 = "";
        try
        {
            this.objVerification_Desc = new Bill_Sys_Verification_Desc();
            this.objVerification_Desc.sz_bill_no = szBillNumber;
            this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.objVerification_Desc.sz_flag = "BILL";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            string str3 = "";
            string str4 = "";
            list.Add(this.objVerification_Desc);
            list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (list2.Contains("NFVER"))
            {
                str3 = "OLD";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/No Fault File/Bills/" + doctorSpeciality.Trim() + "/";
            }
            else
            {
                str3 = "NEW";
                str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/No Fault File/Medicals/" + doctorSpeciality.Trim() + "/Bills/";
            }
            if (p_szPDFNo == "1")
            {
                PDFValueReplacement.PDFValueReplacement replacement = new PDFValueReplacement.PDFValueReplacement();
                string str6 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C4"].ToString();
                str2 = replacement.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C4"].ToString(), str6, szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                ArrayList list3 = new ArrayList();
                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2);
                list3.Add(szBillNumber);
                list3.Add(szCaseID);
                list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this._bill_Sys_NF3_Template.saveGeneratedNF3File(list3);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2 + "'); ", true);
            }
            if (p_szPDFNo == "2")
            {
                PDFValueReplacement.PDFValueReplacement replacement2 = new PDFValueReplacement.PDFValueReplacement();
                string str7 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C42"].ToString();
                str2 = replacement2.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C42"].ToString(), str7, szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                ArrayList list4 = new ArrayList();
                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2);
                list4.Add(szBillNumber);
                list4.Add(szCaseID);
                list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this._bill_Sys_NF3_Template.saveGeneratedNF3File(list4);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2 + "'); ", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
            }
            if (p_szPDFNo == "3")
            {
                PDFValueReplacement.PDFValueReplacement replacement3 = new PDFValueReplacement.PDFValueReplacement();
                string str8 = ConfigurationManager.AppSettings["PDF_FILE_PATH_C43"].ToString();
                str2 = replacement3.ReplacePDFvalues(ConfigurationManager.AppSettings["TEMPLATE_VARIABLES_FILE_FOR_C43"].ToString(), str8, szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, szCaseID);
                ArrayList list5 = new ArrayList();
                list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2);
                list5.Add(szBillNumber);
                list5.Add(szCaseID);
                list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this._bill_Sys_NF3_Template.saveGeneratedNF3File(list5);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2 + "'); ", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx?Message=" + message + "';", true);
            }
            this.objNF3Template = new Bill_Sys_NF3_Template();
            string str9 = this.objNF3Template.getPhysicalPath();
            if (File.Exists(str9 + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2))
            {
                if (!Directory.Exists(str9 + str4))
                {
                    Directory.CreateDirectory(str9 + str4);
                }
                File.Copy(str9 + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + szCaseID + "/Packet Document/" + str2, str9 + str4 + str2);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList list6 = new ArrayList();
            if (str3 == "OLD")
            {
                list6.Add(szBillNumber);
                list6.Add(str4 + str2);
                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list6.Add(szCaseID);
                list6.Add(str2);
                list6.Add(str4);
                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list6.Add(this._bill_Sys_BillTransaction.GetDoctorSpeciality(szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                list6.Add("WC");
                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                this.objNF3Template.saveGeneratedBillPath(list6);
            }
            else
            {
                list6.Add(szBillNumber);
                list6.Add(str4 + str2);
                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                list6.Add(szCaseID);
                list6.Add(str2);
                list6.Add(str4);
                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                list6.Add(this._bill_Sys_BillTransaction.GetDoctorSpeciality(szBillNumber, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                list6.Add("WC");
                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                list6.Add(list2[0].ToString());
                this.objNF3Template.saveGeneratedBillPath_New(list6);
            }
            this._DAO_NOTES_EO = new DAO_NOTES_EO();
            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str2;
            this._DAO_NOTES_BO = new DAO_NOTES_BO();
            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet Get_Bit_To_Change_Amount(string SZ_CaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_GET_BIT_TO_CHANGE_AMOUNT", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CaseID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet GetBillingDoctor(string p_szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MST_BILLING_DOCTOR", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@FLAG", "GETDOCTORLIST");
            selectCommand.Parameters.AddWithValue("@ID", p_szCompanyID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (SqlException ex)
        {
            dataSet = null;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection = null;
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void getDefaultAssociatedDiagCode()
    {
    }

    private void GetDiagnosisCode()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetDoctorDiagnosisCode("", this.txtCaseID.Text, "GET_DOCTOR_DIAGNOSIS_CODE").Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
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

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientDeskList.DataSource = tbo.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this.grdPatientDeskList.DataBind();
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

    private void GetProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable table = new DataTable();
            for (int i = 1; i <= 3; i++)
            {
                table = this._bill_Sys_BillTransaction.GetDoctorProcedureCodeList(doctorId, "TY00000000000000000" + i.ToString(), this.txtCaseID.Text).Tables[0];
                if (table.Rows.Count > 0)
                {
                    try
                    {
                        DataTable table2 = new DataTable();
                        table2.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
                        table2.Columns.Add("DT_DATE_OF_SERVICE");
                        table2.Columns.Add("SZ_PROCEDURE_ID");
                        table2.Columns.Add("SZ_PROCEDURAL_CODE");
                        table2.Columns.Add("SZ_CODE_DESCRIPTION");
                        table2.Columns.Add("FLT_AMOUNT");
                        table2.Columns.Add("I_UNIT");
                        table2.Columns.Add("SZ_TYPE_CODE_ID");
                        table2.Columns.Add("FLT_GROUP_AMOUNT");
                        table2.Columns.Add("I_GROUP_AMOUNT_ID");
                        string str = "";
                        string str2 = "";
                        foreach (DataRow row in table.Rows)
                        {
                            str = row["CODE"].ToString().Substring(0, row["CODE"].ToString().IndexOf("|"));
                            str2 = row["CODE"].ToString().Substring(row["CODE"].ToString().IndexOf("|") + 1, row["CODE"].ToString().Length - (row["CODE"].ToString().IndexOf("|") + 1));
                            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                            DataTable table3 = new DataTable();
                            table3 = this._bill_Sys_BillTransaction.GeSelectedDoctorProcedureCode_List(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, doctorId, str2).Tables[0];
                            foreach (DataRow row2 in table3.Rows)
                            {
                                DataRow row3 = table2.NewRow();
                                row3["SZ_BILL_TXN_DETAIL_ID"] = "";
                                string str3 = row["DESCRIPTION"].ToString().Substring(row["DESCRIPTION"].ToString().Substring(0, row["DESCRIPTION"].ToString().IndexOf("--")).Length + 2, row["DESCRIPTION"].ToString().Length - (row["DESCRIPTION"].ToString().Substring(0, row["DESCRIPTION"].ToString().IndexOf("--")).Length + 2));
                                row3["DT_DATE_OF_SERVICE"] = str3.Substring(0, str3.IndexOf("--"));
                                row3["SZ_PROCEDURE_ID"] = row2["SZ_PROCEDURE_ID"];
                                row3["SZ_PROCEDURAL_CODE"] = row2["SZ_PROCEDURE_CODE"];
                                row3["SZ_CODE_DESCRIPTION"] = row2["SZ_CODE_DESCRIPTION"];
                                row3["I_UNIT"] = "";
                                row3["SZ_TYPE_CODE_ID"] = str2;
                                table2.Rows.Add(row3);
                            }
                        }
                        this.grdTransactionDetails.DataSource = table2;
                        this.grdTransactionDetails.DataBind();
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            this.grdTransactionDetails.Columns[5].Visible = false;
                            this.grdTransactionDetails.Columns[6].Visible = true;
                        }
                        else
                        {
                            this.grdTransactionDetails.Columns[5].Visible = true;
                            this.grdTransactionDetails.Columns[6].Visible = false;
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

                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet doctorSpecialityProcedureCodeList = new DataSet();
            doctorSpecialityProcedureCodeList = this._bill_Sys_BillTransaction.GetDoctorSpecialityProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdProcedure.DataSource = doctorSpecialityProcedureCodeList;
            this.grdProcedure.DataBind();

            for (int i = 0; i < grdProcedure.Items.Count; i++)
            {
                if (objSystemObject.SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROCEDURE == "1")
                {
                    TextBox txtModifier = (TextBox)grdProcedure.Items[i].FindControl("txtModifier");
                    txtModifier.Visible = true;
                }
                else
                {
                    TextBox txtModifier = (TextBox)grdProcedure.Items[i].FindControl("txtModifier");
                    txtModifier.Visible = false;
                }
            }
            this.Session["doctorProcCodes"] = doctorSpecialityProcedureCodeList;
            this.grdGroupProcCodeService.DataSource = this._bill_Sys_BillTransaction.GetDoctorSpecialityGroupProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdGroupProcCodeService.DataBind();

            for (int i = 0; i < grdGroupProcCodeService.Items.Count; i++)
            {
                if (objSystemObject.SZ_ALLOW_MODIFIER_TO_UPDATE_FOR_PROCEDURE == "1")
                {
                    TextBox txtModifierGrp = (TextBox)grdGroupProcCodeService.Items[i].FindControl("txtModifierGroup");
                    txtModifierGrp.Visible = true;
                }
                else
                {
                    TextBox txtModifierGrp = (TextBox)grdGroupProcCodeService.Items[i].FindControl("txtModifierGroup");
                    txtModifierGrp.Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet Getspeciality(string p_szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MST_PROCEDURE_GROUP", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            selectCommand.Parameters.AddWithValue("@ID", p_szCompanyID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (SqlException ex)
        {
            dataSet = null;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection = null;
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdCompleteVisit_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            this.LoadProcedure(e.CommandArgument.ToString(), e);
        }
        if (e.CommandName == "Count")
        {
            this.dummybtnAddServices.Visible = true;
            this.hndPopUpvalue.Value = "PopUpValue";
            string text = e.Item.Cells[8].Text;
            string str2 = e.Item.Cells[12].Text;
            string str3 = e.Item.Cells[13].Text;
            string str4 = e.Item.Cells[6].Text;
            string str5 = e.Item.Cells[7].Text;
            string str6 = e.Item.Cells[9].Text;
            CheckBox box = (CheckBox)e.Item.FindControl("chkSelectItem");
            box.Checked = true;
            this.RefferalModelPopUp(text, str2, str3, str4, str5, str6);
        }
    }

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            if (this.extddlDiagnosisType.Text == "NA")
            {
                this.BindDiagnosisGrid("");
            }
            else
            {
                this.BindDiagnosisGrid(this.extddlDiagnosisType.Text);
            }
            //ScriptManager.RegisterStartupScript((Page) this, base.GetType(), "starScript", "showDiagnosisCodePopup();", true);
            //this.ModalPopupExtender1.Show();
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

    protected void grdLatestBillTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._MUVGenerateFunction = new MUVGenerateFunction();
            log.Debug("Start : grdLatestBillTransaction_ItemCommand");
            if (e.CommandName.ToString() == "Add IC9 Code")
            {
                this.Session["PassedBillID"] = e.CommandArgument;
                base.Response.Redirect("Bill_Sys_BillIC9Code.aspx", false);
            }
            if (e.CommandName.ToString() == "Generate bill")
            {
                log.Debug("Generate bill");
                string str = e.Item.Cells[3].Text.Split(new char[] { ' ' })[0].ToString();
                string text = e.Item.Cells[20].Text;
                this.Session["TM_SZ_CASE_ID"] = e.CommandArgument;
                this.Session["TM_SZ_BILL_ID"] = e.Item.Cells[1].Text;
                this.objNF3Template = new Bill_Sys_NF3_Template();
                this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                this.objVerification_Desc.sz_bill_no = this.Session["TM_SZ_BILL_ID"].ToString();
                this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc.sz_flag = "BILL";
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                string str2 = "";
                string str3 = "";
                list.Add(this.objVerification_Desc);
                list2 = this._bill_Sys_BillTransaction.Get_Node_Type(list);
                if (list2.Contains("NFVER"))
                {
                    str2 = "OLD";
                    str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + str + "/";
                }
                else
                {
                    str2 = "NEW";
                    str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + str + "/Bills/";
                }
                CaseDetailsBO sbo = new CaseDetailsBO();
                string str4 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000002")
                {
                    string str5 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    string str6 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set = new DataSet();
                    string str7 = "";
                    string str8 = "";
                    set = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID, this.Session["TM_SZ_BILL_ID"].ToString());
                    if (set.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            str7 = set.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str7 == "1")
                    {

                        ArrayList list3 = new ArrayList();
                        string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                        Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                        this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str6 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                        }
                        str8 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500;
                        if (str2 == "OLD")
                        {
                            list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list3.Add(str3 + this.str_1500);
                            list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list3.Add(this.Session["TM_SZ_CASE_ID"]);
                            list3.Add(this.str_1500);
                            list3.Add(str3);
                            list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list3.Add(str);
                            list3.Add("NF");
                            list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list3);
                        }
                        else
                        {
                            list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list3.Add(str3 + this.str_1500);
                            list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list3.Add(this.Session["TM_SZ_CASE_ID"]);
                            list3.Add(this.str_1500);
                            list3.Add(str3);
                            list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list3.Add(str);
                            list3.Add("NF");
                            list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list3.Add(list2[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list3);
                        }
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                        {
                            list3.Clear();
                            string companyId = string.Empty;
                            companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            CoverContract_pdf cpdf = new CoverContract_pdf();
                            string fileName = "Contract_" + this.str_1500;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                            bool isGenerated = false;
                            cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str3 + fileName, out isGenerated);
                            if (isGenerated)
                            {
                                list3.Clear();
                                if (str2 == "OLD")
                                {
                                    list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                    list3.Add(str3 + fileName);
                                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                    list3.Add(fileName);
                                    list3.Add(str3);
                                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                    list3.Add(str);
                                    list3.Add("NF");
                                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                    this.objNF3Template.saveGeneratedBillContractPath(list3);
                                }
                                else
                                {
                                    list3.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                    list3.Add(str3 + fileName);
                                    list3.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list3.Add(this.Session["TM_SZ_CASE_ID"]);
                                    list3.Add(fileName);
                                    list3.Add(str3);
                                    list3.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                    list3.Add(str);
                                    list3.Add("NF");
                                    list3.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                    list3.Add(list2[0].ToString());
                                    this.objNF3Template.saveGeneratedBillContractPath_New(list3);
                                }
                            }
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        this.BindLatestTransaction();
                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str8.ToString() + "','','width=800,height=600,left=30,top=30,scrollbars=1'); ", true);
                        ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                    }
                    else
                    {
                        string str9 = ConfigurationManager.AppSettings["DefaultTemplateName"].ToString();
                        ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
                        ConfigurationSettings.AppSettings["NF3_PAGE3"].ToString();
                        ConfigurationSettings.AppSettings["NF3_PAGE4"].ToString();
                        Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
                        string str10 = configuration.getConfigurationSettings(str4, "GET_DIAG_PAGE_POSITION");
                        string str11 = configuration.getConfigurationSettings(str4, "DIAG_PAGE");
                        string str12 = ConfigurationManager.AppSettings["NF3_XML_FILE"].ToString();
                        string str13 = ConfigurationManager.AppSettings["NF3_PDF_FILE"].ToString();
                        string str14 = ConfigurationManager.AppSettings["NF33_XML_FILE"].ToString();
                        string str15 = ConfigurationManager.AppSettings["NF3_PAGE3"].ToString();
                        GenerateNF3PDF enfpdf = new GenerateNF3PDF();
                        this.objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();
                        string str16 = enfpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str9);
                        log.Debug("Bill Details PDF File : " + str16);
                        string str17 = this.objPDFReplacement.ReplacePDFvalues(str12, str13, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                        log.Debug("Page1 : " + str17);
                        string str18 = "";
                        string str19 = this.objPDFReplacement.MergePDFFiles(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), str17, str16);
                        string str20 = this.objPDFReplacement.ReplacePDFvalues(str14, str15, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString());
                        string str21 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string str22 = e.Item.Cells[3].Text;
                        this.bt_include = this._MUVGenerateFunction.get_bt_include(str21, str22, "", "Speciality");
                        string str23 = this._MUVGenerateFunction.get_bt_include(str21, "", "WC000000000000000002", "CaseType");
                        if ((this.bt_include == "True") && (str23 == "True"))
                        {
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        }
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str19, this.objNF3Template.getPhysicalPath() + str5 + str20, this.objNF3Template.getPhysicalPath() + str5 + str20.Replace(".pdf", "_MER.pdf"));
                        string str24 = str20.Replace(".pdf", "_MER.pdf");
                        if ((this.bt_include == "True") && (str23 == "True"))
                        {
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str5 + str24, this.objNF3Template.getPhysicalPath() + str5 + this.str_1500, this.objNF3Template.getPhysicalPath() + str5 + str24.Replace(".pdf", ".pdf"));
                        }
                        str18 = str5 + str24;
                        log.Debug("GenereatedFileName : " + str18);
                        string str25 = "";
                        str25 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str18;
                        string path = this.objNF3Template.getPhysicalPath() + "/" + str18;
                        CutePDFDocumentClass class2 = new CutePDFDocumentClass();
                        string str27 = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
                        class2.initialize(str27);
                        if ((((class2 != null) && File.Exists(path)) && ((str11 != "CI_0000003") && (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= 5))) && ((str10 == "CK_0000003") && ((str11 != "CI_0000004") || (this.objNF3Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) != 5))))
                        {
                            str16 = path.Replace(".pdf", "_NewMerge.pdf");
                        }
                        string str28 = "";
                        if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                        {
                            str18 = path.Replace(".pdf", "_New.pdf").ToString();
                        }
                        if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_NewMerge.pdf").ToString()))
                        {
                            str28 = str25.Replace(".pdf", "_NewMerge.pdf").ToString();
                            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.Replace(".pdf", "_NewMerge.pdf").ToString() + "','','width=800,height=600,left=30,top=30,scrollbars=1'); ", true);
                            ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                        }
                        else if (File.Exists(path) && File.Exists(path.Replace(".pdf", "_New.pdf").ToString()))
                        {
                            str28 = str25.Replace(".pdf", "_New.pdf").ToString();
                            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.Replace(".pdf", "_New.pdf").ToString() + "','','width=800,height=600,left=30,top=30,scrollbars=1'); ", true);
                            ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                        }
                        else
                        {
                            str28 = str25.ToString();
                            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + str25.ToString() + "','','width=800,height=600,left=30,top=30,scrollbars=1'); ", true);
                            ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                        }
                        string str29 = "";
                        string[] strArray = str28.Split(new char[] { '/' });
                        ArrayList list4 = new ArrayList();
                        str28 = str28.Remove(0, ApplicationSettings.GetParameterValue("DocumentManagerURL").Length);
                        str29 = strArray[strArray.Length - 1].ToString();
                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str6 + str29))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str6 + str29, this.objNF3Template.getPhysicalPath() + str3 + str29);
                        }
                        if (str2 == "OLD")
                        {
                            list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list4.Add(str3 + str29);
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list4.Add(this.Session["TM_SZ_CASE_ID"]);
                            list4.Add(strArray[strArray.Length - 1].ToString());
                            list4.Add(str3);
                            list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list4.Add(str);
                            list4.Add("NF");
                            list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list4);
                        }
                        else
                        {
                            list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list4.Add(str3 + str29);
                            list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list4.Add(this.Session["TM_SZ_CASE_ID"]);
                            list4.Add(strArray[strArray.Length - 1].ToString());
                            list4.Add(str3);
                            list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list4.Add(str);
                            list4.Add("NF");
                            list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list4.Add(list2[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list4);
                        }
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ENABLE_CONTRACT_PDF_GENERATION == "1")
                        {
                            list4.Clear();
                            string companyId = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            CoverContract_pdf cpdf = new CoverContract_pdf();
                            string fileName = "Contract_" + str29;// this.Session["TM_SZ_BILL_ID"].ToString() + "_contract.pdf";
                            bool isGenerated = false;
                            cpdf.GenerateCoverContractGorMedicalFacility(companyId, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), this.objNF3Template.getPhysicalPath() + str3 + fileName, out isGenerated);
                            if (isGenerated)
                            {
                                list4.Clear();
                                if (str2 == "OLD")
                                {
                                    list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                    list4.Add(str3 + fileName);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                    list4.Add(fileName);
                                    list4.Add(str3);
                                    list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                    list4.Add(str);
                                    list4.Add("NF");
                                    list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                    this.objNF3Template.saveGeneratedBillContractPath(list4);
                                }
                                else
                                {
                                    list4.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                    list4.Add(str3 + fileName);
                                    list4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                    list4.Add(this.Session["TM_SZ_CASE_ID"]);
                                    list4.Add(fileName);
                                    list4.Add(str3);
                                    list4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                    list4.Add(str);
                                    list4.Add("NF");
                                    list4.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                    list4.Add(list2[0].ToString());
                                    this.objNF3Template.saveGeneratedBillContractPath_New(list4);
                                }
                            }
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str29;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        this.BindLatestTransaction();
                    }
                }
                else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000003")
                {
                    string str30;
                    string companyName;
                    Bill_Sys_PVT_Template template = new Bill_Sys_PVT_Template();
                    bool flag = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                    string str32 = this.Session["TM_SZ_CASE_ID"].ToString();
                    string str33 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string str34 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str35 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        companyName = new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str30 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        companyName = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                        str30 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + template.GeneratePVTBill(flag, str30, str32, str, companyName, str33, str34, str35) + "','','width=800,height=600,left=30,top=30,scrollbars=1'); ", true);
                    ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                }
                else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000004")
                {
                    string str36;
                    string str37;
                    string str38 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.Session["TM_SZ_CASE_ID"].ToString();
                    string str39 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string str40 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str41 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str36 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        str36 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Lien lien = new Lien();
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set2 = new DataSet();
                    string str42 = "";
                    set2 = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, str39);
                    if (set2.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < set2.Tables[0].Rows.Count; j++)
                        {
                            str42 = set2.Tables[0].Rows[j]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str42 == "1")
                    {
                        ArrayList list5 = new ArrayList();
                        string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                        Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                        this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str38 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str38 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                        }
                        str37 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500;
                        if (str2 == "OLD")
                        {
                            list5.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list5.Add(str3 + this.str_1500);
                            list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list5.Add(this.Session["TM_SZ_CASE_ID"]);
                            list5.Add(this.str_1500);
                            list5.Add(str3);
                            list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list5.Add(str);
                            list5.Add("NF");
                            list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list5);
                        }
                        else
                        {
                            list5.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list5.Add(str3 + this.str_1500);
                            list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list5.Add(this.Session["TM_SZ_CASE_ID"]);
                            list5.Add(this.str_1500);
                            list5.Add(str3);
                            list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list5.Add(str);
                            list5.Add("NF");
                            list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list5.Add(list2[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list5);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        this.objNF3Template = new Bill_Sys_NF3_Template();
                        string str43 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string str44 = e.Item.Cells[3].Text;
                        this.bt_include = this._MUVGenerateFunction.get_bt_include(str43, str44, "", "Speciality");
                        string str45 = this._MUVGenerateFunction.get_bt_include(str43, "", "WC000000000000000004", "CaseType");
                        if ((this.bt_include == "True") && (str45 == "True"))
                        {
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str3 + lien.GenratePdfForLienWithMuv(str36, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41), this.objNF3Template.getPhysicalPath() + str38 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            str37 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500.Replace(".pdf", "_MER.pdf");
                            ArrayList list6 = new ArrayList();
                            if (str2 == "OLD")
                            {
                                list6.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list6.Add(this.Session["TM_SZ_CASE_ID"]);
                                list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(str3);
                                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list6.Add(str);
                                list6.Add("NF");
                                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillPath(list6);
                            }
                            else
                            {
                                list6.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list6.Add(this.Session["TM_SZ_CASE_ID"]);
                                list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(str3);
                                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list6.Add(str);
                                list6.Add("NF");
                                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list6.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillPath_New(list6);
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                        else
                        {
                            str37 = lien.GenratePdfForLien(str36, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str37 + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
                    ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                }
                else if (this.objCaseDetailsBO.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000006")
                {
                    string str46 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.Session["TM_SZ_CASE_ID"].ToString();
                    this.Session["TM_SZ_BILL_ID"].ToString();
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                    }
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    new DataSet();
                    ArrayList list7 = new ArrayList();
                    this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                    if (File.Exists(this.objNF3Template.getPhysicalPath() + str46 + this.str_1500))
                    {
                        if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                        {
                            Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                        }
                        File.Copy(this.objNF3Template.getPhysicalPath() + str46 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                    }
                    string text2 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500;
                    if (str2 == "OLD")
                    {
                        list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list7.Add(str3 + this.str_1500);
                        list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list7.Add(this.Session["TM_SZ_CASE_ID"]);
                        list7.Add(this.str_1500);
                        list7.Add(str3);
                        list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list7.Add(str);
                        list7.Add("NF");
                        list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        this.objNF3Template.saveGeneratedBillPath(list7);
                    }
                    else
                    {
                        list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                        list7.Add(str3 + this.str_1500);
                        list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        list7.Add(this.Session["TM_SZ_CASE_ID"]);
                        list7.Add(this.str_1500);
                        list7.Add(str3);
                        list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                        list7.Add(str);
                        list7.Add("NF");
                        list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                        list7.Add(list2[0].ToString());
                        this.objNF3Template.saveGeneratedBillPath_New(list7);
                    }
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                }
                else if (sbo.GetCaseType(this.Session["TM_SZ_BILL_ID"].ToString()) == "WC000000000000000007")
                {
                    string str36;
                    string str37;
                    string str38 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                    this.Session["TM_SZ_CASE_ID"].ToString();
                    string str39 = this.Session["TM_SZ_BILL_ID"].ToString();
                    string str40 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                    string str41 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID != ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
                    {
                        new Bill_Sys_NF3_Template().GetCompanyName(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
                        str36 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                    }
                    else
                    {
                        str36 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    }
                    Employer lien = new Employer();
                    this._MUVGenerateFunction = new MUVGenerateFunction();
                    this.objCaseDetailsBO = new CaseDetailsBO();
                    DataSet set2 = new DataSet();
                    string str42 = "";
                    set2 = this.objCaseDetailsBO.Get1500FormBitForInsurance(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, str39);
                    if (set2.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < set2.Tables[0].Rows.Count; j++)
                        {
                            str42 = set2.Tables[0].Rows[j]["BT_1500_FORM"].ToString();
                        }
                    }
                    if (str42 == "1")
                    {
                        ArrayList list5 = new ArrayList();
                        string szOriginalTemplatePDFFileName = ConfigurationManager.AppSettings["PVT_PDF_FILE"].ToString();
                        Bill_Sys_PVT_Bill_PDF_Replace objPVTReplace = new Bill_Sys_PVT_Bill_PDF_Replace();
                        this.str_1500 = objPVTReplace.ReplacePDFvalues(szOriginalTemplatePDFFileName, this.Session["TM_SZ_BILL_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                        if (File.Exists(this.objNF3Template.getPhysicalPath() + str38 + this.str_1500))
                        {
                            if (!Directory.Exists(this.objNF3Template.getPhysicalPath() + str3))
                            {
                                Directory.CreateDirectory(this.objNF3Template.getPhysicalPath() + str3);
                            }
                            File.Copy(this.objNF3Template.getPhysicalPath() + str38 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500);
                        }
                        str37 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500;
                        if (str2 == "OLD")
                        {
                            list5.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list5.Add(str3 + this.str_1500);
                            list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list5.Add(this.Session["TM_SZ_CASE_ID"]);
                            list5.Add(this.str_1500);
                            list5.Add(str3);
                            list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list5.Add(str);
                            list5.Add("NF");
                            list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list5);
                        }
                        else
                        {
                            list5.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list5.Add(str3 + this.str_1500);
                            list5.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list5.Add(this.Session["TM_SZ_CASE_ID"]);
                            list5.Add(this.str_1500);
                            list5.Add(str3);
                            list5.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list5.Add(str);
                            list5.Add("NF");
                            list5.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list5.Add(list2[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list5);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        this.objNF3Template = new Bill_Sys_NF3_Template();
                        string str43 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        string str44 = e.Item.Cells[3].Text;
                        this.bt_include = this._MUVGenerateFunction.get_bt_include(str43, str44, "", "Speciality");
                        string str45 = this._MUVGenerateFunction.get_bt_include(str43, "", "WC000000000000000007", "CaseType");
                        if ((this.bt_include == "True") && (str45 == "True"))
                        {
                            this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                            MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str3 + lien.GenratePdfForEmployerWithMuv(str36, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41), this.objNF3Template.getPhysicalPath() + str38 + this.str_1500, this.objNF3Template.getPhysicalPath() + str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            str37 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str3 + this.str_1500.Replace(".pdf", "_MER.pdf");
                            ArrayList list6 = new ArrayList();
                            if (str2 == "OLD")
                            {
                                list6.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list6.Add(this.Session["TM_SZ_CASE_ID"]);
                                list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(str3);
                                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list6.Add(str);
                                list6.Add("NF");
                                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                this.objNF3Template.saveGeneratedBillPath(list6);
                            }
                            else
                            {
                                list6.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                                list6.Add(str3 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                list6.Add(this.Session["TM_SZ_CASE_ID"]);
                                list6.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                                list6.Add(str3);
                                list6.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                                list6.Add(str);
                                list6.Add("NF");
                                list6.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                                list6.Add(list2[0].ToString());
                                this.objNF3Template.saveGeneratedBillPath_New(list6);
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                        }
                        else
                        {
                            str37 = lien.GenratePdfForEmployer(str36, str39, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str39, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str40, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str41);
                        }
                    }
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.open('" + str37 + "','','width=800,height=600,left=30,top=30,scrollbars=1');", true);
                    ScriptManager.RegisterClientScriptBlock(grdLatestBillTransaction, grdLatestBillTransaction.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillTransaction.aspx';", true);
                }
                else
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_SelectBillType.aspx'); ", true);
                }
            }
            if (e.CommandName.ToString() == "Doctor's Initial Report")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                foreach (DataGridItem item in this.grdLatestBillTransaction.Items)
                {
                    if ((item.Cells[13].Text != "") && (item.Cells[14].Text == ""))
                    {
                        item.Cells[14].Text = "";
                        item.Cells[15].Text = "";
                        item.Cells[0x10].Text = "";
                    }
                }
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformation.aspx','WC4','menubar=no,scrollbars=yes'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Progress Report")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformationC4_2.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Doctor's Report Of MMI")
            {
                this.Session["TEMPLATE_BILL_NO"] = e.Item.Cells[1].Text;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_PatientInformationC4_3.aspx'); ", true);
            }
            if (e.CommandName.ToString() == "Make Payment")
            {
                if (e.Item.Cells[11].Text != "1")
                {
                    this.Session["PassedBillID"] = e.CommandArgument;
                    this.Session["Balance"] = e.Item.Cells[9].Text;
                    base.Response.Redirect("Bill_Sys_PaymentTransactions.aspx", false);
                }
                else
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Please first add services to bill!'); ", true);
                }
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.Message.ToString());
            log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_BillTransaction. Method - grdLatestBillTransaction_ItemCommand : " + ex.InnerException.StackTrace.ToString());
            }
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

    protected void grdLatestBillTransaction_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.Item.Cells[11].Text == "1")
            {
                e.Item.Cells[10].Text = "";
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            if (this.objCaseDetailsBO.GetCaseType(e.Item.Cells[1].Text) != "WC000000000000000001")
            {
                e.Item.Cells[13].Text = "";
                e.Item.Cells[14].Text = "";
                e.Item.Cells[15].Text = "";
                e.Item.Cells[0x10].Text = "";
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

    protected void grdLatestBillTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdAllReports.Visible = false;
            this.Session["BillID"] = this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[1].Text;
            this.Session["SZ_BILL_NUMBER"] = this.Session["BillID"].ToString();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            if (this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[12].Text != "&nbsp;")
            {
                this.hndDoctorID.Value = this.grdLatestBillTransaction.Items[this.grdLatestBillTransaction.SelectedIndex].Cells[12].Text;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetBillDiagnosisCode(this.Session["BillID"].ToString()).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
            this.SetControlForUpdateBill();
            this.BindDoctorsGrid(this.Session["SZ_BILL_NUMBER"].ToString());
            this.dvcompletevisit.Visible = true;
            double num = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao = new BillTransactionDAO();
                string str = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string procID = ndao.GetProcID(this.txtCompanyID.Text, str);
                string str3 = ndao.GET_IS_LIMITE(this.txtCompanyID.Text, procID);
                if ((str3 != "") && (str3 != "NULL"))
                {
                    for (int i = 0; i < this.grdTransactionDetails.Items.Count; i++)
                    {
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            TextBox box = (TextBox)this.grdTransactionDetails.Items[i].Cells[6].FindControl("txtAmt");
                            if ((box.Text != "") && (box.Text != "&nbsp;"))
                            {
                                num += Convert.ToDouble(box.Text);
                            }
                        }
                        else if ((this.grdTransactionDetails.Items[i].Cells[5].Text != "") && (this.grdTransactionDetails.Items[i].Cells[5].Text != "&nbsp;"))
                        {
                            num += Convert.ToDouble(this.grdTransactionDetails.Items[i].Cells[5].Text);
                        }
                        if (i == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao2 = new BillTransactionDAO();
                            string str4 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str5 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                            string str6 = ndao2.GetProcID(this.txtCompanyID.Text, str4);
                            string str7 = ndao2.GetLimit(this.txtCompanyID.Text, str5, str6);
                            if (str7 != "")
                            {
                                if (Convert.ToDouble(str7) < num)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[10].Text = str7;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                                }
                            }
                            num = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[i].Cells[1].Text != this.grdTransactionDetails.Items[i + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str8 = this.grdTransactionDetails.Items[i].Cells[2].Text.ToString();
                            string str9 = this.grdTransactionDetails.Items[i].Cells[15].Text.ToString();
                            string str10 = ndao3.GetProcID(this.txtCompanyID.Text, str8);
                            string str11 = ndao3.GetLimit(this.txtCompanyID.Text, str9, str10);
                            if (str11 != "")
                            {
                                if (Convert.ToDouble(str11) < num)
                                {
                                    this.grdTransactionDetails.Items[i].Cells[10].Text = str11;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[i].Cells[10].Text = num.ToString();
                                }
                            }
                            num = 0.0;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdTransactionDetails_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdTransactionDetails.CurrentPageIndex = e.NewPageIndex;
            if (this.Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable table = new DataTable();
                table = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    this.grdTransactionDetails.Columns[5].Visible = false;
                    this.grdTransactionDetails.Columns[6].Visible = true;
                }
                else
                {
                    this.grdTransactionDetails.Columns[5].Visible = true;
                    this.grdTransactionDetails.Columns[6].Visible = false;
                }
            }
            else
            {
                this.BindTransactionDetailsGrid(this.Session["BillID"].ToString());
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

    protected void grdTransactionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtTransDetailID.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[1].Text;
            this.txtAmount.Text = this.grdTransactionDetails.Items[this.grdTransactionDetails.SelectedIndex].Cells[9].Text;
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

    public void gridmodelpopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataRow row;
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("I_EventID");
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                if (((!(item.Cells[1].Text.ToString() != "") || !(item.Cells[1].Text.ToString() != "&nbsp;")) || (!(item.Cells[3].Text.ToString() != "") || !(item.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item.Cells[4].Text.ToString() != "") || !(item.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                row = table.NewRow();
                if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                {
                    row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                }
                else
                {
                    row["SZ_BILL_TXN_DETAIL_ID"] = "";
                }
                row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    row["FLT_AMOUNT"] = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                }
                if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                {
                    row["I_UNIT"] = ((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString();
                }
                row["SZ_TYPE_CODE_ID"] = item.Cells[9].Text.ToString();
                row["FLT_GROUP_AMOUNT"] = item.Cells[10].Text.ToString();
                row["I_GROUP_AMOUNT_ID"] = item.Cells[11].Text.ToString();
                row["I_EventID"] = item.Cells[13].Text.ToString();
                table.Rows.Add(row);
            }
            string doctorId = "";
            ArrayList list = new ArrayList();
            DataSet visitTypeList = new DataSet();
            Bill_Sys_Visit_BO t_bo = new Bill_Sys_Visit_BO();
            visitTypeList = t_bo.GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            int num = 0;
            foreach (DataGridItem item2 in this.grdCompleteVisit.Items)
            {
                CheckBox box = (CheckBox)item2.Cells[0].FindControl("chkSelectItem");
                if (box.Checked)
                {
                    string text = item2.Cells[3].Text;
                    for (int j = 0; j < visitTypeList.Tables[0].Rows.Count; j++)
                    {
                        if (!(text == visitTypeList.Tables[0].Rows[j][1].ToString()))
                        {
                            continue;
                        }
                        ArrayList list2 = new ArrayList();
                        list2.Add(this.txtCompanyID.Text);
                        list2.Add(item2.Cells[6].Text);
                        list2.Add(text);
                        list2.Add(item2.Cells[7].Text);
                        DataTable procedureCodeList = new DataTable();
                        procedureCodeList = t_bo.GetProcedureCodeList(list2);
                        if (procedureCodeList.Rows.Count > 0)
                        {
                            string str3 = procedureCodeList.Rows[0][2].ToString();
                            string str4 = procedureCodeList.Rows[0][5].ToString();
                            for (int k = 0; k < table.Rows.Count; k++)
                            {
                                if ((str3 == table.Rows[k][3].ToString()) && (DateTime.Compare(Convert.ToDateTime(str4), Convert.ToDateTime(table.Rows[k][1].ToString())) == 0))
                                {
                                    num = 1;
                                    break;
                                }
                                num = 2;
                            }
                        }
                        if ((num == 2) || (num == 0))
                        {
                            list.Add(procedureCodeList);
                        }
                    }
                    doctorId = item2.Cells[8].Text;
                }
            }
            if (doctorId != "")
            {
                this.GetProcedureCode(doctorId);
                this.hndDoctorID.Value = doctorId;
            }
            else
            {
                this.GetProcedureCode(this.hndDoctorID.Value.ToString());
            }
            for (int i = 0; i < list.Count; i++)
            {
                DataTable table3 = (DataTable)list[i];
                foreach (DataRow row2 in table3.Rows)
                {
                    row = table.NewRow();
                    row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    row["DT_DATE_OF_SERVICE"] = row2.ItemArray.GetValue(5).ToString();
                    row["SZ_PROCEDURE_ID"] = row2.ItemArray.GetValue(0).ToString();
                    row["SZ_PROCEDURAL_CODE"] = row2.ItemArray.GetValue(2).ToString();
                    row["SZ_CODE_DESCRIPTION"] = row2.ItemArray.GetValue(3).ToString();
                    row["FLT_AMOUNT"] = row2.ItemArray.GetValue(4).ToString();
                    row["I_UNIT"] = "1";
                    row["SZ_TYPE_CODE_ID"] = row2.ItemArray.GetValue(1).ToString();
                    table.Rows.Add(row);
                }
            }
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[6].Visible = true;
                for (int m = 0; m < this.grdTransactionDetails.Items.Count; m++)
                {
                    TextBox box2 = (TextBox)this.grdTransactionDetails.Items[m].FindControl("txtUnit");
                    box2.Text = this.grdTransactionDetails.Items[m].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    public void Item_Bound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CheckBox box = (CheckBox)e.Item.FindControl("chkSelectItem");
            string text = e.Item.Cells[2].Text;
            string str2 = e.Item.Cells[3].Text;
            string str3 = e.Item.Cells[8].Text;
            string str4 = e.Item.Cells[12].Text;
            int iDisable = 0;
            if (str4.ToLower().Equals("true") || str4.ToLower().Equals("1"))
            {
                iDisable = 1;
            }
            if (box != null)
            {
                box.Attributes.Add("onclick", string.Concat(new object[] { "return ValidateGridCheckBox('", text, "','", str2, "','", str3, "',", e.Item.ItemIndex, ",", iDisable, ");" }));
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

    private string lfnMergeDiagCodePage(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str;
        try
        {
            this._bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
            string str2 = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            string str3 = configuration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");
            string str4 = "";
            GenerateC42PDF ecpdf = new GenerateC42PDF();
            if ((str3 == "CI_0000005") && (this._bill_Sys_NF3_Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords))
            {
                str4 = ecpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str2);
            }
            if ((str3 == "CI_0000004") && (this._bill_Sys_NF3_Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords))
            {
                str4 = ecpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str2);
            }
            if (str4 == "")
            {
                return p_szGeneratedFileName;
            }
            MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + str4, p_szDefaultPath + str4.Replace(".pdf", "_Merge.pdf"));
            str = str4.Replace(".pdf", "_Merge.pdf");
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
            return null;
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return str;
    }

    private string lfnMergeDiagCodePageForC43(string p_szDefaultPath, string p_szGeneratedFileName, int i_NumberOfRecords)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str;
        try
        {
            this._bill_Sys_NF3_Template = new Bill_Sys_NF3_Template();
            Bill_Sys_Configuration configuration = new Bill_Sys_Configuration();
            string str2 = ConfigurationManager.AppSettings["NextDiagnosisTemplate"].ToString();
            string str3 = configuration.getConfigurationSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "DIAG_PAGE");
            string str4 = "";
            GenerateC43PDF ecpdf = new GenerateC43PDF();
            if ((str3 == "CI_0000005") && (this._bill_Sys_NF3_Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords))
            {
                str4 = ecpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str2);
            }
            if ((str3 == "CI_0000004") && (this._bill_Sys_NF3_Template.getDiagnosisCodeCount(this.Session["TM_SZ_BILL_ID"].ToString()) >= i_NumberOfRecords))
            {
                str4 = ecpdf.GeneratePDF(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, this.Session["TM_SZ_CASE_ID"].ToString(), this.Session["TM_SZ_BILL_ID"].ToString(), "", str2);
            }
            if (str4 == "")
            {
                return p_szGeneratedFileName;
            }
            MergePDF.MergePDFFiles(p_szDefaultPath + p_szGeneratedFileName, p_szDefaultPath + str4, p_szDefaultPath + str4.Replace(".pdf", "_Merge.pdf"));
            str = str4.Replace(".pdf", "_Merge.pdf");
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
            return null;
        }

        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }



    protected void lnkAddGroupService_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataRow row;
            if (this.lstDiagnosisCodes.Items.Count == 0)
            {
                this.lstDiagnosisCodes.Items.Clear();
                Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
                this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, this.hndDoctorID.Value, this.txtCompanyID.Text).Tables[0];
                this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                this.lstDiagnosisCodes.DataValueField = "CODE";
                this.lstDiagnosisCodes.DataBind();
                this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            }
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("I_EVENTID");
            table.Columns.Add("SZ_VISIT_TYPE");
            table.Columns.Add("BT_IS_LIMITE");
            table.Columns.Add("SZ_MODIFIER");
            table.Columns.Add("SZ_MODIFIER_CODE");
            int num = 0;
            foreach (DataGridItem item in this.grdTransactionDetails.Items)
            {
                row = table.NewRow();
                if ((((item.Cells[1].Text.ToString() != "") && (item.Cells[1].Text.ToString() != "&nbsp;")) && ((item.Cells[3].Text.ToString() != "") && (item.Cells[3].Text.ToString() != "&nbsp;"))) && ((item.Cells[4].Text.ToString() != "") && (item.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    if ((item.Cells[0].Text.ToString() != "&nbsp;") && (item.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item.Cells[4].Text.ToString();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        row["FLT_AMOUNT"] = ((TextBox)item.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        row["FLT_AMOUNT"] = item.Cells[5].Text.ToString();
                    }
                    if ((((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item.Cells[6].FindControl("txtUnit")).Text.ToString();
                    }
                    row["SZ_TYPE_CODE_ID"] = item.Cells[9].Text.ToString();
                    BillTransactionDAO ndao = new BillTransactionDAO();
                    string str = item.Cells[2].Text.ToString();
                    string str2 = item.Cells[15].Text.ToString();
                    string procID = ndao.GetProcID(this.txtCompanyID.Text, str);
                    if (ndao.GetLimit(this.txtCompanyID.Text, str2, procID) != "")
                    {
                        row["FLT_GROUP_AMOUNT"] = "";
                    }
                    else
                    {
                        if (item.Cells[10].Text.ToString() != "0.00")
                        {
                            row["FLT_GROUP_AMOUNT"] = item.Cells[10].Text.ToString();
                        }
                    }
                    if (item.Cells[10].Text.ToString() != "0.00")
                    {
                        row["I_GROUP_AMOUNT_ID"] = item.Cells[11].Text.ToString();
                    }
                    else
                    {
                        row["I_GROUP_AMOUNT_ID"] = "";
                    }
                    row["I_EventID"] = item.Cells[14].Text.ToString();
                    row["SZ_VISIT_TYPE"] = item.Cells[15].Text.ToString();
                    row["BT_IS_LIMITE"] = item.Cells[16].Text.ToString();
                    row["SZ_MODIFIER"] = item.Cells[12].Text.ToString();
                    row["SZ_MODIFIER_CODE"] = item.Cells[17].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.txtGroupDateofService.Text.Split(new char[] { ',' });
            this._bill_Sys_LoginBO = new Bill_Sys_LoginBO();
            if (this._bill_Sys_LoginBO.getDefaultSettings(this.txtCompanyID.Text, "SS00005") == "1")
            {
                foreach (DataGridItem item2 in this.grdAllReports.Items)
                {
                    if (((CheckBox)item2.Cells[0].FindControl("chkselect")).Checked)
                    {
                        string str4 = item2.Cells[5].Text.ToString();
                        foreach (DataGridItem item3 in this.grdGroupProcCodeService.Items)
                        {
                            CheckBox box = (CheckBox)item3.FindControl("chkselect");
                            CheckBox box1 = (CheckBox)item2.FindControl("chkLimit");
                            num = 1;
                            if (box.Checked)
                            {
                                DataSet set = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item3.Cells[1].Text.ToString(), this.txtCompanyID.Text, item3.Cells[2].Text.ToString());
                                int num2 = 1;
                                foreach (DataRow row2 in set.Tables[0].Rows)
                                {
                                    row = table.NewRow();
                                    row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item2.Cells[2].Text.ToString()).ToShortDateString();
                                    row["SZ_PROCEDURE_ID"] = row2.ItemArray.GetValue(0);
                                    row["SZ_PROCEDURAL_CODE"] = row2.ItemArray.GetValue(2);
                                    row["SZ_CODE_DESCRIPTION"] = row2.ItemArray.GetValue(3);
                                    row["FLT_AMOUNT"] = row2.ItemArray.GetValue(4);
                                    row["I_UNIT"] = "1";
                                    row["SZ_TYPE_CODE_ID"] = row2.ItemArray.GetValue(1);
                                    if ((num2 == set.Tables[0].Rows.Count) && (item3.Cells[4].Text.ToString() != "") && (item3.Cells[4].Text.ToString() != "0.00"))
                                    {
                                        row["FLT_GROUP_AMOUNT"] = item3.Cells[4].Text.ToString();
                                    }
                                    if (item3.Cells[3].Text.ToString() != "" && (item3.Cells[4].Text.ToString() != "0.00"))
                                    {
                                        row["I_GROUP_AMOUNT_ID"] = item3.Cells[3].Text.ToString();
                                    }
                                    row["I_EventID"] = item2.Cells[4].Text.ToString();
                                    if (num == 1)
                                    {
                                        row["BT_IS_LIMITE"] = "1";
                                    }
                                    else
                                    {
                                        row["BT_IS_LIMITE"] = "0";
                                    }
                                    row["SZ_VISIT_TYPE"] = str4;
                                    table.Rows.Add(row);
                                    num2++;
                                }
                                continue;
                            }
                        }
                        continue;
                    }
                }
                if (this.grdAllReports.Items.Count == 0)
                {
                    if (this.grdCompleteVisit.Items.Count > 0)
                    {
                        foreach (DataGridItem item4 in this.grdCompleteVisit.Items)
                        {
                            CheckBox box2 = (CheckBox)item4.FindControl("chkSelectItem");
                            if (box2.Checked)
                            {
                                string text = item4.Cells[1].Text;
                                foreach (DataGridItem item5 in this.grdGroupProcCodeService.Items)
                                {
                                    CheckBox box3 = (CheckBox)item5.FindControl("chkselect");
                                    CheckBox box8 = (CheckBox)item4.FindControl("chkLimit");
                                    num = 1;
                                    if (box3.Checked)
                                    {
                                        DataSet set2 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item5.Cells[1].Text.ToString(), this.txtCompanyID.Text, item5.Cells[2].Text.ToString());
                                        int num3 = 1;
                                        foreach (DataRow row3 in set2.Tables[0].Rows)
                                        {
                                            row = table.NewRow();
                                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(text).ToShortDateString();
                                            row["SZ_PROCEDURE_ID"] = row3.ItemArray.GetValue(0);
                                            row["SZ_PROCEDURAL_CODE"] = row3.ItemArray.GetValue(2);
                                            row["SZ_CODE_DESCRIPTION"] = row3.ItemArray.GetValue(3);
                                            row["FLT_AMOUNT"] = row3.ItemArray.GetValue(4);
                                            row["I_UNIT"] = "1";
                                            row["SZ_TYPE_CODE_ID"] = row3.ItemArray.GetValue(1);
                                            if ((num3 == set2.Tables[0].Rows.Count) && (item5.Cells[4].Text.ToString() != "") && (item5.Cells[4].Text.ToString() != "0.00"))
                                            {
                                                row["FLT_GROUP_AMOUNT"] = item5.Cells[4].Text.ToString();
                                            }
                                            if (item5.Cells[3].Text.ToString() != "" && (item5.Cells[4].Text.ToString() != "0.00"))
                                            {
                                                row["I_GROUP_AMOUNT_ID"] = item5.Cells[3].Text.ToString();
                                            }
                                            TextBox txtModifierGrp = (TextBox)item5.FindControl("txtModifierGroup");
                                            if (txtModifierGrp.Text == "&nbsp" || txtModifierGrp.Text == "")
                                            {
                                                row["SZ_MODIFIER"] = "";
                                            }
                                            else
                                            {
                                                row["SZ_MODIFIER"] = txtModifierGrp.Text;
                                            }
                                            row["I_EventID"] = item4.Cells[7].Text;
                                            row["SZ_VISIT_TYPE"] = item4.Cells[3].Text;
                                            if (num == 1)
                                            {
                                                row["BT_IS_LIMITE"] = "1";
                                            }
                                            else
                                            {
                                                row["BT_IS_LIMITE"] = "0";
                                            }
                                            if (txtModifierGrp.Text == "&nbsp" || txtModifierGrp.Text == "")
                                            {
                                                row["SZ_MODIFIER_CODE"] = "";
                                            }
                                            else
                                            {
                                                row["SZ_MODIFIER_CODE"] = txtModifierGrp.Text;
                                            }
                                            table.Rows.Add(row);
                                            num3++;
                                        }
                                        continue;
                                    }
                                }
                                continue;
                            }
                        }
                    }
                    else
                    {
                        int num4 = 0;
                        foreach (DataGridItem item6 in this.grdTransactionDetails.Items)
                        {
                            string str6 = item6.Cells[1].Text;
                            foreach (DataGridItem item7 in this.grdGroupProcCodeService.Items)
                            {
                                CheckBox box4 = (CheckBox)item7.FindControl("chkselect");
                                if (box4.Checked)
                                {
                                    DataSet set3 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item7.Cells[1].Text.ToString(), this.txtCompanyID.Text, item7.Cells[2].Text.ToString());
                                    int num5 = 1;
                                    foreach (DataRow row4 in set3.Tables[0].Rows)
                                    {
                                        for (int i = 0; i < table.Rows.Count; i++)
                                        {
                                            if (str6 != "")
                                            {
                                                if ((table.Rows[i][2].ToString() == row4.ItemArray.GetValue(0).ToString()) && (DateTime.Compare(Convert.ToDateTime(str6), Convert.ToDateTime(table.Rows[i][1].ToString())) == 0))
                                                {
                                                    num4 = 1;
                                                    break;
                                                }
                                                num4 = 2;
                                            }
                                            else
                                            {
                                                num4 = 0;
                                            }
                                        }
                                        if (num4 == 2)
                                        {
                                            row = table.NewRow();
                                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                                            row["DT_DATE_OF_SERVICE"] = str6;
                                            row["SZ_PROCEDURE_ID"] = row4.ItemArray.GetValue(0);
                                            row["SZ_PROCEDURAL_CODE"] = row4.ItemArray.GetValue(2);
                                            row["SZ_CODE_DESCRIPTION"] = row4.ItemArray.GetValue(3);
                                            row["FLT_AMOUNT"] = row4.ItemArray.GetValue(4);
                                            row["I_UNIT"] = "1";
                                            row["SZ_TYPE_CODE_ID"] = row4.ItemArray.GetValue(1);
                                            if ((num5 == set3.Tables[0].Rows.Count) && (item7.Cells[4].Text.ToString() != "") && (item7.Cells[4].Text.ToString() != "0.00"))
                                            {
                                                row["FLT_GROUP_AMOUNT"] = item7.Cells[4].Text.ToString();
                                            }
                                            if (item7.Cells[3].Text.ToString() != "" && (item7.Cells[4].Text.ToString() != "0.00"))
                                            {
                                                row["I_GROUP_AMOUNT_ID"] = item7.Cells[3].Text.ToString();
                                            }
                                            row["I_EventID"] = item6.Cells[13].Text;
                                            table.Rows.Add(row);
                                            num5++;
                                        }
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridItem item8 in this.grdGroupProcCodeService.Items)
                {
                    CheckBox box5 = (CheckBox)item8.FindControl("chkselect");
                    if (box5.Checked)
                    {
                        DataSet set4 = this._bill_Sys_BillTransaction.GroupProcedureCodeList(item8.Cells[1].Text.ToString(), this.txtCompanyID.Text, item8.Cells[2].Text.ToString());
                        int num7 = 1;
                        foreach (DataRow row5 in set4.Tables[0].Rows)
                        {
                            row = table.NewRow();
                            row["SZ_BILL_TXN_DETAIL_ID"] = "";
                            row["DT_DATE_OF_SERVICE"] = this.txtBillDate.Text;
                            row["SZ_PROCEDURE_ID"] = row5.ItemArray.GetValue(0);
                            row["SZ_PROCEDURAL_CODE"] = row5.ItemArray.GetValue(2);
                            row["SZ_CODE_DESCRIPTION"] = row5.ItemArray.GetValue(3);
                            row["FLT_AMOUNT"] = row5.ItemArray.GetValue(4);
                            row["I_UNIT"] = "1";
                            row["SZ_TYPE_CODE_ID"] = row5.ItemArray.GetValue(1);
                            if ((num7 == set4.Tables[0].Rows.Count) && (item8.Cells[4].Text.ToString() != "") && (item8.Cells[4].Text.ToString() != "0.00"))
                            {
                                row["FLT_GROUP_AMOUNT"] = item8.Cells[4].Text.ToString();
                            }
                            if (item8.Cells[3].Text.ToString() != "" && (item8.Cells[4].Text.ToString() != "") && (item8.Cells[4].Text.ToString() != "0.00"))
                            {
                                row["I_GROUP_AMOUNT_ID"] = item8.Cells[3].Text.ToString();
                            }
                            table.Rows.Add(row);
                            num7++;
                        }
                        continue;
                    }
                }
            }
            new DataView();
            table.DefaultView.Sort = "DT_DATE_OF_SERVICE";
            this.grdTransactionDetails.DataSource = table;
            this.grdTransactionDetails.DataBind();
            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
            {
                this.grdTransactionDetails.Columns[5].Visible = false;
                this.grdTransactionDetails.Columns[6].Visible = true;
            }
            else
            {
                this.grdTransactionDetails.Columns[5].Visible = true;
                this.grdTransactionDetails.Columns[6].Visible = false;
            }
            double num8 = 0.0;
            if (this.grdTransactionDetails.Items.Count > 0)
            {
                BillTransactionDAO ndao2 = new BillTransactionDAO();
                string str7 = this.grdTransactionDetails.Items[0].Cells[2].Text.ToString();
                this.grdTransactionDetails.Items[0].Cells[15].Text.ToString();
                string str8 = ndao2.GetProcID(this.txtCompanyID.Text, str7);
                string str9 = ndao2.GET_IS_LIMITE(this.txtCompanyID.Text, str8);
                if ((str9 != "") && (str9 != "NULL"))
                {
                    for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                    {
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            TextBox box6 = (TextBox)this.grdTransactionDetails.Items[j].Cells[6].FindControl("txtAmt");
                            string str10 = box6.Text.ToString();
                            if ((str10 != "") && (str10 != "&nbsp;"))
                            {
                                num8 += Convert.ToDouble(str10);
                            }
                        }
                        else if ((this.grdTransactionDetails.Items[j].Cells[5].Text != "") && (this.grdTransactionDetails.Items[j].Cells[5].Text != "&nbsp;"))
                        {
                            num8 += Convert.ToDouble(this.grdTransactionDetails.Items[j].Cells[5].Text);
                        }
                        if (j == (this.grdTransactionDetails.Items.Count - 1))
                        {
                            BillTransactionDAO ndao3 = new BillTransactionDAO();
                            string str11 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str12 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string str13 = ndao3.GetProcID(this.txtCompanyID.Text, str11);
                            string str14 = ndao3.GetLimit(this.txtCompanyID.Text, str12, str13);
                            if (str14 != "")
                            {
                                if (Convert.ToDouble(str14) < num8)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = str14;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = num8.ToString();
                                }
                            }
                            num8 = 0.0;
                        }
                        else if (this.grdTransactionDetails.Items[j].Cells[1].Text != this.grdTransactionDetails.Items[j + 1].Cells[1].Text)
                        {
                            BillTransactionDAO ndao4 = new BillTransactionDAO();
                            string str15 = this.grdTransactionDetails.Items[j].Cells[2].Text.ToString();
                            string str16 = this.grdTransactionDetails.Items[j].Cells[15].Text.ToString();
                            string str17 = ndao4.GetProcID(this.txtCompanyID.Text, str15);
                            string str18 = ndao4.GetLimit(this.txtCompanyID.Text, str16, str17);
                            if (str18 != "")
                            {
                                if (Convert.ToDouble(str18) < num8)
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = str18;
                                }
                                else
                                {
                                    this.grdTransactionDetails.Items[j].Cells[10].Text = num8.ToString();
                                }
                            }
                            num8 = 0.0;
                        }
                    }
                }
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            if (this._bill_Sys_BillTransaction.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int k = 0; k < this.grdTransactionDetails.Items.Count; k++)
                {
                    TextBox box7 = (TextBox)this.grdTransactionDetails.Items[k].FindControl("txtUnit");
                    box7.Text = this.grdTransactionDetails.Items[k].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
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

    protected void lnkbtnRemoveDiag_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < this.lstDiagnosisCodes.Items.Count; i++)
            {
                if (this.lstDiagnosisCodes.Items[i].Selected)
                {
                    this.lstDiagnosisCodes.Items.Remove(this.lstDiagnosisCodes.Items[i]);
                    i--;
                }
            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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

    protected void lnkShowGrid_Click(object sender, EventArgs e)
    {
    }

    protected void lnlInitialReport_Click(object sender, EventArgs e)
    {
        this.Session["TEMPLATE_BILL_NO"] = this.Session["BillID"].ToString();
        this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformation.aspx'); ", true);
    }

    protected void lnlProgessReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.Session["TEMPLATE_BILL_NO"] = this.Session["BillID"].ToString();
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('../Bill_Sys_PatientInformationC4_2.aspx'); ", true);
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

    public void LoadProcedure(string I_Event_Id, DataGridCommandEventArgs index)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataRow row;
            ArrayList list;
            DataTable table = new DataTable();
            table.Columns.Add("SZ_BILL_TXN_DETAIL_ID");
            table.Columns.Add("DT_DATE_OF_SERVICE");
            table.Columns.Add("SZ_PROCEDURE_ID");
            table.Columns.Add("SZ_PROCEDURAL_CODE");
            table.Columns.Add("SZ_CODE_DESCRIPTION");
            table.Columns.Add("FLT_AMOUNT");
            table.Columns.Add("I_UNIT");
            table.Columns.Add("SZ_TYPE_CODE_ID");
            table.Columns.Add("FLT_GROUP_AMOUNT");
            table.Columns.Add("I_GROUP_AMOUNT_ID");
            table.Columns.Add("I_EventID");
            string doctorId = "";
            ArrayList list2 = new ArrayList();
            DataSet visitTypeList = new DataSet();
            Bill_Sys_Visit_BO t_bo = new Bill_Sys_Visit_BO();
            visitTypeList = t_bo.GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            if (index.Item.Cells[9].Text != "1")
            {
                CheckBox box = (CheckBox)index.Item.FindControl("chkSelectItem");
                if (box.Checked)
                {
                    string text = index.Item.Cells[2].Text;
                    for (int i = 0; i < visitTypeList.Tables[0].Rows.Count; i++)
                    {
                        if (text == visitTypeList.Tables[0].Rows[i][1].ToString())
                        {
                            list = new ArrayList();
                            list.Add(this.txtCompanyID.Text);
                            list.Add(index.Item.Cells[6].Text);
                            list.Add(text);
                            list.Add(index.Item.Cells[7].Text);
                            DataTable procedureCodeList = new DataTable();
                            procedureCodeList = t_bo.GetProcedureCodeList(list);
                            list2.Add(procedureCodeList);
                        }
                    }
                    doctorId = index.Item.Cells[8].Text;
                    if (this.hndDoctorID.Value != doctorId)
                    {
                        this.grdTransactionDetails.DataSource = null;
                        this.grdTransactionDetails.DataBind();
                        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                        {
                            this.grdTransactionDetails.Columns[5].Visible = false;
                            this.grdTransactionDetails.Columns[6].Visible = true;
                        }
                        else
                        {
                            this.grdTransactionDetails.Columns[5].Visible = true;
                            this.grdTransactionDetails.Columns[6].Visible = false;
                        }
                        this.lstDiagnosisCodes.Items.Clear();
                        Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
                        this.lstDiagnosisCodes.DataSource = ebo.GetCaseDiagnosisCode(this.txtCaseID.Text, doctorId, this.txtCompanyID.Text).Tables[0];
                        this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                        this.lstDiagnosisCodes.DataValueField = "CODE";
                        this.lstDiagnosisCodes.DataBind();
                        this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
                    }
                    this.hndDoctorID.Value = index.Item.Cells[8].Text;
                }
            }
            else
            {
                foreach (DataGridItem item in this.grdCompleteVisit.Items)
                {
                    CheckBox box2 = (CheckBox)item.FindControl("chkSelectItem");
                    if (box2.Checked)
                    {
                        string str3 = item.Cells[3].Text;
                        for (int j = 0; j < visitTypeList.Tables[0].Rows.Count; j++)
                        {
                            if (str3 == visitTypeList.Tables[0].Rows[j][1].ToString())
                            {
                                list = new ArrayList();
                                list.Add(this.txtCompanyID.Text);
                                list.Add(item.Cells[6].Text);
                                list.Add(str3);
                                list.Add(item.Cells[7].Text);
                                DataTable table3 = new DataTable();
                                table3 = t_bo.GetProcedureCodeList(list);
                                list2.Add(table3);
                            }
                        }
                        doctorId = index.Item.Cells[8].Text;
                        if (this.hndDoctorID.Value != doctorId)
                        {
                            this.grdTransactionDetails.DataSource = null;
                            this.grdTransactionDetails.DataBind();
                            if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                            {
                                this.grdTransactionDetails.Columns[5].Visible = false;
                                this.grdTransactionDetails.Columns[6].Visible = true;
                            }
                            else
                            {
                                this.grdTransactionDetails.Columns[5].Visible = true;
                                this.grdTransactionDetails.Columns[6].Visible = false;
                            }
                            this.lstDiagnosisCodes.Items.Clear();
                            Bill_Sys_AssociateDiagnosisCodeBO ebo2 = new Bill_Sys_AssociateDiagnosisCodeBO();
                            this.lstDiagnosisCodes.DataSource = ebo2.GetCaseDiagnosisCode(this.txtCaseID.Text, doctorId, this.txtCompanyID.Text).Tables[0];
                            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
                            this.lstDiagnosisCodes.DataValueField = "CODE";
                            this.lstDiagnosisCodes.DataBind();
                            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
                        }
                        this.hndDoctorID.Value = index.Item.Cells[8].Text;
                    }
                }
            }
            if (index.Item.Cells[9].Text != "1")
            {
                this.btnAddServices.Visible = true;
                this.btnAddGroup.Visible = true;
                foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                {
                    if (((!(item2.Cells[1].Text.ToString() != "") || !(item2.Cells[1].Text.ToString() != "&nbsp;")) || (!(item2.Cells[3].Text.ToString() != "") || !(item2.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item2.Cells[4].Text.ToString() != "") || !(item2.Cells[4].Text.ToString() != "&nbsp;")))
                    {
                        continue;
                    }
                    row = table.NewRow();
                    if ((item2.Cells[0].Text.ToString() != "&nbsp;") && (item2.Cells[0].Text.ToString() != ""))
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = item2.Cells[0].Text.ToString();
                    }
                    else
                    {
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                    }
                    row["DT_DATE_OF_SERVICE"] = Convert.ToDateTime(item2.Cells[1].Text.ToString()).ToShortDateString();
                    row["SZ_PROCEDURE_ID"] = item2.Cells[2].Text.ToString();
                    row["SZ_PROCEDURAL_CODE"] = item2.Cells[3].Text.ToString();
                    row["SZ_CODE_DESCRIPTION"] = item2.Cells[4].Text.ToString();
                    if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                    {
                        row["FLT_AMOUNT"] = ((TextBox)item2.Cells[6].FindControl("txtAmt")).Text.ToString();
                    }
                    else
                    {
                        row["FLT_AMOUNT"] = item2.Cells[5].Text.ToString();
                    }
                    if ((((TextBox)item2.Cells[7].FindControl("txtUnit")).Text.ToString() != "") && (Convert.ToInt32(((TextBox)item2.Cells[7].FindControl("txtUnit")).Text.ToString()) > 0))
                    {
                        row["I_UNIT"] = ((TextBox)item2.Cells[6].FindControl("txtUnit")).Text.ToString();
                    }
                    row["SZ_TYPE_CODE_ID"] = item2.Cells[9].Text.ToString();
                    row["FLT_GROUP_AMOUNT"] = item2.Cells[10].Text.ToString();
                    row["I_GROUP_AMOUNT_ID"] = item2.Cells[11].Text.ToString();
                    row["I_EventID"] = item2.Cells[13].Text.ToString();
                    table.Rows.Add(row);
                }
            }
            if (index.Item.Cells[9].Text == "1")
            {
                this.btnAddServices.Visible = false;
                this.btnAddGroup.Visible = false;
                this.GetProcedureCode(doctorId);
                this.hndDoctorID.Value = doctorId;
                for (int k = 0; k < list2.Count; k++)
                {
                    DataTable table4 = (DataTable)list2[k];
                    foreach (DataRow row2 in table4.Rows)
                    {
                        row = table.NewRow();
                        row["SZ_BILL_TXN_DETAIL_ID"] = "";
                        row["DT_DATE_OF_SERVICE"] = row2.ItemArray.GetValue(5).ToString();
                        row["SZ_PROCEDURE_ID"] = row2.ItemArray.GetValue(0).ToString();
                        row["SZ_PROCEDURAL_CODE"] = row2.ItemArray.GetValue(2).ToString();
                        row["SZ_CODE_DESCRIPTION"] = row2.ItemArray.GetValue(3).ToString();
                        row["FLT_AMOUNT"] = row2.ItemArray.GetValue(4).ToString();
                        row["I_UNIT"] = "1";
                        row["SZ_TYPE_CODE_ID"] = row2.ItemArray.GetValue(1).ToString();
                        row["I_EventID"] = row2.ItemArray.GetValue(6).ToString();
                        table.Rows.Add(row);
                    }
                }
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    this.grdTransactionDetails.Columns[5].Visible = false;
                    this.grdTransactionDetails.Columns[6].Visible = true;
                }
                else
                {
                    this.grdTransactionDetails.Columns[5].Visible = true;
                    this.grdTransactionDetails.Columns[6].Visible = false;
                }
                Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
                if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
                {
                    this.grdTransactionDetails.Columns[7].Visible = true;
                    for (int m = 0; m < this.grdTransactionDetails.Items.Count; m++)
                    {
                        TextBox box3 = (TextBox)this.grdTransactionDetails.Items[m].FindControl("txtUnit");
                        box3.Text = this.grdTransactionDetails.Items[m].Cells[8].Text;
                    }
                }
                else
                {
                    this.grdTransactionDetails.Columns[7].Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.objSystemObject = (Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"];
        try
        {
            string str;
            try
            {
                if ((base.Request.QueryString["CheckSession"] != "") && (base.Request.QueryString["CheckSession"] != null))
                {
                    if (base.Request.QueryString["CheckSession"] != ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString())
                    {
                        Bill_Sys_CaseObject obj2 = new Bill_Sys_CaseObject();
                        CaseDetailsBO sbo = new CaseDetailsBO();
                        string str2 = base.Request.QueryString["CheckSession"];

                        #region Single procedure 
                        obj2.SZ_PATIENT_ID = sbo.GetCasePatientID(str2, "");
                        obj2.SZ_CASE_ID = str2;
                        obj2.SZ_PATIENT_NAME = sbo.GetPatientName(obj2.SZ_PATIENT_ID);
                        obj2.SZ_COMAPNY_ID = sbo.GetPatientCompanyID(obj2.SZ_PATIENT_ID);
                        obj2.SZ_CASE_NO = sbo.GetCaseNo(obj2.SZ_CASE_ID, obj2.SZ_COMAPNY_ID);
                        #endregion

                        this.Session["CASE_OBJECT"] = obj2;
                        Bill_Sys_Case @case = new Bill_Sys_Case();
                        @case.SZ_CASE_ID = str2;
                        this.Session["CASEINFO"] = @case;
                        base.Response.Clear();
                        base.Response.ClearContent();
                        base.Response.Write("changed");
                    }
                    else
                    {
                        base.Response.Clear();
                        base.Response.ClearContent();
                        base.Response.Write("same");
                    }
                }
            }
            catch
            {
            }
            this.btnLoadProcedures.Visible = true;
            this.btnClearService.Visible = true;
            this.lnkAddDiagnosis.Visible = true;
            this.lnkbtnRemoveDiag.Visible = true;
            this.btnRemove.Visible = true;
            this.btnSave.Visible = true;
            this.btnUpdate.Visible = true;
            if (((base.Request.QueryString["CaseID"] != null) && (base.Request.QueryString["pname"] != null)) && (base.Request.QueryString["cmpid"].ToString() != null))
            {
                Bill_Sys_CaseObject obj3 = new Bill_Sys_CaseObject();
                CaseDetailsBO sbo2 = new CaseDetailsBO();
                obj3.SZ_PATIENT_ID = sbo2.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), "");
                obj3.SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString();
                obj3.SZ_PATIENT_NAME = base.Request.QueryString["pname"].ToString();
                obj3.SZ_CASE_NO = sbo2.GetCaseNo(obj3.SZ_CASE_ID, base.Request.QueryString["cmpid"].ToString());
                this.Session["CASE_OBJECT"] = obj3;
                Bill_Sys_Case case2 = new Bill_Sys_Case();
                case2.SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString();
                this.Session["CASEINFO"] = case2;
            }
            if ((base.Request.QueryString["CaseID"] != null) && (base.Request.QueryString["bno"] != null))
            {
                Bill_Sys_CaseObject obj4 = new Bill_Sys_CaseObject();
                CaseDetailsBO sbo3 = new CaseDetailsBO();
                string s = base.Request.QueryString["CaseID"];
                byte[] bytes = Convert.FromBase64String(s);
                s = Encoding.ASCII.GetString(bytes);
                this.txtCaseIDdummy.Text = s;
                string str4 = base.Request.QueryString["bno"];
                byte[] buffer2 = Convert.FromBase64String(str4);
                this.Session["SZ_BILL_NUMBER"] = Encoding.ASCII.GetString(buffer2);
                obj4.SZ_PATIENT_ID = sbo3.GetCasePatientID(s, "");
                obj4.SZ_CASE_ID = s;
                obj4.SZ_PATIENT_NAME = sbo3.GetPatientName(obj4.SZ_PATIENT_ID);
                obj4.SZ_COMAPNY_ID = sbo3.GetPatientCompanyID(obj4.SZ_PATIENT_ID);
                obj4.SZ_CASE_NO = sbo3.GetCaseNo(obj4.SZ_CASE_ID, obj4.SZ_COMAPNY_ID);
                this.Session["CASE_OBJECT"] = obj4;
                Bill_Sys_Case case3 = new Bill_Sys_Case();
                case3.SZ_CASE_ID = s;
                this.Session["CASEINFO"] = case3;
            }
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                base.Response.Redirect(@"..\Bill_Sys_ReferralBillTransaction.aspx?Type=Search", false);
            }
            this.dummybtnAddServices.Visible = true;
            this.dummybtnAddGroup.Visible = true;
            if (!base.IsPostBack)
            {
                if (((base.Request.QueryString["BillNo"] != null) && (base.Request.QueryString["caseid"] != null)) && (base.Request.QueryString["caseno"] != null))
                {
                    Bill_Sys_CaseObject obj5 = new Bill_Sys_CaseObject();
                    CaseDetailsBO sbo4 = new CaseDetailsBO();
                    string str5 = base.Request.QueryString["CaseID"];
                    obj5.SZ_PATIENT_ID = sbo4.GetCasePatientID(str5, "");
                    obj5.SZ_CASE_ID = str5;
                    obj5.SZ_PATIENT_NAME = sbo4.GetPatientName(obj5.SZ_PATIENT_ID);
                    obj5.SZ_COMAPNY_ID = sbo4.GetPatientCompanyID(obj5.SZ_PATIENT_ID);
                    obj5.SZ_CASE_NO = sbo4.GetCaseNo(obj5.SZ_CASE_ID, obj5.SZ_COMAPNY_ID);
                    this.Session["CASE_OBJECT"] = obj5;
                    Bill_Sys_Case case4 = new Bill_Sys_Case();
                    case4.SZ_CASE_ID = str5;
                    this.Session["CASEINFO"] = case4;
                    this.Session["PassedCaseID"] = str5;
                    this.Session["SZ_BILL_NUMBER"] = base.Request.QueryString["BillNo"].ToString();
                }
                CaseDetailsBO sbo5 = new CaseDetailsBO();
                if ((sbo5.GetCaseStatusArchived(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID) == "2") && ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower() != "admin")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>alert('You can not create bill for this patient..');window.document.location.href='Bill_Sys_CaseDetails.aspx';</script>");
                    return;
                }
                if (base.Request.QueryString["Type"] == null)
                {
                    if (this.Session["CASE_OBJECT"] != null)
                    {
                        this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this.txtCaseIDdummy.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this.BindVisitCompleteGrid();
                        this.dvcompletevisit.Visible = true;
                    }
                    this.Session["SZ_BILL_NUMBER"] = null;
                }
            }
            if (this.Session["CASE_OBJECT"] != null)
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.txtCaseIDdummy.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.txtCaseNo.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                Bill_Sys_Case case5 = new Bill_Sys_Case();
                case5.SZ_CASE_ID = this.txtCaseID.Text;
                this.Session["CASEINFO"] = case5;
                string text = this.txtCaseID.Text;
                this.Session["QStrCaseID"] = text;
                this.Session["Case_ID"] = text;
                this.Session["Archived"] = "0";
                this.Session["QStrCID"] = text;
                this.Session["SelectedID"] = text;
                this.Session["DM_User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["User_Name"] = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                this.Session["SN"] = "0";
                this.Session["LastAction"] = "vb_CaseInformation.aspx";
                this.Session["SZ_CASE_ID_NOTES"] = this.txtCaseID.Text;
                this.Session["TM_SZ_CASE_ID"] = this.txtCaseID.Text;
                this.GetPatientDeskList();
            }
            else
            {
                base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
            }
            this.lblLocationNote.Text = "";
            if (!base.IsPostBack)
            {
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
                {
                    CaseDetailsBO sbo6 = new CaseDetailsBO();
                    string patientLocationID = sbo6.GetPatientLocationID(this.txtCaseID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    if ((patientLocationID != null) && patientLocationID.Equals(""))
                    {
                        this.lblLocationNote.Text = "Note: There is no office location set for the patient / doctor";
                    }
                    DataSet set = sbo6.DoctorName(patientLocationID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.extddlDoctor.DataSource = set;
                    this.extddlDoctor.DataTextField = "DESCRIPTION";
                    this.extddlDoctor.DataValueField = "CODE";
                    this.extddlDoctor.DataBind();
                    ListItem item = new ListItem("---select---", "NA");
                    this.extddlDoctor.Items.Insert(0, item);
                }
                Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
                if (n_bo.checkNf2(this.txtCompanyID.Text, this.txtCaseID.Text))
                {
                    this.txtNf2.Text = "1";
                    this.chkNf2.Checked = true;
                }
                else
                {
                    this.txtNf2.Text = "0";
                    this.chkNf2.Checked = false;
                }
            }
            if (base.Request.QueryString["Type"] == null)
            {
                if (this.dvcompletevisit.Visible)
                {
                    str = "";
                }
                else
                {
                    str = "flag";
                }
            }
            else
            {
                str = base.Request.QueryString["Type"].ToString();
            }
            this.btnSave.Attributes.Add("onclick", "return ConfirmClaimInsurance();");
            this.btnUpdate.Attributes.Add("onclick", "return FormValidation();");
            this.btnAddServices.Attributes.Add("onclick", "return completeGridValidator('" + str + "');");
            this.btnAddGroup.Attributes.Add("onclick", "return CompleteVisitGroupVisit('" + str + "');");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlDiagnosisType.Flag_ID = this.txtCompanyID.Text;
            string doctorID = "";
            if (!base.IsPostBack)
            {
                this.btnUpdate.Enabled = false;
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this.txtBillID.Text = this.Session["SZ_BILL_NUMBER"].ToString();
                    doctorID = new Bill_Sys_Visit_BO().GetDoctorID(this.txtBillID.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.BindDoctorsGrid(this.txtBillID.Text);
                    this.dvcompletevisit.Visible = true;
                    this.hndDoctorID.Value = doctorID;
                    this.BindTransactionData(this.txtBillID.Text);
                    this.btnSave.Enabled = false;
                    this.btnUpdate.Enabled = true;
                }
                this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
                this.txtBillDatedummy.Text = DateTime.Now.Date.ToShortDateString();
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
                if (base.Request.QueryString["PopUp"] != null)
                {
                    if (this.Session["TEMP_DOCTOR_ID"] != null)
                    {
                        this.extddlDoctor.SelectedValue = this.Session["TEMP_DOCTOR_ID"].ToString();
                    }
                    this.GetProcedureCode(this.hndDoctorID.Value.ToString());
                }
                if (this.Session["SZ_BILL_NUMBER"] != null)
                {
                    this.txtBillID.Text = this.Session["SZ_BILL_NUMBER"].ToString();
                    this._editOperation = new EditOperation();
                    this._editOperation.Primary_Value = this.Session["SZ_BILL_NUMBER"].ToString();
                    this._editOperation.WebPage = this.Page;
                    this._editOperation.Xml_File = "BillTransaction.xml";
                    this._editOperation.LoadData();
                    this.txtBillDate.Text = string.Format("{0:MM/dd/yyyy}", this.txtBillDate.Text).ToString();
                    this.setDefaultValues(this.Session["SZ_BILL_NUMBER"].ToString());
                }
                this.BindLatestTransaction();
            }
            else if (this.Session["SELECTED_DIA_PRO_CODE"] != null)
            {
                DataTable table = new DataTable();
                table = (DataTable)this.Session["SELECTED_DIA_PRO_CODE"];
                this.grdTransactionDetails.DataSource = table;
                this.grdTransactionDetails.DataBind();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    this.grdTransactionDetails.Columns[5].Visible = false;
                    this.grdTransactionDetails.Columns[6].Visible = true;
                }
                else
                {
                    this.grdTransactionDetails.Columns[5].Visible = true;
                    this.grdTransactionDetails.Columns[6].Visible = false;
                }
                this.Session["SELECTED_DIA_PRO_CODE"] = null;
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            foreach (DataGridItem item2 in this.grdLatestBillTransaction.Items)
            {
                if (this.objCaseDetailsBO.GetCaseType(item2.Cells[1].Text) != "WC000000000000000001")
                {
                    item2.Cells[13].Text = "";
                    item2.Cells[14].Text = "";
                    item2.Cells[15].Text = "";
                    item2.Cells[0x10].Text = "";
                }
            }
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList claimInsurance = this._bill_Sys_BillTransaction.GetClaimInsurance(this.txtCaseID.Text);
            if (claimInsurance.Count > 0)
            {
                if (((claimInsurance[0].ToString() != "") && (claimInsurance[0].ToString() != "NA")) && ((claimInsurance[2].ToString() != "") && (claimInsurance[2].ToString() != "NA")))
                {
                    if ((claimInsurance[1].ToString() != "") && (claimInsurance[1].ToString() != ""))
                    {
                        this.txtClaimInsurance.Text = "3";
                    }
                    else
                    {
                        this.txtClaimInsurance.Text = "2";
                    }
                }
                else
                {
                    this.txtClaimInsurance.Text = "1";
                }
            }
            else
            {
                this.txtClaimInsurance.Text = "0";
            }
            if (!this.Page.IsPostBack)
            {
                this.Session["DELETED_PROC_CODES"] = null;
            }
            this.SetControlForUpdateBill();
            this.objCaseDetailsBO = new CaseDetailsBO();
            if (this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID) == "WC000000000000000001")
            {
                this.grdTransactionDetails.Columns[14].Visible = false;
            }
            this.checksession();
            if (!base.IsPostBack)
            {
                this.checkLimit();
            }
            if (!base.IsPostBack)
            {
                Bill_Sys_PatientBO tbo = new Bill_Sys_PatientBO();
                new DataSet();
                if (tbo.Get__Compid_Caseid(this.txtCaseID.Text, this.txtCompanyID.Text).ToLower() == "hidden")
                {
                    this.txtNewNF2.Value = "0";
                }
                else
                {
                    this.txtNewNF2.Value = "1";
                }
            }
            if (!base.IsPostBack)
            {
                this.txtCaseID.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                DataSet set2 = new DataSet();
                set2 = this.Get_Bit_To_Change_Amount(this.txtCaseID.Text);
                bt_diagnosis_code = set2.Tables[0].Rows[0]["bt_diagnosis_code"].ToString().ToLower();
                bt_Change_Amount = set2.Tables[0].Rows[0]["bt_modify"].ToString().ToLower();
                if (bt_diagnosis_code == "true")
                {
                    this.hdnValue.Value = "1";
                }
                else
                {
                    this.hdnValue.Value = "0";
                }
            }
        }
        catch (Exception exception)
        {
            log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + exception.Message.ToString());
            log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + exception.StackTrace.ToString());
            if (exception.InnerException != null)
            {
                log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + exception.InnerException.Message.ToString());
                log.Debug("Bill_Sys_BillTransaction. Method - Page_Load : " + exception.InnerException.StackTrace.ToString());
            }
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
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

    protected void Page_Load_Complete(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.SessionCheck.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
        }
        catch (Exception ex)
        {
            this.SessionCheck.Value = "";
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string str = "";
        string str2 = "";
        for (int i = 0; i < this.grdCompleteVisit.Items.Count; i++)
        {
            string text = this.grdCompleteVisit.Items[i].Cells[6].Text;
            string text2 = this.grdCompleteVisit.Items[i].Cells[3].Text;
            str = this.grdCompleteVisit.Items[i].Cells[12].Text;
            str2 = this.grdCompleteVisit.Items[i].Cells[13].Text;
            if (str != null)
            {
                if (str.ToLower().Equals("true") || str.ToLower().Equals("1"))
                {
                    if (str2 != null)
                    {
                        if (str2.ToLower().Equals("false") || str2.ToLower().Equals("0"))
                        {
                            CheckBox box = (CheckBox)this.grdCompleteVisit.Items[i].FindControl("chkSelectItem");
                            box.Enabled = false;
                            LinkButton button = (LinkButton)this.grdCompleteVisit.Items[i].FindControl("LinkBtnCount");
                            button.Enabled = false;
                            Label label = (Label)this.grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                            label.Text = this.grdCompleteVisit.Items[i].Cells[15].Text + ", [Doctor], Not Finalized";
                        }
                        else
                        {
                            Label label2 = (Label)this.grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                            label2.Text = this.grdCompleteVisit.Items[i].Cells[15].Text + ", [Doctor], Finalized";
                        }
                    }
                }
                else
                {
                    Label label3 = (Label)this.grdCompleteVisit.Items[i].FindControl("lblAddedByDoctor");
                    label3.Text = this.grdCompleteVisit.Items[i].Cells[15].Text + ", [User]";
                }
            }
        }
        if (!IsPostBack)
        {

            if (Request.QueryString["Message"] != null)
            {

                this.usrMessage.PutMessage(Request.QueryString["Message"].ToString());
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();

            }
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
        this.Session["SZ_BILL_ID"] = null;
        this.Session["PassedCaseID"] = null;
    }

    protected void RefferalModelPopUp(string SZ_Doctor_Id, string SZ_Is_added_by_doctor, string SZ_finalised, string SZ_SpecialityID, string SZ_Event_Id, string SZ_Is_Rff)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string doctorId = "";
            new ArrayList();
            new DataSet();
            new Bill_Sys_Visit_BO().GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            doctorId = SZ_Doctor_Id;
            if (doctorId != "")
            {
                this.GetProcedureCode(doctorId);
                this.hndDoctorID.Value = doctorId;
            }
            else
            {
                this.GetProcedureCode(this.hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                {
                    TextBox box = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                    box.Text = this.grdTransactionDetails.Items[j].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            if (this.hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupAddservice.Show();
            }
            else if (this.hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupaddgroup.Show();
            }
            for (int i = 0; i < this.grdProcedure.Items.Count; i++)
            {
                CheckBox box2 = (CheckBox)this.grdProcedure.Items[i].FindControl("chkselect");
                box2.Checked = false;
            }
            string str2 = SZ_Event_Id;
            Bill_Sys_BillTransaction_BO n_bo2 = new Bill_Sys_BillTransaction_BO();
            DataSet set = new DataSet();
            set = n_bo2.GET_PROC_CODE_USING_EVENT_ID(str2);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                {
                    for (int m = 0; m < this.grdProcedure.Items.Count; m++)
                    {
                        CheckBox box3 = (CheckBox)this.grdProcedure.Items[m].FindControl("chkselect");
                        if (set.Tables[0].Rows[k]["SZ_PROC_CODE"].ToString() == this.grdProcedure.Items[m].Cells[2].Text)
                        {
                            box3.Checked = true;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void saveQuickBills(string sz_docID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            BillTransactionEO neo = new BillTransactionEO();
            neo.SZ_CASE_ID = this.txtCaseID.Text;
            neo.SZ_COMPANY_ID = this.txtCompanyID.Text;
            neo.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
            neo.SZ_DOCTOR_ID = sz_docID;
            neo.SZ_TYPE = this.ddlType.Text;
            neo.SZ_TESTTYPE = "";
            neo.FLAG = "ADD";
            neo.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
            this._bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            if (this.grdCompleteVisit.Visible)
            {
                new Bill_Sys_Calender();
                foreach (DataGridItem item in this.grdCompleteVisit.Items)
                {
                    CheckBox box = (CheckBox)item.FindControl("chkSelectItem");
                    if (box.Checked)
                    {
                        string text = item.Cells[7].Text;
                        EventEO teo = new EventEO();
                        teo.I_EVENT_ID = text;
                        teo.BT_STATUS = "1";
                        teo.I_STATUS = "2";
                        teo.SZ_BILL_NUMBER = "";
                        teo.DT_BILL_DATE = Convert.ToDateTime(this.txtBillDate.Text);
                        list.Add(teo);
                        foreach (DataGridItem item2 in this.grdTransactionDetails.Items)
                        {
                            if ((item2.Cells[1].Text != "") && (DateTime.Compare(Convert.ToDateTime(item.Cells[1].Text), Convert.ToDateTime(item2.Cells[1].Text)) == 0))
                            {
                                EventRefferProcedureEO eeo = new EventRefferProcedureEO();
                                eeo.SZ_PROC_CODE = item2.Cells[9].Text;
                                eeo.I_EVENT_ID = text;
                                eeo.I_STATUS = "2";
                                list2.Add(eeo);
                            }
                        }
                        continue;
                    }
                }
            }
            this.objCaseDetailsBO = new CaseDetailsBO();
            this.objCaseDetailsBO.GetCaseTypeCaseID(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            ArrayList list3 = new ArrayList();
            foreach (DataGridItem item3 in this.grdTransactionDetails.Items)
            {
                if (((!(item3.Cells[1].Text.ToString() != "") || !(item3.Cells[1].Text.ToString() != "&nbsp;")) || (!(item3.Cells[3].Text.ToString() != "") || !(item3.Cells[3].Text.ToString() != "&nbsp;"))) || (!(item3.Cells[4].Text.ToString() != "") || !(item3.Cells[4].Text.ToString() != "&nbsp;")))
                {
                    continue;
                }
                BillProcedureCodeEO eeo2 = new BillProcedureCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo2.SZ_PROCEDURE_ID = item3.Cells[2].Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    string str3 = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                    if ((str3 != "&nbsp;") && (str3 == ""))
                    {
                        eeo2.FL_AMOUNT = item3.Cells[5].Text.ToString();
                    }
                    else
                    {
                        eeo2.FL_AMOUNT = "0";
                    }
                }
                else if (item3.Cells[5].Text.ToString() != "&nbsp;")
                {
                    eeo2.FL_AMOUNT = item3.Cells[5].Text.ToString();
                }
                else
                {
                    eeo2.FL_AMOUNT = "0";
                }
                eeo2.SZ_BILL_NUMBER = "";
                eeo2.DT_DATE_OF_SERVICE = Convert.ToDateTime(item3.Cells[1].Text.ToString());
                eeo2.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                eeo2.I_UNIT = ((TextBox)item3.Cells[7].FindControl("txtUnit")).Text.ToString();
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    eeo2.FLT_PRICE = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    eeo2.FLT_PRICE = item3.Cells[5].Text.ToString();
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    eeo2.DOCT_AMOUNT = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    eeo2.DOCT_AMOUNT = item3.Cells[5].Text.ToString();
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_ALLOW_TO_EDIT_CODE_AMOUNT == "1")
                {
                    eeo2.PROC_AMOUNT = ((TextBox)item3.Cells[6].FindControl("txtAmt")).Text.ToString();
                }
                else
                {
                    eeo2.PROC_AMOUNT = item3.Cells[5].Text.ToString();
                }
                eeo2.SZ_DOCTOR_ID = sz_docID;
                eeo2.SZ_CASE_ID = this.txtCaseID.Text;
                eeo2.SZ_TYPE_CODE_ID = item3.Cells[9].Text.ToString();
                if (item3.Cells[10].Text.ToString() != "&nbsp;")
                {
                    eeo2.FLT_GROUP_AMOUNT = item3.Cells[10].Text.ToString();
                }
                else
                {
                    eeo2.FLT_GROUP_AMOUNT = "";
                }
                if (((item3.Cells[11].Text.ToString() != "&nbsp;") && (item3.Cells[11].Text.ToString() != "&nbsp;")) && (item3.Cells[11].Text.ToString() != "&nbsp;"))
                {
                    eeo2.I_GROUP_AMOUNT_ID = item3.Cells[11].Text.ToString();
                }
                else
                {
                    eeo2.I_GROUP_AMOUNT_ID = "";
                }
                list3.Add(eeo2);
            }
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            ArrayList list4 = new ArrayList();
            foreach (ListItem item4 in this.lstDiagnosisCodes.Items)
            {
                BillDiagnosisCodeEO eeo3 = new BillDiagnosisCodeEO();
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                eeo3.SZ_DIAGNOSIS_CODE_ID = item4.Value.ToString();
                list4.Add(eeo3);
            }
            BillTransactionDAO ndao = new BillTransactionDAO();
            Result result = new Result();
            result = ndao.SaveBillTransactions(neo, list, list2, list3, list4);
            if (result.msg_code == "ERR")
            {
                this.usrMessage.PutMessage(result.msg);
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
            else
            {
                this.txtBillID.Text = result.bill_no;
                str = this.txtBillID.Text;
                this._DAO_NOTES_EO = new DAO_NOTES_EO();
                this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_CREATED";
                this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = str;
                this._DAO_NOTES_BO = new DAO_NOTES_BO();
                this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this._DAO_NOTES_EO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                string patientID = this.objCaseDetailsBO.GetPatientID(str);
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000001")
                {
                    this.objDefaultValue = new Bill_Sys_InsertDefaultValues();
                    if (this.grdLatestBillTransaction.Items.Count == 0)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorOpinion.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExamInformation.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_History.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PlanOfCare.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatus.xml"), this.txtCompanyID.Text.ToString(), null, patientID);
                    }
                    else if (this.grdLatestBillTransaction.Items.Count >= 1)
                    {
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_DoctorsOpinionC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_ExaminationTreatment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_PermanentImpairment.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                        this.objDefaultValue.InsertDefaultValues(base.Server.MapPath(@"..\Config\DV_WorkStatusC4_2.xml"), this.txtCompanyID.Text.ToString(), str, patientID);
                    }
                }
                this.BindLatestTransaction();
                this.BindVisitCompleteGrid();
                this.ClearControl();
                this.usrMessage.PutMessage(" Bill Saved successfully ! ");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                message = " Bill Saved successfully ! ";
                this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
                string doctorSpeciality = this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.objVerification_Desc = new Bill_Sys_Verification_Desc();
                this.objVerification_Desc.sz_bill_no = str;
                this.objVerification_Desc.sz_company_id = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.objVerification_Desc.sz_flag = "BILL";
                ArrayList list5 = new ArrayList();
                ArrayList list6 = new ArrayList();
                string str6 = "";
                string str7 = "";
                list5.Add(this.objVerification_Desc);
                list6 = this._bill_Sys_BillTransaction.Get_Node_Type(list5);
                if (list6.Contains("NFVER"))
                {
                    str6 = "OLD";
                    str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Bills/" + doctorSpeciality + "/";
                }
                else
                {
                    str6 = "NEW";
                    str7 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/No Fault File/Medicals/" + doctorSpeciality + "/Bills/";
                }
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000002")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000003")
                {
                    this.GenerateAddedBillPDF(str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID));
                }
                else if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000004")
                {
                    string str8;
                    Lien lien = new Lien();
                    string str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str9, doctorSpeciality, "", "Speciality");
                    string str10 = this._MUVGenerateFunction.get_bt_include(str9, "", "WC000000000000000004", "CaseType");
                    if ((this.bt_include == "True") && (str10 == "True"))
                    {
                        string str11 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                        string str12 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string str13 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str7 + lien.GenratePdfForLienWithMuv(str9, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str13, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str12), this.objNF3Template.getPhysicalPath() + str11 + this.str_1500, this.objNF3Template.getPhysicalPath() + str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str8 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str7 + this.str_1500.Replace(".pdf", "_MER.pdf");
                        ArrayList list7 = new ArrayList();
                        if (str6 == "OLD")
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(str7);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("NF");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list7);
                        }
                        else
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(str7);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("NF");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list7.Add(list6[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list7);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        str8 = lien.GenratePdfForLien(this.txtCompanyID.Text, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    }
                    this.pdfpath = str8;
                }
                else if (this.objCaseDetailsBO.GetCaseType(str) == "WC000000000000000007")
                {
                    string str8;
                    Employer lien = new Employer();
                    string str9 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.bt_include = this._MUVGenerateFunction.get_bt_include(str9, doctorSpeciality, "", "Speciality");
                    string str10 = this._MUVGenerateFunction.get_bt_include(str9, "", "WC000000000000000007", "CaseType");
                    if ((this.bt_include == "True") && (str10 == "True"))
                    {
                        string str11 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + this.Session["TM_SZ_CASE_ID"].ToString() + "/Packet Document/";
                        string str12 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        string str13 = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                        this.str_1500 = this._MUVGenerateFunction.FillPdf(this.Session["TM_SZ_BILL_ID"].ToString());
                        MergePDF.MergePDFFiles(this.objNF3Template.getPhysicalPath() + str7 + lien.GenratePdfForEmployerWithMuv(str9, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, str13, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, str12), this.objNF3Template.getPhysicalPath() + str11 + this.str_1500, this.objNF3Template.getPhysicalPath() + str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                        str8 = ApplicationSettings.GetParameterValue("DocumentManagerURL") + str7 + this.str_1500.Replace(".pdf", "_MER.pdf");
                        ArrayList list7 = new ArrayList();
                        if (str6 == "OLD")
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(str7);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("NF");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            this.objNF3Template.saveGeneratedBillPath(list7);
                        }
                        else
                        {
                            list7.Add(this.Session["TM_SZ_BILL_ID"].ToString());
                            list7.Add(str7 + this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            list7.Add(this.Session["TM_SZ_CASE_ID"]);
                            list7.Add(this.str_1500.Replace(".pdf", "_MER.pdf"));
                            list7.Add(str7);
                            list7.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME);
                            list7.Add(doctorSpeciality);
                            list7.Add("NF");
                            list7.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO);
                            list7.Add(list6[0].ToString());
                            this.objNF3Template.saveGeneratedBillPath_New(list7);
                        }
                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_GENERATED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = this.str_1500;
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                        this._DAO_NOTES_EO.SZ_CASE_ID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                    else
                    {
                        str8 = lien.GenratePdfForEmployer(this.txtCompanyID.Text, str, this._bill_Sys_BillTransaction.GetDoctorSpeciality(str, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID), ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    }
                    this.pdfpath = str8;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SetControlForUpdateBill()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.lblDateOfService.Style.Add("visibility", "hidden");
            this.txtDateOfservice.Style.Add("visibility", "hidden");
            this.Image1.Style.Add("visibility", "hidden");
            this.lblGroupServiceDate.Style.Add("visibility", "hidden");
            this.txtGroupDateofService.Style.Add("visibility", "hidden");
            this.imgbtnDateofService.Style.Add("visibility", "hidden");
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

    private void SetDataOfAssociate(string setID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataSet associatedEntry = new DataSet();
            associatedEntry = this._bill_Sys_BillTransaction.GetAssociatedEntry(setID);
            for (int i = 0; i <= (associatedEntry.Tables[0].Rows.Count - 1); i++)
            {
                this.extddlDoctor.SelectedValue = associatedEntry.Tables[0].Rows[i][1].ToString();
                this.hndDoctorID.Value = associatedEntry.Tables[0].Rows[i][1].ToString();
            }
            this.grdTransactionDetails.Visible = true;
            this.extddlDoctor.Enabled = false;
            this.Session["AssociateDiagnosis"] = true;
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

    protected void setDefaultValues(string p_szBillID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            this.txtBillDate.Text = DateTime.Now.Date.ToShortDateString();
            Bill_Sys_AssociateDiagnosisCodeBO ebo = new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lstDiagnosisCodes.DataSource = ebo.GetBillDiagnosisCode(p_szBillID).Tables[0];
            this.lstDiagnosisCodes.DataTextField = "DESCRIPTION";
            this.lstDiagnosisCodes.DataValueField = "CODE";
            this.lstDiagnosisCodes.DataBind();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.grdTransactionDetails.Visible = true;
            this.BindTransactionDetailsGrid(p_szBillID);
            ArrayList billInfo = new ArrayList();
            billInfo = this._bill_Sys_BillTransaction.GetBillInfo(p_szBillID);
            if (billInfo != null)
            {
                this.extddlDoctor.SelectedValue = billInfo[0].ToString();
                this.GetProcedureCode(this.hndDoctorID.Value.ToString());
            }
        }
        catch (Exception ex)
        {
            log.Debug("BillTransaction. Method - setDefaultValues : " + ex.Message.ToString());
            log.Debug("BillTransaction. Method - setDefaultValues : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("BillTransaction. Method - setDefaultValues  : " + ex.InnerException.Message.ToString());
                log.Debug("BillTransaction. Method - setDefaultValues  : " + ex.InnerException.StackTrace.ToString());
            }
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

    protected void showModalPopup()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string doctorId = "";
            new ArrayList();
            new DataSet();
            new Bill_Sys_Visit_BO().GetVisitTypeList(this.txtCompanyID.Text, "GetVisitType");
            foreach (DataGridItem item in this.grdCompleteVisit.Items)
            {
                CheckBox box = (CheckBox)item.Cells[0].FindControl("chkSelectItem");
                if (box.Checked)
                {
                    doctorId = item.Cells[8].Text;
                    string text = item.Cells[6].Text;
                    break;
                }
            }
            if (doctorId != "")
            {
                this.GetProcedureCode(doctorId);
                this.hndDoctorID.Value = doctorId;
            }
            else
            {
                this.GetProcedureCode(this.hndDoctorID.Value.ToString());
            }
            Bill_Sys_BillTransaction_BO n_bo = new Bill_Sys_BillTransaction_BO();
            if (n_bo.checkUnit(this.hndDoctorID.Value.ToString(), this.txtCompanyID.Text))
            {
                this.grdTransactionDetails.Columns[7].Visible = true;
                for (int j = 0; j < this.grdTransactionDetails.Items.Count; j++)
                {
                    TextBox box2 = (TextBox)this.grdTransactionDetails.Items[j].FindControl("txtUnit");
                    box2.Text = this.grdTransactionDetails.Items[j].Cells[8].Text;
                }
            }
            else
            {
                this.grdTransactionDetails.Columns[7].Visible = false;
            }
            new Bill_Sys_AssociateDiagnosisCodeBO();
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
            if (this.hndPopUpvalue.Value.ToString() == "PopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupAddservice.Show();
            }
            else if (this.hndPopUpvalue.Value.ToString() == "GroupPopUpValue")
            {
                this.hndPopUpvalue.Value = "";
                this.modalpopupaddgroup.Show();
            }
            for (int i = 0; i < this.grdProcedure.Items.Count; i++)
            {
                CheckBox box3 = (CheckBox)this.grdProcedure.Items[i].FindControl("chkselect");
                box3.Checked = false;
            }
            foreach (DataGridItem item2 in this.grdCompleteVisit.Items)
            {
                CheckBox box4 = (CheckBox)item2.Cells[0].FindControl("chkSelectItem");
                if (box4.Checked)
                {
                    string str2 = item2.Cells[7].Text;
                    string text2 = item2.Cells[9].Text;
                    Bill_Sys_BillTransaction_BO n_bo2 = new Bill_Sys_BillTransaction_BO();
                    DataSet set = new DataSet();
                    set = n_bo2.GET_PROC_CODE_USING_EVENT_ID(str2);
                    if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
                    {
                        for (int k = 0; k < set.Tables[0].Rows.Count; k++)
                        {
                            for (int m = 0; m < this.grdProcedure.Items.Count; m++)
                            {
                                CheckBox box5 = (CheckBox)this.grdProcedure.Items[m].FindControl("chkselect");
                                if (set.Tables[0].Rows[k]["SZ_PROC_CODE"].ToString() == this.grdProcedure.Items[m].Cells[2].Text)
                                {
                                    box5.Checked = true;
                                }
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void txtUpdatepopup_Click(object sender, EventArgs e)
    {
    }

    private void UpdateTransactionDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
        try
        {
            new ArrayList();
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
    protected void btnRemoveDGCodes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new Bill_Sys_AssociateDiagnosisCodeBO();
        try
        {
            for (int i = 0; i < this.lstDiagnosisCodes.Items.Count; i++)
            {
                if (this.lstDiagnosisCodes.Items[i].Selected)
                {
                    this.lstDiagnosisCodes.Items.Remove(this.lstDiagnosisCodes.Items[i]);
                    i--;
                }
            }
            this.lblDiagnosisCodeCount.Text = this.lstDiagnosisCodes.Items.Count.ToString();
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
    public CyclicCode GetCyclicCode(string szCaseID, string szCmpD, string szInsuranceID, string szSpecilatyID, DataTable tblProcCode)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        CyclicCode objCyclicCode = new CyclicCode();
        DataSet dsValues = new DataSet();

        try
        {
            Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction_bo = new Bill_Sys_BillTransaction_BO();
            dsValues = _bill_Sys_BillTransaction_bo.GetCodeAmount(szCaseID, szCmpD, szInsuranceID, szSpecilatyID, tblProcCode);
            if (dsValues.Tables.Count > 0)
            {
                if (dsValues.Tables[0].Rows.Count > 0)
                {
                    objCyclicCode.sz_configuraton = dsValues.Tables[0].Rows[0]["configuration"].ToString();
                    if (dsValues.Tables[1].Rows.Count > 0)
                    {
                        objCyclicCode.i_Flag = 1;
                        objCyclicCode.ProcValue = dsValues.Tables[1];

                        if (dsValues.Tables[2].Rows.Count > 0)
                        {
                            try
                            {

                                objCyclicCode.i_Count = Convert.ToInt32(dsValues.Tables[2].Rows[0]["Count"]);

                            }
                            catch (Exception)
                            {
                                objCyclicCode.i_Count = 0;

                            }

                        }
                        else
                        {
                            objCyclicCode.i_Count = 0;
                        }
                    }
                    else
                    {
                        objCyclicCode.i_Flag = 0;
                    }

                }
                else
                {
                    objCyclicCode.i_Flag = 0;
                }
            }
            else
            {
                objCyclicCode.i_Flag = 0;
            }

            if (dsValues.Tables.Count == 4)
            {
                objCyclicCode.AllProc = dsValues.Tables[3];
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



        return objCyclicCode;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

