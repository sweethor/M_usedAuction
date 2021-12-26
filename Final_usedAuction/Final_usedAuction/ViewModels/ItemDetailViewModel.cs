using Final_usedAuction.Models;
using Final_usedAuction.Views;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    [QueryProperty(nameof(Itemno), nameof(Itemno))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int s_num;
        private string s_name;
        private string s_con;
        private string s_img;
        private int s_price;
        private int h_bid;
        private string s_id;
        private string t_id;
        private string _Bid;
        public string Bid
        {
            get => _Bid;
            set
            {
                _Bid = value;
                OnPropertyChanged(nameof(Bid));
            }
        }
        private bool isprice;
        public bool Isprice { get => isprice; set => SetProperty(ref isprice, value); }
        private bool isbid;
        public bool Isbid { get => isbid; set => SetProperty(ref isbid, value); }
        private bool iseller;
        public bool Iseller { get => iseller; set => SetProperty(ref iseller, value); }
        private bool isbuyer;
        public bool Isbuyer { get => isbuyer; set => SetProperty(ref isbuyer, value); }
        private bool isbook;
        public bool Isbook { get => isbook; set => SetProperty(ref isbook, value); }
        private bool iscbook;
        public bool Iscbook { get => iscbook; set => SetProperty(ref iscbook, value); }
        public int No { get; set; }
        public string S_name
        {
            get => s_name;
            set => SetProperty(ref s_name, value);
        }
        public string S_con
        {
            get => s_con;
            set => SetProperty(ref s_con, value);
        }
        public string S_img
        {
            get => s_img;
            set => SetProperty(ref s_img, value);
        }
        public int S_price
        {
            get => s_price;
            set => SetProperty(ref s_price, value);
        }
        public int H_bid
        {
            get => h_bid;
            set => SetProperty(ref h_bid, value);
        }
        public string S_id
        {
            get => s_id;
            set => SetProperty(ref s_id, value);
        }
        public string T_id
        {
            get => t_id;
            set => SetProperty(ref t_id, value);
        }
        public string Itemno
        {
            get
            {
                return s_num.ToString();
            }
            set
            {
                s_num = Int32.Parse(value);
                LoadItemId(value);
            }
        }

        public int sellmode;

        public Command Buy { get; }
        public Command Updateitem { get; }
        public Command Deleteitem { get; }
        public Command Bookmark { get; }
        public Command CBookmark { get; }
        public Command Sucbid { get; }
        public Command SellerInfo { get; }

        ConnectFirebase openfirebase = new ConnectFirebase();

        public string myValue = Preferences.Get("user_ID", "default_value");

        private ImageSource _Imagesource;
        public ImageSource Imagesource
        {
            get => _Imagesource;
            set
            {
                _Imagesource = value;
                OnPropertyChanged(nameof(Imagesource));
            }
        }

        public async void LoadItemId(string itemno)
        {
            try
            {
                myValue = Preferences.Get("user_ID", "default_value");
                await openfirebase.Update_cnt(s_num);
                var item = await openfirebase.Select_GetItem(s_num);
                No = item.Sell_num;
                S_name = item.Sell_name;
                S_con = item.Sell_contents;
                S_img = item.Sell_img;
                S_price = item.Sell_price;
                H_bid = item.Highest_bid;
                S_id = item.Sell_ID;
                T_id = item.Topbid_ID;
                if (item.Sell_mode == 1)
                {
                    Isbid = true;
                    Isprice = false;
                    sellmode = 1;
                }
                else if (item.Sell_mode == 2)
                {
                    Isbid = false;
                    Isprice = true;
                    sellmode = 2;
                }
                else
                {
                    Isbid = true;
                    Isprice = true;
                    sellmode = 3;
                }
                if(myValue == item.Sell_ID)
                {
                    Iseller = true;
                    Isbuyer = false;
                }
                else 
                {
                    Iseller = false;
                    Isbuyer = true;
                    if(openfirebase.Select_GetInterItem(myValue,s_num) != null)
                    {
                        Isbook = true;
                        Iscbook = false;
                    }
                    else
                    {
                        Isbook = false;
                        Iscbook = true;
                    }
                }
                ImageSource retSource = null;

                byte[] imageAsBytes = System.Convert.FromBase64String(S_img);
                //byte[] imageAsBytes = value as byte[];
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));


                Imagesource = retSource;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public ItemDetailViewModel()
        {
            Buy = new Command(Onbuy);
            Updateitem = new Command(OnUpdate);
            Deleteitem = new Command(OnDelete);
            Bookmark = new Command(OnBookmark);
            CBookmark = new Command(OnCBookmark);
            Sucbid = new Command(OnSucbid);
            SellerInfo = new Command(OnSellerInfo);
        }

        private async void Onbuy(object obj)
        {
            
            if (sellmode == 1)
            {
                if(Int32.Parse(Bid) <= H_bid)
                {
                    await Application.Current.MainPage.DisplayAlert("under_bid_ck", "입력한 금액이 최고입찰가보다 낮거나 같습니다.", "ok");
                }
                else
                {
                    if(T_id != "")
                    {
                        await openfirebase.AddNoti(T_id, S_name, s_num, S_id, myValue, H_bid, 1);
                    }
                    
                    await openfirebase.Update_bid(s_num, myValue, Int32.Parse(Bid));
                    await Application.Current.MainPage.DisplayAlert("bid_ok", "입찰완료.", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
            }
            else if(sellmode == 2)
            {
                if (Int32.Parse(Bid) == S_price)
                {

                    await openfirebase.AddNoti(myValue, S_name, s_num, S_id, myValue, H_bid, 2);
                    await openfirebase.AddNoti(S_id, S_name, s_num, S_id, myValue, H_bid, 3);
                    await openfirebase.AddbuyItem(myValue, S_name, S_con, S_img, S_price, S_id, myValue, H_bid, sellmode);
                    await openfirebase.AddsoldoutItem(S_id, S_name, S_con, S_img, S_price, S_id, myValue, H_bid, sellmode);
                    await openfirebase.UpdateMember(myValue, 2);
                    await openfirebase.UpdateMember(S_id, 1);
                    await openfirebase.Delete_Item(s_num);
                   
                    await openfirebase.Soldout_Delete_InterItem(s_num);
                    
                    await Application.Current.MainPage.DisplayAlert("buy_ok", "구매완료.", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("buy_no", "입력한 금액이 판매가격과 같지 않습니다.", "ok");
                }
            }
            else 
            {
                if (Int32.Parse(Bid) >= S_price)
                {

                    if (T_id != "")
                    {
                        await openfirebase.AddNoti(T_id, S_name, s_num, S_id, myValue, H_bid, 1);
                    }
                    await openfirebase.AddNoti(S_id, S_name, s_num, S_id, myValue, H_bid, 3);
                    await openfirebase.AddbuyItem(myValue, S_name, S_con, S_img, S_price, S_id, myValue, H_bid, sellmode);
                    await openfirebase.AddsoldoutItem(S_id, S_name, S_con, S_img, S_price, S_id, myValue, H_bid, sellmode);
                    await openfirebase.UpdateMember(myValue, 2);
                    await openfirebase.UpdateMember(S_id, 1);
                    await openfirebase.Delete_Item(s_num);
                    
                    
                    await openfirebase.Soldout_Delete_InterItem(s_num);
                    await Application.Current.MainPage.DisplayAlert("under_bid_ck", "구매완료.", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    if (Int32.Parse(Bid) <= H_bid)
                    {
                        await Application.Current.MainPage.DisplayAlert("under_bid_ck", "입력한 금액이 최고입찰가보다 낮습니다.", "ok");
                    }
                    else
                    {
                        if (T_id != "")
                        {
                            await openfirebase.AddNoti(T_id, S_name, s_num, S_id, myValue, H_bid, 1);
                        }
                        await openfirebase.Update_bid(s_num, myValue, Int32.Parse(Bid));
                        await Application.Current.MainPage.DisplayAlert("bid_ok", "입찰완료.", "ok");
                        await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    }
                }
            }

        }

        private async void OnUpdate(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(UpdateItemPage)}?{nameof(UpdateItemViewModel.Itemno)}={s_num}");
        }

        private async void OnDelete(object obj)
        {
            await openfirebase.Delete_Item(s_num);
            await Shell.Current.GoToAsync($"..");
        }

        private async void OnBookmark(object obj)
        {
            var item = await openfirebase.Select_GetItem(s_num);
            await openfirebase.AddInerItem(myValue, s_num, item.Sell_name, item.Sell_contents,
                item.Sell_img, item.Sell_price, item.Sell_ID, item.Topbid_ID, item.Highest_bid, item.Sell_mode);
            Isbook = false;
            Iscbook = true;
            await Application.Current.MainPage.DisplayAlert("inter_ok", "즐겨찾기에 추가되었습니다.", "ok");
        }

        private async void OnCBookmark(object obj)
        {
            await openfirebase.Delete_InterItem(myValue, s_num);
            Isbook = true;
            Iscbook = false;
            await Application.Current.MainPage.DisplayAlert("inter_ok", "즐겨찾기에서 삭제되었습니다.", "ok");
        }

        private async void OnSucbid(object obj)
        {
            if(T_id == "")
            {
                await Application.Current.MainPage.DisplayAlert("inter_no", "현재 입찰이없습니다.", "ok");
            }
            else
            {
                await openfirebase.AddNoti(T_id, S_name, s_num, S_id, T_id, H_bid, 2);
                await openfirebase.AddNoti(S_id, S_name, s_num, S_id, T_id, H_bid, 3);
                await openfirebase.AddbuyItem(T_id, S_name, S_con, S_img, S_price, S_id, T_id, H_bid, sellmode);
                await openfirebase.AddsoldoutItem(myValue, S_name, S_con, S_img, S_price, S_id, T_id, H_bid, sellmode);
                await openfirebase.UpdateMember(T_id, 2);
                await openfirebase.UpdateMember(myValue, 1);
                await openfirebase.Delete_Item(s_num);
                
                await openfirebase.Soldout_Delete_InterItem(s_num);
                
                
                await Application.Current.MainPage.DisplayAlert("inter_ok", "낙찰완료.", "ok");
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            

        }

        private async void OnSellerInfo(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(MemberTrustCK)}?{nameof(MemberTrustViewModel.ItemID)}={S_id}");
        }
    }
}
