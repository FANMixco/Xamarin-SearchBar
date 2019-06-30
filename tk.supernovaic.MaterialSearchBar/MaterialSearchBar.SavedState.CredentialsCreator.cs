using Android.OS;
using Java.Lang;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        private partial class SavedState
        {
            private class CredentialsCreator : Object, IParcelableCreator
            {
                public Object CreateFromParcel(Parcel source)
                {
                    return new SavedState(source);
                }

                public Object[] NewArray(int size)
                {
                    return new SavedState[size];
                }
            }
        }
    }
}