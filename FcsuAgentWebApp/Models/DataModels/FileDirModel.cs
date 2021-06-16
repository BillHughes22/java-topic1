using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FcsuAgentWebApp.Models.DataModels
{
    public class FileDirModel
    {
        public int FilesDir_i { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}