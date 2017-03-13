using NHS111.Domain.Feedback.Repository;
using StructureMap;
using StructureMap.Graph;

namespace NHS111.Domain.Feedback.IoC
{
    public class FeedbackDomainRegistry : Registry
    {
        public FeedbackDomainRegistry()
        {
            For<IFeedbackRepository>().Use<FeedbackRepository>().Singleton();
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}
