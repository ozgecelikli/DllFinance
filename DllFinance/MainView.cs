using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace DllFinance
{ 
    public partial class MainView : DevExpress.XtraEditors.XtraForm
    {
        public MainView()
        {
            InitializeComponent();
            InitializeControlMappings();
            AssignAccordionClickEvents();
        }
        
        private Dictionary<AccordionControlElement, Func<UserControl>> controlMappings;

        private void InitializeControlMappings()
        {
            controlMappings = new Dictionary<AccordionControlElement, Func<UserControl>>
            {
                { ace_ozet_alissatis, () => new DLLFinance.Ozet.OzetAlisSatis() },
                { ace_ozet_nakitakis, () => new DLLFinance.Ozet.OzetNakitAkis() },
                { ace_ozet_dashboard, () => new DLLFinance.Ozet.OzetDashboard() },

                { ace_faturayonetimi_alis_detay, () => new DLLFinance.FaturaYonetimi.Alis.AlisFaturaDetaylari() },
                { ace_faturayonetimi_alis_cari, () => new DLLFinance.FaturaYonetimi.Alis.AlisCariRapor() },
                { ace_faturayonetimi_alis_urun, () => new DLLFinance.FaturaYonetimi.Alis.AlisUrunRaporu() },

                { ace_faturayonetimi_satis_detay, () => new DLLFinance.FaturaYonetimi.Satis.SatisFaturaDetaylari() },
                { ace_faturayonetimi_satis_cari,  () => new DLLFinance.FaturaYonetimi.Satis.SatisCariRapor() },
                { ace_faturayonetimi_satis_urun,  () => new DLLFinance.FaturaYonetimi.Satis.SatisUrunRaporu() },

                { ace_faturayonetimi_alissatis,  () => new DLLFinance.FaturaYonetimi.AlisSatisKarsilastirma() },

                { ace_faturayonetimi_finans_ceksenet, () => new DLLFinance.FinansOdeme.CekVeSenet() },
                { ace_faturayonetimi_finans_kredi, () => new DLLFinance.FinansOdeme.BankaKredileri() },
                { ace_faturayonetimi_finans_leasing, () => new DLLFinance.FinansOdeme.Leasing() },
                { ace_faturayonetimi_banka, () => new DLLFinance.FinansOdeme.BankaOdemeTahsilat() },
                { ace_faturayonetimi_kasa, () => new DLLFinance.FinansOdeme.KasaOdemeTahsilat() },
                { ace_faturayonetimi_kredikartlari, () => new DLLFinance.FinansOdeme.KrediKartlari() }
            };
        }

        private void AssignAccordionClickEvents()
        {
            foreach (var element in controlMappings.Keys)
            {
                element.Click += AccordionElement_Click;
            }
        }

 
        private void AccordionElement_Click(object sender, EventArgs e)
        {
            try
            {
                var clickedElement = sender as AccordionControlElement;
                if (clickedElement == null || !controlMappings.ContainsKey(clickedElement)) return;

                string formKey = clickedElement.Text;

                // Mevcut formu kontrol et - açık MDI child formlarını ara
                Form existingForm = this.MdiChildren.FirstOrDefault(f => f.Text == formKey);

                if (existingForm != null)
                {
                    // Form zaten varsa aktif et
                    existingForm.Activate();
                }
                else
                {
                    // 1. User Control'ü oluştur
                    var userControl = controlMappings[clickedElement]();

                    // 2. Yeni bir Form oluştur
                    Form childForm = new Form();
                    childForm.Text = formKey;
                    childForm.Name = "form_" + formKey.Replace(" ", "_");
                    childForm.MdiParent = this;

                    // 3. User Control'ü Form içine yerleştir
                    userControl.Dock = DockStyle.Fill;
                    childForm.Controls.Add(userControl);

                    // 4. Formu göster
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {

        }
    }
}
