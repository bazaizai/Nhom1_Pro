using System.IO;
namespace AppView.Models
{
    public class FileService
    {
        public void DeleteBinAndObjDirectories(string rootPath)
        {
            string binPath = Path.Combine(rootPath, "bin");
            string objPath = Path.Combine(rootPath, "obj");

            if (Directory.Exists(binPath))
            {
                Directory.Delete(binPath, true);
            }

            if (Directory.Exists(objPath))
            {
                Directory.Delete(objPath, true);
            }
        }
    }
}
