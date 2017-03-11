using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TableStorageWebAPISignalR.Models;

namespace TableStorageWebAPISignalR.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        //GET api/values
        //public List<Employee> Get()
        //{
        //    CloudTable table = Stopwatch.Initializer();

        //    return new EmployeeDatabase();
        //}

        public StopwatchEntity Get(string UserName)
        {
            StopwatchEntity swe = Stopwatch.GetLoggedInStopwatch(UserName);
            return swe;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post([FromUri] string UserName)
        {

            bool IsInserted = Stopwatch.StartStopWatch(UserName);
            if (IsInserted)
            {
                return Request.CreateResponse(HttpStatusCode.Created);

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
