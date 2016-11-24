//-----------------------------------------------------------------------
// <copyright file="$file$" company="$company$">
//     Copyright (c) Digitwest.com All rights reserved.
//     Website: http://digitwest.com
//	   Create Date: $date$
// </copyright>
// <Author>$author$</Author>
// <version>$ver$</version>
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
using $namespace$.Entities;

#endregion

namespace $namespace$.DAL.Interface
{
    public interface I$class$Provider : IDataProvider,IDataProvider<$class$>
    {  
        #region 操作接口

        
        #endregion
    }
}