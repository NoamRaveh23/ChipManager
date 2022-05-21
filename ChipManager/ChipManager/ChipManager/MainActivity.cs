using Android.App;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Content;

namespace ChipManager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button newGame , conGame, options, profile , rules;
        TextView lstw;
        public bool isgame = false; //is active game
        //MediaPlayer mp;
        //AudioManager am;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            lstw = (TextView)FindViewById(Resource.Id.lstw);
            newGame = (Button)FindViewById(Resource.Id.newGame);
            conGame = (Button)FindViewById(Resource.Id.ConGame);
            options = (Button)FindViewById(Resource.Id.options);
            rules = (Button)FindViewById(Resource.Id.rule);
            rules.Click += Rules_Click;
            newGame.Click += NewGame_Click;
            conGame.Click += ConGame_Click;
            options.Click += Options_Click;
            Intent intent = new Intent(this, typeof(FirstService));
            StartService(intent);
            ISharedPreferences sp = GetSharedPreferences("lastWin", FileCreationMode.Private);
            lstw.Text = " Last Round Winner: " + sp.GetString("lastWin", "non");
            /*mp = MediaPlayer.Create(this, Resource.Raw.PokerMusic);
            mp.Start();
            am = (AudioManager)GetSystemService(Context.AudioService);
            int max = am.GetStreamMaxVolume(Stream.Music);
            am.SetStreamVolume(Stream.Music, max / 2, 0);*/
        }

        private void Rules_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://playpoker.co.il/%D7%A4%D7%95%D7%A7%D7%A8-%D7%97%D7%95%D7%A7%D7%99%D7%9D/"));
            StartActivity(intent);
        }
        
        private void Options_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OptionsActivity));
            StartActivity(intent);
        }

        private void ConGame_Click(object sender, System.EventArgs e)
        {
            if (isgame)
            {
                Intent intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
            }
            else
            {
                isgame = true;
                Intent intent = new Intent(this, typeof(StartGameActivity));
                StartActivity(intent);
            }
        }

        private void NewGame_Click(object sender, System.EventArgs e)
        {
            isgame = true;
            Intent intent = new Intent(this, typeof(StartGameActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}