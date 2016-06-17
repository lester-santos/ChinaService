using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DontiaChinaProxy
{
     public class LOGImage
    {
        public static List<byte[]> ConvertImageToByte()
        {
            string location2 = System.Configuration.ConfigurationManager.AppSettings["image_location"];
            string location = AppDomain.CurrentDomain.BaseDirectory;


            string _dontiacare = location + @"img\Dontiacare_03.png";

            var webClient = new WebClient();

            byte[] image_dontiacare = webClient.DownloadData(_dontiacare);

            List<byte[]> _bytes = new List<byte[]>();

            _bytes.Add(image_dontiacare);

            return _bytes;
        }

        public static List<byte[]> ImgDontiaLink()
        {
            string location2 = System.Configuration.ConfigurationManager.AppSettings["image_location"];
            string location = AppDomain.CurrentDomain.BaseDirectory;


            string _dontiacare = location + @"img\DontiaLINK_Logo_Gray.png";

            var webClient = new WebClient();

            byte[] image_dontiacare = webClient.DownloadData(_dontiacare);

            List<byte[]> _bytes = new List<byte[]>();

            _bytes.Add(image_dontiacare);

            return _bytes;
        }
        public static List<byte[]> ImgDontiaFooter()
        {
            string location2 = System.Configuration.ConfigurationManager.AppSettings["image_location"];
            string location = AppDomain.CurrentDomain.BaseDirectory;


            string _dontiacare = location + @"img\dontiafooter.png";

            var webClient = new WebClient();

            byte[] image_dontiacare = webClient.DownloadData(_dontiacare);

            List<byte[]> _bytes = new List<byte[]>();

            _bytes.Add(image_dontiacare);

            return _bytes;
        }
    }
}
