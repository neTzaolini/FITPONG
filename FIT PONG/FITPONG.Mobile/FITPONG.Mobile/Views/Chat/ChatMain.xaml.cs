﻿using FIT_PONG.Mobile.APIServices;
using FIT_PONG.Mobile.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FIT_PONG.Mobile.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMain : TabbedPage
    {
        ChatMainViewModel viewModel;
        public ChatMain()
        {
            InitializeComponent();
            BindingContext = viewModel = new ChatMainViewModel();
            viewModel.ChatServis.ConnectAsync();

            var ChatGrupeStranica = new ChatGrupe(viewModel.ChatServis);
            ChatGrupeStranica.Title = "Aktivni kanali";
            Children.Add(ChatGrupeStranica);

            var ChatMainKanal = new ChatKonverzacija(viewModel.ChatServis,"Main");
            ChatMainKanal.Title = "Main";
            Children.Add(ChatMainKanal);

            viewModel.ChatServis.StiglaPoruka += (sender, args) =>
            {
                //ako zelim da promijenim odma na stranicu kad se pojavi nova poruka, onda trebam samo 
                //pozvati ovu funkciju promijeni stranicu, bez pozivanja kreiraj stranicu jer se ona unutar
                //nje poziva
                var stranica = KreirajStranicu(args.Primatelj);
                var kontekstStranice = (ChatKonverzacijaViewModel)stranica.BindingContext;
                kontekstStranice.SendLocalMessage(args);
                var stranicaKaoChatKonvo = (ChatKonverzacija)stranica;
                stranicaKaoChatKonvo.SkrolajNaDno();
            };
        }

        private Page PostojiKanalOtvoren(string naziv)
        {
            foreach(var i in Children)
            {
                if (i.Title == naziv)
                    return i;
            }
            return null;
        }
        public Page KreirajStranicu(string NazivPrimatelja)
        {
            var str = PostojiKanalOtvoren(NazivPrimatelja);
            if (str!= null)
                return str;

            var ChatKonvo = new ChatKonverzacija(viewModel.ChatServis, NazivPrimatelja);
            ChatKonvo.Title = NazivPrimatelja;
            Children.Add(ChatKonvo);
            return ChatKonvo;
        }
        public void PromijeniStranicu(string nazivOdabranog)
        {
            var stranica = KreirajStranicu(nazivOdabranog);
            CurrentPage = stranica;
            OnCurrentPageChanged();
        }
    }
}