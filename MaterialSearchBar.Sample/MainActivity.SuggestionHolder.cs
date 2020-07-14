using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace MaterialSearchBar.Sample
{
    public partial class MainActivity
    {
        public class SuggestionHolder : RecyclerView.ViewHolder
        {
            public TextView Title { get; set; }
            public TextView SubTitle { get; set; }
            public ImageView ImgThumbnail { get; set; }

            public SuggestionHolder(View itemView) : base(itemView)
            {
                Title = itemView.FindViewById<TextView>(Resource.Id.title);
                SubTitle = itemView.FindViewById<TextView>(Resource.Id.subtitle);
                ImgThumbnail = itemView.FindViewById<ImageView>(Resource.Id.imgThumbnail);
                itemView.Click += ItemView_Click;
            }

            private void ItemView_Click(object sender, System.EventArgs e)
            {

            }
        }
    }
}