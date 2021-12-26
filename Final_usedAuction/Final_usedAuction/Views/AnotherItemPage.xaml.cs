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
    public partial class AnotherItemPage : ContentPage
    {
        AnotherItemsViewModel _anotherItemsViewModel;

        public AnotherItemPage()
        {
            InitializeComponent();

            BindingContext = _anotherItemsViewModel = new AnotherItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _anotherItemsViewModel.OnAppearing();
        }
    }
}