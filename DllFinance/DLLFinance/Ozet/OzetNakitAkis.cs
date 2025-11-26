using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraPivotGrid;
using DllFinance.SQL;

namespace DllFinance.DLLFinance.Ozet
{
    public partial class OzetNakitAkis : UserControl
    {
        // Timer
        private Timer timer1;

        // ERPIS değişkenleri
        private string xİtarih, xStarih, xmaxvade, xvadesigecmis, xvadesigelen;
        private int xOzetBimno, xOrtalamaVade, xBuldu;
        private double xBorc, xAlacak, xdBorc, xdAlacak, xBorc1, xdBorc1, xTborc, xdTborc, xFark, xdFark;
        private string xRaporNo, xİslemTipi, xGecici;
        private int yil1, ay1, gun1, yil2, ay2, gun2;
        private DateTime ilktarih, sontarih;
        private TimeSpan fark;

        // Veri tabloları
        private DataTable originalNakitAkisDataTable;
        private DataTable borclarDataTable;
        private DataTable alacaklarDataTable;
        private bool isDateRangeInitialized;

        public OzetNakitAkis()
        {
            InitializeComponent();
            InitializeChart();
            InitializeDateFilters();
            InitializeTimer();
            LoadNakitAkisData();
            ConfigurePivotGrids();

            // Form kapanırken temizlik yap
            this.HandleDestroyed += (s, e) => CleanupResources();
        }

        #region Timer ve Başlatma

