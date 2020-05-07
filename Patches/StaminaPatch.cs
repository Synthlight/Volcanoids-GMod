using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class StaminaPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(Stamina).GetProperty(nameof(Stamina.CurrentStamina), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [HarmonyPrefix]
        public static bool Prefix(ref float __result, ref float ___m_maxStamina) {
            try {
                if (GMod.config.infiniteStamina) {
                    __result = ___m_maxStamina;
                    return false;
                }
            } catch (Exception e) {
                GMod.Log(LogLevel.Error, e.ToString());
            }
            return true;
        }
    }
}