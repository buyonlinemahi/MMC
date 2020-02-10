﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.ICDCodeModel
{
    public class ICDCode
    {
        public int icdICDNumberID { get; set; }
        public string icdICDNumber { get; set; }
        public string icdICDTab { get; set; }
        public string ICDDescription { get; set; }
        public string ICDType { get; set; }
    }
}
