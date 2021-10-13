using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class StackSizeMultiplierPatch {
        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var itemDef in RuntimeAssetDatabase.Get<ItemDefinition>().Where(item => item.MaxStack > 1)) {
                itemDef.MaxStack = (int) (itemDef.MaxStack * Plugin.config.stackSizeMultiplier);
            }
        }
    }
}