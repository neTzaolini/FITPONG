﻿using FIT_PONG.Mobile.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FIT_PONG.Mobile.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PotvrdiMejl : ContentPage
    {
        PotvrdiMejlViewModel viewModel;

        public PotvrdiMejl(int userId)
        {
            BindingContext = viewModel = new PotvrdiMejlViewModel(userId);
            InitializeComponent();
        }
    }
}