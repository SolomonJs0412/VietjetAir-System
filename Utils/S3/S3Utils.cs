using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using flightdocs_system.common;
using flightdocs_system.models.http.http_request.Permission;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.Utils.S3
{
    public class S3Utils : IS3Utils
    {
        IAmazonS3 _s3Client = S3ClientFactory.GetS3Client();
        public readonly StringSupport _string;
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

        public async Task<dynamic> CreateFolder(string bucketName, string folderName)
        {
            var result = false;
            try
            {
                var s3Client = new AmazonS3Client(RegionEndpoint.USWest2); // Replace with your desired region

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = folderName,
                    ContentBody = string.Empty // Empty content for creating an empty folder
                };

                var response = await s3Client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Folder created successfully!");
                    result = true;
                }
                else
                {
                    Console.WriteLine("Failed to create the folder.");
                    result = false;
                }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error creating the folder: {ex.Message}");
                result = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                result = false;
            }
            return result;
        }

        public async Task<dynamic> CreateJsonFileAsync(string folderName, List<PerCreateRequest> reqList)
        {
            var result = false;
            try
            {
                String FileName = "permissions.json";
                string bucketName = "flightdocs-demo";
                var permissions = new
                {
                    Read = true,
                    Write = false,
                    Execute = true
                };

                var jsonString = _string.ConvertObjectToJson(reqList);
                var jsonBytes = Encoding.UTF8.GetBytes(jsonString);

                using (var stream = new MemoryStream(jsonBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = folderName + FileName,
                        InputStream = stream
                    };

                    var response = await _s3Client.PutObjectAsync(request);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("JSON file created successfully!");
                        result = true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the JSON file.");
                        result = false;
                    }
                }
            }
            catch (AmazonS3Exception ex)
            {
                result = false;
                Console.WriteLine($"Error creating the JSON file: {ex.Message}");
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return result;
        }

    }
}