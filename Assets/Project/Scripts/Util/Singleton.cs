﻿using JetBrains.Annotations;
using UnityEngine;

namespace ProjetoIV.Util
{
    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        #region Fields

        [CanBeNull]
        private static T _instance;

        [NotNull]
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object Lock = new object();

        private static bool m_thereIsNoInstance = false;

        [SerializeField]
        private bool _persistent = false;

        #endregion

        #region Properties

        [NotNull]
        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    //Debug.LogWarning("[Singleton <" + typeof(T) + ">] Instance will not be returned because the application is quitting.");
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return null;
                }

                lock (Lock)
                {
                    if (_instance != null) return _instance;
                    //if (m_thereIsNoInstance) return null;

                    var instances = FindObjectsOfType<T>();
                    var count = instances.Length;
                    if (count > 0)
                    {
                        if (count == 1)
                            return _instance = instances[0];
                        Debug.LogWarning("[Singleton < " + typeof(T) +
                                         ">] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                        for (var i = 1; i < instances.Length; i++)
                            Destroy(instances[i]);
                        return _instance = instances[0];
                    }

                    //m_thereIsNoInstance = true;
                    //Debug.Log("[Singleton < " + typeof(T) + ">] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                    //return _instance = new GameObject("[MAN] " + typeof(T)).AddComponent<T>();
                    return null;
                }
            }
        }

        #endregion

        #region Methods

        private void Awake()
        {
            if (_instance != null
                && _instance.gameObject != gameObject)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Quitting = false;
                _instance = this as T;
            }

            if (_persistent)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        #endregion
    }

    public abstract class Singleton : MonoBehaviour
    {
        #region Properties

        public static bool Quitting { get; protected set; }

        #endregion

        #region Methods

        private void OnApplicationQuit()
        {
            Quitting = true;
        }

        #endregion
    }
}