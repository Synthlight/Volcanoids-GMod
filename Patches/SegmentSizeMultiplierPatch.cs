using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class SegmentSizeMultiplierPatch {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var segmentItemDef in GameResources.Instance.Items.OfType<TrainEngineItemDefinition>()) {
                segmentItemDef.SegmentCount *= Plugin.config.segmentSizeMultiplier;
            }
        }
    }
}