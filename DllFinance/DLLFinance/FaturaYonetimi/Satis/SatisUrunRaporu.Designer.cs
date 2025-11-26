
namespace DllFinance.DLLFinance.FaturaYonetimi.Satis
{
    partial class SatisUrunRaporu
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartCiro = new DevExpress.XtraCharts.ChartControl();
            this.chartMiktar = new DevExpress.XtraCharts.ChartControl();
            this.gcStok = new DevExpress.XtraGrid.GridControl();
            this.gvStok = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupGrid = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemVertical = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlGroupPie = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemPie = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupCiro = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemCiro = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemCharts = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMiktar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupCiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemCharts)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartCiro);
            this.layoutControl1.Controls.Add(this.chartMiktar);
            this.layoutControl1.Controls.Add(this.gcStok);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(900, 540);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartCiro
            // 
            this.chartCiro.Location = new System.Drawing.Point(467, 312);
            this.chartCiro.Name = "chartCiro";
            this.chartCiro.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartCiro.Size = new System.Drawing.Size(409, 206);
            this.chartCiro.TabIndex = 6;
            // 
            // chartMiktar
            // 
            this.chartMiktar.Location = new System.Drawing.Point(467, 50);
            this.chartMiktar.Name = "chartMiktar";
            this.chartMiktar.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartMiktar.Size = new System.Drawing.Size(409, 194);
            this.chartMiktar.TabIndex = 5;
            // 
            // gcStok
            // 
            this.gcStok.Location = new System.Drawing.Point(24, 50);
            this.gcStok.MainView = this.gvStok;
            this.gcStok.Name = "gcStok";
            this.gcStok.Size = new System.Drawing.Size(399, 468);
            this.gcStok.TabIndex = 4;
            this.gcStok.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStok});
            // 
            // gvStok
            // 
            this.gvStok.GridControl = this.gcStok;
            this.gvStok.Name = "gvStok";
            this.gvStok.OptionsView.ColumnAutoWidth = false;
            this.gvStok.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupGrid,
            this.splitterItemVertical,
            this.layoutControlGroupPie,
            this.splitterItemCharts,
            this.layoutControlGroupCiro});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(900, 540);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroupGrid
            // 
            this.layoutControlGroupGrid.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid});
            this.layoutControlGroupGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupGrid.Name = "layoutControlGroupGrid";
            this.layoutControlGroupGrid.Size = new System.Drawing.Size(443, 520);
            this.layoutControlGroupGrid.Text = "Satış Ürün Özeti";
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gcStok;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(403, 472);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // splitterItemVertical
            // 
            this.splitterItemVertical.AllowHotTrack = true;
            this.splitterItemVertical.Location = new System.Drawing.Point(443, 0);
            this.splitterItemVertical.Name = "splitterItemVertical";
            this.splitterItemVertical.Size = new System.Drawing.Size(12, 520);
            // 
            // layoutControlGroupPie
            // 
            this.layoutControlGroupPie.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemPie});
            this.layoutControlGroupPie.Location = new System.Drawing.Point(455, 0);
            this.layoutControlGroupPie.Name = "layoutControlGroupPie";
            this.layoutControlGroupPie.Size = new System.Drawing.Size(425, 244);
            this.layoutControlGroupPie.Text = "Stok Satış Miktar Dağılımı";
            // 
            // layoutControlItemPie
            // 
            this.layoutControlItemPie.Control = this.chartMiktar;
            this.layoutControlItemPie.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPie.Name = "layoutControlItemPie";
            this.layoutControlItemPie.Size = new System.Drawing.Size(413, 198);
            this.layoutControlItemPie.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemPie.TextVisible = false;
            // 
            // layoutControlGroupCiro
            // 
            this.layoutControlGroupCiro.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCiro});
            this.layoutControlGroupCiro.Location = new System.Drawing.Point(455, 256);
            this.layoutControlGroupCiro.Name = "layoutControlGroupCiro";
            this.layoutControlGroupCiro.Size = new System.Drawing.Size(425, 264);
            this.layoutControlGroupCiro.Text = "Satış Cirosu Top 10";
            // 
            // layoutControlItemCiro
            // 
            this.layoutControlItemCiro.Control = this.chartCiro;
            this.layoutControlItemCiro.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCiro.Name = "layoutControlItemCiro";
            this.layoutControlItemCiro.Size = new System.Drawing.Size(413, 210);
            this.layoutControlItemCiro.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCiro.TextVisible = false;
            // 
            // splitterItemCharts
            // 
            this.splitterItemCharts.AllowHotTrack = true;
            this.splitterItemCharts.Location = new System.Drawing.Point(455, 244);
            this.splitterItemCharts.Name = "splitterItemCharts";
            this.splitterItemCharts.Size = new System.Drawing.Size(425, 12);
            // 
            // SatisUrunRaporu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.layoutControl1);
            this.Name = "SatisUrunRaporu";
            this.Size = new System.Drawing.Size(900, 540);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMiktar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupCiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemCharts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcStok;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStok;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.SplitterItem splitterItemVertical;
        private DevExpress.XtraCharts.ChartControl chartCiro;
        private DevExpress.XtraCharts.ChartControl chartMiktar;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupPie;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPie;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupCiro;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCiro;
        private DevExpress.XtraLayout.SplitterItem splitterItemCharts;
    }
}
