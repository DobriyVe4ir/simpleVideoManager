using Microsoft.Extensions.Hosting;

namespace simpleVideoManager.Scripts
{
    static public class FilesManager
    {
        static string folderName = "wwwroot/Videos";
        static List<string> formats = new List<string>() { ".mp4", "webm", "mp3", "ogv"};
        static List<string> files = new List<string>();
        private static string? basePath;

        public static void Init(IWebHostEnvironment env)
        {
            basePath = env.ContentRootPath;
        }

        public static List<string> GetFilesWithFormats()
        {
            if (basePath == null)
                throw new InvalidOperationException("FilesManager не инициализирован. Вызови Init(env).");

            string folderPath = Path.Combine(basePath, folderName);

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Папка не найдена: {folderPath}");

            // Приводим расширения к нижнему регистру и с точкой, если забыли указать
            var normalizedFormats = formats
                .Select(ext => ext.StartsWith(".") ? ext.ToLower() : "." + ext.ToLower())
                .ToHashSet();

            files = Directory
                .EnumerateFiles(basePath, "*.*", SearchOption.AllDirectories)
                .Where(file => normalizedFormats.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, file))
                .ToList();

            return files;
        }

    }
}
