using MimeKit;
using System;
using System.Collections.Generic;

namespace Mail2BigQuery
{
    internal class Model : IBigQueryRow
    {
        internal static Model Create(MimeMessage message)
        {
            var model = new Model();
            return model;
        }

        private Model()
        {
        }

        public IDictionary<string, object> ToDict()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}