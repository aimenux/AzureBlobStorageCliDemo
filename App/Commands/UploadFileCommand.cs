using App.Extensions;
using Lib;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace App.Commands
{
    [Command(Name = "UploadFileCli", FullName = "UploadFile CLI", Description = "An UploadFile CLI Tool.")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [HelpOption]
    public class UploadFileCommand
    {
        private readonly ILogger _logger;

        public UploadFileCommand(ILogger logger)
        {
            _logger = logger;
        }

        [Option("-fp|--filepath", "Path of file to upload.", CommandOptionType.SingleValue)]
        public string FileToUpload { get; set; }

        [Option("-cn|--containername", "Azure blob storage container name.", CommandOptionType.SingleValue)]
        public string ContainerName { get; set; }

        [Option("-cs|--connectionstring", "Azure blob storage connection string.", CommandOptionType.SingleValue)]
        public string ConnectionString { get; set; }

        public Task OnExecuteAsync(CommandLineApplication app)
        {
            var showHelp =
                string.IsNullOrWhiteSpace(FileToUpload)
                || string.IsNullOrWhiteSpace(ContainerName)
                || string.IsNullOrWhiteSpace(ConnectionString);

            if (showHelp)
            {
                app.ShowHelp();
                return Task.CompletedTask;
            }

            var blobClient = new AzureBlobClient(ConnectionString, ContainerName, _logger);
            return blobClient.UploadBlobAsync(FileToUpload);
        }

        private string GetVersion() => GetType().GetVersion();
    }
}
