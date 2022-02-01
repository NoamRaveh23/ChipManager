using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChipManager
{
    class Game:Player
    {
        
        protected List<Player> game;
        public int code { get; set; }
        public int id { get; set; }
        public Game(List<Player> game)
        {
            this.game = game;
            Random rnd = new Random();
            this.code = rnd.Next(999, 99999);
        }
        public Game()
        {
            this.game = null;
        }
    }
}