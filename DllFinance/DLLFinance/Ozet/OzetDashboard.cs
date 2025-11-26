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
//using DevExpress.XtraGrid.Data;
using DllFinance.SQL;

namespace DllFinance.DLLFinance.Ozet
{
    public partial class OzetDashboard : UserControl
    {
        
        // Örnek veri
        private DataTable sampleData;
        
        // SQL bağlantı ve komut nesneleri
        private SqlConnection sqlConnection;
        private SqlCommand cmd0, cmd1, cmd2;
        private Timer timer1;
        
        // ERPIS değişkenleri
        private string xİtarih, xStarih, xmaxvade, xvadesigecmis, xvadesigelen;
        private int xOzetBimno, xOrtalamaVade, xBuldu;
        private double xBorc, xAlacak, xdBorc, xdAlacak, xBorc1, xdBorc1, xTborc, xdTborc, xFark, xdFark;
        private string xRaporNo, xİslemTipi, xGecici;
        private int yil1, ay1, gun1, yil2, ay2, gun2;
        private DateTime ilktarih, sontarih;
        private TimeSpan fark;

        public OzetDashboard()
        {
            InitializeComponent();
            LoadCombinedData();
            InitializeChart();
        }


        #region ERPIS VGAB Fonksiyonları

        void TimerNakitAkis()
        {
            try
            {
                timer1.Enabled = false; 
                timer1.Stop();

                // Tek prosedür ile tüm verileri yeniden yükle
              //  LoadDataFromViews();

                timer1.Enabled = true; 
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"TimerNakitAkis hatası: {ex.Message}");
            }
        }

