using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainCoreAccessPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccess).GetProperty(nameof(TrainCoreAccess.IsClaimable), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float ___m_claimHealthLevel) {
            try {
                ___m_claimHealthLevel = Plugin.config.shipClaimHealthLevel;
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}