
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DatabaseLinker
{


    public partial class frmMain : Form
    {


        public frmMain()
        {
            InitializeComponent();
        } // End Constructor


        public static void MsgBox(object obj)
        {
            if (obj != null)
                System.Windows.Forms.MessageBox.Show(obj.ToString());
        } // End Sub MsgBox


        // KillAllProcessesOnDb("COR_Basic_Demo", "COR_Basic_SNB", "", null);
        public static void KillAllProcessesOnDb(params string[] DBs)
        {
            for (int i = 0; i < DBs.Length; ++i)
            {
                if (string.IsNullOrEmpty(DBs[i]) || DBs[i].Trim() == string.Empty)
                {
                    DBs[i] = "";
                }

                DBs[i] = string.Format("db_id('{0}')", Settings.Target.SqlEscapeString(DBs[i]));
            } // Next i 

            string strDBs = string.Join(", ", DBs);

            string KillProcessesSQL = string.Format(@"
DECLARE @kill varchar(MAX) = 'USE master; ';
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(15), spid) + ';'
FROM master..sysprocesses 
WHERE dbid IN ({0}) 
AND spid <> @@SPID 

PRINT @kill
EXEC(@kill);
", strDBs);

            Settings.Target.ExecuteNonQuery(KillProcessesSQL);
        } // End Sub KillAllProcessesOnDb


        public static string NormalizeWhitespace(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            return System.Text.RegularExpressions.Regex.Replace(InputStr, @"\s+", " ").Trim();
        }


        public static string NormalizeComma(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            System.Text.RegularExpressions.Regex NormRx =
                new System.Text.RegularExpressions.Regex(@"\s*,\s*");
            return NormRx.Replace(InputStr, ",");
        }


        public static string NormalizeOpenBracket(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            System.Text.RegularExpressions.Regex NormRx =
                new System.Text.RegularExpressions.Regex(@"\s*\(\s*");
            return NormRx.Replace(InputStr, "(");
        }


        public static string NormalizeCloseBracket(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            System.Text.RegularExpressions.Regex NormRx =
                new System.Text.RegularExpressions.Regex(@"\s*\)\s*");
            return NormRx.Replace(InputStr, ")");
        }


        public static string NormalizeEqual(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            System.Text.RegularExpressions.Regex NormRx =
                new System.Text.RegularExpressions.Regex(@"\s*=\s*");
            return NormRx.Replace(InputStr, "=");
        }


        public static void GetProcedureHeader()
        {
            string strSQL = @"SELECT OBJECT_DEFINITION(OBJECT_ID(specific_name))";

            // Begin optional klammer optional 
            string bla = @"
        CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]  (      @Application NVARCHAR(60),      @PageIndex INT = 0,     
@PageSize INT = 15,      @TotalCount INT OUTPUT  )  AS         SET NOCOUNT ON        DECLARE @FirstTimeUTC DATETIME     
DECLARE @FirstSequence INT      DECLARE @StartRow INT      DECLARE @StartRowIndex INT        SELECT           @TotalCount = COUNT(1)       
FROM           [ELMAH_Error]      WHERE           [Application] = @Application        -- Get the ID of the first error for the requested page        SET @StartRowIndex = @PageIndex * @PageSize + 1        IF @StartRowIndex <= @TotalCount      BEGIN            SET ROWCOUNT @StartRowIndex            SELECT                @FirstTimeUTC = [TimeUtc],              @FirstSequence = [Sequence]          FROM               [ELMAH_Error]          WHERE                 [Application] = @Application          ORDER BY               [TimeUtc] DESC,               [Sequence] DESC        END      ELSE      BEGIN            SET @PageSize = 0        END        -- Now set the row count to the requested page size and get      -- all records below it for the pertaining application.        SET ROWCOUNT @PageSize        SELECT           errorId     = [ErrorId],           application = [Application],          host        = [Host],           type        = [Type],          source      = [Source],          message     = [Message],          [user]      = [User],          statusCode  = [StatusCode],           time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'      FROM           [ELMAH_Error] error      WHERE          [Application] = @Application      AND          [TimeUtc] <= @FirstTimeUTC      AND           [Sequence] <= @FirstSequence      ORDER BY          [TimeUtc] DESC,           [Sequence] DESC      FOR          XML AUTO    ";


            bla = @"CREATE PROCEDURE [dbo].[sp_AP_Legenden_Update]
	@in_typ AS varchar(5),
	@in_uid AS varchar(36) 
	WITH RECOMPILE 
AS 
BEGIN";

            string bla3 = @"
CREATE PROCEDURE [dbo].[myELMAH_GetErrorsXml]
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT 
AS ";


            bla = RemoveComments(bla);

            int iOpenCount = 0;
            int iClosedCount = 0;


            int iMagicPosition = 0;

            for (int i = 0; i < bla.Length; ++i)
            {
                if (bla[i] == '(')
                    iOpenCount++;

                if (bla[i] == ')')
                    iClosedCount++;

                if (iOpenCount > 0 && iOpenCount == iClosedCount)
                {
                    iMagicPosition = i + 1;
                    break;
                }

            } // Next i

            string str = bla.Substring(0, iMagicPosition);
            Console.WriteLine(str);
        }


        static string RemoveCstyleComments(string strInput)
        {
            string strPattern = @"/[*][\w\d\s]+[*]/";
            //strPattern = @"/\*.*?\*/"; // Doesn't work 
            //strPattern = "/\\*.*?\\*/"; // Doesn't work 
            //strPattern = @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/ "; // Doesn't work 
            //strPattern = @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/ "; // Doesn't work 

            // http://stackoverflow.com/questions/462843/improving-fixing-a-regex-for-c-style-block-comments 
            strPattern = @"/\*(?>(?:(?>[^*]+)|\*(?!/))*)\*/";  // Works ! 

            string strOutput = System.Text.RegularExpressions.Regex.Replace(strInput, strPattern, string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
            Console.WriteLine(strOutput);
            return strOutput;
        } // End Function RemoveCstyleComments 


        public static string RemoveSingleLineComments(string strInput)
        {
            return System.Text.RegularExpressions.Regex.Replace(strInput, "-{2,}.*", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
        }


        public static string RemoveComments(string strInput)
        {
            strInput = RemoveCstyleComments(strInput);
            strInput = RemoveSingleLineComments(strInput);
            return strInput;
        }


        public static string NormalizeProcedure(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            return System.Text.RegularExpressions.Regex.Replace(InputStr, @"\s+CREATE\s+PROCEDURE\s+", "CREATE PROCEDURE ");
        }


        // \\vmstdzuerich\Reporting Services\ReportServer
        // http://msdn.microsoft.com/en-us/library/ms187926.aspx
        public static string GetRightCombination(string StringToTest, string ObjectSchema, string ObjectName)
        {
            string[] PossibleProcedureTerms = new string[] { "PROC", "PROCEDURE" };

            string[] PossibleSchemaNames = new string[] { 
                ""
                ,ObjectSchema+"."
                ,Settings.Source.QuoteSchema(ObjectSchema) + "."
                ,Settings.Source.BracketSchema(ObjectSchema) + "."
            };

            string[] PossibleObjectNames = new string[] { 
                 ObjectName
                ,Settings.Source.QuoteObject(ObjectName)
                ,Settings.Source.BracketObject(ObjectName)
            };

            string[] arr = new string[PossibleSchemaNames.Length * PossibleObjectNames.Length];

            foreach (string PossibleProcedureTerm in PossibleProcedureTerms)
            {

                foreach (string PossibleSchemaName in PossibleSchemaNames)
                {
                    foreach (string PossibleObjectName in PossibleObjectNames)
                    {
                        string PossibleCombination = "CREATE " + PossibleProcedureTerm + " " + PossibleSchemaName + PossibleObjectName;

                        if (StringToTest.ToLowerInvariant().IndexOf(PossibleCombination.ToLowerInvariant()) != -1)
                        {
                            return PossibleCombination;
                        }

                    } // Next PossibleObjectName

                } // Next PossibleSchemaName

            } // Next PossibleProcedureTerm

            return "";
        }


        public static string NormalizeCreateProcedure(string InputStr)
        {
            if (string.IsNullOrEmpty(InputStr))
                return InputStr;

            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\s*CREATE\s+(PROCEDURE|PROC)\s*");
            return rgx.Replace(InputStr, "CREATE PROCEDURE ", 1); // The 1 makes the difference
        } // End Function NormalizeCreateProcedure 


        public static string CapText(System.Text.RegularExpressions.Match m)
        {
            // Get the matched string. 
            string x = m.ToString();
            // If the first char is lower case... 
            if (char.IsLower(x[0]))
            {
                // Capitalize it. 
                return char.ToUpper(x[0]) + x.Substring(1, x.Length - 1);
            }
            return x;
        } // End Function CapText 


        public void simpleSubstitionTest()
        {
            string text = " bla blab abla;";

            string foo = "ay";
            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            //string result = System.Text.RegularExpressions.Regex.Replace(text, "bla", new System.Text.RegularExpressions.MatchEvaluator(CapText));
            string result = System.Text.RegularExpressions.Regex.Replace(text, "bla", new System.Text.RegularExpressions.MatchEvaluator(
                delegate(System.Text.RegularExpressions.Match m)
                {
                    string strId = "#@#" + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + "#@#";
                    dict.Add(strId, m.Value);

                    return foo;
                }
            ));
        } // End Sub simpleSubstitionTest 



        private void btnOK_Click(object sender, EventArgs e)
        {
            // GetParameterDefinition("dbo", "ELMAH_GetErrorsXml");

            // GetProcedureHeader();


            // [___/*bl*/a] 
            // replace double single-quote
            // replace single-quoted content
            // remove c-style comments comments 
            // remove single-line comments
            // trim


            // DbLinker.LinkTables();
            // DbLinker.LinkViews();

            ProcessRoutine();



            string strGetTablesAndViewsSQL = @"
SELECT 
	 TABLE_NAME 
	,TABLE_TYPE 
FROM INFORMATION_SCHEMA.TABLES 
ORDER BY TABLE_TYPE, TABLE_NAME 
";





            string strListRoutinesSQL = @"
SELECT 
	 SPECIFIC_NAME
	,ROUTINE_NAME 
	,ROUTINE_TYPE
	,DATA_TYPE -- NULL, TABLE, scalar
	
	,NUMERIC_PRECISION
	--,NUMERIC_PRECISION_RADIX
	,NUMERIC_SCALE 
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE (1=1) 
AND ROUTINE_NAME NOT IN 
(
	 'dt_adduserobject'
	,'dt_droppropertiesbyid'
	,'dt_dropuserobjectbyid'
	,'dt_generateansiname'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop_u'
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid_u'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid_u'
	,'dt_verstamp006'
	,'dt_verstamp007'

	,'sp_helpdiagrams'
	,'sp_creatediagram'
	,'sp_alterdiagram'
	,'sp_renamediagram'
	,'sp_dropdiagram'

	,'sp_helpdiagramdefinition'
	,'fn_diagramobjects'
) 

ORDER BY ROUTINE_TYPE DESC, CASE WHEN DATA_TYPE = 'table' THEN 0 ELSE 1 END, DATA_TYPE, ROUTINE_NAME, SPECIFIC_NAME 
"
;




            string strSQL = @"
SELECT 
	 --SPECIFIC_CATALOG
	--,SPECIFIC_SCHEMA
	 SPECIFIC_NAME
	,ORDINAL_POSITION
	,PARAMETER_MODE
	,IS_RESULT
	--,AS_LOCATOR
	,PARAMETER_NAME
	,DATA_TYPE
	,CHARACTER_MAXIMUM_LENGTH
	--,CHARACTER_OCTET_LENGTH
	--,COLLATION_CATALOG
	--,COLLATION_SCHEMA
	--,COLLATION_NAME
	--,CHARACTER_SET_CATALOG
	--,CHARACTER_SET_SCHEMA
	--,CHARACTER_SET_NAME
	,NUMERIC_PRECISION
	,NUMERIC_PRECISION_RADIX
	,NUMERIC_SCALE
	,DATETIME_PRECISION
	--,INTERVAL_TYPE
	--,INTERVAL_PRECISION
	--,USER_DEFINED_TYPE_CATALOG
	--,USER_DEFINED_TYPE_SCHEMA
	--,USER_DEFINED_TYPE_NAME
	--,SCOPE_CATALOG
	--,SCOPE_SCHEMA
	--,SCOPE_NAME
FROM INFORMATION_SCHEMA.PARAMETERS 
WHERE SPECIFIC_NAME 
NOT IN 
(
	'dt_droppropertiesbyid'
	,'dt_droppropertiesbyid'
	,'dt_dropuserobjectbyid'
	,'dt_generateansiname'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop_u'
	,'dt_getobjwithprop_u'
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid_u'
	,'dt_getpropertiesbyid_u'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid_u'
	,'dt_setpropertybyid_u'
	,'dt_setpropertybyid_u'
	,'dt_setpropertybyid_u'

	,'fn_diagramobjects'
	,'sp_helpdiagramdefinition'

	,'sp_alterdiagram'
	,'sp_alterdiagram'
	,'sp_alterdiagram'
	,'sp_alterdiagram'
	,'sp_dropdiagram'
	,'sp_dropdiagram'
	,'sp_helpdiagrams'
	,'sp_helpdiagrams'
	,'sp_renamediagram'
	,'sp_renamediagram'
	,'sp_renamediagram'
) 

ORDER BY SPECIFIC_NAME, ORDINAL_POSITION 
";


            //System.Data.DataTable dt = Settings.DAL.GetDataTable(strGetTablesAndViewsSQL);
            System.Data.DataTable dt2 = Settings.Source.GetDataTable(strSQL);
            dgvDisplayData.DataSource = dt2;
        } // End Sub btnOK_Click



        // http://msdn.microsoft.com/en-us/library/ms176074.aspx

        // GetDefaultParams("dbo", "usp_test1");
        public static List<string> GetParameterDefinition(string ObjectSchema, string ObjectName)
        {
            List<string> ls = new List<string>();
            List<string> lsParams = GetParameterNames(ObjectSchema, ObjectName);

            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            int iLastParameterIndex = lsParams.Count - 1;
            if (lsParams.Count < 1)
                return ls;

            string strLastParameterName = lsParams[iLastParameterIndex];


            // "schema unquoted"   ==> dbo.
            // "schema quoted" ==> @"""dbo""."
            // "schema bracketed" ==> @"[dbo]."
            // "no schema schema"


            string strProcedureDefinition = (@"   

-- this is a test
/*
this is another test

n*/
-- Hello
CREATE PROC dbo.usp_test1
(
    @a UNIQUEIDENTIFIER = NULL,
@i as xml = N'
a
b
c
e
d
f
g
',
    @b DATETIME = '2010''0101',
    @c DATETIME = DEFAULT,
    @d BIT = 1,
    @e BIT,
    @k INT = 1,
    @f BIT = 0, @g NVARCHAR(MAX) = '23235',
    @h INT = 3,
    @j AS DECIMAL(10,2) = DEFAULT
)
WITH RECOMPILE 
FOR REPLICATION 

AS
SELECT 
");

            using (System.Data.IDbCommand cmd = Settings.Source.CreateCommand("SELECT OBJECT_DEFINITION(OBJECT_ID(@__in_objectname)) "))
            {
                Settings.Source.AddParameter(cmd, "__in_objectname", Settings.Source.QuoteSchema(ObjectSchema) + "." + Settings.Source.QuoteObjectName(ObjectName));
                strProcedureDefinition = Settings.Source.ExecuteScalar<string>(cmd);
            } // End System.Data.IDbCommand cmd 


            strProcedureDefinition = strProcedureDefinition.Replace("''", "\u0007");
            // strProcedureDefinition = System.Text.RegularExpressions.Regex.Replace(strProcedureDefinition, "('.*?')", "", System.Text.RegularExpressions.RegexOptions.Singleline);
            strProcedureDefinition = System.Text.RegularExpressions.Regex.Replace(strProcedureDefinition, "('.*?')", new System.Text.RegularExpressions.MatchEvaluator(
                delegate(System.Text.RegularExpressions.Match m)
                {
                    string strId = "#@#" + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + "#@#";
                    dict.Add(strId, m.Value);
                    return strId;
                })
                , System.Text.RegularExpressions.RegexOptions.Singleline
            );

            strProcedureDefinition = RemoveComments(strProcedureDefinition);


            foreach (string strKey in dict.Keys)
            {
                strProcedureDefinition = strProcedureDefinition.Replace(strKey, dict[strKey]);
            } // Next strKey 
            dict.Clear();

            strProcedureDefinition = strProcedureDefinition.Replace("\u0007", "''");

            strProcedureDefinition = NormalizeCreateProcedure(strProcedureDefinition);
            string strCreateProcedureStatement = GetRightCombination(strProcedureDefinition, ObjectSchema, ObjectName);
            if (string.IsNullOrEmpty(strCreateProcedureStatement))
            {
                return null;
            }
                
            
            // int iCreateProcedurePos = strProcedureDefinition.IndexOf(strCreateProcedureStatement);
            int iCreateProcedurePos = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(strProcedureDefinition, strCreateProcedureStatement, System.Globalization.CompareOptions.IgnoreCase);
            if (iCreateProcedurePos == -1)
                return ls;


            strProcedureDefinition = strProcedureDefinition.Substring(iCreateProcedurePos + strCreateProcedureStatement.Length, strProcedureDefinition.Length - iCreateProcedurePos - strCreateProcedureStatement.Length).Trim();
            if (strProcedureDefinition.StartsWith("("))
                strProcedureDefinition = strProcedureDefinition.Substring(1);


            strProcedureDefinition = strProcedureDefinition.Replace("''", "\u0007");
            strProcedureDefinition = System.Text.RegularExpressions.Regex.Replace(strProcedureDefinition, "('.*?')", new System.Text.RegularExpressions.MatchEvaluator(
                delegate(System.Text.RegularExpressions.Match m)
                {
                    string strId = "#@#" + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + "#@#";
                    dict.Add(strId, m.Value);
                    return strId;
                })
                , System.Text.RegularExpressions.RegexOptions.Singleline
            );


            //strProcedureDefinition = System.Text.RegularExpressions.Regex.Replace(strProcedureDefinition, "(\\(.*?\\))", "", System.Text.RegularExpressions.RegexOptions.Singleline);
            strProcedureDefinition = System.Text.RegularExpressions.Regex.Replace(strProcedureDefinition, "(\\(.*?\\))", new System.Text.RegularExpressions.MatchEvaluator(
                delegate(System.Text.RegularExpressions.Match m)
                {
                    string strId = "#@#" + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + System.Guid.NewGuid().ToString().Replace("-", "") + "#@#";
                    dict.Add(strId, m.Value);
                    return strId;
                })
                , System.Text.RegularExpressions.RegexOptions.Singleline
            );


            strProcedureDefinition = NormalizeWhitespace(strProcedureDefinition); // +Trim
            strProcedureDefinition = NormalizeEqual(NormalizeCloseBracket(NormalizeOpenBracket(NormalizeComma(strProcedureDefinition))));
            // Console.WriteLine(strProcedureDefinition);

            string[] ParameterDefs = strProcedureDefinition.Split(',');

            string strProcedureRest = ParameterDefs[iLastParameterIndex];
            if (strProcedureRest.StartsWith(strLastParameterName + " as ", StringComparison.InvariantCultureIgnoreCase))
                strProcedureRest = strLastParameterName + strProcedureRest.Substring((strLastParameterName + " as").Length, strProcedureRest.Length - (strLastParameterName + " as").Length);

            int iAsIndex = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(strProcedureRest,"as ",System.Globalization.CompareOptions.IgnoreCase);
            if (iAsIndex != -1)
                strProcedureRest = strProcedureRest.Substring(0, iAsIndex);

            int iReplicationIndex = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(strProcedureRest,"for replication ",System.Globalization.CompareOptions.IgnoreCase);
            if (iReplicationIndex != -1)
                strProcedureRest = strProcedureRest.Substring(0, iReplicationIndex);

            int iWithIndex = System.Globalization.CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(strProcedureRest,"with ",System.Globalization.CompareOptions.IgnoreCase);
            if (iWithIndex != -1)
                strProcedureRest = strProcedureRest.Substring(0, iWithIndex);

            if (strProcedureRest.EndsWith(")"))
                strProcedureRest = strProcedureRest.Substring(0, strProcedureRest.Length - 1);

            ParameterDefs[iLastParameterIndex] = strProcedureRest;


            for (int i = 0; i <= iLastParameterIndex; ++i)
            {
                foreach (string strKey in dict.Keys)
                {
                    ParameterDefs[i] = ParameterDefs[i].Replace(strKey, dict[strKey]);
                } // Next strKey 

                // ParameterDefs[i] = ParameterDefs[i].Replace("\u0007", "''");
                ls.Add(ParameterDefs[i].Replace("\u0007", "''"));
            } // Next i

            return ls;
        } // End Function GetParameterDefinition 


        public static List<string> GetParameterNames(string ObjectSchema, string SpecificNameOfRoutine)
        {
            List<string> ls;

            string strGetParametersSQL = @"

--SELECT 
--	 --SPECIFIC_CATALOG
--	--,SPECIFIC_SCHEMA
--	 SPECIFIC_NAME
--	,ORDINAL_POSITION
--	,PARAMETER_MODE
--	,IS_RESULT
--	--,AS_LOCATOR
--	,PARAMETER_NAME
--	,DATA_TYPE
--	,CHARACTER_MAXIMUM_LENGTH
--	--,CHARACTER_OCTET_LENGTH
--	--,COLLATION_CATALOG
--	--,COLLATION_SCHEMA
--	--,COLLATION_NAME
--	--,CHARACTER_SET_CATALOG
--	--,CHARACTER_SET_SCHEMA
--	--,CHARACTER_SET_NAME
--	,NUMERIC_PRECISION
--	,NUMERIC_PRECISION_RADIX
--	,NUMERIC_SCALE
--	,DATETIME_PRECISION
--	--,INTERVAL_TYPE
--	--,INTERVAL_PRECISION
--	--,USER_DEFINED_TYPE_CATALOG
--	--,USER_DEFINED_TYPE_SCHEMA
--	--,USER_DEFINED_TYPE_NAME
--	--,SCOPE_CATALOG
--	--,SCOPE_SCHEMA
--	--,SCOPE_NAME
--FROM INFORMATION_SCHEMA.PARAMETERS 



SELECT 
    PARAMETER_NAME 
FROM INFORMATION_SCHEMA.PARAMETERS 
WHERE (1=1) 

AND SPECIFIC_SCHEMA = @__in_specific_schema 
AND SPECIFIC_NAME = @__in_specific_name 

AND SPECIFIC_NAME NOT IN 
(
	 'dt_dropuserobjectbyid' 
	,'dt_generateansiname'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop_u'
	
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid_u'
	,'dt_getpropertiesbyid_u'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid_u'
	,'dt_droppropertiesbyid' 
	
	,'fn_diagramobjects'
	,'sp_helpdiagramdefinition'
	,'sp_alterdiagram'
	,'sp_dropdiagram'
	,'sp_dropdiagram'
	,'sp_helpdiagrams'
	,'sp_renamediagram'
) 

ORDER BY SPECIFIC_NAME, ORDINAL_POSITION 
";

            using (System.Data.IDbCommand cmd = Settings.Source.CreateCommand(strGetParametersSQL))
            {
                Settings.Source.AddParameter(cmd, "@__in_specific_schema", ObjectSchema);
                Settings.Source.AddParameter(cmd, "@__in_specific_name", SpecificNameOfRoutine);

                ls = Settings.Source.GetList<string>(cmd);
            } // End Using System.Data.IDbCommand cmd 

            return ls;
        } // End Function GetParameterNames 


        public static List<string> GetParametersForExecution(string ObjectSchema, string SpecificNameOfRoutine)
        {
            List<string> ls;

            string strGetParametersSQL = @"
SELECT 
	PARAMETER_NAME + 
	CASE WHEN PARAMETER_MODE LIKE '%OUT%' 
		THEN ' OUTPUT' 
		ELSE '' 
	END AS CallParams 
FROM INFORMATION_SCHEMA.PARAMETERS 
WHERE (1=1) 

AND SPECIFIC_SCHEMA = @__in_specific_schema 
AND SPECIFIC_NAME = @__in_specific_name 

AND SPECIFIC_NAME NOT IN 
(
	 'dt_dropuserobjectbyid' 
	,'dt_generateansiname'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop_u'
	
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid_u'
	,'dt_getpropertiesbyid_u'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid_u'
	,'dt_droppropertiesbyid' 
	
	,'fn_diagramobjects'
	,'sp_helpdiagramdefinition'
	,'sp_alterdiagram'
	,'sp_dropdiagram'
	,'sp_dropdiagram'
	,'sp_helpdiagrams'
	,'sp_renamediagram'
) 

ORDER BY SPECIFIC_NAME, ORDINAL_POSITION 
";

            using (System.Data.IDbCommand cmd = Settings.Source.CreateCommand(strGetParametersSQL))
            {
                Settings.Source.AddParameter(cmd, "@__in_specific_schema", ObjectSchema);
                Settings.Source.AddParameter(cmd, "@__in_specific_name", SpecificNameOfRoutine);

                ls = Settings.Source.GetList<string>(cmd);
            } // End Using System.Data.IDbCommand cmd 

            return ls;
        } // End Function GetParameterNames 



        public class ProcedureInformation
        {
            public string Schema;
            public string ProcedureName;
        }


        public static void ProcessRoutine()
        {
            string strSQL = @"
DECLARE @SQL national character varying(MAX) 
SET @SQL= ''

" + "SELECT @SQL= @SQL+ N'DROP PROCEDURE \"' + REPLACE(SPECIFIC_SCHEMA, N'\"', N'\"\"') + N'\".\"' + REPLACE(SPECIFIC_NAME, N'\"', N'\"\"') + N'\"; '" + @"
FROM INFORMATION_SCHEMA.ROUTINES 

WHERE (1=1) 
AND ROUTINE_TYPE = 'PROCEDURE' 
AND ROUTINE_NAME NOT IN 
(
     'dt_adduserobject'
    ,'dt_droppropertiesbyid'
    ,'dt_dropuserobjectbyid'
    ,'dt_generateansiname'
    ,'dt_getobjwithprop'
    ,'dt_getobjwithprop_u'
    ,'dt_getpropertiesbyid'
    ,'dt_getpropertiesbyid_u'
    ,'dt_setpropertybyid'
    ,'dt_setpropertybyid_u'
    ,'dt_verstamp006'
    ,'dt_verstamp007'

    ,'sp_helpdiagrams'
    ,'sp_creatediagram'
    ,'sp_alterdiagram'
    ,'sp_renamediagram'
    ,'sp_dropdiagram'

    ,'sp_helpdiagramdefinition'
    ,'fn_diagramobjects'
    ,'sp_upgraddiagrams'
) 

-- PRINT @SQL
EXEC(@SQL) 
";
            Settings.Target.ExecuteNonQuery(strSQL);


            strSQL = @"
SELECT 
	-- SPECIFIC_NAME 
	  ROUTINE_SCHEMA AS ""Schema"" 
	 ,ROUTINE_NAME AS ProcedureName 
	--,ROUTINE_TYPE
	--,DATA_TYPE -- NULL, TABLE, scalar
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE (1=1) 

AND ROUTINE_TYPE = 'PROCEDURE' 

AND ROUTINE_NAME NOT IN 
(
	 'dt_adduserobject'
	,'dt_droppropertiesbyid'
	,'dt_dropuserobjectbyid'
	,'dt_generateansiname'
	,'dt_getobjwithprop'
	,'dt_getobjwithprop_u'
	,'dt_getpropertiesbyid'
	,'dt_getpropertiesbyid_u'
	,'dt_setpropertybyid'
	,'dt_setpropertybyid_u'
	,'dt_verstamp006'
	,'dt_verstamp007'

	,'sp_helpdiagrams'
	,'sp_creatediagram'
	,'sp_alterdiagram'
	,'sp_renamediagram'
	,'sp_dropdiagram'

	,'sp_helpdiagramdefinition'
	,'fn_diagramobjects'
,'sp_upgraddiagrams'
) 

ORDER BY ROUTINE_TYPE DESC, CASE WHEN DATA_TYPE = 'table' THEN 0 ELSE 1 END, DATA_TYPE, ROUTINE_NAME, SPECIFIC_NAME 
";

            List<string> lsIgnoreThoseProcedures = new List<string>();
            lsIgnoreThoseProcedures.Add("sp_AP_getCalcFlaechen_Old_deletmeifoK");
            lsIgnoreThoseProcedures.Add("_sp_AP_ApertureDWGCache_Wincasa_Temp");
            lsIgnoreThoseProcedures.Add("sp_DMS_getObjektdaten");
            lsIgnoreThoseProcedures.Add("sp_VWS_DATA_Zeichnungsauswahl_Old");
            lsIgnoreThoseProcedures.Add("_sp_AP_ApertureDWGCache_Wincasa_Temp");
            lsIgnoreThoseProcedures.Add("_sp_AP_ApertureDWGCache_Wincasa_Temp");
            lsIgnoreThoseProcedures.Add("_sp_AP_ApertureDWGCache_Wincasa_Temp");


            foreach (ProcedureInformation pi in Settings.Source.GetList<ProcedureInformation>(strSQL))
            {
                // if (StringComparer.InvariantCultureIgnoreCase.Equals(pi.ProcedureName, "sp_Bookings_By_Code")) Console.WriteLine("debug this");
                
                List<string> lsParamsDefinition = GetParameterDefinition(pi.Schema, pi.ProcedureName);
                List<string> lsParamsForExecution = GetParametersForExecution(pi.Schema, pi.ProcedureName);

                if (lsParamsDefinition == null)
                {
                    bool bNoBreak = false;
                    foreach (string str in lsIgnoreThoseProcedures)
                    {
                        if (StringComparer.InvariantCultureIgnoreCase.Equals(pi.ProcedureName, str))
                        {
                            bNoBreak = true;
                            break;
                        }
                            
                    }

                    if (bNoBreak) continue; // For debugging

                    continue;
                } // End if (lsParamsDefinition == null)
                
                string strParamsDefinition = "         " + string.Join(" " + Environment.NewLine + "            ,", lsParamsDefinition.ToArray());
                string strExecutionParams = "      " + string.Join(" " + Environment.NewLine + "            ,", lsParamsForExecution.ToArray());


                System.Diagnostics.Debug.WriteLine(strExecutionParams);
                System.Diagnostics.Debug.WriteLine(strParamsDefinition);

                string strProcedureTemplate = string.Format(@"
CREATE PROCEDURE {3}.{0} 
    {4} 
AS 
BEGIN 
	DECLARE @RC int 
	EXECUTE @RC = {1}.{2}.{3}.{0} 
       {5} 
	; 
	
	RETURN @RC 
END 

", pi.ProcedureName, Settings.RemoteServer, Settings.Source.DefaultDatabase, pi.Schema, strParamsDefinition, strExecutionParams);

                System.Diagnostics.Debug.WriteLine(strProcedureTemplate);
                Settings.Target.ExecuteNonQuery(strProcedureTemplate);
            } // Next ProcedureInformation pi


        }


        public static void AllowRPC()
        {
            Settings.Target.ExecuteNonQuery(string.Format(@"
-- http://blog.sqlauthority.com/2007/10/18/sql-server-2005-fix-error-msg-7411-level-16-state-1-server-is-not-configured-for-rpc/ 
EXEC master.dbo.sp_serveroption @server=N'{0}', @optname=N'rpc', @optvalue=N'true'; 
EXEC master.dbo.sp_serveroption @server=N'{0}', @optname=N'rpc out', @optvalue=N'true'; 
"
, Settings.Target.SqlEscapeString(Settings.RemoteServer)
            ));

        } // End Sub AllowRPC


    } // End Class frmMain : Form


} // End Namespace DatabaseLinker
