using Android.App;
using Android.Content;
using Android.Graphics;
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

     class StartGameActivity : Activity
    {
        public static List<IPlayer> lst = new List<IPlayer>();
        EditText name , money;
        RadioButton boy , girl;
        Button SaveStart , SaveAdd , photo;
        CheckBox vip;
        string gender = "";
        public static int counter = 0; // number of players
        Bitmap bit;
        ImageView img;
        bool t = false; // image check
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
            photo = (Button)FindViewById(Resource.Id.photo);
            vip = (CheckBox)FindViewById(Resource.Id.vip);
            //lst.Add(new Player("rotem", "girl", 1111));
            SaveAdd.Click += SaveAdd_Click;
            SaveStart.Click += SaveStart_Click;
            boy.Click += Boy_Click;
            girl.Click += Girl_Click;
            photo.Click += Photo_Click;
        }

        private void Photo_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 2);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 2)
            {
                if (resultCode == Result.Ok)
                {
                    bit = (Bitmap)data.Extras.Get("data");
                    t = true;
                }
            }
        }
                

        private void Girl_Click(object sender, EventArgs e)
        {
            gender = "girl";
        }

        private void Boy_Click(object sender, EventArgs e)
        {
            gender = "boy";
        }

        private void SaveStart_Click(object sender, EventArgs e)
        {
            addPlayer();
            Intent intent = new Intent(this, typeof(GameActivity));
            StartActivity(intent);
        }

        private void SaveAdd_Click(object sender, EventArgs e)
        {
            if (counter < 5)
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
            bool isVIP = false;
            try
            {
                if (counter < 6)
                {
                    if (vip.Checked)
                    {
                        isVIP = true;
                    }
                    if (t)
                    {
                        if (isVIP)
                        {
                            lst.Add(new VIPplayer(name.Text, gender, Int32.Parse(money.Text), bit));
                        }
                        else
                        {
                            lst.Add(new Player(name.Text, gender, Int32.Parse(money.Text), bit));
                        }
                        bit = null;
                        t = false;
                    }
                    else
                    {
                        lst.Add(new Player(name.Text, gender, Int32.Parse(money.Text)));
                    }
                    
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