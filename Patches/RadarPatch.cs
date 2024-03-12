using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches;

[HarmonyPatch]
[UsedImplicitly]
public static class RadarPatch {
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