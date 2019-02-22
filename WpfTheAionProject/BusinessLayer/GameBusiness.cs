using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheAionProject.PresentationLayer;
using WpfTheAionProject.DataLayer;
using WpfTheAionProject.Models;

namespace WpfTheAionProject.BusinessLayer
{
    public class GameBusiness
    {
        GameSessionViewModel _gameSessionViewModel;

        public GameBusiness()
        {
            bool newPlayer = true;
            Player player = new Player();
            PlayerSetupView playerSetupView = null;

            if (newPlayer)
            {
                playerSetupView = new PlayerSetupView(player);
                playerSetupView.ShowDialog();
            }
            else
            {
                player = GameData.PlayerData();
            }

            //
            // instantiate the view model and initialize the data set
            //
            _gameSessionViewModel = new GameSessionViewModel(
                player,
                GameData.InitialMessages(),
                GameData.GameMap(),
                GameData.InitialGameMapLocation()
                );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();

            //
            // dialog window is initially hidden to mitigate issue with
            // main window closing after dialog window closes
            //
            playerSetupView.Close();
        }
    }
}
