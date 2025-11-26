-- =============================================
-- DllFinance SQL Views
-- OzetDashboard için optimize edilmiş view'lar
-- =============================================

-- 1. ALIŞ FATURA VIEW
CREATE VIEW vw_AlisFaturalari AS
SELECT 
    f.FaturaNo,
    f.FaturaTarihi,
    f.VadeTarihi,
    f.TedarikciKodu,
    t.TedarikciAdi,
    f.ToplamTutar,
    f.KDVToplami,
    f.GenelToplam,
    f.ParaBirimi,
    f.Durum,
    f.Aciklama,
    f.KayitTarihi,
    f.KaydedenKullanici,
    -- Hesaplanan alanlar
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    CASE 
        WHEN f.VadeTarihi < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN f.VadeTarihi <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu,
    -- Filtreleme için
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Tedarikciler t ON f.TedarikciKodu = t.TedarikciKodu
WHERE f.FaturaTipi = 'ALIS' 
  AND f.Durum = 'AKTIF';

-- 2. SATIŞ FATURA VIEW
CREATE VIEW vw_SatisFaturalari AS
SELECT 
    f.FaturaNo,
    f.FaturaTarihi,
    f.VadeTarihi,
    f.MusteriKodu,
    m.MusteriAdi,
    f.ToplamTutar,
    f.KDVToplami,
    f.GenelToplam,
    f.ParaBirimi,
    f.Durum,
    f.Aciklama,
    f.KayitTarihi,
    f.KaydedenKullanici,
    -- Hesaplanan alanlar
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    CASE 
        WHEN f.VadeTarihi < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN f.VadeTarihi <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu,
    -- Filtreleme için
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Musteriler m ON f.MusteriKodu = m.MusteriKodu
WHERE f.FaturaTipi = 'SATIS' 
  AND f.Durum = 'AKTIF';

-- 3. NAKİT AKIŞ VIEW
CREATE VIEW vw_NakitAkis AS
SELECT 
    'GİRİŞ' AS AkisTipi,
    f.FaturaNo AS EvrakNo,
    f.FaturaTarihi AS Tarih,
    f.VadeTarihi,
    f.MusteriKodu AS FirmaKodu,
    m.MusteriAdi AS FirmaAdi,
    f.GenelToplam AS Tutar,
    f.ParaBirimi,
    'CARİ TAHSİLAT' AS Cins,
    f.Aciklama,
    'BAKİYE' AS Tip,
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Musteriler m ON f.MusteriKodu = m.MusteriKodu
WHERE f.FaturaTipi = 'SATIS' 
  AND f.Durum = 'AKTIF'

UNION ALL

SELECT 
    'ÇIKIŞ' AS AkisTipi,
    f.FaturaNo AS EvrakNo,
    f.FaturaTarihi AS Tarih,
    f.VadeTarihi,
    f.TedarikciKodu AS FirmaKodu,
    t.TedarikciAdi AS FirmaAdi,
    f.GenelToplam AS Tutar,
    f.ParaBirimi,
    CASE 
        WHEN f.Aciklama LIKE '%VERGİ%' THEN 'VERGİ'
        ELSE 'CARİ ÖDEME'
    END AS Cins,
    f.Aciklama,
    'BAKİYE' AS Tip,
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Tedarikciler t ON f.TedarikciKodu = t.TedarikciKodu
WHERE f.FaturaTipi = 'ALIS' 
  AND f.Durum = 'AKTIF';

-- 4. VADESİ GELEN BORÇLAR VIEW
CREATE VIEW vw_VadesiGelenBorclar AS
SELECT 
    f.FaturaNo,
    f.FaturaTarihi,
    f.VadeTarihi,
    f.TedarikciKodu,
    t.TedarikciAdi,
    f.GenelToplam AS BorcTutari,
    f.ParaBirimi,
    f.Aciklama,
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    CASE 
        WHEN f.VadeTarihi < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN f.VadeTarihi <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu,
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Tedarikciler t ON f.TedarikciKodu = t.TedarikciKodu
WHERE f.FaturaTipi = 'ALIS' 
  AND f.Durum = 'AKTIF'
  AND f.VadeTarihi <= DATEADD(day, 30, GETDATE());

