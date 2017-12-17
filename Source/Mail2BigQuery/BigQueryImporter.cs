using Google.Apis.Auth.OAuth2;
using Google.Cloud.BigQuery.V2;
using System.Collections.Generic;
using System.Linq;

namespace Mail2BigQuery
{
    internal class BigQueryImporter
    {
        internal BigQueryImporter() : this(null)
        {
        }

        internal BigQueryImporter(string serviceKeyFile)
        {
            _serviceKeyFile = serviceKeyFile;
        }

        private readonly string _serviceKeyFile;

        internal void Import(string projectId, string datasetName, string tableName,
            IEnumerable<IBigQueryRow> source)
        {
            var client = CreateBigQueryClient(projectId, _serviceKeyFile);
            var dataset = client.GetDataset(datasetName);
            var table = dataset.GetTable(tableName);
            var rows = source.Select(x => new BigQueryInsertRow
            {
                x.ToDict()
            });
            table.InsertRows(rows);
        }

        private static BigQueryClient CreateBigQueryClient(string projectId, string serviceKeyFile) =>
            string.IsNullOrEmpty(serviceKeyFile)
                ? BigQueryClient.Create(projectId)
                : BigQueryClient.Create(projectId, GoogleCredential.FromFile(serviceKeyFile));
    }

    internal interface IBigQueryRow
    {
        IDictionary<string, object> ToDict();
    }
}