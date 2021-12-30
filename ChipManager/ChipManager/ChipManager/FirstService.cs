using Android.App;
using Android.Content;
using Android.Media;
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

    [Service(Label = "FirstService")]//write service to menifest file 
    [IntentFilter(new String[] { "com.yourname.FirstService" })]
    class FirstService : Service
    {
        IBinder binder;//null not in bagrut 
        static MediaPlayer mp;
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            // start your service logic here

            mp = MediaPlayer.Create(this, Resource.Raw.PokerMusic);
            mp.Start();
            // Return the correct StartCommandResult for the type of service you are building
            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (mp != null)
            {
                mp.Stop();
                mp.Release();
                mp = null;
            }
        }
        public static void setVolume(float v)
        {
            mp.SetVolume(v/100,v/100);
        }
    }
}