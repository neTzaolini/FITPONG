﻿using FIT_PONG.SharedModels;
using FIT_PONG.SharedModels.Requests.Takmicenja;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FIT_PONG.Mobile.APIServices
{
    public class TakmicenjeAPIService : BaseAPIService
    {
        public TakmicenjeAPIService() : base("takmicenja")
        {

        }
        //vrlo vjerovatno da ce trebati nesto sto ce napuniti combo boxove, jedna funkcija koja ce obaviti
        //4 poziva

        //unutar ovih custom apiservisa mogu i trebati ce postojati metode koje se brinu oko popunjavanja
        //podataka potrebnih za rad formi kao sto su comboboxovi 
        public async Task<Takmicenja> Init(int id)
        {
            var url = $"{APIUrl}/{resurs}/{id}/init";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync("").ReceiveJson<Takmicenja>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Takmicenja);
            }
        }
        public async Task<List<RasporedStavka>> GetRaspored(int id)
        {
            var url = $"{APIUrl}/{resurs}/{id}/raspored";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).GetJsonAsync<List<RasporedStavka>>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return new List<RasporedStavka>();
            }
        }
        public async Task<List<EvidencijaMeca>> GetEvidencije(int id)
        {
            var url = $"{APIUrl}/{resurs}/{id}/evidencije";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).GetJsonAsync<List<EvidencijaMeca>>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return new List<EvidencijaMeca>();
            }
        }

        public async Task<List<EvidencijaMeca>> EvidentirajMec(int id,EvidencijaMeca obj)
        {
            var url = $"{APIUrl}/{resurs}/{id}/evidencije";
            try
            {
                var qryString = obj.ToQueryString();
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync(qryString)
                    .ReceiveJson<List<EvidencijaMeca>>();
                return rezult; // trebalo bi promisliti, mozda je ipak pametnije vratiti konkretnu evidenciju
                //koja se evidentirala
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return new List<EvidencijaMeca>();
            }
        }
        public async Task<List<TabelaStavka>> GetTabela(int id)
        {
            var url = $"{APIUrl}/{resurs}/{id}/tabela";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).GetJsonAsync<List<TabelaStavka>>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return new List<TabelaStavka>();
            }
        }
    }
}