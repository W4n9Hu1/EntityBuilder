using Service;

namespace EntityBuilderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "";
            string tableName = "";
            var mysql = new MysqlService(connStr, tableName);
            string entitystr = mysql.BuildEntityStr();
            string selectstr = mysql.BuildSelectStr();
            string insertstr = mysql.BuildInsertStr();
        }
    }
}
