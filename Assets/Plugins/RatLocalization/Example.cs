using System;
using UnityEngine;
using TMPro;
using Assets.Plugins.RatLocalization.Scripts;

namespace Assets.Plugins.RatLocalization
{
	/// <summary>
	/// Asset usage example.
	/// </summary>
	public class Example : MonoBehaviour
	{
		public TextMeshProUGUI FormattedText;
		public TextMeshProUGUI KeywordsText;

		/// <summary>
		/// Called on app start.
		/// </summary>
		public void Awake()
		{
			LocalizationManager.Read();

            LocalizationManager.Language = Application.systemLanguage switch
            {
                SystemLanguage.Portuguese => "Portuguese",
                _ => "English",
            };

            // This way you can localize and format strings from code.
            FormattedText.text = LocalizationManager.Localize("Settings.Playtime", TimeSpan.FromHours(10.5f).TotalHours);
			KeywordsText.text = LocalizationManager.Localize("Tests.Keyword", LocalizationManager.Keyword("Keywords.Tortellini"));

			// This way you can subscribe to LocalizationChanged event.
			LocalizationManager.OnLocalizationChanged += () => FormattedText.text = LocalizationManager.Localize("Settings.Playtime", TimeSpan.FromHours(10.5f).TotalHours);
			LocalizationManager.OnLocalizationChanged += () => KeywordsText.text = LocalizationManager.Localize("Tests.Keyword", LocalizationManager.Keyword("Keywords.Tortellini"));
		}

		/// <summary>
		/// Change localization at runtime.
		/// </summary>
		public void SetLocalization(string localization)
		{
			LocalizationManager.Language = localization;
		}
	}
}