using Assets.Plugins.RatLocalization.Scripts;
using UnityEngine;

namespace ProjetoIV.Util
{
    public class GameLocalization : Singleton<GameLocalization>
    {
        void Awake()
        {
            LocalizationManager.Read();

            LocalizationManager.Language = Application.systemLanguage switch
            {
                SystemLanguage.Italian => "Italian",
                SystemLanguage.Portuguese => "Portuguese",
                _ => "English",
            };
        }

        public void SetLocalization(string localization)
        {
            LocalizationManager.Language = localization;
        }

        public string Localize(string p_localizeText)
        {
            return LocalizationManager.Localize(p_localizeText);
        }

        public string Localize(string p_localizeText, string p_arguments)
        {
            return LocalizationManager.Localize(p_localizeText, p_arguments);
        }
    }
}