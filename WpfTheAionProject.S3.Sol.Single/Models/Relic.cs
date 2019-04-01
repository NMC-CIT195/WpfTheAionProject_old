using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Relic : GameItem
    {       
        public enum UseActionType
        {
            OPENLOCATION,
            KILLPLAYER
        }

        public UseActionType UseAction { get; set; }

        public Relic(int id, string name, int value, string description, int experiencePoints, string useMessage, UseActionType useAction)
            : base(id, name, value, description, experiencePoints, useMessage)
        {
            UseAction = useAction;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
