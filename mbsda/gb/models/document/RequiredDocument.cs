using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;

namespace gb.mbs.da.model.document
{
    public class RequiredDocument
    {
        public long ID { get; set; }
        public int Type { get; set; }
        public bool IsReceived { get; set; }
        public string Note { get; set; }
        public gbmodel.user.User AssignedTo { get; set; }
        public gbmodel.user.User UpdatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}