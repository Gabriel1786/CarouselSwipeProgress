using System;
using Android.Content;
using Android.Support.V4.View;
using PageSwipe;
using PageSwipe.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ProgressCarouselPage), typeof(CarouselCustomRenderer))]
namespace PageSwipe.Droid
{
    public class CarouselCustomRenderer : CarouselPageRenderer
    {
        private const float thresholdOffset = 0.5f;
        private bool checkDirection;
        private bool dragStarted;
        private bool draggingBack;

        private Context context;
        private int nextActiveIndex;
        private ProgressCarouselPage CarouselContainer => Element as ProgressCarouselPage;

        public CarouselCustomRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CarouselPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null || ChildCount < 1)
            {
                return;
            }

            if (!(GetChildAt(0) is ViewPager pager))
            {
                return;
            }

            pager.PageScrolled += PagerPageScrolled;
            pager.PageSelected += PagerPageSelected;
            pager.PageScrollStateChanged += PagerPageScrollStateChanged;
        }

        void PagerPageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            if (checkDirection)
            {
                if (thresholdOffset > e.PositionOffset)
                {
                    CarouselContainer.NextIndex = CarouselContainer.ActiveIndex + 1;
                    draggingBack = false;
                }
                else
                {
                    CarouselContainer.NextIndex = CarouselContainer.ActiveIndex - 1;
                    draggingBack = true;
                }
            }

            checkDirection = false;

            if (e.PositionOffset == 0)
            {
                checkDirection = true;
            }

            if (!checkDirection)
            {
                var progress = e.PositionOffset;
                if (draggingBack)
                {
                    progress = (progress - 1) * -1;
                }

                Console.WriteLine($"{progress}");
                CarouselContainer.SwipeProgress(progress);
            }
        }

        void PagerPageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            if (e.State == ViewPager.ScrollStateDragging)
            {
                if (!dragStarted)
                {
                    dragStarted = true;
                    checkDirection = true;
                }
            }

            if (e.State == ViewPager.ScrollStateIdle)
            {
                dragStarted = false;
                CarouselContainer.ActiveIndex = nextActiveIndex;
            } 
        }

        void PagerPageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            Console.WriteLine($"PageSelected: {e.Position}");
            nextActiveIndex = e.Position;
        }
    }
}