        private void InitializeDateFilters()
        {
            try
            {
                dateEditBaslangic.EditValue = null;
                dateEditBitis.EditValue = null;
                isDateRangeInitialized = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarih filtreleri başlatma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTimer()
        {
            try
            {
                // Timer'ı başlat - SQL bağlantısı artık gerekmiyor
                timer1 = new Timer();
                timer1.Interval = 30000; // 30 saniye
                timer1.Tick += Timer1_Tick;
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Timer başlatma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Otomatik veri yenileme
                LoadNakitAkisData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Timer hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Pivot Grid Yapılandırma

        private void ConfigurePivotGrids()
        {
            try
            {
                // Borçlar Pivot Grid yapılandırması
                ConfigureBorclarPivotGrid();

                // Alacaklar Pivot Grid yapılandırması
                ConfigureAlacaklarPivotGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Pivot grid yapılandırma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureBorclarPivotGrid()
        {
            try
            {
                // Pivot grid'i temizle
                pivotGridControlBorclar.Fields.Clear();

                // Satır alanı - Firma Adı
                PivotGridField fieldFirma = new PivotGridField("FirmaAdi", PivotArea.RowArea);
                fieldFirma.Caption = "Firma Adı";
                pivotGridControlBorclar.Fields.Add(fieldFirma);

                // Sütun alanı - Yıl-Hafta
                PivotGridField fieldYilHafta = new PivotGridField("YilHafta", PivotArea.ColumnArea);
                fieldYilHafta.Caption = "Yıl-Hafta";
                pivotGridControlBorclar.Fields.Add(fieldYilHafta);

                // Veri alanı - Tutar
                PivotGridField fieldTutar = new PivotGridField("Tutar", PivotArea.DataArea);
                fieldTutar.Caption = "Tutar (TL)";
                fieldTutar.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldTutar.CellFormat.FormatString = "N2";
                pivotGridControlBorclar.Fields.Add(fieldTutar);

                // Pivot grid ayarları
                pivotGridControlBorclar.OptionsView.ShowColumnGrandTotals = true;
                pivotGridControlBorclar.OptionsView.ShowRowGrandTotals = true;
                pivotGridControlBorclar.OptionsView.ShowColumnTotals = true;
                pivotGridControlBorclar.OptionsView.ShowRowTotals = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Borçlar pivot grid yapılandırma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureAlacaklarPivotGrid()
        {
            try
            {
                // Pivot grid'i temizle
                pivotGridControlAlacaklar.Fields.Clear();

                // Satır alanı - Firma Adı
                PivotGridField fieldFirma = new PivotGridField("FirmaAdi", PivotArea.RowArea);
                fieldFirma.Caption = "Firma Adı";
                pivotGridControlAlacaklar.Fields.Add(fieldFirma);

                // Sütun alanı - Yıl-Hafta
                PivotGridField fieldYilHafta = new PivotGridField("YilHafta", PivotArea.ColumnArea);
                fieldYilHafta.Caption = "Yıl-Hafta";
                pivotGridControlAlacaklar.Fields.Add(fieldYilHafta);

                // Veri alanı - Tutar
                PivotGridField fieldTutar = new PivotGridField("Tutar", PivotArea.DataArea);
                fieldTutar.Caption = "Tutar (TL)";
                fieldTutar.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldTutar.CellFormat.FormatString = "N2";
                pivotGridControlAlacaklar.Fields.Add(fieldTutar);

                // Pivot grid ayarları
                pivotGridControlAlacaklar.OptionsView.ShowColumnGrandTotals = true;
                pivotGridControlAlacaklar.OptionsView.ShowRowGrandTotals = true;
                pivotGridControlAlacaklar.OptionsView.ShowColumnTotals = true;
                pivotGridControlAlacaklar.OptionsView.ShowRowTotals = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alacaklar pivot grid yapılandırma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Veri Yükleme

        private void LoadNakitAkisData()
        {
            try
            {
                originalNakitAkisDataTable = SqlHelper.Instance.GetDataTable("CF_vadesigelenView");
                EnsureDateRange(originalNakitAkisDataTable);

                // Vadesi gelen borçları yükle
                LoadVadesiGelenBorclar();

                // Vadesi gelen alacakları yükle
                LoadVadesiGelenAlacaklar();

                // Chart verilerini güncelle
                UpdateChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVadesiGelenBorclar()
        {
            try
            {
                if (originalNakitAkisDataTable != null && originalNakitAkisDataTable.Rows.Count > 0)
                {
                    // Tarih filtresi uygula
                    DateTime baslangicTarihi = dateEditBaslangic.DateTime;
                    DateTime bitisTarihi = dateEditBitis.DateTime;

                    // Borçları filtrele (tipi = "BORÇ") ve tarih aralığına göre
                    var borclarQuery = originalNakitAkisDataTable.AsEnumerable()
                        .Where(row =>
                        {
                            string tipi = row["tipi"]?.ToString() ?? "";
                            DateTime vadeTarihi = Convert.ToDateTime(row["vade_d"]);

                            return tipi.Equals("BORÇ", StringComparison.OrdinalIgnoreCase) &&
                                   vadeTarihi >= baslangicTarihi &&
                                   vadeTarihi <= bitisTarihi;
                        });

                    DataTable borclarData = borclarQuery.Any()
                        ? borclarQuery.CopyToDataTable()
                        : originalNakitAkisDataTable.Clone();

                    borclarDataTable = ProcessDataForPivot(borclarData, "Borç");
                    pivotGridControlBorclar.DataSource = borclarDataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vadesi gelen borçlar yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVadesiGelenAlacaklar()
        {
            try
            {
                if (originalNakitAkisDataTable != null && originalNakitAkisDataTable.Rows.Count > 0)
                {
                    // Tarih filtresi uygula
                    DateTime baslangicTarihi = dateEditBaslangic.DateTime;
                    DateTime bitisTarihi = dateEditBitis.DateTime;

                    // Alacakları filtrele (tipi = "ALACAK") ve tarih aralığına göre
                    var alacaklarQuery = originalNakitAkisDataTable.AsEnumerable()
                        .Where(row =>
                        {
                            string tipi = row["tipi"]?.ToString() ?? "";
                            DateTime vadeTarihi = Convert.ToDateTime(row["vade_d"]);

                            return tipi.Equals("ALACAK", StringComparison.OrdinalIgnoreCase) &&
                                   vadeTarihi >= baslangicTarihi &&
                                   vadeTarihi <= bitisTarihi;
                        });

                    DataTable alacaklarData = alacaklarQuery.Any()
                        ? alacaklarQuery.CopyToDataTable()
                        : originalNakitAkisDataTable.Clone();

                    alacaklarDataTable = ProcessDataForPivot(alacaklarData, "Alacak");
                    pivotGridControlAlacaklar.DataSource = alacaklarDataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vadesi gelen alacaklar yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EnsureDateRange(DataTable sourceData)
        {
            try
            {
                if (sourceData == null || sourceData.Rows.Count == 0)
                {
                    return;
                }

                var availableDates = sourceData.AsEnumerable()
                    .Where(row => row["vade_d"] != DBNull.Value)
                    .Select(row => Convert.ToDateTime(row["vade_d"]))
                    .OrderBy(date => date)
                    .ToList();

                if (!availableDates.Any())
                {
                    return;
                }

                DateTime minDate = availableDates.First();
                DateTime maxDate = availableDates.Last();

                bool dateRangeWasInvalid =
                    dateEditBaslangic.EditValue == null ||
                    dateEditBitis.EditValue == null ||
                    dateEditBaslangic.DateTime <= DateTime.MinValue.AddDays(1) ||
                    dateEditBitis.DateTime <= DateTime.MinValue.AddDays(1) ||
                    dateEditBitis.DateTime < dateEditBaslangic.DateTime;

                if (!isDateRangeInitialized || dateRangeWasInvalid)
                {
                    dateEditBaslangic.DateTime = minDate;
                    dateEditBitis.DateTime = maxDate;
                    isDateRangeInitialized = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarih aralığı belirleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private DataTable ProcessDataForPivot(DataTable sourceTable, string tip)
        {
            try
            {
                DataTable pivotTable = new DataTable();
                pivotTable.Columns.Add("FirmaAdi", typeof(string));
                pivotTable.Columns.Add("YilHafta", typeof(string));
                pivotTable.Columns.Add("Tutar", typeof(decimal));
                pivotTable.Columns.Add("Tip", typeof(string));

                foreach (DataRow row in sourceTable.Rows)
                {
                    try
                    {
                        // CF_vadesigelenView kolonlarına göre veri al
                        string firmaAdi = row["firmaadi"]?.ToString() ?? "Bilinmeyen";
                        DateTime vadeTarihi = Convert.ToDateTime(row["vade_d"]);
                        decimal tutar = Convert.ToDecimal(row["tutar"]);

                        // Yıl-Hafta formatı oluştur (YYYY-WW)
                        int yil = vadeTarihi.Year;
                        int hafta = GetWeekOfYear(vadeTarihi);
                        string yilHafta = $"{yil}-{hafta:D2}";

                        pivotTable.Rows.Add(firmaAdi, yilHafta, tutar, tip);
                    }
                    catch
                    {
                        // Hatalı satırları atla
                        continue;
                    }
                }

                return pivotTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri işleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        private int GetWeekOfYear(DateTime date)
        {
            System.Globalization.CultureInfo ciCurr = System.Globalization.CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekNum;
        }

        private void UpdateChartData()
        {
            try
            {
                // Chart'ı temizle
                if (chartControl1.Series.Count >= 2)
                {
                    chartControl1.Series["GELİR"].Points.Clear();
                    chartControl1.Series["GİDER"].Points.Clear();
                }

                // Borç ve alacak verilerini chart'a ekle
                if (borclarDataTable != null && borclarDataTable.Rows.Count > 0)
                {
                    AddDataToChart(borclarDataTable, "GİDER");
                }

                if (alacaklarDataTable != null && alacaklarDataTable.Rows.Count > 0)
                {
                    AddDataToChart(alacaklarDataTable, "GELİR");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDataToChart(DataTable dataTable, string seriesName)
        {
            try
            {
                var groupedData = dataTable.AsEnumerable()
                    .GroupBy(row => row.Field<string>("YilHafta"))
                    .Select(g => new
                    {
                        YilHafta = g.Key,
                        ToplamTutar = g.Sum(row => row.Field<decimal>("Tutar"))
                    })
                    .OrderBy(g => g.YilHafta)
                    .ToList();

                foreach (var data in groupedData)
                {
                    // Yıl-Hafta formatını DateTime'a çevir
                    string[] parts = data.YilHafta.Split('-');
                    if (parts.Length == 2)
                    {
                        int yil = int.Parse(parts[0]);
                        int hafta = int.Parse(parts[1]);

                        // Hafta numarasından tarih hesapla
                        DateTime tarih = GetDateFromWeek(yil, hafta);

                        chartControl1.Series[seriesName].Points.Add(new DevExpress.XtraCharts.SeriesPoint(tarih, data.ToplamTutar));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart veri ekleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime GetDateFromWeek(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            int firstWeek = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(firstMonday, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                week -= 1;
            }

            return firstMonday.AddDays(week * 7);
        }

        #endregion

        #region Chart Yapılandırma

        private void InitializeChart()
        {
            try
            {
                // Chart'ı temizle
                chartControl1.Series.Clear();
                chartControl1.Titles.Clear();

                // Gelir serisi (yeşil çizgi)
                DevExpress.XtraCharts.Series seriesGelir = new DevExpress.XtraCharts.Series("GELİR", DevExpress.XtraCharts.ViewType.Line);
                seriesGelir.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
                DevExpress.XtraCharts.LineSeriesView lineViewGelir = (DevExpress.XtraCharts.LineSeriesView)seriesGelir.View;
                lineViewGelir.Color = Color.Green;
                lineViewGelir.LineStyle.Thickness = 3;
                lineViewGelir.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                seriesGelir.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                chartControl1.Series.Add(seriesGelir);

                // Gider serisi (kırmızı çizgi)
                DevExpress.XtraCharts.Series seriesGider = new DevExpress.XtraCharts.Series("GİDER", DevExpress.XtraCharts.ViewType.Line);
                seriesGider.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
                DevExpress.XtraCharts.LineSeriesView lineViewGider = (DevExpress.XtraCharts.LineSeriesView)seriesGider.View;
                lineViewGider.Color = Color.Red;
                lineViewGider.LineStyle.Thickness = 3;
                lineViewGider.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                seriesGider.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                chartControl1.Series.Add(seriesGider);

                // Legend ayarları
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
                chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;

                // Başlık ekle
                DevExpress.XtraCharts.ChartTitle title = new DevExpress.XtraCharts.ChartTitle();
                title.Text = "NAKİT AKIŞ GRAFİĞİ";
                title.Font = new Font("Tahoma", 12, FontStyle.Bold);
                chartControl1.Titles.Add(title);

                // Diagram ayarlarını yapılandır
                ConfigureChartDiagram();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart başlatma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureChartDiagram()
        {
            try
            {
                if (chartControl1.Diagram is DevExpress.XtraCharts.XYDiagram diagram)
                {
                    // X eksenini tarih olarak ayarla
                    diagram.AxisX.DateTimeScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
                    diagram.AxisX.Label.TextPattern = "{A:dd.MM.yyyy}";
                    diagram.AxisX.Title.Text = "TARİH";
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    // Y ekseni ayarları
                    diagram.AxisY.Title.Text = "TUTAR (TL)";
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisY.Label.TextPattern = "{V:N2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart diagram ayarlama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers

        private void btnYenile_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNakitAkisData();
                MessageBox.Show("Veriler başarıyla yenilendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yenileme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetay_Click(object sender, EventArgs e)
        {
            try
            {
                // Borç ve alacak verilerini birleştir
                DataTable combinedData = new DataTable();
                combinedData.Columns.Add("FirmaAdi", typeof(string));
                combinedData.Columns.Add("YilHafta", typeof(string));
                combinedData.Columns.Add("Tutar", typeof(decimal));
                combinedData.Columns.Add("Tip", typeof(string));

                if (borclarDataTable != null && borclarDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in borclarDataTable.Rows)
                    {
                        combinedData.Rows.Add(row.ItemArray);
                    }
                }

                if (alacaklarDataTable != null && alacaklarDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in alacaklarDataTable.Rows)
                    {
                        combinedData.Rows.Add(row.ItemArray);
                    }
                }

                if (combinedData.Rows.Count > 0)
                {
                    VeriDetay detayForm = new VeriDetay(combinedData);
                    detayForm.Show();
                }
                else
                {
                    MessageBox.Show("Gösterilecek veri yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Detay formu açma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltre_Click(object sender, EventArgs e)
        {
            try
            {
                // Tarih filtresi uygula
                LoadNakitAkisData();
                MessageBox.Show("Filtre başarıyla uygulandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtre uygulama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Cleanup

        private void CleanupResources()
        {
            try
            {
                if (timer1 != null)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    timer1 = null;
                }

                // SQL bağlantısı artık gerekmiyor - SqlHelper kullanıyoruz
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Cleanup hatası: {ex.Message}");
            }
        }

        #endregion
    }
}