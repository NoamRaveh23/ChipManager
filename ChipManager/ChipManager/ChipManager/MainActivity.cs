using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace ChipManager
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button newGame , conGame, options, profile;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            newGame = (Button)FindViewById(Resource.Id.newGame);
            conGame = (Button)FindViewById(Resource.Id.ConGame);
            options = (Button)FindViewById(Resource.Id.options);
            profile = (Button)FindViewById(Resource.Id.profile);
            newGame.Click += NewGame_Click;
            conGame.Click += ConGame_Click;
            options.Click += Options_Click;
            profile.Click += Profile_Click;
        }

        private void Profile_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Options_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ConGame_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void NewGame_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}