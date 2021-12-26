using Final_usedAuction.Models;
using Final_usedAuction.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Final_usedAuction.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private SellingItem _selectedItem;
        private ObservableCollection<SellingItem> _Items { get; set; }
        public ObservableCollection<SellingItem> Items 
        {
            get { return _Items; }
            set
            {
                _Items = value;
            }
        }
        public int itemsw = 0;
        public Command LoadItemsCommand { get; }
        public Command<SellingItem> ItemTapped { get; }
        ConnectFirebase openfirebase = new ConnectFirebase();
        public  ICommand PerformSearch => new  Command<string>(async (string query) =>
        {
            itemsw = 1;
            await ExecuteLoadItemsCommand(query);
        });

        public AboutViewModel()
        {
            Title = "About";
            Items = new ObservableCollection<SellingItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(""));
            ItemTapped = new Command<SellingItem>(OnItemSelected);
        }

      

        async Task ExecuteLoadItemsCommand(string name)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                if (name == "")
                {
                    if (itemsw == 0)
                    {
                        var items = await openfirebase.Select_GetAllItem();
                        foreach (var item in items)
                        {
                            Items.Add(item);
                        }
                    }
                    itemsw = 0;
                }
                
                if (name != "")
                {
                    Items.Clear();
                    var items = await openfirebase.Select_GetName(name);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
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

        public SellingItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(SellingItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Itemno)}={item.Sell_num}");
        }
    }

    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value,
               Type targetType,
               object parameter,
               System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = System.Convert.FromBase64String(value.ToString());
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

        public object ConvertBack(object value,
               Type targetType,
               object parameter,
               System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}
