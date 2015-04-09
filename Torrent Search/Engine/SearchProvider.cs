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

        public virtual List<TorrentResult> getSearchResults(string query)
        {           
            return null;
        }

        protected string getWebResponse(string query)
        {
            //Get JSON/XML etc. data using search URL + query
            var wc = new WebClient();
            return wc.DownloadString(new Uri(string.Format(searchurl, query)));            
        }
    }
}
