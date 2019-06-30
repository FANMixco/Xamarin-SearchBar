using Android.Support.V7.Widget;
using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public interface IDefaultSuggestionsAdapter
    {
        int GetSingleViewHeight();
        void OnBindSuggestionHolder(string suggestion, RecyclerView.ViewHolder holder, int position);
        RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);
    }
}