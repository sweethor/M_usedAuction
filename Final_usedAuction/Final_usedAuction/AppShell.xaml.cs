using Final_usedAuction.ViewModels;
using Final_usedAuction.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Final_usedAuction
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(UpdateItemPage), typeof(UpdateItemPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(BuyComPage), typeof(BuyComPage));
            Routing.RegisterRoute(nameof(MemberDetailPage), typeof(MemberDetailPage));
            Routing.RegisterRoute(nameof(MemberTrustCK), typeof(MemberTrustCK));
            Routing.RegisterRoute(nameof(MemberUpdatePage), typeof(MemberUpdatePage));
            Routing.RegisterRoute(nameof(NotiPage), typeof(NotiPage));
            Routing.RegisterRoute(nameof(SellingPage), typeof(SellingPage));
            Routing.RegisterRoute(nameof(SoldOutPage), typeof(SoldOutPage));
            Routing.RegisterRoute(nameof(TopBidbySellingitemPage), typeof(TopBidbySellingitemPage));


        }

        /*
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
          
        }
        */
    }
}
