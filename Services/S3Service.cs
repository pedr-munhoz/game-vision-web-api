using Amazon.S3;
using Amazon.S3.Model;

namespace game_vision_web_api.Services
{
    public class S3Service(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> UploadFile(string key, string prefix, IFormFile file, string contentType)
        {
            string accessKey = _configuration["AwsS3AccessKey"] ?? "";
            string secretKey = _configuration["AwsS3SecretKey"] ?? "";
            string bucketName = _configuration["AwsS3RootBucket"] ?? "";

            // Set up AWS credentials
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            // Set up S3 client
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            using var client = new AmazonS3Client(credentials, config);
            try
            {
                // Create a MemoryStream from the uploaded file
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Upload the MemoryStream to S3
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{prefix}/{key}",
                    InputStream = memoryStream,
                    ContentType = contentType
                };
                var response = await client.PutObjectAsync(request);

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