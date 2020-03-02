﻿using FIT_PONG.Models;
using FIT_PONG.Models.BL;
using FIT_PONG.ViewModels;
using FIT_PONG.ViewModels.TakmicenjeVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FIT_PONG.Controllers
{
    public class TakmicenjeController : Controller
    {

        private readonly MyDb db;
        private readonly InitTakmicenja inicijalizator;
        public TakmicenjeController(MyDb instanca, InitTakmicenja instancaInita)
        {
            db = instanca;
            inicijalizator = instancaInita;
        }
        public IActionResult Index(int page = 1, string sortExpression="ID")
        {
            List<TakmicenjeVM> takmicenja = db.Takmicenja
                .Include(tak => tak.Kategorija)
                .Include(tak => tak.Sistem)
                .Include(tak => tak.Vrsta)
                .Include(tak => tak.Status)
                .Include(tak => tak.Feed)
                .Include(tak => tak.Prijave)
                .Select(s => new TakmicenjeVM
                  (s, 0)).ToList();
            foreach (TakmicenjeVM i in takmicenja)
                i.BrojPrijavljenih = db.Prijave.Where(s => s.TakmicenjeID == i.ID).Count();
            var qry = takmicenja.OrderByDescending(x=>x.DatumKreiranja).ToList();
            var takmicenja1 = PagingList.Create(qry, 2, page, sortExpression, "ID");

            return View(takmicenja1);
        }

        public IActionResult Detalji(int? id)
        {
            if (id == null)
            {
                return View("/Takmicenje/Neuspjeh");
            }
            //potreban query za broj rundi,u bracketima se nalazi takmicenjeID ,bar bi trebalo opotrebna migracija
            TakmicenjeVM obj = GetTakmicenjeVM(id);
            if (obj != null)
                return View(obj);

            return Redirect("/Takmicenje/Neuspjeh");
        }

        public IActionResult RezultatiSingleDouble(int? id)
        {
            if (db.Takmicenja.Where(x => x.ID == id).SingleOrDefault() != null)
            {
                ViewBag.id = id;
                return View();
            }
            return View("Neuspjeh");
        }

        public IActionResult RezultatiRoundRobin(int? id)
        {
            TakmicenjeVM obj = GetTakmicenjeVM(id);

            if (obj != null)
            {
                ViewBag.id = id;
                ViewBag.brojRundi = obj.Bracketi[0].Runde.Count();
                return View();
            }
            return View("Neuspjeh");
        }

        public IActionResult EvidentirajMec(int? id)
        {
            TakmicenjeVM obj = GetTakmicenjeVM(id);
            ViewBag.id = id;
            ViewBag.brojRundi = obj.Bracketi[0].Runde.Count();


            return View();
        }


        [HttpPost]
        public IActionResult Dodaj(CreateTakmicenjeVM objekat)
        {
            if (ModelState.IsValid)
            {
                TakmicenjeValidator validator = new TakmicenjeValidator();
                List<(string key, string error)> listaerrora = validator.VratiListuErroraAkcijaDodaj(objekat,
                    db.Takmicenja.Select(x => x.Naziv).ToList(),
                    db.Igraci.ToList());

                if (listaerrora.Count() == 0)
                {
                    using (var transakcija = db.Database.BeginTransaction())//sigurnost u opasnim situacijama 
                    {
                        try
                        {
                            Takmicenje novo = new Takmicenje(objekat);
                            var idUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                            novo.KreatorID = idUser.Id;
                            Feed TakmicenjeFeed = new Feed
                            {
                                Naziv = novo.Naziv + " feed",
                                DatumModifikacije = DateTime.Now
                            };
                            db.Feeds.Add(TakmicenjeFeed);
                            db.SaveChanges();
                            novo.FeedID = TakmicenjeFeed.ID;

                            db.Add(novo);
                            db.SaveChanges();

                            //dobaviti igrace iz regexa
                            if (objekat.RucniOdabir)
                            {
                                validator._listaIgraca = db.Igraci.ToList();
                                List<Igrac> svi = validator.GetListaRucnihIgraca(objekat.RucnoOdabraniIgraci);
                                foreach (Igrac i in svi)
                                {
                                    Prijava novaPrijava = new Prijava
                                    {
                                        DatumPrijave = DateTime.Now,
                                        isTim = false,
                                        Naziv = i.PrikaznoIme,
                                        TakmicenjeID = novo.ID
                                    };

                                    novaPrijava.StanjePrijave = new Stanje_Prijave(novaPrijava.ID);
                                    db.Prijave.Add(novaPrijava);
                                    db.SaveChanges();

                                    Prijava_igrac PrijavaIgracPodatak = new Prijava_igrac
                                    {
                                        IgracID = i.ID,
                                        PrijavaID = novaPrijava.ID
                                    };
                                    db.PrijaveIgraci.Add(PrijavaIgracPodatak);
                                    db.SaveChanges();
                                }
                            }
                            transakcija.Commit();
                            return Redirect("/Takmicenje/Prikaz/" + novo.ID);
                        }
                        catch (DbUpdateException er)
                        {
                            transakcija.Rollback();
                            ModelState.AddModelError("", "Doslo je do greške prilikom spašavanja u bazu");
                        }
                    }
                }
                else
                {
                    //ako je validator vratio errore ovdje cemo ih pametno stavit u modelstate kako bi se prikazali na viewu
                    foreach ((string key, string err) i in listaerrora)
                    {
                        ModelState.AddModelError(i.key, i.err);
                    }
                }
            }
            LoadViewBag();
            return View(objekat);
        }
        
        public IActionResult Dodaj()
        {
            LoadViewBag();
            return View();
        }
        public void LoadViewBag()
        {
            ViewBag.kategorije = db.Kategorije.Select(s => new ComboBoxVM { ID = s.ID, Opis = s.Opis }).ToList();
            ViewBag.sistemi = db.SistemiTakmicenja.Select(s => new ComboBoxVM { ID = s.ID, Opis = s.Opis }).ToList();
            ViewBag.vrste = db.VrsteTakmicenja.Select(s => new ComboBoxVM { ID = s.ID, Opis = s.Naziv }).ToList();
            ViewBag.statusi = db.StatusiTakmicenja.Select(s => new ComboBoxVM { ID = s.ID, Opis = s.Opis }).ToList();
        }

        public IActionResult Prikaz(int? id)
        {
            if (id == null)
            {
                return View("/Takmicenje/Neuspjeh");
            }
            //potreban query za broj rundi,u bracketima se nalazi takmicenjeID ,bar bi trebalo opotrebna migracija
            TakmicenjeVM obj = GetTakmicenjeVM(id);
            if (obj != null)
                return View(obj);
            
            return Redirect("/Takmicenje/Neuspjeh");
        }

        public IActionResult Edit(int id)
        {
            Takmicenje obj = db.Takmicenja.Find(id);
            var idUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            if (obj.KreatorID != idUser.Id)
                return VratiNijeAutorizovan();
            if (obj != null)
            {
                EditTakmicenjeVM ob1 = new EditTakmicenjeVM(obj);
                LoadViewBag();
                return View(ob1);
            }
            return Redirect("/Takmicenje/Neuspjeh");
        }
        [HttpPost]
        public IActionResult Edit(EditTakmicenjeVM objekat)//dodatiiii kod.....za rucni unos
        {
            if (ModelState.IsValid)
            {
                TakmicenjeValidator validator = new TakmicenjeValidator();
                List<(string key, string error)> listaerrora = validator.VratiListuErroraAkcijaEdit(objekat, db.Takmicenja.ToList());
                if (listaerrora.Count() == 0)
                {
                    Takmicenje obj = db.Takmicenja.Find(objekat.ID);
                    if (obj != null)
                    {
                        using (var transakcija = db.Database.BeginTransaction())
                        {
                            try
                            {
                                obj.Naziv = objekat.Naziv;
                                obj.DatumPocetka = objekat.DatumPocetka;
                                obj.DatumZavrsetka = objekat.DatumZavrsetka;
                                if (objekat.RokPocetkaPrijave != null)
                                    //samo ako su registracije otvorene promijeni ove ovdje stvari jer se one ne postavljaju na rucnom unosu
                                {
                                    obj.RokPocetkaPrijave = objekat.RokPocetkaPrijave;
                                    obj.RokZavrsetkaPrijave = objekat.RokZavrsetkaPrijave;
                                    obj.MinimalniELO = objekat.MinimalniELO ?? 0;
                                    obj.KategorijaID = objekat.KategorijaID;
                                    obj.VrstaID = objekat.VrstaID;
                                }
                                obj.StatusID = objekat.StatusID;
                                db.Update(obj);
                                db.SaveChanges();

                                Feed FidObjekat = db.Feeds.Find(obj.FeedID);
                                FidObjekat.Naziv = obj.Naziv + " feed";
                                FidObjekat.DatumModifikacije = DateTime.Now;
                                db.Update(FidObjekat);
                                db.SaveChanges();

                                transakcija.Commit();
                                return Redirect("/Takmicenje/Prikaz/" + obj.ID);
                            }
                            catch (DbUpdateException er)
                            {
                                transakcija.Rollback();
                            }

                        }
                    }
                }
                else
                {
                    //ako je validator vratio errore ovdje cemo ih pametno stavit u modelstate kako bi se prikazali na viewu
                    foreach ((string key, string err) i in listaerrora)
                    {
                        ModelState.AddModelError(i.key, i.err);
                    }
                }
            }
            LoadViewBag();
            return View(objekat);
        }
        public IActionResult VratiNijeAutorizovan()
        {
            ViewBag.poruka = "Niste autorizovani za takvu radnju";
            return View("Neuspjeh");
        }
        public IActionResult Obrisi(int? id)
        {
            if (id == null)
            {
                return View("/Takmicenje/Neuspjeh");
            }
            else
            {
                Takmicenje obj = db.Takmicenja.Find(id);
                var idUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                if (obj.KreatorID != idUser.Id)
                    return VratiNijeAutorizovan();
                if (obj != null)
                {
                    TakmicenjeVM takmicenjeobj = new TakmicenjeVM
                    {
                        ID = obj.ID,
                        Naziv = obj.Naziv
                    };
                    return View(takmicenjeobj);
                }
            }
            return Redirect("/Takmicenje/Neuspjeh");
        }
        [HttpPost]
        public IActionResult PotvrdaBrisanja(int ID)
        {
            //mozda se okrenut na talicev princip tj da ne budu 2 zahtjeva na bazu nego samo alert izbacit 
            //preko js : da li ste sigurni u operaciju,mada nece se svaki dan brisati takmicenje ne vjerujem da ce veliki load
            //biti na bazi,ali u svakom slucaju najmanji problem je skratit jednu funkciju i ubacit alert,ovaj princip je
            //svakako medju prvim koji sam naucio,ne znaci da je ispravan
            try
            {
                Takmicenje obj = db.Takmicenja.Include(x => x.Feed).Where(c => c.ID == ID).SingleOrDefault();
                db.Feeds.Remove(obj.Feed);
                db.Takmicenja.Remove(obj);
                db.SaveChanges();
                return Redirect("/Takmicenje/Index");
            }
            catch (DbUpdateException err)
            {

            }
            return Redirect("/Takmicenje/Neuspjeh");
        }

        public bool PostojiTakmicenje(string naziv)
        {
            if (db.Takmicenja.Where(s => s.Naziv == naziv).Count() > 0)
                return true;
            return false;
        }
        public IActionResult Uspjeh()
        {
            return View();
        }
        public IActionResult Neuspjeh()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prijava(TakmicenjePrijavaVM prijava)
        {

            if (ModelState.IsValid)
            {
                Prijava_igrac pi = db.PrijaveIgraci.Where(p => p.Prijava.TakmicenjeID == prijava.takmicenjeID && p.IgracID == prijava.Igrac1ID).SingleOrDefault();
                if (pi != null)
                    ModelState.AddModelError(nameof(prijava.Igrac1ID), "Igrač je već prijavljen na takmičenje.");

                if (prijava.Igrac1ID == null)
                    ModelState.AddModelError(nameof(prijava.Igrac1ID), "Polje igrač1 je obavezno.");
              
                if (prijava.isTim)
                {
                    Prijava_igrac pi2 = db.PrijaveIgraci.Where(p => p.Prijava.TakmicenjeID == prijava.takmicenjeID && p.IgracID == prijava.Igrac2ID).SingleOrDefault();
                    if (pi2 != null)
                        ModelState.AddModelError(nameof(prijava.Igrac2ID), "Igrač je već prijavljen na takmičenje.");
                    if (prijava.Naziv == null)
                        ModelState.AddModelError(nameof(prijava.Naziv), "Polje naziv je obavezno.");
                    if (prijava.Igrac2ID == null)
                        ModelState.AddModelError(nameof(prijava.Igrac2ID), "Polje igrač2 je obavezno.");
                    if (db.BlokListe.Where(x => x.IgracID == prijava.Igrac2ID && x.TakmicenjeID == prijava.takmicenjeID).SingleOrDefault() != null)
                        ModelState.AddModelError(nameof(prijava.Igrac2ID), "Ovaj igrač je blokiran na ovom takmičenju.");
                }

                if (prijava.Igrac1ID == prijava.Igrac2ID && prijava.Igrac2ID != null)
                    ModelState.AddModelError(nameof(prijava.Igrac2ID), "Ne možete dodati istog igrača kao saigrača.");

                if (db.BlokListe.Where(x => x.IgracID == prijava.Igrac1ID && x.TakmicenjeID == prijava.takmicenjeID).SingleOrDefault() != null)
                    ModelState.AddModelError(nameof(prijava.Igrac1ID), "Blokirani ste na ovom takmičenju.");

                if (ModelState.ErrorCount == 0)
                {
                    Prijava nova = new Prijava
                    {
                        DatumPrijave = DateTime.Now,
                        TakmicenjeID = prijava.takmicenjeID,
                        isTim = prijava.isTim,
                        Naziv = prijava.Naziv
                    };
                    nova.StanjePrijave = new Stanje_Prijave(nova.ID);
                    if (!prijava.isTim)
                        nova.Naziv = db.Igraci.Find(prijava.Igrac1ID).PrikaznoIme;

                    if (PostojiLiPrijava(nova.Naziv, prijava.takmicenjeID))
                    {
                        ModelState.AddModelError(nameof(prijava.Naziv), "Ime je zauzeto.");
                        LoadViewBagPrijava(prijava.takmicenjeID);
                        return View(prijava);
                    }
                    db.Takmicenja.Find(prijava.takmicenjeID).Prijave.Add(nova);
                    db.SaveChanges();
                    KreirajPrijavuIgrac(prijava, nova.ID);

                    return Redirect("/Takmicenje/UspjesnaPrijava");
                }
            }
            LoadViewBagPrijava(prijava.takmicenjeID);

            return View(prijava);
        }

        public IActionResult UspjesnaPrijava()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Prijava(int takmID)
        {
            Takmicenje takm = db.Takmicenja.Where(t => t.ID == takmID).Include(t => t.Vrsta).SingleOrDefault();
            if (takm == null)
                return View("Neuspjeh");
            TakmicenjePrijavaVM tp = new TakmicenjePrijavaVM
            {
                takmicenjeID = takmID,
                isTim = true
            };

            if (takm.Vrsta.Naziv == "Single")
                tp.isTim = false;

            ViewBag.igraci = db.Igraci.Where(i => i.ELO >= takm.MinimalniELO).Select(i => new ComboBoxVM { ID = i.ID, Opis = i.PrikaznoIme }).ToList();

            return View(tp);
        }


        public IActionResult Otkazi(int prijavaID)
        {
            Prijava p = db.Prijave.Find(prijavaID);
            if (p != null)
            {
                Stanje_Prijave sp = db.StanjaPrijave.Where(x => x.PrijavaID == prijavaID).SingleOrDefault();
                if (sp != null)
                    db.Remove(sp);
                List<Prijava_igrac> pi = db.PrijaveIgraci.Where(x => x.PrijavaID == prijavaID).ToList();
                if (pi != null && pi.Count > 1)
                {
                    db.Remove(pi[1]);
                }
                db.Remove(pi[0]);

                db.Remove(p);
                db.SaveChanges();
                return View("OtkazivanjePrijave");
            }
            return View("Neuspjeh");
        }

        private void LoadViewBagPrijava(int id)
        {
            Takmicenje t = db.Takmicenja.Find(id);
            ViewBag.igraci = db.Igraci.Where(i => i.ELO >= t.MinimalniELO).Select(i => new ComboBoxVM { ID = i.ID, Opis = i.PrikaznoIme }).ToList();
        }

        private void KreirajPrijavuIgrac(TakmicenjePrijavaVM prijava, int id)
        {

            Prijava_igrac prijava_Igrac1 = new Prijava_igrac
            {
                IgracID = prijava.Igrac1ID ?? default(int),
                PrijavaID = id
            };

            db.Add(prijava_Igrac1);

            if (prijava.isTim)
            {
                Prijava_igrac prijava_Igrac2 = new Prijava_igrac
                {
                    IgracID = prijava.Igrac2ID ?? default(int),
                    PrijavaID = id
                };
                db.Add(prijava_Igrac2);
            }
            db.SaveChanges();
        }

        public IActionResult BlokirajPrijavu(int prijavaID, string nazivTima)
        {
            Prijava p = db.Prijave.Find(prijavaID);
            if (p != null)
            {
                Stanje_Prijave sp = db.StanjaPrijave.Where(x => x.PrijavaID == prijavaID).SingleOrDefault();
                if (sp != null)
                    db.Remove(sp);
                List<Prijava_igrac> pi = db.PrijaveIgraci.Where(x => x.PrijavaID == prijavaID).ToList();
                Takmicenje t = db.Takmicenja.Find(p.TakmicenjeID);
                BlokLista blok1 = new BlokLista
                {
                    IgracID = pi[0].IgracID,
                    TakmicenjeID = t.ID
                };
                db.Add(blok1);
                if (pi != null && pi.Count > 1)
                {
                    BlokLista blok2 = new BlokLista
                    {
                        IgracID = pi[1].IgracID,
                        TakmicenjeID = t.ID
                    };
                    db.Add(blok2);
                    db.Remove(pi[1]);
                }

                db.Remove(pi[0]);
                db.Remove(p);
                db.SaveChanges();
            }

            ViewBag.takmID = p.TakmicenjeID;
            return View("BlokirajPrijavuUspjeh");
        }
        private bool PostojiLiPrijava(string naziv, int id)
        {
            Prijava prijava = db.Prijave.Where(p => p.TakmicenjeID == id && p.Naziv == naziv).SingleOrDefault();
            if (prijava != null)
                return true;
            return false;
        }
        public IActionResult Init(int ID)//mozda ce biti i task
        {
            List<string> errors = new List<string>();
            Takmicenje _takmicenje = db.Takmicenja
                .Include(tak => tak.Kategorija)
                .Include(tak => tak.Sistem)
                .Include(tak => tak.Vrsta)
                .Include(tak => tak.Status)
                .Include(tak => tak.Feed)
                .Include(tak => tak.Bracketi)
                .Include(tak => tak.Prijave).SingleOrDefault(y => y.ID == ID);
            var idUser = db.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            if (_takmicenje.KreatorID != idUser.Id)
                return VratiNijeAutorizovan();
            if (_takmicenje != null && !_takmicenje.Inicirano)
            {
                if (_takmicenje.RokPocetkaPrijave != null && _takmicenje.RokZavrsetkaPrijave > DateTime.Now)
                    errors.Add("Rok registracija mora zavrsiti prije generisanja rasporeda");
                if (_takmicenje.Prijave.Count() < 4)
                    errors.Add("Takmicenje mora imati barem 4 igraca, otvorite ponovo registracije");
                if (errors.Count() == 0)
                {
                    using (var transakcija = db.Database.BeginTransaction())
                    {
                        try
                        {
                            inicijalizator.GenerisiRaspored(_takmicenje);
                            transakcija.Commit();
                            return Redirect("/Takmicenje/Index");
                        }
                        catch (Exception err)
                        {
                            transakcija.Rollback();
                            return Redirect("/Takmicenje/Neuspjeh");
                        }
                    }
                }
            }
            errors.Add("Takmicenje ne postoji ili je vec inicirano");
            return View("Neuspjeh", errors);
        }

        public TakmicenjeVM GetTakmicenjeVM(int? id)
        {

            //potreban query za broj rundi,u bracketima se nalazi takmicenjeID ,bar bi trebalo opotrebna migracija
            //Takmicenje obj = db.Takmicenja.Include(tak => tak.Kategorija)
            //                              .Include(tak => tak.Sistem)
            //                              .Include(tak => tak.Vrsta)
            //                              .Include(tak => tak.Status)
            //                              .Include(tak => tak.Feed)
            //                              .Include(tak => tak.Prijave)
            //                              .SingleOrDefault(y => y.ID == id);
            Takmicenje obj = db.Takmicenja.Include(tak => tak.Kategorija).
                Include(tak => tak.Sistem)
                .Include(tak => tak.Vrsta)
                .Include(tak => tak.Status)
                .Include(tak => tak.Feed)
                .Include(tak => tak.Prijave)
                .Include(x => x.Bracketi)
                .ThenInclude(x => x.Runde)
                .ThenInclude(x => x.Utakmice)
                .ThenInclude(x => x.UcescaNaUtakmici)
                .ThenInclude(x => x.Igrac)
                .SingleOrDefault(y => y.ID == id);
            if (obj != null)
                return new TakmicenjeVM(obj);
            return null;
        }

    }
}