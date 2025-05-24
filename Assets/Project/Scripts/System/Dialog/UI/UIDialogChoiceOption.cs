using NaughtyAttributes;
using ProjetoIV.Util;
using TMPro;
using UnityEngine;

namespace RatSpeak
{
    public class UIDialogChoiceOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_choicePreviewText;
        [SerializeField] private UIAnimationBehaviour m_choiceAnimation;
        [ReadOnly] public bool isActive;

        public void Set(Dialog p_dialog)
        {
            // m_choicePreviewText.text = p_dialog.dialogChoicePreviewText;
        }

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        public void Show()
        {
            m_choiceAnimation.PlayEntryAnimations();
            isActive = true;
        }

        [Button(enabledMode: EButtonEnableMode.Playmode), ShowIf("m_debug")]
        public void Hide()
        {
            m_choiceAnimation.PlayLeaveAnimations();
            isActive = false;
        }

        [Header("Debug"), SerializeField] private bool m_debug;
    }
}