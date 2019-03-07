using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;
using WpfTheAionProject;
using System.Windows.Threading;

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
        private string _gameTimeDisplay;
        private TimeSpan _gameTime;

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
                OnPropertyChanged(nameof(CurrentLocation));
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
                OnPropertyChanged(nameof(AlphaLocation));
                OnPropertyChanged(nameof(HasAlphaLocation));
            }
        }

        public Location BetaLocation
        {
            get { return _betaLocation; }
            set
            {
                _betaLocation = value;
                OnPropertyChanged(nameof(BetaLocation));
                OnPropertyChanged(nameof(HasBetaLocation));
            }
        }

        public Location GammaLocation
        {
            get { return _gammaLocation; }
            set
            {
                _gammaLocation = value;
                OnPropertyChanged(nameof(GammaLocation));
                OnPropertyChanged(nameof(HasGammaLocation));
            }
        }

        public Location DeltaLocation
        {
            get { return _deltaLocation; }
            set
            {
                _deltaLocation = value;
                OnPropertyChanged(nameof(DeltaLocation));
                OnPropertyChanged(nameof(HasDeltaLocation));
            }
        }

        public bool HasAlphaLocation { get { return AlphaLocation != null; } }
        public bool HasBetaLocation { get { return BetaLocation != null; } }
        public bool HasGammaLocation { get { return GammaLocation != null; } }
        public bool HasDeltaLocation { get { return DeltaLocation != null; } }

        public string MissionTimeDisplay
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(MissionTimeDisplay));
            }
        }

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

            GameTimer();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// game time event, publishes every 1 second
        /// </summary>
        public void GameTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnGameTimerTick;
            timer.Start();
        }

        /// <summary>
        /// game timer event handler
        /// 1) update mission time on window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGameTimerTick(object sender, EventArgs e)
        {
            _gameTime = DateTime.Now - _gameStartTime;
            MissionTimeDisplay = "Mission Time " + _gameTime.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
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
                Location nextAlphaLocation = _gameMap[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column];
                if (nextAlphaLocation != null && 
                    (nextAlphaLocation.Accessible == true || nextAlphaLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints)))
                {
                    AlphaLocation = nextAlphaLocation;
                }
            }

            //
            // not on south boundary of map array (gamma)
            //
            if (_currentLocationCoordinates.Row < _maxRows - 1)
            {
                Location nextGammaLocation = _gameMap[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];
                if (nextGammaLocation != null &&
                    (nextGammaLocation.Accessible == true || nextGammaLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints)))
                {
                    GammaLocation = _gameMap[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];
                }
            }

            //
            // not on west boundary of map array (delta)
            //
            if (_currentLocationCoordinates.Column > 0)
            {
                Location nextDeltaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];
                if (nextDeltaLocation != null &&
                    (nextDeltaLocation.Accessible == true || nextDeltaLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints)))
                {
                    DeltaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];
                }
            }

            //
            // not on east boundary of map array (beta)
            //
            if (_currentLocationCoordinates.Column < _maxColumns - 1)
            {
                Location nextBetaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];
                if (nextBetaLocation != null &&
                    (nextBetaLocation.Accessible == true || nextBetaLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints)))
                {
                    BetaLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];
                }
            }
        }

        /// <summary>
        /// player move event handler
        /// </summary>
        private void OnPlayerMove()
        {
            if (!_player.HasVisited(_currentLocation))
            {
                _player.LocationsVisited.Add(_currentLocation);
                _player.ExperiencePoints += _currentLocation.ExperiencePointsValue;
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
                CurrentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
                OnPlayerMove();
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
                CurrentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
                OnPlayerMove();
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
                CurrentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
                OnPlayerMove();
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
                CurrentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
                UpdateAvailableTravelPoints();
                OnPlayerMove();
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
