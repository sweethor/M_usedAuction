using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Final_usedAuction.Models;
using Final_usedAuction.ViewModels.ViewMyPageModel;
using Final_usedAuction.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    class NotiViewModel : BaseViewModel
    {
        private Noti _noti;
        public ObservableCollection<Noti> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Noti> ItemTapped { get; }
        ConnectFirebase openfirebase = new ConnectFirebase();
        public string myValue = Preferences.Get("user_ID", "default_value");

        public NotiViewModel()
        {
            Title = "알림목록";
            Items = new ObservableCollection<Noti>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Noti>(OnItemSelected);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            
            try
            {
                myValue = Preferences.Get("user_ID", "default_value");
                Items.Clear();
                var items = await openfirebase.Select_GetAllINoti(myValue);
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

        public Noti SelectedItem
        {
            get => _noti;
            set
            {
                SetProperty(ref _noti, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Noti item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            
            if(item.page_sw == 1)
            {
                await openfirebase.Delete_Noti(myValue, item.noti_num);
                await openfirebase.Delete_InterItem(myValue, item.sellitem_num);
            }
            else if(item.page_sw == 2)
            {
                await openfirebase.Delete_Noti(myValue, item.noti_num);
            }
            else 
            {
                await openfirebase.Delete_Noti(myValue, item.noti_num);
            }
            
        }
    }
    
}
