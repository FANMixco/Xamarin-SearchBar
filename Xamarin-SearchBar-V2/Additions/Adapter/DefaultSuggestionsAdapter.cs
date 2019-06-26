using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace Com.Mancj.Materialsearchbar.Adapter
{
    public class DefaultSuggestionsAdapter : SuggestionsAdapter<string, SuggestionHolder>
    {
        private IOnItemViewClickListener Listener { get; set; }

        public override int GetSingleViewHeight()
        {
            return 50;
        }

        public DefaultSuggestionsAdapter(LayoutInflater inflater) : base(inflater)
        {
        }

        public override void OnBindSuggestionHolder(string suggestion, RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}
