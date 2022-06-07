using CRMContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CRMHelper
{
    public static class FileUploadHelper
    {
        public static string UploadFiles(string path, IFormFile file, string fileNamePrefix, ILoggerManager _logger, out bool isUploadedSuccessfully)
        {
            isUploadedSuccessfully = false;
            try
            {
                if (file != null)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newFileName = fileNamePrefix + Path.GetFileNameWithoutExtension(file.FileName.Trim()) + DateTime.Now.Ticks + fi.Extension;
                    path = path + newFileName;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    isUploadedSuccessfully = true;
                }
                else
                    return null;
                return path;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while uploading file.Error: " + ex.ToString());
                return null;
            }
        }
        public static bool IsFileExtensionSupported(IFormFile fileToCheck)
        {
            if (fileToCheck == null)
                return false;
            try
            {
                var fileextension = Path.GetExtension(fileToCheck.FileName);
                return (fileextension != ".csv" || fileextension != ".jpeg" || fileextension != ".jpg" || fileextension != ".png");
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public static string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"{fileSize}bytes";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }
    }

}
