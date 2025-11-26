using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllFinance.SQL
{
    public class DataHelper
    {
        public static void FilterChartData(ChartControl chartControl, DataTable originalDataTable, DateTime startDate, DateTime endDate)
        {
            // Orijinal verileri yıl ve ay bazında grupla
            var monthlyData = originalDataTable.AsEnumerable()
                .Where(row => row.Field<DateTime>("Tarih") >= startDate && row.Field<DateTime>("Tarih") <= endDate)
                .GroupBy(row => row.Field<DateTime>("Tarih").ToString("yyyy-MM")) // Yıl-Ay formatında gruplama
                    .Select(g => new
                    {
                        YilAy = g.Key, // YYYY-MM formatında tarih
                        Tutar = g.Sum(row => row.Field<decimal>("Tutar")) // Aynı ay için Tutar değerlerini topluyoruz
                    })
                    .OrderBy(g => g.YilAy) // Tarihe göre sıralama
                    .ToList();

            // Gruplama sonuçları için yeni bir DataTable oluştur
            DataTable filteredData = new DataTable();
            filteredData.Columns.Add("YilAy", typeof(string));
            filteredData.Columns.Add("Tutar", typeof(decimal));

            foreach (var data in monthlyData)
            {
                filteredData.Rows.Add(data.YilAy, data.Tutar);
            }
            if (chartControl != null)
            {
                foreach (Series series in chartControl.Series)
                {
                    series.DataSource = null; // Mevcut veriyi temizle

                    if (filteredData.Rows.Count > 0) // Veri olup olmadığını kontrol et
                    {
                        series.DataSource = filteredData;
                        series.ArgumentDataMember = "YilAy"; // Yıl ve ay bilgisi
                        series.ValueDataMembers.AddRange(new string[] { "Tutar" });
                    }
                    else
                    {
                        // Opsiyonel olarak bu seri için boş veri durumunu ele al
                        Console.WriteLine($"Seri için veri yok: {series.Name}");
                    }
                }

                // Grafiği güncelle
                chartControl.Refresh();
            }
        }

        public static void ShowFilterMenu(ChartControl chartControl, PivotGridControl pivotGridControl, GridControl gridControl, DataTable originalDataTable)
        {
            ContextMenuStrip filterMenu = new ContextMenuStrip();

            filterMenu.Items.Add("Bu Yıl", null, (s, ev) =>
            {
                ApplyDateRangeFilter(chartControl, pivotGridControl, gridControl, originalDataTable,
                    new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
            });

            filterMenu.Items.Add("Bu Ay", null, (s, ev) =>
            {
                var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                ApplyDateRangeFilter(chartControl, pivotGridControl, gridControl, originalDataTable, startOfMonth, DateTime.Now);
            });

            filterMenu.Items.Add("Tarih Aralığı Seç", null, (s, ev) =>
            {
                using (var datePicker = new DateRangePicker())
                {
                    if (datePicker.ShowDialog() == DialogResult.OK)
                    {
                        ApplyDateRangeFilter(chartControl, pivotGridControl, gridControl, originalDataTable,
                            datePicker.StartDate, datePicker.EndDate);
                    }
                }
            });

            filterMenu.Show(Cursor.Position);
        }

        public class DateRangePicker : Form
        {
            private DateEdit dateEditStart;
            private DateEdit dateEditEnd;
            private SimpleButton btnOk;
            private SimpleButton btnCancel;

            public DateTime StartDate { get; private set; }
            public DateTime EndDate { get; private set; }

            public DateRangePicker()
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

        public static void ApplyDateRangeFilter(ChartControl chartControl, PivotGridControl pivotGridControl, GridControl gridControl, DataTable originalDataTable, DateTime startDate, DateTime endDate)
        {
            DataTable filteredData;
            // Veriyi tarih aralığına göre filtreliyoruz
            try
            {
                filteredData = originalDataTable.AsEnumerable()
             .Where(row => row.Field<DateTime>("Tarih") >= startDate && row.Field<DateTime>("Tarih") <= endDate)
             .CopyToDataTable();
                // YilAy kolonunu ekliyoruz (eğer mevcut değilse)
                if (!filteredData.Columns.Contains("YilAy"))
                {
                    filteredData.Columns.Add("YilAy", typeof(string));
                }

                foreach (DataRow row in filteredData.Rows)
                {
                    // Tarih kolonunu YYYY-MM formatına çeviriyoruz ve YilAy kolonuna ekliyoruz
                    row["YilAy"] = ((DateTime)row["Tarih"]).ToString("yyyy-MM");
                }
            }
            catch
            {

                filteredData = null;
            }

            if (filteredData != null)
            {
                if (chartControl != null)
                {
                    // ChartControl'ü filtreliyoruz
                    FilterChartData(chartControl, filteredData, startDate, endDate);
                }
                if (gridControl != null)
                {

                    // GridControl'e filtrelenmiş veriyi ekliyoruz
                    gridControl.DataSource = filteredData;
                    gridControl.RefreshDataSource();
                }
                if (pivotGridControl != null)
                {
                    // PivotGridControl'e filtrelenmiş veriyi ekliyoruz
                    pivotGridControl.DataSource = filteredData;
                    pivotGridControl.RefreshData();
                }
            }


        }

    }
}
