using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChipManager
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : AppCompatActivity
    {
        private List<Player> lp;
        EditText et;
        Dialog d;
        List<Player> p;
        public static Player turnp;
        public static int bigBet;
        ListView lv;
        PlayerAdapter adapter;
        int counter = 0 , allMoney = 0 , pcount = 1;
        private int i = 0;
        Button  ex, play;
        TextView small, big, t;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);
            lv = (ListView)FindViewById(Resource.Id.lv);            
            ex = (Button)FindViewById(Resource.Id.exit);
            play = (Button)FindViewById(Resource.Id.play);
            small = (TextView)FindViewById(Resource.Id.small);
            big = (TextView)FindViewById(Resource.Id.big);
            t = (TextView)FindViewById(Resource.Id.t);            
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
            turn();                           
        }

        private void endRound()
        {
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.winner);
            et = (EditText)d.FindViewById(Resource.Id.win);
            Button s = (Button)d.FindViewById(Resource.Id.s);
            s.Click += S_Click;
            d.SetTitle("Insert Winner Number");
            d.SetCancelable(true);
            d.Show();
            
            /*int winner = (Int32.Parse(et.Text));
            p[ 1].winMoney(allMoney);
            counter = 0;
            small.Text = p[pcount].getName();
            big.Text = p[pcount + 1].getName();
            t.Text = p[pcount].getName();
            pcount++;*/

        }

        private void S_Click(object sender, EventArgs e)
        {
            d.Dismiss();
            int winner = (Int32.Parse(et.Text));
            p[winner - 1].winMoney(allMoney);
            counter = 0;
            ISharedPreferences sp = GetSharedPreferences("lastWin", FileCreationMode.Private);
            ISharedPreferencesEditor editor = sp.Edit();
            editor.PutString("lastWin", p[winner - 1].getName());
            editor.Commit();
            /*small.Text = p[pcount].getName();
            big.Text = p[pcount + 1].getName();
            t.Text = p[pcount].getName();
            pcount++;*/
        }

        private void Ex_Click(object sender, EventArgs e)
        {
            
        }



        public void turn()
        {
            p = this.lp;

            if (i < lp.Count && counter < 3)
            {
                turnp = p[this.i];
                //t.Text = turnp.getName();
                Intent intent = new Intent(this, typeof(TurnActivity));
                StartActivityForResult(intent, 0);
            }
            else if (i >= lp.Count && counter < 3)
            {
                this.i = 0;
                counter++;
                turnp = p[this.i];
                t.Text = turnp.getName();
                Intent intent = new Intent(this, typeof(TurnActivity));
                StartActivityForResult(intent, 0);
            }
            else
            {
                endRound();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (adapter != null)
            {
                adapter.NotifyDataSetChanged();
                t.Text = turnp.getName();
            }
            if (requestCode == 0)
            {
                this.i++;
                allMoney += bigBet;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.GameMenu , menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item.ItemId == Resource.Id.options)
            {
                Intent intent = new Intent(this, typeof(OptionsActivity));
                StartActivity(intent);
            }
            return true;
        }
    }
}