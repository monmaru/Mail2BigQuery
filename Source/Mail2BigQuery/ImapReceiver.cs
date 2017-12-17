using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System.Collections.Generic;

namespace Mail2BigQuery
{
    internal class ImapReceiver
    {
        internal ImapReceiver(string host, int port, string userName, string password)
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

        internal IEnumerable<MimeMessage> DownloadMessages(SearchQuery query)
        {
            using (var client = new ImapClient(new ProtocolLogger("imap.log")))
            {
                client.Connect(_host, _port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(_userName, _password);
                client.Inbox.Open(FolderAccess.ReadOnly);

                foreach (var uid in client.Inbox.Search(query))
                {
                    yield return client.Inbox.GetMessage(uid);
                }

                client.Disconnect(true);
            }
        }
    }
}