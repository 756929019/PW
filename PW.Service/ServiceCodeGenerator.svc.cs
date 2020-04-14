using PW.DBCommon.Dao;
using PW.DBCommon.Model;
using System.Collections.Generic;
using System;

namespace PW.Service
{
    public class ServiceCodeGenerator : IServiceCodeGenerator
    {
        public List<column> queryColumns(string tableName)
        {
            return new CodeGeneratorDao().queryColumns(tableName);
        }

        public List<table> queryTables()
        {
            return new CodeGeneratorDao().queryTables();
        }

        public List<table> queryViews()
        {
            return new CodeGeneratorDao().queryViews();
        }
    }
}
