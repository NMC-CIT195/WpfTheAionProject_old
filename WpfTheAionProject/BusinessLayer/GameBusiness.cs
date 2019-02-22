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
            //
            // instantiate and open the player setup window
            //
            Player player = new Player();
            PlayerSetupView playerSetupView = new PlayerSetupView(player);
            playerSetupView.ShowDialog();

            //
            // instantiate the view model and initialize the data set
            //
            _gameSessionViewModel = new GameSessionViewModel(
                GameData.PlayerData(),
                GameData.InitialMessages(),
                GameData.GameMap(),
                GameData.InitialGameMapLocation()
                );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();
        }
    }
}
