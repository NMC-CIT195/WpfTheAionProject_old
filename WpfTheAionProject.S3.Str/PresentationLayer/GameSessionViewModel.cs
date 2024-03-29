﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.Models;
using WpfTheAionProject;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;

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

        private Map _gameMap;
        private Location _currentLocation;
        private Location _northLocation, _eastLocation, _southLocation, _westLocation;
        private string _currentLocationInformation;

        // todo 25 GameSessionViewModel: add field for the current game item
        //private GameItem _currentGameItem;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return _currentLocation.Message; }
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
                _currentLocationInformation = _currentLocation.Description;
                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }

        //
        // expose information about travel points from current location
        //
        public Location NorthLocation
        {
            get { return _northLocation; }
            set
            {
                _northLocation = value;
                OnPropertyChanged(nameof(NorthLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
            }
        }

        public Location EastLocation
        {
            get { return _eastLocation; }
            set
            {
                _eastLocation = value;
                OnPropertyChanged(nameof(EastLocation));
                OnPropertyChanged(nameof(HasEastLocation));
            }
        }

        public Location SouthLocation
        {
            get { return _southLocation; }
            set
            {
                _southLocation = value;
                OnPropertyChanged(nameof(SouthLocation));
                OnPropertyChanged(nameof(HasSouthLocation));
            }
        }

        public Location WestLocation
        {
            get { return _westLocation; }
            set
            {
                _westLocation = value;
                OnPropertyChanged(nameof(WestLocation));
                OnPropertyChanged(nameof(HasWestLocation));
            }
        }


        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }

        public bool HasNorthLocation
        {
            get
            {
                if (NorthLocation != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //
        // shortened code with same functionality as above
        //
        public bool HasEastLocation { get { return EastLocation != null; } }
        public bool HasSouthLocation { get { return SouthLocation != null; } }
        public bool HasWestLocation { get { return WestLocation != null; } }

        public string MissionTimeDisplay
        {
            get { return _gameTimeDisplay; }
            set
            {
                _gameTimeDisplay = value;
                OnPropertyChanged(nameof(MissionTimeDisplay));
            }
        }

        // todo 26 GameSessionViewModel: add property for the current game item
        //public GameItem CurrentGameItem
        //{
        //    get { return _currentGameItem; }
        //    set { _currentGameItem = value; }
        //}

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(
            Player player,
            Map gameMap,
            GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;

            _gameMap = gameMap;
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap.CurrentLocation;
            InitializeView();

            GameTimer();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAvailableTravelPoints();
            _currentLocationInformation = CurrentLocation.Description;

            // todo 27 GameSessionViewModel: update the InitializeView method
            //_player.UpdateInventoryCategories();
            //_player.InitializeWealth();
        }

        #region MOVEMENT METHODS

        /// <summary>
        /// calculate the available travel points from current location
        /// game slipstreams are a mapping against the 2D array where 
        /// </summary>
        private void UpdateAvailableTravelPoints()
        {
            //
            // reset travel location information
            //
            NorthLocation = null;
            EastLocation = null;
            SouthLocation = null;
            WestLocation = null;

            //
            // north location exists
            //
            if (_gameMap.NorthLocation() != null)
            {
                Location nextNorthLocation = _gameMap.NorthLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextNorthLocation.Accessible == true || PlayerCanAccessLocation(nextNorthLocation))
                {
                    NorthLocation = nextNorthLocation;
                }
            }

            //
            // east location exists
            //
            if (_gameMap.EastLocation() != null)
            {
                Location nextEastLocation = _gameMap.EastLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextEastLocation.Accessible == true || PlayerCanAccessLocation(nextEastLocation))
                {
                    EastLocation = nextEastLocation;
                }
            }

            //
            // south location exists
            //
            if (_gameMap.SouthLocation() != null)
            {
                Location nextSouthLocation = _gameMap.SouthLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextSouthLocation.Accessible == true || PlayerCanAccessLocation(nextSouthLocation))
                {
                    SouthLocation = nextSouthLocation;
                }
            }

            //
            // west location exists
            //
            if (_gameMap.WestLocation() != null)
            {
                Location nextWestLocation = _gameMap.WestLocation();

                //
                // location generally accessible or player has required conditions
                //
                if (nextWestLocation.Accessible == true || PlayerCanAccessLocation(nextWestLocation))
                {
                    WestLocation = nextWestLocation;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextLocation">location to check accessibility</param>
        /// <returns>accessibility</returns>
        private bool PlayerCanAccessLocation(Location nextLocation)
        {
            //
            // check access by experience points
            //
            if (nextLocation.IsAccessibleByExperiencePoints(_player.ExperiencePoints))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// player move event handler
        /// </summary>
        private void OnPlayerMove()
        {
            //
            // update player stats when in new location
            //
            if (!_player.HasVisited(_currentLocation))
            {
                //
                // add location to list of visited locations
                //
                _player.LocationsVisited.Add(_currentLocation);

                // 
                // update experience points
                //
                _player.ExperiencePoints += _currentLocation.ModifiyExperiencePoints;

                //
                // update health
                //
                _player.Health += _currentLocation.ModifyHealth;

                //
                // update lives
                //
                _player.Lives += _currentLocation.ModifyLives;

                //
                // display a new message if available
                //
                OnPropertyChanged(nameof(MessageDisplay));
            }
        }

        /// <summary>
        /// travel north
        /// </summary>
        public void MoveNorth()
        {
            if (HasNorthLocation)
            {
                _gameMap.MoveNorth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// travel east
        /// </summary>
        public void MoveEast()
        {
            if (HasEastLocation)
            {
                _gameMap.MoveEast();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// travel south
        /// </summary>
        public void MoveSouth()
        {
            if (HasSouthLocation)
            {
                _gameMap.MoveSouth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        /// <summary>
        /// travel west
        /// </summary>
        public void MoveWest()
        {
            if (HasWestLocation)
            {
                _gameMap.MoveWest();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        #endregion

        #region GAME TIME METHODS

        /// <summary>
        /// running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

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

        #endregion

        #region ACTION METHODS

        // todo 28 GameSessionViewModel: add method to add a game item to inventory
        /// <summary>
        /// add a new item to the players inventory
        /// </summary>
        /// <param name="selectedItem"></param>
        //public void AddItemToInventory()
        //{
        //    //
        //    // confirm a game item selected and is in current location
        //    // subtract from location and add to inventory
        //    //
        //    if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
        //    {
        //        //
        //        // cast selected game item 
        //        //
        //        GameItem selectedGameItem = _currentGameItem as GameItem;

        //        _currentLocation.RemoveGameItemFromLocation(selectedGameItem);
        //        _player.AddGameItemToInventory(selectedGameItem);

        //        OnPlayerPickUp(selectedGameItem);
        //    }
        //}

        // todo 29 GameSessionViewModel: add method to remove a game item to inventory
        /// <summary>
        /// remove item from the players inventory
        /// </summary>
        /// <param name="selectedItem"></param>
        //public void RemoveItemFromInventory()
        //{
        //    //
        //    // confirm a game item selected and is in inventory
        //    // subtract from inventory and add to location
        //    //
        //    if (_currentGameItem != null)
        //    {
        //        //
        //        // cast selected game item 
        //        //
        //        GameItem selectedGameItem = _currentGameItem as GameItem;

        //        _currentLocation.AddGameItemToLocation(selectedGameItem);
        //        _player.RemoveGameItemFromInventory(selectedGameItem);

        //        OnPlayerPutDown(selectedGameItem);
        //    }
        //}

        // todo 30 GameSessionViewModel: add methods to handle the pickup and put down events
        /// <summary>
        /// process events when a player picks up a new game item
        /// </summary>
        /// <param name="gameItem">new game item</param>
        //private void OnPlayerPickUp(GameItem gameItem)
        //{
        //    _player.ExperiencePoints += gameItem.ExperiencePoints;
        //    _player.Wealth += gameItem.Value;
        //}

        ///// <summary>
        ///// process events when a player puts down a new game item
        ///// </summary>
        ///// <param name="gameItem">new game item</param>
        //private void OnPlayerPutDown(GameItem gameItem)
        //{
        //    _player.Wealth -= gameItem.Value;
        //}

        // todo 31 GameSessionViewModel: add a method to handle the game item use event
        /// <summary>
        /// process using an item in the player's inventory
        /// </summary>
        //public void OnUseGameItem()
        //{
        //    switch (_currentGameItem)
        //    {
        //        case Potion potion:
        //            ProcessPotionUse(potion);
        //            break;
        //        case Relic relic:
        //            ProcessRelicUse(relic);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        // todo 32 GameSessionViewModel: add methods to handle the game item use event for each game item class
        /// <summary>
        /// process the effects of using the relic
        /// </summary>
        /// <param name="potion">potion</param>
        //private void ProcessRelicUse(Relic relic)
        //{
        //    string message;

        //    switch (relic.UseAction)
        //    {
        //        case Relic.UseActionType.OPENLOCATION:
        //            message = _gameMap.OpenLocationsByRelic(relic.Id);
        //            CurrentLocationInformation = relic.UseMessage;
        //            break;
        //        case Relic.UseActionType.KILLPLAYER:
        //            PlayerDies(relic.UseMessage);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// process the effects of using the potion
        /// </summary>
        /// <param name="potion">potion</param>
        //private void ProcessPotionUse(Potion potion)
        //{
        //    _player.Health += potion.HealthChange;
        //    _player.Lives += potion.LivesChange;
        //    _player.RemoveGameItemFromInventory(_currentGameItem);
        //}

        // todo 33 GameSessionViewModel: add method to handle the player dies event
        /// <summary>
        /// process player dies with option to reset and play again
        /// </summary>
        /// <param name="message">message regarding player death</param>
        //private void OnPlayerDies(string message)
        //{
        //    string messagetext = message +
        //        "\n\nWould you like to play again?";

        //    string titleText = "Death";
        //    MessageBoxButton button = MessageBoxButton.YesNo;
        //    MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);

        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            ResetPlayer();
        //            break;
        //        case MessageBoxResult.No:
        //            QuiteApplication();
        //            break;
        //    }
        //}

        /// <summary>
        /// player chooses to exit game
        /// </summary>
        private void QuiteApplication()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// player chooses to reset game
        /// </summary>
        private void ResetPlayer()
        {
            Environment.Exit(0);
        }

        #endregion

        #endregion

        #region EVENTS



        #endregion
    }

}
