
CREATE TABLE [dbo].[T_SYS_Language](
	[SYSLANG_LCID] [int] NOT NULL,
	[SYSLANG_CultureName] [nvarchar](255) NULL,
	[SYSLANG_Name] [nvarchar](255) NULL,
	[SYSLANG_IetfLanguageTag] [nvarchar](255) NULL,
	[SYSLANG_TwoLetterISOLanguageName] [nvarchar](255) NULL,
	[SYSLANG_ThreeLetterISOLanguageName] [nvarchar](255) NULL,
	[SYSLANG_ThreeLetterWindowsLanguageName] [nvarchar](255) NULL,
	[SYSLANG_EnglishName] [nvarchar](255) NULL,
	[SYSLANG_NativeName] [nvarchar](255) NULL,
	[SYSLANG_DisplayName] [nvarchar](255) NULL,
	[SYSLANG_NativeCalendarName] [nvarchar](255) NULL,
	[SYSLANG_FullDateTimePattern] [nvarchar](255) NULL,
	[SYSLANG_RFC1123Pattern] [nvarchar](255) NULL,
	[SYSLANG_SortableDateTimePattern] [nvarchar](255) NULL,
	[SYSLANG_UniversalSortableDateTimePattern] [nvarchar](255) NULL,
	[SYSLANG_DateSeparator] [nvarchar](255) NULL,
	[SYSLANG_LongDatePattern] [nvarchar](255) NULL,
	[SYSLANG_ShortDatePattern] [nvarchar](255) NULL,
	[SYSLANG_LongTimePattern] [nvarchar](255) NULL,
	[SYSLANG_ShortTimePattern] [nvarchar](255) NULL,
	[SYSLANG_YearMonthPattern] [nvarchar](255) NULL,
	[SYSLANG_MonthDayPattern] [nvarchar](255) NULL,
	[SYSLANG_PMDesignator] [nvarchar](255) NULL,
	[SYSLANG_AMDesignator] [nvarchar](255) NULL,
	[SYSLANG_Calendar] [nvarchar](255) NULL,
	[SYSLANG_IsNeutralCulture] [bit] NULL,
	[SYSLANG_IsRightToLeft] [bit] NULL,
	[SYSLANG_ParentLCID] [int] NULL,
	[SYSLANG_ANSICodePage] [int] NULL,
	[SYSLANG_EBCDICCodePage] [int] NULL,
	[SYSLANG_MacCodePage] [int] NULL,
	[SYSLANG_OEMCodePage] [int] NULL,
	[SYSLANG_ListSeparator] [nvarchar](255) NULL,
	[SYSLANG_NumberDecimalSeparator] [nvarchar](255) NULL,
	[SYSLANG_NumberGroupSeparator] [nvarchar](255) NULL,
	[SYSLANG_NumberNegativePattern] [nvarchar](255) NULL,
	[SYSLANG_CurrencyDecimalSeparator] [nvarchar](255) NULL,
	[SYSLANG_CurrencyGroupSeparator] [nvarchar](255) NULL,
	[SYSLANG_CurrencySymbol] [nvarchar](255) NULL,
	[SYSLANG_CurrencyNegativePattern] [nvarchar](255) NULL,
	[SYSLANG_CurrencyPositivePattern] [nvarchar](255) NULL,
	[SYSLANG_PercentDecimalSeparator] [nvarchar](255) NULL,
	[SYSLANG_PercentGroupSeparator] [nvarchar](255) NULL,
	[SYSLANG_PercentNegativePattern] [nvarchar](255) NULL,
	[SYSLANG_PercentPositivePattern] [nvarchar](255) NULL,
	[SYSLANG_CorUse] [bit] NULL,
 CONSTRAINT [PK_T_SYS_Language] PRIMARY KEY CLUSTERED 
(
	[SYSLANG_LCID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


