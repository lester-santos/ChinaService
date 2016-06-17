using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace DentaLINKProxy.App_Code.Models
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
    }
}
