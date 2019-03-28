﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Treasure : GameItem
    {
        public enum TreasureType
        {
            Coin,
            Jewel,
            Manuscript
        }

        public TreasureType Type { get; set; }

        public Treasure(int id, string name, int value, TreasureType type, string description)
            : base(id, name, value, description)
        {
            Type = type;
        }
    }
}
