using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;
using WpfTheAionProject;

namespace WpfTheAionProject.PresentationLayer
{
    /// <summary>
    /// view model for the game session view
    /// </summary>
    public class GameSessionViewModel : ObservableObject
    {
        #region ENUMS

        #endregion

        #region FIELDS

        private DateTime _gameStartTime;

        private Player _player;
        private List<string> _messages;

        private Location[,] _gameMap;
        private int _maxRows, _maxColumns;
        private GameMapCoordinates _currentLocationCoordinates;
        private Location _currentLocation;
        private Location _alphaLocation, _betaLocation, _gammaLocation, _deltaLocation;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }
        public Location[,] GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged("CurrentLocation");
            }
        }

        //
        // expose information about travel points from current location
        //
        public Location AlphaLocation
        {
            get { return _alphaLocation; }
            set
            {
                _alphaLocation = value;
                OnPropertyChanged("AlphaLocation");
                OnPropertyChanged("HasAlphaLocation");
            }
        }
        public Location BetaLocation
        {
            get { return _betaLocation; }
            set
            {
                _betaLocation = value;
                OnPropertyChanged("BetaLocation");
                OnPropertyChanged("HasBetaLocation");
            }
        }
        public Location GammaLocation
        {
            get { return _gammaLocation; }
            set
            {
                _gammaLocation = value;
                OnPropertyChanged("GammaLocation");
                OnPropertyChanged("HasGammaLocation");
            }
        }
        public Location DeltaLocation
        {
            get { return _deltaLocation; }
            set
            {
                _deltaLocation = value;
                OnPropertyChanged("DeltaLocation");
                OnPropertyChanged("HasDeltaLocation");
            }
        }
        public bool HasAlphaLocation { get { return AlphaLocation != null; } }
        public bool HasBetaLocation { get { return BetaLocation != null; } }
        public bool HasGammaLocation { get { return GammaLocation != null; } }
        public bool HasDeltaLocation { get { return DeltaLocation != null; } }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(
            Player player,
            List<string> initialMessages,
            Location[,] gameMap,
            GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;
            _messages = initialMessages;

            _gameMap = gameMap;
            _maxRows = _gameMap.GetLength(0);
            _maxColumns = _gameMap.GetLength(1);

            _currentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            UpdateAvailableTravelPoints();
        }

        /// <summary>
        /// return a unique empty location object
        /// </summary>
        /// <returns>empty location object</returns>
        public Location EmptyLocation()
        {
            return new Location()
            {
                Id = 0,
                Name = " *** No Available Slipstream ***",
                Description = "This channel does not currently have an available slipstream from this access point. Please choose another slipstream.",
                Accessible = true
            };
        }

        /// <summary>
        /// calculate the available travel points from current location
        /// game slipstreams are a mapping against the 2D array where 
        /// Alpha = North
        /// Beta = East
        /// Gamma = South
        /// Delta = West
        /// </summary>
        private void UpdateAvailableTravelPoints()
        {
            //
            // reset travel location information
            //
            AlphaLocation = null;
            BetaLocation = null;
            GammaLocation = null;
            DeltaLocation = null;

            //
            // not on north boundary of map array (alpha)
            //
            if (_currentLocationCoordinates.Row > 0)
            {
                if (_gameMap[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column] != null) // location exists
                {
                    AlphaLocation = _gameMap[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column];
                }
            }

            //
            // not on south boundary of map array (gamma)
            //
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                if (_gameMap[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column] != null) // location exists
                {
                    GammaLocation = _gameMap[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];
                }
            }

            //
            // not on west boundary of map array (delta)
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                if (_gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1] != null) // location exists
                {
                    DeltaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];
                }
            }

            //
            // not on east boundary of map array (beta)
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                if (_gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1] != null) // location exists
                {
                    BetaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];
                }
            }
        }

        /// <summary>
        /// travel north (alpha)
        /// </summary>
        public void AlphaTravel()
        {
            if (HasAlphaLocation)
            {
                _currentLocationCoordinates.Row--;
                _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
            }
        }

        /// <summary>
        /// travel east (beta)
        /// </summary>
        public void BetaTravel()
        {
            if (HasBetaLocation)
            {
                _currentLocationCoordinates.Column++;
                _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
            }
        }

        /// <summary>
        /// travel south (gamma)
        /// </summary>
        public void GammaTravel()
        {
            if (HasGammaLocation)
            {
                _currentLocationCoordinates.Row++;
                _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
            }
        }

        /// <summary>
        /// travel west (delta)
        /// </summary>
        public void DeltaTravel()
        {
            if (HasDeltaLocation)
            {
                _currentLocationCoordinates.Column--;
                _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
            }
        }

        /// <summary>
        /// generates a sting of mission messages with time stamp with most current first
        /// </summary>
        /// <returns>string of formated mission messages</returns>
        private string FormatMessagesForViewer()
        {
            List<string> lifoMessages = new List<string>();

            for (int index = 0; index < _messages.Count; index++)
            {
                lifoMessages.Add($" <T:{GameTime().ToString(@"hh\:mm\:ss")}> " + _messages[index]);
            }

            lifoMessages.Reverse();

            return string.Join("\n\n", lifoMessages);
        }

        /// <summary>
        /// running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        #endregion

        #region EVENTS



        #endregion
    }

}
