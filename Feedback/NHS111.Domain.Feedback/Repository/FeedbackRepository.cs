using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NHS111.Domain.Feedback.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly CloudTable _table;

        public FeedbackRepository()
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            _table = tableClient.GetTableReference(CloudConfigurationManager.GetSetting("StorageTableReference"));
        }

        public Task<int> Add(Models.Feedback feedback)
        {
            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(feedback);
            // Execute the insert operation.
            var tableResult = _table.ExecuteAsync(insertOperation);
            return Task.Run(() => { return tableResult.Id; });
        }

        public Task<int> Delete(string partitionKey, string rowKey)
        {
            // Create a retrieve operation that expects a customer entity.
            var retrieveOperation = TableOperation.Retrieve<Models.Feedback>(partitionKey, rowKey);
            // Execute the operation.
            var retrievedResult = _table.ExecuteAsync(retrieveOperation);
            // Assign the result to a Feedback.
            var deleteEntity = (Models.Feedback)retrievedResult.Result.Result;
            // Create the Delete TableOperation.
            if (deleteEntity != null)
            {
                var deleteOperation = TableOperation.Delete(deleteEntity);
                // Execute the operation.
                var tableResult = _table.ExecuteAsync(deleteOperation);
                return Task.Run(() => tableResult.Id);
            }
            return Task.Run(() => -1);
        }

        public Task<int> Delete(int identifier)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Feedback>> List(int pageNumber = 0, int pageSize = 1000)
        {
            // Construct the query operation for all customer entities.
            TableQuery<Models.Feedback> query = new TableQuery<Models.Feedback>();
            var results = _table.ExecuteQueryAsync<Models.Feedback>(query);
            return Task.Run(() =>
            {
                if (results.Result == null || !results.Result.Any()) return new List<Models.Feedback>();
                var result = results.Result.OrderByDescending(f => f.DateAdded);
                var feedback = (pageNumber > 0) ? result.Skip((pageNumber - 1) * pageSize).Take(pageSize) : result.Take(pageSize);
                return feedback;
            });
        }
    }

    public static class AzureHelper
    {
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(this CloudTable table, TableQuery<T> query, CancellationToken ct = default(CancellationToken), Action<IList<T>> onProgress = null) where T : ITableEntity, new()
        {

            var items = new List<T>();
            TableContinuationToken token = null;

            do
            {

                TableQuerySegment<T> seg = await table.ExecuteQuerySegmentedAsync<T>(query, token);
                token = seg.ContinuationToken;
                items.AddRange(seg);
                if (onProgress != null) onProgress(items);

            } while (token != null && !ct.IsCancellationRequested);

            return items;
        }
    }
}
