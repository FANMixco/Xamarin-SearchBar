using Android.Text;
using Java.Lang;

namespace MaterialSearchBar.Sample
{
    public partial class MainActivity
    {
        private class MaterialSearchBarListener : Java.Lang.Object, ITextWatcher
        {
            public void AfterTextChanged(IEditable s)
            {

            }

            public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
            {

            }

            public void OnTextChanged(ICharSequence s, int start, int before, int count)
            {
                CustomSuggestionsAdapterPreview.Filter.InvokeFilter(s);
            }
        }
    }
}