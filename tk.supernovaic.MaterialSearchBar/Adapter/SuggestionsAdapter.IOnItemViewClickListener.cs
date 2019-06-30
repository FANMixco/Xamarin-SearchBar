using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public partial class SuggestionsAdapter
    {
        public interface IOnItemViewClickListener
        {
            void IOnItemClickListener(int position, View v);
            void IOnItemDeleteListener(int position, View v);
        }
    }
}