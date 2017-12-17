using System.Linq;

namespace Mail2BigQuery
{
    internal class Program
    {
        private static void Main()
        {
            var conf = Config.Load();
            var pop3 = new Pop3Receiver(
                host: conf.Host,
                port: conf.Port,
                userName: conf.UserName,
                password: conf.Password);
            var importer = new BigQueryImporter();

            pop3.DownloadMessages()
                .Where(message => message.Subject.StartsWith("Test"))
                .Select(Model.Create)
                .Buffer(100)
                .ForEach(models => importer.Import(conf.ProjectId, conf.DatasetName, conf.TableName, models));
        }
    }
}