using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    public class MapTravelUiPatch {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() {
            return typeof(MapTravelUi).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        public static void Postfix() {
            if (Plugin.config.revealFullMapOnTravel) {
                Island.Current.TryGetComponentSafe<MapMemory>()?.RevealAll();
            }
        }
    }
}