using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Weapon : GameItem
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public Weapon(int itemTypeID, string name, int value, int minDamage, int maxDamage, string description)
            : base(itemTypeID, name, value, description)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public Weapon Clone(int itemTypeID, string name, int value, int minDamage, int maxDamage, string description)
        {
            return new Weapon(ItemTypeID, Name, Value, MinimumDamage, MaximumDamage, Description);
        }
    }
}
