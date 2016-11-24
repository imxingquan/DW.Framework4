/*******************
 * 容量转换
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DW.Framework.XConvert
{
    /// <summary>
    /// 容量转换
    /// </summary>
    public class Capacity
    {
        /// <summary>
        /// Bytes 转换成 MB
        /// </summary>
        /// <remarks>1MB = 1024KB = 1048576 Bytes</remarks>
        public static decimal BytesToMB(decimal bytes, int decimals)
        {
            return Math.Round(bytes / 1048576, decimals);
        }

        /// <summary>
        /// Bytes 转换成 KB
        /// </summary>
        public static decimal BytesToKB(decimal bytes, int decimals)
        {
            return Math.Round(bytes / 1024, decimals);
        }

        /// <summary>
        /// MB 转换岩层 Bytes
        /// </summary>
        public static decimal MbToBytes(decimal mb)
        {
            return mb * 1048576;
        }

        /// <summary>
        /// MB 转换成 KB
        /// </summary>
        public static decimal MbToKb(decimal mb)
        {
            return mb * 1024;
        }

        /// <summary>
        /// KB 转换成 Bytes
        /// </summary>
        public static decimal KbToBytes(decimal kb)
        {
            return kb * 1024;
        }

        /// <summary>
        /// KB 转换成 MB
        /// </summary>
        public static decimal KbToMb(decimal kb, int decimals)
        {
            return Math.Round(kb / 1024, decimals);
        }
    }
}
