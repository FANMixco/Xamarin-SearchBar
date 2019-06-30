using Android.Animation;
using Android.Views;
using Android.Widget;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        private class AnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
        {
            private readonly ViewGroup.LayoutParams LP;
            private readonly RelativeLayout Last;
            public AnimatorUpdateListener(ViewGroup.LayoutParams lp, RelativeLayout last)
            {
                LP = lp;
                Last = last;
            }
            public void OnAnimationUpdate(ValueAnimator animation)
            {
                LP.Height = (int)animation.AnimatedValue;
                Last.LayoutParameters = LP;
            }
        }
    }
}