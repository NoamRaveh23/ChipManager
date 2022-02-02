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

        protected List<Player> gList;
        public int code { get; set; }
        public int id { get; set; }

        public int turn { get; set; }
        public Game(List<Player> game)
        {
            this.gList = game;
            Random rnd = new Random();
            this.code = rnd.Next(999, 99999);
            turn = 0;
        }
        public Game()
        {
            this.gList = null;
            turn = 0;
        }
        public void setGList(List<Player> lst)
        {
            this.gList = lst;
        }
        public List<Player> getGList()
        {
            return this.gList;
        }
        /*public async void SetGameAsync(int code)
        {
            this.game = await FirebaseHelper.Get(code);
        }*/
    }
}