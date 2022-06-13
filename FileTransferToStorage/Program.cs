using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder();

if (Environment.GetEnvironmentVariable("ENVIRONMENT") == "Development")
{
    builder.UseEnvironment("development");

    builder.ConfigureAppConfiguration(cb =>
    {
        cb.AddJsonFile("local.settings.json");
    });
    builder.ConfigureServices((b, c) =>
    {
        Environment.SetEnvironmentVariable("QueueName", b.Configuration["QueueName"]);
    });

}

string? homeDirPath = Environment.GetEnvironmentVariable("HOME");

builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices()
    //.AddFiles(a => a.RootPath = homeDirPath) // Files トリガー用
    .AddAzureStorageQueues();    // Queue トリガー用
});

builder.ConfigureLogging((context, b) =>
{
    b.AddConsole();
});

var host = builder.Build();
using (host)
{
    await host.RunAsync();  // 継続的ジョブとして実行
}
