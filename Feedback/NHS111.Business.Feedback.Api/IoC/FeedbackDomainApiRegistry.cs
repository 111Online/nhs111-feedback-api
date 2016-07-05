using NHS111.Domain.Feedback.IoC;
using StructureMap;
using StructureMap.Graph;

namespace NHS111.Business.Feedback.Api.IoC
{
    public class FeedbackDomainApiRegistry : Registry
    {
        public FeedbackDomainApiRegistry()
        {
            IncludeRegistry<FeedbackDomainRegistry>();
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}