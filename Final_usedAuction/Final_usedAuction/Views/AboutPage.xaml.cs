using System;
using System.ComponentModel;
using Xamarin.Forms;
using Final_usedAuction.ViewModels;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Final_usedAuction.Models;

namespace Final_usedAuction.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel _aboutViewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = _aboutViewModel = new AboutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _aboutViewModel.OnAppearing();
        }
    }
}