using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using flightdocs_system.common;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.Utils.S3
{
    public class S3Utils : IS3Utils
    {
        IAmazonS3 _s3Client = S3ClientFactory.GetS3Client();
        public async Task<dynamic> UploadFileToS3(IFormFile file)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {


                if (file == null || file.Length <= 0)
                {
                    result.isSuccess = false;
                    result.Message = "No file provided";
                    result.StatusCode = 400;
                    return result;
                }

                using (var fileStream = file.OpenReadStream())
                {
                    var putRequest = new Amazon.S3.Model.PutObjectRequest
                    {
                        BucketName = "flightdocs-demo",
                        Key = file.FileName,
                        InputStream = fileStream,
                    };

                    var response = await _s3Client.PutObjectAsync(putRequest);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result.isSuccess = true;
                        result.StatusCode = 201;
                        result.Message = "Success";
                        return result;
                    }
                    else
                    {
                        result.isSuccess = false;
                        result.Message = "Failed to upload";
                        result.StatusCode = 400;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating from S3 utils" + ex.Message);
                return result;
            }
        }

        public async Task<byte[]> ReadPdfFromS3Async(string bucketName, string key)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            using (var response = await _s3Client.GetObjectAsync(request))
            using (var stream = response.ResponseStream)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public async Task<dynamic> RemoveFileFromS3Async(string bucketName, string key)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(request);
                result.StatusCode = 200;
                result.isSuccess = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.StatusCode = 500;
                result.isSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}