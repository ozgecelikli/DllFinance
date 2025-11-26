using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DllFinance.DLLFinance.FaturaYonetimi.Alis;
using DllFinance.DLLFinance.FaturaYonetimi.Satis;

namespace DllFinance.DLLFinance.Ozet
{
    /// <summary>
    /// Dashboard yönetimi için yardımcı sınıf
    /// Tüm user control'ları ve grafikleri dinamik olarak yönetir
    /// </summary>
    public class DashboardManager
    {
        private Dictionary<string, UserControl> userControls;
        private Dictionary<string, ChartControl> chartControls;
        private Dictionary<string, PivotGridControl> pivotControls;
        private Dictionary<string, GridControl> gridControls;
        private Dictionary<string, DataTable> dataTables;

        public DashboardManager()
        {
            InitializeCollections();
        }

        private void InitializeCollections()
        {
            userControls = new Dictionary<string, UserControl>();
            chartControls = new Dictionary<string, ChartControl>();
            pivotControls = new Dictionary<string, PivotGridControl>();
            gridControls = new Dictionary<string, GridControl>();
            dataTables = new Dictionary<string, DataTable>();
        }

        /// <summary>
        /// User control'ı dictionary'e ekler
        /// </summary>
        public void RegisterUserControl(string key, UserControl userControl)
        {
            if (!userControls.ContainsKey(key))
            {
                userControls[key] = userControl;
            }
        }

        /// <summary>
        /// Chart control'ı dictionary'e ekler
        /// </summary>
        public void RegisterChartControl(string key, ChartControl chartControl)
        {
            if (!chartControls.ContainsKey(key))
            {
                chartControls[key] = chartControl;
            }
        }

        /// <summary>
        /// PivotGrid control'ı dictionary'e ekler
        /// </summary>
        public void RegisterPivotControl(string key, PivotGridControl pivotControl)
        {
            if (!pivotControls.ContainsKey(key))
            {
                pivotControls[key] = pivotControl;
            }
        }

        /// <summary>
        /// Grid control'ı dictionary'e ekler
        /// </summary>
        public void RegisterGridControl(string key, GridControl gridControl)
        {
            if (!gridControls.ContainsKey(key))
            {
                gridControls[key] = gridControl;
            }
        }

        /// <summary>
        /// Veri tablosunu dictionary'e ekler
        /// </summary>
        public void RegisterDataTable(string key, DataTable dataTable)
        {
            if (!dataTables.ContainsKey(key))
            {
                dataTables[key] = dataTable;
            }
        }

        /// <summary>
        /// Belirtilen key'e sahip user control'ı döndürür
        /// </summary>
        public UserControl GetUserControl(string key)
        {
            return userControls.ContainsKey(key) ? userControls[key] : null;
        }

        /// <summary>
        /// Belirtilen key'e sahip chart control'ı döndürür
        /// </summary>
        public ChartControl GetChartControl(string key)
        {
            return chartControls.ContainsKey(key) ? chartControls[key] : null;
        }

        /// <summary>
        /// Belirtilen key'e sahip pivot control'ı döndürür
        /// </summary>
        public PivotGridControl GetPivotControl(string key)
        {
            return pivotControls.ContainsKey(key) ? pivotControls[key] : null;
        }

        /// <summary>
        /// Belirtilen key'e sahip grid control'ı döndürür
        /// </summary>
        public GridControl GetGridControl(string key)
        {
            return gridControls.ContainsKey(key) ? gridControls[key] : null;
        }

        /// <summary>
        /// Belirtilen key'e sahip data table'ı döndürür
        /// </summary>
        public DataTable GetDataTable(string key)
        {
            return dataTables.ContainsKey(key) ? dataTables[key] : null;
        }

        /// <summary>
        /// Tüm kontrolleri yeniler
        /// </summary>
        public void RefreshAllControls()
        {
            foreach (var chart in chartControls.Values)
            {
                chart.Refresh();
            }

            foreach (var pivot in pivotControls.Values)
            {
                pivot.RefreshData();
            }

            foreach (var grid in gridControls.Values)
            {
                grid.RefreshDataSource();
            }
        }

        /// <summary>
        /// Belirtilen key'e sahip kontrolleri yeniler
        /// </summary>
        public void RefreshControls(string key)
        {
            if (chartControls.ContainsKey(key))
            {
                chartControls[key].Refresh();
            }

            if (pivotControls.ContainsKey(key))
            {
                pivotControls[key].RefreshData();
            }

            if (gridControls.ContainsKey(key))
            {
                gridControls[key].RefreshDataSource();
            }
        }

        /// <summary>
        /// Grafik türünü değiştirir
        /// </summary>
        public void ChangeChartType(string key, ViewType viewType)
        {
            var chart = GetChartControl(key);
            if (chart != null && chart.Series.Count > 0)
            {
                chart.Series[0].ChangeView(viewType);
                chart.Refresh();
            }
        }

