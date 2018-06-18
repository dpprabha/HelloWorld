using HelloWorld.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Services
{
    public interface IDbContext
    {
        IDbSet<Data> Datas { get; set; }
    }
}
