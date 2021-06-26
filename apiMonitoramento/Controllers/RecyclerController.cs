using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using clMonitoramento.Business;

namespace apiMonitoramento.Controllers
{
    public class RecyclerController : ApiController
    {
        #region Get

        [Route("api/recycler/status")]
        [HttpGet]
        public string GetRecyclerStatus()
        {
            return RecyclerBusiness.GetRecyclerStatus();
        }

        #endregion

        #region Post

        [Route("api/recycler/process/{days}")]
        [HttpPost]
        public HttpStatusCode RunRecycler(int days)
        {
            RecyclerBusiness.RunRecycler(days);
            return HttpStatusCode.Accepted;
        }

        #endregion
    }
}
