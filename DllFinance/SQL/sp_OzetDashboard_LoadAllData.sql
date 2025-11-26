-- =============================================
-- Tek Prosedür - Tüm Vadesi Gelen Veriler
-- Doğru mantık: Önce mizan, sonra detaylar ve kalan tutarlar
-- =============================================

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[sp_OzetDashboard_LoadAllData]
    @sirketkodu NVARCHAR(30) = 'AYAZKAUCUK',
    @ievraktarihi NVARCHAR(8) = '20170101',
    @sevraktarihi NVARCHAR(8) = '20991231',
    @maxvade NVARCHAR(8) = '20991231',
    @vadesigecmis NVARCHAR(8) = '20170101',
    @vadesigelen NVARCHAR(8) = '20991231',
    @bbakiye NUMERIC(15,2) = 0,
    @islemtipi NVARCHAR(3) = 'FKO',
    @haricmahsup NVARCHAR(100) = '',
    @sicilno NVARCHAR(30) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 1. ALACAKLAR MİZANI (Firma bazında özet)
    WITH AlacaklarMizan AS (
        SELECT 
            d1.firmakodu,
            d1.hesapkodu,
            SUM(d1.borc) AS toplam_borc,
            SUM(d1.alacak) AS toplam_alacak,
            SUM(d1.dborc) AS toplam_dborc,
            SUM(d1.dalacak) AS toplam_dalacak,
            SUM(d1.borc) - SUM(d1.alacak) AS net_bakiye
        FROM mahsupbilgileridetay d1 WITH (NOLOCK)
        WHERE d1.aktifpasif = 'A' 
          AND d1.sirketkodu = @sirketkodu 
          AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
          AND d1.evraktarihi >= @ievraktarihi 
          AND d1.evraktarihi <= @sevraktarihi
          AND LEFT(d1.hesapkodu, 3) = '120' -- Alacak hesapları
        GROUP BY d1.firmakodu, d1.hesapkodu
        HAVING SUM(d1.borc) - SUM(d1.alacak) > @bbakiye -- Minimum bakiye filtresi
    )
    SELECT 
        'ALACAK_MIZAN' AS RaporTipi,
        am.firmakodu,
        fb.firmaadi,
        dbo.hg_firmakodlari_obs(@sirketkodu, am.firmakodu) AS guncel_obs,
        am.hesapkodu,
        am.net_bakiye AS bbakiye,
        am.toplam_borc AS borc,
        am.toplam_alacak AS alacak,
        am.toplam_dborc AS dborc,
        am.toplam_dalacak AS dalacak,
        am.toplam_dborc - am.toplam_dalacak AS dbbakiye,
        fb.vg_bbakiyetakipsekli,
        fb.vg_abakiyetakipsekli,
        am.net_bakiye AS ch_bakiyesi,
        YEAR(GETDATE()) AS Yil,
        MONTH(GETDATE()) AS Ay,
        DAY(GETDATE()) AS Gun,
        'ALACAK' AS Cins,
        'BAKİYE' AS Tip,
        'TL' AS ParaBirimi,
        '' AS evrakturu,
        '' AS evrakno,
        '' AS vade_d,
        '' AS evraktarihi_d,
        0 AS odemesuresi,
        '' AS vade,
        0 AS kalantutar,
        0 AS bimno,
        0 AS mbd_bimno,
        0 AS KalanGun,
        '' AS VadeDurumu,
        '' AS plasiyer,
        '' AS AkisTipi
    FROM AlacaklarMizan am
    INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = @sirketkodu 
        AND fb.aktifpasif = 'A' 
        AND fb.firmakodu = am.firmakodu
    
    UNION ALL
    
    -- 2. ALACAK DETAYLARI VE KALAN TUTARLAR
    SELECT 
        'ALACAK_DETAY' AS RaporTipi,
        d1.firmakodu,
        fb.firmaadi,
        '' AS guncel_obs,
        d1.hesapkodu,
        CASE 
            WHEN am.net_bakiye >= 0 THEN am.net_bakiye -- Pozitifse direkt ekle
            ELSE d1.alacak -- Negatifse detaydan kalan tutar
        END AS bbakiye,
        d1.borc AS borc,
        d1.alacak AS alacak,
        d1.dborc AS dborc,
        d1.dalacak AS dalacak,
        d1.dborc - d1.dalacak AS dbbakiye,
        '' AS vg_bbakiyetakipsekli,
        '' AS vg_abakiyetakipsekli,
        CASE 
            WHEN am.net_bakiye >= 0 THEN am.net_bakiye
            ELSE d1.alacak
        END AS ch_bakiyesi,
        YEAR(d1.evraktarihi_d) AS Yil,
        MONTH(d1.evraktarihi_d) AS Ay,
        DAY(d1.evraktarihi_d) AS Gun,
        'CARİ TAHSİLAT' AS Cins,
        'BAKİYE' AS Tip,
        d1.para AS ParaBirimi,
        d1.evrakturu AS evrakturu,
        d1.evrakno AS evrakno,
        CONVERT(NVARCHAR(10), d1.odemevadesi_d, 104) AS vade_d,
        CONVERT(NVARCHAR(10), d1.evraktarihi_d, 104) AS evraktarihi_d,
        d1.odemesuresi AS odemesuresi,
        CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112) AS vade,
        CASE 
            WHEN am.net_bakiye >= 0 THEN am.net_bakiye
            ELSE d1.alacak
        END AS kalantutar,
        d1.bimno AS bimno,
        d1.ozet_bimno AS mbd_bimno,
        DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
        CASE 
            WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
            WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
            ELSE 'NORMAL'
        END AS VadeDurumu,
        '' AS plasiyer,
        'GİRİŞ' AS AkisTipi
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = d1.sirketkodu 
        AND fb.firmakodu = d1.firmakodu
    INNER JOIN AlacaklarMizan am ON am.firmakodu = d1.firmakodu 
        AND am.hesapkodu = d1.hesapkodu
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = @sirketkodu 
      AND d1.evraktarihi >= @ievraktarihi 
      AND d1.evraktarihi <= @sevraktarihi
      AND LEFT(d1.hesapkodu, 3) = '120'
      AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
      AND d1.alacak > 0
      -- Sadece mizanda olan firmaların detayları
      AND EXISTS (
          SELECT 1 FROM AlacaklarMizan am2 
          WHERE am2.firmakodu = d1.firmakodu 
            AND am2.hesapkodu = d1.hesapkodu
      )
    
    UNION ALL
    
    -- 3. BORÇLAR MİZANI (Firma bazında özet)
    SELECT 
        'BORÇ_MIZAN' AS RaporTipi,
        bm.firmakodu,
        fb.firmaadi,
        dbo.hg_firmakodlari_obs(@sirketkodu, bm.firmakodu) AS guncel_obs,
        bm.hesapkodu,
        bm.net_bakiye AS bbakiye,
        bm.toplam_borc AS borc,
        bm.toplam_alacak AS alacak,
        bm.toplam_dborc AS dborc,
        bm.toplam_dalacak AS dalacak,
        bm.toplam_dborc - bm.toplam_dalacak AS dbbakiye,
        fb.vg_bbakiyetakipsekli,
        fb.vg_abakiyetakipsekli,
        bm.net_bakiye AS ch_bakiyesi,
        YEAR(GETDATE()) AS Yil,
        MONTH(GETDATE()) AS Ay,
        DAY(GETDATE()) AS Gun,
        'BORÇ' AS Cins,
        'BAKİYE' AS Tip,
        'TL' AS ParaBirimi,
        '' AS evrakturu,
        '' AS evrakno,
        '' AS vade_d,
        '' AS evraktarihi_d,
        0 AS odemesuresi,
        '' AS vade,
        0 AS kalantutar,
        0 AS bimno,
        0 AS mbd_bimno,
        0 AS KalanGun,
        '' AS VadeDurumu,
        '' AS plasiyer,
        '' AS AkisTipi
    FROM (
        SELECT 
            d1.firmakodu,
            d1.hesapkodu,
            SUM(d1.borc) AS toplam_borc,
            SUM(d1.alacak) AS toplam_alacak,
            SUM(d1.dborc) AS toplam_dborc,
            SUM(d1.dalacak) AS toplam_dalacak,
            SUM(d1.borc) - SUM(d1.alacak) AS net_bakiye
        FROM mahsupbilgileridetay d1 WITH (NOLOCK)
        WHERE d1.aktifpasif = 'A' 
          AND d1.sirketkodu = @sirketkodu 
          AND d1.evraktarihi >= @ievraktarihi 
          AND d1.evraktarihi <= @sevraktarihi
          AND LEFT(d1.hesapkodu, 3) = '320' -- Borç hesapları
        GROUP BY d1.firmakodu, d1.hesapkodu
        HAVING SUM(d1.borc) - SUM(d1.alacak) > @bbakiye -- Minimum bakiye filtresi
    ) bm
    INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.aktifpasif = 'A' 
        AND fb.firmakodu = bm.firmakodu
    INNER JOIN sirketbilgileri sb WITH (NOLOCK) ON sb.sirketkodu = @sirketkodu 
        AND sb.sirketkodu_firmakodu = fb.sirketkodu
    
    UNION ALL
    
    -- 4. BORÇ DETAYLARI VE KALAN TUTARLAR
    SELECT 
        'BORÇ_DETAY' AS RaporTipi,
        d1.firmakodu,
        fb.firmaadi,
        '' AS guncel_obs,
        d1.hesapkodu,
        CASE 
            WHEN bm.net_bakiye >= 0 THEN bm.net_bakiye -- Pozitifse direkt ekle
            ELSE d1.borc -- Negatifse detaydan kalan tutar
        END AS bbakiye,
        d1.borc AS borc,
        d1.alacak AS alacak,
        d1.dborc AS dborc,
        d1.dalacak AS dalacak,
        d1.dborc - d1.dalacak AS dbbakiye,
        '' AS vg_bbakiyetakipsekli,
        '' AS vg_abakiyetakipsekli,
        CASE 
            WHEN bm.net_bakiye >= 0 THEN bm.net_bakiye
            ELSE d1.borc
        END AS ch_bakiyesi,
        YEAR(d1.evraktarihi_d) AS Yil,
        MONTH(d1.evraktarihi_d) AS Ay,
        DAY(d1.evraktarihi_d) AS Gun,
        CASE 
            WHEN d1.evrakturu LIKE '%VERGİ%' THEN 'VERGİ'
            ELSE 'CARİ ÖDEME'
        END AS Cins,
        'BAKİYE' AS Tip,
        d1.para AS ParaBirimi,
        d1.evrakturu AS evrakturu,
        d1.evrakno AS evrakno,
        CONVERT(NVARCHAR(10), d1.odemevadesi_d, 104) AS vade_d,
        CONVERT(NVARCHAR(10), d1.evraktarihi_d, 104) AS evraktarihi_d,
        d1.odemesuresi AS odemesuresi,
        CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112) AS vade,
        CASE 
            WHEN bm.net_bakiye >= 0 THEN bm.net_bakiye
            ELSE d1.borc
        END AS kalantutar,
        d1.bimno AS bimno,
        d1.ozet_bimno AS mbd_bimno,
        DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
        CASE 
            WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
            WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
            ELSE 'NORMAL'
        END AS VadeDurumu,
        COALESCE((
            SELECT TOP 1 di1.siciladi 
            FROM firmakodlari di1 WITH(NOLOCK) 
            WHERE di1.sirketkodu = @sirketkodu 
              AND di1.aktifpasif = 'A'
              AND di1.firmakodu = d1.firmakodu
        ), '') AS plasiyer,
        'ÇIKIŞ' AS AkisTipi
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = d1.sirketkodu 
        AND fb.firmakodu = d1.firmakodu
    INNER JOIN (
        SELECT 
            d2.firmakodu,
            d2.hesapkodu,
            SUM(d2.borc) - SUM(d2.alacak) AS net_bakiye
        FROM mahsupbilgileridetay d2 WITH (NOLOCK)
        WHERE d2.aktifpasif = 'A' 
          AND d2.sirketkodu = @sirketkodu 
          AND d2.evraktarihi >= @ievraktarihi 
          AND d2.evraktarihi <= @sevraktarihi
          AND LEFT(d2.hesapkodu, 3) = '320'
        GROUP BY d2.firmakodu, d2.hesapkodu
        HAVING SUM(d2.borc) - SUM(d2.alacak) > @bbakiye
    ) bm ON bm.firmakodu = d1.firmakodu 
        AND bm.hesapkodu = d1.hesapkodu
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = @sirketkodu 
      AND d1.evraktarihi >= @ievraktarihi 
      AND d1.evraktarihi <= @sevraktarihi
      AND LEFT(d1.hesapkodu, 3) = '320'
      AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
      AND d1.borc > 0
    
    UNION ALL
    
    -- 5. BİRLEŞİK NAKİT AKIŞ (Dashboard için)
    SELECT 
        'NAKİT_AKIŞ' AS RaporTipi,
        d1.firmakodu,
        fb.firmaadi,
        '' AS guncel_obs,
        d1.hesapkodu,
        CASE WHEN LEFT(d1.hesapkodu, 3) = '120' THEN d1.alacak ELSE d1.borc END AS bbakiye,
        d1.borc AS borc,
        d1.alacak AS alacak,
        d1.dborc AS dborc,
        d1.dalacak AS dalacak,
        d1.dborc - d1.dalacak AS dbbakiye,
        '' AS vg_bbakiyetakipsekli,
        '' AS vg_abakiyetakipsekli,
        CASE WHEN LEFT(d1.hesapkodu, 3) = '120' THEN d1.alacak ELSE d1.borc END AS ch_bakiyesi,
        YEAR(d1.evraktarihi_d) AS Yil,
        MONTH(d1.evraktarihi_d) AS Ay,
        DAY(d1.evraktarihi_d) AS Gun,
        CASE 
            WHEN LEFT(d1.hesapkodu, 3) = '120' THEN 'CARİ TAHSİLAT'
            WHEN d1.evrakturu LIKE '%VERGİ%' THEN 'VERGİ'
            ELSE 'CARİ ÖDEME'
        END AS Cins,
        'BAKİYE' AS Tip,
        d1.para AS ParaBirimi,
        d1.evrakturu AS evrakturu,
        d1.evrakno AS evrakno,
        CONVERT(NVARCHAR(10), d1.odemevadesi_d, 104) AS vade_d,
        CONVERT(NVARCHAR(10), d1.evraktarihi_d, 104) AS evraktarihi_d,
        d1.odemesuresi AS odemesuresi,
        CONVERT(NVARCHAR(8), d1.odemevadesi_d, 112) AS vade,
        CASE WHEN LEFT(d1.hesapkodu, 3) = '120' THEN d1.alacak ELSE d1.borc END AS kalantutar,
        d1.bimno AS bimno,
        d1.ozet_bimno AS mbd_bimno,
        DATEDIFF(day, GETDATE(), d1.odemevadesi_d) AS KalanGun,
        CASE 
            WHEN d1.odemevadesi_d < GETDATE() THEN 'VADESİ GEÇMİŞ'
            WHEN d1.odemevadesi_d <= DATEADD(day, 30, GETDATE()) THEN 'VADESİ GELEN'
            ELSE 'NORMAL'
        END AS VadeDurumu,
        '' AS plasiyer,
        CASE 
            WHEN LEFT(d1.hesapkodu, 3) = '120' THEN 'GİRİŞ'
            ELSE 'ÇIKIŞ'
        END AS AkisTipi
    FROM mahsupbilgileridetay d1 WITH (NOLOCK)
    INNER JOIN firmakodlari fb WITH (NOLOCK) ON fb.sirketkodu = d1.sirketkodu 
        AND fb.firmakodu = d1.firmakodu
    WHERE d1.aktifpasif = 'A' 
      AND d1.sirketkodu = @sirketkodu 
      AND d1.evraktarihi >= @ievraktarihi 
      AND d1.evraktarihi <= @sevraktarihi
      AND (LEFT(d1.hesapkodu, 3) = '120' OR LEFT(d1.hesapkodu, 3) = '320')
      AND (d1.islemtipi = 'R' OR d1.islemtipi = 'H')
      AND (d1.alacak > 0 OR d1.borc > 0)
    
    ORDER BY RaporTipi, firmakodu, firmaadi;
    
END
GO

-- Kullanım örneği:
-- EXEC sp_OzetDashboard_LoadAllData 
--     @sirketkodu = 'AYAZKAUCUK',
--     @ievraktarihi = '20240101',
--     @sevraktarihi = '20241231',
--     @maxvade = '20251231',
--     @vadesigecmis = '20240101',
--     @vadesigelen = '20241231',
--     @bbakiye = 0,
--     @islemtipi = 'FKO',
--     @haricmahsup = '',
--     @sicilno = ''