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
    class MemberTrustViewModel : BaseViewModel
    {
        private string _Name;
        public string Name { get => _Name; set => SetProperty(ref _Name, value); }
        private string _Nname;
        public string Nname { get => _Nname; set => SetProperty(ref _Nname, value); }
        private string s_ID;
        private float _Trust;
        public float Trust { get => _Trust; set => SetProperty(ref _Trust, value); }
        private float _Answer;
        public float Answer { get => _Answer; set => SetProperty(ref _Answer, value); }
        private float _Satissfied;
        public float Satissfied { get => _Satissfied; set => SetProperty(ref _Satissfied, value); }
        private string _T_Trust;
        public string T_Trust { get => _T_Trust; set => SetProperty(ref _T_Trust, value); }
        private string _T_Answer;
        public string T_Answer { get => _T_Answer; set => SetProperty(ref _T_Answer, value); }
        private string _T_Satissfied;
        public string T_Satissfied { get => _T_Satissfied; set => SetProperty(ref _T_Satissfied, value); }

        ConnectFirebase openfirebase = new ConnectFirebase();

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

        public Command Back { get; }

        public async void LoadItemId(string itemno)
        {
            try
            {
                var item = await openfirebase.Select_GetMember(ItemID);
                Name = item.MemberName;
                Nname = item.MemberNName;
                Trust = item.MemberTrust / (float)item.MemberT_count;
                Answer = item.MemberAnswer / (float)item.MemberT_count;
                Satissfied = item.MemberSatissfied / (float)item.MemberT_count;
                T_Trust = ((int)(Trust * 100 )).ToString()+"%";
                T_Answer = ((int)(Answer * 100)).ToString() + "%";
                T_Satissfied = ((int)(Satissfied * 100)).ToString() + "%";

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public MemberTrustViewModel()
        {
            Back = new Command(OnBack);
        }

        private async void OnBack(object obj)
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
