using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.Utils.S3
{
    public interface IS3Utils
    {
        Task<dynamic> UploadFileToS3(IFormFile file);
        Task<byte[]> ReadPdfFromS3Async(string bucketName, string key);
        Task<dynamic> RemoveFileFromS3Async(string bucketName, string key);
        Task<dynamic> CreateFolder(string bucketName, string folderName);
    }
}