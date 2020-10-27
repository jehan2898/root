using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace gb.mbs.da.services.common
{
   public class SrvFile
    {
       public static string  ValidateFile(string sFileName)
       {
           char[] arrNotAllowChar = { '~','!','@','#','$','%','^','&','*','?',':','\'','\\','/','<','>','{','}',',','`','[',']','=','+','|','(',')'};
           for (int i = 0; i < arrNotAllowChar.Length; i++)
           {
               if(sFileName.Contains(arrNotAllowChar[i].ToString()))
               {
                   sFileName = sFileName.Replace(arrNotAllowChar[i], '_');
               
               }
           }
           string sExtension = Path.GetExtension(sFileName); // returns .exe
           sFileName = Path.GetFileNameWithoutExtension(sFileName); // returns File
           sFileName = sFileName.Replace('.', '_');
           sFileName = sFileName  + sExtension;
           return sFileName;

       }
    }
}
