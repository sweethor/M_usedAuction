using Final_usedAuction.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Final_usedAuction.Models;


namespace Final_usedAuction.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ConnectFirebase openfirebase = new ConnectFirebase();
        public Command LoginCommand { get; }
        public Command SignUpCommand { get; }
        private string _LoginID;
        public string LoginID
        {
            get => _LoginID;
            set
            {
                _LoginID = value;
                OnPropertyChanged(nameof(LoginID));
            }
        }
        private string _LoginPW;
        public string LoginPW
        {
            get => _LoginPW;
            set
            {
                _LoginPW = value;
                OnPropertyChanged(nameof(LoginPW));
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);

        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            MessagingCenter.Send<LoginViewModel>(this, "login");
             var member = await openfirebase.L_GetMember(LoginID, LoginPW);
            if (member != null)
            {

                MessagingCenter.Send<LoginViewModel, string>(this, "sendid", member.MemberID);
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                //await Shell.Current.GoToAsync($"//MyPage?{nameof(MyPageViewModel.Mem_ID)}={member.MemberID}");
                // await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login","Login Faild","ok");
            }
           // await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
        private async void OnSignUpClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            MessagingCenter.Send<LoginViewModel>(this, "SignUp");
            await Shell.Current.GoToAsync($"//{nameof(SignUpPage)}");
        }
    }
}
