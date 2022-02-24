
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage;

namespace WebAppNikita2.Controllers
{
    public class PdfController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("PdfController")]
        public async Task<IActionResult> Index(IFormFile files)
        {
            string? message = null;
            if (files == null || files.Length == 0)
            {
                message = "Please Upload a Valid pdf File.";
            }

            else
            {
                string connectionstring = "DefaultEndpointsProtocol=https;AccountName=myblobstorageacc;AccountKey=7dWSxl3GLlhiXteRWyXVXF8BOLbVEs6N/xmsX0NJqfaeHxOYSUp4PmhyPSGQndFxpL0qu5QhK8nv+AStI3tpDA==;EndpointSuffix=core.windows.net";
                string blobContainerName = "pdfs";
                BlobClient blobClient = new BlobClient(connectionString: connectionstring, blobContainerName: blobContainerName, blobName: files.FileName);
                try
                {
                    var result = await blobClient.UploadAsync(files.OpenReadStream());
                    message = files.FileName + " File successfully uploaded ";
                }
                catch (Exception)
                {
                    message = "An Error Occured!!!";
                }
            }
            return Ok(message);

        }
    }
}
