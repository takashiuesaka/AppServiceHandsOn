using Azure.Storage.Queues;
using FileUploaderToLocalDisk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;

namespace FileUploaderToLocalDisk.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger<HomeController> _logger;

        private string? UploadFileDestDirFullPath { get; }

        private string QueueName { get; }

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this.Configuration = configuration;
            _logger = logger;

            string? homeDir = Environment.GetEnvironmentVariable("HOME");
            string? uploadFileDestDirName = this.Configuration["uploadFileDestDirName"];
            this.UploadFileDestDirFullPath = Path.Combine(homeDir, uploadFileDestDirName);

            this.QueueName = this.Configuration["QueueName"];
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (!Directory.Exists(this.UploadFileDestDirFullPath))
                Directory.CreateDirectory(this.UploadFileDestDirFullPath);

            var files = Directory.GetFiles(this.UploadFileDestDirFullPath);

            return View(files);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile uploadFile)
        {
            // upuloadFile の FileName の取り扱いには注意。表示とログ記録の目的以外に使用しないこと。
            // そのままのFileNameで DocumentRoot 配下に保存すると、外部から実行される可能性がある。
            // ファイル名を変えて、Document Root の外（できればAzure Storageに）に保存すること。
            // https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0


            if (uploadFile != null && uploadFile.Length > 0)
            {
                string newFilename = uploadFile.FileName;

                string? localFilePath = Path.Combine(this.UploadFileDestDirFullPath, newFilename);

                using (var localFileStream = System.IO.File.Create(localFilePath))
                {
                    await uploadFile.CopyToAsync(localFileStream);
                }

                string? connectionString = this.Configuration["AzureWebJobsStorage"];
                QueueClient queueClient = new QueueClient(connectionString, this.QueueName.ToLower());

                await queueClient.CreateIfNotExistsAsync();

                // Send a message to the queue
                queueClient.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(newFilename)));
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}