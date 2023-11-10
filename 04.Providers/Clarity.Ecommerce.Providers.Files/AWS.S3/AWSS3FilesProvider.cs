// <copyright file="AWSS3FilesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AWS S3 files provider class</summary>
#pragma warning disable 1584,1711,1572,1581,1580,CS1574
namespace Clarity.Ecommerce.Providers.Files.AWSS3
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon;
    using Amazon.Runtime;
    using Amazon.S3;
    using Amazon.S3.Model;
    using Amazon.S3.Transfer;
    using Interfaces.Providers.Files;
    using ServiceStack.Web;

    /// <summary>The AWS S3 files provider.</summary>
    /// <seealso cref="FilesProviderBase"/>
    public class AWSS3FilesProvider : FilesProviderBase
    {
        /// <summary>The AWS S3 client.</summary>
        private AmazonS3Client client;

        /// <summary>Gets the AWS S3 client.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <value>The AWS S3 client.</value>
        public AmazonS3Client S3Client
        {
            get
            {
                if (client != null)
                {
                    return client;
                }
                if (!AWSS3FilesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated))
                {
                    throw new ArgumentException(
                        "Cannot create an AmazonS3Client: null values for Profile, AccessKeyId, SecretAccessKey, or Region");
                }
                Amazon.Util.ProfileManager.RegisterProfile(
                    AWSS3FilesProviderConfig.Profile,
                    AWSS3FilesProviderConfig.AccessKey,
                    AWSS3FilesProviderConfig.Secret);
                client = new AmazonS3Client(
                    new StoredProfileAWSCredentials(AWSS3FilesProviderConfig.Profile),
                    RegionEndpoint.GetBySystemName(AWSS3FilesProviderConfig.Region));
                return client;
            }
        }

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => AWSS3FilesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<IUploadResponse> UploadFileAsync(
            IUpload upload, IHttpFile[] files, Enums.FileEntityType fileEntityType, string? contextProfileName)
        {
            var uploadResponse = new UploadResponse(upload);
            try
            {
                var uploadController = RegistryLoaderWrapper.GetInstance<IUploadController>();
                await uploadController.AddUploadAsync(uploadResponse).ConfigureAwait(false);
                var fileTransferUtility = new TransferUtility(S3Client);
                var tokenSource = new CancellationTokenSource();
                foreach (var file in files)
                {
                    var fileKey = AWSS3FilesProviderConfig.Folder + file.FileName;
                    var accessControlList = GetCannedAcl(AWSS3FilesProviderConfig.Acl);
                    var transferRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = AWSS3FilesProviderConfig.Bucket,
                        InputStream = file.InputStream,
                        ////FilePath = uploadKey, //Not actually used to read file. Mainly used to reference as a key for finding file later
                        StorageClass = S3StorageClass.Standard,
                        ////PartSize = 6291456, // 6 MB.
                        Key = fileKey,
                        CannedACL = accessControlList,
                    };
                    var fileName = accessControlList == S3CannedACL.Private
                        ? fileKey
                        : $"{AWSS3FilesProviderConfig.AmazonAwsRootUrl}{AWSS3FilesProviderConfig.Bucket}/{fileKey}";
                    uploadResponse.UploadFiles.Add(new UploadResult
                    {
                        FileName = fileName,
                        FileKey = fileKey, // Used to match up with UploadResults
                        ContentType = file.ContentType,
                        ContentLength = file.ContentLength,
                        UploadFileStatus = Enums.UploadStatus.Created,
                    });
                    transferRequest.UploadProgressEvent += UploadProgressEventHandler;
                    await fileTransferUtility.UploadAsync(transferRequest, tokenSource.Token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("AWSS3FilesProvider.UploadFile", ex.Message, ex, contextProfileName).ConfigureAwait(false);
                throw;
            }
            return uploadResponse;
        }

        /// <inheritdoc/>
        public override Task<string> GetFileUrlAsync<TIFileModel>(TIFileModel file, Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(
                GetCannedAcl(AWSS3FilesProviderConfig.Acl) != S3CannedACL.Private
                    ? file.Slave.FileName
                    : S3Client.GetPreSignedURL(
                        new GetPreSignedUrlRequest
                        {
                            BucketName = AWSS3FilesProviderConfig.Bucket,
                            Key = file.Slave.FileName,
                            Expires = DateExtensions.GenDateTime.AddMinutes(5),
                        }));
        }

        /// <inheritdoc/>
        public override Task<string> GetFileUrlAsync(string file, Enums.FileEntityType fileEntityType)
        {
            return Task.FromResult(
                GetCannedAcl(AWSS3FilesProviderConfig.Acl) != S3CannedACL.Private
                    ? file
                    : S3Client.GetPreSignedURL(
                        new GetPreSignedUrlRequest
                        {
                            BucketName = AWSS3FilesProviderConfig.Bucket,
                            Key = file,
                            Expires = DateExtensions.GenDateTime.AddMinutes(5),
                        }));
        }

        /// <inheritdoc/>
        public override Task<string> GetFileSaveRootPathAsync(Enums.FileEntityType? fileEntityType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<string> GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<string> GetRelativeFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType fileEntityType)
        {
            throw new NotImplementedException();
        }

        /// <summary>Uploads the progress event handler.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     The UploadProgressArgs to process.</param>
        private static void UploadProgressEventHandler(object sender, UploadProgressArgs e)
        {
            try
            {
                var transferRequest = (TransferUtilityUploadRequest)sender;
                var uploadController = RegistryLoaderWrapper.GetInstance<IUploadController>();
                var fileKey = transferRequest.Key;
                var percentDone = e.PercentDone;
                var totalBytes = e.TotalBytes;
                var transferredBytes = e.TransferredBytes;
                // ReSharper disable AsyncConverter.AsyncWait
                var task = uploadController.GetUploadByFileKeyAsync(fileKey);
                task.Wait(10_000);
                var upload = task.Result;
                // ReSharper restore AsyncConverter.AsyncWait
                var file = upload?.UploadFiles.Find(f => f.FileKey == fileKey);
                if (file == null)
                {
                    return;
                }
                file.PercentDone = percentDone;
                file.TotalBytes = totalBytes;
                file.TransferredBytes = transferredBytes;
                file.UploadFileStatus = percentDone > 0
                    ? percentDone < 100
                        ? Enums.UploadStatus.InProgress
                        : Enums.UploadStatus.Completed
                    : Enums.UploadStatus.Created;
                Debug.WriteLine($"{file.FileKey} - {file.TransferredBytes}/{file.TotalBytes} ({file.PercentDone}%) : {file.UploadFileStatus}");
            }
            catch
            {
                // Ignore Exceptions
            }
        }

        /// <summary>Gets canned Access Control List.</summary>
        /// <param name="acl">The Access Control List.</param>
        /// <returns>The canned Access Control List.</returns>
        private static S3CannedACL GetCannedAcl(string acl)
        {
            switch (acl)
            {
                case "private":
                {
                    return S3CannedACL.Private;
                }
                // case "public-read":
                default:
                {
                    return S3CannedACL.PublicRead;
                }
            }
        }
    }
}
