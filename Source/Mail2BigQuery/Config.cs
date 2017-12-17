using System;
using System.Configuration;

namespace Mail2BigQuery
{
    internal class Config
    {
        public static Config Load() => new Config();

        private Config()
        {
            Host = ConfigurationManager.AppSettings["pop3Host"];
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["pop3Port"]);
            UserName = ConfigurationManager.AppSettings["userName"];
            Password = ConfigurationManager.AppSettings["password"];
            ProjectId = ConfigurationManager.AppSettings["projectId"];
            DatasetName = ConfigurationManager.AppSettings["datasetName"];
            TableName = ConfigurationManager.AppSettings["tableName"];
        }

        public string Host { get; }
        public int Port { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ProjectId { get; }
        public string DatasetName { get; }
        public string TableName { get; }
    }
}