//-----------------------------------------------------------------------
// <copyright file="ILogsProvider.cs" company="im@xingquan.org">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: 2012/7/31 9:04:27
// </copyright>
// <Author>im@xingquan.org</Author>
// <version>1.0</version>
// <summary>
//  自定义操作的接口定义，例如
//    bool Login(string username,string password);
//    具体实现在实现接口类里完成
// </summary>
//-----------------------------------------------------------------------

#region using

using System;
using System.Collections.Generic;
using System.Text;

using DW.Framework.Data;
using DW.Framework.Pager;
using DW.Framework.Logger.Entities;

#endregion

namespace DW.Framework.Logger.DAL.Interface
{
    public interface ILogsProvider : IDataProvider,IDataProvider<Logs>
    {  
        #region 操作接口

        
        #endregion
    }
}