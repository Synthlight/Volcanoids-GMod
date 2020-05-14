using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class InventoryPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(Inventory).GetProperty(nameof(Inventory.Capacity), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref int __result, ref int ___m_capacity) {
            try {
                __result = (int) (___m_capacity * DevSettings.Instance.InventorySizeMultiplier * GMod.config.inventorySizeMultiplier);

                return false;
            } catch (Exception e) {
                GMod.Log(LogLevel.Error, e.ToString());
                return true;
            }
        }
    }
}