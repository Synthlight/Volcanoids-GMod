using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public static class AimSwayPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(ToolCameraTravel).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix() {
            try {
                return !Plugin.config.disableAimSway;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
                return true;
            }
        }
    }
}