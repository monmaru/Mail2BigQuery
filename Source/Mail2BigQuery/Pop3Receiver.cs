using MailKit;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;
using System.Collections.Generic;

namespace Mail2BigQuery
{
    internal class Pop3Receiver
    {
        internal Pop3Receiver(string host, int port, string userName, string password)
        {
            _host = host;
            _port = port;
            _userName = userName;
            _password = password;
        }

        private readonly string _host;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;

        internal IEnumerable<MimeMessage> DownloadMessages()
        {
            using (var client = new Pop3Client(new ProtocolLogger("pop3.log")))
            {
                client.Connect(_host, _port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(_userName, _password);

                for (var i = 0; i < client.Count; i++)
                {
                    yield return client.GetMessage(i);
                    // mark the message for deletion
                    client.DeleteMessage(i);
                }

                client.Disconnect(true);
            }
        }
    }
}