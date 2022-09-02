using Microsoft.AspNetCore.Mvc;
using Minio;
using System.Text;

namespace MinioApi.Controllers
{
    [ApiController]
    [Route("")]
    public class MinioController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] MinioClient minio)
        {
            bool BucketExists = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(Environment.GetEnvironmentVariable("MINIO_BUCKET_FILES")));

            if (!BucketExists) await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(Environment.GetEnvironmentVariable("MINIO_BUCKET_FILES")));

            var FileName = "ARQUIVO.txt";
            var FileBytes = Encoding.UTF8.GetBytes("CONTEÚDO DO ARQUIVO");

            using var filestream = new MemoryStream(FileBytes);

            await minio.PutObjectAsync(new PutObjectArgs()
                       .WithBucket(Environment.GetEnvironmentVariable("MINIO_BUCKET_FILES"))
                       .WithObject(FileName)
                       .WithStreamData(filestream)
                       .WithObjectSize(filestream.Length)
                       .WithContentType("application/octet-stream"));

            return Ok(await minio.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                                 .WithBucket(Environment.GetEnvironmentVariable("MINIO_BUCKET_FILES"))
                                 .WithObject(FileName)
                                 .WithExpiry(60 * 10)
                                 .WithHeaders(new Dictionary<string, string> { { "response-content-type", "application/octet-stream" } })));
        }
    }
}