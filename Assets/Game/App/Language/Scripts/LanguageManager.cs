using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.App
{
    public sealed class LanguageManager : MonoBehaviour
    {
        public static event Action<SystemLanguage> OnLanguageChanged;

        public static SystemLanguage CurrentLanguage
        {
            get { return GetCurrentLanguage(); }
            set { SetCurrentLanguage(value); }
        }

        private static LanguageManager instance;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        private SystemLanguage currentLanguage = default;

        [SerializeField]
        private bool useSystemLanguage;

        [HideIf("useSystemLanguage")]
        [SerializeField]
        private SystemLanguage initialLanguage;
        
        private void Awake()
        {
            if (instance != null)
            {
                throw new Exception("Instance is already created!");
            }

            instance = this;
            this.InitLanguage();
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void InitLanguage()
        {
            if (this.useSystemLanguage)
            {
                this.currentLanguage = Application.systemLanguage;
            }
            else
            {
                this.currentLanguage = this.initialLanguage;
            }
        }

        private static SystemLanguage GetCurrentLanguage()
        {
            if (!ReferenceEquals(instance, null))
            {
                return instance.currentLanguage;
            }

            return default;
        }

        public static void SetCurrentLanguage(SystemLanguage language)
        {
            instance.currentLanguage = language;
            OnLanguageChanged?.Invoke(language);
        }

#if UNITY_EDITOR
        [PropertySpace(10)]
        [Button("Set Language")]
        private void Editor_SetLanguage(SystemLanguage language)
        {
            SetCurrentLanguage(language);
        }
#endif
    }
}