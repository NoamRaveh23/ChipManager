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
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        TextView tvName, tvWin, tvGames;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile);
            tvName = (TextView)FindViewById(Resource.Id.name);
            tvWin = (TextView)FindViewById(Resource.Id.win);
            tvGames = (TextView)FindViewById(Resource.Id.games);
            // Create your application here
        }
    }
}