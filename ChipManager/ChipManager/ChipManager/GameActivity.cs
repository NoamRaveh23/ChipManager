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
        private List<Player> lp; // list of players
        EditText et;
        Dialog d; // dialog for choosing the winner
        List<Player> p; // clone of lp
        public static Player turnp; // player to play
        public static int bigBet; // the highest bet yet
        ListView lv;
        PlayerAdapter adapter;
        public static int counter = 0 , g = 0; //g --> games played
        int allMoney = 0 , pcount = 1; // allMOney --> the pot of the game so far
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
            for (int i =0; i<lp.Count; i++)
            {
                if ((p[i].getMoney() <=0)&&(i!=(winner-1)))
                {
                    p[i].setElim(true);
                }
            }
            p[winner - 1].winMoney(allMoney);
            restartGame();
            ISharedPreferences sp = GetSharedPreferences("lastWin", FileCreationMode.Private);
            ISharedPreferencesEditor editor = sp.Edit();
            editor.PutString("lastWin", p[winner - 1].getName());
            editor.Commit();
            /*small.Text = p[pcount].getName();
            big.Text = p[pcount + 1].getName();
            t.Text = p[pcount].getName();
            pcount++;*/
        }

        public void restartGame()
        {
            counter = 0;
            allMoney = 0;
            bigBet = 0;
            if (g < p.Count)
            {
                g++;
            }
            else
            {
                g = 0;
            }
            turnp = p[g];
        }
        private void Ex_Click(object sender, EventArgs e)
        {
            Finish();
        }



        public void turn()
        {
            p = this.lp;
            /*if (!p[this.i].getElim() && !p[this.i].getAllIn())
            {
                if (i < lp.Count && counter < 3)
                {
                    turnp = p[this.i];
                    t.Text = turnp.getName();
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
            else
            {
                if (i < lp.Count && counter < 3)
                {
                    turnp = p[this.i];
                    t.Text = turnp.getName();
                    //t.Text = turnp.getName();
                    this.i++;
                    
                }
                else if (i >= lp.Count && counter < 3)
                {
                    this.i = 0;
                    counter++;
                    turnp = p[this.i];
                    t.Text = turnp.getName();
                    this.i++;
                    
                }
                else
                {
                    endRound();
                }
            }*/
            
            if (!turnp.getElim()&& !turnp.getAllIn())
            {
                if (i < lp.Count && counter < 3)
                {
                    turnp = p[this.i];
                    t.Text = turnp.getName();
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
            else
            {
                skip(p , this.i);
                
                if (counter >= 3)
                {
                    if (adapter != null)
                    {
                        adapter.NotifyDataSetChanged();
                        t.Text = turnp.getName();
                    }
                    endRound();
                }
                else
                {
                    Toast.MakeText(this, "aaaaaa", ToastLength.Short).Show();
                }
                /*if (i <= lp.Count - 1 && counter < 3)
                {

                    turnp = p[this.i];
                    this.i++;
                    t.Text = turnp.getName();
                }
                else if (i >= lp.Count-1 && counter < 3)
                {
                    this.i = 0;
                    counter++;
                    turnp = p[this.i];
                }
                else if (i >= lp.Count && counter >= 3)
                {
                    if (adapter != null)
                    {
                        adapter.NotifyDataSetChanged();
                        t.Text = turnp.getName();
                    }
                    endRound();
                }
                else
                {
                    Toast.MakeText(this, "aaaaaa", ToastLength.Short).Show();
                }*/
                /*if (i < lp.Count && counter < 3)
                {
                    turnp = p[this.i];
                    t.Text = turnp.getName();
                    
                }
                else if (i >= lp.Count && counter < 3)
                {
                    this.i = 0;
                    counter++;
                    turnp = p[this.i];
                    t.Text = turnp.getName();
                    
                }
                else
                {
                    endRound();
                }*/
            }

        }

        public static void skip(List<Player> p , int i)
        {
            if (i < p.Count-1)
            {
                i++;
                turnp = p[i];
            }
            else if (i>=p.Count-1)
            {
                i = 0;
                turnp = p[i];
                counter++;
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
                if (turnp.getMoney() == 0 && !turnp.getElim())
                {
                    turnp.setAllIn(true);
                    allMoney += turnp.getBet();
                }
                else
                {
                    allMoney += bigBet;
                }
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
        /*public static bool Elim(Player pl)
        {
            if (pl.getElim())
            {
                return true;
            }
            return false;
        }*/
    }
}