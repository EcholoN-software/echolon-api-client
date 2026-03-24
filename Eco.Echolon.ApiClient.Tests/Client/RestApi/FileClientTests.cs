using System;
using System.IO;
using System.Threading.Tasks;
using Eco.Echolon.ApiClient.Client.RestApi;
using Eco.Echolon.ApiClient.Model;
using Eco.Echolon.ApiClient.Model.DomainTypes;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Eco.Echolon.ApiClient.Tests.Client.RestApi;

public class FileClientTests
{
    private readonly IBaseRestClient _restClient;
    private readonly FileClient _sut;

    public FileClientTests()
    {
        _restClient = Substitute.For<IBaseRestClient>();
        _sut = new FileClient(_restClient);
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Upload_PassesResolvedMimeType_ToUploadFileData()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var fileInput = new FileInput("report.pdf", TimeSpan.FromHours(1));
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });

        _restClient.CreateNewFile(fileInput).Returns(ApiResult.Success(fileKey));
        _restClient.UploadFileData(fileKey, stream, Arg.Any<string>()).Returns(ApiResult.Success());

        await _sut.Upload(fileInput, stream);

        await _restClient.Received(1).UploadFileData(fileKey, stream, "application/pdf");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Upload_PassesTextPlain_ForTxtFile()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var fileInput = new FileInput("notes.txt", TimeSpan.FromHours(1));
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });

        _restClient.CreateNewFile(fileInput).Returns(ApiResult.Success(fileKey));
        _restClient.UploadFileData(fileKey, stream, Arg.Any<string>()).Returns(ApiResult.Success());

        await _sut.Upload(fileInput, stream);

        await _restClient.Received(1).UploadFileData(fileKey, stream, "text/plain");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Upload_PassesOctetStream_ForUnknownExtension()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var fileInput = new FileInput("data.xyz123", TimeSpan.FromHours(1));
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });

        _restClient.CreateNewFile(fileInput).Returns(ApiResult.Success(fileKey));
        _restClient.UploadFileData(fileKey, stream, Arg.Any<string>()).Returns(ApiResult.Success());

        await _sut.Upload(fileInput, stream);

        await _restClient.Received(1).UploadFileData(fileKey, stream, "application/octet-stream");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Upload_DoesNotCallUploadFileData_WhenCreateNewFileFails()
    {
        var fileInput = new FileInput("report.pdf", TimeSpan.FromHours(1));
        var stream = new MemoryStream(new byte[] { 1, 2, 3 });
        var faults = new[] { new Fault("CREATE_FAILED", "Could not create file") };

        _restClient.CreateNewFile(fileInput).Returns(ApiResult.Faulted<FileKey>(faults));

        var result = await _sut.Upload(fileInput, stream);

        result.IsFaulted.ShouldBeTrue();
        result.Faults[0].Code.ShouldBe("CREATE_FAILED");
        await _restClient.DidNotReceive().UploadFileData(
            Arg.Any<FileKey>(), Arg.Any<Stream>(), Arg.Any<string>());
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Info_ReturnsFileInfo_WhenSuccessful()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var expected = new FileInfoResult
        {
            Key = fileKey.ToString(),
            Filename = "export.zip",
            FileExtension = ".zip",
            FileSize = 1024,
            MimeType = "application/zip",
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(1)
        };

        _restClient.GetFileInfo(fileKey).Returns(ApiResult.Success(expected));

        var result = await _sut.Info(fileKey);

        result.IsSucceeded.ShouldBeTrue();
        result.GetData().ShouldBe(expected);
        result.GetData().Filename.ShouldBe("export.zip");
        result.GetData().FileExtension.ShouldBe(".zip");
        result.GetData().FileSize.ShouldBe(1024);
        result.GetData().MimeType.ShouldBe("application/zip");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Info_ReturnsFaulted_WhenRestClientFails()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var faults = new[] { new Fault("NOT_FOUND", "File not found") };

        _restClient.GetFileInfo(fileKey).Returns(ApiResult.Faulted<FileInfoResult>(faults));

        var result = await _sut.Info(fileKey);

        result.IsFaulted.ShouldBeTrue();
        result.Faults.Length.ShouldBe(1);
        result.Faults[0].Code.ShouldBe("NOT_FOUND");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Download_ReturnsStream_WhenSuccessful()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var expectedData = new byte[] { 0x50, 0x4B, 0x03, 0x04 }; // ZIP magic bytes
        var expectedStream = new MemoryStream(expectedData);

        _restClient.DownloadFile(fileKey).Returns(ApiResult.Success<Stream>(expectedStream));

        var result = await _sut.Download(fileKey);

        result.IsSucceeded.ShouldBeTrue();
        var stream = result.GetData();
        stream.ShouldNotBeNull();

        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        ms.ToArray().ShouldBe(expectedData);
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Download_ReturnsFaulted_WhenRestClientFails()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var faults = new[] { new Fault("NOT_FOUND", "File not found") };

        _restClient.DownloadFile(fileKey).Returns(ApiResult.Faulted<Stream>(faults));

        var result = await _sut.Download(fileKey);

        result.IsFaulted.ShouldBeTrue();
        result.Faults.Length.ShouldBe(1);
        result.Faults[0].Code.ShouldBe("NOT_FOUND");
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Info_DelegatesToRestClient()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        var expected = new FileInfoResult
        {
            Key = fileKey.ToString(),
            Filename = "test.pdf",
            FileExtension = ".pdf",
            FileSize = 2048,
            MimeType = "application/pdf",
            ExpiresAt = null
        };

        _restClient.GetFileInfo(fileKey).Returns(ApiResult.Success(expected));

        await _sut.Info(fileKey);

        await _restClient.Received(1).GetFileInfo(fileKey);
    }

    [Fact]
    [Trait("Category", "fast")]
    public async Task Download_DelegatesToRestClient()
    {
        var fileKey = new FileKey(Guid.NewGuid());
        _restClient.DownloadFile(fileKey).Returns(ApiResult.Success<Stream>(new MemoryStream()));

        await _sut.Download(fileKey);

        await _restClient.Received(1).DownloadFile(fileKey);
    }
}
