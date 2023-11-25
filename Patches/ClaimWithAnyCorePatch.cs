using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public static class ClaimWithAnyCorePatch1 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccess).GetMethod(nameof(TrainCoreAccess.CanInstall), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref bool __result) {
            try {
                if (Plugin.config.claimWithAnyCore) {
                    __result = true;
                    return false;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }
    }

    [HarmonyPatch]
    [UsedImplicitly]
    public static class ClaimWithAnyCorePatch2 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccess).GetMethod(nameof(TrainCoreAccess.IsBetterCore), BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref bool __result) {
            try {
                if (Plugin.config.claimWithAnyCore) {
                    __result = true;
                    return false;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }
    }
}