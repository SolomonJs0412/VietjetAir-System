using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using flightdocs_system.common;
using flightdocs_system.models.Documents;
using flightdocs_system.models.http.http_request.Documents;
using flightdocs_system.services.DocumentServices;
using flightdocs_system.staticObject.StaticResultResponse;
using flightdocs_system.Utils.S3;

namespace flightdocs_system.repositories.Document
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration? _config;
        public readonly IS3Utils _s3;
        public readonly StringSupport _string;
        public readonly DocumentSevices _docServices;
        public DocumentRepository(IConfiguration configuration, IS3Utils s3, StringSupport stringSP, DocumentSevices docServices)
        {
            _config = configuration;
            _s3 = s3;
            _string = stringSP;
            _docServices = docServices;
        }

        public async Task<dynamic> UploadNewDocs(UploadNewDocs request, IFormFile file)
        {
            DocumentInfo doc = new DocumentInfo();
            ServiceResponse result = new ServiceResponse();

            try
            {
                var s3Response = await _s3.UploadFileToS3(file);
                if (s3Response.isSuccess != true)
                {
                    result.isSuccess = false;
                    result.Message = "S3 error: ";
                    result.StatusCode = s3Response.StatusCode;
                    return result;
                }
                var s3Key = file.FileName;
                if (!request.isUpdate)
                {
                    doc.Version = 1.0;
                }
                else
                {
                    doc.Version = request.Version;
                }

                doc.S3Key = s3Key;
                doc.FlightCd = request.FlightCd;
                doc.GroupCd = request.GroupCd;
                doc.Type = request.Type;
                doc.Permission = _string.ConvertStringToJson(request.Permission);

                var isSuccess = await _docServices.SaveDocuments(doc);

                if (isSuccess)
                {
                    result.isSuccess = isSuccess;
                    result.StatusCode = 201;
                    result.Message = "Uploaded documents were successfully";
                    result.Database = s3Key;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading new documents" + ex.Message);
            }

            return result;
        }

        public async Task<dynamic> GetPdfFromS3(string key)
        {
            ServiceResponse result = new ServiceResponse();

            try
            {
                string bucketName = "flightdocs-demo";
                byte[] pdfBytes = await _s3.ReadPdfFromS3Async(bucketName, key);
                result.isSuccess = true;
                result.Database = pdfBytes;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting file from S3: " + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 500;
                return result;
            }
            return result;
        }

        public async Task<dynamic> DeleteFileFromS3(string key)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                string bucketName = "flightdocs-demo";
                await _s3.RemoveFileFromS3Async(bucketName, key);
                result.isSuccess = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting file from S3: " + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 500;
                return result;
            }
            return result;
        }
    }
}