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
    public partial class Leasing : UserControl
    {
        public Leasing()
        {
            InitializeComponent();
            LoadLeasingData();
            ConfigureGridColumns();
        }

        private void LoadLeasingData()
        {
            try
            {
                // CF_leasingView verilerini SqlHelper'dan al
                DataTable leasingData = SqlHelper.Instance.GetDataTable("CF_leasingView");
                
                if (leasingData != null && leasingData.Rows.Count > 0)
                {
                    // Grid'e veriyi yükle
                    gridControl1.DataSource = leasingData;
                    
                    // Grid ayarlarını güncelle
                    ConfigureGridColumns();
                }
                else
                {
                    MessageBox.Show("CF_leasingView verisi bulunamadı. Lütfen veritabanı bağlantısını kontrol edin.", 
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
                        case "LEASINGKODU":
                        case "FIRMAKODU":
                        case "KOD":
                            column.Caption = "Leasing Kodu";
                            column.Width = 100;
                            break;
                        case "LEASINGADI":
                        case "FIRMAADI":
                        case "ADI":
                        case "AD":
                            column.Caption = "Leasing Adı";
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
            LoadLeasingData();
        }
    }
}
