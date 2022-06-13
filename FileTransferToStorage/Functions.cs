using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FileTransferToStorage
{
    public class Functions
    {
        private IConfiguration Configuration { get; }

        public Functions(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task TransferFileToStorage(
            [QueueTrigger("%QueueName%")] string uploadfileName,
            ILogger logger)
        {
            
            string? homeDir = Environment.GetEnvironmentVariable("HOME");
            string? uploadFileDestDirName = this.Configuration["uploadFileDestDirName"];
            string filePath = Path.Combine(homeDir, uploadFileDestDirName, uploadfileName);

            using (Stream fileStream = File.OpenRead(filePath))
            {

                string connectionString = this.Configuration["AzureWebJobsStorage"];
                string blobContainerName = this.Configuration["blobContainerName"];
                BlobContainerClient container = new BlobContainerClient(connectionString, blobContainerName);
                await container.CreateIfNotExistsAsync();

                // Get a reference to a blob
                BlobClient blob = container.GetBlobClient(uploadfileName);

                // Upload file data
                await blob.UploadAsync(fileStream);
            }

            File.Delete(filePath);
        }
    }
}