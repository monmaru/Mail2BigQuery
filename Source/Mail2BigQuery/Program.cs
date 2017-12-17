using MailKit.Search;
using System;
using System.Linq;

namespace Mail2BigQuery
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var conf = Config.Load();
            var imap = new ImapReceiver(
                host: conf.Host,
                port: conf.Port,
                userName: conf.UserName,
                password: conf.Password);
            var bigQuery = new BigQueryImporter();
            var yesterday = DateTime.Today.AddDays(-1);
            var query = SearchQuery.SubjectContains("Test").And(SearchQuery.SentOn(yesterday));
            var models = imap.DownloadMessages(query).Select(Model.Create);
            bigQuery.Import(conf.ProjectId, conf.DatasetName, conf.TableName, models);
        }
    }
}