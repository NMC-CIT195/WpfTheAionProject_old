using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class GameItem
    {
        //
        // auto implemented properties are used for 
        //
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public GameItem(int itemTypeID, string name, int value, string description)
        {
            ItemTypeID = itemTypeID;
            Name = name;
            Value = value;
            Description = description;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Value, Description);
        }
    }
}
