using System.Collections.Generic;
using System.Reflection;
using Base_Mod;
using Base_Mod.Models;
using HarmonyLib;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class InventorySizePatch {
        private static readonly FieldInfo INVENTORY_M_CAPACITY = typeof(Inventory).GetField("m_capacity", BindingFlags.NonPublic | BindingFlags.Instance);

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var inventory in GameResources.Instance.Items.WithComponent<Inventory>()) {
                inventory.SetPrivateField("m_capacity", (int) (inventory.GetPrivateField<int>(INVENTORY_M_CAPACITY) * Plugin.config.inventorySizeMultiplier));
            }
        }
    }

    [HarmonyPatch]
    [UsedImplicitly]
    public static class InventoryMaxCapacityPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(Inventory).GetProperty(nameof(Inventory.Capacity), BindingFlags.Public | BindingFlags.Instance)?.GetGetMethod();
        }

        [UsedImplicitly]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            foreach (var instruction in instructions) {
                if (instruction.operand?.Equals(128) ?? false) {
                    instruction.operand = int.MaxValue;
                }

                yield return instruction;
            }
        }
    }
}