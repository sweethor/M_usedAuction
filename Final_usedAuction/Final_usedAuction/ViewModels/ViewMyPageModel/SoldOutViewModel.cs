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
    class SoldOutViewModel : BaseViewModel
    {
        private SoldOutItem _selectedItem;

        public ObservableCollection<SoldOutItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<SoldOutItem> ItemTapped { get; }
        ConnectFirebase openfirebase = new ConnectFirebase();
        public string myValue = Preferences.Get("user_ID", "default_value");

        public SoldOutViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<SoldOutItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<SoldOutItem>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await openfirebase.Select_GetAllSoldOutItem(myValue);
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

        public SoldOutItem SelectedItem
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

        async void OnItemSelected(SoldOutItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MemberDetailPage)}?{nameof(MemberDetailViewModel.ItemID)}={item.Topbid_ID}");
        }


    }


}

