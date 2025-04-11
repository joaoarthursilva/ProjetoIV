using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Plugins.RatLocalization.Scripts
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        private string LocalizationKey;

        [HideInInspector] public int listIndex = 0;
        [HideInInspector] public List<string> LocalizationKeys = new();

        public void Awake()
        {
            LocalizationKey = LocalizationKeys[listIndex].Replace("/", ".");
        }

        public void Start()
        {
            Localize();
            LocalizationManager.OnLocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.OnLocalizationChanged -= Localize;
        }

        private void Localize()
        {
            GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
        }

    }
}