﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// player class
    /// </summary>
    public class Player : Character
    {
        #region ENUMS

        public enum JobTitleName { Explorer, MissionLeader, Supervisor }

        #endregion

        #region FIELDS

        private int _lives;
        private int _health;
        private int _experiencePoints;
        private int _wealth;
        private JobTitleName _jobTitle;
        private List<Location> _locationsVisited;

        // todo 01 Player: update player fields
        //private ObservableCollection<GameItem> _inventory;
        //private ObservableCollection<GameItem> _potions;
        //private ObservableCollection<GameItem> _treasure;
        //private ObservableCollection<GameItem> _weapons;
        //private ObservableCollection<GameItem> _relics;

        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set
            {
                _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }

        public JobTitleName JobTitle
        {
            get { return _jobTitle; }
            set
            {
                _jobTitle = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (_health > 100)
                {
                    _health = 100;
                }
                else if (_health <= 0)
                {
                    _health = 100;
                    _lives--;
                }

                OnPropertyChanged(nameof(Health));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }

        public int Wealth
        {
            get { return _wealth; }
            set
            {
                _wealth = value;
                OnPropertyChanged(nameof(Wealth));
            }
        }

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        // todo 02 Player: update player properties
        //public ObservableCollection<GameItem> Inventory
        //{
        //    get { return _inventory; }
        //    set { _inventory = value; }
        //}

        //public ObservableCollection<GameItem> Weapons
        //{
        //    get { return _weapons; }
        //    set { _weapons = value; }
        //}

        //public ObservableCollection<GameItem> Potions
        //{
        //    get { return _potions; }
        //    set { _potions = value; }
        //}

        //public ObservableCollection<GameItem> Treasure
        //{
        //    get { return _treasure; }
        //    set { _treasure = value; }
        //}

        //public ObservableCollection<GameItem> Relics
        //{
        //    get { return _relics; }
        //    set { _relics = value; }
        //}

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            // todo 03 Player: instantiate observable collections in constructor
            _locationsVisited = new List<Location>();
            //_weapons = new ObservableCollection<GameItem>();
            //_treasure = new ObservableCollection<GameItem>();
            //_potions = new ObservableCollection<GameItem>();
            //_relics = new ObservableCollection<GameItem>();
        }

        #endregion

        #region METHODS

        // todo 05 Player: method to initialize players wealth
        //
        // set the players wealth based on the initial inventory
        //
        //public void CalculateWealth()
        //{
        //    Wealth = _inventory.Sum(i => i.Value);
        //}

        // todo 04 Player: method to update the game item observable collections
        /// <summary>
        /// update the game item category lists
        /// </summary>
        //public void UpdateInventoryCategories()
        //{
        //    Potions.Clear();
        //    Weapons.Clear();
        //    Treasure.Clear();
        //    Relics.Clear();

        //    foreach (var gameItem in _inventory)
        //    {
        //        if (gameItem is Potion) Potions.Add(gameItem);
        //        if (gameItem is Weapon) Weapons.Add(gameItem);
        //        if (gameItem is Treasure) Treasure.Add(gameItem);
        //        if (gameItem is Relic) Relics.Add(gameItem);
        //    }
        //}

        // todo 06 Player: method to add a game item to inventory
        /// <summary>
        /// remove selected item from inventory
        /// </summary>
        /// <param name="selectedGameItem">selected item</param>
        //public void AddGameItemToInventory(GameItem selectedGameItem)
        //{
        //    if (selectedGameItem != null)
        //    {
        //        _inventory.Add(selectedGameItem);
        //    }
        //}

        // todo 07 Player: method to add a game item to inventory
        /// <summary>
        /// remove selected item from inventory
        /// </summary>
        /// <param name="selectedGameItem">selected item</param>
        //public void RemoveGameItemFromInventory(GameItem selectedGameItem)
        //{
        //    if (selectedGameItem != null)
        //    {
        //        _inventory.Remove(selectedGameItem);
        //    }
        //}

        /// <summary>
        /// determine if this is a old location
        /// </summary>
        /// <param name="location">old location</param>
        /// <returns></returns>
        public bool HasVisited(Location location)
        {
            return _locationsVisited.Contains(location);
        }

        /// <summary>
        /// override the default greeting in the Character class to include the job title
        /// set the proper article based on the job title
        /// </summary>
        /// <returns>default greeting</returns>
        public override string DefaultGreeting()
        {
            string article = "a";

            List<string> vowels = new List<string>() { "A", "E", "I", "O", "U" };

            if (vowels.Contains(_jobTitle.ToString().Substring(0, 1))) ;
            {
                article = "an";
            }

            return $"Hello, my name is {_name} and I am {article} {_jobTitle} for the Aion Project.";
        }

        #endregion

        #region EVENTS



        #endregion

    }
}
