using System;
using NHS111.Utils.Configuration;

namespace NHS111.Domain.Feedback.Convertors
{
    public class FeedbackConverter : IDataConverter<Models.Feedback>
    {
        public const string IDENTIFIER = "rowId";
        public const string FEEDBACKTEXT_FIELDNAME = "feedbackText";
        public const string PAGE_ID_FIELDNAME = "pageId";
        public const string RATING_FIELDNAME = "rating";
        public const string FEEDBACK_DATETIME_FIELDNAME = "feedbackDate";
        public const string FEEDBACK_DATA_FIELDNAME = "feedbackData";
        public const string USERID_FIELDNAME = "sessionId";
        public const string EMAIL_ADDRESS_FIELDNAME = "email";

        public string[] Fields()
        {
            return new[]
                {
                    IDENTIFIER,
                    FEEDBACKTEXT_FIELDNAME, 
                    PAGE_ID_FIELDNAME, 
                    RATING_FIELDNAME, 
                    FEEDBACK_DATETIME_FIELDNAME,
                    FEEDBACK_DATA_FIELDNAME, 
                    USERID_FIELDNAME, 
                    EMAIL_ADDRESS_FIELDNAME
                };
        }

        public Models.Feedback Convert(IManagedDataReader managedDataReader)
        {
            var dataReader = managedDataReader.DataReader;
            var feedback = new Models.Feedback();

            if (dataReader[IDENTIFIER] != null
                    && dataReader[IDENTIFIER] != DBNull.Value)
                feedback.Id = dataReader[IDENTIFIER].ToString();

            if (dataReader[FEEDBACKTEXT_FIELDNAME] != null
                    && dataReader[FEEDBACKTEXT_FIELDNAME] != DBNull.Value)
                feedback.Text = dataReader[FEEDBACKTEXT_FIELDNAME].ToString();

            if (dataReader[PAGE_ID_FIELDNAME] != null
                    && dataReader[PAGE_ID_FIELDNAME] != DBNull.Value)
                feedback.PageId = dataReader[PAGE_ID_FIELDNAME].ToString();

            if (dataReader[FEEDBACK_DATA_FIELDNAME] != null
                    && dataReader[FEEDBACK_DATA_FIELDNAME] != DBNull.Value)
                feedback.JSonData = dataReader[FEEDBACK_DATA_FIELDNAME].ToString();

            if (dataReader[USERID_FIELDNAME] != null
                    && dataReader[USERID_FIELDNAME] != DBNull.Value)
                feedback.UserId = dataReader[USERID_FIELDNAME].ToString();

            if (dataReader[RATING_FIELDNAME] != null
                 && dataReader[RATING_FIELDNAME] != DBNull.Value)
                feedback.Rating = System.Convert.ToInt32(dataReader[RATING_FIELDNAME].ToString());

            if (dataReader[FEEDBACK_DATETIME_FIELDNAME] != null
                    && dataReader[FEEDBACK_DATETIME_FIELDNAME] != DBNull.Value)
                feedback.DateAdded = DateTime.Parse(dataReader[FEEDBACK_DATETIME_FIELDNAME].ToString());

            if (dataReader[EMAIL_ADDRESS_FIELDNAME] != null
                    && dataReader[EMAIL_ADDRESS_FIELDNAME] != DBNull.Value)
                feedback.EmailAddress = dataReader[EMAIL_ADDRESS_FIELDNAME].ToString();

            return feedback;
        }

        public StatementParameters Convert(Models.Feedback feedback)
        {
            var parameters = new StatementParameters();
            if (!String.IsNullOrEmpty(feedback.Id))
                parameters.Add(IDENTIFIER, feedback.Id);

            if (!String.IsNullOrEmpty(feedback.UserId))
                parameters.Add(USERID_FIELDNAME, feedback.UserId);

            if (!String.IsNullOrEmpty(feedback.Text))
                parameters.Add(FEEDBACKTEXT_FIELDNAME, feedback.Text);

            if (!String.IsNullOrEmpty(feedback.PageId))
                parameters.Add(PAGE_ID_FIELDNAME, feedback.PageId);

            if (!String.IsNullOrEmpty(feedback.JSonData))
                parameters.Add(FEEDBACK_DATA_FIELDNAME, feedback.JSonData);

            if (feedback.DateAdded != DateTime.MinValue)
                parameters.Add(FEEDBACK_DATETIME_FIELDNAME, feedback.DateAdded);

            if (feedback.Rating.HasValue)
                parameters.Add(RATING_FIELDNAME, feedback.Rating.Value.ToString());

            if (!String.IsNullOrEmpty(feedback.EmailAddress))
                parameters.Add(EMAIL_ADDRESS_FIELDNAME, feedback.EmailAddress);

            return parameters;
        }
    }

    public interface IDataConverter<T>
    {
        T Convert(IManagedDataReader dataReader);
        StatementParameters Convert(T objectToConvert);
        string[] Fields();
    }
}
