using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches;

[HarmonyPatch]
[UsedImplicitly]
public static class RunSpeedMultiplier {
    [HarmonyTargetMethod]
    [UsedImplicitly]
    public static MethodBase TargetMethod() {
        return typeof(PlayerMovement).GetProperty(nameof(PlayerMovement.RunSpeed), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
    }

    [HarmonyPostfix]
    [UsedImplicitly]
    public static void Postfix(ref float __result) {
        try {
            __result = (int) (__result * Plugin.config.playerRunSpeedMultiplier);
        } catch (Exception e) {
            Debug.LogError(e.ToString());
        }
    }
}