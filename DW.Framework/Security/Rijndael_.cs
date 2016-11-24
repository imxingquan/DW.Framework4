namespace DW.Framework.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Rijndael_
    {
        public string IV;
        public string Key;
        private RijndaelManaged myRijndael;

        public Rijndael_(string key)
        {
            this.myRijndael = new RijndaelManaged();
            this.Key = key;
            this.IV = "67^%*(&(*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
        }

        public Rijndael_(string key, string iv)
        {
            this.myRijndael = new RijndaelManaged();
            this.Key = key;
            this.IV = iv;
        }

        public byte[] Decrypt(byte[] Source)
        {
            byte[] bytes;
            try
            {
                byte[] buffer = Source;
                MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length);
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                ICryptoTransform transform = this.myRijndael.CreateDecryptor();
                CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(stream2);
                bytes = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }
            catch (Exception exception)
            {
                throw new Exception("在文件解密的时候出现错误！错误提示： \n" + exception.Message);
            }
            return bytes;
        }

        public string Decrypt(string Source)
        {
            string str;
            try
            {
                if (string.IsNullOrEmpty(Source))
                {
                    return "";
                }
                byte[] buffer = Convert.FromBase64String(Source);
                MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length);
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                ICryptoTransform transform = this.myRijndael.CreateDecryptor();
                CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                str = new StreamReader(stream2).ReadToEnd();
            }
            catch (Exception exception)
            {
                throw new Exception("在文件解密的时候出现错误！错误提示： \n" + exception.Message);
            }
            return str;
        }

        public void Decrypt(string inFileName, string outFileName)
        {
            try
            {
                FileStream stream = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                FileStream stream2 = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream2.SetLength(0L);
                byte[] buffer = new byte[100];
                long num = 0L;
                long length = stream.Length;
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                ICryptoTransform transform = this.myRijndael.CreateDecryptor();
                CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
                while (num < length)
                {
                    int count = stream.Read(buffer, 0, 100);
                    stream3.Write(buffer, 0, count);
                    num += count;
                }
                stream3.Close();
                stream2.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                throw new Exception("在文件解密的时候出现错误！错误提示： \n" + exception.Message);
            }
        }

        public string Encrypt(string Source)
        {
            string str;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Source);
                MemoryStream stream = new MemoryStream();
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                ICryptoTransform transform = this.myRijndael.CreateEncryptor();
                CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                
                stream.Close();
                str = Convert.ToBase64String(stream.ToArray());
            }
            catch (Exception exception)
            {
                throw new Exception("在文件加密的时候出现错误！错误提示： \n" + exception.Message);
            }
            return str;
        }

        public byte[] Encrypt(byte[] Source)
        {
            byte[] buffer3;
            try
            {
                byte[] buffer = Source;
                MemoryStream stream = new MemoryStream();
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                ICryptoTransform transform = this.myRijndael.CreateEncryptor();
                CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                buffer3 = stream.ToArray();
            }
            catch (Exception exception)
            {
                throw new Exception("在文件加密的时候出现错误！错误提示： \n" + exception.Message);
            }
            return buffer3;
        }

        public void Encrypt(string inFileName, string outFileName)
        {
            try
            {
                FileStream stream = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                FileStream stream2 = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream2.SetLength(0L);
                this.myRijndael.Key = this.GetLegalKey();
                this.myRijndael.IV = this.GetLegalIV();
                byte[] buffer = new byte[100];
                long num = 0L;
                long length = stream.Length;
                ICryptoTransform transform = this.myRijndael.CreateEncryptor();
                CryptoStream stream3 = new CryptoStream(stream2, transform, CryptoStreamMode.Write);
                while (num < length)
                {
                    int count = stream.Read(buffer, 0, 100);
                    stream3.Write(buffer, 0, count);
                    num += count;
                }
                stream3.Close();
                stream2.Close();
                stream.Close();
            }
            catch (Exception exception)
            {
                throw new Exception("在文件加密的时候出现错误！错误提示： \n" + exception.Message);
            }
        }

        private byte[] GetLegalIV()
        {
            string iV = this.IV;
            this.myRijndael.GenerateIV();
            int length = this.myRijndael.IV.Length;
            if (iV.Length > length)
            {
                iV = iV.Substring(0, length);
            }
            else if (iV.Length < length)
            {
                iV = iV.PadRight(length, ' ');
            }
            return Encoding.ASCII.GetBytes(iV);
        }

        private byte[] GetLegalKey()
        {
            string key = this.Key;
            this.myRijndael.GenerateKey();
            int length = this.myRijndael.Key.Length;
            if (key.Length > length)
            {
                key = key.Substring(0, length);
            }
            else if (key.Length < length)
            {
                key = key.PadRight(length, ' ');
            }
            return Encoding.ASCII.GetBytes(key);
        }
    }
}

