
using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseLinker 
{


    class Months
    {


        public static byte[] SliceMe(byte[] source, int length)
        {
            byte[] destfoo = new byte[length];
            Array.Copy(source, 0, destfoo, 0, length);
            return destfoo;
        }


        public static string[] SliceMe(string[] source, int length)
        {
            string[] destfoo = new string[length];
            Array.Copy(source, 0, destfoo, 0, length);
            return destfoo;
        }


        public class MonthNameInfo
        {
            public System.Globalization.CultureInfo ci;
            public string Name;
            public string AbbreviatedName;
            public string GenitiveName;
            public string AbbreviatedGenitiveName;

            public int MonthIndexBaseZero;

            public int MonthIndexBaseOne
            {
                get
                {
                    return MonthIndexBaseZero + 1;
                }
            }

            public string LowerCaseName
            {
                get
                {
                    return this.ci.TextInfo.ToLower(Name);
                }
            }

            public string UpperCaseName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(Name); ;
                }
            }

            public string TitleCaseName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(Name); ;
                }
            }



            public string LowerCaseAbbreviatedName
            {
                get
                {
                    return this.ci.TextInfo.ToLower(this.AbbreviatedName);
                }
            }

            public string UpperCaseAbbreviatedName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(this.AbbreviatedName); ;
                }
            }

            public string TitleCaseAbbreviatedName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(this.AbbreviatedName); ;
                }
            }



            public string LowerCaseGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToLower(GenitiveName);
                }
            }

            public string UpperCaseGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(GenitiveName); ;
                }
            }

            public string TitleCaseGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(GenitiveName); ;
                }
            }




            public string LowerCaseAbbreviatedGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToLower(AbbreviatedGenitiveName);
                }
            }

            public string UpperCaseAbbreviatedGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(AbbreviatedGenitiveName); ;
                }
            }

            public string TitleCaseAbbreviatedGenitiveName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(AbbreviatedGenitiveName); ;
                }
            }

        }


        public static System.Data.DataTable GetMonthInfo()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.TableName = "T_SYS_Language_MonthNames";

            dt.Columns.Add("SYSMONTHS_SYSLANG_LCID", typeof(int));// ci.LCID
            dt.Columns.Add("SYSMONTHS_MonthIndexBaseZero", typeof(int));
            dt.Columns.Add("SYSMONTHS_MonthIndexBaseOne", typeof(int));

            dt.Columns.Add("SYSMONTHS_SYSLANG_IetfLanguageTag", typeof(string));// ci.IetfLanguageTag

            dt.Columns.Add("SYSMONTHS_Name", typeof(string));
            dt.Columns.Add("SYSMONTHS_LowerCaseName", typeof(string));
            dt.Columns.Add("SYSMONTHS_UpperCaseName", typeof(string));
            dt.Columns.Add("SYSMONTHS_TitleCaseName", typeof(string));

            dt.Columns.Add("SYSMONTHS_GenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_LowerCaseGenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_UpperCaseGenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_TitleCaseGenitiveName", typeof(string));

            dt.Columns.Add("SYSMONTHS_AbbreviatedName", typeof(string));
            dt.Columns.Add("SYSMONTHS_LowerCaseAbbreviatedName", typeof(string));
            dt.Columns.Add("SYSMONTHS_UpperCaseAbbreviatedName", typeof(string));
            dt.Columns.Add("SYSMONTHS_TitleCaseAbbreviatedName", typeof(string));

            dt.Columns.Add("SYSMONTHS_AbbreviatedGenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_LowerCaseAbbreviatedGenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_UpperCaseAbbreviatedGenitiveName", typeof(string));
            dt.Columns.Add("SYSMONTHS_TitleCaseAbbreviatedGenitiveName", typeof(string));



            System.Data.DataRow dr = null;
            foreach (System.Globalization.CultureInfo ci in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
            {

                if (!ci.IsNeutralCulture)
                {



                    // @Thomas Levesque this may be simply a "feature" of MS implementation. 
                    // For example the function that convert a string to title case (great britain => Great Britain) 
                    // works the same way for Russian language, while actually in Russian it is correct to capitalize 
                    // only the first letter of the first word (great britain => Great britain). 
                    string[] monthnames = SliceMe(ci.DateTimeFormat.MonthNames, 12);
                    string[] MonthGenitiveNames = SliceMe(ci.DateTimeFormat.MonthGenitiveNames, 12);

                    string[] AbbreviatedMonthNames = SliceMe(ci.DateTimeFormat.AbbreviatedMonthNames, 12);
                    string[] AbbreviatedMonthGenitiveNames = SliceMe(ci.DateTimeFormat.AbbreviatedMonthGenitiveNames, 12);



                    List<MonthNameInfo> lsMonths = new List<MonthNameInfo>();
                    for (int i = 0; i < monthnames.Length; ++i)
                    {
                        lsMonths.Add(new MonthNameInfo()
                        {
                            ci = ci,
                            MonthIndexBaseZero = i,
                            Name = monthnames[i]
                            ,
                            AbbreviatedName = AbbreviatedMonthNames[i]
                            ,
                            GenitiveName = MonthGenitiveNames[i]
                            ,
                            AbbreviatedGenitiveName = AbbreviatedMonthGenitiveNames[i]
                        });

                        dr = dt.NewRow();

                        dr["SYSMONTHS_SYSLANG_LCID"] = ci.LCID;
                        dr["SYSMONTHS_SYSLANG_IetfLanguageTag"] = ci.IetfLanguageTag;


                        dr["SYSMONTHS_MonthIndexBaseZero"] = lsMonths[i].MonthIndexBaseZero;
                        dr["SYSMONTHS_MonthIndexBaseOne"] = lsMonths[i].MonthIndexBaseOne;

                        dr["SYSMONTHS_Name"] = lsMonths[i].Name;
                        dr["SYSMONTHS_UpperCaseName"] = lsMonths[i].UpperCaseName;
                        dr["SYSMONTHS_LowerCaseName"] = lsMonths[i].LowerCaseName;
                        dr["SYSMONTHS_TitleCaseName"] = lsMonths[i].TitleCaseName;


                        dr["SYSMONTHS_GenitiveName"] = lsMonths[i].GenitiveName;
                        dr["SYSMONTHS_UpperCaseGenitiveName"] = lsMonths[i].UpperCaseGenitiveName;
                        dr["SYSMONTHS_LowerCaseGenitiveName"] = lsMonths[i].LowerCaseGenitiveName;
                        dr["SYSMONTHS_TitleCaseGenitiveName"] = lsMonths[i].TitleCaseGenitiveName;


                        dr["SYSMONTHS_AbbreviatedName"] = lsMonths[i].AbbreviatedName;
                        dr["SYSMONTHS_UpperCaseAbbreviatedName"] = lsMonths[i].UpperCaseAbbreviatedName;
                        dr["SYSMONTHS_LowerCaseAbbreviatedName"] = lsMonths[i].LowerCaseAbbreviatedName;
                        dr["SYSMONTHS_TitleCaseAbbreviatedName"] = lsMonths[i].TitleCaseAbbreviatedName;

                        dr["SYSMONTHS_AbbreviatedGenitiveName"] = lsMonths[i].AbbreviatedGenitiveName;
                        dr["SYSMONTHS_UpperCaseAbbreviatedGenitiveName"] = lsMonths[i].UpperCaseAbbreviatedGenitiveName;
                        dr["SYSMONTHS_LowerCaseAbbreviatedGenitiveName"] = lsMonths[i].LowerCaseAbbreviatedGenitiveName;
                        dr["SYSMONTHS_TitleCaseAbbreviatedGenitiveName"] = lsMonths[i].TitleCaseAbbreviatedGenitiveName;

                        dt.Rows.Add(dr);
                    }

                    Console.WriteLine(lsMonths);
                } // End if (!ci.IsNeutralCulture) 

            } // Next System.Globalization.CultureInfo ci 

            /*
            string strSQL = TableConverter.TableCreateStatement(dt);
            Settings.Target.ExecuteNonQuery(strSQL);

            strSQL = string.Format(@"ALTER TABLE {0}.{1} ALTER COLUMN SYSMONTHS_SYSLANG_LCID INTEGER NOT NULL;
ALTER TABLE {0}.{1} ALTER COLUMN SYSMONTHS_MonthIndexBaseZero INTEGER NOT NULL;", "dbo", dt.TableName);
            Settings.Target.ExecuteNonQuery(strSQL);

            strSQL = string.Format(@"
ALTER TABLE {0}.{1}
ADD CONSTRAINT PK_{1} PRIMARY KEY CLUSTERED (SYSMONTHS_SYSLANG_LCID ASC, SYSMONTHS_MonthIndexBaseZero ASC);
", "dbo", dt.TableName);

            Settings.Target.ExecuteNonQuery(strSQL);
            */

            Settings.Target.InsertUpdateTable(dt.TableName, dt);

            return dt;
        } // End Sub GetMonthInfo 

    }
}
