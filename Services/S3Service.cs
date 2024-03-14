using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace game_vision_web_api.Services
{
    public class S3Service(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task UploadFile(string key, string prefix)
        {
            string accessKey = _configuration["AwsS3AccessKey"] ?? "";
            string secretKey = _configuration["AwsS3SecretKey"] ?? "";
            string bucketName = _configuration["AwsS3RootBucket"] ?? "";

            // Set up AWS credentials
            var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            // Set up S3 client
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1 // Change this to your bucket's region
            };
            var client = new AmazonS3Client(credentials, config);

            try
            {
                // Create an empty stream
                var stream = new MemoryStream();

                // Upload the empty stream as an object
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{prefix}/{key}",
                    InputStream = stream,
                    ContentType = "text/plain" // Set content type as text/plain for a text file
                };

                var response = await client.PutObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                { }
                else
                { }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}