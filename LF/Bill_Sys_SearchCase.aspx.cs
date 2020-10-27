using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using CustomControls.ContextMenuScope;
using System.Collections;
using mbs.lawfirm;
using mbs.ApplicationSettings;
using DevExpress.Web;
using System.Data.OleDb;
using System.Text;
using ExtendedDropDownList;

public partial class PatientList : PageBase
{
    private Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    static  int iDownloaCount = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //drdBatchStatus.SelectedIndex = 1;
        if (drdBatchStatus.SelectedValue.ToString() == "0")
        {
            txtBatchStatus.Text = "2";
        }
        else if (drdBatchStatus.SelectedValue.ToString() == "1")
        {
            txtBatchStatus.Text = "0";
        }

        else if (drdBatchStatus.SelectedValue.ToString() == "3")
        {

            txtBatchStatus.Text = "1";
        }
        
        txtCompanyId.Visible = false;
        txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlCaseType.Flag_ID = txtCompanyId.Text.ToString();
        extCompanyName.Flag_ID = txtCompanyId.Text.ToString();
       
        PopulateContextMenu();
        //ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        ajAutoClaim.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        ajPatientName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        
       
        
        btnDownload.Attributes.Add("onclick", "return Validate();");
        btnDownloadAll.Attributes.Add("onclick", "return ValidateALL();");

        this.con.SourceGrid = grid;
        this.txtSearchBox.SourceGrid = grid;
        this.grid.Page = this.Page;
        this.grid.PageNumberList = this.con;

        this.con1.SourceGrid = grdLitigationCompanyWise;
        //this.XGridSearchTextBox1.SourceGrid = grdLitigationCompanyWise;
        this.grdLitigationCompanyWise.Page = this.Page;
        grdLitigationCompanyWise.XGridBind();
        for (int j = 0; j < grdLitigationCompanyWise.Rows.Count; j++)
        {
          
            LinkButton lPlus = (LinkButton)grdLitigationCompanyWise.Rows[j].FindControl("lnkPlus");
            if (j == 1 )
            {

                
                lPlus.Visible = false;
            }
            else
            {
               
                lPlus.Visible = true;
            }

        }

        if (!IsPostBack)
        {
            hDnl.Value = ""; 
            Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
            DataSet Batchds = new DataSet();
            Batchds = _objBatch.ShowBatches(txtCompanyId.Text);
            iDownloaCount = Convert.ToInt16(ConfigurationManager.AppSettings["DownLoadPatient"].ToString());
            hDownlaodNumber.Value = iDownloaCount.ToString();
            DtlView.DataSource = Batchds;
            DtlView.DataBind();

        //    if(Session["Check"]!=null)
        //{
        //    if (Session["Check"].ToString() == "Not Correct")
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mm", "<script language='javascript'>alert('Record does not exist');</script>");
        //    }
        //}
        
            txtLawFirmID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
            txtCaseType.Text = extddlCaseType.Text.ToString();
            txtMedicalFacility.Text = extCompanyName.Text.ToString();
            grid.XGridBind();
        }
        if (Session["PatientName"] != null)
        {
            txtPatient.Text = Session["PatientName"].ToString();
            txtCaseType.Visible = true;
            txtLawFirmID.Visible = true;
            txtCaseType.Text = extddlCaseType.Text.ToString();
            txtMedicalFacility.Text = extCompanyName.Text.ToString();

            grid.XGridBind();
            txtPatient.Text = "";
            txtCaseType.Visible = false;
            txtLawFirmID.Visible = false;
            txtCompanyId.Visible = false;
            Session["PatientName"] = null;
        }
        txtLawFirmID.Visible = false;
        // grid.Visible = true;
        lbl_export.Text = "<br/><i>(Exports all bills in status 'Transferred')</i>";
        lbl_downloaded_status.Text = "<i><b>(Text file with 1 bill number per line)</b></i>";
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + grid.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void ContextMenu1_ItemCommand(object sender, CommandEventArgs e)
    {
        int rowIndex = this.grid.RightClickedRow.RowIndex;
    }

