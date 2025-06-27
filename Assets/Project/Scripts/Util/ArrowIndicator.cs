using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace ProjetoIV.Util
{
    public class ArrowIndicator : Singleton<ArrowIndicator>
    {
        [SerializeField] private GameObject m_arrowObject;
        [SerializeField] private AnimationCurve m_upDownCurve;
        [SerializeField] private float m_upDownSpeed;
        [SerializeField] private float m_rotationSpeed;
        [SerializeField] private float m_yOffset;
        [SerializeField] private float m_upDownTime;
        [SerializeField] private Vector3 m_upPosition;
        [SerializeField] private Vector3 m_downPosition;

        private IEnumerator RotIdle()
        {
            float t = 0f;
            while (true)
            {
                t += Time.deltaTime;
                m_arrowObject.transform.rotation = Quaternion.Euler(-90f,
                    m_arrowObject.transform.rotation.y + (m_rotationSpeed * t),
                    m_arrowObject.transform.rotation.z);
                yield return null;
            }
        }

        private IEnumerator PosIdle()
        {
            while (true)
            {
                float t = 0f;
                while (!Mathf.Approximately(m_arrowObject.transform.localPosition.y, m_downPosition.y))
                {
                    t += Time.deltaTime;
                    m_arrowObject.transform.localPosition =
                        Vector3.Lerp(m_upPosition, m_downPosition,
                            m_upDownCurve.Evaluate(t / m_upDownTime * m_upDownSpeed));
                    yield return null;
                }

                t = 0f;

                while (!Mathf.Approximately(m_arrowObject.transform.localPosition.y, m_upPosition.y))
                {
                    t += Time.deltaTime;
                    m_arrowObject.transform.localPosition =
                        Vector3.Lerp(m_downPosition, m_upPosition,
                            m_upDownCurve.Evaluate(t / m_upDownTime));
                    yield return null;
                }
            }
        }

        public void Show(Vector3 p_position)
        {
            transform.position = new Vector3(p_position.x, p_position.y + m_yOffset, p_position.z);
            m_arrowObject.SetActive(true);
            StartCoroutine(PosIdle());
            StartCoroutine(RotIdle());
        }

        [Button]
        public void HideArrow()
        {
            m_arrowObject.SetActive(false);
            StopAllCoroutines();
        }

        [Header("Debug"), SerializeField] private bool m_debug;
        [SerializeField, ShowIf("m_debug")] private Vector3 m_debugPos;

        [Button, ShowIf("m_debug")]
        private void DebugShow() => Show(m_debugPos);
    }
}