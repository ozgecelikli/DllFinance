using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DllFinance.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DllFinance.DLLFinance.FaturaYonetimi.Alis
{
    public partial class AlisFaturaDetaylari : UserControl
    {
        private DataTable alisFaturaVerileri;
        private string tarihKolonAdi;
        private string firmaKolonAdi;
        private string stokKolonAdi;

        public AlisFaturaDetaylari()
        {
            InitializeComponent();
            btnListele.Click += BtnListele_Click;
            YukleAlisFaturaVerileri();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            FiltreleriUygula();
        }

        private void YukleAlisFaturaVerileri()
        {
            try
            {
                alisFaturaVerileri = SqlHelper.Instance.GetDataTable("CF_alisfaturaView");

                if (alisFaturaVerileri == null || alisFaturaVerileri.Rows.Count == 0)
                {
                    MessageBox.Show("CF_alisfaturaView verisi bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gcAlisFatura.DataSource = alisFaturaVerileri;
                KolonIsimleriniCoz();
                GridKolonlariniYapilandir();
                FiltreleriUygula();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KolonIsimleriniCoz()
        {
            if (alisFaturaVerileri == null)
            {
                return;
            }

            tarihKolonAdi = TarihKolonunuBul();
            firmaKolonAdi = KolonBul(new[] { "FIRMAKOD", "FIRMA_KOD", "FIRMAKODU", "CARIKOD", "CARI_KOD", "CARIKODU" });
            stokKolonAdi = KolonBul(new[] { "STOKKOD", "STOK_KOD", "STOKKODU", "URUNKOD", "URUN_KOD", "URUNKODU" });
        }

        private string TarihKolonunuBul()
        {
            var kolon = alisFaturaVerileri.Columns.Cast<DataColumn>()
                .FirstOrDefault(c => c.DataType == typeof(DateTime));

            if (kolon != null)
            {
                return kolon.ColumnName;
            }

            var isimleEslesenKolon = alisFaturaVerileri.Columns.Cast<DataColumn>()
                .FirstOrDefault(c => new[] { "TARIH", "TARİH", "FATURATARIH", "FATURA_TARIH", "FATURATARIHI", "FATURA_TARİHİ" }
                    .Any(keyword => c.ColumnName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0));

            if (isimleEslesenKolon != null && isimleEslesenKolon.DataType == typeof(DateTime))
            {
                return isimleEslesenKolon.ColumnName;
            }

            return null;
        }

        private string KolonBul(string[] anahtarKelimeler)
        {
            return alisFaturaVerileri.Columns.Cast<DataColumn>()
                .FirstOrDefault(c => anahtarKelimeler.Any(keyword =>
                    c.ColumnName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                ?.ColumnName;
        }

        private void GridKolonlariniYapilandir()
        {
            if (gvAlisFatura == null)
            {
                return;
            }

            GridView view = gvAlisFatura;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowFooter = true;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsBehavior.Editable = false;

            foreach (GridColumn column in view.Columns)
            {
                column.Caption = KolonBasliginiDuzenle(column.Caption);

                if (!string.IsNullOrEmpty(tarihKolonAdi) &&
                    string.Equals(column.FieldName, tarihKolonAdi, StringComparison.OrdinalIgnoreCase))
                {
                    column.Caption = "Fatura Tarihi";
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = "dd.MM.yyyy";
                }

                if (!string.IsNullOrEmpty(firmaKolonAdi) &&
                    string.Equals(column.FieldName, firmaKolonAdi, StringComparison.OrdinalIgnoreCase))
                {
                    column.Caption = "Firma Kodu";
                }

                if (!string.IsNullOrEmpty(stokKolonAdi) &&
                    string.Equals(column.FieldName, stokKolonAdi, StringComparison.OrdinalIgnoreCase))
                {
                    column.Caption = "Stok Kodu";
                }

                if (column.ColumnType == typeof(decimal) ||
                    column.ColumnType == typeof(double) ||
                    column.ColumnType == typeof(float))
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    column.DisplayFormat.FormatString = "n2";
                    column.Summary.Clear();
                    column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0:n2}");
                }
            }

            view.BestFitColumns();
        }

        private static string KolonBasliginiDuzenle(string orijinalBaslik)
        {
            if (string.IsNullOrWhiteSpace(orijinalBaslik))
            {
                return orijinalBaslik;
            }

            return orijinalBaslik.Replace("_", " ");
        }

        private void FiltreleriUygula()
        {
            if (gvAlisFatura == null || alisFaturaVerileri == null)
            {
                return;
            }

            try
            {
                var filtreler = new List<CriteriaOperator>();

                DateTime? baslangicTarihi = deBaslangicTarihi.EditValue is DateTime baslangic
                    ? baslangic.Date
                    : (DateTime?)null;

                DateTime? bitisTarihi = deBitisTarihi.EditValue is DateTime bitis
                    ? bitis.Date
                    : (DateTime?)null;

                string firmaKodu = txtFirmaKodu.Text?.Trim();
                string stokKodu = txtStokKodu.Text?.Trim();

                if (baslangicTarihi.HasValue && !string.IsNullOrEmpty(tarihKolonAdi))
                {
                    filtreler.Add(new BinaryOperator(new OperandProperty(tarihKolonAdi), baslangicTarihi.Value, BinaryOperatorType.GreaterOrEqual));
                }

                if (bitisTarihi.HasValue && !string.IsNullOrEmpty(tarihKolonAdi))
                {
                    DateTime bitisGunSonu = bitisTarihi.Value.Date.AddDays(1).AddTicks(-1);
                    filtreler.Add(new BinaryOperator(new OperandProperty(tarihKolonAdi), bitisGunSonu, BinaryOperatorType.LessOrEqual));
                }

                if (!string.IsNullOrEmpty(firmaKodu) && !string.IsNullOrEmpty(firmaKolonAdi))
                {
                    filtreler.Add(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(firmaKolonAdi), new OperandValue(firmaKodu)));
                }

                if (!string.IsNullOrEmpty(stokKodu) && !string.IsNullOrEmpty(stokKolonAdi))
                {
                    filtreler.Add(new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty(stokKolonAdi), new OperandValue(stokKodu)));
                }

                if (filtreler.Count > 0)
                {
                    gvAlisFatura.ActiveFilterCriteria = new GroupOperator(GroupOperatorType.And, filtreler);
                }
                else
                {
                    gvAlisFatura.ActiveFilterCriteria = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Filtre uygulanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
