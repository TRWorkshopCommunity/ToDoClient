using System.Collections;
using System.Collections.Generic;
using DAL.Interface.Entities;
using Newtonsoft.Json;

namespace DAL.Common
{
    public class JsonSerializer
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
        public string Serialize(IEnumerable<object> items)
        {
            return JsonConvert.SerializeObject(items, Formatting.Indented);
        }

        public IEnumerable<T> Deserialize<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T[]>(json);
            return result;
        }

        public object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
