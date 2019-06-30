using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class DefaultSuggestionsAdapter : SuggestionsAdapter
    {
        private static IOnItemViewClickListener Listener;
        private readonly LayoutInflater Inflater;
        private static List<string> Suggestions_Clone { get; set; }

        public void SetListener(IOnItemViewClickListener listener)
        {
            Listener = listener;
        }

        public DefaultSuggestionsAdapter(LayoutInflater inflater) : base(inflater)
        {
            Inflater = inflater;
            Suggestions_Clone = Suggestions;
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