        /// <summary>
        /// Veri filtreleme işlemi
        /// </summary>
        public void ApplyDataFilter(string key, DateTime startDate, DateTime endDate)
        {
            var dataTable = GetDataTable(key);
            if (dataTable != null)
            {
                // Filtreleme işlemi burada yapılacak
                // Bu kısım veri kaynağına göre özelleştirilecek
            }
        }

        /// <summary>
        /// Dashboard için özel user control'ları oluşturur
        /// </summary>
        public void CreateSpecializedUserControls()
        {
            // Alış-Satış Analizi User Control
            var alisSatisControl = new OzetAlisSatis();
            RegisterUserControl("AlisSatis", alisSatisControl);

            // Kapasite Analizi User Control
            var kapasiteControl = new OzetKapasite();
            RegisterUserControl("Kapasite", kapasiteControl);

            // Nakit Akış Analizi User Control
            var nakitAkisControl = new OzetNakitAkis();
            RegisterUserControl("NakitAkis", nakitAkisControl);

            // Alış Cari Rapor User Control
            var alisCariControl = new AlisCariRapor();
            RegisterUserControl("AlisCari", alisCariControl);

            // Satış Cari Rapor User Control
            var satisCariControl = new SatisCariRapor();
            RegisterUserControl("SatisCari", satisCariControl);
        }

        /// <summary>
        /// Dashboard verilerini yükler
        /// </summary>
        public void LoadDashboardData()
        {
            try
            {
                // Alış verilerini yükle
                var alisData = SQL.SqlHelper.Instance.GetDataTable("CF_alisfaturaView");
                if (alisData != null)
                {
                    RegisterDataTable("AlisData", alisData);
                }

                // Satış verilerini yükle
                var satisData = SQL.SqlHelper.Instance.GetDataTable("CF_satisfaturaView");
                if (satisData != null)
                {
                    RegisterDataTable("SatisData", satisData);
                }

                // Diğer veri kaynakları buraya eklenebilir
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Dashboard istatistiklerini hesaplar
        /// </summary>
        public Dictionary<string, object> CalculateDashboardStatistics()
        {
            var statistics = new Dictionary<string, object>();

            try
            {
                var alisData = GetDataTable("AlisData");
                var satisData = GetDataTable("SatisData");

                if (alisData != null)
                {
                    // Alış istatistikleri
                    var alisToplam = alisData.AsEnumerable().Sum(r => r.Field<decimal>("Tutar"));
                    var alisOrtalama = alisData.AsEnumerable().Average(r => r.Field<decimal>("Tutar"));
                    var alisKayitSayisi = alisData.Rows.Count;

                    statistics["AlisToplam"] = alisToplam;
                    statistics["AlisOrtalama"] = alisOrtalama;
                    statistics["AlisKayitSayisi"] = alisKayitSayisi;
                }

                if (satisData != null)
                {
                    // Satış istatistikleri
                    var satisToplam = satisData.AsEnumerable().Sum(r => r.Field<decimal>("Tutar"));
                    var satisOrtalama = satisData.AsEnumerable().Average(r => r.Field<decimal>("Tutar"));
                    var satisKayitSayisi = satisData.Rows.Count;

                    statistics["SatisToplam"] = satisToplam;
                    statistics["SatisOrtalama"] = satisOrtalama;
                    statistics["SatisKayitSayisi"] = satisKayitSayisi;
                }

                // Kar/zarar hesaplama
                if (statistics.ContainsKey("SatisToplam") && statistics.ContainsKey("AlisToplam"))
                {
                    var karZarar = (decimal)statistics["SatisToplam"] - (decimal)statistics["AlisToplam"];
                    statistics["KarZarar"] = karZarar;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İstatistik hesaplanırken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return statistics;
        }

        /// <summary>
        /// Dashboard'u temizler
        /// </summary>
        public void ClearDashboard()
        {
            userControls.Clear();
            chartControls.Clear();
            pivotControls.Clear();
            gridControls.Clear();
            dataTables.Clear();
        }

        /// <summary>
        /// Dashboard durumunu kontrol eder
        /// </summary>
        public bool IsDashboardReady()
        {
            return userControls.Count > 0 && chartControls.Count > 0;
        }

        /// <summary>
        /// Dashboard özetini döndürür
        /// </summary>
        public string GetDashboardSummary()
        {
            var summary = new StringBuilder();
            summary.AppendLine("📊 Dashboard Özeti:");
            summary.AppendLine($"• User Control Sayısı: {userControls.Count}");
            summary.AppendLine($"• Chart Control Sayısı: {chartControls.Count}");
            summary.AppendLine($"• Pivot Control Sayısı: {pivotControls.Count}");
            summary.AppendLine($"• Grid Control Sayısı: {gridControls.Count}");
            summary.AppendLine($"• Veri Tablosu Sayısı: {dataTables.Count}");
            
            return summary.ToString();
        }
    }
}
















