using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Documents;
using flightdocs_system.repositories.Document;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers.Document
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _docServices;

        public DocumentController(IDocumentRepository docServices)
        {
            _docServices = docServices;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, GO")]
        [Route("upload")]
        public async Task<dynamic> UploadDocument([FromForm] UploadNewDocs request, IFormFile file)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _docServices.UploadNewDocs(request, file);
                if (response.StatusCode != 201)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != "" || response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.Message = (response.Message != "" || response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    result.Database = response.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }

        [HttpGet]
        [Route("/{s3key}")]
        public async Task<dynamic> GetFileAsync(string s3key)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _docServices.GetPdfFromS3(s3key);
                if (!response.isSuccess)
                {
                    result.isSuccess = false;
                    result.Message = response.Message ? response.Message : "";
                    return result;
                }
                result.Database = response.Database;
            }
            catch (Exception ex)
            {

            }
            return File(result.Database, "application/pdf", s3key);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, GO")]
        [Route("/{s3key}")]
        public async Task<dynamic> DeleteFileAsync(string s3key)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _docServices.DeleteFileFromS3(s3key);
                if (response.isSuccess != true)
                {
                    result.isSuccess = false;
                    result.StatusCode = 500;
                    return result;
                }
                result.isSuccess = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error from delete file s33" + ex.Message);
                return BadRequest(ex.Message);
            }
            return result;
        }
    }
}