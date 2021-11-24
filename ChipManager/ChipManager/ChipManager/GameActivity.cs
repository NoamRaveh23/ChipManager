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
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity 
    {
        ListView lv;
        PlayerAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);
            lv = StartGameActivity.lst;
            lv.Adapter = adapter;
            adapter = new PlayerAdapter(this, lst);
            
        }
    }
}