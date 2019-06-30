using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class DefaultSuggestionsAdapter
    {
        public partial class SuggestionHolder
        {
            public class ItemViewDeleteListener : Java.Lang.Object, View.IOnClickListener
            {
                public void OnClick(View v)
                {
                    int position = AdapterPosition_Clone;
                    if (position > 0 && position < Suggestions_Clone.Count)
                    {
                        v.Tag = Suggestions_Clone[AdapterPosition_Clone];
                        Listener.IOnItemClickListener(AdapterPosition_Clone, v);
                    }
                }
            }
        }
    }
}
