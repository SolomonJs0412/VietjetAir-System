using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;

namespace flightdocs_system.common
{
    public class S3ClientFactory
    {
        private static IAmazonS3 _s3Client;

        public static IAmazonS3 GetS3Client()
        {
            if (_s3Client == null)
            {
                _s3Client = new AmazonS3Client(RegionEndpoint.GetBySystemName("ap-northeast-1"));
            }

            return _s3Client;
        }
    }
}