namespace Edunext_API.Helpers
{
    public class UploadFiles
    {
        static readonly string rootUrl = "";
        static readonly string baseFolder = "uploads";
        public static string SaveFile(string folder, IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{baseFolder}\\{folder}");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var finalPath = Path.Combine(Directory.GetCurrentDirectory(), $"{baseFolder}\\{folder}", fileName);
            using (var fileSystem = new FileStream(finalPath, FileMode.Create))
            {
                image.CopyTo(fileSystem);
            }
            return rootUrl + baseFolder + "/" + folder + "/" + fileName;
        }

        public static void DeleteFile(string fileName)
        {
            string filePath = fileName[rootUrl.Length..];
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
