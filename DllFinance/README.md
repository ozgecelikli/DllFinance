# DllFinance Çek ve Senet ekranını görüntüleme

Bu proje Windows üzerinde DevExpress bileşenleriyle çalışan bir WinForms uygulamasıdır. Aşağıdaki adımlar, son eklenen Çek ve Senet ekranını uygulama içinde nasıl görebileceğinizi özetler.

## Gerekli ortam
- Windows üzerinde Visual Studio (DevExpress WinForms bileşenleri yüklü olmalı).
- Uygulamanın bağlanacağı SQL Server erişimi. `Program.cs` içindeki `ip`, `databaseName`, `username`, `password` değerlerini kendi ortamınıza göre düzenleyin.

## Çalıştırma ve ekranı görme
1. `DllFinance.sln` dosyasını Visual Studio ile açın.
2. Çözüm açıldığında `Program.cs` dosyasındaki veritabanı bağlantı bilgilerini kontrol edin ve gerekirse güncelleyin.
3. Çözümü **Build** edin ve ardından `MainView` açılacak şekilde **Start (F5)** ile çalıştırın.
4. Sol taraftaki akordeon menüsünde **Finansal Yükümlülükler ve Ödemeler** başlığını genişletin.
5. **Çek ve Senet** öğesine tıklayın. Ekranda:
   - Üstte, vade tarihine göre **YIL/AY** kırılımında toplanmış tutarların olduğu grup gridini,
   - Altta tüm kayıtların listelendiği detay gridini
   görebilirsiniz.
6. Veriler `CF_ceksenetView` görünümünden geldiği için tarih ve tutar kolonlarında otomatik formatlama ve TL toplam özetleri yer alır. Gerekirse otomatik filtre satırıyla veri arayabilirsiniz.

Bu adımları izleyerek yapılan değişiklikleri uygulama içinde gözlemleyebilirsiniz.
