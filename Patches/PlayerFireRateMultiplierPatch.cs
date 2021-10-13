using System.Collections.Generic;
using System.Linq;
using Base_Mod.Models;
using JetBrains.Annotations;

namespace GMod.Patches {
    public static class PlayerFireRateMultiplierPatch {
        private static readonly List<GUID> PLAYER_AMMO_ITEMS = new List<GUID> {
            GUID.Parse("25ef235fa1e492441844c9fc34f5cb47"), // Revolver ammo
            GUID.Parse("55468dc2f73c108439216ba9093c549e"), // Revolver high-power ammo
            GUID.Parse("94b9be8c248e72048ba0618864aa0611"), // Explosive revolver ammo
            GUID.Parse("c7b01a0232ae53e478f0b4b92031c416"), // Buckshot
            GUID.Parse("84fdc501781979841a5e78d3a4bdfe03"), // Slugs
            GUID.Parse("6b328e39137971946aec0befbd285190"), // AP Slugs
            GUID.Parse("c7f58533040c3c646b6ea331e8dd0e76"), // SMG Ammo
            GUID.Parse("52c87180c4ed42c40903a92b40b127f5"), // SMG Ammo T2
            GUID.Parse("ee1e9c8eb67fd2544982afda0ee6dcf2"), // Sniper Ammo
            GUID.Parse("721881fe35216b3468df695b3d23acda"), // Mortar grenade
            GUID.Parse("57337a97eb81c7f4ab9f355ddc0d8fdc"), // High-explosive mortar grenade
            GUID.Parse("0c215781eafd06a4ead9810f8800e13a"), // Gatling ammo
        };

        [OnIslandSceneLoaded]
        [UsedImplicitly]
        public static void Patch() {
            foreach (var ammoDef in RuntimeAssetDatabase.Get<ItemDefinition>().Where(def => PLAYER_AMMO_ITEMS.Contains(def.AssetId)).Cast<AmmoDefinition>()) {
                var stats = ammoDef.AmmoStats;
                stats.RateOfFire  *= Plugin.config.playerFireRateMultiplier;
                ammoDef.AmmoStats =  stats;
            }
        }
    }
}