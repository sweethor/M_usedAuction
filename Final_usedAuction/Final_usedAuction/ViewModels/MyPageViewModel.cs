using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.Models;
using Final_usedAuction.Views;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    public class MyPageViewModel : BaseViewModel
    {
        private Inter_item _selectedItem;

        public ObservableCollection<Inter_item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Inter_item> ItemTapped { get; }
        ConnectFirebase openfirebase = new ConnectFirebase();
        public Command UpdateInfoCommand { get; }
        public Command UpdatememCommand { get; }
        public Command Vi_buyCommand { get; }
        public Command Vi_sellCommand { get; }
        public Command Vi_interCommand { get; }
        public Command NotiCommand { get; }
        public Command CommandBuy { get; }
        public Command CommandAuc { get; }
        public Command CommandSelling { get; }
        public Command CommandSelled { get; }

        private bool vi_buy;
        public bool Vi_buy { get => vi_buy; set => SetProperty(ref vi_buy, value); }
        private bool vi_sell;
        public bool Vi_sell { get => vi_sell; set => SetProperty(ref vi_sell, value); }
        private bool vi_inter;
        public bool Vi_inter { get => vi_inter; set => SetProperty(ref vi_inter, value); }

        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }
        private string nname;
        public string NName { get => nname; set => SetProperty(ref nname, value); }

        private string noti_cnt;
        public string Noti_cnt { get => noti_cnt; set => SetProperty(ref noti_cnt, value); }

        private string buy_cnt;
        public string Buy_cnt { get => buy_cnt; set => SetProperty(ref buy_cnt, value); }
        private string auc_cnt;
        public string Auc_cnt { get => auc_cnt; set => SetProperty(ref auc_cnt, value); }

        private string selling_cnt;
        public string Selling_cnt { get => selling_cnt; set => SetProperty(ref selling_cnt, value); }
        private string selled_cnt;
        public string Selled_cnt { get => selled_cnt; set => SetProperty(ref selled_cnt, value); }

        public string myValue = Preferences.Get("user_ID", "default_value");



        public MyPageViewModel()
        {
            Name = "ID";
            NName = "계정명";
            Title = "나의 활동";
            UpdateInfoCommand = new Command(OnproClicked);
            Vi_buyCommand = new Command(OnbuyClicked);
            Vi_sellCommand = new Command(OnsellClicked);
            Vi_interCommand = new Command(OninterClicked);
            Items = new ObservableCollection<Inter_item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Inter_item>(OnItemSelected);
            CommandAuc = new Command(OnAucClicked);
            NotiCommand = new Command(OnNotiClicked);
            CommandBuy = new Command(OnBuyClicked);
            CommandSelled = new Command(OnSelledClicked);
            CommandSelling = new Command(OnSellingClicked);
            UpdatememCommand = new Command(OnupdatememClicked);

        }

        private async void OnproClicked(object obj)
        {
            myValue = Preferences.Get("user_ID", "default_value");
            var mypro = await openfirebase.Select_GetMember(myValue);
            var mynoti = await openfirebase.Select_GetAllINoti(myValue);
            Name = mypro.MemberName;
            NName = mypro.MemberNName;
            Noti_cnt ="현재 알림 개수 : " + mynoti.Count;
        }

        private async void OnbuyClicked(object obj)
        {
            var myatv = await openfirebase.Select_GetMember(myValue);
            var myauc = await openfirebase.Select_GetAlltopbidItems(myValue);   
            Buy_cnt = "구매물품" + myatv.MemberB_count + "건";
            Auc_cnt = "경매내역" + myauc.Count + "건"; // 미구현
            Vi_buy = true;
            Vi_inter = false;
            Vi_sell = false;
        }

        private async void OnsellClicked(object obj)
        {
            var myauc = await openfirebase.Select_GetSeller(myValue);
            var myatv = await openfirebase.Select_GetMember(myValue);
            Selling_cnt = "등록물품" + myauc.Count + "건"; // 미구현
            Selled_cnt = "판매완료" + myatv.MemberS_count + "건"; // 미구현
            Vi_buy = false;
            Vi_inter = false;
            Vi_sell = true;
        }

        private void OninterClicked(object obj)
        {
            Vi_buy = false;
            Vi_inter = true;
            Vi_sell = false;
        }

        private async void OnupdatememClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(MemberUpdatePage)}");
        }

        private async void OnNotiClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NotiPage)}");
        }

        private async void OnBuyClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(BuyComPage)}");
        }

        private async void OnAucClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(TopBidbySellingitemPage)}");
        }

        private async void OnSellingClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(SellingPage)}");
        }

        private async void OnSelledClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(SoldOutPage)}");
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await openfirebase.Select_GetAllInterItem(myValue);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Inter_item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Inter_item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Itemno)}={item.Sell_num}");
        }

    }
}