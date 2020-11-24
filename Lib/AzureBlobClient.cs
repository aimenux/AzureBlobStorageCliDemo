using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using ShellProgressBar;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Lib
{
    public class AzureBlobClient : IAzureBlobClient
    {
        private readonly ProgressBarOptions _options;
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly ILogger _logger;
        private const int MaxTicks = 100;

        public AzureBlobClient(string connectionString, string containerName, ILogger logger)
        {
            _options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                ForegroundColorDone = ConsoleColor.DarkGreen,
                BackgroundColor = ConsoleColor.DarkGray,
                BackgroundCharacter = '\u2593'
            };

            _connectionString = connectionString;
            _containerName = containerName;
            _logger = logger;
        }

        public async Task UploadBlobAsync(string filepath)
        {
            try
            {
                using (var progressBar = new ProgressBar(MaxTicks, "Upload File Progress", _options))
                {
                    var fileInfo = new FileInfo(filepath);
                    var totalBytes = fileInfo.Length;
                    var blobName = fileInfo.Name;

                    var progress = new Progress<long>();
                    progress.ProgressChanged += (object _, long currentBytes) => ProgressChanged(progressBar, currentBytes, totalBytes);

                    var blobClient = new BlobClient(_connectionString, _containerName, blobName);
                    await blobClient.UploadAsync(filepath, progressHandler: progress);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception has occured: {ex}", ex);
            }
        }

        public async Task DownloadBlobAsync(string blobName, string filepath)
        {
            try
            {
                var blobClient = new BlobClient(_connectionString, _containerName, blobName);
                var blobResponse = await blobClient.DownloadAsync();
                var blobDownloadInfo = blobResponse.Value;
                var totalBytes = blobDownloadInfo.ContentLength;
                var bufferBytes = new byte[2 * 1024 * 1024];
                var currentBytes = 0;
                var streamBytes = 0;

                using (var outputStream = File.Create(filepath))
                using (var progressBar = new ProgressBar(MaxTicks, "Download File Progress", _options))
                {
                    while ((streamBytes = await blobDownloadInfo.Content.ReadAsync(bufferBytes, 0, bufferBytes.Length)) != 0)
                    {
                        await outputStream.WriteAsync(bufferBytes, 0, streamBytes);
                        currentBytes += streamBytes;
                        ProgressChanged(progressBar, currentBytes, totalBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception has occured: {ex}", ex);
            }
        }

        private void ProgressChanged(ProgressBar progressBar, long currentBytes, long totalBytes)
        {
            progressBar.Tick((int)ComputeProgressPercentage(currentBytes, totalBytes));
        }

        private static double ComputeProgressPercentage(double currentBytes, double totalBytes)
        {
            return Math.Round(currentBytes / totalBytes, 2) * 100;
        }
    }
}
