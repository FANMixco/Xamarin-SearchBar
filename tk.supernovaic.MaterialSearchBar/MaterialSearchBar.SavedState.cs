using System.Collections.Generic;
using Android.OS;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        private partial class SavedState : BaseSavedState, IParcelable
        {
            public int IsSearchBarVisible { get; set; }
            public int SuggestionsVisible { get; set; }
            public int SpeechMode { get; set; }
            public int SearchIconRes { get; set; }
            public int NavIconResId { get; set; }
            public string Hint { get; set; }
            public List<string> Suggestions { get; set; }
            public int MaxSuggestions { get; set; }

            public SavedState(IParcelable superState) : base(superState)
            {
            }

            public SavedState(Parcel source) : base(source)
            {
                IsSearchBarVisible = source.ReadInt();
                SuggestionsVisible = source.ReadInt();
                SpeechMode = source.ReadInt();

                NavIconResId = source.ReadInt();
                SearchIconRes = source.ReadInt();
                Hint = source.ReadString();
                Suggestions = source.ReadArrayList(null) as List<string>;
                MaxSuggestions = source.ReadInt();
            }

            public override void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
            {
                base.WriteToParcel(dest, flags);
                dest.WriteInt(IsSearchBarVisible);
                dest.WriteInt(SuggestionsVisible);
                dest.WriteInt(SpeechMode);

                dest.WriteInt(SearchIconRes);
                dest.WriteInt(NavIconResId);
                dest.WriteString(Hint);
                dest.WriteList(Suggestions);
                dest.WriteInt(MaxSuggestions);
            }
        }
    }
}