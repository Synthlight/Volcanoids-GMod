using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainUpgradesPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainUpgrades).GetProperty(nameof(TrainUpgrades.Radar), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float __result, ref float ___m_radar) {
            try {
                __result = (int) (___m_radar * GMod.config.radarRangeMultiplier);
                return false;
            } catch (Exception e) {
                GMod.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}