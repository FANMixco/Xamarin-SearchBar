using Android.Views;

namespace tk.supernovaic.MaterialSearchBar.Adapter
{
    public interface IOnItemViewClickListener
    {
        void IOnItemClickListener(int position, View v);
        void IOnItemDeleteListener(int position, View v);
    }
}