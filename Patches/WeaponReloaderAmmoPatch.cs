using System.Reflection;
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
            DevSettings.Instance.InfiniteAmmo = true;
            return true;
        }

        [HarmonyPostfix]
        public static void Postfix() {
            DevSettings.Instance.InfiniteAmmo = false;
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
            DevSettings.Instance.InfiniteAmmo = true;
            return true;
        }

        [HarmonyPostfix]
        public static void Postfix() {
            DevSettings.Instance.InfiniteAmmo = false;
        }
    }
}