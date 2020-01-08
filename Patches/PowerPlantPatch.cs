using System.Reflection;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class PowerPlantPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(PowerPlant).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static void Prefix(ref PowerPlant __instance) {
            __instance.EnergyPerSecond *= 2f;
            __instance.StopThreshold = 2f;
        }
    }
}