using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotiPage : ContentPage
    {
        NotiViewModel _viewModel;

        public NotiPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotiViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}