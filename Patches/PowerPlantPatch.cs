using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class PowerPlantPatch1 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix(ref PowerPlant __instance) {
            try {
                __instance.EnergyPerSecond *= Plugin.config.powerPerSecondMultiplier;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
        }
    }

    [HarmonyPatch]
    [UsedImplicitly]
    public class PowerPlantPatch2 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetProperty(nameof(PowerPlant.AutoRegulation), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref bool __result) {
            try {
                if (Plugin.config.disablePowerPlantStopThreshold) {
                    __result = false;
                    return false;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }
    }
}