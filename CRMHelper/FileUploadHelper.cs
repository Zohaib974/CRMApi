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
                    var newFileName = fileNamePrefix + file.FileName.Trim() + DateTime.Now.Ticks + fi.Extension;
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
    }

}
