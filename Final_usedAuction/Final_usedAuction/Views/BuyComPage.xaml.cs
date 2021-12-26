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
    public partial class BuyComPage : ContentPage
    {
        BuyComViewModel _buyComViewModel;

        public BuyComPage()
        {
            InitializeComponent();

            BindingContext = _buyComViewModel = new BuyComViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _buyComViewModel.OnAppearing();
        }
    }
}