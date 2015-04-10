using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torrent_Search.Engine
{
    public class SearchEngine
    {
        public async Task<List<TorrentResult>> queryResults(string query)
        {
            var results = new List <TorrentResult>();

            var strike = new Engine.Providers.GetStrike.GetStrike();
            var strikeResultsTask = strike.getSearchResults(query);

            var yify = new Engine.Providers.YiFY.YiFY();
            var yifyResultsTask = yify.getSearchResults(query);

            try
            {
                results.AddRange(await strikeResultsTask);
            }
            catch { }

            try
            {
                results.AddRange(await yifyResultsTask);
            }
            catch { }

            return results;
        }
    }
}