/**
 * (C)大连西数网络技术有限公司
 * 文件名: Smtp.cs
 * 创建日期: 2009-8-26
 * 作者：成海涛
 * 描述: 
 *       通过SMTP发送Email。
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace DW.Framework.Utils
{
    public class Smtp
    {
        
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromMail">发件人地址</param>
        /// <param name="fromName">发件人姓名</param>
        /// <param name="toMail">收件人地址。多个电子邮件地址必须用逗号字符（“,”）分隔。</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isBodyHtml">是否为HTML邮件</param>
        /// <param name="smtpHost">Smtp 主机名或 IP 地址</param>
        /// <param name="smtpPort">Smtp 端口</param>
        /// <param name="smtpSsl">是否启用SSL加密</param>
        /// <param name="smtpUser">Smtp 账号</param>
        /// <param name="smtpPwd">Smtp 密码</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string SendMail(string fromMail, string fromName, string toMail, string subject, string body, bool isBodyHtml, string smtpHost, int smtpPort, bool smtpSsl, string smtpUser, string smtpPwd, Encoding encoding,bool isAsync)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(fromMail, fromName, encoding);
            /* 上面3个参数分别是发件人地址，发件人姓名，编码*/

            msg.To.Add(toMail);  //收件人地址
            msg.Subject = subject;   //邮件标题 
            msg.SubjectEncoding = encoding;    //邮件标题编码 
            msg.Body = body;  //邮件内容 
            msg.BodyEncoding = encoding;   //邮件内容编码 
            msg.IsBodyHtml = isBodyHtml; //是否是HTML邮件 

            msg.Priority = MailPriority.High;   //邮件优先级 

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPwd);//SMTP的账号和密码 

            client.Port = smtpPort;  //SMTP使用的端口 
            client.Host = smtpHost; //SMTP主机地址
            client.EnableSsl = smtpSsl;        //经过ssl加密 
            //object userState = msg;
            try
            {
                if (isAsync)
                {
                    //client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                    client.SendAsync(msg, null);
                    
                }
                else
                    client.Send(msg);
                //
                
                return "发送成功。";
            }
            catch (Exception ex)
            {
                return ex.Message ;
            }
        }

        static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DW.Framework.Logger.Log.Write(e.Error.Message);
        }

        /// <summary>
        /// 发送邮件（其他信息从配置文件读取）
        /// </summary>
        /// <param name="toMail">收件人地址。多个电子邮件地址必须用逗号字符（“,”）分隔。</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns></returns>
        
        //public static string SendMail(string toMail, string subject, string body)
        //{
        //    //从配置文件读取基本信息
        //    string fromMail = Common.Config.Config.AppSettings("fromMail");
        //    string fromName = Common.Config.Config.AppSettings("fromName");
        //    bool isBodyHtml = Convert.ToBoolean(Common.Config.Config.AppSettings("isBodyHtml"));
        //    string smtpHost = Common.Config.Config.AppSettings("smtpHost");
        //    int smtpPort = Convert.ToInt32(Common.Config.Config.AppSettings("smtpPort"));
        //    bool smtpSsl = Convert.ToBoolean(Common.Config.Config.AppSettings("smtpSsl"));
        //    string smtpUser = Common.Config.Config.AppSettings("smtpUser");
        //    string smtpPwd = Common.Config.Config.AppSettings("smtpPwd");
        //    Encoding encoding = Encoding.UTF8;

        //    MailMessage msg = new MailMessage();

        //    msg.From = new MailAddress(fromMail, fromName, encoding);
        //    /* 上面3个参数分别是发件人地址，发件人姓名，编码*/

        //    msg.To.Add(toMail);  //收件人地址
        //    msg.Subject = subject;   //邮件标题 
        //    msg.SubjectEncoding = encoding;    //邮件标题编码 
        //    msg.Body = body;  //邮件内容 
        //    msg.BodyEncoding = encoding;   //邮件内容编码 
        //    msg.IsBodyHtml = isBodyHtml; //是否是HTML邮件 

        //    msg.Priority = MailPriority.High;   //邮件优先级 

        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPwd);//SMTP的账号和密码 

        //    client.Port = smtpPort;  //SMTP使用的端口 
        //    client.Host = smtpHost; //SMTP主机地址
        //    client.EnableSsl = smtpSsl;        //经过ssl加密 
        //    //object userState = msg;
        //    try
        //    {
        //        client.Send(msg);
        //        //client.SendAsync(msg, userState);

        //        return "发送成功。";
        //    }
        //    catch (System.Net.Mail.SmtpException ex)
        //    {
        //        return ex.Message;
        //    }
        //}
       
    }
}