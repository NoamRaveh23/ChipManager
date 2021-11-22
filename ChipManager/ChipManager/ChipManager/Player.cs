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
    class Player
    {
        private string name;
        private int money;
        private int bet;
        private string gender;
        public Player(string name,string gender , int money , int bet)
        {
            this.name = name;
            this.money = money;
            this.bet = bet;
            this.gender = gender;
        }
        public string getName()
        {
            return this.name;
        }
        public string getGender()
        {
            return this.gender;
        }
        public int getMoney()
        {
            return this.money;
        }
        public int getBet()
        {
            return this.bet;
        }
    }
}