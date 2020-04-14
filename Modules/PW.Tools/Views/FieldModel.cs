using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PW.Tools.Views
{
    public class FieldModel
    {
        public string FieldName { get; set; }
        public string DbType { get; set; }
        public string FieldLength { get; set; }
        public string VarType { get; set; }
        public string ToVarConvertMethod { get; set; }
        public string DefaultValueVar { get; set; }
        public string Mark { get; set; }
        public string VarName { get; set; }
        public string VarNameLocal { get; set; }
        //        select* from sysdatabases where name not in ('master','model','msdb','tempdb')
        //select* from sysobjects where xtype='U' order by name
        //select* from sysobjects where xtype='V' order by name
        //select sysobjects.name , sys.extended_properties.value as comment, syscolumns.name  , systypes.name, syscolumns.length, syscolumns.xprec, syscolumns.xscale, syscolumns.isnullable
        //, IsIdentity = Case syscolumns.status when 128 then 1 else 0 end
        // , IsPK = Case
        //                        when exists
        //                        (
        //                        select 1 
        //                        from sysobjects
        //                        inner join sysindexes
        //                        on sysindexes.name = sysobjects.name
        //                        inner join sysindexkeys
        //                        on sysindexes.id = sysindexkeys.id
        //                        and sysindexes.indid = sysindexkeys.indid
        //                        where xtype = 'PK'
        //                        and parent_obj = syscolumns.id
        //                        and sysindexkeys.colid = syscolumns.colid
        //                        ) then 1
        //                        else 0 end
        //                                        from syscolumns inner join sysobjects on syscolumns.id=sysobjects.id
        //left join sys.extended_properties on sysobjects.id = sys.extended_properties.major_id and sys.extended_properties.minor_id= syscolumns.colid, systypes
        //                                        where
        //                                        syscolumns.xtype= systypes.xtype
        //                                        and sysobjects.xtype= 'U'

        //                                                                                and systypes.name<>'sysname'
        //                                        order by sysobjects.name, syscolumns.id
    }
}
