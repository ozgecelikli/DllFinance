-- =============================================
-- Vadesi Gelen Borç/Alacak View'ları
-- Orijinal prosedürlerden çevrilmiş View'lar
-- =============================================

-- 1. VADESİ GELEN ALACAKLAR MİZAN VIEW
-- hg_vga_alacaklar_firmamizani prosedüründen çevrilmiş
CREATE VIEW vw_VadesiGelenAlacaklarMizan AS
WITH x AS (
    SELECT 
        d1.firmakodu,
        d1.hesapkodu,
        borc = SUM(borc),
        alacak = SUM(alacak),
        dborc = 0,
        dalacak = 0
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = 'AYAZKAUCUK' -- Varsayılan şirket kodu
      AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
      AND d1.evraktarihi >= '20170101' -- Başlangıç tarihi
      AND d1.evraktarihi <= '20991231' -- Bitiş tarihi
      AND LEFT(d1.hesapkodu, 3) = '120' -- Alacak hesapları
    GROUP BY d1.firmakodu, d1.hesapkodu
    
    UNION ALL
    
    SELECT 
        d1.firmakodu,
        d1.hesapkodu,
        borc = 0,
        alacak = 0,
        dborc = SUM(d1.dborc),
        dalacak = SUM(d1.dalacak)
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = 'AYAZKAUCUK'
      AND d1.evraktarihi >= '20170101'
      AND d1.evraktarihi <= '20991231'
      AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
      AND LEFT(d1.hesapkodu, 3) = '120'
      AND dbo.hg_parabirimiuygunmu_tutar(d1.para, dborc + dalacak) = 1
    GROUP BY d1.firmakodu, d1.hesapkodu
)
SELECT 
    d1.firmakodu,
    fb.firmaadi,
    guncel_obs = dbo.hg_firmakodlari_obs('AYAZKAUCUK', d1.firmakodu),
    d1.hesapkodu,
    bbakiye = SUM(d1.borc) - SUM(d1.alacak),
    borc = SUM(d1.borc),
    alacak = SUM(d1.alacak),
    dborc = SUM(d1.dborc),
    dalacak = SUM(d1.dalacak),
    dbbakiye = SUM(d1.dborc) - SUM(d1.dalacak),
    vg_bbakiyetakipsekli = MAX(fb.vg_bbakiyetakipsekli),
    vg_abakiyetakipsekli = MAX(fb.vg_abakiyetakipsekli),
    ch_bakiyesi = SUM(d1.borc) - SUM(d1.alacak)
FROM x d1
INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = 'AYAZKAUCUK' 
    AND fb.aktifpasif = 'A' 
    AND fb.firmakodu = d1.firmakodu
GROUP BY d1.firmakodu, fb.firmaadi, d1.hesapkodu
ORDER BY d1.firmakodu, fb.firmaadi;

-- 2. VADESİ GELEN ALACAKLAR DETAY VIEW
-- hg_vga_alacaklar_firmadetayi prosedüründen çevrilmiş
CREATE VIEW vw_VadesiGelenAlacaklarDetay AS
SELECT 
    d1.firmakodu,
    d1.mahsupno,
    d1.evrakturu,
    d1.evrakno,
    vade_d = CONVERT(NVARCHAR(10), d1.odemevadesi_d, 104),
    evraktarihi_d = CONVERT(NVARCHAR(10), d1.evraktarihi_d, 104),
    d1.odemesuresi,
    d1.hesapkodu,
    evraktarihi,
    borc = d1.borc,
    alacak = d1.alacak,
    vade = CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112),
    kalantutar = d1.alacak,
    toplam = 0,
    d1.dborc,
    d1.dalacak,
    d1.para,
    d1.bimno,
    mbd_bimno = d1.ozet_bimno,
    -- Filtreleme için
    YEAR(d1.evraktarihi_d) AS Yil,
    MONTH(d1.evraktarihi_d) AS Ay,
    DAY(d1.evraktarihi_d) AS Gun,
    DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
    CASE 
        WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu
FROM mahsupbilgileridetay d1 WITH (NOLOCK)
WHERE d1.aktifpasif = 'A' 
  AND d1.sirketkodu = 'AYAZKAUCUK'
  AND d1.evraktarihi >= '20150101'
  AND d1.evraktarihi <= '20151231'
  AND LEFT(d1.hesapkodu, 3) = '120'
  AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
