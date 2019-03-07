using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// class for the game map locations
    /// </summary>
    public class Location
    {
        #region ENUMS

 
        #endregion

        #region FIELDS

        private int _id; // must be a unique value for each object
        private string _name;
        private string _description;
        private bool _accessible;
        private int _requiredExperiencePoints;
        private int _experiencePointsValue;

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

        public int ExperiencePointsValue
        {
            get { return _experiencePointsValue; }
            set { _experiencePointsValue = value; }
        }
        
        public int RequiredExperiencePoints
        {
            get { return _requiredExperiencePoints; }
            set { _requiredExperiencePoints = value; }
        }

        #endregion


        #region CONSTRUCTORS



        #endregion


        #region METHODS

        public bool IsAccessibleByExperiencePoints(int playerExperiencePoints)
        {
            return playerExperiencePoints >= _requiredExperiencePoints ? true : false;
        }

        #endregion
    }
}