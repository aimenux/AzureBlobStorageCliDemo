using System.Threading.Tasks;

namespace Lib
{
    public interface IAzureBlobClient
    {
        Task UploadBlobAsync(string filepath);
        Task DownloadBlobAsync(string blobName, string filepath);
    }
}
