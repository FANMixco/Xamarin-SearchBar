using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class DefaultSuggestionsAdapter
    {
        public partial class SuggestionHolder : RecyclerView.ViewHolder
        {
            public readonly TextView Text;
            public readonly ImageView Iv_delete;
            private static int AdapterPosition_Clone { get; set; }

            public SuggestionHolder(View itemView) : base(itemView)
            {
                AdapterPosition_Clone = AbsoluteAdapterPosition;
                Text = itemView.FindViewById<TextView>(Resource.Id.text);
                Iv_delete = itemView.FindViewById<ImageView>(Resource.Id.iv_delete);

                itemView.SetOnClickListener(new ItemViewListener());
                Iv_delete.SetOnClickListener(new ItemViewDeleteListener());
            }
        }
    }
}
