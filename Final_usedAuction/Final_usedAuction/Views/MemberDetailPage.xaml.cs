using Final_usedAuction.ViewModels;
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
    public partial class MemberDetailPage : ContentPage
    {
        public MemberDetailPage()
        {
            InitializeComponent();
            BindingContext = new MemberDetailViewModel();
        }
    }
}