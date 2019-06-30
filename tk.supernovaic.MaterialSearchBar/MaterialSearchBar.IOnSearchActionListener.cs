namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        /**
         * Interface definition for MaterialSearchBar callbacks.
         */
        public interface IOnSearchActionListener
        {
            /**
             * Invoked when SearchBar opened or closed
             *
             * @param enabled state
             */
            void OnSearchStateChanged(bool enabled);

            /**
             * Invoked when search confirmed and "search" button is clicked on the soft keyboard
             *
             * @param text search input
             */
            void OnSearchConfirmed(string text);

            /**
             * Invoked when "speech" or "navigation" buttons clicked.
             *
             * @param buttonCode {@link #BUTTON_NAVIGATION}, {@link #BUTTON_SPEECH} or {@link #BUTTON_BACK} will be passed
             */
            void OnButtonClicked(int buttonCode);
        }
    }
}