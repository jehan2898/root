using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.office
{
    public class Office
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public gbmodel.account.Account Account { set; get; }
    }
}