-- 5. VADESİ GELEN ALACAKLAR VIEW
CREATE VIEW vw_VadesiGelenAlacaklar AS
SELECT 
    f.FaturaNo,
    f.FaturaTarihi,
    f.VadeTarihi,
    f.MusteriKodu,
    m.MusteriAdi,
    f.GenelToplam AS AlacakTutari,
    f.ParaBirimi,
    f.Aciklama,
    DATEDIFF(day, GETDATE(), f.VadeTarihi) AS KalanGun,
    CASE 
        WHEN f.VadeTarihi < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN f.VadeTarihi <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu,
    YEAR(f.FaturaTarihi) AS Yil,
    MONTH(f.FaturaTarihi) AS Ay,
    DAY(f.FaturaTarihi) AS Gun
FROM Faturalar f
INNER JOIN Musteriler m ON f.MusteriKodu = m.MusteriKodu
WHERE f.FaturaTipi = 'SATIS' 
  AND f.Durum = 'AKTIF'
  AND f.VadeTarihi <= DATEADD(day, 30, GETDATE());

-- 6. ÇEK SENET VIEW
CREATE VIEW vw_CekSenet AS
SELECT 
    cs.CekSenetNo,
    cs.Tarih,
    cs.VadeTarihi,
    cs.FirmaKodu,
    cs.FirmaAdi,
    cs.Tutar,
    cs.ParaBirimi,
    cs.Tip, -- VERILEN, ALINAN, PORTFOY, RISK
    cs.Durum,
    cs.Aciklama,
    DATEDIFF(day, GETDATE(), cs.VadeTarihi) AS KalanGun,
    CASE 
        WHEN cs.VadeTarihi < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN cs.VadeTarihi <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu,
    YEAR(cs.Tarih) AS Yil,
    MONTH(cs.Tarih) AS Ay,
    DAY(cs.Tarih) AS Gun
FROM CekSenet cs
WHERE cs.Durum = 'AKTIF';

-- 7. KASA İŞLEMLERİ VIEW
CREATE VIEW vw_KasaIslemleri AS
SELECT 
    ki.IslemNo,
    ki.Tarih,
    ki.Aciklama,
    ki.Tutar,
    ki.ParaBirimi,
    ki.IslemTipi, -- GİRİŞ, ÇIKIŞ
    ki.KasaKodu,
    k.KasaAdi,
    ki.KayitTarihi,
    ki.KaydedenKullanici,
    YEAR(ki.Tarih) AS Yil,
    MONTH(ki.Tarih) AS Ay,
    DAY(ki.Tarih) AS Gun
FROM KasaIslemleri ki
INNER JOIN Kasalar k ON ki.KasaKodu = k.KasaKodu
WHERE ki.Durum = 'AKTIF';

-- 8. BANKA İŞLEMLERİ VIEW
CREATE VIEW vw_BankaIslemleri AS
SELECT 
    bi.IslemNo,
    bi.Tarih,
    bi.Aciklama,
    bi.Tutar,
    bi.ParaBirimi,
    bi.IslemTipi, -- GİRİŞ, ÇIKIŞ
    bi.BankaKodu,
    b.BankaAdi,
    bi.HesapNo,
    bi.KayitTarihi,
    bi.KaydedenKullanici,
    YEAR(bi.Tarih) AS Yil,
    MONTH(bi.Tarih) AS Ay,
    DAY(bi.Tarih) AS Gun
FROM BankaIslemleri bi
INNER JOIN Bankalar b ON bi.BankaKodu = b.BankaKodu
WHERE bi.Durum = 'AKTIF';

-- 9. PERİYODİK GİDERLER VIEW
CREATE VIEW vw_PeriyodikGiderler AS
SELECT 
    pg.GiderKodu,
    pg.GiderAdi,
    pg.Tutar,
    pg.ParaBirimi,
    pg.Periyot, -- AYLIK, YILLIK, HAFTALIK
    pg.BaslangicTarihi,
    pg.BitisTarihi,
    pg.SonOdemeTarihi,
    pg.Durum,
    pg.Aciklama,
    DATEDIFF(day, GETDATE(), pg.SonOdemeTarihi) AS KalanGun,
    YEAR(pg.BaslangicTarihi) AS Yil,
    MONTH(pg.BaslangicTarihi) AS Ay,
    DAY(pg.BaslangicTarihi) AS Gun
