using FluentNHibernate.Mapping;
using OwinSelfHosting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHosting.Mappings
{
    public class UserMap: ClassMap<User>
    {
        public UserMap()
        {
            Table("User");
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Login).Length(50);
        }
    }
}
