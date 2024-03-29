﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.EmailRecordModel
{
    public class EmailRecord
    {
        public int EmailRecordId { get; set; }
        public string EmRecTo { get; set; }
        public string EmRecCC { get; set; }
        public string EmRecSubject { get; set; }
        public string EmRecBody { get; set; }
        public DateTime EmailRecDate { get; set; }
        public int UserID { get; set; }
    }
}
