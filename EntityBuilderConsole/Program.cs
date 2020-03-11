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
            var context = new DbServiceContext(mysql);
            string str = context.BuildEntityStr();
        }
    }
}
