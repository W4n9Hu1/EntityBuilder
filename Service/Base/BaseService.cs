using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class BaseService
    {
        public string _tableName;
        public string _connStr;

        public BaseService(string connStr, string tableName)
        {
            _connStr = connStr;
            _tableName = tableName;
        }

        public string BuildEntityStr(IEnumerable<ColumnsInfo> columns)
        {
            string tableName = columns.FirstOrDefault().TableName;
            tableName = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// " + tableName);
            sb.AppendLine("/// </summary>");
            sb.AppendLine($"public class {tableName}");
            sb.AppendLine("{");
            columns.ToList().ForEach(n =>
            {
                sb.AppendLine();
                if (!string.IsNullOrEmpty(n.ColumnComment))
                {
                    sb.AppendLine("\t/// <summary>");
                    sb.AppendLine("\t/// " + n.ColumnComment);
                    sb.AppendLine("\t/// </summary>");
                }
                string typeName = "ERROR";
                switch (n.DataType)
                {
                    case "bit":
                    case "int":
                    case "tinyint":
                    case "smallint":
                        typeName = "int";
                        break;
                    case "bigint":
                        typeName = "long";
                        break;
                    case "varchar":
                        typeName = "string";
                        break;
                    case "timestamp":
                    case "datetime":
                    case "date":
                        typeName = "DateTime";
                        break;
                }
                if (string.Equals(n.IsNullable, "YES", StringComparison.CurrentCultureIgnoreCase) && !new List<string> { "varchar" }.Contains(n.DataType))
                {
                    typeName += "?";
                }
                sb.AppendLine(string.Format("\tpublic {0} {1} {{ get; set; }}", typeName, n.ColumnName));
            });
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string BuildSelectStr(IEnumerable<ColumnsInfo> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            foreach (var item in columns)
            {
                sb.AppendLine("t." + item.ColumnName + ",");
            }
            sb.AppendLine("from " + _tableName + " t");
            return sb.ToString();
        }


        public string BuildInsertStr(IEnumerable<ColumnsInfo> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO " + _tableName + " (");
            foreach (var item in columns)
            {
                sb.AppendLine(item.ColumnName + ",");
            }
            sb.AppendLine(") VALUES (");
            foreach (var item in columns)
            {
                sb.AppendLine("@" + item.ColumnName + ",");
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
