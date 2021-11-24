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
    [Activity(Label = "StartGameActivity")]
    public class StartGameActivity : Activity
    {
        public static List<Player> lst = new List<Player>();
        EditText name , money;
        RadioButton boy , girl;
        Button SaveStart , SaveAdd;
        string gender = "";
        private int counter = 0; // number of players

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startGame);
            name = (EditText)FindViewById(Resource.Id.ETname);
            money = (EditText)FindViewById(Resource.Id.ETmoney);
            boy = (RadioButton)FindViewById(Resource.Id.radio_male);
            girl = (RadioButton)FindViewById(Resource.Id.radio_female);
            SaveAdd = (Button)FindViewById(Resource.Id.sna);
            SaveStart = (Button)FindViewById(Resource.Id.sns);
            SaveAdd.Click += SaveAdd_Click;
            SaveStart.Click += SaveStart_Click;
        }

        private void SaveStart_Click(object sender, EventArgs e)
        {
            addPlayer();
            Intent intent = new Intent(this, typeof(GameActivity));
            StartActivity(intent);
        }

        private void SaveAdd_Click(object sender, EventArgs e)
        {
            if (this.counter < 5)
            {
                addPlayer();
            }
            else 
            {
                addPlayer();
                Toast.MakeText(this, "You can add only 6 players", ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            }

            
        }

        public void addPlayer()
        {
            try
            {
                if (this.counter < 6)
                {
                    if (boy.Selected)
                    {
                        gender = "boy";
                    }
                    else if (girl.Selected)
                    {
                        gender = "girl";
                    }
                    else
                    {
                        gender = "non";
                    }
                    lst.Add(new Player(name.Text, gender, Int32.Parse(money.Text)));
                    Toast.MakeText(this, "Player added!", ToastLength.Short).Show();
                    name.Text = "";
                    money.Text = "";
                    counter++;
                }
                else
                {
                    Toast.MakeText(this, "You can add only 6 players", ToastLength.Short).Show();
                }
                
            }
            catch
            {
                Toast.MakeText(this, "Something went wrong", ToastLength.Short).Show();
            }
            
        }
    }
}