using System;

namespace Eco.Echolon.ApiClient.Model
{
    public class FileInput
    {
        public string FileName { get; }
        public TimeSpan Expiration { get; }

        public FileInput(string fileName, TimeSpan expiration)
        {
            FileName = fileName;
            Expiration = expiration;
        }
    }
}