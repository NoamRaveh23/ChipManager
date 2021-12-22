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
        List<Player> p;
        public static Player turnp;
        public static int bigBet;
        ListView lv;
        PlayerAdapter adapter;
        int counter = 0 , allMoney = 0 , pcount = 0;
        private int i = 0;
        Button op, ex, play;
        TextView small, big, t;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);
            lv = (ListView)FindViewById(Resource.Id.lv);
            op = (Button)FindViewById(Resource.Id.options);
            ex = (Button)FindViewById(Resource.Id.exit);
            play = (Button)FindViewById(Resource.Id.play);
            small = (TextView)FindViewById(Resource.Id.small);
            big = (TextView)FindViewById(Resource.Id.big);
            t = (TextView)FindViewById(Resource.Id.t);
            //op.Click += Op_Click;
            ex.Click += Ex_Click;
            play.Click += Play_Click;
            this.lp = StartGameActivity.lst;
            p = this.lp;
            turnp = p[0];
            adapter = new PlayerAdapter(this, lp);
            lv.Adapter = adapter;
            small.Text = p[0].getName();
            big.Text = p[1].getName();
            t.Text = p[0].getName();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (counter < 4)
            {
                turn();
                counter++;
            }
            else
            {
                endRound();
            }

        }

        private void endRound()
        {
            Dialog d = new Dialog(this);
            d.SetContentView(Resource.Layout.winner);
            d.SetTitle("Insert Winner Number");
            d.Show();
            EditText et = (EditText)d.FindViewById(Resource.Id.win);
            int winner = (Int32.Parse(et.Text));
            p[winner - 1].winMoney(allMoney);
            counter = 0;
            small.Text = p[pcount].getName();
            big.Text = p[pcount + 1].getName();
            t.Text = p[pcount].getName();

        }

        private void Ex_Click(object sender, EventArgs e)
        {
            
        }



        public void turn()
        {
            p = this.lp;

            if (i <= lp.Count)
            {
                turnp = p[this.i];
                Intent intent = new Intent(this, typeof(TurnActivity));
                StartActivityForResult(intent, 0);
            }
            else
            {
                
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0)
            {
                this.i++;
                allMoney += bigBet;
            }
        }
    }
}