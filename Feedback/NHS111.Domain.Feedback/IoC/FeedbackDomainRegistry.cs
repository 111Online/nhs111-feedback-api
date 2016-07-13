using NHS111.Domain.Feedback.Convertors;
using NHS111.Domain.Feedback.Repository;
using NHS111.Utils.Configuration;
using StructureMap;
using StructureMap.Graph;

namespace NHS111.Domain.Feedback.IoC
{
    public class FeedbackDomainRegistry : Registry
    {
        public FeedbackDomainRegistry()
        {
            For<IFeedbackRepository>().Use<FeedbackRepository>().Singleton();
            For<ISqliteConfiguration>().Use<SqliteConfiguration>().Singleton();
            For<IConnectionManager>().Use<SqliteConnectionManager>().Singleton();
            For(typeof(IDataConverter<Models.Feedback>)).Use(typeof(FeedbackConverter));
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}
