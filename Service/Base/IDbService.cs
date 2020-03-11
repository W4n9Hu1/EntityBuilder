using System.Collections.Generic;

namespace Service
{
    public interface IDbService
    {
        IEnumerable<ColumnsInfo> GetColumnsInfo();

         string BuildEntityStr(IEnumerable<ColumnsInfo> columns);
    }
}
