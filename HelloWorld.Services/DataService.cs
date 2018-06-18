using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.Model;

namespace HelloWorld.Services
{
    public class DataService : IDataService
    {
        private IDbContext dbContext;
        public DataService(IDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public Data WriteText()
        {
            var data = this.dbContext.Datas.FirstOrDefault();
            return data;
        }
    }
}
