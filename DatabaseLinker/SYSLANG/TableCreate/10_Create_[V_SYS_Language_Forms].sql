
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_SYS_Language_Forms]'))
DROP VIEW [dbo].[V_SYS_Language_Forms]
GO




CREATE VIEW [dbo].[V_SYS_Language_Forms] 
AS 
SELECT 
	 LANG_UID
	,LANG_Modul
	,LANG_Object
	,LANG_Register 
	,LANG_Position 
	,DE.LANG_i18n_Text AS LANG_DE 
	,FR.LANG_i18n_Text AS LANG_FR 
	,IT.LANG_i18n_Text AS LANG_IT 
	,EN.LANG_i18n_Text AS LANG_EN 
	,LANG_Fieldname
	,LANG_FieldType
	,LANG_IsRequired
	,LANG_Validate
	,LANG_Reftable
	,LANG_CheckHistory
	,LANG_IsValidity
	,LANG_ErfDate
	,LANG_Status
FROM T_SYS_Language_Forms 

LEFT JOIN T_SYS_Language_Forms_i18n AS DE 
	ON DE.LANG_i18n_LANG_UID = T_SYS_Language_Forms.LANG_UID 
	AND DE.LANG_i18n_SYSLANG_LCID = 2055 
	
LEFT JOIN T_SYS_Language_Forms_i18n AS FR 
	ON FR.LANG_i18n_LANG_UID = T_SYS_Language_Forms.LANG_UID 
	AND FR.LANG_i18n_SYSLANG_LCID = 4108 
	
LEFT JOIN T_SYS_Language_Forms_i18n AS IT 
	ON IT.LANG_i18n_LANG_UID = T_SYS_Language_Forms.LANG_UID 
	AND IT.LANG_i18n_SYSLANG_LCID = 2064 
	
LEFT JOIN T_SYS_Language_Forms_i18n AS EN 
	ON EN.LANG_i18n_LANG_UID = T_SYS_Language_Forms.LANG_UID 
	AND EN.LANG_i18n_SYSLANG_LCID = 1033 
	
GO


