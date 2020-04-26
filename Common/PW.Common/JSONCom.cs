using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Common
{
    public class JSONCom
    {
        // 对象转换
        public static T ConvertObject<T>(object obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }

        public static T DBToViewModelObject<T>(object obj, Dictionary<string, string> mapping)
        {
            JsonData json = JsonMapper.ToObject(JsonMapper.ToJson(obj));
            ICollection<string> keys = mapping.Keys;
            JsonData toJson = new JsonData();
            foreach (string key in keys) {
                toJson[key] = json[mapping[key]];
            }
            return JsonMapper.ToObject<T>(toJson.ToJson());
        }

        public static T ViewModelToDBObject<T>(object obj, Dictionary<string, string> mapping)
        {
            JsonData json = JsonMapper.ToObject(JsonMapper.ToJson(obj));
            ICollection<string> keys = mapping.Keys;
            JsonData toJson = new JsonData();
            foreach (string key in keys)
            {
                toJson[mapping[key]] = json[key];
            }
            return JsonMapper.ToObject<T>(toJson.ToJson());
        }
    }
}
