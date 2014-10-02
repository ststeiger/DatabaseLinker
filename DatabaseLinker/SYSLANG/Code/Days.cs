
using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseLinker 
{


    public class Days
    {

        public class DayNameInfo
        {
            public System.Globalization.CultureInfo ci;
            public string Name;
            public string AbbreviatedName;
            public string ShortestName;

            public int DayOfWeekIndexBaseZero;
            public int DayOfWeekIndexBaseOne
            {
                get
                {
                    if (DayOfWeekIndexBaseZero == 0)
                        return 7;
                    return DayOfWeekIndexBaseZero;
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
                    return this.ci.TextInfo.ToLower(AbbreviatedName);
                }
            }

            public string UpperCaseAbbreviatedName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(AbbreviatedName); ;
                }
            }

            public string TitleCaseAbbreviatedName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(AbbreviatedName); ;
                }
            }





            public string LowerCaseShortestName
            {
                get
                {
                    return this.ci.TextInfo.ToLower(ShortestName);
                }
            }

            public string UpperCaseShortestName
            {
                get
                {
                    return this.ci.TextInfo.ToUpper(ShortestName); ;
                }
            }

            public string TitleCaseShortestName
            {
                get
                {
                    return this.ci.TextInfo.ToTitleCase(ShortestName); ;
                }
            }

        }


        public static System.Data.DataTable GetDayInfo()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.TableName = "T_SYS_Language_DayNames";

            dt.Columns.Add("SYSDAYS_SYSLANG_LCID", typeof(int));
            dt.Columns.Add("SYSDAYS_DayOfWeekIndexBaseZero", typeof(int));
            dt.Columns.Add("SYSDAYS_DayOfWeekIndexBaseOne", typeof(int));
            dt.Columns.Add("SYSDAYS_SYSLANG_IetfLanguageTag", typeof(string));// ci.IetfLanguageTag

            dt.Columns.Add("SYSDAYS_Name", typeof(string));
            dt.Columns.Add("SYSDAYS_LowerCaseName", typeof(string));
            dt.Columns.Add("SYSDAYS_UpperCaseName", typeof(string));
            dt.Columns.Add("SYSDAYS_TitleCaseName", typeof(string));

            dt.Columns.Add("SYSDAYS_AbbreviatedName", typeof(string));
            dt.Columns.Add("SYSDAYS_LowerCaseAbbreviatedName", typeof(string));
            dt.Columns.Add("SYSDAYS_UpperCaseAbbreviatedName", typeof(string));
            dt.Columns.Add("SYSDAYS_TitleCaseAbbreviatedName", typeof(string));

            dt.Columns.Add("SYSDAYS_ShortestName", typeof(string));
            dt.Columns.Add("SYSDAYS_LowerCaseShortestName", typeof(string));
            dt.Columns.Add("SYSDAYS_UpperCaseShortestName", typeof(string));
            dt.Columns.Add("SYSDAYS_TitleCaseShortestName", typeof(string));

            System.Data.DataRow dr = null;
            foreach (System.Globalization.CultureInfo ci in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
            {

                if (!ci.IsNeutralCulture)
                {
                    List<DayNameInfo> lsDays = new List<DayNameInfo>();
                    string[] daynames = ci.DateTimeFormat.DayNames;
                    string[] abbreviateddaynames = ci.DateTimeFormat.AbbreviatedDayNames;
                    string[] shortestdaynames = ci.DateTimeFormat.ShortestDayNames;

                    for (int i = 0; i < daynames.Length; ++i)
                    {
                        lsDays.Add(new DayNameInfo()
                        {
                            ci = ci
                            ,
                            DayOfWeekIndexBaseZero = i
                            ,
                            Name = daynames[i]
                            ,
                            AbbreviatedName = abbreviateddaynames[i]
                            ,
                            ShortestName = shortestdaynames[i]
                        }
                        );

                        dr = dt.NewRow();

                        dr["SYSDAYS_SYSLANG_LCID"] = ci.LCID;
                        dr["SYSDAYS_SYSLANG_IetfLanguageTag"] = ci.IetfLanguageTag;


                        dr["SYSDAYS_DayOfWeekIndexBaseZero"] = lsDays[i].DayOfWeekIndexBaseZero;
                        dr["SYSDAYS_DayOfWeekIndexBaseOne"] = lsDays[i].DayOfWeekIndexBaseOne;

                        dr["SYSDAYS_Name"] = lsDays[i].Name;
                        dr["SYSDAYS_UpperCaseName"] = lsDays[i].UpperCaseName;
                        dr["SYSDAYS_LowerCaseName"] = lsDays[i].LowerCaseName;
                        dr["SYSDAYS_TitleCaseName"] = lsDays[i].TitleCaseName;


                        dr["SYSDAYS_AbbreviatedName"] = lsDays[i].AbbreviatedName;
                        dr["SYSDAYS_LowerCaseAbbreviatedName"] = lsDays[i].LowerCaseAbbreviatedName;
                        dr["SYSDAYS_UpperCaseAbbreviatedName"] = lsDays[i].UpperCaseAbbreviatedName;
                        dr["SYSDAYS_TitleCaseAbbreviatedName"] = lsDays[i].TitleCaseAbbreviatedName;


                        dr["SYSDAYS_ShortestName"] = lsDays[i].ShortestName;
                        dr["SYSDAYS_LowerCaseShortestName"] = lsDays[i].LowerCaseShortestName;
                        dr["SYSDAYS_UpperCaseShortestName"] = lsDays[i].UpperCaseShortestName;
                        dr["SYSDAYS_TitleCaseShortestName"] = lsDays[i].TitleCaseShortestName;

                        dt.Rows.Add(dr);
                    }

                    Console.WriteLine(lsDays);



                    int dayCount = daynames.Length;
                    if (dayCount != 7)
                    {
                        Console.WriteLine(dayCount);
                    }


                    //dt.Rows.Add(dr);
                } // End if (!ci.IsNeutralCulture) 

            } // Next System.Globalization.CultureInfo ci 

            /*
            string strSQL = TableConverter.TableCreateStatement(dt);
            Settings.Target.ExecuteNonQuery(strSQL);

            strSQL = string.Format(@"ALTER TABLE {0}.{1} ALTER COLUMN SYSDAYS_SYSLANG_LCID INTEGER NOT NULL;
ALTER TABLE {0}.{1} ALTER COLUMN SYSDAYS_DayOfWeekIndexBaseZero INTEGER NOT NULL;", "dbo", dt.TableName);
            Settings.Target.ExecuteNonQuery(strSQL);
            

            strSQL = string.Format(@"
ALTER TABLE {0}.{1}
ADD CONSTRAINT PK_{1} PRIMARY KEY CLUSTERED (SYSDAYS_SYSLANG_LCID ASC, SYSDAYS_DayOfWeekIndexBaseZero ASC);
", "dbo", dt.TableName);

            Settings.Target.ExecuteNonQuery(strSQL);
            */ 

            Settings.Target.InsertUpdateTable(dt.TableName, dt);

            return dt;
        } // End Sub GetDayInfo


    }
}
