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
        RadioButton check, addB, allIn;
        TextView Pname;
        Player p;
        int BigB;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.turn);
            
            p = GameActivity.turnp;
            Toast.MakeText(this, "p:" + p.getName(), ToastLength.Short).Show();
            BigB = GameActivity.bigBet;
            Pname = (TextView)FindViewById(Resource.Id.pname);
            check = (RadioButton)FindViewById(Resource.Id.radio_check);
            addB = (RadioButton)FindViewById(Resource.Id.radio_Bet);
            allIn = (RadioButton)FindViewById(Resource.Id.radio_All);
            bet = (EditText)FindViewById(Resource.Id.newBet);
            save = (Button)FindViewById(Resource.Id.save);
            clean = (Button)FindViewById(Resource.Id.clean);
            Pname.Text = p.getName();
            save.Click += Save_Click;
            clean.Click += Clean_Click;
            check.Click += Check_Click; 
            addB.Click += AddB_Click;
            allIn.Click += AllIn_Click;
        }

        private void AllIn_Click(object sender, EventArgs e)
        {
            bet.Text = p.getMoney().ToString();
            p.setAllIn(true);
            p.isCheck = true;
        }

        private void AddB_Click(object sender, EventArgs e)
        {
            p.setAllIn(false);
            p.isCheck = false;
        }

        private void Check_Click(object sender, EventArgs e)
        {
            bet.Text = BigB.ToString();
            p.setBet(BigB);
            p.setAllIn(false);
            p.isCheck = true;
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            addB.Text = "Add Bet";
            bet.Text = "";
            p.setAllIn(false);
            p.isCheck = false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Int32.Parse(bet.Text) >= BigB && Int32.Parse(bet.Text) < p.getMoney())
                {
                    if (Int32.Parse(bet.Text) > BigB)
                    {
                        p.isCheck = false;
                        for (int i = 0; i < GameActivity.p.Count; i++)
                        {
                            if (!(GameActivity.p[i].getAllIn()) && !(GameActivity.p[i].getElim()))
                            {
                                GameActivity.p[i].isCheck = false;
                            }
                        }

                    }
                    else if (Int32.Parse(bet.Text) == BigB)
                    {
                        p.isCheck = true;
                    }
                    p.setBet(Int32.Parse(bet.Text));
                    p.setMoney(Int32.Parse(bet.Text));
                    GameActivity.turnp = p;
                    BigB = Int32.Parse(bet.Text);
                    GameActivity.bigBet = BigB;
                    Finish();
                }
                else if (Int32.Parse(bet.Text) == p.getMoney())
                {
                    p.setAllIn(true);
                    p.isCheck = true;
                    p.setBet(p.getMoney());
                    p.setMoney(p.getMoney());
                    GameActivity.turnp = p;
                    if (p.getBet() > BigB)
                    {
                        BigB = Int32.Parse(bet.Text);
                        GameActivity.bigBet = BigB;
                    }
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Minimum bet:" + (BigB + 1), ToastLength.Short).Show();
                }
            }
            catch
            {
                Toast.MakeText(this, "something went wrong, try again", ToastLength.Short).Show();
            }
            
        }
    }
}