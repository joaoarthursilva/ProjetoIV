using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.RatLocalization.Scripts.Editor
{
    /// <summary>
    /// Adds "Sync" button to LocalizationSync script.
    /// </summary>
    [CustomEditor(typeof(LocalizedDropdown))]
    public class LocalizedDropdownEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var component = (LocalizedDropdown)target;

            for (int i = 0; i < component.GetComponent<TMP_Dropdown>().options.Count; i++)
            {
                int option = i + 1;
                GUIContent arrayLabel = new GUIContent("Localization Key #" + option);

                if (component.GetComponent<TMP_Dropdown>().options.Count < component.LocalizationKeys.Count)
                {
                    component.LocalizationKeys.RemoveAt(component.LocalizationKeys.Count - 1);
                }
                else if (component.GetComponent<TMP_Dropdown>().options.Count > component.LocalizationKeys.Count)
                {
                    component.LocalizationKeys.Add(new LocalizedDropdownText());
                }
                component.LocalizationKeys[i].listIndex = EditorGUILayout.Popup(arrayLabel, component.LocalizationKeys[i].listIndex, component.LocalizationKeys[i].LocalizationKeys.ToArray());
            }


            if (GUILayout.Button("Sync Keys"))
            {
                LocalizationManager.Read();

                if (LocalizationManager.Dictionary.Keys.Count == 0) return;
                var first = LocalizationManager.Dictionary.First();

                foreach (var item in component.LocalizationKeys)
                {
                    item.LocalizationKeys.Clear();
                    foreach (var keys in first.Value.Keys)
                    {
                        item.LocalizationKeys.Add(keys.Replace(".", "/"));
                    }
                }

            }

            if (GUILayout.Button("Localization Editor"))
            {
                LocalizationEditorWindow.Open();
            }
        }
    }
}
