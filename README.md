# Xamarin-Material SearchBar Android
Material Design Search Bar for Android, you can download it from:

https://www.nuget.org/packages/Xamarin-MaterialSearchBar

This version is **based on** the fantastic version created by:

https://github.com/mancj

**The original one:**

https://github.com/mancj/MaterialSearchBar

This beautiful and easy to use library will help to add Lollipop Material Design SearchView in your project.

<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/preview.gif" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv1.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv2.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv3.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv4.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv5.png" width="400">

**Basic example:**

**XML:**

    <tk.supernovaic.MaterialSearchBar.MaterialSearchBar
        style="@style/MaterialSearchBarLight"
        app:mt_speechMode="true"
        app:mt_hint="Custom hint"
        app:mt_maxSuggestionsCount="10"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:id="@+id/searchBar" />

**C#:**

    public partial class YourClassActivity : AppCompatActivity, MaterialSearchBar.IOnSearchActionListener
    {    
        private MaterialSearchBar MSearchBar { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MSearchBar = FindViewById<MaterialSearchBar>(Resource.Id.searchBar);

            MSearchBar.SetOnSearchActionListener(this);

            MSearchBar.AddTextChangeListener(new MaterialSearchBarListener());
        }

        void MaterialSearchBar.IOnSearchActionListener.OnButtonClicked(int p0)
        {
            switch (p0)
            {
                case MaterialSearchBar.ButtonNavigation:
                    Drawer.OpenDrawer((int)GravityFlags.Left);
                    break;
                case MaterialSearchBar.ButtonSpeech:
                    break;
                case MaterialSearchBar.ButtonBack:
                    MSearchBar.DisableSearch();
                    break;
            }
        }
    }
    
    public partial class YourClassActivity
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

            }
        }
    }
