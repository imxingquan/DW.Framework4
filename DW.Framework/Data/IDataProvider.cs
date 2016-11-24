using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DW.Framework.Pager;

namespace DW.Framework.Data
{
    public interface IDataProvider 
    {
        string ConnectionString { get; set; }
    }

    public interface IDataProvider<T>
    {
        int Insert(T table);
        bool Update(T table);
        bool Update(string fields, string where);
        bool Delete(int id);
        bool Delete(string where);
        int GetCount(string where);
        int GetMaxID();
        // operator
        T GetById(int id);
        T GetByWhere(string where);
        IPagedList<T> GetTables(string where, string sortExpression, int startRowIndex, int maximumRows);

    }
}
