using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;

namespace ChipManager
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "android.intent.action.PHONE_STATE" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class MyPhoneReceiver : BroadcastReceiver
    {
        public MyPhoneReceiver()
        {
        }
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (action.Equals("android.intent.action.ACTION_POWER_CONNECTED"))
            {
                Toast.MakeText(context, "USB CONNECTED TO DEVICE", ToastLength.Short).Show();
            }
            else if (action.Equals("android.intent.action.ACTION_POWER_DISCONNECTED"))
            {
                Toast.MakeText(context, "USB DISCONNECTED FROM DEVICE", ToastLength.Short).Show();
            }
            //MyPhoneStateListener phoneStateListener = new MyPhoneStateListener(this);
            //TelephonyManager telephonyManager = (TelephonyManager)Application.Context.GetSystemService(Context.TelephonyService); telephonyManager.Listen(phoneStateListener, PhoneStateListenerFlags.CallState);
        }
    }
}