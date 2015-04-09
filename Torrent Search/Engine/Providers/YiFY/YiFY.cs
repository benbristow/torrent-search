using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Torrent_Search.Engine.Providers.YiFY
{
    class YiFY : Torrent_Search.Engine.SearchProvider
    {
        public override string name { get { return "YiFY"; } }
        public override string searchurl { get { return "https://yts.im/api/v2/list_movies.json?query_term={0}"; } }
        public override string homepage { get { return "https://yts.im/"; } }

        public override List<TorrentResult> getSearchResults(string query)
        {
            var torrentResults = new List<TorrentResult>();
            var r = getWebResponse(query);
            var j = JsonConvert.DeserializeObject<JSON.Rootobject>(r);

            foreach (JSON.Movie movie in j.data.movies)
            {
                foreach (var torrent in movie.torrents)
                {
                    var t = new TorrentResult();
                    t.source = this.name;

                    t.title = movie.title_long + " " + torrent.quality;
                    t.torrentFile = torrent.url;
                    t.date = DateTime.Parse(torrent.date_uploaded);
                    t.url = movie.url;
                    t.setSize(torrent.size_bytes);
                    t.peers = torrent.peers;
                    t.seeds = torrent.seeds;

                    torrentResults.Add(t);
                }
            }

            return torrentResults;
        }
    }
}