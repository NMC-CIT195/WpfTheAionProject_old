using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// game map class
    /// </summary>
    public class Map
    {
        #region FIELDS

        private Location[,] _mapLocations;
        private int _maxRows, _maxColumns;

        #endregion


        #region PROPERTIES

        public Location[,] MapLocations
        {
            get { return _mapLocations; }
            set { _mapLocations = value; }
        }

        #endregion


        #region CONSTRUCTORS

        public Map(int rows, int columns)
        {
            _maxRows = rows;
            _maxColumns = columns;
            _mapLocations = new Location[rows, columns];
        }

        #endregion


        #region METHODS

        //
        // get the alpha location if it exists
        //
        public Location AlphaLocation(GameMapCoordinates currentLocationCoordinates, int playerXp)
        {
            Location alphaLocation = null;

            //
            // not on north border
            //
            if (currentLocationCoordinates.Row > 0)
            {
                Location nextAlphaLocation = _mapLocations[currentLocationCoordinates.Row - 1, currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextAlphaLocation != null &&
                    (nextAlphaLocation.Accessible == true || nextAlphaLocation.IsAccessibleByExperiencePoints(playerXp)))
                {
                    alphaLocation = nextAlphaLocation;
                }
            }

            return alphaLocation;
        }

        //
        // get the beta location if it exists
        //
        public Location BetaLocation(GameMapCoordinates currentLocationCoordinates, int playerXp)
        {
            Location betaLocation = null;

            //
            // not on east border
            //
            if (currentLocationCoordinates.Column < _maxColumns - 1)
            {
                Location nextBetaLocation = _mapLocations[currentLocationCoordinates.Row, currentLocationCoordinates.Column + 1];

                //
                // location exists and player can access location
                //
                if (nextBetaLocation != null &&
                    (nextBetaLocation.Accessible == true || nextBetaLocation.IsAccessibleByExperiencePoints(playerXp)))
                {
                    betaLocation = nextBetaLocation;
                }
            }

            return betaLocation;
        }

        //
        // get the gamma location if it exists
        //
        public Location GammaLocation(GameMapCoordinates currentLocationCoordinates, int playerXp)
        {
            Location gammaLocation = null;

            //
            // not on south border
            //
            if (currentLocationCoordinates.Row < _maxRows - 1)
            {
                Location nextGammaLocation = _mapLocations[currentLocationCoordinates.Row + 1, currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextGammaLocation != null &&
                    (nextGammaLocation.Accessible == true || nextGammaLocation.IsAccessibleByExperiencePoints(playerXp)))
                {
                    gammaLocation = nextGammaLocation;
                }
            }

            return gammaLocation;
        }

        //
        // get the delta location if it exists
        //
        public Location DeltaLocation(GameMapCoordinates currentLocationCoordinates, int playerXp)
        {
            Location deltaLocation = null;

            //
            // not on west border
            //
            if (currentLocationCoordinates.Column > 0)
            {
                Location nextDeltaLocation = _mapLocations[currentLocationCoordinates.Row, currentLocationCoordinates.Column - 1];

                //
                // location exists and player can access location
                //
                if (nextDeltaLocation != null &&
                    (nextDeltaLocation.Accessible == true || nextDeltaLocation.IsAccessibleByExperiencePoints(playerXp)))
                {
                    deltaLocation = nextDeltaLocation;
                }
            }

            return deltaLocation;
        }

        #endregion
    }
}
