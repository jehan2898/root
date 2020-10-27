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
using mbs.lawfirm;
using XMLData;
using System.Data.SqlClient;
using System.IO;

public partial class LF_Bill_Sys_Transfer_Cases : PageBase
{
    public int i = 0;
    public bool j = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grdTranferCompanyWise.Page = this.Page;
        this.grdLitigationCompanyWise.Page = this.Page;
        this.grdCaseCount.Page = this.Page;
        
        this.con.SourceGrid = grdBillSearch;
        this.txtSearchBox.SourceGrid = grdBillSearch;
        this.grdBillSearch.Page = this.Page;

        this.grdBillSearch.PageNumberList = this.con;
       
        btnRejecte.Attributes.Add("onclick", "return checkSelected();");
        if (!IsPostBack)
        {
            btnRejecte.Visible = false;
            Session["Subflag"] = null;
            txtCompanyId.Text=((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            grdTranferCompanyWise.XGridBind();
            Session["OFF_ID"] = null;
            Session["ALL_OFF_ID"] = "YES";
        }
        if (Session["Subflag"] != null)
        {
            txtSubFlag.Text = Session["Subflag"].ToString();
        }
    }

    protected void grdLitigationCompanyWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBills")
        {
            btnRejecte.Visible = true;
            grdCaseCount.Visible = true;
            grdBillSearch.Visible = true;
            int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string szOffId = "'"+grdLitigationCompanyWise.DataKeys[iIndex][0].ToString()+"'";
            string szOffName = grdLitigationCompanyWise.DataKeys[iIndex][1].ToString();

            txtOfficeID.Text = szOffId;

            Session["ALL_OFF_ID"] = null;
            Session["OFF_ID"] = szOffId;
            txtOfficeID.Text =  szOffId;

            Session["Subflag"] = null;
            txtSubFlag.Text = "";
            

            //BinGrid();
            grdBillSearch.XGridBindSearch();
           
            grdCaseCount.XGridBind();
            LinkButton lnkCnt;
            for (int cnt = 0; cnt < grdCaseCount.Rows.Count; cnt++)
            {

                lnkCnt = (LinkButton)grdCaseCount.Rows[cnt].FindControl("lnkcount");
                if (lnkCnt != null)
                {
                    lnkCnt.Enabled = true;
                }


            }
            Session["OFF_ID"] = szOffId;

            if (szOffName != "&nbsp;" && szOffName != "&nbsp;")
            {
                lblOffName.Text = szOffName;
                UpdatePanel33.DataBind();
            }

            string str = txtCompany.Text;
            string str1 = txtCompanyId.Text;
            txtOfficeID.Text = txtOfficeID.Text.Replace("'", "");
            txtOfficeID.Text.Trim();
            
            grdLitigationCompanyWise.XGridBind();
            //txtOfficeID.Text = "";
            txtOfficeID.Text = "'" + txtOfficeID.Text + "'";
            Label lblAmount;
            LinkButton lnkAmount;
            for (int j = 0; j < grdLitigationCompanyWise.Rows.Count; j++)
            {
                lblAmount = (Label)grdLitigationCompanyWise.Rows[j].FindControl("lblAmount");
                lnkAmount = (LinkButton)grdLitigationCompanyWise.Rows[j].FindControl("lnkAmt");
                if (j == 0 || j == 1 || j == 2)
                {

                    lblAmount.Visible = true;
                    lnkAmount.Visible = false;
                }
                else
                {
                    lblAmount.Visible = false;
                    lnkAmount.Visible = true;
                }
            }
        }
    }

