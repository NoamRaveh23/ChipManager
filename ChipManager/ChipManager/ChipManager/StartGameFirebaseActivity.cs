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
using Firebase.Database;
using Firebase.Database.Query;

namespace ChipManager
{
    [Activity(Label = "StartGameFirebaseActivity")]
    public class StartGameFirebaseActivity : Activity
    {
        public static List<Player> lst = new List<Player>();
        public static int code;
        EditText name, money;
        RadioButton boy, girl;
        Button SaveStart, SaveAdd, photo;
        string gender = "";
        public static int counter = 0; // number of players
        Bitmap bit;
        ImageView img;
        bool t = false; // image check
        bool create; // checks if the player join or create the game
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startGame);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startGame);
            name = (EditText)FindViewById(Resource.Id.ETname);
            money = (EditText)FindViewById(Resource.Id.ETmoney);
            boy = (RadioButton)FindViewById(Resource.Id.radio_male);
            girl = (RadioButton)FindViewById(Resource.Id.radio_female);
            SaveAdd = (Button)FindViewById(Resource.Id.sna);
            SaveStart = (Button)FindViewById(Resource.Id.sns);
            photo = (Button)FindViewById(Resource.Id.photo);
            //lst.Add(new Player("rotem", "girl", 1111));
            SaveAdd.Visibility = ViewStates.Invisible;
            SaveStart.Click += SaveStart_Click;
            boy.Click += Boy_Click;
            girl.Click += Girl_Click;
            photo.Click += Photo_Click;
        }
        //open camera
        private void Photo_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 3);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 3)
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

        private async void SaveStart_Click(object sender, EventArgs e)
        {
            
            if (create)
            {
                addPlayer();
                Game game = new Game(lst);
                Intent intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            }
            else
            {
                try
                {
                    Game game = new Game();
                    code = game.code;
                    game = await FirebaseHelper.Get(code);
                    lst = game.getGList();
                    addPlayer();
                    game.setGList(lst);
                    Intent intent = new Intent(this, typeof(GameActivity));
                    StartActivity(intent);
                }
                catch
                {
                    Toast.MakeText(this, "Invalid Game Code", ToastLength.Short).Show();
                }
            }
            
        }

        
        public void addPlayer()
        {
            try
            {
                if (lst.Count < 6)
                {
                    if (t)
                    {
                        lst.Add(new Player(name.Text, gender, Int32.Parse(money.Text), bit));
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