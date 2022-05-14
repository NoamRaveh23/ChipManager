using Android.App;
using Android.Content;
using Android.Graphics;
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
    class Player : IPlayer
    {
        private string name;
        private int money;
        private int bet;
        private string gender;
        private bool elim = false, allIn = false, imgcheck = false;
        public bool isCheck = false;
        private Bitmap bit;
        protected int bonus;
        protected playerType type;
        public Player(string name, string gender, int money, Bitmap bit)
        {
            type = playerType.Player;
            this.name = name;
            this.money = money;
            this.bet = 0;
            this.gender = gender;
            this.elim = false;
            this.bit = bit;
            this.bonus = 0;
            this.imgcheck = true;
            this.isCheck = false;
        }
        public Player(string name, string gender, int money)
        {
            type = playerType.Player;
            this.name = name;
            this.money = money;
            this.bet = 0;
            this.gender = gender;
            this.elim = false;
            this.imgcheck = false;
            this.isCheck = false;
        }
        public Player()
        {
            type = playerType.Player;
            this.name = "";
            this.money = 0;
            this.bet = 0;
            this.gender = "boy";
            this.elim = false;
            this.imgcheck = false;
            this.isCheck = false;
        }

        public playerType getType()
        {
            return type;
        }


        public void addBonus()
        {
            //ddd
            bonus += 100;
        }

        public int getBonus()
        {
            return bonus;
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
        public bool getElim()
        {
            return this.elim;
        }
        public void setBet(int newbet)
        {
            this.bet = newbet;
        }
        public void setMoney(int cost)
        {
            this.money -= cost;
        }
        public void winMoney(int m)
        {
            this.money += m;
        }
        public void setElim(bool b)
        {
            this.elim = b;
            if (b)
            {
                if (this.gender.Equals("girl"))
                {
                    this.gender = "elimGirl";
                }
                else if (this.gender.Equals("boy"))
                {
                    this.gender = "elimBoy";
                }
            }
        }
        public void setAllIn(bool b)
        {
            this.allIn =b;
        }
        public bool getAllIn()
        {
            return this.allIn;
        }
        public void setPBit(Bitmap bit)
        {
            this.bit = bit;
        }
        public Bitmap getBit()
        {
            return this.bit;
        }
        public bool ifPImage()
        {
            return this.imgcheck;
        }
        public void setChecked(bool x)
        {
            isCheck = x;
        }

        public bool getChecked()
        {
            return this.isCheck;
        }
    }

}