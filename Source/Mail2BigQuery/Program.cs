using System.Linq;
using System.Threading;

namespace Mail2BigQuery
{
    internal class Program
    {
        private static void Main()
        {
            var pop3 = new Pop3Receiver(host: "host", port: 111, userName: "userName", password: "password");
            var importer = new BigQueryImporter();
            while (true)
            {
                pop3.DownloadMessages()
                    .Where(message => message.Subject.StartsWith("Test"))
                    .Select(Model.Create)
                    .Buffer(100)
                    .ForEach(models => importer.Import("projectId", "datasetName", "tableName", models));
                Thread.Sleep(300_000);
            }
        }
    }
}