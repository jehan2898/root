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

/// <summary>
/// Summary description for Bill_Sys_CheckUploadFile
/// </summary>
public static class FileUtilities
{

    public static string FormatUploadedFileName(string sz_UploadFileName)
    {
        string szFileName = sz_UploadFileName;
        string strFileName = "";
        string FileNameExtension = "";

        szFileName = szFileName.Replace(" ", "_");
        szFileName = szFileName.Replace("+", "_");
        szFileName = szFileName.Replace("&", "_");
        szFileName = szFileName.Replace("$", "_");
        szFileName = szFileName.Replace(",", "_");
        szFileName = szFileName.Replace(":", "_");
        szFileName = szFileName.Replace(";", "_");
        szFileName = szFileName.Replace("?", "_");
        szFileName = szFileName.Replace("@", "_");
        szFileName = szFileName.Replace("<", "_");
        szFileName = szFileName.Replace(">", "_");
        szFileName = szFileName.Replace("/", "_");
        szFileName = szFileName.Replace("-", "_");
        szFileName = szFileName.Replace("%", "_");
        szFileName = szFileName.Replace("!", "_");

        strFileName = szFileName.Substring(0, szFileName.LastIndexOf((".")));
        FileNameExtension = szFileName.Substring(szFileName.LastIndexOf((".")));

        strFileName = strFileName.Replace(".", "_");

        FileNameExtension = FileNameExtension.ToLower();
        szFileName = strFileName + FileNameExtension;

        return szFileName;

    }
}
