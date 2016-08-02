using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NHS111.Business.Feedback.Api.Features;
using NHS111.Domain.Feedback.Models;
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
            var newId = await _feedbackRepository.Add(feedback);
            feedback.Id = newId;
            var response = Request.CreateResponse(System.Net.HttpStatusCode.Created, feedback);
            return response;
        }

        [HttpDelete]
        [Route("delete/{identifier}")]
        public async Task<HttpResponseMessage> DeleteFeedback(string identifier)
        {
            DeleteFeedbackFeature featureToggle = new DeleteFeedbackFeature();
            HttpResponseMessage response;

            if (featureToggle.IsEnabled)
            {
                var result = (DatabaseCode)await _feedbackRepository.Delete(identifier);

                if (result == DatabaseCode.Success)
                {
                    var responseMessage = string.Format("Row with id {0} was successfully deleted.", identifier);
                    response = Request.CreateResponse(System.Net.HttpStatusCode.OK, responseMessage);
                }
                else
                {
                    var responseMessage = string.Format("Row with id {0} could not be found.", identifier);
                    response = Request.CreateResponse(System.Net.HttpStatusCode.NotFound, responseMessage);
                }

                return response;
            }
            else
            {
                response = Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
                return response;
            }

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
