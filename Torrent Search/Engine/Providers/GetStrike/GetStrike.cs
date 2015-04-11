using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Torrent_Search.Engine.Providers.GetStrike
{
    class GetStrike:Torrent_Search.Engine.SearchProvider
    {
        public override string name { get { return "GetStrike"; } }
        public override string searchurl { get { return "https://getstrike.net/api/v2/torrents/search/?phrase={0}"; } }
        public override string filelistingurl { get { return "https://getstrike.net/api/v2/torrents/info/?hashes={0}"; } }
        public override string homepage { get { return "https://getstrike.net/torrents/"; } }


        public async Task<List<TorrentResult>> getSearchResults(string query)
        {
            var torrentResults = new List<TorrentResult>();
            string r = await doSearchRequest(query);
            var j = JsonConvert.DeserializeObject<JSON.Rootobject>(r);

            foreach (JSON.Torrent torrent in j.torrents)
            {
                var t = new TorrentResult();
                t.title = torrent.torrent_title;
                t.hash = torrent.torrent_hash;
                t.magnet = torrent.magnet_uri;
                t.setSize(torrent.size);
                t.source = this;
                t.url = torrent.page;
                t.seeds = torrent.seeds;
                t.peers = torrent.leeches;
                t.date = DateTime.Parse(torrent.upload_date);
                torrentResults.Add(t);
            }

            return torrentResults;
        }

        public async override Task<List<String>> getFileListing(TorrentResult torrent)
        {
            var fileList = new List<string>();
            var r = await doFileListingRequest(torrent.hash);
            var j = JsonConvert.DeserializeObject<JSON.FileListing.Rootobject>(r);

            foreach (string f in j.torrents[0].file_info.file_names)
            {
                fileList.Add(f);
            }

            return fileList;
        }
    }
}
