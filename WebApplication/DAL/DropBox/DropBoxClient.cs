using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using System.IO;
using Dropbox.Api.Files;
using Dropbox.Api.Sharing;

namespace DAL.DropBox
{
    public class DropBoxClient : IDisposable
    {
        private readonly string appKey = "kivhjh90rlfgkai";
        private readonly string appToken = "8QQ16056LUAAAAAAAAAAClzD-t8xfyEWE6KvvPksxKUz5F7DJYl-5O4Mr7pBKYoS";
        private readonly string fileNamePrefix = "todoFile";
        private readonly string fileExt = ".json";
        private readonly DropboxClient client;

        public DropBoxClient()
        {
            DropboxCertHelper.InitializeCertPinning();
            var httpClient = new HttpClient(new WebRequestHandler {ReadWriteTimeout = 10*1000})
            {
                Timeout = TimeSpan.FromMinutes(20)
            };

            var config = new DropboxClientConfig("SimpleTestApp")
            {
                HttpClient = httpClient
            };
            client = new DropboxClient(appToken, config);
        }

        public async Task UploadFileAsync(int userId, string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(byteArray);
            await client.Files.UploadAsync(GenerateFileName(userId), WriteMode.Overwrite.Instance, body: stream);
        }

        public async Task<string> DownloadFileAsync(int userId)
        {
            var result = await client.Files.ListFolderAsync("", false);
            foreach (var item in result.Entries)
            {
                if (item.IsFile && item.Name == GenerateFileName(userId).TrimStart(new []{'/'}))
                    using (var response = await client.Files.DownloadAsync(GenerateFileName(userId)))
                    {
                        return await response.GetContentAsStringAsync();
                    }
            }
            return "";
        }

        private string GenerateFileName(int userId)
        {
            if (userId == 0)
            {
                return $"/queue{fileExt}";
            }
            return $"/{fileNamePrefix}{userId}{fileExt}";
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    client.Dispose();
                }

                disposedValue = true;
            }
        }
        ~DropBoxClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
