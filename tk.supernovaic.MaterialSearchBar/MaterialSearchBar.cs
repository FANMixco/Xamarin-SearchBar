using System;
using System.Collections.Generic;
using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.CardView.Widget;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using tk.supernovaic.MaterialSearchBar.Adapter;
using static tk.supernovaic.MaterialSearchBar.Adapter.SuggestionsAdapter;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar : FrameLayout, View.IOnClickListener, Animation.IAnimationListener, IOnItemViewClickListener
    {
        #region properties
        public const int BUTTON_SPEECH = 1;
        public const int BUTTON_NAVIGATION = 2;
        public const int BUTTON_BACK = 3;
        public const int VIEW_VISIBLE = 1;
        public const int VIEW_INVISIBLE = 0;

        private CardView SearchBarCardView { get; set; }
        private LinearLayout InputContainer { get; set; }

        private int NavIconRes { get; set; }
        private int? NavToBackRes { get; set; }
        private int? BackToNavRes { get; set; }

        private ImageView NavIcon { get; set; }
        private ImageView MenuIcon { get; set; }
        private ImageView SearchIcon { get; set; }
        private ImageView ArrowIcon { get; set; }
        private ImageView ClearIcon { get; set; }
        public EditText SearchEdit { get; set; }
        public TextView PlaceHolder { get; set; }
        private View SuggestionDivider { get; set; }
        public IOnFocusChangeListener OnFocusChangeListener { get; set; }

        private IOnSearchActionListener OnSearchActionListener { get; set; }
        public bool IsSearchEnabled { get; set; }
        public bool IsSuggestionsVisible { get; set; }
        public bool IsSuggestionsEnabled = true;
        private int LastSuggestionHeight { get; set; }
        private static SuggestionsAdapter Adapter { get; set; }
        private float Destiny { get; set; }

        public PopupMenu PopupMenu { get; set; }

        private int MenuIconRes { get; set; }
        public int SearchIconRes { get; set; }
        private int SpeechIconRes { get; set; }
        public int ArrowIconRes { get; set; }
        public int ClearIconRes { get; set; }

        private bool SpeechMode { get; set; }
        private int MaxSuggestionCount { get; set; }
        private int SuggestionAnimationSpeed { get; set; }
        private bool NavButtonEnabled { get; set; }
        private bool RoundedSearchBarEnabled { get; set; }
        private Color DividerColor { get; set; }
        private Color SearchBarColor { get; set; }

        private string HintText { get; set; }
        private string PlaceholderText { get; set; }
        private ColorStateList TextColor { get; set; }
        private ColorStateList HintColor { get; set; }

        public ColorStateList PlaceholderColor { get; set; }
        private Color NavIconTint { get; set; }
        private Color MenuIconTint { get; set; }
        private Color SearchIconTint { get; set; }
        private Color ArrowIconTint { get; set; }
        private Color ClearIconTint { get; set; }
        private bool NavIconTintEnabled { get; set; }
        private bool MenuIconTintEnabled { get; set; }
        private bool SearchIconTintEnabled { get; set; }
        private bool ArrowIconTintEnabled { get; set; }
        private bool ClearIconTintEnabled { get; set; }
        public bool BorderlessRippleEnabled { get; set; }

        public Color TextCursorColor { get; set; }
        public Color HighlightedTextColor { get; set; }

        public bool RemoveAccents { get; set; } = false;
        #endregion

        #region constructors
        public MaterialSearchBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(attrs);
        }

        public MaterialSearchBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(attrs);
        }

        public MaterialSearchBar(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(attrs);
        }

        protected MaterialSearchBar(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }
        #endregion

        private void Init(IAttributeSet attrs)
        {
            Inflate(Context, Resource.Layout.searchbar, this);

            TypedArray array = Context.ObtainStyledAttributes(attrs, Resource.Styleable.MaterialSearchBar);

            //Base Attributes
            SpeechMode = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_speechMode, false);
            MaxSuggestionCount = array.GetInt(Resource.Styleable.MaterialSearchBar_mt_maxSuggestionsCount, 3);
            NavButtonEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_navIconEnabled, false);
            RoundedSearchBarEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_roundedSearchBarEnabled, false);
            DividerColor = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_dividerColor, ContextCompat.GetColor(Context, Resource.Color.searchBarDividerColor));
            SearchBarColor = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_searchBarColor, ContextCompat.GetColor(Context, Resource.Color.searchBarPrimaryColor));
            SuggestionAnimationSpeed = array.GetInteger(Resource.Styleable.MaterialSearchBar_mt_suggestionsAnimationSpeed, 1200);

            //Icon Related Attributes
            NavIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_navIconDrawable, Resource.Drawable.ic_menu_animated);

            MenuIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_menuIconDrawable, Resource.Drawable.ic_dots_vertical_black_48dp);
            SearchIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_searchIconDrawable, Resource.Drawable.ic_magnify_black_48dp);
            SpeechIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_speechIconDrawable, Resource.Drawable.ic_microphone_black_48dp);
            ArrowIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_backIconDrawable, Resource.Drawable.ic_arrow_left_black_48dp);
            ClearIconRes = array.GetResourceId(Resource.Styleable.MaterialSearchBar_mt_clearIconDrawable, Resource.Drawable.ic_close_black_48dp);
            NavIconTint = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_navIconTint, ContextCompat.GetColor(Context, Resource.Color.searchBarNavIconTintColor));
            MenuIconTint = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_menuIconTint, ContextCompat.GetColor(Context, Resource.Color.searchBarMenuIconTintColor));
            SearchIconTint = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_searchIconTint, ContextCompat.GetColor(Context, Resource.Color.searchBarSearchIconTintColor));
            ArrowIconTint = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_backIconTint, ContextCompat.GetColor(Context, Resource.Color.searchBarBackIconTintColor));
            ClearIconTint = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_clearIconTint, ContextCompat.GetColor(Context, Resource.Color.searchBarClearIconTintColor));
            NavIconTintEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_navIconUseTint, true);
            MenuIconTintEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_menuIconUseTint, true);
            SearchIconTintEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_searchIconUseTint, true);
            ArrowIconTintEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_backIconUseTint, true);
            ClearIconTintEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_clearIconUseTint, true);
            BorderlessRippleEnabled = array.GetBoolean(Resource.Styleable.MaterialSearchBar_mt_borderlessRippleEnabled, false);

            //Text Related Attributes
            HintText = array.GetString(Resource.Styleable.MaterialSearchBar_mt_hint);
            PlaceholderText = array.GetString(Resource.Styleable.MaterialSearchBar_mt_placeholder);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                TextColor = Context.GetColorStateList(Resource.Color.searchBarTextColor);
                HintColor = Context.GetColorStateList(Resource.Color.searchBarHintColor);
                PlaceholderColor = Context.GetColorStateList(Resource.Color.searchBarPlaceholderColor);
            }

            TextCursorColor = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_textCursorTint, ContextCompat.GetColor(Context, Resource.Color.searchBarCursorColor));
            HighlightedTextColor = array.GetColor(Resource.Styleable.MaterialSearchBar_mt_highlightedTextColor, ContextCompat.GetColor(Context, Resource.Color.searchBarTextHighlightColor));

            Destiny = Resources.DisplayMetrics.Density;
            if (Adapter == null)
            {
                Adapter = new DefaultSuggestionsAdapter(LayoutInflater.From(Context));
                ((DefaultSuggestionsAdapter)Adapter).SetListener(this);
            }

            Adapter.SetMaxSuggestionsCount(MaxSuggestionCount);

            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.mt_recycler);
            recyclerView.SetAdapter(Adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));

            array.Recycle();

            //View References
            SearchBarCardView = FindViewById<CardView>(Resource.Id.mt_container);
            SuggestionDivider = FindViewById<View>(Resource.Id.mt_divider);
            MenuIcon = FindViewById<ImageView>(Resource.Id.mt_menu);
            ClearIcon = FindViewById<ImageView>(Resource.Id.mt_clear);
            SearchIcon = FindViewById<ImageView>(Resource.Id.mt_search);
            ArrowIcon = FindViewById<ImageView>(Resource.Id.mt_arrow);
            SearchEdit = FindViewById<EditText>(Resource.Id.mt_editText);
            PlaceHolder = FindViewById<TextView>(Resource.Id.mt_placeholder);
            InputContainer = FindViewById<LinearLayout>(Resource.Id.inputContainer);
            NavIcon = FindViewById<ImageView>(Resource.Id.mt_nav);
            FindViewById<ImageView>(Resource.Id.mt_clear).Click += Clear_Click;

            //Listeners
            SetOnClickListener(this);
            ArrowIcon.Click += ArrowIcon_Click;
            SearchIcon.Click += SearchIcon_Click;

            SearchEdit.FocusChange += SearchEdit_FocusChange;
            SearchEdit.EditorAction += SearchEdit_EditorAction;
            NavIcon.SetOnClickListener(this);

            if (OnFocusChangeListener != null)
            {
                OnFocusChangeListener.OnFocusChange(this, HasFocus);
            }

            PostSetup();
        }

        private void SearchIcon_Click(object sender, EventArgs e)
        {
            if (ListenerExists())
            {
                OnSearchActionListener.OnButtonClicked(BUTTON_SPEECH);
            }
        }

        private void ArrowIcon_Click(object sender, EventArgs e)
        {
            DisableSearch();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            SearchEdit.Text = "";
        }

        public void SetOnFocusChangeListener(IOnFocusChangeListener listener)
        {
            OnFocusChangeListener = listener;
        }

        private void SearchEdit_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (ListenerExists())
            {
                OnSearchActionListener.OnSearchConfirmed(!RemoveAccents ? SearchEdit.Text : SearchEdit.Text.RemoveDiacritics());
            }
            if (IsSuggestionsVisible)
            {
                HideSuggestionsList();
            }
            if (Adapter.GetType() == typeof(DefaultSuggestionsAdapter))
            {
                Adapter.AddSuggestion(!RemoveAccents ? SearchEdit.Text : SearchEdit.Text.RemoveDiacritics());
            }
        }

        private void SearchEdit_FocusChange(object sender, FocusChangeEventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
            if (e.HasFocus)
            {
                /*if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
                {*/
                imm.ShowSoftInput(SearchEdit, ShowFlags.Implicit);
                /*}
                else
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    imm.ShowSoftInput(SearchEdit, ShowFlags.Implicit);
#pragma warning restore CS0618 // Type or member is obsolete
                }*/
            }
            else
            {
                imm.HideSoftInputFromWindow(SearchEdit.WindowToken, 0);
            }
        }

        /**
         * Inflate menu for searchBar
         *
         * @param menuResource - menu resource
         */
        public void InflateMenu(int menuResource)
        {
            InflateMenuRequest(menuResource, -1);
        }

        /**
         * Inflate menu for searchBar with custom Icon
         *
         * @param menuResource - menu resource
         * @param icon         - icon resource id
         */
        public void InflateMenu(int menuResource, int icon)
        {
            InflateMenuRequest(menuResource, icon);
        }

        private void InflateMenuRequest(int menuResource, int iconResId)
        {
            int menuResource1 = menuResource;
            if (menuResource1 > 0)
            {
                ImageView menuIcon = FindViewById<ImageView>(Resource.Id.mt_menu);
                if (iconResId != -1)
                {
                    MenuIconRes = iconResId;
                    MenuIcon.SetImageResource(MenuIconRes);
                }

                MenuIcon.Visibility = ViewStates.Visible;
                MenuIcon.Click += MenuIcon_Click;
                PopupMenu = new PopupMenu(Context, menuIcon);
                PopupMenu.Inflate(menuResource);
                PopupMenu.Gravity = GravityFlags.Right;
            }
        }

        private void MenuIcon_Click(object sender, EventArgs e)
        {
            PopupMenu.Show();
        }

        private void PostSetup()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                SetupTextColors();
            }

            SetupRoundedSearchBarEnabled();
            SetupSearchBarColor();
            SetupIcons();
            SetupSearchEditText();
        }

        /**
         * Capsule shaped searchbar enabled
         * Only works on SDK V21+ due to odd behavior on lower
         */
        private void SetupRoundedSearchBarEnabled()
        {
            if (RoundedSearchBarEnabled && Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                SearchBarCardView.Radius = Resources.GetDimension(Resource.Dimension.corner_radius_rounded);
            }
            else
            {
                SearchBarCardView.Radius = Resources.GetDimension(Resource.Dimension.corner_radius_default);
            }
        }

        private void SetupSearchBarColor()
        {
            SearchBarCardView.SetCardBackgroundColor(SearchBarColor);
            SetupDividerColor();
        }

        private void SetupDividerColor()
        {
            SuggestionDivider.SetBackgroundColor(DividerColor);
        }

        private void SetupTextColors()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                SearchEdit.SetHintTextColor(HintColor);
                SearchEdit.SetTextColor(TextColor);
                PlaceHolder.SetTextColor(PlaceholderColor);
            }
        }

        /**
        * Setup editText coloring and drawables
        */
        private void SetupSearchEditText()
        {
            SetupCursorColor();
            SearchEdit.SetHighlightColor(HighlightedTextColor);
            SearchEdit.AddTextChangedListener(new TextWatcherChangeListener(this));

            if (!string.IsNullOrEmpty(HintText))
            {
                SearchEdit.Hint = HintText;
            }
            if (!string.IsNullOrEmpty(PlaceholderText))
            {
                ArrowIcon.Background = null;
                PlaceHolder.Text = PlaceholderText;
            }
        }

        private void SetupCursorColor()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    if (SearchEdit.TextCursorDrawable is InsetDrawable)
                    {
                        InsetDrawable insetDrawable = (InsetDrawable)SearchEdit.TextCursorDrawable;
                        insetDrawable.SetColorFilter(new BlendModeColorFilter(TextCursorColor, BlendMode.SrcIn));
                        SearchEdit.TextCursorDrawable = insetDrawable;
                    }

                    else if (SearchEdit.TextCursorDrawable is BitmapDrawable)
                    {
                        BitmapDrawable insetDrawable = (BitmapDrawable)SearchEdit.TextSelectHandle;
                        insetDrawable.SetColorFilter(new BlendModeColorFilter(TextCursorColor, BlendMode.SrcIn));
                        SearchEdit.TextSelectHandle = insetDrawable;
                    }

                    else if (SearchEdit.TextCursorDrawable is BitmapDrawable)
                    {
                        BitmapDrawable insetDrawable = (BitmapDrawable)SearchEdit.TextSelectHandleRight;
                        insetDrawable.SetColorFilter(new BlendModeColorFilter(TextCursorColor, BlendMode.SrcIn));
                        SearchEdit.TextSelectHandleRight = insetDrawable;
                    }

                    else if (SearchEdit.TextCursorDrawable is BitmapDrawable)
                    {
                        BitmapDrawable insetDrawable = (BitmapDrawable)SearchEdit.TextSelectHandleLeft;
                        insetDrawable.SetColorFilter(new BlendModeColorFilter(TextCursorColor, BlendMode.SrcIn));
                        SearchEdit.TextSelectHandleLeft = insetDrawable;
                    }
                }
                else
                {
                    var field = Class.FromType(typeof(TextView)).GetDeclaredField("mEditor");
                    field.Accessible = true;
                    var editor = field.Get(SearchEdit);

                    field = Class.FromType(typeof(TextView)).GetDeclaredField("mCursorDrawableRes");
                    field.Accessible = true;
                    int cursorDrawableRes = field.GetInt(SearchEdit);
                    var cursorDrawable = ContextCompat.GetDrawable(Context, cursorDrawableRes).Mutate();

#pragma warning disable CS0618 // Type or member is obsolete
                    cursorDrawable.SetColorFilter(TextCursorColor, PorterDuff.Mode.SrcIn);

#pragma warning restore CS0618 // Type or member is obsolete

                    Drawable[] drawables = { cursorDrawable, cursorDrawable };
                    field = Class.FromType(typeof(TextView)).GetDeclaredField("mCursorDrawable");
                    field.Accessible = true;
                    field.Set(editor, drawables);
                }
            }
            catch (NoSuchFieldException e)
            {
                e.PrintStackTrace();
            }
            catch (IllegalAccessException e)
            {
                e.PrintStackTrace();
            }
        }

        //Setup Icon Colors And Drawables
        private void SetupIcons()
        {
            //Drawables
            //Animated Nav Icon
            NavIconRes = Resource.Drawable.ic_menu_animated;
            NavIcon.SetImageResource(NavIconRes);
            SetNavButtonEnabled(NavButtonEnabled);

            //Menu
            if (PopupMenu == null)
            {
                FindViewById<ImageView>(Resource.Id.mt_menu).Visibility = ViewStates.Gone;
            }

            //Search
            SetSpeechMode(SpeechMode);

            //Arrow
            ArrowIcon.SetImageResource(ArrowIconRes);

            //Clear
            ClearIcon.SetImageResource(ClearIconRes);

            //Colors
            SetupNavIconTint();
            SetupMenuIconTint();
            SetupSearchIconTint();
            SetupArrowIconTint();
            SetupClearIconTint();
            SetupIconRippleStyle();
        }

        private void SetupNavIconTint()
        {
            if (NavIconTintEnabled)
            {
                NavIcon.SetColorFilter(NavIconTint, PorterDuff.Mode.SrcIn);
            }
            else
            {
                NavIcon.ClearColorFilter();
            }
        }

        private void SetupMenuIconTint()
        {
            if (MenuIconTintEnabled)
            {
                MenuIcon.SetColorFilter(MenuIconTint, PorterDuff.Mode.SrcIn);
            }
            else
            {
                MenuIcon.ClearColorFilter();
            }
        }

        private void SetupSearchIconTint()
        {
            if (SearchIconTintEnabled)
            {
                SearchIcon.SetColorFilter(SearchIconTint, PorterDuff.Mode.SrcIn);
            }
            else
            {
                SearchIcon.ClearColorFilter();
            }
        }

        private void SetupArrowIconTint()
        {
            if (ArrowIconTintEnabled)
            {
                ArrowIcon.SetColorFilter(ArrowIconTint, PorterDuff.Mode.SrcIn);
            }
            else
            {
                ArrowIcon.ClearColorFilter();
            }
        }

        private void SetupClearIconTint()
        {
            if (ClearIconTintEnabled)
            {
                ClearIcon.SetColorFilter(ClearIconTint, PorterDuff.Mode.SrcIn);
            }
            else
            {
                ClearIcon.ClearColorFilter();
            }
        }

        private void SetupIconRippleStyle()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            {
                TypedValue rippleStyle = new TypedValue();
                if (BorderlessRippleEnabled && Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackgroundBorderless, rippleStyle, true);
                }
                else
                {
                    Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, rippleStyle, true);
                }
                NavIcon.SetBackgroundResource(rippleStyle.ResourceId);
                SearchIcon.SetBackgroundResource(rippleStyle.ResourceId);
                MenuIcon.SetBackgroundResource(rippleStyle.ResourceId);
                ArrowIcon.SetBackgroundResource(rippleStyle.ResourceId);
                ClearIcon.SetBackgroundResource(rippleStyle.ResourceId);
            }
            /*else
            {
                Console.WriteLine("setupIconRippleStyle() Only Available On SDK Versions Higher Than 16!");
            }*/
        }

        /**
         * Register listener for search bar callbacks.
         *
         * @param onSearchActionListener the callback listener
         */
        public void SetOnSearchActionListener(IOnSearchActionListener onSearchActionListener)
        {
            OnSearchActionListener = onSearchActionListener;
        }

        /**
         * Hides search input and close arrow
         */
        public void DisableSearch()
        {
            AnimateNavIcon(false);
            IsSearchEnabled = false;
            var out_ = AnimationUtils.LoadAnimation(Context, Resource.Animation.fade_out);
            var in_ = AnimationUtils.LoadAnimation(Context, Resource.Animation.fade_in_right);
            out_.SetAnimationListener(this);
            SearchIcon.Visibility = ViewStates.Visible;
            InputContainer.StartAnimation(out_);
            SearchIcon.StartAnimation(in_);

            if (!string.IsNullOrEmpty(PlaceholderText))
            {
                PlaceHolder.Visibility = ViewStates.Visible;
                PlaceHolder.StartAnimation(in_);
            }
            if (ListenerExists())
            {
                OnSearchActionListener.OnSearchStateChanged(false);
            }
            if (IsSuggestionsVisible)
            {
                AnimateSuggestions(GetListHeight(false), 0);
            }
        }

        /**
         * Shows search input and close arrow
         */
        public void EnableSearch()
        {
            if (IsSearchEnabled)
            {
                OnSearchActionListener.OnSearchStateChanged(true);
                SearchEdit.RequestFocus();
                return;
            }
            AnimateNavIcon(true);
            Adapter.NotifyDataSetChanged();
            IsSearchEnabled = true;
            var left_in = AnimationUtils.LoadAnimation(Context, Resource.Animation.fade_in_left);
            var left_out = AnimationUtils.LoadAnimation(Context, Resource.Animation.fade_out_left);
            left_in.SetAnimationListener(this);
            PlaceHolder.Visibility = ViewStates.Gone;
            InputContainer.Visibility = ViewStates.Visible;
            InputContainer.StartAnimation(left_in);
            if (ListenerExists())
            {
                OnSearchActionListener.OnSearchStateChanged(true);
            }
            SearchIcon.StartAnimation(left_out);
        }

        public void SetNavIcon(int navIconResId, int navToBackId, int backToNavId)
        {
            NavIconRes = navIconResId;
            NavToBackRes = navToBackId;
            BackToNavRes = backToNavId;
            NavIcon.SetImageResource(navIconResId);
        }

        private void AnimateNavIcon(bool menuState)
        {
            if (menuState)
            {
                if (NavToBackRes != null)
                {
                    NavIcon.SetImageResource(NavToBackRes.Value);
                }
                else
                {
                    NavIcon.SetImageResource(Resource.Drawable.ic_menu_animated);
                }
            }
            else
            {
                if (BackToNavRes != null)
                {
                    NavIcon.SetImageResource(BackToNavRes.Value);
                }
                else
                {
                    NavIcon.SetImageResource(Resource.Drawable.ic_back_animated);
                }
            }
            if (NavIcon.Drawable is AnimatedVectorDrawable a)
            {
                a.Start();
            }
        }

        private void AnimateSuggestions(int from, int to)
        {
            IsSuggestionsVisible = to > 0;

            RecyclerView suggestionsList = FindViewById<RecyclerView>(Resource.Id.mt_recycler);

            ViewGroup.LayoutParams lp = suggestionsList.LayoutParameters;
            if (to == 0 && lp.Height == 0)
            {
                return;
            }

            FindViewById(Resource.Id.mt_divider).Visibility = to > 0 ? ViewStates.Visible : ViewStates.Gone;

            ValueAnimator animator = ValueAnimator.OfInt(from, to);
            animator.SetDuration(SuggestionAnimationSpeed);
            animator.AddUpdateListener(new AnimatorUpdateListener(lp, suggestionsList));
            if (Adapter.ItemCount > 0)
            {
                animator.Start();
            }
            LastSuggestionHeight = to;
        }

        public void ShowSuggestionsList()
        {
            AnimateSuggestions(LastSuggestionHeight, GetListHeight(false));
        }

        public void HideSuggestionsList()
        {
            AnimateSuggestions(GetListHeight(false), 0);
        }

        public void ClearSuggestions()
        {
            if (IsSuggestionsVisible)
            {
                AnimateSuggestions(GetListHeight(false), 0);
            }
            Adapter.ClearSuggestions();
        }

        /**
         * Set Menu Icon Drawable
         *
         * @param menuIconResId icon resource id
         */
        public void SetMenuIcon(int menuIconResId)
        {
            MenuIconRes = menuIconResId;
            MenuIcon.SetImageResource(MenuIconRes);
        }

        /**
         * Set search icon drawable
         *
         * @param searchIconResId icon resource id
         */
        public void SetSearchIcon(int searchIconResId)
        {
            SearchIconRes = searchIconResId;
            SearchIcon.SetImageResource(searchIconResId);
        }

        /**
         * Set back arrow icon drawable
         *
         * @param arrowIconResId icon resource id
         */
        public void SetArrowIcon(int arrowIconResId)
        {
            ArrowIconRes = arrowIconResId;
            ArrowIcon.SetImageResource(ArrowIconRes);
        }

        /**
         * Set clear icon drawable
         *
         * @param clearIconResId icon resource id
         */
        public void SetClearIcon(int clearIconResId)
        {
            ClearIconRes = clearIconResId;
            ClearIcon.SetImageResource(ClearIconRes);
        }

        /**
         * Set the tint color of the navigation icon
         *
         * @param navIconTint nav icon color
         */
        public void SetNavIconTint(Color navIconTint)
        {
            NavIconTint = navIconTint;
            SetupNavIconTint();
        }

        /**
         * Set the tint color of the menu icon
         *
         * @param menuIconTint menu icon color
         */
        public void SetMenuIconTint(Color menuIconTint)
        {
            MenuIconTint = menuIconTint;
            SetupMenuIconTint();
        }

        /**
         * Set the tint color of the search/speech icon
         *
         * @param searchIconTint search icon color
         */
        public void SetSearchIconTint(Color searchIconTint)
        {
            SearchIconTint = searchIconTint;
            SetupSearchIconTint();
        }

        /**
         * Set the tint color of the back arrow icon
         *
         * @param arrowIconTint arrow icon color
         */
        public void SetArrowIconTint(Color arrowIconTint)
        {
            ArrowIconTint = arrowIconTint;
            SetupArrowIconTint();
        }

        /**
         * Set the tint color of the clear icon
         *
         * @param clearIconTint clear icon tint
         */
        public void SetClearIconTint(Color clearIconTint)
        {
            ClearIconTint = clearIconTint;
            SetupClearIconTint();
        }

        /**
         * Show a borderless ripple(circular) when icon is pressed
         * Borderless only available on SDK V21+
         *
         * @param borderlessRippleEnabled true for borderless, false for default
         */
        public void SetIconRippleStyle(bool borderlessRippleEnabled)
        {
            BorderlessRippleEnabled = borderlessRippleEnabled;
            SetupIconRippleStyle();
        }

        /**
         * Sets search bar hintText
         *
         * @param hintText hintText text
         */
        public void SetHint(string hintText)
        {
            HintText = hintText;
            SearchEdit.Hint = hintText;
        }

        /**
         * sets the speechMode for the search bar.
         * If set to true, microphone icon will display instead of the search icon.
         * Also clicking on this icon will trigger the callback method onButtonClicked()
         *
         * @param speechMode enable speech
         * @see #BUTTON_SPEECH
         * @see OnSearchActionListener#onButtonClicked(int)
         */
        public void SetSpeechMode(bool speechMode)
        {
            SpeechMode = speechMode;

            SearchIcon.Clickable = speechMode;
            if (speechMode)
            {
                SearchIcon.SetImageResource(SpeechIconRes);
            }
            else
            {
                SearchIcon.SetImageResource(SearchIconRes);
            }
        }

        /**
         * True if MaterialSearchBar is in speech mode
         *
         * @return speech mode
         */
        public bool IsSpeechModeEnabled()
        {
            return SpeechMode;
        }

        /**
         * Specifies the maximum number of search queries stored until the activity is destroyed
         *
         * @param maxSuggestionsCount maximum queries
         */
        public void SetMaxSuggestionCount(int maxSuggestionsCount)
        {
            MaxSuggestionCount = maxSuggestionsCount;
            Adapter.SetMaxSuggestionsCount(maxSuggestionsCount);
        }

        /**
         * Sets a custom adapter for suggestions list view.
         *
         * @param suggestionAdapter customized adapter
         */
        public void SetCustomSuggestionAdapter(SuggestionsAdapter suggestionAdapter)
        {
            Adapter = suggestionAdapter;
            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.mt_recycler);
            recyclerView.SetAdapter(Adapter);
        }

        /**
         * Returns the last search queries.
         * The queries are stored only for the duration of one activity session.
         * When the activity is destroyed, the queries will be deleted.
         * To save queries, use the method getLastSuggestions().
         * To recover the queries use the method setLastSuggestions().
         * <p><b color="red">List< String >  will be returned if You don't use custom adapter.</b></p>
         *
         * @return array with the latest search queries
         * @see #setLastSuggestions(List)
         * @see #setMaxSuggestionCount(int)
         */
        public List<string> GetLastSuggestions()
        {
            return Adapter.GetSuggestions();
        }

        /**
         * Sets the array of recent search queries.
         * It is advisable to save the queries when the activity is destroyed
         * and call this method when creating the activity.
         * <p><b color="red">Pass a List< String > if You don't use custom adapter.</b></p>
         *
         * @param suggestions an array of queries
         * @see #getLastSuggestions()
         * @see #setMaxSuggestionCount(int)
         */
        public void SetLastSuggestions(List<string> suggestions)
        {
            Adapter.SetSuggestions(suggestions);
        }

        /**
         * Changes the array of recent search queries with animation.
         * <p><b color="red">Pass a List< String >  if You don't use custom adapter.</b></p>
         *
         * @param suggestions an array of queries
         */
        public void UpdateLastSuggestions(List<string> suggestions)
        {
            int startHeight = GetListHeight(false);
            if (suggestions.Count > 0)
            {
                var newSuggestions = new List<string>(suggestions);
                Adapter.SetSuggestions(newSuggestions);
                AnimateSuggestions(startHeight, GetListHeight(false));
            }
            else
            {
                AnimateSuggestions(startHeight, 0);
            }
        }

        /**
         * Allows you to intercept the suggestions click event
         * <p><b color="red">This method will not work with custom Suggestion Adapter</b></p>
         *
         * @param listener click listener
         */
        public void SetSuggestionsClickListener(IOnItemViewClickListener listener)
        {
            if (Adapter.GetType() == typeof(DefaultSuggestionsAdapter))
            {
                ((DefaultSuggestionsAdapter)Adapter).SetListener(listener);
            }
        }

        /**
         * Set search input text color
         *
         * @param textColor text color
         */
        public void SetTextColor(ColorStateList textColor)
        {
            TextColor = textColor;
            SetupTextColors();
        }

        /**
         * Set text input hintText color
         *
         * @param hintColor text hintText color
         */
        public void SetTextHintColor(ColorStateList hintColor)
        {
            HintColor = hintColor;
            SetupTextColors();
        }

        /**
         * Set placeholder text color
         *
         * @param placeholderColor placeholder color
         */
        public void SetPlaceHolderColor(ColorStateList placeholderColor)
        {
            PlaceholderColor = placeholderColor;
            SetupTextColors();
        }

        /**
         * Set the color of the highlight when text is selected
         *
         * @param highlightedTextColor selected text highlight color
         */
        public void SetTextHighlightColor(Color highlightedTextColor)
        {
            HighlightedTextColor = highlightedTextColor;
            SearchEdit.SetHighlightColor(highlightedTextColor);
        }

        public void SetDividerColor(Color dividerColor)
        {
            DividerColor = dividerColor;
            SetupDividerColor();
        }

        /**
         * Set navigation drawer menu icon enabled
         *
         * @param navButtonEnabled icon enabled
         */
        public void SetNavButtonEnabled(bool navButtonEnabled)
        {
            NavButtonEnabled = navButtonEnabled;
            NavIcon.Clickable = navButtonEnabled;
            if (navButtonEnabled)
            {
                NavIcon.Visibility = ViewStates.Visible;
                NavIcon.Clickable = true;
                ArrowIcon.Visibility = ViewStates.Gone;
            }
            else
            {
                NavIcon.Visibility = ViewStates.Gone;
                NavIcon.Clickable = false;
                ArrowIcon.Visibility = ViewStates.Visible;
            }
            NavIcon.RequestLayout();
            PlaceHolder.RequestLayout();
            ArrowIcon.RequestLayout();
        }

        /**
         * Enable capsule shaped SearchBar (API 21+)
         *
         * @param roundedSearchBarEnabled capsule shape enabled
         * @
         */
        public void SetRoundedSearchBarEnabled(bool roundedSearchBarEnabled)
        {
            RoundedSearchBarEnabled = roundedSearchBarEnabled;
            SetupRoundedSearchBarEnabled();
        }

        /**
         * Set CardView elevation
         *
         * @param elevation desired elevation
         */
        public void SetCardViewElevation(int elevation)
        {
            FindViewById<CardView>(Resource.Id.mt_container).CardElevation = elevation;
        }

        /**
         * Get search text
         *
         * @return text
         */
        public string GetText()
        {
            return SearchEdit.Text;
        }

        /**
         * Set search text
         *
         * @param text text
         */
        public void SetText(string text)
        {
            SearchEdit.Text = text;
        }

        /**
         * Add text watcher to searchbar's EditText
         *
         * @param textWatcher textWatcher to add
         */
        public void AddTextChangeListener(ITextWatcher textWatcher)
        {
            SearchEdit.AddTextChangedListener(textWatcher);
        }

        public EditText GetSearchEditText()
        {
            return SearchEdit;
        }

        /**
         * Set the place holder text
         *
         * @param placeholder placeholder text
         */
        public void SetPlaceHolder(string placeholder)
        {
            PlaceholderText = placeholder;
            PlaceHolder.Text = placeholder;
        }

        private bool ListenerExists()
        {
            return OnSearchActionListener != null;
        }

        public void OnClick(View v)
        {
            int id = v.Id;
            if (id == Id)
            {
                if (!IsSearchEnabled)
                {
                    EnableSearch();
                }
            }
            else if (id == Resource.Id.mt_arrow)
            {
                DisableSearch();
            }
            else if (id == Resource.Id.mt_search)
            {
                if (ListenerExists())
                {
                    OnSearchActionListener.OnButtonClicked(BUTTON_SPEECH);
                }
            }
            else if (id == Resource.Id.mt_clear)
            {
                SearchEdit.Text = "";
            }
            else if (id == Resource.Id.mt_menu)
            {
                PopupMenu.Show();
            }
            else if (id == Resource.Id.mt_nav)
            {
                int button = IsSearchEnabled ? BUTTON_BACK : BUTTON_NAVIGATION;
                if (IsSearchEnabled)
                {
                    DisableSearch();
                }
                if (ListenerExists())
                {
                    OnSearchActionListener.OnButtonClicked(button);
                }
            }
        }

        public void OnAnimationStart(Animation animation)
        {

        }

        public void OnAnimationEnd(Animation animation)
        {
            if (!IsSearchEnabled)
            {
                InputContainer.Visibility = ViewStates.Gone;
                SearchEdit.Text = "";
            }
            else
            {
                SearchIcon.Visibility = ViewStates.Gone;
                SearchEdit.RequestFocus();
                if (!IsSuggestionsVisible && IsSuggestionsEnabled)
                {
                    ShowSuggestionsList();
                }
            }
        }

        public void OnAnimationRepeat(Animation animation)
        {

        }

        /**
         * For calculate the height change when item delete or add animation
         * false is return the full height of item,
         * true is return the height of position subtraction one
         *
         * @param isSubtraction is subtraction enabled
         */
        private int GetListHeight(bool isSubtraction)
        {
            if (!isSubtraction)
            {
                return (int)(Adapter.GetListHeight() * Destiny);
            }
            return (int)((Adapter.ItemCount - 1) * Adapter.GetSingleViewHeight() * Destiny);
        }

        public void IOnItemClickListener(int position, View v)
        {
            SearchEdit.Text = v.Tag.ToString();
        }

        public void IOnItemDeleteListener(int position, View v)
        {
            /*Order of two line should't be change,
            because should calculate the height of item first*/
            AnimateSuggestions(GetListHeight(false), GetListHeight(true));
            Adapter.DeleteSuggestion(position, v.Tag.ToString());
        }

        protected override IParcelable OnSaveInstanceState()
        {
            SavedState savedState = new SavedState(base.OnSaveInstanceState())
            {
                IsSearchBarVisible = IsSearchEnabled ? VIEW_VISIBLE : VIEW_INVISIBLE,
                SuggestionsVisible = IsSuggestionsVisible ? VIEW_VISIBLE : VIEW_INVISIBLE,
                SpeechMode = IsSpeechModeEnabled() ? VIEW_VISIBLE : VIEW_INVISIBLE,
                NavIconResId = NavIconRes,
                SearchIconRes = SearchIconRes,
                Suggestions = GetLastSuggestions(),
                MaxSuggestions = MaxSuggestionCount
            };
            if (HintText != null)
            {
                savedState.Hint = HintText;
            }
            return savedState;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            SavedState savedState = (SavedState)state;
            base.OnRestoreInstanceState(savedState.SuperState);
            IsSearchEnabled = savedState.IsSearchBarVisible == VIEW_VISIBLE;
            IsSuggestionsVisible = savedState.SuggestionsVisible == VIEW_VISIBLE;
            SetLastSuggestions(savedState.Suggestions);
            if (IsSuggestionsEnabled)
            {
                AnimateSuggestions(0, GetListHeight(false));
            }
            if (IsSearchEnabled)
            {
                InputContainer.Visibility = ViewStates.Visible;
                PlaceHolder.Visibility = ViewStates.Gone;
                SearchIcon.Visibility = ViewStates.Gone;
            }
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.KeyCode == Keycode.Back && IsSearchEnabled)
            {
                AnimateSuggestions(GetListHeight(false), 0);
                DisableSearch();
                return true;
            }
            return base.DispatchKeyEvent(e);
        }
    }
}