using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class ProductionSpeedMultiplierPatch {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var recipeDef in RuntimeAssetDatabase.Get<Recipe>().Where(recipeDef => recipeDef.ProductionTime > 0)) {
                recipeDef.ProductionTime *= Plugin.config.productionSpeedMultiplier;
            }
        }
    }
}