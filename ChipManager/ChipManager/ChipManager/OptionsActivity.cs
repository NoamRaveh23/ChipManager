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
    [Activity(Label = "OptionsActivity")]
    public class OptionsActivity : Activity
    {
        SeekBar Msb, Gsb;
        Button quit, resume;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.options);
            // Create your application here
            Msb = (SeekBar)FindViewById(Resource.Id.Msb);
            Gsb = (SeekBar)FindViewById(Resource.Id.Gsb);
            quit = (Button)FindViewById(Resource.Id.quit);
            resume = (Button)FindViewById(Resource.Id.resume);
            Msb.ProgressChanged += Msb_ProgressChanged;
            quit.Click += Quit_Click;
            resume.Click += Resume_Click;
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(FirstService));
            StopService(intent);
            Intent intent2 = new Intent(this, typeof(MainActivity));
            StartActivity(intent2);
        }

        private void Msb_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            FirstService.setVolume(Msb.Progress);
        }

    }
}