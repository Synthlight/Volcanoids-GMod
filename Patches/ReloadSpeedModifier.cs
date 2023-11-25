using System;
using System.Linq;
using System.Reflection;
using Base_Mod;
using Base_Mod.Models;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    // Reload speed.
    public static class ReloadSpeedModifier1 {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var itemDef in RuntimeAssetDatabase.Get<ItemDefinition>().OfType<ToolItemDefinition>()) {
                if (itemDef.TryGetComponent(out WeaponReloaderAmmo reloaderAmmo)) {
                    reloaderAmmo.Definition.ReloadTime *= Plugin.config.reloadSpeedMultiplier;
                }
            }
        }
    }

    // Reload animation speed.
    [HarmonyPatch]
    [UsedImplicitly]
    public static class ReloadSpeedModifier2 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(WeaponAnimationDuration).GetProperty(nameof(WeaponAnimationDuration.SpeedMultiplier), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix(ref WeaponAnimationDuration __instance, ref float __result) {
            try {
                if (__instance.m_animatorParameter != "ReloadSpeed") return;
                __result /= Plugin.config.reloadSpeedMultiplier;
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
        }
    }
}