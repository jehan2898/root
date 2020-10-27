using System;
using System.Collections.Generic;
using System.Text;

namespace gb.mbs.da.model.common
{
    public class File
    {
        public long ID { set; get; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte[] Bytes { get; set; }
        public string ShortPath { get; set; }

        public string GetFullPath()
        {
            //TODO: Check fo name and path is null
            return Path + "/" + Name;
        }
    }
}