    protected void grdTranferCompanyWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sum")
        {
            btnRejecte.Visible = true;
            Session["OFF_ID"] = null;
            Session["ALL_OFF_ID"] = "YES";
            string sz_Off_id = "";
            txtOfficeID.Text = "";
            int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string szCompany = grdTranferCompanyWise.DataKeys[iIndex]["SZ_COMPANY_ID"].ToString();
            txtCompany.Text = szCompany;
            Session["Company"] = szCompany;
            txtLoginCompanyId.Text = szCompany;
            txtFlag.Text = "0";
            grdLitigationCompanyWise.XGridBind();
            lblOffName.Text = "";
            txtOfficeID.Text = "NULL";
            
            //grdCaseCount.Visible=false;
            //grdBillSearch.Visible = false;
            //grdBillSearch.XGridBind();

            
            if (Session["ALL_OFF_ID"] != null && Session["ALL_OFF_ID"].ToString() == "YES")
            {
                for (int grdCount = 0; grdCount < grdLitigationCompanyWise.Rows.Count; grdCount++)
                {
                    if (grdCount != 0 && grdCount != 1 && grdCount != 2 && grdCount != 3)
                    {
                        if (sz_Off_id == "")
                        {
                            sz_Off_id = "'" + grdLitigationCompanyWise.DataKeys[grdCount][0].ToString() + "'";

                        }
                        else
                        {
                            sz_Off_id = sz_Off_id + "," + "'" + grdLitigationCompanyWise.DataKeys[grdCount][0].ToString() + "'";

                        }
                    }

                }


                txtOfficeID.Text = sz_Off_id;


            }
            else
            {
                txtOfficeID.Text = "'" + Session["OFF_ID"].ToString() + "'";
            }
            grdCaseCount.XGridBind();
           
                 grdBillSearch.XGridBindSearch();
               
          


           // grdBillSearch.RecordCount = 0;
           // grdBillSearch.PageCount = 0;
            Label lblAmount;
            LinkButton lnkAmount;
            for (int j = 0; j < grdLitigationCompanyWise.Rows.Count; j++)
            {
                lblAmount = (Label)grdLitigationCompanyWise.Rows[j].FindControl("lblAmount");
                lnkAmount = (LinkButton)grdLitigationCompanyWise.Rows[j].FindControl("lnkAmt");
                if (j == 0 || j == 1 || j == 2)
                {

                    lblAmount.Visible = true;
                    lnkAmount.Visible = false;
                }
                else
                {
                    lblAmount.Visible = false;
                    lnkAmount.Visible = true;
                }

            }

            

        }
    }

    protected void grdCaseCount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string szSubFlag = grdCaseCount.DataKeys[iIndex][0].ToString();
        int iRowCount = Convert.ToInt32(szSubFlag);
        
        if(iRowCount==7)
        {
            ScriptManager.RegisterClientScriptBlock(this,GetType(),"ShowReport","window.open('"+grdCaseCount.DataKeys[iRowCount]["link"].ToString()+txtLoginCompanyId.Text+"')",true);
        }
        else
        {
            if (iRowCount == 5 || iRowCount == 6)
            {
                btnRejecte.Visible = false;
            }
            else
            {
                btnRejecte.Visible = true;
            }
            txtSubFlag.Text = szSubFlag;
            grdBillSearch.XGridBindSearch();
           
            txtSubFlag.Text = "";
            Session["Subflag"] = szSubFlag;
        }

    }

    protected void grdLitigationCompanyWise_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (i != 0 && !j)
        {
            DataRowView ds = (DataRowView)e.Row.DataItem;
            grdLitigationCompanyWise.HeaderRow.Cells[0].Text = "Title" + "  "+ "(" + ds.Row[4].ToString() + ")";
            j = true;
        }
        else
        {
            i = 1;
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        if (Session["Subflag"] != null)
        {
            txtSubFlag.Text = Session["Subflag"].ToString();
        }
        else
        {
            txtSubFlag.Text = "";
        }

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

    }

    protected void grdBillSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      //string szBillStatusName=  e.Row.Cells[8].Text.ToString();
      //double szBlance = 0;
        
      //  string szBal=e.Row.Cells[7].Text.ToString().Replace('$',' ');
      //  if (szBal.Trim().ToString() != "&nbsp;" && szBal.Trim().ToString() != "" )
      //{
      //    szBlance = Convert.ToDouble(szBal);
      //}

      //if (e.Row.Cells[8].Text.Trim().ToLower() != "sold" || (e.Row.Cells[8].Text.Trim().ToLower() == "sold" && szBlance > 100))
      //{
      //    CheckBox chk = (CheckBox)e.Row.Cells[14].FindControl("ChkDelete");
      //    if (chk != null)
      //    {
      //        chk.Enabled = false;
      //    }
      //}

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdBillSearch.Rows.Count; i++)
        {
            string szBillStatusName = grdBillSearch.Rows[i].Cells[8].Text;  
             double szBlance = 0;
             string szBal = grdBillSearch.DataKeys[i]["FLT_BALANCE"].ToString();
             if (szBal.Trim().ToString() != "&nbsp;" && szBal.Trim().ToString() != "")
             {
                 szBlance = Convert.ToDouble(szBal);
             }
             if (grdBillSearch.Rows[i].Cells[8].Text.Trim().ToLower() == "sold" || grdBillSearch.Rows[i].Cells[8].Text.Trim().ToLower() == "bill rejected by lawfirm" )
             {
                 CheckBox chk = (CheckBox)grdBillSearch.Rows[i].Cells[17].FindControl("ChkDelete");
                 if (chk != null)
                 {
                     chk.Enabled = false;
                 }
             }
             else if (szBlance > 15)
             {
                 CheckBox chk = (CheckBox)grdBillSearch.Rows[i].Cells[17].FindControl("ChkDelete");
                 if (chk != null)
                 {
                     chk.Enabled = false;
                 }
             }

        }
    }
    protected void btnRejecte_Click(object sender, EventArgs e)
    {
        ArrayList arrBillNo = new ArrayList();
           for (int i = 0; i < grdBillSearch.Rows.Count; i++)
                {   //check checkbox value
                    CheckBox chkDelete1 = (CheckBox)grdBillSearch.Rows[i].FindControl("ChkDelete");
                    if (chkDelete1.Checked)
                    {   
                      
                            arrBillNo.Add(grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString()); 
                            

                       
                    }
                }
                string szMessage = "";
                string szMessage1 = "";
                if (arrBillNo.Count > 0)
                {       for(int j=0; j<arrBillNo.Count;j++)
                        {
                            Bill_Sys_CollectDocAndZip _objBillStatus = new Bill_Sys_CollectDocAndZip();
                            int cnt =  _objBillStatus.UpdateBillStatusToRejecte(arrBillNo[j].ToString(),"BRL");
                          if (cnt == 0)
                          {
                              if (szMessage == "")
                              {
                                  szMessage = szMessage + " " + arrBillNo[j].ToString();
                              }
                              else
                              {
                                  szMessage = szMessage + "," + arrBillNo[j].ToString();
                              }
                          }
                          else
                          {
                              if (szMessage1 == "")
                              {
                                  szMessage1 = szMessage1 + arrBillNo[j].ToString();
                              }
                              else
                              {
                                  szMessage1 = szMessage1+"," + arrBillNo[j].ToString();
                              }

                          }
                        }
                    

                }
                if (szMessage1 != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('"+szMessage1+" -Bills Are Rejected Successfully .');", true);
                } if (szMessage != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert(' Rejection Failed for" + szMessage + " Bills.');", true);
                }
               
                this.grdCaseCount.Page = this.Page;
            
             grdCaseCount.XGridBind();
                grdBillSearch.XGridBindSearch();
                
    }
    protected void btnBatchXls_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsAll = new DataSet();
            XMLData.XMLData obj = new XMLData.XMLData();
            XMLData.XMLData xd = new XMLData.XMLData();
            string szxmlfilename = ConfigurationManager.AppSettings["LFDESKOFFICEINFO"].ToString();
            xd = obj.ReadXml(szxmlfilename);
            dsAll = obj.XGridBind(this.Page, xd, 1, grdBillSearch.RecordCount, "", txtSearchBox.Text);
             string sz_Bill_No="";
             for (int i = 0; i < dsAll.Tables[0].Rows.Count; i++)
             {
                 if (sz_Bill_No == "")
                 {
                     sz_Bill_No = "'" + dsAll.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString() + "'";
                 }
                 else
                 {
                     sz_Bill_No = sz_Bill_No = sz_Bill_No + "," + "'" + dsAll.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString() + "'";
                 }
             }
             Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
             DataSet dsInfo = new DataSet();

             dsInfo = _objBatch.GetPateintInfo(sz_Bill_No);
             if (dsInfo.Tables[0].Rows.Count > 0)
             {
                 string xmlName = _objBatch.getFileName();
                 string exlName = xmlName.Replace(".xml", ".xls");
                 string file_path = ConfigurationSettings.AppSettings["XLPATH"].ToString();
                 string Folder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
               
                 File.Copy(file_path, getPhysicalPath() + "Reports/" + exlName);
                 _objBatch.GenerateXL(dsInfo.Tables[0], getPhysicalPath() + "Reports/" + exlName);
                 if (File.Exists(getPhysicalPath() +Folder+ exlName))
                 {
                     ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + exlName + "'); ", true);
                 }
                 else
                 {
                     ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Batch is Empty');", true);
                 }
             }
        }
        catch (Exception ex)
        {
            
            
        }
        
     }
    public string getPhysicalPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlCon);
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();

            }
        }
        return szParamValue;
    }
}
