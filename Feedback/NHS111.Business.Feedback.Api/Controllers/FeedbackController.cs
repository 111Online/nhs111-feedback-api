using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NHS111.Domain.Feedback.Repository;
using NHS111.Utils.Attributes;

namespace NHS111.Business.Feedback.Api.Controllers
{
    [LogHandleErrorForApi]
    public class FeedbackController : ApiController
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> AddFeedback(Domain.Feedback.Models.Feedback feedback)
        {
            await _feedbackRepository.Add(feedback);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.Created, feedback);
            return response;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<Domain.Feedback.Models.Feedback>> ListFeedback()
        {
            var result = await _feedbackRepository.List();
            return result;
        }

    }
}
