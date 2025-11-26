using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllFinance.DLLFinance.Ozet
{
    public partial class OzetAlisSatis : UserControl
    {
        public OzetAlisSatis()
        {
            InitializeComponent();
            LoadData();
        }
        DataTable originalAlisDataTable, originalSatisDataTable;
      
        private void LoadData()
        {
            // Alış verilerini yükle
            LoadAlisData();

            // Satış verilerini yükle
            LoadSatisData();
        }
        private void LoadAlisData()
        {
            // Singleton'dan alış faturalarını al
            originalAlisDataTable = SQL.SqlHelper.Instance.GetDataTable("CF_alisfaturaView");

            DataTable alisFaturaTable = SQL.SqlHelper.Instance.GetDataTable("CF_alisfaturaView");

            if (alisFaturaTable != null)
            {
                // Alış faturalarını yıl ve ay bazında grupluyoruz ve Tutar değerlerini topluyoruz
                var groupedData = alisFaturaTable.AsEnumerable()
                    .GroupBy(row => row.Field<DateTime>("Tarih").ToString("yyyy-MM")) // Yıl-Ay formatında gruplama
                    .Select(g => new
                    {
                        YilAy = g.Key, // YYYY-MM formatında tarih
                Tutar = g.Sum(row => row.Field<decimal>("Tutar")) // Aynı ay için Tutar değerlerini topluyoruz
            })
                    .OrderBy(g => g.YilAy) // Tarihe göre sıralama
                    .ToList();

                // Gruplanmış verileri yeni bir DataTable'a ekle
                DataTable filteredData = new DataTable();
                filteredData.Columns.Add("YilAy", typeof(string));
                filteredData.Columns.Add("Tutar", typeof(decimal));

                foreach (var data in groupedData)
                {
                    filteredData.Rows.Add(data.YilAy, data.Tutar);
                }

                // Alış verilerini PivotGrid ve GridControl'e ekliyoruz
                AddDataToGridControlAndPivot("Alış Faturaları", filteredData, originalAlisDataTable, pgc_Alis, cc_alis);
            }
        }
        private void LoadSatisData()
        {
            // Singleton'dan alış faturalarını al
            originalSatisDataTable = SQL.SqlHelper.Instance.GetDataTable("CF_satisfaturaView");

            DataTable satisFaturaTable = SQL.SqlHelper.Instance.GetDataTable("CF_satisfaturaView");

            if (satisFaturaTable != null)
            {
                // Satış faturalarını yıl ve ay bazında grupluyoruz ve Tutar değerlerini topluyoruz
                var groupedData = satisFaturaTable.AsEnumerable()
                    .GroupBy(row => row.Field<DateTime>("Tarih").ToString("yyyy-MM")) // Yıl-Ay formatında gruplama
                    .Select(g => new
                    {
                        YilAy = g.Key, // YYYY-MM formatında tarih
                        Tutar = g.Sum(row => row.Field<decimal>("Tutar")) // Aynı ay için Tutar değerlerini topluyoruz
                    })
                    .OrderBy(g => g.YilAy) // Tarihe göre sıralama
                    .ToList();

                // Gruplanmış verileri yeni bir DataTable'a ekle
                DataTable filteredData = new DataTable();
                filteredData.Columns.Add("YilAy", typeof(string));
                filteredData.Columns.Add("Tutar", typeof(decimal));

                foreach (var data in groupedData)
                {
                    filteredData.Rows.Add(data.YilAy, data.Tutar);
                }

                // Alış verilerini PivotGrid ve GridControl'e ekliyoruz
                AddDataToGridControlAndPivot("Satış Faturaları", filteredData, originalSatisDataTable, pgc_Satis, cc_satis);
            }
        }
      
        private void AddDataToGridControlAndPivot(string seriesName, DataTable filteredData, DataTable originalDataTable, PivotGridControl pivotGrid, ChartControl chartControl)
        {
            // Orijinal tabloya Yıl-Ay sütunu ekliyoruz
            originalDataTable.Columns.Add("YilAy", typeof(string));
            foreach (DataRow row in originalDataTable.Rows)
            {
                row["YilAy"] = ((DateTime)row["Tarih"]).ToString("yyyy-MM");
            }

            // PivotGridControl'e veri kaynağını veriyoruz
            pivotGrid.DataSource = originalDataTable;

            // PivotGrid alanlarını temizleyip tekrar ekliyoruz
            pivotGrid.Fields.Clear();

            // Satır alanı olarak FaturaTipi
            PivotGridField fieldFaturaTipi = new PivotGridField("Tipi", PivotArea.RowArea);
            pivotGrid.Fields.Add(fieldFaturaTipi);

            // Sütun alanı olarak YilAy (Yıl-Ay)
            PivotGridField fieldYilAy = new PivotGridField("YilAy", PivotArea.ColumnArea);
            pivotGrid.Fields.Add(fieldYilAy);

            // Veri alanları olarak Tutar ve Miktar
            PivotGridField fieldTutar = new PivotGridField("Tutar", PivotArea.DataArea);
            pivotGrid.Fields.Add(fieldTutar);

            PivotGridField fieldMiktar = new PivotGridField("Miktar", PivotArea.DataArea);
            pivotGrid.Fields.Add(fieldMiktar);

            // PivotGrid'i yenile
            pivotGrid.RefreshData();

            // Seriyi grafiğe ekle
            Series series = new Series(seriesName, ViewType.Bar);
            series.DataSource = filteredData;
            series.ArgumentDataMember = "YilAy"; // X ekseni (YYYY-MM)
            series.ValueDataMembers.AddRange(new string[] { "Tutar" }); // Y ekseni (Tutar)

            // Chart'e seriyi ekliyoruz
            chartControl.Series.Add(series);

            // Grafiği yenile
            chartControl.Refresh();
        }
         
        private void layoutControlGroup1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption.ToString() == "Filtre")
            {
                SQL.DataHelper.ShowFilterMenu(cc_alis, pgc_Alis, null, originalAlisDataTable);
 
                //// ContextMenu oluşturma
                //ContextMenuStrip filterMenu = new ContextMenuStrip();

                //// Menü öğeleri ekle
                //filterMenu.Items.Add("Bu Yıl", null, (s, ev) => ApplyFilter("Bu Yıl", pgc_Alis, cc_alis, originalAlisDataTable));
                //filterMenu.Items.Add("Bu Ay", null, (s, ev) => ApplyFilter("Bu Ay", pgc_Alis, cc_alis, originalAlisDataTable));
                //filterMenu.Items.Add("Tarih Aralığı Seç", null, (s, ev) => ApplyFilter("Tarih Aralığı", pgc_Alis, cc_alis, originalAlisDataTable));

                //// Menü göster
                //filterMenu.Show(Cursor.Position);
            }
            if (e.Button.Properties.Caption.ToString() == "Detay")
            {
                // `pgc_Alis` PivotGridControl'ünden veri alın
                DataTable detailDataTable = pgc_Alis.DataSource as DataTable;
                 
                if (detailDataTable != null)
                {
                    VeriDetay detayForm = new VeriDetay(detailDataTable);
                    detayForm.Show();
                }
                else
                {
                    MessageBox.Show("Gösterilecek veri yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private void ApplyFilter(string filterOption, PivotGridControl pivotGrid, ChartControl chartControl, DataTable originalDataTable )
        {
            DateTime startDate, endDate;

            switch (filterOption)
            {
                case "Bu Yıl":
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                    endDate = new DateTime(DateTime.Now.Year, 12, 31);
                    SQL.DataHelper.ApplyDateRangeFilter(chartControl, pivotGrid, null, originalDataTable, startDate, endDate);
                    break;

                case "Bu Ay":
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    SQL.DataHelper.ApplyDateRangeFilter(chartControl, pivotGrid, null, originalDataTable, startDate, endDate);
                    break;

                case "Tarih Aralığı":
                    using (var dateRangeForm = new DateRangeForm())
                    {
                        if (dateRangeForm.ShowDialog() == DialogResult.OK)
                        {
                            startDate = dateRangeForm.StartDate;
                            endDate = dateRangeForm.EndDate;
                            SQL.DataHelper.ApplyDateRangeFilter(chartControl, pivotGrid, null, originalDataTable, startDate, endDate);
                        }
                    }
                    break;
            }
        }
        public class DateRangeForm : Form
        {
            private DateEdit dateEditStart;
            private DateEdit dateEditEnd;
            private SimpleButton btnOk;
            private SimpleButton btnCancel;

            public DateTime StartDate { get; private set; }
            public DateTime EndDate { get; private set; }

            public DateRangeForm()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                // Başlangıç tarihi seçici
                dateEditStart = new DateEdit
                {
                    Location = new System.Drawing.Point(20, 20),
                    Width = 200,
                    Properties = { VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView }
                };
                dateEditStart.Properties.Mask.EditMask = "yyyy-MM"; // Yıl-Ay formatı
                dateEditStart.Properties.Mask.UseMaskAsDisplayFormat = true;

                // Bitiş tarihi seçici
                dateEditEnd = new DateEdit
                {
                    Location = new System.Drawing.Point(20, 60),
                    Width = 200,
                    Properties = { VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView }
                };
                dateEditEnd.Properties.Mask.EditMask = "yyyy-MM"; // Yıl-Ay formatı
                dateEditEnd.Properties.Mask.UseMaskAsDisplayFormat = true;

                // Tamam butonu
                btnOk = new SimpleButton
                {
                    Text = "Tamam",
                    Location = new System.Drawing.Point(20, 100),
                    Width = 90
                };
                btnOk.Click += BtnOk_Click;

                // İptal butonu
                btnCancel = new SimpleButton
                {
                    Text = "İptal",
                    Location = new System.Drawing.Point(130, 100),
                    Width = 90
                };
                btnCancel.Click += BtnCancel_Click;

                // Form ayarları
                this.Text = "Tarih Aralığı Seç";
                this.ClientSize = new System.Drawing.Size(250, 150);
                this.Controls.Add(dateEditStart);
                this.Controls.Add(dateEditEnd);
                this.Controls.Add(btnOk);
                this.Controls.Add(btnCancel);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.AcceptButton = btnOk;
                this.CancelButton = btnCancel;
            }

            private void BtnOk_Click(object sender, EventArgs e)
            {
                // Seçilen başlangıç ve bitiş tarihlerini al
                var selectedStartDate = dateEditStart.DateTime;
                var selectedEndDate = dateEditEnd.DateTime;

                // Başlangıç tarihini ayın ilk günü olarak ayarla
                StartDate = new DateTime(selectedStartDate.Year, selectedStartDate.Month, 1);

                // Bitiş tarihini ayın son günü olarak ayarla
                EndDate = new DateTime(selectedEndDate.Year, selectedEndDate.Month, DateTime.DaysInMonth(selectedEndDate.Year, selectedEndDate.Month));

                // Tarihlerin geçerliliğini kontrol et
                if (StartDate > EndDate)
                {
                    XtraMessageBox.Show("Başlangıç tarihi bitiş tarihinden sonra olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            private void BtnCancel_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void layoutControlGroup2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption.ToString() == "Filtre")
            {
                SQL.DataHelper.ShowFilterMenu(cc_satis, pgc_Satis, null, originalSatisDataTable);
                //// ContextMenu oluşturma
                //ContextMenuStrip filterMenu = new ContextMenuStrip();

                //// Menü öğeleri ekle
                //filterMenu.Items.Add("Bu Yıl", null, (s, ev) => ApplyFilter("Bu Yıl", pgc_Satis, cc_satis,originalSatisDataTable));
                //filterMenu.Items.Add("Bu Ay", null, (s, ev) => ApplyFilter("Bu Ay", pgc_Satis, cc_satis, originalSatisDataTable));
                //filterMenu.Items.Add("Tarih Aralığı Seç", null, (s, ev) => ApplyFilter("Tarih Aralığı", pgc_Satis, cc_satis, originalSatisDataTable));

                //// Menü göster
                //filterMenu.Show(Cursor.Position);
            }
            if (e.Button.Properties.Caption.ToString() == "Detay")
            {
                // `pgc_Alis` PivotGridControl'ünden veri alın
                DataTable detailDataTable = pgc_Satis.DataSource as DataTable;

                if (detailDataTable != null)
                {
                    VeriDetay detayForm = new VeriDetay(detailDataTable);
                    detayForm.Show();
                }
                else
                {
                    MessageBox.Show("Gösterilecek veri yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
