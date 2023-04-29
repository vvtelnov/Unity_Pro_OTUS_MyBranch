using Game.App;

namespace Game.Tutorial.App
{
    public static class TutorialAnalytics
    {
        public static void LogTutorialStarted()
        {
            const string eventName = "tutorial_started";
            LogEventAndCache(eventName);
        }

        public static void LogTutorialCompleted()
        {
            const string eventName = "tutorial_completed";
            LogEventAndCache(eventName);
        }

        public static void LogEventAndCache(string eventName)
        {
            var key = "tutorial_analytics/" + eventName;
            if (PlayerPreferences.KeyExists(key))
            {
                return;
            }

            AnalyticsManager.LogEvent(eventName);
            PlayerPreferences.Save(key, 1);
        }
    }
}