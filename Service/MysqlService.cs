using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Service
{
    public class MysqlService : BaseService, IDbService
    {
        public MysqlService(string connStr,string tableName) : base(connStr,tableName) { }

        public IEnumerable<ColumnsInfo> GetColumnsInfo()
        {
            string sql = $@"SELECT 
    TABLE_NAME TableName,
    COLUMN_NAME ColumnName,
    IS_NULLABLE IsNullable,
    DATA_TYPE DataType,
    COLUMN_COMMENT ColumnComment
FROM
    `information_schema`.`COLUMNS`
WHERE
    TABLE_NAME = '{_tableName}'";
            using (var conn = new MySqlConnection(_connStr))
            {
                var columns = conn.Query<ColumnsInfo>(sql);
                return columns;
            }
        }
    }
}
