using System;
using System.Reflection;
using BepInEx.Logging;
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
            if (___m_upgrades == null || ___m_upgrades.Core == null) return true;

            try {
                __result = (int) (___m_upgrades.Core.SlotCount * Plugin.config.coreSlotMultiplier);

                return false;
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
                return true;
            }
        }
    }
}