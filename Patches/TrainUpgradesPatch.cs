using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class TrainUpgradesPatch1 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainUpgrades).GetProperty(nameof(TrainUpgrades.Radar), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix(ref float __result, ref float ___m_radar) {
            try {
                __result = (int) (___m_radar * Plugin.config.radarRangeMultiplier);
                return false;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }
    }

    [HarmonyPatch]
    [UsedImplicitly]
    public class TrainUpgradesPatch2 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(TrainUpgrades).GetMethod(nameof(TrainUpgrades.ValidateUpgrade), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyTranspiler]
        [UsedImplicitly]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            var il = instructions.ToList();

            try {
                if (Plugin.config.segmentSizeMultiplier > 1) {
                    il.InsertRange(115, new List<CodeInstruction> {
                        new CodeInstruction(OpCodes.Ldc_I4, Plugin.config.segmentSizeMultiplier),
                        new CodeInstruction(OpCodes.Mul)
                    });
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }

            return il;
        }
    }
}