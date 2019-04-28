using System;
using Xamarin.Forms;

namespace PageSwipe
{
    public class ProgressCarouselPage : CarouselPage
    {
        public int ActiveIndex { get; set; }
        public int NextIndex { get; set; }

        public event EventHandler<double> OnSwipeProgressUpdated;

        public void SwipeProgress(double progress)
        {
            OnSwipeProgressUpdated?.Invoke(this, progress);
        }
    }
}
