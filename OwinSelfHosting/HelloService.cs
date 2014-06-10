using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHosting
{
    public class HelloService: IHelloService
    {
        private readonly Guid _id;

        public HelloService()
        {
            _id = Guid.NewGuid();
        }

        public string SayHello()
        {
            return _id.ToString();
        }
    }
}
