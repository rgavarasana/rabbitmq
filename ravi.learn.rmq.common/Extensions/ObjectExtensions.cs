using Newtonsoft.Json;
using System;
using System.Text;

namespace ravi.learn.rmq.common.Extensions
{
    public static class ObjectExtensions
    {
        public static byte[] Serialize(this Object obj)
        {
            if (obj == null)
                return null;

            var jsonData = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(jsonData);            
        }

        public static T Deserialize<T>(this byte[] byteData)
        {
            var jsonData = Encoding.Default.GetString(byteData);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static string DeserializeToText(this byte[] byteData)
        {
            return Encoding.Default.GetString(byteData);
        }
    }
}
