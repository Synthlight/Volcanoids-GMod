using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches;

public static class ProductionCostMultiplierPatch {
    [OnIslandSceneLoaded]
    [UsedImplicitly]
    public static void Patch() {
        foreach (var recipeDef in RuntimeAssetDatabase.Get<Recipe>().Where(recipeDef => recipeDef.Inputs.Length > 0)) {
            for (var i = 0; i < recipeDef.Inputs.Length; i++) {
                // It's a struct, so, copy, edit, assign back.
                var input = recipeDef.Inputs[i];
                if (input.Amount > 0) {
                    var newAmount                = (int) (input.Amount * Plugin.config.productionCostMultiplier);
                    if (newAmount < 1) newAmount = 1;
                    input.Amount        = newAmount;
                    recipeDef.Inputs[i] = input;
                }
            }
        }
    }
}