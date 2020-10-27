using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Bill_Sys_ChangeVersion
/// </summary>
public class Bill_Sys_ChangeVersion
{
    Page pg;
    string[] listButton;
    string[] gridIndex;
    protected Button btn = null;
    public Bill_Sys_ChangeVersion(Page pg1)
	{
        pg = pg1;
	}
    public string [] MakeReadOnlyPage(string PageName)
    {
        int num = 0;
        VisibleButton(PageName);
        while (num <= (pg.Form.Controls.Count - 1))
        {
            if (pg.Form.Controls[num].ID != null)
            {
                if (pg.Form.Controls[num].GetType() == typeof(Panel))
                {
                    for (int k = 0; k <= (((Panel)pg.Form.Controls[num]).Controls.Count - 1); k++)
                    {
                        if (pg.Form.Controls[num].Controls[k].GetType() == typeof(Button))
                        {
                            if (listButton != null)
                            {
                                foreach (string buttonid in listButton)
                                {
                                    if (buttonid.Equals(pg.Form.Controls[num].Controls[k].ID))
                                    {
                                        (pg.Form.Controls[num]).Controls[k].Visible = false;
                                    }
                                }
                                //  (pg.Form.Controls[num]).Controls[k].Visible = false;
                            }
                        }
                    }

                }
                else if (pg.Form.Controls[num].GetType() == typeof(HtmlTable))
                {

                    for (int k = 0; k <= (((HtmlTable)pg.Form.Controls[num]).Controls.Count - 1); k++)
                    {
                        if (pg.Form.Controls[num].Controls[k].GetType() == typeof(Button))
                        {
                           if (listButton!=null)
                           {
                            foreach (string buttonid in listButton)
                            {
                                if (buttonid.Equals(pg.Form.Controls[num].Controls[k].ID))
                                {
                                    (pg.Form.Controls[num]).Controls[k].Visible = false;
                                }
                            }
                            //pg.Form.Controls[num]).Controls[k].Visible = false;
                           }
                        }
                    }
                }
                else if (pg.Form.Controls[num].GetType() == typeof(ContentPlaceHolder))
                {
                    for (int k = 0; k <= (((ContentPlaceHolder)pg.Form.Controls[num]).Controls.Count - 1); k++)
                    {
                        if (pg.Form.Controls[num].Controls[k].GetType() == typeof(Button))
                        {
                            if (listButton != null)
                            {
                                foreach (string buttonid in listButton)
                                {
                                    if (buttonid.Equals(pg.Form.Controls[num].Controls[k].ID))
                                    {
                                        (pg.Form.Controls[num]).Controls[k].Visible = false;
                                    }
                                }
                                //(pg.Form.Controls[num]).Controls[k].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    if (pg.Form.Controls[num].GetType() == typeof(Button))
                    {
                        foreach (string buttonid in listButton)
                        {
                            if (listButton != null)
                            {
                                if (buttonid.Equals(pg.Form.Controls[num].ID))
                                {
                                    (pg.Form.Controls[num]).Visible = false;
                                }
                            }
                            // (pg.Form.Controls[num]).Visible = false;
                        }
                    }
                }
            }
            num++;
        }
       // gridIndexVisible();
        return gridIndex;
    }

    public void VisibleButton(string pageName)
    {
        string strButtonList = null;
        string strgridindex = null;
        string strconn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strconn);        
        SqlCommand cmd = new SqlCommand();
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader dr;
        try
        {
            con.Open();
            cmd.CommandText = "SP_VISIBLE_BUTTON";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PAGENAME", pageName);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strButtonList = dr[0].ToString();
                strgridindex = dr[1].ToString();
            }
            if (strButtonList != null)
            {
                char[] seperator = { ',' };
                listButton = strButtonList.Split(seperator);
                gridIndex = strgridindex.Split(seperator);
            }
            
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            con.Close();
        }

    }
    

    public string  VisibleButton1(string pageName)
    {
        string strButtonList = null;
        string strgridindex = null;
        string strconn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader dr;
        try
        {
            con.Open();
            cmd.CommandText = "SP_VISIBLE_BUTTON";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PAGENAME", pageName);
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strButtonList = dr[0].ToString();
                strgridindex = dr[1].ToString();
            }
            if (strButtonList != null)
            {
                char[] seperator = { ',' };
                listButton = strButtonList.Split(seperator);
                gridIndex = strgridindex.Split(seperator);
            }
            return strgridindex;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); return null;
        }
        finally
        {
            con.Close();
        }
        return null;
    }
}
