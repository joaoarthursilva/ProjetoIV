// using System.Linq;
// using UnityEditor;
// using UnityEngine;

// namespace Assets.Plugins.RatLocalization.Scripts.Editor
// {
//     /// <summary>
//     /// Adds "Sync" button to LocalizationSync script.
//     /// </summary>
//     [CustomEditor(typeof(LocalizedText))]
//     public class LocalizedTextEditor : UnityEditor.Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             DrawDefaultInspector();

//             var component = (LocalizedText)target;

//             GUIContent arrayLabel = new GUIContent("Localization Keys");
//             component.listIndex = EditorGUILayout.Popup(arrayLabel, component.listIndex, component.LocalizationKeys.ToArray());

//             if (GUILayout.Button("Sync Keys"))
//             {
//                 LocalizationManager.Read();

//                 if (LocalizationManager.Dictionary.Keys.Count == 0) return;
//                 component.LocalizationKeys.Clear();
//                 var first = LocalizationManager.Dictionary.First();

//                 foreach (var keys in first.Value.Keys)
//                 {
//                     component.LocalizationKeys.Add(keys.Replace(".", "/"));
//                 }

//             }

//             if (GUILayout.Button("Localization Editor"))
//             {
//                 LocalizationEditorWindow.Open();
//             }
//         }
//     }
// }