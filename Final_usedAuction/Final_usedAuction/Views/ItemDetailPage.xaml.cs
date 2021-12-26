using Final_usedAuction.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Final_usedAuction.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}