using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// class for the game map locations
    /// </summary>
    public class Location : ObservableObject
    {
        #region ENUMS


        #endregion

        #region FIELDS

        private int _id; // must be a unique value for each object
        private string _name;
        private string _description;
        private bool _accessible;
        private int _requiredExperiencePoints;

        // todo 13 Location: add field for the required game item to open the location
        //private int _requiredRelicId;
        private int _modifiyExperiencePoints;
        private int _modifyHealth;
        private int _modifyLives;
        private string _message;

        // todo 11 Location: add field for the observable collection of game items
        //private ObservableCollection<GameItem> _gameItems;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Accessible
        {
            get { return _accessible; }
            set { _accessible = value; }
        }

        public int ModifiyExperiencePoints
        {
            get { return _modifiyExperiencePoints; }
            set { _modifiyExperiencePoints = value; }
        }

        public int RequiredExperiencePoints
        {
            get { return _requiredExperiencePoints; }
            set { _requiredExperiencePoints = value; }
        }

        // todo 14 Location: add property for the required game item to open the location
        //public int RequiredRelicId
        //{
        //    get { return _requiredRelicId; }
        //    set { _requiredRelicId = value; }
        //}

        public int ModifyHealth
        {
            get { return _modifyHealth; }
            set { _modifyHealth = value; }
        }

        public int ModifyLives
        {
            get { return _modifyLives; }
            set { _modifyLives = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        // todo 12 Location: add property for the observable collection of game items
        //public ObservableCollection<GameItem> GameItems
        //{
        //    get { return _gameItems; }
        //    set { _gameItems = value; }
        //}

        #endregion

        #region CONSTRUCTORS

        public Location()
        {
            // todo 15 Location: instantiate the observable collection of game items
            //_gameItems = new ObservableCollection<GameItem>();
        }

        #endregion

        #region METHODS

        //
        // location is open if character has enough XP
        //
        public bool IsAccessibleByExperiencePoints(int playerExperiencePoints)
        {
            return playerExperiencePoints >= _requiredExperiencePoints ? true : false;
        }

        //
        // Stopgap to force the list of items in the location to update
        //
        // todo Velis refactor using the CollectionChanged event

        // todo 16 Location: method to update the observable collection of game items
        /// <summary>
        /// update the observable collection of game items
        /// </summary>
        //public void UpdateLocationGameItems()
        //{
        //    ObservableCollection<GameItem> updatedLocationGameItems = new ObservableCollection<GameItem>();

        //    foreach (GameItem GameItem in _gameItems)
        //    {
        //        updatedLocationGameItems.Add(GameItem);
        //    }

        //    GameItems.Clear();

        //    foreach (GameItem gameItem in updatedLocationGameItems)
        //    {
        //        GameItems.Add(gameItem);
        //    }
        //}

        // todo 17 Location: method to add a game item to the location
        /// <summary>
        /// add selected item to location
        /// </summary>
        /// <param name="selectedGameItem">selected item</param>
        //public void AddGameItemToLocation(GameItem selectedGameItem)
        //{
        //    if (selectedGameItem != null)
        //    {
        //        _gameItems.Add(selectedGameItem);
        //    }

        //    UpdateLocationGameItems();
        //}

        // todo 18 Location: method to remove a game item to the location
        /// <summary>
        /// remove selected item from location
        /// </summary>
        /// <param name="selectedGameItem">selected item</param>
        //public void RemoveGameItemFromLocation(GameItem selectedGameItem)
        //{
        //    if (selectedGameItem != null)
        //    {
        //        _gameItems.Remove(selectedGameItem);
        //    }

        //    UpdateLocationGameItems();
        //}

        #endregion
    }
}