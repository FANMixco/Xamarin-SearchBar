using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using static MaterialSearchBar.Sample.MainActivity.CustomSuggestionsAdapter;

namespace MaterialSearchBar.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public partial class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, tk.supernovaic.MaterialSearchBar.MaterialSearchBar.IOnSearchActionListener
    {
        public tk.supernovaic.MaterialSearchBar.MaterialSearchBar MSearchBar { get; set; }
        private DrawerLayout Drawer { get; set; }

        private static CustomSuggestionsAdapter CustomSuggestionsAdapterPreview { get; set; }

        private List<Product> Products = new List<Product>() {
            new Product("Simvastatin", "Red"),
            new Product("Carrot Daucus carota", "Orange"),
            new Product("Sodium Fluoride", "Orange"),
            new Product("White Kidney Beans", "Mandarin"),
            new Product("Salicylic Acid", "Green"),
            new Product("cetirizine hydrochloride", "Purple"),
            new Product("Mucor racemosus", "Red"),
            new Product("Thymol", "Brown"),
            new Product("TOLNAFTATE", "Brown"),
            new Product("Albumin Human", "Red")
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.activity_main);

                Drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.SetNavigationItemSelectedListener(this);
                MSearchBar = FindViewById<tk.supernovaic.MaterialSearchBar.MaterialSearchBar>(Resource.Id.searchBar);
                MSearchBar.SetOnSearchActionListener(this);
                MSearchBar.InflateMenu(Resource.Menu.main);

                MSearchBar.SetCardViewElevation(10);
                MSearchBar.AddTextChangeListener(new TextWatcher(this));

                Drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                FloatingActionButton searchButton = FindViewById<FloatingActionButton>(Resource.Id.searchButton);
                searchButton.Click += SearchButton_Click;

                MSearchBar = FindViewById<tk.supernovaic.MaterialSearchBar.MaterialSearchBar>(Resource.Id.searchBar);

                MSearchBar.SetOnSearchActionListener(this);

                MSearchBar.AddTextChangeListener(new MaterialSearchBarListener());

                LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);

                CustomSuggestionsAdapterPreview = new CustomSuggestionsAdapter(inflater);
                CustomSuggestionsAdapterPreview.Filter = new CustomFilter(CustomSuggestionsAdapterPreview);

                List<string> suggestions = Products.Select(x => x.Title).ToList();

                CustomSuggestionsAdapterPreview.SetSuggestions(suggestions);
                CustomSuggestionsAdapterPreview.UpdateSuggestions(Products);

                MSearchBar.SetCustomSuggestionAdapter(CustomSuggestionsAdapterPreview);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        private void SearchButton_Click(object sender, System.EventArgs e)
        {
            MSearchBar.EnableSearch();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Drawer.IsDrawerOpen(GravityCompat.Start))
            {
                //replace this with actual function which returns if the drawer is open
                Drawer.CloseDrawers();     // replace this with actual function which closes drawer
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Inflate the menu; this adds items to the action bar if it is present.
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Handle action bar item clicks here. The action bar will
            // automatically handle clicks on the Home/Up button, so long
            // as you specify a parent activity in AndroidManifest.xml.
            int id = item.ItemId;

            //noinspection SimplifiableIfStatement
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void OnSearchStateChanged(bool enabled)
        {

        }

        public void OnSearchConfirmed(string text)
        {

        }

        public void OnButtonClicked(int buttonCode)
        {
            switch (buttonCode)
            {
                case tk.supernovaic.MaterialSearchBar.MaterialSearchBar.BUTTON_NAVIGATION:
                    Drawer.OpenDrawer((int)GravityFlags.Left);
                    break;
                case tk.supernovaic.MaterialSearchBar.MaterialSearchBar.BUTTON_SPEECH:
                    break;
                case tk.supernovaic.MaterialSearchBar.MaterialSearchBar.BUTTON_BACK:
                    MSearchBar.DisableSearch();
                    break;
            }
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            // Handle navigation view item clicks here.
            int id = menuItem.ItemId;
            switch (id)
            {
                case Resource.Id.nav_camera:
                    break;
                case Resource.Id.nav_gallery:
                    break;
                case Resource.Id.nav_slideshow:
                    break;
                case Resource.Id.nav_manage:
                    break;
                case Resource.Id.nav_share:
                    break;
                case Resource.Id.nav_send:
                    break;
            }

            Drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
    }
}