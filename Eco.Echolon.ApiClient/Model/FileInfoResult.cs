using System;

namespace Eco.Echolon.ApiClient.Model
{
    public class FileInfoResult
    {
        public string? Key { get; set; }
        public string? Filename { get; set; }
        public string? FileExtension { get; set; }
        public long FileSize { get; set; }
        public string? MimeType { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }
}
