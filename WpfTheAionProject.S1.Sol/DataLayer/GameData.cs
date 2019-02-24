using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.DataLayer
{
    /// <summary>
    /// static class to store the game data set
    /// </summary>
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
                "\tYou have been hired by the Norlon Corporation to participate in its latest endeavor, the Aion Project. Your mission is to  test the limits of the new Aion Engine and report back to the Norlon Corporation.",
                "\tYou will begin by choosing a new location and using Aion Engine to travel to that point in the Galaxy.\n\tThe Aion Engine, design by Dr. Tormeld, is limited to four slipstreams, denoted by the first four Greek letters, from any given locations."
            };
        }
    }
}