        void VgaAlacaklar()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                // Tarih formatlarını hazırla
                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");
                xmaxvade = currentDate.AddMonths(12).ToString("yyyyMMdd");
                xvadesigecmis = currentDate.AddDays(-30).ToString("yyyyMMdd");
                xvadesigelen = currentDate.AddDays(30).ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_vga_alacaklar_firmamizani";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@ifirmakodu", "");
                cmd0.Parameters.AddWithValue("@sfirmakodu", "");
                cmd0.Parameters.AddWithValue("@ievraktarihi", xİtarih);
                cmd0.Parameters.AddWithValue("@sevraktarihi", xStarih);
                cmd0.Parameters.AddWithValue("@maxvade", xmaxvade);
                cmd0.Parameters.AddWithValue("@vadesigecmis", xvadesigecmis);
                cmd0.Parameters.AddWithValue("@bbakiye", 0.0);
                cmd0.Parameters.AddWithValue("@islemtipi", xİslemTipi);
                cmd0.Parameters.AddWithValue("@haricmahsup", "");
                cmd0.Parameters.AddWithValue("@vadesigelen", xvadesigelen);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds, "mahsupbilgileridetay");
                
                // Grid'e veri yükle
                if (ds.Tables["mahsupbilgileridetay"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables["mahsupbilgileridetay"];
                    //UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"VgaAlacaklar hatası: {ex.Message}");
            }
        }

        void Vgaalacaklardetay()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_erpis_vadesigelenbadetay_getir_bimno_detay";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@ozet_bimno", xOzetBimno);

                DataSet ds1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds1, "mahsupbilgileridetay");
                
                if (ds1.Tables["mahsupbilgileridetay"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds1.Tables["mahsupbilgileridetay"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vgaalacaklardetay hatası: {ex.Message}");
            }
        }

        void Vgaborclar()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                // Tarih formatlarını hazırla
                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");
                xmaxvade = currentDate.AddMonths(12).ToString("yyyyMMdd");
                xvadesigecmis = currentDate.AddDays(-30).ToString("yyyyMMdd");
                xvadesigelen = currentDate.AddDays(30).ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_vga_borclar_firmamizani";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@ifirmakodu", "");
                cmd0.Parameters.AddWithValue("@sfirmakodu", "");
                cmd0.Parameters.AddWithValue("@ievraktarihi", xİtarih);
                cmd0.Parameters.AddWithValue("@sevraktarihi", xStarih);
                cmd0.Parameters.AddWithValue("@maxvade", xmaxvade);
                cmd0.Parameters.AddWithValue("@vadesigecmis", xvadesigecmis);
                cmd0.Parameters.AddWithValue("@bbakiye", 0.0);
                cmd0.Parameters.AddWithValue("@islemtipi", xİslemTipi);
                cmd0.Parameters.AddWithValue("@haricmahsup", "");
                cmd0.Parameters.AddWithValue("@vadesigelen", xvadesigelen);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vgaborclar hatası: {ex.Message}");
            }
        }

        void Vgaborclardetay()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_erpis_vadesigelenbadetay_getir_bimno_detay";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@ozet_bimno", xOzetBimno);

                DataSet ds1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds1, "mahsupbilgileridetay");
                
                if (ds1.Tables["mahsupbilgileridetay"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds1.Tables["mahsupbilgileridetay"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vgaborclardetay hatası: {ex.Message}");
            }
        }

        void CekRaporu()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_cekraporu";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);
                cmd0.Parameters.AddWithValue("@raporno", xRaporNo);

                DataSet ds1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds1, "ceksenetbilgileri");
                
                if (ds1.Tables["ceksenetbilgileri"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds1.Tables["ceksenetbilgileri"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CekRaporu hatası: {ex.Message}");
            }
        }

        void KasaRaporu()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.Year + "0101";
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_kasaraporu";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);
                cmd0.Parameters.AddWithValue("@raporno", "");

                DataSet ds1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds1, "genelraporlama");
                
                if (ds1.Tables["genelraporlama"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds1.Tables["genelraporlama"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"KasaRaporu hatası: {ex.Message}");
            }
        }

        void BankaRaporu()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.Year + "0101";
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_bankaraporu";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);
                cmd0.Parameters.AddWithValue("@raporno", "");

                DataSet ds1 = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds1, "genelraporlama");
                
                if (ds1.Tables["genelraporlama"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds1.Tables["genelraporlama"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"BankaRaporu hatası: {ex.Message}");
            }
        }

        void RaporPeriyodikGiderler()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.Year + "0101";
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_nakitakis_periyodikgiderleri";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@subekodu", "");
                cmd0.Parameters.AddWithValue("@bolumkodu", "");
                cmd0.Parameters.AddWithValue("@tipi", xRaporNo);
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds, "genelraporlama");
                
                if (ds.Tables["genelraporlama"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables["genelraporlama"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RaporPeriyodikGiderler hatası: {ex.Message}");
            }
        }

        void RaporBankaKrediBilgileri()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_bankakredileriozet";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RaporBankaKrediBilgileri hatası: {ex.Message}");
            }
        }

        void RaporPoliceBilgileri()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_policebilgileriozet";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RaporPoliceBilgileri hatası: {ex.Message}");
            }
        }

        void RaporKredikartiTaksitBilgileri()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed) 
                { 
                    sqlConnection.Open(); 
                }

                DateTime currentDate = DateTime.Now;
                xİtarih = currentDate.ToString("yyyyMMdd");
                xStarih = currentDate.ToString("yyyyMMdd");

                cmd0.Connection = sqlConnection;
                cmd0.CommandText = "hg_nakitakis_kredikartitaksitbilgileridetay";
                cmd0.CommandType = CommandType.StoredProcedure;
                cmd0.CommandTimeout = sqlConnection.ConnectionTimeout;
                cmd0.Parameters.Clear();
                cmd0.Parameters.AddWithValue("@dil", "TR");
                cmd0.Parameters.AddWithValue("@sirketkodu", "001");
                cmd0.Parameters.AddWithValue("@batarih", xİtarih);
                cmd0.Parameters.AddWithValue("@bitarih", xStarih);

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd0);
                adapter.Fill(ds, "genelraporlama");
                
                if (ds.Tables["genelraporlama"].Rows.Count > 0)
                {
                    gridControl1.DataSource = ds.Tables["genelraporlama"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RaporKredikartiTaksitBilgileri hatası: {ex.Message}");
            }
        }

        void NakitAkisTablosuSuan()
        {
            // Şu anki nakit akış tablosu - örnek implementasyon
            try
            {
                // Bu fonksiyon gerçek stored procedure ile implement edilecek
                MessageBox.Show("NakitAkisTablosuSuan fonksiyonu çağrıldı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"NakitAkisTablosuSuan hatası: {ex.Message}");
            }
        }

        void NakitAkisTablosuYillik()
        {
            // Yıllık nakit akış tablosu - örnek implementasyon
            try
            {
                // Bu fonksiyon gerçek stored procedure ile implement edilecek
                MessageBox.Show("NakitAkisTablosuYillik fonksiyonu çağrıldı");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"NakitAkisTablosuYillik hatası: {ex.Message}");
            }
        }

        #endregion

        /// <summary>
        /// Program.cs'de yüklenen tüm view verilerini birleştirir ve grid'e yükler
        /// </summary>
        private void LoadCombinedData()
        {
            try
            {
                // SqlHelper'dan birleştirilmiş veriyi al (Program.cs'de zaten yüklenmiş)
                DataTable combinedData = SqlHelper.Instance.GetCombinedDataTable();
                
                if (combinedData != null && combinedData.Rows.Count > 0)
                {
                    // Grid'e veriyi yükle
                    gridControl1.DataSource = combinedData;
                    
                    // Grid view ayarlarını güncelle
                    ConfigureGridColumns();
                    
                    // Özet bilgileri güncelle
                    UpdateCombinedDataSummary(combinedData);
                    
                    // Chart'ı doğrudan DataTable'dan güncelle
                    UpdateChartFromDataTable(combinedData);
                }
                else
                {
                    MessageBox.Show("Yüklenecek veri bulunamadı. Lütfen Program.cs'de view'ların yüklendiğinden emin olun.", 
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Birleştirilmiş veriler yüklenirken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grid kolonlarını yapılandırır
        /// </summary>
        private void ConfigureGridColumns()
        {
            try
            {
                // ViewSource kolonunu en başa taşı
                if (gridView1.Columns["ViewSource"] != null)
                {
                    gridView1.Columns["ViewSource"].VisibleIndex = 0;
                    gridView1.Columns["ViewSource"].Caption = "KAYNAK";
                    gridView1.Columns["ViewSource"].Width = 150;
                }

                // Diğer kolonları otomatik boyutlandır
                gridView1.BestFitColumns();
                
                // Grup panelini göster
                gridView1.OptionsView.ShowGroupPanel = true;
                
                // Footer'ı göster
                gridView1.OptionsView.ShowFooter = true;
                
                // ViewSource'a göre gruplama yap
                if (gridView1.Columns["ViewSource"] != null)
                {
                    gridView1.Columns["ViewSource"].GroupIndex = 0;
                }

                // Grid event'lerini bağla - Chart'ın otomatik güncellenmesi için
                gridView1.ColumnFilterChanged -= GridView1_ColumnFilterChanged;
                gridView1.ColumnFilterChanged += GridView1_ColumnFilterChanged;
                
                gridView1.RowCountChanged -= GridView1_RowCountChanged;
                gridView1.RowCountChanged += GridView1_RowCountChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid kolonları yapılandırılırken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grid filtresi değiştiğinde chart'ı güncelle
        /// </summary>
        private void GridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateChartFromGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtre değişikliğinde hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grid satır sayısı değiştiğinde chart'ı güncelle
        /// </summary>
        private void GridView1_RowCountChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateChartFromGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Satır sayısı değişikliğinde hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Birleştirilmiş veri için özet bilgileri günceller
        /// </summary>
        private void UpdateCombinedDataSummary(DataTable data)
        {
            try
            {
                if (data == null || data.Rows.Count == 0)
                    return;

                int totalRows = data.Rows.Count;
                
                // View kaynaklarına göre sayıları hesapla
                var viewCounts = data.AsEnumerable()
                    .GroupBy(r => r.Field<string>("ViewSource"))
                    .Select(g => new { ViewName = g.Key, Count = g.Count() })
                    .ToList();

                // Chart'ı güncelle
              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Özet güncellenirken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chart kontrolünü başlangıçta yapılandırır
        /// </summary>
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
                title.Text = "GELİR / GİDER GRAFİĞİ";
                title.Font = new Font("Tahoma", 12, FontStyle.Bold);
                chartControl1.Titles.Add(title);
                
                // Diagram ayarlarını sadece ilk veri yüklendiğinde yap
                ConfigureChartDiagram();
                
                // Zoom event'lerini bağla
                AttachChartZoomEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart başlatma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        /// <summary>
        /// Chart diagram ayarlarını yapılandırır
        /// </summary>
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
                    
                    // Zoom özelliklerini etkinleştir
                    EnableChartZoom(diagram);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart diagram ayarlama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chart zoom özelliklerini etkinleştirir
        /// </summary>
        private void EnableChartZoom(DevExpress.XtraCharts.XYDiagram diagram)
        {
            try
            {
                // Mouse ile crosshair etkinleştir
                chartControl1.CrosshairOptions.ShowArgumentLabels = true;
                chartControl1.CrosshairOptions.ShowValueLabels = true;
                
                // X ekseni zoom ayarları - basit yaklaşım
                diagram.AxisX.VisualRange.Auto = false;
                diagram.AxisX.WholeRange.Auto = true;
                
                // Y ekseni zoom ayarları
                diagram.AxisY.VisualRange.Auto = false;
                diagram.AxisY.WholeRange.Auto = true;
                
                // Chart'ın mouse wheel event'ini yakala
                chartControl1.MouseWheel -= ChartControl1_MouseWheel;
                chartControl1.MouseWheel += ChartControl1_MouseWheel;
                
                // Chart'a çift tıklama ile reset zoom
                chartControl1.DoubleClick -= ChartControl1_DoubleClick;
                chartControl1.DoubleClick += ChartControl1_DoubleClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart zoom etkinleştirme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chart çift tıklama event'i - zoom reset
        /// </summary>
        private void ChartControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (chartControl1.Diagram is DevExpress.XtraCharts.XYDiagram diagram)
                {
                    // Zoom'u sıfırla
                    diagram.AxisX.VisualRange.Auto = true;
                    diagram.AxisY.VisualRange.Auto = true;
                    
                    // Chart'ı yenile
                    chartControl1.RefreshData();
                    
                    // Format güncellemesi yap
                    System.Threading.Tasks.Task.Delay(100).ContinueWith(_ => 
                    {
                        if (chartControl1.InvokeRequired)
                        {
                            chartControl1.Invoke(new Action(UpdateChartFormatBasedOnZoom));
                        }
                        else
                        {
                            UpdateChartFormatBasedOnZoom();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Double click event hatası: {ex.Message}");
            }
        }

        // Zoom için gerekli değişkenler
        private bool isZooming = false;
        private Point zoomStartPoint;
        private Rectangle zoomRectangle;

        /// <summary>
        /// Chart mouse down event'i - zoom başlangıcı
        /// </summary>
        private void ChartControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    isZooming = true;
                    zoomStartPoint = e.Location;
                    zoomRectangle = Rectangle.Empty;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse down event hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Chart mouse move event'i - zoom rectangle çizimi
        /// </summary>
        private void ChartControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isZooming && e.Button == MouseButtons.Left)
                {
                    // Zoom rectangle'ı güncelle
                    int x = Math.Min(zoomStartPoint.X, e.X);
                    int y = Math.Min(zoomStartPoint.Y, e.Y);
                    int width = Math.Abs(e.X - zoomStartPoint.X);
                    int height = Math.Abs(e.Y - zoomStartPoint.Y);
                    
                    zoomRectangle = new Rectangle(x, y, width, height);
                    
                    // Chart'ı yeniden çiz
                    chartControl1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse move event hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Chart zoom event'lerini bağlar
        /// </summary>
        private void AttachChartZoomEvents()
        {
            try
            {
                if (chartControl1.Diagram is DevExpress.XtraCharts.XYDiagram diagram)
                {
                    // Chart'ın paint event'ini yakala (zoom sonrası güncelleme için)
                    chartControl1.Paint -= ChartControl1_Paint;
                    chartControl1.Paint += ChartControl1_Paint;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart zoom event bağlama hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chart mouse up event'i - zoom tamamlama ve format güncelleme
        /// </summary>
        private void ChartControl1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (isZooming && e.Button == MouseButtons.Left)
                {
                    // Zoom işlemini tamamla
                    isZooming = false;
                    
                    if (zoomRectangle.Width > 10 && zoomRectangle.Height > 10)
                    {
                        // Zoom rectangle'ı temizle
                        zoomRectangle = Rectangle.Empty;
                        chartControl1.Invalidate();
                        
                        // Format güncellemesi yap
                        System.Threading.Tasks.Task.Delay(100).ContinueWith(_ => 
                        {
                            if (chartControl1.InvokeRequired)
                            {
                                chartControl1.Invoke(new Action(UpdateChartFormatBasedOnZoom));
                            }
                            else
                            {
                                UpdateChartFormatBasedOnZoom();
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse up event hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Chart mouse wheel event'i - zoom in/out
        /// </summary>
        private void ChartControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (chartControl1.Diagram is DevExpress.XtraCharts.XYDiagram diagram)
                {
                    // Mouse wheel delta değerini al
                    int delta = e.Delta;
                    
                    // Basit zoom yaklaşımı - VisualRange'i Auto'ya çevir
                    if (delta > 0) // Zoom In - daha dar aralık
                    {
                        // Mevcut range'i daralt
                        var xRange = diagram.AxisX.VisualRange;
                        var yRange = diagram.AxisY.VisualRange;
                        
                        if (xRange != null && yRange != null)
                        {
                            // Güvenli tip dönüşümü
                            double xCenter, xSpan;
                            
                            try
                            {
                                if (xRange.MinValue is DateTime minDateTime && xRange.MaxValue is DateTime maxDateTime)
                                {
                                    // DateTime değerlerini OADate'e çevir
                                    double minOADate = minDateTime.ToOADate();
                                    double maxOADate = maxDateTime.ToOADate();
                                    xCenter = (minOADate + maxOADate) / 2;
                                    xSpan = maxOADate - minOADate;
                                }
                                else
                                {
                                    // Double değerler
                                    xCenter = (Convert.ToDouble(xRange.MinValue) + Convert.ToDouble(xRange.MaxValue)) / 2;
                                    xSpan = Convert.ToDouble(xRange.MaxValue) - Convert.ToDouble(xRange.MinValue);
                                }
                                
                                // %20 daralt
                                xSpan *= 0.8;
                                
                                xRange.MinValue = xCenter - xSpan / 2;
                                xRange.MaxValue = xCenter + xSpan / 2;
                            }
                            catch
                            {
                                // Dönüşüm hatası - işlemi atla
                                return;
                            }
                        }
                    }
                    else // Zoom Out - daha geniş aralık
                    {
                        // Mevcut range'i genişlet
                        var xRange = diagram.AxisX.VisualRange;
                        var yRange = diagram.AxisY.VisualRange;
                        
                        if (xRange != null && yRange != null)
                        {
                            // Güvenli tip dönüşümü
                            double xCenter, xSpan;
                            
                            try
                            {
                                if (xRange.MinValue is DateTime minDateTime && xRange.MaxValue is DateTime maxDateTime)
                                {
                                    // DateTime değerlerini OADate'e çevir
                                    double minOADate = minDateTime.ToOADate();
                                    double maxOADate = maxDateTime.ToOADate();
                                    xCenter = (minOADate + maxOADate) / 2;
                                    xSpan = maxOADate - minOADate;
                                }
                                else
                                {
                                    // Double değerler
                                    xCenter = (Convert.ToDouble(xRange.MinValue) + Convert.ToDouble(xRange.MaxValue)) / 2;
                                    xSpan = Convert.ToDouble(xRange.MaxValue) - Convert.ToDouble(xRange.MinValue);
                                }
                                
                                // %20 genişlet
                                xSpan *= 1.2;
                                
                                xRange.MinValue = xCenter - xSpan / 2;
                                xRange.MaxValue = xCenter + xSpan / 2;
                            }
                            catch
                            {
                                // Dönüşüm hatası - işlemi atla
                                return;
                            }
                        }
                    }
                    
                    // Chart'ı yenile
                    chartControl1.RefreshData();
                    
                    // Format güncellemesi yap
                    System.Threading.Tasks.Task.Delay(50).ContinueWith(_ => 
                    {
                        if (chartControl1.InvokeRequired)
                        {
                            chartControl1.Invoke(new Action(UpdateChartFormatBasedOnZoom));
                        }
                        else
                        {
                            UpdateChartFormatBasedOnZoom();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse wheel event hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Chart paint event'i - görsel güncelleme sonrası format kontrolü
        /// </summary>
        private void ChartControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                // Paint sonrası format güncellemesi
                UpdateChartFormatBasedOnZoom();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Paint event hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Zoom seviyesine göre chart formatını günceller
        /// </summary>
        private void UpdateChartFormatBasedOnZoom()
        {
            try
            {
                if (chartControl1.Diagram is DevExpress.XtraCharts.XYDiagram diagram)
                {
                    // Mevcut zoom aralığını al
                    var visualRange = diagram.AxisX.VisualRange;
                    if (visualRange != null)
                    {
                        // VisualRange değerlerini güvenli şekilde al
                        DateTime minDate, maxDate;
                        
                        try
                        {
                            if (visualRange.MinValue is DateTime minDateTime)
                            {
                                minDate = minDateTime;
                            }
                            else
                            {
                                double minValue = Convert.ToDouble(visualRange.MinValue);
                                minDate = DateTime.FromOADate(minValue);
                            }
                            
                            if (visualRange.MaxValue is DateTime maxDateTime)
                            {
                                maxDate = maxDateTime;
                            }
                            else
                            {
                                double maxValue = Convert.ToDouble(visualRange.MaxValue);
                                maxDate = DateTime.FromOADate(maxValue);
                            }
                        }
                        catch
                        {
                            // Tarih dönüşümü başarısız olursa varsayılan değerler kullan
                            minDate = DateTime.Now.AddDays(-30);
                            maxDate = DateTime.Now;
                        }
                        
                        TimeSpan span = maxDate - minDate;

                        // Zoom seviyesine göre tarih formatını belirle
                        if (span.TotalDays <= 7)
                        {
                            // 1 hafta veya daha az - günlük görünüm
                            diagram.AxisX.Label.TextPattern = "{A:dd.MM.yyyy}";
                            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Day;
                        }
                        else if (span.TotalDays <= 60)
                        {
                            // 2 ay veya daha az - günlük görünüm (daha az etiket)
                            diagram.AxisX.Label.TextPattern = "{A:dd.MM}";
                            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Day;
                        }
                        else if (span.TotalDays <= 365)
                        {
                            // 1 yıl veya daha az - aylık görünüm
                            diagram.AxisX.Label.TextPattern = "{A:MM.yyyy}";
                            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Month;
                        }
                        else if (span.TotalDays <= 1095) // 3 yıl
                        {
                            // 3 yıl veya daha az - aylık görünüm (daha az etiket)
                            diagram.AxisX.Label.TextPattern = "{A:MM.yyyy}";
                            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Quarter;
                        }
                        else
                        {
                            // 3 yıldan fazla - yıllık görünüm
                            diagram.AxisX.Label.TextPattern = "{A:yyyy}";
                            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DevExpress.XtraCharts.DateTimeGridAlignment.Year;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Zoom format hatası - kritik değil
                System.Diagnostics.Debug.WriteLine($"Zoom format hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Chart'ı doğrudan DataTable'dan doldurur
        /// </summary>
        private void UpdateChartFromDataTable(DataTable data)
        {
            try
            {
                if (data == null || data.Rows.Count == 0)
                    return;

                // Serileri temizle
                if (chartControl1.Series.Count < 2)
                    return;
                    
                chartControl1.Series["GELİR"].Points.Clear();
                chartControl1.Series["GİDER"].Points.Clear();

                // DataTable'daki verileri topla
                Dictionary<DateTime, decimal> gelirData = new Dictionary<DateTime, decimal>();
                Dictionary<DateTime, decimal> giderData = new Dictionary<DateTime, decimal>();

                // Gerekli kolonların varlığını kontrol et
                bool hasYil = data.Columns.Contains("Yil");
                bool hasAy = data.Columns.Contains("Ay");
                bool hasGun = data.Columns.Contains("Gun");
                bool hasGelirGider = data.Columns.Contains("GelirGider");
                bool hasTutar = data.Columns.Contains("Tutar");

                if (!hasYil || !hasAy || !hasGun || !hasGelirGider || !hasTutar)
                {
                    MessageBox.Show("DataTable'da Yil, Ay, Gun, GelirGider veya Tutar kolonları bulunamadı!", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataRow row in data.Rows)
                {
                    try
                    {
                        // Yıl, Ay, Gün bilgilerini al
                        object yilObj = row["Yil"];
                        object ayObj = row["Ay"];
                        object gunObj = row["Gun"];

                        if (yilObj == null || yilObj == DBNull.Value ||
                            ayObj == null || ayObj == DBNull.Value ||
                            gunObj == null || gunObj == DBNull.Value)
                        {
                            continue; // Tarih bilgisi eksikse atla
                        }

                        int yil = Convert.ToInt32(yilObj);
                        int ay = Convert.ToInt32(ayObj);
                        int gun = Convert.ToInt32(gunObj);

                        // Geçerli tarih kontrolü
                        if (yil < 1900 || yil > 2100 || ay < 1 || ay > 12 || gun < 1 || gun > 31)
                        {
                            continue;
                        }

                        DateTime tarih;
                        try
                        {
                            tarih = new DateTime(yil, ay, gun);
                        }
                        catch
                        {
                            continue; // Geçersiz tarih
                        }

                        // Tutar bilgisini al
                        object tutarObj = row["Tutar"];
                        
                        if (tutarObj == null || tutarObj == DBNull.Value)
                        {
                            continue; // Tutar yoksa atla
                        }

                        decimal tutar = Convert.ToDecimal(tutarObj);

                        // GelirGider tipini al
                        object gelirGiderObj = row["GelirGider"];
                        if (gelirGiderObj == null || gelirGiderObj == DBNull.Value)
                        {
                            continue;
                        }

                        string gelirGider = gelirGiderObj.ToString().Trim();

                        // Tarihe göre topla
                        if (gelirGider.Equals("Gelir", StringComparison.OrdinalIgnoreCase))
                        {
                            if (gelirData.ContainsKey(tarih))
                                gelirData[tarih] += tutar;
                            else
                                gelirData[tarih] = tutar;
                        }
                        else if (gelirGider.Equals("Gider", StringComparison.OrdinalIgnoreCase))
                        {
                            if (giderData.ContainsKey(tarih))
                                giderData[tarih] += tutar;
                            else
                                giderData[tarih] = tutar;
                        }
                    }
                    catch
                    {
                        // Satır hatası varsa devam et
                        continue;
                    }
                }

                // Tüm tarihleri birleştir ve sırala
                var allDates = gelirData.Keys.Union(giderData.Keys).OrderBy(d => d).ToList();

                if (allDates.Count == 0)
                {
                    // Veri yoksa boş chart
                    return;
                }

                // Serilere veri ekle
                foreach (var tarih in allDates)
                {
                    decimal gelir = gelirData.ContainsKey(tarih) ? gelirData[tarih] : 0;
                    decimal gider = giderData.ContainsKey(tarih) ? giderData[tarih] : 0;

                    chartControl1.Series["GELİR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(tarih, gelir));
                    chartControl1.Series["GİDER"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(tarih, gider));
                }

                // Veri yüklendikten sonra zoom event'lerini yeniden bağla
                AttachChartZoomEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart güncelleme hatası: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ResolveFieldName(GridView view, params string[] candidates)
        {
            if (view == null || candidates == null || candidates.Length == 0)
            {
                return null;
            }

            // Önce birebir eşleşmeleri kontrol et
            foreach (string candidate in candidates)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                {
                    continue;
                }

                GridColumn directMatch = view.Columns.ColumnByFieldName(candidate);
                if (directMatch != null)
                {
                    return directMatch.FieldName;
                }
            }

            // Ardından normalize edilmiş isimler üzerinden karşılaştır
            foreach (GridColumn column in view.Columns)
            {
                string normalizedColumn = NormalizeFieldName(column.FieldName);

                foreach (string candidate in candidates)
                {
                    if (string.IsNullOrWhiteSpace(candidate))
                    {
                        continue;
                    }

                    if (string.Equals(normalizedColumn, NormalizeFieldName(candidate), StringComparison.OrdinalIgnoreCase))
                    {
                        return column.FieldName;
                    }
                }
            }

            return null;
        }

        private static string NormalizeFieldName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            string normalized = name.Trim();

            normalized = normalized.Replace("İ", "I")
                                   .Replace("ı", "i")
                                   .Replace("Ş", "S")
                                   .Replace("ş", "s")
                                   .Replace("Ğ", "G")
                                   .Replace("ğ", "g")
                                   .Replace("Ü", "U")
                                   .Replace("ü", "u")
                                   .Replace("Ö", "O")
                                   .Replace("ö", "o")
                                   .Replace("Ç", "C")
                                   .Replace("ç", "c");

            return normalized.Replace("_", string.Empty)
                             .Replace(" ", string.Empty);
        }

        /// <summary>
        /// Chart'ı grid'deki verilerle doldurur (Yil, Ay, Gun, GelirGider kolonlarını kullanır)
        /// </summary>
        private void UpdateChartFromGrid()
        {
            try
            {
                // Grid view'dan verileri al
                GridView view = gridView1;
                if (view == null || view.RowCount == 0)
                {
                    // Grid henüz yüklenmemiş veya veri yok
                    return;
                }

                // Serileri temizle
                if (chartControl1.Series.Count < 2)
                    return;
                    
                chartControl1.Series["GELİR"].Points.Clear();
                chartControl1.Series["GİDER"].Points.Clear();

                // Grid'deki verileri topla
                Dictionary<DateTime, decimal> gelirData = new Dictionary<DateTime, decimal>();
                Dictionary<DateTime, decimal> giderData = new Dictionary<DateTime, decimal>();

                // Gerekli kolon adlarını tespit et
                string yilField = ResolveFieldName(view, "Yil", "YIL", "Yıl", "YEAR");
                string ayField = ResolveFieldName(view, "Ay", "AY", "Month");
                string gunField = ResolveFieldName(view, "Gun", "GUN", "Gün", "DAY");
                string gelirGiderField = ResolveFieldName(view, "GelirGider", "GELIRGIDER", "Gelir_Gider", "GelirGiderTipi", "Tip");
                string tutarField = ResolveFieldName(view, "Tutar", "TUTAR", "ToplamTutar", "TutarToplam", "Amount");

                if (string.IsNullOrEmpty(yilField) ||
                    string.IsNullOrEmpty(ayField) ||
                    string.IsNullOrEmpty(gunField) ||
                    string.IsNullOrEmpty(gelirGiderField) ||
                    string.IsNullOrEmpty(tutarField))
                {
                    // Gerekli kolonlar yoksa grafiği güncellemeyi atla
                    return;
                }

                for (int i = 0; i < view.RowCount; i++)
                {
                    try
                    {
                        // Yıl, Ay, Gün bilgilerini al
                        object yilObj = view.GetRowCellValue(i, yilField);
                        object ayObj = view.GetRowCellValue(i, ayField);
                        object gunObj = view.GetRowCellValue(i, gunField);

                        if (yilObj == null || yilObj == DBNull.Value ||
                            ayObj == null || ayObj == DBNull.Value ||
                            gunObj == null || gunObj == DBNull.Value)
                        {
                            continue; // Tarih bilgisi eksikse atla
                        }

                        int yil = Convert.ToInt32(yilObj);
                        int ay = Convert.ToInt32(ayObj);
                        int gun = Convert.ToInt32(gunObj);

                        // Geçerli tarih kontrolü
                        if (yil < 1900 || yil > 2200 || ay < 1 || ay > 12 || gun < 1 || gun > 31)
                        {
                            continue;
                        }

                        DateTime tarih;
                        try
                        {
                            tarih = new DateTime(yil, ay, gun);
                        }
                        catch
                        {
                            continue; // Geçersiz tarih
                        }

                        // Tutar bilgisini al
                        object tutarObj = view.GetRowCellValue(i, tutarField);
                        
                        if (tutarObj == null || tutarObj == DBNull.Value)
                        {
                            continue; // Tutar yoksa atla
                        }

                        decimal tutar = Convert.ToDecimal(tutarObj);

                        // GelirGider tipini al
                        object gelirGiderObj = view.GetRowCellValue(i, gelirGiderField);
                        if (gelirGiderObj == null || gelirGiderObj == DBNull.Value)
                        {
                            continue;
                        }

                        string gelirGider = gelirGiderObj.ToString().Trim();

                        // Tarihe göre topla
                        if (gelirGider.Equals("Gelir", StringComparison.OrdinalIgnoreCase))
                        {
                            if (gelirData.ContainsKey(tarih))
                                gelirData[tarih] += tutar;
                            else
                                gelirData[tarih] = tutar;
                        }
                        else if (gelirGider.Equals("Gider", StringComparison.OrdinalIgnoreCase))
                        {
                            if (giderData.ContainsKey(tarih))
                                giderData[tarih] += tutar;
                            else
                                giderData[tarih] = tutar;
                        }
                    }
                    catch
                    {
                        // Satır hatası varsa devam et
                        continue;
                    }
                }

                // Tüm tarihleri birleştir ve sırala
                var allDates = gelirData.Keys.Union(giderData.Keys).OrderBy(d => d).ToList();

                if (allDates.Count == 0)
                {
                    // Veri yoksa boş chart
                    return;
                }

                // Serilere veri ekle
                foreach (var tarih in allDates)
                {
                    decimal gelir = gelirData.ContainsKey(tarih) ? gelirData[tarih] : 0;
                    decimal gider = giderData.ContainsKey(tarih) ? giderData[tarih] : 0;

                    chartControl1.Series["GELİR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(tarih, gelir));
                    chartControl1.Series["GİDER"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(tarih, gider));
                }

                // Veri yüklendikten sonra zoom event'lerini yeniden bağla
                AttachChartZoomEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chart güncelleme hatası: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chart'ı verilerle doldurur (DataTable'dan)
        /// </summary>
  
     
    }
}
