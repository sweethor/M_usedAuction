using Final_usedAuction.Models;
using Final_usedAuction.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    [QueryProperty(nameof(ItemID), nameof(ItemID))]
    class MemberDetailViewModel : BaseViewModel
    {
        private string _Name;
        public string Name { get => _Name; set => SetProperty(ref _Name, value); }
        private string _Nname;
        public string Nname { get => _Nname; set => SetProperty(ref _Nname, value); }
        private string _Phone;
        public string Phone { get => _Phone; set => SetProperty(ref _Phone, value); }
        private string _KaID;
        public string KaID { get => _KaID; set => SetProperty(ref _KaID, value); }
        private string _Email;
        public string Email { get => _Email; set => SetProperty(ref _Email, value); }
        private string s_ID;
        private int _Trust;
        public int Trust { get => _Trust; set => SetProperty(ref _Trust, value); }
        private int _Answer;
        public int Answer { get => _Answer; set => SetProperty(ref _Answer, value); }
        private int _Satissfied;
        public int Satissfied { get => _Satissfied; set => SetProperty(ref _Satissfied, value); }
        public string ItemID
        {
            get
            {
                return s_ID;
            }
            set
            {
                s_ID = value;
                LoadItemId(value);
            }
        }
        ConnectFirebase openfirebase = new ConnectFirebase();

        public async void LoadItemId(string itemno)
        {
            try
            {
               var item = await openfirebase.Select_GetMember(ItemID);
                Name = item.MemberName;
                Nname = item.MemberNName;
                Phone = item.MemberPhone;
                Email = item.MemberEMail;
                KaID = item.MemberKAID;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public MemberDetailViewModel()
        {
            Sendtrust = new Command(OnSendtrust);
            Back = new Command(OnBack);
        }

        public Command Sendtrust { get; }
        public Command Back { get; }

        public async void OnSendtrust(object obj)
        {
            await openfirebase.UpdateMember(ItemID, Trust, Answer, Satissfied);
            await Application.Current.MainPage.DisplayAlert("trust", "신뢰도정보 기입완료.", "ok");
        }

        public async void OnBack(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(MyPage)}");
        }
    }
}
