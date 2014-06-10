using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Diagnostics;

namespace OwinSelfHosting.Controllers
{
    public class TestController: ApiController
    {
        private readonly IHelloService _helloService;
        private readonly ISession _session;

        public TestController(IHelloService helloService, ISession session)
        {
            _helloService = helloService;
            _session = session; ;
        }

        public string Get()
        {
            return  _session.GetSessionImplementation().SessionId.ToString();
        }
    }
}
