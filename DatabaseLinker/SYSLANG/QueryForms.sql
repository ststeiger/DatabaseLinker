
--USE [WincasaMirror]


DECLARE @in_lang_id int
--SET @in_lang_id = (
--    SELECT Lang_ID
--    FROM T_Languages
--    WHERE Lang_ISO_TwoLetterName = 'DE'
--)


SET @in_lang_id = (
	SELECT SYSLANG_LCID 
	FROM T_SYS_Language 
	WHERE (1=1) 
	AND T_SYS_Language.SYSLANG_TwoLetterISOLanguageName = 'EN' 
	AND T_SYS_Language.SYSLANG_CorUse = 1 
)


SELECT 
     LANG_UID 
    ,LANG_Default -- Default Fallback field (internal name/one language only setup), just in ResultSet for demo-purposes
    ,@in_lang_id AS langId
    ,LANG_i18n_Text  -- Translation text, just in ResultSet for demo-purposes
    ,LANG_i18n_Cust_Text  -- Custom Translations (e.g. per customer) Just in ResultSet for demo-purposes
    ,COALESCE(LANG_i18n_Cust_Text, LANG_i18n_Text, LANG_Default) AS DisplayText -- What we actually want to show 
FROM T_SYS_Language_Forms2 

LEFT JOIN T_SYS_Language_Forms_i18n 
    ON LANG_i18n_LANG_UID = T_SYS_Language_Forms2.LANG_UID 
    AND LANG_i18n_SYSLANG_LCID = @in_lang_id 

LEFT JOIN T_SYS_Language_Forms_i18n_Cust 
    ON LANG_i18n_Cust_LANG_UID = T_SYS_Language_Forms2.LANG_UID
    AND LANG_i18n_Cust_SYSLANG_LCID = @in_lang_id 
--USE COR_Basic


--SELECT 
--	 SYS_Uid
--	,SYS_Sprache_IsDe
--	,SYS_Sprache_IsFr
--	,SYS_Sprache_IsIt
--	,SYS_Sprache_IsEn
--FROM T_SYS_Settings_System


--SELECT 
--	 LANG_UID 
--	,LANG_MDT_ID
--	,LANG_Code
--	,LANG_Kurz_DE
--	,LANG_Kurz_FR
--	,LANG_Kurz_IT
--	,LANG_Kurz_EN
--	,LANG_Lang_DE
--	,LANG_Lang_FR
--	,LANG_Lang_IT
--	,LANG_Lang_EN
--	,LANG_Status
--	,LANG_Sort
--	,LANG_StylizerFore
--	,LANG_StylizerBack
--	,LANG_StylizerPattern
--	,LANG_StylizerLine
--	,LANG_IsDefault
--	,LANG_DatumMut
--	,LANG_DatumUser
--FROM T_SYS_Ref_Language
