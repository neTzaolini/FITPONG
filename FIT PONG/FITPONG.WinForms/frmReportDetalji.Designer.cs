﻿namespace FIT_PONG.WinForms
{
    partial class frmReportDetalji
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblNaslov = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPosiljatelj = new System.Windows.Forms.TextBox();
            this.lblDatum = new System.Windows.Forms.Label();
            this.dTPDatum = new System.Windows.Forms.DateTimePicker();
            this.lblSadrzaj = new System.Windows.Forms.Label();
            this.rtxtSadrzaj = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvPrilozi = new System.Windows.Forms.ListView();
            this.lblPrilozi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNaslov
            // 
            this.lblNaslov.AutoSize = true;
            this.lblNaslov.Location = new System.Drawing.Point(47, 35);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(40, 13);
            this.lblNaslov.TabIndex = 0;
            this.lblNaslov.Text = "Naslov";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(50, 61);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.ReadOnly = true;
            this.txtNaziv.Size = new System.Drawing.Size(234, 20);
            this.txtNaziv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Posiljatelj";
            // 
            // txtPosiljatelj
            // 
            this.txtPosiljatelj.Location = new System.Drawing.Point(50, 138);
            this.txtPosiljatelj.Name = "txtPosiljatelj";
            this.txtPosiljatelj.ReadOnly = true;
            this.txtPosiljatelj.Size = new System.Drawing.Size(234, 20);
            this.txtPosiljatelj.TabIndex = 1;
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Location = new System.Drawing.Point(401, 36);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(38, 13);
            this.lblDatum.TabIndex = 0;
            this.lblDatum.Text = "Datum";
            // 
            // dTPDatum
            // 
            this.dTPDatum.Enabled = false;
            this.dTPDatum.Location = new System.Drawing.Point(404, 61);
            this.dTPDatum.Name = "dTPDatum";
            this.dTPDatum.Size = new System.Drawing.Size(235, 20);
            this.dTPDatum.TabIndex = 2;
            // 
            // lblSadrzaj
            // 
            this.lblSadrzaj.AutoSize = true;
            this.lblSadrzaj.Location = new System.Drawing.Point(47, 204);
            this.lblSadrzaj.Name = "lblSadrzaj";
            this.lblSadrzaj.Size = new System.Drawing.Size(42, 13);
            this.lblSadrzaj.TabIndex = 0;
            this.lblSadrzaj.Text = "Sadrzaj";
            // 
            // rtxtSadrzaj
            // 
            this.rtxtSadrzaj.Enabled = false;
            this.rtxtSadrzaj.Location = new System.Drawing.Point(50, 221);
            this.rtxtSadrzaj.Name = "rtxtSadrzaj";
            this.rtxtSadrzaj.Size = new System.Drawing.Size(300, 206);
            this.rtxtSadrzaj.TabIndex = 3;
            this.rtxtSadrzaj.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lvPrilozi
            // 
            this.lvPrilozi.HideSelection = false;
            this.lvPrilozi.LargeImageList = this.imageList1;
            this.lvPrilozi.Location = new System.Drawing.Point(404, 164);
            this.lvPrilozi.Name = "lvPrilozi";
            this.lvPrilozi.Size = new System.Drawing.Size(373, 263);
            this.lvPrilozi.SmallImageList = this.imageList1;
            this.lvPrilozi.TabIndex = 4;
            this.lvPrilozi.UseCompatibleStateImageBehavior = false;
            this.lvPrilozi.ItemActivate += new System.EventHandler(this.lvPrilozi_ItemActivate);
            // 
            // lblPrilozi
            // 
            this.lblPrilozi.AutoSize = true;
            this.lblPrilozi.Location = new System.Drawing.Point(401, 138);
            this.lblPrilozi.Name = "lblPrilozi";
            this.lblPrilozi.Size = new System.Drawing.Size(34, 13);
            this.lblPrilozi.TabIndex = 0;
            this.lblPrilozi.Text = "Prilozi";
            // 
            // frmReportDetalji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvPrilozi);
            this.Controls.Add(this.rtxtSadrzaj);
            this.Controls.Add(this.dTPDatum);
            this.Controls.Add(this.txtPosiljatelj);
            this.Controls.Add(this.lblSadrzaj);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPrilozi);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.lblNaslov);
            this.Name = "frmReportDetalji";
            this.Text = "frmReportDetalji";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPosiljatelj;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.DateTimePicker dTPDatum;
        private System.Windows.Forms.Label lblSadrzaj;
        private System.Windows.Forms.RichTextBox rtxtSadrzaj;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView lvPrilozi;
        private System.Windows.Forms.Label lblPrilozi;
    }
}