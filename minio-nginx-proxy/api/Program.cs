using Minio;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton((sp) => new MinioClient()
                .WithEndpoint(Environment.GetEnvironmentVariable("MINIO_ENDPOINT"))
                .WithProxy(new WebProxy(Environment.GetEnvironmentVariable("MINIO_ENDPOINT_PROXY")))
                .WithCredentials(Environment.GetEnvironmentVariable("MINIO_ACCESSKEY"), Environment.GetEnvironmentVariable("MINIO_SECRETKEY"))
                .WithHttpClient(new HttpClient(new HttpClientHandler() { Proxy = new WebProxy(Environment.GetEnvironmentVariable("MINIO_ENDPOINT_PROXY")) }))
                .Build());

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
