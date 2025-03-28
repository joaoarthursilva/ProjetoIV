using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIV.Util
{
    public class UIFollowObject : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private bool startFollowingOnEnable;

        [SerializeField] bool isStaticPosition;
        public float positionOffsetX;
        public float positionOffsetY;
        public float positionOffsetZ;
        [SerializeField] float minScaleSize;
        [SerializeField] float maxScaleSize;
        public bool useScreenPoint;

        [Header("Components")]
        public RectTransform uiElement;

        public Transform gameObjectToFollow = null;
        public Vector3 positionToFollow = Vector3.zero;

        public void Setup(Transform l_objectToFollow, bool p_isStaticPosition = false)
        {
            this.gameObjectToFollow = l_objectToFollow;
            this.isStaticPosition = p_isStaticPosition;
            StartCoroutine(FollowGameobject());
        }

        public void Setup(Vector3 l_positionToFollow, bool p_isStaticPosition = false)
        {
            this.positionToFollow = l_positionToFollow;
            this.isStaticPosition = p_isStaticPosition;
            StartCoroutine(FollowGameobject());
        }

        private void OnEnable()
        {
            if (!startFollowingOnEnable) return;
            if (isStaticPosition) SetUIElementPosition();
            else StartCoroutine(FollowGameobject());
        }

        private void OnDisable()
        {
            StopCoroutine(FollowGameobject());
        }

        IEnumerator FollowGameobject()
        {
            while (true)
            {
                SetUIElementPosition();
                yield return null;
            }
        }

        float l_tempObjectScale;
        float l_tempObjectDistance;

        private void SetUIElementPosition()
        {
            l_tempObjectScale = Mathf.Lerp(maxScaleSize, minScaleSize, l_tempObjectDistance / 20f);
            uiElement.localScale = new Vector3(l_tempObjectScale, l_tempObjectScale, 1f);

            if (gameObjectToFollow != null)
            {
                l_tempObjectDistance = Vector3.Distance(gameObjectToFollow.position, Camera.main.transform.position);

                if (useScreenPoint)
                    uiElement.position = Camera.main.ViewportToScreenPoint(gameObjectToFollow.position +
                                                                           new Vector3(positionOffsetX, positionOffsetY,
                                                                               positionOffsetZ));
                else
                    uiElement.position = Camera.main.WorldToScreenPoint(gameObjectToFollow.position +
                                                                        new Vector3(positionOffsetX, positionOffsetY,
                                                                            positionOffsetZ));
            }
            else if (positionToFollow != Vector3.zero)
            {
                l_tempObjectDistance = Vector3.Distance(positionToFollow, Camera.main.transform.position);

                if (useScreenPoint)
                    uiElement.position = Camera.main.ViewportToScreenPoint(positionToFollow +
                                                                           new Vector3(positionOffsetX, positionOffsetY,
                                                                               positionOffsetZ));
                else
                    uiElement.position = Camera.main.WorldToScreenPoint(positionToFollow +
                                                                        new Vector3(positionOffsetX, positionOffsetY,
                                                                            positionOffsetZ));
            }
        }
    }
}