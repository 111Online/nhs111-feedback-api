using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHS111.Domain.Feedback.Repository
{
    public interface IFeedbackRepository
    {
        Task<int> Add(Models.Feedback feedback);
        Task<int> Delete(string partitionKey, string rowKey);
        Task<IEnumerable<Models.Feedback>> List(int pageNumber = 0, int pageSize = 1000);
    }
}
