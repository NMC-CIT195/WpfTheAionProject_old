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
        private GameMapCoordinates _currentLocationCoordinates;

        #endregion

        #region PROPERTIES

        public Location[,] MapLocations
        {
            get { return _mapLocations; }
            set { _mapLocations = value; }
        }

        public GameMapCoordinates CurrentLocationCoordinates
        {
            get { return _currentLocationCoordinates; }
            set { _currentLocationCoordinates = value; }
        }

        public Location CurrentLocation
        {
            get { return _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column]; }
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
        // move alpha
        //
        public void MoveAlpha()
        {
            //
            // not on north border
            //
            if (_currentLocationCoordinates.Row > 0)
            {
                _currentLocationCoordinates.Row -= 1;
            }
        }

        //
        // move alpha
        //
        public void MoveBeta()
        {
            //
            // not on east border
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                _currentLocationCoordinates.Column += 1;
            }
        }

        //
        // move gamma
        //
        public void MoveGamma()
        {
            //
            // not on south border
            //
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                _currentLocationCoordinates.Row += 1;
            }
        }


        //
        // move alpha
        //
        public void MoveDelta()
        {
            //
            // not on west border
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                _currentLocationCoordinates.Column -= 1;
            }
        }

        //
        // get the alpha location if it exists
        //
        public Location AlphaLocation(Player player)
        {
            Location alphaLocation = null;

            //
            // not on north border
            //
            if (_currentLocationCoordinates.Row > 0)
            {
                Location nextAlphaLocation = _mapLocations[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextAlphaLocation != null &&
                    (nextAlphaLocation.Accessible == true || nextAlphaLocation.IsAccessibleByExperiencePoints(player.ExperiencePoints)))
                {
                    alphaLocation = nextAlphaLocation;
                }
            }

            return alphaLocation;
        }

        //
        // get the beta location if it exists
        //
        public Location BetaLocation(Player player)
        {
            Location betaLocation = null;

            //
            // not on east border
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                Location nextBetaLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];

                //
                // location exists and player can access location
                //
                if (nextBetaLocation != null &&
                    (nextBetaLocation.Accessible == true || nextBetaLocation.IsAccessibleByExperiencePoints(player.ExperiencePoints)))
                {
                    betaLocation = nextBetaLocation;
                }
            }

            return betaLocation;
        }

        //
        // get the gamma location if it exists
        //
        public Location GammaLocation(Player player)
        {
            Location gammaLocation = null;

            //
            // not on south border
            //
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                Location nextGammaLocation = _mapLocations[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];

                //
                // location exists and player can access location
                //
                if (nextGammaLocation != null &&
                    (nextGammaLocation.Accessible == true || nextGammaLocation.IsAccessibleByExperiencePoints(player.ExperiencePoints)))
                {
                    gammaLocation = nextGammaLocation;
                }
            }

            return gammaLocation;
        }

        //
        // get the delta location if it exists
        //
        public Location DeltaLocation(Player player)
        {
            Location deltaLocation = null;

            //
            // not on west border
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                Location nextDeltaLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];

                //
                // location exists and player can access location
                //
                if (nextDeltaLocation != null &&
                    (nextDeltaLocation.Accessible == true || nextDeltaLocation.IsAccessibleByExperiencePoints(player.ExperiencePoints)))
                {
                    deltaLocation = nextDeltaLocation;
                }
            }

            return deltaLocation;
        }

        #endregion
    }
}
