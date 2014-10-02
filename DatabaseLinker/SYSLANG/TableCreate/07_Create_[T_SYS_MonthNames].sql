
CREATE TABLE [dbo].[T_SYS_Language_MonthNames](
	[SYSMONTHS_SYSLANG_LCID] [int] NOT NULL,
	[SYSMONTHS_MonthIndexBaseZero] [int] NOT NULL,
	[SYSMONTHS_MonthIndexBaseOne] [int] NULL,
	[SYSMONTHS_SYSLANG_IetfLanguageTag] [nvarchar](255) NULL,
	[SYSMONTHS_Name] [nvarchar](255) NULL,
	[SYSMONTHS_LowerCaseName] [nvarchar](255) NULL,
	[SYSMONTHS_UpperCaseName] [nvarchar](255) NULL,
	[SYSMONTHS_TitleCaseName] [nvarchar](255) NULL,
	[SYSMONTHS_GenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_LowerCaseGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_UpperCaseGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_TitleCaseGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_AbbreviatedName] [nvarchar](255) NULL,
	[SYSMONTHS_LowerCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSMONTHS_UpperCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSMONTHS_TitleCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSMONTHS_AbbreviatedGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_LowerCaseAbbreviatedGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_UpperCaseAbbreviatedGenitiveName] [nvarchar](255) NULL,
	[SYSMONTHS_TitleCaseAbbreviatedGenitiveName] [nvarchar](255) NULL,
 CONSTRAINT [PK_T_SYS_Language_MonthNames] PRIMARY KEY CLUSTERED 
(
	[SYSMONTHS_SYSLANG_LCID] ASC,
	[SYSMONTHS_MonthIndexBaseZero] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


