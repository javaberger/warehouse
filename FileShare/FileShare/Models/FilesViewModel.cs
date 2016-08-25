using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FileShare.Helpers;
using FileShare.Models;

namespace FileShare.Helpers
{
        public class FilesViewModel
        {
            public ViewDataUploadFilesResult[] Files { get; set; }
        }
}