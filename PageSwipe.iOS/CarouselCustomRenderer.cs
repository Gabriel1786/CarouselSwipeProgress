using System;
using System.Linq;
using PageSwipe;
using PageSwipe.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCarouselPage), typeof(CarouselCustomRenderer))]
namespace PageSwipe.iOS
{
    public class CarouselCustomRenderer : CarouselPageRenderer
    {
        private const float thresholdOffset = 0.5f;
        private bool checkDirection;
        private bool dragStarted;
        private bool draggingBack;
        private UIScrollView scrollView;
        private CustomCarouselPage CarouselContainer => Element as CustomCarouselPage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (scrollView != null)
            {
                scrollView.Scrolled -= ScrollViewScrolled;
            }

            if (e.NewElement == null || View?.Subviews.Count() < 1)
            {
                return;
            }

            scrollView = View.Subviews[0] as UIScrollView;
            scrollView.Scrolled += ScrollViewScrolled;
            scrollView.Bounces = false;
        }

        void ScrollViewScrolled(object sender, EventArgs e)
        {
            var scroll = sender as UIScrollView;
            if (scroll.Frame.Width == 0)
            {
                return;
            }

            var progress = scroll.ContentOffset.X / scroll.Frame.Width;
            while (progress > 1)
            {
                progress -= 1;
            }

            if (checkDirection)
            {
                if (thresholdOffset > progress)
                {
                    CarouselContainer.NextIndex = CarouselContainer.ActiveIndex + 1;
                    draggingBack = false;
                }
                else
                {
                    CarouselContainer.NextIndex = CarouselContainer.ActiveIndex - 1;
                    draggingBack = true;
                }

                checkDirection = false;
            }

            if (draggingBack)
            {
                progress = (progress - 1) * -1;
            }

            if (progress != 1 && progress != 0)
            {
                Console.WriteLine($"{progress}");
                CarouselContainer.SwipeProgress(progress);
            }

            var index = (int)(scroll.ContentOffset.X / scroll.Frame.Width);
            if (progress == 0 || progress == 1 || CarouselContainer.ActiveIndex - 2 == index || CarouselContainer.ActiveIndex + 2 == index)
            {
                checkDirection = true;
                CarouselContainer.ActiveIndex = index;
            }
        }
    }
}
