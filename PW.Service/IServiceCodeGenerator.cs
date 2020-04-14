using PW.DBCommon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PW.Service
{
    [ServiceContract]
    public interface IServiceCodeGenerator
    {

        [OperationContract]
        List<table> queryTables();

        [OperationContract]
        List<table> queryViews();

        [OperationContract]
        List<column> queryColumns(string tableName);
    }
}
