using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace gb.mbs.da.common
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get the nummer of lines in the string.
        /// </summary>
        /// <returns>Nummer of lines</returns>
        public static int LineCount(string str)
        {
            string[] lines = Regex.Split(str, "\n\n\n\n");

            return lines.Length-1;
        }
    }
}
