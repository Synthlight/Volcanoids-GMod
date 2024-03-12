using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches;

[HarmonyPatch]
[UsedImplicitly]
public static class StaminaPatch {
    [HarmonyTargetMethod]
    [UsedImplicitly]
    public static MethodBase TargetMethod() {
        return typeof(Stamina).GetProperty(nameof(Stamina.CurrentStamina), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
    }

    [HarmonyPrefix]
    [UsedImplicitly]
    public static bool Prefix(ref float __result, ref float ___m_maxStamina) {
        try {
            if (Plugin.config.infiniteStamina) {
                __result = ___m_maxStamina;
                return false;
            }
        } catch (Exception e) {
            Debug.LogError(e.ToString());
        }
        return true;
    }
}