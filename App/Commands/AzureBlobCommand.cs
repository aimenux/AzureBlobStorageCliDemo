using App.Extensions;
using McMaster.Extensions.CommandLineUtils;

namespace App.Commands
{
    [Command(Name = "azureblob-cli", FullName = "Azure blob CLI", Description = "A azure blob CLI tool.")]
    [HelpOption]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [Subcommand(typeof(UploadFileCommand), typeof(DownloadFileCommand))]
    public class AzureBlobCommand
    {
        public void OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
        }

        private static string GetVersion() => typeof(AzureBlobCommand).GetVersion();
    }
}
