using Final_usedAuction.Models;
using Final_usedAuction.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateItemPage : ContentPage
    {
        public UpdateItemPage()
        {
            InitializeComponent();
            BindingContext = new UpdateItemViewModel();
        }
    }
}