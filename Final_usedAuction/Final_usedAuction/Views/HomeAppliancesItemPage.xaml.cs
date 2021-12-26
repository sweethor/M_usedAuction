using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.ViewModels.ViewCateModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeAppliancesItemPage : ContentPage
    {
        HomeAppliancesItemsViewModel _homeAppliancesItemsViewModel;

        public HomeAppliancesItemPage()
        {
            InitializeComponent();

            BindingContext = _homeAppliancesItemsViewModel = new HomeAppliancesItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _homeAppliancesItemsViewModel.OnAppearing();
        }
    }
}