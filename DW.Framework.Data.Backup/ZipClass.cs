namespace DW.Framework.Zip
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    public class ZipClass
    {
        public static bool Zip(string FileToZip, string ZipedFile, string Password)
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, Password);
            }
            return (File.Exists(FileToZip) && ZipFile(FileToZip, ZipedFile, Password));
        }

        private static bool ZipFile(string FileToZip, string ZipedFile, string Password)
        {
            if (!File.Exists(FileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            FileStream baseOutputStream = null;
            ZipOutputStream stream2 = null;
            ZipEntry entry = null;
            bool flag = true;
            try
            {
                baseOutputStream = File.OpenRead(FileToZip);
                byte[] buffer = new byte[baseOutputStream.Length];
                baseOutputStream.Read(buffer, 0, buffer.Length);
                baseOutputStream.Close();
                baseOutputStream = File.Create(ZipedFile);
                stream2 = new ZipOutputStream(baseOutputStream);
                stream2.Password = Password;
                entry = new ZipEntry(Path.GetFileName(FileToZip));
                stream2.PutNextEntry(entry);
                stream2.SetLevel(6);
                stream2.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (entry != null)
                {
                    entry = null;
                }
                if (stream2 != null)
                {
                    stream2.Finish();
                    stream2.Close();
                }
                if (baseOutputStream != null)
                {
                    baseOutputStream.Close();
                    baseOutputStream = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return flag;
        }

        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool flag = true;
            ZipEntry entry = null;
            FileStream stream = null;
            Crc32 crc = new Crc32();
            try
            {
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));
                s.PutNextEntry(entry);
                s.Flush();
                string[] files = Directory.GetFiles(FolderToZip);
                foreach (string str in files)
                {
                    stream = File.OpenRead(str);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(str)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = stream.Length;
                    stream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            string[] directories = Directory.GetDirectories(FolderToZip);
            foreach (string str2 in directories)
            {
                if (!ZipFileDictory(str2, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }
            return flag;
        }

        private static bool ZipFileDictory(string FolderToZip, string ZipedFile, string Password)
        {
            if (!Directory.Exists(FolderToZip))
            {
                return false;
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(6);
            s.Password = Password;
            bool flag = ZipFileDictory(FolderToZip, s, "");
            s.Finish();
            s.Close();
            return flag;
        }
    }
}

