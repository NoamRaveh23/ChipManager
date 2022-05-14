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
     class MyPhoneStateListener : PhoneStateListener
    {
        private readonly GameActivity _activity;

        public MyPhoneStateListener(GameActivity activity)
        {
            _activity = activity; 
        }
        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            base.OnCallStateChanged(state, incomingNumber);
            _activity.UpdateCallState(state, incomingNumber);

        }
        
    }
}