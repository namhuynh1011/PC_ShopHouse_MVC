using System.Text.Json;

namespace PC_ShopHouse.Models.Extentions
{
    public static class SessionExtentions
    {
        public static void SetOjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? GetObjectFromSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
