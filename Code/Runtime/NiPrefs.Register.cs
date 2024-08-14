using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        private static bool _init;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
#if UNITY_EDITOR
            var domainReloadDisabled = 
                UnityEditor.EditorSettings.enterPlayModeOptionsEnabled && 
                (UnityEditor.EditorSettings.enterPlayModeOptions & UnityEditor.EnterPlayModeOptions.DisableDomainReload) != 0;
            
            if (_init && !domainReloadDisabled) return;
#else
            if (_init) return;
#endif
            
            RegisterProvider<string, StringPlayerPrefsProvider>();
            
            RegisterProvider<int, IntPlayerPrefsProvider>();
            RegisterProvider<float, FloatPlayerPrefsProvider>();
            RegisterProvider<bool, BoolPlayerPrefsProvider>();
            
            RegisterProvider<Color, ColorPlayerPrefsProvider>();
            RegisterProvider<Color32, Color32PlayerPrefsProvider>();
            
            RegisterProvider<Vector2, Vector2PlayerPrefsProvider>();
            RegisterProvider<Vector2Int, Vector2IntPlayerPrefsProvider>();
            RegisterProvider<Vector3, Vector3PlayerPrefsProvider>();
            RegisterProvider<Vector3Int, Vector3IntPlayerPrefsProvider>();
            RegisterProvider<Vector4, Vector4PlayerPrefsProvider>();
            
            RegisterProvider<Quaternion, QuaternionPlayerPrefsProvider>();
            
            RegisterProvider<Resolution, ResolutionPlayerPrefsProvider>();
            
            _init = true;
        }
    }
}