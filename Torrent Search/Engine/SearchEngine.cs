using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torrent_Search.Engine
{
    public class SearchEngine
    {
        public List<TorrentResult> getResults(string query)
        {
            //Create new results list
            var results = new List<TorrentResult>();

            //Search with strike
            var strike = new Providers.GetStrike.GetStrike();
            results.AddRange(strike.getSearchResults(query));

            //Search with YiFY
            var yify = new Providers.YiFY.YiFY();
            results.AddRange(yify.getSearchResults(query));
    
            //Return search results
            return results;
        }
    }
}
