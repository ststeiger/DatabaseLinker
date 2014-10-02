
CREATE TABLE [dbo].[T_SYS_Language_DayNames](
	[SYSDAYS_SYSLANG_LCID] [int] NOT NULL,
	[SYSDAYS_DayOfWeekIndexBaseZero] [int] NOT NULL,
	[SYSDAYS_DayOfWeekIndexBaseOne] [int] NULL,
	[SYSDAYS_SYSLANG_IetfLanguageTag] [nvarchar](255) NULL,
	[SYSDAYS_Name] [nvarchar](255) NULL,
	[SYSDAYS_LowerCaseName] [nvarchar](255) NULL,
	[SYSDAYS_UpperCaseName] [nvarchar](255) NULL,
	[SYSDAYS_TitleCaseName] [nvarchar](255) NULL,
	[SYSDAYS_AbbreviatedName] [nvarchar](255) NULL,
	[SYSDAYS_LowerCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSDAYS_UpperCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSDAYS_TitleCaseAbbreviatedName] [nvarchar](255) NULL,
	[SYSDAYS_ShortestName] [nvarchar](255) NULL,
	[SYSDAYS_LowerCaseShortestName] [nvarchar](255) NULL,
	[SYSDAYS_UpperCaseShortestName] [nvarchar](255) NULL,
	[SYSDAYS_TitleCaseShortestName] [nvarchar](255) NULL,
 CONSTRAINT [PK_T_SYS_Language_DayNames] PRIMARY KEY CLUSTERED 
(
	[SYSDAYS_SYSLANG_LCID] ASC,
	[SYSDAYS_DayOfWeekIndexBaseZero] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


