using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TestProject1.Web.Helpers
{
    public static class ObjectExtensions
    {
     
        public static object FromJson(this string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

    }
}
