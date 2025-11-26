using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DllFinance.SQL;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DllFinance.DLLFinance.FaturaYonetimi.Satis
{
    public partial class SatisUrunRaporu : UserControl
    {
        private DataTable satisFaturaTablosu;
        private DataTable stokOzetTablosu;
        private string stokKoduKolonu;
        private string stokAdiKolonu;
        private string birimFiyatKolonu;
        private string miktarKolonu;
        private string tipKolonu;
        private string belgeKolonu;

        public SatisUrunRaporu()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                YukleVeri();
            }
        }

        private void YukleVeri()
        {
            try
            {
                satisFaturaTablosu = SqlHelper.Instance.GetDataTable("CF_satisfaturaView");

                if (satisFaturaTablosu == null || satisFaturaTablosu.Rows.Count == 0)
                {
                    MessageBox.Show("CF_satisfaturaView verisi bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                KolonlariCoz();
                stokOzetTablosu = StokBazliOzetOlustur();

                if (stokOzetTablosu == null || stokOzetTablosu.Rows.Count == 0)
                {
                    MessageBox.Show("Stok verisi oluşturulamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                gcStok.DataSource = stokOzetTablosu;
                GridAyarla();
                MiktarPastasiniCiz();
                CiroTop10Ciz();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KolonlariCoz()
        {
            stokKoduKolonu = KolonBul("STOKKODU", "STOKKOD", "STOK_KODU", "URUNKODU", "URUN_KODU");
            stokAdiKolonu = KolonBul("STOKADI", "STOK_ADI", "URUNADI", "URUN_ADI");
            birimFiyatKolonu = KolonBul("BIRIMFIYAT", "BIRIM_FIYAT", "FIYAT", "BIRIM_TUTAR");
            miktarKolonu = KolonBul("MIKTAR", "ADET", "BIRIMMikTAR", "BIRIMMIKTAR");
            tipKolonu = KolonBul("TIPI", "TIP", "ISLEMTIPI", "HAREKETTIPI");
            belgeKolonu = KolonBul("BELGETIPI", "BELGETIP", "FATURATIPI");
        }

        private string KolonBul(params string[] ihtimaller)
        {
            if (satisFaturaTablosu == null)
            {
                return null;
            }

            return satisFaturaTablosu.Columns.Cast<DataColumn>()
                .FirstOrDefault(c => ihtimaller.Any(aranan =>
                    c.ColumnName.Equals(aranan, StringComparison.OrdinalIgnoreCase) ||
                    c.ColumnName.Replace("_", "").Equals(aranan.Replace("_", ""), StringComparison.OrdinalIgnoreCase)))
                ?.ColumnName;
        }

        private DataTable StokBazliOzetOlustur()
        {
            if (string.IsNullOrEmpty(stokKoduKolonu) || string.IsNullOrEmpty(miktarKolonu))
            {
                MessageBox.Show("Stok kodu veya miktar kolonları bulunamadı. Lütfen view alanlarını kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var tablo = new DataTable();
            tablo.Columns.Add("StokKodu", typeof(string));
            tablo.Columns.Add("StokAdi", typeof(string));
            tablo.Columns.Add("MinBirimFiyat", typeof(decimal));
            tablo.Columns.Add("MaxBirimFiyat", typeof(decimal));
            tablo.Columns.Add("SatisToplamMiktar", typeof(decimal));
            tablo.Columns.Add("SatisIadeToplamMiktar", typeof(decimal));
            tablo.Columns.Add("CiroToplami", typeof(decimal));

            var gruplanmis = satisFaturaTablosu.AsEnumerable()
                .Where(row => !string.IsNullOrWhiteSpace(row[stokKoduKolonu]?.ToString()))
                .GroupBy(row => row[stokKoduKolonu]?.ToString());

            foreach (var grup in gruplanmis)
            {
                var fiyatlar = grup.Select(r => SayiAl(r, birimFiyatKolonu)).Where(v => v > 0).ToList();
                var minFiyat = fiyatlar.Count > 0 ? fiyatlar.Min() : 0m;
                var maxFiyat = fiyatlar.Count > 0 ? fiyatlar.Max() : 0m;
                decimal satisMiktari = 0;
                decimal iadeMiktari = 0;
                decimal ciro = 0;
                string stokAdi = string.Empty;

                foreach (var satir in grup)
                {
                    if (string.IsNullOrEmpty(stokAdi) && !string.IsNullOrEmpty(stokAdiKolonu))
                    {
                        var aday = satir[stokAdiKolonu]?.ToString();
                        if (!string.IsNullOrWhiteSpace(aday))
                        {
                            stokAdi = aday;
                        }
                    }

                    decimal miktar = SayiAl(satir, miktarKolonu);
                    decimal fiyat = SayiAl(satir, birimFiyatKolonu);
                    bool isIade = SatirIadeMi(satir);

                    if (isIade)
                    {
                        iadeMiktari += miktar;
                        ciro -= fiyat * miktar;
                    }
                    else
                    {
                        satisMiktari += miktar;
                        ciro += fiyat * miktar;
                    }
                }

                var yeniSatir = tablo.NewRow();
                yeniSatir["StokKodu"] = grup.Key;
                yeniSatir["StokAdi"] = stokAdi;
                yeniSatir["MinBirimFiyat"] = minFiyat;
                yeniSatir["MaxBirimFiyat"] = maxFiyat;
                yeniSatir["SatisToplamMiktar"] = satisMiktari;
                yeniSatir["SatisIadeToplamMiktar"] = iadeMiktari;
                yeniSatir["CiroToplami"] = ciro;
                tablo.Rows.Add(yeniSatir);
            }

            return tablo;
        }

        private decimal SayiAl(DataRow row, string kolonAdi)
        {
            if (string.IsNullOrEmpty(kolonAdi) || row == null || !satisFaturaTablosu.Columns.Contains(kolonAdi))
            {
                return 0;
            }

            var deger = row[kolonAdi];
            if (deger == DBNull.Value || deger == null)
            {
                return 0;
            }

            if (deger is decimal dec)
            {
                return dec;
            }

            if (deger is double dbl)
            {
                return Convert.ToDecimal(dbl);
            }

            if (deger is float flt)
            {
                return Convert.ToDecimal(flt);
            }

            if (decimal.TryParse(Convert.ToString(deger, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sonuc))
            {
                return sonuc;
            }

            return 0;
        }

        private bool SatirIadeMi(DataRow row)
        {
            string DegerAl(string kolon)
                => string.IsNullOrEmpty(kolon) ? null : row[kolon]?.ToString();

            string tipDeger = DegerAl(tipKolonu);
            string belgeDeger = DegerAl(belgeKolonu);

            return MetinIadeMi(tipDeger) || MetinIadeMi(belgeDeger);
        }

        private static bool MetinIadeMi(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var upper = text.ToUpperInvariant();
            return upper.Contains("IADE") || upper.Contains("İADE");
        }

        private void GridAyarla()
        {
            if (gvStok == null)
            {
                return;
            }

            GridView view = gvStok;
            view.OptionsView.ShowAutoFilterRow = true;
            view.OptionsView.ShowFooter = true;
            view.OptionsView.ColumnAutoWidth = false;
            view.OptionsBehavior.Editable = false;

            void KolonAyarla(string fieldName, string baslik, string format = null, bool summary = false)
            {
                GridColumn column = view.Columns[fieldName];
                if (column == null)
                {
                    return;
                }

                column.Caption = baslik;
                if (!string.IsNullOrEmpty(format))
                {
                    column.DisplayFormat.FormatType = FormatType.Numeric;
                    column.DisplayFormat.FormatString = format;
                }

                if (summary)
                {
                    column.Summary.Clear();
                    column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, fieldName, "{0:n2}");
                }
            }

            KolonAyarla("StokKodu", "Stok Kodu");
            KolonAyarla("StokAdi", "Stok Adı");
            KolonAyarla("MinBirimFiyat", "Min. Birim Fiyat", "n2");
            KolonAyarla("MaxBirimFiyat", "Maks. Birim Fiyat", "n2");
            KolonAyarla("SatisToplamMiktar", "Satış Toplam Miktarı", "n2", true);
            KolonAyarla("SatisIadeToplamMiktar", "Satış İade Toplam Miktarı", "n2", true);

            if (view.Columns["CiroToplami"] != null)
            {
                view.Columns["CiroToplami"].Visible = false;
            }

            view.BestFitColumns();
        }

        private void MiktarPastasiniCiz()
        {
            if (chartMiktar == null || stokOzetTablosu == null)
            {
                return;
            }

            chartMiktar.Series.Clear();

            var data = stokOzetTablosu.AsEnumerable()
                .OrderByDescending(r => r.Field<decimal>("SatisToplamMiktar"))
                .Where(r => r.Field<decimal>("SatisToplamMiktar") > 0)
                .Take(20)
                .Select(r => new
                {
                    StokKodu = r.Field<string>("StokKodu"),
                    Miktar = r.Field<decimal>("SatisToplamMiktar")
                })
                .ToList();

            Series seri = new Series("Stok Satış Miktarları", ViewType.Pie)
            {
                DataSource = data,
                ArgumentDataMember = "StokKodu"
            };
            seri.ValueDataMembers.AddRange("Miktar");
            seri.LabelsVisibility = DefaultBoolean.True;
            ((PieSeriesLabel)seri.Label).TextPattern = "{A}: {VP:p0} ({V:n0})";

            chartMiktar.Series.Add(seri);
            chartMiktar.Titles.Clear();
            chartMiktar.Titles.Add(new ChartTitle { Text = "Top 20 Stok - Satış Miktarı" });
        }

        private void CiroTop10Ciz()
        {
            if (chartCiro == null || stokOzetTablosu == null)
            {
                return;
            }

            chartCiro.Series.Clear();

            var data = stokOzetTablosu.AsEnumerable()
                .OrderByDescending(r => r.Field<decimal>("CiroToplami"))
                .Where(r => r.Field<decimal>("CiroToplami") > 0)
                .Take(10)
                .Select(r => new
                {
                    StokKodu = r.Field<string>("StokKodu"),
                    Ciro = r.Field<decimal>("CiroToplami")
                })
                .ToList();

            Series seri = new Series("Ciro", ViewType.Bar)
            {
                DataSource = data,
                ArgumentDataMember = "StokKodu"
            };
            seri.ValueDataMembers.AddRange("Ciro");
            seri.LabelsVisibility = DefaultBoolean.True;
            ((SideBySideBarSeriesLabel)seri.Label).TextPattern = "{V:n0}";

            chartCiro.Series.Add(seri);
            chartCiro.Titles.Clear();
            chartCiro.Titles.Add(new ChartTitle { Text = "Top 10 Stok - Satış Cirosu (Birim Fiyat x Miktar)" });
        }
    }
}
