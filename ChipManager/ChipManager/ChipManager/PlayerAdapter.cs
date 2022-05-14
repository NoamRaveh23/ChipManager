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
    class PlayerAdapter : BaseAdapter<IPlayer>
    {
        Context context;
        List<IPlayer> objects;
        
        public PlayerAdapter(Android.Content.Context context, System.Collections.Generic.List<IPlayer> objects)
        {
            this.context = context;
            this.objects = objects;
        }

        public List<IPlayer> GetList()
        {
            return this.objects;
        }
        public override IPlayer this[int position]
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
            LinearLayout ll2 = view.FindViewById<LinearLayout>(Resource.Id.ll2);
            ImageView ivGender = view.FindViewById<ImageView>(Resource.Id.iv1);
            TextView tvName = view.FindViewById<TextView>(Resource.Id.tv1);
            TextView tvMoney = view.FindViewById<TextView>(Resource.Id.tv2);
            TextView tvBet = view.FindViewById<TextView>(Resource.Id.tv3);
            IPlayer temp = objects[position];
            if (temp != null)
            {
                if (temp.ifPImage())
                {
                    ivGender.SetImageBitmap(temp.getBit());
                }                
                else if (temp.getGender().Equals("boy"))
                {
                    ivGender.SetImageResource(Resource.Drawable.boy);
                }
                else if (temp.getGender().Equals("girl"))
                {
                    ivGender.SetImageResource(Resource.Drawable.girl);
                }
                else if (temp.getGender().Equals("elimGirl"))
                {
                    ivGender.SetImageResource(Resource.Drawable.girlElim);
                }
                else if (temp.getGender().Equals("elimBoy"))
                {
                    ivGender.SetImageResource(Resource.Drawable.boyElim);
                }
                tvName.Text = temp.getName();
                tvMoney.Text = ""+temp.getMoney();
                tvBet.Text = "" + temp.getBet();


                playerType t = temp.getType();


                if (t == playerType.VIPplayer)
                {
                    ll2.SetBackgroundDrawable(context.GetDrawable(Resource.Drawable.vipBack));
                    Console.WriteLine("vip");
                }

                
            }
            return view;
        }
    }
}
