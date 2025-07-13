using System.Collections;
using ProjetoIV.Util;
using UnityEngine;

public class UIClipIntro : MonoBehaviour
{

    [SerializeField] private float m_waitTime = 2f;

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(m_waitTime);

        SceneLoader.Instance.Load(SceneLoader.Scene.SCN_Menu);
    }

}
