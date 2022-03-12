using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChipManager
{
    public class StringTimeCount
    {
        private string STC;
        public StringTimeCount(string s)
        {
            this.STC = s;
        }
        public string getSTC()
        {
            return this.STC;
        }
        public void setSTC(string s)
        {
            this.STC = s;
        }
    }
}