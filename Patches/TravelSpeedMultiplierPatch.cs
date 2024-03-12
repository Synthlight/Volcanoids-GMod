using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches;

public static class TravelSpeedMultiplierPatch {
    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var tread in RuntimeAssetDatabase.Get<TrainTracksItemDefinition>()) {
            tread.SurfaceMovementSpeed *= Plugin.config.surfaceTravelSpeedMultiplier;
        }
    }
}