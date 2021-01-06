using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class InventoryPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(Inventory).GetProperty(nameof(Inventory.Capacity), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref int __result, ref int ___m_capacity) {
            try {
                __result = (int) (___m_capacity * DevSettings.Instance.InventorySizeMultiplier * Plugin.config.inventorySizeMultiplier);

                return false;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
                return true;
            }
        }
    }
}