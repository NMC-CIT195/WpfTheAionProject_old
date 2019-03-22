using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTheAionProject.Models
{
    public class GameItemQuantity
    {
        public int GameItemId { get; set; }
        public int Quantity { get; set; }

        public GameItemQuantity(int gameItemId, int quantity)
        {
            GameItemId = gameItemId;
            Quantity = quantity;
        }
    }
}
