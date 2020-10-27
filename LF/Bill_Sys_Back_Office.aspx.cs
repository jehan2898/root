using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using Ionic.Zip;
using log4net;
using System.Data.OleDb;
using XGridView;


public partial class _Default : PageBase 
{
    private string szExcelFileNamePrefix = null;
    private static DataTable tableInward=new DataTable();
    private static DataTable tableInwardNew = new DataTable();
    private static   DataTable tableInVal=new DataTable();
    public ArrayList successcase;
    public static string[] save;
    private SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
    private int count;
    private static string szFileName, szdirName;
    private static ILog log = LogManager.GetLogger("LawFirmBackOfficeUpload");

    protected void btnExportToExcelInvalid_Click(object sender, EventArgs e)
    {
        DataTable tableInVal = new DataTable();
        //tableInVal = Bill_Sys_Back_Office.tableInVal;
        DataSet set = new DataSet();
        set.Tables.Add(tableInVal.Copy());
        if (set.Tables[0].Rows.Count != 0)
        {
            string str = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
            string str2 = this.lfnFileName() + ".xls";
            File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), (str + str2).Trim());
            new XGridViewControl().GenerateXL(set.Tables[0], str + str2);
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href =' " + (ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + str2) + "'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "ss", "alert('No Bills are available to Export to Excel sheet!');", true);
        }
    }

    protected void btnExportToExcelValid_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        table = tableInward.Copy();
        DataSet set = new DataSet();
        table.Columns.Remove("Claim Amount");
        table.Columns.Remove("Paid Amount");
        table.Columns.Remove("Balance Amount");
        table.Columns.Remove("Law Firm ID");
        set.Tables.Add(table);
        if (set.Tables[0].Rows.Count != 0)
        {
            string str = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
            string str2 = this.lfnFileName() + ".xls";
            File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), (str + str2).Trim());
            new XGridViewControl().GenerateXL(set.Tables[0], str + str2);
            ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "mm", " window.location.href =' " + (ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + str2) + "'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript((Page)this, base.GetType(), "ss", "alert('No Bills are available to Export to Excel sheet!');", true);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            log.Debug("Checking File Extension - Start");
            if (this.flUpload.HasFile)
            {
                string extension = Path.GetExtension(this.flUpload.FileName);
                if ((this.filedrpdwn.SelectedValue == "csv") && !(extension == ".csv"))
                {
                    this.Status.InnerHtml = "The uploaded file is not a CSV file";
                    this.Button2.Visible = false;
                    this.Button1.Visible = false;
                }
                else
                {
                    if ((!(this.filedrpdwn.SelectedValue == "excel") || (extension == ".xls")) || (extension == ".xlsx"))
                    {
                        goto Label_010C;
                    }
                    this.Status.InnerHtml = "The uploaded file is not a MS Excel file";
                    this.Button2.Visible = false;
                    this.Button1.Visible = false;
                }
            }
            else
            {
                this.Status.InnerHtml = "No file has been selected";
                this.Button2.Visible = false;
                this.Button1.Visible = false;
            }
            return;
        Label_010C:
            log.Debug("Checking File Extension - Done");
            string filename = ConfigurationManager.AppSettings["Upload_Folder"].ToString() + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + this.flUpload.FileName.Substring(this.flUpload.FileName.LastIndexOf("."));
            try
            {
                log.Debug("Saving Uploaded file");
                this.flUpload.SaveAs(filename);
            }
            catch (Exception exception)
            {
                log.Debug("Saving Upload file - Error");
                throw new Exception("Error in uploading file.<br />" + exception.Message);
            }
            log.Debug("Calling validateFile() method");
            this.grdInValidateData.Visible = false;
            this.validateFile(filename);
            log.Debug("Execute validateFile()- DONE");
        }
        catch (Exception exception2)
        {
            log.Debug("Exception occured in validateFile() method");
            this.Status.InnerHtml = exception2.Message;
            this.grdValidateData.Visible = false;
            this.Button1.Visible = false;
            this.Button2.Visible = false;
            tableInward = null;
            save = null;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        log.Debug("Process Button Clicked - Creating SQL Connection");
        SqlConnection selectConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 0x8ca0;
            command.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            string str = "";
            int count = tableInward.Rows.Count;
            int num2 = 0;
            int index = 0;
            string[] strArray = new string[tableInward.Columns.Count - 1];
            while (num2 < count)
            {
                int num4 = 0;
                string[] strArray3 = strArray;
                for (int j = 0; j < strArray3.Length; j++)
                {
                    string text1 = strArray3[j];
                    try
                    {
                        if (tableInward.Rows[num2][num4].ToString() == "")
                        {
                            throw new Exception();
                        }
                        strArray[num4] = "'" + tableInward.Rows[num2][num4].ToString() + "'";
                    }
                    catch
                    {
                        strArray[num4] = "''";
                    }
                    num4++;
                }
                int num5 = 0;
                foreach (string str2 in strArray)
                {
                    if (num5 == 0)
                    {
                        if (str2 != null)
                        {
                            str = str2;
                        }
                    }
                    else if (str2 != null)
                    {
                        str = str + "," + str2;
                    }
                    num5++;
                }
                log.Debug("Creating Query for CaseID");
                command.CommandText = "DECLARE @output nvarchar(255) exec sp_validate_law_firm_back_office_data_upload " + str + ",@output OUTPUT SELECT @output [status]";
                log.Debug(command.CommandText);
                try
                {
                    new SqlDataAdapter(command.CommandText, selectConnection).Fill(dataTable);
                    if (dataTable.Rows[num2][0].ToString().Trim() == "[OK]")
                    {
                        save[index] = "exec sp_upload_law_firm_back_office_data_upload " + str;
                        index++;
                    }
                }
                catch (Exception exception)
                {
                    selectConnection.Close();
                    command.Dispose();
                    throw new Exception("Data Error: " + exception.Message);
                }
                num2++;
            }
            count = 0;
            int num6 = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                tableInward.Rows[count]["Status"] = row[0].ToString();
                if (row[0].ToString() == "[OK]")
                {
                    num6++;
                }
                count++;
            }
            tableInVal = new DataTable();
            tableInVal.Columns.Add("Case ID");
            tableInVal.Columns.Add("Bill Number");
            tableInVal.Columns.Add("Status");
            long num7 = 0L;
            for (int i = 0; i < tableInward.Rows.Count; i++)
            {
                if (tableInward.Rows[i]["Status"].ToString().Trim() != "[OK]")
                {
                    DataRow row2 = tableInVal.NewRow();
                    num7 += 1L;
                    row2["Case ID"] = tableInward.Rows[i]["Case ID"];
                    row2["Bill Number"] = tableInward.Rows[i]["Bill Number"];
                    string[] strArray2 = tableInward.Rows[i]["Status"].ToString().Split(new char[] { ':' });
                    if (strArray2.Length == 2)
                    {
                        if (strArray2[1].ToString().Trim() == "1001")
                        {
                            row2["Status"] = "Either the case does not exist in the system or it is not assigned to the lawfirm running the batch";
                        }
                        else if (strArray2[1].ToString().Trim() == "1002")
                        {
                            row2["Status"] = "The bill number and case ID do not have a relationship. There is no bill number for the input case id";
                        }
                        else if (strArray2[1].ToString().Trim() == "1003")
                        {
                            row2["Status"] = "The bill status in the system is not set or is null ";
                        }
                        else if (strArray2[1].ToString().Trim() == "1004")
                        {
                            row2["Status"] = "The bill is not in transferred or downloaded status";
                        }
                        else if (strArray2[1].ToString().Trim() == "1005")
                        {
                            row2["Status"] = "The law firm has not yet provided their case ID, but are adding the index # or purchase date";
                        }
                    }
                    else
                    {
                        row2["Status"] = tableInward.Rows[i]["Status"];
                    }
                    tableInVal.Rows.Add(row2);
                }
            }
            int num10 = 0;
            while (num10 < tableInward.Rows.Count)
            {
                DataRow row3 = tableInward.Rows[num10];
                if (tableInward.Rows[num10]["Status"].ToString().Trim() != "[OK]")
                {
                    tableInward.Rows.Remove(row3);
                    tableInward.AcceptChanges();
                }
                else
                {
                    num10++;
                }
            }
            tableInward.AcceptChanges();
            this.grdValidateData.DataSource = tableInward;
            this.grdValidateData.DataBind();
            if (num10 > 0)
            {
                this.Button2.Enabled = true;
            }
            selectConnection.Close();
            if (num7 > 0L)
            {
                DataSet set = new DataSet();
                set.Tables.Add(tableInVal);
                set.AcceptChanges();
                this.grdInValidateData.DataSource = set.Tables[0];
                this.grdInValidateData.DataBind();
                this.grdInValidateData.Visible = true;
                this.btnExportToExcelInvalid.Visible = true;
            }
        }
        catch (Exception exception2)
        {
            selectConnection.Close();
            this.Status.InnerHtml = this.Status.InnerHtml + "<br />" + exception2.Message;
            this.Button1.Enabled = false;
            this.Button2.Enabled = false;
            save = null;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        try
        {
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 0x8ca0;
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            connection.Open();
            foreach (string str in save)
            {
                if ((str != "") && (str != null))
                {
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }
            }
            foreach (DataRow row in tableInward.Rows)
            {
                row[tableInward.Columns.Count - 1] = "[DONE]";
            }
            this.grdValidateData.DataSource = tableInward;
            this.grdValidateData.DataBind();
            connection.Close();
            this.Button2.Enabled = false;
        }
        catch (Exception exception)
        {
            connection.Close();
            this.Status.InnerHtml = "Error:<br />" + exception.Message;
            save = null;
            this.Button2.Enabled = false;
        }
    }

    protected string GetCol(int i)
    {
        return Convert.ToChar((int)(i + 0x41)).ToString();
    }

    protected void getlist(DirectoryInfo d1, string[] parm)
    {
        SqlCommand command = new SqlCommand();
        command.CommandTimeout = 0x8ca0;
        if (this.con1.State == ConnectionState.Open)
        {
            this.con1.Close();
        }
        this.con1.Open();
        command.Connection = this.con1;
        try
        {
            foreach (FileSystemInfo info in d1.GetFileSystemInfos())
            {
                if (info is FileInfo)
                {
                    bool flag;
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Concat(new object[] { "DECLARE @LOOP BIT='false' IF (EXISTS (SELECT ImageID FROM tblDocImages where Filename='", ((FileInfo)info).Name, "' and Status='1')) BEGIN DECLARE @PARM int =0 WHILE (@LOOP='false') BEGIN SET @PARM = (SELECT TOP 1 Imageid from tblDocImages where Imageid>@parm and Filename='", ((FileInfo)info).Name, "' and Status='1' order by ImageId asc) IF ((SELECT TagID FROM tblImageTag where ImageID=@PARM) = '", Convert.ToInt32(parm[2]), "') SET @LOOP= 'true' ELSE SET @PARM=(SELECT TOP 1 Imageid from tblDocImages where Imageid>@parm and Filename='", ((FileInfo)info).Name, "' and Status='1' order by ImageId asc) IF(@PARM is null) break END END ELSE SET @LOOP= 'false' select @LOOP " });
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        flag = Convert.ToBoolean(reader[0].ToString());
                    }
                    ((FileInfo)info).CopyTo(parm[1].ToString() + @"\" + ((FileInfo)info).Name, true);
                    if (flag)
                    {
                        goto Label_04FC;
                    }
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.Clear();
                    }
                    catch
                    {
                    }
                    command.CommandText = "sp_ws_upload_document";
                    command.Parameters.AddWithValue("@SZ_CASE_ID", parm[0]);
                    command.Parameters.AddWithValue("@SZ_COMPANY_ID", parm[3]);
                    command.Parameters.AddWithValue("@SZ_FILE_NAME", ((FileInfo)info).Name);
                    command.Parameters.AddWithValue("@SZ_FILE_PATH", parm[4] + "/");
                    command.Parameters.AddWithValue("@I_TAG_ID", Convert.ToInt32(parm[2]));
                    command.Parameters.AddWithValue("@SZ_USER_NAME", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    command.ExecuteNonQuery();
                }
                if (info is DirectoryInfo)
                {
                    string[] strArray = new string[] { parm[0], parm[1], parm[2], parm[3], parm[4] };
                    this.count++;
                    command.CommandType = CommandType.Text;
                    command.CommandText = string.Concat(new object[] { 
                        "IF EXISTS (SELECT NodeID FROM tblTags WHERE CaseID='", parm[0], "' AND NodeName='", ((DirectoryInfo) info).Name, "' and ParentID='", Convert.ToInt32(parm[2]), "' and SZ_COMPANY_ID='", parm[3].ToString(), "') BEGIN SELECT NodeID FROM tblTags WHERE CaseID='", parm[0], "' AND NodeName='", ((DirectoryInfo) info).Name, "' and ParentID='", Convert.ToInt32(parm[2]), "' and SZ_COMPANY_ID='", parm[3].ToString(), 
                        "' END ELSE BEGIN INSERT INTO tblTags(ParentID,NodeName,CaseID,NodeLevel,SZ_COMPANY_ID) VALUES('", Convert.ToInt32(parm[2]), "','", ((DirectoryInfo) info).Name, "','", parm[0].ToString(), "','", this.count, "','", parm[3].ToString(), "') SELECT NodeID FROM tblTags WHERE CaseID='", parm[0].ToString(), "' AND NodeName='", ((DirectoryInfo) info).Name, "' and ParentID='", Convert.ToInt32(parm[2]), 
                        "' and SZ_COMPANY_ID='", parm[3].ToString(), "' END "
                     });
                    using (SqlDataReader reader2 = command.ExecuteReader())
                    {
                        reader2.Read();
                        strArray[2] = reader2[0].ToString();
                    }
                    if (!Directory.Exists(parm[1].ToString() + @"\" + ((DirectoryInfo)info).Name))
                    {
                        Directory.CreateDirectory(parm[1].ToString() + @"\" + ((DirectoryInfo)info).Name);
                    }
                    strArray[1] = parm[1].ToString() + @"\" + ((DirectoryInfo)info).Name;
                    strArray[4] = parm[4].ToString() + "/" + ((DirectoryInfo)info).Name;
                    this.getlist((DirectoryInfo)info, strArray);
                    this.count--;
                }
            Label_04FC: ;
            }
        }
        catch (Exception exception)
        {
            this.zipstatus.InnerHtml = "An error occured&nbsp;&nbsp;Details: " + exception.Message;
        }
    }

    protected void grdInValidateData_PageIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdInValidateDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdInValidateData.PageIndex = e.NewPageIndex;
        this.grdInValidateData.DataSource = tableInVal;
        this.grdInValidateData.DataBind();
    }

    protected void grdValidateDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdValidateData.PageIndex = e.NewPageIndex;
        this.grdValidateData.DataSource = tableInward;
        this.grdValidateData.DataBind();
    }

    private string lfnFileName()
    {
        Random random = new Random();
        DateTime now = DateTime.Now;
        if (this.szExcelFileNamePrefix == null)
        {
            this.szExcelFileNamePrefix = "excel";
        }
        return (this.szExcelFileNamePrefix + "_" + random.Next(1, 0x2710).ToString() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LblDataUpload.Text = "<a href='#' title='View Accepted Formats'>Accepted Formats</a>";
        this.LblDocsUpload.Text = "<a href='#' title='View Accepted Formats'>Accepted Formats</a>";
        this.grdValidateData.PageIndexChanging += new GridViewPageEventHandler(this.grdValidateDate_PageIndexChanging);
        this.grdInValidateData.PageIndexChanging += new GridViewPageEventHandler(this.grdInValidateDate_PageIndexChanging);
        if (!this.Page.IsPostBack)
        {
            this.refresh();
        }
    }

    protected void refresh()
    {
        this.Status.InnerHtml = "";
        this.Button1.Enabled = true;
        this.Button1.Visible = false;
        this.Button2.Visible = false;
        this.Button2.Enabled = false;
        this.zipbtnsave.Visible = false;
    }

    protected void validateFile(string p_szFileName)
    {
        OleDbConnection connection;
        log.Debug("Executing validateFile()");
        this.grdValidateData.Columns.Clear();
        tableInward = new DataTable();
        tableInward.Columns.Add("Case ID");
        tableInward.Columns.Add("Bill Number");
        tableInward.Columns.Add("Law Firm ID");
        tableInward.Columns.Add("Claim Amount");
        tableInward.Columns.Add("Paid Amount");
        tableInward.Columns.Add("Balance Amount");
        tableInward.Columns.Add("Law Firm Case ID");
        tableInward.Columns.Add("Index No");
        tableInward.Columns.Add("Purchase Date");
        tableInward.Columns.Add("Trial  Date");
        tableInward.Columns.Add("LawFirm Status");
        tableInward.Columns.Add("Status");
        DataRow row = null;
        string str3 = this.filedrpdwn.SelectedValue.ToString();
        if (str3 == null)
        {
            goto Label_07F2;
        }
        if (!(str3 == "csv"))
        {
            if (str3 == "excel")
            {
                goto Label_02F6;
            }
            goto Label_07F2;
        }
        TextReader reader = null;
        try
        {
            try
            {
                log.Debug("Creating Text Reader to read the uploaded file");
                reader = new StreamReader(p_szFileName);
            }
            catch (Exception exception)
            {
                throw new Exception("Unable to read the file.\n" + exception.Message);
            }
            string str = "";
            string[] strArray = new string[tableInward.Columns.Count - 1];
            while (str != null)
            {
                row = tableInward.NewRow();
                str = reader.ReadLine();
                if (str == null)
                {
                    break;
                }
                try
                {
                    strArray = str.Split(new char[] { ';' });
                }
                catch (Exception exception2)
                {
                    reader.Close();
                    throw new Exception("Number of parameters in data file don't match.\n" + exception2.Message);
                }
                int num = 0;
                int num2 = 0;
                while (num < (tableInward.Columns.Count - 1))
                {
                    try
                    {
                        if (num == 2)
                        {
                            row[num] = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        }
                        else if ((((num == 7) || (num == 8)) || ((num == 0) || (num == 1))) || (((num == 6) || (num == 10)) || (num == 11)))
                        {
                            row[num] = strArray[num2++];
                        }
                        else
                        {
                            row[num] = "";
                        }
                    }
                    catch
                    {
                        row[num] = "";
                    }
                    num++;
                }
                tableInward.Rows.Add(row);
            }
            reader.Close();
            goto Label_07F2;
        }
        catch (Exception exception3)
        {
            try
            {
                reader.Close();
            }
            catch
            {
            }
            throw new Exception("Error in CSV Upload. " + exception3.Message);
        }
    Label_02F6:
        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + p_szFileName + ";Extended Properties='Excel 12.0;HDR=Yes;'");
        OleDbCommand selectCommand = new OleDbCommand();
        selectCommand.CommandType = CommandType.Text;
        selectCommand.CommandText = "SELECT * FROM [Sheet1$]";
        selectCommand.Connection = connection;
        OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
        DataSet dataSet = new DataSet();
        OleDbDataReader reader2 = null;
        try
        {
            try
            {
                adapter.Fill(dataSet);
            }
            catch (Exception exception4)
            {
                throw new Exception(exception4.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            DataTable table = new DataTable();
            int count = dataSet.Tables[0].Rows.Count;
            int num4 = dataSet.Tables[0].Columns.Count;
            for (int i = 0; i < num4; i++)
            {
                table.Columns.Add(i.ToString(), Type.GetType("System.String"));
            }
            for (int j = 0; j < count; j++)
            {
                DataRow row2 = table.NewRow();
                for (int k = 0; k < num4; k++)
                {
                    try
                    {
                        row2[k] = dataSet.Tables[0].Rows[j][k].ToString();
                    }
                    catch
                    {
                        row2[k] = "";
                    }
                }
                table.Rows.Add(row2);
            }
            dataSet.Tables.Add(table);
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + p_szFileName + ";Extended Properties='Excel 12.0;HDR=No;'";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int num8 = 1;
            int num9 = 6;
            foreach (DataRow row3 in dataSet.Tables[1].Rows)
            {
                num8++;
                for (int m = 0; m < num9; m++)
                {
                    row3[m].ToString();
                    if ((row3[m] == null) || (row3[m].ToString() == ""))
                    {
                        selectCommand.CommandText = "SELECT * FROM [Sheet1$" + this.GetCol(m) + num8.ToString() + ":" + this.GetCol(m) + num8.ToString() + "]";
                        selectCommand.ExecuteScalar().ToString();
                        dataSet.Tables[1].Rows[num8 - 2][m] = selectCommand.ExecuteScalar().ToString();
                    }
                }
            }
            for (num8 = 0; num8 < dataSet.Tables[1].Rows.Count; num8++)
            {
                if ((dataSet.Tables[1].Rows[num8][0].ToString() == null) || (dataSet.Tables[1].Rows[num8][0].ToString() == ""))
                {
                    goto Label_07F2;
                }
                row = tableInward.NewRow();
                int num11 = 0;
                int num12 = 0;
                while (num11 < (tableInward.Columns.Count - 1))
                {
                    try
                    {
                        if (num11 == 2)
                        {
                            row[num11] = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            if (row[num11].ToString() == "")
                            {
                                throw new Exception();
                            }
                        }
                        else if (((num11 == 7) || (num11 == 8)) || (((num11 == 0) || (num11 == 1)) || (num11 == 6)))
                        {
                            row[num11] = dataSet.Tables[1].Rows[num8][num12++];
                        }
                        else
                        {
                            switch (num11)
                            {
                                case 9:
                                    row[num11] = dataSet.Tables[1].Rows[num8][5];
                                    goto Label_0758;

                                case 10:
                                    row[num11] = dataSet.Tables[1].Rows[num8][6];
                                    goto Label_0758;
                            }
                            row[num11] = "";
                        }
                    }
                    catch
                    {
                        row[num11] = "";
                    }
                Label_0758:
                    num11++;
                }
                tableInward.Rows.Add(row);
            }
        }
        catch (Exception exception5)
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                adapter.Dispose();
                if (!reader2.IsClosed)
                {
                    reader2.Close();
                }
            }
            catch
            {
            }
            throw new Exception("Error in Excel File. " + exception5.Message);
        }
    Label_07F2:
        log.Debug("Datatable created from uploaded file");
        save = new string[tableInward.Rows.Count];
        foreach (DataColumn column in tableInward.Columns)
        {
            BoundField field = new BoundField();
            field.DataField = column.ColumnName;
            field.HeaderText = column.ColumnName;
            field.Visible = false;
            this.grdValidateData.Columns.Add(field);
        }
        this.grdValidateData.Columns[0].Visible = true;
        this.grdValidateData.Columns[1].Visible = true;
        this.grdValidateData.Columns[7].Visible = true;
        this.grdValidateData.Columns[8].Visible = true;
        this.grdValidateData.Columns[6].Visible = true;
        this.grdValidateData.Columns[9].Visible = true;
        this.grdValidateData.Columns[10].Visible = true;
        this.grdValidateData.Columns[tableInward.Columns.Count - 1].Visible = true;
        this.grdValidateData.DataSource = tableInward;
        this.grdValidateData.DataBind();
        this.Session["ExcelDataTable"] = tableInward;
        this.Status.InnerHtml = "Upload Completed";
        log.Debug("Upload Completed");
        this.Button1.Visible = true;
        this.Button2.Visible = true;
        this.Button1.Enabled = true;
        this.Button2.Enabled = false;
        this.btnExportToExcelValid.Visible = true;
        this.grdValidateData.Visible = true;
    }

    protected void zipbtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.zpupload.HasFile)
            {
                if (Path.GetExtension(this.zpupload.FileName) == ".zip")
                {
                    goto Label_006D;
                }
                this.zipstatus.InnerHtml = "Only zip format is supported";
                this.zipbtnsave.Enabled = false;
            }
            else
            {
                this.zipstatus.InnerHtml = "No file has been selected";
                this.zipbtnsave.Enabled = false;
            }
            return;
        Label_006D:
            szFileName = "";
            szdirName = "";
            szFileName = ConfigurationManager.AppSettings["Upload_Folder"].ToString() + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + @"\" + this.zpupload.FileName;
            szdirName = ConfigurationManager.AppSettings["Upload_Folder"].ToString() + ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + @"\";
            if (!Directory.Exists(szdirName))
            {
                Directory.CreateDirectory(szdirName);
            }
            this.zpupload.SaveAs(szFileName);
            using (ZipFile file = ZipFile.Read(szFileName))
            {
                file.ExtractAll(szdirName);
            }
            this.zipstatus.InnerHtml = "File has been succesfully uploaded";
            try
            {
                if (File.Exists(szFileName))
                {
                    File.Delete(szFileName);
                }
            }
            catch
            {
                throw new Exception("Cannot delete the uploaded zip file");
            }
            this.zipbtnsave.Visible = true;
            this.zipbtnsave.Enabled = true;
        }
        catch (Exception exception)
        {
            this.zipstatus.InnerHtml = "Error&nbsp;Details:" + exception.Message;
            this.zipbtnsave.Visible = false;
        }
    }

    protected void zipbtnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DirectoryInfo info = new DirectoryInfo(szdirName);
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.CommandTimeout = 0x8ca0;
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            selectCommand.Connection = connection;
            selectCommand.CommandType = CommandType.Text;
            foreach (DirectoryInfo info2 in info.GetDirectories())
            {
                selectCommand.CommandText = "IF (EXISTS (select 1 from MST_CASE_MASTER where SZ_CASE_ID='" + info2.Name + "' and SZ_ASSIGNED_LAW_FIRM_ID='" + ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "')) BEGIN SELECT SZ_CASE_ID [CaseID], sz_company_id [Result] from mst_case_master where sz_case_id='" + info2.Name + "' END ELSE BEGIN SELECT '" + info2.Name + "' [CaseID], 'INVALID CASEID' [Result]END";
                adapter.Fill(dataTable);
            }
            string[] strArray = new string[dataTable.Rows.Count];
            int num = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                if ((row[1].ToString() == "INVALID CASEID") || (row[1].ToString() == ""))
                {
                    strArray[num++] = row[0].ToString();
                }
            }
            string message = "";
            if (num > 0)
            {
                message = "Following CaseID are invalid<br />";
                foreach (string str2 in strArray)
                {
                    if ((str2 != "") && (str2 != null))
                    {
                        message = message + " -" + str2.ToString() + "<br />";
                    }
                }
                throw new Exception(message);
            }
            DataTable table2 = new DataTable();
            foreach (DataRow row2 in dataTable.Rows)
            {
                selectCommand.CommandText = "DECLARE @SZ_NODEID nvarchar(50) create table #temp (nodeid nvarchar(50)) insert into #temp(nodeid) exec sp_check_lawfirmdoc_files_folder_for_caseid '" + row2[0].ToString() + "', '" + row2[1].ToString() + "' set @SZ_NODEID = (select nodeid from #temp) drop table #temp DECLARE @SZPATH NVARCHAR(4000) DECLARE @NODELEVEL VARCHAR(10) DECLARE @SZPARENTID NVARCHAR(10) DECLARE @SZ_TEMP_PARENTID NVARCHAR(10) DECLARE @NODENAME NVARCHAR(1000) SET @NODELEVEL = 0 SELECT @NODELEVEL = NODELEVEL ,  @SZPARENTID = PARENTID ,  @NODENAME=NODENAME  FROM   TBLTAGS  WHERE NODEID=@SZ_NODEID SET @SZPATH = @NODENAME WHILE @NODELEVEL >= 1 BEGIN\t SELECT @SZ_TEMP_PARENTID=PARENTID,@NODENAME=NODENAME FROM TBLTAGS WHERE NODEID=@SZPARENTID SET @SZPARENTID=@SZ_TEMP_PARENTID if @NODELEVEL=1 set @SZPATH='" + row2[0].ToString() + @"'+'\'+@SZPATH else SET @SZPATH = @NODENAME + '\' + @SZPATH  SET @NODELEVEL = @NODELEVEL - 1 END SELECT '" + row2[0].ToString() + "' [CaseID], REPLACE(((SELECT ParameterValue FROM tblApplicationSettings WHERE ParameterID=2)+(SELECT sz_company_name from mst_billing_company where sz_company_id='" + row2[1].ToString() + @"')+'\'+@SZPATH),'/','\') [Path],@sz_nodeid [NodeID], '" + row2[1].ToString() + "' [CompanyID] ,REPLACE((SELECT sz_company_name from mst_billing_company where sz_company_id='" + row2[1].ToString() + @"')+'/'+@SZPATH,'\','/') [virtualpath]";
                adapter.Fill(table2);
            }
            this.successcase = new ArrayList();
            foreach (DataRow row3 in table2.Rows)
            {
                this.count = 1;
                DirectoryInfo info3 = new DirectoryInfo(szdirName + row3[0].ToString());
                if (!Directory.Exists(row3[1].ToString()))
                {
                    Directory.CreateDirectory(row3[1].ToString());
                }
                string[] parm = new string[5];
                parm[0] = row3[0].ToString();
                this.successcase.Add(parm[0]);
                parm[1] = row3[1].ToString();
                parm[2] = row3[2].ToString();
                parm[3] = row3[3].ToString();
                parm[4] = row3[4].ToString();
                this.getlist(info3, parm);
            }
            Directory.Delete(szdirName, true);
            this.zipstatus.InnerHtml = "Changes Saved Successfully for following CaseID";
            foreach (string str3 in this.successcase)
            {
                this.zipstatus.InnerHtml = this.zipstatus.InnerHtml + "<br /> -" + str3;
            }
            this.zipbtnsave.Enabled = false;
        }
        catch (Exception exception)
        {
            this.zipstatus.InnerHtml = "Error&nbsp;Details:" + exception.Message;
            this.zipbtnsave.Enabled = false;
        }
    }

}