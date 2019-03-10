using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.PresentationLayer
{
    /// <summary>
    /// view model for the game session view
    /// </summary>
    public class GameSessionViewModel
    {
        #region ENUMS



        #endregion

        #region FIELDS

        private Player _player;
        private List<string> _messages;
        private DateTime _gameStartTime;
        private Map _gameMap;
        private Location _currentLocation;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel(
            Player player,
            List<string> initialMessages,
            Map gameMap,
            GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;
            _messages = initialMessages;

            _gameMap = gameMap;
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap.CurrentLocation;
            InitializeView();
        }

        //public GameSessionViewModel(
        //    Player player,
        //    List<string> initialMessages)
        //{
        //    _player = player;
        //    _messages = initialMessages;
        //    InitializeView();
        //}

        #endregion

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
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



        /// <summary>
        /// travel north (alpha)
        /// </summary>
        public void AlphaTravel()
        {
                _gameMap.MoveAlpha();
                CurrentLocation = _gameMap.CurrentLocation;
        }

        /// <summary>
        /// travel east (beta)
        /// </summary>
        public void BetaTravel()
        {
                _gameMap.MoveBeta();
                CurrentLocation = _gameMap.CurrentLocation;
        }

        /// <summary>
        /// travel south (gamma)
        /// </summary>
        public void GammaTravel()
        {
                  _gameMap.MoveGamma();
                CurrentLocation = _gameMap.CurrentLocation;
        }

        /// <summary>
        /// travel west (delta)
        /// </summary>
        public void DeltaTravel()
        {
                _gameMap.MoveDelta();
                CurrentLocation = _gameMap.CurrentLocation;

        }
        #endregion

        #region EVENTS



        #endregion
    }
}
