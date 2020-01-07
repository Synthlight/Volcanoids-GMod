using System.Reflection;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainUpgradesPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainUpgrades).GetProperty(nameof(TrainUpgrades.Radar), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float __result) {
            __result = 5000f;
            return false;
        }
    }
}