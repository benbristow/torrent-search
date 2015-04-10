using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Torrent_Search.Engine
{
    abstract class SearchProvider
    {
        public virtual string name { get; protected set; }
        public virtual string searchurl { get; protected set; }
        public virtual string homepage { get; protected set; }

        protected async Task<string> doWebRequest(string query)
        {
            var wc = new WebClient();
            string r = await wc.DownloadStringTaskAsync(new Uri(string.Format(searchurl, query)));
            return r;
        }
    }
}
