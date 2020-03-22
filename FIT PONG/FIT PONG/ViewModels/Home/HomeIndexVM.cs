﻿using FIT_PONG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_PONG.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<string> ZadnjiRezultati { get; set; }
        public List<(Objava, Takmicenje)> ZadnjeObjave { get; set; } = new List<(Objava, Takmicenje)>();
        public List<string> FunFacts { get; set; }
    }
}
