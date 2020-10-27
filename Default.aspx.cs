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

public partial class Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EncryptPassword();

            //"K6fnk0ag8Hao6+QlmpHr/Q=="
           // grdVisit.DataSource = "";
           // grdVisit.DataBind();
        }
    }

    private string EncryptPassword()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strPassPhrase = "Pas5pr@se";        // can be any string
        string strSaltValue = "s@1tValue";              // can be any string
        string strHashAlgorithm = "SHA1";           // can be "MD5"
        int intPasswordIterations = 2;           // can be any number
        string strInitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        int intKeySize = 256;
        string EncryptedPassword = "";
        try
        {
            EncryptedPassword = Bill_Sys_EncryDecry.Decrypt("8rMx4oFpjCEBtDi1CBBwwg==", strPassPhrase, strSaltValue, strHashAlgorithm, intPasswordIterations, strInitVector, intKeySize);
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
        return EncryptedPassword;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
