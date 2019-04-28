using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PageSwipe
{
    public partial class ForecastPage : ContentPage
    {
        private double animationTranslationYDistance = -370;

        public WeatherModel WeatherModel { get; set; }

        public ForecastPage(WeatherModel WeatherModel)
        {
            InitializeComponent();

            BindingContext = this.WeatherModel = WeatherModel;

            container.TranslationY = animationTranslationYDistance;
        }

        public void AnimateUp(double progress)
        {
            container.TranslationY = animationTranslationYDistance * progress; 
        }

        public void AnimateDown(double progress)
        {
            container.TranslationY = animationTranslationYDistance - (animationTranslationYDistance * progress);
        }
    }
}
