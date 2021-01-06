using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class ClaimHealthLevelPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccess).GetProperty(nameof(TrainCoreAccess.IsClaimable), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref float ___m_claimHealthLevel) {
            try {
                ___m_claimHealthLevel = Plugin.config.shipClaimHealthLevel;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }
    }
}