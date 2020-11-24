using App.Extensions;
using Lib;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace App.Commands
{
    [Command(Name = "DownloadFileCli", FullName = "DownloadFile CLI", Description = "A DownloadFile CLI Tool.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption]
    public class DownloadFileCommand
    {
        private readonly ILogger _logger;

        public DownloadFileCommand(ILogger logger)
        {
            _logger = logger;
        }

        [Option("-bn|--blobname", "Name of blob to download.", CommandOptionType.SingleValue)]
        public string BlobName { get; set; }

        [Option("-cn|--containername", "Azure blob storage container name.", CommandOptionType.SingleValue)]
        public string ContainerName { get; set; }

        [Option("-cs|--connectionstring", "Azure blob storage connection string.", CommandOptionType.SingleValue)]
        public string ConnectionString { get; set; }

        [Option("-fp|--filepath", "Path to use for the downloaded file.", CommandOptionType.SingleValue)]
        public string DownloadFilePath { get; set; }

        public Task OnExecuteAsync(CommandLineApplication app)
        {
            var showHelp =
                string.IsNullOrWhiteSpace(BlobName)
                || string.IsNullOrWhiteSpace(ContainerName)
                || string.IsNullOrWhiteSpace(ConnectionString)
                || string.IsNullOrWhiteSpace(DownloadFilePath);

            if (showHelp)
            {
                app.ShowHelp();
                return Task.CompletedTask;
            }

            var blobClient = new AzureBlobClient(ConnectionString, ContainerName, _logger);
            return blobClient.DownloadBlobAsync(BlobName, DownloadFilePath);
        }

        private string GetVersion() => GetType().GetVersion();
    }
}
