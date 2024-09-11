using System.Text;

namespace DemoApp.Utilities.FileManagement
{
    public class FileManagement
    {
        public static string[] GetListOfFiles(string folderPath, string searchPattern = "", SearchOption searchOption = SearchOption.AllDirectories)
        {
            return Directory.GetFiles(folderPath);
        }
        public static void CreateFile(string folderPath, string fileName, string content)
        {
            var filePath = $"{folderPath}\\{fileName}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fs = File.Create(filePath))
            {
                var info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }

        public static void RenameFile(string oldFileName, string newFileName)
        {
            if (!File.Exists(newFileName))
            {
                File.Move(oldFileName, newFileName);
            }
        }
    }
}
