using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Base.Tools
{
	public static class Downloader
	{
		/// <summary>
		/// General download process for API responses
		/// </summary>
		/// <param name="link">API reference with parameter(s)</param>
		/// <returns>Answer string</returns>
		public static async Task<string> DownloadWebResponse(Uri link)
		{
			// Create Web Request and avoid caching with header changes
			var request = (HttpWebRequest)WebRequest.Create(link);
            if (request.Headers == null)
                request.Headers = new WebHeaderCollection();

            try
            {
                request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();
            }
            catch (ArgumentException)
            {

            }

            // Download data
            var response = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 60));
            var receiveStream = response.GetResponseStream();
            var readStream = new StreamReader(receiveStream, Encoding.GetEncoding("iso-8859-1"));

            return await readStream.ReadToEndAsync();
		}
	}

    internal static class WebRequestExtensions
    {
        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
            {
                var t = Task.Factory.FromAsync<WebResponse>(
                    request.BeginGetResponse,
                    request.EndGetResponse,
                    null);

                if (!t.Wait(timeout)) throw new TimeoutException();

                return t.Result;
            });
        }
    }
}