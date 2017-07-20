using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NHS111.Business.Feedback.Api.Features;
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("add")]
        public async Task<HttpResponseMessage> AddFeedback(Domain.Feedback.Models.Feedback feedback)
        {
            await _feedbackRepository.Add(feedback);
            return Request.CreateResponse(System.Net.HttpStatusCode.Created, feedback);
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("delete/{partitionKey}/{rowKey}")]
        public async Task<HttpResponseMessage> DeleteFeedback(string partitionKey, string rowKey)
        {
            DeleteFeedbackFeature featureToggle = new DeleteFeedbackFeature();
            HttpResponseMessage response;

            if (featureToggle.IsEnabled)
            {
                var result = await _feedbackRepository.Delete(partitionKey, rowKey);

                if (result >= 0)
                {
                    var responseMessage = string.Format("Row with partitionKey {0} and rowKey {1} was successfully deleted.", partitionKey, rowKey);
                    response = Request.CreateResponse(System.Net.HttpStatusCode.OK, responseMessage);
                }
                else
                {
                    var responseMessage = string.Format("Row with partitionKey {0} and rowKey {1} could not be found.", partitionKey, rowKey);
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("list")]
        public async Task<IEnumerable<Domain.Feedback.Models.Feedback>> List()
        {
            return await _feedbackRepository.List();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("list/{pageNumber:int}/{pageSize:int}")]
        public async Task<IEnumerable<Domain.Feedback.Models.Feedback>> ListWithPaging(int pageNumber, int pageSize)
        {
            return await _feedbackRepository.List(pageNumber, pageSize);
        }
    }
}
