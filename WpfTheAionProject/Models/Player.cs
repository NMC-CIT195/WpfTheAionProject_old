using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class Player : Character
    {
        #region ENUMS

        public enum JobTitleName { Explorer, MissionLeader, Supervisor }

        #endregion

        #region FIELDS

        private int _lives;
        private JobTitleName _jobTitle;

        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public JobTitleName JobTitle
        {
            get { return _jobTitle; }
            set { _jobTitle = value; }
        }

        #endregion

        #region CONSTRUCTORS



        #endregion

        #region METHODS

        /// <summary>
        /// override the default greeting in the Character class to include the job title
        /// set the proper article based on the job title
        /// </summary>
        /// <returns>default greeting</returns>
        public override string DefaultGreeting()
        {
            string article = "a";

            List<string> vowels = new List<string>() { "A", "E", "I", "O", "U" };

            if (vowels.Contains(_jobTitle.ToString().Substring(0, 1)));
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
