using ProjetoIV.Util;
using TMPro;
using UnityEngine;

public class UITipManager : MonoBehaviour
{
    [SerializeField] private UIAnimationBehaviour m_animationBehaviour;
    [SerializeField] private TextMeshProUGUI m_titleText;
    [SerializeField] private TextMeshProUGUI m_contentText;

    public void ShowTip(string p_title, string p_content)
    {
        m_titleText.text = p_title;
        m_contentText.text = p_content;
        m_animationBehaviour.PlayEntryAnimations();
    }

    public void HideTip()
    {
        m_animationBehaviour.PlayLeaveAnimations();
    }
}
