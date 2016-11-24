using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;
using System.IO;

namespace DW.Framework.Utils
{
    public class DownFile
    {
        /**/
        /// <summary>
        /// �����ļ���֧�ִ��ļ����������ٶ����ơ�֧����������ӦͷAccept-Ranges��ETag������ͷRange ��
        /// Accept-Ranges����Ӧͷ����ͻ���ָ�����˽���֧�ֿɻָ�����.ʵ�ֺ�̨���ܴ������BITS����ֵΪ��bytes��
        /// ETag����Ӧͷ�����ڶԿͻ��˵ĳ�ʼ��200����Ӧ���Լ����Կͻ��˵Ļָ�����
        /// ����Ϊÿ���ļ��ṩһ��Ψһ��ETagֵ�������ļ������ļ�����޸ĵ�������ɣ�����ʹ�ͻ�������ܹ���֤�����Ѿ����ص��ֽڿ��Ƿ���Ȼ�����µġ�
        /// Range����������ʼλ�ã����Ѿ����ص��ͻ��˵��ֽ�����ֵ�磺bytes=1474560- ��
        /// ���⣺UrlEncode��������ļ����еĿո�ת����+��+ת��Ϊ%2b��������������ǲ������Ӻ�Ϊ�ո�ģ���������������صõ����ļ����ո�ͱ���˼Ӻţ�
        /// ����취��UrlEncode ֮��, �� "+" �滻�� "%20"����Ϊ�������%20ת��Ϊ�ո�
        /// </summary>
        /// <param name="httpContext">��ǰ�����HttpContext</param>
        /// <param name="filePath">�����ļ�������·������·�����ļ���</param>
        /// <param name="speed">�����ٶȣ�ÿ���������ص��ֽ���</param>
        /// <returns>true���سɹ���false����ʧ��</returns>
        public static bool DownloadFile(HttpContext httpContext, string fileName, string filePath, long speed)
        {
            bool ret = true;
            try
            {
                //--��֤��HttpMethod��������ļ��Ƿ����
                #region--��֤��HttpMethod��������ļ��Ƿ����

                switch (httpContext.Request.HttpMethod.ToUpper())
                { //Ŀǰֻ֧��GET��HEAD����
                    case "GET":
                    case "HEAD":
                        break;
                    default:
                        httpContext.Response.StatusCode = 501;
                        return false;
                }
                if (!File.Exists(filePath))
                {
                    httpContext.Response.StatusCode = 404;
                    return false;
                }
                #endregion

                //����ֲ�����
                #region ����ֲ�����
                long startBytes = 0;
                int packSize = 1024 * 10; //�ֿ��ȡ��ÿ��10K bytes
                //string fileName = Path.GetFileName(filePath);
                FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                long fileLength = myFile.Length;

                int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//����������ȡ��һ���ݿ��ʱ����
                string lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
                string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//���ڻָ�����ʱ��ȡ����ͷ;
                #endregion

                #region--��֤���ļ��Ƿ�̫���Ƿ��������������ϴα����������֮���Ƿ��޸Ĺ�--------------
                if (myFile.Length > Int32.MaxValue)
                {//-------�ļ�̫����-------
                    httpContext.Response.StatusCode = 413;//����ʵ��̫��
                    return false;
                }

                if (httpContext.Request.Headers["If-Range"] != null)//��Ӧ��ӦͷETag���ļ���+�ļ�����޸�ʱ��
                {
                    //----------�ϴα����������֮���޸Ĺ�--------------
                    if (httpContext.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                    {//�ļ��޸Ĺ�
                        httpContext.Response.StatusCode = 412;//Ԥ����ʧ��
                        return false;
                    }
                }
                #endregion

                try
                {
                    #region -------�����Ҫ��Ӧͷ����������ͷ�������֤-------------------
                    httpContext.Response.Clear();
                    httpContext.Response.Buffer = false;
                    httpContext.Response.AddHeader("Content-MD5", DW.Framework.Security.Md5.getMd5Hash(myFile));//������֤�ļ�
                    httpContext.Response.AddHeader("Accept-Ranges", "bytes");//��Ҫ����������
                    httpContext.Response.AppendHeader("ETag", "\"" + eTag + "\"");//��Ҫ����������
                    httpContext.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//������޸�����д����Ӧ                
                    httpContext.Response.ContentType = "application/octet-stream";//MIME���ͣ�ƥ�������ļ�����
                    httpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                    httpContext.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    httpContext.Response.AddHeader("Connection", "Keep-Alive");
                    httpContext.Response.ContentEncoding = Encoding.UTF8;
                    if (httpContext.Request.Headers["Range"] != null)
                    {//------����������������ȡ��������ʼλ�ã����Ѿ����ص��ͻ��˵��ֽ���------
                        httpContext.Response.StatusCode = 206;//��Ҫ���������룬��ʾ�ֲ���Χ��Ӧ����ʼ����ʱĬ��Ϊ200
                        string[] range = httpContext.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-"
                        startBytes = System.Convert.ToInt64(range[1]);//�Ѿ����ص��ֽ��������������صĿ�ʼλ��  
                        if (startBytes < 0 || startBytes >= fileLength)
                        {//��Ч����ʼλ��
                            return false;
                        }
                    }
                    if (startBytes > 0)
                    {//------������������󣬸��߿ͻ��˱��εĿ�ʼ�ֽ������ܳ��ȣ��Ա�ͻ��˽���������׷�ӵ�startBytesλ�ú�----------
                        httpContext.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    #endregion

                    #region -------��ͻ��˷������ݿ�-------------------
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//�ֿ����أ�ʣ�ಿ�ֿɷֳɵĿ���
                    for (int i = 0; i < maxCount && httpContext.Response.IsClientConnected; i++)
                    {//�ͻ����ж����ӣ�����ͣ
                        httpContext.Response.BinaryWrite(br.ReadBytes(packSize));
                        httpContext.Response.Flush();
                        if (sleep > 1) Thread.Sleep(sleep);
                    }
                    #endregion
                }
                catch
                {
                    ret = false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}
