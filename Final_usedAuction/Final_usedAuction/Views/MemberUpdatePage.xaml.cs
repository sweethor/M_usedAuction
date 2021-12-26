﻿using Final_usedAuction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_usedAuction.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemberUpdatePage : ContentPage
    {
        public MemberUpdatePage()
        {
            InitializeComponent();
            this.BindingContext = new MemberUpdateViewModel();
        }
    }
}