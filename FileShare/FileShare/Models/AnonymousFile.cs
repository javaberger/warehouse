using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileShare.Models
{
    public class AnonymousFile
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public byte[] File { get; set; }
        public string KeyAccess { get; set; }
    }
}