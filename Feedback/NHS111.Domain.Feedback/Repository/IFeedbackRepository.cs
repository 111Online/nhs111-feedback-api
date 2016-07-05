using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHS111.Domain.Feedback.Repository
{
    public interface IFeedbackRepository
    {
        Task<int> Add(Models.Feedback feedback);
        Task<IEnumerable<Models.Feedback>> List();
    }
}
