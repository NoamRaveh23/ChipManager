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
using System.Threading;

namespace ChipManager
{
    public class MyTimer
    {
        int counter;
        MyHandler handler;
        public bool stop;
        public bool pause;
        public MyTimer(MyHandler handler, int counter)
        {
            this.handler = handler;
            this.counter = counter;
        }
        public void Begin()
        {
            ThreadStart threadStart = new ThreadStart(Run);
            Thread t = new Thread(threadStart);
            this.stop = false;
            this.pause = false;
            t.Start();
        }
        public void Run()
        {
            while (!stop)
            {
                if (!pause)
                {
                    counter++;
                    Thread.Sleep(1000);
                    Message msg = new Message();
                    msg.Arg1 = counter;
                    handler.SendMessage(msg);
                    GameActivity.time.Text = counter + "";
                }
            }
        }
    }
}