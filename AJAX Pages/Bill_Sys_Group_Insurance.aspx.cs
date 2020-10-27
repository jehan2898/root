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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_Sys_Group_Insurance : PageBase
{
    string sz_CompanyID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lstMenu.Attributes.Add("onChange", "this.form.TextBox1.value=this.options[this.selectedIndex].text");
        TextBox1.Attributes.Add("onKeyUp", "autoComplete(this,this.form.ctl00_ContentPlaceHolder1_lstMenu,'text',true)");

        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
        if (!Page.IsPostBack)
        {
            Insurance_Group oInsurance_Group = new Insurance_Group();
            lstMenu.DataSource = oInsurance_Group.Get_Insurance_Group(sz_CompanyID);
            lstMenu.DataTextField = "DESCRIPTION";
            lstMenu.DataValueField = "CODE";
            lstMenu.DataBind();
            LoadData();
        }
      
    }

    protected void btn_ADD_Click(object sender, EventArgs e)
    {
         ArrayList _objarr = new ArrayList();
         string str = hfselectedNodeinListbox.Value;
         string[] list = hfselectedNodeinListbox.Value.Split(';');
         //for (int i = 0; lstMenusToRole.Items.Count > i; i++)
         //{
         //    InsuranceSave _obj = new InsuranceSave();
         //    _obj.InsuranceId = lstMenusToRole.Items[i].Value;
         //    _objarr.Add(_obj);
         //}
         string userid = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();
         Insurance_Group _objinsGroup = new Insurance_Group();
         //_objinsGroup.
            _objinsGroup.Save_Insurance_Group(list, sz_CompanyID, txt_Group.Text, userid);
            hfselectedNodeinListbox.Value = "";
            hfselectedNodeTextinListbox.Value = "";
            txt_Group.Text = "";
            LoadData();
            lstMenusToRole.Items.Clear();
        
    }


    public void LoadData()
    {
        Insurance_Group _objbind = new Insurance_Group();
        DataSet ds = _objbind.BindGrid(sz_CompanyID);
        DataSet _Bindset = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("sz_Group_Id");
        dt.Columns.Add("sz_Group_Name");
        dt.Columns.Add("Insurance Id");
        dt.Columns.Add("Insurance Name");

        string bindString = "";
        string InsuranceID = "";
        string Groupcurrentname = "";
        string GroupprevName = "";

        string prvInsName = "";
        string CurrInsName = "";
        string prvInsID = "";
        string CurrInsID = "";
        int count = 0;
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            Groupcurrentname = dr["sz_Group_Name"].ToString();
            CurrInsName = dr["Insurance Name"].ToString();
            CurrInsID = dr["Insurance ID"].ToString();

            if (count != 0)
            {
                if (GroupprevName.Equals(Groupcurrentname))
                {
                    bindString = bindString + ';' + CurrInsName;
                    InsuranceID = InsuranceID + ';' + CurrInsID;
                }
                else
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["sz_Group_Id"] = "";
                    dr1["sz_Group_Name"] = GroupprevName;
                    dr1["Insurance Name"] = bindString;
                    dr1["Insurance Id"] = InsuranceID;
                    
                 dt.Rows.Add(dr1);
                    dt.AcceptChanges();
                    bindString = "";
                    InsuranceID = "";
                    GroupprevName = dr["sz_Group_Name"].ToString();
                    prvInsName = dr["Insurance Name"].ToString();
                    prvInsID = dr["Insurance Id"].ToString();
                    bindString = prvInsName;
                    InsuranceID = prvInsID;
                }
                GroupprevName = dr["sz_Group_Name"].ToString();
            }
            else
            {
                GroupprevName  = dr["sz_Group_Name"].ToString();
                prvInsName = dr["Insurance Name"].ToString();
                prvInsID = dr["Insurance Id"].ToString();
                bindString = prvInsName;
                InsuranceID = prvInsID;
                count = 1;

            }
            
        }
        if (count != 0)
        {
            DataRow dr1 = dt.NewRow();
            dr1["sz_Group_Id"] = "";
            dr1["sz_Group_Name"] = GroupprevName;
            dr1["Insurance Name"] = bindString;
            dr1["Insurance Id"] = InsuranceID;
            dt.Rows.Add(dr1);
            dt.AcceptChanges();
        }
        grdInsuranceGroup.DataSource = dt; //_objbind.BindGrid(sz_CompanyID);
        grdInsuranceGroup.DataBind();
        hfselectedNodeinListbox.Value  = "";
        hfselectedNodeTextinListbox.Value = "";
    }
    public void Save_Insurance_Group(string[] _objArr, string CompanyID, string GroupName, string UserID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
            //SqlTransaction trans;
          //  trans = conn.BeginTransaction();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SP_DELETE_INSURANCE_GROUP";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
         // comm.Transaction = trans;
            comm.Parameters.AddWithValue("@sz_group_name", GroupName);
            comm.Parameters.AddWithValue("@sz_company_id", CompanyID);
            comm.ExecuteNonQuery();
            foreach (string objid in _objArr)
            {
                //InsuranceSave _objSave = (InsuranceSave)_objArr[i];
                if (objid != "")
                {
                    comm = new SqlCommand();
                    comm.CommandText = "SP_ADD_INSURANCE_GROUP";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                 
                    comm.Parameters.AddWithValue("@sz_insurance_id", objid);
                    comm.Parameters.AddWithValue("@sz_group_name", GroupName);
                    comm.Parameters.AddWithValue("@sz_company_id", CompanyID);
                    comm.Parameters.AddWithValue("@sz_user_id", UserID);
                    comm.ExecuteNonQuery();
                }

            }

           // trans.Commit();

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
        finally
        {
            //if (conn.State == ConnectionState.Open)
            //{
            //    conn.Close();
            //}
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void grdInsuranceGroup_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfLinkSelected.Value = "1";
        lstMenusToRole.Items.Clear();
        int index = int.Parse(e.CommandArgument.ToString());
        string insId = grdInsuranceGroup.DataKeys[index]["Insurance Id"].ToString();
        string insname = grdInsuranceGroup.Rows[index].Cells[3].Text.ToString();
        string insGroupName = grdInsuranceGroup.DataKeys[index]["sz_Group_Name"].ToString();
        txt_Group.Text = insGroupName;
        string[] arrID = insId.Split(';');
        hfselectedNodeinListbox.Value = insId + ';';
        insname = insname.Replace("&amp;", "&");
        string[] arrName = insname.Split(';');
        hfselectedNodeTextinListbox.Value = insname + ';';

        insname = insname.Replace("&amp;", "&");
       

        for (int i = 0; i < arrID.Length; i++)
        {
            if (arrID[i] != "")
            {
                ListItem l = new ListItem(arrName[i], arrID[i]);
                lstMenu.Items.Remove(l);
                lstMenusToRole.Items.Add(l);
                lstMenusToRole.DataBind();
                //txtOffice.Text = txtOffice.Text + officevalue[i];
            }
        }
        
    }
    protected void lb_Group_Name_OnClick(object sender, EventArgs e)
    {
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lstMenusToRole.Items.Clear();
        txt_Group.Text = "";
        hfselectedNodeinListbox.Value = "";
        hfselectedNodeTextinListbox.Value = "";
        hfLinkSelected.Value = "0";
        lstMenu.DataBind();
    }
}
