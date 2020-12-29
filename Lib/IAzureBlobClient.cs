using System.Threading.Tasks;

namespace Lib
{
    public interface IAzureBlobClient
    {
        Task UploadBlobAsync(string blobName, string filepath);
        Task DownloadBlobAsync(string blobName, string filepath, DownloadStrategies strategy = DownloadStrategies.UsingBlobPropertiesAndStreams);
    }
}
