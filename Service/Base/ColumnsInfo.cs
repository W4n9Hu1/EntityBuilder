namespace Service
{
    /// <summary>
    /// ColmunsInfo
    /// </summary>
    public class ColumnsInfo
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public string ColumnComment { get; set; }
    }
}
