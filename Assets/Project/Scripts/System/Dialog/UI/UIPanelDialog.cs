using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace RatSpeak
{
    public class UIPanelDialog : MonoBehaviour
    {
        [Header("Dialog")]
        [SerializeField] private GameObject m_choiceOptionPrefab;

        [SerializeField] private Transform m_choiceOptionParent;
        [SerializeField] private List<UIDialogChoiceOption> m_choiceOptions;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        public void Show(Dialog p_dialog)
        {
        }

        public void ShowOptions(List<Dialog> p_dialog)
        {
            for (int i = 0; i < p_dialog.Count; i++)
            {
                if (i >= m_choiceOptions.Count)
                {
                    UIDialogChoiceOption newOption =
                        Instantiate(m_choiceOptionPrefab, m_choiceOptionParent).GetComponent<UIDialogChoiceOption>();
                    m_choiceOptions.Add(newOption);
                }

                m_choiceOptions[i].Set(p_dialog[i]);
                m_choiceOptions[i].Show();
            }
        }

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DeactivateOptions()
        {
            for (int i = 0; i < m_choiceOptions.Count; i++)
            {
                m_choiceOptions[i].Hide();
            }
        }

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        public void Hide()
        {
        }

        [Header("Debug"), SerializeField] private bool m_debug;

        [SerializeField, ShowIf("m_debug")] private List<Dialog> m_debugDialogOptions;
        [SerializeField, ShowIf("m_debug")] private Dialog m_debugShowDialog;

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowDialog() => Show(m_debugShowDialog);

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        private void DebugShowOptions() => ShowOptions(m_debugDialogOptions);
    }
}