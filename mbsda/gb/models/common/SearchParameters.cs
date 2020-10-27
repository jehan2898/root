using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.common
{
    public class SearchParameters
    {
        public int StartIndex { set; get; }
        public int EndIndex { set; get; }
        public int Count { set; get; }
        public string OrderBy { set; get; }
        public string SearchText { set; get; }
        public string SortOrder { set; get; }
    }
}
