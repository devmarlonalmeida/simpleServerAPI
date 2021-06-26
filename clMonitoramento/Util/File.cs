using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clMonitoramento.Util
{
    public static class FileUtil
    {
        static string defaultDirectory = AppDomain.CurrentDomain.BaseDirectory + $"Videos";

        public static bool SaveFile(string directory, string fileName, byte[] content)
        {
            try
            {
                string pathDir = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory),
                    pathFile = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory, fileName);

                if (!Directory.Exists(pathDir))
                {
                    Directory.CreateDirectory(pathDir);
                }

                File.WriteAllBytes(pathFile, content);

                if (!File.Exists(pathFile))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFile(string directory, string fileName)
        {
            try
            {
                string pathDir = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory);

                if (!File.Exists(pathDir + "\\\\" + fileName))
                    return false;

                File.Delete(pathDir + "\\\\" + fileName);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteDirectory(string directory)
        {
            try
            {
                string pathDir = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory);

                if (!Directory.Exists(pathDir))
                    return false;

                Directory.Delete(pathDir, true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int GetFileSize(string directory, string fileName)
        {
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory),
                pathFile = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory, fileName);

            if (!File.Exists(pathDir + "\\\\" + fileName))
                return -1;

            FileStream fs = null;
            try
            {
                using (fs = File.OpenRead(pathFile))
                {
                    return (int)fs.Length;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }

        public static byte[] GetFileContent(string directory, string fileName)
        {
            string pathDir = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory),
                pathFile = Path.Combine(Directory.GetCurrentDirectory(), defaultDirectory, directory, fileName);

            if (!File.Exists(pathDir + "\\\\" + fileName))
                return null;

            FileStream fs = null;
            try
            {
                byte[] data = null;
                using (fs = File.OpenRead(pathFile))
                {
                    var binaryReader = new BinaryReader(fs);
                    data = binaryReader.ReadBytes((int)fs.Length);
                }
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }
    }
}
