using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torrent_Search.Engine.Providers.GetStrike.JSON.FileListing
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
        public File_Info file_info { get; set; }
    }

    public class File_Info
    {
        public string[] file_names { get; set; }
    }

}
