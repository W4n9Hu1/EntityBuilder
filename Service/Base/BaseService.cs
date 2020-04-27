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
        public IEnumerable<ColumnsInfo> _columns;

        public BaseService(string connStr, string tableName)
        {
            _connStr = connStr;
            _tableName = tableName;
        }

        public string BuildEntityStr()
        {
            string tableName = _columns.FirstOrDefault().TableName;
            tableName = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// " + tableName);
            sb.AppendLine("/// </summary>");
            sb.AppendLine($"public class {tableName}");
            sb.AppendLine("{");
            _columns.ToList().ForEach(n =>
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
                    case "decimal":
                        typeName = "decimal";
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

        public string BuildSelectStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            foreach (var item in _columns)
            {
                sb.AppendLine("t." + item.ColumnName + ",");
            }
            string sql = sb.ToString().Trim().TrimEnd(',');
            sql += $"\r\nfrom {_tableName} t";
            return sql;
        }

        public string BuildInsertStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO " + _tableName + " (");
            foreach (var item in _columns)
            {
                sb.AppendLine(item.ColumnName + ",");
            }
            string sql = sb.ToString().Trim().TrimEnd(',');
            sql += "\r\n) VALUES ( \r\n";
            sb = new StringBuilder();
            foreach (var item in _columns)
            {
                sb.AppendLine("@" + item.ColumnName + ",");
            }
            sql += sb.ToString().Trim().TrimEnd(',');
            sql += "\r\n);";
            return sql;
        }
    }
}
