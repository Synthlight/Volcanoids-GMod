using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches;

public static class CoreSlotMultiplierPatch {
    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var coreItemDef in RuntimeAssetDatabase.Get<TrainCoreItemDefinition>()) {
            coreItemDef.SlotCount = (int) (coreItemDef.SlotCount * Plugin.config.coreSlotMultiplier);
        }
    }
}