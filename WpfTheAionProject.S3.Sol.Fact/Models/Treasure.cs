using System;
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

        public Treasure(int itemTypeID, string name, int value, TreasureType type, string description)
            : base(itemTypeID, name, value, description)
        {
            Type = type;
        }

        public Treasure Clone(int itemTypeID, string name, int value, TreasureType type, string description)
        {
            return new Treasure(ItemTypeID, Name, value, Type, Description);
        }
    }
}
