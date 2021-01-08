using System.Collections.Generic;
using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class StackSizeMultiplierPatch {
        private static readonly GUID COAL_ORE = GUID.Parse("e89c0cdbf40a07f4ebb20d2865717a52");

        private static readonly GUID COPPER_ORE    = GUID.Parse("d1c5ccba9f65302408be2e46975020e7");
        private static readonly GUID COPPER_INGOT  = GUID.Parse("9386d447191c7aa4fa913943abd75481");
        private static readonly GUID SULFUR_ORE    = GUID.Parse("9ffb5e60912dc6a4280c317dbdbcab52");
        private static readonly GUID SULFUR_POWDER = GUID.Parse("156562c6e6475dd4eb885291a4662a6f");

        private static readonly GUID IRON_ORE      = GUID.Parse("8184e2f7fbec373469f650ff3febcf99");
        private static readonly GUID IRON_INGOT    = GUID.Parse("f74e4356dcd38b7418a666b63c0304b1");
        private static readonly GUID CRYSTAL_ORE   = GUID.Parse("1846377ac119fd64ab8fd73dccfe0923");
        private static readonly GUID CRYSTAL_INGOT = GUID.Parse("63e5ebe76f201c549b93e73a3be1044a");

        private static readonly GUID TITANIUM_ORE   = GUID.Parse("3d4fce1d9e298384ea12a6e66fa92ba2");
        private static readonly GUID TITANIUM_INGOT = GUID.Parse("c3d1b45a69512824fbade5378c5a4a52");
        private static readonly GUID DIAMOND_ORE    = GUID.Parse("98244c77b188a0740a2ba3df0dfab2fe");
        private static readonly GUID DIAMOND_INGOT  = GUID.Parse("7ba2d6c7d6bf2eb41b9732aa1b8f2396");

        private static readonly GUID ALLOY_T1_INGOT = GUID.Parse("8cbb0001e180fe241b3da6ef1018b3df");
        private static readonly GUID ALLOY_T2_INGOT = GUID.Parse("7d475c9d18e267f41b82f736ed19a194");
        private static readonly GUID ALLOY_T3_INGOT = GUID.Parse("13fbc577a43e7f34e914b3d8d8832c16");

        private static readonly List<GUID> ORES_AND_INGOTS = new List<GUID> {
            COAL_ORE,
            COPPER_ORE,
            COPPER_INGOT,
            SULFUR_ORE,
            SULFUR_POWDER,
            IRON_ORE,
            IRON_INGOT,
            CRYSTAL_ORE,
            CRYSTAL_INGOT,
            TITANIUM_ORE,
            TITANIUM_INGOT,
            DIAMOND_ORE,
            DIAMOND_INGOT,
            ALLOY_T1_INGOT,
            ALLOY_T2_INGOT,
            ALLOY_T3_INGOT
        };

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var itemDef in GameResources.Instance.Items.Where(item => ORES_AND_INGOTS.Contains(item.AssetId))) {
                itemDef.MaxStack = (int) (itemDef.MaxStack * Plugin.config.oreAndIngotStackSizeMultiplier);
            }
        }
    }
}