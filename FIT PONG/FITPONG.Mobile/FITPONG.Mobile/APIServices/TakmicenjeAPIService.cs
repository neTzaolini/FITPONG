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
            var url = $"{APIUrl}/{Resurs}/{id}/akcije/init";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync("").ReceiveJson<Takmicenja>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
#pragma warning disable IDE0034 // Simplify 'default' expression
                return default(Takmicenja);
#pragma warning restore IDE0034 // Simplify 'default' expression
            }
        }
        public async Task<List<RasporedStavka>> GetRaspored(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/raspored";
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
            var url = $"{APIUrl}/{Resurs}/{id}/evidencije";
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

        public async Task<EvidencijaMeca> EvidentirajMec(int id,EvidencijaMeca obj)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/evidencije";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync(obj)
                    .ReceiveJson<EvidencijaMeca>();
                return rezult; // trebalo bi promisliti, mozda je ipak pametnije vratiti konkretnu evidenciju
                //koja se evidentirala
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(EvidencijaMeca);
            }
        }
        public async Task<List<TabelaStavka>> GetTabela(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/tabela";
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
        public async Task<Prijave> BlokirajPrijavu(int id, int prijavaId)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/prijava/{prijavaId}/bloklista";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync("").ReceiveJson<Prijave>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Prijave);
            }
        }
        public async Task<Prijave> Prijava(int id, PrijavaInsert obj)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/prijava";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync(obj).ReceiveJson<Prijave>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Prijave);
            }
        }

        public async Task<List<Prijave>> GetPrijave(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/prijave";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync("").ReceiveJson<List<Prijave>>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(List<Prijave>);
            }
        }

        public async Task<Prijave> OtkaziPrijavu(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/prijava";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).DeleteAsync().ReceiveJson<Prijave>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Prijave);
            }
        }

        public async Task<Favoriti> GetFavoriti(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/favoriti";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).GetAsync().ReceiveJson<Favoriti>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Favoriti);
            }
        }

        public async Task<Favoriti> OznaciUtakmicu(int id)
        {
            var url = $"{APIUrl}/{Resurs}/{id}/favoriti";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).PostJsonAsync("").ReceiveJson<Favoriti>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(Favoriti);
            }
        }

        public async Task<List<string>> GetListaUseraNotifikacije(int utakId)
        {
            var url = $"{APIUrl}/{Resurs}/{utakId}/lista-notifikacije";
            try
            {
                var rezult = await url.WithBasicAuth(Username, Password).GetJsonAsync<List<string>>();
                return rezult;
            }
            catch (FlurlHttpException ex)
            {
                var errori = GetErrore(ex).Result;
                await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
                return default(List<string>);
            }
        }

        //public override async Task<T> Update<T>(int id, object request)
        //{
        //    var url = $"{APIUrl}/{resurs}/{id}";
        //    try
        //    {
        //        return await url.WithBasicAuth(Username, Password).PatchJsonAsync(request).ReceiveJson<T>();
        //    }
        //    catch (FlurlHttpException ex)
        //    {
        //        var errori = GetErrore(ex).Result;
        //        await Application.Current.MainPage.DisplayAlert("Greška", errori, "OK");
        //        return default(T);
        //    }
        //}
    }
}