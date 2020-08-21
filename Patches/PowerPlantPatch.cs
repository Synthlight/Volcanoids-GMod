using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class PowerPlantPatch1 {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        public static void Postfix(ref PowerPlant __instance) {
            try {
                __instance.EnergyPerSecond *= Plugin.config.powerPerSecondMultiplier;
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
        }
    }

    [HarmonyPatch]
    public class PowerPlantPatch2 {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetProperty(nameof(PowerPlant.AutoRegulation), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref bool __result) {
            try {
                if (Plugin.config.disablePowerPlantStopThreshold) {
                    __result = false;
                    return false;
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}