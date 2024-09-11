using Amazon.S3;
using Amazon.S3.Model;

namespace game_vision_web_api.Services
{
    public class S3Service
    {
        private readonly AmazonS3Client _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            string accessKey = configuration["AwsS3AccessKey"] ?? "";
            string secretKey = configuration["AwsS3SecretKey"] ?? "";
            _bucketName = configuration["AwsS3RootBucket"] ?? "";

            // Set up AWS credentials
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            // Set up S3 client
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            _s3Client = new AmazonS3Client(credentials, config);
        }

        public async Task<bool> UploadFile(string key, string prefix, IFormFile file, string contentType)
        {
            try
            {
                // Create a MemoryStream from the uploaded file
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Upload the MemoryStream to S3
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = $"{prefix}/{key}",
                    InputStream = memoryStream,
                    ContentType = contentType
                };
                var response = await _s3Client.PutObjectAsync(request);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return false;
            }
            catch (AmazonS3Exception ex)
            {
                // Handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteFile(string key, string prefix)
        {
            try
            {
                // Create a DeleteObjectRequest to delete the specified file
                var request = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = $"{prefix}/{key}"
                };
                var response = await _s3Client.DeleteObjectAsync(request);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                    return false;
            }
            catch (AmazonS3Exception ex)
            {
                // Handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteFiles(IEnumerable<string> keys, string prefix)
        {
            try
            {
                // Create a DeleteObjectRequest to delete the specified file
                var request = new DeleteObjectsRequest { BucketName = _bucketName, };

                foreach (var key in keys)
                {
                    request.AddKey($"{prefix}/{key}");
                }

                var response = await _s3Client.DeleteObjectsAsync(request);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    return false;
            }
            catch (AmazonS3Exception ex)
            {
                // Handle the exception as needed
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
