using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torrent_Search.Engine.Providers.GetStrike.JSON
{
    public class Rootobject
    {
        public int results { get; set; }
        public int statuscode { get; set; }
        public float responsetime { get; set; }
        public Torrent[] torrents { get; set; }
    }

    public class Torrent
    {
        public string torrent_hash { get; set; }
        public string torrent_title { get; set; }
        public string torrent_category { get; set; }
        public string sub_category { get; set; }
        public int seeds { get; set; }
        public int leeches { get; set; }
        public int file_count { get; set; }
        public long size { get; set; }
        public int download_count { get; set; }
        public string upload_date { get; set; }
        public string uploader_username { get; set; }
        public string page { get; set; }
        public string rss_feed { get; set; }
        public string magnet_uri { get; set; }
    }
}
