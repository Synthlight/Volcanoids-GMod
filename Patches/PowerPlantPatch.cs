using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class PowerPlantPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix(ref PowerPlant __instance) {
            try {
                if (GMod.config.disablePowerPlantStopThreshold) {
                    __instance.EnergyPerSecond *= 2f;
                    __instance.StopThreshold   =  2f;
                }
            } catch (Exception e) {
                GMod.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}