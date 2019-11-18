﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_PONG.Models
{
    public class Feed
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int TakmicenjeID { get; set; }
        public Takmicenje Takmicenje { get; set; }
        public DateTime DatumModifikacije{ get; set; }
    }
}