ORDER BY d1.firmakodu, CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112);

-- 3. VADESİ GELEN BORÇLAR MİZAN VIEW
-- hg_vga_borclar_firmamizani prosedüründen çevrilmiş
CREATE VIEW vw_VadesiGelenBorclarMizan AS
WITH x AS (
    SELECT 
        d1.firmakodu,
        d1.hesapkodu,
        bbakiye = SUM(d1.borc) - SUM(d1.alacak),
        borc = SUM(borc),
        alacak = SUM(alacak),
        dborc = SUM(dborc),
        dalacak = SUM(dalacak)
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = 'KOTA' -- Varsayılan şirket kodu
      AND d1.evraktarihi >= '20100101'
      AND d1.evraktarihi <= '20991299'
      AND LEFT(d1.hesapkodu, 3) = '320' -- Borç hesapları
    GROUP BY d1.firmakodu, d1.hesapkodu
)
SELECT 
    d1.firmakodu,
    fb.firmaadi,
    guncel_obs = dbo.hg_firmakodlari_obs('KOTA', d1.firmakodu),
    d1.hesapkodu,
    d1.bbakiye,
    d1.borc,
    d1.alacak,
    d1.dborc,
    d1.dalacak,
    fb.vg_bbakiyetakipsekli,
    fb.vg_abakiyetakipsekli,
    test = 'X'
FROM x d1
INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.aktifpasif = 'A' 
    AND fb.firmakodu = d1.firmakodu
INNER JOIN sirketbilgileri sb WITH (NOLOCK) ON sb.sirketkodu = 'KOTA' 
    AND sb.sirketkodu_firmakodu = fb.sirketkodu
ORDER BY d1.firmakodu, fb.firmaadi;

-- 4. VADESİ GELEN BORÇLAR DETAY VIEW
-- hg_vga_borclar_firmadetayi prosedüründen çevrilmiş
CREATE VIEW vw_VadesiGelenBorclarDetay AS
SELECT 
    d1.firmakodu,
    d1.mahsupno,
    d1.evrakturu,
    d1.evrakno,
    vade_d = CONVERT(NVARCHAR(10), d1.odemevadesi_d, 104),
    evraktarihi_d = CONVERT(NVARCHAR(10), d1.evraktarihi_d, 104),
    d1.odemesuresi,
    d1.hesapkodu,
    evraktarihi,
    borc = d1.borc,
    alacak = d1.alacak,
    vade = CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112),
    kalantutar = d1.alacak,
    toplam = 0,
    d1.dborc,
    d1.dalacak,
    d1.para,
    d1.bimno,
    mbd_bimno = d1.ozet_bimno,
    plasiyer = COALESCE((
        SELECT TOP 1 di1.siciladi 
        FROM firmakodlari di1 WITH(NOLOCK) 
        WHERE di1.sirketkodu = 'KOCEL' 
          AND di1.aktifpasif = 'A'
          AND di1.firmakodu = d1.firmakodu
    ), ''),
    -- Filtreleme için
    YEAR(d1.evraktarihi_d) AS Yil,
    MONTH(d1.evraktarihi_d) AS Ay,
    DAY(d1.evraktarihi_d) AS Gun,
    DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
    CASE 
        WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu
FROM mahsupbilgileridetay d1 WITH (NOLOCK)
WHERE d1.aktifpasif = 'A' 
  AND d1.sirketkodu = 'KOCEL'
  AND d1.evraktarihi >= '20150101'
  AND d1.evraktarihi <= '20151231'
  AND LEFT(d1.hesapkodu, 3) = '320'
  AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
ORDER BY d1.firmakodu, CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112);

