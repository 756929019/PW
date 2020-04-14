using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace PW.Infrastructure
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
            string jsonStr = JsonConvert.SerializeObject(this.parameters);
            return jsonStr;
        }

        /// <summary>
        /// 获取所有参数Json
        /// </summary>
        /// <returns></returns>
        public void setAllParametersJsonStr(String jsonStr)
        {
            parameters = JsonConvert.DeserializeObject<Hashtable>(jsonStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String ConvertJson2Str(object json)
        {
            try
            {
                String jsonStr = JsonConvert.SerializeObject(json);
                return jsonStr;
            }
            catch
            {
                return json.ToString();
            }
        }
    }
}
