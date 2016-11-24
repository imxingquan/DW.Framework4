using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace DW.Framework.Utils
{
    public class ValidateImage
    {
        private int imageWidth = 55;
        private int imageHeight = 22;
        private int fontSize = 12;
        private string validValue="";

        public void SetImageWH(int width, int height)
        {
            this.imageHeight = height;
            this.imageWidth = width;

        }
        public void SetFontSize(int fontSize)
        {
            this.fontSize = fontSize;
        }

        public string ValidateValue
        {
            get { return this.validValue; }
        }
        //验证码
        public string GetValidateStr()
        {
            string retVal = "";
            int num1;
            int num2;
            int v;
            Random rnd = new Random();
            num1 = rnd.Next(1, 99);
            num2 = rnd.Next(1, 10);

            if (num1 % 2 == 0)
            {
                v = num1 + num2;
                if (num1 > 30)
                {
                    retVal = string.Format("{0}加{1}=", num1, num2);
                }
                else
                {
                    retVal = string.Format("{0}+{1}=", num1, num2);
                }
            }
            else
            {
                v = num1 - num2;
                if (num1 > 20)
                {
                    retVal = string.Format("{0}減{1}=", num1, num2);
                }
                else
                {
                    retVal = string.Format("{0}-{1}=", num1, num2);
                }
            }
            this.validValue = v.ToString();


            return retVal;
        }
        //生成验证码图片
        public void GenValidateImage(string content)
        {
            Random r = new Random();
            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            
            string[] fonts = {"隶书","宋体","楷书","方正舒体","华文新魏"};
            string[] colors = { "Blue","BlueViolet","Brown","BurlyWood","CadetBlue","Chartreuse","Chocolate",
                                "Coral","CornflowerBlue","Crimson","DarkBlue","DarkCyan","DarkGreen","DarkMagenta",
                                "DarkOliveGreen","DarkOrchid","DarkSlateBlue","Indigo","Maroon","MediumBlue","OrangeRed"};
            Font font = new Font(fonts[r.Next(0,fonts.Length)], fontSize, FontStyle.Bold);
            string colorName = colors[r.Next(0, colors.Length)];
            Brush brush = new SolidBrush(Color.FromName(colorName));
            g.DrawString(content, font, brush, 0, 1);

            
            for (int i = 0; i < 20; i++)
            {   Pen pen = new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));
                int x = r.Next(0, imageWidth);
                int y = r.Next(0, imageHeight);
                g.DrawEllipse(pen, x, y, r.Next(1, 8), r.Next(1, 8));
            }

            bitmap = TwistImage(bitmap, false, r.Next(1, 9), r.Next(1, 7));
            bitmap.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
            bitmap.Dispose();
            g.Dispose();
        }

        #region 图像扭曲

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        /// <param >波形的幅度倍数</param>
        /// <param >波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                    && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }

        #endregion
    }
}
