﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10206629_PRG2Assignment
{
    internal class Topping
    {
        public string Type {  get; set; }
        Topping() { }
        Topping(string type) 
        {
            this.Type = type;
        }
        public override string ToString()
        {
            return Type;
        }
    }
}