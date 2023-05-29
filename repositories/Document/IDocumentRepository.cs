using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Documents;

namespace flightdocs_system.repositories.Document
{
    public interface IDocumentRepository
    {
        Task<dynamic> UploadNewDocs(UploadNewDocs request, IFormFile file);
        Task<dynamic> GetPdfFromS3(string key);
        Task<dynamic> DeleteFileFromS3(string key);
    }
}