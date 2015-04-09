using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Torrent_Search.Engine.Providers.GetStrike
{
    class GetStrike:Torrent_Search.Engine.SearchProvider
    {
        public override string name { get { return "GetStrike"; } }
        public override string searchurl { get { return "https://getstrike.net/api/v2/torrents/search/?phrase={0}"; } }
        public override string homepage { get { return "https://getstrike.net/torrents/"; } }

        public override List<TorrentResult> getSearchResults(string query)
        {
            var torrentResults = new List<TorrentResult>();
            var r = getWebResponse(query);
            var j = JsonConvert.DeserializeObject<JSON.Rootobject>(r);

            foreach (JSON.Torrent torrent in j.torrents)
            {
                var t = new TorrentResult();
                t.title = torrent.torrent_title;
                t.magnet = torrent.magnet_uri;
                t.setSize(torrent.size);
                t.source = this.name;
                t.url = torrent.page;
                t.seeds = torrent.seeds;
                t.peers = torrent.leeches;
                t.date = DateTime.Parse(torrent.upload_date);
                torrentResults.Add(t);
            }

            return torrentResults;
        }
    }
}
