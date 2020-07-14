using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class DefaultSuggestionsAdapter : SuggestionsAdapter
    {
        private static IOnItemViewClickListener Listener;

        public DefaultSuggestionsAdapter(LayoutInflater inflater) : base(inflater)
        {

        }

        public void SetListener(IOnItemViewClickListener listener)
        {
            Listener = listener;
        }

        public override int GetSingleViewHeight()
        {
            return 50;
        }

        public override void OnBindSuggestionHolder(string suggestion, RecyclerView.ViewHolder holder, int position)
        {
            var h = holder as SuggestionHolder;
            h.Text.Text = Suggestions[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new SuggestionHolder(Inflater.Inflate(Resource.Layout.item_last_request, parent, false));
        }
    }
}
