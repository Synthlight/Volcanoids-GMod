using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class WeaponReloaderAmmoPatch1 {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(WeaponReloaderAmmo).GetMethod(nameof(WeaponReloaderAmmo.LoadAmmo), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = true;
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }

        [HarmonyPostfix]
        public static void Postfix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = false;
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
        }
    }

    [HarmonyPatch]
    public class WeaponReloaderAmmoPatch2 {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(WeaponReloaderAmmo).GetMethod(nameof(WeaponReloaderAmmo.UnloadAmmo), BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = true;
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }

        [HarmonyPostfix]
        public static void Postfix() {
            try {
                if (Plugin.config.infiniteAmmo) {
                    DevSettings.Instance.InfiniteAmmo = false;
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
        }
    }
}