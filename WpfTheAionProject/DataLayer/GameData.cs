﻿using System;
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

        public static GameMapLocation InitialGameMapLocation()
        {
            return new GameMapLocation() { Row = 0, Column = 0 };
        }

        public static Location[,] GameMap()
        {
            int rows = 3;
            int columns = 4;

            Location[,] mapLocations = new Location[rows, columns];

            mapLocations[0, 1] = new Location()
            {
                Id = 1,
                Name = "Aion Base Lab",
                Description = "The Norlon Corporation research facility located in the city of Heraklion on the north coast of Crete and the top secret research lab for the Aion Project.\nThe lab is a large, well lit room, and staffed by a small number of scientists, all wearing light blue uniforms with the hydra-like Norlan Corporation logo.",
                Accessible = true,
                ExperiencePoints = 10
            };

            mapLocations[1, 1] = new Location()
            {
                Id = 2,
                Name = "Felandrian Plains",
                Description = "The Felandrian Plains are a common destination for tourist. Located just north of the equatorial line on the planet of Corlon, they provide excellent habitat for a rich ecosystem of flora and fauna.",
                Accessible = true,
                ExperiencePoints = 10
            };

            mapLocations[2, 0] = new Location()
            {
                Id = 3,
                Name = "Xantoria Market",
                Description = "The Xantoria market, once controlled by the Thorian elite, is now an open market managed by the Xantorian Commerce Coop. It is a place where many races from various systems trade goods.",
                Accessible = false,
                ExperiencePoints = 20
            };

            mapLocations[0, 0] = new Location()
            {
                Id = 4,
                Name = "Norlon Corporate Headquarters",
                Description = "The Norlon Corporation Headquarters is located in just outside of Detroit Michigan.Norlon, founded in 1985 as a bio-tech company, is now a 36 billion dollar company with huge holdings in defense and space research and development.",
                Accessible = true,
                ExperiencePoints = 10
            };

            mapLocations[2, 1] = new Location()
            {
                Id = 4,
                Name = "The Tamfasia Galactic Academy",
                Description = "The Tamfasia Galactic Academy was founded in the early 4th galactic metachron. You are currently in the library, standing next to the protoplasmic encabulator that stores all recorded information of the galactic history.",
                Accessible = true,
                ExperiencePoints = 10
            };
            return mapLocations;
        }
    }
}
