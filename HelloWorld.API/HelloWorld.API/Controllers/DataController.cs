using HelloWorld.Model;
using HelloWorld.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorld.API.Controllers
{
    public class DataController : ApiController
    {
        private DataService dataService;
        public DataController(IDbContext dbContext)
        {
            dataService = new DataService(dbContext);
        }

        [Route("api/Data")]
        public string Get()
        {            
            var result = dataService.WriteText();
            return result.Text;
        }
    }
}
