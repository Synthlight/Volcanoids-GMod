using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class SegmentSizeMultiplierPatch {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var segmentItemDef in RuntimeAssetDatabase.Get<TrainEngineItemDefinition>()) {
                segmentItemDef.SegmentCount = (int) (segmentItemDef.SegmentCount * Plugin.config.segmentSizeMultiplier);
            }
        }
    }
}