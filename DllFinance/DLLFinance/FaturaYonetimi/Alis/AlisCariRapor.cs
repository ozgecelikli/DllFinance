using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Charts;
using DevExpress.XtraCharts;
namespace DllFinance.DLLFinance.FaturaYonetimi.Alis
{
    public partial class AlisCariRapor : UserControl
    {
        public AlisCariRapor()
        {
            InitializeComponent();
            LoadData();
            chartFirma.ObjectSelected += Chart_ObjectSelected; // Firma grafiği için
            chartStok.ObjectSelected += Chart_ObjectSelected; // Stok kodu grafiği için
            SetupChartContextMenus();
            InitializeChartZooming();
        }
        DataTable alisFaturaTable;
        private void InitializeChartZooming()
        {
            // Birden fazla grafik için MouseWheel olayını dinle
            chartFirma.MouseWheel += chartControl_MouseWheel;
            chartStok.MouseWheel += chartControl_MouseWheel;
            // Diğer chart kontrollerini de buraya ekleyebilirsiniz
        }

        private void chartControl_MouseWheel(object sender, MouseEventArgs e)
        {
            // Zoom miktarı, fare tekerleğiyle belirlenir
            double zoomFactor = e.Delta > 0 ? 1.1 : 0.9; // Yukarı kaydırma için 1.1, aşağı kaydırma için 0.9

            // Sender, şu anda olayla etkileşime giren ChartControl'ü belirtir
            ChartControl chart = sender as ChartControl;
            if (chart != null && chart.Diagram is XYDiagram diagram)
            {
                ApplyZoom(diagram, zoomFactor);
            }
        }

        private void ApplyZoom(XYDiagram diagram, double zoomFactor)
        {
            // X eksenini zoomlamak için
            if (diagram.AxisX != null)
            {
                // Min ve Max değerleri double'a dönüştürerek çarpma işlemi yapıyoruz
                double minX = Convert.ToDouble(diagram.AxisX.WholeRange.MinValue);
                double maxX = Convert.ToDouble(diagram.AxisX.WholeRange.MaxValue);

                diagram.AxisX.WholeRange.SetMinMaxValues(minX * zoomFactor, maxX * zoomFactor);
            }

            // Y eksenini zoomlamak için
            if (diagram.AxisY != null)
            {
                // Min ve Max değerleri double'a dönüştürerek çarpma işlemi yapıyoruz
                double minY = Convert.ToDouble(diagram.AxisY.WholeRange.MinValue);
                double maxY = Convert.ToDouble(diagram.AxisY.WholeRange.MaxValue);

                diagram.AxisY.WholeRange.SetMinMaxValues(minY * zoomFactor, maxY * zoomFactor);
            }
        }

        private void SetupChartContextMenus()
        {
            // Firma ve Stok grafikleri için context menüleri oluşturuyoruz
            chartFirma.ContextMenuStrip = CreateChartContextMenu(chartFirma);
            chartStok.ContextMenuStrip = CreateChartContextMenu(chartStok);
        }

