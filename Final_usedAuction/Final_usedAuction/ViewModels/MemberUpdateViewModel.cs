using Final_usedAuction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    class MemberUpdateViewModel : BaseViewModel
    {
        public string myValue = Preferences.Get("user_ID", "default_value");
        ConnectFirebase openfirebase = new ConnectFirebase();
        public Command OverlapID_CKCommand { get; }
        public Command OverlapNName_CKCommand { get; }
        public Command SignUp_Command { get; }
        public Command Back { get; }
       
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
        public MemberUpdateViewModel()
        {
            OverlapNName_CKCommand = new Command(OnOverlapNNameClicked);
            SignUp_Command = new Command(OnSignUpClicked);
            Back = new Command(OnBack);
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
            myValue = Preferences.Get("user_ID", "default_value");
            if (ck_overlap >= 1)
            {
                await openfirebase.UpdateMember(myValue,SignUpPW,SignUpNName,SignUpPNumber,SignUpEmail,SignUpka_ID);
                ck_overlap = 0;
                await Application.Current.MainPage.DisplayAlert("SignUP", "회원가입완료", "ok");
                SignUpEmail = string.Empty;
                SignUpka_ID = string.Empty;
                SignUpName = string.Empty;
                SignUpNName = string.Empty;
                SignUpPNumber = string.Empty;
                SignUpPW = string.Empty;
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("SignUP", "중복확인을 해주세요", "ok");
            }


        }
        private async void OnBack(object obj)
        {
            SignUpEmail = string.Empty;
            SignUpka_ID = string.Empty;
            SignUpName = string.Empty;
            SignUpNName = string.Empty;
            SignUpPNumber = string.Empty;
            SignUpPW = string.Empty;
            await Shell.Current.GoToAsync($"..");
        }
    }
}
