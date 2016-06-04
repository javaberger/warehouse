using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileShare.Models
{
    public class FilesStorageContext:DbContext
    {
        public FilesStorageContext() : base("ConnectionString") { }

        public DbSet<AnonymousFile> AnonymousFile { get; set; }
    }
}