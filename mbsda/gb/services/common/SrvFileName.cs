using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.common
{
    public class SrvFileName
    {
        private static string GetFileName(string p_sPrefix)
        {
            String szFileName;
            DateTime currentDate;
            currentDate = DateTime.Now;

            System.Random objRandom = new Random();
            szFileName = p_sPrefix + "_" + objRandom.Next(1, 10000).ToString() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
            return szFileName;
        }

        public static string ForPOM()
        {
            return GetFileName("P");
        }
    }
}