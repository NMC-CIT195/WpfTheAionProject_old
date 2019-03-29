using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Relic : GameItem
    {
        public Relic(int id, string name, int value, string description, int experiencePoints)
            : base(id, name, value, description, experiencePoints)
        {

        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
