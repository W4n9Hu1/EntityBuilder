using System;

namespace Service
{
    public class DbServiceContext
    {
        readonly IDbService _service;

        public DbServiceContext(IDbService service)
        {
            _service = service;
        }

        public string BuildEntityStr()
        {
            try
            {
                var columns = _service.GetColumnsInfo();
                var str = _service.BuildEntityStr(columns);
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
