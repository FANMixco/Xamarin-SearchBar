using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class DefaultSuggestionsAdapter
    {
        public partial class SuggestionHolder
        {
            public class ItemViewListener : Java.Lang.Object, View.IOnClickListener
            {
                public void OnClick(View v)
                {
                    v.Tag = Suggestions[AdapterPosition_Clone];
                    Listener.IOnItemClickListener(AdapterPosition_Clone, v);
                }
            }
        }
    }
}
