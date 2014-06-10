using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHosting.Entities
{
    public class User
    {
        public virtual Guid Id { protected internal set; get; }
        public virtual string Login { get; set; }
    }
}
