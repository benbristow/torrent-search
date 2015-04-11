using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torrent_Search.Engine
{
    public class TorrentResult
    {
        public string title { get; set; }
        public float size { get;  set; }
        public string magnet { get;  set; }
        public string torrentFile { get; set; }
        public string url { get;  set; }
        public SearchProvider source { get;  set; }
        public DateTime date { get;  set; }
        public int peers { get; set; }
        public int seeds { get; set; }
        public string hash { get; set; }


        public void setSize(float size) {
            //Convert bytes to MB;
            this.size = size / 1024 / 1024;
        }

        public async Task<List<string>> getFileListing()
        {
            var fileListingTask = source.getFileListing(this);
            return await fileListingTask;
        }
    }
}
