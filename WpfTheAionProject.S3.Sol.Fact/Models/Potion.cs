using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Potion : GameItem
    {
        public int HealthChange { get; set; }
        public int LivesChange { get; set; }

        public Potion(int itemTypeID, string name, int value, int healthChange, int livesChange, string description)
            : base(itemTypeID, name, value, description)
        {
            HealthChange = healthChange;
            LivesChange = livesChange;
        }

        public Potion Clone(int itemTypeID, string name, int value, int healthChange, int livesChange, string description)
        {
            return new Potion(ItemTypeID, Name, Value, HealthChange, LivesChange, Description);
        }
    }
}
