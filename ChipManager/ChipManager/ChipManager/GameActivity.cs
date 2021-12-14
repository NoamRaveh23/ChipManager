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
        private List<Player> lp;
        ListView lv;
        PlayerAdapter adapter;
        int counter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);
            lv = (ListView)FindViewById(Resource.Id.lv);
            this.lp = StartGameActivity.lst;
            adapter = new PlayerAdapter(this, lp);
            lv.Adapter = adapter;
        }
        public void turn()
        {
            int i = 0;
            List<Player> p  = this.lp;
            List<Player> first = this.lp;
            while (i<lp.Count)
            {
                Intent intent = new Intent(this, typeof(TurnActivity));
                intent.PutExtras(p);
                StartActivity(intent);
            }
        }
    }
}