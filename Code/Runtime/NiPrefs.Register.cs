using System.Runtime.CompilerServices;
using NiGames.PlayerPrefs.Providers;
using UnityEngine;

namespace NiGames.PlayerPrefs
{
    public static partial class NiPrefs
    {
        [MethodImpl(256)]
        private static void Register()
        {
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
        }
    }
}