using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                ExperiencePoints = 10,
                LocationId = 0
            };
        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "\tYou have been hired by the Norlon Corporation to participate in its latest endeavor, the Aion Project. Your mission is to  test the limits of the new Aion Engine and report back to the Norlon Corporation." +
                "\tYou will begin by choosing a new location and using Aion Engine to travel to that point in the Galaxy.\n\tThe Aion Engine, design by Dr. Tormeld, is limited to four slipstreams, denoted by the first four Greek letters, from any given locations."
            };
        }

        public static GameMapCoordinates InitialGameMapLocation()
        {
            return new GameMapCoordinates() { Row = 0, Column = 0 };
        }

        public static Map GameMap()
        {
            int rows = 3;
            int columns = 4;

            Map gameMap = new Map(rows, columns);

            //
            // row 1
            //
            gameMap.MapLocations[0, 0] = new Location()
            {
                Id = 1,
                Name = "Norlon Corporate Headquarters",
                Description = "The Norlon Corporation Headquarters is located in just outside of Detroit " +
                "Michigan.Norlon, founded in 1985 as a bio-tech company, is now a 36 billion dollar company with " +
                "huge holdings in defense and space research and development.",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "\tYou have been hired by the Norlon Corporation to participate in its latest endeavor, the " +
                "Aion Project. Your mission is to  test the limits of the new Aion Engine and report back to the Norlon " +
                "Corporation. You will begin by choosing a new location and using Aion Engine to travel to that point in the " +
                "Galaxy.\n\tThe Aion Engine, design by Dr. Tormeld, is limited to four slipstreams, denoted by the cardinal points on a compass, from any given locations."
            };
            gameMap.MapLocations[0, 1] = new Location()
            {
                Id = 2,
                Name = "Aion Base Lab",
                Description = "The Norlon Corporation research facility located in the city of Heraklion on " +
                "the north coast of Crete and the top secret research lab for the Aion Project.\nThe lab is a large, " +"" +
                "well lit room, and staffed by a small number of scientists, all wearing light blue uniforms with the hydra-like Norlan Corporation logo.",
                Accessible = true,
                ModifiyExperiencePoints = 10,
                Message = "Traveler, to move from location to location, simply touch the name of the desired location on your forearm."
            };

            //
            // row 2
            //
            gameMap.MapLocations[1, 1] = new Location()
            {
                Id = 3,
                Name = "Felandrian Plains",
                Description = "The Felandrian Plains are a common destination for tourist. Located just north of the " +
                "equatorial line on the planet of Corlon, they provide excellent habitat for a rich ecosystem of flora and fauna.",
                Accessible = true,
                ModifiyExperiencePoints = 10
            };
            gameMap.MapLocations[1, 2] = new Location()
            {
                Id = 4,
                Name = "Epitoria's Reading Room",
                Description = "Queen Epitoria, the Torian Monarh of the 5th Dynasty, was know for her passion for " +
                "galactic history. The room has a tall vaulted ceiling, open in the middle  with four floors of wrapping " +
                "balconies filled with scrolls, texts, and infocrystals. As you enter the room a red fog desends from the ceiling " +
                "and you begin feeling your life energy slip away slowly until you are dead.",
                Accessible = false,
                ModifiyExperiencePoints = 50,
                ModifyLives = -1,
                RequiredExperiencePoints = 40                
            };

            //
            // row 3
            //
            gameMap.MapLocations[2, 0] = new Location()
            {
                Id = 5,
                Name = "Xantoria Market",
                Description = "The Xantoria market, once controlled by the Thorian elite, is now an open market managed " +
                "by the Xantorian Commerce Coop. It is a place where many races from various systems trade goods." +
                "You purchase a blue potion in a thin, clear flask, drink it and receive 50 points of health.",
                Accessible = false,
                ModifiyExperiencePoints = 20,
                ModifyHealth = 50,
                Message = "Traveler, our telemetry places you at the Xantoria Market. We have reports of local health potions."
            };
            gameMap.MapLocations[2, 1] = new Location()
            {
                Id = 6,
                Name = "The Tamfasia Galactic Academy",
                Description = "The Tamfasia Galactic Academy was founded in the early 4th galactic metachron. " +
                "You are currently in the library, standing next to the protoplasmic encabulator that stores all " +
                "recorded information of the galactic history.",
                Accessible = true,
                ModifiyExperiencePoints = 10
            };

            return gameMap;
        }
    }
}
