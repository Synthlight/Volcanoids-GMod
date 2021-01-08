using System.Collections.Generic;
using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class NoGunsInTurretRecipesPatch {
        private static readonly GUID TURRET_MODULE_PISTOL  = GUID.Parse("3bf96763d5dfb564aad936a087cb614e");
        private static readonly GUID TURRET_MODULE_SHOTGUN = GUID.Parse("6dcf6c5ca3b8f1741af35a4011cdaa1e");
        private static readonly GUID TURRET_MODULE_SMG     = GUID.Parse("4912009609358ea46bc2ceac554045e6");
        private static readonly GUID TURRET_MODULE_SNIPER  = GUID.Parse("b40d3f9773a3a014c9ce1801b2696538");
        private static readonly GUID TURRET_MODULE_MORTAR  = GUID.Parse("a2a0d7a9fab7dee4f8b2f7a6d0912b5a");
        private static readonly GUID TURRET_MODULE_GATLING = GUID.Parse("4b17c7bc9d6c2ee46b7807292f26b432");

        private static readonly GUID REVOLVER = GUID.Parse("e29b47f8c6c51304fa6a6de445d0990d");
        private static readonly GUID SHOTGUN  = GUID.Parse("40870ff1f1c5f6648beabf356d296a5b");
        private static readonly GUID SMG      = GUID.Parse("96048d612ab1ea246b50745a12b9b05e");
        private static readonly GUID SNIPER   = GUID.Parse("75416daac1a28094bb7b6d8f2d690c99");
        private static readonly GUID MORTAR   = GUID.Parse("da137d36a12c54e46b3a132180b98bb1");
        private static readonly GUID GATLING  = GUID.Parse("0670120db8e3ade4ba19a1928b19ce3d");

        private static readonly GUID COPPER_GUN_COMPONENTS = GUID.Parse("0ee40de42e641e34689855fcd200f82a");

        private static readonly List<GUID> TURRETS = new List<GUID> {
            TURRET_MODULE_PISTOL,
            TURRET_MODULE_SHOTGUN,
            TURRET_MODULE_SMG,
            TURRET_MODULE_SNIPER,
            TURRET_MODULE_MORTAR,
            TURRET_MODULE_GATLING
        };

        private static readonly List<GUID> WEAPONS = new List<GUID> {
            REVOLVER,
            SHOTGUN,
            SMG,
            SNIPER,
            MORTAR,
            GATLING
        };

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            if (!Plugin.config.changeTurretRecipes) return;

            var copperGunComponents = GameResources.Instance.Items.FirstOrDefault(item => item.AssetId == COPPER_GUN_COMPONENTS);

            foreach (var recipeDef in GameResources.Instance.Recipes.Where(recipe => TURRETS.Contains(recipe.Output.Item.AssetId))) {
                for (var i = 0; i < recipeDef.Inputs.Length; i++) {
                    var input = recipeDef.Inputs[i];

                    if (WEAPONS.Contains(input.Item.AssetId)) {
                        input.Amount        = GetComponentCost(input.Item.AssetId);
                        input.Item          = copperGunComponents;
                        recipeDef.Inputs[i] = input;
                    }
                }
            }
        }

        private static int GetComponentCost(GUID assetId) {
            if (assetId == REVOLVER) {
                return 1;
            }
            if (assetId == SHOTGUN) {
                return 2;
            }
            if (assetId == SMG || assetId == MORTAR) {
                return 4;
            }
            if (assetId == SNIPER || assetId == GATLING) {
                return 5;
            }
            return 1;
        }
    }
}