﻿using Android.App;
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
    [Activity(Label = "StartGameActivity")]
    public class StartGameActivity : Activity
    {
        List<Player> lst;
        EditText name , money;
        RadioGroup gender;
        Button SaveStart , SaveAdd;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.startGame);
            lst = new List<Player>();

        }
    }
}