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
    class PlayerAdapter : BaseAdapter<Player>
    {
        Context context;
        List<Player> objects;
        
        public PlayerAdapter(Android.Content.Context context, System.Collections.Generic.List<Player> objects)
        {
            this.context = context;
            this.objects = objects;
        }

        public List<Player> GetList()
        {
            return this.objects;
        }
        public override Player this[int position]
        {
            get { return this.objects[position]; }
        }

        public override int Count
        {
            get { return this.objects.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((GameActivity)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.playerView, parent, false);
            ImageView ivGender = view.FindViewById<ImageView>(Resource.Id.iv1);
            TextView tvName = view.FindViewById<TextView>(Resource.Id.tv1);
            TextView tvMoney = view.FindViewById<TextView>(Resource.Id.tv2);
            TextView tvBet = view.FindViewById<TextView>(Resource.Id.tv3);
            Player temp = objects[position];
            if (temp != null)
            {
                if (temp.getGender().Equals("boy"))
                {
                    //ivGender.Background = (Android.Graphics.Drawables.Drawable)"@drawable/boy";
                }
                else if(temp.getGender().Equals("girl"))
                {
                    //ivGender.Background = (Android.Graphics.Drawables.Drawable)"@drawable/girl";
                }
                tvName.Text = temp.getName();
                tvMoney.Text = ""+temp.getMoney();
                tvBet.Text = "" + temp.getBet();
            }
            return view;
        }
    }
}
