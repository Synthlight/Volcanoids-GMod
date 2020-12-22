using System;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    public class HelpCrosshairUiPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(HelpCrosshairUi).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        public static void Postfix(ref GameObject ___m_newHelp, ref GameObject ___m_knownHelp) {
            try {
                if (Plugin.config.hideHelpIconNearCrosshair) {
                    ___m_newHelp.SetActive(false);
                    ___m_knownHelp.SetActive(false);
                }
            } catch (Exception e) {
                Plugin.Log(LogLevel.Error, e.ToString());
            }
        }
    }
}