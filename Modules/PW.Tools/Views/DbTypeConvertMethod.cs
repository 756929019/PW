using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.Tools.Views
{
    class DbTypeConvertMethod
    {
        /// 
        /// 根据变量类型获取从数据库字段转换到该类型的转换语句
        /// 
        /// 
        private static void InitConvertMethod(FieldModel Field)
        {
            switch (Field.VarType)
            {
                case "string":
                    Field.ToVarConvertMethod = "Convert.ToString";
                    break;
                case "int":
                    Field.ToVarConvertMethod = "Convert.ToInt32";
                    break;
                case "DateTime":
                    Field.ToVarConvertMethod = "Convert.ToDateTime";
                    break;
                case "bool":
                    Field.ToVarConvertMethod = "Convert.ToBoolean";
                    break;
                case "float":
                    Field.ToVarConvertMethod = "Convert.ToSingle";
                    break;
                case "decimal":
                    Field.ToVarConvertMethod = "Convert.ToDecimal";
                    break;
                case "byte[]":
                    Field.ToVarConvertMethod = "(byte[])";
                    break;
                case "money":
                    Field.ToVarConvertMethod = "Convert.ToDecimal";
                    break;
                default:
                    throw new Exception(string.Format("变量类型（{0}）的转换方法未定义！", Field.VarType));
            }
        }


        /// 
        /// 初始化数据库实体类属性默认值
        /// 
        /// 数据库实体类属性
        private static void InitDefaultVarValue(FieldModel Field)
        {
            switch (Field.DbType)
            {
                case "char":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "nchar":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "varchar":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "nvarchar":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "text":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "ntext":
                    Field.DefaultValueVar = "String.Empty";
                    break;
                case "int":
                    Field.DefaultValueVar = "0";
                    break;
                case "bit":
                    Field.DefaultValueVar = "false";
                    break;
                case "datetime":
                    Field.DefaultValueVar = "DateTime.MinValue";
                    break;
                case "float":
                    Field.DefaultValueVar = "0";
                    break;
                case "decimal":
                    Field.DefaultValueVar = "0";
                    break;
                case "image":
                    Field.DefaultValueVar = "null";
                    break;
                case "money":
                    Field.DefaultValueVar = "0";
                    break;
                default:
                    throw new Exception(string.Format("数据库字段类型（{0}）未定义！", Field.DbType));
            }
        }

        /// 
        /// 获得数据访问参数的类型
        /// 
        /// 
        /// 
        private static string GetSqlParamTypeFromSqlDbType(string DbType)
        {
            switch (DbType.ToLower())
            {
                case "char":
                    return "SqlDbType.Char";
                case "nchar":
                    return "SqlDbType.NChar";
                case "varchar":
                    return "SqlDbType.VarChar";
                case "nvarchar":
                    return "SqlDbType.NVarChar";
                case "text":
                    return "SqlDbType.Text";
                case "ntext":
                    return "SqlDbType.NText";
                case "int":
                    return "SqlDbType.Int";
                case "bit":
                    return "SqlDbType.Bit";
                case "datetime":
                    return "SqlDbType.DateTime";
                case "float":
                    return "SqlDbType.Float";
                case "image":
                    return "SqlDbType.Image";
                case "decimal":
                    return "SqlDbType.Decimal";
                case "money":
                    return "SqlDbType.Money";
                default:
                    throw new Exception(string.Format("数据库字段类型（{0}）未定义！", DbType));
            }
        }

        /// 
        /// 获得属性变量类型
        /// 
        /// 
        /// 
        public static string GetVarTypeFromSqlDbType(string DbType)
        {
            switch (DbType.ToLower())
            {
                case "char":
                    return "string";
                case "nchar":
                    return "string";
                case "varchar":
                    return "string";
                case "nvarchar":
                    return "string";
                case "text":
                    return "string";
                case "ntext":
                    return "string";
                case "int":
                    return "int";
                case "bit":
                    return "bool";
                case "datetime":
                    return "DateTime";
                case "float":
                    return "float";
                case "image":
                    return "byte[]";
                case "decimal":
                    return "decimal";
                case "money":
                    return "decimal";
                case "numeric":
                    return "decimal";
                default:
                    throw new Exception(string.Format("数据库字段类型（{0}）未定义！", DbType));
            }
        }
    }
}
