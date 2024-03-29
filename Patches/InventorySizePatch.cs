using System.Collections.Generic;
using System.Reflection;
using Base_Mod;
using Base_Mod.Models;
using HarmonyLib;
using JetBrains.Annotations;

namespace GMod.Patches;

public static class InventorySizePatch {
    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var inventory in RuntimeAssetDatabase.Get<ItemDefinition>().WithComponent<Inventory>()) {
            inventory.m_capacity = (int) (inventory.m_capacity * Plugin.config.inventorySizeMultiplier);
        }
        foreach (var coreItemDef in RuntimeAssetDatabase.Get<TrainCoreItemDefinition>()) {
            coreItemDef.InventoryCapacity = (int) (coreItemDef.InventoryCapacity * Plugin.config.inventorySizeMultiplier);
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