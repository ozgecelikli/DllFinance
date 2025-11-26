using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DllFinance.SQL;

namespace DllFinance.DLLFinance.FinansOdeme
{
    public partial class BankaKredileri : UserControl
    {
        public BankaKredileri()
        {
            InitializeComponent();
            LoadKrediData();
            ConfigureGridColumns();
        }

        private void LoadKrediData()
        {
            try
            {
                // CF_krediView verilerini SqlHelper'dan al
                DataTable krediData = SqlHelper.Instance.GetDataTable("CF_krediView");
                
                if (krediData != null && krediData.Rows.Count > 0)
                {
                    // Grid'e veriyi yükle
                    gridControl1.DataSource = krediData;
                    
                    // Grid ayarlarını güncelle
                    ConfigureGridColumns();
                }
                else
                {
                    MessageBox.Show("CF_krediView verisi bulunamadı. Lütfen veritabanı bağlantısını kontrol edin.", 
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            try
            {
                GridView view = gridView1;
                
                // Grid görünüm ayarları
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ShowAutoFilterRow = true;
                view.OptionsView.ShowFooter = true;
                // Çoklu seçim özelliği kaldırıldı
                // view.OptionsSelection.MultiSelect = true;
                // view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                
                // Kolonları gizle (varsayılan olarak tüm kolonlar görünür)
                view.BestFitColumns();
                
                // Önce tüm kolonları göster ve başlıklarını ayarla
                foreach (GridColumn column in view.Columns)
                {
                    // Tüm kolonları görünür yap
                    column.Visible = true;
                    
                    switch (column.FieldName.ToUpper())
                    {
                        case "YIL":
                            column.Caption = "Yıl";
                            column.Width = 60;
                            break;
                        case "AY":
                            column.Caption = "Ay";
                            column.Width = 50;
                            break;
                        case "GUN":
                        case "GÜN":
                            column.Caption = "Gün";
                            column.Width = 50;
                            break;
                        case "TARIH":
                        case "TARİH":
                            column.Caption = "Tarih";
                            column.Width = 100;
                            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            column.DisplayFormat.FormatString = "dd.MM.yyyy";
                            break;
                        case "BANKAKODU":
                        case "FIRMAKODU":
                        case "KOD":
                            column.Caption = "Banka Kodu";
                            column.Width = 100;
                            break;
                        case "BANKAADI":
                        case "FIRMAADI":
                        case "ADI":
                        case "AD":
                            column.Caption = "Banka Adı";
                            column.Width = 150;
                            break;
                        case "TUTAR":
                        case "MİKTAR":
                        case "MIKTAR":
                        case "BORC":
                        case "BORÇ":
                            column.Caption = "Tutar";
                            column.Width = 120;
                            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            column.DisplayFormat.FormatString = "N2";
                            break;
                        default:
                            // Diğer kolonlar için orijinal ismi kullan
                            column.Caption = column.FieldName;
                            column.Width = 100;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid kolonları yapılandırılırken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData()
        {
            LoadKrediData();
        }
    }
}
