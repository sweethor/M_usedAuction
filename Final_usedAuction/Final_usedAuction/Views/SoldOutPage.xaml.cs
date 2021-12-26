using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.ViewModels.ViewMyPageModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoldOutPage : ContentPage
    {
        SoldOutViewModel _soldOutViewModel;

        public SoldOutPage()
        {
            InitializeComponent();

            BindingContext = _soldOutViewModel = new SoldOutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _soldOutViewModel.OnAppearing();
        }
    }
}