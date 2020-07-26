using Android.Text;
using Android.Views;
using Java.Lang;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        private class TextWatcherChangeListener : Object, ITextWatcher
        {
            private MaterialSearchBar MSearchBar { get; set; }
            public TextWatcherChangeListener(MaterialSearchBar mSearchBar)
            {
                MSearchBar = mSearchBar;
            }

            public void AfterTextChanged(IEditable s)
            {

            }

            public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
            {

            }

            public void OnTextChanged(ICharSequence s, int start, int before, int count)
            {
                MSearchBar.ClearIcon.Visibility = s.Length() == 0 ? ViewStates.Gone : ViewStates.Visible;
            }
        }
    }
}