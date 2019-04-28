using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PageSwipe
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class WeatherMainPage : CustomCarouselPage
    {
        public WeatherMainPage()
        {
            InitializeComponent();

            var t1 = new ForecastPage(new WeatherModel { Name = "Cloudy", Temperature = "20" }) { BackgroundColor = Color.Gray };
            var t2 = new ForecastPage(new WeatherModel { Name = "Sunny", Temperature = "40" }) { BackgroundColor = Color.LightGoldenrodYellow };
            var t3 = new ForecastPage(new WeatherModel { Name = "Rainy", Temperature = "15" }) { BackgroundColor = Color.DarkSlateGray };
            var t4 = new ForecastPage(new WeatherModel { Name = "Snowy", Temperature = "-15" }) { BackgroundColor = Color.NavajoWhite };
            var t5 = new ForecastPage(new WeatherModel { Name = "Cloudy", Temperature = "20" }) { BackgroundColor = Color.AliceBlue };
            var t6 = new ForecastPage(new WeatherModel { Name = "Sunny", Temperature = "40" }) { BackgroundColor = Color.ForestGreen };
            var t7 = new ForecastPage(new WeatherModel { Name = "Rainy", Temperature = "15" }) { BackgroundColor = Color.HotPink };
            var t8 = new ForecastPage(new WeatherModel { Name = "Snowy", Temperature = "-15" }) { BackgroundColor = Color.Goldenrod };

            OnSwipeProgressUpdated += WeatherMainPageOnSwipeProgressUpdated;

            Children.Add(t1);
            Children.Add(t2);
            Children.Add(t3);
            Children.Add(t4);
            Children.Add(t5);
            Children.Add(t6);
            Children.Add(t7);
            Children.Add(t8);
        }

        void WeatherMainPageOnSwipeProgressUpdated(object sender, double progress)
        {
            if (NextIndex >= 0 && NextIndex < Children.Count)
            {
                var leftForecasePage = Children[NextIndex] as ForecastPage;
                leftForecasePage.AnimateUp(progress);
            }

            var forecastPage = Children[ActiveIndex] as ForecastPage;
            forecastPage.AnimateDown(progress);
        }
    }
}
