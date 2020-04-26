using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PW.Common
{
    public static class CodeGeneratorUtil
    {
        public static string object2xml<T>(this T value)
        {
            if (value == null) return string.Empty;

            var xmlserializer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true, Encoding = UTF8Encoding.UTF8, OmitXmlDeclaration=false }))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                }
            }
        }
        public static XmlReader string2xml(string xml)
        {
            XmlReader readerXml = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(xml)));
            return readerXml;
        }
        public static XmlReader ConvertDataTableToXml(DataTable dt)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.AppendLine("<TabelModel>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strXml.AppendLine("<Rows>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strXml.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j] + "</" + dt.Columns[j].ColumnName + ">");
                }
                strXml.AppendLine("</Rows>");
            }
            strXml.AppendLine("</TabelModel>");
            XmlReader readerXml = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(strXml.ToString())));
            return readerXml;
        }
        public static bool TransferXml(string xml, string XslPath, string TargetFileName)
        {
            XslCompiledTransform xslTran = new XslCompiledTransform();
            XmlReader readerXml = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(xml)));
            XmlTextWriter xw = null;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(TargetFileName);
                if (!Directory.Exists(dir.Parent.FullName))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(dir.Parent.FullName);
                }
                xw = new XmlTextWriter(TargetFileName, Encoding.UTF8);
                xslTran.Load(XslPath);
                xslTran.Transform(readerXml, xw);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                if (xw != null)
                {
                    xw.Flush();
                    xw.Close();
                }
            }
        }

        public static string TransferXmlToString(XmlDocument XmlDoc, string XsltPath)
        {
            //获取Model类模板
            string ResultStr = "";
            MemoryStream ms = null;
            StreamReader sr = null;
            try
            {
                XslCompiledTransform XTran = new XslCompiledTransform();
                XTran.Load(XsltPath);
                ms = new System.IO.MemoryStream();
                XPathNavigator nav = XmlDoc.CreateNavigator();
                XTran.Transform(nav, null, ms);
                ms.Position = 0;
                sr = new StreamReader(ms);
                ResultStr = sr.ReadToEnd();
                ResultStr = ResultStr.Replace("<?xml version=\"1.0\" encoding=\"utf - 8\"?>", "");
                return ResultStr;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
        }

        public static string toHump(string str,bool startUpper = true)
        {
            string[] strarr = str.Split('_');
            str = "";
            foreach (var item in strarr)
            {
                if (item.Length > 0)
                {
                    str += item.Substring(0, 1).ToUpper() + item.Substring(1).ToLower();
                }
            }
            if (!startUpper)
            {
                str = str.Substring(0, 1).ToLower() + str.Substring(1);
            }
            return str;
        }

        public static string startLower(string str)
        {
            str = str.Substring(0, 1).ToLower() + str.Substring(1);
            return str;
        }
    }
}
