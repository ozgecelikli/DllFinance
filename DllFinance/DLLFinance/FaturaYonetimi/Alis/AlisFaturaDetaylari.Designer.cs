
namespace DllFinance.DLLFinance.FaturaYonetimi.Alis
{
    partial class AlisFaturaDetaylari
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition4 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition5 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnListele = new DevExpress.XtraEditors.SimpleButton();
            this.txtStokKodu = new DevExpress.XtraEditors.TextEdit();
            this.txtFirmaKodu = new DevExpress.XtraEditors.TextEdit();
            this.deBitisTarihi = new DevExpress.XtraEditors.DateEdit();
            this.deBaslangicTarihi = new DevExpress.XtraEditors.DateEdit();
            this.gcAlisFatura = new DevExpress.XtraGrid.GridControl();
            this.gvAlisFatura = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupFilters = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemBaslangic = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBitis = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFirma = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemStok = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemListele = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStokKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirmaKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBitisTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBitisTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBaslangicTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBaslangicTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlisFatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlisFatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslangic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFirma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListele)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnListele);
            this.layoutControl1.Controls.Add(this.txtStokKodu);
            this.layoutControl1.Controls.Add(this.txtFirmaKodu);
            this.layoutControl1.Controls.Add(this.deBitisTarihi);
            this.layoutControl1.Controls.Add(this.deBaslangicTarihi);
            this.layoutControl1.Controls.Add(this.gcAlisFatura);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(813, 450);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnListele
            // 
            this.btnListele.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnListele.Location = new System.Drawing.Point(673, 42);
            this.btnListele.Name = "btnListele";
            this.btnListele.Size = new System.Drawing.Size(128, 27);
            this.btnListele.StyleController = this.layoutControl1;
            this.btnListele.TabIndex = 9;
            this.btnListele.Text = "Listele";
            // 
            // txtStokKodu
            // 
            this.txtStokKodu.Location = new System.Drawing.Point(429, 42);
            this.txtStokKodu.Name = "txtStokKodu";
            this.txtStokKodu.Size = new System.Drawing.Size(240, 22);
            this.txtStokKodu.StyleController = this.layoutControl1;
            this.txtStokKodu.TabIndex = 8;
            // 
            // txtFirmaKodu
            // 
            this.txtFirmaKodu.Location = new System.Drawing.Point(103, 42);
            this.txtFirmaKodu.Name = "txtFirmaKodu";
            this.txtFirmaKodu.Size = new System.Drawing.Size(235, 22);
            this.txtFirmaKodu.StyleController = this.layoutControl1;
            this.txtFirmaKodu.TabIndex = 7;
            // 
            // deBitisTarihi
            // 
            this.deBitisTarihi.EditValue = null;
            this.deBitisTarihi.Location = new System.Drawing.Point(429, 14);
            this.deBitisTarihi.Name = "deBitisTarihi";
            this.deBitisTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBitisTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBitisTarihi.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.deBitisTarihi.Properties.MaskSettings.Set("mask", "dd.MM.yyyy");
            this.deBitisTarihi.Size = new System.Drawing.Size(240, 22);
            this.deBitisTarihi.StyleController = this.layoutControl1;
            this.deBitisTarihi.TabIndex = 6;
            // 
            // deBaslangicTarihi
            // 
            this.deBaslangicTarihi.EditValue = null;
            this.deBaslangicTarihi.Location = new System.Drawing.Point(103, 14);
            this.deBaslangicTarihi.Name = "deBaslangicTarihi";
            this.deBaslangicTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBaslangicTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBaslangicTarihi.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.deBaslangicTarihi.Properties.MaskSettings.Set("mask", "dd.MM.yyyy");
            this.deBaslangicTarihi.Size = new System.Drawing.Size(235, 22);
            this.deBaslangicTarihi.StyleController = this.layoutControl1;
            this.deBaslangicTarihi.TabIndex = 5;
            // 
            // gcAlisFatura
            // 
            this.gcAlisFatura.Location = new System.Drawing.Point(12, 83);
            this.gcAlisFatura.MainView = this.gvAlisFatura;
            this.gcAlisFatura.Name = "gcAlisFatura";
            this.gcAlisFatura.Size = new System.Drawing.Size(789, 355);
            this.gcAlisFatura.TabIndex = 4;
            this.gcAlisFatura.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAlisFatura});
            // 
            // gvAlisFatura
            // 
            this.gvAlisFatura.GridControl = this.gcAlisFatura;
            this.gvAlisFatura.Name = "gvAlisFatura";
            this.gvAlisFatura.OptionsView.ColumnAutoWidth = false;
            this.gvAlisFatura.OptionsView.ShowAutoFilterRow = true;
            this.gvAlisFatura.OptionsView.ShowGroupPanel = false;
            this.gvAlisFatura.OptionsView.ShowFooter = true;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupFilters,
            this.layoutControlItemGrid});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(813, 450);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroupFilters
            // 
            this.layoutControlGroupFilters.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroupFilters.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroupFilters.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupFilters.GroupBordersVisible = true;
            this.layoutControlGroupFilters.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemBaslangic,
            this.layoutControlItemBitis,
            this.layoutControlItemFirma,
            this.layoutControlItemStok,
            this.layoutControlItemListele});
            this.layoutControlGroupFilters.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            this.layoutControlGroupFilters.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupFilters.Name = "layoutControlGroupFilters";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition1.Width = 20D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 20D;
            columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition3.Width = 20D;
            columnDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition4.Width = 20D;
            columnDefinition5.SizeType = System.Windows.Forms.SizeType.AutoSize;
            this.layoutControlGroupFilters.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] {
            columnDefinition1,
            columnDefinition2,
            columnDefinition3,
            columnDefinition4,
            columnDefinition5});
            rowDefinition1.Height = 26D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.AutoSize;
            this.layoutControlGroupFilters.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] {
            rowDefinition1});
            this.layoutControlGroupFilters.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlGroupFilters.Size = new System.Drawing.Size(793, 71);
            this.layoutControlGroupFilters.Text = "Filtreler";
            // 
            // layoutControlItemBaslangic
            // 
            this.layoutControlItemBaslangic.Control = this.deBaslangicTarihi;
            this.layoutControlItemBaslangic.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemBaslangic.Name = "layoutControlItemBaslangic";
            this.layoutControlItemBaslangic.OptionsTableLayoutItem.ColumnIndex = 0;
            this.layoutControlItemBaslangic.Size = new System.Drawing.Size(202, 29);
            this.layoutControlItemBaslangic.Text = "Başlama Tarihi";
            this.layoutControlItemBaslangic.TextSize = new System.Drawing.Size(87, 16);
            // 
            // layoutControlItemBitis
            // 
            this.layoutControlItemBitis.Control = this.deBitisTarihi;
            this.layoutControlItemBitis.Location = new System.Drawing.Point(202, 0);
            this.layoutControlItemBitis.Name = "layoutControlItemBitis";
            this.layoutControlItemBitis.OptionsTableLayoutItem.ColumnIndex = 1;
            this.layoutControlItemBitis.Size = new System.Drawing.Size(203, 29);
            this.layoutControlItemBitis.Text = "Bitiş Tarihi";
            this.layoutControlItemBitis.TextSize = new System.Drawing.Size(87, 16);
            // 
            // layoutControlItemFirma
            // 
            this.layoutControlItemFirma.Control = this.txtFirmaKodu;
            this.layoutControlItemFirma.Location = new System.Drawing.Point(405, 0);
            this.layoutControlItemFirma.Name = "layoutControlItemFirma";
            this.layoutControlItemFirma.OptionsTableLayoutItem.ColumnIndex = 2;
            this.layoutControlItemFirma.Size = new System.Drawing.Size(202, 29);
            this.layoutControlItemFirma.Text = "Firma Kodu";
            this.layoutControlItemFirma.TextSize = new System.Drawing.Size(87, 16);
            // 
            // layoutControlItemStok
            // 
            this.layoutControlItemStok.Control = this.txtStokKodu;
            this.layoutControlItemStok.Location = new System.Drawing.Point(607, 0);
            this.layoutControlItemStok.Name = "layoutControlItemStok";
            this.layoutControlItemStok.OptionsTableLayoutItem.ColumnIndex = 3;
            this.layoutControlItemStok.Size = new System.Drawing.Size(202, 29);
            this.layoutControlItemStok.Text = "Stok Kodu";
            this.layoutControlItemStok.TextSize = new System.Drawing.Size(87, 16);
            // 
            // layoutControlItemListele
            // 
            this.layoutControlItemListele.Control = this.btnListele;
            this.layoutControlItemListele.Location = new System.Drawing.Point(809, 0);
            this.layoutControlItemListele.MaxSize = new System.Drawing.Size(140, 31);
            this.layoutControlItemListele.MinSize = new System.Drawing.Size(100, 31);
            this.layoutControlItemListele.Name = "layoutControlItemListele";
            this.layoutControlItemListele.OptionsTableLayoutItem.ColumnIndex = 4;
            this.layoutControlItemListele.Size = new System.Drawing.Size(140, 29);
            this.layoutControlItemListele.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemListele.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemListele.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gcAlisFatura;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(793, 359);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // AlisFaturaDetaylari
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "AlisFaturaDetaylari";
            this.Size = new System.Drawing.Size(813, 450);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtStokKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirmaKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBitisTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBitisTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBaslangicTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBaslangicTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlisFatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlisFatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupFilters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBaslangic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBitis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFirma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemListele)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnListele;
        private DevExpress.XtraEditors.TextEdit txtStokKodu;
        private DevExpress.XtraEditors.TextEdit txtFirmaKodu;
        private DevExpress.XtraEditors.DateEdit deBitisTarihi;
        private DevExpress.XtraEditors.DateEdit deBaslangicTarihi;
        private DevExpress.XtraGrid.GridControl gcAlisFatura;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAlisFatura;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupFilters;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBaslangic;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBitis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFirma;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemStok;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemListele;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
    }
}
