using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class TrainCorePatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainCore).GetProperty(nameof(TrainCore.SlotCapacity), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref int __result, ref TrainUpgrades ___m_upgrades) {
            if (___m_upgrades == null || ___m_upgrades.Core == null) return true;

            try {
                __result = (int) (___m_upgrades.Core.SlotCount * Plugin.config.coreSlotMultiplier);

                return false;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
                return true;
            }
        }
    }
}