using Final_usedAuction.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using Final_usedAuction.Views;
using System;

namespace Final_usedAuction.ViewModels
{
    [QueryProperty(nameof(Itemno), nameof(Itemno))]
    public class UpdateItemViewModel : BaseViewModel
    {
        ConnectFirebase openfirebase = new ConnectFirebase();
        private int s_num;
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _Contents;
        public string Contents
        {
            get => _Contents;
            set
            {
                _Contents = value;
                OnPropertyChanged(nameof(Contents));
            }
        }
        private string _Price;
        public string Price
        {
            get => _Price;
            set
            {
                _Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
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
        public byte[] img_arr;

        public int sellmode;
        public int sellcate;

        public string Itemno
        {
            get
            {
                return s_num.ToString();
            }
            set
            {
                s_num = Int32.Parse(value);
            }
        }

        public string myValue = Preferences.Get("user_ID", "default_value");

        public Command Mode1 { get; }
        public Command Mode2 { get; }
        public Command Mode3 { get; }

        public Command Cate1 { get; }
        public Command Cate2 { get; }
        public Command Cate3 { get; }
        public Command Cate4 { get; }

        public Command PickImage { get; }
        public Command TakeImage { get; }

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }



        public UpdateItemViewModel()
        {
            PickImage = new Command(OnPickImage);
            TakeImage = new Command(OnTakeImage);
            Mode1 = new Command(OnMode1_CK);
            Mode2 = new Command(OnMode2_CK);
            Mode3 = new Command(OnMode3_CK);
            Cate1 = new Command(OnCate1_CK);
            Cate2 = new Command(OnCate2_CK);
            Cate3 = new Command(OnCate3_CK);
            Cate4 = new Command(OnCate4_CK);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancle);
        }

        private void OnMode1_CK(object obj)
        {
            sellmode = 1;
        }
        private void OnMode2_CK(object obj)
        {
            sellmode = 2;
        }
        private void OnMode3_CK(object obj)
        {
            sellmode = 3;
        }

        private void OnCate1_CK(object obj)
        {
            sellcate = 1;
        }
        private void OnCate2_CK(object obj)
        {
            sellcate = 2;
        }
        private void OnCate3_CK(object obj)
        {
            sellcate = 3;
        }
        private void OnCate4_CK(object obj)
        {
            sellcate = 4;
        }

        private async void OnPickImage(object obj)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            var stream = await result.OpenReadAsync();
            var stream2 = await result.OpenReadAsync();

            img_arr = GetImageStreamAsBytes(stream2, stream2.Length);

            Imagesource = ImageSource.FromStream(() => stream);



        }
        private async void OnTakeImage(object obj)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            var stream = await result.OpenReadAsync();
            var stream2 = await result.OpenReadAsync();

            img_arr = GetImageStreamAsBytes(stream2, stream2.Length);

            Imagesource = ImageSource.FromStream(() => stream);
        }


        private async void OnSave(object obj)
        {
            myValue = Preferences.Get("user_ID", "default_value");
            string img_arr_str = System.Convert.ToBase64String(img_arr);
            await openfirebase.Update_Item(s_num, Name, Contents, img_arr_str, Int32.Parse(Price), Int32.Parse(Bid), sellmode, sellcate);

            await Shell.Current.GoToAsync($"..");
        }

        private async void OnCancle(object obj)
        {
            Name = "";
            Contents = "";
            Price = "";
            Bid = "";
            Imagesource = null;
            await Shell.Current.GoToAsync($"..");
        }

        private byte[] GetImageStreamAsBytes(Stream input, long length)
        {
            var buffer = new byte[length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
