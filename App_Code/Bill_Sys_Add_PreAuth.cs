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
using System.Collections;

/// <summary>
/// Summary description for Bill_Sys_Add_PreAuth
/// </summary>
public class Bill_Sys_Add_PreAuth
{
    String strConn;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter da;
    DataSet ds;
    SqlDataReader dr;

    public DataSet Nodeleval(DataSet ds)
    {
        try
        {
            string str = "IMG-";
            string expression1 = "NodeID LIKE '" + str + "%'";
            DataRow[] foundrows;
            foundrows = ds.Tables[0].Select(expression1);
            foreach (DataRow dr in foundrows)
            {
                string id = dr.ItemArray.GetValue(0).ToString();
                string Parenetid = dr.ItemArray.GetValue(1).ToString();



                while (Parenetid != "")
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["NodeID"].ToString() == id && ds.Tables[0].Rows[i]["ParentID"].ToString() == Parenetid)
                        {
                            ds.Tables[0].Rows[i]["BT_UPDATE"] = "1";
                            id = Parenetid;

                            string expression2 = "NodeID LIKE '" + id + "%'";
                            DataRow[] foundrows1;
                            foundrows1 = ds.Tables[0].Select(expression2);
                            DataRow dr1 = foundrows1[0];
                            Parenetid = dr1.ItemArray.GetValue(1).ToString();
                            break;
                        }
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("NodeID");
                dt.Columns.Add("ParentID");
                dt.Columns.Add("NodeName");
                dt.Columns.Add("NodeIcon");
                dt.Columns.Add("NodeLevel");
                dt.Columns.Add("Expanded");
                dt.Columns.Add("BT_DELETE");
                dt.Columns.Add("BT_UPDATE");


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["BT_UPDATE"].ToString() == "1")
                    {
                        DataRow drow = dt.NewRow();
                        drow["NodeID"] = ds.Tables[0].Rows[i]["NodeID"].ToString();
                        drow["ParentID"] = ds.Tables[0].Rows[i]["ParentID"].ToString();
                        drow["NodeName"] = ds.Tables[0].Rows[i]["NodeName"].ToString();
                        drow["NodeIcon"] = ds.Tables[0].Rows[i]["NodeIcon"].ToString();
                        drow["NodeLevel"] = ds.Tables[0].Rows[i]["NodeLevel"].ToString();
                        drow["Expanded"] = ds.Tables[0].Rows[i]["Expanded"].ToString();
                        drow["BT_DELETE"] = ds.Tables[0].Rows[i]["BT_DELETE"].ToString();
                        drow["BT_UPDATE"] = ds.Tables[0].Rows[i]["BT_UPDATE"].ToString();
                        dt.Rows.Add(drow);
                    }
                }
                DataTableReader oReader;
                oReader = dt.CreateDataReader();

            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return ds;

       
    }
    public Bill_Sys_Add_PreAuth()
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }
    public DataSet Get_PreAuthorisation(string szcompayid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_get_patient_for_preauthoriasation", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dsResult;
    }

    public DataSet Get_PreAuthorisation_For_Specialty(string szcompayid)
    {
        SqlConnection sqlCon = new SqlConnection(strConn);
        DataSet dsResult = new DataSet();
        try
        {
            sqlCon.Open();
            comm = new SqlCommand("sp_get_preauth_bit_for_specialty", sqlCon);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szcompayid);
            da = new SqlDataAdapter(comm);
            da.Fill(dsResult);

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dsResult;
    }

    
}
