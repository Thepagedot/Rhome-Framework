using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.Base.Tools
{
	public static class Downloader
	{
        private static readonly Dictionary<string, CacheFile<string>> Cache = new Dictionary<string, CacheFile<string>>();

	    /// <summary>
	    /// General download process for API responses
	    /// </summary>
	    /// <param name="url">API reference with parameter(s)</param>
	    /// <param name="cacheTime">Maximum cache age. 'null' if no caching is needed.</param>
	    /// <returns>Answer string</returns>
	    public static async Task<string> DownloadWebResponse(string url, TimeSpan? cacheTime = null)
		{
            // Check if cached response is available
	        if (cacheTime != null)
	        {
	            if (Cache.ContainsKey(url))
	            {
	                var cacheFile = Cache[url];
	                if (DateTime.Now.Subtract(cacheFile.TimeStamp) < cacheTime)
	                {
	                    return cacheFile.Value;
	                }
	            }
	        }

			// Create Web Request and avoid caching with header changes
			var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
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
            var result = await readStream.ReadToEndAsync();

            // Save result to cache
            Cache[url] = new CacheFile<string>(result);

	        return result;
		}
	}

    internal class CacheFile<T>
    {
        public DateTime TimeStamp { get; set; }
        public T Value { get; set; }

        public CacheFile(T value)
        {
            TimeStamp = DateTime.Now;
            Value = value;
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

                if (!t.Wait(timeout))
                    throw new TimeoutException();

                return t.Result;
            });
        }
    }
}