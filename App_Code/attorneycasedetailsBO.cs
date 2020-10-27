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
/// Summary description for attorneycasedetailsBO
/// </summary>
public class attorneycasedetailsBO
{
    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
	public attorneycasedetailsBO()
	{
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
	}
    public DataSet QuickSearchAttorney(string Search_Text, string str_Company_Id, string str_Prefix,string p_userid)
    {
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_QUICKSEARCH_ATT";
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            int flag = 0;
            int testflag = 4;
            //foreach (Char chTest in Search_Text)
            //{
            //    if (Char.IsNumber(chTest))
            //    {
            //        flag = 0;
            //    }
            //    else
            //    {
            //        flag = 1; 
            //    }
            //}

            foreach (Char chTest in Search_Text)
            {
                if (!Char.IsNumber(chTest))
                {
                    flag = 0;
                }
                else
                {
                    flag = 1;
                    testflag = 1;
                    break;
                }
            }

            foreach (Char chTest in Search_Text)
            {
                if (Char.IsNumber(chTest))
                {
                    flag = 2;
                    testflag = 2;
                    break;
                }
                else
                {
                    if (testflag != 4)
                    {
                        flag = 3;
                        testflag = 3;
                        break;
                    }
                }
            }

            if (flag == 2)
            {
                comm.Parameters.AddWithValue("@SZ_CASE_NO", Search_Text);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_FLAG", "CASE_NO");
                comm.Parameters.AddWithValue("@sz_user_id", p_userid);
            }
            if (flag == 0 && testflag == 4)
            {
                comm.Parameters.AddWithValue("@SZ_PATIENT_NAME", Search_Text);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_FLAG", "NAME");
                comm.Parameters.AddWithValue("@sz_user_id", p_userid);
            }
            if (flag == 3)
            {
                int i = 0;
                foreach (Char chTest in Search_Text)
                {
                    if (!Char.IsNumber(chTest))
                    {
                        i++;
                    }

                }
                string Case_No = Search_Text.Substring(i);
                comm.Parameters.AddWithValue("@SZ_CASE_NO", Case_No);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", str_Company_Id);
                comm.Parameters.AddWithValue("@SZ_PREFIX", str_Prefix);
                comm.Parameters.AddWithValue("@SZ_FLAG", "PREFIX");
                comm.Parameters.AddWithValue("@sz_user_id", p_userid);
            }
            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally { conn.Close(); }
   return null;


    }
}