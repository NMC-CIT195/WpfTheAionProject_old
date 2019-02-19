using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.DataLayer
{
    public static class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "Bonzo",
                Age = 43,
                JobTitle = Player.JobTitleName.Explorer,
                Race = Character.RaceType.Human,
                Health = 100,
                Lives = 3,
                ExperiencePoints = 0,
                LocationId = 0
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "You have been hired by the Norlon Corporation to participate in its latest endeavor, the Aion Project. Your mission is to  test the limits of the new Aion Engine and report back to the Norlon Corporation.",
                "You will begin by choosing a new location and using Aion Engine to travel to that point in the Galaxy."
            };
        }

        public static Location[,] Map()
        {
            int rows = 3;
            int columns = 4;

            Location[,] mapLocations = new Location[rows, columns];

            mapLocations[0, 0] = new Location()
            {
                Id = 1,

            };

            return mapLocations;
        }
    }
}
