using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod.Patches {
    [HarmonyPatch]
    public class ToolCameraTravelPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(ToolCameraTravel).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPrefix]
        public static bool Prefix() {
            try {
                return !Plugin.config.disableAimSway;
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
                return true;
            }
        }
    }
}