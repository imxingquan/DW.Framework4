//-----------------------------------------------------------------------
// <copyright file="IUsersProvider.cs" company="$company$">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: 2012/5/31 3:06:34
// </copyright>
// <Author>im@xingquan.org</Author>
// <version>1.0.1</version>
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
using TestDB.Entities;

#endregion

namespace TestDB.DAL.Interface
{
    public interface IUsersProvider : IDataProvider,IDataProvider<Users>
    {  
        #region 操作接口

        
        #endregion
    }
}