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
            Player.Local.TryGetComponentSafe<MapMemory>()?.RevealAll();
        }
    }
}