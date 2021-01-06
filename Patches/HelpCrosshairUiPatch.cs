using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class HelpCrosshairUiPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(HelpCrosshairUi).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix(ref GameObject ___m_newHelp, ref GameObject ___m_knownHelp) {
            try {
                if (Plugin.config.hideHelpIconNearCrosshair) {
                    ___m_newHelp.SetActive(false);
                    ___m_knownHelp.SetActive(false);
                }
            } catch (Exception e) {
                Debug.LogError(e.ToString());
            }
        }
    }
}