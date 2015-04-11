using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Torrent_Search.Engine
{
    public abstract class SearchProvider
    {
        public virtual string name { get; protected set; }
        public virtual string searchurl { get; protected set; }
        public virtual string filelistingurl { get; protected set; }
        public virtual string homepage { get; protected set; }


        protected async Task<string> doSearchRequest(string query)
        {
            var wc = new WebClient();
            string r = await wc.DownloadStringTaskAsync(new Uri(string.Format(searchurl, query)));
            return r;
        }

        protected async Task<string> doFileListingRequest(string hash)
        {
            var wc = new WebClient();
            string r = await wc.DownloadStringTaskAsync(new Uri(string.Format(filelistingurl, hash)));
            return r;
        }

        public virtual async Task<List<String>> getFileListing(TorrentResult torrent)
        {
            //Default functionality if not overriden.
            var list = new List<String>();
            list.Add("No file listing available");
            return list;
        }

        public override string ToString()
        {
            //Override ToString method to show name in DGV.
            return this.name;
        }
    }
}
