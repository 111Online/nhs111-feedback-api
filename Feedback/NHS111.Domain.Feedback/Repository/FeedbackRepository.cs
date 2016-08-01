using System.Collections.Generic;
using System.Threading.Tasks;
using NHS111.Domain.Feedback.Convertors;
using NHS111.Utils.Configuration;

namespace NHS111.Domain.Feedback.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IConnectionManager _sqliteConnectionManager;
        private readonly IDataConverter<Models.Feedback> _feedbackConverter;

        public FeedbackRepository(IConnectionManager sqlConnectionManager, IDataConverter<Models.Feedback> feedbackConverter)
        {
            _sqliteConnectionManager = sqlConnectionManager;
            _feedbackConverter = feedbackConverter;
        }

        public async Task<int> Add(Models.Feedback feedback)
        {
            var statementParameters = _feedbackConverter.Convert(feedback);
            var insertQuery = statementParameters.GenerateInsertStatement("feedback");
            return await _sqliteConnectionManager.ExecteNonQueryReturningIdAsync(insertQuery, statementParameters);
        }

        public async Task<int> Delete(string identifier)
        {
            string deleteStatement = string.Format("{0}{1}{2}{3}",
                "DELETE FROM feedback ",
                "WHERE rowId = ",
                identifier,
                ";");

            return await _sqliteConnectionManager.ExecteNonQueryAsync(deleteStatement, new StatementParameters());
        }

        public async Task<IEnumerable<Models.Feedback>> List()
        {
            string selectStatement = string.Format("{0}{1}{2}{3}", 
                "SELECT ",
                string.Join(",", _feedbackConverter.Fields()),
                " FROM feedback",
                " ORDER BY feedbackDate DESC");

            Task<List<Models.Feedback>> task = new Task<List<Models.Feedback>>
                (
                () =>
                {
                    var feedbackList = new List<Models.Feedback>();
                    using ( IManagedDataReader reader = _sqliteConnectionManager.GetReader(selectStatement, new StatementParameters()))
                    {
                        while (reader.Read())
                        {
                            feedbackList.Add(_feedbackConverter.Convert(reader));
                        }
                    }

                    return feedbackList;
                });
            task.Start();

            return await task;
        }
    }
}
