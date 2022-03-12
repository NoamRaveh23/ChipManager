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
    public class MyHandler : Handler
    {
        Context context;
        StringTimeCount tvResult;

        [System.Obsolete]
        public MyHandler(Context context, StringTimeCount tv)
        {
            this.context = context;
            this.tvResult = tv;
        }
        public override void HandleMessage(Message msg)
        {
            this.tvResult.setSTC("" + msg.Arg1);
        }
    }
}