        private ContextMenuStrip CreateChartContextMenu(ChartControl chart)
        {
            // Yeni bir ContextMenuStrip oluşturuyoruz
            ContextMenuStrip chartContextMenu = new ContextMenuStrip();

            // Menüye grafik türleri ekliyoruz
            chartContextMenu.Items.Add("Bar Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.Bar));
            chartContextMenu.Items.Add("Pasta Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.Pie));
            chartContextMenu.Items.Add("Çizgi Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.Line));
            chartContextMenu.Items.Add("Alan Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.Area));
            chartContextMenu.Items.Add("Adım Çizgi Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.StepLine));
            chartContextMenu.Items.Add("Spline Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.Spline));
            chartContextMenu.Items.Add("Tam Yığılmış Bar Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.FullStackedBar));
            chartContextMenu.Items.Add("Yığılmış Bar Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.StackedBar));
            chartContextMenu.Items.Add("Yığılmış Alan Grafiği", null, (s, e) => ChangeChartType(chart, ViewType.StackedArea));


            return chartContextMenu;
        }
        
        private void ChangeChartType(ChartControl chart, ViewType viewType)
        {
            // Seçilen grafik türünü uygula
            chart.Series[0].ChangeView(viewType);  // İlk seri üzerinde değişiklik yapıyoruz
        }
        private void Chart_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            // Tıklanan öğenin bir seri noktası olup olmadığını kontrol ediyoruz
            if (e.HitInfo.InSeries && e.HitInfo.SeriesPoint != null)
            {
                // Tıklanan segmentin argümanını (örneğin, firma adı veya stok kodu) alıyoruz
                string filterValue = e.HitInfo.SeriesPoint.Argument; // Firma adı veya stok kodu

                // Hangi grafikte tıklanıldığını belirliyoruz ve ona göre filtreyi uyguluyoruz
                if (sender == chartFirma)
                {
                    // Firma grafiği için filtre uygula
                    ApplyGridFilter(filterValue, "FirmaAdi");
                }
                else if (sender == chartStok)
                {
                    // Stok kodu grafiği için filtre uygula
                    ApplyGridFilter(filterValue, "StokKodu");
                }
            } 
        }
        private void ApplyGridFilter(string filterValue, string FilterColumn)
        {
            // Grid'deki filtreleyeceğimiz kolonu belirliyoruz. Örneğin, firma adı veya stok kodu.
            string columnToFilter = FilterColumn;  

            // GridControl'e filtre uyguluyoruz. Burada, tıklanan firma adını veya stok kodunu filtreliyoruz
            gvAlis.ActiveFilterString = $"[{columnToFilter}] = '{filterValue}'";
        }
        private void LoadData()
        {
            // Singleton kullanarak veriyi al
             alisFaturaTable = SQL.SqlHelper.Instance.GetDataTable("CF_alisfaturaView");

            if (alisFaturaTable != null)
            {
                // GridControl'e veri kaynağı ekleyin
                gcAlis.DataSource = alisFaturaTable;

                // Firma bazında top 10 tutarları bulmak için gruplama ve sıralama işlemi
                var top10Firms = alisFaturaTable.AsEnumerable()
                    .GroupBy(row => row.Field<string>("FirmaAdi"))
                    .Select(g => new
                    {
                        FirmaAdi = g.Key,
                        ToplamTutar = g.Sum(row => row.Field<decimal>("Tutar"))
                    })
                    .OrderByDescending(x => x.ToplamTutar)
                    .Take(20)
                    .ToList();

                // Top 10 firma verisini pasta grafiğe ekleme
                Series firmaSeries = new Series("Top 10 Firmalar", ViewType.Pie);
                firmaSeries.DataSource = top10Firms;
                firmaSeries.ArgumentDataMember = "FirmaAdi";
                firmaSeries.ValueDataMembers.AddRange(new string[] { "ToplamTutar" });

                // Etiketlerin görünür olmasını sağlıyoruz
                firmaSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                // Etiketlerin formatını ayarlıyoruz (örneğin: Firma Adı - Tutar)
                ((PieSeriesLabel)firmaSeries.Label).TextPattern = "{A}: {V:c}"; // A = Argument, V = Value (para birimi formatında gösterim)

                // Grafiğe seriyi ekliyoruz
                chartFirma.Series.Add(firmaSeries);

                // Firma grafiği başlığını ekliyoruz
                ChartTitle firmaTitle = new ChartTitle();
                firmaTitle.Text = "Top 20 Firmalar - Tutar";
                chartFirma.Titles.Add(firmaTitle);

                // Stok kodu bazında top 10 tutarları bulmak için gruplama ve sıralama işlemi
                var top10StockCodes = alisFaturaTable.AsEnumerable()
                    .GroupBy(row => row.Field<string>("StokKodu"))
                    .Select(g => new
                    {
                        StokKodu = g.Key,
                        ToplamTutar = g.Sum(row => row.Field<decimal>("Tutar"))
                    })
                    .OrderByDescending(x => x.ToplamTutar)
                    .Take(20)
                    .ToList();

                // Top 10 stok kodu verisini çubuk grafiğe ekleme
                Series stokSeries = new Series("Top 20 Stok Kodları", ViewType.Bar);
                stokSeries.DataSource = top10StockCodes;
                stokSeries.ArgumentDataMember = "StokKodu";
                stokSeries.ValueDataMembers.AddRange(new string[] { "ToplamTutar" });

                // Etiketlerin görünür olmasını sağlıyoruz
                stokSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                // Etiketlerin formatını ayarlıyoruz (örneğin: Stok Kodu - Tutar)
                ((SideBySideBarSeriesLabel)stokSeries.Label).TextPattern = "{A}: {V:c}"; // A = Argument, V = Value (para birimi formatında gösterim)

                // Stok grafiğine seriyi ekliyoruz
                chartStok.Series.Add(stokSeries);

                // Stok grafiği başlığını ekliyoruz
                ChartTitle stokTitle = new ChartTitle();
                stokTitle.Text = "Top 20 Stok Kodları - Tutar";
                chartStok.Titles.Add(stokTitle);

                // Chart'leri güncelle
                chartFirma.Refresh();
                chartStok.Refresh();
            }
        }

        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            SQL.DataHelper.ShowFilterMenu(null, null, gcAlis, alisFaturaTable);
        }
    }
}
