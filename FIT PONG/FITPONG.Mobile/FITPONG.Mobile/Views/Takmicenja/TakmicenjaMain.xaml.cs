﻿using FIT_PONG.Mobile.ViewModels.Takmicenja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FIT_PONG.Mobile.Views.Takmicenja
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakmicenjaMain : TabbedPage
    {
        TakmicenjaDetaljiViewModel viewModel;
        public TakmicenjaMain(TakmicenjaDetaljiViewModel model)
        {
            InitializeComponent();

            BindingContext = viewModel = model;
            var TakmicenjaDetaljiStranica = new TakmicenjaDetalji(model);
            TakmicenjaDetaljiStranica.Title = "Detalji";
            Children.Add(TakmicenjaDetaljiStranica);

            Title = model.Takmicenje.Naziv;
        }
    }
}