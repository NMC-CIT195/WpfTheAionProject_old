using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    /// <summary>
    /// game map class
    /// </summary>
    public class Map
    {
        #region FIELDS

        private Location[,] _mapLocations;

        #endregion


        #region PROPERTIES

        public Location[,] MapLocations
        {
            get { return _mapLocations; }
            set { _mapLocations = value; }
        }

        #endregion


        #region CONSTRUCTORS

        public Map(int rows, int columns)
        {
            _mapLocations = new Location[rows, columns];
        }

        #endregion


        #region METHODS



        #endregion
    }
}
