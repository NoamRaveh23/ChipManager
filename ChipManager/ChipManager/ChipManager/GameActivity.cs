using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChipManager
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : AppCompatActivity
    {
        MyHandler mH;
        public static MyTimer mT;
        MyPhoneReceiver phone;
        StringTimeCount stc = new StringTimeCount("0");
        private List<Player> lp; // list of players
        EditText et;
        Dialog d; // dialog for choosing the winner
        public static List<Player> p; // clone of lp
        public static Player turnp; // player to play
        public static int bigBet, betterLoc; // the highest bet yet    betterLoc --> Location of the beting player in list
        ListView lv; //view the players
        PlayerAdapter adapter;
        public static int counter = 0 , g = 0 , timeCount = 0; //g --> games played  timeCount --> counting the time
        int allMoney = 0 , pcount = 1; // allMOney --> the pot of the game so far
        public static int i = 0; // position of turnp
        Button  ex, play ; // play --> play turn   ex --> exit  
        public static Button time; //  time  -->  shows the time
        public static bool stop = false, pause = false; // stop --> checks if timer should stop   pause --> check if timer should pause
        TextView small, big, t;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game);
            phone = new MyPhoneReceiver();
            lv = (ListView)FindViewById(Resource.Id.lv);
            ex = (Button)FindViewById(Resource.Id.exit);
            play = (Button)FindViewById(Resource.Id.play);
            small = (TextView)FindViewById(Resource.Id.small);
            big = (TextView)FindViewById(Resource.Id.big);
            t = (TextView)FindViewById(Resource.Id.t);
            time = (Button)FindViewById(Resource.Id.time);
            time.Text = "0";
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
            /*mH = new MyHandler(this, stc);
            mT = new MyTimer(mH, 0);
            mT.Begin();*/
            ThreadStart threadStart = new ThreadStart(Timer);
            Thread th = new Thread(threadStart);
            th.Start();
            MyPhoneStateListener phoneStateListener = new MyPhoneStateListener(this);
            TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            telephonyManager.Listen(phoneStateListener, PhoneStateListenerFlags.CallState);
        }
        public void UpdateCallState(CallState state, string incomingNumber)
        {
            switch (state)
            {
                case CallState.Ringing:
                    pause = true;
                    break;
                case CallState.Offhook:
                    pause = false;
                    break;
                case CallState.Idle:
                    pause = false;
                    break;
            }
        }
        protected override void OnResume()
        {
            RegisterReceiver(phone, new IntentFilter("android.intent.action.PHONE_STATE"));
            base.OnResume();
            
        }
        protected override void OnPause()
        {
            UnregisterReceiver(phone);
            base.OnPause();
        }
        private void Timer()
        {
            while (!stop)
            {
                if (!pause)
                {
                    timeCount++;
                    Thread.Sleep(1000);
                    RunOnUiThread(() =>
                    {
                        time.Text = timeString(timeCount);
                    });
                    
                }
            }
        }

        private String timeString(int t)
        {
            string str = "";
            int h = 0, m = 0, s = 0; // h --> hours  m --> minutes  s --> seconds
            h = t / 3600;
            m = (t % 3600) / 60;
            s = t % 60;
            str = h + " : " + m + " : " + s;
            return str;
        }
        private void Play_Click(object sender, EventArgs e)
        {           
            turn();
        }

        private void endRound()
        {
            counter++;
            d = new Dialog(this);
            d.SetContentView(Resource.Layout.winner);
            et = (EditText)d.FindViewById(Resource.Id.win);
            Button s = (Button)d.FindViewById(Resource.Id.s);
            s.Click += S_Click;
            d.SetTitle("Insert Winner Number");
            d.SetCancelable(true);
            d.Show();
            for (int i = 0; i < p.Count; i++)
            {
                if (!(p[i].getAllIn()) && !(p[i].getElim()))
                {
                    p[i].isCheck = false;
                }
            }
            
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
            for (int i = 0; i<p.Count; i++)
            {
                p[i].setBet(0);
            }
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
            pause = true;
            Finish();

        }



        public void turn()
        {
            p = this.lp;
            if (!turnp.getElim() && !turnp.getAllIn())
            {
                if (i < lp.Count && counter < 3)
                {
                    turnp = p[i];
                    t.Text = turnp.getName();
                    //t.Text = turnp.getName();
                    if (i == betterLoc-1)
                    {
                        if (everyoneCheck(p))
                        {
                            counter++;
                        }
                    }
                    Intent intent = new Intent(this, typeof(TurnActivity));
                    StartActivityForResult(intent, 0);
                }
                else if (i >= lp.Count && counter < 3)
                {
                    i = 0;
                    //counter++;
                    turnp = p[i];
                    t.Text = turnp.getName();
                    if (i == betterLoc-1)
                    {
                        if (everyoneCheck(p))
                        {
                            counter++;
                        }
                    }
                    Intent intent = new Intent(this, typeof(TurnActivity));
                    StartActivityForResult(intent, 0);
                }
                else if (everyoneCheck(p))
                {
                    endRound();
                }
            }
            else
            {
                skip(p, i);

                if (counter >= 3)
                {
                    if (everyoneCheck(p))
                    {
                        if (adapter != null)
                        {
                            adapter.NotifyDataSetChanged();
                            t.Text = turnp.getName();
                        }
                        endRound();
                    }
                    
                }
                else
                {
                    Toast.MakeText(this, "aaaaaa", ToastLength.Short).Show();
                }
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

        public static bool everyoneCheck(List<Player> p)
        {
            for (int i = 0; i< p.Count; i++)
            {
                if (!(p[i].isCheck))
                {
                    return false;
                }
            }
            return true;
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
                i++;
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