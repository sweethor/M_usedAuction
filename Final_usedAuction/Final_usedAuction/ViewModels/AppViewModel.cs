using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Final_usedAuction.ViewModels
{
    class AppViewModel : BaseViewModel
    {
        private bool islogin;
        public bool Islogin { get => islogin; set => SetProperty(ref islogin, value); }
        private string isid;
        public string IsID { get => isid; set => SetProperty(ref isid, value); }

        public AppViewModel()
        {
            MessagingCenter.Subscribe<LoginViewModel>(this, "login", (sender) =>
             {
                 Islogin = false;
             });
            MessagingCenter.Subscribe<LoginViewModel>(this, "SignUp", (sender) =>
            {
                Islogin = true;
            });
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "sendid", (sender, arg) =>
            {
                IsID = arg;
                Preferences.Clear();
                Preferences.Remove("user_ID");
                Preferences.Set("user_ID", IsID);
            });
        }

    }
}
