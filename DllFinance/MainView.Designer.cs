
namespace DllFinance
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.ace_ozet = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_ozet_alissatis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_ozet_nakitakis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_ozet_dashboard = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_alis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_alis_detay = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_alis_cari = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_alis_urun = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_satis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_satis_detay = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_satis_cari = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_satis_urun = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_alissatis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_finans = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_finans_ceksenet = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_finans_kredi = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_finans_leasing = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_banka = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_kasa = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_kredikartlari = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_periyodik = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_periyodik_gelir = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_faturayonetimi_periyodik_gider = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlSeparator1 = new DevExpress.XtraBars.Navigation.AccordionControlSeparator();
            this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
            this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabControl1.Location = new System.Drawing.Point(325, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(780, 395);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabStop = false;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_ozet,
            this.ace_faturayonetimi,
            this.ace_faturayonetimi_finans,
            this.ace_faturayonetimi_periyodik,
            this.accordionControlSeparator1});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(325, 587);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // ace_ozet
            // 
            this.ace_ozet.Appearance.Default.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.ace_ozet.Appearance.Default.Options.UseFont = true;
            this.ace_ozet.Appearance.Disabled.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.ace_ozet.Appearance.Disabled.Options.UseFont = true;
            this.ace_ozet.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.ace_ozet.Appearance.Hovered.Options.UseFont = true;
            this.ace_ozet.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.ace_ozet.Appearance.Normal.Options.UseFont = true;
            this.ace_ozet.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_ozet_alissatis,
            this.ace_ozet_nakitakis,
            this.ace_ozet_dashboard});
            this.ace_ozet.Expanded = true;
            this.ace_ozet.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ace_ozet.ImageOptions.SvgImage")));
            this.ace_ozet.Name = "ace_ozet";
            this.ace_ozet.Text = "Özet";
            // 
            // ace_ozet_alissatis
            // 
            this.ace_ozet_alissatis.Name = "ace_ozet_alissatis";
            this.ace_ozet_alissatis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_ozet_alissatis.Text = "Alış-Satış ";
            // 
            // ace_ozet_nakitakis
            // 
            this.ace_ozet_nakitakis.Name = "ace_ozet_nakitakis";
            this.ace_ozet_nakitakis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_ozet_nakitakis.Text = "Nakit Akış";
            // 
            // ace_ozet_dashboard
            // 
            this.ace_ozet_dashboard.Name = "ace_ozet_dashboard";
            this.ace_ozet_dashboard.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_ozet_dashboard.Text = "Dashboard";
            // 
            // ace_faturayonetimi
            // 
            this.ace_faturayonetimi.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_faturayonetimi_alis,
            this.ace_faturayonetimi_satis,
            this.ace_faturayonetimi_alissatis});
            this.ace_faturayonetimi.Expanded = true;
            this.ace_faturayonetimi.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ace_faturayonetimi.ImageOptions.SvgImage")));
            this.ace_faturayonetimi.Name = "ace_faturayonetimi";
            this.ace_faturayonetimi.Text = "Fatura Yönetimi";
            // 
            // ace_faturayonetimi_alis
            // 
            this.ace_faturayonetimi_alis.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_faturayonetimi_alis_detay,
            this.ace_faturayonetimi_alis_cari,
            this.ace_faturayonetimi_alis_urun});
            this.ace_faturayonetimi_alis.Expanded = true;
            this.ace_faturayonetimi_alis.Name = "ace_faturayonetimi_alis";
            this.ace_faturayonetimi_alis.Text = "Alış Faturaları";
            // 
            // ace_faturayonetimi_alis_detay
            // 
            this.ace_faturayonetimi_alis_detay.Name = "ace_faturayonetimi_alis_detay";
            this.ace_faturayonetimi_alis_detay.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_alis_detay.Text = "Alış Fatura Detayları";
            // 
            // ace_faturayonetimi_alis_cari
            // 
            this.ace_faturayonetimi_alis_cari.Name = "ace_faturayonetimi_alis_cari";
            this.ace_faturayonetimi_alis_cari.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_alis_cari.Text = "Alış Cari Rapor";
            // 
            // ace_faturayonetimi_alis_urun
            // 
            this.ace_faturayonetimi_alis_urun.Name = "ace_faturayonetimi_alis_urun";
            this.ace_faturayonetimi_alis_urun.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_alis_urun.Text = "Alış Ürün Raporu";
            // 
            // ace_faturayonetimi_satis
            // 
            this.ace_faturayonetimi_satis.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_faturayonetimi_satis_detay,
            this.ace_faturayonetimi_satis_cari,
            this.ace_faturayonetimi_satis_urun});
            this.ace_faturayonetimi_satis.Expanded = true;
            this.ace_faturayonetimi_satis.Name = "ace_faturayonetimi_satis";
            this.ace_faturayonetimi_satis.Text = "Satış Faturaları";
            // 
            // ace_faturayonetimi_satis_detay
            // 
            this.ace_faturayonetimi_satis_detay.Name = "ace_faturayonetimi_satis_detay";
            this.ace_faturayonetimi_satis_detay.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_satis_detay.Text = "Satış Fatura Detayları";
            // 
            // ace_faturayonetimi_satis_cari
            // 
            this.ace_faturayonetimi_satis_cari.Name = "ace_faturayonetimi_satis_cari";
            this.ace_faturayonetimi_satis_cari.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_satis_cari.Text = "Satış Cari Rapor";
            // 
            // ace_faturayonetimi_satis_urun
            // 
            this.ace_faturayonetimi_satis_urun.Name = "ace_faturayonetimi_satis_urun";
            this.ace_faturayonetimi_satis_urun.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_satis_urun.Text = "Satış Ürün Raporu";
            // 
            // ace_faturayonetimi_alissatis
            // 
            this.ace_faturayonetimi_alissatis.Name = "ace_faturayonetimi_alissatis";
            this.ace_faturayonetimi_alissatis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_alissatis.Text = "Alış-Satış Karşılaştırma";
            // 
            // ace_faturayonetimi_finans
            // 
            this.ace_faturayonetimi_finans.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_faturayonetimi_finans_ceksenet,
            this.ace_faturayonetimi_finans_kredi,
            this.ace_faturayonetimi_finans_leasing,
            this.ace_faturayonetimi_banka,
            this.ace_faturayonetimi_kasa,
            this.ace_faturayonetimi_kredikartlari});
            this.ace_faturayonetimi_finans.Expanded = true;
            this.ace_faturayonetimi_finans.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ace_faturayonetimi_finans.ImageOptions.SvgImage")));
            this.ace_faturayonetimi_finans.Name = "ace_faturayonetimi_finans";
            this.ace_faturayonetimi_finans.Text = "Finansal Yükümlülükler ve Ödemeler";
            // 
            // ace_faturayonetimi_finans_ceksenet
            // 
            this.ace_faturayonetimi_finans_ceksenet.Name = "ace_faturayonetimi_finans_ceksenet";
            this.ace_faturayonetimi_finans_ceksenet.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_finans_ceksenet.Text = "Çek ve Senet";
            // 
            // ace_faturayonetimi_finans_kredi
            // 
            this.ace_faturayonetimi_finans_kredi.Name = "ace_faturayonetimi_finans_kredi";
            this.ace_faturayonetimi_finans_kredi.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_finans_kredi.Text = "Kredi";
            // 
            // ace_faturayonetimi_finans_leasing
            // 
            this.ace_faturayonetimi_finans_leasing.Name = "ace_faturayonetimi_finans_leasing";
            this.ace_faturayonetimi_finans_leasing.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_finans_leasing.Text = "Leasing";
            // 
            // ace_faturayonetimi_banka
            // 
            this.ace_faturayonetimi_banka.Name = "ace_faturayonetimi_banka";
            this.ace_faturayonetimi_banka.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_banka.Text = "Banka Ödemeleri ve Tahsilatları";
            // 
            // ace_faturayonetimi_kasa
            // 
            this.ace_faturayonetimi_kasa.Name = "ace_faturayonetimi_kasa";
            this.ace_faturayonetimi_kasa.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_kasa.Text = "Kasa Ödeme ve Tahsilatları";
            // 
            // ace_faturayonetimi_kredikartlari
            // 
            this.ace_faturayonetimi_kredikartlari.Name = "ace_faturayonetimi_kredikartlari";
            this.ace_faturayonetimi_kredikartlari.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_kredikartlari.Text = "Kredi Kartları";
            // 
            // ace_faturayonetimi_periyodik
            // 
            this.ace_faturayonetimi_periyodik.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_faturayonetimi_periyodik_gelir,
            this.ace_faturayonetimi_periyodik_gider});
            this.ace_faturayonetimi_periyodik.Expanded = true;
            this.ace_faturayonetimi_periyodik.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ace_faturayonetimi_periyodik.ImageOptions.SvgImage")));
            this.ace_faturayonetimi_periyodik.Name = "ace_faturayonetimi_periyodik";
            this.ace_faturayonetimi_periyodik.Text = "Kesinleşmemiş Gelirler ve Giderler";
            // 
            // ace_faturayonetimi_periyodik_gelir
            // 
            this.ace_faturayonetimi_periyodik_gelir.Name = "ace_faturayonetimi_periyodik_gelir";
            this.ace_faturayonetimi_periyodik_gelir.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_periyodik_gelir.Text = "Gelirler";
            // 
            // ace_faturayonetimi_periyodik_gider
            // 
            this.ace_faturayonetimi_periyodik_gider.Name = "ace_faturayonetimi_periyodik_gider";
            this.ace_faturayonetimi_periyodik_gider.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_faturayonetimi_periyodik_gider.Text = "Giderler";
            // 
            // accordionControlSeparator1
            // 
            this.accordionControlSeparator1.Name = "accordionControlSeparator1";
            // 
            // tabFormContentContainer1
            // 
            this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 0);
            this.tabFormContentContainer1.Name = "tabFormContentContainer1";
            this.tabFormContentContainer1.Size = new System.Drawing.Size(298, 284);
            this.tabFormContentContainer1.TabIndex = 3;
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl2.Size = new System.Drawing.Size(300, 300);
            this.xtraTabControl2.TabIndex = 3;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.tabFormContentContainer1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(298, 284);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // documentManager1
            // 
            this.documentManager1.MdiParent = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1105, 587);
            this.Controls.Add(this.accordionControl1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_ozet;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_finans;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_finans_ceksenet;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_finans_kredi;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_periyodik;
        private DevExpress.XtraBars.Navigation.AccordionControlSeparator accordionControlSeparator1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_finans_leasing;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_alis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_satis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_alis_detay;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_alis_cari;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_alis_urun;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_satis_detay;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_satis_cari;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_satis_urun;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_alissatis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_periyodik_gelir;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_periyodik_gider;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_banka;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_kasa;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_faturayonetimi_kredikartlari;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_ozet_alissatis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_ozet_nakitakis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_ozet_dashboard;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
    }
}