-- 5. VADESİ GELEN ÖZET DETAY VIEW
-- hg_erpis_vadesigelenbadetay_getir_bimno_detay prosedüründen çevrilmiş
CREATE VIEW vw_VadesiGelenOzetDetay AS
SELECT 
    d1.firmakodu,
    fb.firmaadi,
    vadesigecmis = dbo.hg_vadesigesmistutar(d2.vadesigecmis, d2.vadesigelen, d1.vade, d2.maxvade, d1.borc),
    vadesigelen = dbo.hg_vadesigelentutar(d2.vadesigecmis, d2.vadesigelen, d1.vade, d2.maxvade, d1.borc),
    odenmesigereken = dbo.hg_vadesigesmistutar(d2.vadesigecmis, d2.vadesigelen, d1.vade, d2.maxvade, d1.borc) +
                     dbo.hg_vadesigelentutar(d2.vadesigecmis, d2.vadesigelen, d1.vade, d2.maxvade, d1.borc),
    d1.borc,
    evraktarihi_d = d1.evraktarihi_d,
    odemesuresi = DATEDIFF(DAY, d1.evraktarihi_d, d1.vade_d),
    vggs = DATEDIFF(DAY, d1.vade_d, GETDATE()),
    ft_dovizkuru = dbo.hg_dovizkuru_tipsel(d1.para, d1.evraktarihi, 'ALIS'),
    od_dovizkuru = dbo.hg_dovizkuru_tipsel(d1.para, CONVERT(NVARCHAR(8), GETDATE(), 112), 'ALIS'),
    kurfarki = dbo.hg_vgab_kurfarki(
        dbo.hg_dovizkuru_tipsel(d1.para, CONVERT(NVARCHAR(8), GETDATE(), 112), 'ALIS'), 
        d1.dborc, d1.borc),
    vade_d = d1.vade_d,
    d1.para,
    d1.dborc,
    dvadesigecmis = 0.00,
    dvadesigelen = 0.00,
    dodenmesigereken = 0.00,
    gunsayisi = DATEDIFF(d, d1.vade_d, d2.vadesigecmis_d),
    d1.hesapkodu,
    hesapadi = '',
    d1.toplamborc,
    d1.dtoplamborc,
    d1.mahsupno,
    d1.evrakturu,
    d1.evrakno,
    vade = CONVERT(NVARCHAR(8), d1.vade_d, 112),
    evraktarihi = CONVERT(NVARCHAR(8), d1.evraktarihi_d, 112),
    fb.odemetahsilatsekli,
    yil = LEFT(CONVERT(NVARCHAR(8), d1.vade_d, 112), 4),
    yilay = LEFT(CONVERT(NVARCHAR(8), d1.vade_d, 112), 6),
    yilhafta = dbo.hg_yilhafta(d1.vade_d),
    ort_odemesuresi = d1.odemesuresi,
    kyt_odemesuresi = fb.odemesuresi_tl,
    ievraktarihi_d = CONVERT(NVARCHAR(10), d2.ievraktarihi_d, 104),
    sevraktarihi_d = CONVERT(NVARCHAR(10), d2.sevraktarihi_d, 104),
    vadesigecmis_d = CONVERT(NVARCHAR(10), d2.vadesigecmis_d, 104),
    vadesigelen_d = CONVERT(NVARCHAR(10), d2.vadesigelen_d, 104),
    maxvade_d = CONVERT(NVARCHAR(10), d2.maxvade_d, 104),
    altlimit = d2.bbakiye,
    kayittarihi_d = d2.kayittarihi_d,
    gecengunsayisi = DATEDIFF(DAY, d1.vade_d, GETDATE()),
    kalantutar_tl = borc,
    ch_bakiyesi = 0.0,
    vadesigelmemis = 0.0,
    evraktutari = borc,
    odemetutari_tl = 0.0,
    fb.sicilno,
    siciladi = dbo.hg_personelbilgileri_adi('OZGUR', fb.sicilno),
    siciladigrp = dbo.hg_personelbilgileri_adi('OZGUR', fb.sicilno) + '  ' + fb.sicilno,
    okno = 0,
    -- Filtreleme için
    YEAR(d1.vade_d) AS Yil,
    MONTH(d1.vade_d) AS Ay,
    DAY(d1.vade_d) AS Gun,
    DATEDIFF(day, GETDATE(), d1.vade_d) AS KalanGun,
    CASE 
        WHEN d1.vade_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN d1.vade_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu
FROM vadesigelenbadetay d1 WITH (NOLOCK)
INNER JOIN vadesigelenbaozet d2 WITH (NOLOCK) ON d2.bimno = d1.ozet_bimno
INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.aktifpasif = 'A' 
    AND fb.sirketkodu = d2.sirketkodu 
    AND fb.firmakodu = d1.firmakodu
