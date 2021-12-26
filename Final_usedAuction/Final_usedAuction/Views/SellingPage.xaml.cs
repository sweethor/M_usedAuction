using Final_usedAuction.ViewModels.ViewMyPageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{   

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellingPage : ContentPage
    {
        SellingViewModel _sellingViewModel;


        public SellingPage()
        {
            InitializeComponent();

            BindingContext = _sellingViewModel = new SellingViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _sellingViewModel.OnAppearing();
        }
    }
}