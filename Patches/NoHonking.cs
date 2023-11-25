using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public static class NoHonking {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(AudioSource).GetMethod(nameof(AudioSource.PlayOneShot), BindingFlags.Public | BindingFlags.Instance, null, new[] {typeof(AudioClip), typeof(float)}, null);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref AudioClip clip) {
            Debug.Log($"---Clip.name: {clip.name}");
            return true;
        }
    }
}