using System;
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
        private JobTitleName _jobTitle;
        private List<Location> _locationsVisited;
        private ObservableCollection<GameItemQuantity> _inventory;
        private ObservableCollection<GameItemQuantity> _potions;
        private ObservableCollection<GameItemQuantity> _treasure;
        private ObservableCollection<GameItemQuantity> _weapons;

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

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        public ObservableCollection<GameItemQuantity> Inventory
        {
            get { return _inventory; }
            set
            {
                _inventory = value;
                UpdateInventoryCategories();
            }
        }

        public ObservableCollection<GameItemQuantity> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }

        public ObservableCollection<GameItemQuantity> Potions
        {
            get { return _potions; }
            set { _potions = value; }
        }

        public ObservableCollection<GameItemQuantity> Treasure
        {
            get { return _treasure; }
            set { _treasure = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<Location>();
            _weapons = new ObservableCollection<GameItemQuantity>();
            _treasure = new ObservableCollection<GameItemQuantity>();
            _potions = new ObservableCollection<GameItemQuantity>();
        }

        #endregion

        #region METHODS

        public void UpdateInventoryCategories()
        {
            Potions.Clear();
            Weapons.Clear();
            Treasure.Clear();

            foreach (var item in _inventory)
            {
                if (item.GameItem is Potion) Potions.Add(item);
                if (item.GameItem is Weapon) Weapons.Add(item);
                if (item.GameItem is Treasure) Treasure.Add(item);
            }
        }

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
