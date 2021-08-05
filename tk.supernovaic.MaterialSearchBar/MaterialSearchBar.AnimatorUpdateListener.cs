using Android.Animation;
using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace tk.supernovaic.MaterialSearchBar
{
    public partial class MaterialSearchBar
    {
        private class AnimatorUpdateListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
        {
            private readonly ViewGroup.LayoutParams LP;
            private readonly RecyclerView Last;

            public AnimatorUpdateListener(ViewGroup.LayoutParams lp, RecyclerView last)
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