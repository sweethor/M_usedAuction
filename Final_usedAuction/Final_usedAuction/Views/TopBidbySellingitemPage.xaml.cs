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
    public partial class TopBidbySellingitemPage : ContentPage
    {
        TopBidbySellingitemViewModel _topBidbySellingitemViewModel;

        public TopBidbySellingitemPage()
        {
            InitializeComponent();

            BindingContext = _topBidbySellingitemViewModel = new TopBidbySellingitemViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _topBidbySellingitemViewModel.OnAppearing();
        }
    }
}