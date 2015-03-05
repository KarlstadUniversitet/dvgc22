using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Repository
{
    public class Filehandler
    {
        private StreamReader streamReader;

        /// <summary>
        /// Hämtar en fil från det stora vida nätet och returnerar en stream. Används gärna tillsammans med readFile().
        /// </summary>
        /// <param name="url">URL till filen på naetet</param>
        /// <returns>En stream på filen vid URLen</returns>
        public Stream GetFileFromUrl(string url)
        {
            WebClient client = new WebClient();
            Stream stream;
            try
            {
                stream = client.OpenRead(url);
            }
            catch (Exception)
            {
                stream = null;
            }
            return stream;
        }
        /// <summary>
        /// Genererar en sträng från en Stream.
        /// Linebreak är \n
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>String</returns>
        public string ReadFile(Stream stream)
        {
            streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}