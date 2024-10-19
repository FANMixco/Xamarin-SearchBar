<a href="https://github.com/sponsors/FANMixco/" target="_blank">
   <img src="https://raw.githubusercontent.com/FANMixco/Xamarin-SearchBar/master/bmc-rezr5vpd.gif" alt="sponsor" />
</a>

# Xamarin-Material SearchBar Android
Material Design Search Bar for Android.

[![.NET](https://github.com/FANMixco/Xamarin-SearchBar/actions/workflows/dotnet.yml/badge.svg)](https://github.com/FANMixco/Xamarin-SearchBar/actions/workflows/dotnet.yml)

|Latest Release|Downloads|
|:------------:|:------------:|
|[![NuGet Badge Xamarin-MaterialSearchBar](https://img.shields.io/nuget/v/Xamarin-MaterialSearchBar)](https://www.nuget.org/packages/Xamarin-MaterialSearchBar/)|[![NuGet Badge SpotsDialog.Xamarin](https://img.shields.io/nuget/dt/Xamarin-MaterialSearchBar)](https://www.nuget.org/packages/Xamarin-MaterialSearchBar/)|

This version is **based on** the fantastic version created by: <a href="https://github.com/mancj
">Mansur Nashaev</a>

**The original one:** <a href="https://github.com/mancj/MaterialSearchBar"><img src="https://img.shields.io/badge/GitHub-MaterialSearchBar-green.svg" /></a>

This beautiful and easy to use library will help to add Lollipop Material Design SearchView in your project.

<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/preview.gif" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv1.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv2.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv3.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv4.png" width="400">
<img src="https://raw.githubusercontent.com/mancj/MaterialSearchBar/master/art/pv5.png" width="400">

***

### Read the <a href="https://github.com/FANMixco/Xamarin-SearchBar/wiki">Wiki</a> for advanced examples and options.

***

But now, let's go deep into some code!

### Basic example:

First to add Xamarin-MaterialSearchBar into your project you need to download the package from <a href="https://www.nuget.org/packages/Xamarin-MaterialSearchBar/">NuGet</a>.

Next edit your XML and add the custom control:

```xml
<tk.supernovaic.MaterialSearchBar.MaterialSearchBar
	style="@style/MaterialSearchBarLight"
	app:mt_speechMode="true"
	app:mt_hint="Custom hint"
	app:mt_maxSuggestionsCount="10"
	android:layout_width="match_parent"
	android:layout_height="wrap_content"
	android:id="@+id/searchBar" />
```
After that you can edit your activity and add the following piece of code:

```csharp
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
                       //Here you can execute the query with the texted data.
		}
	}
}
```


### Follow me at:

|  LinkedIn  |YouTube|Amazon|Goodreads|Instagram|Cyber Prophets|Sharing Your Stories|
|:----------|:------------:|:------------:|:------------:|:------------:|:------------:|:------------:|
|[![LinkedIn](https://i.sstatic.net/idQWu.png)](https://bit.ly/lfanmixco)|[![YouTube](https://i.sstatic.net/CFPMR.png)](https://youtube.com/c/FedericoNavarrete)|[![Amazon](https://i.sstatic.net/NFOeE.png)](https://www.amazon.com/Federico-Navarrete/e/B08NJTXQRV)|[![Goodreads](https://i.sstatic.net/oBk0g.jpg)](https://www.goodreads.com/author/show/21125413.Federico_Navarrete)|[![Instagram](https://i.sstatic.net/PIfqY.png)](https://www.instagram.com/federico_the_consultant)|[![RedCircle Podcast](https://i.sstatic.net/4XICF.png)](https://redcircle.com/shows/cyber-prophets)|[![RedCircle Podcast](https://i.sstatic.net/4XICF.png)](https://redcircle.com/shows/sharing-your-stories)|
