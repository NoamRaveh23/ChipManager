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

        }
    }
}