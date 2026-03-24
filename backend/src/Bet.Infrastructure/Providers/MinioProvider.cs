using Bet.Application.IoC;
using Bet.Application.Teams.Commands;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace Bet.Infrastructure.Providers;

public class MinioProvider : IMinioProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> UploadLogoTeam(
        UploadLogoTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(Constants.General.LOGO);
            
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(Constants.General.LOGO);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                
                _logger.LogInformation("Make bucket {0} in minio", Constants.General.LOGO);
            }

            var objectName = command.TeamId + Constants.General.JPG;
            
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(Constants.General.LOGO)
                .WithObject(command.TeamId.ToString())
                .WithStreamData(command.Stream);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            
            _logger.LogInformation("Put object {0} to bucket {1} in minio", 
                command.TeamId.ToString(), Constants.General.LOGO);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload files in minio");

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }
}