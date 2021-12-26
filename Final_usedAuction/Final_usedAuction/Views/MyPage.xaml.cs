using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.Models;
using Final_usedAuction.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPage : ContentPage
    {
        

        public MyPage()
        {
            InitializeComponent();
            this.BindingContext = new MyPageViewModel();
            
        }
        
    }
}