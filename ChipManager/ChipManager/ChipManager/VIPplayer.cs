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
    class VIPplayer : Player
    {
        public VIPplayer(string name, string gender, int money, Bitmap bit) : base(name, gender, money, bit) 
        {
            type = playerType.VIPplayer;
        }
        public VIPplayer(string name, string gender, int money)
        {
            type = playerType.VIPplayer;
        }
        public void addBonus()
        {
            bonus += 200;
        }
        
    }
}