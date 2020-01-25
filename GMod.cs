using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace GMod {
    [BepInPlugin(UUID, "Greg's Mod", "1.0.0.0")]
    public class GMod : BaseUnityPlugin {
        private const string UUID = "com.gmod";
        private static ManualLogSource logSource;

        private static Dictionary<string, int> itemSortOrders = new Dictionary<string, int>();

        static GMod() {
            var i = 0;
            itemSortOrders.Add("Revolver", i++);
            itemSortOrders.Add("Revolver ammo", i++);
            itemSortOrders.Add("Medkit", i++);
            itemSortOrders.Add("Module repair kit", i++);
        }

        public static int GetItemSortOrder(string item) {
            if (itemSortOrders.ContainsKey(item)) {
                return itemSortOrders[item];
            } else {
                return 9999;
            }
        }

        public void Awake() {
            logSource = Logger;

            Log(LogLevel.Info, "GMod loaded.");

            var harmony = new Harmony(UUID);
            harmony.PatchAll();

            foreach (var patchedMethod in harmony.GetPatchedMethods()) {
                Log(LogLevel.Info, $"Patched: {patchedMethod.DeclaringType?.FullName}:{patchedMethod}");
            }
        }

        public static void Log(LogLevel level, string msg) {
            logSource.Log(level, msg);
        }
    }
}