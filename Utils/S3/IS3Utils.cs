using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Permission;

namespace flightdocs_system.Utils.S3
{
    public interface IS3Utils
    {
        Task<dynamic> UploadFileToS3(IFormFile file);
        Task<byte[]> ReadPdfFromS3Async(string bucketName, string key);
        Task<dynamic> RemoveFileFromS3Async(string bucketName, string key);
        Task<dynamic> CreateFolder(string bucketName, string folderName);
        Task<dynamic> CreateJsonFileAsync(string folderName, List<PerCreateRequest> request);
    }
}