WHERE d1.toplamborc <> 0
ORDER BY fb.firmaadi, 
         SUBSTRING(CONVERT(NVARCHAR(10), vade_d, 104), 7, 4) + 
         SUBSTRING(CONVERT(NVARCHAR(10), d1.vade_d, 104), 4, 2) + 
         SUBSTRING(CONVERT(NVARCHAR(10), d1.vade_d, 104), 1, 2);

-- 6. BİRLEŞİK NAKİT AKIŞ VIEW (Ana Dashboard için)
CREATE VIEW vw_NakitAkisBirlesik AS
-- Alacaklar (Giriş)
SELECT 
    'GİRİŞ' AS AkisTipi,
    'ALACAK' AS Tip,
    d1.firmakodu AS FirmaKodu,
    fb.firmaadi AS FirmaAdi,
    d1.hesapkodu,
    d1.mahsupno AS EvrakNo,
    d1.evrakturu,
    d1.evraktarihi_d AS Tarih,
    d1.odemevadesi_d AS VadeTarihi,
    d1.alacak AS Tutar,
    d1.para AS ParaBirimi,
    'CARİ TAHSİLAT' AS Cins,
    d1.evrakturu AS Aciklama,
    'BAKİYE' AS Durum,
    DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
    YEAR(d1.evraktarihi_d) AS Yil,
    MONTH(d1.evraktarihi_d) AS Ay,
    DAY(d1.evraktarihi_d) AS Gun,
    CASE 
        WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu
FROM mahsupbilgileridetay d1 WITH (NOLOCK)
INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = d1.sirketkodu 
    AND fb.firmakodu = d1.firmakodu
WHERE d1.aktifpasif = 'A' 
  AND LEFT(d1.hesapkodu, 3) = '120' -- Alacak hesapları
  AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
  AND d1.alacak > 0

UNION ALL

-- Borçlar (Çıkış)
SELECT 
    'ÇIKIŞ' AS AkisTipi,
    'BORÇ' AS Tip,
    d1.firmakodu AS FirmaKodu,
    fb.firmaadi AS FirmaAdi,
    d1.hesapkodu,
    d1.mahsupno AS EvrakNo,
    d1.evrakturu,
    d1.evraktarihi_d AS Tarih,
    d1.odemevadesi_d AS VadeTarihi,
    d1.borc AS Tutar,
    d1.para AS ParaBirimi,
    CASE 
        WHEN d1.evrakturu LIKE '%VERGİ%' THEN 'VERGİ'
        ELSE 'CARİ ÖDEME'
    END AS Cins,
    d1.evrakturu AS Aciklama,
    'BAKİYE' AS Durum,
    DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
    YEAR(d1.evraktarihi_d) AS Yil,
    MONTH(d1.evraktarihi_d) AS Ay,
    DAY(d1.evraktarihi_d) AS Gun,
    CASE 
        WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
        WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
        ELSE 'NORMAL'
    END AS VadeDurumu
FROM mahsupbilgileridetay d1 WITH (NOLOCK)
INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = d1.sirketkodu 
    AND fb.firmakodu = d1.firmakodu
WHERE d1.aktifpasif = 'A' 
  AND LEFT(d1.hesapkodu, 3) = '320' -- Borç hesapları
  AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
  AND d1.borc > 0;

-- INDEX'LER (Performans için)
CREATE INDEX IX_vw_VadesiGelenAlacaklarMizan_Firma ON vw_VadesiGelenAlacaklarMizan (firmakodu, hesapkodu);
CREATE INDEX IX_vw_VadesiGelenBorclarMizan_Firma ON vw_VadesiGelenBorclarMizan (firmakodu, hesapkodu);
CREATE INDEX IX_vw_VadesiGelenAlacaklarDetay_Tarih ON vw_VadesiGelenAlacaklarDetay (Yil, Ay, Gun);
CREATE INDEX IX_vw_VadesiGelenBorclarDetay_Tarih ON vw_VadesiGelenBorclarDetay (Yil, Ay, Gun);
CREATE INDEX IX_vw_NakitAkisBirlesik_Tarih ON vw_NakitAkisBirlesik (Yil, Ay, Gun, VadeDurumu);
CREATE INDEX IX_vw_VadesiGelenOzetDetay_Tarih ON vw_VadesiGelenOzetDetay (Yil, Ay, Gun);















