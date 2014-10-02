
using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseLinker 
{
    public class TableConverter
    {

        // TableConverter.TableCreateStatement(dt);
        public static string TableCreateStatement(System.Data.DataTable dt)
        {
            string strSQL = "CREATE TABLE " + dt.TableName + " " + Environment.NewLine + "( " + Environment.NewLine;

            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                System.Data.DataColumn col = dt.Columns[i];
                string sql_type = "";


                switch (col.DataType.Name)
                {
                    case "Guid":
                        sql_type = "uniqueidentifier";
                        break;
                    case "Int16":
                        sql_type = "smallinteger";
                        break;
                    case "Int32":
                        sql_type = "integer";
                        break;
                    case "Int64":
                        sql_type = "biginteger";
                        break;
                    case "Single":
                        sql_type = "real";
                        break;
                    case "Double":
                        sql_type = "float";
                        break;
                    case "Boolean":
                        sql_type = "bit";
                        break;
                    case "Char":
                        sql_type = "nchar(1)";
                        break;
                    case "String":
                        sql_type = "nvarchar(255)";
                        break;
                    case "DateTime":
                        sql_type = "datetime2";
                        break;
                    case "Byte[]":
                        sql_type = "varbinary(MAX)";
                        break;
                    case "Object":
                        sql_type = "sql_variant";
                        break;
                    default:
                        sql_type = col.DataType.Name;
                        break;
                }

                if (i == 0)
                    strSQL += "     " + col.ColumnName + " " + sql_type + " NULL";
                else
                    strSQL += "    ," + col.ColumnName + " " + sql_type + " NULL";

                strSQL += Environment.NewLine;
            }

            strSQL += ");" + Environment.NewLine;
            // Console.WriteLine(strSQL);

            return strSQL;
        }


    }
}
