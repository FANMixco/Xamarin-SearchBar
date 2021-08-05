using Android.Text;
using Android.Widget;
using Java.Lang;

namespace MaterialSearchBar.Sample
{
    internal class TextWatcher : Object, ITextWatcher
    {
        private readonly MainActivity context;
        private tk.supernovaic.MaterialSearchBar.MaterialSearchBar SearchBar;

        public TextWatcher(MainActivity context)
        {
            this.context = context;
        }

        public void AfterTextChanged(IEditable s)
        {

        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {

        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            Toast.MakeText(context, context.MSearchBar.GetText(), ToastLength.Short);
        }
    }
}