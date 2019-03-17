using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;
using WpfTheAionProject;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using WpfTheAionProject.DataLayer;

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

        private Map _gameMap;
        private Location _currentLocation;
        //private Location _northLocation, _eastLocation, _southLocation, _westLocation;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public List<string> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(MessageDisplay));
            }
        }

        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }
        public Map GameMap
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
        //public Location NorthLocation
        //{
        //    get { return _northLocation; }
        //    set
        //    {
        //        _northLocation = value;
        //        OnPropertyChanged(nameof(NorthLocation));
        //        OnPropertyChanged(nameof(HasNorthLocation));
        //    }
        //}

        //public Location EastLocation
        //{
        //    get { return _eastLocation; }
        //    set
        //    {
        //        _eastLocation = value;
        //        OnPropertyChanged(nameof(EastLocation));
        //        OnPropertyChanged(nameof(HasEastLocation));
        //    }
        //}

        //public Location SouthLocation
        //{
        //    get { return _southLocation; }
        //    set
        //    {
        //        _southLocation = value;
        //        OnPropertyChanged(nameof(SouthLocation));
        //        OnPropertyChanged(nameof(HasSouthLocation));
        //    }
        //}

        //public Location WestLocation
        //{
        //    get { return _westLocation; }
        //    set
        //    {
        //        _westLocation = value;
        //        OnPropertyChanged(nameof(WestLocation));
        //        OnPropertyChanged(nameof(HasWestLocation));
        //    }
        //}

        //public bool HasNorthLocation { get { return NorthLocation != null; } }
        //public bool HasEastLocation { get { return EastLocation != null; } }
        //public bool HasSouthLocation { get { return SouthLocation != null; } }
        //public bool HasWestLocation { get { return WestLocation != null; } }

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
            Map gameMap)
        {
            _player = player;
            _messages = initialMessages;
            _gameMap = gameMap;
            _currentLocation = _gameMap.CurrentLocation;
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
        }

        /// <summary>
        /// return a unique empty location object
        /// </summary>
        /// <returns>empty location object</returns>
        //public Location EmptyLocation()
        //{
        //    return new Location()
        //    {
        //        Id = 0,
        //        Name = " *** No Available Slipstream ***",
        //        Description = "This channel does not currently have an available slipstream from this access point. Please choose another slipstream.",
        //        Accessible = true
        //    };
        //}

        /// <summary>
        /// calculate the available travel points from current location
        /// game slipstreams are a mapping against the 2D array where 
        /// </summary>
        //private void UpdateAvailableTravelPoints()
        //{
        //    //
        //    // reset travel location information
        //    //
        //    NorthLocation = null;
        //    EastLocation = null;
        //    SouthLocation = null;
        //    WestLocation = null;

        //    if (_gameMap.NorthLocation(_player) != null)
        //    {
        //        NorthLocation = _gameMap.NorthLocation(_player);
        //    }

        //    if (_gameMap.EastLocation(_player) != null)
        //    {
        //        EastLocation = _gameMap.EastLocation(_player);
        //    }

        //    if (_gameMap.SouthLocation(_player) != null)
        //    {
        //        SouthLocation = _gameMap.SouthLocation(_player);
        //    }

        //    if (_gameMap.WestLocation(_player) != null)
        //    {
        //        WestLocation = _gameMap.WestLocation(_player);
        //    }      
        //}

        /// <summary>
        /// player move event handler
        /// </summary>
        private void OnPlayerMove()
        {
            //
            // update player stats
            //
            if (!_player.HasVisited(_currentLocation))
            {
                _player.LocationsVisited.Add(_currentLocation);
                _player.ExperiencePoints += _currentLocation.ModifiyExperiencePoints;
            }

            if (_currentLocation.ModifyHealth != 0)
            {
                _player.Health += _currentLocation.ModifyHealth;
                if (_player.Health > 100)
                {
                    _player.Health = 100;
                    _player.Lives++;
                }
            }

            if (_currentLocation.ModifyLives != 0) _player.Lives += _currentLocation.ModifyLives;

            //
            // display a new message if available
            //
            if (_currentLocation.Message != null) Messages.Add(_currentLocation.Message);
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
