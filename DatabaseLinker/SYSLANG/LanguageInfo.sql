
USE WincasaMirror


SELECT 
	 SYSLANG_LCID
	,SYSLANG_ParentLCID
	,SYSLANG_CultureName
	,SYSLANG_Name

	,SYSLANG_IsNeutralCulture
	,SYSLANG_IetfLanguageTag
	,SYSLANG_TwoLetterISOLanguageName
	,SYSLANG_ThreeLetterISOLanguageName
	,SYSLANG_ThreeLetterWindowsLanguageName
	,SYSLANG_EnglishName
	,SYSLANG_NativeName
	,SYSLANG_DisplayName
	,SYSLANG_NativeCalendarName
	,SYSLANG_FullDateTimePattern
	,SYSLANG_RFC1123Pattern
	,SYSLANG_SortableDateTimePattern
	,SYSLANG_UniversalSortableDateTimePattern
	,SYSLANG_DateSeparator
	,SYSLANG_LongDatePattern
	,SYSLANG_ShortDatePattern
	,SYSLANG_LongTimePattern
	,SYSLANG_ShortTimePattern
	,SYSLANG_YearMonthPattern
	,SYSLANG_MonthDayPattern
	,SYSLANG_PMDesignator
	,SYSLANG_AMDesignator
	,SYSLANG_Calendar

	,SYSLANG_IsRightToLeft

	,SYSLANG_ANSICodePage
	,SYSLANG_EBCDICCodePage
	,SYSLANG_MacCodePage
	,SYSLANG_OEMCodePage
	,SYSLANG_ListSeparator
	,SYSLANG_NumberDecimalSeparator
	,SYSLANG_NumberGroupSeparator
	,SYSLANG_NumberNegativePattern
	,SYSLANG_CurrencyDecimalSeparator
	,SYSLANG_CurrencyGroupSeparator
	,SYSLANG_CurrencySymbol
	,SYSLANG_CurrencyNegativePattern
	,SYSLANG_CurrencyPositivePattern
	,SYSLANG_PercentDecimalSeparator
	,SYSLANG_PercentGroupSeparator
	,SYSLANG_PercentNegativePattern
	,SYSLANG_PercentPositivePattern
	
	,SYSLANG_CorUse
FROM T_SYS_Language
WHERE (1=1) 
AND 
(
	(1=2) 
	--OR SYSLANG_EnglishName LIKE '%English%'
	--OR SYSLANG_LCID = 127
	--OR 1=1 
	OR SYSLANG_TwoLetterISOLanguageName = 'de'
	--OR SYSLANG_TwoLetterISOLanguageName = 'fr'
	--OR SYSLANG_TwoLetterISOLanguageName = 'it'
	--OR SYSLANG_TwoLetterISOLanguageName = 'en'
)
--AND SYSLANG_ParentLCID <> 127 
--AND SYSLANG_ParentLCID = 127 
--AND SYSLANG_IetfLanguageTag LIKE '%-CH'
AND SYSLANG_CorUse = 1 

-- ORDER BY SYSLANG_CultureName 
--AND SYSLANG_PercentPositivePattern = 5
