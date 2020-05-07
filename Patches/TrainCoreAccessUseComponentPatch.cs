using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainCoreAccessUseComponentPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainCoreAccessUseComponent).GetMethod("IsEnabledForUse", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float ___m_claimHealthLevel) {
            try {
                ___m_claimHealthLevel = GMod.config.shipClaimHealthLevel;
            } catch (Exception e) {
                GMod.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}