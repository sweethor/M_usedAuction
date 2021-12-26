using Final_usedAuction.Models;
using Final_usedAuction.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels.ViewMyPageModel
{
    class BuyComViewModel : BaseViewModel
    {
        private BuyComplete _selectedItem;

        public ObservableCollection<BuyComplete> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<BuyComplete> ItemTapped { get; }
        ConnectFirebase openfirebase = new ConnectFirebase();
        public string myValue = Preferences.Get("user_ID", "default_value");

        public BuyComViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<BuyComplete>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<BuyComplete>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await openfirebase.Select_GetAllBuyItem(myValue);
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

        public BuyComplete SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(BuyComplete item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MemberDetailPage)}?{nameof(MemberDetailViewModel.ItemID)}={item.Sell_ID}");
        }


    }


}


