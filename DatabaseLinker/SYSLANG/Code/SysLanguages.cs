
using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseLinker
{


    public class SysLanguages
    {

        public static void CreateLanguagInfoTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("SYSLANG_LCID", typeof(int));// ci.LCID
            dt.Columns.Add("SYSLANG_CultureName", typeof(string));// ci.TextInfo.CultureName            
            dt.Columns.Add("SYSLANG_Name", typeof(string));// ci.Name

            dt.Columns.Add("SYSLANG_IetfLanguageTag", typeof(string));// ci.IetfLanguageTag
            dt.Columns.Add("SYSLANG_TwoLetterISOLanguageName", typeof(string));// ci.TwoLetterISOLanguageName
            dt.Columns.Add("SYSLANG_ThreeLetterISOLanguageName", typeof(string));// ci.ThreeLetterISOLanguageName
            dt.Columns.Add("SYSLANG_ThreeLetterWindowsLanguageName", typeof(string));// ci.ThreeLetterWindowsLanguageName

            dt.Columns.Add("SYSLANG_EnglishName", typeof(string));// ci.EnglishName
            dt.Columns.Add("SYSLANG_NativeName", typeof(string));// ci.NativeName
            dt.Columns.Add("SYSLANG_DisplayName", typeof(string));// ci.DisplayName

            dt.Columns.Add("SYSLANG_NativeCalendarName", typeof(string));// ci.DateTimeFormat.NativeCalendarName
            dt.Columns.Add("SYSLANG_FullDateTimePattern", typeof(string));// ci.DateTimeFormat.FullDateTimePattern
            dt.Columns.Add("SYSLANG_RFC1123Pattern", typeof(string));// ci.DateTimeFormat.RFC1123Pattern
            dt.Columns.Add("SYSLANG_SortableDateTimePattern", typeof(string));// ci.DateTimeFormat.SortableDateTimePattern
            dt.Columns.Add("SYSLANG_UniversalSortableDateTimePattern", typeof(string));// ci.DateTimeFormat.UniversalSortableDateTimePattern
            dt.Columns.Add("SYSLANG_DateSeparator", typeof(string));// ci.DateTimeFormat.DateSeparator
            dt.Columns.Add("SYSLANG_LongDatePattern", typeof(string));// ci.DateTimeFormat.LongDatePattern
            dt.Columns.Add("SYSLANG_ShortDatePattern", typeof(string));// ci.DateTimeFormat.ShortDatePattern
            dt.Columns.Add("SYSLANG_LongTimePattern", typeof(string));// ci.DateTimeFormat.LongTimePattern
            dt.Columns.Add("SYSLANG_ShortTimePattern", typeof(string));// ci.DateTimeFormat.ShortTimePattern
            dt.Columns.Add("SYSLANG_YearMonthPattern", typeof(string));// ci.DateTimeFormat.YearMonthPattern
            dt.Columns.Add("SYSLANG_MonthDayPattern", typeof(string));// ci.DateTimeFormat.MonthDayPattern
            dt.Columns.Add("SYSLANG_PMDesignator", typeof(string));// ci.DateTimeFormat.PMDesignator
            dt.Columns.Add("SYSLANG_AMDesignator", typeof(string));// ci.DateTimeFormat.AMDesignator
            dt.Columns.Add("SYSLANG_Calendar", typeof(string));// ci.Calendar.ToString()
            dt.Columns.Add("SYSLANG_IsNeutralCulture", typeof(bool));// ci.IsNeutralCulture

            // dt.Columns.Add("SYSLANG_IsReadOnly", typeof(bool));// ci.IsReadOnly
            dt.Columns.Add("SYSLANG_IsRightToLeft", typeof(bool));// ci.TextInfo.IsRightToLeft

            dt.Columns.Add("SYSLANG_ParentLCID", typeof(int));// ci.Parent.LCID

            dt.Columns.Add("SYSLANG_ANSICodePage", typeof(int));// ci.TextInfo.ANSICodePage
            dt.Columns.Add("SYSLANG_EBCDICCodePage", typeof(int));// ci.TextInfo.EBCDICCodePage
            dt.Columns.Add("SYSLANG_MacCodePage", typeof(int));// ci.TextInfo.MacCodePage
            dt.Columns.Add("SYSLANG_OEMCodePage", typeof(int));// ci.TextInfo.OEMCodePage
            dt.Columns.Add("SYSLANG_ListSeparator", typeof(string));// ci.TextInfo.ListSeparator
            dt.Columns.Add("SYSLANG_NumberDecimalSeparator", typeof(string));// ci.NumberFormat.NumberDecimalSeparator
            dt.Columns.Add("SYSLANG_NumberGroupSeparator", typeof(string));// ci.NumberFormat.NumberGroupSeparator
            // dt.Columns.Add("SYSLANG_NumberGroupSizes", typeof(string));// ci.NumberFormat.NumberGroupSizes
            dt.Columns.Add("SYSLANG_NumberNegativePattern", typeof(string));// ci.NumberFormat.NumberNegativePattern
            dt.Columns.Add("SYSLANG_CurrencyDecimalSeparator", typeof(string));// ci.NumberFormat.CurrencyDecimalSeparator
            dt.Columns.Add("SYSLANG_CurrencyGroupSeparator", typeof(string));// ci.NumberFormat.CurrencyGroupSeparator
            dt.Columns.Add("SYSLANG_CurrencySymbol", typeof(string));// ci.NumberFormat.CurrencySymbol
            dt.Columns.Add("SYSLANG_CurrencyNegativePattern", typeof(string));// ci.NumberFormat.CurrencyNegativePattern
            dt.Columns.Add("SYSLANG_CurrencyPositivePattern", typeof(string));// ci.NumberFormat.CurrencyPositivePattern
            dt.Columns.Add("SYSLANG_PercentDecimalSeparator", typeof(string));// ci.NumberFormat.PercentDecimalSeparator
            dt.Columns.Add("SYSLANG_PercentGroupSeparator", typeof(string));// ci.NumberFormat.PercentGroupSeparator
            dt.Columns.Add("SYSLANG_PercentNegativePattern", typeof(string));// ci.NumberFormat.PercentNegativePattern
            dt.Columns.Add("SYSLANG_PercentPositivePattern", typeof(string));// ci.NumberFormat.PercentPositivePattern
            dt.Columns.Add("SYSLANG_CorUse", typeof(bool));// ci.IetfLanguageTag.EndsWith("-CH")


            dt.TableName = "T_SYS_Language";

            string strSQL = TableConverter.TableCreateStatement(dt);

            // http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.percentpositivepattern(v=vs.110).aspx
            string[] PercentPositivePattern = new string[] { "n %", "n%", "%n", "% n" };

            // http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.percentnegativepattern(v=vs.110).aspx
            string[] PercentNegativePattern = new string[] { "-n %", "-n%", "-%n", "%-n", "%n-", "n-%", "n%-", "-% n", "n %-", "% n-", "% -n", "n- %" };

            // http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencypositivepattern(v=vs.110).aspx
            string[] CurrencyPositivePattern = new string[] { "$n", "n$", "$ n", "n $" };

            // http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.currencynegativepattern(v=vs.110).aspx
            string[] CurrencyNegativePattern = new string[] { "($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)" };

            // http://msdn.microsoft.com/en-us/library/system.globalization.numberformatinfo.numbernegativepattern(v=vs.110).aspx
            string[] NumberNegativePattern = new string[] { "(n)", "-n", "- n", "n-", "n -" };


            System.Data.DataRow dr = null;
            foreach (System.Globalization.CultureInfo ci in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
            {
                dr = dt.NewRow();

                dr["SYSLANG_LCID"] = ci.LCID;
                dr["SYSLANG_Name"] = ci.Name;
                dr["SYSLANG_IetfLanguageTag"] = ci.IetfLanguageTag;
                dr["SYSLANG_TwoLetterISOLanguageName"] = ci.TwoLetterISOLanguageName;
                dr["SYSLANG_ThreeLetterISOLanguageName"] = ci.ThreeLetterISOLanguageName;
                dr["SYSLANG_ThreeLetterWindowsLanguageName"] = ci.ThreeLetterWindowsLanguageName;

                dr["SYSLANG_EnglishName"] = ci.EnglishName;
                dr["SYSLANG_NativeName"] = ci.NativeName;
                dr["SYSLANG_DisplayName"] = ci.DisplayName;

                if (!ci.IsNeutralCulture)
                {
                    dr["SYSLANG_NativeCalendarName"] = ci.DateTimeFormat.NativeCalendarName;
                    dr["SYSLANG_FullDateTimePattern"] = ci.DateTimeFormat.FullDateTimePattern;
                    dr["SYSLANG_RFC1123Pattern"] = ci.DateTimeFormat.RFC1123Pattern;
                    dr["SYSLANG_SortableDateTimePattern"] = ci.DateTimeFormat.SortableDateTimePattern;
                    dr["SYSLANG_UniversalSortableDateTimePattern"] = ci.DateTimeFormat.UniversalSortableDateTimePattern;

                    dr["SYSLANG_DateSeparator"] = ci.DateTimeFormat.DateSeparator;

                    dr["SYSLANG_LongDatePattern"] = ci.DateTimeFormat.LongDatePattern;
                    dr["SYSLANG_ShortDatePattern"] = ci.DateTimeFormat.ShortDatePattern;

                    dr["SYSLANG_LongTimePattern"] = ci.DateTimeFormat.LongTimePattern;
                    dr["SYSLANG_ShortTimePattern"] = ci.DateTimeFormat.ShortTimePattern;

                    dr["SYSLANG_YearMonthPattern"] = ci.DateTimeFormat.YearMonthPattern;
                    dr["SYSLANG_MonthDayPattern"] = ci.DateTimeFormat.MonthDayPattern;

                    dr["SYSLANG_PMDesignator"] = ci.DateTimeFormat.PMDesignator;
                    dr["SYSLANG_AMDesignator"] = ci.DateTimeFormat.AMDesignator;




                    dr["SYSLANG_NumberDecimalSeparator"] = ci.NumberFormat.NumberDecimalSeparator;



                    dr["SYSLANG_NumberGroupSeparator"] = ci.NumberFormat.NumberGroupSeparator;
                    // dr["SYSLANG_NumberGroupSizes"] = ci.NumberFormat.NumberGroupSizes;

                    dr["SYSLANG_NumberNegativePattern"] = NumberNegativePattern[ci.NumberFormat.NumberNegativePattern];

                    dr["SYSLANG_CurrencyDecimalSeparator"] = ci.NumberFormat.CurrencyDecimalSeparator;
                    dr["SYSLANG_CurrencyGroupSeparator"] = ci.NumberFormat.CurrencyGroupSeparator;
                    dr["SYSLANG_CurrencySymbol"] = ci.NumberFormat.CurrencySymbol;


                    dr["SYSLANG_CurrencyNegativePattern"] = CurrencyNegativePattern[ci.NumberFormat.CurrencyNegativePattern];
                    dr["SYSLANG_CurrencyPositivePattern"] = CurrencyPositivePattern[ci.NumberFormat.CurrencyPositivePattern];

                    dr["SYSLANG_PercentDecimalSeparator"] = ci.NumberFormat.PercentDecimalSeparator;
                    dr["SYSLANG_PercentGroupSeparator"] = ci.NumberFormat.PercentGroupSeparator;

                    dr["SYSLANG_PercentNegativePattern"] = PercentNegativePattern[ci.NumberFormat.PercentNegativePattern];
                    dr["SYSLANG_PercentPositivePattern"] = PercentPositivePattern[ci.NumberFormat.PercentPositivePattern];
                }



                dr["SYSLANG_Calendar"] = ci.Calendar.ToString();

                dr["SYSLANG_IsNeutralCulture"] = ci.IsNeutralCulture;
                //dr["SYSLANG_IsReadOnly"] = ci.IsReadOnly;
                dr["SYSLANG_ParentLCID"] = ci.Parent.LCID;

                dr["SYSLANG_CultureName"] = ci.TextInfo.CultureName;
                dr["SYSLANG_IsRightToLeft"] = ci.TextInfo.IsRightToLeft;
                dr["SYSLANG_ANSICodePage"] = ci.TextInfo.ANSICodePage;
                dr["SYSLANG_EBCDICCodePage"] = ci.TextInfo.EBCDICCodePage;

                dr["SYSLANG_MacCodePage"] = ci.TextInfo.MacCodePage;
                dr["SYSLANG_OEMCodePage"] = ci.TextInfo.OEMCodePage;


                dr["SYSLANG_ListSeparator"] = ci.TextInfo.ListSeparator;
                dr["SYSLANG_CorUse"] = ci.IetfLanguageTag.EndsWith("-CH") || ci.IetfLanguageTag.Equals("en-US", StringComparison.InvariantCultureIgnoreCase);

                dt.Rows.Add(dr);
            }

            // dgvDisplayData.DataSource = dt;

            Settings.Target.ExecuteNonQuery(strSQL);

            strSQL = string.Format(@"ALTER TABLE {0}.{1} ALTER COLUMN SYSLANG_LCID INTEGER NOT NULL", "dbo", dt.TableName);
            Settings.Target.ExecuteNonQuery(strSQL);


            strSQL = string.Format(@"
ALTER TABLE {0}.{1}
ADD CONSTRAINT PK_{1} PRIMARY KEY CLUSTERED (SYSLANG_LCID ASC);
", "dbo", dt.TableName);

            Settings.Target.ExecuteNonQuery(strSQL);


            Settings.Target.InsertUpdateTable(dt.TableName, dt);

            return;
        } // End SUb CreateLanguagInfoTable() 


    } // End Class SysLanguages


} // End Namespace DatabaseLinker
