using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class WeaponReloaderAmmoPatch1 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(WeaponReloaderAmmo).GetMethod(nameof(WeaponReloaderAmmo.LoadAmmo), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = true;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = false;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
        }
    }

    [HarmonyPatch]
    [UsedImplicitly]
    public class WeaponReloaderAmmoPatch2 {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(WeaponReloaderAmmo).GetMethod(nameof(WeaponReloaderAmmo.UnloadAmmo), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        [UsedImplicitly]
        public static bool Prefix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = true;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
            return true;
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = false;
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
        }
    }
}