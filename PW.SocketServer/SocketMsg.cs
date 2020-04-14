using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace PW.SocketServer
{
    public class SocketMsg
    {
        /// <summary>
        /// 请求的参数
        /// </summary>
        protected Hashtable parameters;

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <returns></returns>
        public string getParameter(string parameter)
        {
            string s = (string)parameters[parameter];
            return (null == s) ? "" : s;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter">参数名</param>
        /// <param name="parameterValue">参数值</param>
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }

        /// <summary>
        /// 获取所有参数
        /// </summary>
        /// <returns></returns>
        public Hashtable getAllParameters()
        {
            return this.parameters;
        }

        /// <summary>
        /// 获取所有参数Json
        /// </summary>
        /// <returns></returns>
        public String getAllParametersJsonStr()
        {
            JavaScriptSerializer jsser = new JavaScriptSerializer();
            String jsonStr = jsser.Serialize(this.parameters);
            return jsonStr;
        }

        /// <summary>
        /// 获取所有参数Json
        /// </summary>
        /// <returns></returns>
        public void setAllParametersJsonStr(String jsonStr)
        {
            JavaScriptSerializer jsser = new JavaScriptSerializer();
            parameters = jsser.Deserialize<Hashtable>(jsonStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String ConvertJson2Str(object json)
        {
            try
            {
                JavaScriptSerializer jsser = new JavaScriptSerializer();
                String jsonStr = jsser.Serialize(json);
                return jsonStr;
            }
            catch
            {
                return json.ToString();
            }
        }
    }
}