FROM PeriyodikGiderler pg
WHERE pg.Durum = 'AKTIF';

-- 10. PERİYODİK GELİRLER VIEW
CREATE VIEW vw_PeriyodikGelirler AS
SELECT 
    pg.GelirKodu,
    pg.GelirAdi,
    pg.Tutar,
    pg.ParaBirimi,
    pg.Periyot, -- AYLIK, YILLIK, HAFTALIK
    pg.BaslangicTarihi,
    pg.BitisTarihi,
    pg.SonTahsilatTarihi,
    pg.Durum,
    pg.Aciklama,
    DATEDIFF(day, GETDATE(), pg.SonTahsilatTarihi) AS KalanGun,
    YEAR(pg.BaslangicTarihi) AS Yil,
    MONTH(pg.BaslangicTarihi) AS Ay,
    DAY(pg.BaslangicTarihi) AS Gun
FROM PeriyodikGelirler pg
WHERE pg.Durum = 'AKTIF';

-- 11. ÖZET DASHBOARD VIEW (Ana Dashboard için)
CREATE VIEW vw_OzetDashboard AS
SELECT 
    'ALIŞ' AS Tip,
    COUNT(*) AS KayitSayisi,
    SUM(GenelToplam) AS ToplamTutar,
    SUM(CASE WHEN VadeTarihi < GETDATE() THEN GenelToplam ELSE 0 END) AS VadesiGecmisTutar,
    SUM(CASE WHEN VadeTarihi <= DATEADD(day, 30, GETDATE()) AND VadeTarihi >= GETDATE() THEN GenelToplam ELSE 0 END) AS VadesiGelenTutar,
    YEAR(FaturaTarihi) AS Yil,
    MONTH(FaturaTarihi) AS Ay
FROM vw_AlisFaturalari
GROUP BY YEAR(FaturaTarihi), MONTH(FaturaTarihi)

UNION ALL

SELECT 
    'SATIŞ' AS Tip,
    COUNT(*) AS KayitSayisi,
    SUM(GenelToplam) AS ToplamTutar,
    SUM(CASE WHEN VadeTarihi < GETDATE() THEN GenelToplam ELSE 0 END) AS VadesiGecmisTutar,
    SUM(CASE WHEN VadeTarihi <= DATEADD(day, 30, GETDATE()) AND VadeTarihi >= GETDATE() THEN GenelToplam ELSE 0 END) AS VadesiGelenTutar,
    YEAR(FaturaTarihi) AS Yil,
    MONTH(FaturaTarihi) AS Ay
FROM vw_SatisFaturalari
GROUP BY YEAR(FaturaTarihi), MONTH(FaturaTarihi);

-- 12. FİLTRELEME İÇİN HELPER VIEW
CREATE VIEW vw_FiltrelemeVerileri AS
SELECT 
    DISTINCT
    Yil,
    Ay,
    Cins,
    Tip,
    ParaBirimi,
    VadeDurumu
FROM (
    SELECT Yil, Ay, 'CARİ ÖDEME' AS Cins, 'BAKİYE' AS Tip, ParaBirimi, VadeDurumu FROM vw_AlisFaturalari
    UNION ALL
    SELECT Yil, Ay, 'VERGİ' AS Cins, 'BAKİYE' AS Tip, ParaBirimi, VadeDurumu FROM vw_AlisFaturalari WHERE Aciklama LIKE '%VERGİ%'
    UNION ALL
    SELECT Yil, Ay, 'CARİ TAHSİLAT' AS Cins, 'BAKİYE' AS Tip, ParaBirimi, VadeDurumu FROM vw_SatisFaturalari
) AS FiltrelemeVerileri;

-- INDEX'LER (Performans için)
CREATE INDEX IX_vw_AlisFaturalari_Tarih ON vw_AlisFaturalari (FaturaTarihi, VadeTarihi);
CREATE INDEX IX_vw_SatisFaturalari_Tarih ON vw_SatisFaturalari (FaturaTarihi, VadeTarihi);
CREATE INDEX IX_vw_NakitAkis_Tarih ON vw_NakitAkis (Tarih, VadeTarihi);
CREATE INDEX IX_vw_CekSenet_Tarih ON vw_CekSenet (Tarih, VadeTarihi);















