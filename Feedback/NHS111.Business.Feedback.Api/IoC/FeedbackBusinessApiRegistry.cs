using NHS111.Domain.Feedback.IoC;
using StructureMap;
using StructureMap.Graph;

namespace NHS111.Business.Feedback.Api.IoC
{
    public class FeedbackBusinessApiRegistry : Registry
    {
        public FeedbackBusinessApiRegistry()
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