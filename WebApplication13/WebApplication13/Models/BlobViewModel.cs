using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{
    public class BlobViewModel
    {
        public string ContainerName { get; set; }
        public string StorageUri { get; set; }
        public string ActualFileName { get; set; }
        public string PrimaryUri { get; set; }
        public string fileExtention { get; set; }

        public string FileNameWithOutExt
        {
            get { return Path.GetFileNameWithoutExtension(ActualFileName); }
        }

        public string FileNameExtentionOnly
        {
            get { return System.IO.Path.GetExtension(ActualFileName).Substring(1); }
        }
    }
}