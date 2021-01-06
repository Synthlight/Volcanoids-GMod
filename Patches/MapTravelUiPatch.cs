using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace GMod.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public class MapTravelUiPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(MapTravelUi).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyPostfix]
        [UsedImplicitly]
        public static void Postfix() {
            if (Plugin.config.revealFullMapOnTravel) {
                Island.Current.TryGetComponentSafe<MapMemory>()?.RevealAll();
            }
        }
    }
}