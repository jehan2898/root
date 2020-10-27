using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MergePDF
/// </summary>
public static class MergePDF
{
    public static string MergePDFFiles(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    {
        int iResult = 0;
        try
        {
            CUTEFORMCOLib.CutePDFUtilities objMyForm = new CUTEFORMCOLib.CutePDFUtilities();

            string KeyForCutePDF = System.Configuration.ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
            objMyForm.initialize(KeyForCutePDF);
            string errormessage = string.Empty;
            iResult = objMyForm.openFile(p_szSource1);
            if (iResult != 0)
            {
                iResult = objMyForm.appendFile(p_szSource2);

            }
            if (iResult != 0)
            {
                iResult = objMyForm.saveFile(p_szDestinationFileName);

            }
            objMyForm.saveFile(p_szDestinationFileName);

            if (iResult == 0)
                return "FAIL";
            else
                return "SUCCESS";



        }
        catch (Exception ex)
        {
           
            throw;
        }
       
    }
}