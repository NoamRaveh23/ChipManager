using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChipManager
{
    enum playerType
    {
        IPlayer     = 0,
        Player      = 1,
        VIPplayer   = 2,
        other       = 3
    }

    interface IPlayer
    {
        public playerType getType();
        
        public abstract void addBonus();
        public abstract int getBonus();
        public string getName();
        public string getGender();
        public int getMoney();
        public int getBet();
        public bool getElim();
        public void setBet(int newbet);
        public void setMoney(int cost);
        public void winMoney(int m);
        public void setElim(bool b);
        public void setAllIn(bool b);
        public bool getAllIn();
        public void setPBit(Bitmap bit);
        public Bitmap getBit();

        public bool ifPImage();

        public void setChecked(bool x);
        public bool getChecked();


    }
}