    private void PopulateContextMenu()
    {
        string PhysicalPath = ConfigurationManager.AppSettings["CONTEXTMENU"].ToString();
        XmlReaderSettings settings = new XmlReaderSettings();
        string contextmenuxml = Path.Combine(PhysicalPath, "contextmenu.xml");

        NameTable nameTable = new NameTable();
        object contextMenuItem = nameTable.Add("contextmenuitem");

        settings.NameTable = nameTable;

        using (XmlReader reader = XmlReader.Create(contextmenuxml, settings))
        {
            while (reader.Read())
            {
                // Read a single ContextMenuItem
                if ((reader.NodeType == XmlNodeType.Element) &&
                    (contextMenuItem.Equals(reader.LocalName)))
                {
                    XmlReader subTree = reader.ReadSubtree();
                    ContextMenuItem menuItem = new ContextMenuItem();

                    // Get contents of a single ContextMenuItem
                    while (subTree.Read())
                    {
                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("text")))
                            menuItem.Text = subTree.ReadString();

                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("commandname")))
                            menuItem.CommandName = subTree.ReadString();

                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("tooltip")))
                            menuItem.Tooltip = subTree.ReadString();

                        if ((subTree.NodeType == XmlNodeType.Element) &&
                            (subTree.LocalName.Equals("onclientclick")))
                            menuItem.OnClientClick = subTree.ReadString();
                    }

                    // Add item to ContextMenu
                    this.ContextMenu1.ContextMenuItems.Add(menuItem);
                }
            }
        }
    }

    public void BindGrid()
    {

        string connStr = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection mySQLconnection = new SqlConnection(connStr);
        _bill_Sys_BillingCompanyDetails_BO = new Bill_Sys_BillingCompanyDetails_BO();

        if (mySQLconnection.State == ConnectionState.Closed)
        {
            mySQLconnection.Open();
        }

        // SqlCommand mySqlCommand = new SqlCommand(GetQuery(), mySQLconnection);
        SqlCommand mySqlCommand = new SqlCommand("SP_GET_PATIENT_SNAPSHOT", mySQLconnection);
        mySqlCommand.CommandType = CommandType.StoredProcedure;

        mySqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        mySqlCommand.Parameters.AddWithValue("@I_START_INDEX", "0");
        mySqlCommand.Parameters.AddWithValue("@I_END_INDEX", "10");


        SqlDataAdapter mySqlAdapter = new SqlDataAdapter(mySqlCommand);
        DataSet myDataSet = new DataSet();
        mySqlAdapter.Fill(myDataSet);
        grid.Visible = true;
        grid.DataSource = myDataSet;
        grid.DataBind();

        if (mySQLconnection.State == ConnectionState.Open)
        {
            mySQLconnection.Close();
        }
    }

    public string GetQuery()
    {
        string szQuery = " SELECT  cast(MST_CASE_MASTER.SZ_CASE_ID as int) [SZ_CASE_ID],	MST_CASE_MASTER.SZ_CASE_NO, isnull(MST_PATIENT.SZ_CHART_NO,'') [SZ_CHART_NO], MST_PATIENT.SZ_PATIENT_FIRST_NAME+' ' + MST_PATIENT.SZ_PATIENT_LAST_NAME [SZ_PATIENT_NAME], 	MST_CASE_TYPE.SZ_CASE_TYPE_NAME, isnull(MST_INSURANCE_COMPANY.SZ_INSURANCE_NAME,'') [SZ_INSURANCE_NAME], CONVERT(VARCHAR(20), DT_DATE_OF_ACCIDENT, 106) [DT_DATE_OF_ACCIDENT],";
        szQuery = szQuery + " DT_CREATED_DATE, MST_CASE_STATUS.SZ_STATUS_NAME, (isnull(MST_ATTORNEY.SZ_ATTORNEY_FIRST_NAME,'') +' ' + isnull(MST_ATTORNEY.SZ_ATTORNEY_LAST_NAME,'') ) [SZ_ATTORNEY_FIRST_NAME], MST_BILLING_COMPANY.SZ_COMPANY_NAME FROM MST_CASE_MASTER join MST_PATIENT on (MST_PATIENT.SZ_PATIENT_ID=MST_CASE_MASTER.SZ_PATIENT_ID) join MST_BILLING_COMPANY on (MST_BILLING_COMPANY.SZ_COMPANY_ID=MST_CASE_MASTER.SZ_COMPANY_ID)";
        szQuery = szQuery + " left join MST_ATTORNEY on (MST_ATTORNEY.SZ_ATTORNEY_ID=MST_CASE_MASTER.SZ_ATTORNEY_ID) left join MST_CASE_STATUS on (MST_CASE_STATUS.SZ_CASE_STATUS_ID=MST_CASE_MASTER.SZ_CASE_STATUS_ID) left join MST_CASE_TYPE on (MST_CASE_TYPE.SZ_CASE_TYPE_ID=MST_CASE_MASTER.SZ_CASE_TYPE_ID) left join MST_INSURANCE_COMPANY	 on (MST_INSURANCE_COMPANY.SZ_INSURANCE_ID=MST_CASE_MASTER.SZ_INSURANCE_ID) WHERE MST_CASE_MASTER.SZ_COMPANY_ID='CO000000000000000059' ";
        return szQuery;
    }

    protected void extddlCaseType_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCaseType.Text = extddlCaseType.Text;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       
        
        txtCaseType.Visible = true;
        txtLawFirmID.Visible = true;
        txtCaseType.Text = extddlCaseType.Text.ToString();
        txtMedicalFacility.Text = extCompanyName.Text.ToString();
        if (drdBatchStatus.SelectedValue.ToString() == "0")
        {
            txtBatchStatus.Text = "2";
            //grid.Columns["Status"].SortExpression = "SZ_STATUS";
        }
        else if (drdBatchStatus.SelectedValue.ToString() == "1")
        {
            txtBatchStatus.Text = "0";
        }

        else if (drdBatchStatus.SelectedValue.ToString() == "3")
        {

            txtBatchStatus.Text = "1";
        }

        grid.XGridBindSearch();

        txtCaseType.Visible = false;
        txtLawFirmID.Visible = false;
    }

    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int num = 0;
        if (e.CommandName.ToString() == "CaseNO")
        {
            //  string szCaseID =  + grdPacketing.DataKeys[index][0].ToString();
            Response.Redirect("WorkAreaWidget.aspx", true);
        }


        if (e.CommandName.ToString() == "VerifPLS")
        {
            for (int i = 0; i < this.grid.Rows.Count; i++)
            {
                LinkButton button = (LinkButton)this.grid.Rows[i].FindControl("lnkVM");
                LinkButton button2 = (LinkButton)this.grid.Rows[i].FindControl("lnkVP");
                if (button.Visible)
                {
                    button.Visible = false;
                    button2.Visible = true;
                }
            }
            this.grid.Columns[0x11].Visible = true;
            this.grid.Columns[0x12].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str = "div2";
            str = str + this.grid.DataKeys[num][5].ToString();
            GridView view = (GridView)this.grid.Rows[num].FindControl("grdVerification_Gird");
            LinkButton button3 = (LinkButton)this.grid.Rows[num].FindControl("lnkVP");
            LinkButton button4 = (LinkButton)this.grid.Rows[num].FindControl("lnkVM");
            string str2 = "'" + this.grid.DataKeys[num][5].ToString() + "'";
            string str3 = this.grid.DataKeys[num][1].ToString();
            DataSet verification = this.GetVerification(str3, str2);
            view.DataSource = verification;
            view.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
            button3.Visible = false;
            button4.Visible = true;
            LinkButton button5 = (LinkButton)this.grid.Rows[num].FindControl("lnkDM");
            LinkButton button6 = (LinkButton)this.grid.Rows[num].FindControl("lnkDP");
            button6.Visible = true;
            if (button5.Visible)
            {
                button5.Visible = false;
                button6.Visible = true;
            }
        }
        if (e.CommandName.ToString() == "VerifMNS")
        {
            this.grid.Columns[0x11].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str4 = "div2";
            str4 = str4 + this.grid.DataKeys[num][5].ToString();
            LinkButton button7 = (LinkButton)this.grid.Rows[num].FindControl("lnkVP");
            LinkButton button8 = (LinkButton)this.grid.Rows[num].FindControl("lnkVM");
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", "HideChildGrid('" + str4 + "') ;", true);
            button7.Visible = true;
            button8.Visible = false;
        }
        if (e.CommandName.ToString() == "DenialPLS")
        {
            for (int j = 0; j < this.grid.Rows.Count; j++)
            {
                LinkButton button9 = (LinkButton)this.grid.Rows[j].FindControl("lnkDM");
                LinkButton button10 = (LinkButton)this.grid.Rows[j].FindControl("lnkDP");
                if (button9.Visible)
                {
                    button9.Visible = false;
                    button10.Visible = true;
                }
            }
            this.grid.Columns[0x11].Visible = false;
            this.grid.Columns[0x12].Visible = true;
            num = Convert.ToInt32(e.CommandArgument);
            string str5 = "div1";
            str5 = str5 + this.grid.DataKeys[num][5].ToString();
            GridView view2 = (GridView)this.grid.Rows[num].FindControl("grdDenial");
            LinkButton button11 = (LinkButton)this.grid.Rows[num].FindControl("lnkDP");
            LinkButton button12 = (LinkButton)this.grid.Rows[num].FindControl("lnkDM");
            string str6 = this.grid.DataKeys[num][5].ToString();
            string str7 = this.grid.DataKeys[num][1].ToString();
            DataSet denialInfo = this.GetDenialInfo(str7, str6);
            view2.DataSource = denialInfo;
            view2.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", "ShowDenialChildGrid('" + str5 + "') ;", true);
            button11.Visible = false;
            button12.Visible = true;
            LinkButton button13 = (LinkButton)this.grid.Rows[num].FindControl("lnkVP");
            LinkButton button14 = (LinkButton)this.grid.Rows[num].FindControl("lnkVM");
            button13.Visible = true;
            if (button14.Visible)
            {
                button14.Visible = false;
                button13.Visible = true;
            }
        }
        if (e.CommandName.ToString() == "DenialMNS")
        {
            this.grid.Columns[0x12].Visible = false;
            num = Convert.ToInt32(e.CommandArgument);
            string str8 = "div";
            str8 = str8 + this.grid.DataKeys[num][5].ToString();
            LinkButton button15 = (LinkButton)this.grid.Rows[num].FindControl("lnkDP");
            LinkButton button16 = (LinkButton)this.grid.Rows[num].FindControl("lnkDM");
            button15.Visible = true;
            button16.Visible = false;
        }
    }

    //protected void btnDownload_Click(object sender, EventArgs e)
    //{
         
    //    string B= hBatch.Value;
    //    string S = hStatus.Value;
           
       
    //    DAO_PatientList _objDesc;
    //    ArrayList objAL = new ArrayList();


                               
    //    string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    string useId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //    string username = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;

    //    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert(" + szRemoteAddr + ");", true);
    //    //string szXForwardedFor = Page.Request.ServerVariables["X_FORWARDED_FOR"].ToString();
    //    if(hIsCount.Value=="N")
    //    {
    //    for (int i = 0; i < grid.Rows.Count; i++)
    //    {

    //        CheckBox chkFind = (CheckBox)grid.Rows[i].FindControl("ChkPatientList");
    //        if (chkFind.Checked)
    //        {
    //            string SZ_CASE_ID = grid.DataKeys[i]["SZ_CASE_ID"].ToString();
    //            string SZ_CASE_NO = grid.DataKeys[i]["SZ_CASE_NO"].ToString();
                
    //            string SZ_PATIENT_NAME = grid.DataKeys[i]["Patient Name"].ToString();
    //            _objDesc = new DAO_PatientList();
    //            _objDesc.CaseID = SZ_CASE_ID;
    //            _objDesc.CompanyId = grid.DataKeys[i]["SZ_COMPANY_ID"].ToString();
    //            _objDesc.LAWFIRM_COMPANY_ID = txtLawFirmID.Text.ToString();
    //            _objDesc.USER_ID = useId;
    //            _objDesc.IP_ADDRESS = szRemoteAddr;
    //            _objDesc.CASE_NO = SZ_CASE_NO;
    //            _objDesc.PATIENT_NAME = SZ_PATIENT_NAME;
    //            _objDesc.USER_NAME = username;
    //               Bill_Sys_CollectDocAndZip cNzs = new Bill_Sys_CollectDocAndZip();  
    //            objAL.Add(_objDesc);
    //          //string statustype=  cNzs.GetBatchstaus(SZ_CASE_ID);
            

    //        }
    //    }
    //}
    //else if (hIsCount.Value == "Y")
    //    {
    //        if (hCount.Value == "YES")
    //        {
    //            for (int i = 0; i < iDownloaCount; i++)
    //            {

    //                CheckBox chkFind = (CheckBox)grid.Rows[i].FindControl("ChkPatientList");
    //                if (chkFind.Checked)
    //                {
    //                    String SZ_CASE_ID = grid.DataKeys[i]["SZ_CASE_ID"].ToString();
    //                    string SZ_CASE_NO = grid.DataKeys[i]["SZ_CASE_NO"].ToString();
    //                    string SZ_PATIENT_NAME = grid.DataKeys[i]["Patient Name"].ToString();
    //                    _objDesc = new DAO_PatientList();
    //                    _objDesc.CaseID = SZ_CASE_ID;
    //                    _objDesc.CompanyId = grid.DataKeys[i]["SZ_COMPANY_ID"].ToString();
    //                    _objDesc.LAWFIRM_COMPANY_ID = txtLawFirmID.Text.ToString();
    //                    _objDesc.USER_ID = useId;
    //                    _objDesc.IP_ADDRESS = szRemoteAddr;
    //                    _objDesc.CASE_NO = SZ_CASE_NO;
    //                    _objDesc.PATIENT_NAME = SZ_PATIENT_NAME;
    //                    _objDesc.USER_NAME = username;
    //                    Bill_Sys_CollectDocAndZip cNzs = new Bill_Sys_CollectDocAndZip();
    //                    objAL.Add(_objDesc);
    //                   // string statustype = cNzs.GetBatchstaus(SZ_CASE_ID);


    //                }
    //            }
    //        }
    //    }
    //    string szUserCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //    Bill_Sys_CollectDocAndZip cNz = new Bill_Sys_CollectDocAndZip();
    //  //  ModalPopupExtender.Show();
    //    string path = cNz.CollectAndZipMultipalFiles(objAL, szUserCmpId,S,B);
    //    hDnl.Value = ""; 
       
    //    if (path == ""  )
    //    {
          
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
    //    } 
    //    else
    //    {
           
      
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + path + "'); ", true);
    //    }
    
    //    Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
    //    DataSet Batchds = new DataSet();
    //    Batchds = _objBatch.ShowBatches(txtCompanyId.Text);

    //    txtCaseType.Text = extddlCaseType.Text.ToString();
    //    txtMedicalFacility.Text = extCompanyName.Text.ToString();
    //    grid.XGridBind();
    //    DtlView.DataSource = Batchds;
    //    DtlView.DataBind();
        
    //    }
    //protected void btnDownloadALL_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        string B = hBatch.Value;
    //        string S = hStatus.Value;

    //        Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
    //         txtCaseType.Text = extddlCaseType.Text.ToString();
    //        txtMedicalFacility.Text = extCompanyName.Text.ToString();
    //        DataSet ds= new DataSet();
    //        if (drdBatchStatus.SelectedValue.ToString() == "0")
    //        {
    //            txtBatchStatus.Text = "2";
    //        }
    //        else if (drdBatchStatus.SelectedValue.ToString() == "1")
    //        {
    //            txtBatchStatus.Text = "0";
    //        }

    //        else if (drdBatchStatus.SelectedValue.ToString() == "3")
    //        {

    //            txtBatchStatus.Text = "1";
    //        }

    //        ds = _objBatch.DownloadAll(txtPatient.Text, txtCaseType.Text, txtInsurance.Text, txtAccidentDate.Text, txtBirth.Text, txtMedicalFacility.Text, txtClaimNo.Text, txtSSNNo.Text, txtBatchStatus.Text, txtLawFirmID.Text,txtSearchBox.Text);

    //             DAO_PatientList _objDesc;
    //             ArrayList objAL = new ArrayList();


                               
    //    string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    string useId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
    //    string username = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
    //    if (ds.Tables[0].Rows.Count > iDownloaCount)
    //    {
    //        for (int i = 0; i < iDownloaCount; i++)
    //        {
    //            _objDesc = new DAO_PatientList();
    //            _objDesc.CaseID = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
    //            _objDesc.CompanyId = ds.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
    //            _objDesc.LAWFIRM_COMPANY_ID = txtLawFirmID.Text.ToString();
    //            _objDesc.USER_ID = useId;
    //            _objDesc.IP_ADDRESS = szRemoteAddr;
    //            _objDesc.CASE_NO = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
    //            _objDesc.PATIENT_NAME = ds.Tables[0].Rows[i]["Patient Name"].ToString();
    //            _objDesc.USER_NAME = username;
    //            //Bill_Sys_CollectDocAndZip cNzs = new Bill_Sys_CollectDocAndZip();

    //            //string statustype = cNzs.GetBatchstaus(ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString());
    //            objAL.Add(_objDesc);
    //        }


    //    }
    //    else
    //    {
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            _objDesc = new DAO_PatientList();
    //            _objDesc.CaseID = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
    //            _objDesc.CompanyId = ds.Tables[0].Rows[i]["SZ_COMPANY_ID"].ToString();
    //            _objDesc.LAWFIRM_COMPANY_ID = txtLawFirmID.Text.ToString();
    //            _objDesc.USER_ID = useId;
    //            _objDesc.IP_ADDRESS = szRemoteAddr;
    //            _objDesc.CASE_NO = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
    //            _objDesc.PATIENT_NAME = ds.Tables[0].Rows[i]["Patient Name"].ToString();
    //            _objDesc.USER_NAME = username;
    //            //Bill_Sys_CollectDocAndZip cNzs = new Bill_Sys_CollectDocAndZip();

    //            //string statustype = cNzs.GetBatchstaus(ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString());
    //            objAL.Add(_objDesc);
    //        }

    //    }

    //    string szUserCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    //    Bill_Sys_CollectDocAndZip cNz = new Bill_Sys_CollectDocAndZip();
       
    //    string path = cNz.CollectAndZipMultipalFiles(objAL, szUserCmpId,S,B);
    //    hDnl.Value = ""; 
    //    if (path == "")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
    //    }
    //    else
    //    {


    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + path + "'); ", true);
    //    }
    //    //Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
    //    DataSet Batchds = new DataSet();
    //    Batchds = _objBatch.ShowBatches(txtCompanyId.Text);

    //    txtCaseType.Text = extddlCaseType.Text.ToString();
    //    txtMedicalFacility.Text = extCompanyName.Text.ToString();
    //    grid.XGridBind();
    //    DtlView.DataSource = Batchds;
    //    DtlView.DataBind();




    //    }
    //    catch (Exception ex)
    //    {
            
    //        throw;
    //    }
    //}

    protected void DtlView_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string sz_batch_name= e.CommandArgument.ToString();
        mbs.ApplicationSettings.ApplicationSettings_BO objApplicationName = null;

        objApplicationName = (mbs.ApplicationSettings.ApplicationSettings_BO)Application["OBJECT_APP_SETTINGS"];


        if (objApplicationName == null)
        {
            objApplicationName = new mbs.ApplicationSettings.ApplicationSettings_BO();
            Application["OBJECT_APP_SETTINGS"] = objApplicationName;
        }
           

        //Bill_Sys_CollectDocAndZip _obj = new Bill_Sys_CollectDocAndZip();
        Bill_Sys_CollectDocAndZip _obj = new Bill_Sys_CollectDocAndZip(objApplicationName);
          
        if (e.CommandName == "lnkDwn")
        {
            string path = _obj.CollectAndZipForBatch(sz_batch_name);
            if (path == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + _obj.getLawFirmLogicalPath() + path + "'); ", true);
                Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
                DataSet Batchds = new DataSet();
                Batchds = _objBatch.ShowBatches(txtCompanyId.Text);
                DtlView.DataSource = Batchds;
                DtlView.DataBind();
            }

        }
        if (e.CommandName == "lnkOpen")
        {
            string path = e.CommandArgument.ToString();
            if (path == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('"  + path + "'); ", true);
            }
        }
            if (e.CommandName == "dnlxls")
            {
                string xlspath = _obj.GetxlBills(sz_batch_name);
                  
                if (xlspath == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Batch is Empty');", true);
                }
                 else
                 {
                     if (File.Exists(_obj.getPhysicalPath() + "Reports/" + xlspath))
                     {
                         ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + xlspath + "'); ", true);
                     }
                     else
                     {
                         ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Batch is Empty');", true);
                     }
                 }
              }

          
    }

    protected void grdLitigationCompanyWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        try
        {
            //For Verification gridview
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdLitigationCompanyWise.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdLitigationCompanyWise.Rows[i].FindControl("lnkMinus");
                    LinkButton plus1 = (LinkButton)grdLitigationCompanyWise.Rows[i].FindControl("lnkPlus");
                    //LinkButton Dminus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDM");
                    //LinkButton DPlus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDP");
                    //Dminus1.Visible = false;
                    //DPlus1.Visible = true;
                    //if (Dminus1.Visible)
                    //{
                    //  Dminus1.Visible = false;
                    //}

                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                    }
                }
                grdLitigationCompanyWise.Columns[3].Visible = true;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                divname = divname + grdLitigationCompanyWise.DataKeys[index][0].ToString();
                GridView gv = (GridView)grdLitigationCompanyWise.Rows[index].FindControl("grdVerification");
                LinkButton plus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkPlus");
                LinkButton minus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkMinus");
                //string billno = "'" + grdLitigationCompanyWise.DataKeys[index][0].ToString() + "'";
                string companyid = txtCompanyId.Text;
                string lfId = grdLitigationCompanyWise.DataKeys[index][0].ToString();
                DataSet dsVeri = new DataSet ();
                if (lfId == "off")
                {
                    //dsVeri = GetTotalData(companyid);    
                    dsVeri = GetTotalDatabyBill(companyid);   
                }
                else
                {
                  //  dsVeri = GetData(companyid, lfId);
                   dsVeri = GetDataByBill(companyid, lfId); 
                   
                }

                
                gv.DataSource = dsVeri;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;

                //LinkButton Dminus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkDM");
                //LinkButton DPlus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkDP");
                //DPlus.Visible = true;
                //if (Dminus.Visible == true)
                //{
                //    Dminus.Visible = false;
                //    DPlus.Visible = true;
                //}

            }
            if (e.CommandName.ToString() == "MNS")
            {
                grdLitigationCompanyWise.Columns[3].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                divname = divname + grdLitigationCompanyWise.DataKeys[index][0].ToString();
                LinkButton plus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkPlus");
                LinkButton minus = (LinkButton)grdLitigationCompanyWise.Rows[index].FindControl("lnkMinus");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataSet GetData(string sz_CompanyID, string offID)
    {
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_get_lawfirm_subgrid_summury", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_office_id", offID);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

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
        return ds;

    }

    public DataSet GetTotalData(string sz_CompanyID)
    {
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_get_lawfirm_total_subgrid_summury", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
          
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

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
        return ds;

    }

    public DataSet GetTotalDatabyBill(string sz_CompanyID)
    {
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_get_lawfirm_total_subgrid_summury_new", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

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
        return ds;

    }

    public DataSet GetDataByBill(string sz_CompanyID, string offID)
    {
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_get_lawfirm_subgrid_summury_by_bill", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_office_id", offID);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

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
        return ds;

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {

        string B = hBatch.Value;
        string S = hStatus.Value;


        DAO_PatientList _objDesc;
        ArrayList objAL = new ArrayList();



        string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string useId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        string username = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;

        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert(" + szRemoteAddr + ");", true);
        //string szXForwardedFor = Page.Request.ServerVariables["X_FORWARDED_FOR"].ToString();
        if (hIsCount.Value == "N")
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {

                CheckBox chkFind = (CheckBox)grid.Rows[i].FindControl("ChkPatientList");
                if (chkFind.Checked)
                {
                    string SZ_CASE_ID = grid.DataKeys[i]["SZ_CASE_ID"].ToString();
                    string SZ_CASE_NO = grid.DataKeys[i]["SZ_CASE_NO"].ToString();

                    string SZ_PATIENT_NAME = grid.DataKeys[i]["Patient Name"].ToString();
                    _objDesc = new DAO_PatientList();
                    _objDesc.CaseID = SZ_CASE_ID;
                    _objDesc.CompanyId = grid.DataKeys[i]["SZ_COMPANY_ID"].ToString();
                    _objDesc.LAWFIRM_COMPANY_ID = txtLawFirmID.Text.ToString();
                    _objDesc.USER_ID = useId;
                    _objDesc.IP_ADDRESS = szRemoteAddr;
                    _objDesc.CASE_NO = SZ_CASE_NO;
                    _objDesc.PATIENT_NAME = SZ_PATIENT_NAME;
                    _objDesc.USER_NAME = username;
                    _objDesc.Bill_NO = grid.Rows[i].Cells[2].Text;
                    _objDesc.PROCEDURE_GROUP_ID = grid.DataKeys[i]["SZ_SPECIALITY_ID"].ToString();
                    
                    objAL.Add(_objDesc);
                    //string statustype=  cNzs.GetBatchstaus(SZ_CASE_ID);


                }
            }
        }
        //string sz_Company_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        //string sz_Company_ID=objAL[3].ToString();
        Bill_Sys_CollectDocAndZip cNzs = new Bill_Sys_CollectDocAndZip();
        ArrayList Paths = cNzs.Download(objAL, B);
        hDnl.Value = "";

        if (Paths.Count < 1)
        {

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
        }
        else
        {


            for (int i = 0; i < Paths.Count; i++)
            {
                string path = "";
                path = Paths[i].ToString().Replace(cNzs.getLawFirmPhysicalPath(), "");
                ScriptManager.RegisterStartupScript(this, typeof(string), "popup" + i.ToString(), "window.open('" + cNzs.getLawFirmLogicalPath() + path.Trim() + "'); ", true);
            }


        }

        Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
        DataSet Batchds = new DataSet();
        Batchds = _objBatch.ShowBatches(txtCompanyId.Text);

        txtCaseType.Text = extddlCaseType.Text.ToString();
        txtMedicalFacility.Text = extCompanyName.Text.ToString();
        grid.XGridBind();
        DtlView.DataSource = Batchds;
        DtlView.DataBind();
        for (int i = 0; i < DtlView.Items.Count; i++)
        {
            
        }

    }

    protected void btnDownloadALL_Click(object sender, EventArgs e)
    {
        Bill_Sys_CollectDocAndZip cNz = new Bill_Sys_CollectDocAndZip();
        string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string useId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
         string UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
       
        ArrayList Paths = new ArrayList();  
        DataSet dsAll = new DataSet();
        XMLData.XMLData obj = new XMLData.XMLData();
        XMLData.XMLData xd = new XMLData.XMLData();
        string szxmlfilename = ConfigurationManager.AppSettings["LFSearchCase"].ToString();
        xd = obj.ReadXml(szxmlfilename);
        dsAll = obj.XGridBind(this.Page, xd, 1, grid.RecordCount, "", txtSearchBox.Text);
        string B = hBatch.Value;
        string S = hStatus.Value;
        Paths=  cNz.DownloadAll(dsAll, B, useId, txtCompanyId.Text, szRemoteAddr, UserName);

        if (Paths.Count < 1)
        {

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
        }
        else
        {


            for (int i = 0; i < Paths.Count; i++)
            {
                string path = "";
                path = Paths[i].ToString().Replace(cNz.getLawFirmPhysicalPath(), "");
                ScriptManager.RegisterStartupScript(this, typeof(string), "popup" + i.ToString(), "window.open('" + cNz.getLawFirmLogicalPath() + path.Trim() + "'); ", true);
            }


        }
        Bill_Sys_CollectDocAndZip _objBatch = new Bill_Sys_CollectDocAndZip();
        DataSet Batchds = new DataSet();
        Batchds = _objBatch.ShowBatches(txtCompanyId.Text);

        txtCaseType.Text = extddlCaseType.Text.ToString();
        txtMedicalFacility.Text = extCompanyName.Text.ToString();
        grid.XGridBind();
        DtlView.DataSource = Batchds;
        DtlView.DataBind();
        hDnl.Value = ""; 

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        mbs.ApplicationSettings.ApplicationSettings_BO objApplicationName = null;

        objApplicationName = (mbs.ApplicationSettings.ApplicationSettings_BO)Application["OBJECT_APP_SETTINGS"];

        string sz_Bill_No = "";
        DataSet dsInfo = new DataSet();
        if (objApplicationName == null)
        {
            objApplicationName = new mbs.ApplicationSettings.ApplicationSettings_BO();
            Application["OBJECT_APP_SETTINGS"] = objApplicationName;
        }
        string szDocumentRootPath = ApplicationSettings.GetParameterValue("PhysicalBasePath");

        Bill_Sys_CollectDocAndZip _obj = new Bill_Sys_CollectDocAndZip(objApplicationName);

        for (int i = 0; i < grid.Rows.Count; i++)
        {

            CheckBox chkFind = (CheckBox)grid.Rows[i].FindControl("ChkPatientList");
            if (chkFind.Checked)
            {
                if (sz_Bill_No == "")
                {
                    sz_Bill_No = "'" + grid.Rows[i].Cells[2].Text.ToString() + "'";
                }
                else
                {
                    sz_Bill_No = sz_Bill_No = sz_Bill_No + "," + "'" + grid.Rows[i].Cells[2].Text.ToString() + "'";
                }
            }
        }

        dsInfo = _obj.GetPateintInfo(sz_Bill_No);
        if (dsInfo.Tables[0].Rows.Count > 0)
        {
            string xmlName = _obj.getFileName();
            string exlName = xmlName.Replace(".xml", ".xls");
            string file_path = ConfigurationSettings.AppSettings["XLPATH"].ToString();
            string Folder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();

            File.Copy(file_path, szDocumentRootPath + "Reports/" + exlName);
            _obj.GenerateXL(dsInfo.Tables[0], szDocumentRootPath + "Reports/" + exlName);
            if (File.Exists(szDocumentRootPath + Folder + exlName))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + exlName + "'); ", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Batch is Empty');", true);
            }
        }

    }

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
            selectCommand.Parameters.AddWithValue("@bt_operation", 2);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (SqlException exception)
        {
            exception.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        catch (SqlException exception)
        {
            exception.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
    }
    
    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        DataSet ds = new DataSet();
        mbs.ApplicationSettings.ApplicationSettings_BO objApplicationName = null;
        string file = "";

        objApplicationName = (mbs.ApplicationSettings.ApplicationSettings_BO)Application["OBJECT_APP_SETTINGS"];

        if (objApplicationName == null)
        {
            objApplicationName = new mbs.ApplicationSettings.ApplicationSettings_BO();
            Application["OBJECT_APP_SETTINGS"] = objApplicationName;
        }
        string szDocumentRootPath = ((mbs.ApplicationSettings.ApplicationSettings_DO)objApplicationName.getParameterValue(mbs.ApplicationSettings.ApplicationSettings_BO.KEY_DOCUMENT_UPLOAD_LOCATION_PHYSICAL)).ParameterValue;
        
        Bill_Sys_CollectDocAndZip _obj = new Bill_Sys_CollectDocAndZip(objApplicationName);
        mbs.lawfirm.SrvLawfirm obj_Headers = new SrvLawfirm();

        ds = obj_Headers.SelectGBBHeader(txtLawFirmID.Text.ToString());
        string exlName = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            string xmlName = _obj.getFileName();
            exlName = xmlName.Replace(".xml", ".xls");
            string file_path = ConfigurationSettings.AppSettings["GBBHeader_Path"].ToString();
            string Folder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();

            File.Copy(file_path, szDocumentRootPath + "Reports/" + exlName);
            string file_name=szDocumentRootPath + "Reports/" + exlName;
            file = SrvLawfirm.DatatabletoExcel(ds.Tables[0], file_name);
        }

        ArrayList list = new ArrayList();
        list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(exlName));
        Session["Download_Files"] = list;
    }
    
    protected void lnk_Download_status_Click(object sender, EventArgs e)
    {
        try
        {
            string line;
            DataTable table_Office = new DataTable();
            table_Office.Columns.Add("sz_bill_id", typeof(string));
            table_Office.Columns.Add("sz_assigned_lawfirm_id", typeof(string));
            table_Office.Columns.Add("sz_user_id", typeof(string));
            string sz_file_Name = ReportUpload.FileName;
            string temp_path = ConfigurationManager.AppSettings["temp_files"].ToString();
            string sz_user_id = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

            if (ReportUpload.HasFile)
            {
                ReportUpload.SaveAs(temp_path + sz_file_Name);
                string get_file_path = temp_path + sz_file_Name;

                using (StreamReader sr = new StreamReader(get_file_path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        DataRow row = table_Office.NewRow();
                        row["sz_bill_id"] = line;
                        row["sz_assigned_lawfirm_id"] = txtLawFirmID.Text.ToString();
                        row["sz_user_id"] = sz_user_id;
                        table_Office.Rows.Add(row);
                        table_Office.AcceptChanges();
                    }
                    sr.Close();
                }
            }
            mbs.lawfirm.SrvLawfirm obj_Headers = new SrvLawfirm();
            obj_Headers.MarkBillsDownloaded(table_Office, sz_user_id);
            ScriptManager.RegisterStartupScript(this, base.GetType(), "mmasd", "<script language='javascript'>alert('Bills Updated Successfully');</script>", false);
        }
        catch (Exception ex)
        {
            string exception = ex.Message.ToString();
        }
        
    }

    protected void DtlView_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item ||
              e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DateTime batchdate = Convert.ToDateTime(drv.Row["dt_batch_date"]);
            LinkButton lnk = (LinkButton)e.Item.FindControl("lnkDownloads");
            LinkButton lnkopen = (LinkButton)e.Item.FindControl("lnkOpen");

            int icount = Convert.ToInt32(ConfigurationSettings.AppSettings["BatchDays"].ToString());
            if ((DateTime.Now - batchdate).Days > icount)
            {
                lnk.Visible = true;
                lnkopen.Visible = false;
            }
            else
            {
                lnk.Visible = false;
                lnkopen.Visible = true;
            }
        }

    }
   
    

}
