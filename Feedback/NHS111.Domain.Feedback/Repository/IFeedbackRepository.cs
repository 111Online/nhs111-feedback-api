using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHS111.Domain.Feedback.Repository
{
    public interface IFeedbackRepository
    {
        Task<int> Add(Models.Feedback feedback);
        Task<int> Delete(string identifier);
        Task<IEnumerable<Models.Feedback>> List();
    }
}
