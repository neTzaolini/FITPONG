﻿using FIT_PONG.SharedModels;
using FIT_PONG.SharedModels.Requests;
using FIT_PONG.WinForms.APIServices;
using FIT_PONG.WinForms.Izvjestaji;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIT_PONG.WinForms
{
    public partial class frmIzvjestajIgraci : Form
    {
        UsersAPIService usersAPIService = new UsersAPIService();
        private string naziv;
        private List<string> selektovani;

        private PagedResponse<Users> igraci = null;
        public frmIzvjestajIgraci(string naziv, List<string> selektovani)
        {
            InitializeComponent();
            this.naziv = naziv;
            this.selektovani = selektovani;
        }

        private async void frmIzvjestajIgraci_Load(object sender, EventArgs e)
        {
            ReportParameterCollection rpc = new ReportParameterCollection();
            rpc.Add(new ReportParameter("Datum", DateTime.Now.ToString()));
            rpc.Add(new ReportParameter("Naziv", naziv));
            rpc.Add(new ReportParameter("jacaRuka_visible", selektovani.Contains("jacaruka") ? "True" : "False"));
            rpc.Add(new ReportParameter("spol_visible", selektovani.Contains("spol") ? "True" : "False"));
            rpc.Add(new ReportParameter("visina_visible", selektovani.Contains("visina") ? "True" : "False"));
            rpc.Add(new ReportParameter("elo_visible", selektovani.Contains("elo") ? "True" : "False"));
            rpc.Add(new ReportParameter("brojPosjeta_visible", selektovani.Contains("brojposjeta") ? "True" : "False"));


            igraci = await usersAPIService.GetAll<PagedResponse<Users>>();

            DSIgraci.IgraciDataTable tbl = new DSIgraci.IgraciDataTable();

            do
            {
                foreach (var igrac in igraci.Stavke)
                {
                    DSIgraci.IgraciRow red = tbl.NewIgraciRow();
                    red.ID = igrac.ID;
                    red.PrikaznoIme = igrac.PrikaznoIme;
                    red.ELO = igrac.ELO;
                    red.BrojPosjetaNaProfil = igrac.BrojPosjetaNaProfil;
                    red.Visina = igrac.Visina ?? default(double);
                    red.JacaRuka = igrac.JacaRuka;
                    red.Spol = igrac.Spol.ToString();

                    tbl.Rows.Add(red);
                }

                if (igraci.IducaStranica != null)
                {
                    int pozicija = igraci.IducaStranica.ToString().LastIndexOf("/") + 1;
                    string resurs = igraci.IducaStranica.ToString().Substring(pozicija);
                    usersAPIService = new UsersAPIService(resurs);
                    igraci = await usersAPIService.GetAll<PagedResponse<Users>>();
                }
                else
                {
                    igraci = null;
                }

            } while (igraci != null);


            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DSIgraci";
            rds.Value = tbl;


            rpvIgraci.LocalReport.ReportPath = "Izvjestaji/rptIgraci.rdlc";
            rpvIgraci.LocalReport.SetParameters(rpc);
            rpvIgraci.LocalReport.DataSources.Add(rds);

            this.rpvIgraci.RefreshReport();
        }

    }
}
