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
    public class MyPhoneStateListener : PhoneStateListener
    {
        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            base.OnCallStateChanged(state, incomingNumber);
            switch (state)
            {
                case CallState.Ringing:
                    Toast.MakeText(Application.Context, incomingNumber, ToastLength.Short).Show(); break;
                case CallState.Offhook:
                    break;
                case CallState.Idle:
                    Toast.MakeText(Application.Context, "idle", ToastLength.Short).Show(); break;
            }
        }
    }
}