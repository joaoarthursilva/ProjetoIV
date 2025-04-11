using System.Collections.Generic;

namespace Assets.Plugins.RatLocalization.Scripts
{
    public static class Constants
    {
        public const string LocalizationEditorUrl = "https://script.google.com/macros/s/AKfycbwu1UGE4Kc8vXy6VSeUw4KW752HkGiSS7cBiCznVBjIzsrYUpgIMZ4OVsnrwoRXuvR1IA/exec";
        public const string SheetResolverUrl = "https://script.google.com/macros/s/AKfycbyGSo-3U6Znhl1sUvJ22bKpm5XNOrL8v9qxDZxGVMk4Y2KZs9U-W-l5qYGO3M7qkyTVAg/exec";
        public const string TableUrlPattern = "https://docs.google.com/spreadsheets/d/{0}";
        public const string ExampleTableId = "13hIETEoV0q9yn1ktnaZieTu1JizeKy4kyyX3akOV5R8";
        public static readonly Dictionary<string, int> ExampleSheets = new() { { "Menu", 0 }, { "Settings", 1378613232 }, { "Tests", 1679736010 }, { "Keywords", 1408209378 } };
    }
}