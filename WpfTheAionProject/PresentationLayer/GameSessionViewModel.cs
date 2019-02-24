using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.PresentationLayer
{
    public class GameSessionViewModel
    {
        #region ENUMS



        #endregion

        #region FIELDS

        private Player _player;
        private List<string> _messages;
        private Location[,] _gameMap;
        private GameMapCoordinates _currentLocationCoordinates;
        private Location _currentLocation;




        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return string.Join("\n\n", _messages); }
        }

        public Location[,] GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
        }

        public string CurrentLocationName
        {
            get { return _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column].Name; }
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
            _currentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column];
            InitializeMainPanel();
        }

        private void InitializeMainPanel()
        {

        }

        #endregion

        #region METHODS



        #endregion

        #region EVENTS



        #endregion
    }

}
