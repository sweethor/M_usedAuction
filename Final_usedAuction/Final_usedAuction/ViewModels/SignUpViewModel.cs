using System;
using System.Collections.Generic;
using System.Text;
using Final_usedAuction.Models;
using Final_usedAuction.Views;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    class SignUpViewModel :BaseViewModel
    {
        ConnectFirebase openfirebase = new ConnectFirebase();
        public Command OverlapID_CKCommand { get; }
        public Command OverlapNName_CKCommand { get; }
        public Command SignUp_Command { get; }
        private string _SignUpID;
        public string SignUpID
        {
            get => _SignUpID;
            set
            {
                _SignUpID = value;
                OnPropertyChanged(nameof(SignUpID));
            }
        }
        private string _SignUpPW;
        public string SignUpPW
        {
            get => _SignUpPW;
            set
            {
                _SignUpPW = value;
                OnPropertyChanged(nameof(SignUpPW));
            }
        }
        private string _SignUpName;
        public string SignUpName
        {
            get => _SignUpName;
            set
            {
                _SignUpName = value;
                OnPropertyChanged(nameof(SignUpName));
            }
        }
        private string _SignUpNName;
        public string SignUpNName
        {
            get => _SignUpNName;
            set
            {
                _SignUpNName = value;
                OnPropertyChanged(nameof(SignUpNName));
            }
        }
        private string _SignUpPNumber;
        public string SignUpPNumber
        {
            get => _SignUpPNumber;
            set
            {
                _SignUpPNumber = value;
                OnPropertyChanged(nameof(SignUpPNumber));
            }
        }
        private string _SignUpEmail;
        public string SignUpEmail
        {
            get => _SignUpEmail;
            set
            {
                _SignUpEmail = value;
                OnPropertyChanged(nameof(SignUpEmail));
            }
        }
        private string _SignUpka_ID;
        public string SignUpka_ID
        {
            get => _SignUpka_ID;
            set
            {
                _SignUpka_ID = value;
                OnPropertyChanged(nameof(SignUpka_ID));
            }
        }
        public int ck_overlap = 0;
        public SignUpViewModel()
        {
            OverlapID_CKCommand = new Command(OnOverlapIDClicked);
            OverlapNName_CKCommand = new Command(OnOverlapNNameClicked);
            SignUp_Command = new Command(OnSignUpClicked);
        }

        private async void OnOverlapIDClicked(object obj)
        {
            var member = await openfirebase.ID_CK_GetMember(SignUpID);
            if (member != null)
            {
                await Application.Current.MainPage.DisplayAlert("ID_Check", "존재하는 ID 입니다.", "ok");
                SignUpID = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("ID_Check", "사용가능한 ID 입니다.", "ok");
                ck_overlap++;
            }
        }
        private async void OnOverlapNNameClicked(object obj)
        {
            var member = await openfirebase.NNAME_CK_GetMember(SignUpNName);
            if (member != null)
            {
                await Application.Current.MainPage.DisplayAlert("ID_Check", "존재하는 계정명 입니다.", "ok");
                SignUpNName = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("ID_Check", "사용가능한 계정명 입니다.", "ok");
                ck_overlap++;
            }
        }
        private async void OnSignUpClicked(object obj)
        {
            if(ck_overlap >= 2)
            {
                await openfirebase.AddMember(SignUpID, SignUpPW, SignUpName, SignUpNName, SignUpPNumber, SignUpEmail, SignUpka_ID);
                ck_overlap = 0;
                await Application.Current.MainPage.DisplayAlert("SignUP", "회원가입완료", "ok");
                SignUpEmail = string.Empty;
                SignUpID = string.Empty;
                SignUpka_ID = string.Empty;
                SignUpName = string.Empty;
                SignUpNName = string.Empty;
                SignUpPNumber = string.Empty;
                SignUpPW = string.Empty;
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("SignUP", "중복확인을 해주세요", "ok");
            }
            

        }
    }
}
