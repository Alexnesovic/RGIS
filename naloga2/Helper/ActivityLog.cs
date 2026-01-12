using System.Text.Json;

namespace LevelStat_AlexNesovic.Helpers
{
    public record ActivityItem(DateTime WhenUtc, string Message);

    public static class ActivityLog
    {
        private const string Key = "ActivityLog";

        public static List<ActivityItem> Get(HttpContext http)
        {
            var json = http.Session.GetString(Key);
            if (string.IsNullOrWhiteSpace(json))
                return new List<ActivityItem>();

            try
            {
                return JsonSerializer.Deserialize<List<ActivityItem>>(json) ?? new List<ActivityItem>();
            }
            catch
            {
                return new List<ActivityItem>();
            }
        }

        public static void Add(HttpContext http, string message, int maxItems = 10)
        {
            var list = Get(http);

            list.Insert(0, new ActivityItem(DateTime.UtcNow, message));

            if (list.Count > maxItems)
                list = list.Take(maxItems).ToList();

            http.Session.SetString(Key, JsonSerializer.Serialize(list));
        }

        public static void Clear(HttpContext http) => http.Session.Remove(Key);
    }
}
