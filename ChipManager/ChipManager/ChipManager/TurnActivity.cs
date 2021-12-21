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
    [Activity(Label = "TurnActivity")]
    public class TurnActivity : Activity
    {
        EditText bet;
        Button clean, save;
        RadioButton check, addB;
        TextView Pname;
        Player p;
        int BigB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.turn);
            p = GameActivity.turnp;
            BigB = GameActivity.bigBet;
            Pname = (TextView)FindViewById(Resource.Id.pname);
            check = (RadioButton)FindViewById(Resource.Id.radio_check);
            addB = (RadioButton)FindViewById(Resource.Id.radio_Bet);
            bet = (EditText)FindViewById(Resource.Id.newBet);
            save = (Button)FindViewById(Resource.Id.save);
            clean = (Button)FindViewById(Resource.Id.clean);
            Pname.Text = p.getName();
            save.Click += Save_Click;
            clean.Click += Clean_Click;
            check.Click += Check_Click; 
            addB.Click += AddB_Click;
        }

        private void AddB_Click(object sender, EventArgs e)
        {
            
        }

        private void Check_Click(object sender, EventArgs e)
        {
            bet.Text = "XXXXXXXX";
            p.setBet(BigB);
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            addB.Text = "New Bet";
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(bet.Text) > BigB && Int32.Parse(bet.Text)<= p.getMoney())
            {
                p.setBet(Int32.Parse(bet.Text));
                p.setMoney(Int32.Parse(bet.Text));
                BigB = Int32.Parse(bet.Text);
                GameActivity.bigBet = BigB;
                Finish();
            }
            else 
            {
                Toast.MakeText(this, "Minimum bet:" + (BigB+1), ToastLength.Short).Show();
            }
            
        }
    }
}