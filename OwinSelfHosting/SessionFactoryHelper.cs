using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using OwinSelfHosting.Entities;
using OwinSelfHosting.Mappings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHosting
{
    internal class SessionFactoryHelper
    {
        internal static ISessionFactory GetSessionFactory()
        {
            //fluently configure from hibernate config
            var fluentConfig = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString("Data Source=nhibernate.db;Version=3"));//.ShowSql());//);


            ////add fluent mappings, add conventions
            fluentConfig.Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>());

            Configuration nhConfig = null;

            fluentConfig.ExposeConfiguration(config =>
            {
                nhConfig = config;
            });


            var factory = fluentConfig.BuildSessionFactory();

            using (ISession session = factory.OpenSession())
            {
                var sb = new StringBuilder();
                //the key point is pass your session.Connection here
                new SchemaExport(nhConfig).Execute(true, true, false, session.Connection, new StringWriter(sb));
                session.Flush();

                //seed database
                var userNames = new[] { "user1", "user2", "user3" };

                var newUsers = userNames.Select(x => new User { Login = x });

                foreach(var user in newUsers)
                {
                    session.Save(user);
                }

                session.Flush();
                session.Close();
            }

            


            return factory;
        }
    }
}
