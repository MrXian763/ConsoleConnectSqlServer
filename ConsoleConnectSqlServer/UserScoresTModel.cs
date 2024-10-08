﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleConnectSqlServer
{
    [Table("UserScoresT")]
    public class UserScoresTModel
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public float Chinese { get; set; }
        public float English { get; set; }
        public float Math { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
