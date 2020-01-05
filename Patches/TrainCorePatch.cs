using System.Reflection;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class TrainCorePatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(TrainCore).GetProperty(nameof(TrainCore.SlotCapacity), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref int __result, ref TrainUpgrades ___m_upgrades) {
            __result = ___m_upgrades.Core.SlotCount * 2;
            return false;
        }
    }
}