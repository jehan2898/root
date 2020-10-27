/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Login.aspx.cs
/*Purpose              :       To Login in Application
/*Author               :       Sandeep D
/*Date of creation     :       16 Dec 2008  
/*Modified By          :
/*Modified Date        :
/************************************************************/
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
using log4net;
using System.Reflection;
using System.Diagnostics;

public partial class Bill_Sys_Login : System.Web.UI.Page
{
    private static ILog log = LogManager.GetLogger("PDFValueReplacement");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        lblCompanyName.Text = VersionInfo.CompanyName;
        lblVersion.Text = VersionInfo.Version;
        lblDatabaseServer.Text = VersionInfo.DatabaseServer;
        lblDomainError.Visible = false;
        lblDomainError.Text = "";
        if (Request.QueryString["dt"] != null)
        {
            hdUrlIntegration.Value = Request.QueryString["dt"].ToString();
            Session["urlintegration"] = hdUrlIntegration.Value;
        }
        else
        {
        }
        if (Session["DomainErro"] != null)
        {
            if (Session["DomainErro"].ToString().ToLower().Trim() == "true")
            {
                lblDomainError.Text = "The username or password you entered is incorrect.";
                lblDomainError.Visible = true;
                lblDomainError.ForeColor = System.Drawing.Color.Red;
                Session["DomainErro"] = null;
                return;
                
            }
            else
            {
                lblDomainError.Visible = false;
                lblDomainError.Text = "";
            }
        }
        else
        {
            lblDomainError.Visible = false;
            lblDomainError.Text = "";
        }
        
        if(!Page.IsPostBack)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("Apllication Strat");
            if (Application["XmlStatus"] == null)
            {
                try
                {
                    XMLData.XMLData xd = new XMLData.XMLData(); // object of the XMLData class.
                    string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "XML\\";//get path of the "XML" folder
                    log.Debug("path " + path);
                    string[] fileEntries = System.IO.Directory.GetFiles(path);// get files in "XML" folder of the application
                    foreach (string szFilePath in fileEntries)    // read each file name in the directory "XML"
                    {
                        string szFileName = szFilePath.Substring(szFilePath.LastIndexOf("\\")).Replace("\\", "");   // Get file name with extension
                        log.Debug("szFileName " + szFileName);
                        string szExtension = szFileName.Substring(szFileName.LastIndexOf("."));     // get extension of the file.
                        if (szExtension.Equals(".xml") || szExtension.Equals(".XML")) // check extension of the file in directory "XML".
                        {
                            if (szFileName != "Location.xml")
                            {
                                xd = xd.ReadXml(szFilePath);// read xml and add the values in XMLData object.
                                string szKey = szFileName.Replace(szExtension, "");// name of the file is key of the variable.
                                Application.Add(szKey, xd);// add key in the application.
                                Application["XmlStatus"] = true;
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
                    base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
                }
               
            }
            
            if (Session["BILLING_COMPANY_OBJECT"] == null)
            {
                if (Request.Cookies["UserID"] != null)
                {
                    Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
                }

                if (Request.Cookies["UserName"] != null)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                }
                if (Request.Cookies["DefaultURL"] != null)
                {
                    Response.Cookies["DefaultURL"].Expires = DateTime.Now.AddDays(-1);
                }
                Session["USER_OBJECT"] = null;
                Session["BILLING_COMPANY_OBJECT"] = null;
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["DefaultURL"] = null;
                Session.Abandon();
            }
            else
            {
                log.Debug("Before ReDirecting TO  Bill_Sys_Default.aspx ");
                Response.Redirect("Bill_Sys_Default.aspx", false);  
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void linkurl_Click(object sender, EventArgs e)
    {   
        string strConn = ConfigurationManager.AppSettings["DoctorModuleURL"].ToString();
        Response.Redirect(strConn);
    }
}