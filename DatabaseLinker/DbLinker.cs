
using System;
using System.Collections.Generic;


namespace DatabaseLinker
{


    public class DbLinker
    {


        public static void LinkTables()
        {
            string strDrop = @"
DECLARE @sql VARCHAR(MAX)='';
SELECT @sql = @sql + 'DROP VIEW [' + TABLE_NAME + ']; ' FROM INFORMATION_SCHEMA.VIEWS WHERE table_name NOT IN ( 'dtproperties', 'sysdiagrams' ) 
PRINT @sql 
EXEC(@sql); 
";
            Settings.Target.ExecuteNonQuery(strDrop);

            List<string> lsTables = Settings.Source.GetList<string>(@"
SELECT 
    TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE' 

AND TABLE_NAME NOT IN 
( 
     'dtproperties' 
    ,'sysdiagrams' 
) 

ORDER BY TABLE_TYPE, TABLE_NAME 
");

            CreateLinkedViews(lsTables);
        } // End Sub LinkTables



        public static void LinkViews()
        {
            List<string> lsViews = Settings.Source.GetList<string>(@"
SELECT 
    TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'VIEW' 

AND TABLE_NAME NOT IN 
( 
     'dtproperties' 
    ,'sysdiagrams' 
) 

ORDER BY TABLE_TYPE, TABLE_NAME 
");

            CreateLinkedViews(lsViews);
        } // End Sub LinkedViews



        public enum ObjectType_t : int
        {
            TABLE,
            VIEW,
            TABLE_VALUED_FUNCTION,
        }


        public static List<string> GetColumns(string ObjectName, ObjectType_t ObjectType)
        {
            List<string> ls = null;
            string strSQL = null;

            switch (ObjectType)
            {
                case ObjectType_t.TABLE:
                case ObjectType_t.VIEW:
                    strSQL = @"
SELECT 
	 COLUMN_NAME 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = @__in_tablename 

ORDER BY TABLE_NAME, ORDINAL_POSITION 
";
                    break;
                case ObjectType_t.TABLE_VALUED_FUNCTION:
                    strSQL = @"
SELECT 
	-- --TABLE_CATALOG
	----,TABLE_SCHEMA
	-- TABLE_NAME
	COLUMN_NAME
	--,ORDINAL_POSITION
	--,COLUMN_DEFAULT
	--,IS_NULLABLE
	--,DATA_TYPE
	--,CHARACTER_MAXIMUM_LENGTH
	----,CHARACTER_OCTET_LENGTH
	--,NUMERIC_PRECISION
	--,NUMERIC_PRECISION_RADIX
	--,NUMERIC_SCALE
	--,DATETIME_PRECISION
	----,CHARACTER_SET_CATALOG
	----,CHARACTER_SET_SCHEMA
	----,CHARACTER_SET_NAME
	----,COLLATION_CATALOG
	----,COLLATION_SCHEMA
	----,COLLATION_NAME
	----,DOMAIN_CATALOG
	----,DOMAIN_SCHEMA
	----,DOMAIN_NAME
FROM INFORMATION_SCHEMA.ROUTINE_COLUMNS 
WHERE TABLE_NAME = @__in_tablename 

ORDER BY TABLE_NAME, ORDINAL_POSITION 
";
                    break;

            }

            using (System.Data.IDbCommand cmd = Settings.Source.CreateCommand(strSQL))
            {
                Settings.Source.AddParameter(cmd, "@__in_tablename", ObjectName);
                ls = Settings.Source.GetList<string>(cmd);
            } // End Using cmd 

            return ls;
        } // End Function GetColumns


        public static void CreateLinkedViews(List<string> lsViewsOrTables)
        {
            foreach (string strThisTable in lsViewsOrTables)
            {
                List<string> lsColumns = GetColumns(strThisTable, ObjectType_t.TABLE);

                for (int i = 0; i < lsColumns.Count; ++i)
                {
                    lsColumns[i] = Settings.Target.QuoteColumnName(lsColumns[i]);
                } // Next i

                string strColumns = string.Join(Environment.NewLine + "    ,", lsColumns.ToArray());

                string strView = string.Format(@"
CREATE VIEW dbo.{0} 
AS 
SELECT 
     {1}
FROM {2}.{3}.{4}.{0} 
;", Settings.Target.QuoteTableName(strThisTable), strColumns, Settings.RemoteServer, Settings.Source.DefaultDatabase, Settings.Source.DefaultSchema);

                Settings.Target.ExecuteNonQuery(strView);
            } // Next strThisTable

        } // End Sub CreateLinkedViews 


    } // End Class DbLinker


} // End Namespace DatabaseLinker 
