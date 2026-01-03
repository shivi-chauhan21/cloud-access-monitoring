using File_Access_Monitoring.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
namespace File_Access_Monitoring
{
    public static class SessionExtension
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void Set(string key, object value)
        {
            //HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
            ////session.SetString(key, JsonConvert.SerializeObject(value));
        }

    }
}
