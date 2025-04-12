using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Plugins.RatLocalization.Scripts
{
    /// <summary>
    /// Localize dropdown component.
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    public class LocalizedDropdown : MonoBehaviour
    {
        public List<LocalizedDropdownText> LocalizationKeys = new();

        public void Awake()
        {
            foreach (var item in LocalizationKeys)
            {
                item.LocalizationKey = item.LocalizationKeys[item.listIndex].Replace("/", ".");
            }
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
            var dropdown = GetComponent<TMP_Dropdown>();

            for (var i = 0; i < LocalizationKeys.Count; i++)
            {
                dropdown.options[i].text = LocalizationManager.Localize(LocalizationKeys[i].LocalizationKey);
            }

            if (dropdown.value < LocalizationKeys.Count)
            {
                dropdown.captionText.text = LocalizationManager.Localize(LocalizationKeys[dropdown.value].LocalizationKey);
            }
        }
    }

    [System.Serializable]
    public class LocalizedDropdownText
    {
        public string LocalizationKey;

        [HideInInspector] public int listIndex = 0;
        [HideInInspector] public List<string> LocalizationKeys = new();
    }
}