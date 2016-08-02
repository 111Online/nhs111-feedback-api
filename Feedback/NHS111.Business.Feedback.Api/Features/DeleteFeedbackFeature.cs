namespace NHS111.Business.Feedback.Api.Features
{
    using Utils.FeatureToggle;

    public class DeleteFeedbackFeature : BaseFeature, IDeleteFeedbackFeature
    {
        public DeleteFeedbackFeature()
        {
            DefaultSettingStrategy = new DisabledByDefaultSettingStrategy();
        }
    }

    public interface IDeleteFeedbackFeature : IFeature { }
}